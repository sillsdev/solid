using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace SolidConsole
{
    public class SolidReport
    {
        public class Entry
        {
            int _recordID;
            int _sourceLine;
            string _errorClass;
            string _marker;
            string _description;

            public Entry()
            {

            }
        }

        public class SolidEntries : List<Entry>
        {
        }

        private SolidEntries _entries = new SolidEntries();

        private string _file;

        public SolidEntries Entries
        {
            get { return _entries; }
        }

        public string File
        {
            get { return _file; }
            set { _file = value; }
        }
	
        public SolidReport()
        {

        }

        public void Reset()
        {
            _entries.Clear();
        }

        public void Open(string file)
        {
            _file = file;
        }

        public void Add(Entry e)
        {
            _entries.Add(e);
        }

        public bool Read()
        {
            bool retval = true;
            XmlSerializer xs = new XmlSerializer(typeof(SolidEntries));
            try
            {
                using (StreamReader reader = new StreamReader(_file))
                {
                    _entries = (SolidEntries)xs.Deserialize(reader);
                }
            }
            catch
            {
                _entries = new SolidEntries();
                retval = false;
                //!!! Should rethrow the exception
            }
            return retval;
        }

        public void Write()
        {
            XmlSerializer xs = new XmlSerializer(typeof(SolidEntries));
            using (StreamWriter writer = new StreamWriter(_file))
            {
                xs.Serialize(writer, _entries);
            }
        }


    }
}
