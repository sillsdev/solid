// Copyright (c) 2007-2014 SIL International
// Licensed under the MIT license: opensource.org/licenses/MIT

using System;
using System.Windows.Forms;
using SolidGui.Engine;
using SolidGui.Filter;

namespace SolidGui.Search
{
    public partial class SearchView : Form
    {
        private static SearchView _searchView;  // Singleton? -JMC
        private SfmEditorView _sfmEditorView;

        private SearchViewModel _searchModel;  // /
        private RecordNavigatorPM _navigatorModel;  // /

        private int _textIndex;
        private int _startingTextIndex = -1;
        private int _startingRecordIndex = -1;

        private SearchResult _searchResult;  //added to support regex -JMC Feb 2014

        private void ResetStartingPoint()
        {
            _startingRecordIndex = -1;
            _startingTextIndex = -1;
            _searchResult = null;
        }

        private void BindModel(MainWindowPM model)
        {
            _searchModel = model.SearchModel;
            _searchModel.WordFound += OnWordFound;
            _navigatorModel = model.NavigatorModel;
            ResetStartingPoint();
        }

        public static SearchView CreateSearchView(MainWindowPM model, SfmEditorView sfmEditorView)
        {
            if (_searchView == null || _searchView.IsDisposed)
            {
                _searchView = new SearchView(sfmEditorView);
            }

            _searchView.BindModel(model);
            return _searchView;
        }

        private SearchView(SfmEditorView sfmEditorView)
        {
            InitializeComponent();
            // JMC: Add a call here to KeyboardController.Register() ? Would not need to be as smart as for the rich edit control. Ideally, we'd probably default to the vernacular keyboard?
            _sfmEditorView = sfmEditorView;
            _scopeComboBox.SelectedIndex = 0;
        }

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

        private void OnFindNextButton_Click(object sender, EventArgs e)
        {
            try
            {
                Find(false);                
            }
            catch (Exception error)
            {
                string msg = "An unexpected error occurred:\r\n" + error.Message;
                //Palaso.Reporting.ErrorReport.ReportFatalMessageWithStackTrace(msg, error);
                //Palaso.Reporting.ErrorReport.ReportNonFatalException(error);
                Palaso.Reporting.ErrorReport.ReportNonFatalExceptionWithMessage(error, msg);
            }
        }

        private void OnReplaceButton_Click(object sender, EventArgs e)
        {
            Find(true);
        }

        public void OnWordFound(object sender, SearchViewModel.SearchResultEventArgs e)
        {
            this._searchResult = e.SearchResult;
        }

        private void Find(bool replace)
        {
            _searchModel.UseRegex = this.checkBoxRegex.Checked;
            bool firstTime = true;
            string f = null;
            string r = "";
            if (_searchResult != null)
            {  // result of previous find
                f = _searchResult.Found;
                r = _searchResult.ReplaceWith;
            }
            if (replace && _sfmEditorView.ContentsBox.SelectedText == f) // == _findTextbox.Text)
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

                _searchModel.FindNext(  _findTextbox.Text,
                                        _replaceTextBox.Text,
                                        RecordIndex,
                                        TextIndex,
                                        _startingRecordIndex,
                                        _startingTextIndex);
                

            }
            
            // bring the search form back into focus -JMC
            _searchView.BringToFront();
            _searchView.Focus();

        }

        private void OnCancelButton_Click(object sender, EventArgs e)
        {
            ResetStartingPoint(); //for good measure -JMC

            // Added Close() and disabled Dispose(), but then realized that Hide() might solve issue #326 ("remember last find"), and it seems to! -JMC 2013-09
            this.Hide(); 
        }

        private void SearchView_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Added this so that no matter which way the user 'closes' the dialog, it only hides. A cheap way to remember field contents (#326). -JMC Feb 2014
            this.Hide();
            e.Cancel = true;
        }

        private void OnFindTextbox_TextChanged(object sender, EventArgs e)
        {
            ResetStartingPoint();
        }

        private void OnScopeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ResetStartingPoint();
        }

        private void _replaceTextBox_TextChanged(object sender, EventArgs e)
        {
            ResetStartingPoint();
        }
        
    }
}