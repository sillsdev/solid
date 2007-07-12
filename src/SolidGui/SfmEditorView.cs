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
        private Color _errorTextColor = Color.Red;
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
            _contentsBox.SelectionStart = startIndex;
            _contentsBox.SelectionLength = length;
            _contentsBox.Select(startIndex, length);
            //_contentsBox.SelectionBackColor = Color.Blue;
            //_contentsBox.SelectionColor = Color.White;
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
                if (_currentRecord != e._record)
                {
                    

                    //_contentsBox.TextChanged -= OnTextChanged;
                    SaveContentsOfTextBox();
                    ClearContentsOfTextBox();
                    _currentRecord = e._record;
                    DisplayEachFieldInCurrentRecord();
                    //_contentsBox.TextChanged += OnTextChanged;
                }
            }
        }

        private void ClearContentsOfTextBox()
        {
            _contentsBox.Text = "";
        }

        private void DisplayEachFieldInCurrentRecord()
        {
            foreach (Record.Field field in _currentRecord.Fields)
            {
                string indentation = new string(' ', field.Depth * _spacesInIndentation);
                string markerPrefix = (field.Inferred) ? "\\+" : "\\";
                string fieldText = indentation + markerPrefix + field.Marker + " " + field.Value;
                _contentsBox.SelectionColor = _defaultTextColor;
                if (field.Inferred)
                {
                    _contentsBox.SelectionColor = _inferredTextColor;
                }
                if (field.ErrorState > 0)
                {
                    _contentsBox.SelectionColor = _errorTextColor;
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

        private void _contentsBox_Leave(object sender, EventArgs e)
        {
            
        }

        private void OnTextChanged(object sender, EventArgs e)
        {

        }

        public void SaveContentsOfTextBox()
        {
            int currentIndex = _contentsBox.SelectionStart;
            if (_currentRecord != null && _currentRecord.ToStructuredString() != _contentsBox.Text)
            {
                string test = ContentsBoxTextWithoutInferredFields();
                _model.UpdateCurrentRecord(_currentRecord, test);

                if (RecordTextChanged != null)
                    RecordTextChanged.Invoke(this, new EventArgs());
            }
            _contentsBox.SelectionStart = currentIndex;
        }
    }
}
