using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using NUnit.Framework;
using Palaso.TestUtilities;
using SolidGui.Engine;
using SolidGui.Export;
using SolidGui.Model;

namespace SolidGui.Tests.Export
{
    [TestFixture]
    public class ExportLiftRegressionTests
    {

        class EnvironmentForTest : IDisposable
        {
            private readonly TemporaryFolder _folder;
            private readonly TempFile _sfmFile;
            private SfmDictionary _dictionary;
            private bool _isDictionaryOpen;


            public EnvironmentForTest(string sfm)
            {
                _folder = new TemporaryFolder("ExportLiftRegressionTests");
                _isDictionaryOpen = false;
                Dictionary = new SfmDictionary();
                SolidSettings = new SolidSettings();

                _sfmFile = _folder.GetNewTempFile(false);
                File.WriteAllText(_sfmFile.Path, sfm);

            }

            public SfmDictionary Dictionary
            {
                get
                {
                    if (!_isDictionaryOpen)
                    {
                        _dictionary.Open(_sfmFile.Path, SolidSettings, new RecordFilterSet());
                        _isDictionaryOpen = true;
                    }
                    return _dictionary;
                }
                private set { _dictionary = value; }
            }

            public SolidSettings SolidSettings { get; private set; }

            public string LiftPath { 
                get {
                    return Path.ChangeExtension(_sfmFile.Path, "lift");
                }
            }

            public void SetupMarker(string marker, string liftConcept, string writingSystem)
            {
                var setting = SolidSettings.FindOrCreateMarkerSetting(marker);
                setting.WritingSystemRfc4646 = writingSystem;
                setting.Mappings[1] = liftConcept;
            }

            public void SetupMarker(string marker, string liftConcept, string writingSystem, string parent, bool infer)
            {
                var setting = SolidSettings.FindOrCreateMarkerSetting(marker);
                setting.WritingSystemRfc4646 = writingSystem;
                setting.Mappings[1] = liftConcept;
                setting.StructureProperties.Add(new SolidStructureProperty(parent));
                if (infer)
                {
                    setting.InferedParent = parent;
                }
            }

            public string LiftAsString()
            {
                return File.ReadAllText(LiftPath);
            }

            public void Dispose()
            {
                _folder.Dispose();
            }
        }

        [Test]
        public void Bug148_LiftExportWithEnglish_WritesEn()
        {
            string sfm = @"
\lx Lexeme
";
            using (var e = new EnvironmentForTest(sfm))
            {
                e.SetupMarker("lx", "lexicalUnit", "en");
                var liftExporter = new LiftExporter();
                liftExporter.Export(e.Dictionary.AllRecords, e.SolidSettings, e.LiftPath);
                AssertThatXmlIn.String(e.LiftAsString()).HasAtLeastOneMatchForXpath("/lift/entry/lexical-unit/form[@lang='en'][text='Lexeme']");
            }
        }

        [Test]
        public void Bug150_LiftExportWithBorrowedWord_IsNotVariant()
        {
            string sfm = @"
\lx Lexeme
\bw BorrowedWord
";
            using (var e = new EnvironmentForTest(sfm))
            {
                e.SetupMarker("lx", "lexicalUnit", "en");
                e.SetupMarker("bw", "borrowedWord", "en");
                var liftExporter = new LiftExporter();
                liftExporter.Export(e.Dictionary.AllRecords, e.SolidSettings, e.LiftPath);
                AssertThatXmlIn.String(e.LiftAsString()).HasAtLeastOneMatchForXpath("/lift/entry/trait[@name='etymology'][@value='BorrowedWord']");
            }
        }

        [Test]
        // http://projects.palaso.org/issues/show/157
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
\va ra
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
            using (var e = new EnvironmentForTest(sfm))
            {
                e.SetupMarker("lx", "lexicalUnit", "vv");
                e.SetupMarker("hm", "homonym", "en", "lx", false);
                e.SetupMarker("ph", "pronunciation", "en-fonipa", "lx", false);
                e.SetupMarker("sd", "semanticDomain", "en", "lx", false);
                e.SetupMarker("ps", "grammi", "en", "lx", false);
                e.SetupMarker("pn", "grammi", "nn", "ps", false);
                e.SetupMarker("sn", "sense", "en", "ps", false);
                e.SetupMarker("ge", "gloss", "en", "sn", true);
                e.SetupMarker("de", "definition", "en", "sn", false);
                e.SetupMarker("gn", "gloss", "nn", "sn", false);
                e.SetupMarker("va", "variant", "vv", "lx", false);
                e.SetupMarker("dt", "dateModified", "en", "lx", false);
                var liftExporter = new LiftExporter();
                liftExporter.Export(e.Dictionary.AllRecords, e.SolidSettings, e.LiftPath);
                AssertThatXmlIn.String(e.LiftAsString()).HasAtLeastOneMatchForXpath("/lift/entry/trait[@name='etymology'][@value='BorrowedWord']");
            }
        }


    }
}
