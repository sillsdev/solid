// Copyright (c) 2007-2014 SIL International
// Licensed under the MIT license: opensource.org/licenses/MIT

namespace SolidGui.Engine
{
    public enum MultiplicityAdjacency
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
                    s = "i";
                    break;
                case MultiplicityAdjacency.MultipleTogether:
                    s = "ii";
                    break;
                case MultiplicityAdjacency.MultipleApart:
                    s = "i..ii";
                    break;
            }
            return s;
        }
    }


}