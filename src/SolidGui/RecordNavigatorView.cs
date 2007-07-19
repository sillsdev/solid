using System;
using System.Windows.Forms;

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

        public RecordNavigatorView()
        {
            InitializeComponent();
//            _searchButton.Image =
//                _searchButton.Image.GetThumbnailImage(_searchButton.Width - 8, _searchButton.Height - 8, ReturnFalse,
//                                                      System.IntPtr.Zero);
        }

        private bool ReturnFalse()
        {
            return false;
        }

        public void BindModel(RecordNavigatorPM model)
        {
                _model = model;
                //if (_model == null)//happens at design time
                //    return;
                //UpdateDisplay();
        }

        private void _PreviousButton_Click(object sender, EventArgs e)
        {
            _model.MoveToPrevious();
            UpdateDisplay();
        }

        public void UpdateDisplay()
        {
            if (_model == null)
                return;
            _descriptionLabel.Text = _model.Description;
            _nextButton.Enabled = _model.CanGoNext();
            _PreviousButton.Enabled = _model.CanGoPrev();
            _firstButton.Enabled = _model.CanGoPrev();
            _recordNumber.Text = string.Format("{0}", _model.CurrentRecordIndex+1);
        }

        private void _nextButton_Click(object sender, EventArgs e)
        {
            _model.MoveToNext();
            UpdateDisplay();
        }

        public void OnFilterChanged(object sender, FilterChooserPM.RecordFilterChangedEventArgs e)
        {
            UpdateDisplay();
        }

        private void _firstButton_Click(object sender, EventArgs e)
        {
            _model.MoveToFirst();
            UpdateDisplay();
        }

        private void _lastButton_Click(object sender, EventArgs e)
        {
            _model.MoveToLast();
            UpdateDisplay();
        }

        private void _searchButton_Click(object sender, EventArgs e)
        {
            SearchButtonClicked.Invoke(this, new EventArgs());
        }
    }
}
