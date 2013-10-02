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
        public FilterChooserPM Model  //JMC: unused? delete?
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
        public void OnWarningFilterChanged(object sender, FilterChooserPM.RecordFilterChangedEventArgs e)
        {
            _changingFilter = true; // lock
            bool foundFilter = false;
            foreach (RecordFilter filter in _warningFilterListBox.Items)
            {
                if (filter == e.RecordFilter)
                {
                    foundFilter = true;
                    _warningFilterListBox.SelectedIndex = _warningFilterListBox.Items.IndexOf(filter);
                    break;
                }
            }
            if (!foundFilter)
            {
                _warningFilterListBox.SelectedIndex = -1;
            }
            _changingFilter = false; // unlock
        }

        private void _filterList_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            var sel = _warningFilterListBox.SelectedItems;
            // JMC: Just in case, maybe add a wait, if/while _changingFilter is true
            if (sel != null && sel.Count > 0 && !_changingFilter)
            {
                _model.ActiveWarningFilter = (RecordFilter)_warningFilterListBox.SelectedItem;
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
