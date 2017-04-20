// Copyright (c) 2007-2014 SIL International
// Licensed under the MIT license: opensource.org/licenses/MIT

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Media;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using SolidGui.Model;

namespace SolidGui.Search
{
    // Created to replace the simpler SearchView dialog that already existed. Very similar code, in basic mode. -JMC Feb 2014
    public partial class FindReplaceDialog : Form
    {
        private SfmEditorView _sfmEditorView;

        private SearchViewModel _searchModel; // just a reference to MainWindowPm.SearchModel 

        private readonly Color _errorColor = Color.DarkRed;
        private readonly Color _changeColor = Color.DarkGreen;
        private readonly Color _noChangeColor = Color.Black;

        private static Regex _regLinuxNewline = new Regex(@"\r?\n",
            RegexOptions.Compiled | RegexOptions.CultureInvariant);

        private int _cursorIndex;
        private int _startingTextIndex = -1;
        private int _startingRecordIndex = -1;

        private SearchResult _searchResult;

        public int RecordIndex { get; set; }

        public int CursorIndex
        {
            set
            {
                if (value < _sfmEditorView.ContentsBox.Text.Length)
                {
                    _cursorIndex = value;
                }
                // JMC: else throw an exception?
            }
            get { return _cursorIndex; }
        }

        private MainWindowPM _model;

        public event EventHandler<SearchResultEventArgs> WordFound;
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

        public FindReplaceDialog(SfmEditorView sfmEditorView, MainWindowPM model)
        {

            InitializeComponent();
            _scopeComboBox.SelectedIndex = 0;
            if (DesignMode) return;

            // JMC: Add a call here to KeyboardController.Register() ? Would not need to be as smart as for the rich edit control. Ideally, we'd probably default to the vernacular keyboard?
            _sfmEditorView = sfmEditorView;

            if (model != null) _searchModel = model.SearchModel;
            _model = model;
            ResetStartingPoint();
        }

        public void SetFields(RegexItem r)
        {
            checkBoxMultiline.Checked = false;
            checkBoxCaseSensitive.Checked = true;

            if (r.Double)
            {
                radioButtonDoubleRegex.Checked = true;
            }
            else
            {
                radioButtonRegex.Checked = true;
            }
            textBoxContextFind.Text = r.FindContext;
            textBoxContextReplace.Text = r.ReplaceContext;
            textBoxFind.Text = r.Find;
            textBoxReplace.Text = r.Replace;
            HelpMessage = r.HelpMessage;
        }

        private void ResetStartingPoint() // and Refresh
        {
            _startingRecordIndex = -1;
            _startingTextIndex = -1;
            _searchResult = null; // has ch
            //_sfmEditorView. Select();

            // If the user pastes in tabs, change them to spaces
            textBoxFind.Text = SearchViewModel.NoTabs(textBoxFind.Text);
            textBoxContextFind.Text = SearchViewModel.NoTabs(textBoxContextFind.Text);
            textBoxReplace.Text = SearchViewModel.NoTabs(textBoxReplace.Text);
            textBoxContextReplace.Text = SearchViewModel.NoTabs(textBoxContextReplace.Text);
            //textBoxContextReplace.Text = SearchViewModel.RegexTab.Replace(textBoxContextReplace.Text, " ");

            //JMC:! This is probably also the best time to update the Replace preview. But be careful to not instantly wipe out error messages (for \x \c etc.)
            textBoxReplacePreview.Text = "";
            textBoxContextPreview.Text = "";

            UpdateDisplay();
            IsValidSearch();
        }

        private void OnRadioButtonMode_CheckedChanged(object sender, EventArgs e)
        {
            ResetStartingPoint();

            /*
            // With leading spaces, tree view doesn't fit well with serious (regex) Find
            if (!radioButtonModeBasic.Checked)
            {
                var rf = new RecordFormatter();
                rf.SetDefaultsUiFlat();
                _searchModel.SyncFormat(rf);
            }
             */

            UpdateDisplay();
            bool dubble = radioButtonDoubleRegex.Checked;
            // _searchModel.UseDoubleRegex = dubble;  //JMC: should probably do this here (or in an ApplyToModel() method), rather than in Find()
            if (dubble)
            {
                textBoxContextFind.Focus();
            }
            else
            {
                textBoxFind.Focus();
            }
        }

        public void ShowHelp()
        {
            if (String.IsNullOrEmpty(this.HelpMessage) || SearchViewModel.AlreadyShown.Contains(HelpMessage)) return;
            SearchViewModel.AlreadyShown.Add(HelpMessage);
            MessageBox.Show(HelpMessage, "About the selected recipe", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void UpdateDisplay()
        {
            buttonHelp.Visible = !(String.IsNullOrEmpty(HelpMessage));

            bool dubble = radioButtonDoubleRegex.Checked;
            groupBoxFindContext.Enabled = dubble;

            if (dubble || radioButtonRegex.Checked)
            {
                checkBoxMultiline.Checked = false;
            }

            //No longer using leading spaces (if indent works well) -JMC Mar 2014
            /* 
            //Make the radio button compatible with editor's current indentation, since
            //with leading spaces, tree view is not very compatible with serious regex mode
            bool indent = _searchModel.RecordFormatter.ShowIndented;
            if (indent)
            {
                
                if (!radioButtonModeBasic.Checked)
                {
                    radioButtonModeBasic.Select();
                }
            }
             */
        }


        public void OnEditorRecordFormatterChanged(object sender, RecordFormatterChangedEventArgs e)
        {
            ResetStartingPoint();
            _searchModel.SyncFormat(e.NewFormatter);
            UpdateDisplay();
        }

        public void SelectFind()
        {
            this.textBoxFind.Select();
            this.textBoxFind.SelectAll();
        }

        private bool SafeToReplace()
        {
            if (_searchResult != null)
            {
                // result of previous find
                string f = SearchViewModel.NoTabs(_searchResult.Found);
                string sel = SearchViewModel.NoTabs(_sfmEditorView.ContentsBox.SelectedText);
                return sel.ToLowerInvariant() == f.ToLowerInvariant();
            }
            return false;
        }

        // JMC: This code is highly parallel to ValidSearch()
        // but it assumes that we're not in Basic mode
        private RegexItem GetReggie()
        {
            var ri = new RegexItem();
            bool cs = checkBoxCaseSensitive.Checked;
            bool dubble = radioButtonDoubleRegex.Checked;
            string f = textBoxFind.Text;
            string fc = textBoxContextFind.Text;
            ri.SetFind(f, cs);
            ri.Replace = textBoxReplace.Text;
            if (dubble)
            {
                ri.SetFindContext(fc, f, cs);
                ri.ReplaceContext = textBoxContextReplace.Text;
            }
            return ri;
        }

        private int GetTextIndex()
        {
            return _sfmEditorView.ContentsBox.SelectionStart + 1;
                //JMC:! I don't like the +1 but be careful when tweaking any of this stuff.
        }

        private int GetRecordIndex(bool currentFilter)
        {
            // firstTime = (_startingTextIndex == -1); //why?

            if (currentFilter)
            {
                return _model.NavigatorModel.ActiveFilter.CurrentIndex; // position in current filter instead
            }
            return _model.NavigatorModel.CurrentRecordID; // position in file (i.e. in the All filter)
        }

        // Note the textbox side effect(s), and the significant overlap with the code GetReggie()
        private bool IsValidSearch()
        {
            bool success = true;  //assume the best
            bool dubble = radioButtonDoubleRegex.Checked;
            bool single = radioButtonRegex.Checked;
            bool cs = checkBoxCaseSensitive.Checked;
            string f = textBoxFind.Text;
            string fc = textBoxContextFind.Text;
            string msgEmpty = "Please provide something to find (even just a '^' or '$' regex will suffice).";

            textBoxContextPreview.ForeColor = _noChangeColor;
            textBoxContextPreview.Text = "";
            if (dubble && (fc == ""))
            {
                textBoxContextPreview.ForeColor = _errorColor;
                textBoxContextPreview.Text = msgEmpty;
                success = false;
            }
            textBoxReplacePreview.ForeColor = _noChangeColor;
            textBoxReplacePreview.Text = "";
            if (f == "")
            {
                textBoxReplacePreview.ForeColor = _errorColor;
                textBoxReplacePreview.Text = msgEmpty;
                success = false;
            }

            if (single || dubble)
            {
                var ri = new RegexItem();
                string msg = "Problem with regex: {0}";
                try
                {
                    ri.SetFind(f, cs);
                }
                catch (Exception error)  //Don't crash on bad regex. Just catching ArgumentException wasn't enough; e.g. typing the following regex would crash with an IndexOutOfRangeException: (?<-
                {
                    textBoxReplacePreview.ForeColor = _errorColor; 
                    textBoxReplacePreview.Text = string.Format(msg, error);
                    success = false;
                }
                if (dubble)
                {
                    try
                    {
                        ri.SetFindContext(fc, f, cs);
                    }
                    catch (ArgumentException error)
                    {
                        textBoxContextPreview.ForeColor = _errorColor;
                        textBoxContextPreview.Text = string.Format(msg, error);
                        success = false;
                    }
                }

            }
            return success;
        
        }
    

        /// <summary>
        /// Finds and/or replaces one or more occurrences. The two parameters define diverse behaviors.
        /// (false, false) : find one occurrence
        /// (true, false) : replace the currently highlighted occurrence.
        /// (true, true) : replace all
        /// (false, true) : undefined, currently (no "Find All" or "Count" features yet)
        /// </summary>
        /// <param name="replace"></param>
        /// <param name="replaceAll"></param>
        /// <returns></returns>
        private bool Find(bool replace, bool replaceAll)
        {
            RegexItem ri = null;
            textBoxReplacePreview.Text = "";
            bool scopeCurrent = (_scopeComboBox.SelectedIndex == 0); // "Current Filter" (formerly "Check Result") -JMC
            RecordIndex = GetRecordIndex(scopeCurrent);
            _startingRecordIndex = RecordIndex;  //do we need this?
            bool single = radioButtonRegex.Checked;
            bool dubble = radioButtonDoubleRegex.Checked;
            bool reg = single || dubble;
            if (!IsValidSearch())
            {
                return false; //bail
            }
            if (reg)
            {
                //regex (single or double)
                ri = GetReggie();
                _searchModel.Setup(ri, RecordIndex, GetTextIndex()-1, scopeCurrent);  //JMC: I don't like the -1
            }
            else
            {
                //basic
                _searchModel.Setup(textBoxFind.Text, textBoxReplace.Text, RecordIndex, GetTextIndex() - 1, scopeCurrent);   //JMC: I don't like the -1
            }
            _searchModel.UseRegex = reg;
            _searchModel.UseDoubleRegex = dubble;
            _searchModel.CaseSensitive = checkBoxCaseSensitive.Checked;

            string f = null;
            string rw = null;
            if (_searchResult != null)
            {  // result of previous find
                rw = _searchResult.ReplaceWith;
            }

            try
            {
                while (true)
                {
                    if (replace)
                    {
                        if (rw == null) throw new Exception("Cannot replace text with null.");
                        if (SafeToReplace())
                        {
                            // Replace current selection
                            int origCursor = _sfmEditorView.ContentsBox.SelectionStart;
                            _sfmEditorView.ContentsBox.SelectedText = rw; //do the replace
                            _sfmEditorView.UpdateModelFromView();
                            _sfmEditorView.ContentsBox.SelectionStart = origCursor + rw.Length;  //move the cursor
                            if (!replaceAll)
                            {
                                return true;
                            }
                        }
                    }
                    else
                    {
                        // Find next
                        CursorIndex = GetTextIndex();
                        RecordIndex = GetRecordIndex(scopeCurrent);

                        SearchResult result = _searchModel.NextResult(RecordIndex, CursorIndex);
                        _searchResult = result; // used to be in this.OnWordFound() -JMC Jun 2014

                        if (result == null)
                        {
                            CantFindWordErrorMessage(ri, textBoxFind.Text);
                            ResetStartingPoint();
                        }
                        else
                        {
                            if (result.IntermediateValue != result.Found) textBoxContextPreview.ForeColor = _changeColor;
                            string tmp = "[{0}]"; // "Context found:\n[{0}]";
                            tmp = string.Format(tmp, result.IntermediateValue);
                            tmp = _regLinuxNewline.Replace(tmp, "\r\n");
                            textBoxContextPreview.Text = tmp;
                            string compareTo = (dubble) ? result.IntermediateValue : result.Found;
                            if (result.ReplaceWith != compareTo) textBoxReplacePreview.ForeColor = _changeColor;
                            tmp = "[{0}]"; // "Will replace selection with this:\r\n[{0}]";
                            tmp = string.Format(tmp, result.ReplaceWith);
                            tmp = _regLinuxNewline.Replace(tmp, "\r\n");
                            textBoxReplacePreview.Text = tmp;
                        }

                        if (replaceAll)
                        {
                            if (result == null)
                            {
                                //MakeBing();
                                break;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }

                    replace = (!replace); // toggle; simplifies "Replace + Find"
                }  // while (true)

                if (_searchResult != null)
                {
                    if (WordFound != null) WordFound.Invoke(this, new SearchResultEventArgs(_searchResult));  // Used to be in SearchViewModel; note that this only gets invoked for the *last* Replace. -JMC Jun 2014
                }

            }
            catch (ArgumentException error)
            {
                this.textBoxReplacePreview.Text = error.ToString(); // Show the user why their input was bad. -JMC
            }
            catch (Exception error)
            {
                string msg = string.Format("An unexpected error occurred while searching:\r\n{0}\r\n", error);
                // JMC:! Palaso bug? If I don't include error here, the real exception name won't be in the report at all!
                //SIL.Reporting.ErrorReport.ReportNonFatalException(error);
                SIL.Reporting.ErrorReport.ReportNonFatalExceptionWithMessage(error, msg);
            }

            // bring the search form back into focus -JMC
            //this.BringToFront();
            //this.Focus();
            return (_searchResult != null);
        }

        // Make a dinging sound (well, the system Asterisk). Called on wraparound.
        private void MakeBing()
        {
            SystemSounds.Asterisk.Play();
        }

        /// <summary>
        /// Only one of the parameters will be used (prefers ri if non-null).
        /// </summary>
        /// <param name="ri">Should be non-null if in regex mode.</param>
        /// <param name="find">Pass a plain string if in basic mode.</param>
        private static void CantFindWordErrorMessage(RegexItem ri, string find)
        {
            if (ri != null)
            {
                find = ri.FindContext + "\r\n" + ri.Find;
            }
            MessageBox.Show("Cannot find\r\n" + find, "Solid", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void OnFindNextButton_Click(object sender, EventArgs e)
        {
            Find(false, false);
            textBoxFind.Focus();
        }

        /// <summary>
        /// Replace the current selection if it's safe to do so (i.e. is is a found match).
        /// Otherwise, do a Find Next.
        /// </summary>
        private bool OneReplaceOrFind()
        {
            bool doReplace = SafeToReplace();
            if (doReplace)
            {
                Find(true, false); // Replace
            }
            else
            {
                Find(false, false); // Find
            }
            textBoxReplace.Focus();
            return doReplace;
        }
        
        private void OnReplaceButton_Click(object sender, EventArgs e)
        {
            OneReplaceOrFind();
            textBoxReplace.Focus();
        }

        /// <summary>
        /// Find the next match. But first, replace the current selection if it is a match.
        /// </summary>
        private void OnReplaceFindButton_Click(object sender, EventArgs e)
        {
            if (OneReplaceOrFind()) // one replace...  (or one find)
            {
                Find(false, false); // ...followed by one find  (unless we already did that)
            }
            textBoxReplace.Focus();
        }

        private void OnCancelButton_Click(object sender, EventArgs e)
        {
            HelpMessage = "";
            ResetStartingPoint(); //for good measure -JMC

            // Added Close() and disabled Dispose(), but then realized that Hide() might solve issue #326 ("remember last find"), and it seems to! -JMC 2013-09
            Hide();
        }

        private void OnFindReplaceDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            ResetStartingPoint(); //for good measure -JMC

            // Added this so that no matter which way the user 'closes' the dialog, it only hides. A cheap way to remember field contents (#326). -JMC Feb 2014
            this.Hide();
            e.Cancel = true; // cancel the close
        }


        private void OnScopeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ResetStartingPoint();
            textBoxFind.Focus();
        }

        private void OnTextBoxFind_TextChanged(object sender, EventArgs e)
        {
            ResetStartingPoint();
            IsValidSearch(); //checks regexes; might display error in preview box
        }

        private void OnTextBoxReplace_TextChanged(object sender, EventArgs e)
        {
            ResetStartingPoint();
        }

        private void OnTextBoxContextFind_TextChanged(object sender, EventArgs e)
        {
            ResetStartingPoint();
            IsValidSearch(); //checks regexes; might display error in preview box
        }

        private void OnTextBoxContextReplace_TextChanged(object sender, EventArgs e)
        {
            ResetStartingPoint();
        }

        private void OnFindReplaceDialog_Activated(object sender, EventArgs e)
        {
            UpdateDisplay();
        }

        private void OnCheckBoxMultiline_CheckedChanged(object sender, EventArgs e)
        {
            ResetStartingPoint();
            bool ch = checkBoxMultiline.Checked;
            textBoxFind.Multiline = ch;
            textBoxReplace.Multiline = ch;

            // Determine what the Enter key will do
            this.AcceptButton = ch ? null : _findNextButton;

            // Not compatible with regex modes; force Basic
            if (ch)
            {
                radioButtonModeBasic.Checked = true;
            }
            else
            {
                //trim down to the first line
                var nl = new char[] { '\n', '\r' };
                textBoxFind.Text = textBoxFind.Text.Split(nl)[0];
                textBoxReplace.Text = textBoxReplace.Text.Split(nl)[0];
            }

            textBoxFind.Focus();
        }

        public string HelpMessage;

        private void OnHelpButton_Click(object sender, EventArgs e)
        {
            ShowHelp();
        }

        private void OnReplaceAllButton_Click(object sender, EventArgs e)
        {
            Find(true, true);
            ResetStartingPoint();  // right? -JMC
            textBoxReplace.Focus();
        }

        private void OnFindReplaceDialog_Leave(object sender, EventArgs e)
        {

        }

        private void OnCheckBoxCaseSensitive_CheckedChanged(object sender, EventArgs e)
        {
            ResetStartingPoint();
            textBoxFind.Focus();
        }

        public void LaunchSearch (RegexItem r)
        {
            SetFields(r);
            LaunchSearch();
        }

        public void LaunchSearch()
        {
            this.Hide();
            this.TopMost = true; // means that this form should always be in front of all others
            this.SelectFind();
            this.Show();
            this.WindowState = FormWindowState.Minimized;
            this.WindowState = FormWindowState.Normal;
            this.Focus();
            this.ShowHelp();
        }
    }
}
