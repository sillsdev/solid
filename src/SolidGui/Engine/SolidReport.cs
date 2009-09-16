using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Solid.Engine
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
            EntryType _entryType;
            int _recordID;
            int _fieldID;
            //int _recordStartLine;
            //int _recordEndLine;
            string _entryName;
            string _marker;
            string _description;

            public Entry()
            {}

            public Entry(EntryType type, XmlNode entry, XmlNode field, string description)
            {
                _entryType = type;
                if (entry != null)
                {
                    XmlHelper xhEntry = new XmlHelper(entry);
                    _entryName = entry.Name; //??? TODO what's a good name for this entry???
                    _recordID = Convert.ToInt32(xhEntry.GetAttribute("record", "-1"));
                    //_recordStartLine = Convert.ToInt32(entry.Attributes["startline"].Value);
                    //_recordEndLine = Convert.ToInt32(entry.Attributes["endline"].Value);
                }
                if (field != null)
                {
                    XmlHelper xhField = new XmlHelper(field);
                    _fieldID = Convert.ToInt32(xhField.GetAttribute("field", "-1"));
                    _marker = field.Name;
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

        public class SolidEntries : List<SolidReport.Entry>
        {
            public SolidEntries() : base()
            {
            }

            public SolidEntries(SolidEntries rhs) : base(rhs)
            {
            }
        }

        public SolidEntries _entries;
                
        [XmlIgnore]
        private string _filePath;

        public SolidEntries Entries
        {
            get { return _entries; }
        }

        [XmlIgnore]
        public string FilePath
        {
            get { return _filePath; }
            set { _filePath = value; }
        }
	
        public SolidReport()
        {
            _entries = new SolidEntries();
        }

        public SolidReport(SolidReport report)
        {
            _entries = new SolidEntries(report.Entries);
        }

        public void Reset()
        {
            _entries.Clear();
        }

        public void Add(Entry e)
        {
            _entries.Add(e);
        }

        public Entry GetEntryById(int id)
        {
            Entry retVal =_entries.Find(
                delegate(Entry entry)
                    {
                        return entry.FieldID == id;
                    }
            );

            return retVal;
        }

        public void AddEntry(EntryType type, XmlNode entry, XmlNode field, string description)
        {
            Add(new Entry(type, entry, field, description));
        }

        public List<SolidReport.Entry> EntriesForRecord(int recordID)
        {
            return _entries.FindAll(
                delegate(Entry rhs)
                {
                    return rhs.RecordID == recordID;
                }
            );
        }

        public List<SolidReport.Entry> EntriesForMarker(string marker)
        {
            return _entries.FindAll(
                delegate(Entry rhs)
                {
                    return rhs.Marker == marker;
                }
            );
        }

        public List<string> Markers()
        {
            List<string> list = new List<string>();
            foreach (Entry entry in _entries)
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
            get { return _entries.Count; }
        }

        public static SolidReport OpenSolidReport(string path)
        {
            SolidReport r;
            XmlSerializer xs = new XmlSerializer(typeof(SolidReport), new Type[] { typeof(Entry) });
            try
            {
                using (StreamReader reader = new StreamReader(path))
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
            SaveAs(_filePath);
        }

        public void SaveAs(string filePath)
        {
            _filePath = filePath;
            XmlSerializer xs = new XmlSerializer(typeof(SolidReport));//, new Type[]{typeof(Entry)});
            using (StreamWriter writer = new StreamWriter(_filePath))
            {
                xs.Serialize(writer, this);
            }
        }


    }
}
