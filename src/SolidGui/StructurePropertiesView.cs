using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using SolidConsole;
using System.Windows.Forms;

namespace SolidGui
{
    public partial class StructurePropertiesView : UserControl
    {
        private StructurePropertiesPM _model;

        public StructurePropertiesPM Model
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

        public StructurePropertiesView()
        {
            InitializeComponent();
        }

        public void UpdateDisplay()
        {
            foreach (SolidStructureProperty property in _model.StructureProperties)
            {
                ListViewItem item = new ListViewItem(property.Parent);
                item.Tag = property;
                _parentListView.Items.Add(item);
                _InferComboBox.Items.Add(property.Parent);
            }
        }

        private void _parentListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(_parentListView.SelectedItems.Count > 0)
            {
                SolidStructureProperty property = (SolidStructureProperty)_parentListView.SelectedItems[0].Tag;
                //case()

                
            }
        }
    }
}
