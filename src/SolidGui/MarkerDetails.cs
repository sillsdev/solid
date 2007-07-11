using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using SolidEngine;

namespace SolidGui
{
    public partial class MarkerDetails : UserControl
    {
        private Dictionary _dictionary;
        private SolidSettings _settings;
        private MarkerSettingsPM _markerSettingsModel;
        public event EventHandler<FilterChooserPM.RecordFilterChangedEventArgs> RecordFilterChanged;


        public MarkerDetails()
        {
            InitializeComponent();
        }


        public void UpdateDisplay(MarkerSettingsPM markerSettingsModel, Dictionary dictionary, SolidSettings settings)
        {
            _markerSettingsModel = markerSettingsModel;
            _settings = settings;
            _dictionary = dictionary;
            _listView.Items.Clear();

            foreach (KeyValuePair<string, int> pair in dictionary.MarkerFrequencies)
            {
                ListViewItem item = new ListViewItem(pair.Key);
                //add frequency
                item.SubItems.Add(pair.Value.ToString());
                List<SolidStructureProperty> properties = settings.FindMarkerSetting(pair.Key).StructureProperties;
                string parents="";
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
                item.SubItems.Add(parents);
                _listView.Items.Add(item);
            }
        }

        private void _listView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(_listView.SelectedItems.Count == 0)
            {
                return;
            }  
            string marker = _listView.SelectedItems[0].Text;

            if (RecordFilterChanged != null)
                RecordFilterChanged.Invoke(this,
                                           new FilterChooserPM.RecordFilterChangedEventArgs(
                                               new MarkerFilter(_dictionary, marker)));
        }

        private void OnEditSettingsClick(object sender, EventArgs e)
        {
            OpenSettingsDialog();
        }

        private void OpenSettingsDialog()
        {
            if(_listView.SelectedItems.Count == 0)
            {
                return;
            }          
            string marker = _listView.SelectedItems[0].Text;
            MarkerSettingsDialog dialog = new MarkerSettingsDialog(_markerSettingsModel, marker);
            dialog.ShowDialog();
        }

        private void _listView_DoubleClick(object sender, EventArgs e)
        {
            OpenSettingsDialog();
        }
    }
}
