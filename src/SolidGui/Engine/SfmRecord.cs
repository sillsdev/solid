using System;
using System.Collections.Generic;

namespace SolidGui.Engine
{
    public class SfmField
    {
        public string Marker;
        public string Value;  // JMC: on set, auto-chop1 any trailing white space and move it to the beginning of Trailing
        // JMC: add this: public string Trailing (for trailing white space)
        public int SourceLine;  // not yet used but could be useful for logging, reporting "error on line __", displaying line number (as of last Recheck), etc. -JMC

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