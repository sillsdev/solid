using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

using System.Linq;
using SolidGui.Engine;
using SolidGui.Processes;

namespace SolidGui.Model
{
    public class Record
    {
        private static int _recordIdCounter = 0;

        private SfmLexEntry _entry;
        // TODO delegate this to SfmLexEntry CP 2010-08
        private List<SfmFieldModel> _fields = new List<SfmFieldModel>();
        private int _recordID = -1;
        public static event EventHandler RecordTextChanged;
        private SolidReport _report;
        public SfmLexEntry LexEntry { get; set; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (Record)) return false;
            return Equals((Record) obj);
        }

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

        public Record()
        {
            _recordID = _recordIdCounter++;
        }


        static public Record CreateRecordFromSfmLexEntry(SfmLexEntry entry, SolidReport report)
        {
            var record = new Record();
            record.LexEntry = entry;
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
                    SfmFieldModel field = new SfmFieldModel(entry.Name, xmlChild.InnerText, depth, isInferred, fieldID);
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

        public List<SfmFieldModel> Fields
        {
            get { return _fields; }
        }

        public bool HasMarker(string marker)
        {
            return _fields.Find(
                       f => f.Marker == marker
                       ) != null;
        }

        public bool IsMarkerNotEmpty(string marker)
        {
            return _fields.Find(
                       f => f.Marker == marker && f.Value != string.Empty
                       ) != null;
        }

        public string GetField(int id)
        {
            SfmFieldModel field = _fields.Find(delegate(SfmFieldModel aField) { return aField.Id == id; });
            return field.ToString();
        }

        public SfmFieldModel GetFirstFieldWithMarker(string marker)
        {
            return _fields.FirstOrDefault(f=> f.Marker == marker);
        }

        public void SetFieldValue(int id, string value)
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
            foreach (SfmFieldModel field in _fields)
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

        public void SetRecordContents(string setToText, SolidSettings solidSettings)
        {
            _entry = SfmLexEntry.CreateFromText(setToText);
            var report = new SolidReport();
            IProcess process = new ProcessStructure(solidSettings);
            var xmlResult = process.Process(_entry, report);

            Report = report;
        }

        public void AddMarkerStatistics(Dictionary<string, int> frequencies, Dictionary<string, int> errorCount)
        {
            foreach (SfmFieldModel field in _fields)
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

        public void MoveField(SfmFieldModel field, int after)
        {
            int from = _fields.IndexOf(field);
            _fields.RemoveAt(from);
            if (from > after)
            {
                _fields.Insert(after + 1, field);               
            }
            else
            {
                _fields.Insert(after, field);               
            }
        }

        public void RemoveField(int index)
        {
            if(index == 0)
                throw new ApplicationException("Cannot remove the first field, which is the record marker");

            if(index < 0 || index >= _fields.Count)
                throw new ArgumentOutOfRangeException(string.Format("RemoveField({0}) was asked to remove an index which is out of range", index));

            _fields.RemoveAt(index);
        }

        public void InsertFieldAt(SfmFieldModel field, int indexForThisField)
        {
            _fields.Insert(indexForThisField,field);
        }

        public bool Equals(Record obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj._recordID == _recordID;
        }

        public override int GetHashCode()
        {
            return _recordID;
        }

        public static bool operator ==(Record left, Record right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Record left, Record right)
        {
            return !Equals(left, right);
        }
    }
}