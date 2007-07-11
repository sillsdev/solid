using System.Collections.Generic;
using SolidEngine;

namespace SolidGui
{
    public class MarkerSettingsPM
    {
        private IEnumerable<string> _allDictionaryMarkers;
        private List<SolidMarkerSetting> _markerSettings;
        private StructurePropertiesPM _structurePropertiesModel;
        private MappingPM _mappingModel;
        private string _root;

        public MarkerSettingsPM()
        {
            _root = "";
            _markerSettings = new List<SolidMarkerSetting>();
            _allDictionaryMarkers = new List<string>();
            _structurePropertiesModel = new StructurePropertiesPM();
            MappingModel = new MappingPM();
        }

        public SolidMarkerSetting GetMarkerSetting(string marker)
        {
            foreach (SolidMarkerSetting markerSetting in _markerSettings)
            {
                if(markerSetting.Marker == marker)
                {
                    return markerSetting;
                }
            }
            SolidMarkerSetting ms = new SolidMarkerSetting(marker);
            _markerSettings.Add(ms);
            return ms;
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
        public List<SolidMarkerSetting>MarkerSettings
        {
            get
            {
                return _markerSettings;
            }
            set
            {
                _markerSettings = value;
            }
        }

        public IList<string> GetValidMarkers()
        {
            List<string> allValidMarkers = new List<string>();

            allValidMarkers.AddRange(_allDictionaryMarkers);
            foreach (SolidMarkerSetting setting in _markerSettings)
            {
                if(!allValidMarkers.Contains(setting.Marker))
                {
                    allValidMarkers.Add(setting.Marker);
                }
            }

            return allValidMarkers;
        }

        public string Root
        {
            get
            {
                return _root;
            }
            set
            {
                _root = value;
            }
        }

        public StructurePropertiesPM StructurePropertiesModel
        {
            get { return _structurePropertiesModel; }
        }

        public MappingPM MappingModel
        {
            get
            {
                return _mappingModel;
            }
            set
            {
                _mappingModel = value;
            }
        }
    }
}
