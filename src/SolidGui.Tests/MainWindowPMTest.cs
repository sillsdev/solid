using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
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
            Reporting.ErrorReporter.OkToInteractWithUser = false;
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
            _mainWindowPM.SaveDictionaryAs(SavePath);
            
            Assert.AreEqual(File.ReadAllText(SavePath),File.ReadAllText(DictionaryPath));
        }

        [Test, ExpectedException(typeof(Reporting.ErrorReporter.NonFatalMessageSentToUserException))]
        public void SaveDictionaryFailsWhenDictionaryEditedOutsideOfSolid()
        {
            //passes when ran individually

            _mainWindowPM.OpenDictionary(DictionaryPath);

            System.Threading.Thread.Sleep(2000);

            File.WriteAllText(DictionaryPath,"This is a test");

            Assert.IsFalse(_mainWindowPM.SaveDictionary());
        }

        [Test]
        public void SaveDictionarySucceedsWhenDictionaryNotEditedOutsideOfSolid()
        {
            MainWindowPM temp = new MainWindowPM();
            _mainWindowPM.OpenDictionary(DictionaryPath);
            Assert.IsTrue(_mainWindowPM.SaveDictionary());
        }

        [Test]
        public void SaveDictionaryWritesWhenThePathDoesNotExist()
        {
            _mainWindowPM.OpenDictionary(DictionaryPath);
            Assert.IsTrue(_mainWindowPM.SaveDictionary());
        }

        [Test]
        public void TemplatePathsFindsTemplatesInTemplatesDir()
        {
            string one = Path.Combine(_mainWindowPM.PathToFactoryTemplatesDirectory, "testTemplate1.solid");
            string two = Path.Combine(_mainWindowPM.PathToFactoryTemplatesDirectory, "testTemplate2.solid");
            try
            {
                File.WriteAllText(one, "test");
                File.WriteAllText(two, "test");
                Assert.IsTrue(_mainWindowPM.TemplatePaths.Contains(one));
                Assert.IsTrue(_mainWindowPM.TemplatePaths.Contains(two));
            }
            finally
            {
                File.Delete(one);
                File.Delete(two);
            }
        }

        [Test]
        public void TemplatePathsFindsTemplatesDictionaryDir()
        {
            _mainWindowPM.OpenDictionary(DictionaryPath);

            string one = Path.Combine(_projectFolder, "testTemplate1.solid");
            string two = Path.Combine(_projectFolder, "testTemplate2.solid");
            try
            {
                File.WriteAllText(one, "test");
                File.WriteAllText(two, "test");
                Assert.IsTrue(_mainWindowPM.TemplatePaths.Contains(one));
                Assert.IsTrue(_mainWindowPM.TemplatePaths.Contains(two));
            }
            finally
            {
                File.Delete(one);
                File.Delete(two);
            }
        }
    }

}