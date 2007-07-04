using System;
using System.IO;
using System.Text;
using System.Xml;
using NUnit.Framework;
using SolidEngine;

namespace SolidTests
{
    [TestFixture]
    public class SfmXmlReader_XmlDoc_Test
    {

        [TestFixtureSetUp]
        public void Init()
        {
        }

        [Test]
        public void EmptyDoc_Correct()
        {
            string sfm = "";
            string xml = @"<root></root>";
//            XmlReader xmlReader = new SfmXmlReader("file://c:/src/sil/solid/trunk/data/dict2-1entry.txt");
//            XmlReader xmlReader = new SfmXmlReader("file://c:/src/sil/solid/trunk/data/empty.txt");
            XmlReader xmlReader = new SfmXmlReader(new StringReader(sfm));
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
            XmlReader xmlReader = new SfmXmlReader(new StringReader(sfm));
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
            string xml = "<root _a=\"1\"><entry record=\"0\" startline=\"2\" endline=\"3\"><lx field=\"0\">lex1</lx><ph field=\"1\">ph1</ph></entry></root>";
            XmlReader xmlReader = new SfmXmlReader(new StringReader(sfm));
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
            string xml = "<entry record=\"0\" startline=\"2\" endline=\"3\"><lx field=\"0\">lex1</lx><ph field=\"1\">ph1</ph></entry>";
            XmlReader xmlReader = new SfmXmlReader(new StringReader(sfm));
            xmlReader.ReadToFollowing("entry");
            XmlReader entryReader = xmlReader.ReadSubtree();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(entryReader);
            Assert.AreEqual(xml, xmlDoc.InnerXml);
       }


    }
}
