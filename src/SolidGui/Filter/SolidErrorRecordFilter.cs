// Copyright (c) 2007-2014 SIL International
// Licensed under the MIT license: opensource.org/licenses/MIT

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using SolidGui.Engine;
using SolidGui.Filter;

namespace SolidGui.Model
{
    public class SolidErrorRecordFilter : RecordFilter
    {
        private readonly string _marker;
        SolidReport.EntryType _errorType;

        readonly List<string> _errorMessages = new List<string>();

        public SolidErrorRecordFilter(SfmDictionary d, string marker,SolidReport.EntryType errorType, string name) :
            base(d, name)
        {
            _marker = marker;
            _errorType = errorType;
            _name = name;
        }

        public override void UpdateFilter()
        {
            return;  // JMC:! not yet done!
            // throw new NotImplementedException("Not yet able to self-update");
        }

        public override IEnumerable<string> HighlightMarkers
        {
            get { return new[] { _marker }; }
        }

        public override string Description(int index)
        {
            return _errorMessages[index];
        }

        /// <summary>
        /// Which line number should be scrolled to when first opening the current record. 
        /// </summary>
        public override int CurrentInitialLine()
        {
            string m = _marker;
            int i = 0;
            foreach (SfmFieldModel f in Current.Fields)
            {
                if (f.Marker == _marker && f.HasReportEntry)
                    foreach (var e in f.ReportEntries)
                    {
                        if (e.EntryType == _errorType)
                        {
                            return i;
                        }
                    }
                i += f.Newlines();
            }
            return 0;
        }

    }
}