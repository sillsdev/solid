using System;
using System.IO;
using NUnit.Framework;
using SolidGui.Engine;
using SolidGui.Export;


namespace SolidGui.Tests.Export
{
    [TestFixture]
    public class ExportLift_Test
    {

        [Test]
        public void ExportSamples_Correct()
        {
            var factory = ExportFactory.Singleton();
            string srcDataPath = EngineEnvironment.PathOfBase + "/src/SolidEngine.Test/ExportLift.TestData";
            string tempPath = Path.GetTempPath() + "SolidTest\\";
            if (Directory.Exists(tempPath))
            {
                Directory.Delete(tempPath, true);
            }
            Directory.CreateDirectory(tempPath);
            foreach (var srcFilePath in Directory.GetFiles(srcDataPath, "*.db"))
            {
                string outputFilePath = String.Format("{0}{1}.lift", tempPath, Path.GetFileNameWithoutExtension(srcFilePath));
                var exporter = factory.CreateFromFileFilter("LIFT (*.lift)|*.lift");
                exporter.Export(srcFilePath, outputFilePath);
                Assert.DoAssert(new ExportTestAsserter(srcFilePath, outputFilePath));
            }
        }

    }
}