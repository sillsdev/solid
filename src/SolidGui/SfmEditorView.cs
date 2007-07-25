using System;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Windows.Forms;
using SolidEngine;
using System.Collections.Generic;

namespace SolidGui
{
    public partial class SfmEditorView : UserControl
    {
        class LineMessages: TextBox
        {
            private bool _showing = false;
            private static LineMessages _message;
            private Dictionary<int, string> _lineMessage = new Dictionary<int, string>();
            
            public LineMessages(Control parent)
            {
                Parent = parent;
                Hide();
            }

            public bool Showing
            {
                get { return _showing; }
            }

            public void AddLineMessage(int line, string message)
            {
                _lineMessage[line] = message;
            }

            public void ShowMessage(string message)
            {
                if (Parent != null)
                {
                    Text = message;
                    BackColor = Color.LightYellow;
                    ReadOnly = true;
                    Point point = Parent.PointToClient(MousePosition);
                    point.Y += Font.Height;
                    point.X += 7;
                    Location = point;
                    BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
                    Font = new Font("times new Romans", 10);
                    Show();
                    _showing = true;
                }
            }

            public void ClearLineMessages()
            {
                _lineMessage.Clear();
            }

            public void ShowLineMessage(int line)
            {
                if(_lineMessage.ContainsKey(line))
                    ShowMessage(_lineMessage[line]);
                else
                    Hide();
            }

            public new void Hide()
            {
                _showing = false;
                base.Hide();
            }
        }

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
        private int _lineNumber = -1;
        private Color _inferredTextColor = Color.Blue;
        private Color _errorTextColor = Color.Red;
        private Color _defaultTextColor = Color.Black;
        private KeyScanner _keyScanner = new KeyScanner();
        private ToolTip tip = new ToolTip();
        private LineMessages _lineMessages; 
        private SfmEditorPM _model;
        private Record _currentRecord;
        public event EventHandler RecordTextChanged;
        private const string _processingMark = "\x01";
        private int _indent = 130;
        private int _delay = 10;

        public int Indent
        {
            get { return _indent; }
            set { _indent = value; }
        }	

        public SfmEditorView()
        {
            _currentRecord = null;
            InitializeComponent();
            _contentsBox.SelectionIndent = _leftMarigin;
            _lineMessages = new LineMessages(_contentsBox);
            _timer.Tick += OnTick;
            _timer.Start();
        }

        public void OnRecheckClicked(object sender, EventArgs e)
        {
            UpdateContentsOfTextBox();
        }

        public void BindModel(SfmEditorPM model)
        {
            _model = model;
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
                _contentsBox.TextChanged -= _contentsBox_TextChanged;
                SaveContentsOfTextBox();
                ClearContentsOfTextBox();
                _currentRecord = e._record;
                DisplayEachFieldInCurrentRecord();
                _keyScanner.Reset();
                _contentsBox.TextChanged += _contentsBox_TextChanged;
            }
        }

        private void ClearContentsOfTextBox()
        {
            _contentsBox.Text = "";
        }

        private void DisplayEachFieldInCurrentRecord()
        {
            _lineMessages.ClearLineMessages();
            _contentsBox.SelectAll();
            _contentsBox.SelectionTabs = new int[] { _indent };

            int currentPosition = 0;
            bool foundProcessingMark = false;
            int lineNumber = 0;

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
                    _lineMessages.AddLineMessage(lineNumber, "Inferred");
                    _contentsBox.SelectionColor = _inferredTextColor;
                }
                else if (field.ErrorState > 0)
                {
                    _lineMessages.AddLineMessage(lineNumber, "Error");
                    _contentsBox.SelectionColor = _errorTextColor;
                }
                _contentsBox.AppendText(fieldText + "\n");
                lineNumber++;
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

        public void SaveContentsOfTextBox()
        {
            int currentIndex = _contentsBox.SelectionStart;
            if (_currentRecord != null && _currentRecord.ToStructuredString() != _contentsBox.Text)
            {
                _model.UpdateCurrentRecord(_currentRecord, ContentsBoxTextWithoutInferredFields());
            }
            _contentsBox.SelectionStart = currentIndex;
        }

/*        
        private void _contentsBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (_keyScanner.ProcessKey(e.KeyValue))
            {
                _contentsBox.SelectedText = _processingMark;
                UpdateContentsOfTextBox();
                _keyScanner.Reset();
            }
            if (RecordTextChanged != null)
            {
                RecordTextChanged.Invoke(this, new EventArgs());
            }
        }
*/

        private void UpdateContentsOfTextBox()
        {
            _contentsBox.TextChanged -= _contentsBox_TextChanged;
            SaveContentsOfTextBox();
            ClearContentsOfTextBox();
            DisplayEachFieldInCurrentRecord();
            _contentsBox.TextChanged += _contentsBox_TextChanged;
        }

        private void _contentsBox_MouseHover(object sender, EventArgs e)
        {

        }

        private bool CursorIsAboveLastLine()
        {
            Point cursorPositionRelativeToContentsBox = _contentsBox.PointToClient(MousePosition);
            int YpositionOfLastLineOfText = _contentsBox.Lines.Length*_contentsBox.Font.Height;
            return cursorPositionRelativeToContentsBox.Y < YpositionOfLastLineOfText;
        }

        private bool CursorIsOnMarker()
        {
            Point positionOfCursor = _contentsBox.PointToClient(MousePosition);

            if (positionOfCursor.X < 120)
            {
                int indexOfNearestCharacter = _contentsBox.GetCharIndexFromPosition(positionOfCursor);
                Point positionOfNearestCharacter = _contentsBox.GetPositionFromCharIndex(indexOfNearestCharacter);
                int cursorDistanceFromNearestCharacter = Math.Abs(positionOfCursor.X - positionOfNearestCharacter.X);

                return cursorDistanceFromNearestCharacter < 5;
            }

            return false;
            
        }

        private void _contentsBox_MouseLeave(object sender, EventArgs e)
        {
            _lineNumber = -1;
            _lineMessages.Hide();
        }

        private void _contentsBox_MouseClick(object sender, MouseEventArgs e)
        {
            if (_lineMessages != null)
            {
                _lineMessages.Hide();
                _delay = 5;
            }
        }

        private void _contentsBox_MouseMove(object sender, MouseEventArgs e)
        {
            int index = GetIndex();

            if (index != -1)
            {
                int newLineNumber = _contentsBox.GetLineFromCharIndex(index);

                if (_lineNumber != newLineNumber)
                {
                    _lineNumber = newLineNumber;
                    _delay = (_lineMessages.Showing) ? 0 : 5;
                    _lineMessages.Hide();
                }
            }
            else
            {
                _lineMessages.Hide();
                _lineNumber = -1;
            }
        }

        public void OnTick(object sender, EventArgs e)
        {
            _delay--;
            if (_delay < 0 && !_lineMessages.Showing)
            {
                _lineMessages.ShowLineMessage(_lineNumber);
            }
        }

        private int GetIndex()
        {
            int index = -1;

            if (CursorIsAboveLastLine() && CursorIsOnMarker())
            {
                index = _contentsBox.GetCharIndexFromPosition(_contentsBox.PointToClient(MousePosition));
            }
            return index;
        }

        private void _contentsBox_TextChanged(object sender, EventArgs e)
        {
            if (RecordTextChanged != null)
                RecordTextChanged.Invoke(this, new EventArgs());
        }


    }
}
