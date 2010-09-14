using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using NUnit.Framework.Constraints;

namespace SolidGui.Tests.Export
{

    public class ExportTestConstraint : NUnit.Framework.Constraints.Constraint
    {
        private string _srcFilePath;
        private string _message;
        private string _line;
        private string _srcLine;

        public ExportTestConstraint(string filePath)
        {
            _srcFilePath = filePath;
        }

        public override bool Matches(object value)
        {
            string outputFilePath = value as string;
            TextReader exportFile = new StreamReader(outputFilePath);
            Console.WriteLine("Testing {0:s}", _srcFilePath);
            bool retval = true;
            
            string masterFilePath = Path.ChangeExtension(_srcFilePath, ".tmpl");
            try
            {
                TextReader masterFile = new StreamReader(masterFilePath);
                
                int lineCount = 0;
                while ((_srcLine = exportFile.ReadLine()) != null)
                {
                    lineCount++;
                    _line = masterFile.ReadLine();
                    if (_line == null)
                    {
                        _message = string.Format("null fail");
                        retval = false;
                        break;
                    }
                    
                    if (_srcLine.Contains("*"))
                    {
                        string patternString = _srcLine.Replace("*", ".*");
                        var pattern = new Regex(patternString);
                        Match match = pattern.Match(_line);
                        if (!match.Success)
                        {
                            retval = false;
                            break;
                        }
                    }
                    else if (_line != _srcLine)
                    {
                        _message = string.Format(
                            "\n{0:s}\n\tFile compare fail in _line {3:d}:\n\texp: {1:s}\n\tgot: {2:s}",
                            _srcFilePath, _line, _srcLine, lineCount
                            );
                        retval = false;
                        break;
                    }
                }
                masterFile.Close();
            }
            catch (IOException e)
            {
                _message = string.Format("\n{0:s}\n\t{1:s}", _srcFilePath, e.Message);
                retval = false;
            }
            exportFile.Close();
            return retval;
        }

        public override void WriteDescriptionTo(MessageWriter writer)
        {
            writer.DisplayStringDifferences(_line, _srcLine, 0, false, true);
        }
    }
}
