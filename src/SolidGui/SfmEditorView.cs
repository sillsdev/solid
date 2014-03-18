// Copyright (c) 2007-2014 SIL International
// Licensed under the MIT license: opensource.org/licenses/MIT

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Palaso.UI.WindowsForms.SuperToolTip;
using SolidGui.Engine;
using SolidGui.Model;


namespace SolidGui
{
    public partial class SfmEditorView : UserControl
    {
        private const int _leftMarigin = 20;
        private const int _spacesInIndentation = 4;
        private readonly RichTextBox _contentsBoxDB; // Cheap double buffer for the ContentsBox
        private readonly Font _defaultFont = new Font(FontFamily.GenericSansSerif, 13);
        private readonly Color _defaultTextColor = Color.Black;
        private readonly Color _errorTextColor = Color.Red;
        private readonly Font _highlightMarkerFont = new Font(FontFamily.GenericSansSerif, 13, FontStyle.Bold);
        private readonly Color _inferredTextColor = Color.Blue;

        private readonly KeyScanner _keyScanner = new KeyScanner();
        //private const string _processingMark = "\x01";
        
        private readonly MarkerTip _markerTip;
        private int _indent = 130;
        private bool _isDirty;
        private int _lineNumber = -1;
        private int _markerTipDisplayDelay = 10;

        private Record _currentRecord; // /
        private MainWindowPM _model; // /

        public SfmEditorView()
        {
            _currentRecord = null;
            InitializeComponent();
            // JMC: Add a call here to KeyboardController.Register() ? Would need some smarts for determining what field the cursor is in.
            _contentsBoxDB = new RichTextBox();
            _contentsBoxDB.Visible = false;
            ContentsBox.TextChanged -= _contentsBox_TextChanged;
            ContentsBox.SelectionIndent = _leftMarigin;
            _markerTip = new MarkerTip(ContentsBox, components);
            _timer.Tick += OnTick;
            _timer.Start();
            ContentsBox.DragEnter += _contentsBox_DragEnter;
            ContentsBox.TextChanged += _contentsBox_TextChanged;
        }

        public int Indent
        {
            get { return _indent; }
            set { _indent = value; }
        }

        public IEnumerable<string> HighlightMarkers{ get; set;}
        public event EventHandler RecordTextChanged;
        public event EventHandler RecheckKeystroke;

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

        public void OnRefreshClicked(object sender, EventArgs e)
        {
            RefreshRecord();
        }

        public void OnNavFilterChanged(object sender, RecordFilterChangedEventArgs e)
        {
            Reload();
        }

        private void RefreshRecord()
        {
            UpdateModel();
            UpdateView();
            ContentsBox.Focus();
        }

        public void BindModel(MainWindowPM model)
        {
            _model = model;
            _currentRecord = null;
        }

        public void Highlight(int startIndex, int length)
        {
            ContentsBox.TextChanged -= _contentsBox_TextChanged;
            ContentsBox.Select(startIndex, length);
            ContentsBox.TextChanged += _contentsBox_TextChanged;
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
            ContentsBox.Focus();
        }
       
        public void UpdateModel()
        {
            //int currentIndex = ContentsBox.SelectionStart;
            if (_currentRecord != null && _isDirty)  // && ContentsBox.Text.Length > 0)  // We now allow clearing to delete a record, so zero length is fine. -JMC 2013-09
            {
                _model.SfmEditorModel.UpdateCurrentRecord(_currentRecord, ContentsBox.Text);
            }
            _isDirty = false;
            if (_model.Settings != null)
            {
                _model.Settings.NotifyIfNewMarkers();
            }
            //ContentsBox.SelectionStart = currentIndex;
        }

        public void ClearContentsOfTextBox()
        {
            ContentsBox.TextChanged -= _contentsBox_TextChanged;
            ContentsBox.Clear();
            ContentsBox.TextChanged += _contentsBox_TextChanged;
        }

        public void DisplayEachFieldInCurrentRecord()
        {
            // Note: This uses a non visible control to render in, and then copies the RTF to the visible control.
            // This prevents undesirable visual effects caused by moving the selection point in the visible control.
            ContentsBox.TextChanged -= _contentsBox_TextChanged;

            _markerTip.ClearLineMessages();
            _contentsBoxDB.Clear();
            _contentsBoxDB.SelectAll();
            _contentsBoxDB.SelectionTabs = new[] { _indent };

            //const int currentPosition = 0;
            //const bool foundProcessingMark = false;
            int lineNumber = 0;

            if (_currentRecord == null || _currentRecord.Fields.Count < 1 || _currentRecord.Fields[0] == null)
            {
                ContentsBox.Text = "";
                ContentsBox.Visible = false; // Don't let the user edit--the data wouldn't be saveable. -JMC
                return;
            }
            ContentsBox.Visible = true;

            // JMC:! Most or all of the following needs to go into into a method (see ToStructuredString), perhaps in SfmLexEntry or SfmEditorPM

            foreach (SfmFieldModel field in _currentRecord.Fields)
            {
                if (field == null) break;
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
                foreach (ReportEntry reportEntry in field.ReportEntries)
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
                _contentsBoxDB.AppendText(markerPrefix + marker);

                // 3) (tab + Value) + Trailing Whitespace 
                _contentsBoxDB.SelectionColor = _defaultTextColor;
                _contentsBoxDB.SelectionFont = _model.SfmEditorModel.FontForMarker(field.Marker) ?? _defaultFont;
                string displayValue = _model.SfmEditorModel.GetUnicodeValueFromLatin1(field);
                if (displayValue != "")
                {
                    _contentsBoxDB.AppendText("\t" + displayValue);
                }
                _contentsBoxDB.AppendText(field.Trailing);
                
                lineNumber++;
            }

            _contentsBoxDB.SelectionFont = _defaultFont;
            _contentsBoxDB.SelectionColor = _defaultTextColor;
            _contentsBoxDB.SelectionStart = 0;  // = (foundProcessingMark) ? currentPosition - 1 : 0;

            // Copy the buffer to the real control.
            ContentsBox.Rtf = _contentsBoxDB.Rtf;

            ContentsBox.TextChanged += _contentsBox_TextChanged;
        }


/*        
        private void _contentsBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (_keyScanner.ProcessKey(e.KeyValue))
            {
                ContentsBox.SelectedText = _processingMark;
                UpdateContentsOfTextBox();
                _keyScanner.Reset();
            }
            if (RecordTextChanged != null)
            {
                RecordTextChanged.Invoke(this, new EventArgs());
            }
        }
*/

        public void UpdateView()
        {
            ClearContentsOfTextBox();
            DisplayEachFieldInCurrentRecord();
        }

        public void Reload()
        {
            ClearContentsOfTextBox();
            DisplayEachFieldInCurrentRecord();
        }    

        [DebuggerStepThrough]
        private bool CursorIsAboveLastLine()  //JMC: What's the purpose here? AFAIK this always returns true. Maybe it's due to the multiplication below?
        {
            Point cursorPositionRelativeToContentsBox = ContentsBox.PointToClient(MousePosition);
            int YpositionOfLastLineOfText = ContentsBox.Lines.Length * ContentsBox.Font.Height;
            return cursorPositionRelativeToContentsBox.Y < YpositionOfLastLineOfText;
        }

        [DebuggerStepThrough]
        private bool CursorIsOnMarker()
        {
            Point positionOfCursor = ContentsBox.PointToClient(MousePosition);

            if (positionOfCursor.X < 120 && CursorIsAboveLastLine())
            {
                int indexOfNearestCharacter = ContentsBox.GetCharIndexFromPosition(positionOfCursor);
                Point positionOfNearestCharacter = ContentsBox.GetPositionFromCharIndex(indexOfNearestCharacter);
                int xCursorDistanceFromNearestCharacter = Math.Abs(positionOfCursor.X - positionOfNearestCharacter.X);
                int yCursorDistanceFromNearestCharacter = Math.Abs(positionOfCursor.Y - positionOfNearestCharacter.Y);
                return xCursorDistanceFromNearestCharacter < 5;
            }

            return false;
            
        }

        [DebuggerStepThrough]
        private void _contentsBox_MouseLeave(object sender, EventArgs e)
        {
            if (_markerTip != null)
            {
                _markerTipDisplayDelay = 5;
                _lineNumber = -1;
                _markerTip.Hide();
            }
        }

        [DebuggerStepThrough] // JMC: for now anyway (might remove later; see the "fixed #1201" revision of SfmEditorView, just after rev 33c506d54a74)
        private void _contentsBox_MouseMove(object sender, MouseEventArgs e)
        {
            int index = GetIndex();

            if (index != -1)
            {
                int newLineNumber = ContentsBox.GetLineFromCharIndex(index);

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

        [DebuggerStepThrough]
        private int GetIndex()
        {
            int nearestCharacterIndex = -1;

            if (CursorIsOnMarker())
            {
                nearestCharacterIndex = ContentsBox.GetCharIndexFromPosition(ContentsBox.PointToClient(MousePosition));
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
                            _model.SfmEditorModel.MoveToLast();
                        else
                            _model.SfmEditorModel.MoveToNext();
                        e.Handled = true;
                    }
                    break;
                case Keys.PageUp:
                    if (e.Control)
                    {
                        if (e.Shift)
                            _model.SfmEditorModel.MoveToFirst();
                        else
                            _model.SfmEditorModel.MoveToPrevious();
                        e.Handled = true;
                    }
                    break;
                case Keys.F5:
                    if (e.Control)
                    {
                        RecheckKeystroke.Invoke(this, new EventArgs());
                    }
                    else
                    {
                        RefreshRecord();
                    }
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

    }
}
