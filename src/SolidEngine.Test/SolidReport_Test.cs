using System;
using System.IO;
using System.Text;
using System.Xml;
using NUnit.Framework;
using SolidEngine;

namespace SolidTests
{
    [TestFixture]
    public class SolidReportTest
    {

        [TestFixtureSetUp]
        public void Init()
        {
        }

        [Test]
        public void SolidReport_Empty_Correct()
        {
            SolidReport r = new SolidReport();
            Assert.AreEqual(0, r.Entries.Count);
        }

        [Test]
        public void SolidReport_AddNullEntry_Correct()
        {
            SolidReport r = new SolidReport();
            Assert.AreEqual(0, r.Entries.Count);
            r.Add(new SolidReport.Entry(
                SolidReport.EntryType.Error, null, null, "Test"
            ));
            Assert.AreEqual(1, r.Entries.Count);
        }

        [Test]
        public void SolidReport_AddEntry_Correct()
        {
            string xml = "<entry id=\"22\" startline=\"33\" endline=\"44\"><lx>a</lx><ge>b</ge></entry>";
            XmlDocument entry = new XmlDocument();
            entry.LoadXml(xml);
            XmlNode field = entry.DocumentElement.LastChild;
            SolidReport r = new SolidReport();
            Assert.AreEqual(0, r.Entries.Count);
            r.Add(new SolidReport.Entry(
                SolidReport.EntryType.Error, entry.DocumentElement, field, "Test"
            ));
            Assert.AreEqual(1, r.Entries.Count);
            SolidReport.Entry reportEntry = r.Entries[0];
            Assert.AreEqual(22, reportEntry.RecordID);
            Assert.AreEqual(33, reportEntry.RecordStartLine);
            Assert.AreEqual(44, reportEntry.RecordEndLine);
            Assert.AreEqual("ge", reportEntry.Marker);
            Assert.AreEqual("Test", reportEntry.Description);
        }

        [Test]
        public void SolidReport_SaveOpen_Correct()
        {
            SolidReport r = new SolidReport();
            Assert.AreEqual(0, r.Entries.Count);
            r.Add(new SolidReport.Entry(
                SolidReport.EntryType.Error, null, null, "Test"
            ));
            Assert.AreEqual(1, r.Entries.Count);

        }

    }
}
