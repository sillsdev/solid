using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SolidGui
{
    public partial class RecordNavigatorView : UserControl
    {
        private RecordNavigatorPresentationModel _model;
        public RecordNavigatorView()
        {
            InitializeComponent();
        }

        public RecordNavigatorPresentationModel Model
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

        private void UpdateDisplay()
        {
            if (_model == null)
                return;
            _descriptionLabel.Text = _model.Description;
            _nextButton.Enabled = _model.CanGoNext();
            _PreviousButton.Enabled = _model.CanGoPrev();
            _recordNumber.Text = string.Format("{0}/{1}", _model.CurrentIndex+1, _model.Count);
        }

        private void _nextButton_Click(object sender, EventArgs e)
        {
            _model.Next();
            UpdateDisplay();
        }

        public void OnFilterChanged(object sender, FilterListPresentationModel.RecordFilterChangedEventArgs e)
        {
            UpdateDisplay();
        }
    }
}
