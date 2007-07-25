using System;
using System.IO;
using System.Text;
using System.Xml;
using NUnit.Framework;
using SolidEngine;

namespace SolidTests
{
    [TestFixture]
    public class SolidXmlReader_XmlDoc_Test
    {

        [TestFixtureSetUp]
        public void Init()
        {
        }

        private SolidSettings CreateSettings()
        {
            SolidSettings s = new SolidSettings();
            return s;
        }

        [Test]
        public void EmptyDoc_Correct()
        {
            string sfm = "";
            string xml = @"<root></root>";
            //            XmlReader xmlReader = new SolidXmlReader("file://c:/src/sil/solid/trunk/data/dict2-1entry.txt");
            //            XmlReader xmlReader = new SolidXmlReader("file://c:/src/sil/solid/trunk/data/empty.txt");
            XmlReader xmlReader = new SolidXmlReader(new StringReader(sfm), CreateSettings());
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlReader);
            string docxml = xmlDoc.InnerXml;
            Assert.AreEqual(xml, docxml);
        }

        [Test]
        public void HeaderDoc_Correct()
        {
            string sfm = "\\_sh v3.0  269  MDF 4.0 (alternate hierarchy)\n" +
                "\\_DateStampHasFourDigitYear\n";

            string xml = "<root _sh=\"v3.0  269  MDF 4.0 (alternate hierarchy)\" _DateStampHasFourDigitYear=\"\"></root>";
            XmlReader xmlReader = new SolidXmlReader(new StringReader(sfm), CreateSettings());
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlReader);
            string docxml = xmlDoc.InnerXml;
            Assert.AreEqual(xml, docxml);
        }

        [Test]
        public void OneRecordDoc_Correct()
        {
            string sfm = "\\_a 1\n"
                + "\\lx lex1\n"
                + "\\ph ph1\n";

            string xml = "<root _a=\"1\"><entry record=\"0\" startline=\"2\" endline=\"3\"><lx field=\"0\"><data>lex1</data></lx><ph field=\"1\"><data>ph1</data></ph></entry></root>";
            XmlReader xmlReader = new SolidXmlReader(new StringReader(sfm), CreateSettings());
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlReader);
            string docxml = xmlDoc.InnerXml;
            Assert.AreEqual(xml, docxml);
        }

        [Test]
        public void OneRecordNoHeaderDoc_Correct()
        {
            string sfm = "\\lx lex1\n"
                + "\\ph ph1\n";

            string xml = "<root><entry record=\"0\" startline=\"1\" endline=\"2\"><lx field=\"0\"><data>lex1</data></lx><ph field=\"1\"><data>ph1</data></ph></entry></root>";
            XmlReader xmlReader = new SolidXmlReader(new StringReader(sfm), CreateSettings());
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlReader);
            string docxml = xmlDoc.InnerXml;
            Assert.AreEqual(xml, docxml);
        }

        [Test]
        public void OneEntrySfmDoc_Correct()
        {
            string sfm = "\\_a 1\n"
                + "\\lx lex1\n"
                + "\\ph ph1\n";
            string xml = "<root _a=\"1\"><entry record=\"0\" startline=\"2\" endline=\"3\"><lx field=\"0\"><data>lex1</data></lx><ph field=\"1\"><data>ph1</data></ph></entry></root>";
            XmlReader xmlReader = new SolidXmlReader(new StringReader(sfm), CreateSettings());
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlReader);
            Assert.AreEqual(xml, xmlDoc.InnerXml);
        }

        [Test]
        public void ReadSubtree_Correct()
        {
            string sfm =
                "\\_a 1\n" +
                "\\lx lex1\n" +
                "\\ph ph1\n";
            string xml = "<entry record=\"0\" startline=\"2\" endline=\"3\"><lx field=\"0\"><data>lex1</data></lx><ph field=\"1\"><data>ph1</data></ph></entry>";
            XmlReader xmlReader = new SolidXmlReader(new StringReader(sfm), CreateSettings());
            bool result = xmlReader.ReadToFollowing("entry");
            Assert.IsTrue(result);
            XmlReader entryReader = xmlReader.ReadSubtree();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(entryReader);
            Assert.AreEqual(xml, xmlDoc.InnerXml);
        }

        [Test]
        public void ReadSubtreeNoHeader_Correct()
        {
            string sfm =
                "\\lx lex1\n" +
                "\\ph ph1\n";
            string xml = "<entry record=\"0\" startline=\"1\" endline=\"2\"><lx field=\"0\"><data>lex1</data></lx><ph field=\"1\"><data>ph1</data></ph></entry>";
            XmlReader xmlReader = new SolidXmlReader(new StringReader(sfm), CreateSettings());
            bool result = xmlReader.ReadToFollowing("entry");
            Assert.IsTrue(result);
            XmlReader entryReader = xmlReader.ReadSubtree();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(entryReader);
            Assert.AreEqual(xml, xmlDoc.InnerXml);
        }

        [Test]
        public void ReadSubtreeEmptyValue_Correct()
        {
            string sfm =
                "\\lx lex1\n" +
                "\\qq \n" +
                "\\ph ph1\n";
            string xml = "<entry record=\"0\" startline=\"1\" endline=\"3\"><lx field=\"0\"><data>lex1</data></lx><qq field=\"1\"><data /></qq><ph field=\"2\"><data>ph1</data></ph></entry>";
            XmlReader xmlReader = new SolidXmlReader(new StringReader(sfm), CreateSettings());
            bool result = xmlReader.ReadToFollowing("entry");
            Assert.IsTrue(result);
            XmlReader entryReader = xmlReader.ReadSubtree();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(entryReader);
            Assert.AreEqual(xml, xmlDoc.InnerXml);
        }

        [Test]
        public void ReadSubtreeEmptyValueTabDelimited_Correct()
        {
            string sfm =
                "\\lx\tlex1\n" +
                "\\qq \n" +
                "\\ph ph1\n";
            string xml = "<entry record=\"0\" startline=\"1\" endline=\"3\"><lx field=\"0\"><data>lex1</data></lx><qq field=\"1\"><data /></qq><ph field=\"2\"><data>ph1</data></ph></entry>";
            XmlReader xmlReader = new SolidXmlReader(new StringReader(sfm), CreateSettings());
            bool result = xmlReader.ReadToFollowing("entry");
            Assert.IsTrue(result);
            XmlReader entryReader = xmlReader.ReadSubtree();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(entryReader);
            Assert.AreEqual(xml, xmlDoc.InnerXml);
        }


    }
}
