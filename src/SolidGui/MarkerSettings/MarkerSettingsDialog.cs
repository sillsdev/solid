using System;
using System.Windows.Forms;
using SolidEngine;

namespace SolidGui
{
    public partial class MarkerSettingsDialog : Form
    {
        private string _selectedMarker;
        private string _initialArea;
        private SolidMarkerSetting.MappingType _mappingType;

        public MarkerSettingsDialog(MarkerSettingsPM markerSettingsModel, string marker)
        {
            InitializeComponent();
            _markerSettingsView.MarkerModel = markerSettingsModel;
            _selectedMarker = marker;
            _mappingType = SolidMarkerSetting.MappingType.Flex;
            _initialArea = "structure";
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