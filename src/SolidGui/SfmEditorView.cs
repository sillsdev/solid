using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using SolidEngine;

namespace SolidGui
{
    public partial class SfmEditorView : UserControl
    {
        private int _spacesInIndentation = 4;
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
            foreach (Record.Field field in _currentRecord.Fields)
            {
                string indentation = new string(' ', field.Depth * _spacesInIndentation);
                string fieldText;
                if (field.Inferred)
                {
                    _contentsBox.SelectionColor = _inferredTextColor;
                    fieldText = indentation + "\\+" + field.Marker + " " + field.Value;
                }
                else
                {
                    _contentsBox.SelectionColor = _defaultTextColor;
                    fieldText = indentation + "\\" + field.Marker + " " + field.Value;
                }
                _contentsBox.AppendText(fieldText + "\r\n");
            }
            _contentsBox.SelectionColor = _defaultTextColor;
        }

        private string ContentsBoxTextWithoutInferredFields()
        {
            string textWithoutInferred = string.Empty;
            _contentsBox.SelectionStart = 0;
            foreach (string line in _contentsBox.Lines)
            {
                int startOfTextOnLine = line.IndexOf("\\");
                if(startOfTextOnLine == -1)
                {
                    startOfTextOnLine = 0;
                }
                int startOfInference = line.IndexOf("\\+");
                if(startOfInference == -1)
                {
                    textWithoutInferred += line.Substring(startOfTextOnLine) + "\r\n";
                }

            }
            return textWithoutInferred;
        }



        private void OnTextChanged (object sender, EventArgs e)
        {
        }

        private void _contentsBox_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void _contentsBox_Leave(object sender, EventArgs e)
        {
            string structuredStringWithInferred = _currentRecord.ToStructuredString();
            if (_currentRecord != null && structuredStringWithInferred != _contentsBox.Text)
            {
                string test = ContentsBoxTextWithoutInferredFields();
                _model.UpdateCurrentRecord(_currentRecord, test);

                if (RecordTextChanged != null)
                    RecordTextChanged.Invoke(this, new EventArgs());
            }
        }

        private void _contentsBox_KeyDown(object sender, KeyEventArgs e)
        {
        }
    }
}
