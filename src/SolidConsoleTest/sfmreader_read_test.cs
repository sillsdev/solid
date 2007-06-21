using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using SolidConsole;

namespace SolidConsoleTest
{
    [TestFixture]
    public class SfmReader_Read_Test
    {
        SfmRecordReader _sfmReader;
        bool _result;

        [TestFixtureSetUp]
        public void Init()
        {
            Uri uri = new Uri("file://c:/src/sil/solid/trunk/data/dict2.txt");
            _sfmReader = new SfmRecordReader(uri, Encoding.Default, "", 4096);
            _result = _sfmReader.Read();
            _result = _sfmReader.Read();
        }

        [Test]
        public void RecordRead_True()
        {
            Assert.AreEqual(true, _result);
        }

        [Test]
        public void RecordFieldCount_38()
        {
            Assert.AreEqual(38, _sfmReader.FieldCount);
        }

        [Test]
        public void RecordKey0_Correct()
        {
            Assert.AreEqual("lx", _sfmReader.Key(0));
        }

        [Test]
        public void RecordKey1_Correct()
        {
            Assert.AreEqual("ph", _sfmReader.Key(1));
        }

        [Test]
        public void RecordStartLine_Correct()
        {
            Assert.AreEqual(4, _sfmReader._recordStartLine);
        }

        [Test]
        public void RecordEndLine_Correct()
        {
            Assert.AreEqual(44, _sfmReader._recordEndLine);
        }


        /*
        [Test]
        public void Key1_Correct()
        { 
            foreach (SFMRecord record in _sfmReader)
            {
                foreach (SFMField field in record)
                {
                }
            }
        }
        */

    }
}
