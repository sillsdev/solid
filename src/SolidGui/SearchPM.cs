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

        public List<SearchResult> SearchForward(string word, int recordIndex, int textIndex)
        {
            List<SearchResult> results = new List<SearchResult>();
            for (;recordIndex < _masterRecordList.Count; recordIndex++)
            {
                String currentRecord = _masterRecordList[recordIndex].Value;

                textIndex = currentRecord.IndexOf(word, textIndex);
                while (textIndex != -1)
                {
                    results.Add(new SearchResult(recordIndex, textIndex, word.Length));
                    textIndex = currentRecord.IndexOf(word, textIndex);
                }
                textIndex = 0;
            }
            return results;
        }

        public List<SearchResult> SearchBackward(string word, int recordIndex, int textIndex)
        {
            List<SearchResult> results = new List<SearchResult>();
            for (; recordIndex >= 0; recordIndex--)
            {
                String currentRecord = _masterRecordList[recordIndex].Value;

                textIndex = currentRecord.IndexOf(word, textIndex);
                while (textIndex != -1)
                {
                    results.Add(new SearchResult(recordIndex, textIndex, word.Length));
                    textIndex = currentRecord.IndexOf(word, textIndex);
                }
                textIndex = 0;
            }
            return results;
        }

        public void FindNext(string word, int recordIndex, int textIndex)
        {
          
            for (; recordIndex < _masterRecordList.Count && recordIndex >= 0; recordIndex++)
            {
                String currentRecord = _masterRecordList[recordIndex].Value;

                textIndex = currentRecord.IndexOf(word, textIndex);
                if(textIndex != -1)
                {
                    SearchResult result = new SearchResult(recordIndex, textIndex, word.Length);
                    wordFound.Invoke(this, new SearchResultEventArgs(result));
                    return;
                }
                textIndex = 0;
            }
            System.Windows.Forms.MessageBox.Show("Can't Find \"" + word + "\"");
        }

        public void FindPrevious(string word, int recordIndex, int textIndex)
        {

            for (; recordIndex >= 0 && recordIndex < _masterRecordList.Count; recordIndex--)
            {
                String currentRecord = _masterRecordList[recordIndex].Value;

                int count = 0;
                int currentIndex = textIndex;
                do
                {
                    textIndex = currentRecord.IndexOf(word, currentIndex, count);
                    currentIndex--;
                    count++;
                } 
                while (textIndex == -1 && currentIndex > 0);

                if (textIndex != -1)
                {
                    SearchResult result = new SearchResult(recordIndex, textIndex, word.Length);
                    wordFound.Invoke(this, new SearchResultEventArgs(result));
                    return;
                }
                if (recordIndex > 0)
                {
                    textIndex = _masterRecordList[recordIndex - 1].Value.Length;
                }
            }
            System.Windows.Forms.MessageBox.Show("Can't Find \"" + word + "\"");
        }
    }
}
