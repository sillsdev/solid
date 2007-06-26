using System;
using System.Collections.Generic;
using System.Text;

namespace SolidGui
{
    public class SearchResult
    {
        private int _recordIndex;
        private int _textIndex;
        private int _resultLength;

        public SearchResult(int recordIndex, int textIndex, int resultLength)
        {
            RecordIndex = recordIndex;
            TextIndex = textIndex;
            ResultLength = resultLength;
        }

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
