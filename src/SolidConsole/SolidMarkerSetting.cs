using System;
using System.Collections.Generic;
using System.Text;

namespace SolidConsole
{

    public class SolidMarkerSettings : List<SolidMarkerSetting>
    {
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
