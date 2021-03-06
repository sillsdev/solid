// Copyright (c) 2007-2014 SIL International
// Licensed under the MIT license: opensource.org/licenses/MIT

using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using SIL.Reporting;
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

        public SfmLexEntry Process(SfmLexEntry lexEntry, SolidReport report)  //JMC: void would make more sense
        {
            Encoding utf8Encoding = Encoding.GetEncoding("utf-8", new EncoderExceptionFallback(), new DecoderExceptionFallback());
            Encoding legacyEncoding = SolidSettings.LegacyEncoding;  //was: Encoding.GetEncoding("iso-8859-1");


            //JMC:! Can this method be deleted? It overlaps with SfmDictionary.ReadDictionary() and thus with:
            // filter.Add(entry.Marker, CreateSolidErrorRecordFilter(entry.EntryType, entry.Marker));

            // Iterate through each (flat) node in the src d
            foreach (SfmFieldModel sfmField in lexEntry.Fields)
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
                            string convertedString = utf8Encoding.GetString(legacyEncoding.GetBytes(value));
                        }
                        catch  // (Exception e)
                        {
                            // string tmp = String.Format("  ProcessEncoding: ignoring exception: {0}", e);
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
                                String.Format("Field \\{0} may use a hacked font", sfmField.Marker)
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