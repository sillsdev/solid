using System;
using System.Collections.Generic;
using System.Text;
using SolidEngine;

namespace SolidGui
{

    public class SolidReportRecordFilter : RecordFilter
    {
        SolidReport _report;

        public SolidReportRecordFilter(SolidReport report)
        {
            _report = report;
            Name = "Solid Report";

        }

        public static SolidReportRecordFilter Create()
        {
            return new SolidReportRecordFilter(null);
        }
    }

    public class RegExRecordFilter : RecordFilter
    {
        private readonly bool _matchWhenNotFound;
        private string _pattern;
        public RegExRecordFilter(string name, string pattern, bool matchWhenNotFound,List<Record> records)
        {
            _descriptions = new List<string>();
            _matchWhenNotFound = matchWhenNotFound;
            _name = name;
            _pattern = pattern;
            _indexesOfRecords = GetIndicesOfMatchingRecords(records);
            
        }
        public RegExRecordFilter(string name, string pattern, List<Record> records):this(name,pattern,false,records)
        {
            _name = name;
            _pattern = pattern;
        }

        protected override List<int> GetIndicesOfMatchingRecords(List<Record> records)
        {
            _indexesOfRecords.Clear();

            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(_pattern, 
                System.Text.RegularExpressions.RegexOptions.Compiled & System.Text.RegularExpressions.RegexOptions.Singleline);

            for (int i = 0; i < records.Count; i++)
            {
                bool match = regex.IsMatch(records[i].Value);
                if(match == !_matchWhenNotFound)
                {
                    _indexesOfRecords.Add(i);
                    _descriptions.Add(String.Format("Records that match '{0}'", _pattern));
                }
            }
            return _indexesOfRecords;
        }

        public override List<string> Descriptions
        {
            get
            {
                return _descriptions;
            }
        }
    }


    public class AllRecordFilter : RecordFilter
    {
        public AllRecordFilter(List<Record> records)
        {
            _name = "All";
            _descriptions = new List<string>();
            _indexesOfRecords = GetIndicesOfMatchingRecords(records);
        }

        protected override List<int> GetIndicesOfMatchingRecords(List<Record> records)
        {
            _indexesOfRecords.Clear();

            for(int i = 0 ; i <records.Count ; i++)
            {
                _descriptions.Add("These are all the records in the dictionary");
                _indexesOfRecords.Add(i);
            }
            return _indexesOfRecords;
        }
    }

    public class NullRecordFilter : RecordFilter
    {
        public NullRecordFilter()
            : base("None", new List<string>(), new List<int>())
        {
        }
    }

    public class RecordFilter
    {
        protected string _name;
        protected List<string> _descriptions;
        protected List<int> _indexesOfRecords;

        public RecordFilter()
        {
            _name = "";
            _descriptions = new List<string>();
            _indexesOfRecords = new List<int>();
        }

        public RecordFilter(string name, List<string> descriptions, List<int> indexes)
        {
            _name = name;
            _descriptions = descriptions;
            _indexesOfRecords = indexes;

        }

        public override string ToString()
        {
            return _name + " (" + RecordCount + ")";
        }

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }
        public virtual List<string> Descriptions
        {
            get
            {
                return _descriptions;
            }
            set
            {
                _descriptions = value;
            }
        }
        public List<int> IndexesOfRecords
        {
            get
            {
                return _indexesOfRecords;
            }
            set
            {
                _indexesOfRecords = value;
            }
        }
        protected virtual List<int> GetIndicesOfMatchingRecords(List<Record> records)
        {
            return IndexesOfRecords;
        }

        public int RecordCount
        {
            get
            {
                return _indexesOfRecords.Count;
            }
        }
    }
}
