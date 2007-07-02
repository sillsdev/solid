using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using SolidEngine;

namespace SolidGui
{
    class DummyProcessor
    {
        private List<SolidStructureProperty> _rules;
        private string _reportPath;
        private List<RecordFilter> _filters;

        public DummyProcessor()
        {
            _rules = new List<SolidStructureProperty>();
            _reportPath = @"C:\Documents and Settings\WeSay\Desktop\Solid\trunk\data\report.xml";
            _filters = new List<RecordFilter>();
        }

        public void ReadRules(string rulesPath)
        {
            XmlSerializer xs = new XmlSerializer(typeof(List<SolidStructureProperty>));
            try
            {
                using (StreamReader reader = new StreamReader(rulesPath))
                {
                    _rules = (List<SolidStructureProperty>) xs.Deserialize(reader);
                }
            }
            catch
            {
                _rules = new List<SolidStructureProperty>();
            }
        }

        public void ProcessDictionary(List<Record> masterRecordList)
        {
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
