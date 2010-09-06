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

        public class Entry
        {
            readonly EntryType _entryType;
            readonly int _recordID; // TODO Remove these, they aren't used?
            readonly int _fieldID;  // TODO Remove these, they aren't used?
            //int _recordStartLine;
            //int _recordEndLine;
            readonly string _entryName;
            readonly string _marker;
            readonly string _description;

            public Entry(EntryType type, SfmLexEntry entry, SfmFieldModel field, string description)
            {
                _entryType = type;
                if (entry != null)
                {
                    _entryName = entry.Name; //??? TODO what's a good name for this entry???
                    _recordID = entry.RecordId;
                    //_recordStartLine = Convert.ToInt32(entry.Attributes["startline"].Value);
                    //_recordEndLine = Convert.ToInt32(entry.Attributes["endline"].Value);
                }
                if (field != null)
                {
                    _fieldID = field.FieldId;
                    _marker = field.Marker;
                }
                _description = description;
            }

            public EntryType EntryType
            {
                get { return _entryType; }
            }


            public int RecordID
            {
                get { return _recordID; }
            }

            public int FieldID
            {
                get { return _fieldID; }
            }
            /*
            public int RecordStartLine
            {
                get { return _recordStartLine; }
            }

            public int RecordEndLine
            {
                get { return _recordEndLine; }
            }
            */
            public string Marker
            {
                get { return _marker; }
            }

            public string Description
            {
                get { return _description; }
            }

            public string Name
            {
                get { return _entryName; }
            }
        }

        public List<Entry> Entries { get; set; }

        [XmlIgnore]
        public string FilePath { get; set; }

        public SolidReport()
        {
            Entries = new List<Entry>();
        }

        public SolidReport(SolidReport report)        {
            Entries = new List<Entry>(report.Entries);
        }

        public void Reset()
        {
            Entries.Clear();
        }

        public void Add(Entry e)
        {
            Entries.Add(e);
        }

        public Entry GetEntryById(int id)
        {
            var retVal =Entries.Find(
                entry => entry.FieldID == id
            );

            return retVal;
        }

        public void AddEntry(EntryType type, SfmLexEntry entry, SfmFieldModel field, string description)
        {
            Add(new Entry(type, entry, field, description));
        }

        public List<Entry> EntriesForRecord(int recordID)
        {
            return Entries.FindAll(
                rhs => rhs.RecordID == recordID
            );
        }

        public List<Entry> EntriesForMarker(string marker)
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
            var xs = new XmlSerializer(typeof(SolidReport), new[] { typeof(Entry) });
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
            var xs = new XmlSerializer(typeof(SolidReport));//, new Type[]{typeof(Entry)});
            using (var writer = new StreamWriter(FilePath))
            {
                xs.Serialize(writer, this);
            }
        }


    }
}