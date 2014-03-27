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
        public SearchResult ContextFound = null;
        public string Find { get; private set; }
        public Regex Reggie { get; private set; }
        public string Replace = "";

        public bool Double { get; private set; }
        public bool CaseSensitive { get; private set; }
        public string Title = "";
        public string HelpMessage = "";

        public static Regex makeReggie(string find, bool cs)
        {
            RegexOptions opt = RegexOptions.Multiline | RegexOptions.Compiled;
            if (!cs)
            {
                opt = RegexOptions.IgnoreCase | opt;
            }
            return new Regex(find, opt);  
        }

        public void setFind(string find, bool cs)
        {
            Find = find;
            CaseSensitive = cs;
            Double = false;
            Reggie = makeReggie(find, cs);
        }

        public void setFind(string findContext, string find, bool cs)
        {
            Double = true;
            setFind(find, cs);
            FindContext = findContext;
            ReggieContext = makeReggie(findContext, cs);
        }


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
        public static RegexItem GetUnwrap()
        {
            string f = @"[ ]*[\n]+[ \n]*(?=[^\n\\])";
            string r = @" ";
            var reg = new RegexItem();
            reg.Find = f;
            reg.Replace = r;
            reg.HelpMessage =
                "From a given line, this will remove any trailing spaces, one newline,\r\n" +
                "and any leading spaces, replacing them with a single space.";
            return reg;
        }

        public static RegexItem GetDeleteFields()
        {
            string f = @"\\(deOld|bad|delme).*\n([^\\].*\n)*";
            string r = @"";
            var reg = new RegexItem();
            reg.Find = f;
            reg.Replace = r;
            reg.HelpMessage =
                "Will globally delete all the mentioned fields, \r\n" +
                "WARNING: you should first unwrap hard-wrapped fields,\r\n" + 
                "or at least make sure none of them includes any totally blank lines.";
            return reg;
        }

    }
}
