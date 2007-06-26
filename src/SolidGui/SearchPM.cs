using System;
using System.Collections.Generic;

namespace SolidGui
{
    public class SearchPM
    {
        private List<Record> _masterRecordList;

        public class SearchResultEventArgs : EventArgs
        {
            private SearchResult _searchResult;

            public SearchResultEventArgs(SearchResult value)
            {
                _searchResult = value;
            }

            public SearchResult SearchResult
            {
                get
                {
                    return _searchResult;
                }
            }
        }

        public event EventHandler<SearchResultEventArgs> wordFound;

        public List<Record>MasterRecordList
        {
            get
            {
                return _masterRecordList;
            }
            set
            {
                _masterRecordList = value;
            }
        }
        private static void CantFindWordErrorMessage(string word)
        {
            System.Windows.Forms.MessageBox.Show("Can't Find \"" + word + "\"");
        }

        public void FindNext(string word, int recordIndex, int textIndex)
        {
            SearchResult result = NextForwardResult(word, recordIndex, textIndex);

            if (result != null)
            {
                wordFound.Invoke(this, new SearchResultEventArgs(result));
            }
            else
            {
                CantFindWordErrorMessage(word);
            }
        }

        private SearchResult NextForwardResult(string word, int recordIndex, int textIndex)
        {
            for (; recordIndex < _masterRecordList.Count && recordIndex >= 0; recordIndex++)
            {
                String currentRecord = _masterRecordList[recordIndex].Value;

                textIndex = currentRecord.IndexOf(word, textIndex);
                if (textIndex != -1)
                {
                    return new SearchResult(recordIndex, textIndex, word.Length);
                }
                textIndex = 0;
            }
            return null;
        }

        private SearchResult NextBackwardResult(string word, int recordIndex, int textIndex)
        {
            for (; recordIndex >= 0 && recordIndex < _masterRecordList.Count; recordIndex--)
            {
                String currentRecord = _masterRecordList[recordIndex].Value;
                
                textIndex = currentRecord.LastIndexOf(word, textIndex);
                
                if(textIndex != -1)
                {
                    return new SearchResult(recordIndex, textIndex, word.Length);
                }
                if (recordIndex > 0)
                {
                    textIndex = _masterRecordList[recordIndex - 1].Value.Length;
                }
            }
            return null;
        }

        public void FindPrevious(string word, int recordIndex, int textIndex)
        {
            SearchResult result = NextBackwardResult(word, recordIndex, textIndex);
            
            if(result != null)
            {
                wordFound.Invoke(this, new SearchResultEventArgs(result));
            }
            else
            {
                CantFindWordErrorMessage(word);   
            }
        }
    }
}
