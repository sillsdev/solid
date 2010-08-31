using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
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

        public override bool Matches(object actual)
        {
            string desFilePath = actual as String;
            Console.WriteLine("Testing {0:s}", _srcFilePath);
            bool retval = true;
            TextReader exportFile = new StreamReader(desFilePath);
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
                    

                    if (_line != _srcLine)
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
