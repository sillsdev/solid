// Copyright (c) 2007-2014 SIL International
// Licensed under the MIT license: opensource.org/licenses/MIT

using System;
using System.Collections.Generic;
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
        private static Regex RegexTab = new Regex(@"\t", RegexOptions.Compiled | RegexOptions.CultureInvariant);
        //private static Regex ReggieTempHack = new Regex(@"\r\n?", RegexOptions.Compiled | RegexOptions.CultureInvariant);
        public static string NoTabs(string input)  //replace all tabs with spaces
        {
            return RegexTab.Replace(input, " ");
        }


        public event EventHandler<RecordFormatterChangedEventArgs> SearchRecordFormatterChanged;

        private MainWindowPM _model;
        private SfmDictionary _dictionary;
        private int _startRecordOfWholeSearch;
        private int _startIndexOfWholeSearch;

        public RecordFormatter RecordFormatter { get; set; }

        public RecordFilter Filter;

        private RegexItem _reggie;

        public bool CaseSensitive = false;

        private bool _useRegex = false;
        public bool UseRegex
        {
            get { return _useRegex; }
            set
            {
                _useRegex = value;
                if (value == false && UseDoubleRegex)
                {
                    UseDoubleRegex = false;
                }
            }
        }

        private bool _useDoubleRegex = false;
        public bool UseDoubleRegex
        {
            get { return _useDoubleRegex; }
            set
            {
                _useDoubleRegex = value;
                if (value && !UseRegex)
                {
                    UseRegex = true;
                }
            }
            
        }

        public string FindThis;
        public string ReplaceWith;
        //public Regex FindThisRegex;

        //JMC: Other possibilities:
        // CultureInvariant  (unticked checkbox: "use OS case rules")
        // Multiline (ticked checkbox: "allow inline ^ $")
        // Singleline (unticked checkbox: "dot matches newline")

        public static HashSet<string> AlreadyShown; // basically a global variable (I added) -JMC
        
        public SearchViewModel(MainWindowPM model, RecordFormatter recordFormatter)
        {
            _model = model;
            RecordFormatter = recordFormatter;
            AlreadyShown = AlreadyShown ?? new HashSet<string>(); // avoiding null
        }

        public void Setup(RegexItem ri, int recordIndex, int textIndex, bool currentFilter)
        {
            _reggie = ri;
            Setup(null, null, recordIndex, textIndex, currentFilter);
        }
        public void Setup(string f, string r, int recordIndex, int textIndex, bool currentFilter)
        {
            FindThis = f;
            ReplaceWith = r;
            if (currentFilter)
            {
                Filter = _model.NavigatorModel.ActiveFilter;
            }
            else
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
                        if (WordFound != null) WordFound.Invoke(this, new SearchResultEventArgs(result));
                    }
                    else
                    {
                        CantFindWordErrorMessage(_reggie.Find);  //JMC:! Without Invoke this is a bit inconsistent; and it launches a messagebox! (a no-no in the model; my bad)
                    }
                }*/


        /*
        public SearchResult NextResult(int recordIndex, int startIndexChar)
        {
            if (UseDoubleRegex)
            {
                //do tricky stuff
            }
            else
            {
                return NextResult2(recordIndex, startIndexChar);
            }
        }
        */

        /// Find within the specified filter
        public SearchResult NextResult(int recordIndex, int startIndexChar)
        {
            int startingRecordIndex = recordIndex;
            SearchResult searchResult = null;
            int searchResultIndex = -1;
            if (Filter.Count <= 0)
            {
                return null;
            }

            //bool first = true;
            while (true)  // loops once per record until match found (returns immediately) or we pass the starting point.
            {
                searchResultIndex = -1;

                if (UseDoubleRegex) 
                {
                    //get context using first regex to do one find/replace
                    searchResult = FindWordInRecord(recordIndex, startIndexChar, _reggie.ReggieContext, _reggie.ReplaceContext);
                    if (searchResult != null)
                    {
                        //do a replace all in that context using second regex
                        string iv = searchResult.IntermediateValue = searchResult.ReplaceWith;
                        _reggie.Replace = Regex.Unescape(_reggie.Replace);  //deal with backslash codes etc.
                        //string rw = m.Result(_reggie.Replace);
                        searchResult.ReplaceWith = _reggie.Reggie.Replace(iv, _reggie.Replace);
                    }
                }

                else //not double
                {
                    if (UseRegex)
                    {
                        searchResult = FindWordInRecord(recordIndex, startIndexChar, _reggie.Reggie, _reggie.Replace);
                    }
                    else
                    {   //basic mode
                        searchResult = FindWordInRecord(recordIndex, startIndexChar, null, this.ReplaceWith);
                    }

                }

                if (searchResult != null) // (searchResultIndex != -1)
                {
                    searchResultIndex = searchResult.TextIndex;
                }

                if (SearchStartingPointPassed(recordIndex, startIndexChar, searchResultIndex))
                {
                    return null;
                }

                if (searchResult != null)
                {
                    return searchResult;
                }
/*
                if (!first && recordIndex == startingRecordIndex) // have we come back around completely?
                {
                    break;
                }
                first = false;
 */ 
                startIndexChar = 0;
                recordIndex++;
                recordIndex = WrapRecordIndex(recordIndex, this.Filter);
            }
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
        private SearchResult FindWordInRecord(int recordIndex, int startTextIndex, Regex reg, string replaceWith)
        {
            SearchResult res = null;
            Record record = Filter.GetRecord(recordIndex);
            if (record == null)
                return null; // -1;
            string recordText;

            recordText = RecordFormatter.FormatPlain(record, _model.MarkerSettingsModel.SolidSettings);
            recordText = NoTabs(recordText);  // force this regardless of RecordFormatter

            if (reg == null)
            {
                //Basic mode
                string f = _regWindowsNewline.Replace(FindThis, "\n"); // the Find dialog gives \r\n (when multiline) but we need to match the \n from the rich textbox
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
                    res = new SearchResult(recordIndex, finalTextIndex, Filter, f);
                    res.ReplaceWith = replaceWith; //move this out?
                }
            }
            else
            {   
                //Regex mode (or first part of double regex mode)
                Match m = reg.Match(recordText, startTextIndex);
                if (m.Success)
                {
                    replaceWith = Regex.Unescape(replaceWith);  //deal with backslash codes etc.
                    string rw = m.Result(replaceWith);
                    res = new SearchResult(recordIndex, m.Index, Filter, m.Value);
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
                if (SearchRecordFormatterChanged != null) SearchRecordFormatterChanged.Invoke(this, arg);
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