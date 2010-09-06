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

        private readonly int _recordID = -1;
        public static event EventHandler RecordTextChanged;
        private SolidReport _report;
        public SfmLexEntry LexEntry { get; set; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(Record)) return false;
            return Equals((Record)obj);
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
            LexEntry = new SfmLexEntry();
            _recordID = _recordIdCounter++;
        }

        public Record(SfmLexEntry entry, SolidReport report)
        {
            LexEntry = entry;
            Report = new SolidReport(report);
            _recordID = _recordIdCounter++;
        }

        public SolidReport Report
        {
            set { _report = value; }
            get { return _report; }
        }

        public List<SfmFieldModel> Fields
        {
            get { return LexEntry.Fields; }
        }

        public bool HasMarker(string marker)
        {
            return LexEntry.HasMarker(marker);
        }

        public bool IsMarkerNotEmpty(string marker)
        {
            return LexEntry.IsMarkerNotEmpty(marker);
        }

        public string GetField(int id)
        {
            return LexEntry.GetField(id);
        }

        public SfmFieldModel GetFirstFieldWithMarker(string marker)
        {
            return LexEntry.GetFirstFieldWithMarker(marker);
        }

        

        
        // dont move to SfmLexEntry
        public void SetFieldValue(int id, string value)
        {
            if(LexEntry.Fields[id].Value != value)
            {
                LexEntry.Fields[id].Value = value;
                if (RecordTextChanged != null)
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

        //!!! Shouldn't be used ??? // TODO Move to SearchPM? CP 2010-08
        public string ToStructuredString()
        {
            StringBuilder record = new StringBuilder();
            foreach (SfmFieldModel field in LexEntry.Fields)
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
            LexEntry = SfmLexEntry.CreateFromText(setToText);
            var report = new SolidReport();
            IProcess process = new ProcessStructure(solidSettings);
            var xmlResult = process.Process(LexEntry, report);

            Report = report;
        }

        public void MoveField(SfmFieldModel field, int after)
        {
            LexEntry.MoveField(field, after);
        }

        public void RemoveField(int index)
        {
            LexEntry.RemoveField(index);

        }

        public void InsertFieldAt(SfmFieldModel field, int indexForThisField)
        {
            LexEntry.InsertFieldAt(field, indexForThisField);
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