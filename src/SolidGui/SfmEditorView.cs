using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Elsehemy;
using SolidEngine;
using System.Collections.Generic;

namespace SolidGui
{
    public partial class SfmEditorView : UserControl
    {

        class MarkerTip: SuperToolTip
        {
            private bool _showing = false;
            private Dictionary<int, string> _lineMessage = new Dictionary<int, string>();
            private Control _textBox;
            
            public MarkerTip(Control textBox, IContainer container):
                base(container)
            {
                _textBox = textBox;
                
                SuperToolTipInfoWrapper wrapper = new SuperToolTipInfoWrapper();
                wrapper.SuperToolTipInfo = CreateSuperInfo();
                wrapper.UseSuperToolTip = true;
                SetSuperStuff(_textBox, wrapper);
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

                Point point = MousePosition;
                point.Y += _textBox.Font.Height/2;
                point.X += 4;

                point = _textBox.PointToClient(point);

                GetSuperStuff(_textBox).SuperToolTipInfo.BodyText = message;
                GetSuperStuff(_textBox).SuperToolTipInfo.OffsetForWhereToDisplay = point;

                Show(_textBox);
                _showing = true;
                _textBox.Parent.Focus();
            }

            public SuperToolTipInfo CreateSuperInfo()
            {
                SuperToolTipInfo superToolTipInfo = new SuperToolTipInfo();

                superToolTipInfo.BackgroundGradientBegin = Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
                superToolTipInfo.BackgroundGradientEnd = Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(218)))), ((int)(((byte)(239)))));
                superToolTipInfo.BackgroundGradientMiddle = Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(246)))), ((int)(((byte)(251)))));
                superToolTipInfo.BodyFont = new Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                superToolTipInfo.BodyText = "";
                superToolTipInfo.HeaderFont = new Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
                superToolTipInfo.HeaderText = "Problem Description";
                superToolTipInfo.OffsetForWhereToDisplay = new Point(0, 0);

                return superToolTipInfo;
            }

            public void ClearLineMessages()
            {
                _lineMessage.Clear();
            }

            public void ShowMessageForLine(int line)
            {
                if(_lineMessage.ContainsKey(line))
                    ShowMessage(_lineMessage[line]);
                else 
                    Hide();
            }

            public void Hide()
            {
                if (_showing)
                {
                    _showing = false;
                    Close();
                }
            }

            protected override void MouseEntered(object sender, EventArgs e)
            {
                
            }

            protected override void MouseLeft(object sender, EventArgs e)
            {
                
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

        private SfmEditorPM _model;
        private Record _currentRecord;

        private int _spacesInIndentation = 4;
        private int _leftMarigin = 20;
        private Color _inferredTextColor = Color.Blue;
        private Color _errorTextColor = Color.Red;
        private Color _defaultTextColor = Color.Black;
        private int _indent = 130;
        private Font _defaultFont = new Font("Arial", 13);

        private KeyScanner _keyScanner = new KeyScanner();
        private const string _processingMark = "\x01";
        
        private MarkerTip _markerTip;
        private int _lineNumber = -1;
        private int _markerTipDisplayDelay = 10;
                
        public event EventHandler RecordTextChanged;
        
 
        

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
            _markerTip = new MarkerTip(_contentsBox, components);
            _timer.Tick += OnTick;
            _timer.Start();
            _contentsBox.DragEnter += new DragEventHandler(_contentsBox_DragEnter);
            
        }

        void _contentsBox_DragEnter(object sender, DragEventArgs e)
        {
            if(e.Data.GetDataPresent(DataFormats.Text))
            {
                if((e.KeyState & 8) == 8)
                {   //what to do if the control key is held down on drag drop
                    e.Effect = DragDropEffects.Copy;
                }
                else
                {
                    e.Effect = DragDropEffects.Move;
                }
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
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
                SaveContentsOfTextBox();
                ClearContentsOfTextBox();
                _currentRecord = e._record;
                DisplayEachFieldInCurrentRecord();
                _keyScanner.Reset();
            }
        }
       
        public void SaveContentsOfTextBox()
        {
            int currentIndex = _contentsBox.SelectionStart;
            if (_currentRecord != null && _currentRecord.ToStructuredString() != _contentsBox.Text)
            {
                _model.UpdateCurrentRecord(_currentRecord, GetContentsBoxTextWithoutInferredFields());
            }
            _contentsBox.SelectionStart = currentIndex;
        }

        public void ClearContentsOfTextBox()
        {
            _contentsBox.TextChanged -= _contentsBox_TextChanged;
            _contentsBox.Clear();
            _contentsBox.TextChanged += _contentsBox_TextChanged;
        }

        public void DisplayEachFieldInCurrentRecord()
        {
            _contentsBox.TextChanged -= _contentsBox_TextChanged;

            _markerTip.ClearLineMessages();
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
                //_contentsBox.Font = _model.DisplayFont(field.Marker);
/*
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
*/
                _contentsBox.SelectionColor = _defaultTextColor;
                if (field.Inferred)
                {
                    _markerTip.AddLineMessage(lineNumber, "Inferred");
                    _contentsBox.SelectionColor = _inferredTextColor;
                }
                else if (field.ErrorState > 0)
                {
                    _markerTip.AddLineMessage(lineNumber, GetErrorForField(_currentRecord.Report, field));
                    _contentsBox.SelectionColor = _errorTextColor;
                };

                _contentsBox.AppendText(indentation);

                _contentsBox.SelectionFont = _defaultFont;
                _contentsBox.AppendText(markerPrefix + field.Marker + "\t");

                _contentsBox.SelectionColor = _defaultTextColor;
                _contentsBox.SelectionFont = _model.DisplayFont(field.Marker) ?? _defaultFont;
                _contentsBox.AppendText(field.Value + "\n");

                lineNumber++;
            }

            _contentsBox.SelectionFont = _defaultFont;
            _contentsBox.SelectionColor = _defaultTextColor;
            _contentsBox.SelectionStart = (foundProcessingMark) ? currentPosition - 1 : 0;

            _contentsBox.TextChanged += _contentsBox_TextChanged;
        }

        private string GetErrorForField(SolidReport report, Record.Field field)
        {
            SolidReport.Entry entry = report.GetEntryById(field.Id);
            if (entry != null)
            {
                return entry.Description;
            }
            return "This isn't really an error";
        }

        private string GetContentsBoxTextWithoutInferredFields()
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
            SaveContentsOfTextBox();
            ClearContentsOfTextBox();
            DisplayEachFieldInCurrentRecord();
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

            if (positionOfCursor.X < 120 && CursorIsAboveLastLine())
            {
                int indexOfNearestCharacter = _contentsBox.GetCharIndexFromPosition(positionOfCursor);
                Point positionOfNearestCharacter = _contentsBox.GetPositionFromCharIndex(indexOfNearestCharacter);
                int xCursorDistanceFromNearestCharacter = Math.Abs(positionOfCursor.X - positionOfNearestCharacter.X);
                int yCursorDistanceFromNearestCharacter = Math.Abs(positionOfCursor.Y - positionOfNearestCharacter.Y);
                return xCursorDistanceFromNearestCharacter < 5;
            }

            return false;
            
        }

        private void _contentsBox_MouseLeave(object sender, EventArgs e)
        {
            if (_markerTip != null)
            {
                _markerTipDisplayDelay = 5;
                _lineNumber = -1;
                _markerTip.Hide();
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
                    _markerTipDisplayDelay = (_markerTip.Showing) ? 0 : 5;
                    _markerTip.Hide();
                }
            }
            else
            {
                _lineNumber = -1;
                _markerTip.Hide();
            }
        }

        public void OnTick(object sender, EventArgs e)
        {
            _markerTipDisplayDelay--;
            if (_markerTipDisplayDelay < 0 && !_markerTip.Showing)
            {
                _markerTip.ShowMessageForLine(_lineNumber);
            }
        }

        private int GetIndex()
        {
            int nearestCharacterIndex = -1;

            if (CursorIsOnMarker())
            {
                nearestCharacterIndex = _contentsBox.GetCharIndexFromPosition(_contentsBox.PointToClient(MousePosition));
            }
            return nearestCharacterIndex;
        }

        private void _contentsBox_TextChanged(object sender, EventArgs e)
        {
            if (RecordTextChanged != null)
                RecordTextChanged.Invoke(this, new EventArgs());
        }

        private void _contentsBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (_markerTip != null)
            {
                _markerTipDisplayDelay = 5;
                _lineNumber = -1;
                _markerTip.Hide();
            }
        }
    }
}
