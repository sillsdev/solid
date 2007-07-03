using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Diagnostics;

namespace SolidEngine
{
    public class ProcessStructure
    {
        SolidReport _report;
        SolidSettings _settings;

        string[] _mapNames = new string[(int)SolidMarkerSetting.MappingType.Max];

        public ProcessStructure(SolidReport report, SolidSettings settings)
        {
            _report = report;
            _settings = settings;
            _mapNames[(int)SolidMarkerSetting.MappingType.Lift] = "lift";
            _mapNames[(int)SolidMarkerSetting.MappingType.Flex] = "flex";

        }

        private bool InsertInTree(XmlNode src, XmlDocument xmlOut, List<XmlNode> scope)
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
                    XmlNode fieldNode = xmlOut.ImportNode(src, true);
                    for (int j = 0; j < setting.Mapping.Length; j++)
                    {
                        if (setting.Mapping[j] != null && setting.Mapping[j] != String.Empty)
                        {
                            XmlAttribute attribute = xmlOut.CreateAttribute(_mapNames[j]);
                            attribute.Value = setting.Mapping[j];
                            fieldNode.Attributes.Append(attribute);
                        }
                    }
                    XmlNode dataNode = xmlOut.CreateElement("data");
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

        public XmlNode Process(XmlNode entry)
        {
            XmlDocument xmlOut = new XmlDocument();
            // Iterate through each (flat) node in the src d
            List<XmlNode> scope = new List<XmlNode>();
            scope.Add(xmlOut.AppendChild(xmlOut.CreateElement("root")));
            XmlNode field = entry.FirstChild;
            while (field != null)
            {
                SolidMarkerSetting setting = _settings.FindMarkerSetting(field.Name);
                if (!InsertInTree(field, xmlOut, scope))
                {
                    // Can we infer a node.
                    if (setting.InferedParent != String.Empty)
                    {
                        XmlNode inferredNode = xmlOut.CreateElement(setting.InferedParent);
                        XmlNode attribute = inferredNode.Attributes.Append(xmlOut.CreateAttribute("inferred"));
                        attribute.Value = "true";
                        // Attempt to insert the inferred node in the tree.
                        // The inferred node needs to find a valid parent
                        if (InsertInTree(inferredNode, xmlOut, scope))
                        {
                            //inferredNode.Attributes["inferred"].Value = "true";
                            // Now try and add the current node under the inferred node.
                            if (!InsertInTree(field, xmlOut, scope))
                            {
                                //??? This is bordering on an exception. It indicates that there is an inconsistency with the seutp.
                                // Error.
                                _report.Add(new SolidReport.Entry(
                                    SolidReport.EntryType.Error,
                                    entry,
                                    field,
                                    "Cannot insert field into inferred parent"
                                ));
                            }
                        }
                        else
                        {
                            // Error.
                            _report.Add(new SolidReport.Entry(
                                SolidReport.EntryType.Error,
                                entry,
                                inferredNode,
                                "No parent for inferred marker"
                            ));
                        }
                    } 
                    else
                    {
                        // Error
                        _report.Add(new SolidReport.Entry(
                            SolidReport.EntryType.Error,
                            entry,
                            field,
                            "No parent for this field and none could be inferred"
                        ));
                    }
                }
                field = field.NextSibling;
            }
            return xmlOut;
        }
    }
}
