using System;
using System.IO;
using System.Text;
using NUnit.Framework;
using Palaso.IO;
using Palaso.Progress.LogBox;
using Palaso.TestUtilities;
using SolidGui.Engine;
using SolidGui.Export;
using SolidGui.Filter;
using SolidGui.Model;

namespace SolidGui.Tests.Export
{
    class ExportTestScenario : IDisposable
    {
        private readonly TemporaryFolder _folder;
        private readonly TempFile _sfmFile;
        private SfmDictionary _dictionary;
        private bool _isDictionaryOpen;
         private StringBuilderProgress _progress = new StringBuilderProgress();

        public string ProgressOutput
        {
            get
            {
                return _progress.Text;
            }
        }
            
        public ExportTestScenario(string sfm):this()
        {          
            Input = sfm;
             Dictionary = new SfmDictionary();
        }

        public ExportTestScenario()
        {
            _folder = new TemporaryFolder("ExportLiftRegressionTests");
            _sfmFile = _folder.GetNewTempFile(false);
            _isDictionaryOpen = false;
            Dictionary = new SfmDictionary();
            SolidSettings = new SolidSettings();
            SetupMarker("lx", "lexicalUnit", "en");
            SetupMarker("sn", "sense", "en", "lx", false);

       
          }
        public string Input
        {
            set
            {
                //don't make the tests line each line up against the margin.
                var builder = new StringBuilder();
                foreach (var line in value.Split(new char[]{'\n'}))
                {
                    builder.AppendLine(line.Trim());
                }
                File.WriteAllText(_sfmFile.Path, builder.ToString());
                Dictionary = new SfmDictionary();
            }
        }

        public void Export()
        {
            var liftExporter = new ExportLift();
            liftExporter.Export(Dictionary.AllRecords, SolidSettings, LiftPath, new MultiProgress(new IProgress[] {_progress, new ConsoleProgress()}));;
        }

        public void AssertExportsSingleInstance(string xpath)
        {
            Export();
            AssertThatXmlIn.String(LiftAsString()).HasSpecifiedNumberOfMatchesForXpath(xpath,1);
        }

        public void AssertHasSingleInstance(string xpath)
        {
            AssertThatXmlIn.String(LiftAsString()).HasSpecifiedNumberOfMatchesForXpath(xpath, 1);
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

        public void AssertNoErrorWasReported()
        {
            Assert.False(ProgressOutput.ToLower().Contains("error"));
        }
        public void AssertErrorWasReported()
        {
            Assert.That(ProgressOutput.ToLower(), Contains.Substring("error"));
        }
    }
}