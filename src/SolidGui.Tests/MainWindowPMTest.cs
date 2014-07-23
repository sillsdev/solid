using System;
using System.IO;
using System.Text;
using Palaso.Reporting;

using NUnit.Framework;
using Palaso.TestUtilities;
using SolidGui.Engine;


namespace SolidGui.Tests
{
    [TestFixture]
    public class MainWindowPMTest
    {
        public class EnvironmentForTest : IDisposable
        {
            private readonly string _projectFolder;

            public EnvironmentForTest()
            {
                _projectFolder = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
                Directory.CreateDirectory(_projectFolder);

                ErrorReport.IsOkToInteractWithUser = false;
            }

            public string ProjectFolder
            {
                get { return _projectFolder; }
            }

            public string PathToSettingsFileThatGoesWithDictionary
            {
                get
                {
                    return
                    Path.Combine(Path.GetDirectoryName(DictionaryPath),
                                 Path.GetFileNameWithoutExtension(DictionaryPath) + ".solid");
                }
            }

            public string DictionaryPath
            {
                get { return Path.Combine(_projectFolder, "dictionary.db"); }
            }

            public void WriteTwoEntryDictionary()
            {
                var builder = new StringBuilder();
                builder.AppendLine(@"\lx one");
                builder.AppendLine(@"\ge oneGloss");
                builder.AppendLine(@"\lx two");
                builder.AppendLine(@"\ge twoGloss");
                File.WriteAllText(DictionaryPath, builder.ToString());
            }

            public void Dispose()
            {
                TestUtilities.DeleteFolderThatMayBeInUse(_projectFolder);
            }
        }

        private static string PathToMDFTemplate(MainWindowPM pm)
        {
            return Path.Combine(MainWindowPM.PathToFactoryTemplatesDirectory, "MDF.solid");
        }

        [Test]
        public void RecordFilter_RecordFilterList_ReturnsList()
        {
            var pm = new MainWindowPM();
            Assert.IsNotNull(pm.RecordFilters);
        }

        [Test]
        public void OpenExistingDictionaryLoadsRecordLists()
        {
            using (var e = new EnvironmentForTest())
            {
                var pm = new MainWindowPM();
                File.Copy(PathToMDFTemplate(pm), e.PathToSettingsFileThatGoesWithDictionary);
                e.WriteTwoEntryDictionary();

                pm.OpenDictionary(e.DictionaryPath, null, false);

                // Assert.AreEqual(2, pm.MasterRecordList.Count);
                Assert.AreEqual(2, pm.WorkingDictionary.AllRecords.Count);  // JMC:! Verify that this works
            }
        }

        //private void OpenDictionaryWithPreExistingSettings()
        //{
        //    File.Copy(PathToMDFTemplate, PathToSettingsFileThatGoesWithDictionary);
        //    _mainWindowPM.OpenDictionary(DictionaryPath, null);
        //}

        //[Test, Ignore("Needs redoing in Dictionary")]
        //public void SaveDictionaryFailsWhenDictionaryEditedOutsideOfSolid()
        //{
        //    //passes when ran individually

        //    OpenDictionaryWithPreExistingSettings();
            
        //    File.WriteAllText(DictionaryPath, "This is a test");

        //    System.Threading.Thread.Sleep(2000);
            
        //    Assert.IsFalse(_mainWindowPM.DictionaryAndSettingsSave());
        //}

        //[Test, Ignore("Needs redoing in Dictionary")]
        //public void SaveDictionarySucceedsWhenDictionaryNotEditedOutsideOfSolid()
        //{
        //    MainWindowPM temp = new MainWindowPM();
        //    OpenDictionaryWithPreExistingSettings();
        //    Assert.IsTrue(_mainWindowPM.DictionaryAndSettingsSave());
        //}

        //[Test, Ignore("Needs redoing in Dictionary")]
        //public void SaveDictionaryWritesWhenThePathDoesNotExist()
        //{
        //    OpenDictionaryWithPreExistingSettings();
        //    Assert.IsTrue(_mainWindowPM.DictionaryAndSettingsSave());
        //}

        [Test]
        public void ShouldAskForTemplateBeforeOpeningWhenSettingsMissing()
        {
            using (var e = new EnvironmentForTest())
            {
                var pm = new MainWindowPM();
                Assert.IsTrue(pm.ShouldAskForTemplateBeforeOpening(e.DictionaryPath));
            }
        }

		[Test]
		public void ShouldAskTemplateBeforeOpeningWithInvalidSettingsFile()
		{
            using (var e = new EnvironmentForTest())
            {
                var pm = new MainWindowPM();
                string path = e.PathToSettingsFileThatGoesWithDictionary;
                File.WriteAllText(path, "hello");
                Assert.Throws<ErrorReport.ProblemNotificationSentToUserException>(
                    () => pm.ShouldAskForTemplateBeforeOpening(e.DictionaryPath)
                );
            }

		}

        [Test]
        public void ShouldNotAskTemplateBeforeOpeningWhenValidSettingsFileExists()
        {
            using (var e = new EnvironmentForTest())
            {
                var pm = new MainWindowPM();
                var solidSettings = new SolidSettings();
                solidSettings.SaveAs(e.PathToSettingsFileThatGoesWithDictionary);
                Assert.IsFalse(pm.ShouldAskForTemplateBeforeOpening(e.DictionaryPath));
            }
        }

        [Test]
        [Ignore("Appears to be unfinished--never creates the lexicon it then tries to open.")]
        public void OpeningWithTemplateMakesCorrectSettingsFile()
        {
            using (var e = new EnvironmentForTest())
            {
                var pm = new MainWindowPM();
                pm.OpenDictionary(e.DictionaryPath, PathToMDFTemplate(pm), false);
                Assert.IsTrue(File.Exists(e.PathToSettingsFileThatGoesWithDictionary));
                Assert.AreEqual(File.ReadAllText(PathToMDFTemplate(pm)), File.ReadAllText(e.PathToSettingsFileThatGoesWithDictionary));
            }

        }

        [Test]
        public void TemplatePathWithEmptyDictionaryFilePath_HasSomeTemplates()
        {
            var pm = new MainWindowPM();
            Assert.Greater(pm.TemplatePaths.Count, 0);
        }

        [Test]
        public void TemplatePathsFindsTemplatesInTemplatesDir()
        {
            using (var e = new EnvironmentForTest())
            {
                var pm = new MainWindowPM();
                string one = Path.Combine(MainWindowPM.PathToFactoryTemplatesDirectory, "testTemplate1.solid");
                string two = Path.Combine(MainWindowPM.PathToFactoryTemplatesDirectory, "testTemplate2.solid");
                File.WriteAllText(one, "test");
                File.WriteAllText(two, "test");
                Assert.IsTrue(pm.TemplatePaths.Contains(one));
                Assert.IsTrue(pm.TemplatePaths.Contains(two));
                File.Delete(one);
                File.Delete(two);
            }
        }

        //[Test]
        //public void TemplatePathsFindsTemplatesDictionaryDir()
        //{
        //    using (var e = new EnvironmentForTest())
        //    {
        //        OpenDictionaryWithPreExistingSettings();
        //        string one = Path.Combine(e.ProjectFolder, "testTemplate1.solid");
        //        string two = Path.Combine(e.ProjectFolder, "testTemplate2.solid");
        //        File.WriteAllText(one, "test");
        //        File.WriteAllText(two, "test");
        //        Assert.IsTrue(_mainWindowPM.TemplatePaths.Contains(one));
        //        Assert.IsTrue(_mainWindowPM.TemplatePaths.Contains(two));
        //    }
        //}
    }

}