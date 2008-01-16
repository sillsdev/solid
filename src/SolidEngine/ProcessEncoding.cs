using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace SolidEngine
{
    public class ProcessEncoding : IProcess
    {
        SolidSettings _settings;

        public ProcessEncoding(SolidSettings settings)
        {
            _settings = settings;
        }

        public XmlNode Process(XmlNode xmlEntry, SolidReport report)
        {
            // Iterate through each (flat) node in the src d
            XmlNode xmlField = xmlEntry.FirstChild;
            while (xmlField != null)
            {
                string marker = xmlField.Name;
                if (xmlField.FirstChild != null)
                {
                    SolidMarkerSetting setting = _settings.FindMarkerSetting(xmlField.Name);
                    string value = xmlField.FirstChild.Value;
                    if (setting.Unicode)
                    {
                        // Confirm that the value is in valid unicode
                        if (value.Length > 0)
                        {
                            Encoding byteEncoding = Encoding.GetEncoding("iso-8859-1");
                            byte[] valueAsBytes = byteEncoding.GetBytes(value);
                            Encoding stringEncoding = Encoding.UTF8;
                            string valueAsUnicode = stringEncoding.GetString(valueAsBytes);
                            if (valueAsUnicode.Length == 0)
                            {
                                report.AddEntry(
                                    SolidReport.EntryType.EncodingBadUnicode,
                                    xmlEntry,
                                    xmlField,
                                    String.Format("Marker \\{0} contains bad unicode data", xmlField.Name)
                                );
                            }
                        }
                    }
                    else
                    {
                        // Check for hacked fonts (data 0x80 to 0xff)
                        foreach (char c in value)
                        {
                            if (c >= 0x0080)
                            {
                                report.AddEntry(
                                    SolidReport.EntryType.EncodingUpperAscii,
                                    xmlEntry,
                                    xmlField,
                                    String.Format("Marker \\{0} may use a hacked font", xmlField.Name)
                                );
                                break;
                            }
                        }
                    }
                }
                xmlField = xmlField.NextSibling;
            }
            return xmlEntry;
        }
    }
}
