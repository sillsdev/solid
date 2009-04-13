using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using NUnit.Framework;
using SolidEngine;
using SolidEngineTests;

namespace SolidTests
{
    [TestFixture]
    public class ExportLift_Test
    {

        [Test]
        public void ExportSamples_Correct()
        {
            ExportFactory factory = ExportFactory.Singleton();
            string srcDataPath = EngineEnvironment.PathOfBase + "/src/SolidEngine.Test/ExportLift.TestData";
            string tempPath = Path.GetTempPath() + "SolidTest\\";
            if (Directory.Exists(tempPath))
            {
                Directory.Delete(tempPath, true);
            }
            Directory.CreateDirectory(tempPath);
            foreach (string srcFilePath in Directory.GetFiles(srcDataPath, "*.db"))
            {
                string outputFilePath = String.Format("{0}{1}.lift", tempPath, Path.GetFileNameWithoutExtension(srcFilePath));
                IExporter exporter = factory.CreateFromFileFilter("LIFT (*.lift)|*.lift");
                exporter.Export(srcFilePath, outputFilePath);
                Assert.DoAssert(new ExportTestAsserter(srcFilePath, outputFilePath));
            }
        }

    }
}
