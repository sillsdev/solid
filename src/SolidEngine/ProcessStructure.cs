using System;
using System.Collections.Generic;
using System.Xml;

namespace SolidEngine
{
    public class ProcessStructure : IProcess
    {
        SolidSettings _settings;

        private string[] _mapNames = new string[(int)SolidMarkerSetting.MappingType.Max];

        public ProcessStructure(SolidSettings settings)
        {
            _settings = settings;
            _mapNames[(int)SolidMarkerSetting.MappingType.Lift] = "lift";
            _mapNames[(int)SolidMarkerSetting.MappingType.Flex] = "flex";

        }

        private void InsertInTreeAnyway(XmlNode source, XmlDocument destination, SolidReport report, List<XmlNode> scope)
        {
            
            SolidMarkerSetting setting = _settings.FindMarkerSetting(source.Name);
           
            int i = 0;
            
            XmlHelper xh = new XmlHelper(scope[scope.Count - 1]);
            SolidReport.Entry e = (report.GetEntryById(Convert.ToInt32(xh.GetAttribute("field"))));
            
            int level = (e != null) ? 2 : 1;
            
            if (scope.Count >= level)
            {
                i = scope.Count - level; //!!! Bit hacky.
            }

            TruncateScope(i, scope);
            // Add the node under scope[i]
            XmlNode fieldNode = CreateFieldNode(source, setting, destination);
            XmlNode n = scope[i].AppendChild(fieldNode);
            scope.Add(n);
        }

        private bool InsertInTree(XmlNode source, XmlDocument destination, SolidReport report, List<XmlNode> scope)
        {
            // Get the marker settings for this node.
            SolidMarkerSetting setting = _settings.FindMarkerSetting(source.Name);
            bool foundParent = false;
            int i = scope.Count;
            // Check for record marker (assume is root)
            while (i > 0 && !foundParent)
            {
                i--;
                SolidStructureProperty structureProperty = setting.getStructureProperty(scope[i].Name);
                if (structureProperty != null)
                {
                    if (i == scope.Count - 1)
                    {
                        foundParent = true;
                    }
                    else if (scope[i + 1].Name == setting.Marker &&
                             structureProperty.MultipleAdjacent != SolidGui.MultiplicityAdjacency.Once)
                    {
                        foundParent = true;
                    }
                    else if (structureProperty.MultipleAdjacent == SolidGui.MultiplicityAdjacency.Once)
                    {
                        foundParent = true;

                        //make sure the parent doesn't allready contain the node we want to add
                        foreach (XmlNode childNode in scope[i].ChildNodes)
                        {
                            if (childNode.Name == source.Name)
                                foundParent = false;
                        }
                    }
                    else if (structureProperty.MultipleAdjacent == SolidGui.MultiplicityAdjacency.MultipleApart)
                    {
                        foundParent = true;
                    }
                }
            }

            if(foundParent)
            {
                // Add the node under this parent
                XmlNode fieldNode = CreateFieldNode(source, setting, destination);
                XmlNode fieldNodeInTree = scope[i].AppendChild(fieldNode);
                UpdateScope(scope, i, fieldNodeInTree);
            }
            
            return foundParent;
        }

        private void UpdateScope(List<XmlNode> scope, int i, XmlNode n)
        {
            TruncateScope(i, scope);
            scope.Add(n);
        }

        private void TruncateScope(int i, List<XmlNode> scope)
        {
            if (i < scope.Count - 1)
            {
                scope.RemoveRange(i + 1, scope.Count - i - 1);
            }
        }

        private XmlNode CreateFieldNode(XmlNode source, SolidMarkerSetting setting, XmlDocument destination)
        {
            XmlNode fieldNode = destination.ImportNode(source, true);
            
            for (int j = 0; j < setting.Mapping.Length; j++)
            {
                if (setting.Mapping[j] != null && setting.Mapping[j] != String.Empty)
                {
                    XmlAttribute attribute = destination.CreateAttribute(_mapNames[j]);
                    attribute.Value = setting.Mapping[j];
                    fieldNode.Attributes.Append(attribute);
                }
            }


            XmlNode dataNode = destination.CreateElement("data");
            if (fieldNode.FirstChild != null)
            {
                dataNode.AppendChild(fieldNode.FirstChild);
            }
            fieldNode.AppendChild(dataNode);
            return fieldNode;
        }

        public XmlNode Process(XmlNode source, SolidReport report)
        {
            XmlDocument destination = new XmlDocument();
            // Iterate through each (flat) node in the src d
            List<XmlNode> scope = new List<XmlNode>();
            scope.Add(destination.AppendChild(destination.ImportNode(source, false)));
            XmlNode field = source.FirstChild;
            int fieldId = 0;
            while (field != null)
            {
                SolidMarkerSetting setting = _settings.FindMarkerSetting(field.Name);
                if (!InsertInTree(field, destination, report, scope))
                {
                    // Can we infer a node.
                    if (setting.InferedParent != String.Empty)
                    {
                        XmlNode inferredNode = destination.CreateElement(setting.InferedParent);
                        XmlNode attribute = inferredNode.Attributes.Append(destination.CreateAttribute("inferred"));
                        attribute.Value = "true";
                        // Attempt to insert the inferred node in the tree.
                        // The inferred node needs to find a valid parent
                        if (InsertInTree(inferredNode, destination, report, scope))
                        {
                            //inferredNode.Attributes["inferred"].Value = "true";
                            // Now try and add the current node under the inferred node.
                            if (!InsertInTree(field, destination, report, scope))
                            {
                                //??? This is bordering on an exception. It indicates that there is an inconsistency with the seutp.
                                // Error.
                                report.AddEntry(
                                    SolidReport.EntryType.StructureInsertInInferredFailed,
                                    source,
                                    field,
                                    String.Format("Inferred marker '{0}' is not a valid parent of '{1}'", setting.InferedParent, field.Name)
                                );
                                InsertInTreeAnyway(field, destination, report, scope);
                            }
                        }
                        else
                        {
                            // Error.
                            report.AddEntry(
                                SolidReport.EntryType.StructureParentNotFoundForInferred,
                                source,
                                field,
                                "Inferred marker {0} could not be placed in structure." + String.Format("'{0}'", inferredNode.Name)
                            );
                            InsertInTreeAnyway(field, destination, report, scope);
                        }
                    } 
                    else
                    {
                        // Error
                        report.AddEntry(
                            SolidReport.EntryType.StructureParentNotFound,
                            source,
                            field,
                            string.Format("Marker {0} could not be placed in structure, and nothing could be inferred.", field.Name)
                        );
                        InsertInTreeAnyway(field, destination, report, scope);
                    }
                }

                fieldId++;
                field = field.NextSibling;
            }
            return destination.DocumentElement;
        }
    }
}
