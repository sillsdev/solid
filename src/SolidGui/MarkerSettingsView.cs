using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SolidGui
{
    public partial class MarkerSettingsView : UserControl
    {

        private MarkerSettingsPM _model;

        public MarkerSettingsView()
        {
            InitializeComponent();
            _structurePropertiesView.Model = new StructurePropertiesPM();
        }

        public MarkerSettingsPM Model
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

        public void UpdateDisplay()
        {
            _markersListView.Clear();

            foreach (string marker in _model.AllMarkers)
            {
                ListViewItem item = new ListViewItem(marker);
                item.Tag = _model.GetMarkerSetting(marker);
                _markersListView.Items.Add(item);
            }
        }

        private void _markersListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_markersListView.SelectedItems.Count > 0)
            {
                _structurePropertiesView.Model.MarkerSetting = (SolidEngine.SolidMarkerSetting) _markersListView.SelectedItems[0].Tag;
                _structurePropertiesView.UpdateDisplay();
            }
        }
    }
}
