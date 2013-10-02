using System;
using System.Windows.Forms;

namespace SolidGui.Search
{
    public partial class SearchView : Form
    {
        private static SearchView _searchView;
        private SearchViewModel _searchModel;
        private readonly RecordNavigatorPM _navigatorModel;
        private readonly SfmEditorView _sfmEditorView;
        private int _textIndex;
        private int _startingTextIndex = -1;
        private int _startingRecordIndex = -1;

        public SearchViewModel SearchModel
        {
            set
            {
                _searchModel = value;
            }
        }

        public static SearchView CreateSearchView(RecordNavigatorPM navigatorModel, SfmEditorView sfmEditorView)
        {
            if (_searchView == null || _searchView.IsDisposed)
            {
                _searchView = new SearchView(navigatorModel, sfmEditorView);
            }
            return _searchView;
        }

        private SearchView(RecordNavigatorPM navigatorModel, SfmEditorView sfmEditorView)
        {
            InitializeComponent();
            _navigatorModel = navigatorModel;
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
            TextIndex = _sfmEditorView.ContentsBox.SelectionStart + 1;
            _startingTextIndex = (_startingTextIndex == -1) ? TextIndex-1 : _startingTextIndex;

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

        private void OnCancelButton_Click(object sender, EventArgs e)
        {
            // TODO Should this be Close(); CP 2010-10
            Dispose();
        }

        private void OnReplaceButton_Click(object sender, EventArgs e)
        {
            if(_sfmEditorView.ContentsBox.SelectedText != _findTextbox.Text)
            {
                OnFindNextButton_Click(new object(),new EventArgs());
            }
            else
            {
                _sfmEditorView.ContentsBox.SelectedText = _replaceTextBox.Text;
                _sfmEditorView.UpdateModel();
            }
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