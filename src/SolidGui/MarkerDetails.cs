using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using EXControls;
using SolidEngine;

namespace SolidGui
{
    public partial class MarkerDetails : UserControl
    {
        private Dictionary _dictionary;
        private SolidSettings _settings;
        private MarkerSettingsPM _markerSettingsPM;
        private FilterChooserPM _filterChooserPM;
        private MarkerFilter _filter = null;
        public event EventHandler MarkerSettingPossiblyChanged;

        //public event EventHandler<FilterChooserPM.RecordFilterChangedEventArgs> RecordFilterChanged;


        public MarkerDetails()
        {
            InitializeComponent();
        }

        public void BindModel(MarkerSettingsPM markerSettingsPM, FilterChooserPM filterChooserPM, Dictionary dictionary, SolidSettings settings)
        {
            _listView.SuspendLayout();
            _markerSettingsPM = markerSettingsPM;
            _filterChooserPM = filterChooserPM;
            _settings = settings;
            _dictionary = dictionary;
            //UpdateDisplay();
        }
        
        public void UpdateDisplay()
        {
            string previouslySelectedMarker = string.Empty;
            
            if(_listView.SelectedItems.Count > 0)
            {
                previouslySelectedMarker = _listView.SelectedItems[0].Text;
            }

            _listView.Items.Clear();
            //_listView.MySortBrush  = null;
            _listView.MySortBrush = Brushes.Coral;
            _listView.MyHighlightBrush = System.Drawing.SystemBrushes.Highlight;

            ImageList colimglst = new ImageList();
           // colimglst.Images.Add("down", Image.FromFile("down.png"));
           // colimglst.Images.Add("up", Image.FromFile("up.png"));
           // colimglst.ColorDepth = ColorDepth.Depth32Bit;
            colimglst.ImageSize = new Size(20, 20); // this will affect the row height
            _listView.SmallImageList = colimglst;

            foreach (KeyValuePair<string, int> pair in _dictionary.MarkerFrequencies)
            {
                EXControls.EXListViewItem item = new EXListViewItem(pair.Key);
                
                //The order these are called in matters
                FillInFrequencyColumn(item, pair.Value.ToString());
                SolidMarkerSetting markerSetting = _settings.FindMarkerSetting(pair.Key);
                AddLinkSubItem(item, MakeStructureLinkLabel(markerSetting.StructureProperties), OnStructureLinkClicked);
                AddLinkSubItem(item, MakeWritingSystemLinkLabel(markerSetting.WritingSystem), OnWritingSystemLinkClicked);
                AddLinkSubItem(item, MakeMappingLinkLabel(SolidMarkerSetting.MappingType.Flex, markerSetting), OnFlexMappingLinkClicked);
                AddLinkSubItem(item, MakeMappingLinkLabel(SolidMarkerSetting.MappingType.Lift, markerSetting), OnLiftMappingLinkClicked);              
              //  FillInStructureColumn(item, _settings.FindMarkerSetting(pair.Key).StructureProperties);
              //  FillInCheckedColumn(item, _dictionary.MarkerErrors[pair.Key]);
                
                _listView.Items.Add(item);
            }

            SelectMarker(previouslySelectedMarker);
        }


        private void FillInCheckedColumn(ListViewItem item, int errorCount)
        {
            if(errorCount > 0)
            {
                item.SubItems.Add(string.Format("Error ({0})", errorCount));
            }
            else
            {
                item.SubItems.Add("Good");
            }
        }

        private void FillInFrequencyColumn(ListViewItem item, string  frequency)
        {
            EXControlListViewSubItem x = new EXControlListViewSubItem();
            x.Text = frequency;
            item.SubItems.Add(x);
        }

        private string MakeWritingSystemLinkLabel(string writingSystemId)
        {
            if (string.IsNullOrEmpty(writingSystemId))
            {
                return "??";
            }
            
            Palaso.WritingSystems.LdmlInFolderWritingSystemRepository repository =
                    new Palaso.WritingSystems.LdmlInFolderWritingSystemRepository();

            Palaso.WritingSystems.WritingSystemDefinition definition = repository.LoadDefinition(writingSystemId);

            return (definition != null) ? definition.DisplayLabel : "??";
        }

        private string MakeMappingLinkLabel(SolidMarkerSetting.MappingType type, SolidMarkerSetting markerSetting)
        {
            string conceptId = markerSetting.GetMappingConceptId(type);
            MappingPM.MappingSystem mappingSystem = _markerSettingsPM.MappingModel.TargetChoices[(int)type];
            
            MappingPM.Concept concept = mappingSystem.GetConceptById(conceptId);

            string mapping = (concept != null) ? concept.ToString() : null;
            
            return mapping ?? "??";
        }

        private string MakeStructureLinkLabel(IList<SolidStructureProperty> properties)
        {
            string parents = "";
            foreach (SolidStructureProperty property in properties)
            {
                if (!string.IsNullOrEmpty(property.Parent))
                {
                    if (parents!= "")
                    {
                        parents += ", ";
                    }
                    parents += property.Parent;
                }
            }
            if (parents == "")
            {
                parents = "???";
            }
            return parents;
        }

        private void AddLinkSubItem(EXListViewItem item, string text, LinkLabelLinkClickedEventHandler clickHandler)
        {
            EXControlListViewSubItem subItem = new EXControlListViewSubItem();
            LinkLabel label = new LinkLabel();
            label.Text = text;
            if (text == "???")
            {
                label.LinkColor = System.Drawing.Color.Red;
            }

            label.Tag = item;
            label.LinkClicked += clickHandler;
            _listView.AddControlToSubItem(label, subItem);

            subItem.Tag = label;
            item.SubItems.Add(subItem);
        }

        private void OnStructureLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ListViewItem item = (ListViewItem) ((LinkLabel)sender).Tag;
            item.Selected = true;
            OpenSettingsDialog("structure");
        }

        private void OnWritingSystemLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ListViewItem item = (ListViewItem) ((LinkLabel)sender).Tag;
            item.Selected = true;
            OpenSettingsDialog("writingSystem");
        }

        private void OnFlexMappingLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ListViewItem item = (ListViewItem) ((LinkLabel)sender).Tag;
            item.Selected = true;
            OpenSettingsDialog("mapping", SolidMarkerSetting.MappingType.Flex);
        }

        private void OnLiftMappingLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ListViewItem item = (ListViewItem)((LinkLabel)sender).Tag;
            item.Selected = true;
            OpenSettingsDialog("mapping", SolidMarkerSetting.MappingType.Lift);
        }

        private void _listView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(_listView.SelectedItems.Count == 0)
            {
                return;
            }  
            string marker = _listView.SelectedItems[0].Text;

            _filter = new MarkerFilter(_dictionary, marker);
            _filterChooserPM.ActiveRecordFilter = _filter;
        }

        // When someone changes the filter in the PM
        public void OnFilterChanged(object sender, FilterChooserPM.RecordFilterChangedEventArgs e)
        {
            //_changingFilter = true;
            if (_filter != e._recordFilter)
            {
                // Remove the selection
                for (int i = 0; i < _listView.Items.Count; i++)
                {
                    _listView.Items[i].Selected = false;
                }
            }
            //_changingFilter = false;
        }

        private void OnEditSettingsClick(object sender, EventArgs e)
        {
            OpenSettingsDialog(null);
        }

        private void UpdateSelectedItems(SolidMarkerSetting setting)
        {
            ((LinkLabel)_listView.SelectedItems[0].SubItems[2].Tag).Text = MakeStructureLinkLabel(setting.StructureProperties);
            ((LinkLabel)_listView.SelectedItems[0].SubItems[3].Tag).Text = MakeWritingSystemLinkLabel(setting.WritingSystem);
            ((LinkLabel)_listView.SelectedItems[0].SubItems[4].Tag).Text = MakeMappingLinkLabel(SolidMarkerSetting.MappingType.Flex, setting);
            ((LinkLabel)_listView.SelectedItems[0].SubItems[5].Tag).Text = MakeMappingLinkLabel(SolidMarkerSetting.MappingType.Lift, setting);
        }

        private void OpenSettingsDialog(string area)
        {
            if(_listView.SelectedItems.Count == 0)
            {
                return;
            }          
            string marker = _listView.SelectedItems[0].Text;
            MarkerSettingsDialog dialog = new MarkerSettingsDialog(_markerSettingsPM, marker);
            dialog.SelectedArea = area;
            dialog.ShowDialog();
            if (MarkerSettingPossiblyChanged != null)
                MarkerSettingPossiblyChanged.Invoke(this, new EventArgs());
            UpdateSelectedItems(_markerSettingsPM.GetMarkerSetting(marker));
        }

        private void OpenSettingsDialog(string area, SolidMarkerSetting.MappingType mappingType)
        {
            if (_listView.SelectedItems.Count == 0)
            {
                return;
            }
            string marker = _listView.SelectedItems[0].Text;
            MarkerSettingsDialog dialog = new MarkerSettingsDialog(_markerSettingsPM, marker);
            dialog.SelectedArea = area;
            dialog.MappingType = mappingType;
            dialog.ShowDialog();
            if (MarkerSettingPossiblyChanged != null)
                MarkerSettingPossiblyChanged.Invoke(this, new EventArgs());
            UpdateSelectedItems(_markerSettingsPM.GetMarkerSetting(marker));
        }

        private void _listView_DoubleClick(object sender, EventArgs e)
        {
            OpenSettingsDialog(null);
        }

        public void SelectMarker(string marker)
        {
            foreach(ListViewItem a in _listView.Items)
            {
                if(a.Text == marker)
                {
                    a.Selected = true;
                }
                else
                {
                    a.Selected = false;
                }
            }
        }
    }
}
