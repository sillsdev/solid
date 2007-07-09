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
            _model = new SfmEditorPM();
            _currentRecord = null;
            InitializeComponent();
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
            for (int i = 0; i < _currentRecord.FieldCount; i++)
            {
                string fieldText = _currentRecord.GetFieldStructured(i);
                if (_currentRecord.IsFieldInferred(i))
                {
                    _contentsBox.SelectionColor = _inferredTextColor;
                }
                else
                {
                    _contentsBox.SelectionColor = _defaultTextColor;
                }
                _contentsBox.AppendText(fieldText + "\n");
            }
        }

        private void OnTextChanged(object sender, System.EventArgs e)
        {
            if (_currentRecord!=null && _currentRecord.ToStructuredString()!=_contentsBox.Text)
            {
                _model.UpdateCurrentRecord(_currentRecord, ContentsBoxTextWithoutInferredFields());

                if (RecordTextChanged != null)
                    RecordTextChanged.Invoke(this, new EventArgs());
            }
        }

        private string ContentsBoxTextWithoutInferredFields()
        {
            string textWithoutInferred = "";
            int initialCaratPosition = _contentsBox.SelectionStart;
            _contentsBox.SelectionStart = 0;
            for (int i = 0; i < _contentsBox.Text.Length; i++)
            {
                _contentsBox.SelectionStart = i;
                if(_contentsBox.SelectionColor != _inferredTextColor)
                {
                    textWithoutInferred += _contentsBox.SelectedText;
                }
            }

            _contentsBox.SelectionStart = initialCaratPosition;
            return textWithoutInferred;
        }
    }
}
