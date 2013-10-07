using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Palaso.Reporting;
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
            var utf8Encoding = Encoding.GetEncoding("utf-8", new EncoderExceptionFallback(), new DecoderExceptionFallback());
            var iso88591Encoding = Encoding.GetEncoding("iso-8859-1");
            
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
                        try
                        {
                            var convertedString = utf8Encoding.GetString(iso88591Encoding.GetBytes(value));
                        }
                        catch(Exception e)
                        {
                            // string tmp = String.Format("  ProcessEncoding: ignoring exception: {0}", e);
                            // Logger.WriteEvent(tmp); // Started to log this... never mind; looks like this is this a safe exception to ignore... -JMC
                            // Logger.WriteEvent(tmp); // Started to log this... never mind; looks like this is this a safe exception to ignore... -JMC
                            // Logger.WriteEvent(tmp); // Started to log this... never mind; looks like this is this a safe exception to ignore... -JMC
                            // Logger.WriteEvent(tmp); // Started to log this... never mind; looks like this is this a safe exception to ignore... -JMC
                            isValid = false;  
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