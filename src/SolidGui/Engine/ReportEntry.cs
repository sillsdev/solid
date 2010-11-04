using System;
using System.Collections.Generic;
using System.Text;
using SolidGui.Model;

namespace SolidGui.Engine
{
    public class ReportEntry
    {
        readonly SolidReport.EntryType _entryType;
        readonly int _recordID; // TODO Remove these, they aren't used?
        readonly int _fieldID;  // TODO Remove these, they aren't used?
        //int _recordStartLine;
        //int _recordEndLine;
        readonly string _entryName;
        readonly string _marker;
        readonly string _description;

        public ReportEntry(SolidReport.EntryType type, SfmLexEntry entry, SfmFieldModel field, string description)
        {
            _entryType = type;
            if (entry != null)
            {
                _entryName = entry.Name; // TODO This is bogus, it needs decoding first.  What's a good name for this entry???
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

        public SolidReport.EntryType EntryType
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

        public override string ToString()
        {
            return "NAME:" + _entryName + " DESCRIP:" + _description + " TYPE:" + _entryType;
        }
    }

}
