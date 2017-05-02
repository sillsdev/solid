// Copyright (c) 2007-2014 SIL International
// Licensed under the MIT license: opensource.org/licenses/MIT

// I'm not sure why, but these two GUI elements (this and FilterChooserView) aren't embedding quite right with Dock = Fill.
// I had to add some top padding so the top part (e.g. column headings) would be visible. -JMC Jan 2014

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using GlacialComponents.Controls;
using SIL.Reporting;
using SIL.Windows.Forms.WritingSystems;
using SIL.WritingSystems;
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

        // top left
        private int _xOfDialog = -90;
        private int _yOfDialog = -90; 

        private SfmDictionary _dictionary; // /  JMC:! I'd like to pull this out, and access it via MainWindowPM.
        private MarkerSettingsPM _markerSettingsPm;  // JMC:!! Ditto esp. for this one, since it can't pick up on GiveSolidSettingsToModels
        private MarkerSettingsDialog _markerSettingsDialog;  // added so this can be a non-modal dialog. -JMC Sep 2014

        public MarkerSettingsListView()
        {
            InitializeComponent();

            GLColumn frequencyCount = (from GLColumn c in _markerListView.Columns where c.Text == "Count" select c).First();
            frequencyCount.ComparisonFunction = (a, b) => int.Parse(a).CompareTo(int.Parse(b));   // TODO! Consider doing the same to make other columns sortable too -JMC
            _markerListView.GridLineStyle = GLGridLineStyles.gridNone;
            _markerListView.SelectionColor = Color.LightYellow;
            _markerListView.SelectedTextColor = Color.Black;
        }

        public void BindModel(MarkerSettingsPM markerSettingsPm, SfmDictionary dictionary)
        {
            if (_markerSettingsPm != null)
            {
                _markerSettingsPm.MarkerFilterChanged -= OnMarkerFilterChanged;
            }

            _markerListView.SuspendLayout();
            _markerSettingsPm = markerSettingsPm;

            _dictionary = dictionary;

            _markerSettingsPm.MarkerFilterChanged += OnMarkerFilterChanged;
            
            //UpdateDisplay();
        }

        private GLItem FillListItem (KeyValuePair<string, int> pair, GLItem item)
        {
            item.SubItems.Add(pair.Key);  // MARKER

            //The order these are called in matters
            FillInFrequencyColumn(item, pair.Value.ToString());  // COUNT

            SolidMarkerSetting markerSetting = _markerSettingsPm.SolidSettings.FindOrCreateMarkerSetting(pair.Key);
            AddLinkSubItem(item, MarkerSettingsPM.MakeStructureLinkLabel(markerSetting.StructureProperties, markerSetting), OnStructureLinkClicked);  // UNDER

            AddLinkSubItem(item, MakeWritingSystemLinkLabel(markerSetting.WritingSystemRfc4646), OnWritingSystemLinkClicked);  // WS

            var u = new GLSubItem();
            u.Checked = markerSetting.Unicode;
            item.SubItems.Add(u);  // UNICODE  //added -JMC Feb 2014

            AddLinkSubItem(item, MakeMappingLinkLabel(SolidMarkerSetting.MappingType.Lift, markerSetting), OnLiftMappingLinkClicked); //LIFT 

            string shortComment = FirstChars(markerSetting.Comment, 45);
            AddLinkSubItem(item, shortComment, OnStructureLinkClicked); // COMMENT

            //FillInErrorColumn(item, _dictionary.MarkerErrors[pair.Key]);
            return item;
        }

        private static string FirstChars(string s, int max)
        {
            if (String.IsNullOrEmpty(s)) return "";
            int n = Math.Min(max, s.Length);
            string tmp = s.Substring(0, n);
            return tmp;
        }

        public void UpdateDisplay(bool fullRefresh)
        {
            if (DesignMode) return;

            string previouslySelectedMarker = string.Empty;
            
            if(_markerListView.SelectedItems.Count > 0)
            {
                previouslySelectedMarker = _markerListView.SelectedItems[0].Text;
            }

            if (fullRefresh)
            {
                _markerListView.Items.Clear();
                //_markerListView.MySortBrush  = null;
                // _markerListView.MySortBrush = Brushes.Coral;
                // _markerListView.MyHighlightBrush = System.Drawing.SystemBrushes.Highlight;

                //            ImageList colimglst = new ImageList();
                //            colimglst.ImageSize = new Size(20, 20); // this will affect the row height
                //            _markerListView.SmallImageList = colimglst;

                if (_dictionary == null) return;
            }

            if (!fullRefresh)
            {
                foreach(GLItem item in _markerListView.Items)
                {
                    string key = item.Text;
                    int value = _dictionary.MarkerFrequencies[key];
                    var pair = new KeyValuePair<string, int>(key, value);
                    item.SubItems.Clear();
                    FillListItem(pair, item);
                }
            }
            else
            {
                // Here we either need to lock here (and elsewhere) for thread safety and use for(), or make a copy.  http://projects.palaso.org/issues/1279
                // Using foreach() is nicer, and it's not a huge dataset, so I'm making a copy. -JMC July 2014
                KeyValuePair<string, int>[] tmp = _dictionary.MarkerFrequencies.ToArray(); // copy
                foreach (KeyValuePair<string, int> pair in tmp)
                {
                    GLItem item = new GLItem();
                    FillListItem(pair, item);
                    _markerListView.Items.Add(item);
                }

                //           _markerListView.Sorting = SortOrder.Ascending;
                //          _markerListView.Sort();

                _markerListView.Columns[0].LastSortState = SortDirections.SortAscending;
                _markerListView.SortColumn(0); // TODO: review... how to keep the old order? -CP
                SelectMarker(previouslySelectedMarker);

                Workaround(_markerListView);
            }

            Refresh();

            if (_markerSettingsDialog != null && !_markerSettingsDialog.IsDisposed)
            {
                _markerSettingsDialog.UpdateDisplay();
            }

        }

        public static void Workaround(GlacialList gl)
        {
            //Would this (copied code) help us here too? -JMC

/*
            //Workaround so that we don't lose our row highlight. (Not needed when debugging with breakpoints!) -JMC Feb 2014
            gl.Hide(); gl.Show();
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
            WritingSystemDefinition definition = repository.Get(writingSystemId);
            return (definition != null) ? definition.DisplayLabel : "??";
        }

        private string MakeMappingLinkLabel(SolidMarkerSetting.MappingType type, SolidMarkerSetting markerSetting)
        {
            string conceptId = markerSetting.GetMappingConceptId(type);
            var mappingSystem = _markerSettingsPm.MappingModel.TargetChoices[(int)type];
            string mapping = "??";
            if (conceptId != null)
            {
                MappingPM.Concept concept = mappingSystem.GetConceptById(conceptId);
                if (concept != null) mapping = concept.Label();
            }
            return mapping;
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
            OpenSettingsDialog("", MarkerSettingsDialog.firstTab);
        }

        private void OnWritingSystemLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var item = (GLItem) ((LinkLabel)sender).Tag;
            item.Selected = true;
            OpenSettingsDialog("", MarkerSettingsDialog.firstTab);
        }


        private void OnLiftMappingLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var item = (GLItem)((LinkLabel)sender).Tag;
            item.Selected = true;
            OpenSettingsDialog("", MarkerSettingsDialog.mappingTab);
        }

        // When someone changes the filter in the PM
        public void OnMarkerFilterChanged(object sender, RecordFilterChangedEventArgs e)
        {

            _changingFilter = true; // prevents event-firing loops -JMC
            string marker = "";

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
            if (e.RecordFilter != null)
            {
                foreach (GLItem gItem in _markerListView.Items)
                {
                    string m = MarkerFilter.MarkerLabel + gItem.Text;
                    if (m == e.RecordFilter.Name)
                    {
                        marker = gItem.Text;
                        gItem.Selected = true;

                        break;
                    }
                }
            }

            _changingFilter = false;

            // changing to a marker filter should update any open settings dialog, to keep it in sync (#1283)
            if (marker != "" && _markerSettingsDialog != null && !_markerSettingsDialog.IsDisposed)
            {
                _markerSettingsDialog.SetMarker(marker);
                _markerSettingsDialog.UpdateDisplay();
            }
        }

        public void CloseSettingsDialog()
        {
            if(_markerSettingsDialog != null && !_markerSettingsDialog.IsDisposed)
            {
                _markerSettingsDialog.Close();
                _markerSettingsDialog.Dispose();
            }
            _markerSettingsDialog = null;
        }

        public bool OpenSettingsDialog(string marker, string area)
        {

            if(_markerListView.SelectedItems.Count == 0)
            {
                return false;
            }
            if (String.IsNullOrEmpty(marker))
            {
                marker = _markerListView.SelectedItems[0].Text;
            }
            if (String.IsNullOrEmpty(area))
            {
                area = MarkerSettingsDialog.firstTab;
            }

            UsageReporter.SendNavigationNotice("Settings/"+area);


            if (_markerSettingsDialog == null || _markerSettingsDialog.IsDisposed)
            {
                _markerSettingsDialog = new MarkerSettingsDialog(_markerSettingsPm, marker, area);

                _markerSettingsDialog.Left = _xOfDialog;
                _markerSettingsDialog.Top = _yOfDialog;
                var myDelegate = new EventHandler(OnMarkerSettingPossiblyChanged);
                _markerSettingsDialog.Listen(myDelegate); //wire it up (to be responsive to the non-modal dialog)
                _markerSettingsDialog.BringToFront();

                Screen scr = Screen.FromControl(this);
                _xOfDialog = scr.WorkingArea.Left + 1; //scr.WorkingArea.Width - this.Width;  // nearly top-right
                _yOfDialog = scr.WorkingArea.Top + 1; //_searchDialog.Top = scr.WorkingArea.Height - (_searchDialog.Height);
            }
            else
            {
                _markerSettingsDialog.SetArea(area);
                _markerSettingsDialog.SetMarker(marker);
                //_markerSettingsDialog.Hide();
            }
            
            _markerSettingsDialog.UpdateDisplay();  //Will the next line trigger this anyway? Yes, but it'll crash without this. -JMC
            _markerSettingsDialog.Show();  //was .ShowDialog();
            _markerSettingsDialog.BringToFront();

            return true;
        }

        private void OnMarkerSettingPossiblyChanged(object sender, EventArgs e)
        {
            if (MarkerSettingPossiblyChanged != null) MarkerSettingPossiblyChanged.Invoke(this, EventArgs.Empty);

            /* the above now causes the whole app to update anyway
            UpdateDisplay(); //more effective at highlighting the row: rebuild all rows
            //UpdateSelectedItems(_markerSettingsPM.GetMarkerSetting(marker));  //this was more efficient: update one row; but it adds maintenance overhead, and doesn't highlight the row. Removed. -JMC Feb 2014
             */
        }

        public string SelectedMarker()
        {
            if (_markerListView.Items.Count > 0)
            {
                foreach (GLItem a in _markerListView.Items)
                {
                    if (a.Selected) return a.Text;
                }

            }
            return "";
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

            // Handle the new unicode column -JMC
            SolidMarkerSetting m = _markerSettingsPm.GetMarkerSetting(marker);
            if (m.Unicode != item.SubItems[4].Checked)
            {
                _markerSettingsPm.WillNeedSave();
                m.Unicode = !m.Unicode;
                if (MarkerSettingPossiblyChanged != null) MarkerSettingPossiblyChanged.Invoke(this, EventArgs.Empty);
            }

            if (!_changingFilter)
            {
                _markerSettingsPm.ActiveMarkerFilter = new MarkerFilter(_dictionary, marker); 
                // JMC:! why did we create a new one here, but not for the same situation in FilterChooserView, _filterList_SelectedIndexChanged() ?
            }

        }

        private void _markerListView_DoubleClick(object sender, EventArgs e)
        {
            OpenSettingsDialog("", "");
        }
    }

}
