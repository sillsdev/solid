using System;
using System.Windows.Forms;

namespace SolidGui
{
    public partial class SearchView : Form
    {
        private SearchPM _searchModel;
        private RecordNavigatorView _navigatorView;
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

        public SearchView(RecordNavigatorView navigatorView, SfmEditorView sfmEditorView)
        {
            InitializeComponent();
            _navigatorView = navigatorView;
            _sfmEditorView = sfmEditorView;
        }

        public int RecordIndex
        {
            set
            {
                if (value < _searchModel.MasterRecordList.Count)
                {
                    _recordIndex = value;
                }
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
            //move past the currently highlighted word in case it was the one just found
            TextIndex = _sfmEditorView._contentsBox.SelectionStart + _sfmEditorView._contentsBox.SelectionLength;

            RecordIndex = _navigatorView.Model.CurrentRecordIndex;

            _searchModel.FindNext(_findTextbox.Text, RecordIndex, TextIndex);
        }

        private void _cancelButton_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void _findPreviousButton_Click(object sender, EventArgs e)
        {
           
            TextIndex = _sfmEditorView._contentsBox.SelectionStart;
            RecordIndex = _navigatorView.Model.CurrentRecordIndex;

            _searchModel.FindPrevious(_findTextbox.Text, RecordIndex, TextIndex);

        }
    }
}