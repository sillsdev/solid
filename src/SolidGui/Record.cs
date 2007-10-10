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

            public string ToStructuredString()
            {
                int spacesInIndentation = 4;
                
                string indentation = new string(' ', Depth*spacesInIndentation);
                
                if(!Inferred)
                    return indentation + "\\" + Marker + " " + Value;
                else
                    return indentation + "\\+" + Marker + " " + Value;

            }
            
            public int ErrorState
            {
                get { return _errorState; }
                set { _errorState = value; }
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
        private int _recordID = -1;
        public static event EventHandler RecordTextChanged;
        private SolidReport _report;

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

        static public Record CreateFromXml(XmlNode entry, SolidReport report)
        {
            XmlHelper xh = new XmlHelper(entry);
            string s = xh.GetAttribute("record");
            int recordID = (s != string.Empty) ? Convert.ToInt32(s) : -1;
            Record record = new Record(recordID);
            foreach (XmlNode xmlChild in entry.ChildNodes)
            {
                record.ReadField(xmlChild, 0, report);
            }

            record.Report = new SolidReport(report);

            return record;
        }

        public SolidReport Report
        {
            set{ _report = value; }
            get { return _report; }
        }

        private void ReadField(XmlNode entry, int depth, SolidReport report)
        {
            XmlHelper x = new XmlHelper(entry);
            string s;
            bool isInferred = x.GetAttribute("inferred") == "true";
            s = x.GetAttribute("field");
            int fieldID = (s != string.Empty) ? Convert.ToInt32(s) : 0;
            foreach (XmlNode xmlChild in entry.ChildNodes)
            {
                if (xmlChild.Name == "data")
                {
                    Field field = new Field(entry.Name, xmlChild.InnerText, depth, isInferred, fieldID);
                    SolidReport.Entry reportEntry = report.GetEntryById(field.Id);
                    field.ErrorState = (reportEntry != null) ? 1 : 0;
                    _fields.Add(field);
                }
                else
                {
                    ReadField(xmlChild, depth + 1, report);
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
        /*
        public override string ToString()
        {
            StringBuilder record = new StringBuilder();
            foreach(Field field in _fields)
            {
                record.AppendLine(field.ToString());
            }
            return record.ToString();
        }
        */
        /*
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
        */
        public string Value
        {
            get { return ToString(); }
        }

        //!!! Shouldn't be used ???
        public string ToStructuredString()
        {
            StringBuilder record = new StringBuilder();
            foreach (Field field in _fields)
            {
                record.Append(field.ToStructuredString() + "\n");
            }
            return record.ToString();
        }

        //!!! Not used ???
        //public string GetFieldStructured(int i)
        //{
        //    return _fields[i].ToStructuredString();
        //}

        public void SetRecord(string setToText, SolidSettings _solidSettings)
        {
            //!!!setToText = "\\_sh a\n" + setToText; //!!! Test for lx being first, i.e no header.  CP
            SfmXmlReader xr = new SfmXmlReader(new StringReader(setToText));
            if(!xr.ReadToFollowing("entry"))
            {
                return;
            }
            XmlReader entryReader = xr.ReadSubtree();
            // Load the current record from xr into an XmlDocument
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(entryReader);
            SolidReport report = new SolidReport();
            IProcess process = new ProcessStructure(_solidSettings);
            XmlNode xmlResult = process.Process(xmldoc.DocumentElement, report);

            Report = report;
            _fields.Clear();
            foreach (XmlNode xmlChild in xmlResult.ChildNodes)
            {
                ReadField(xmlChild, 0, report);
            }
        }

        public void AddMarkerStatistics(Dictionary<string, int> frequencies, Dictionary<string, int> errorCount)
        {
            foreach (Field field in _fields)
            {
                if (!frequencies.ContainsKey(field.Marker))
                {
                    frequencies.Add(field.Marker, 0);
                    errorCount.Add(field.Marker, 0);
                }
                
                frequencies[field.Marker] += 1;
                
                if(field.ErrorState > 0)
                {
                    errorCount[field.Marker] += 1;    
                }
            }
        }

    }
}
