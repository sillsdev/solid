using System.Collections.Generic;
using SolidEngine;

namespace SolidGui
{
    public class MarkerSettingsPM
    {
        private IEnumerable<string> _allDictionaryMarkers;

        public MarkerSettingsPM()
        {
            Root = "";
            _allDictionaryMarkers = new List<string>();
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

        public IEnumerable<string> AllMarkers
        {
            get
            {
                return _allDictionaryMarkers;
            }
            set
            {
                _allDictionaryMarkers = value;
            }
        }

        public IList<string> GetValidMarkers()
        {
            List<string> allValidMarkers = new List<string>();

            allValidMarkers.AddRange(_allDictionaryMarkers);

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
