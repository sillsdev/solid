using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace SolidEngine
{
    public class SolidReport
    {
        public enum EntryType
        {
            Error,
            Info,
            Warning
        }

        public class Entry
        {
            EntryType _type;
            int _recordID;
            int _recordStartLine;
            int _recordEndLine;
            string _entryName;
            string _marker;
            string _description;

            public Entry(EntryType type, XmlNode entry, XmlNode field, string description)
            {
                _type = type;
                if (entry != null)
                {
                    _entryName = entry.Name; //??? TODO what's a good name for this entry???
                    _recordID = Convert.ToInt32(entry.Attributes["id"].Value);
                    _recordStartLine = Convert.ToInt32(entry.Attributes["startline"].Value);
                    _recordEndLine = Convert.ToInt32(entry.Attributes["endline"].Value);
                }
                if (field != null)
                {
                    _marker = field.Name;
                }
                _description = description;
            }

            public int RecordID
            {
                get { return _recordID; }
            }

            public int RecordStartLine
            {
                get { return _recordStartLine; }
            }

            public int RecordEndLine
            {
                get { return _recordEndLine; }
            }

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

        public class SolidEntries : List<Entry>
        {
        }

        private SolidEntries _entries;

        [XmlIgnore]
        private string _filePath;

        public SolidEntries Entries
        {
            get { return _entries; }
        }

        public string FilePath
        {
            get { return _filePath; }
            set { _filePath = value; }
        }
	
        public SolidReport()
        {
            _entries = new SolidEntries();
        }

        public void Reset()
        {
            _entries.Clear();
        }

        public void Add(Entry e)
        {
            _entries.Add(e);
        }

        public static SolidReport OpenSolidReport(string path)
        {
            SolidReport r;
            XmlSerializer xs = new XmlSerializer(typeof(SolidReport));
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
            XmlSerializer xs = new XmlSerializer(typeof(SolidReport));
            using (StreamWriter writer = new StreamWriter(_filePath))
            {
                xs.Serialize(writer, _entries);
            }
        }


    }
}
