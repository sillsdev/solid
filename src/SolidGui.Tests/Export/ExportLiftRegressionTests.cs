using System;
using NUnit.Framework;
using Palaso.Progress;
using Palaso.TestUtilities;
using SolidGui.Export;

namespace SolidGui.Tests.Export
{
    [TestFixture]
    public class ExportLiftRegressionTests
    {
        [Test]
        public void Bug148_LiftExportWithEnglish_WritesEn()
        {
            string sfm = @"
\lx Lexeme
";
            using (var e = new ExportTestScenario(sfm))
            {
                e.SetupMarker("lx", "lexicalUnit", "en");
                var liftExporter = new ExportLift();
                liftExporter.Export(e.Dictionary.AllRecords, e.SolidSettings, e.LiftPath, new ConsoleProgress());
                AssertThatXmlIn.String(e.LiftAsString()).HasAtLeastOneMatchForXpath("/lift/entry/lexical-unit/form[@lang='en'][text='Lexeme']");
            }
        }



        [Test]
        public void DateTime_ddmmmyyyy_Ok()
        {
            string sfm = @"
\lx Lexeme
\dt 08/Oct/1969
";
            using (var e = new ExportTestScenario(sfm))
            {
				DateTime expectedDateTime = DateTime.Parse("08/Oct/1969");
                e.SetupMarker("lx", "lexicalUnit", "en");
                e.SetupMarker("dt", "dateModified", "en", "lx", false);
                var liftExporter = new ExportLift();
                liftExporter.Export(e.Dictionary.AllRecords, e.SolidSettings, e.LiftPath, new ConsoleProgress());
				AssertThatXmlIn.String(e.LiftAsString()).HasAtLeastOneMatchForXpath(
					String.Format("/lift/entry[contains(@dateCreated,'{0}')]",
					expectedDateTime.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssZ"))
				);
            }
        }

        [Test]
        public void NoteGrammer_AppearsUnderSense_ExportsUnderSense()
        {
            string sfm = @"
\lx Lexeme
\sn
\ng Note
";
            using (var e = new ExportTestScenario(sfm))
            {
                e.SetupMarker("ng", "noteGrammar", "en", "sn", false);
                var liftExporter = new ExportLift();
                liftExporter.Export(e.Dictionary.AllRecords, e.SolidSettings, e.LiftPath, new ConsoleProgress());
                AssertThatXmlIn.String(e.LiftAsString()).HasAtLeastOneMatchForXpath("/lift/entry/sense/note/form[text='Note']");
            }
        }

        [Test]
        // http://projects.palaso.org/issues/show/157
        // Note that we exclude variant (va) for the purpose of this test
        public void Bug157_LiftExportPartOfSpeech_IsInLift()
        {
            string sfm = @"
\lx d'ara
\hm 1
\ph dara
\sd location, body
\ps n
\pn kb
\sn 1
\ge inside
\gn dalam
\sn 2
\ge character; emotions; heart
\de character, seat of emotions.
\gn hati; pikiran
\ps TAM
\ge in process of; busy doing
\gn dalam (keadaan)
\ps Prep
\pn kdep
\ge in; inside
\gn dalam; di dalam
\dt 19/Jan/2008
";
            using (var e = new ExportTestScenario(sfm))
            {
                e.SetupMarker("lx", "lexicalUnit", "vv");
                e.SetupMarker("hm", "homonym", "en", "lx", false);
                e.SetupMarker("ph", "pronunciation", "en-fonipa", "lx", false);
                e.SetupMarker("sd", "semanticDomain", "en", "lx", false);
                e.SetupMarker("ps", "grammi", "en", "lx", false);
                e.SetupMarker("pn", "ignore", "nn", "ps", false);
                e.SetupMarker("sn", "sense", "en", "ps", false);
                e.SetupMarker("ge", "gloss", "en", "sn", true);
                e.SetupMarker("de", "definition", "en", "sn", false);
                e.SetupMarker("gn", "gloss", "nn", "sn", false);
                e.SetupMarker("dt", "dateModified", "en", "lx", false);
                var liftExporter = new ExportLift();
                liftExporter.Export(e.Dictionary.AllRecords, e.SolidSettings, e.LiftPath, new ConsoleProgress());
                AssertThatXmlIn.String(e.LiftAsString()).HasSpecifiedNumberOfMatchesForXpath("/lift/entry/sense/grammatical-info", 4);
            }
        }


    }
}
