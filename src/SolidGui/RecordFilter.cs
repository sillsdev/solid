using System;
using System.Collections.Generic;
using System.Text;

namespace SolidGui
{

    public class RegExRecordFilter : RecordFilter
    {
        private readonly bool _matchWhenNotFound;
        private string _pattern;
        public RegExRecordFilter(string name, string pattern, bool matchWhenNotFound)
        {
            _matchWhenNotFound = matchWhenNotFound;
            _name = name;
            _pattern = pattern;
        }
        public RegExRecordFilter(string name, string pattern):this(name,pattern,false)
        {
            _name = name;
            _pattern = pattern;
        }

        public override IEnumerable<int> GetIndicesOfMatchingRecords(IList<Record> records)
        {
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(_pattern, 
                System.Text.RegularExpressions.RegexOptions.Compiled & System.Text.RegularExpressions.RegexOptions.Singleline);
           
            for (int i = 0; i < records.Count; i++)
            {
                bool match = regex.IsMatch(records[i].Value);
                if(match == !_matchWhenNotFound)
                {
                    yield return i;
                }
            }
        }

        public override string Description
        {
            get
            {
                return String.Format("Records that match '{0}'",_pattern);
            }
        }
    }


    public class AllRecordFilter : RecordFilter
    {
        public AllRecordFilter()
            : base("All", "These are all the records in the dictionary",null)
        {
        }

        public override IEnumerable<int> GetIndicesOfMatchingRecords(IList<Record> records)
        {
            for(int i = 0 ; i <records.Count ; i++)
            {
                yield return i;
            }
        }
    }

    public class NullRecordFilter : RecordFilter
    {
        public NullRecordFilter()
            : base("None", "Should be empty",new List<int>())
        {
        }
    }

    public class RecordFilter
    {
        protected string _name;
        protected string _description;
        protected List<int> _indexesOfRecords;

        public RecordFilter()
        {
            _name = "";
            _description = "";
            _indexesOfRecords = new List<int>();
        }

        public RecordFilter(string name, string description, List<int> indexes)
        {
            _name = name;
            _description = description;
            _indexesOfRecords = indexes;

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
        public virtual string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
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
        public virtual IEnumerable<int> GetIndicesOfMatchingRecords(IList<Record> records)
        {
            return IndexesOfRecords;
        }
    }
}
