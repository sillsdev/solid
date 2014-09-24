﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Palaso.UI.WindowsForms.WritingSystems.WSIdentifiers;
using System;
using System.Windows.Forms;
using Palaso.UI.WindowsForms.WritingSystems;
using Palaso.WritingSystems;
using SolidGui.Engine;

namespace SolidGui.MarkerSettings
{
    public partial class MarkerSettingsDialog : Form
    {
        public const string firstTab = "structureTabPage";
        public const string mappingTab = "mappingTabPage";

        private string _marker;  // todo: unify this with _currentMarkerSetting
        private string _area;

        private readonly WritingSystemSetupModel _wsModel;
        private readonly IWritingSystemRepository _store;

        // Added the following reference because of what seems to be new behavior in Palaso's BetterLabel. Now trying to make sure the WS dialog is not disposed each time.
        // So, need to dispose of these things prior to app shutdown? Yes, added a Cleanup() method and called it from the parent dialog's FormClose event -JMC Feb 2014
        // JMC: Would it be better to put it in Dispose()? Probably.
        private readonly WritingSystemSetupDialog _wsDialog;

        private bool _initializing;  // added to avoid triggering "Changed" events during initial loading. -JMC Feb 2014

        private SolidMarkerSetting _currentMarkerSetting;
        private MarkerSettingsPM MarkerModel { get; set; }
        private SolidMarkerSetting.MappingType _mappingType;


        /*
        public MarkerSettingsDialog()
        {
            InitializeComponent();
        }
        */

        public MarkerSettingsDialog(MarkerSettingsPM markerSettingsPm, string marker, string area)
        {
            _initializing = true;
            InitializeComponent();
            if (DesignMode) return; // Without this, the Palaso code crashes Visual Studio's Designer because it now demands explicit Dispose() -JMC Feb 2014

            _store = AppWritingSystems.WritingSystems;
            _wsModel = new WritingSystemSetupModel(_store);
            _wsDialog = new WritingSystemSetupDialog(_wsModel); //-JMC

            InitDisplay(markerSettingsPm, marker, area);
            _initializing = false;
        }

        private void InitDisplay(MarkerSettingsPM markerSettingsPm, string marker, string area)
        {
            SetArea(area);

            MarkerModel = markerSettingsPm;

            FillListBox(markerSettingsPm);

            //_mappingView.BindModel(markerSettingsPm.MappingModel);

            SetMarker(marker);

            _wsModel.SelectionChanged += _uiEditMade;
            wsPickerUsingComboBox1.BindToModel(_wsModel);
            wsPickerUsingComboBox1.SelectedComboIndexChanged += wsPickerUsingComboBox1_SelectedComboIndexChanged;
            
            _wsModel.SetCurrentIndexFromRfc46464(_currentMarkerSetting.WritingSystemRfc4646);

            _structurePropertiesView.Model.AllValidMarkers = MarkerModel.GetValidMarkers();
            _structurePropertiesView.Model.MarkerSetting = _currentMarkerSetting;
            _structurePropertiesView.UpdateDisplay();

            _mappingView.Model.MarkerSetting = _currentMarkerSetting;
            //_mappingView.Model.Type = type;
            //_mappingView.InitializeDisplay();

            _cbUnicode.Checked = _currentMarkerSetting.Unicode;

            //SelectInitialArea(initialArea);


            foreach (var ms in MarkerModel.MarkersInDictionary)
            {
                _markersListBox.Items.Add(ms);
            }

            UpdateDisplay();  //TODO: Fix update display to set marker title and active marker and both tabs 
        }


        private void FillListBox(MarkerSettingsPM markerSettingsPm)
        {
            _markersListBox.Items.Clear();
            var tmp = markerSettingsPm.MarkersInDictionary.ToList();  //make a copy, for thread safety in case a future version of this dialog is non-modal (http://projects.palaso.org/issues/1279)
            tmp.Sort(); // and for easy sorting
            foreach(var ms in tmp)
            {
                _markersListBox.Items.Add(ms);
            }
        }

        public void SetMarker(string marker)
        {
            _marker = marker;
            _currentMarkerSetting = MarkerModel.GetMarkerSetting(marker);
            var ms = MarkerModel.GetMarkerSetting(marker);

            //setMarker for Mapping tab
            _mappingView.BindModel(MarkerModel.MappingModel);
            MarkerModel.MappingModel.MarkerSetting = ms;

            //setMarker for Structure tab
            _structurePropertiesView.Model = MarkerModel.StructurePropertiesModel;
            if (_structurePropertiesView.Model != null)
            {
                _structurePropertiesView.Model.MarkerSetting = ms;
            }


            //TODO: and for Values tab too
        }

        public void SetArea(string area)
        {
            if (string.IsNullOrEmpty(area))
            {
                _tabControl.SelectedTab = _tabControl.TabPages[0];  //default to first tab
                return;
            }
            foreach (TabPage page in _tabControl.TabPages)
            {
                if (page.Name.Contains(area))
                {
                    _tabControl.SelectedTab = page;
                    break;
                }
            }           
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetArea(_tabControl.SelectedTab.Name);
            UpdateDisplay();
        }

        public void UpdateDisplay()
        {
            // set selected item in marker list
            int i = _markersListBox.FindStringExact(_marker);
            if (i >= 0)
            {
                _markersListBox.SetSelected(i, true);
            }
            else
            {
                _markersListBox.SetSelected(0, true);
            }
            
            // update nested controls
            _mappingView.UpdateDisplay();
            _structurePropertiesView.UpdateDisplay();
        }

        private void _markersListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DesignMode || _initializing || _markersListBox.SelectedItem == null) return;

            string m = (string)_markersListBox.SelectedItem;
            SetMarker(m);
            _mappingView.UpdateDisplay();
            _structurePropertiesView.UpdateDisplay();

            if ((_markersListBox.Text) != MarkerModel.Root)
            {
                _structurePropertiesView.setLxEnabled(true);
            }
            else
            {
                _structurePropertiesView.setLxEnabled(false);
            }

        }

        private void SomeSettingChanged()
        {
            MarkerModel.WillNeedSave();
            if (SettingChanged != null) SettingChanged.Invoke(this, EventArgs.Empty);
        }

        private void _closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }




        void wsPickerUsingComboBox1_SelectedComboIndexChanged(object sender, EventArgs e)
        {
            if (DesignMode || _initializing) return;
            UpdateModel();
        }

        public void UpdateModel()
        {
            if (_initializing) return;

            // update WS model
            if (!_wsModel.HasCurrentSelection)
            {
                _currentMarkerSetting.WritingSystemRfc4646 = string.Empty;  //todo: add a SomethingChanged() here?
            }
            else
            {
                string a = _currentMarkerSetting.WritingSystemRfc4646;
                string b = _wsModel.CurrentRFC4646;
                if (a != b)
                {
                    _currentMarkerSetting.WritingSystemRfc4646 = b;
                    SomeSettingChanged();
                }
            }

            bool aa = _currentMarkerSetting.Unicode;
            bool bb = _cbUnicode.Checked;
            if (aa != bb)
            {
                _currentMarkerSetting.Unicode = bb;
                SomeSettingChanged();
            }
            
        }

        private void _structureTabControl_Leave(object sender, EventArgs e)
        {
            if (DesignMode || _initializing) return;
            UpdateModel();
        }

        private void _uiEditMade(object sender, EventArgs e)
        {
            if (DesignMode || _initializing) return;
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

        private event EventHandler SettingChanged;

        /// <summary>
        /// Subscribes the listener to detect changes in this dialog and its nested components
        /// </summary>
        public void Listen(EventHandler listener)
        {
            SettingChanged += listener;
            _structurePropertiesView.Model.SettingChanged += listener;
            _mappingView.Model.SettingChanged += listener;
        }

        private void MarkerSettingsDialog_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            Cleanup();
        }

        private void MarkerSettingsDialog_Deactivate(object sender, System.EventArgs e)
        {
            _structurePropertiesView.CommentTextBoxNeedsCheck(sender, e);
        }


    }
}
