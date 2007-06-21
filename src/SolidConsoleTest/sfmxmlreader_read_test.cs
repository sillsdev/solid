using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using SolidConsole;

namespace SolidConsoleTest
{
    [TestFixture]
    public class SfmXmlReader_Test
    {
        SfmXmlReader _reader;

        [TestFixtureSetUp]
        public void Init()
        {
            Uri uri = new Uri("file://c:/src/sil/solid/trunk/data/dict2.txt");
            _reader = new SfmXmlReader(uri, Encoding.Default, null);
        }

        [Test]
        public void NotYetTested()
        {
            Assert.AreEqual(true, false);
        }
/*
        [Test]
        public void FieldCount_2()
        {
            Assert.AreEqual(2, _reader.FieldCount);
        }

        [Test]
        public void Field0_Key_Correct()
        {
            SfmCollection.SfmField f = _reader.Field(0);
            Assert.AreEqual("_sh", f.key);
        }

        [Test]
        public void Field0_Value_Correct()
        {
            SfmCollection.SfmField f = _reader.Field(0);
            Assert.AreEqual("v3.0  269  MDF 4.0 (alternate hierarchy)", f.value);
        }

        [Test]
        public void Key0_Correct()
        {
            Assert.AreEqual("_sh", _reader.Key(0));
        }

        [Test]
        public void Key1_Correct()
        {
            Assert.AreEqual("_DateStampHasFourDigitYear", _reader.Key(1));
        }
*/

    }
}
