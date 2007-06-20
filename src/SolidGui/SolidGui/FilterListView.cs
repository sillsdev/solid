using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SolidGui
{
    public partial class FilterListView : UserControl
    {
        private FilterListPresentationModel _filterListPM;

        public FilterListView()
        {
            InitializeComponent();
        }

        public FilterListPresentationModel Model
        {
            get
            {
                return _filterListPM;
            }
            set
            {
                _filterListPM = value;
            }
        }

        private void FilterListView_Load(object sender, EventArgs e)
        {
            _filterList.Clear();

            foreach (RecordFilter filter in Model.RecordFilters)
            {
                ListViewItem item = new ListViewItem();
                item.Tag = filter;
                item.Text = filter.Name;
                _filterList.Items.Add(item);
            }
            
        }

        private void _filterList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_filterList.SelectedItems != null && _filterList.SelectedItems.Count > 0)
            {
                _filterListPM.ActiveRecordFilter = (RecordFilter)_filterList.SelectedItems[0].Tag;
            }
        }
    }
}
