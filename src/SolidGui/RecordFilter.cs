using System;
using System.Collections.Generic;
using System.Text;
using SolidEngine;

namespace SolidGui
{

    public class SolidReportRecordFilter : RecordFilter
    {
        SolidReport _report;

        public SolidReportRecordFilter(SolidReport report) :
            base("Solid Report")
        {
            _report = report;
        }

        public static SolidReportRecordFilter Create()
        {
            return new SolidReportRecordFilter(null);
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
        Dictionary _dictionary;


        public AllRecordFilter(Dictionary dictionary) :
            base("All")
        {
            _dictionary = dictionary;
        }

        public override int RecordCount
        {
            get
            {
                return _dictionary.Count;
            }
        }
        /*
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
         */ 
    }

    public class NullRecordFilter : RecordFilter
    {
        public NullRecordFilter()
            : base("None")
        {
        }
    }

    public class RecordFilter
    {
        protected string _name;
    //    protected List<string> _descriptions;
      //  protected List<int> _indexesOfRecords;

        protected RecordFilter(string name)
        {
            _name = name;
        //    _descriptions = new List<string>();
        //    _indexesOfRecords = new List<int>();
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
            /*
            set
            {
                _name = value;
            }
            */
        }
        
        public virtual string Description(int index)
        {
            return "unknown description";
        }

        public virtual List<int> IndexesOfRecords
        {
            get
            {
                return new List<int>();
            }
            /*
            set
            {
                _indexesOfRecords = value;
            }
            */
        }
        
        public virtual int RecordCount
        {
            get
            {
                return 0;
            }
        }
    }
}
