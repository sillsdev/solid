using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using Palaso.Progress.LogBox;
using Palaso.TestUtilities;
using SolidGui.Engine;
using SolidGui.Export;
using SolidGui.Model;
using SolidGui.Tests.Engine;


namespace SolidGui.Tests.Export
{
    [TestFixture]
    public class ExportLift_Test
    {

        [Test]
        [Ignore("SOLID is not yet able to export properly due to insufficiencies in the Palaso Lift exporter.")]
        public void LiftExporter_CompareOutputToLiftSpecTemplate_GeneratedMatchesTemplate()
        {

            string srcDataPath = EngineEnvironment.PathOfBase + "/src/SolidGui.Tests/Export/ExportLift.TestData";
            
            foreach (var srcFilePath in Directory.GetFiles(srcDataPath, "*.db"))
            {
                var dictionary = new SfmDictionary();
                var liftExporter = new ExportLift();
                var solidSettings = SolidSettings.OpenSolidFile(SolidSettings.GetSettingsFilePathFromDictionaryPath(srcFilePath));
                dictionary.Open(srcFilePath, solidSettings, new RecordFilterSet());

                //string outputFilePath = "C:/src/sil/solid/src/SolidGui.Tests/Export/ExportLift.TestData/TestOutput/test.txt";//Path.GetTempPath() + Path.GetRandomFileName();



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

                liftExporter.Export(dictionary.AllRecords, solidSettings, outputFilePath, new ConsoleProgress());

                string templateFilePath = Path.ChangeExtension(srcFilePath, ".tmpl");

                Assert.That(outputFilePath, new ExportTestConstraint(templateFilePath));
             
            }
        }

        [Test]
        public void LiftExporter_CompareOutputToSOLIDfriendlyTemplateWithCustomFields_GeneratedMatchesTemplate()
        {
            string srcDataPath = EngineEnvironment.PathOfBase + "/src/SolidGui.Tests/Export/ExportLift.TestData";

            foreach (var srcFilePath in Directory.GetFiles(srcDataPath, "*.db"))
            {
                var dictionary = new SfmDictionary();
                var liftExporter = new ExportLift();
                var solidSettings = SolidSettings.OpenSolidFile(SolidSettings.GetSettingsFilePathFromDictionaryPath(srcFilePath));
                dictionary.Open(srcFilePath, solidSettings, new RecordFilterSet());

                string outputFilePath = Path.ChangeExtension(srcFilePath, "lift");
                string templateFilePath = Path.ChangeExtension(srcFilePath, ".newtmpl");
                Console.WriteLine("Testing:\nTemplate: {0} \nOutput: {1}", templateFilePath, outputFilePath);
                
                if (File.Exists(outputFilePath))
                {
                    File.Delete(outputFilePath);
                }
                liftExporter.Export(dictionary.AllRecords, solidSettings, outputFilePath, new ConsoleProgress());
                
                Assert.That(outputFilePath, new ExportTestConstraint(templateFilePath));
            }

        }

        [Test]
        public void LiftExporter_IndividualFile_GeneratedMatchesTemplate()
        {
            string srcDataPath = EngineEnvironment.PathOfBase + "/src/SolidGui.Tests/Export/ExportLift.TestData";

            var srcFilePath = srcDataPath + "/xe_WithTranslation.db";
            
            var dictionary = new SfmDictionary();
            var liftExporter = new ExportLift();
            var solidSettings = SolidSettings.OpenSolidFile(SolidSettings.GetSettingsFilePathFromDictionaryPath(srcFilePath));
            dictionary.Open(srcFilePath, solidSettings, new RecordFilterSet());

            string outputFilePath = Path.ChangeExtension(srcFilePath, "lift");
            string templateFilePath = Path.ChangeExtension(srcFilePath, ".newtmpl");
            Console.WriteLine("Testing:\nTemplate: {0} \nOutput: {1}", templateFilePath, outputFilePath);


            if (File.Exists(outputFilePath))
            {
                File.Delete(outputFilePath);
            }
            liftExporter.Export(dictionary.AllRecords, solidSettings, outputFilePath, new ConsoleProgress());


            Assert.That(outputFilePath, new ExportTestConstraint(templateFilePath));
            

        }


        [Test]
        public void BorrowedWord_ExportedAsEtymologySource()
        {
            using (var e = new ExportTestScenario())
            {
                e.Input = @"\lx Lexeme
                            \bw English";
                e.SetupMarker("bw", "borrowedWord", "en");
                e.AssertExportsSingleInstance("/lift/entry/etymology[@type='borrowed' and @source='English']");
            }
        }
        [Test]
        public void Pronunciation_ExportedAsPronunciation()
        {
            using (var e = new ExportTestScenario())
            {
                e.Input = @"\lx boat
                            \pr bot";
                e.SetupMarker("pr", "pronunciation", "en");
                e.AssertExportsSingleInstance("/lift/entry/pronunciation/form[text='bot']");
            }
        }
        [Test]
        public void GrammarNote_ExportedAsNoteWithGrammarType()
        {
            using (var e = new ExportTestScenario())
            {
                e.Input = @"\lx boat
                            \sn
                            \ng about grammar";
                e.SetupMarker("ng", "noteGrammar", "en", "sn", true);
                e.AssertExportsSingleInstance("/lift/entry/sense/note[@type='grammar']/form[text='about grammar']");
            }
        }
        [Test]
        public void Note_ExportedAsNoteWithNoType()
        {
            using (var e = new ExportTestScenario())
            {
                e.Input = @"\lx boat
                            \sn
                            \nt blah blah";
                e.SetupMarker("nt", "note", "en", "sn", true);
                e.AssertExportsSingleInstance("/lift/entry/sense/note[not(@type)]/form[text='blah blah']");
            }
        }
        [Test]
        public void Reversal_ExportedAsReversal()
        {
            using (var e = new ExportTestScenario())
            {
                e.Input = @"\lx boat
                            \sn
                            \re ship";
                e.SetupMarker("re", "reversal", "en", "sn", true);
                e.AssertExportsSingleInstance("/lift/entry/sense/reversal/form[text='ship']");
            }
        }

        [Test]
        public void Etymology_ProtoAndSourceAndGloss_ExportedToEtymologyElementWithTypeOfProto()
        {
            using (var e = new ExportTestScenario())
            {
                e.Input = @"\lx form
                            \et proto
                            \eg gloss
                            \es English";
                e.SetupMarker("et", "etymology", "en");
                e.SetupMarker("eg", "etymologyGloss", "en");
                e.SetupMarker("es", "etymologySource", "en");
                e.AssertExportsSingleInstance("/lift/entry/etymology[@type='proto' and @source='English']");
            }
        }


        [Test]
        public void Variant_2FreeVariants()
        {
            using (var e = new ExportTestScenario())
            {
                e.Input = @"\lx form
                            \va 1
                           \va 2";
                e.SetupMarker("va", "variant", "en");
                e.AssertExportsSingleInstance("/lift/entry/variant/form[text='1']");
                e.AssertExportsSingleInstance("/lift/entry/variant/form[text='2']");
            }
        }
    }
}