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
        public string FindThis;
        public Regex FindThisRegex;
        public string ReplaceWith;
        public bool CaseSensitive = false;
        public bool UseRegex = true;
        public bool UseDoubleRegex = false;  //JMC: if true, UseRegex needs to be true too (enforce with get/set ?)
        //JMC: Other possibilities:
        // CultureInvariant  (checkbox "use OS case rules")
        // Multiline (checkbox "allow inline ^ $")
        // Singleline ("dot matches newline")
        
        public SearchViewModel(MainWindowPM model)
        {
            _model = model;
            RecordFormatter = new RecordFormatter();
            RecordFormatter.SetDefaultsUiTree();
        }

        public void setFindThis(string val)
        {
            FindThis = val;
            if (!UseRegex)
            {
                FindThisRegex = null;
                return;
            }

            RegexOptions opt = RegexOptions.Multiline | RegexOptions.Compiled;
            if (!CaseSensitive)
            {
                opt = RegexOptions.IgnoreCase | opt;
            }
            FindThisRegex = new Regex(val, opt);  
        }

        public SfmDictionary Dictionary
        {
            get { return _dictionary; }
            set { _dictionary = value; }
        }

        private static void CantFindWordErrorMessage(string word)
        {
            MessageBox.Show("Cannot find\n'" + word + "'", "Solid", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// Find within the specified filter (specify null for All Records)
        /// If called multiple times with the same values for startingRecord and startingIndex, it will search the 
        /// whole file (including wrap-around with ding) and stop at that starting point. -JMC
        public void FindReplace(string word, string possReplace, int recordIndex, int textIndex, int startingRecord,
                                int startingIndex)
        {
            FindReplace(word, possReplace, recordIndex, textIndex, startingRecord, startingIndex, false);
        }

        /// Same, but with the option to replace all
        public void FindReplace( string word, string possReplace, int recordIndex, int textIndex, int startingRecord, int startingIndex, bool all)
        {
            if (this.Filter == null)
            {
                this.Filter = AllRecordFilter.CreateAllRecordFilter(_dictionary, null);
            }
            this.setFindThis(word);
            this.ReplaceWith = possReplace;
            _startRecordOfWholeSearch = startingRecord;
            _startIndexOfWholeSearch = startingIndex;

            SearchResult result;
            while (true)
            {
                result = NextResult(recordIndex, textIndex);
                if (!all)
                {
                    break;
                }
                else
                {
                    break;  //JMC: Stub; put Replace All functionality here?? Break after one full pass through the file.
                }
            }

            if (result != null)
            {
                WordFound.Invoke(this, new SearchResultEventArgs(result));
            }
            else
            {
                CantFindWordErrorMessage(word);  //JMC:! Without Invoke this is a bit inconsistent; and it launches a messagebox! (a no-no in the model)
            }
        }

        /// Find within the specified filter
        private SearchResult NextResult(int recordIndex, int searchStartIndex)
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

                if (this.UseDoubleRegex)
                {
                    skip = true;
                    // set the context
                    SearchResult tmp = null; // = FindWordInRecord(recordIndex, searchStartIndex, ctxreg, ctxrw, null); //JMC:! unfinished
                    if (tmp != null)
                    { 
                        searchStartIndex = 0;
                        context = tmp.Found;
                        skip = false;
                    }
                } 

                if (!skip)
                {
                    if (this.UseRegex)
                    {
                        searchResult = FindWordInRecord(recordIndex, searchStartIndex, this.FindThisRegex, this.ReplaceWith, context);
                        //Note that context will be null unless UseDoubleRegex is true. -JMC
                    }
                    else
                    {
                        searchResult = FindWordInRecord(recordIndex, searchStartIndex, null, this.ReplaceWith, null);
                    }

                    if (searchResult != null) // (searchResultIndex != -1)
                    {
                        searchResultIndex = searchResult.TextIndex;
                    }
                }

                if (SearchStartingPointPassed(recordIndex, searchStartIndex, searchResultIndex))
                {
                    MakeBing();
                }

                if (searchResult != null)
                {
                    return searchResult;
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
                // JMC:! WARNING! This has to match the editor's textbox perfectly in character count 
                // (esp. identical newlines and indents); so, replace ToStructuredString() with something better               
                recordText = RecordFormatter.FormatPlain(record, _model.MarkerSettingsModel.SolidSettings);

                //string recordTextORIG = record.ToStructuredString(_model.MarkerSettingsModel.SolidSettings);  
                // JMC:! Hack: swap out newline temporarily, since RichTextBox uses plain \n regardless of System.Environment.Newline (\r\n)
                // Is apparently due to round-tripping through RTF: http://stackoverflow.com/questions/7067899/richtextbox-newline-conversion
                // recordText = ReggieTempHack.Replace(recordText, "\n");

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
                    res = new SearchResult(recordIndex, finalTextIndex, filter, f, replaceWith);
                }
            }
            else
            {   
                //Regex mode
                Match m = reg.Match(recordText, startTextIndex);
                if (m.Success)
                {
                    //replaceWith = replaceWith.Replace("\\\\", "\\"); //JMC:! need to deal with backslashed codes here, first.
                    replaceWith = Regex.Unescape(replaceWith);
                    string rw = m.Result(replaceWith);
                    res = new SearchResult(recordIndex, m.Index, filter, m.Value, rw);
                }
            }

            return res;
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

        public void SyncFormat(RecordFormatter rf)
        {
            if (RecordFormatter.ShowIndented != rf.ShowIndented)
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