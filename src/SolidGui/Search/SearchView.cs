using System;
using System.Windows.Forms;

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

/*
        public SearchViewModel SearchModel
        {
            set
            {
                _searchModel = value;
            }
        }
*/
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
            _sfmEditorView = sfmEditorView;
            _scopeComboBox.SelectedIndex = 0;
        }

        private void BindModel(MainWindowPM model)
        {
            _searchModel = model.SearchModel;
            _navigatorModel = model.NavigatorModel;
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
            Find(false);
        }

        private void OnReplaceButton_Click(object sender, EventArgs e)
        {
            Find(true);
        }

        private void Find(bool replace)
        {

            if (replace && _sfmEditorView.ContentsBox.SelectedText == _findTextbox.Text)
            {
                // Replace current selection
                _sfmEditorView.ContentsBox.SelectedText = _replaceTextBox.Text;
                _sfmEditorView.UpdateModel();
            }
            else
            {
                // Find next
                TextIndex = _sfmEditorView.ContentsBox.SelectionStart + 1;
                _startingTextIndex = (_startingTextIndex == -1) ? TextIndex - 1 : _startingTextIndex;

                if (_scopeComboBox.SelectedIndex == 0)  // "Check Result"
                {
                    RecordIndex = _navigatorModel.ActiveFilter.CurrentIndex;
                    _startingRecordIndex = (_startingRecordIndex == -1) ? RecordIndex : _startingRecordIndex;
                    _searchModel.FindNext(_navigatorModel.ActiveFilter,
                                          _findTextbox.Text,
                                          RecordIndex,
                                          TextIndex,
                                          _startingRecordIndex,
                                          _startingTextIndex);
                }
                else  // "Entire Dictionary"
                {
                    RecordIndex = _navigatorModel.CurrentRecordID;
                    _startingRecordIndex = (_startingRecordIndex == -1) ? RecordIndex : _startingRecordIndex;
                    _searchModel.FindNext(_findTextbox.Text,
                                          RecordIndex,
                                          TextIndex,
                                          _startingRecordIndex,
                                          _startingTextIndex);
                }

            }
            
            // bring the search form back into focus -JMC
            _searchView.BringToFront();
            _searchView.Focus();

        }

        private void OnCancelButton_Click(object sender, EventArgs e)
        {
            this.Hide(); // Added Close() and disabled Dispose(), then realized that Hide() might also solve issue #326, and it seems to! -JMC 2013-09
            // We'll also dispose this from within MainWindowView_FormClosing() though that seems unnecessary.

//            Close(); // TODO Should this be Close(); CP 2010-10
//            // Dispose(); 
        }

        private void OnFindTextbox_TextChanged(object sender, EventArgs e)
        {
            ResetStartingPoint();
        }

        private void OnScopeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ResetStartingPoint();
        }
        
        private void ResetStartingPoint()
        {
            _startingRecordIndex = -1;
            _startingTextIndex = -1;
        }
    }
}