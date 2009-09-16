using System;
using System.Collections.Generic;
using System.Text;

namespace Solid.Engine
{
    public class SfmField
    {
        public string key;
        public string value;
        public int sourceLine;
        public int endLine;

        public SfmField()
        {
            key = String.Empty;
            value = String.Empty;
            sourceLine = 0;
            endLine = 0;
        }
    }

    public class SfmRecord : List<SfmField>
    {
        public SfmRecord()
            :
            base()
        {
        }

        public SfmRecord(SfmRecord rhs)
            :
            base(rhs)
        {
        }

    }


}
