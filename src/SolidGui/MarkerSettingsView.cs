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

        public void UpdateDisplay(string selectedMarker)
        {
            SolidMarkerSetting currentSettings = new SolidMarkerSetting();
            _markerListBox.Items.Clear();

            foreach (string marker in _model.GetValidMarkers())
            {

                _markerListBox.Items.Add(_model.GetMarkerSetting(marker));
                if (marker == selectedMarker)
                {
                    _markerListBox.SelectedIndex = _markerListBox.Items.Count - 1;
                    currentSettings = (SolidMarkerSetting)_markerListBox.SelectedItem;
                }
            }
            _structurePropertiesView.Model.AllValidMarkers = Model.GetValidMarkers();
            _structurePropertiesView.Model.MarkerSetting = currentSettings;
            _structurePropertiesView.UpdateDisplay();
        }

        public void UpdateDisplay()
        {
            UpdateDisplay(string.Empty);
        }

        private void _markerListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_markerListBox.SelectedItem != null)
            {
                _structurePropertiesView.Model.MarkerSetting = (SolidMarkerSetting) _markerListBox.SelectedItem;
                _structurePropertiesView.UpdateDisplay();
                
                if ( ("\\_" + _markerListBox.Text) != Model.Root)
                {
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
          
            if(DesignMode)
            {
                return;
            }
            _structurePropertiesView.Model = Model.StructurePropertiesModel;
            _mappingView.Model = Model.MappingModel;
        }
    }
}
