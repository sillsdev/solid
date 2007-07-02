using System.Collections.Generic;

namespace SolidConsole
{

    public class SolidMarkerSettings : List<SolidMarkerSetting>
    {
        public SolidMarkerSetting Find(string marker)
        {
            // Search for the marker. If not found return default marker settings.
            SolidMarkerSetting result = Find(delegate(SolidMarkerSetting item) { return item.Marker == marker; });
            if (result == null)
            {
                result = new SolidMarkerSetting(marker);
            }
            return result;
        }
    }

    public class SolidMarkerSetting
    {
        private List<SolidStructureProperty> _structureProperties;
        private string _marker;
        private string _inferedParent;

        public SolidMarkerSetting()
        {
            _marker = "";
            _inferedParent = "";
            _structureProperties = new List<SolidStructureProperty>();
        }

        public SolidMarkerSetting(string marker)
        {
            _marker = marker;
            _inferedParent = "";
            _structureProperties = new List<SolidStructureProperty>();
        }

        public string InferedParent
        {
            get
            {
                return _inferedParent;
            }
            set
            {
                _inferedParent = value;
            }
        }

        public List<SolidStructureProperty> StructureProperties
        {
            get 
            { 
                return _structureProperties; 
            }
            set
            {
                _structureProperties = value;
            }
        }

        public string Marker
        {
            get
            {
                return _marker;
            }
            set
            {
                _marker = value;
            }
        }
        
    }

}
