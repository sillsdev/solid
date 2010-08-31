using System;
using System.Collections.Generic;

namespace SolidGui.Engine
{
    // TODO Should probably make these nested classes of SfmRecordReader CP 2010-08
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