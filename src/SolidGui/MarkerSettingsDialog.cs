using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SolidGui
{
    public partial class MarkerSettingsDialog : Form
    {
        private string _selectedMarker;
        private string _initialArea;

        public MarkerSettingsDialog(MarkerSettingsPM markerSettingsModel, string marker)
        {
            InitializeComponent();
            _markerSettingsView.Model = markerSettingsModel;
            _selectedMarker = marker;
        }

        //what group of settings to show, initially
        public string SelectedArea
        {
            set
            {
                _initialArea = value;
            }
        }

        private void _closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void OnMarkerSettings_Load(object sender, EventArgs e)
        {
            _markerSettingsView.UpdateDisplay(_initialArea, _selectedMarker);
        }
    }
}