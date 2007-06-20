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

        private void _filterList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_listControl.SelectedItems != null && _listControl.SelectedItems.Count > 0)
            {
                _model.ActiveRecordFilter = (RecordFilter)_listControl.SelectedItems[0].Tag;
            }
        }
    }
}
