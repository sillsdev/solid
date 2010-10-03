using System.Collections.Generic;
using SolidGui.Engine;

namespace SolidGui.Model
{
    public class RecordFilterSet : List<RecordFilter>
    {
        SfmDictionary _currentDictionary = null;

        class ErrorFilterForType : Dictionary<string, SolidErrorRecordFilter>
        {
        }

        ErrorFilterForType[] _solidErrors = new ErrorFilterForType[(int)SolidReport.EntryType.Max];

        public RecordFilterSet()
        {
        }

        public SfmDictionary Dictionary
        {
            get { return _currentDictionary; }
        }

        public SolidErrorRecordFilter CreateSolidErrorRecordFilter(SolidReport.EntryType type, string marker)
        {
            SolidErrorRecordFilter retval = null;
            switch (type)
            {
                case SolidReport.EntryType.StructureInsertInInferredFailed:
                    retval = new SolidErrorRecordFilter(
                        _currentDictionary,
                        marker,
                        type,
                        string.Format("Failed to insert {0} in inferred marker", marker)
                        );
                    break;
                case SolidReport.EntryType.StructureParentNotFound:
                    retval = new SolidErrorRecordFilter(
                        _currentDictionary,
                        marker,
                        type,
                        string.Format("Marker {0} could not be placed in structure", marker)
                        );
                    break;
                case SolidReport.EntryType.StructureParentNotFoundForInferred:
                    retval = new SolidErrorRecordFilter(
                        _currentDictionary,
                        marker,
                        type,
                        string.Format("Inferred marker {0} could not be placed in structure", marker)
                        );
                    break;
                case SolidReport.EntryType.EncodingUpperAscii:
                    retval = new SolidErrorRecordFilter(
                        _currentDictionary,
                        marker,
                        type,
                        string.Format("Marker {0} contains upper ascii data", marker)
                        );
                    break;
                case SolidReport.EntryType.EncodingBadUnicode:
                    retval = new SolidErrorRecordFilter(
                        _currentDictionary,
                        marker,
                        type,
                        string.Format("Marker {0} contains bad unicode data", marker)
                        );
                    break;
            }
            return retval;
        }

        public void BeginBuild(SfmDictionary currentDictionary)
        {
            base.Clear();
            _currentDictionary = currentDictionary;
            for (int i = 0; i < (int)SolidReport.EntryType.Max; i++)
            {
                _solidErrors[i] = new ErrorFilterForType();
            }
        }

        public void EndBuild()
        {
            if (_currentDictionary != null)
            {
                // Error Filters
                int filterCount = 0;
                foreach (ErrorFilterForType filter in _solidErrors)
                {
                    if (filter != null)
                    {
                        foreach (KeyValuePair<string, SolidErrorRecordFilter> recordFilter in filter)
                        {
                            if (recordFilter.Value.Count > 0)
                            {
                                filterCount++;
                                Add(recordFilter.Value);
                            }
                        }
                    }
                }
                // If no errors add the all filter.
                if (filterCount == 0)
                {
                    Add(AllRecordFilter.CreateAllRecordFilter(_currentDictionary));
                }


                //Add(new RegExRecordFilter("Has Note", @"\\nt\s\w", _currentDictionary));
                //Add(new RegExRecordFilter("Missing N Gloss", @"\\gn\s\w", true, _currentDictionary));
                //Add(new RegExRecordFilter("Missing ps", @"\\ps\s\w", true, _currentDictionary));
            }
            //Add(new NullRecordFilter());
        }

        public void AddRecord(int record, SolidReport report)
        {
            foreach (ReportEntry entry in report.Entries)
            {
                ErrorFilterForType filter = _solidErrors[(int)entry.EntryType];
                if (!filter.ContainsKey(entry.Marker))
                {
                    filter.Add(entry.Marker, CreateSolidErrorRecordFilter(entry.EntryType, entry.Marker));
                }
                filter[entry.Marker].AddEntry(entry);
            }
        }
    }
}