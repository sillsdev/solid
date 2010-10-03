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
        [Ignore("SOLID is not yet able to export properly due to insufficiencies in the Palaso Lift exporter.")]
        public void LiftExporter_CompareOutputToLiftSpecTemplate_GeneratedMatchesTemplate()
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

                if(File.Exists(outputFilePath))
                {
                    File.Delete(outputFilePath);
                }

                liftExporter.Export(dictionary.AllRecords, solidSettings, outputFilePath);

                string templateFilePath = Path.ChangeExtension(srcFilePath, ".tmpl");

                Assert.That(outputFilePath, new ExportTestConstraint(templateFilePath));
             
            }
        }

        [Test]
        public void LiftExporter_CompareOutputToSOLIDfriendlyTemplateWithCustomFields_GeneratedMatchesTemplate()
        {
            string srcDataPath = EngineEnvironment.PathOfBase + "/src/SolidEngine.Test/ExportLift.TestData";

            foreach (var srcFilePath in Directory.GetFiles(srcDataPath, "*.db"))
            {
                var dictionary = new SfmDictionary();
                var liftExporter = new LiftExporter();
                var solidSettings = SolidSettings.OpenSolidFile(SolidSettings.GetSettingsFilePathFromDictionaryPath(srcFilePath));
                dictionary.Open(srcFilePath, solidSettings, new RecordFilterSet());

                string outputFilePath = Path.ChangeExtension(srcFilePath, "lift");
                string templateFilePath = Path.ChangeExtension(srcFilePath, ".newtmpl");
                Console.WriteLine("Testing:\nTemplate: {0} \nOutput: {1}", templateFilePath, outputFilePath);
                
                if (File.Exists(outputFilePath))
                {
                    File.Delete(outputFilePath);
                }
                liftExporter.Export(dictionary.AllRecords, solidSettings, outputFilePath);
                
                Assert.That(outputFilePath, new ExportTestConstraint(templateFilePath));
            }

        }

        [Test]
        public void LiftExporter_IndividualFile_GeneratedMatchesTemplate()
        {
            string srcDataPath = EngineEnvironment.PathOfBase + "/src/SolidEngine.Test/ExportLift.TestData";

            var srcFilePath = srcDataPath + "/xe_WithTranslation.db";
            
            var dictionary = new SfmDictionary();
            var liftExporter = new LiftExporter();
            var solidSettings = SolidSettings.OpenSolidFile(SolidSettings.GetSettingsFilePathFromDictionaryPath(srcFilePath));
            dictionary.Open(srcFilePath, solidSettings, new RecordFilterSet());

            string outputFilePath = Path.ChangeExtension(srcFilePath, "lift");
            string templateFilePath = Path.ChangeExtension(srcFilePath, ".newtmpl");
            Console.WriteLine("Testing:\nTemplate: {0} \nOutput: {1}", templateFilePath, outputFilePath);


            if (File.Exists(outputFilePath))
            {
                File.Delete(outputFilePath);
            }
            liftExporter.Export(dictionary.AllRecords, solidSettings, outputFilePath);


            Assert.That(outputFilePath, new ExportTestConstraint(templateFilePath));
            

        }

    }
}