// Copyright (c) 2007-2014 SIL International
// Licensed under the MIT license: opensource.org/licenses/MIT

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolidGui.Engine
{
    /// <summary>
    /// Stores info about what the auto-fix and auto-migration code did while opening a .solid file.
    /// Intended to be displayed to the user immediately after opening a file, if any settings existed and were tweaked.
    /// Note: Unknown elements will still be dropped silently, and tweaking the order of XML elements will not be reported.
    /// </summary>
    public class SettingsFileReport  // Feature #1298 : http://projects.palaso.org/issues/1298 -created by JMC July 2014
    {
       
        private StringBuilder _reportBuilder = new StringBuilder();

        //private int _count

        public string GetReport()  // should we also put a brief summary into an overloaded ToString()? -JMC
        {
            return _reportBuilder.ToString();
        } 

        public void AppendLine(string s)
        {
            _reportBuilder.AppendLine(s + ""); // + any specifically counted/summarized issues
        }

    }
}
