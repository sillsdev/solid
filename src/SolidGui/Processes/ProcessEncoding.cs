using System;
using System.Xml;
using SolidGui.Engine;
using SolidGui.Model;

namespace SolidGui.Processes
{
    public class ProcessEncoding : IProcess
    {
        readonly SolidSettings _settings;

        public ProcessEncoding(SolidSettings settings)
        {
            _settings = settings;
        }

        public SfmLexEntry Process(SfmLexEntry lexEntry, SolidReport report)
        {
            // Iterate through each (flat) node in the src d
            foreach (var sfmField in lexEntry.Fields)
            {
                SolidMarkerSetting setting = _settings.FindOrCreateMarkerSetting(sfmField.Marker);
                string value = sfmField.Value;
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
                                lexEntry,
                                sfmField,
                                String.Format("Marker \\{0} contains bad unicode data", sfmField.Marker)
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
                                lexEntry,
                                sfmField,
                                String.Format("Marker \\{0} may use a hacked font", sfmField.Marker)
                                );
                            break;
                        }
                    }
                }

            }
            return lexEntry;
        }
    }
}