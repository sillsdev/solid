using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using SolidConsole;

namespace SolidConsoleTest
{
    [TestFixture]
    public class SfmReader_Header_Test
    {
        SfmRecordReader _sfmReader;
        bool _result;

        [TestFixtureSetUp]
        public void Init()
        {
            Uri uri = new Uri("file://c:/src/sil/solid/trunk/data/dict2.txt");
            _sfmReader = new SfmRecordReader(uri, Encoding.Default, "", 4096);
            _result = _sfmReader.Read();
        }

        [Test]
        public void RecordRead_True()
        {
            Assert.AreEqual(true, _result);
        }

        [Test]
        public void HeaderFieldCount_2()
        {
            Assert.AreEqual(2, _sfmReader.FieldCount);
        }

        [Test]
        public void HeaderField0_Key_Correct()
        {
            SfmRecordReader.SfmField f = _sfmReader.Field(0);
            Assert.AreEqual("_sh", f.key);
        }

        [Test]
        public void HeaderField0_Value_Correct()
        {
            SfmRecordReader.SfmField f = _sfmReader.Field(0);
            Assert.AreEqual("v3.0  269  MDF 4.0 (alternate hierarchy)", f.value);
        }

        [Test]
        public void HeaderKey0_Correct()
        {
            Assert.AreEqual("_sh", _sfmReader.Key(0));
        }

        [Test]
        public void HeaderKey1_Correct()
        {
            Assert.AreEqual("_DateStampHasFourDigitYear", _sfmReader.Key(1));
        }

        [Test]
        public void HeaderStartLine_Correct()
        {
            Assert.AreEqual(1, _sfmReader._recordStartLine);
        }

        [Test]
        public void HeaderEndLine_Correct()
        {
            Assert.AreEqual(3, _sfmReader._recordEndLine);
        }

    }
}
