using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using SolidEngine;

namespace SolidGui
{
    public partial class SfmEditorView : UserControl
    {
        private SfmEditorPM _model;
        private Record _currentRecord;
        private Color _inferredTextColor = Color.Blue;
        private Color _defaultTextColor = Color.Black;
        public event EventHandler RecordTextChanged;
        
        public SfmEditorView()
        {
            _currentRecord = null;
            InitializeComponent();
        }

        public SfmEditorPM Model
        {
            set { _model = value; }
        }


        public void Highlight(int startIndex, int length)
        {
            _contentsBox.Select(startIndex, length);
        }

        public void OnRecordChanged(object sender, RecordNavigatorPM.RecordChangedEventArgs e)
        {
            if (e._record == null)
            {
                _currentRecord = null;
                ClearContentsOfTextBox();
            }
            else
            {
                _currentRecord = e._record;

                ClearContentsOfTextBox();
                DisplayEachFieldInRecord();
            }
        }

        private void ClearContentsOfTextBox()
        {
            _contentsBox.Text = "";
        }

        private void DisplayEachFieldInRecord()
        {
            for (int i = 0; i < _currentRecord.Fields.Count; i++)
            {
                string fieldText = _currentRecord.GetFieldStructured(i);
                if (_currentRecord.Fields[i].Inferred)
                {
                    _contentsBox.SelectionColor = _inferredTextColor;
                }
                else
                {
                    _contentsBox.SelectionColor = _defaultTextColor;
                }
                _contentsBox.AppendText(fieldText + "\r\n");
            }
        }

        private string ContentsBoxTextWithoutInferredFields()
        {
            string textWithoutInferred = "";
            int initialCaratPosition = _contentsBox.SelectionStart;
            _contentsBox.SelectionStart = 0;
            for (int i = 0; i < _contentsBox.Text.Length; i++)
            {
                _contentsBox.Select(i, 1);
                if(_contentsBox.SelectionColor != _inferredTextColor)
                {
                    textWithoutInferred += _contentsBox.SelectedText;
                }
            }

            _contentsBox.SelectionStart = initialCaratPosition;
            return textWithoutInferred;
        }

        private void OnTextChanged (object sender, EventArgs e)
        {
            
        }

        private void _contentsBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(true/*check to see if e is a character*/)
            {
                    _contentsBox.TextChanged += OnTextChanged;
            }
        }

        private void _contentsBox_Leave(object sender, EventArgs e)
        {
            string structuredStringWithInferred = _currentRecord.ToStructuredString();
            if (_currentRecord != null && structuredStringWithInferred != _contentsBox.Text)
            {
                _model.UpdateCurrentRecord(_currentRecord, ContentsBoxTextWithoutInferredFields());

                if (RecordTextChanged != null)
                    RecordTextChanged.Invoke(this, new EventArgs());
            }
            _contentsBox.TextChanged -= OnTextChanged;
        }
    }
}
