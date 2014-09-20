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

        // Added the following reference because of what seems to be new behavior in Palaso's BetterLabel. Now trying to make sure the WS dialog is not disposed each time.
        // So, need to dispose of these things prior to app shutdown? Yes, added a Cleanup() method and called it from the parent dialog's FormClose event -JMC Feb 2014
        // JMC: Would it be better to put it in Dispose()? Probably.
        private readonly WritingSystemSetupDialog _wsDialog;

        private bool _initializing;  // added to avoid triggering "Changed" events during initial loading. -JMC Feb 2014

        private SolidMarkerSetting _currentMarkerSetting;
        public MarkerSettingsPM MarkerModel { get; set; }

        public MarkerSettingsView()
        {
            _initializing = true;
            InitializeComponent();
            if (DesignMode) // Without this, the Palaso code crashes Visual Studio's Designer because it now demands explicit Dispose() -JMC Feb 2014
            {
                return;
            }
            _store = AppWritingSystems.WritingSystems;
            _wsModel = new WritingSystemSetupModel(_store);
            _wsDialog = new WritingSystemSetupDialog(_wsModel); //-JMC
            // _wsModel.SelectionChanged += new EventHandler(_wsModel_SelectionChanged);
            wsPickerUsingComboBox1.BindToModel(_wsModel);
            wsPickerUsingComboBox1.SelectedComboIndexChanged += wsPickerUsingComboBox1_SelectedComboIndexChanged;
        }

        void wsPickerUsingComboBox1_SelectedComboIndexChanged(object sender, EventArgs e)
        {
            UpdateModel();
        }

        public void UpdateModel()
        {
            if (_initializing) return;

            if (!_wsModel.HasCurrentSelection)
            {
                _currentMarkerSetting.WritingSystemRfc4646 = string.Empty;
                //MarkerModel.WillNeedSave(); JMC: unnecessary?
            }
            else
            {
                string a = _currentMarkerSetting.WritingSystemRfc4646;
                string b = _wsModel.CurrentRFC4646;
                if (a != b)
                {
                    _currentMarkerSetting.WritingSystemRfc4646 = b;
                    MarkerModel.WillNeedSave();
                }
            }

            bool aa = _currentMarkerSetting.Unicode;
            bool bb = _cbUnicode.Checked;
            if (aa != bb)
            {
                _currentMarkerSetting.Unicode = bb;
                MarkerModel.WillNeedSave();
            }
            
        }

/*        public void UpdateDisplay()
        {
            UpdateDisplay(null, string.Empty, SolidMarkerSetting.MappingType.Lift);
        } */

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
            _initializing = false;
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

        //JMC: Ok to delete this, and the ListBox?
        private void _markerListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            return; //verifying... -JMC
/*
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
 */
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
            UpdateModel();
        }

        private void _cbUnicode_CheckedChanged(object sender, EventArgs e)
        {
            UpdateModel();
        }

/*	
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
*/
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
        public void Cleanup()  //JMC: I would think this really belongs in this.Dispose(), but Palaso wants its stuff disposed sooner. 
        {
            _wsDialog.Dispose();
        }

    }
}