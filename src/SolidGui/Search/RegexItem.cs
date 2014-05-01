using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SolidGui.Search
{
    public class RegexItem
    {
        public string FindContext = "";
/*
        {
            get;
            set
            {
                this.Double = true;
                this.FindContext = value;
            }
        }
 */ 
        public string ReplaceContext = "";
        public Regex ReggieContext { get; private set; }
        public string Find { get; private set; }
        public Regex Reggie { get; private set; }
        public string Replace = "";

        public bool Double { get; private set; }
        public bool CaseSensitive { get; private set; }
        public string Title = "";
        public string HelpMessage = "";

        public static Regex MakeReggie(string find, bool cs)
        {
            RegexOptions opt = RegexOptions.Multiline | RegexOptions.Compiled;
            if (!cs)
            {
                opt = RegexOptions.IgnoreCase | opt;
            }
            return new Regex(find, opt);  
        }

        public void SetFind(string find, bool cs)
        {
            Find = find;
            CaseSensitive = cs;
            Double = false;
            Reggie = MakeReggie(find, cs);
        }

        public void SetFindContext(string findContext, string find, bool cs)
        {
            Double = true; // we expect this to be called after SetFind()
            FindContext = findContext;
            ReggieContext = MakeReggie(findContext, cs);
        }

        // Utility regexes follow. (These could perhaps be stored in a config file.) -JMC Apr 2014

        /// <summary>
        /// Utility regex.
        /// </summary>
        /// <returns></returns>
        public static RegexItem GetSplitOnSemicolon()
        {
            string fc = @"^\\(re) (.+)$";
            string rc = @"\\\1 \2";
            string f = @"[ ]*;[ ]*";
            string r = @"\n\\re ";
            var reg = new RegexItem();
            reg.Find = f;
            reg.Replace = r;
            reg.Double = true;
            reg.FindContext = fc;
            reg.ReplaceContext = rc;
            reg.HelpMessage =
                "This will split one field (re) in one pass. For multiple fields,\r\n" +
                "you can instead run a (non-double) regex multiple times:\r\n" +
                @"  ^\\(re|va|cf) (.+)[ ]*;[ ]*" + "\r\n" +
                @"  \\\\1 \2\r\n\\\1 " + "\r\n";
            return reg;
        }

        /// <summary>
        /// Utility regex.
        /// </summary>
        /// <returns></returns>
        public static RegexItem GetUnwrap()
        {
            string f = @"[ ]*[\n]+[ \n]*(?=[^\n\\])";
                   f = @"[ ]*[\n][ ]*(?=[^\n\\])";
            string r = @" ";
            var reg = new RegexItem();
            reg.Find = f;
            reg.Replace = r;
            reg.HelpMessage =
                "From a given line, this will use one space to replace any trailing spaces, \n" +
                "one newline, then any leading spaces, iff the next line isn't a different field. \n" +
                "WARNING: If any fields begin with indentation (not \\), use Trim to remove that first.";
            return reg;
        }

        /// <summary>
        /// Utility regex.
        /// </summary>
        /// <returns></returns>
        public static RegexItem GetTrim()
        {
            string f = @"(^[ \t]+|[ \t]+$)";
            string r = @"";
            var reg = new RegexItem();
            reg.Find = f;
            reg.Replace = r;
            reg.HelpMessage =
                "Finds and removes whitespace beginning/ending a line.\n";
            return reg;
        }

        /// <summary>
        /// Utility regex.
        /// </summary>
        /// <returns></returns>
        public static RegexItem GetDeleteFields()
        {
            string f = @"\\(deOld|bad|delme).*\n([^\n\\].*\n)*";
            string r = @"";
            var reg = new RegexItem();
            reg.Find = f;
            reg.Replace = r;
            reg.HelpMessage =
                "Will globally delete all the mentioned fields, \n" +
                "WARNING: you should first unwrap hard-wrapped fields, \n" + 
                "or at least make sure none of them includes any totally \n" +
                "blank lines followed by data.";
            return reg;
        }

    }
}
