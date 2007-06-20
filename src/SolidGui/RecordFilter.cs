using System;
using System.Collections.Generic;
using System.Text;

namespace SolidGui
{
    public class RecordFilter
    {
        private string _name;
        private string _description;

        public RecordFilter()
        {}

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
        public IEnumerable<int> GetIndicesOfMatchingRecords()
        {
            yield return 1;
            yield return 2;
            yield return 3;
        }
    }
}
