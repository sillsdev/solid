using System;
using System.Collections.Generic;
using System.Text;

namespace SolidGui
{
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

    public class RecordFilter
    {
        private string _name;
        private string _description;

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
        public string Description
        {
            get
            {
                return _description;
            }
        }
        public virtual IEnumerable<int> GetIndicesOfMatchingRecords(IList<string> records)
        {
            yield return 2;
            yield return 3;
        }

   
    }
}
