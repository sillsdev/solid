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

        public override IEnumerable<int> GetIndicesOfMatchingRecords(IList<string> records)
        {
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(_pattern, 
                System.Text.RegularExpressions.RegexOptions.Compiled & System.Text.RegularExpressions.RegexOptions.Singleline);
           
            for (int i = 0; i < records.Count; i++)
            {
                bool match = regex.IsMatch(records[i]);
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
            : base("All", "These are all the records in the dictionary")
        {
        }

        public override IEnumerable<int> GetIndicesOfMatchingRecords(IList<string> records)
        {
            for (int i = 0; i < records.Count; i++)
            {
                yield return i;   
            }
        }
    }

    public class NullRecordFilter : RecordFilter
    {
        public NullRecordFilter()
            : base("None", "Should be empty")
        {
        }

        public override IEnumerable<int> GetIndicesOfMatchingRecords(IList<string> records)
        {
            for (int i = 0; false;)
            {
                yield return i;
            }
        }
    }

    public abstract class RecordFilter
    {
        protected string _name;
        protected string _description;

        public RecordFilter()
        {
        }

        public RecordFilter(string name, string description)
        {
            _name = name;
            _description = description;
        }

        public string Name
        {
            get
            {
                return _name;
            }
        }
        public virtual string Description
        {
            get
            {
                return _description;
            }
        }

        public abstract IEnumerable<int> GetIndicesOfMatchingRecords(IList<string> records);

   
    }
}
