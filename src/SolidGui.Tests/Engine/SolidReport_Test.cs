using System.Xml;
using NUnit.Framework;
using SolidGui.Engine;

namespace SolidGui.Tests.Engine
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
            var r = new SolidReport();
            Assert.AreEqual(0, r.Count);
        }

        [Test]
        public void SolidReport_AddNullEntry_Correct()
        {
            var r = new SolidReport();
            Assert.AreEqual(0, r.Count);
            r.AddEntry(
                SolidReport.EntryType.StructureParentNotFound, null, null, "Test"
                );
            Assert.AreEqual(1, r.Count);
        }
        /*
        [Test]
        public void SolidReport_AddEntry_Correct()
        {
            // TODO this test isn't high value.  Improve it. CP 2010-08
            const string xml = "<entry record=\"22\" startline=\"33\" endline=\"44\"><lx field=\"0\">a</lx><ge field=\"1\">b</ge></entry>";
            var entry = new XmlDocument();
            entry.LoadXml(xml);
            var field = entry.DocumentElement.LastChild;
            var r = new SolidReport();
            Assert.AreEqual(0, r.Count);
            r.AddEntry(
                SolidReport.EntryType.StructureParentNotFound, entry.DocumentElement, field, "Test"
                );
            Assert.AreEqual(1, r.Count);

            var reportEntry = r.Entries[0]; 
            Assert.AreEqual(22, reportEntry.RecordID);
            Assert.AreEqual(1, reportEntry.FieldID);
            //Assert.AreEqual(33, reportEntry.RecordStartLine); 
            //Assert.AreEqual(44, reportEntry.RecordEndLine); 
            Assert.AreEqual("ge", reportEntry.Marker); 
            Assert.AreEqual("Test", reportEntry.Description); 
        }
         */

        [Test, Ignore] //!!! TODO Streaming isn't working yet. But currently we don't need to save it anyway.
        public void SolidReport_SaveOpen_Correct()
        {
            var save = new SolidReport();
            Assert.AreEqual(0, save.Count);
            save.AddEntry(SolidReport.EntryType.StructureParentNotFound, null, null, "Test");
            Assert.AreEqual(1, save.Count);
            save.SaveAs("../../SolidReport_SaveOpen_Correct.xml");
            var open = SolidReport.OpenSolidReport("../../SolidReport_SaveOpen_Correct.xml");
            Assert.IsNotNull(open);
            Assert.AreEqual(1, open.Count);
        }

    }
}