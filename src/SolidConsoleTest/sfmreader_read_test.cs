using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using SolidConsole;

namespace SolidConsoleTest
{
    [TestFixture]
    public class SFMReader_Read_Test
    {
        SfmCollection _sfmReader;
        bool _result;

        [TestFixtureSetUp]
        public void Init()
        {
            Uri uri = new Uri("file://c:/src/sil/solid/trunk/data/dict2.txt");
            _sfmReader = new SfmCollection(uri, Encoding.Default, "", 4096);
            _result = _sfmReader.Read();
            _result = _sfmReader.Read();
        }

        [Test]
        public void Result_True()
        {
            Assert.AreEqual(true, _result);
        }

        [Test]
        public void FieldCount_38()
        {
            Assert.AreEqual(38, _sfmReader.FieldCount);
        }

        [Test]
        public void Key0_Correct()
        {
            Assert.AreEqual("lx", _sfmReader.Key(0));
        }

        [Test]
        public void Key1_Correct()
        {
            Assert.AreEqual("ph", _sfmReader.Key(1));
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
