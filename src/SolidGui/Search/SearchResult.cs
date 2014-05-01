// Copyright (c) 2007-2014 SIL International
// Licensed under the MIT license: opensource.org/licenses/MIT

using SolidGui.Filter;

namespace SolidGui.Search
{
    public class SearchResult
    {
        private int _recordIndex;
        private int _textIndex;
        private int _resultLength;
        private RecordFilter _filter;
        private string _found;
        private string _replaceWith;

        public SearchResult(int recordIndex, int textIndex, RecordFilter filter, string found)
        {
            Filter = filter;
            RecordIndex = recordIndex;    //record within the filtered list of records
            TextIndex = textIndex;        //line within the matching record
            _found = found;               //the exact matching string (needed for regex)
            //_replaceWith = replaceWith;   //what a Replace would replace it with (needed for regex)
        }

        // Added these two properties so we can support regex find/replace, in which these values vary -JMC Feb 2014
        public string Found
        {
            get { return _found; }
        }

        public string ReplaceWith
        {
            get { return _replaceWith; }
            set { _replaceWith = value; }
        }

        // Added this one to support double regex. -JMC Apr 2014
        public string IntermediateValue = "";
        
        public RecordFilter Filter
        {
            get { return _filter; }
            set { _filter = value; }
        }

        /*
        public int ResultLength
        {
            get
            {
                return _resultLength;
            }
            set
            {
                _resultLength = value;
            }
        }
         */

        public int RecordIndex
        {
            get
            {
                return _recordIndex;
            }
            set
            {
                _recordIndex = value;
            }
        }

        public int TextIndex
        {
            get
            {
                return _textIndex;
            }
            set
            {
                _textIndex = value;
            }
        }
    }
}