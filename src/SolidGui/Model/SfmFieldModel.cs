// Copyright (c) 2007-2014 SIL International
// Licensed under the MIT license: opensource.org/licenses/MIT

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using SolidGui.Engine;

namespace SolidGui.Model
{
    // SfmFieldNode might've been clearer, since it's part of a tree structure. 
    // Like SfmField, but with more structure (indentation depth) and additional methods. Contained by SfmLexEntry -JMC
    public class SfmFieldModel
    {
        private readonly int _id;
        private readonly List<SfmFieldModel> _children;
        private static int _fieldId = 0;
        private readonly List<ReportEntry> _reportEntries;

        public SfmFieldModel(string marker) :
            this(marker, "")
        {
        }

        public SfmFieldModel(string marker, string value) :
            this(marker, value, 0, false)
        {
        }

        public SfmFieldModel(string marker, string value, int depth, bool inferred) :
            this(marker, value, SfmField.DefaultTrailing, depth, inferred)
        {
        }

        public SfmFieldModel(string markerNoSlash, string value, string trailing, int depth, bool inferred)
        {
            Marker = markerNoSlash;
            Value = value;
            Trailing = trailing;
            Depth = depth;
            Inferred = inferred;
            _id = _fieldId++;
            _children = new List<SfmFieldModel>();
            _reportEntries = new List<ReportEntry>();
        }

        public List<SfmFieldModel> Children
        {
            get { return _children; }
        }

        private SfmFieldModel _parent;

        public IEnumerable<ReportEntry> ReportEntries
        {
            get { return _reportEntries; }
        }

        public void AddReportEntry(ReportEntry reportEntry)
        {
            _reportEntries.Add(reportEntry);
        }

        public SfmFieldModel Parent
        {
            get { return _parent; }
            set
            {
                if (_parent != null)
                {
                    throw new ArgumentException(String.Format("Parent already exists in {0}", Marker));
                }
                _parent = value;
            }
        }

        public int FieldId { get; private set; }
        public bool Inferred { get; set; }

        public string Marker { get; private set; }

        //JMC! The following needs to be set each time the depth of the following field is established.
        public List<string> Closers; //one or more closing tags; e.g. "xe" "rf" "sn" "se"
        //Example: if \xe ends a subentry, the whole field could be saved as: \xe They wept.\xe*\rf*\sn*\se*\r\n

        //JMC: Issue #1219. Remove hard-coded references to markers (e.g. in the link-checking quick fix)
        // In order to fix that, I think we need for the following to always get set properly.
        public string Mapping { get; set; }

        public string Value { get; set; }

        // ?? Return unicode for either kind of field; used for display? -JMC
        public string ValueAsUnicode()
        {
            string retval = string.Empty;
            string value = Value;
            if (value.Length > 0)
            {
                Encoding byteEncoding = SolidSettings.LegacyEncoding; // was Encoding.GetEncoding("iso-8859-1"); -JMC
                //Encoding byteEncoding = Encoding.Unicode;
                byte[] valueAsBytes = byteEncoding.GetBytes(value);
                Encoding stringEncoding = Encoding.UTF8;
                retval = stringEncoding.GetString(valueAsBytes);
                if (retval.Length == 0)  // JMC: Is this sufficient error detection?
                {
                    retval = "Non Unicode Data Found";  //JMC:! How about throwing an exception? Maybe fatal, or maybe UI just catches and recommends not saving?
                    // TODO: Need to lock this field of the current record at this point.
                    // The editor must *never* write back to the model (for this field)
                }
            }
            return retval;
        }

        public string ValueForDisplay(SolidSettings solidSettings)
        {
            if (solidSettings == null) return Value;

            SolidMarkerSetting markerSetting = solidSettings.FindOrCreateMarkerSetting(Marker);
            if (markerSetting.Unicode)
            {
                return ValueAsUnicode();
            }
            return Value;
        }

        public void AppendChild(SfmFieldModel node)
        {
            node.Parent = this;
            _children.Add(node);
            node.Depth = Depth + 1;
        }


        public int Id
        {
            get { return _id; }
        }

        private string _trailing;
        public string Trailing 
        {
            get { return String.IsNullOrEmpty(_trailing) ? SfmField.DefaultTrailing : _trailing; }
            set { _trailing = value.Contains("\n") ? value : null; } 
        }


        public int Depth { get; set; }

        public bool HasReportEntry
        {
            get { return _reportEntries.Count > 0; }
        }

        public bool HasValue
        {
            get { return !String.IsNullOrEmpty(Value); }
        }

        public SfmFieldModel this[int i]
        {
            get { return _children[i]; }
        }

        public override string ToString()
        {
            if(String.IsNullOrEmpty(Value))
            {
                return "\"" + Marker + "\"" + " DEPTH:" + this.Depth;
            }
            return "\"" + Marker + " " + Value + "\""  + " DEPTH:" + this.Depth; ;
        }

    }




}
