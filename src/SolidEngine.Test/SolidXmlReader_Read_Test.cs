using System;
//using System.Collections.Generic;
using System.IO;
//using System.Text;
using System.Xml;
using NUnit.Framework;
using Solid.Engine;


namespace SolidTests
{
    [TestFixture]
    public class SolidXmlReaderTests : Assertion
    {

        private XmlReader ReadOneRecordData()
        {
            string sfm =
                "\\_sh v3.0  269  MDF 4.0 (alternate hierarchy)\n" +
                "\\_DateStampHasFourDigitYear\n" +
                "\\lx a\n" +
                "\\ge b\n";
            XmlReader xmlReader = new SolidXmlReader(new StringReader(sfm), CreateSettings());
            return xmlReader;
        }

        private SolidSettings CreateSettings()
        {
            SolidSettings s = new SolidSettings();
            return s;
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
            XmlReader xmlReader = new SolidXmlReader(
                new StringReader(sfm), CreateSettings()
            );

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
            XmlReader xmlReader = new SolidXmlReader(
                new StringReader(sfm), CreateSettings()
            );

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

    }
}
