using System;
using System.IO;
using NUnit.Framework;
using SolidGui.Engine;
using SolidGui.Export;
using SolidGui.Model;


namespace SolidGui.Tests.Export
{
    [TestFixture]
    public class ExportLift_Test
    {

        [Test]
        [Ignore]
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
            SfmDictionary dictionary = new SfmDictionary();
            
            
            foreach (var srcFilePath in Directory.GetFiles(srcDataPath, "*.db"))
            {
                //dictionary.Open(srcFilePath, new SolidSettings(),  );

                string outputFilePath = String.Format("{0}{1}.lift", tempPath, Path.GetFileNameWithoutExtension(srcFilePath));
                var exporter = factory.CreateFromFileFilter("LIFT (*.lift)|*.lift");
                Assert.Fail("need to get list of sfmLexEntries for exporter - smw 10sep2010");//exporter.Export(srcFilePath, outputFilePath);
                Assert.That(outputFilePath, Has.Some.Matches(new ExportTestConstraint(srcFilePath)));
            }
        }

        [Test]
        public void TestLiftExport()
        {

            string srcDataPath = EngineEnvironment.PathOfBase + "/src/SolidEngine.Test/ExportLift.TestData";
            

            foreach (var srcFilePath in Directory.GetFiles(srcDataPath, "*.db"))
            {
                var dictionary = new SfmDictionary();
                var liftExporter = new LiftExporter();
                var solidSettings = SolidSettings.OpenSolidFile(SolidSettings.GetSettingsFilePathFromDictionaryPath(srcFilePath));
                dictionary.Open(srcFilePath, solidSettings, new RecordFilterSet());

                //string outputFilePath = "C:/src/sil/solid/src/SolidEngine.Test/ExportLift.TestData/TestOutput/test.txt";//Path.GetTempPath() + Path.GetRandomFileName();



                /*string tempPath = Path.GetTempPath() + "SolidTest\\";
                if (Directory.Exists(tempPath))
                {
                    Directory.Delete(tempPath, true);
                }
                Directory.CreateDirectory(tempPath);*/
                //string outputFilePath = String.Format("{0}{1}.lift", tempPath, Path.GetFileNameWithoutExtension(srcFilePath));
                string outputFilePath = Path.ChangeExtension(srcFilePath, "lift"); // Change this back!!! CP 2010-09

                liftExporter.Export(dictionary.AllRecords, solidSettings, outputFilePath);


                //TextReader exportFile = new StreamReader(outputFilePath);
                //Assert.That(outputFilePath, new ExportTestConstraint(srcFilePath));
                // Assert.That(null, Has.Some.Matches(new ExportTestConstraint(srcFilePath)));

            }

        }

    }
}