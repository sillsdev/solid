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
using SolidGui.Mapping;
using SolidGui.Model;


namespace SolidGui.MarkerSettings
{
    public partial class MarkerSettingsListView : UserControl
    {
        private GlacialList _markerListView;
        private bool _changingFilter = false;
        public event EventHandler MarkerSettingPossiblyChanged;

        private SfmDictionary _dictionary; // /  JMC:! I'd like to pull this out, and access it via MainWindowPM.
        private MarkerSettingsPM _markerSettingsPM;  // JMC:!! Ditto esp. for this one, since it can't pick up on GiveSolidSettingsToModels

        public MarkerSettingsListView()
        {
            InitializeComponent();

            GLColumn frequencyCount = (from GLColumn c in _markerListView.Columns where c.Text == "Count" select c).First();
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

        private GLItem MakeListItem (KeyValuePair<string, int> pair)
        {
            var item = new GLItem(); 
            item.SubItems.Add(pair.Key);  // MARKER

            //The order these are called in matters
            FillInFrequencyColumn(item, pair.Value.ToString());  // COUNT

            SolidMarkerSetting markerSetting = _markerSettingsPM.SolidSettings.FindOrCreateMarkerSetting(pair.Key);
            AddLinkSubItem(item, MakeStructureLinkLabel(markerSetting.StructureProperties, markerSetting), OnStructureLinkClicked);  // UNDER

            AddLinkSubItem(item, MakeWritingSystemLinkLabel(markerSetting.WritingSystemRfc4646), OnWritingSystemLinkClicked);  // WS

            var u = new GLSubItem();
            u.Checked = markerSetting.Unicode;
            item.SubItems.Add(u);  // UNICODE  //added -JMC Feb 2014

            AddLinkSubItem(item, MakeMappingLinkLabel(SolidMarkerSetting.MappingType.Lift, markerSetting), OnLiftMappingLinkClicked); //LIFT 

            //FillInErrorColumn(item, _dictionary.MarkerErrors[pair.Key]);
            return item;
        }
        
        public void UpdateDisplay()
        {
            if (DesignMode) return;

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
                GLItem item = MakeListItem(pair);
                _markerListView.Items.Add(item);
            }

            //           _markerListView.Sorting = SortOrder.Ascending;
            //          _markerListView.Sort();

            _markerListView.Columns[0].LastSortState = SortDirections.SortAscending;
            _markerListView.SortColumn(0); // TODO: review... how to keep the old order?
            SelectMarker(previouslySelectedMarker);

            Workaround(_markerListView);

        }

        public static void Workaround(GlacialList gl)
        {
            //Would this (copied code) help us here too? -JMC

/*
            //Workaround so that we don't lose our row highlight. (Not needed when debugging with breakpoints!) -JMC Feb 2014
            gl.Hide();
            gl.Show();
            if (gl.SelectedItems.Count > 0)
            {
                gl.FocusedItem = gl.SelectedItems[0];
            }
*/

        }

        private void FillInErrorColumn(ListViewItem item, int errorCount)
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

            IWritingSystemRepository repository = AppWritingSystems.WritingSystems;
            if (!repository.Contains(writingSystemId))
            {
                return writingSystemId;
            }
            IWritingSystemDefinition definition = repository.Get(writingSystemId);
            return (definition != null) ? definition.DisplayLabel : "??";
        }

        private string MakeMappingLinkLabel(SolidMarkerSetting.MappingType type, SolidMarkerSetting markerSetting)
        {
            string conceptId = markerSetting.GetMappingConceptId(type);
            var mappingSystem = _markerSettingsPM.MappingModel.TargetChoices[(int)type];

            MappingPM.Concept concept = mappingSystem.GetConceptById(conceptId); // JMC: often null; is it safe to wrap this in an if (conceptId != null) ? w/b faster but check the called functions

            string mapping = (concept != null) ? concept.Label() : null;
            
            return mapping ?? "??";
        }

        private static string MakeStructureLinkLabel(IEnumerable<SolidStructureProperty> properties, SolidMarkerSetting markerSetting)
        {
            string parents = "";

            if (!String.IsNullOrEmpty(markerSetting.InferedParent))  //implements issue #1272 -JMC Mar 2014
            {
                parents += "+" + markerSetting.InferedParent;
            }

            foreach (SolidStructureProperty property in properties)
            {
                if (!string.IsNullOrEmpty(property.Parent))
                {
                    if (parents!= "")
                    {
                        parents += ", ";
                    }
                    parents += String.Format("{0} ({1})", property.Parent, property.Multiplicity.Abbr());
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
            OpenSettingsDialog("mapping"); //, SolidMarkerSetting.MappingType.Lift);
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
                //JMC:! don't we need to cast filter from object to RecordFilter ?
                if (filter == e.RecordFilter)
                {
                    _markerListView.Items[n].Selected = true;
                    break;
                }
                n++;
            }

            _changingFilter = false;
        }

        public bool OpenSettingsDialog(string area)
        {

            if(_markerListView.SelectedItems.Count == 0)
            {
                return false;
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
                MarkerSettingPossiblyChanged.Invoke(this, EventArgs.Empty);
            }

            UpdateDisplay(); //more effective at highlighting the row: rebuild all rows
            //UpdateSelectedItems(_markerSettingsPM.GetMarkerSetting(marker));  //this was more efficient: update one row; but it adds maintenance overhead, and doesn't highlight the row. Removed. -JMC Feb 2014
            return true;
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
            if (e.ItemIndex < 0) return;
            GLItem item = _markerListView.Items[e.ItemIndex];
            string marker = item.Text;

            if (!_changingFilter)
            {
                _markerSettingsPM.ActiveMarkerFilter = new MarkerFilter(_dictionary, marker); 
                // JMC:! why did we create a new one here, but not for the same situation in FilterChooserView, _filterList_SelectedIndexChanged() ?
            }

            // Handle the new unicode column -JMC
            SolidMarkerSetting m = _markerSettingsPM.GetMarkerSetting(marker);
            if (m.Unicode != item.SubItems[4].Checked)
            {
                _markerSettingsPM.WillNeedSave();
                m.Unicode = !m.Unicode;
                MarkerSettingPossiblyChanged.Invoke(this, EventArgs.Empty);
            }
        
        }

        private void _markerListView_DoubleClick(object sender, EventArgs e)
        {
            OpenSettingsDialog(null);
        }



    }

}
