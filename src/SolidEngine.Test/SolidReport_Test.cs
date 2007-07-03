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
        public void SolidReport_AddEntry_Correct()
        {
            SolidReport r = new SolidReport();
            Assert.AreEqual(0, r.Entries.Count);
            r.Add(new SolidReport.Entry(
                SolidReport.EntryType.Error, null, null, "Test"
            ));
            Assert.AreEqual(1, r.Entries.Count);
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
