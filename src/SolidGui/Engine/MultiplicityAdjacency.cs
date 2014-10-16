// Copyright (c) 2007-2014 SIL International
// Licensed under the MIT license: opensource.org/licenses/MIT

namespace SolidGui.Engine
{
    public enum MultiplicityAdjacency  // don't rename these values unless you're willing to create a new version of .solid -JMC
    {
        Once,
        MultipleApart,
        MultipleTogether
    }

    public static class Extensions  //added so our columns can summarize more info. -JMC Feb 2014
    {
        public static string Abbr(this MultiplicityAdjacency m)
        {
            string s="";
            switch (m)
            {
                case MultiplicityAdjacency.Once:
                    s = "1";
                    break;
                case MultiplicityAdjacency.MultipleTogether:
                    s = "2";
                    break;
                case MultiplicityAdjacency.MultipleApart:
                    s = "2..2";
                    break;
            }
            return s;
        }
    }


}