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

            SolidReport.Entry reportEntry = r.Entries[0]; 
            Assert.AreEqual(22, reportEntry.RecordID);
            Assert.AreEqual(1, reportEntry.FieldID);
            //Assert.AreEqual(33, reportEntry.RecordStartLine); 
            //Assert.AreEqual(44, reportEntry.RecordEndLine); 
            Assert.AreEqual("ge", reportEntry.Marker); 
            Assert.AreEqual("Test", reportEntry.Description); 
        }

        [Test, Ignore] //!!! TODO Streaming isn't working yet. But currently we don't need to save it anyway.
        public void SolidReport_SaveOpen_Correct()
        {
            SolidReport save = new SolidReport();
            Assert.AreEqual(0, save.Count);
            save.AddEntry(SolidReport.EntryType.StructureParentNotFound, null, null, "Test");
            Assert.AreEqual(1, save.Count);
            save.SaveAs("../../SolidReport_SaveOpen_Correct.xml");
            SolidReport open = SolidReport.OpenSolidReport("../../SolidReport_SaveOpen_Correct.xml");
            Assert.IsNotNull(open);
            Assert.AreEqual(1, open.Count);
        }

    }
}
