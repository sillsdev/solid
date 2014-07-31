// Copyright (c) 2007-2014 SIL International
// Licensed under the MIT license: opensource.org/licenses/MIT

using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace SolidGui.Engine
{
    public class SolidMarkerSetting
    {
        public enum MappingType
        {
            FlexDefunct, //don't remove, this messes up old solid files
            Lift,
            Max
        }

        private string _marker;
        private string _inferedParent;
        private string _writingSystemRfc4646;
        private bool _isUnicode;
        private List<SolidStructureProperty> _structureProperties;
        
        public SolidMarkerSetting() : this("", false) // defaults to legacy rather than unicode -JMC
        {
        }

        public SolidMarkerSetting(string marker, bool isUnicode)
        {
            Mappings = new string[(int)MappingType.Max];
            _marker = marker;
            _isUnicode = isUnicode;
            _inferedParent = "";
            _writingSystemRfc4646 = "";
            _structureProperties = new List<SolidStructureProperty>();
        }

        public string Marker  // Don't rename
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

        [XmlElement(ElementName = "WritingSystem")]
        public string WritingSystemRfc4646  // an "extended language subtag"; see en.wikipedia.org/wiki/IETF_language_tag  -JMC
        {
            get
            {
                return _writingSystemRfc4646;
            }
            set
            {
                _writingSystemRfc4646 = value;
            }
        }

        // Don't rename these properties unless you're willing to specify ElementName, or to create a new version of .solid -JMC

        public bool Unicode  // Don't rename
        {
            get { return _isUnicode; }
            set { _isUnicode = value; }
        }

        public string[] Mappings { get; set; }  // Don't rename

        public void SetMappingConcept(MappingType mappingType, string id)
        {
            Mappings[(int) mappingType] = id;
        }

        public string GetMappingConceptId(MappingType mappingType)
        {
            return Mappings[(int) mappingType];
        }

        public bool ParentExists(string marker)
        {
            SolidStructureProperty result = _structureProperties.Find(item => item.Parent == marker);
            return result != null;
        }

        public override string ToString()
        {
            return Marker;
        }

        public string InferedParent  // Don't rename
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


        public List<SolidStructureProperty> StructureProperties  // Don't rename
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

        public SolidStructureProperty GetStructurePropertiesForParent(string name)
        {
            return _structureProperties.Find(
                property => property.Parent == name
                );
        }

    }
}