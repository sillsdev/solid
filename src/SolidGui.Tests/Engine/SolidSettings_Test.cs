using NUnit.Framework;
using SolidGui.Engine;


namespace SolidGui.Tests.Engine
{
    [TestFixture]
    public class SolidSettingsTest
    {

        [TestFixtureSetUp]
        public void Init()
        {
        }

        [Test]
        public void SolidSettings_FindOrCreateMarkerSetting_CreatesMarker()
        {
            var f = new SolidSettings();
            f.FindOrCreateMarkerSetting("mk");

            Assert.IsTrue(f.HasMarker("mk"));
        }

        [Test]
        public void SolidSettings_WriteRead_HasMarker()
        {
            var f = new SolidSettings();
            f.FilePath = "myfile.solid";
            f.FindOrCreateMarkerSetting("mk");
            f.Save();

            f = SolidSettings.OpenSolidFile("myfile.solid");

            Assert.IsTrue(f.HasMarker("mk"));
        }

        [Test]
        public void SolidSettings_SettingsFilePath_Correct()
        {
            string result = SolidSettings.GetSettingsFilePathFromDictionaryPath("mydatafile.txt");
            Assert.AreEqual("mydatafile.solid", result);
        }

        [Test]
        // http://projects.palaso.org/issues/show/404
        public void SolidSettings_SettingsFilePathWithNoExtension_Correct()
        {
            string result = SolidSettings.GetSettingsFilePathFromDictionaryPath("mydatafile");
            Assert.AreEqual("mydatafile.solid", result);
        }

    }
}