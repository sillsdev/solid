using System;
using System.IO;
using System.Text;
using System.Xml;
using NUnit.Framework;
using SolidConsole;

namespace SolidTests
{
    [TestFixture]
    public class SolidFileTest
    {

        [TestFixtureSetUp]
        public void Init()
        {
        }

        [Test]
        public void SolidFile_FileProperty_SetGetCorrect()
        {
            SolidFile f = new SolidFile();
            f.File = "myfile";
            Assert.AreEqual("myfile", f.File);
        }

        [Test]
        public void SolidFile_Close_ResetProperties()
        {
            SolidFile f = new SolidFile();
            f.File = "myfile";
            f.Close();
            Assert.AreEqual(String.Empty, f.File);
        }

        [Test]
        public void SolidFile_WriteRead_Correct()
        {
            SolidFile f = new SolidFile();
            f.File = "myfile.solid";
            f.MarkerSettings.Add(new SolidMarkerSetting("mk"));
            Assert.AreEqual(1, f.MarkerSettings.Count);
            f.Close();
            Assert.AreEqual(0, f.MarkerSettings.Count);
            Assert.AreEqual("myfile.solid", f.File);
        }

        [Test]
        public void SolidFileWriteRead_Correct()
        {
            SolidFile f = new SolidFile();
            f.File = "myfile.solid";
            f.MarkerSettings.Add(new SolidMarkerSetting("mk"));
            f.Write();
            f.Close();
            Assert.AreEqual(0, f.MarkerSettings.Count);
            f.Read();
            Assert.AreEqual(1, f.MarkerSettings.Count);
        }

    }
}
