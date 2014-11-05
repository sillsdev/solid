using System;
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

        private string _marker;  // TODO: unify this with _currentMarkerSetting -JMC

        private readonly WritingSystemSetupModel _wsModel;
        private readonly IWritingSystemRepository _store;

        // Added the following reference because of what seems to be new behavior in Palaso's BetterLabel. Now trying to make sure the WS dialog is not disposed each time.
        // So, need to dispose of these things prior to app shutdown? Yes, added a Cleanup() method and called it from the parent dialog's FormClose event -JMC Feb 2014
        // TODO: Would it be better to put it in Dispose()? Probably, but this seems to happen too late to make Palaso lib happy. -JMC
        private readonly WritingSystemSetupDialog _wsDialog;

        private bool _isProcessing=false;  // added to avoid triggering "Changed" events during initial loading. -JMC Feb 2014

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
            _isProcessing = true;
            InitializeComponent();
            if (DesignMode) return; // Without this, the Palaso code crashes Visual Studio's Designer because it now demands explicit Dispose() -JMC Feb 2014

            _store = AppWritingSystems.WritingSystems;
            _wsModel = new WritingSystemSetupModel(_store);
            _wsDialog = new WritingSystemSetupDialog(_wsModel); //-JMC
            _wsDialog.ShowIcon = false;

            InitDisplay(markerSettingsPm, marker, area);
            _isProcessing = false;
        }

        private void InitDisplay(MarkerSettingsPM markerSettingsPm, string marker, string area)
        {
            SetArea(area);

            MarkerModel = markerSettingsPm;

            FillListBox(markerSettingsPm);

            //_mappingView.BindModel(markerSettingsPm.MappingModel);

            SetMarker(marker);

            _wsModel.SelectionChanged -= _uiEditMade;
            _wsModel.SelectionChanged += _uiEditMade;
            _wsPalasoPicker.BindToModel(_wsModel);
            _wsPalasoPicker.SelectedComboIndexChanged -= WsPalasoPickerSelectedComboIndexChanged;
            _wsPalasoPicker.SelectedComboIndexChanged += WsPalasoPickerSelectedComboIndexChanged;

            //_structurePropertiesView.Model.AllValidMarkers = MarkerModel.GetAllMarkers();
            _structurePropertiesView.Model.MarkerSetting = _currentMarkerSetting;
            //_structurePropertiesView.UpdateDisplay();

            //_mappingView.Model.MarkerSetting = _currentMarkerSetting; 
            //_mappingView.Model.Type = type;
            //_mappingView.InitializeDisplay();

            //SelectInitialArea(initialArea);


            UpdateDisplay();
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

            _cbUnicode.Checked = _currentMarkerSetting.Unicode;
            
            bool success = _wsModel.SetCurrentIndexFromRfc46464(_currentMarkerSetting.WritingSystemRfc4646); // can silently fail, but that's ok
            if (!success) _wsModel.ClearSelection();

            //setMarker for Mapping tab
            _mappingView.BindModel(MarkerModel.MappingModel, _currentMarkerSetting);

            //setMarker for Structure tab
            _structurePropertiesView.Model = MarkerModel.StructurePropertiesModel;
            if (_structurePropertiesView.Model != null)
            {
                _structurePropertiesView.Model.MarkerSetting = _currentMarkerSetting;
            }

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

        /*
        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            // SetArea(_tabControl.SelectedTab.Name);
            // UpdateDisplay();
        }
         */

        public void UpdateDisplay()
        {
            _isProcessing = true;

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
            
            // update own fields (WS and Unicode)
            

            // update nested controls (regardless of which one is showing--slower but simplifies life)
            _mappingView.UpdateDisplay();

            _structurePropertiesView.UpdateDisplay();

            _isProcessing = false;
        }

        private void _markersListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            OnSelectedMarkerChanged();
        }

        private void OnSelectedMarkerChanged()
        {
            if (DesignMode || _isProcessing || _markersListBox.SelectedItem == null) return;

            string m = (string)_markersListBox.SelectedItem;
            SetMarker(m);
            _mappingView.UpdateDisplay();
            _structurePropertiesView.UpdateDisplay();
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

        

        void WsPalasoPickerSelectedComboIndexChanged(object sender, EventArgs e)
        {
            if (DesignMode || _isProcessing) return;
            UpdateModel();
        }

        /// <summary>
        /// Updates the pieces of the model that aren't handled by embedded controls. That is,
        /// it updates WS and encoding. (StructurePropertiesView and MappingView handle their own data.)
        /// </summary>
        public void UpdateModel()
        {

            // update WS model
            if (_wsModel.HasCurrentSelection)
            {
                string a = _currentMarkerSetting.WritingSystemRfc4646;
                string b = _wsModel.CurrentRFC4646;
                if (a != b && String.IsNullOrEmpty(b))   // The check for empty 'fixes' #1309. -JMC Nov 2014
                {
                    _currentMarkerSetting.WritingSystemRfc4646 = b;
                    SomeSettingChanged();
                }
            }
            // else do nothing; we don't want to blow away temporary non-WSes: "vern" and "nat" and "reg"

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
            if (DesignMode || _isProcessing) return;
            UpdateModel();
        }

        private void _uiEditMade(object sender, EventArgs e)
        {
            if (DesignMode || _isProcessing) return;
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

        public void Cleanup()  //TODO: I would think this really belongs in this.Dispose(), but Palaso wants its stuff disposed sooner. -JMC
        {
            _wsDialog.Dispose();
        }

        private event EventHandler SettingChanged;

        /// <summary>
        /// Subscribes the listener to detect changes in this dialog and its nested components
        /// </summary>
        public void Listen(EventHandler listener)
        {
            SettingChanged -= listener;
            SettingChanged += listener;
            _structurePropertiesView.Model.StructureSettingChanged -= listener;
            _structurePropertiesView.Model.StructureSettingChanged += listener;
            _mappingView.Model.MappingSettingChanged -= listener;
            _mappingView.Model.MappingSettingChanged += listener;
        }

        private void MarkerSettingsDialog_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            Cleanup();
        }

        private void MarkerSettingsDialog_Deactivate(object sender, System.EventArgs e)
        {
            _structurePropertiesView.CommentTextBoxMaybeChanged(sender, e);
        }


    }
}
