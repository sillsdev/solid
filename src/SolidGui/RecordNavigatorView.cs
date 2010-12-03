using System;
using System.Windows.Forms;
using SolidGui.Filter;

namespace SolidGui
{
    /// <summary>
    /// The record navigator the control that shows the user the description of the current
    /// filter and lets them say "next" and "previous".
    /// This class is the Presentation Model(ui-specific) half of this control
    /// </summary>
    public partial class RecordNavigatorView : UserControl
    {
        private RecordNavigatorPM _model;
        public event EventHandler SearchButtonClicked;

        private readonly ToolTip _ttPrevious;
        private readonly ToolTip _ttNext;
        private readonly ToolTip _ttFirst;
        private readonly ToolTip _ttFind;
        private readonly ToolTip _ttRefresh;

        public RecordNavigatorView()
        {
            InitializeComponent();
//            _findButton.Image =
//                _findButton.Image.GetThumbnailImage(_findButton.Width - 8, _findButton.Height - 8, ReturnFalse,
//                                                      System.IntPtr.Zero);
            _descriptionLabel.Text = "";

            // Tooltips
            _ttPrevious = new ToolTip();
            _ttPrevious.SetToolTip(_previousButton, "Previous (Ctrl+PgUp)");
            _ttNext = new ToolTip();
            _ttNext.SetToolTip(_nextButton, "Next (Ctrl+PgDown)");
            _ttFirst = new ToolTip();
            _ttFirst.SetToolTip(_firstButton, "First (Ctrl+Shift+PgUp)");
            _ttFind = new ToolTip();
            _ttFind.SetToolTip(_findButton, "Find (Ctrl+F)");
            _ttRefresh = new ToolTip();
            _ttRefresh.SetToolTip(_recheckButton, "Refresh (F5)");
        }

        private bool ReturnFalse()
        {
            return false;
        }

        public void BindModel(RecordNavigatorPM model)
        {
            _model = model;
            _model.RecordChanged += OnRecordChanged;
                //if (_model == null)//happens at design time
                //    return;
                //UpdateDisplay();
        }

        public void UpdateDisplay()
        {
            if (_model == null)
                return;
            _descriptionLabel.Text = _model.Description;
            _nextButton.Enabled = _model.CanGoNext();
            _previousButton.Enabled = _model.CanGoPrev();
            _firstButton.Enabled = _model.CanGoPrev();
            _recordNumber.Text = string.Format("{0}", _model.CurrentRecordIndex+1);
        }

        public void OnFilterChanged(object sender, FilterChooserPM.RecordFilterChangedEventArgs e)
        {
            UpdateDisplay();
        }

        public void OnRecordChanged(object sender, RecordNavigatorPM.RecordChangedEventArgs e)
        {
            UpdateDisplay();
        }

        private void _PreviousButton_Click(object sender, EventArgs e)
        {
            _model.MoveToPrevious();
        }

        private void _nextButton_Click(object sender, EventArgs e)
        {
            _model.MoveToNext();
        }

        private void _firstButton_Click(object sender, EventArgs e)
        {
            _model.MoveToFirst();
        }

        private void _lastButton_Click(object sender, EventArgs e)
        {
            _model.MoveToLast();
        }

        private void _searchButton_Click(object sender, EventArgs e)
        {
            SearchButtonClicked.Invoke(this, new EventArgs());
        }

        private void RecordNavigatorView_Load(object sender, EventArgs e)
        {
            _descriptionLabel.Text = "";
            _recordNumber.Text = "0";
        }

    }
}
