// Copyright (c) 2007-2014 SIL International
// Licensed under the MIT license: opensource.org/licenses/MIT

using System;
using System.Collections.Generic;
using Solid.Engine;
using SolidGui.Engine;
using SolidGui.Filter;
using SolidGui.Mapping;
using SolidGui.Model;

namespace SolidGui.MarkerSettings
{
    public class MarkerSettingsPM
    {

        public MarkerSettingsPM(MainWindowPM m)
        {
            _mainWindowPm = m;
            Root = "";
            MarkersInDictionary = new List<string>();
            StructurePropertiesModel = new StructurePropertiesPM(this);
            MappingModel = new MappingPM(this);
        }

        /// <summary>
        /// The calls to this (and avoiding calling it) are a way allowing 
        /// the user to either edit or just look at marker settings. Just 
        /// looking shouldn't triggering the "needs save" state. -JMC 2014
        /// </summary>
        public void WillNeedSave()
        {
            _mainWindowPm.needsSave = true;
        }

        private MainWindowPM _mainWindowPm { get; set; }

        private MarkerFilter _activeMarkerFilter = null;

        public override string ToString()
        {
            return String.Format("{{mrk: Active: {0}; All: {1}}}",
                _activeMarkerFilter, SolidSettings);
        }

        public static string MakeStructureLinkLabel(IEnumerable<SolidStructureProperty> properties, SolidMarkerSetting markerSetting)
        {
            string parents = "";

            foreach (SolidStructureProperty property in properties)
            {
                if (!String.IsNullOrEmpty(property.Parent))
                {
                    if (parents != "")
                    {
                        parents += ", ";
                    }
                    parents += String.Format("{0} ({1})", property.Parent, property.Multiplicity.Abbr());
                    if (property.Required) parents += "*";
                }
            }
            if (parents == "")
            {
                parents = "???";
            }

            if (!String.IsNullOrEmpty(markerSetting.InferedParent))  //implements issue #1272 -JMC Mar 2014
            {
                parents += "; [+" + markerSetting.InferedParent + "]";
            }

            return parents;
        }


        public MarkerFilter ActiveMarkerFilter
        {
            get
            {
                return _activeMarkerFilter;
            }
            set
            {
                if (_activeMarkerFilter == value) return;
                _activeMarkerFilter = value;
                if (MarkerFilterChanged != null) MarkerFilterChanged.Invoke(this, new RecordFilterChangedEventArgs(_activeMarkerFilter));
            }
        }

        public SolidSettings SolidSettings { get; set; }

        public string Root { get; set; }

        public StructurePropertiesPM StructurePropertiesModel { get; private set; }

        public MappingPM MappingModel { get; set; }

        public SolidMarkerSetting GetMarkerSetting(string marker)
        {
            return SolidSettings.FindOrCreateMarkerSetting(marker);
        }

        public IEnumerable<string> MarkersInDictionary { get; set; }

        public IList<string> GetAllMarkers()
        {
            List<string> allValidMarkers = new List<string>();

            allValidMarkers.AddRange(MarkersInDictionary);

            foreach (string marker in SolidSettings.Markers)
            {
                if (!allValidMarkers.Contains(marker))
                {
                     allValidMarkers.Add(marker);
                }
            }

            return allValidMarkers;
        }

        public event EventHandler<RecordFilterChangedEventArgs> MarkerFilterChanged;  // added -JMC 2013-09

        public void OnNavFilterChanged(object sender, RecordFilterChangedEventArgs e)  // added -JMC 2013-10
        {
            var filter = e.RecordFilter;
            if (filter != ActiveMarkerFilter)
            {
                // The nav filter just changed, and it wasn't me.
                ActiveMarkerFilter = null;
            }
        }
    }
}