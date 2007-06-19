using System;
using System.Collections.Generic;
using System.Text;

namespace SolidGui
{
    public class RecordFilter
    {
        public string Name
        {
            get
            {
                return "hello";
            }
        }
        public string Description
        {
            get
            {
                return "world";
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
