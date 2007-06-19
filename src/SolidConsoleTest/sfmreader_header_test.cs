using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using SolidConsole;

namespace SolidConsoleTest
{
    [TestFixture]
    public class SFMReader_Header_Test
    {
        SfmCollection _sfmReader;
        bool _result;

        [TestFixtureSetUp]
        public void Init()
        {
            Uri uri = new Uri("file://c:/src/sil/solid/trunk/data/dict2.txt");
            _sfmReader = new SfmCollection(uri, Encoding.Default, "", 4096);
            _result = _sfmReader.Read();
        }

        [Test]
        public void Result_True()
        {
            Assert.AreEqual(true, _result);
        }

        [Test]
        public void FieldCount_2()
        {
            Assert.AreEqual(2, _sfmReader.FieldCount);
        }

        [Test]
        public void Field0_Key_Correct()
        {
            SfmCollection.SfmField f = _sfmReader.Field(0);
            Assert.AreEqual("_sh", f.key);
        }

        [Test]
        public void Field0_Value_Correct()
        {
            SfmCollection.SfmField f = _sfmReader.Field(0);
            Assert.AreEqual("v3.0  269  MDF 4.0 (alternate hierarchy)", f.value);
        }

        [Test]
        public void Key0_Correct()
        {
            Assert.AreEqual("_sh", _sfmReader.Key(0));
        }

        [Test]
        public void Key1_Correct()
        {
            Assert.AreEqual("_DateStampHasFourDigitYear", _sfmReader.Key(1));
        }


    }
}
