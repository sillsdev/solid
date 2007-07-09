using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using NUnit.Framework;

namespace SolidGui.Tests
{
    [TestFixture]
    public class DictionaryTest
    {
        private Dictionary _dictionary;
        private string _dictionaryPath;
        private string _projectFolder;
        private string _tempDictionaryPath;

        [SetUp]
        public void SetUp()
        {
            Reporting.ErrorReporter.OkToInteractWithUser = false;
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

            _dictionary.Open(_dictionaryPath);

            foreach (string marker in _dictionary.AllMarkers)
            {
                Assert.IsTrue(_markers.Contains(marker));
            }
        }

        [Test]
        public void OpenReadsInAllRecords()
        {
            _dictionary.Open(_dictionaryPath);

            Assert.AreEqual(2, _dictionary.Count);
        }

        [Test]
        public void GetDirectoryPathReturnsPathToDirectoryContainingDictionary()
        {
            _dictionary.Open(_dictionaryPath);
            Assert.AreEqual(_projectFolder,_dictionary.GetDirectoryPath());
        }

        [Test]
        public void GetFileNameReturnsNameOfDictionary()
        {
            _dictionary.Open(_dictionaryPath);
            Assert.AreEqual("Dictionary",_dictionary.GetFileNameNoExtension());
        }

        [Test]
        public void CopyToWritesDictionaryToFile()
        {
            _dictionary.Open(_dictionaryPath);
            _dictionary.CopyTo(_tempDictionaryPath);
            Assert.AreEqual(File.ReadAllText(_dictionaryPath), File.ReadAllText(_tempDictionaryPath));
        }

        [Test]
        public void SaveSavesDictionaryBackToOriginalFile()
        {
            _dictionary.Open(_dictionaryPath);
            List<Record> data = _dictionary.AllRecords;
            data[1].SetField(1,"\\ge threeGloss");
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
