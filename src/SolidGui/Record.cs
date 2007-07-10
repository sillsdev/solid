using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using SolidEngine;

namespace SolidGui
{
    public class Record
    {
        public class Field
        {
            private int _id;
            private string _marker;
            private string _value;
            private int _depth;
            private int _errorState;
            private bool _inferred;

            public Field(string marker, string value, int depth, bool inferred, int id)
            {
                _marker = marker;
                _value = value;
                _depth = depth;
                _inferred = inferred;
                _id = id;
            }
            /* not used ???
            public Field(string field)
            {
                _marker = field.Substring(0, field.IndexOf(" "));
                _value = field.Substring(field.IndexOf(" ")+1);
            }
            */

            

            public override string ToString()
            {
                return Marker+ " " + Value;
            }

            public string ToStructuredString()
            {
                int spacesInIndentation = 4;
                
                string indentation = new string(' ', Depth*spacesInIndentation);
                
                return indentation + Marker + " " + Value;
            }

            public int Id
            {
                get { return _id; }
            }

            public string Marker
            {
                get { return _marker; }
            }


            public string Value
            {
                get { return _value; }
                set { _value = value; }
            }

            public int Depth
            {
                get { return _depth; }
            }


            public bool Inferred
            {
                get { return _inferred; }
            }
        }
                
        private List<Field> _fields = new List<Field>();
        private SolidReport _report;
        private static int _recordID = -1;
        public static event EventHandler RecordTextChanged;

        public int ID
        {
           get { return _recordID; }
        }
        /*
        public Record(List<string> fieldValues)
        {
            _id++;
            _fields = new List<Field>();
            foreach (string value in fieldValues)
            {
                _fields.Add(new Field(value));
            }
        }
        */

        public Record(int recordID)
        {
            _recordID = recordID;
        }

        /*
        public Record(XmlNode entry, SolidReport report)
        {
            _id++;
            _fields = new List<Field>();
            _report = report;
            ReadEntry(entry.FirstChild, 0);
            //_fields.Add(new Field("\\lx", "foo", 0, false, 0));
            //_fields.Add(new Field("\\sn", "", 1, true, 1));
            //_fields.Add(new Field("\\ge", "foo", 1, false, 2));
            //_fields.Add(new Field("\\ps", "foo", 2, true, 3));
            //_fields.Add(new Field("\\pe", "foo", 1, false, 4));
            //_fields.Add(new Field("\\sn", "foo", 1, false, 5));
        }
        */

        static public Record CreateFromXml(XmlNode entry, SolidReport report)
        {
            XmlHelper xh = new XmlHelper(entry);
            string s = xh.GetAttribute("record");
            int recordID = (s != string.Empty) ? Convert.ToInt32(s) : -1;
            Record record = new Record(recordID);
            foreach (XmlNode xmlChild in entry.ChildNodes)
            {
                record.ReadField(xmlChild, 0);
            }
            return record;
        }

        private void ReadField(XmlNode entry, int depth)
        {
            XmlHelper x = new XmlHelper(entry);
            string s;
            //Field f = new Field(
            bool isInferred = x.GetAttribute("inferred") == "true";
            s = x.GetAttribute("record");
            int fieldID = (s != string.Empty) ? Convert.ToInt32(s) : 0;
            foreach (XmlNode xmlChild in entry.ChildNodes)
            {
                if (xmlChild.Name == "data")
                {
                    _fields.Add(new Field("\\" + entry.Name, xmlChild.InnerText, depth, isInferred, fieldID));
                }
                else
                {
                    ReadField(xmlChild, depth + 1);
                }
            }
        }

        public List<Field> Fields
        {
            get { return _fields; }
        }

        public bool HasMarker(string marker)
        {
            return _fields.Find(
                delegate(Field f)
                {
                    return f.Marker == marker;
                }
            ) != null;
        }

        public string GetField(int id)
        {
            Field field = _fields.Find(delegate(Field aField) { return aField.Id == id; });
            return field.ToString();
        }

        public void SetField(int id, string value)
        {
            if(_fields[id].Value != value)
            {
                _fields[id].Value = value;
                if(RecordTextChanged != null)
                    RecordTextChanged.Invoke(this, new EventArgs());
            }
        }

        public override string ToString()
        {
            StringBuilder record = new StringBuilder();
            foreach(Field field in _fields)
            {
                record.AppendLine(field.ToString());
            }
            return record.ToString();
        }

        public string ToStringWithoutInferred()
        {
            StringBuilder record = new StringBuilder();
            foreach (Field field in _fields)
            {
                if (!field.Inferred)
                {
                    record.AppendLine(field.ToString());
                }
            }
            return record.ToString();
        }

        public string Value
        {
            get { return ToString(); }
        }

        public string ToStructuredString()
        {
            StringBuilder record = new StringBuilder();
            foreach (Field field in _fields)
            {
                record.AppendLine(field.ToStructuredString());
            }
            return record.ToString();
        }

        public string GetFieldStructured(int i)
        {
            return _fields[i].ToStructuredString();
        }

        public void SetRecord(string setToText, SolidSettings _solidSettings)
        {
            SfmXmlReader xr = new SfmXmlReader(new StringReader(setToText));
            xr.ReadToFollowing("entry");
            XmlReader entryReader = xr.ReadSubtree();
            // Load the current record from xr into an XmlDocument
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(entryReader);
            SolidReport report = new SolidReport();
            IProcess process = new ProcessStructure(_solidSettings);
            XmlNode xmlResult = process.Process(xmldoc.DocumentElement, report);

            _fields.Clear();
            foreach (XmlNode xmlChild in xmlResult.ChildNodes)
            {
                ReadField(xmlChild, 0);
            }
        }

    }
}
