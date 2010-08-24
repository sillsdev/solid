using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SolidGui.Model
{
    public class SfmDataSet : IEnumerable<SfmLexEntry>
    {
        
        public int NumberOfEntries;


        public IEnumerator<SfmLexEntry> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
