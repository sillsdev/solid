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

    }
}
