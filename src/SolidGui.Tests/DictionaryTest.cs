using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using NUnit.Framework;
using SolidEngine;

namespace SolidGui.Tests
{
    [TestFixture]
    public class DictionaryTest
    {
        private Dictionary _dictionary;
        private string _dictionaryPath;
        private string _projectFolder;
        private string _tempDictionaryPath;

        private SolidSettings _settings;

        [SetUp]
        public void SetUp()
        {
            _settings = new SolidSettings();
            SolidMarkerSetting lxSetting = new SolidMarkerSetting("lx");
            lxSetting.StructureProperties.Add(new SolidStructureProperty("entry", MultiplicityAdjacency.Once));
            SolidMarkerSetting geSetting = new SolidMarkerSetting("ge");
            geSetting.StructureProperties.Add(new SolidStructureProperty("sn", MultiplicityAdjacency.MultipleApart));
            SolidMarkerSetting snSetting = new SolidMarkerSetting("sn");
            snSetting.StructureProperties.Add(new SolidStructureProperty("lx", MultiplicityAdjacency.MultipleApart));

            _settings.MarkerSettings.Add(lxSetting);
            _settings.MarkerSettings.Add(snSetting);
            _settings.MarkerSettings.Add(geSetting);

            Palaso.Reporting.ErrorReport.IsOkToInteractWithUser = false;
            _dictionary = new Dictionary();
            _projectFolder = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            Directory.CreateDirectory(_projectFolder);
            _dictionaryPath = Path.Combine(_projectFolder, "Dictionary.db");
            _tempDictionaryPath = Path.Combine(_projectFolder, "tempDictionary.db");

            StringBuilder builder = new StringBuilder();
            builder.AppendLine(@"\lx one");
            builder.AppendLine(@"\ge oneGloss");
            builder.AppendLine(@"\lx two");
            builder.AppendLine(@"\ge twoGloss");
            File.WriteAllText(_dictionaryPath, builder.ToString());
        }

        [TearDown]
        public void TearDown()
        {
            TestUtilities.DeleteFolderThatMayBeInUse(_projectFolder);
        }

        [Test]
        public void OpenReadsInAllDictionaryMarkers()
        {
            List<string> _markers = new List<string>();
            _markers.Add("lx");
            _markers.Add("ge");

            _dictionary.Open(_dictionaryPath, _settings, new RecordFilterSet());
            int markerCount = 0;

            foreach (string  storedMarker in _dictionary.AllMarkers)
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
            List<Record> data = _dictionary.AllRecords;
            data[1].SetField(1,"threeGloss");
            _dictionary.Save();

            StringBuilder builder = new StringBuilder();
            builder.AppendLine(@"\lx one");
            builder.AppendLine(@"\ge oneGloss");
            builder.AppendLine(@"\lx two");
            builder.AppendLine(@"\ge threeGloss");
            File.WriteAllText(_tempDictionaryPath, builder.ToString());

            Assert.AreEqual(File.ReadAllText(_tempDictionaryPath),File.ReadAllText(_dictionaryPath));


            
        }
    }
}
