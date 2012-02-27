using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using GlacialComponents.Controls;
using Palaso.Reporting;
using Palaso.WritingSystems;
using SolidGui.Engine;
using SolidGui.Filter;
using SolidGui.Model;


namespace SolidGui.MarkerSettings
{
    public partial class MarkerDetails : UserControl
    {
        private SfmDictionary _dictionary;
        private SolidSettings _settings;
        private MarkerSettingsPM _markerSettingsPM;
        private FilterChooserPM _filterChooserPM;
        private MarkerFilter _filter;
        public event EventHandler MarkerSettingPossiblyChanged;

        //public event EventHandler<FilterChooserPM.RecordFilterChangedEventArgs> RecordFilterChanged;


        public MarkerDetails()
        {
            InitializeComponent();

            var frequencyCount = (from GLColumn c in _listView.Columns where c.Text == "Count" select c).First();
            frequencyCount.ComparisonFunction = (a, b) => int.Parse(a).CompareTo(int.Parse(b));
            _listView.GridLineStyle = GLGridLineStyles.gridNone;
            _listView.SelectionColor = Color.LightYellow;
            _listView.SelectedTextColor = Color.Black;
        }

        public void BindModel(MarkerSettingsPM markerSettingsPM, FilterChooserPM filterChooserPM, SfmDictionary dictionary, SolidSettings settings)
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
            // _listView.MySortBrush = Brushes.Coral;
            // _listView.MyHighlightBrush = System.Drawing.SystemBrushes.Highlight;

//            ImageList colimglst = new ImageList();
//            colimglst.ImageSize = new Size(20, 20); // this will affect the row height
//            _listView.SmallImageList = colimglst;

            foreach (KeyValuePair<string, int> pair in _dictionary.MarkerFrequencies)
            {
                var item = new GLItem();// (pair.Key);
                item.SubItems.Add(pair.Key);
                
                //The order these are called in matters
                FillInFrequencyColumn(item, pair.Value.ToString());
                SolidMarkerSetting markerSetting = _settings.FindOrCreateMarkerSetting(pair.Key);
                AddLinkSubItem(item, MakeStructureLinkLabel(markerSetting.StructureProperties), OnStructureLinkClicked);
                AddLinkSubItem(item, MakeWritingSystemLinkLabel(markerSetting.WritingSystemRfc4646), OnWritingSystemLinkClicked);
                AddLinkSubItem(item, MakeMappingLinkLabel(SolidMarkerSetting.MappingType.Lift, markerSetting), OnLiftMappingLinkClicked);              
                //  FillInStructureColumn(item, _settings.FindOrCreateMarkerSetting(pair.Key).StructureProperties);
                //  FillInCheckedColumn(item, _dictionary.MarkerErrors[pair.Key]);

                _listView.Items.Add(item);
            }
            //           _listView.Sorting = SortOrder.Ascending;
            //          _listView.Sort();

            _listView.Columns[0].LastSortState = SortDirections.SortAscending;
            _listView.SortColumn(0);//review... how to keep the old order?
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

        private static void FillInFrequencyColumn(GLItem item, string frequency)
        {
            item.SubItems.Add(new GLSubItem {Text = frequency} );
        }

        private static string MakeWritingSystemLinkLabel(string writingSystemId)
        {
            if (string.IsNullOrEmpty(writingSystemId))
            {
                return "??";
            }

        	var repository = AppWritingSystems.WritingSystems;
            if (!repository.Contains(writingSystemId))
            {
                return writingSystemId;
            }
            var definition = repository.Get(writingSystemId);
            return (definition != null) ? definition.DisplayLabel : "??";
        }

        private string MakeMappingLinkLabel(SolidMarkerSetting.MappingType type, SolidMarkerSetting markerSetting)
        {
            string conceptId = markerSetting.GetMappingConceptId(type);
            var mappingSystem = _markerSettingsPM.MappingModel.TargetChoices[(int)type];
            
            var concept = mappingSystem.GetConceptById(conceptId);

            string mapping = (concept != null) ? concept.ToString() : null;
            
            return mapping ?? "??";
        }

        private static string MakeStructureLinkLabel(IEnumerable<SolidStructureProperty> properties)
        {
            string parents = "";
            foreach (var property in properties)
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

        private static void AddLinkSubItem(GLItem item, string text, LinkLabelLinkClickedEventHandler clickHandler)
        {
            var label = new LinkLabel();
            label.Text = text;
            label.AutoEllipsis = true;
            label.LinkColor = Color.Black;
            if (text == "???")
            {
                label.LinkColor = Color.Red;
            }

            label.Tag = item;
            label.BackColor = Color.Transparent;
            label.LinkClicked += clickHandler;
            label.LinkBehavior = LinkBehavior.HoverUnderline;

            var link = new GLSubItem();
            //label.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            link.Control = label;
            item.SubItems.Add(link);
            //  item.SubItems.Add(text);
        }

        private void OnStructureLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var item = (GLItem) ((LinkLabel)sender).Tag;
            item.Selected = true;
            OpenSettingsDialog("structure");
        }

        private void OnWritingSystemLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var item = (GLItem) ((LinkLabel)sender).Tag;
            item.Selected = true;
            OpenSettingsDialog("writingSystem");
        }


        private void OnLiftMappingLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var item = (GLItem)((LinkLabel)sender).Tag;
            item.Selected = true;
            OpenSettingsDialog("mapping", SolidMarkerSetting.MappingType.Lift);
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

        private void UpdateSelectedItems(SolidMarkerSetting setting)
        {
            _listView.SelectedItems[0].SubItems[2].Control.Text = MakeStructureLinkLabel(setting.StructureProperties);
            _listView.SelectedItems[0].SubItems[3].Control.Text = MakeWritingSystemLinkLabel(setting.WritingSystemRfc4646);
            _listView.SelectedItems[0].SubItems[4].Control.Text = MakeMappingLinkLabel(SolidMarkerSetting.MappingType.Lift, setting);
        }

        public void OpenSettingsDialog(string area)
        {
            if(_listView.SelectedItems.Count == 0)
            {
                return;
            }
            if (String.IsNullOrEmpty(area))
            {
                area = "structure";
            }

            UsageReporter.SendNavigationNotice("Settings/"+area);

            string marker = _listView.SelectedItems[0].Text;
            var dialog = new MarkerSettingsDialog(_markerSettingsPM, marker);
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
            var dialog = new MarkerSettingsDialog(_markerSettingsPM, marker);
            dialog.SelectedArea = area;
            dialog.MappingType = mappingType;
            dialog.ShowDialog();
            if (MarkerSettingPossiblyChanged != null)
                MarkerSettingPossiblyChanged.Invoke(this, new EventArgs());
            UpdateSelectedItems(_markerSettingsPM.GetMarkerSetting(marker));
        }



        public void SelectMarker(string marker)
        {
            foreach(GLItem a in _listView.Items)
            {
                a.Selected = (a.Text == marker);
            }
            if(String.IsNullOrEmpty(marker) && _listView.Items.Count > 0)
            {
                _listView.Items[0].Selected = true;
                //hack for a bug in Glacial List
                _listView_SelectedIndexChanged(null, new ClickEventArgs(0, 0));
            }
        }

        private void OnEditSettingsClick(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenSettingsDialog(null);
        }

        private void _listView_SelectedIndexChanged(object source, ClickEventArgs e)
        {
            if (_listView.SelectedItems.Count == 0)
            {
                return;
            }
            string marker = _listView.Items[e.ItemIndex].Text;

            _filter = new MarkerFilter(_dictionary, marker);
            _filterChooserPM.ActiveRecordFilter = _filter;
        }

        private void _listView_DoubleClick(object sender, EventArgs e)
        {
            OpenSettingsDialog(null);
        }
    }
}