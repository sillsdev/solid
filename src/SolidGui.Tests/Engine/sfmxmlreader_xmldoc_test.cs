using System.IO;
using System.Xml;
using NUnit.Framework;
using SolidGui.Engine;


namespace SolidGui.Tests.Engine
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
            const string sfm = "";
            const string xml = @"<root></root>";
//            XmlReader xmlReader = new SfmXmlReader("file://c:/src/sil/solid/trunk/data/dict2-1entry.txt");
//            XmlReader xmlReader = new SfmXmlReader("file://c:/src/sil/solid/trunk/data/empty.txt");
            XmlReader xmlReader = new SfmXmlReader(new StringReader(sfm));
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlReader);
            string docxml = xmlDoc.InnerXml;
            Assert.AreEqual(xml, docxml);
        }

        [Test]
        public void HeaderDoc_Correct()
        {
            const string sfm = "\\_sh v3.0  269  MDF 4.0 (alternate hierarchy)\n" +
                               "\\_DateStampHasFourDigitYear\n";

            const string xml = "<root _sh=\"v3.0  269  MDF 4.0 (alternate hierarchy)\" _DateStampHasFourDigitYear=\"\"></root>";
            XmlReader xmlReader = new SfmXmlReader(new StringReader(sfm));
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlReader);
            string docxml = xmlDoc.InnerXml;
            Assert.AreEqual(xml, docxml);
        }

        [Test]
        public void OneRecordDoc_Correct()
        {
            const string sfm = "\\_a 1\n"
                               + "\\lx lex1\n"
                               + "\\ph ph1\n";

            const string xml = "<root _a=\"1\"><entry record=\"0\" startline=\"2\" endline=\"3\"><lx field=\"0\">lex1</lx><ph field=\"1\">ph1</ph></entry></root>";
            XmlReader xmlReader = new SfmXmlReader(new StringReader(sfm));
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlReader);
            string docxml = xmlDoc.InnerXml;
            Assert.AreEqual(xml, docxml);
        }

        [Test]
        public void OneRecordNoHeaderDoc_Correct()
        {
            const string sfm = "\\lx lex1\n"
                               + "\\ph ph1\n";

            const string xml = "<root><entry record=\"0\" startline=\"1\" endline=\"2\"><lx field=\"0\">lex1</lx><ph field=\"1\">ph1</ph></entry></root>";
            XmlReader xmlReader = new SfmXmlReader(new StringReader(sfm));
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlReader);
            string docxml = xmlDoc.InnerXml;
            Assert.AreEqual(xml, docxml);
        }

        [Test]
        public void OneEntrySfmDoc_Correct()
        {
            const string sfm = "\\_a 1\n"
                               + "\\lx lex1\n"
                               + "\\ph ph1\n";
            const string xml = "<root _a=\"1\"><entry record=\"0\" startline=\"2\" endline=\"3\"><lx field=\"0\">lex1</lx><ph field=\"1\">ph1</ph></entry></root>";
            XmlReader xmlReader = new SfmXmlReader(new StringReader(sfm));
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlReader);
            Assert.AreEqual(xml, xmlDoc.InnerXml);
        }

        [Test]
        public void ReadSubtree_Correct()
        {
            const string sfm = "\\_a 1\n" +
                               "\\lx lex1\n" +
                               "\\ph ph1\n";
            const string xml = "<entry record=\"0\" startline=\"2\" endline=\"3\"><lx field=\"0\">lex1</lx><ph field=\"1\">ph1</ph></entry>";
            XmlReader xmlReader = new SfmXmlReader(new StringReader(sfm));
            bool result = xmlReader.ReadToFollowing("entry");
            Assert.IsTrue(result);
            var entryReader = xmlReader.ReadSubtree();
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(entryReader);
            Assert.AreEqual(xml, xmlDoc.InnerXml);
        }

        [Test]
        public void ReadSubtreeNoHeader_Correct()
        {
            const string sfm = "\\lx lex1\n" +
                               "\\ph ph1\n";
            const string xml = "<entry record=\"0\" startline=\"1\" endline=\"2\"><lx field=\"0\">lex1</lx><ph field=\"1\">ph1</ph></entry>";
            XmlReader xmlReader = new SfmXmlReader(new StringReader(sfm));
            bool result = xmlReader.ReadToFollowing("entry");
            Assert.IsTrue(result);
            var entryReader = xmlReader.ReadSubtree();
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(entryReader);
            Assert.AreEqual(xml, xmlDoc.InnerXml);
        }

        [Test]
        public void ReadSubtreeEmptyValue_Correct()
        {
            const string sfm = "\\lx lex1\n" +
                               "\\qq \n" + 
                               "\\ph ph1\n";
            const string xml = "<entry record=\"0\" startline=\"1\" endline=\"3\"><lx field=\"0\">lex1</lx><qq field=\"1\"></qq><ph field=\"2\">ph1</ph></entry>";
            XmlReader xmlReader = new SfmXmlReader(new StringReader(sfm));
            bool result = xmlReader.ReadToFollowing("entry");
            Assert.IsTrue(result);
            var entryReader = xmlReader.ReadSubtree();
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(entryReader);
            Assert.AreEqual(xml, xmlDoc.InnerXml);
        }

        [Test]
        public void ReadSubtreeEmptyValueTabDelimited_Correct()
        {
            const string sfm = "\\lx\tlex1\n" +
                               "\\qq \n" +
                               "\\ph ph1\n";
            const string xml = "<entry record=\"0\" startline=\"1\" endline=\"3\"><lx field=\"0\">lex1</lx><qq field=\"1\"></qq><ph field=\"2\">ph1</ph></entry>";
            XmlReader xmlReader = new SfmXmlReader(new StringReader(sfm));
            bool result = xmlReader.ReadToFollowing("entry");
            Assert.IsTrue(result);
            var entryReader = xmlReader.ReadSubtree();
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(entryReader);
            Assert.AreEqual(xml, xmlDoc.InnerXml);
        }


    }
}