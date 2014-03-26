// Copyright (c) 2007-2014 SIL International
// Licensed under the MIT license: opensource.org/licenses/MIT

using System;
using System.Collections.Generic;
using System.Linq;
using SolidGui.Engine;
using SolidGui.Model;
using SolidGui;

namespace SolidGui.Filter
{
    public class RecordFilterSet : List<RecordFilter>, IDisposable
    {
        SfmDictionary _currentDictionary = null;

        class ErrorFilterForType : Dictionary<string, SolidErrorRecordFilter>
        {
        }

        private Dictionary<Enum, ErrorFilterForType> _solidErrors = new Dictionary<Enum, ErrorFilterForType>();
        //private ErrorFilterForType[] _solidErrors = new ErrorFilterForType[(int)SolidReport.NumTypes];

        public RecordFilterSet()
        {
        }

        public override string ToString()
        {
            return string.Format("{{fset ({0}); {1}}}", 
                Count, GetHashCode());
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
                        string.Format("Reminder: marker {0} contains upper ASCII data not yet converted to unicode.", marker)
                        );
                    break;
                case SolidReport.EntryType.EncodingBadUnicode:
                    retval = new SolidErrorRecordFilter(
                        _currentDictionary,
                        marker,
                        type,
                        string.Format("WARNING: do NOT save! Marker {0} contains bad unicode data.", marker)
                        );
                    break;
            }
            return retval;
        }

        public void BeginBuild(SfmDictionary currentDictionary)
        {
            base.Clear();
            _currentDictionary = currentDictionary;
            var v = EnumUtil.GetEnumValues<SolidReport.EntryType>();

            // Initialize a dict entry for each one. Another option would be to remove this and instead
            // use the GetSetDefault() extension method, but the new RecordFilterSet() constructor would be awkward. -JMC Mar 2014
            foreach(var x in v)
            {
                _solidErrors[x] = new ErrorFilterForType();
            }

            /* //The old way, when the enum went 0 1 2 3 4 5 and not 0 1 2 4 8 16  -JMC
            for (int i = 0; i < (int)SolidReport.NumTypes; i++)
            {
                _solidErrors[i] = new ErrorFilterForType();
            }
             */
        }

        public void EndBuild()
        {
            if (_currentDictionary != null)
            {
                int filterCount = 0;

                // Error Filters
                foreach (KeyValuePair<Enum, ErrorFilterForType> pair in _solidErrors)
                //was: foreach (ErrorFilterForType filter in _solidErrors)
                {
                    var filter = pair.Value;
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

                // Add the All Records filter in first position--this will be the default when first opening the file. Moved this to the top -JMC 2013-09 (see issue #274)
                if (filterCount == 0)
                {
                    Insert(0, AllRecordFilter.CreateAllRecordFilter(_currentDictionary, "All Records (No issues found)"));
                }
                else
                {
                    Insert(0, AllRecordFilter.CreateAllRecordFilter(_currentDictionary, "All Records"));
                }

                //Add(new RegExRecordFilter("Has Note", @"\\nt\s\w", _currentDictionary));
                //Add(new RegExRecordFilter("Missing N Gloss", @"\\gn\s\w", true, _currentDictionary));
                //Add(new RegExRecordFilter("Missing ps", @"\\ps\s\w", true, _currentDictionary));
            }
            //Add(new NullRecordFilter());
        }

        public void AddRecord(int index, SolidReport report)
        {
            foreach (ReportEntry entry in report.Entries)
            {
                ErrorFilterForType filter = _solidErrors.GetOrDefault(entry.EntryType, new ErrorFilterForType());
                if (!filter.ContainsKey(entry.Marker))
                {
                    filter.Add(entry.Marker, CreateSolidErrorRecordFilter(entry.EntryType, entry.Marker));
                }
                filter[entry.Marker].AddEntry(index);
            }
        }

        public void Dispose()
        {
            _solidErrors = null;
            _currentDictionary = null;
        }
    }
}