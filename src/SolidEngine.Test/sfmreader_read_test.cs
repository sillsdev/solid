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

        [Test]
        public void ReadNoHeader_Correct()
        {
            string sfm =
                "\\lx a\n" +
                "\\ge b\n";
            SfmRecordReader r = new SfmRecordReader(new StringReader(sfm), 4096);
            bool result = r.Read();
            Assert.IsTrue(result);
            Assert.AreEqual(0, r.Header.Count);
            Assert.AreEqual(2, r.FieldCount);
            Assert.AreEqual("a", r.Value("lx"));
            Assert.AreEqual("b", r.Value("ge"));
        }

        [Test]
        public void ReadNoHeaderTabDelimited_Correct()
        {
            string sfm =
                "\\lx\ta\n" +
                "\\ge\tb\n";
            SfmRecordReader r = new SfmRecordReader(new StringReader(sfm), 4096);
            bool result = r.Read();
            Assert.IsTrue(result);
            Assert.AreEqual(0, r.Header.Count);
            Assert.AreEqual(2, r.FieldCount);
            Assert.AreEqual("a", r.Value("lx"));
            Assert.AreEqual("b", r.Value("ge"));
        }

        [Test]
        public void ReadEmptyValue_Correct()
        {
            string sfm =
                "\\lx a\n" +
                "\\ge\n";
            SfmRecordReader r = new SfmRecordReader(new StringReader(sfm), 4096);
            bool result = r.Read();
            Assert.IsTrue(result);
            Assert.AreEqual(0, r.Header.Count);
            Assert.AreEqual(2, r.FieldCount);
            Assert.AreEqual("a", r.Value("lx"));
            Assert.AreEqual("", r.Value("ge"));
        }

        [Test]
        public void ReadEmptyKey_Correct()
        {
            string sfm =
                "\\lx a\n" +
                "\\\n" +
                "\\ge b";
            SfmRecordReader r = new SfmRecordReader(new StringReader(sfm), 4096);
            bool result = r.Read();
            Assert.IsTrue(result);
            Assert.AreEqual(0, r.Header.Count);
            Assert.AreEqual(3, r.FieldCount);
            Assert.AreEqual("a", r.Value("lx"));
            Assert.AreEqual("b", r.Value("ge"));
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
            Assert.AreEqual(3, r.RecordStartLine);
        }

        [Test]
        public void RecordEndLine_Correct()
        {
            SfmRecordReader r = ReadTwoRecordData();
            Assert.AreEqual(4, r.RecordEndLine);
        }

        [Test]
        public void Record_EOF_Correct()
        {
            SfmRecordReader r = ReadTwoRecordData(); // Reads the first record for us.
            bool result = r.Read();
            Assert.IsTrue(result);
            result = r.Read();
            Assert.IsFalse(result);
        }

        [Test]
        public void RecordID_Correct()
        {
            SfmRecordReader r = ReadTwoRecordData();
            Assert.AreEqual(0, r.RecordID);
            bool result = r.Read();
            Assert.IsTrue(result); // Should be for two records.
            Assert.AreEqual(1, r.RecordID);
        }
    }
}
