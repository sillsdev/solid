// Copyright (c) 2007-2014 SIL International
// Licensed under the MIT license: opensource.org/licenses/MIT

// JMC: I'm not sure why, but these two GUI elements (this and FilterChooserView) aren't embedding quite right with Dock = Fill.
// I had to add some top padding so the top part (e.g. column headings) would be visible. -JMC Jan 2014

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using GlacialComponents.Controls;
using Palaso.Reporting;
using Palaso.WritingSystems;
using SolidGui.Engine;
using SolidGui.Filter;
using SolidGui.Model;


namespace SolidGui.MarkerSettings
{
    public partial class MarkerSettingsListView : UserControl
    {
        private bool _changingFilter = false;
        public event EventHandler MarkerSettingPossiblyChanged;

        private SfmDictionary _dictionary; // /  JMC:! I'd like to pull this out, and access it via MainWindowPM.
        private MarkerSettingsPM _markerSettingsPM;  // JMC:!! Ditto esp. for this one, since it can't pick up on GiveSolidSettingsToModels

        public MarkerSettingsListView()
        {
            InitializeComponent();

            var frequencyCount = (from GLColumn c in _markerListView.Columns where c.Text == "Count" select c).First();
            frequencyCount.ComparisonFunction = (a, b) => int.Parse(a).CompareTo(int.Parse(b));   // JMC: Consider doing the same to make other columns sortable too
            _markerListView.GridLineStyle = GLGridLineStyles.gridNone;
            _markerListView.SelectionColor = Color.LightYellow;
            _markerListView.SelectedTextColor = Color.Black;
        }

        public void BindModel(MarkerSettingsPM markerSettingsPM, SfmDictionary dictionary)
        {
            if (_markerSettingsPM != null)
            {
                _markerSettingsPM.MarkerFilterChanged -= OnMarkerFilterChanged;
            }

            _markerListView.SuspendLayout();
            _markerSettingsPM = markerSettingsPM;

            _dictionary = dictionary;

            _markerSettingsPM.MarkerFilterChanged += OnMarkerFilterChanged;
            
            //UpdateDisplay();
        }
        
        public void UpdateDisplay()
        {
            string previouslySelectedMarker = string.Empty;
            
            if(_markerListView.SelectedItems.Count > 0)
            {
                previouslySelectedMarker = _markerListView.SelectedItems[0].Text;
            }

            _markerListView.Items.Clear();
            //_markerListView.MySortBrush  = null;
            // _markerListView.MySortBrush = Brushes.Coral;
            // _markerListView.MyHighlightBrush = System.Drawing.SystemBrushes.Highlight;

//            ImageList colimglst = new ImageList();
//            colimglst.ImageSize = new Size(20, 20); // this will affect the row height
//            _markerListView.SmallImageList = colimglst;

            // if (_settings == null) return;  // Could add this check, but it would mask a bad state. -JMC

            if (_dictionary == null) return;
            foreach (KeyValuePair<string, int> pair in _dictionary.MarkerFrequencies)
            {
                var item = new GLItem();// (pair.Key);
                item.SubItems.Add(pair.Key);
                
                //The order these are called in matters
                FillInFrequencyColumn(item, pair.Value.ToString());
                SolidMarkerSetting markerSetting = _markerSettingsPM.SolidSettings.FindOrCreateMarkerSetting(pair.Key);
                AddLinkSubItem(item, MakeStructureLinkLabel(markerSetting.StructureProperties), OnStructureLinkClicked);
                AddLinkSubItem(item, MakeWritingSystemLinkLabel(markerSetting.WritingSystemRfc4646), OnWritingSystemLinkClicked);
                AddLinkSubItem(item, MakeMappingLinkLabel(SolidMarkerSetting.MappingType.Lift, markerSetting), OnLiftMappingLinkClicked);  //JMC:! add another (checkbox) column here for Unic ?          
                //  FillInStructureColumn(item, _settings.FindOrCreateMarkerSetting(pair.Key).StructureProperties);
                //  FillInCheckedColumn(item, _dictionary.MarkerErrors[pair.Key]);

                _markerListView.Items.Add(item);
            }

            //           _markerListView.Sorting = SortOrder.Ascending;
            //          _markerListView.Sort();

            _markerListView.Columns[0].LastSortState = SortDirections.SortAscending;
            _markerListView.SortColumn(0); // TODO: review... how to keep the old order?
            SelectMarker(previouslySelectedMarker);

            //JMC:! to update NeedSave, either trigger an event here, or (if we had a handle) call MainWindowView's UpdateDisplay() 
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

            var concept = mappingSystem.GetConceptById(conceptId); // JMC: often null; is it safe to wrap this in an if (conceptId != null) ? w/b faster but check the called functions

            string mapping = (concept != null) ? concept.Label() : null;
            
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
        public void OnMarkerFilterChanged(object sender, RecordFilterChangedEventArgs e)
        {

            _changingFilter = true; // prevents event-firing loops -JMC

            // Remove the selection (start with blank slate)
            _markerListView.Items.ClearSelection();
            /*
            for (int i = 0; i < _markerListView.Items.Count; i++)
            {
                _markerListView.Items[i].Selected = false;
            }
            */

            // Alas, one item may still be invisibly selected, so we need to clear that too:
            _markerListView.FocusedItem = null;

            // Find the new filter in our list and select it
            int n = 0;
            foreach (var filter in _markerListView.Items)
            {
                if (filter == e.RecordFilter)
                {
                    _markerListView.Items[n].Selected = true;
                    break;
                }
                n++;
            }

            _changingFilter = false;
        }

        private void UpdateSelectedItems(SolidMarkerSetting setting)
        {
            if (_markerListView.SelectedItems.Count > 0)
            {
                _markerListView.SelectedItems[0].SubItems[2].Control.Text = MakeStructureLinkLabel(setting.StructureProperties);
                _markerListView.SelectedItems[0].SubItems[3].Control.Text = MakeWritingSystemLinkLabel(setting.WritingSystemRfc4646);
                _markerListView.SelectedItems[0].SubItems[4].Control.Text = MakeMappingLinkLabel(SolidMarkerSetting.MappingType.Lift, setting);
            }
        }

        // JMC: considering refactoring these two
        public void OpenSettingsDialog(string area)
        {
            if(_markerListView.SelectedItems.Count == 0)
            {
                return;
            }
            if (String.IsNullOrEmpty(area))
            {
                area = "structure";
            }

            UsageReporter.SendNavigationNotice("Settings/"+area);

            string marker = _markerListView.SelectedItems[0].Text;
            var dialog = new MarkerSettingsDialog(_markerSettingsPM, marker);
            dialog.SelectedArea = area;
            dialog.ShowDialog();
            if (MarkerSettingPossiblyChanged != null)
            {
                MarkerSettingPossiblyChanged.Invoke(this, new EventArgs());
            }

            UpdateSelectedItems(_markerSettingsPM.GetMarkerSetting(marker));
            UpdateDisplay();
        }

        private void OpenSettingsDialog(string area, SolidMarkerSetting.MappingType mappingType)
        {
            if (_markerListView.SelectedItems.Count == 0)
            {
                return;
            }
            string marker = _markerListView.SelectedItems[0].Text;
            var dialog = new MarkerSettingsDialog(_markerSettingsPM, marker);
            dialog.SelectedArea = area;
            dialog.MappingType = mappingType;
            dialog.ShowDialog();
            if (MarkerSettingPossiblyChanged != null)
            {
                MarkerSettingPossiblyChanged.Invoke(this, new EventArgs());
            }
            UpdateSelectedItems(_markerSettingsPM.GetMarkerSetting(marker));
            UpdateDisplay();
        }



        public void SelectMarker(string marker)
        {
            if (_markerListView.Items.Count > 0) return;

            foreach(GLItem a in _markerListView.Items)
            {
                a.Selected = (a.Text == marker);
            }
        }

        private void _markerListView_SelectedIndexChanged(object source, ClickEventArgs e)
        {
            // Disabled this check, as it was causing issue #1196 (extra click required to apply marker filter)  -JMC 2013-09
/*          
            if (_markerListView.SelectedItems.Count == 0)
            {
                return;
            }
*/
            if (!_changingFilter)
            {
                string marker = _markerListView.Items[e.ItemIndex].Text;
                _markerSettingsPM.ActiveMarkerFilter = new MarkerFilter(_dictionary, marker);  
                // JMC:! why did we create a new one here, but not for the same situation in FilterChooserView, _filterList_SelectedIndexChanged() ?
            }
        }

        private void _markerListView_DoubleClick(object sender, EventArgs e)
        {
            OpenSettingsDialog(null);
        }


    }

}
