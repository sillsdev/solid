using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SolidGui.Engine
{
    public class SfmField
    {
        public static readonly string DefaultTrailing = SolidSettings.NewLine; // s/b "\r\n" on Windows
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
        public string Value;  
        public int SourceLine;  // was here already; could be useful for logging, reporting "error on line __", displaying line number (as of last Recheck), etc. -JMC

        private string _trailing;
        public string Trailing // (for trailing white space); added -JMC 2013-09
        {
            get { return String.IsNullOrEmpty(_trailing) ? DefaultTrailing : _trailing; }
            set { _trailing = value.Contains("\n") ? value : null; } 
        }


        public void SetSplitValue(string val) // JMC: write tests for this
        {
            SetSplitValue(val, " ");
        }

        // Set both the value and the trailing-space value using a single string
        public void SetSplitValue(string val, string separator)
        {
            if (val.Trim() == "")
            {
                // for empty fields, remember the first trailing whitespace (the separator) too
                separator = (separator == "\n") ? SolidSettings.NewLine : separator;
                Value = "";
                Trailing = separator + val;
                return;
            }

            MatchCollection m = Reggie.Matches(val);
            if (m.Count > 2)
            {
                throw new ArgumentException("Bug: a single field shouldn't be able to end in whitespace twice.");
            }

            if (m.Count == 1)
            {
                int i = m[0].Index;
                Value = val.Substring(0, i);
                Trailing = val.Substring(i);
            }
            else  // no trailing whitespace found
            {
                Value = val;
                Trailing = DefaultTrailing;
            }


        }
    
    }

}