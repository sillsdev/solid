using System;
using System.IO;
using System.Text;
using System.Xml;
using NUnit.Framework;
using SolidEngine;

namespace SolidTests
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
            SolidSettings f = new SolidSettings();
            f.FindOrCreateMarkerSetting("mk");

            Assert.IsTrue(f.HasMarker("mk"));
        }

        [Test]
        public void SolidSettings_WriteRead_HasMarker()
        {
            SolidSettings f = new SolidSettings();
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



    }
}
