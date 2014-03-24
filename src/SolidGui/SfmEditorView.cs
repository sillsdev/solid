// Copyright (c) 2007-2014 SIL International
// Licensed under the MIT license: opensource.org/licenses/MIT

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Palaso.UI.WindowsForms.SuperToolTip;
using SolidGui.Engine;
using SolidGui.Model;
using Spart.Parsers.NonTerminal;


namespace SolidGui
{
    public partial class SfmEditorView : UserControl
    {
        private const int _leftMarigin = 20;
        private readonly RichTextBox _contentsBoxDB; // Cheap double buffer for the ContentsBox

        private readonly MarkerTip _markerTip;

        public bool IsDirty;
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

        public const int TabPositionNear = 50;
        public const int TabPositionFar = 130;
        private static int _tabPosition = TabPositionFar;
        public static int TabPosition
        {
            get { return _tabPosition; }
            set { _tabPosition = value; }
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

        public void UpdateBoth()
        {
            UpdateModel();
            UpdateView();
        }

        private void RefreshRecord()
        {
            UpdateBoth();
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
            }
            ContentsBox.Focus();
        }
       
        // "Save" from the on-screen editing box into memory
        public void UpdateModel()
        {
            //int currentIndex = ContentsBox.SelectionStart;
            if (_currentRecord != null && IsDirty)  // && ContentsBox.Text.Length > 0)  // We now allow clearing to delete a record, so zero length is fine. -JMC 2013-09
            {
                _model.SfmEditorModel.UpdateCurrentRecord(_currentRecord, ContentsBox.Text);
            }
            IsDirty = false;
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

            if (_currentRecord == null || _currentRecord.Fields.Count < 1 || _currentRecord.Fields[0] == null)
            {
                ContentsBox.Text = "";
                ContentsBox.Visible = false; // Don't let the user edit--the data wouldn't be saveable. -JMC
                return;
            }
            ContentsBox.Visible = true;
            
            _markerTip.ClearLineMessages();
            _contentsBoxDB.Clear();
            _contentsBoxDB.SelectAll();
            _contentsBoxDB.SelectionTabs = new[] { _tabPosition };


            //The new way, using RecordFormatter (note all the side effects)
            _model.EditorRecordFormatter.FormatRich(_currentRecord, _model, _contentsBoxDB, HighlightMarkers, _markerTip);
            
            _contentsBoxDB.SelectionStart = 0;  // = (foundProcessingMark) ? currentPosition - 1 : 0;
            // Copy the buffer to the real control.
            ContentsBox.Rtf = _contentsBoxDB.Rtf;

            // JMC:! Temporary check
            string plain = _model.EditorRecordFormatter.FormatPlain(_currentRecord, _model.Settings);
            if (_contentsBoxDB.Text != plain)
            {
                throw new Exception("RecordFormatter failed to provide an identical copy for Find/Replace.\nXX" + _contentsBoxDB.Text + "XX" + plain + "XX");
            }

            ContentsBox.TextChanged += _contentsBox_TextChanged;
        }

        // Warning: You should almost always call UpdateModel first, or edits may be lost!
        public void UpdateView()
        {
            string backup = ContentsBox.Text;
            try
            {
                ClearContentsOfTextBox();
                DisplayEachFieldInCurrentRecord();
            }
            catch (Exception error)
            {
                string msg = string.Format("An unexpected error occurred; it's safest if you now Save As and compare with the previous version. (Or, don't save.):\r\n{0}\r\n", error);
                Palaso.Reporting.ErrorReport.ReportNonFatalExceptionWithMessage(error, msg);
                IsDirty = false;  // a little white lie that lets us exit. -JMC
                //ContentsBox.Text = backup;
            }
        }

        // This was identical to UpdateView() ; if intent is really the same, we should merge them. -JMC Mar 2014
        public void Reload()
        {
            UpdateView();
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
                Point point = MousePosition;
                _markerTip.ShowMessageForLine(_lineNumber, point);
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
            IsDirty = true;
            if (RecordTextChanged != null)
            {
                RecordTextChanged.Invoke(this, EventArgs.Empty);
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

        // JMC: There also used to be a KeyUp event, which apparently detected a space after a backslash and 
        // auto-indented on the fly. It's been dead code for a long time, so I've removed it ( just after this changeset: 19 Mar 2014, 97f17ef696a04430e0f1a88e65121f694564e29d )

        // Mainly handles the shortcut keys for the navigator view, but also for Recheck All.
        private void _contentsBox_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.PageDown:
                    if (e.Control)
                    {
                        if (e.Shift)
                            _model.SfmEditorModel.MoveToLast();  // Ctrl+Shift+PgDn
                        else
                            _model.SfmEditorModel.MoveToNext();  // Ctrl+PgDn
                        e.Handled = true;
                    }
                    break;
                case Keys.PageUp:
                    if (e.Control)
                    {
                        if (e.Shift)
                            _model.SfmEditorModel.MoveToFirst();  // Ctrl+Shift+PgUp
                        else
                            _model.SfmEditorModel.MoveToPrevious(); // Ctrl+PgUp
                        e.Handled = true;
                    }
                    break;
                case Keys.F5:
                    if (e.Control)
                    {
                        RecheckKeystroke.Invoke(this, EventArgs.Empty);  // Ctrl+F5
                    }
                    else
                    {
                        RefreshRecord();  // F5
                    }
                    e.Handled = true;
                    break;
            }
        }

        //Fixes #1255 "Edits made just before Find (or marker settings) are lost" -JMC Mar 2014
        private void ContentsBox_Leave(object sender, EventArgs e)
        {
            if (IsDirty)
            {
                UpdateBoth();
            }
        }

    }

    // This was an inner class of SfmEditorView. I split it out so it could be passed to RecordFormatter. -JMC Mar 2014
    public class MarkerTip: SuperToolTip
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
            var ret = _lineMessage.GetSetDefault(line, ""); // Fixes #1256 (using the new Extensions class)

            _lineMessage[line] = ret + message + "\n\n";
        }

        public void ShowMessage(string message, Point point)
        {

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
        public void ShowMessageForLine(int line, Point point)
        {
            if(_lineMessage.ContainsKey(line))
                ShowMessage(_lineMessage[line], point);
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


}
