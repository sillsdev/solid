// Copyright (c) 2007-2014 SIL International
// Licensed under the MIT license: opensource.org/licenses/MIT

using System;
using System.Windows.Forms;
using SolidGui.Engine;
using SolidGui.MarkerSettings;


namespace SolidGui.MarkerSettings
{
    public partial class MarkerSettingsDialog : Form
    {
        private string _selectedMarker;
        private string _initialArea;
        private SolidMarkerSetting.MappingType _mappingType;

        public MarkerSettingsDialog(MarkerSettingsPM markerSettingsModel, string marker)
        {
            InitializeComponent();
            BindModel(markerSettingsModel);
            _selectedMarker = marker;
        }

        // JMC: No need to make this public and call it from MainWindowView? But what if we start reusing the dialog? (E.g. to easily remember our last tab or whatever)
        public void BindModel(MarkerSettingsPM markerSettingsModel)
        {
            _markerSettingsView.MarkerModel = markerSettingsModel;
            _mappingType = SolidMarkerSetting.MappingType.Lift;
            _initialArea = "structure";
            _selectedMarker = null;
        }


        public Button CloseButton
        {
            get { return _closeButton; }
        }

        //what group of settings to show, initially
        public string SelectedArea
        {
            set
            {
                _initialArea = value;
            }
        }

        public SolidMarkerSetting.MappingType MappingType
        {
            set { _mappingType = value; }
        }

        private void _closeButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void OnMarkerSettings_Load(object sender, EventArgs e)
        {
            Text = string.Format("{0} Settings", _selectedMarker);
            _markerSettingsView.UpdateDisplay(_initialArea, _selectedMarker, _mappingType);
        }
    }
}