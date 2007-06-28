using System;
//using System.Collections.Generic;
using System.IO;
//using System.Text;
using System.Xml;
using NUnit.Framework;
using SolidConsole;

namespace SolidTests
{
    [TestFixture]
    public class SfmXmlReaderTests : Assertion
    {
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
        public void SFMSingleEntryDocument_Correct()
        {
            string file = @"../../../../data/dict2-1entry.txt";
            XmlReader xmlReader = new SfmXmlReader(new StreamReader(file));

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
                0 // attributeCount
            );

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

            // elementvalue empty
            AssertNode(
                xmlReader, // xmlReader
                XmlNodeType.Text, // nodeType
                2, //depth
                false, // isEmptyElement
                String.Empty, // name
                String.Empty, // prefix
                String.Empty, // localName
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

        // expecting parser error
        [Test]
        public void EmptyElementWithBadName()
        {
            string xml = "<1foo/>";
            XmlReader xmlReader =
                new SfmXmlReader(new StringReader(xml));

            bool caughtXmlException = false;

            try
            {
                xmlReader.Read();
            }
            catch (XmlException)
            {
                caughtXmlException = true;
            }

            Assert(caughtXmlException);
        }

        [Test]
        public void EmptyElementWithStartAndEndTag()
        {
            string xml = "<foo></foo>";
            XmlReader xmlReader =
                new SfmXmlReader(new StringReader(xml));

            AssertStartDocument(xmlReader);

            AssertNode(
                xmlReader, // xmlReader
                XmlNodeType.Element, // nodeType
                0, //depth
                false, // isEmptyElement
                "foo", // name
                String.Empty, // prefix
                "foo", // localName
                String.Empty, // namespaceURI
                String.Empty, // value
                0 // attributeCount
            );

            AssertNode(
                xmlReader, // xmlReader
                XmlNodeType.EndElement, // nodeType
                0, //depth
                false, // isEmptyElement
                "foo", // name
                String.Empty, // prefix
                "foo", // localName
                String.Empty, // namespaceURI
                String.Empty, // value
                0 // attributeCount
            );

            AssertEndDocument(xmlReader);
        }

        // checking parser
        [Test]
        public void EmptyElementWithStartAndEndTagWithWhitespace()
        {
            string xml = "<foo ></foo >";
            XmlReader xmlReader =
                new SfmXmlReader(new StringReader(xml));

            AssertStartDocument(xmlReader);

            AssertNode(
                xmlReader, // xmlReader
                XmlNodeType.Element, // nodeType
                0, //depth
                false, // isEmptyElement
                "foo", // name
                String.Empty, // prefix
                "foo", // localName
                String.Empty, // namespaceURI
                String.Empty, // value
                0 // attributeCount
            );

            AssertNode(
                xmlReader, // xmlReader
                XmlNodeType.EndElement, // nodeType
                0, //depth
                false, // isEmptyElement
                "foo", // name
                String.Empty, // prefix
                "foo", // localName
                String.Empty, // namespaceURI
                String.Empty, // value
                0 // attributeCount
            );

            AssertEndDocument(xmlReader);
        }

        [Test]
        public void EmptyElementWithAttribute()
        {
            string xml = @"<foo bar=""baz""/>";
            XmlReader xmlReader =
                new SfmXmlReader(new StringReader(xml));

            AssertStartDocument(xmlReader);

            AssertNode(
                xmlReader, // xmlReader
                XmlNodeType.Element, // nodeType
                0, //depth
                true, // isEmptyElement
                "foo", // name
                String.Empty, // prefix
                "foo", // localName
                String.Empty, // namespaceURI
                String.Empty, // value
                1 // attributeCount
            );

            AssertAttribute(
                xmlReader, // xmlReader
                "bar", // name
                String.Empty, // prefix
                "bar", // localName
                String.Empty, // namespaceURI
                "baz" // value
            );

            AssertEndDocument(xmlReader);
        }

        [Test]
        public void EmptyElementInNamespace()
        {
            string xml = @"<foo:bar xmlns:foo='http://foo/' />";
            XmlReader xmlReader =
                new SfmXmlReader(new StringReader(xml));

            AssertStartDocument(xmlReader);

            AssertNode(
                xmlReader, // xmlReader
                XmlNodeType.Element, // nodeType
                0, // depth
                true, // isEmptyElement
                "foo:bar", // name
                "foo", // prefix
                "bar", // localName
                "http://foo/", // namespaceURI
                String.Empty, // value
                1 // attributeCount
            );

            AssertAttribute(
                xmlReader, // xmlReader
                "xmlns:foo", // name
                "xmlns", // prefix
                "foo", // localName
                "http://www.w3.org/2000/xmlns/", // namespaceURI
                "http://foo/" // value
            );

            AssertEquals("http://foo/", xmlReader.LookupNamespace("foo"));

            AssertEndDocument(xmlReader);
        }

        [Test]
        public void EntityReferenceInAttribute()
        {
            string xml = "<foo bar='&baz;'/>";
            XmlReader xmlReader =
                new SfmXmlReader(new StringReader(xml));

            AssertStartDocument(xmlReader);

            AssertNode(
                xmlReader, // xmlReader
                XmlNodeType.Element, // nodeType
                0, //depth
                true, // isEmptyElement
                "foo", // name
                String.Empty, // prefix
                "foo", // localName
                String.Empty, // namespaceURI
                String.Empty, // value
                1 // attributeCount
            );

            AssertAttribute(
                xmlReader, // xmlReader
                "bar", // name
                String.Empty, // prefix
                "bar", // localName
                String.Empty, // namespaceURI
                "&baz;" // value
            );

            AssertEndDocument(xmlReader);
        }

        /*
        [Test]
        public void FragmentConstructor()
        {
            XmlDocument doc = new XmlDocument();
            //			doc.LoadXml("<root/>");

            string xml = @"<foo><bar xmlns=""NSURI"">TEXT NODE</bar></foo>";
            MemoryStream ms = new MemoryStream(Encoding.Default.GetBytes(xml));

            XmlParserContext ctx = new XmlParserContext(doc.NameTable, new XmlNamespaceManager(doc.NameTable), "", "", "", "",
                doc.BaseURI, "", XmlSpace.Default, Encoding.Default);

            XmlTextReader xmlReader = new SfmXmlReader(ms, XmlNodeType.Element, ctx);
            AssertNode(xmlReader, XmlNodeType.Element, 0, false, "foo", "", "foo", "", "", 0);

            AssertNode(xmlReader, XmlNodeType.Element, 1, false, "bar", "", "bar", "NSURI", "", 1);

            AssertNode(xmlReader, XmlNodeType.Text, 2, false, "", "", "", "", "TEXT NODE", 0);

            AssertNode(xmlReader, XmlNodeType.EndElement, 1, false, "bar", "", "bar", "NSURI", "", 0);

            AssertNode(xmlReader, XmlNodeType.EndElement, 0, false, "foo", "", "foo", "", "", 0);

            AssertEndDocument(xmlReader);
        }
        */
        [Test]
        public void AttributeWithCharacterReference()
        {
            string xml = @"<a value='hello &amp; world' />";
            XmlReader xmlReader =
                new SfmXmlReader(new StringReader(xml));
            xmlReader.Read();
            AssertEquals("hello & world", xmlReader["value"]);
        }

        [Test]
        public void AttributeWithEntityReference()
        {
            string xml = @"<a value='hello &ent; world' />";
            XmlReader xmlReader =
                new SfmXmlReader(new StringReader(xml));
            xmlReader.Read();
            xmlReader.MoveToFirstAttribute();
            xmlReader.ReadAttributeValue();
            AssertEquals("hello ", xmlReader.Value);
            Assert(xmlReader.ReadAttributeValue());
            AssertEquals(XmlNodeType.EntityReference, xmlReader.NodeType);
            AssertEquals("ent", xmlReader.Name);
            AssertEquals(XmlNodeType.EntityReference, xmlReader.NodeType);
            Assert(xmlReader.ReadAttributeValue());
            AssertEquals(" world", xmlReader.Value);
            AssertEquals(XmlNodeType.Text, xmlReader.NodeType);
            Assert(!xmlReader.ReadAttributeValue());
            AssertEquals(" world", xmlReader.Value); // remains
            AssertEquals(XmlNodeType.Text, xmlReader.NodeType);
            xmlReader.ReadAttributeValue();
            AssertEquals(XmlNodeType.Text, xmlReader.NodeType);
        }

        [Test]
        public void QuoteChar()
        {
            string xml = @"<a value='hello &amp; world' value2="""" />";
            XmlReader xmlReader =
                new SfmXmlReader(new StringReader(xml));
            xmlReader.Read();
            xmlReader.MoveToFirstAttribute();
            AssertEquals("First", '\'', xmlReader.QuoteChar);
            xmlReader.MoveToNextAttribute();
            AssertEquals("Next", '"', xmlReader.QuoteChar);
            xmlReader.MoveToFirstAttribute();
            AssertEquals("First.Again", '\'', xmlReader.QuoteChar);
        }

        [Test]
        public void ReadInnerXmlWrongInit()
        {
            // This behavior is different from XmlNodeReader.
            XmlReader reader = new SfmXmlReader(new StringReader("<root>test of <b>mixed</b> string.</root>"));
            reader.ReadInnerXml();
            AssertEquals("initial.ReadState", ReadState.Initial, reader.ReadState);
            AssertEquals("initial.EOF", false, reader.EOF);
            AssertEquals("initial.NodeType", XmlNodeType.None, reader.NodeType);
        }

        [Test]
        public void EntityReference()
        {
            string xml = "<foo>&bar;</foo>";
            XmlReader xmlReader = new SfmXmlReader(new StringReader(xml));
            AssertNode(
                xmlReader, // xmlReader
                XmlNodeType.Element, // nodeType
                0, //depth
                false, // isEmptyElement
                "foo", // name
                String.Empty, // prefix
                "foo", // localName
                String.Empty, // namespaceURI
                String.Empty, // value
                0 // attributeCount
            );

            AssertNode(
                xmlReader, // xmlReader
                XmlNodeType.EntityReference, // nodeType
                1, //depth
                false, // isEmptyElement
                "bar", // name
                String.Empty, // prefix
                "bar", // localName
                String.Empty, // namespaceURI
                String.Empty, // value
                0 // attributeCount
            );

            AssertNode(
                xmlReader, // xmlReader
                XmlNodeType.EndElement, // nodeType
                0, //depth
                false, // isEmptyElement
                "foo", // name
                String.Empty, // prefix
                "foo", // localName
                String.Empty, // namespaceURI
                String.Empty, // value
                0 // attributeCount
            );

            AssertEndDocument(xmlReader);
        }

        [Test]
        public void EntityReferenceInsideText()
        {
            string xml = "<foo>bar&baz;quux</foo>";
            XmlReader xmlReader = new SfmXmlReader(new StringReader(xml));
            AssertNode(
                xmlReader, // xmlReader
                XmlNodeType.Element, // nodeType
                0, //depth
                false, // isEmptyElement
                "foo", // name
                String.Empty, // prefix
                "foo", // localName
                String.Empty, // namespaceURI
                String.Empty, // value
                0 // attributeCount
            );

            AssertNode(
                xmlReader, // xmlReader
                XmlNodeType.Text, // nodeType
                1, //depth
                false, // isEmptyElement
                String.Empty, // name
                String.Empty, // prefix
                String.Empty, // localName
                String.Empty, // namespaceURI
                "bar", // value
                0 // attributeCount
            );

            AssertNode(
                xmlReader, // xmlReader
                XmlNodeType.EntityReference, // nodeType
                1, //depth
                false, // isEmptyElement
                "baz", // name
                String.Empty, // prefix
                "baz", // localName
                String.Empty, // namespaceURI
                String.Empty, // value
                0 // attributeCount
            );

            AssertNode(
                xmlReader, // xmlReader
                XmlNodeType.Text, // nodeType
                1, //depth
                false, // isEmptyElement
                String.Empty, // name
                String.Empty, // prefix
                String.Empty, // localName
                String.Empty, // namespaceURI
                "quux", // value
                0 // attributeCount
            );

            AssertNode(
                xmlReader, // xmlReader
                XmlNodeType.EndElement, // nodeType
                0, //depth
                false, // isEmptyElement
                "foo", // name
                String.Empty, // prefix
                "foo", // localName
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