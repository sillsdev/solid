// Copyright (c) 2007-2014 SIL International
// Licensed under the MIT license: opensource.org/licenses/MIT

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

    // Wraps SfmLexEntry, adding structure (indentation level), reporting, and manipulations. -JMC
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

        public bool Equals(Record obj)  //JMC: is this superfluous?
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj._recordID == _recordID;
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
            Report = (report == null) ? null : SolidReport.MakeCopy(report);
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

        public static void DecodeUtf8(SfmRecord entry, SolidSettings s)
        {
            foreach(SfmField f in entry)
            {
                f.Value = SfmFieldModel.ValueAsLatin1(f.Marker, f.Value, s);
            }
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
                if (RecordTextChanged != null) RecordTextChanged.Invoke(this, EventArgs.Empty);
            }
        }


        public void SetRecordContents(string setToText, SolidSettings solidSettings)
        {
            LexEntry = SfmLexEntry.CreateFromText(setToText);
            var report = new SolidReport();
            IProcess process = new ProcessStructure(solidSettings);
            LexEntry = process.Process(LexEntry, report);

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