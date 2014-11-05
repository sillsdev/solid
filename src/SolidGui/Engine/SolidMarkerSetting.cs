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
        
        public SolidMarkerSetting() : this("", false) // defaults to legacy rather than unicode (legacy is actually safer) -JMC
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
            Comment = "";
        }

        [XmlElement(ElementName = "Marker", Order = 0)]
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

        [XmlElement(ElementName = "WritingSystem", Order = 1)]
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

        [XmlElement(ElementName = "Unicode", Order = 2)]
        public bool Unicode 
        {
            get { return _isUnicode; }
            set { _isUnicode = value; }
        }

        public void SetMappingConcept(MappingType mappingType, string id)
        {
            Mappings[(int) mappingType] = id;
        }

        public string GetMappingConceptId(MappingType mappingType)
        {
            return Mappings[(int) mappingType];
        }

        public bool IsAnAllowedParent(string marker)
        {
            SolidStructureProperty result = _structureProperties.Find(item => item.Parent == marker);
            return result != null;
        }

        public override string ToString()
        {
            return Marker;
        }

        [XmlArray("StructureProperties", Order = 3)]  
        [XmlArrayItem("SolidStructureProperty", typeof(SolidStructureProperty))]
        public List<SolidStructureProperty> StructureProperties  // don't rename
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
                
        [XmlElement("InferedParent", Order = 4)]
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

        [XmlElement("Comments", Order = 6)]
        public string Comment { get; set; }

        [XmlArray("Mappings", Order = 7)]
        [XmlArrayItem("string", typeof(string))]
        public string[] Mappings { get; set; } 

        public SolidStructureProperty GetStructurePropertiesForParent(string name)
        {
            return _structureProperties.Find(
                property => property.Parent == name
                );
        }

    }
}