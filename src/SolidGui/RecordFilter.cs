using System;
using System.Collections.Generic;
using System.Text;
using SolidEngine;

namespace SolidGui
{

    public class SolidReportRecordFilter : RecordFilter
    {
        SolidReport _report;

        public SolidReportRecordFilter(Dictionary d, SolidReport report) :
            base(d, "Solid Report")
        {
            _report = report;
        }

    }
    /*
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
    */

    public class AllRecordFilter : RecordFilter
    {
        public AllRecordFilter(Dictionary dictionary) :
            base(dictionary, "All")
        {
        }

    }

    
    public class NullRecordFilter : RecordFilter
    {
        public NullRecordFilter()
            : base(null, "None")
        {
        }
    }
     

    public class RecordFilter : RecordManagerDecorator
    {
        protected string _name;
      //  protected List<string> _descriptions;
      //  protected List<int> _indexesOfRecords;

        RecordManager _d;

        public RecordFilter(RecordManager d, string name) :
            base(d)
        {
            _d = d;
            _name = name;
        }

        public override string ToString()
        {
            return _name + " (" + Count + ")";
        }

        public string Name
        {
            get { return _name; }
        }
        
        public virtual string Description(int index)
        {
            return "unknown description";
        }

    }

}
