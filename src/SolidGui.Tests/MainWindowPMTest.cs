using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;

namespace SolidGui.Tests
{
    [TestFixture]
    public class MainWindowPMTest
    {
        private MainWindowPM _mainWindowPM;
        private string _projectFolder;


        [SetUp]
        public void Setup()
        {
            _mainWindowPM = new MainWindowPM();
            _projectFolder = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            Directory.CreateDirectory(_projectFolder);

            System.Text.StringBuilder builder = new System.Text.StringBuilder();
            builder.AppendLine(@"\lx one");
            builder.AppendLine(@"\ge oneGloss");
            builder.AppendLine(@"\lx two");
            builder.AppendLine(@"\ge twoGloss");
            File.WriteAllText(DictionaryPath, builder.ToString());
        }

        private string DictionaryPath
        {
            get
            {
                return Path.Combine(_projectFolder, "dictionary.db");
            }
        }

        private string SavePath
        {
            get
            {
                return Path.Combine(_projectFolder, "save.db");
            }
        }
        [TearDown]
        public void TearDown()
        {
            TestUtilities.DeleteFolderThatMayBeInUse(_projectFolder);
        }

        [Test]
        public void RecordFilter_RecordFilterList_ReturnsList()
        {
            Assert.IsNotNull(_mainWindowPM.RecordFilters);
        }

        [Test]
        public void OpenExistingDictionaryLoadsRecordLists()
        {
            _mainWindowPM.OpenDictionary(DictionaryPath);
            Assert.AreEqual(2,_mainWindowPM.MasterRecordList.Count);
        }

        [Test]
        public void SaveDictionarySavesCurrentDictionary()
        {
            _mainWindowPM.OpenDictionary(DictionaryPath);
            _mainWindowPM.SaveDictionary(SavePath);
            _mainWindowPM.OpenDictionary(SavePath);

            Assert.AreEqual(2, _mainWindowPM.MasterRecordList.Count);
        }
    }

}