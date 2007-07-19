using System;
using System.Collections.Generic;

namespace SolidGui
{
    public class SearchPM
    {
        private  Dictionary _dictionary;
        
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

        public Dictionary Dictionary
        {
            get
            {
                return _dictionary;
            }
            set
            {
                _dictionary = value;
            }
        }
        private static void CantFindWordErrorMessage(string word)
        {
            System.Windows.Forms.MessageBox.Show("Can't Find \"" + word + "\"");
        }

        public void FindNext( RecordFilter filter, string word, int recordIndex, int textIndex)
        {
            SearchResult result = NextResult(filter, word, recordIndex, textIndex);

            if (result != null)
            {
                wordFound.Invoke(this, new SearchResultEventArgs(result));
            }
            else
            {
                CantFindWordErrorMessage(word);
            }
        }
        public void FindNext(string word, int recordIndex, int textIndex)
        {
            FindNext(AllRecordFilter.CreateAllRecordFilter(_dictionary), word, recordIndex, textIndex);
        }

        private SearchResult NextResult(RecordFilter filter, string word, int recordIndex, int textIndex)
        {
            int startingRecordIndex = recordIndex;
            string recordText;
            do
            {
                recordText = filter.GetRecord(recordIndex).ToStructuredString();
                textIndex = recordText.IndexOf(word, textIndex);
                if(textIndex != -1)
                {
                    return new SearchResult(recordIndex, textIndex, word.Length, filter);
                }

                recordIndex++;
                if (recordIndex >= filter.Count)
                {
                    recordIndex = 0;
                }
                textIndex = 0;
            } while (recordIndex != startingRecordIndex);

            recordText = filter.GetRecord(recordIndex).ToStructuredString();
            textIndex = recordText.IndexOf(word, textIndex);
            if (textIndex != -1)
            {
                return new SearchResult(recordIndex, textIndex, word.Length, filter);
            }
            
            return null;
        }
        /*
                private SearchResult NextForwardResult(string word, int recordIndex, int textIndex)
                {
                    for (; recordIndex < _dictionary.Count && recordIndex >= 0; recordIndex++)
                    {
                        String currentRecord = _dictionary.Current.ToStructuredString();
                        _dictionary.MoveToNext();

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
                        string currentRecord = _masterRecordList[recordIndex].ToStructuredString();
                
                        textIndex = currentRecord.LastIndexOf(word, textIndex);
                
                        if(textIndex != -1)
                        {
                            return new SearchResult(recordIndex, textIndex, word.Length);
                        }
                        if (recordIndex > 0)
                        {
                            textIndex = (_masterRecordList[recordIndex - 1].ToStructuredString().Length)-1;
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
     */
    }
}
