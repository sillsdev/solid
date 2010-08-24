using System;
using System.Collections.Generic;
using System.Xml;
using SolidGui.Engine;

namespace SolidGui.Processes
{
    public class ProcessStructure : IProcess
    {
        SolidSettings _settings;

        private string[] _mapNames = new string[(int)SolidMarkerSetting.MappingType.Max];

        public ProcessStructure(SolidSettings settings)
        {
            _settings = settings;
            _mapNames[(int)SolidMarkerSetting.MappingType.FlexDefunct] = "flex";
            _mapNames[(int)SolidMarkerSetting.MappingType.Lift] = "lift";

        }

        private void InsertInTreeAnyway(XmlNode source, XmlDocument destination, SolidReport report, List<XmlNode> scope)
        {
            
            SolidMarkerSetting setting = _settings.FindOrCreateMarkerSetting(source.Name);
           
            int level = 1;
            int i = scope.Count - level;
            
            XmlHelper xh = new XmlHelper(scope[scope.Count - 1]);
            string inferred = xh.GetAttribute("inferred");
            string field = xh.GetAttribute("field");
            if (inferred != "true" && field != string.Empty)
            {
                SolidReport.Entry e = (report.GetEntryById(Convert.ToInt32(field)));
                level = (e != null) ? 2 : 1;
                if (scope.Count >= level)
                {
                    i = scope.Count - level; //!!! Bit hacky.
                }
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
            SolidMarkerSetting setting = _settings.FindOrCreateMarkerSetting(source.Name);
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
                             structureProperty.MultipleAdjacent != MultiplicityAdjacency.Once)
                    {
                        foundParent = true;
                    }
                    else if (structureProperty.MultipleAdjacent == MultiplicityAdjacency.Once)
                    {
                        foundParent = true;

                        //make sure the parent doesn't allready contain the node we want to add
                        foreach (XmlNode childNode in scope[i].ChildNodes)
                        {
                            if (childNode.Name == source.Name)
                                foundParent = false;
                        }
                    }
                    else if (structureProperty.MultipleAdjacent == MultiplicityAdjacency.MultipleApart)
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
            
            for (int j = 0; j < setting.Mappings.Length; j++)
            {
                if (setting.Mappings[j] != null && setting.Mappings[j] != String.Empty)
                {
                    XmlAttribute mapAttribute = destination.CreateAttribute(_mapNames[j]);
                    mapAttribute.Value = setting.Mappings[j];
                    fieldNode.Attributes.Append(mapAttribute);
                    XmlAttribute writingSystemAttribute = destination.CreateAttribute("writingsystem");
                    if (setting.WritingSystemRfc4646 == "")
                    {
                        writingSystemAttribute.Value = "zxx";
                    }
                    else
                    {
                        writingSystemAttribute.Value = setting.WritingSystemRfc4646;
                    }
                    fieldNode.Attributes.Append(writingSystemAttribute);
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

        private bool InferNode(XmlNode xmlEntry, XmlNode xmlSourceField, XmlDocument destination, SolidReport report, List<XmlNode> scope, ref int recurseCount)
        {
            // Can we infer a node.
            bool retval = false;
            SolidMarkerSetting setting = _settings.FindOrCreateMarkerSetting(xmlSourceField.Name);
            if (setting.InferedParent != String.Empty)
            {
                XmlNode inferredNode = destination.CreateElement(setting.InferedParent);
                XmlNode attribute = inferredNode.Attributes.Append(destination.CreateAttribute("inferred"));
                attribute.Value = "true";
                // Attempt to insert the inferred node in the tree.
                // The inferred node needs to find a valid parent
                if (InsertInTree(inferredNode, destination, report, scope))
                {
                    // Now try and add the current node under the inferred node.
                    if (InsertInTree(xmlSourceField, destination, report, scope))
                    {
                        retval = true;
                    }
                    else
                    {
                        //??? This is bordering on an exception. It indicates that there is an inconsistency with the seutp.
                        // Error.
                        report.AddEntry(
                            SolidReport.EntryType.StructureInsertInInferredFailed,
                            xmlEntry,
                            xmlSourceField,
                            String.Format("Inferred marker \\{0} is not a valid parent of \\{1}", setting.InferedParent, xmlSourceField.Name)
                            );
                        InsertInTreeAnyway(xmlSourceField, destination, report, scope);
                    }
                }
                else
                {
                    if (recurseCount < 10)
                    {
                        if (InferNode(xmlEntry, inferredNode, destination, report, scope, ref recurseCount))
                        {
                            // Now try and add the current node under the inferred node.
                            if (InsertInTree(xmlSourceField, destination, report, scope))
                            {
                                retval = true;
                            }
                            else
                            {
                                //??? This is bordering on an exception. It indicates that there is an inconsistency with the seutp.
                                // Error.
                                report.AddEntry(
                                    SolidReport.EntryType.StructureInsertInInferredFailed,
                                    xmlEntry,
                                    xmlSourceField,
                                    String.Format("Inferred marker \\{0} is not a valid parent of \\{1}", setting.InferedParent, xmlSourceField.Name)
                                    );
                                InsertInTreeAnyway(xmlSourceField, destination, report, scope);
                            }
                        }
                        // No else required, the InferNode puts the entries in the tree.
                    }
                    else
                    {
                        throw new Exception("Circular inference rules detected");
                    }
                    if (!retval)
                    {
                        // Error.
                        report.AddEntry(
                            SolidReport.EntryType.StructureParentNotFoundForInferred,
                            xmlEntry,
                            inferredNode,
                            String.Format("Inferred marker \\{0} could not be placed in structure.", inferredNode.Name)
                            );
                        // Error
                        report.AddEntry(
                            SolidReport.EntryType.StructureParentNotFound,
                            xmlEntry,
                            xmlSourceField,
                            string.Format("Marker \\{0} could not be placed in structure", xmlSourceField.Name)
                            );
                        InsertInTreeAnyway(xmlSourceField, destination, report, scope);
                    }
                }
            }
            else
            {
                // Error
                report.AddEntry(
                    SolidReport.EntryType.StructureParentNotFound,
                    xmlEntry,
                    xmlSourceField,
                    string.Format("Marker \\{0} could not be placed in structure, and nothing could be inferred.", xmlSourceField.Name)
                    );
                InsertInTreeAnyway(xmlSourceField, destination, report, scope);
            }
            return retval;
        }

        public XmlNode Process(XmlNode xmlEntry, SolidReport report)
        {
            XmlDocument destination = new XmlDocument();
            // Iterate through each (flat) node in the src d
            List<XmlNode> scope = new List<XmlNode>();
            scope.Add(destination.AppendChild(destination.ImportNode(xmlEntry, false)));
            XmlNode xmlField = xmlEntry.FirstChild;
            int fieldId = 0;
            while (xmlField != null)
            {
                if (!InsertInTree(xmlField, destination, report, scope))
                {
                    int recurseCount = 0;
                    InferNode(xmlEntry, xmlField, destination, report, scope, ref recurseCount);
                }

                fieldId++;
                xmlField = xmlField.NextSibling;
            }
            return destination.DocumentElement;
        }
    }
}