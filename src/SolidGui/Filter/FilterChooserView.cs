using System;
using System.Windows.Forms;

namespace SolidGui.Filter
{
    /// <summary>
    /// The filter chooser is the list of filters the user can click on.
    /// This class is the View (ui-specific) half of this control
    /// </summary>
    public partial class FilterChooserView : UserControl
    {
        private FilterChooserPM _model;
        private bool _changingFilter = false;

        //??? Would be nice if we didn't need to expose this. CJP
        public FilterChooserPM Model
        {
            get { return _model; }
        }

        public FilterChooserView()
        {
            InitializeComponent();
        }

        public void BindModel(FilterChooserPM model)
        {
            _model = model;
        }

        private void FilterChooserView_Load(object sender, EventArgs e)
        {
            UpdateDisplay();            
        }

        //when someone changes the filter in our PM
        public void OnWarningFilterChanged(object sender, RecordFilterChangedEventArgs e)
        {

            _changingFilter = true; // prevents event-firing loops -JMC

            // Remove the selection (start with blank slate)
            _warningFilterListBox.SelectedIndex = -1;

            // Find the new filter in our list and select it
            if (e.RecordFilter != null)
            {
                _warningFilterListBox.SelectedIndex = _warningFilterListBox.Items.IndexOf(e.RecordFilter);
            }

/*
            foreach (RecordFilter filter in _warningFilterListBox.Items)
            {
                if (filter == e.RecordFilter)
                {
                    _warningFilterListBox.SelectedIndex = _warningFilterListBox.Items.IndexOf(filter);
                    break;
                }
            }
*/

            _changingFilter = false; 
        }

        private void _filterList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_changingFilter)
            {
                var sel = _warningFilterListBox.SelectedItems;
                if (sel != null && sel.Count > 0)
                {
                    _model.ActiveWarningFilter = (RecordFilter)_warningFilterListBox.SelectedItem;
                }
            }
        }

        public void UpdateDisplay()
        {
            if (DesignMode)
            {
                return;
            }

            _warningFilterListBox.Items.Clear();

            foreach (RecordFilter filter in _model.RecordFilters)
            {
                _warningFilterListBox.Items.Add(filter);
            }
        }
    }
}
