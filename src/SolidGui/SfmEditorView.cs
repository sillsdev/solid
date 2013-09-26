using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Palaso.UI.WindowsForms.SuperToolTip;
using SolidGui.Model;


namespace SolidGui
{
    public partial class SfmEditorView : UserControl
    {
        private const int _leftMarigin = 20;
        private const int _spacesInIndentation = 4;
        private readonly RichTextBox _contentsBoxDB; // Cheap double buffer for the _contentsBox
        private readonly Font _defaultFont = new Font(FontFamily.GenericSansSerif, 13);
        private readonly Color _defaultTextColor = Color.Black;
        private readonly Color _errorTextColor = Color.Red;
        private readonly Font _highlightMarkerFont = new Font(FontFamily.GenericSansSerif, 13, FontStyle.Bold);
        private readonly Color _inferredTextColor = Color.Blue;

        private readonly KeyScanner _keyScanner = new KeyScanner();
        //private const string _processingMark = "\x01";
        
        private readonly MarkerTip _markerTip;
        private Record _currentRecord;
        private int _indent = 130;
        private bool _isDirty;
        private int _lineNumber = -1;
        private int _markerTipDisplayDelay = 10;
        private SfmEditorPM _model;

        public SfmEditorView()
        {
            _currentRecord = null;
            InitializeComponent();
            _contentsBoxDB = new RichTextBox();
            _contentsBoxDB.Visible = false;
            _contentsBox.TextChanged -= _contentsBox_TextChanged;
            _contentsBox.SelectionIndent = _leftMarigin;
            _markerTip = new MarkerTip(_contentsBox, components);
            _timer.Tick += OnTick;
            _timer.Start();
            _contentsBox.DragEnter += _contentsBox_DragEnter;
            _contentsBox.TextChanged += _contentsBox_TextChanged;
        }

        public int Indent
        {
            get { return _indent; }
            set { _indent = value; }
        }

        public IEnumerable<string> HighlightMarkers{ get; set;}
        public event EventHandler RecordTextChanged;

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
            Recheck();
            _contentsBox.Focus();
        }

        private void Recheck()
        {
            UpdateModel();
            UpdateView();
        }

        public void BindModel(SfmEditorPM model)
        {
            _model = model;
        }

        public void Highlight(int startIndex, int length)
        {
            _contentsBox.TextChanged -= _contentsBox_TextChanged;
            _contentsBox.Select(startIndex, length);
            _contentsBox.TextChanged += _contentsBox_TextChanged;
        }

        public void OnRecordChanged(object sender, RecordNavigatorPM.RecordChangedEventArgs e)
        {
            if (e.Record == null)
            {
                _currentRecord = null;
                ClearContentsOfTextBox();
                return;
            }
            if (_currentRecord != e.Record || HighlightMarkers != e.HighlightMarkers)
            {
                UpdateModel();
                _currentRecord = e.Record;
                HighlightMarkers =  e.HighlightMarkers;
                UpdateView();
                _keyScanner.Reset();
            }
            _contentsBox.Focus();
        }
       
        public void UpdateModel()
        {
            //int currentIndex = _contentsBox.SelectionStart;
            if (_currentRecord != null && _isDirty && _contentsBox.Text.Length > 0)
            {
                _model.UpdateCurrentRecord(_currentRecord, _contentsBox.Text);
            }
            _isDirty = false;
            _model.SolidSettings.NotifyIfNewMarkers();
            //_contentsBox.SelectionStart = currentIndex;
        }

        public void ClearContentsOfTextBox()
        {
            _contentsBox.TextChanged -= _contentsBox_TextChanged;
            _contentsBox.Clear();
            _contentsBox.TextChanged += _contentsBox_TextChanged;
        }

        public void DisplayEachFieldInCurrentRecord()
        {
            // Note: This uses a non visible control to render in, and then copies the RTF to the visible control.
            // This prevents undesirable visual effects caused by moving the selection point in the visible control.
            _contentsBox.TextChanged -= _contentsBox_TextChanged;

            _markerTip.ClearLineMessages();
            _contentsBoxDB.Clear();
            _contentsBoxDB.SelectAll();
            _contentsBoxDB.SelectionTabs = new[] { _indent };

            const int currentPosition = 0;
            const bool foundProcessingMark = false;
            int lineNumber = 0;

            foreach (SfmFieldModel field in _currentRecord.Fields)
            {
                string indentation = new string(' ', field.Depth * _spacesInIndentation);
                string markerPrefix = (field.Inferred) ? "\\+" : "\\";
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
                _contentsBoxDB.SelectionColor = _defaultTextColor;
                if (field.Inferred)
                {
                    _markerTip.AddLineMessage(lineNumber, "Inferred");
                    _contentsBoxDB.SelectionColor = _inferredTextColor;
                }
                foreach (var reportEntry in field.ReportEntries)
                {
                    _markerTip.AddLineMessage(lineNumber, reportEntry.Description);
                    _contentsBoxDB.SelectionColor = _errorTextColor;
                }

                // 1) Indentation
                _contentsBoxDB.AppendText(indentation);

                // 2) Marker
                string marker = field.Marker.Trim(new[] { '_' });
                if (HighlightMarkers!=null && HighlightMarkers.Contains(marker))
                {
                    _contentsBoxDB.SelectionFont = _highlightMarkerFont;
                }
                else
                {
                    _contentsBoxDB.SelectionFont = _defaultFont;
                }
                _contentsBoxDB.AppendText(markerPrefix + marker + "\t");

                // 3) Value
                _contentsBoxDB.SelectionColor = _defaultTextColor;
                _contentsBoxDB.SelectionFont = _model.FontForMarker(field.Marker) ?? _defaultFont;
                string displayValue = _model.GetUnicodeValueFromLatin1(field);
                _contentsBoxDB.AppendText(displayValue + "\n");

                lineNumber++;
            }

            _contentsBoxDB.SelectionFont = _defaultFont;
            _contentsBoxDB.SelectionColor = _defaultTextColor;
            _contentsBoxDB.SelectionStart = (foundProcessingMark) ? currentPosition - 1 : 0;

            // Copy the buffer to the real control.
            _contentsBox.Rtf = _contentsBoxDB.Rtf;

            _contentsBox.TextChanged += _contentsBox_TextChanged;
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
        public void OnSolidSettingsChange()
        {
            UpdateView();
        }

        private void UpdateView()
        {
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

        [DebuggerStepThrough] // JMC: for now anyway
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
            _isDirty = true;
            if (RecordTextChanged != null)
            {
                RecordTextChanged.Invoke(this, new EventArgs());
            }
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

        private void _contentsBox_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.PageDown:
                    if (e.Control)
                    {
                        if (e.Shift)
                            _model.MoveToLast();
                        else
                            _model.MoveToNext();
                        e.Handled = true;
                    }
                    break;
                case Keys.PageUp:
                    if (e.Control)
                    {
                        if (e.Shift)
                            _model.MoveToFirst();
                        else
                            _model.MoveToPrevious();
                        e.Handled = true;
                    }
                    break;
                case Keys.F5:
                    Recheck();
                    e.Handled = true;
                    break;
            }
        }

        #region Nested type: KeyScanner

        class KeyScanner
        {
            State _state;

            public KeyScanner()
            {
                Reset();
            }

            public void Reset()
            {
                _state = State.ScanBackslash;
            }

            public bool ProcessKey(int c)  //JMC: Dead code? Delete it?
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

            #region Nested type: State

            enum State
            {
                ScanBackslash,
                ScanWhite
            }

            #endregion
        }

        #endregion

        #region Nested type: MarkerTip

        class MarkerTip: SuperToolTip
        {
            private readonly Dictionary<int, string> _lineMessage = new Dictionary<int, string>();
            private readonly Control _textBox;
            private bool _showing;

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
                [DebuggerStepThrough]
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

                superToolTipInfo.BackgroundGradientBegin = Color.FromArgb(((((255)))), ((((255)))), ((((255)))));
                superToolTipInfo.BackgroundGradientEnd = Color.FromArgb(((((202)))), ((((218)))), ((((239)))));
                superToolTipInfo.BackgroundGradientMiddle = Color.FromArgb(((((242)))), ((((246)))), ((((251)))));
                superToolTipInfo.BodyFont = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((0)));
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

            [DebuggerStepThrough] 
            public void ShowMessageForLine(int line)
            {
                if(_lineMessage.ContainsKey(line))
                    ShowMessage(_lineMessage[line]);
                else 
                    Hide();
            }

            [DebuggerStepThrough]
            public void Hide()
            {
                if (_showing)
                {
                    _showing = false;
                    Close();
                }
            }

            [DebuggerStepThrough]
            protected override void MouseEntered(object sender, EventArgs e)
            {
                
            }

            [DebuggerStepThrough]
            protected override void MouseLeft(object sender, EventArgs e)
            {
                
            }
        }

        #endregion

        public void Reload()
        {
            ClearContentsOfTextBox();
            DisplayEachFieldInCurrentRecord();
        }    
    }
}
