using System;
using System.Media;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using SolidGui.Engine;
using SolidGui.Filter;
using SolidGui.Model;

namespace SolidGui.Search
{
    public class SearchViewModel
    {
        private  SfmDictionary _dictionary;
        private int _startRecordOfWholeSearch;
        private int _startIndexOfWholeSearch;
        private MainWindowPM _model;
        
        public SearchViewModel(MainWindowPM model)
        {
            _model = model;
        }

        public class SearchResultEventArgs : EventArgs
        {
            private readonly SearchResult _searchResult;

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

        public event EventHandler<SearchResultEventArgs> WordFound;

        public SfmDictionary Dictionary
        {
            get { return _dictionary; }
            set { _dictionary = value; }
        }

        private static Regex ReggieTempHack = new Regex(@"\r\n?", RegexOptions.Compiled | RegexOptions.CultureInvariant);

        private static void CantFindWordErrorMessage(string word)
        {
            MessageBox.Show("Cannot find\n'" + word + "'", "Solid", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Find within all records, since no filter was specified
        public void FindNext(string word, int recordIndex, int textIndex, int startingRecord, int startingIndex)
        {
            FindNext(AllRecordFilter.CreateAllRecordFilter(_dictionary, null), word, recordIndex, textIndex, startingRecord, startingIndex);
        }

        // Find within the specified filter
        public void FindNext( RecordFilter filter, string word, int recordIndex, int textIndex, int startingRecord, int startingIndex)
        {
            SearchResult result = NextResult(filter, word, recordIndex, textIndex);
            _startRecordOfWholeSearch = startingRecord;
            _startIndexOfWholeSearch = startingIndex;

            if (result != null)
            {
                WordFound.Invoke(this, new SearchResultEventArgs(result));
            }
            else
            {
                CantFindWordErrorMessage(word);
            }
        }

        // Find within the specified filter
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
                
                // JMC: the following could be refactored? (repeated code)
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
        
        // Return the index of the word (first match), or -1 (if not found)
        private int FindIndexOfWordInRecord(int recordIndex, RecordFilter filter, string word, int startTextIndex)
        {
            var record = filter.GetRecord(recordIndex);
            if (record == null)
                return -1;
            string recordText = record.ToStructuredString(_model.MarkerSettingsModel.SolidSettings);  // JMC:! WARNING! This has to match the editor's textbox perfectly in character count (e.g. identical newlines); so, replace ToStructuredString() with something better

            // JMC:! Hack: swap out newline temporarily, since RichEditControl uses plain \n regardless of System.Environment.Newline (\r\n)
            // Apparently due to round-tripping through RTF: http://stackoverflow.com/questions/7067899/richtextbox-newline-conversion
            recordText = ReggieTempHack.Replace(recordText, "\n");

            int finalTextIndex = recordText.IndexOf(word, startTextIndex);
            return finalTextIndex;
        }

        // Make a dinging sound (well, the system Asterisk). Called on wraparound, or on no match found.
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