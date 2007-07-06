using System;
using System.Collections.Generic;
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

            public Field(string field)
            {
                _marker = field.Substring(0, field.IndexOf(" "));
                _value = field.Substring(field.IndexOf(" ")+1);
            }

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
                
        private List<Field> _fields;
        private SolidReport _report;
        private static int _id = -1;
        public static event EventHandler RecordTextChanged;

        public int ID
        {
           get { return _id; }
        }

        public Record(List<string> fieldValues)
        {
            _id++;
            _fields = new List<Field>();
            foreach (string value in fieldValues)
            {
                _fields.Add(new Field(value));
            }
        }

        public Record(XmlNode entry, SolidReport report)
        {
            _id++;
            _fields = new List<Field>();
            _report = report;
            //ReadEntry(entry.FirstChild,0);
            _fields.Add(new Field("\\lx", "foo", 0, false, 0));
            _fields.Add(new Field("\\sn", "", 1, true, 1));
            _fields.Add(new Field("\\ge", "foo", 1, false, 2));
            _fields.Add(new Field("\\ps", "foo", 2, true, 3));
            _fields.Add(new Field("\\pe", "foo", 1, false, 4));
            _fields.Add(new Field("\\sn", "foo", 1, false, 5));
        }

        private void ReadEntry(XmlNode entry, int depth)
        {
            int id = 0;
            bool inferred = false;
            if (entry.Attributes.Count > 0)
            {
                inferred = entry.Attributes["inferred"].Value == "true";
                id = Convert.ToInt32(entry.Attributes["record"].Value);
            }

            _fields.Add(new Field(entry.Name, entry.InnerText, depth, inferred, id));

            if (entry.HasChildNodes)
                ReadEntry(entry.FirstChild, depth + 1);
                        
            if (entry.NextSibling != null)
                ReadEntry(entry.NextSibling, depth);
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

        public int Count
        {
            get { return _fields.Count; }
        }

        public bool GetFieldInferred(int i)
        {
            return _fields[i].Inferred;
        }

        public string GetFieldStructured(int i)
        {
            return _fields[i].ToStructuredString();
        }
    }
}
