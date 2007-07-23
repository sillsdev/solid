using System;
using System.Collections.Generic;

namespace SolidEngine
{
    public class SolidMarkerSetting
    {
        public enum MappingType
        {
            Flex,
            Lift,
            Max
        }

        private string _marker;
        private string _inferedParent;
        private string _writingSystem;
        private List<SolidStructureProperty> _structureProperties;
        private string[] _mappings = new string[(int)MappingType.Max];

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

        public string WritingSystem
        {
            get { return _writingSystem; }
            set { _writingSystem = value; }
        }

        public string[] Mapping
        {
            get { return _mappings; }
        }

        public void SetMapping(SolidMarkerSetting.MappingType mappingType, string mapping)
        {
            _mappings[(int) mappingType] = mapping;
        }

        public string GetMapping(MappingType mappingType)
        {
            return _mappings[(int) mappingType];
        }
	
        public bool ParentExists(string marker)
        {
            SolidStructureProperty result = _structureProperties.Find(
                delegate(SolidStructureProperty item)
                {
                    return item.Parent == marker;
                }
            );
            return result != null;
        }

        public override string ToString()
        {
            return Marker;
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

        public string[] Mappings
        {
            get { return _mappings;}
            set { _mappings = value; }
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

        public SolidStructureProperty getStructureProperty(string name)
        {
            return _structureProperties.Find(
                delegate (SolidStructureProperty property)
                {
                    return property.Parent == name;
                }
            );
        }
    }

}
