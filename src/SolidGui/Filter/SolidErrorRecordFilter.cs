// Copyright (c) 2007-2014 SIL International
// Licensed under the MIT license: opensource.org/licenses/MIT

using System;
using System.Collections.Generic;
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

        public void AddEntry(int sfmLexEntryIndex)
        {
            // if (!_indexesOfRecords.Contains(sfmLexEntryIndex))  // Was too inefficient -JMC
            if ( (Count == 0) || (_indexesOfRecords[Count-1] != sfmLexEntryIndex) ) // If the same record has the same error multiple time, just store the record once per filter
            {
                _indexesOfRecords.Add(sfmLexEntryIndex);
            }
        }

        public override IEnumerable<string> HighlightMarkers
        {
            get { return new[] { _marker }; }
        }

        public override string Description(int index)
        {
            return _errorMessages[index];
        }



    }
}