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
        public void SolidReport_Empty_AllEntriesCorrect()
        {
            SolidReport r = new SolidReport();
            XmlNode n = r.AllEntries();
            Assert.IsNotNull(n);
        }

        [Test]
        public void SolidReport_Empty_CountCorrect()
        {
            SolidReport r = new SolidReport();
            Assert.AreEqual(0, r.Count);
        }

        [Test]
        public void SolidReport_AddNullEntry_Correct()
        {
            SolidReport r = new SolidReport();
            Assert.AreEqual(0, r.Count);
            r.AddEntry(
                SolidReport.EntryType.StructureParentNotFound, null, null, "Test"
            );
            Assert.AreEqual(1, r.Count);
        }

        [Test]
        public void SolidReport_AddEntry_Correct()
        {
            string xml = "<entry record=\"22\" startline=\"33\" endline=\"44\"><lx field=\"0\">a</lx><ge field=\"1\">b</ge></entry>";
            XmlDocument entry = new XmlDocument();
            entry.LoadXml(xml);
            XmlNode field = entry.DocumentElement.LastChild;
            SolidReport r = new SolidReport();
            Assert.AreEqual(0, r.Count);
            r.AddEntry(
                SolidReport.EntryType.StructureParentNotFound, entry.DocumentElement, field, "Test"
            );
            Assert.AreEqual(1, r.Count);
            XmlNode reportEntries = r.AllEntries();
            XmlNode reportEntry = reportEntries.FirstChild;
            Assert.AreEqual("0", reportEntry.Attributes["id"].Value);
            Assert.AreEqual("22", reportEntry.Attributes["record"].Value);
            //Assert.AreEqual("33", reportEntry.RecordStartLine);
            //Assert.AreEqual("44", reportEntry.RecordEndLine);
            Assert.AreEqual("ge", reportEntry.Attributes["marker"].Value);
            Assert.AreEqual("Test", reportEntry.InnerText);
            Assert.AreEqual("1", reportEntry.Attributes["field"].Value);
        }

        [Test]
        public void SolidReport_SaveOpen_Correct()
        {
            SolidReport save = new SolidReport();
            Assert.AreEqual(0, save.Count);
            save.AddEntry(
                SolidReport.EntryType.StructureParentNotFound, null, null, "Test"
            );
            Assert.AreEqual(1, save.Count);
            save.SaveAs("../../SolidReport_SaveOpen_Correct.xml");
            SolidReport open = SolidReport.OpenSolidReport("../../SolidReport_SaveOpen_Correct.xml");
            Assert.IsNotNull(open);
            Assert.AreEqual(1, open.Count);
        }

    }
}
