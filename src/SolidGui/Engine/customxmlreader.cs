/*
* 
* An XmlReader implementation for loading SFM delimited files
*
* Copyright (c) 2001-2005 Microsoft Corporation. All rights reserved.
*
* Chris Lovett
* 
*/

using System;
using System.Xml;
using System.IO;
using System.Collections;
using System.Text;
using System.Net;

namespace Solid.Engine {

    /// <summary>
    /// Summary description for XmlCustomReader.
    /// </summary>
    public class CustomXmlReader : XmlReader {
//        SfmReader _csvReader;
        protected string _proxy;
        protected Uri _baseUri;
        protected Uri _href;
        //        string _root = "root";
//        string _rowname = "row";
        protected XmlNameTable _nt;
//        string[] _names;
//        State _state = State.Initial;
//        int _attr = 0;
  //      bool _asAttrs = false;
//        bool _firstRowHasColumnNames = false;
//        char _delimiter;
        //        Encoding _encoding;

        #region overrides
        public override bool CanReadBinaryContent
        {
            get
            {
                return base.CanReadBinaryContent;
            }
        }

        public override bool CanReadValueChunk
        {
            get
            {
                return base.CanReadValueChunk;
            }
        }
        public override bool CanResolveEntity
        {
            get
            {
                return base.CanResolveEntity;
            }
        }
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override bool HasAttributes
        {
            get
            {
                return (AttributeCount > 0);
            }
        }
        public override bool IsStartElement()
        {
            return base.IsStartElement();
        }
        public override bool IsStartElement(string localname, string ns)
        {
            return base.IsStartElement(localname, ns);
        }
        public override bool IsStartElement(string name)
        {
            return base.IsStartElement(name);
        }
        public override XmlNodeType MoveToContent()
        {
            return base.MoveToContent();
        }
        public override object ReadContentAs(Type returnType, IXmlNamespaceResolver namespaceResolver)
        {
            return base.ReadContentAs(returnType, namespaceResolver);
        }
        public override int ReadContentAsBase64(byte[] buffer, int index, int count)
        {
            return base.ReadContentAsBase64(buffer, index, count);
        }
        public override int ReadContentAsBinHex(byte[] buffer, int index, int count)
        {
            return base.ReadContentAsBinHex(buffer, index, count);
        }
        public override bool ReadContentAsBoolean()
        {
            return base.ReadContentAsBoolean();
        }
        public override DateTime ReadContentAsDateTime()
        {
            return base.ReadContentAsDateTime();
        }
        public override decimal ReadContentAsDecimal()
        {
            return base.ReadContentAsDecimal();
        }
        public override double ReadContentAsDouble()
        {
            return base.ReadContentAsDouble();
        }
        public override float ReadContentAsFloat()
        {
            return base.ReadContentAsFloat();
        }
        public override int ReadContentAsInt()
        {
            return base.ReadContentAsInt();
        }
        public override long ReadContentAsLong()
        {
            return base.ReadContentAsLong();
        }
        public override object ReadContentAsObject()
        {
            return base.ReadContentAsObject();
        }
        public override string ReadContentAsString()
        {
            return base.ReadContentAsString();
        }
        public override object ReadElementContentAs(Type returnType, IXmlNamespaceResolver namespaceResolver)
        {
            return base.ReadElementContentAs(returnType, namespaceResolver);
        }
        public override object ReadElementContentAs(Type returnType, IXmlNamespaceResolver namespaceResolver, string localName, string namespaceURI)
        {
            return base.ReadElementContentAs(returnType, namespaceResolver, localName, namespaceURI);
        }
        public override int ReadElementContentAsBase64(byte[] buffer, int index, int count)
        {
            return base.ReadElementContentAsBase64(buffer, index, count);
        }
        public override int ReadElementContentAsBinHex(byte[] buffer, int index, int count)
        {
            return base.ReadElementContentAsBinHex(buffer, index, count);
        }
        public override bool ReadElementContentAsBoolean()
        {
            return base.ReadElementContentAsBoolean();
        }
        public override bool ReadElementContentAsBoolean(string localName, string namespaceURI)
        {
            return base.ReadElementContentAsBoolean(localName, namespaceURI);
        }
        public override DateTime ReadElementContentAsDateTime()
        {
            return base.ReadElementContentAsDateTime();
        }
        public override DateTime ReadElementContentAsDateTime(string localName, string namespaceURI)
        {
            return base.ReadElementContentAsDateTime(localName, namespaceURI);
        }
        public override decimal ReadElementContentAsDecimal()
        {
            return base.ReadElementContentAsDecimal();
        }
        public override decimal ReadElementContentAsDecimal(string localName, string namespaceURI)
        {
            return base.ReadElementContentAsDecimal(localName, namespaceURI);
        }
        public override double ReadElementContentAsDouble()
        {
            return base.ReadElementContentAsDouble();
        }
        public override double ReadElementContentAsDouble(string localName, string namespaceURI)
        {
            return base.ReadElementContentAsDouble(localName, namespaceURI);
        }
        public override float ReadElementContentAsFloat()
        {
            return base.ReadElementContentAsFloat();
        }
        public override float ReadElementContentAsFloat(string localName, string namespaceURI)
        {
            return base.ReadElementContentAsFloat(localName, namespaceURI);
        }
        public override int ReadElementContentAsInt()
        {
            return base.ReadElementContentAsInt();
        }
        public override int ReadElementContentAsInt(string localName, string namespaceURI)
        {
            return base.ReadElementContentAsInt(localName, namespaceURI);
        }
        public override long ReadElementContentAsLong()
        {
            return base.ReadElementContentAsLong();
        }
        public override long ReadElementContentAsLong(string localName, string namespaceURI)
        {
            return base.ReadElementContentAsLong(localName, namespaceURI);
        }
        public override object ReadElementContentAsObject()
        {
            return base.ReadElementContentAsObject();
        }
        public override object ReadElementContentAsObject(string localName, string namespaceURI)
        {
            return base.ReadElementContentAsObject(localName, namespaceURI);
        }
        public override string ReadElementContentAsString()
        {
            return base.ReadElementContentAsString();
        }
        public override string ReadElementContentAsString(string localName, string namespaceURI)
        {
            return base.ReadElementContentAsString(localName, namespaceURI);
        }
        public override string ReadElementString()
        {
            return base.ReadElementString();
        }
        public override string ReadElementString(string localname, string ns)
        {
            return base.ReadElementString(localname, ns);
        }
        public override string ReadElementString(string name)
        {
            return base.ReadElementString(name);
        }
        public override void ReadEndElement()
        {
            base.ReadEndElement();
        }
        public override void ReadStartElement()
        {
            base.ReadStartElement();
        }
        public override void ReadStartElement(string localname, string ns)
        {
            
            base.ReadStartElement(localname, ns);
        }
        public override void ReadStartElement(string name)
        {
            base.ReadStartElement(name);
        }
        public override XmlReader ReadSubtree()
        {
            return base.ReadSubtree();
        }
        public override bool ReadToDescendant(string localName, string namespaceURI)
        {
            return base.ReadToDescendant(localName, namespaceURI);
        }
        public override bool ReadToDescendant(string name)
        {
            return base.ReadToDescendant(name);
        }
        public override bool ReadToFollowing(string localName, string namespaceURI)
        {
            return base.ReadToFollowing(localName, namespaceURI);
        }
        public override bool ReadToFollowing(string name)
        {
            return base.ReadToFollowing(name);
        }
        public override bool ReadToNextSibling(string localName, string namespaceURI)
        {
            return base.ReadToNextSibling(localName, namespaceURI);
        }
        public override bool ReadToNextSibling(string name)
        {
            return base.ReadToNextSibling(name);
        }
        public override int ReadValueChunk(char[] buffer, int index, int count)
        {
            return base.ReadValueChunk(buffer, index, count);
        }
        public override System.Xml.Schema.IXmlSchemaInfo SchemaInfo
        {
            get
            {
                return base.SchemaInfo;
            }
        }
        public override XmlReaderSettings Settings
        {
            get
            {
                return base.Settings;
            }
        }
        public override void Skip()
        {
            base.Skip();
        }
        public override string ToString()
        {
            return base.ToString();
        }
        public override Type ValueType
        {
            get
            {
                return base.ValueType;
            }
        }
        #endregion
        protected virtual void Init() {
        }

        /// <summary>
        /// Construct XmlCustomReader.  You must specify an HRef
        /// location or a TextReader before calling Read().
        /// </summary>
        public CustomXmlReader() {
            _nt = new NameTable();
        }

        /// <summary>
        /// Construct an XmlCustomReader.
        /// </summary>
        /// <param name="location">The location of the .csv file</param>
        /// <param name="nametable">The nametable to use for atomizing element names</param>
        public CustomXmlReader(Uri location)
        {
            _baseUri = location;
            _href = location;
            _nt = new NameTable();
        }
        
        /// <summary>
        /// Construct an XmlCustomReader.
        /// </summary>
        /// <param name="location">The location of the .csv file</param>
        /// <param name="nametable">The nametable to use for atomizing element names</param>
        public CustomXmlReader(Uri location, XmlNameTable nametable) {
            _baseUri = location;
            _href = location;
            _nt = nametable;
            if (nametable == null)
            {
                _nt = new NameTable();
            }
        }

        /// <summary>
        /// Specifies the base URI to use for resolving the Href property.
        /// This is optional.
        /// </summary>
        public string BaseUri {
            get { return _baseUri == null ? String.Empty : _baseUri.AbsoluteUri; }
            set { _baseUri = new Uri(value); }
        }

        /// <summary>
        /// Specifies the URI location of the .csv file to parse.
        /// This can also be a local file name.
        /// This can be a relative URI if a BaseUri has been provided.
        /// You must specify either this property or a TextReader as input
        /// before calling Read.
        /// </summary>
        public string Href {
            get { return _href == null ? "" : _href.AbsoluteUri; }
            set { 
                if (_baseUri != null) {
                    _href = new Uri(_baseUri, value); 
                } else {
                    try {
                        _href = new Uri(value); 
                    } 
                    catch (Exception) {
                        string file = Path.GetFullPath(value);
                        _href = new Uri(file);
                    } 
                    _baseUri = _href;
                }
                Init();
            }
        }

        /// <summary>
        /// Specifies the proxy server.  This is only needed for internet HTTP requests
        /// where the caller is behind a proxy server internet gateway. 
        /// </summary>
        public string Proxy {
            get { return _proxy; }
            set { _proxy = value; }
        }

        public override XmlNodeType NodeType // CJP Property override
        { 
            get 
            {
                return XmlNodeType.None;
            }       
        }

        public override string Name // CJP Property override
        {
            get 
            {
                return this.LocalName;
            }
        }

        public override string LocalName // CJP Property override
        { 
            get 
            {
                return string.Empty;
            }
        }

        public override string NamespaceURI // CJP Property override
        { 
            get 
            {
                return String.Empty;
            }
        }

        public override string Prefix // CJP Property override
        { 
            get 
            {
                return String.Empty;
            }
        }

        public override bool HasValue // CJP Property override
        { 
            get 
            {
                return Value != String.Empty;
            }
        }

        public override string Value // CJP Property override
        { 
            get 
            {
                return null;
            }
        }

        public override int Depth // CJP Property override
        { 
            get 
            {
                return 0;
            }       
        }

        public override string BaseURI // CJP Property override
        { 
            get 
            {
                return _baseUri != null ? _baseUri.AbsolutePath : String.Empty;
            }
        }

        public override bool IsEmptyElement // CJP Property override
        { 
            get 
            {
                return false;
            }
        }
        public override bool IsDefault // CJP Property override
        { 
            get 
            {
                return false;
            }
        }
        public override char QuoteChar // CJP Property override
        { 
            get 
            {
                return '"';
            }
        }

        public override string XmlLang // CJP Property override
        { 
            get 
            {
                return String.Empty;
            }
        }

        public override XmlSpace XmlSpace // CJP Property override
        {
            get
            {
                return XmlSpace.Default;
            }
        }


        public override int AttributeCount // CJP Property override
        { 
            get 
            {
                return 0;
            }
        }

        public override string GetAttribute(string name) {
            return null;
        }

/*!!!
        int GetOrdinal(string name) {
            if (_names != null) {
                string n = _nt.Add(name);
                for (int i = 0; i < _names.Length; i++) {
                    if ((object)_names[i] == (object)n)
                        return i;
                }
                throw new Exception("Attribute '"+name+"' not found.");
            }
            // names are assigned a0, a1, a2, ...
            return Int32.Parse(name.Substring(1));
        }
*/
        public override string GetAttribute(string name, string namespaceURI) 
        {
            if (namespaceURI != string.Empty && namespaceURI != null)
            {
                return null;
            }
            return GetAttribute(name);
        }

        public override string GetAttribute(int i) 
        {
            return null;
        }

        public override string this [ int i ] 
        { 
            get 
            {
                return GetAttribute(i);
            }
        }

        public override string this [ string name ] 
        { 
            get 
            {
                return GetAttribute(name);
            }
        }

        public override string this [ string name,string namespaceURI ] 
        { 
            get 
            {
                return GetAttribute(name, namespaceURI);
            }
        }

        public override bool MoveToAttribute(string name) 
        {
            return false;
        }

        public override bool MoveToAttribute(string name, string ns) 
        {
            if (ns != string.Empty && ns != null)
            {
                return false;
            }
            return MoveToAttribute(name);
        }

        public override void MoveToAttribute(int i) 
        {
            //!!! NYI.  Would like to have attribs for each node implemented as std in XMLCustomReader
        }

        public override bool MoveToFirstAttribute()  // CJP Method override
        {
            /*
            if (! _asAttrs) return false;
            if (AttributeCount > 0) {
                _attr = 0;
                _state = State.Attr;
                return true;
            }
            */
            return false;
        }

        public override bool MoveToNextAttribute()  // CJP Method override
        {
            /*
            if (! _asAttrs) return false;
            if (_attr < AttributeCount-1) {
                _attr = (_state == State.Attr || _state == State.AttrValue) ? _attr+1 : 0;
                _state = State.Attr;
                return true;
            }
            */
            return false;
        }

        public override bool MoveToElement()  // CJP Method override
        {
            return false;
        }

        public override bool Read()  // CJP Method override
        {
            return false;
        }

        public override bool EOF // CJP Property override
        { 
            get {
                return true;
            }
        }

        public override void Close() // CJP Method override
        {
        }

        public override ReadState ReadState // CJP Property override
        { 
            get 
            {
                return ReadState.EndOfFile;
            }
        }

        public override string ReadString()  // CJP Method override
        {
            return String.Empty;
        }

        public override string ReadInnerXml()  // CJP Method override - stay in custom
        {
            StringWriter sw = new StringWriter();
            XmlTextWriter xw = new XmlTextWriter(sw);
            xw.Formatting = Formatting.Indented;
            while (!this.EOF && this.NodeType != XmlNodeType.EndElement) {
                xw.WriteNode(this, true);
            }
            xw.Close();
            return sw.ToString();
        }

        public override string ReadOuterXml()  // CJP Method override - stay in custom
        {
            StringWriter sw = new StringWriter();
            XmlTextWriter xw = new XmlTextWriter(sw);
            xw.Formatting = Formatting.Indented;
            xw.WriteNode(this, true);
            xw.Close();
            return sw.ToString();
        }

        public override XmlNameTable NameTable // CJP Property override
        { 
            get {
                return _nt;
            }
        }

        public override string LookupNamespace(string prefix)  // CJP Method override
        {     
            return null;
        }

        public override void ResolveEntity()  // CJP Method override
        {
            throw new NotImplementedException();
        }

        public override bool ReadAttributeValue()  // CJP Method override
        {
            return false;
/*
            if (_state == State.Attr) {
                _state = State.AttrValue;
                return true;
            }
            else if (_state == State.AttrValue) {
                return false;
            }
            throw new Exception("Not on an attribute.");
*/
        } 

    }
 
}