using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace SolidGui
{
    class DummyProcessor
    {
        private List<Rule> _rules;
        private string _reportPath;
        private List<RecordFilter> _filters;

        public DummyProcessor()
        {
            _rules = new List<Rule>();
            _reportPath = @"C:\Documents and Settings\WeSay\Desktop\Solid\trunk\data\report.xml";
            _filters = new List<RecordFilter>();
        }

        public void ReadRules(string rulesPath)
        {
            XmlSerializer xs = new XmlSerializer(typeof(List<Rule>));
            using (StreamReader reader = new StreamReader(rulesPath))
            {
                _rules = (List<Rule>) xs.Deserialize(reader);
            }
        }

        public void ProcessDictionary(List<Record> masterRecordList)
        {
            _filters.Clear();

            foreach (Rule rule in _rules)
            {
                int index = 0;
                List<int> indexesOfRecords = new List<int>();
                
                foreach (Record record in masterRecordList)
                {
                    if(rule.Required == true && record.Value.IndexOf(rule.Marker) == -1)
                    {
                        indexesOfRecords.Add(index);
                    }

                    index++;
                }
                if(indexesOfRecords.Count > 0)
                {
                    _filters.Add(new RecordFilter("Violations of rule "+rule.Name,
                                                   "These Records don't have a " + rule.Marker + " marker",
                                                   indexesOfRecords));
                }
            }

            WriteReport();
        }

        private void WriteReport()
        {
            XmlSerializer xs = new XmlSerializer(typeof (List<RecordFilter>));

            using(StreamWriter writer = new StreamWriter(_reportPath,false))
            {
                xs.Serialize(writer, _filters);
            }
        }
    }
}
