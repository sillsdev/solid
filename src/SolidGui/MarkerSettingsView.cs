using System;
using System.Windows.Forms;
using SolidEngine;

namespace SolidGui
{
    public partial class MarkerSettingsView : UserControl
    {

        private MarkerSettingsPM _model;

        public MarkerSettingsView()
        {
            InitializeComponent();
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
            _markerListBox.Items.Clear();

            foreach (string marker in _model.AllMarkers)
            {
                _markerListBox.Items.Add(_model.GetMarkerSetting(marker));
            }
        }

        private void _markerListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_markerListBox.SelectedItem != null)
            {
                if ( ("\\_" + _markerListBox.Text) != Model.Root)
                {
                    _structurePropertiesView.Model.MarkerSetting = (SolidMarkerSetting) _markerListBox.SelectedItem;
                    _structurePropertiesView.UpdateDisplay();
                    _structurePropertiesView.Enabled = true;
                }
                else
                {
                    _structurePropertiesView.Enabled = false;
                }
            }
        }

        private void MarkerSettingsView_Load(object sender, EventArgs e)
        {
          
            if(this.DesignMode)
            {
                return;
            }
            _structurePropertiesView.Model = Model.StructurePropertiesModel;
        }
    }
}
