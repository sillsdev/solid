using System;
using System.Collections.Generic;
using Solid.Engine;
using SolidGui.Engine;
using SolidGui.Filter;
using SolidGui.Mapping;

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

        public event EventHandler<FilterChooserPM.RecordFilterChangedEventArgs> MarkerFilterChanged;  // JMC:! test! ; Added to fix issue #1196  -JMC 2013-09

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

    }
}