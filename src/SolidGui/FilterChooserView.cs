using System;
using System.Windows.Forms;

namespace SolidGui
{
    /// <summary>
    /// The filter chooser is the list of filters the user can click on.
    /// This class is the View (ui-specific) half of this control
    /// </summary>
    public partial class FilterChooserView : UserControl
    {
        private FilterChooserPM _model;
        private bool _changingFilter = false;

        public FilterChooserView()
        {
            InitializeComponent();
        }

        public FilterChooserPM Model
        {
            get
            {
                return _model;
            }
            set
            {
                _model = value;
            }
        }

        private void FilterChooserView_Load(object sender, EventArgs e)
        {
            if (DesignMode)
            {
                return;
            }
            _listControl.Clear();

            foreach (RecordFilter filter in Model.RecordFilters)
            {
                ListViewItem item = new ListViewItem();
                item.Tag = filter;
                item.Text = filter.Name;
                _listControl.Items.Add(item);
            }
            
        }

        //when someone changes the filter in our PM
        public void OnFilterChanged(object sender, FilterChooserPM.RecordFilterChangedEventArgs e)
        {
            _changingFilter = true;
            foreach (ListViewItem item in _listControl.Items)
            {
                if (item.Tag == e._recordFilter)
                {
                    item.Selected = true;
                    break;
                }
            }
            _changingFilter = false;
        }

        private void _filterList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_listControl.SelectedItems != null && _listControl.SelectedItems.Count > 0 && !_changingFilter)
            {
                _model.ActiveRecordFilter = (RecordFilter)_listControl.SelectedItems[0].Tag;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
