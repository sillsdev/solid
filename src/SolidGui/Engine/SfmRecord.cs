using System;
using System.Collections.Generic;

namespace SolidGui.Engine
{
    public class SfmField
    {
        public string Marker;
        public string Value;
        public int SourceLine;

        public SfmField()
        {
            Marker = String.Empty;
            Value = String.Empty;
            SourceLine = 0;
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