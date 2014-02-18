// Copyright (c) 2007-2014 SIL International
// Licensed under the MIT license: opensource.org/licenses/MIT

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
        private MainWindowPM _model;
        private SfmDictionary _dictionary;
        private int _startRecordOfWholeSearch;
        private int _startIndexOfWholeSearch;
        
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

        /// Find within the specified filter (specify null for All Records)
        /// If called multiple times with the same values for startingRecord and startingIndex, it will search the 
        /// whole file (including wrap-around with ding) and stop at that starting point. -JMC
        public void FindNext( RecordFilter filter, string word, string possReplace, int recordIndex, int textIndex, int startingRecord, int startingIndex)
        {
            if (filter == null)
            {
                filter = AllRecordFilter.CreateAllRecordFilter(_dictionary, null);
            }
            SearchResult result = NextResult(filter, word, possReplace, recordIndex, textIndex);
            _startRecordOfWholeSearch = startingRecord;
            _startIndexOfWholeSearch = startingIndex;

            if (result != null)
            {
                WordFound.Invoke(this, new SearchResultEventArgs(result));
            }
            else
            {
                CantFindWordErrorMessage(word);  //JMC: This is a bit inconsistent, since it launches a messagebox instead of invoking an event
            }
        }

        /// Find within the specified filter
        private SearchResult NextResult(RecordFilter filter, string word, string replaceWith, int recordIndex, int searchStartIndex)
        {
            int startingRecordIndex = recordIndex;
            SearchResult searchResult = null;
            int searchResultIndex = -1;
            if (filter.Count <= 0)
            {
                return null;
            }

            bool first = true;
            while (true)
            {
                searchResultIndex = -1;
                searchResult = FindWordInRecord(recordIndex, filter, word, replaceWith, searchStartIndex);
                if (searchResult != null) // (searchResultIndex != -1)
                {
                    searchResultIndex = searchResult.TextIndex;
                }
                if (SearchStartingPointPassed(recordIndex, searchStartIndex, searchResultIndex))
                {
                    MakeBing();
                }

                if (searchResult != null) // (searchResultIndex != -1)
                {
                    return searchResult; // new SearchResult(recordIndex, searchResultIndex, word.Length, filter, f, rw);
                }

                if (!first && recordIndex == startingRecordIndex) // have we come back around completely?
                {
                    break;
                }
                first = false;
                searchStartIndex = 0;
                recordIndex++;
                recordIndex = WrapRecordIndex(recordIndex, filter);
            }
            
            return null;
        }

        private int WrapRecordIndex(int recordIndex, RecordFilter filter)
        {
            if (recordIndex >= filter.Count)
            {
                recordIndex = 0;
            }
            return recordIndex;
        }
        
        // Return the index of the findThis (first match), or -1 (if not found)
        private SearchResult FindWordInRecord(int recordIndex, RecordFilter filter, string findThis, string replaceWith, int startTextIndex)
        {
            var record = filter.GetRecord(recordIndex);
            if (record == null)
                return null; // -1;
            string recordText = record.ToStructuredString(_model.MarkerSettingsModel.SolidSettings);  // JMC:! WARNING! This has to match the editor's textbox perfectly in character count (e.g. identical newlines); so, replace ToStructuredString() with something better

            // JMC:! Hack: swap out newline temporarily, since RichEditControl uses plain \n regardless of System.Environment.Newline (\r\n)
            // Is apparently due to round-tripping through RTF: http://stackoverflow.com/questions/7067899/richtextbox-newline-conversion
            recordText = ReggieTempHack.Replace(recordText, "\n");

            int finalTextIndex = recordText.IndexOf(findThis, startTextIndex);
            string foundThis = findThis;

            if (finalTextIndex == -1)
            {
                return null;
            }
            return new SearchResult(recordIndex, finalTextIndex, filter, foundThis, replaceWith);
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

    }
}