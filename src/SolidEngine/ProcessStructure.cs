using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Diagnostics;

namespace SolidEngine
{
    public class ProcessStructure
    {
        SolidSettings _settings;

        string[] _mapNames = new string[(int)SolidMarkerSetting.MappingType.Max];

        private SolidReport _report;
        private XmlDocument _document;

        public SolidReport Report
        {
            get { return _report; }
            set { _report = value; }
        }


        public XmlNode Document
        {
            get { return _document; }
        }

        public ProcessStructure(SolidSettings settings)
        {
            _settings = settings;
            _mapNames[(int)SolidMarkerSetting.MappingType.Lift] = "lift";
            _mapNames[(int)SolidMarkerSetting.MappingType.Flex] = "flex";

        }

        private void InsertInTreeAnyway(XmlNode src, List<XmlNode> scope)
        {
            // Get the marker settings for this node.
            SolidMarkerSetting setting = _settings.FindMarkerSetting(src.Name);
            // Insert src as sibling of the last element in scope.
            int i = scope.Count - 2; //!!! Bit hacky. Want to insert under the second to last in scope.
            // Truncate the scope
            if (i < scope.Count - 1)
            {
                scope.RemoveRange(i + 1, scope.Count - i);
            }
            // Add the node under this parent
            XmlNode fieldNode = _document.ImportNode(src, true);
            for (int j = 0; j < setting.Mapping.Length; j++)
            {
                if (setting.Mapping[j] != null && setting.Mapping[j] != String.Empty)
                {
                    XmlAttribute attribute = _document.CreateAttribute(_mapNames[j]);
                    attribute.Value = setting.Mapping[j];
                    fieldNode.Attributes.Append(attribute);
                }
            }
            XmlNode dataNode = _document.CreateElement("data");
            if (fieldNode.FirstChild != null)
            {
                dataNode.AppendChild(fieldNode.FirstChild);
            }
            fieldNode.AppendChild(dataNode);
            XmlNode n = scope[i].AppendChild(fieldNode);
            // Add this node to the scope
            scope.Add(n);

        }

        private bool InsertInTree(XmlNode src, List<XmlNode> scope)
        {
            // Get the marker settings for this node.
            SolidMarkerSetting setting = _settings.FindMarkerSetting(src.Name);
            bool foundParent = false;
            // Check for record marker (assume is root)
            for (int i = scope.Count - 1; !foundParent && i >= 0; i--)
            {
                if (setting.ParentExists(scope[i].Name))
                {
                    foundParent = true;
                    // Truncate the scope
                    if (i < scope.Count - 1)
                    {
                        scope.RemoveRange(i + 1, scope.Count - i);
                    }
                    // Add the node under this parent
                    XmlNode fieldNode = _document.ImportNode(src, true);
                    for (int j = 0; j < setting.Mapping.Length; j++)
                    {
                        if (setting.Mapping[j] != null && setting.Mapping[j] != String.Empty)
                        {
                            XmlAttribute attribute = _document.CreateAttribute(_mapNames[j]);
                            attribute.Value = setting.Mapping[j];
                            fieldNode.Attributes.Append(attribute);
                        }
                    }
                    XmlNode dataNode = _document.CreateElement("data");
                    if (fieldNode.FirstChild != null)
                    {
                        dataNode.AppendChild(fieldNode.FirstChild);
                    }
                    fieldNode.AppendChild(dataNode);
                    XmlNode n = scope[i].AppendChild(fieldNode);
                    // Add this node to the scope
                    scope.Add(n);
                }
            }
            return foundParent;
        }

        public void Process(XmlNode entry)
        {
            _document = new XmlDocument();
            _report = new SolidReport();
            // Iterate through each (flat) node in the src d
            List<XmlNode> scope = new List<XmlNode>();
            scope.Add(_document.AppendChild(_document.CreateElement("root")));
            XmlNode field = entry.FirstChild;
            while (field != null)
            {
                SolidMarkerSetting setting = _settings.FindMarkerSetting(field.Name);
                if (!InsertInTree(field, scope))
                {
                    // Can we infer a node.
                    //!!! TODO Add xpath here to check for conditions based on adjacency etc.
                    if (setting.InferedParent != String.Empty)
                    {
                        XmlNode inferredNode = _document.CreateElement(setting.InferedParent);
                        XmlNode attribute = inferredNode.Attributes.Append(_document.CreateAttribute("inferred"));
                        attribute.Value = "true";
                        // Attempt to insert the inferred node in the tree.
                        // The inferred node needs to find a valid parent
                        if (InsertInTree(inferredNode, scope))
                        {
                            //inferredNode.Attributes["inferred"].Value = "true";
                            // Now try and add the current node under the inferred node.
                            if (!InsertInTree(field, scope))
                            {
                                //??? This is bordering on an exception. It indicates that there is an inconsistency with the seutp.
                                // Error.
                                _report.AddEntry(
                                    SolidReport.EntryType.StructureInsertInInferredFailed,
                                    entry,
                                    field,
                                    String.Format("Inferred marker '{0}' is not a valid parent of '{1}'", setting.InferedParent, field.Name)
                                );
                                InsertInTreeAnyway(field, scope);
                            }
                        }
                        else
                        {
                            // Error.
                            _report.AddEntry(
                                SolidReport.EntryType.StructureParentNotFoundForInferred,
                                entry,
                                field,
                                "No parent found for inferred marker " + String.Format("'{0}'", inferredNode.Name)
                            );
                            InsertInTreeAnyway(field, scope);
                        }
                    } 
                    else
                    {
                        // Error
                        _report.AddEntry(
                            SolidReport.EntryType.StructureParentNotFound,
                            entry,
                            field,
                            "No parent for this field and none could be inferred"
                        );
                        InsertInTreeAnyway(field, scope);
                    }
                }
                field = field.NextSibling;
            }
        }
    }
}
