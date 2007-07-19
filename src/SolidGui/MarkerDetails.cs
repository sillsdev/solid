using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
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
                AddLinkSubItem(item, MakeStructureLinkLabel(_settings.FindMarkerSetting(pair.Key).StructureProperties), OnStructureLinkClicked);
                AddLinkSubItem(item, "??", OnWritingSystemLinkClicked );
                AddLinkSubItem(item, MakeMappingLinkLabel(SolidMarkerSetting.MappingType.Flex, _settings.FindMarkerSetting(pair.Key)), OnMappingLinkClicked);
              //  FillInStructureColumn(item, _settings.FindMarkerSetting(pair.Key).StructureProperties);
              //  FillInCheckedColumn(item, _dictionary.MarkerErrors[pair.Key]);
                
                _listView.Items.Add(item);
            }
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

        private string MakeMappingLinkLabel(SolidMarkerSetting.MappingType type, SolidMarkerSetting markerSetting)
        {
            string mapping = markerSetting.GetMapping(type);

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

        private void OnMappingLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ListViewItem item = (ListViewItem) ((LinkLabel)sender).Tag;
            item.Selected = true;
            OpenSettingsDialog("mapping");
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
            UpdateDisplay();
        }

        private void _listView_DoubleClick(object sender, EventArgs e)
        {
            OpenSettingsDialog(null);
        }
    }
}
