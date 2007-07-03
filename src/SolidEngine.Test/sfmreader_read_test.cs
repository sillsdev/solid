using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using NUnit.Framework;
using SolidEngine;

namespace SolidTests
{
    [TestFixture]
    public class SfmReader_Read_Test
    {
        [TestFixtureSetUp]
        public void Init()
        {
        }

        [Test]
        public void EmptySFMRecordRead_False()
        {
            string sfm = @"";
            SfmRecordReader r = new SfmRecordReader(new StringReader(sfm), 4096);
            bool result = r.Read();
            Assert.AreEqual(false, result);
        }

        [Test]
        public void HeaderOnlySFMRecordRead_False()
        {
            string sfm = 
                "\\_sh v3.0  269  MDF 4.0 (alternate hierarchy)\n" +
                "\\_DateStampHasFourDigitYear\n";
            SfmRecordReader r = new SfmRecordReader(new StringReader(sfm), 4096);
            bool result = r.Read();
            Assert.AreEqual(false, result);
        }

        private SfmRecordReader ReadOneRecordData()
        {
            string sfm =
                "\\_sh v3.0  269  MDF 4.0 (alternate hierarchy)\n" +
                "\\_DateStampHasFourDigitYear\n" +
                "\\lx a\n" +
                "\\ge b\n";
            SfmRecordReader r = new SfmRecordReader(new StringReader(sfm), 4096);
            bool result = r.Read();
            Assert.AreEqual(true, result);
            return r;
        }

        private SfmRecordReader ReadTwoRecordData()
        {
            string sfm =
                "\\_sh v3.0  269  MDF 4.0 (alternate hierarchy)\n" +
                "\\_DateStampHasFourDigitYear\n" +
                "\\lx a\n" +
                "\\ge b\n" +
                "\\lx c\n" +
                "\\gn d\n";
            SfmRecordReader r = new SfmRecordReader(new StringReader(sfm), 4096);
            bool result = r.Read();
            Assert.AreEqual(true, result);
            return r;
        }

        [Test]
        public void OneSFMRecordReadToEOF_Correct()
        {
            SfmRecordReader r = ReadOneRecordData();
            Assert.AreEqual(2, r.FieldCount);
            Assert.AreEqual("lx", r.Key(0));
        }

        [Test]
        public void OneSFMRecordReadToNextMarker_Correct()
        {
            SfmRecordReader r = ReadTwoRecordData();
            Assert.AreEqual(2, r.FieldCount);
            Assert.AreEqual("ge", r.Key(1));
        }

        [Test]
        public void OneSFMRecordRead_Key0_Correct()
        {
            SfmRecordReader r = ReadTwoRecordData();
            Assert.AreEqual("lx", r.Key(0));
        }

        [Test]
        public void OneSFMRecordRead_Key1_Correct()
        {
            SfmRecordReader r = ReadTwoRecordData();
            Assert.AreEqual("ge", r.Key(1));
        }

        [Test]
        public void RecordStartLine_Correct()
        {
            SfmRecordReader r = ReadTwoRecordData();
            Assert.AreEqual(3, r._recordStartLine);
        }

        [Test]
        public void RecordEndLine_Correct()
        {
            SfmRecordReader r = ReadTwoRecordData();
            Assert.AreEqual(4, r._recordEndLine);
        }

    }
}
