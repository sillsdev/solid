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
        public void SolidSettings_FileProperty_SetGetCorrect()
        {
            SolidSettings f = new SolidSettings();
            f.FilePath = "myfile";
            Assert.AreEqual("myfile", f.FilePath);
        }

        [Test]
        public void SolidSettings_Write1Read2_Correct()
        {
            SolidSettings f;
            f = new SolidSettings();
            f.FilePath = "myfile.solid";
            f.MarkerSettings.Add(new SolidMarkerSetting("mk"));
            f.Save();
            f = SolidSettings.OpenSolidFile("myfile.solid");
            //2 returned instead of 1 because lx has been hardcoded in
            Assert.AreEqual(2, f.MarkerSettings.Count);
        }

        [Test]
        public void SolidSettings_SettingsFilePath_Correct()
        {
            string result = SolidSettings.GetSettingsFilePathFromDictionaryPath("mydatafile.txt");
            Assert.AreEqual("mydatafile.solid", result);
        }



    }
}
