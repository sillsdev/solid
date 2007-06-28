using System.Collections;
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
            if(RuleNameDoesNotExist(name))
            {
                _rules.Add(new Rule(name, marker, required));
            }
            else
            {
                Rule rule = GetRule(name);
                rule.Required = required;
            }
        }

        public bool RuleNameDoesNotExist(string name)
        {
            foreach (Rule rule in _rules)
            {
                if (rule.Name == name)
                {
                    return false;
                }
            }
            return true;
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

            try 
            {
                using (StreamReader reader = new StreamReader(_rulesXmlPath))
                {
                        _rules = (List<Rule>) xs.Deserialize(reader);
                }
            }
            catch
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

        public Rule GetRule(string name)
        {
            foreach (Rule rule in _rules)
            {
                if (rule.Name == name)
                {
                    return rule;
                }
            }
            return new Rule();
        }

        public List<string> GetAllRuleNames()
        {
            List<string> allNames = new List<string>();
            foreach(Rule rule in _rules)
            {
                allNames.Add(rule.Name);
            }
            return allNames;
        }

        public void RemoveRule(string name)
        {
            _rules.Remove(GetRule(name));
        }
    }
}
