using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using NUnit.Framework;
using SolidConsole;

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

        [Test]
        public void OneSFMRecordRead_Correct()
        {
            string file = "../../../../data/dict2.txt";
            SfmRecordReader r = new SfmRecordReader(new StreamReader(file), 4096);
            bool result = r.Read();
            Assert.AreEqual(true, result);
            Assert.AreEqual(38, r.FieldCount);
            Assert.AreEqual("lx", r.Key(0));
        }

        [Test]
        public void OneSFMRecordRead_FieldCount_Correct()
        {
            string file = "../../../../data/dict2.txt";
            SfmRecordReader r = new SfmRecordReader(new StreamReader(file), 4096);
            bool result = r.Read();
            Assert.AreEqual(true, result);
            Assert.AreEqual(38, r.FieldCount);
        }

        [Test]
        public void OneSFMRecordRead_Key0_Correct()
        {
            string file = "../../../../data/dict2.txt";
            SfmRecordReader r = new SfmRecordReader(new StreamReader(file), 4096);
            bool result = r.Read();
            Assert.AreEqual(true, result);
            Assert.AreEqual("lx", r.Key(0));
        }

        [Test]
        public void OneSFMRecordRead_Key1_Correct()
        {
            string file = "../../../../data/dict2.txt";
            SfmRecordReader r = new SfmRecordReader(new StreamReader(file), 4096);
            bool result = r.Read();
            Assert.AreEqual(true, result);
            Assert.AreEqual("ph", r.Key(1));
        }

        [Test]
        public void RecordStartLine_Correct()
        {
            string file = "../../../../data/dict2.txt";
            SfmRecordReader r = new SfmRecordReader(new StreamReader(file), 4096);
            bool result = r.Read();
            Assert.AreEqual(true, result);
            Assert.AreEqual(4, r._recordStartLine);
        }

        [Test]
        public void RecordEndLine_Correct()
        {
            string file = "../../../../data/dict2.txt";
            SfmRecordReader r = new SfmRecordReader(new StreamReader(file), 4096);
            bool result = r.Read();
            Assert.AreEqual(true, result);
            Assert.AreEqual(44, r._recordEndLine);
        }

    }
}
