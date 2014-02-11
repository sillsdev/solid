// Copyright (c) 2007-2014 SIL International
// Licensed under the MIT license: opensource.org/licenses/MIT

using System;
using System.Windows.Forms;
using Palaso.UI.WindowsForms.WritingSystems;
using Palaso.WritingSystems;
using SolidGui.Engine;

namespace SolidGui.MarkerSettings
{
    public partial class MarkerSettingsView : UserControl
    {
        private readonly WritingSystemSetupModel _wsModel;
        private readonly IWritingSystemRepository _store;

        // Added the following reference because of a BetterLabel issue. Now trying to make sure the WS dialog is not disposed each time
        // So, need to dispose of these things prior to app shutdown? Yes, added a Cleanup() method and called it from the parent dialog's FormClose event -JMC Feb 2014
        // JMC: Would it be better to also make this class implement IDisposable?
        private readonly WritingSystemSetupDialog _wsDialog;

        // JMC:! Need a BindModel for these? (See MarkerSettingsDialog)
        private SolidMarkerSetting _currentMarkerSetting;
        public MarkerSettingsPM MarkerModel { get; set; }

        public MarkerSettingsView()
        {
            InitializeComponent();
            _store = AppWritingSystems.WritingSystems;
            _wsModel = new WritingSystemSetupModel(_store);
            _wsDialog = new WritingSystemSetupDialog(_wsModel);
            // _wsModel.SelectionChanged += new EventHandler(_wsModel_SelectionChanged);
            wsPickerUsingComboBox1.BindToModel(_wsModel);
            wsPickerUsingComboBox1.SelectedComboIndexChanged += wsPickerUsingComboBox1_SelectedComboIndexChanged;
        }

        void wsPickerUsingComboBox1_SelectedComboIndexChanged(object sender, EventArgs e)
        {
            //review: this is a  bit weird
            if(_wsModel.HasCurrentSelection)
                _currentMarkerSetting.WritingSystemRfc4646 = _wsModel.CurrentRFC4646;
            else
            {
                _currentMarkerSetting.WritingSystemRfc4646 = string.Empty;
            }
            //MarkerModel.WillNeedSave(); JMC:! Re-enable this in a way that doesn't always trigger
            
        }

        public void UpdateDisplay()
        {
            UpdateDisplay(null, string.Empty, SolidMarkerSetting.MappingType.Lift);
        }

        public void UpdateDisplay(string initialArea, string selectedMarker, SolidMarkerSetting.MappingType type)
        {
            _currentMarkerSetting = MarkerModel.GetMarkerSetting(selectedMarker); 

            _wsModel.SetCurrentIndexFromRfc46464(_currentMarkerSetting.WritingSystemRfc4646);
            
            _structurePropertiesView.Model.AllValidMarkers = MarkerModel.GetValidMarkers();
            _structurePropertiesView.Model.MarkerSetting = _currentMarkerSetting;
            _structurePropertiesView.UpdateDisplay();

            _mappingView.Model.MarkerSetting = _currentMarkerSetting;
            _mappingView.Model.Type = type;
            _mappingView.InitializeDisplay();

            _cbUnicode.Checked = _currentMarkerSetting.Unicode;

            SelectInitialArea(initialArea);
        }

        private void SelectInitialArea(string initialArea)
        {
            if(!string.IsNullOrEmpty(initialArea))
            {
                foreach (TabPage  page in _structureTabControl.TabPages )
                {
                    if(page.Name.Contains(initialArea))
                    {
                        _structureTabControl.SelectedTab = page;
                        break;
                    }
                }
            }
        }

        private void _markerListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_markerListBox.SelectedItem != null)
            {
                _structurePropertiesView.Model.MarkerSetting = (SolidMarkerSetting) _markerListBox.SelectedItem;
                _structurePropertiesView.UpdateDisplay();
                
                if ( (_markerListBox.Text) != MarkerModel.Root)
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
            _structurePropertiesView.Model = MarkerModel.StructurePropertiesModel;

            _mappingView.BindModel(MarkerModel.MappingModel);  // was: _mappingView.Model = MarkerModel.MappingModel;
        }

        private void _structureTabControl_Leave(object sender, EventArgs e)
        {
            if (_wsModel.HasCurrentSelection)
            {
                string a = _currentMarkerSetting.WritingSystemRfc4646;
                string b = _wsModel.CurrentRFC4646;
                if (a != b)
                {
                    _currentMarkerSetting.WritingSystemRfc4646 = b;
                    MarkerModel.WillNeedSave();
                }
                
            }
        }

        private void _cbUnicode_CheckedChanged(object sender, EventArgs e)
        {
            bool a = _currentMarkerSetting.Unicode;
            bool b = _cbUnicode.Checked;
            if (a != b)
            {
                _currentMarkerSetting.Unicode = b;
                MarkerModel.WillNeedSave();
            }
        }

        private void XXX_setupWsLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            using (var d = new WritingSystemSetupDialog(_wsModel))  // problem: the Palaso code doesn't like being disposed by this using (used to be ok?) -JMC Feb 2014
            {
                if (_wsModel.HasCurrentSelection)
                {
                    Cursor.Current = Cursors.WaitCursor;
                    d.ShowDialog(_wsModel.CurrentRFC4646);
                }
                else
                {
                    d.ShowDialog();
                }
            }
        }

        private void _setupWsLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var d = _wsDialog;

            if (_wsModel.HasCurrentSelection)
            {
                Cursor.Current = Cursors.WaitCursor;
                d.ShowDialog(_wsModel.CurrentRFC4646);
            }
            else
            {
                d.ShowDialog();
            } 
        }

        public void Cleanup()
        {
            _wsDialog.Dispose(); //not sure whether it would be
        }

    }
}