using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace SolidGui
{
    public class ReportReader
    {
        private XmlDocument _report;
        private XmlNodeList _recordFilterNodes;
        private int _currentRecordFilter;
        private XmlNode _recordFilterNameNode;
        private XmlNode _recordFilterDescriptionNode;
        private XmlNodeList _recordFilterIndexes;

        public ReportReader()
        {
            _report = new XmlDocument();
        }

        public void Load(string path)
        {
            try
            {
                _report.Load(path);
            }
            catch
            {
                
            }
            _recordFilterNodes = _report.GetElementsByTagName("recordfilter");
            _currentRecordFilter = -1;
        }

        public bool NextRecordFilter()
        {
            _currentRecordFilter++;
            if(_currentRecordFilter >= _recordFilterNodes.Count)
            {
                return false;
            }

            _recordFilterNameNode = _recordFilterNodes[_currentRecordFilter].FirstChild;
            _recordFilterDescriptionNode = _recordFilterNameNode.NextSibling;
            _recordFilterIndexes = _recordFilterDescriptionNode.NextSibling.ChildNodes;

            return true;
            
        }

        public String Name
        {
            get
            {
                return _recordFilterNameNode.InnerText;
            }
        }

        public String Description
        {
            get
            {
                return _recordFilterDescriptionNode.InnerText;
            }
        }

        public List<int> Indexes
        {
            get
            {
                List<int> indexes = new List<int>();

                for(int i = 0; i<_recordFilterIndexes.Count; i++)
                {
                    XmlNode index = _recordFilterIndexes[i];
                    indexes.Add(int.Parse(index.InnerText));
                }
                return indexes;
            }
        }


        
    }
}
