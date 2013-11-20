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

        // JMC:! Need a BindModel for these? (See MarkerSettingsDialog)
        private SolidMarkerSetting _currentMarkerSetting;
        public MarkerSettingsPM MarkerModel { get; set; }

        public MarkerSettingsView()
        {
            InitializeComponent();
            _store = AppWritingSystems.WritingSystems;
            _wsModel = new WritingSystemSetupModel(_store);
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
            if(_wsModel.HasCurrentSelection)
                _currentMarkerSetting.WritingSystemRfc4646 = _wsModel.CurrentRFC4646;
        }

        private void _cbUnicode_CheckedChanged(object sender, EventArgs e)
        {
            _currentMarkerSetting.Unicode = _cbUnicode.Checked;
        }

        private void _setupWsLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (var d = new WritingSystemSetupDialog(_wsModel))
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
    }
}