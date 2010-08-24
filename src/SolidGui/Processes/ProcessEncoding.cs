using System;
using System.Xml;
using SolidGui.Engine;

namespace SolidGui.Engine
{
    public class ProcessEncoding : IProcess
    {
        readonly SolidSettings _settings;

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
                if (xmlField.FirstChild != null)
                {
                    SolidMarkerSetting setting = _settings.FindOrCreateMarkerSetting(xmlField.Name);
                    string value = xmlField.FirstChild.Value;
                    if (setting.Unicode)
                    {
                        // Confirm that the value is in valid unicode encoded as UTF-8
                        if (value.Length > 0)
                        {
                            bool isValid = true;
                            int remaining = 0;
                            for (int i = 0; i < value.Length && isValid; ++i)
                            {
                                if (remaining > 0) 
                                {
                                    isValid = (value[i] & 0xC0) == 0x80;
                                    --remaining;
                                }
                                else 
                                {
                                    if ((value[i] & 0xF8) == 0xF0) 
                                    {
                                        remaining = 3;
                                    }
                                    else if ((value[i] & 0xF0) == 0xE0)
                                    {
                                        remaining = 2;
                                    }
                                    else if ((value[i] & 0xE0) == 0xC0)
                                    {
                                        remaining = 1;
                                    }
                                    else if ((value[i] & 0x80) == 0x00)
                                    {
                                        remaining = 0;
                                    }
                                    else
                                    {
                                        isValid = false;
                                    }
                                }
                            }

                            if (!isValid)
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