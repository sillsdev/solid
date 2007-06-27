using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace SolidGui
{
    public class MarkerRulesPM
    {
        private List<string> _allMarkers;
        private List<Rule> _rules;
        private string _rulesXmlPath;

        public MarkerRulesPM()
        {
            _rules = new List<Rule>();
        }

        public void AddRule(string name, string marker, bool required)
        {
            if(!MarkerAlreadyHasRule(marker))
            {
                _rules.Add(new Rule(name, marker, required));
            }
            else
            {
                Rule rule = GetRule(marker);
                rule.Required = required;
            }
        }

        public bool MarkerAlreadyHasRule(string marker)
        {
            foreach (Rule rule in _rules)
            {
                if (rule.Marker == marker)
                {
                    return true;
                }
            }
            return false;
        }

        public void WriteRulesToXml()
        {
            XmlSerializer xs = new XmlSerializer(typeof (List<Rule>));

            using (StreamWriter writer = new StreamWriter(_rulesXmlPath))
            {
                xs.Serialize(writer, _rules);
            }
        }

        public void ReadRulesFromXml()
        {
            XmlSerializer xs = new XmlSerializer(typeof(List<Rule>));

            if (File.Exists(_rulesXmlPath))
            {
                using (StreamReader reader = new StreamReader(_rulesXmlPath))
                {
                    _rules = (List<Rule>) xs.Deserialize(reader);
                }
            }
            else
            {
                _rules = new List<Rule>();   
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

        public Rule GetRule(string marker)
        {
            foreach (Rule rule in _rules)
            {
                if (rule.Marker == marker)
                {
                    return rule;
                }
            }
            return new Rule(marker);
        }
    }
}
