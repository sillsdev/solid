using System;
using System.Collections.Generic;
using System.Text;
using SolidEngine;

namespace SolidGui
{
    public class RecordFilterSet : List<RecordFilter>
    {
        Dictionary _currentDictionary = null;
        SolidReport _currentReport = null;

        public RecordFilterSet()
        {
        }

        public Dictionary Dictionary
        {
            get { return _currentDictionary; }
            set { _currentDictionary = value; }
        }


        public void BuildFilters()
        {
            Clear();

            if (_currentDictionary != null)
            {
                Add(new AllRecordFilter(_currentDictionary));
                foreach (string marker in _currentDictionary.AllMarkers)
                {
                    //Add(new MarkerFilter(_currentDictionary, marker));
                }
                //Add(new RegExRecordFilter("Has Note", @"\\nt\s\w", _currentDictionary));
                //Add(new RegExRecordFilter("Missing N Gloss", @"\\gn\s\w", true, _currentDictionary));
                //Add(new RegExRecordFilter("Missing ps", @"\\ps\s\w", true, _currentDictionary));
            }
            if (_currentReport != null)
            {
                //Add(new SolidReportRecordFilter(_currentReport));
            }
            //Add(new NullRecordFilter());
        }

        public void AddRecord(SolidReport report)
        {
            _currentReport.Entries.AddRange(report.Entries);
        }
    }
}
