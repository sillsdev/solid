using System;
using System.Windows.Forms;

namespace SolidGui
{
    public partial class SearchView : Form
    {
        private static SearchView _searchView;
        private SearchPM _searchModel;
        private RecordNavigatorPM _navigatorModel;
        private SfmEditorView _sfmEditorView;
        private int _recordIndex = 0;
        private int _textIndex = 0;

        public SearchPM SearchModel
        {
            set
            {
                _searchModel = value;
            }
        }

        public static SearchView CreatSearchView(RecordNavigatorPM navigatorModel, SfmEditorView sfmEditorView)
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

        public int RecordIndex
        {
            set
            {
                    _recordIndex = value;
            }
            get
            {
                return _recordIndex;
            }
        }

        public int TextIndex
        {
            set
            {
                if (value < _sfmEditorView._contentsBox.Text.Length)
                {
                    _textIndex = value;
                }
            }
            get
            {
                return _textIndex;
            }
        }

        private void _findNextButton_Click(object sender, EventArgs e)
        {
            TextIndex = _sfmEditorView._contentsBox.SelectionStart + 1;

            if (_scopeComboBox.SelectedIndex == 0)
            {
                RecordIndex = _navigatorModel.ActiveFilter.CurrentIndex;
                _searchModel.FindNext(_navigatorModel.ActiveFilter,
                                      _findTextbox.Text,
                                      RecordIndex,
                                      TextIndex);
            }
            else
            {
                RecordIndex = _navigatorModel.CurrentRecordID;
                _searchModel.FindNext(_findTextbox.Text, RecordIndex, TextIndex);
            }
        }

        private void _cancelButton_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void _replaceButton_Click(object sender, EventArgs e)
        {
            if(_sfmEditorView._contentsBox.SelectedText != _findTextbox.Text)
            {
                _findNextButton_Click(new object(),new EventArgs());
            }
            else
            {
                _sfmEditorView._contentsBox.SelectedText = _replaceTextBox.Text;
                _sfmEditorView.SaveContentsOfTextBox();
            }
        }
    }
}