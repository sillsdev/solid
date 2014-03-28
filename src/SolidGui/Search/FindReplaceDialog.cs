// Copyright (c) 2007-2014 SIL International
// Licensed under the MIT license: opensource.org/licenses/MIT

using System;
using System.ComponentModel;
using System.Media;
using System.Windows.Forms;
/*
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text; */
using SolidGui.Model;

namespace SolidGui.Search
{
    // Created to replace the simpler SearchView dialog that already existed. Very similar code, in basic mode. -JMC Feb 2014
    public partial class FindReplaceDialog : Form
    {
        private SfmEditorView _sfmEditorView;

        private SearchViewModel _searchModel;  // just a reference to MainWindowPm.SearchModel 

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
            get
            {
                return _cursorIndex;
            }
        }

        private MainWindowPM _model;

        public FindReplaceDialog(SfmEditorView sfmEditorView, MainWindowPM model)
        {

            InitializeComponent();
            _scopeComboBox.SelectedIndex = 0;
            if (DesignMode) return;

            // JMC: Add a call here to KeyboardController.Register() ? Would not need to be as smart as for the rich edit control. Ideally, we'd probably default to the vernacular keyboard?
            _sfmEditorView = sfmEditorView;

            _searchModel = model.SearchModel;
            _searchModel.WordFound += OnWordFound;
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

        private void ResetStartingPoint()  // and Refresh
        {
            _startingRecordIndex = -1;
            _startingTextIndex = -1;
            _searchResult = null;  // has ch
            //_sfmEditorView. Select();

            // If the user pastes in tabs, change them to spaces
            textBoxFind.Text = SearchViewModel.RegexTab.Replace(textBoxFind.Text, " ");
            textBoxContextFind.Text = SearchViewModel.RegexTab.Replace(textBoxContextFind.Text, " ");
            textBoxReplace.Text = SearchViewModel.RegexTab.Replace(textBoxReplace.Text, " ");
            textBoxContextReplace.Text = SearchViewModel.RegexTab.Replace(textBoxContextReplace.Text, " ");
            
            //JMC:! This is probably also the best time to update the Replace preview. But be careful to not instantly wipe out error messages (for \x \c etc.)
            textBoxReplacePreview.Text = "";
            textBoxContextPreview.Text = "";

            UpdateDisplay();
        }

        private void radioButtonMode_CheckedChanged(object sender, EventArgs e)
        {

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
        }

        public void OnWordFound(object sender, SearchViewModel.SearchResultEventArgs e)
        {
            this._searchResult = e.SearchResult;
        }

        public void ShowHelp()
        {
            if (String.IsNullOrEmpty(this.HelpMessage)) return;
            MessageBox.Show(HelpMessage, "About the selected recipe", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void UpdateDisplay()
        {
            buttonHelp.Visible = !(String.IsNullOrEmpty(HelpMessage));

            bool dbl = radioButtonDoubleRegex.Checked;
            groupBoxFindContext.Enabled = dbl;

            if (dbl || radioButtonRegex.Checked)
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
            {  // result of previous find
                string f = _searchResult.Found;
                return _sfmEditorView.ContentsBox.SelectedText.ToLowerInvariant() == f.ToLowerInvariant();
            }
            return false;
        }

        private RegexItem GetReggie()
        {
            var ri = new RegexItem();
            bool cs = this.checkBoxCaseSensitive.Checked;
            bool dub = radioButtonDoubleRegex.Checked;
            string f = textBoxFind.Text;
            string fc = textBoxContextFind.Text;
            if (dub)
            {
                ri.setFind(fc, f, cs);
                ri.ReplaceContext = textBoxContextReplace.Text;
            }
            else
            {
                ri.setFind(f, cs);
            }
            ri.Replace = textBoxContextReplace.Text;
            return ri;
        }

        private int GetTextIndex()
        {
            return _sfmEditorView.ContentsBox.SelectionStart + 1;
        }

        private int GetRecordIndex()
        {
            // firstTime = (_startingTextIndex == -1); //why?

            if (_scopeComboBox.SelectedIndex == 0) // "Current Filter" (formerly "Check Result") -JMC
            {
                return _model.NavigatorModel.ActiveFilter.CurrentIndex;  // position in current filter instead
            }
            return _model.NavigatorModel.CurrentRecordID;  // position in file (i.e. in the All filter)
        }


        private bool Find(bool replace, bool replaceAll)
        {
            //_searchModel.UseRegex = this.radioButtonRegex.Checked;
            //_searchModel.CaseSensitive = this.checkBoxCaseSensitive.Checked;
            bool reg = this.radioButtonRegex.Checked;
            RegexItem ri = null;
            RecordIndex = GetRecordIndex();
            _startingRecordIndex = RecordIndex;  //do we need this?
            if (reg)
            {
                ri = GetReggie();
                _searchModel.Setup(ri, RecordIndex, GetTextIndex());  //was GetTextIndex()-1
            }
            else
            {
                _searchModel.Setup(textBoxFind.Text, textBoxReplace.Text, RecordIndex, GetTextIndex()); //was GetTextIndex()-1
            }
            _searchModel.UseRegex = reg;
            _searchModel.Filter = _model.NavigatorModel.ActiveFilter;

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
                        if (!SafeToReplace()) return false; //redundant?
                        // Replace current selection
                        _sfmEditorView.ContentsBox.SelectedText = rw; 
                        _sfmEditorView.UpdateModel();
                        if (!replaceAll) return true;

                    }
                    else
                    {
                        // Find next
                        CursorIndex = GetTextIndex();
                        RecordIndex = GetRecordIndex(); 

                        SearchResult result = _searchModel.NextResult(RecordIndex, CursorIndex);

                        if (result == null) CantFindWordErrorMessage(ri, textBoxFind.Text); 

                        if (replaceAll)
                        {
                            if (result == null)
                            {
                                MakeBing();
                                break;
                            }
                            replace = (!replace); //alternate back and forth
                        }
                        else
                        {
                            break;
                        }

                    }
                    string ss = "preview "; //JMC:! unfinished
                    this.textBoxContextPreview.Text = ss;

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
                //Palaso.Reporting.ErrorReport.ReportNonFatalException(error);
                Palaso.Reporting.ErrorReport.ReportNonFatalExceptionWithMessage(error, msg);
            }

            // bring the search form back into focus -JMC
            this.BringToFront();
            this.Focus();
            return (_searchResult != null);
        }

        // Make a dinging sound (well, the system Asterisk). Called on wraparound, or on no match found.
        private void MakeBing()
        {
            SystemSounds.Asterisk.Play();
        }

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
        }

        private void OnReplaceButton_Click(object sender, EventArgs e)
        {
            Find(true, false);
        }

        private void buttonReplaceFind_Click(object sender, EventArgs e)
        {
            if (SafeToReplace())
            {
                bool success = Find(true, false);
                if (!success) return; //probably an unnecessary check
            }
            Find(false, false);
        }

        private void OnCancelButton_Click(object sender, EventArgs e)
        {
            HelpMessage = "";
            ResetStartingPoint(); //for good measure -JMC

            // Added Close() and disabled Dispose(), but then realized that Hide() might solve issue #326 ("remember last find"), and it seems to! -JMC 2013-09
            Hide();
        }

        private void FindReplaceDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            ResetStartingPoint(); //for good measure -JMC

            // Added this so that no matter which way the user 'closes' the dialog, it only hides. A cheap way to remember field contents (#326). -JMC Feb 2014
            this.Hide();
            e.Cancel = true; // cancel the close
        }


        private void OnScopeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ResetStartingPoint();
        }

        private void textBoxFind_TextChanged(object sender, EventArgs e)
        {
            ResetStartingPoint();
        }

        private void textBoxReplace_TextChanged(object sender, EventArgs e)
        {
            ResetStartingPoint();
        }

        private void textBoxContextFind_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxContextReplace_TextChanged(object sender, EventArgs e)
        {

        }

        private void FindReplaceDialog_Activated(object sender, EventArgs e)
        {
            UpdateDisplay();
        }

        private void checkBoxMultiline_CheckedChanged(object sender, EventArgs e)
        {
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

        }

        public string HelpMessage;

        private void buttonHelp_Click(object sender, EventArgs e)
        {
            ShowHelp();
        }

        private void buttonReplaceAll_Click(object sender, EventArgs e)
        {
            Find(true, true);
        }

        private void FindReplaceDialog_Leave(object sender, EventArgs e)
        {

        }

    }
}
