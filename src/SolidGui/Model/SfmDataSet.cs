using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
#if false
namespace SolidGui.Model
{
    public class SfmDataSet : IEnumerable<SfmLexEntry>
    {

        
        public int NumberOfEntries;


        public IEnumerator<SfmLexEntry> GetEnumerator()
        {

        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public static void CreateFromFilePath(string filePath)
        {
            throw new NotImplementedException();
        }
    }
}
#endif
