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
        public static Regex RegexTab = new Regex(@"\t", RegexOptions.Compiled | RegexOptions.CultureInvariant);
        //private static Regex ReggieTempHack = new Regex(@"\r\n?", RegexOptions.Compiled | RegexOptions.CultureInvariant);

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
        public event EventHandler<RecordFormatterChangedEventArgs> SearchRecordFormatterChanged;

        private MainWindowPM _model;
        private SfmDictionary _dictionary;
        private int _startRecordOfWholeSearch;
        private int _startIndexOfWholeSearch;

        public RecordFormatter RecordFormatter { get; set; }

        public RecordFilter Filter;

        private RegexItem _reggie;

        public bool CaseSensitive = false;
        public bool UseRegex = true;
        public bool UseDoubleRegex = false;  //JMC: if true, UseRegex needs to be true too (enforce with get/set ?)

        public string FindThis;
        public string ReplaceWith;
        //public Regex FindThisRegex;

        //JMC: Other possibilities:
        // CultureInvariant  (checkbox "use OS case rules")
        // Multiline (checkbox "allow inline ^ $")
        // Singleline ("dot matches newline")

        public SearchViewModel(MainWindowPM model, RecordFormatter recordFormatter)
        {
            _model = model;
            RecordFormatter = recordFormatter;
        }

        public void Setup(RegexItem ri, int recordIndex, int textIndex)
        {
            _reggie = ri;
            Setup(null, null, recordIndex, textIndex);
        }
        public void Setup(string f, string r, int recordIndex, int textIndex)
        {
            FindThis = f;
            ReplaceWith = r;
            if (Filter == null)
            {
                Filter = AllRecordFilter.CreateAllRecordFilter(_dictionary, null);
            }
            //we're starting our first find in a potential series; bookmark this
            _startRecordOfWholeSearch = recordIndex;
            _startIndexOfWholeSearch = textIndex;
        }

/* JMC: delete
        public void setFindThis(string val)
        {
            FindThis = val;
            if (!UseRegex)
            {
                _reggie = null;
                return;
            }
            _reggie = _reggie ?? new RegexItem();
            _reggie.setFind(val, CaseSensitive);
        }
        */
        public SfmDictionary Dictionary
        {
            get { return _dictionary; }
            set { _dictionary = value; }
        }

        // Find within the specified filter (specify null for All Records)
        // If called multiple times with the same values for startingRecord and startingIndex, it will search the 
        // whole file (including wrap-around with ding) and stop at that starting point. -JMC
        //public void FindReplace(int recordIndex, int textIndex, int startingRecord, int startingIndex, bool all)
                 
            /*
            string word = FindThis;
            string possReplace = ReplaceWith;
            if (_reggie != null)
            {
                word = _reggie.Find;
                possReplace = _reggie.Replace;
            }
            */
        
/*            if (this.Filter == null)
            {
                this.Filter = AllRecordFilter.CreateAllRecordFilter(_dictionary, null);
            }
            _startRecordOfWholeSearch = startingRecord;
            _startIndexOfWholeSearch = startingIndex;

            SearchResult result;

            result = NextResult(recordIndex, textIndex);
            if (result != null)
            {
                WordFound.Invoke(this, new SearchResultEventArgs(result));
            }
            else
            {
                CantFindWordErrorMessage(_reggie.Find);  //JMC:! Without Invoke this is a bit inconsistent; and it launches a messagebox! (a no-no in the model; my bad)
            }
        }*/

        /// Find within the specified filter
        public SearchResult NextResult(int recordIndex, int startIndexChar)
        {
            RecordFilter filter = this.Filter;
            Regex reg = null;

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

                string context = null;
                bool skip = false;

                if (this.UseDoubleRegex) //JMC:! unfinished
                {
                    skip = true;
                    // set the context
                    SearchResult tempResult = FindWordInRecord(recordIndex, startIndexChar, _reggie.ReggieContext,
                        _reggie.ReplaceContext, null); 
                    startIndexChar = 0;
                    context = tempResult.Found;
                    if (!String.IsNullOrEmpty(context))
                    {
                        _reggie.ContextFound = tempResult;
                        skip = false;  //found the context; can now do the find
                    }
                } 

                if (!skip)
                {
                    if (this.UseRegex)
                    {
                        searchResult = FindWordInRecord(recordIndex, startIndexChar, _reggie.Reggie, _reggie.Replace, context);
                        //Note that context will be null unless UseDoubleRegex is true. -JMC
                    }
                    else
                    {   //basic mode
                        searchResult = FindWordInRecord(recordIndex, startIndexChar, null, this.ReplaceWith, null);
                    }

                    if (searchResult != null) // (searchResultIndex != -1)
                    {
                        searchResultIndex = searchResult.TextIndex;
                    }
                }

                if (SearchStartingPointPassed(recordIndex, startIndexChar, searchResultIndex))
                {
                    return null;
                }

                if (searchResult != null)
                {
                    WordFound.Invoke(this, new SearchResultEventArgs(searchResult));  //move to dialog? -JMC
                    return searchResult;
                }
/*
                if (!first && recordIndex == startingRecordIndex) // have we come back around completely?
                {
                    break;
                }
 */ 
                first = false;
                startIndexChar = 0;
                recordIndex++;
                recordIndex = WrapRecordIndex(recordIndex, filter);
            }
            
            return null;
        }

        private int WrapRecordIndex(int recordIndex, RecordFilter filter)  //probably should be static -JMC
        {
            if (recordIndex >= filter.Count)
            {
                recordIndex = 0;
            }
            return recordIndex;
        }


        private static Regex _regWindowsNewline = new Regex( @"\r\n", 
            RegexOptions.Compiled | RegexOptions.CultureInvariant);
        
        // Return the index and the word (first match), or null (if not found). Uses a regex if reg is not null; otherwise uses this.FindThis .
        private SearchResult FindWordInRecord(int recordIndex, int startTextIndex, Regex reg, string replaceWith, string context)
        {
            RecordFilter filter = this.Filter;
            SearchResult res = null;

            Record record = filter.GetRecord(recordIndex);
            if (record == null)
                return null; // -1;
            string recordText;
            if (context == null)
            {
                recordText = RecordFormatter.FormatPlain(record, _model.MarkerSettingsModel.SolidSettings);
                recordText = RegexTab.Replace(recordText, " "); //replace all tabs with spaces
            }
            else
            {
                recordText = context; 
            }

            if (reg == null)
            {
                //Basic mode
                string f = _regWindowsNewline.Replace(FindThis, "\n"); // the Find dialog gives \r\n (when multiline) but needs to match the \n from the rich textbox
                string rec = _regWindowsNewline.Replace(recordText, "\n"); // just in case
                string rec2 = rec;
                string f2 = f;
                if (!CaseSensitive)
                {
                    rec2 = rec.ToLowerInvariant();
                    f2 = f.ToLowerInvariant();
                }

                int finalTextIndex = rec2.IndexOf(f2, startTextIndex);
                if (finalTextIndex > -1)
                {
                    res = new SearchResult(recordIndex, finalTextIndex, filter, f);
                    res.ReplaceWith = replaceWith; //move this out?
                }
            }
            else
            {   
                //Regex mode (or double regex mode)
                Match m = reg.Match(recordText, startTextIndex);
                if (m.Success)
                {
                    replaceWith = Regex.Unescape(replaceWith);  //deal with backslash codes etc.
                    string rw = m.Result(replaceWith);
                    res = new SearchResult(recordIndex, m.Index, filter, m.Value);
                    res.ReplaceWith = rw; //move this out?
                }
            }

            return res;
        }

        public bool SearchStartingPointPassed(int recordIndex, int startTextIndex, int textIndex)
        {
            return recordIndex == _startRecordOfWholeSearch &&   // we're back to our starting record
                   startTextIndex <= _startIndexOfWholeSearch &&   // and character
                   (textIndex >= _startIndexOfWholeSearch || textIndex == -1); // and 
        }

        public void SyncFormat(RecordFormatter rf)
        {
            if (RecordFormatter.ShowIndented != rf.ShowIndented)  //prevents ping-ponging invokes
            {
                // Mismatch. We now need to get in sync with the editing pane's indentation.
                RecordFormatter = rf;
                var arg = new RecordFormatterChangedEventArgs(rf);
                SearchRecordFormatterChanged.Invoke(this, arg);
                /*
                _recordFormatter = new RecordFormatter();
                if (editorRF.Indented)
                {
                    _recordFormatter.SetDefaultsUiTree();
                }
                else
                {
                    _recordFormatter.SetDefaultsUiFlat();
                }
                 */
            }
        }


    }
}