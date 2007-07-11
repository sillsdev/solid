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

    //    List<int>[]  SolidReport.EntryType[]
        SolidReportErrorRecordFilter[] _solidErrors = null;

        public RecordFilterSet()
        {
        }

        public Dictionary Dictionary
        {
            get { return _currentDictionary; }
        }

        public void BeginBuild(Dictionary currentDictionary)
        {
            base.Clear();
            _currentDictionary = currentDictionary;
            _solidErrors = new SolidReportErrorRecordFilter[(int)SolidReport.EntryType.Max];
            _solidErrors[(int)SolidReport.EntryType.StructureInsertInInferredFailed] =
                new SolidReportErrorRecordFilter(
                    _currentDictionary,
                    SolidReport.EntryType.StructureInsertInInferredFailed,
                    "Failed to insert in inferred marker"
            );
            _solidErrors[(int)SolidReport.EntryType.StructureParentNotFound] =
                new SolidReportErrorRecordFilter(
                    _currentDictionary,
                    SolidReport.EntryType.StructureParentNotFound,
                    "Parent not found for marker"
            );
            _solidErrors[(int)SolidReport.EntryType.StructureParentNotFoundForInferred] =
                new SolidReportErrorRecordFilter(
                    _currentDictionary,
                    SolidReport.EntryType.StructureParentNotFoundForInferred,
                    "Parent not found for inferred marker"
            );
        }

        public void EndBuild()
        {
            if (_currentDictionary != null)
            {
                // All Filter
                Add(new AllRecordFilter(_currentDictionary));
                // Marker Filters
                foreach (string marker in _currentDictionary.AllMarkers)
                {
                    Add(new MarkerFilter(_currentDictionary, marker));
                }
                // Error Filters
                foreach (SolidReportErrorRecordFilter solidFilter in _solidErrors)
                {
                    if (solidFilter.Count > 0)
                    {
                        Add(solidFilter);
                    }
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

        public void AddRecord(int record, SolidReport report)
        {
            foreach (SolidReportErrorRecordFilter filter in _solidErrors)
            {
                filter.AddReport(record, report);
            }
        }
    }
}
