using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Media;
using System.Windows.Forms;

namespace SolidGui
{
    public class SearchPM
    {
        private  SfmDictionary _dictionary;
        private int _startRecordOfWholeSearch;
        private int _startIndexOfWholeSearch;
        
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

        public SfmDictionary Dictionary
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
            MessageBox.Show("Cannot find\n'" + word + "'", "SOLID", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void FindNext( RecordFilter filter, string word, int recordIndex, int textIndex, int startingRecord, int startingIndex)
        {
            SearchResult result = NextResult(filter, word, recordIndex, textIndex);
            _startRecordOfWholeSearch = startingRecord;
            _startIndexOfWholeSearch = startingIndex;

            if (result != null)
            {
                wordFound.Invoke(this, new SearchResultEventArgs(result));
            }
            else
            {
                CantFindWordErrorMessage(word);
            }
        }
        public void FindNext(string word, int recordIndex, int textIndex, int startingRecord, int startingIndex)
        {
            FindNext(AllRecordFilter.CreateAllRecordFilter(_dictionary), word, recordIndex, textIndex, startingRecord, startingIndex);
        }

        private SearchResult NextResult(RecordFilter filter, string word, int recordIndex, int searchStartIndex)
        {
            int startingRecordIndex = recordIndex;
            int searchResultIndex = -1;
            if (filter.Count > 0)
            {
                do
                {
                    searchResultIndex = FindIndexOfWordInRecord(recordIndex, filter, word, searchStartIndex);
                    if (SearchStartingPointPassed(recordIndex, searchStartIndex, searchResultIndex))
                    {
                        MakeBing();
                    }

                    if (searchResultIndex != -1)
                    {
                        return new SearchResult(recordIndex, searchResultIndex, word.Length, filter);
                    }

                    searchStartIndex = 0;
                    recordIndex++;
                    recordIndex = WrapRecordIndex(recordIndex, filter);
                } while (recordIndex != startingRecordIndex);

                searchResultIndex = FindIndexOfWordInRecord(recordIndex, filter, word, searchStartIndex);
                if (SearchStartingPointPassed(recordIndex, searchStartIndex, searchResultIndex))
                {
                    MakeBing();
                }
            }
            
            if (searchResultIndex != -1)
            {
                return new SearchResult(recordIndex, searchResultIndex, word.Length, filter);
            }
            
            return null;
        }

        private int WrapRecordIndex(int recordIndex, RecordFilter filter)
        {
            if (recordIndex >= filter.Count)
            {recordIndex = 0;}
            return recordIndex;
        }

        private int FindIndexOfWordInRecord(int recordIndex, RecordFilter filter, string word, int startTextIndex)
        {
            string recordText;
            int finalTextIndex;
            recordText = filter.GetRecord(recordIndex).ToStructuredString();
            finalTextIndex = recordText.IndexOf(word, startTextIndex);
            return finalTextIndex;
        }

        private void MakeBing()
        {
            SystemSounds.Asterisk.Play();
        }

        private bool SearchStartingPointPassed(int recordIndex, int startTextIndex, int textIndex)
        {
            return recordIndex == _startRecordOfWholeSearch && 
                   startTextIndex <= _startIndexOfWholeSearch &&
                   (textIndex >= _startIndexOfWholeSearch || textIndex == -1);
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
