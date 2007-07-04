using System;
//using System.Collections.Generic;
using System.IO;
//using System.Text;
using System.Xml;
using NUnit.Framework;
using SolidEngine;

namespace SolidTests
{
    [TestFixture]
    public class SfmXmlReaderTests : Assertion
    {

        private XmlReader ReadOneRecordData()
        {
            string sfm =
                "\\_sh v3.0  269  MDF 4.0 (alternate hierarchy)\n" +
                "\\_DateStampHasFourDigitYear\n" +
                "\\lx a\n" +
                "\\ge b\n";
            XmlReader xmlReader = new SfmXmlReader(new StringReader(sfm));
            return xmlReader;
        }

        private void AssertStartDocument(XmlReader xmlReader)
        {
            Assert(xmlReader.ReadState == ReadState.Initial);
            Assert(xmlReader.NodeType == XmlNodeType.None);
            Assert(xmlReader.Depth == 0);
            Assert(!xmlReader.EOF);
        }

        private void AssertNode(
            XmlReader xmlReader,
            XmlNodeType nodeType,
            int depth,
            bool isEmptyElement,
            string name,
            string prefix,
            string localName,
            string namespaceURI,
            string value,
            int attributeCount)
        {
            Assert("#Read", xmlReader.Read());
            Assert("#ReadState", xmlReader.ReadState == ReadState.Interactive);
            Assert(!xmlReader.EOF);
            AssertNodeValues(xmlReader, nodeType, depth, isEmptyElement, name, prefix, localName, namespaceURI, value, attributeCount);
        }

        private void AssertNodeValues(
            XmlReader xmlReader,
            XmlNodeType nodeType,
            int depth,
            bool isEmptyElement,
            string name,
            string prefix,
            string localName,
            string namespaceURI,
            string value,
            int attributeCount)
        {
            AssertEquals("NodeType", nodeType, xmlReader.NodeType);
            AssertEquals("Depth", depth, xmlReader.Depth);
            AssertEquals("IsEmptyElement", isEmptyElement, xmlReader.IsEmptyElement);

            AssertEquals("name", name, xmlReader.Name);

            AssertEquals("prefix", prefix, xmlReader.Prefix);

            AssertEquals("localName", localName, xmlReader.LocalName);

            AssertEquals("namespaceURI", namespaceURI, xmlReader.NamespaceURI);

            AssertEquals("hasValue", (value != String.Empty), xmlReader.HasValue);

            AssertEquals("Value", value, xmlReader.Value);

            AssertEquals("hasAttributes", attributeCount > 0, xmlReader.HasAttributes);

            AssertEquals("attributeCount", attributeCount, xmlReader.AttributeCount);
        }

        private void AssertAttribute(
            XmlReader xmlReader,
            string name,
            string prefix,
            string localName,
            string namespaceURI,
            string value)
        {
            AssertEquals("value.Indexer", value, xmlReader[name]);

            AssertEquals("value.GetAttribute", value, xmlReader.GetAttribute(name));

            if (namespaceURI != String.Empty)
            {
                Assert(xmlReader[localName, namespaceURI] == value);
                Assert(xmlReader.GetAttribute(localName, namespaceURI) == value);
            }
        }

        private void AssertEndDocument(XmlReader xmlReader)
        {
            Assert("could read", !xmlReader.Read());
            AssertEquals("NodeType is not XmlNodeType.None", XmlNodeType.None, xmlReader.NodeType);
            AssertEquals("Depth is not 0", 0, xmlReader.Depth);
            AssertEquals("ReadState is not ReadState.EndOfFile", ReadState.EndOfFile, xmlReader.ReadState);
            Assert("not EOF", xmlReader.EOF);

            xmlReader.Close();
            AssertEquals("ReadState is not ReadState.Closed", ReadState.Closed, xmlReader.ReadState);
        }

        [Test]
        public void SFMEmptyDocument_Correct()
        {
            string sfm = @"";
            XmlReader xmlReader =
                new SfmXmlReader(new StringReader(sfm));

            AssertStartDocument(xmlReader);

            AssertNode(
                xmlReader, // xmlReader
                XmlNodeType.Element, // nodeType
                0, //depth
                false, // isEmptyElement
                "root", // name
                String.Empty, // prefix
                "root", // localName
                String.Empty, // namespaceURI
                String.Empty, // value
                0 // attributeCount
            );
            /*
            AssertAttribute(
                xmlReader, // xmlReader
                "bar", // name
                String.Empty, // prefix
                "bar", // localName
                String.Empty, // namespaceURI
                "baz" // value
            );
            */

            AssertNode(
                xmlReader, // xmlReader
                XmlNodeType.EndElement, // nodeType
                0, //depth
                false, // isEmptyElement
                "root", // name
                String.Empty, // prefix
                "root", // localName
                String.Empty, // namespaceURI
                String.Empty, // value
                0 // attributeCount
            );

            AssertEndDocument(xmlReader);
        }

        [Test]
        public void SFMHeaderDocument_Correct()
        {
            string sfm = "\\_a a\n"
                + "\\_b b";
            XmlReader xmlReader =
                new SfmXmlReader(new StringReader(sfm));

            AssertStartDocument(xmlReader);

            AssertNode(
                xmlReader, // xmlReader
                XmlNodeType.Element, // nodeType
                0, //depth
                false, // isEmptyElement
                "root", // name
                String.Empty, // prefix
                "root", // localName
                String.Empty, // namespaceURI
                String.Empty, // value
                2 // attributeCount
            );
            /*
            AssertAttribute(
                xmlReader, // xmlReader
                "bar", // name
                String.Empty, // prefix
                "bar", // localName
                String.Empty, // namespaceURI
                "baz" // value
            );
            */

            AssertNode(
                xmlReader, // xmlReader
                XmlNodeType.EndElement, // nodeType
                0, //depth
                false, // isEmptyElement
                "root", // name
                String.Empty, // prefix
                "root", // localName
                String.Empty, // namespaceURI
                String.Empty, // value
                2 // attributeCount
            );

            AssertEndDocument(xmlReader);
        }

        [Test]
        public void ReadSubtreeFromXml_Correct()
        {
            string xmlIn = "<root _sh=\"sh\" _DateTimeStampHasFourDigitYear=\"true\"><entry><lx>lex1</lx><ge>ggg</ge></entry></root>";
            XmlReader xmlReader = XmlReader.Create(new StringReader(xmlIn));
            xmlReader.ReadToFollowing("entry");
            XmlReader entryReader = xmlReader.ReadSubtree();

            AssertStartDocument(entryReader);

            AssertNode(
                entryReader, // xmlReader
                XmlNodeType.Element, // nodeType
                0, //depth
                false, // isEmptyElement
                "entry", // name
                String.Empty, // prefix
                "entry", // localName
                String.Empty, // namespaceURI
                String.Empty, // value
                0 // attributeCount
            );

            AssertNode(
                entryReader, // xmlReader
                XmlNodeType.Element, // nodeType
                1, //depth
                false, // isEmptyElement
                "lx", // name
                String.Empty, // prefix
                "lx", // localName
                String.Empty, // namespaceURI
                String.Empty, // value
                0 // attributeCount
            );

            AssertNode(
                entryReader, // xmlReader
                XmlNodeType.Text, // nodeType
                2, //depth
                false, // isEmptyElement
                String.Empty, // name
                String.Empty, // prefix
                String.Empty, // localName
                String.Empty, // namespaceURI
                "lex1", // value
                0 // attributeCount
            );

            AssertNode(
                entryReader, // xmlReader
                XmlNodeType.EndElement, // nodeType
                1, //depth
                false, // isEmptyElement
                "lx", // name
                String.Empty, // prefix
                "lx", // localName
                String.Empty, // namespaceURI
                String.Empty, // value
                0 // attributeCount
            );

            AssertNode(
                entryReader, // xmlReader
                XmlNodeType.Element, // nodeType
                1, //depth
                false, // isEmptyElement
                "ge", // name
                String.Empty, // prefix
                "ge", // localName
                String.Empty, // namespaceURI
                String.Empty, // value
                0 // attributeCount
            );

            AssertNode(
                entryReader, // xmlReader
                XmlNodeType.Text, // nodeType
                2, //depth
                false, // isEmptyElement
                String.Empty, // name
                String.Empty, // prefix
                String.Empty, // localName
                String.Empty, // namespaceURI
                "ggg", // value
                0 // attributeCount
            );

            AssertNode(
                entryReader, // xmlReader
                XmlNodeType.EndElement, // nodeType
                1, //depth
                false, // isEmptyElement
                "ge", // name
                String.Empty, // prefix
                "ge", // localName
                String.Empty, // namespaceURI
                String.Empty, // value
                0 // attributeCount
            );

            AssertNode(
                entryReader, // xmlReader
                XmlNodeType.EndElement, // nodeType
                0, //depth
                false, // isEmptyElement
                "entry", // name
                String.Empty, // prefix
                "entry", // localName
                String.Empty, // namespaceURI
                String.Empty, // value
                0 // attributeCount
            );

            AssertEndDocument(entryReader);
        }


        [Test]
        public void ReadSubtreeFromSfm_Correct()
        {
            string sfm =
                "\\_a 1\n" +
                "\\lx lex1\n" +
                "\\ge ggg\n";
            XmlReader xmlReader = new SfmXmlReader(new StringReader(sfm));
            xmlReader.ReadToFollowing("entry");
            XmlReader entryReader = xmlReader.ReadSubtree();

            AssertStartDocument(entryReader);

            AssertNode(
                entryReader, // xmlReader
                XmlNodeType.Element, // nodeType
                0, //depth
                false, // isEmptyElement
                "entry", // name
                String.Empty, // prefix
                "entry", // localName
                String.Empty, // namespaceURI
                String.Empty, // value
                3 // attributeCount
            );

            AssertNode(
                entryReader, // xmlReader
                XmlNodeType.Element, // nodeType
                1, //depth
                false, // isEmptyElement
                "lx", // name
                String.Empty, // prefix
                "lx", // localName
                String.Empty, // namespaceURI
                String.Empty, // value
                0 // attributeCount
            );

            AssertNode(
                entryReader, // xmlReader
                XmlNodeType.Text, // nodeType
                2, //depth
                false, // isEmptyElement
                String.Empty, // name
                String.Empty, // prefix
                String.Empty, // localName
                String.Empty, // namespaceURI
                "lex1", // value
                0 // attributeCount
            );

            AssertNode(
                entryReader, // xmlReader
                XmlNodeType.EndElement, // nodeType
                1, //depth
                false, // isEmptyElement
                "lx", // name
                String.Empty, // prefix
                "lx", // localName
                String.Empty, // namespaceURI
                String.Empty, // value
                0 // attributeCount
            );

            AssertNode(
                entryReader, // xmlReader
                XmlNodeType.Element, // nodeType
                1, //depth
                false, // isEmptyElement
                "ge", // name
                String.Empty, // prefix
                "ge", // localName
                String.Empty, // namespaceURI
                String.Empty, // value
                0 // attributeCount
            );

            AssertNode(
                entryReader, // xmlReader
                XmlNodeType.Text, // nodeType
                2, //depth
                false, // isEmptyElement
                String.Empty, // name
                String.Empty, // prefix
                String.Empty, // localName
                String.Empty, // namespaceURI
                "ggg", // value
                0 // attributeCount
            );

            AssertNode(
                entryReader, // xmlReader
                XmlNodeType.EndElement, // nodeType
                1, //depth
                false, // isEmptyElement
                "ge", // name
                String.Empty, // prefix
                "ge", // localName
                String.Empty, // namespaceURI
                String.Empty, // value
                0 // attributeCount
            );

            AssertNode(
               entryReader, // xmlReader
               XmlNodeType.EndElement, // nodeType
               0, //depth
               false, // isEmptyElement
               "entry", // name
               String.Empty, // prefix
               "entry", // localName
               String.Empty, // namespaceURI
               String.Empty, // value
               3 // attributeCount
           );
            
           AssertEndDocument(entryReader);
        }


        [Ignore]
        public void SFMSingleEntryDocument_Correct()
        {
            //string file = @"../../data/dict2-1entry.txt";
            XmlReader xmlReader = ReadOneRecordData();

            AssertStartDocument(xmlReader);

            // element root
            AssertNode(
                xmlReader, // xmlReader
                XmlNodeType.Element, // nodeType
                0, //depth
                false, // isEmptyElement
                "root", // name
                String.Empty, // prefix
                "root", // localName
                String.Empty, // namespaceURI
                String.Empty, // value
                2 // attributeCount
            );
            /*
            // elementvalue empty
            AssertNode(
                xmlReader, // xmlReader
                XmlNodeType.Text, // nodeType
                1, //depth
                false, // isEmptyElement
                String.Empty, // name
                String.Empty, // prefix
                String.Empty, // localName
                String.Empty, // namespaceURI
                String.Empty, // value
                0 // attributeCount
            );
            */
            // element entry
            AssertNode(
                xmlReader, // xmlReader
                XmlNodeType.Element, // nodeType
                1, //depth
                false, // isEmptyElement
                "entry", // name
                String.Empty, // prefix
                "entry", // localName
                String.Empty, // namespaceURI
                String.Empty, // value
                0 // attributeCount
            );
            // element entry
            AssertNode(
                xmlReader, // xmlReader
                XmlNodeType.Element, // nodeType
                2, //depth
                false, // isEmptyElement
                "lx", // name
                String.Empty, // prefix
                "lx", // localName
                String.Empty, // namespaceURI
                "a", // value
                0 // attributeCount
            );
            
            // elementvalue empty
            AssertNode(
                xmlReader, // xmlReader
                XmlNodeType.Text, // nodeType
                3, //depth
                false, // isEmptyElement
                "a", // name
                String.Empty, // prefix
                "a", // localName
                String.Empty, // namespaceURI
                String.Empty, // value
                0 // attributeCount
            );
            

            AssertNode(
                xmlReader, // xmlReader
                XmlNodeType.EndElement, // nodeType
                0, //depth
                false, // isEmptyElement
                "root", // name
                String.Empty, // prefix
                "root", // localName
                String.Empty, // namespaceURI
                String.Empty, // value
                0 // attributeCount
            );

            AssertEndDocument(xmlReader);
        }

    }

}

#if false

namespace SolidConsoleTest
{
    [TestFixture]
    public class SfmXmlReader_Test
    {
        XmlReader _sfmreader;
        XmlReader _reader;

        [TestFixtureSetUp]
        public void Init()
        {
            Uri uri = new Uri("file://c:/src/sil/solid/trunk/data/dict2-1entry.txt");
            _sfmreader = new SfmXmlReader(uri, Encoding.Default, null);

            _reader = new SfmXmlReader("file://c:/src/sil/solid/trunk/data/dict2-1entry.xml");        
        }

        [Test]
        public void NotYetTested()
        {
            XmlNodeType nt;

            while (_reader.Read())
            {
                string name;
                string value;
                nt = _reader.NodeType;
                switch (nt)
                {
                    case XmlNodeType.Element:
                        name = _reader.LocalName;
                        value = _reader.Value;
                        _reader.MoveToFirstAttribute();
                        Console.Out.Write("<" + name + ">");
                        break;
                    case XmlNodeType.Text:
                        value = _reader.Value;
                        Console.Out.Write(value);
                        break;
                    case XmlNodeType.EndElement:
                        name = _reader.LocalName;
                        value = _reader.Value;
                        Console.Out.Write("</" + name + ">\n");
                        break;
                }

            }

            Console.Out.Write("END");

            Assert.Fail("Implement this");
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
#endif