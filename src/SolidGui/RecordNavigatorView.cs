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
        public RecordNavigatorView()
        {
            InitializeComponent();
        }

        public RecordNavigatorPM Model
        {
            get
            {
                return _model;
            }
            set
            {
                _model = value;
                if (_model == null)//happens at design time
                    return;
                UpdateDisplay();
            }
        }

        private void _PreviousButton_Click(object sender, EventArgs e)
        {
            _model.Previous();
            UpdateDisplay();
        }

        public void UpdateDisplay()
        {
            if (_model == null)
                return;
            _descriptionLabel.Text = _model.Description;
            _nextButton.Enabled = _model.CanGoNext();
            _PreviousButton.Enabled = _model.CanGoPrev();
            _recordNumber.Text = string.Format("{0}/{1}", _model.CurrentIndexIntoFilteredRecords+1, _model.Count);
        }

        private void _nextButton_Click(object sender, EventArgs e)
        {
            _model.Next();
            UpdateDisplay();
        }

        public void OnFilterChanged(object sender, FilterChooserPM.RecordFilterChangedEventArgs e)
        {
            UpdateDisplay();
        }
    }
}
