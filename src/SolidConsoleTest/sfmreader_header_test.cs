using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using NUnit.Framework;
using SolidConsole;

namespace SolidTests
{
    [TestFixture]
    public class SfmReader_Header_Test
    {
        [TestFixtureSetUp]
        public void Init()
        {
        }

        [Test]
        public void EmptySFM_HeaderCount_0()
        {
            string sfm = @"";
            SfmRecordReader r = new SfmRecordReader(new StringReader(sfm), 4096);
            bool result = r.Read();
            Assert.AreEqual(false, result);
            Assert.AreEqual(0, r.Header.Count);
        }

        [Test]
        public void HeaderOnly_Header_Correct()
        {
            string sfm =
                "\\_sh v3.0  269  MDF 4.0 (alternate hierarchy)\n" +
                "\\_DateStampHasFourDigitYear\n";
            SfmRecordReader r = new SfmRecordReader(new StringReader(sfm), 4096);
            bool result = r.Read();
            Assert.AreEqual(false, result);
            Assert.AreEqual(2, r.Header.Count);
            Assert.AreEqual("_sh", r.Header[0].key);
            Assert.AreEqual("v3.0  269  MDF 4.0 (alternate hierarchy)", r.Header[0].value);
            Assert.AreEqual("_DateStampHasFourDigitYear", r.Header[1].key);
            Assert.AreEqual("", r.Header[1].value);
        }

    }
}
