// Copyright (c) 2007-2014 SIL International
// Licensed under the MIT license: opensource.org/licenses/MIT

using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using SolidGui.Model;

namespace SolidGui.Engine
{
    public class SolidReport
    {
        [Flags]
        public enum EntryType
        {
            EncodingBadUnicode = 0,
            StructureInsertInInferredFailed = 1, 
            StructureParentNotFound = 2, 
            StructureParentNotFoundForInferred = 4,
            EncodingUpperAscii = 8,
            StructureRequiredMissing = 16,
            StructureKickout = 32
        }

        public const int NumTypes = 5;
        // Max was just used to tell how many possible values there are. -JMC Mar 2014
        // Seems better to use [Flags] above and values for bitwise and/or.
        // http://stackoverflow.com/questions/4455719/compare-two-enums-w-bitwise-for-a-single-true-result

        public static bool IsDataWarning (EntryType et)
        {
            var d = (EntryType.EncodingBadUnicode | EntryType.EncodingUpperAscii);
            bool match = (int)(et & d) > 0;
            return match;
        }

        public List<ReportEntry> Entries { get; set; }

        [XmlIgnore]
        public string FilePath { get; set; }

        public SolidReport()
        {
            Entries = new List<ReportEntry>();
            FilePath = "";
        }

        public static SolidReport MakeCopy(SolidReport report)
        {
            SolidReport s = new SolidReport();
            s.Entries = new List<ReportEntry>(report.Entries);
            return s;
        }

        public void Reset()
        {
            Entries.Clear();
        }

        public void Add(ReportEntry e)
        {
            Entries.Add(e);
        }

        public ReportEntry GetEntryById(int id)
        {
            ReportEntry retVal = Entries.Find( entry => entry.FieldID == id );
            return retVal;
        }

        // Create a report entry and add it to both the report and the field itself
        public void AddEntry(EntryType type, SfmLexEntry entry, SfmFieldModel field, string description)
        {
            var reportEntry = new ReportEntry(type, entry, field, description);
            Add(reportEntry);
            field.AddReportEntry(reportEntry);
        }

        public List<ReportEntry> EntriesForRecord(int recordID)
        {
            return Entries.FindAll(
                rhs => rhs.RecordID == recordID
            );
        }

        public List<ReportEntry> EntriesForMarker(string marker)
        {
            return Entries.FindAll(
                rhs => rhs.Marker == marker
            );
        }

        public List<string> Markers()
        {
            var list = new List<string>();
            foreach (ReportEntry entry in Entries)
            {
                if (!list.Contains(entry.Marker))
                {
                    list.Add(entry.Marker);
                }
            }
            return list;
        }

        public int Count
        {
            get { return Entries.Count; }
        }

        public static SolidReport OpenSolidReport(string path)
        {
            SolidReport r;
            var xs = new XmlSerializer(typeof(SolidReport), new[] { typeof(ReportEntry) });
            try
            {
                using (var reader = new StreamReader(path))
                {
                    r = (SolidReport)xs.Deserialize(reader);
                    r.FilePath = path;
                }
            }
            catch
            {
                r = new SolidReport();
            }
            return r;
        }

        public void Save()
        {
            SaveAs(FilePath);
        }

        public void SaveAs(string filePath)
        {
            FilePath = filePath;
            var xs = new XmlSerializer(typeof(SolidReport));//, new Type[]{typeof(ReportEntry)});
            using (var writer = new StreamWriter(FilePath))
            {
                xs.Serialize(writer, this);
            }
        }


    }
}