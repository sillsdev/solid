using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
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
            _markerSettingsView.Model = markerSettingsModel;
            _selectedMarker = marker;
            _mappingType = SolidMarkerSetting.MappingType.Flex;
            _initialArea = "structure";
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
            this.Close();
        }

        private void OnMarkerSettings_Load(object sender, EventArgs e)
        {
            this.Text = string.Format("{0} Settings", _selectedMarker);
            _markerSettingsView.UpdateDisplay(_initialArea, _selectedMarker, _mappingType);
        }
    }
}