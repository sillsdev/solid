// Copyright (c) 2007-2014 SIL International
// Licensed under the MIT license: opensource.org/licenses/MIT

using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;

namespace SolidGui.Setup
{
    class EncodingChecker
    {
        /*
        private string _encoding;

        public EncodingChecker(string encoding)
        {
            _encoding = encoding;
        }
        */

        public static string ReadLines(string filePath, Encoding encoding, int lines)
        {
            // make it strict:
            encoding = Encoding.GetEncoding(encoding.CodePage, EncoderFallback.ExceptionFallback, DecoderFallback.ExceptionFallback);

            var sb = new StringBuilder();
            using (var r = new StreamReader(filePath, encoding, false))
            {
                try
                {
                    for (int i = 0; i < lines; i++)
                    {
                        string ss = r.ReadLine();
                        if (ss == null) break;
                        sb.AppendLine(ss);
                    }
                    string s = r.ReadToEnd();

                }
                catch (Exception e)
                {
                    //swallow
                    sb.AppendLine("\nERROR: " + e.ToString());
                }
            }
            return sb.ToString();
        }
        

        public static bool CanBeReadAs(string filePath, Encoding encoding)
        {
            // make it strict:
            encoding = Encoding.GetEncoding(encoding.CodePage, EncoderFallback.ExceptionFallback, DecoderFallback.ExceptionFallback);

            using (var r = new StreamReader(filePath, encoding, false))
            {
                try
                {
                    for (int i = 0; i < 88; i++)
                    {
                        string ss = r.ReadLine();
                        
                    }
                    string s = r.ReadToEnd();

                }
                catch (Exception)
                {
                    //swallow
                    return false;
                }
            }
            return true;
        }

    }
}
