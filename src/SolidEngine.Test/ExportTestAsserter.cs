using System;
using System.IO;
using NUnit.Framework;

namespace Solid.EngineTests
{
    public class ExportTestAsserter : IAsserter
    {
        private readonly string _srcFilePath;
        private readonly string _desFilePath;

        private string _message;

        public string Message
        {
            get { return _message; }
        }

        public ExportTestAsserter(string srcFilePath, string desFilePath)
        {
            _srcFilePath = srcFilePath;
            _desFilePath = desFilePath;
            _message = string.Empty;
        }

        /// <summary>
        /// called by nunit DoAssert()
        /// </summary>
        /// <returns></returns>
        public bool Test()
        {
            Console.WriteLine("Testing {0:s}", _srcFilePath);
            bool retval = true;
            TextReader exportFile = new StreamReader(_desFilePath);
            string masterFilePath = Path.ChangeExtension(_srcFilePath, ".tmpl");
            try
            {
                TextReader masterFile = new StreamReader(masterFilePath);
                string srcLine;
                int lineCount = 0;
                while ((srcLine = exportFile.ReadLine()) != null)
                {
                    lineCount++;
                    string line = masterFile.ReadLine();
                    if (line == null)
                    {
                        _message = string.Format("null fail");
                        retval = false;
                        break;
                    }
                    if (line != srcLine)
                    {
                        _message = string.Format(
                            "\n{0:s}\n\tFile compare fail in line {3:d}:\n\texp: {1:s}\n\tgot: {2:s}",
                            _srcFilePath, line, srcLine, lineCount
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

    };
}