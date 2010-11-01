using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using NUnit.Framework.Constraints;

namespace SolidGui.Tests.Export
{

    public class ExportFileTestConstraint : NUnit.Framework.Constraints.Constraint
    {
        private string _templateFilePath;
        private string _message;
        private string _templateLine;
        private string _outputLine;

        public ExportFileTestConstraint(string filePath)
        {
            _templateFilePath = filePath;
        }

        public override bool Matches(object value)
        {
            string outputFilePath = value as string;
            TextReader outputFile = new StreamReader(outputFilePath);
            //Console.WriteLine("Testing {0:s}", _templateFilePath);
            bool retval = true;
            
            try
            {
                TextReader templateFile = new StreamReader(_templateFilePath);
                
                int lineCount = 0;
                while ((_outputLine = outputFile.ReadLine()) != null)
                {
                    lineCount++;
                    _templateLine = templateFile.ReadLine();
                    if (_templateLine == null)
                    {
                        _message = string.Format("null fail");
                        retval = false;
                        break;
                    }
                    
                    if (_templateLine.Contains("*"))
                    {
                        string patternString = _templateLine.Replace("*", ".*");
                        var pattern = new Regex(patternString);
                        Match match = pattern.Match(_outputLine);
                        if (!match.Success)
                        {
                            retval = false;
                            break;
                        }
                    }
                    else if (_templateLine != _outputLine)
                    {
                        _message = string.Format(
                            "\n{0:s}\n\tFile compare fail in _line {3:d}:\n\texp: {1:s}\n\tgot: {2:s}",
                            _templateFilePath, _templateLine, _outputLine, lineCount
                            );
                        retval = false;
                        break;
                    }
                }
                templateFile.Close();
            }
            catch (IOException e)
            {
                _message = string.Format("\n{0:s}\n\t{1:s}", _templateFilePath, e.Message);
                retval = false;
            }
            outputFile.Close();
            return retval;
        }

        public override void WriteDescriptionTo(MessageWriter writer)
        {
            writer.DisplayStringDifferences(_templateLine, _outputLine, 0, false, true);
        }
    }
}
