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
        public void OnFilterChanged(object sender, FilterChooserPM.RecordFilterChangedEventArgs e)
        {
            _changingFilter = true;
            bool foundFilter = false;
            foreach (RecordFilter filter in _filterListBox.Items)
            {
                if (filter == e._recordFilter)
                {
                    foundFilter = true;
                    _filterListBox.SelectedIndex = _filterListBox.Items.IndexOf(filter);
                    break;
                }
            }
            if (!foundFilter)
            {
                _filterListBox.SelectedIndex = -1;
            }
            _changingFilter = false;
        }

        private void _filterList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_filterListBox.SelectedItems != null && _filterListBox.SelectedItems.Count > 0 && !_changingFilter)
            {
                _model.ActiveRecordFilter = (RecordFilter) _filterListBox.SelectedItem;
            }
        }

        public void UpdateDisplay()
        {
            if (DesignMode)
            {
                return;
            }

            _filterListBox.Items.Clear();

            foreach (RecordFilter filter in _model.RecordFilters)
            {
                _filterListBox.Items.Add(filter);
            }
        }
    }
}
