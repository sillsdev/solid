using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using SolidEngine;

namespace SolidGui
{
    public partial class SfmEditorView : UserControl
    {
        class KeyScanner
        {
            enum State
            {
                ScanBackslash,
                ScanWhite
            }
            State _state;

            public KeyScanner()
            {
                Reset();
            }

            public void Reset()
            {
                _state = State.ScanBackslash;
            }

            public bool ProcessKey(int c)
            {
                bool retval = false;
                switch (_state)
                {
                    case State.ScanBackslash:
                        if (c == '\\' || c == 220)
                        {
                            _state = State.ScanWhite;
                        }
                        break;
                    case State.ScanWhite:
                        if (c == ' ' || c == 0x09)
                        {
                            retval = true;
                        }
                        break;
                }
                return retval;
            }
        }

        private int _spacesInIndentation = 4;
        private int _leftMarigin = 20;
        private SfmEditorPM _model;
        private Record _currentRecord;
        private Color _inferredTextColor = Color.Blue;
        private Color _errorTextColor = Color.Red;
        private Color _defaultTextColor = Color.Black;
        public event EventHandler RecordTextChanged;
        private KeyScanner _keyScanner = new KeyScanner();
        private const string _processingMark = "\x01";

        public SfmEditorView()
        {
            _currentRecord = null;
            InitializeComponent();
            _contentsBox.SelectionIndent = _leftMarigin;
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
            else if (_currentRecord != e._record)
            {
                SaveContentsOfTextBox();
                ClearContentsOfTextBox();
                _currentRecord = e._record;
                DisplayEachFieldInCurrentRecord();
                _keyScanner.Reset();
            }
            
        }

        private void ClearContentsOfTextBox()
        {
            _contentsBox.Text = "";
        }

        private void DisplayEachFieldInCurrentRecord()
        {
            int currentPosition = 0;
            bool foundProcessingMark = false;
            foreach (Record.Field field in _currentRecord.Fields)
            {
                string indentation = new string(' ', field.Depth * _spacesInIndentation);
                string markerPrefix = (field.Inferred) ? "\\+" : "\\";
                string fieldText = indentation + markerPrefix + field.Marker + "\t" + field.Value;
                if (!foundProcessingMark)
                {
                    if (field.Value == _processingMark)
                    {
                        foundProcessingMark = true;
                        field.Value = "";
                        fieldText = indentation + markerPrefix + field.Marker + "\t" + field.Value;
                    }
                    currentPosition += fieldText.Length + 1;
                }
                _contentsBox.SelectionColor = _defaultTextColor;
                if (field.Inferred)
                {
                    _contentsBox.SelectionColor = _inferredTextColor;
                }
                if (field.ErrorState > 0)
                {
                    _contentsBox.SelectionColor = _errorTextColor;
                }
                _contentsBox.AppendText(fieldText + "\n");
            }
            _contentsBox.SelectionColor = _defaultTextColor;
            _contentsBox.SelectionStart = (foundProcessingMark) ? currentPosition - 1 : 0;
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

        private void _contentsBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (_keyScanner.ProcessKey(e.KeyValue))
            {
                //int currentIndex = _contentsBox.SelectionStart;
                _contentsBox.SelectedText = _processingMark;
                SaveContentsOfTextBox();
                ClearContentsOfTextBox();
                DisplayEachFieldInCurrentRecord();
                //_contentsBox.SelectionStart = currentIndex;
                _keyScanner.Reset();
            }
            if (RecordTextChanged != null)
            {
                RecordTextChanged.Invoke(this, new EventArgs());
            }
        }
    }
}
