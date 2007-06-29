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
        public void SolidFile_Write1Read1_Correct()
        {
            SolidFile f;
            f = new SolidFile();
            f.File = "myfile.solid";
            f.MarkerSettings.Add(new SolidMarkerSetting("mk"));
            f.Write();
            f = new SolidFile();
            f.File = "myfile.solid";
            Assert.AreEqual(0, f.MarkerSettings.Count);
            f.Read();
            Assert.AreEqual(1, f.MarkerSettings.Count);
        }

    }
}
