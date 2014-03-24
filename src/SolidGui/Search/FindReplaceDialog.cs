// Copyright (c) 2007-2014 SIL International
// Licensed under the MIT license: opensource.org/licenses/MIT

using System;
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
        private RecordNavigatorPM _navigatorModel;

        private int _textIndex;
        private int _startingTextIndex = -1;
        private int _startingRecordIndex = -1;

        private SearchResult _searchResult;

        public int RecordIndex { get; set; }

        public int TextIndex
        {
            set
            {
                if (value < _sfmEditorView.ContentsBox.Text.Length)
                {
                    _textIndex = value;
                }
            }
            get
            {
                return _textIndex;
            }
        }

        public FindReplaceDialog(SfmEditorView sfmEditorView, MainWindowPM model)
        {

            InitializeComponent();
            _scopeComboBox.SelectedIndex = 0;
            if (DesignMode) return;

            // JMC: Add a call here to KeyboardController.Register() ? Would not need to be as smart as for the rich edit control. Ideally, we'd probably default to the vernacular keyboard?
            _sfmEditorView = sfmEditorView;

            _searchModel = model.SearchModel;
            _searchModel.WordFound += OnWordFound;
            _navigatorModel = model.NavigatorModel;
            ResetStartingPoint();
        }



        private void ResetStartingPoint()
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
        }

        private void radioButtonMode_CheckedChanged(object sender, EventArgs e)
        {
            bool dbl = radioButtonDoubleRegex.Checked;
            groupBoxFindContext.Enabled = dbl;
            
            if (dbl || radioButtonRegex.Checked )
            {
                checkBoxMultiline.Checked = false;
            }

            /*
            // With leading spaces, tree view doesn't fit well with serious (regex) Find
            if (!radioButtonModeBasic.Checked)
            {
                var rf = new RecordFormatter();
                rf.SetDefaultsUiFlat();
                _searchModel.SyncFormat(rf);
            }
             */
        }

        public void OnWordFound(object sender, SearchViewModel.SearchResultEventArgs e)
        {
            this._searchResult = e.SearchResult;
        }


        private void UpdateDisplay()
        {
            //make the radio button compatible with editor's current indentation
            bool indent = _searchModel.RecordFormatter.ShowIndented;

            /*
            //With leading spaces, tree view is not very compatible with serious regex mode
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

        private bool ReadyToReplace()
        {
            if (_searchResult != null)
            {  // result of previous find
                string f = _searchResult.Found;
                return _sfmEditorView.ContentsBox.SelectedText.ToLowerInvariant() == f.ToLowerInvariant();
            }
            return false;
        }

        private void Find(bool replace)
        {
            Find(replace, false);
        }

        private void Find(bool replace, bool all)
        {
            _searchModel.UseRegex = this.radioButtonRegex.Checked;
            _searchModel.CaseSensitive = this.checkBoxCaseSensitive.Checked;
            bool firstTime = true;
            string f = null;
            string r = "";
            if (_searchResult != null)
            {  // result of previous find
                r = _searchResult.ReplaceWith;
            }
            if (replace && ReadyToReplace())
            {
                // Replace current selection
                _sfmEditorView.ContentsBox.SelectedText = r; // _replaceTextBox.Text;
                _sfmEditorView.UpdateModel();
            }
            else
            {
                // Find next
                TextIndex = _sfmEditorView.ContentsBox.SelectionStart + 1;
                RecordIndex = _navigatorModel.CurrentRecordID;
                firstTime = (_startingTextIndex == -1);
                if (firstTime)
                {
                    //we're starting our first find in a potential series; bookmark this
                    _startingTextIndex = TextIndex - 1;
                    _startingRecordIndex = RecordIndex;
                }

                _searchModel.Filter = null;
                if (_scopeComboBox.SelectedIndex == 0) // "Current Filter" (formerly "Check Result") -JMC
                {
                    _searchModel.Filter = _navigatorModel.ActiveFilter;
                    RecordIndex = _navigatorModel.ActiveFilter.CurrentIndex;
                }

                if (all)
                {
                    
                }
                else
                {
                    _searchModel.FindReplace(textBoxFind.Text,
                                        textBoxReplace.Text,
                                        RecordIndex,
                                        TextIndex,
                                        _startingRecordIndex,
                                        _startingTextIndex);
                }


            }

            // bring the search form back into focus -JMC
            this.BringToFront();
            this.Focus();

        }

        private void TryFind(bool replace)
        {
            try
            {
                Find(replace); //JMC:! Is crashing on things like \xv that resembles hex codes (in regex, \\xv works)
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
        }

        private void OnFindNextButton_Click(object sender, EventArgs e)
        {
            TryFind(false);
        }

        private void OnReplaceButton_Click(object sender, EventArgs e)
        {
            TryFind(true);
        }

        private void buttonReplaceFind_Click(object sender, EventArgs e)
        {
            if (ReadyToReplace())
            {
                TryFind(true);
            }
            //JMC:! need to verify that we got a match?
            TryFind(false);
        }

        private void OnCancelButton_Click(object sender, EventArgs e)
        {
            ResetStartingPoint(); //for good measure -JMC

            // Added Close() and disabled Dispose(), but then realized that Hide() might solve issue #326 ("remember last find"), and it seems to! -JMC 2013-09
            this.Hide();
        }

        private void FindReplaceDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            ResetStartingPoint(); //for good measure -JMC

            // Added this so that no matter which way the user 'closes' the dialog, it only hides. A cheap way to remember field contents (#326). -JMC Feb 2014
            this.Hide();
            e.Cancel = true;
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

    }
}
