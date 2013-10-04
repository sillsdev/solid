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

        public MarkerSettingsPM()
        {
            Root = "";
            MarkersInDictionary = new List<string>();
            StructurePropertiesModel = new StructurePropertiesPM();
            MappingModel = new MappingPM();
        }

        private MarkerFilter _activeMarkerFilter = null;

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
                if (MarkerFilterChanged != null)
                {
                    MarkerFilterChanged.Invoke(this, new RecordFilterChangedEventArgs(_activeMarkerFilter));
                }
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

        public IList<string> GetValidMarkers()
        {
            List<string> allValidMarkers = new List<string>();

            allValidMarkers.AddRange(MarkersInDictionary);

            foreach (var marker in SolidSettings.Markers)
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