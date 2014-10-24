// Copyright (c) 2007-2014 SIL International
// Licensed under the MIT license: opensource.org/licenses/MIT

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
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

        //JMC:! The following needs to be set each time the depth of the following field is established.
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

        public string ValueMaybeUtf8(SolidSettings solidSettings)
        {
            return IsUnicode(Marker, solidSettings) ? AsUtf8(Value) : Value;
        }

        public string ValueForceUtf8(SolidSettings solidSettings)
        {
            if (IsUnicode(Marker, solidSettings))
            {
                // Already utf8, basically
                return AsUtf8(Value);
            }
            else
            {
                // Use cp1252 to convert to utf8
                var utf8 = new UTF8Encoding(false, true);  // BOM: false, throw errors: true  -JMC
                byte[] legacyBytes = SolidSettings.LegacyEncoding.GetBytes(Value);
                byte[] utf8Bytes = Encoding.Convert(SolidSettings.LegacyEncoding, utf8, legacyBytes);
                string utf8String = Encoding.UTF8.GetString(utf8Bytes);
                return utf8String;
            }
        }

        private static string AsUtf8 (string value)
        {
            byte[] valueAsBytes = SolidSettings.LegacyEncoding.GetBytes(value);  // replaced Encoding.GetEncoding("iso-8859-1") with Legacy -JMC 
            var utf8 = new UTF8Encoding(false, false); // no BOM, nor errors yet (that is up to ProcessEncoding). -JMC
            return utf8.GetString(valueAsBytes);
        }

        private static string AsLatin1(string value)
        {
            byte[] valueAsBytes = Encoding.UTF8.GetBytes(value);
            string s = SolidSettings.LegacyEncoding.GetString(valueAsBytes);
            return s;
        }

        public static string ValueAsLatin1(string marker, string value, SolidSettings solidSettings)
        {
            if (solidSettings == null) return value;
            if (IsUnicode(marker, solidSettings))  //safety check
            {
                // needs conversion from UTF8 to bytes (stored as a "string")
                value = AsLatin1(value);
            }
            return value;
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


        private static Regex RegexSplitTrailing = new Regex(
            @"^(.*?\r?\n)", RegexOptions.Compiled | RegexOptions.CultureInvariant);

        /// <summary>
        /// Trailing may consist of two pieces:
        /// 1. Trailing whitespace up to and including the first newline
        /// 2. All whitespace after that (should NOT move/delete with the field)
        /// If 2 exists, this removes it from the current record.
        /// Fixes #1286: Move Up shouldn't move trailing whitespace too -JMC Nov 2014
        /// </summary>
        /// <returns>Whatever it removed.</returns>
        public string RemoveExtraTrailing()

        {
            if (_trailing == SfmField.DefaultTrailing) return "";

            var m = RegexSplitTrailing.Match(_trailing);
            if (!m.Success) return "";
            int split = m.Index + m.Length;

            string extra = _trailing.Substring(split);
            _trailing = _trailing.Substring(0, split);
            
            return extra;
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
            string v = Value ?? "";
            return "\"" + Marker + " " + v + "\""  + " DEPTH:" + this.Depth;
        }
    }




}
