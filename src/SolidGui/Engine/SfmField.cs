using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SolidGui.Engine
{
    public class SfmField
    {
        public static readonly string DefaultTrailing = SolidSettings.NewLine; // "\r\n";  //JMC: wouldn't need to be readonly; might be fun to try "\n" now and then, now that it's a central setting
        private static string Pat = @"[\t \r\n]+$";
        private static Regex Reggie = new Regex(
            Pat, RegexOptions.Compiled | RegexOptions.CultureInvariant);

        public SfmField()
        {
            Marker = String.Empty;
            Value = String.Empty;
            _trailing = DefaultTrailing;
            SourceLine = 0;
        }

        public string Marker;
        public string Value;  // JMC: on set, auto-chop1 any trailing white space and move it to the beginning of Trailing
        public int SourceLine;  // was here already; could be useful for logging, reporting "error on line __", displaying line number (as of last Recheck), etc. -JMC

        private string _trailing;
        public string Trailing // (for trailing white space); added -JMC 2013-09
        {
            get { return String.IsNullOrEmpty(_trailing) ? DefaultTrailing : _trailing; }
            set { _trailing = value; }
        }

        // Set both the value and the trailing-space value using a single string
        public void SetSplitValue(string val)  // JMC: write tests for this
        {
            MatchCollection m = Reggie.Matches(val);
            if (m.Count > 0)
            {
                int i = m[0].Index;
                Value = val.Substring(0, i);
                Trailing = val.Substring(i);
            }
            else
            {
                Value = val;
                Trailing = DefaultTrailing;
            }

            if (m.Count > 2)
            {
                throw new ArgumentException("Bug: a single field shouldn't be able to end in whitespace twice.");
            }

        }
    
    }

}