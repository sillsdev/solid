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
            RecordIndex = _navigatorView.Model.CurrentRecordID;
            TextIndex = _sfmEditorView._contentsBox.SelectionStart;

            if(_forwardRadioButton.Checked)
            {
                //move past the currently highlighted word in case it was the one just found
                TextIndex ++;

                _searchModel.FindNext(_findTextbox.Text, RecordIndex, TextIndex);
            }
            else
            {
                if(TextIndex > 0)
                    TextIndex--;

                _searchModel.FindPrevious(_findTextbox.Text,RecordIndex,TextIndex);
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
            }
        }
    }
}