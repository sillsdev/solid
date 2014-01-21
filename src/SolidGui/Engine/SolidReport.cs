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
        public enum EntryType
        {
            StructureInsertInInferredFailed, 
            StructureParentNotFound, 
            StructureParentNotFoundForInferred,
            EncodingBadUnicode,
            EncodingUpperAscii,
            Max 
        }



        public List<ReportEntry> Entries { get; set; }

        [XmlIgnore]
        public string FilePath { get; set; }

        public SolidReport()
        {
            Entries = new List<ReportEntry>();
        }

        public SolidReport(SolidReport report)        {
            Entries = new List<ReportEntry>(report.Entries);
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
            var retVal = Entries.Find( entry => entry.FieldID == id );
            return retVal;
        }

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
            foreach (var entry in Entries)
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