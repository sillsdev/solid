using System.Collections.Generic;
using System.IO;
using System.Text;
using NUnit.Framework;
using SolidGui.Engine;
using SolidGui.Model;

namespace SolidGui.Tests
{
    [TestFixture]
    public class DictionaryTest
    {
        private SfmDictionary _dictionary;
        private string _dictionaryPath;
        private string _projectFolder;
        private string _tempDictionaryPath;

        private SolidSettings _settings;

        [SetUp]
        public void SetUp()
        {
            _settings = new SolidSettings();
            var lxSetting = _settings.FindOrCreateMarkerSetting("lx");
            lxSetting.StructureProperties.Add(new SolidStructureProperty("entry", MultiplicityAdjacency.Once));

            var geSetting = _settings.FindOrCreateMarkerSetting("ge");
            geSetting.StructureProperties.Add(new SolidStructureProperty("sn", MultiplicityAdjacency.MultipleApart));
            
            var snSetting = _settings.FindOrCreateMarkerSetting("sn");
            snSetting.StructureProperties.Add(new SolidStructureProperty("lx", MultiplicityAdjacency.MultipleApart));

            Palaso.Reporting.ErrorReport.IsOkToInteractWithUser = false;
            _dictionary = new SfmDictionary();
            _projectFolder = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            Directory.CreateDirectory(_projectFolder);
            _dictionaryPath = Path.Combine(_projectFolder, "Dictionary.db");
            _tempDictionaryPath = Path.Combine(_projectFolder, "tempDictionary.db");

            var builder = new StringBuilder();
            builder.AppendLine(@"\lx one");
            builder.AppendLine(@"\ge oneGloss");
            builder.AppendLine(@"\lx two");
            builder.AppendLine(@"\ge twoGloss");
            File.WriteAllText(_dictionaryPath, builder.ToString());
        }

        [TearDown]
        public void TearDown()
        {
            Palaso.TestUtilities.TestUtilities.DeleteFolderThatMayBeInUse(_projectFolder);
        }

        [Test]
        public void OpenReadsInAllDictionaryMarkers()
        {
            var _markers = new List<string>();
            _markers.Add("lx");
            _markers.Add("ge");

            _dictionary.Open(_dictionaryPath, _settings, new RecordFilterSet());
            int markerCount = 0;

            foreach (var storedMarker in _dictionary.AllMarkers)
            {
                    markerCount++;
                    Assert.IsTrue(_markers.Contains(storedMarker));
            }

            Assert.AreEqual(2, markerCount);
        }

        [Test]
        public void OpenReadsInAllRecords()
        {
            _dictionary.Open(_dictionaryPath, _settings, new RecordFilterSet());

            Assert.AreEqual(2, _dictionary.Count);
        }

        [Test]
        public void GetDirectoryPathReturnsPathToDirectoryContainingDictionary()
        {
            _dictionary.Open(_dictionaryPath, _settings, new RecordFilterSet());
            Assert.AreEqual(_projectFolder,_dictionary.GetDirectoryPath());
        }

        [Test]
        public void SaveAsWritesDictionaryToFile()
        {
            _dictionary.Open(_dictionaryPath, _settings, new RecordFilterSet());
            _dictionary.SaveAs(_tempDictionaryPath);
            Assert.AreEqual(File.ReadAllText(_dictionaryPath), File.ReadAllText(_tempDictionaryPath));
        }

        [Test]
        public void SaveSavesDictionaryBackToOriginalFile()
        {
            _dictionary.Open(_dictionaryPath, _settings, new RecordFilterSet());
            var data = _dictionary.AllRecords;
            data[1].SetFieldValue(1,"threeGloss");
            _dictionary.Save();

            var builder = new StringBuilder();
            builder.AppendLine(@"\lx one");
            builder.AppendLine(@"\ge oneGloss");
            builder.AppendLine(@"\lx two");
            builder.AppendLine(@"\ge threeGloss");
            File.WriteAllText(_tempDictionaryPath, builder.ToString());

            Assert.AreEqual(File.ReadAllText(_tempDictionaryPath),File.ReadAllText(_dictionaryPath));


            
        }
    }
}
