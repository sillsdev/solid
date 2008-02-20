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
            Palaso.Reporting.ErrorReport.IsOkToInteractWithUser = false;
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
            OpenDictionaryWithPreExistingSettings();
            Assert.AreEqual(2,_mainWindowPM.MasterRecordList.Count);
        }

        private void OpenDictionaryWithPreExistingSettings()
        {
            File.Copy(PathToMDFTemplate, PathToSettingsFileThatGoesWithDictionary);
            _mainWindowPM.OpenDictionary(DictionaryPath, null);
        }

        [Test, Ignore("Needs redoing in Dictionary"), ExpectedException(typeof(Palaso.Reporting.ErrorReport.NonFatalMessageSentToUserException))]
        public void SaveDictionaryFailsWhenDictionaryEditedOutsideOfSolid()
        {
            //passes when ran individually

            OpenDictionaryWithPreExistingSettings();
            
            File.WriteAllText(DictionaryPath, "This is a test");

            System.Threading.Thread.Sleep(2000);
            
            Assert.IsFalse(_mainWindowPM.DictionarySave());
        }

        [Test, Ignore("Needs redoing in Dictionary")]
        public void SaveDictionarySucceedsWhenDictionaryNotEditedOutsideOfSolid()
        {
            MainWindowPM temp = new MainWindowPM();
            OpenDictionaryWithPreExistingSettings();
            Assert.IsTrue(_mainWindowPM.DictionarySave());
        }

        [Test, Ignore("Needs redoing in Dictionary")]
        public void SaveDictionaryWritesWhenThePathDoesNotExist()
        {
            OpenDictionaryWithPreExistingSettings();
            Assert.IsTrue(_mainWindowPM.DictionarySave());
        }

        [Test]
        public void ShouldAskForTemplateBeforeOpeningWhenSettingsMissing()
        {
            Assert.IsTrue(_mainWindowPM.ShouldAskForTemplateBeforeOpening(DictionaryPath));
        }

        [Test]
        public void ShouldNotAskTemplateBeforeOpeningWhenSettingsExist()
        {
            string path = PathToSettingsFileThatGoesWithDictionary;
            try
            {
                File.WriteAllText(
                    path,
                    "hello");
                Assert.IsFalse(_mainWindowPM.ShouldAskForTemplateBeforeOpening(DictionaryPath));
            }
            finally
            {
                File.Delete(path);
            }
        }

        private string PathToSettingsFileThatGoesWithDictionary
        {
            get
            {
                return
                    Path.Combine(Path.GetDirectoryName(DictionaryPath),
                                 Path.GetFileNameWithoutExtension(DictionaryPath) + ".solid");
            }
        }

        [Test]
        public void OpeningWithTemplateMakesCorrectSettingsFile()
        {
            _mainWindowPM.OpenDictionary(DictionaryPath, PathToMDFTemplate);
            Assert.IsTrue(File.Exists(PathToSettingsFileThatGoesWithDictionary));
            Assert.AreEqual(File.ReadAllText(PathToMDFTemplate), File.ReadAllText(PathToSettingsFileThatGoesWithDictionary));
        }

        private string PathToMDFTemplate
        {
            get
            {
                return Path.Combine(_mainWindowPM.PathToFactoryTemplatesDirectory, "MDF Unicode.solid");
            }
        }

        [Test]
        public void TemplatePathWithEmptyDictionaryFilePath_HasSomeTemplates()
        {
            Assert.Greater(_mainWindowPM.TemplatePaths.Count, 0);
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
            OpenDictionaryWithPreExistingSettings();

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