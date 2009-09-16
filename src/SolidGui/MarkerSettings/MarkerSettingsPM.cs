using System.Collections.Generic;
using System.Linq;
using Solid.Engine;
using SolidGui.Mapping;

namespace SolidGui.MarkerSettings
{
    public class MarkerSettingsPM
    {

        public MarkerSettingsPM()
        {
            Root = "";
            MarkersInDictioanary = new List<string>();
            StructurePropertiesModel = new StructurePropertiesPM();
            MappingModel = new MappingPM();
        }

        public SolidSettings SolidSettings { get; set; }

        public string Root { get; set; }

        public StructurePropertiesPM StructurePropertiesModel { get; private set; }

        public MappingPM MappingModel { get; set; }

        public SolidMarkerSetting GetMarkerSetting(string marker)
        {
            return SolidSettings.FindOrCreateMarkerSetting(marker);
        }

        public IEnumerable<string> MarkersInDictioanary { get; set; }

        public IList<string> GetValidMarkers()
        {
            List<string> allValidMarkers = new List<string>();

            allValidMarkers.AddRange(MarkersInDictioanary);

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