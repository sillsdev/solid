// Copyright (c) 2007-2014 SIL International
// Licensed under the MIT license: opensource.org/licenses/MIT

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SolidGui.Engine
{

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