using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using NUnit.Framework;
using SolidEngine;

namespace SolidTests
{
    public class TestFileAsserter : IAsserter
    {
        private readonly string _srcFilePath;

        private string _message;

        public string Message
        {
            get { return _message; }
        }

        public TestFileAsserter(string srcFilePath)
        {
            _srcFilePath = srcFilePath;
            _message = string.Empty;
        }

        public bool Test()
        {
            Console.WriteLine("Testing {0:s}", _srcFilePath);
            bool retval = true;
            TextReader exportFile = new StreamReader(_srcFilePath);
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
                        _message = string.Format("\n{0:s}\n\tFile compare fail in line {3:d}:\n\texp: {1:s}\n\tgot: {2:s}",
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

    [TestFixture]
    public class ExportLift_Test
    {

        [Test]
        public void ExportSamples_Correct()
        {
            ExportFactory factory = ExportFactory.Singleton();
            string path = EngineEnvironment.PathOfBase + "/src/SolidEngine.Test/ExportLift.TestData";
            foreach (string srcFile in Directory.GetFiles(path, "*.db"))
            {
                IExporter exporter = factory.CreateFromFileFilter("LIFT (*.lift)|*.lift");
                string desFile = Path.ChangeExtension(srcFile, ".lift");
                exporter.Export(srcFile, desFile);
                Assert.DoAssert(new TestFileAsserter(desFile));
            }
        }

    }
}
