using System;
using System.Windows.Forms;
using SolidEngine;

namespace SolidGui
{
    public partial class MarkerSettingsView : UserControl
    {
        private SolidMarkerSetting _currentMarkerSetting;
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

        public void UpdateDisplay(string initialArea, string selectedMarker, SolidMarkerSetting.MappingType type)
        {
            
            foreach (string marker in _model.GetValidMarkers())
            {
                _markerListBox.Items.Add(_model.GetMarkerSetting(marker));
                if (marker == selectedMarker)
                {
                    _markerListBox.SelectedIndex = _markerListBox.Items.Count - 1;
                    _currentMarkerSetting = (SolidMarkerSetting)_markerListBox.SelectedItem;
                }
            }
            _writingSystemPicker.IdentifierOfSelectedWritingSystem = _currentMarkerSetting.WritingSystem;
            
            _structurePropertiesView.Model.AllValidMarkers = Model.GetValidMarkers();
            _structurePropertiesView.Model.MarkerSetting = _currentMarkerSetting;
            _structurePropertiesView.UpdateDisplay();

            _mappingView.Model.MarkerSetting = _currentMarkerSetting;
            _mappingView.Model.Type = type;
            _mappingView.InitializeDisplay();

            SelectInitialArea(initialArea);

        }

        private void SelectInitialArea(string initialArea)
        {
            if(!string.IsNullOrEmpty(initialArea))
            {
                foreach (TabPage  page in this._structureTabControl.TabPages )
                {
                    if(page.Name.Contains(initialArea))
                    {
                        _structureTabControl.SelectedTab = page;
                        break;
                    }
                }
            }
        }

        public void UpdateDisplay()
        {
            UpdateDisplay(null, string.Empty, SolidMarkerSetting.MappingType.Flex);
        }

        private void _markerListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_markerListBox.SelectedItem != null)
            {
                _structurePropertiesView.Model.MarkerSetting = (SolidMarkerSetting) _markerListBox.SelectedItem;
                _structurePropertiesView.UpdateDisplay();
                
                if ( (_markerListBox.Text) != Model.Root)
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

        private void _structureTabControl_Leave(object sender, EventArgs e)
        {
            _currentMarkerSetting.WritingSystem = _writingSystemPicker.IdentifierOfSelectedWritingSystem;
        }
    }
}
