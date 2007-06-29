using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using SolidConsole;

namespace SolidGui
{
    public class MarkerRulesPM
    {
        private string _selectedMarker;
        private List<string> _allMarkers;
        private List<SolidMarkerSetting> _markerProperties;
        private string _rulesXmlPath;

        public MarkerRulesPM()
        {
            _markerProperties = new List<SolidMarkerSetting>();
        }

        public void AddProperty(string parent, MultiplicityAdjacency ma)
        {
            SolidMarkerSetting current = GetMarkerProperties(_selectedMarker);

            if(current!= null)
            {
                if (PropertyDoesNotExist(parent))
                {
                    current.StructureProperties.Add(new SolidStructureProperty(parent,ma));
                }
                else
                {
                    SolidStructureProperty structureProperty = GetProperty(parent);

                    structureProperty.Parent = parent;
                    structureProperty.MultipleAdjacent = ma;
                }
            }
        }

        private SolidMarkerSetting GetMarkerProperties(string marker)
        {
            foreach(SolidMarkerSetting mp in _markerProperties)
            {
                if(mp.Marker == marker)
                {
                    return mp;
                }
            }
            SolidMarkerSetting newProperty = new SolidMarkerSetting(marker);
            _markerProperties.Add(newProperty);
            return newProperty;
        }

        public bool PropertyDoesNotExist(string parent)
        {
            SolidMarkerSetting current = GetMarkerProperties(_selectedMarker);

            if (current != null)
            {
                foreach (SolidStructureProperty property in current.StructureProperties)
                {
                    if (property.Parent == parent)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public void WriteRulesToXml()
        {
            XmlSerializer xs = new XmlSerializer(typeof (List<SolidMarkerSetting>));

            using (StreamWriter writer = new StreamWriter(_rulesXmlPath))
            {
                xs.Serialize(writer, _markerProperties);
            }
        }

        public void ReadRulesFromXml()
        {
            XmlSerializer xs = new XmlSerializer(typeof(List<SolidMarkerSetting>));

            try 
            {
                using (StreamReader reader = new StreamReader(_rulesXmlPath))
                {
                        _markerProperties = (List<SolidMarkerSetting>) xs.Deserialize(reader);
                }
            }
            catch
            {
                _markerProperties = new List<SolidMarkerSetting>();
            }
        }

        public string SelectedMarker
        {
            get
            {
                return _selectedMarker;
            }
            set
            {
                _selectedMarker = value;
            }
        }

        public string RulesXmlPath
        {
            set
            {
                _rulesXmlPath = value;
                ReadRulesFromXml();
            }
        }

        public List<string> AllMarkers
        {
            get
            {
                return _allMarkers;
            }
            set
            {
                _allMarkers = value;
            }
        }

        public SolidStructureProperty GetProperty(string parent)
        {
            SolidMarkerSetting current = GetMarkerProperties(_selectedMarker);
            if (current != null)
            {
                foreach (SolidStructureProperty property in current.StructureProperties)
                {
                    if (property.Parent == parent)
                    {
                        return property;
                    }
                }
            }
            return new SolidStructureProperty();
        }

        public void RemoveRule(string description)
        {
            SolidMarkerSetting current = GetMarkerProperties(_selectedMarker);
            if (current != null)
            {
                current.StructureProperties.Remove(GetProperty(description));
            }
        }

        public void SetInferedParent(string inferedParent)
        {
            SolidMarkerSetting current = GetMarkerProperties(_selectedMarker);
            {
                current.InferedParent = inferedParent;
            }
        }

        public string GetInferedParent()
        {
            return GetMarkerProperties(_selectedMarker).InferedParent;
        }
    }
}
