// Copyright (c) 2007-2014 SIL International
// Licensed under the MIT license: opensource.org/licenses/MIT

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Xml;
using SolidGui.Engine;
using System.Linq;

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
            get
            {
                // Reverse-sort first, to put the warnings at the top.
                _reportEntries.OrderByDescending(x => x.Description);
                return _reportEntries;
            }
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

        public static bool IsUnicode(string Marker, SolidSettings solidSettings)
        {
            SolidMarkerSetting markerSetting = solidSettings.FindOrCreateMarkerSetting(Marker);
            return markerSetting.Unicode;
        }

        public static string ValueAsUtf8(string value)
        {
            Encoding sourceEnc = SolidSettings.LegacyEncoding;  // replaced Encoding.GetEncoding("iso-8859-1") with Legacy -JMC
            Encoding targetEnc = Encoding.UTF8;
            string retval = value;
            string valueInMemory = value;
            if (valueInMemory.Length > 0)  // might be safer to check settings, and to skip if unicode -JMC
            {
                byte[] valueAsBytes = sourceEnc.GetBytes(valueInMemory);
                retval = targetEnc.GetString(valueAsBytes);
                if (retval.Length == 0)  // JMC: Is this sufficient error detection?
                {
                    retval = "Non Unicode Data Found";  //JMC:! How about throwing an exception? Maybe fatal, or maybe UI just catches and recommends not saving?
                    // TODO: Need to lock this field of the current record at this point.
                    // The editor must *never* write back to the model (for this field)
                }
            }
            return retval;
        }

        private static string ToLatin1(string value)
        {
            byte[] valueAsBytes = Encoding.UTF8.GetBytes(value);
            return SolidSettings.LegacyEncoding.GetString(valueAsBytes);
        }

        // JMC:! Can't we get rid of this? Why do we ever need to 
        public static string ValueAsLatin1(string marker, string value, SolidSettings solidSettings)
        {
            if (solidSettings == null) return value;
            if (IsUnicode(marker, solidSettings))  //safety check
            {
                // needs conversion from UTF8 to bytes (stored as a "string")
                value = ToLatin1(value);
            }
            return value;
        }

        public string ValueForDisk(string marker, string value, SolidSettings solidSettings)
        {
            if (solidSettings == null) return value;
            if (IsUnicode(marker, solidSettings))  //safety check
            {
                // needs conversion from UTF8 to bytes (stored as a "string")
                value = ToLatin1(value);
            }
            return IsUnicode(Marker, solidSettings) ? ValueAsUtf8(Value) : ToLatin1(Value);
        }


        public string ValueForDisplay(SolidSettings solidSettings)
        {
            if (solidSettings == null) return Value;
            return IsUnicode(Marker, solidSettings) ? ValueAsUtf8(Value) : Value;
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
