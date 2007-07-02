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
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Net;

namespace SolidEngine
{

    /// <summary>
    /// Summary description for XmlSfmReader.
    /// </summary>
    public class SfmXmlReader : CustomXmlReader
    {
        //??? This is really more of a context for the xmlstate.
        enum SfmState
        {
            Root,
            Record,
            Field
        }

        enum XmlState
        {
            Start,
            Element,
            Attribute,
            AttributeValue,
            ElementValue,
            EndElement,
            Eof,
            Closed
        }

        //??? Really a 'context' info, in a general sense.
        struct SfmStateInfo
        {
            public int depth;
            public string name;
            public bool isLeaf;
            public string[] attributes;

            public SfmStateInfo(int depth_, string name_, bool isLeaf_, string[] attributes_)
            {
                depth = depth_;
                name = name_;
                isLeaf = isLeaf_;
                attributes = attributes_;
            }
        }

        SfmStateInfo[] _sfmStateInfo = new SfmStateInfo[] 
        {
            new SfmStateInfo(
                0,
                "root",
                false,
                null
            ),
            new SfmStateInfo(
                1, 
                "entry",
                false,
                null //!!! new string[] {"startline", "endline"}
            ),
            new SfmStateInfo(
                2,
                "field", // Not its real name
                true,
                null
            )
        };

        SfmRecordReader _sfmReader;
//!!!        string _root = "root";
//!!!        string _recordName = "entry";
        
        SfmState _sfmContext = SfmState.Root;
        int _fieldIndex = 0;

        XmlState _xmlState = XmlState.Start;
        int _attributeIndex = 0;

        Encoding _encoding;

        /// <summary>
        /// Construct XmlSfmReader.  You must specify an HRef
        /// location or a TextReader before calling Read().
        /// </summary>
        public SfmXmlReader() :
            base()
        {
            _encoding = Encoding.Default;
        }

        /// <summary>
        /// Construct an XmlSfmReader.
        /// </summary>
        /// <param name="uri">The location of the SFM file</param>
        /// <param name="nametable">The nametable to use for atomizing element names</param>
        public SfmXmlReader(Uri uri, Encoding encoding, XmlNameTable nametable) :
            base(uri, nametable)
        {
            _encoding = encoding;
            //!!!_sfmReader = new SfmCollection(location, encoding, null, 4096); // Read will create from uri by default.
        }

        /// <summary>
        /// Construct an XmlSfmReader.
        /// </summary>
        /// <param name="input">The sfm input stream</param>
        /// <param name="baseUri">The base URI of the sfm.</param>
        /// <param name="nametable">The nametable to use for atomizing element names</param>
        public SfmXmlReader(Stream input, Encoding encoding, Uri baseUri, XmlNameTable nametable) :
            base(baseUri, nametable)
        {
            _encoding = encoding;
            _sfmReader = new SfmRecordReader(input, _encoding, 4096);
        }

        /// <summary>
        /// Construct an XmlSfmReader.
        /// </summary>
        /// <param name="input">The uri of the input stream</param>
        public SfmXmlReader(string uri)
            :
            base(new Uri(uri))
        {
            _encoding = Encoding.Default;
        }

        /// <summary>
        /// Construct an XmlSfmReader.
        /// </summary>
        /// <param name="input">The input text reader</param>
        public SfmXmlReader(TextReader input)
            :
            base()
        {
            _encoding = Encoding.Default;
            _sfmReader = new SfmRecordReader(input, 4096);
        }

        /// <summary>
        /// Construct an XmlSfmReader.
        /// </summary>
        /// <param name="input">The .csv input text reader</param>
        /// <param name="baseUri">The base URI of the .csv.</param>
        /// <param name="nametable">The nametable to use for atomizing element names</param>
        public SfmXmlReader(TextReader input, Uri baseUri, XmlNameTable nametable) :
            base(baseUri, nametable)
        {
            _encoding = Encoding.Default;
            _sfmReader = new SfmRecordReader(input, 4096);
        }
        /*
                /// <summary>
                /// Specifies the base URI to use for resolving the Href property.
                /// This is optional.
                /// </summary>
                public string BaseUri 
                {
                    get { return _baseUri == null ? "" : _baseUri.AbsoluteUri; }
                    set { _baseUri = new Uri(value); }
                }
        */

        protected override void Init()
        {
            base.Init();
            _sfmContext = SfmState.Root;
            _xmlState = XmlState.Start;
            _fieldIndex = 0;
            _attributeIndex = 0;
        }

        /*!!!
        /// <summary>
        /// Specifies the encoding to use when loading the .csv file.
        /// </summary>
        public Encoding Encoding {
            get { return _encoding == null ? System.Text.Encoding.Default : _encoding; }
            set { _encoding = value; }
        }
        */
        /*
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
                        _sfmReader = null;
                        Init();
                    }
                }
        */
        /* !!!
                /// <summary>
                /// Specifies the proxy server.  This is only needed for internet HTTP requests
                /// where the caller is behind a proxy server internet gateway. 
                /// </summary>
                public string Proxy {
                    get { return _proxy; }
                    set { _proxy = value; }
                }
        */
        /// <summary>
        /// Returns the TextReader that contains the SFM file contents.
        /// </summary>
        public TextReader TextReader
        {
            get
            {
                return _sfmReader != null ? _sfmReader.Reader : null;
            }
            set
            {
                _sfmReader = new SfmRecordReader(value, 4096);
                Init();
            }
        }

        /// <summary>
        /// Specifies the name of the root element, the default is "root".
        /// </summary>
        public string RootName
        {
            get
            {
                return _sfmStateInfo[(int)SfmState.Root].name; // _root;
            }
            set
            {
                _sfmStateInfo[(int)SfmState.Root].name = value;
                //!!! _root = _nt.Add(value);
            }
        }

        /// <summary>
        /// Specifies the name of the XML element generated for each record
        /// in the SFM data.  The default is "entry".
        /// </summary>
        public string RecordName
        {
            get
            {
                return _sfmStateInfo[(int)SfmState.Record].name;
            }
            set
            {
                _sfmStateInfo[(int)SfmState.Record].name = value;
                //!!!_recordName = _nt.Add(value);
            }
        }

        public override XmlNodeType NodeType
        {
            get
            {
                Console.Error.WriteLine("Node " + String.Format("{0:D}", _xmlState));
                XmlNodeType retval = XmlNodeType.None;
                switch (_xmlState)
                {
                    case XmlState.Start:
                    case XmlState.Eof:
                        retval = XmlNodeType.None;
                        break;
                    case XmlState.Element:
                        retval = XmlNodeType.Element;
                        break;
                    case XmlState.Attribute:
                        retval = XmlNodeType.Attribute;
                        break;
                    case XmlState.AttributeValue:
                    case XmlState.ElementValue:
                        retval = XmlNodeType.Text;
                        break;
                    case XmlState.EndElement:
                        retval = XmlNodeType.EndElement;
                        break;
                    default:
                        Console.Error.WriteLine("Unknown node in " + String.Format("{0:D}", _xmlState));
                        Debug.Assert(false);
                        break;
                }
                Console.Error.WriteLine("XNT: " + String.Format("{0:D}", retval));
                return retval;
            }
        }

        public override string Name
        {
            get
            {
                return this.LocalName;
            }
        }

        public override string LocalName
        {
            get
            {
                string retval = string.Empty;

                switch (_xmlState)
                {
                    case XmlState.Element:
                    case XmlState.EndElement:
                        // Field name is context dependent.
                        switch (_sfmContext)
                        {
                            case SfmState.Root:
                            case SfmState.Record:
                                retval = XmlConvert.EncodeLocalName(_sfmStateInfo[(int)_sfmContext].name);
                                break;
                            case SfmState.Field:
                                // The local name of a field is it's sfm key.
                                retval = XmlConvert.EncodeLocalName(_sfmReader.Key(_fieldIndex));
                                break;
                        }
                        break;
                    case XmlState.Attribute:
                        switch (_sfmContext)
                        {
                            case SfmState.Root:
                                retval = _sfmReader.Header[_attributeIndex].key;
                                break;
                            default:
                                // No dynamic attributes, so fetch from the StateInfo
                                retval = XmlConvert.EncodeLocalName(_sfmStateInfo[(int)_sfmContext].attributes[_attributeIndex]);
                                break;
                        }
                        break;
                }
                return retval;
            }
        }

        public override bool HasValue
        {
            // TODO: Move this to the base
            get
            {
//                bool retval = false;
//                if (_xmlState == XmlState.ElementValue || _xmlState == XmlState.AttributeValue)
//                {
                    return Value != String.Empty;
//                }
//                return retval;
            }
        }

        public override string Value
        {
            get
            {
                string retval = String.Empty;
                switch (_xmlState)
                {
                    case XmlState.ElementValue:
                        retval = ElementValue();
                        break;
                    case XmlState.AttributeValue:
                        switch (_sfmContext)
                        {
                            case SfmState.Root:
                                retval = _sfmReader.Header[_attributeIndex].value;
                                break;
                            case SfmState.Record:
                                switch (_attributeIndex)
                                {
                                    case 0:
                                        retval = String.Format("{0:D}", _sfmReader.Field(_fieldIndex).sourceLine);
                                        break;
                                }
                                break;
                        }
                        break;
                }
                return retval;
            }
        }

        private string ElementValue()
        {
            string retval = String.Empty;
            switch (_sfmContext)
            {
                case SfmState.Field:
                    retval = _sfmReader.Value(_fieldIndex);
                    break;
            }
            return retval;
        }

        public override int Depth
        {
            get
            {
                int retval = _sfmStateInfo[(int)_sfmContext].depth;
                switch (_xmlState)
                {
                    case XmlState.Attribute:
                        retval += 1;
                        break;
                    case XmlState.AttributeValue:
                        retval += 2;
                        break;
                    case XmlState.ElementValue:
                        retval += 1;
                        break;
                }
                return retval;
            }
        }

        public override bool IsEmptyElement
        {
            get
            {
                //!!! Root can be empty, a field can be empty, entry should always have 1 field min
                bool retval = false;
                //if (_xmlState == XmlState.Element)
                //{
                    switch (_sfmContext)
                    {
                        case SfmState.Root:
                        case SfmState.Record:
                            retval = (_sfmReader.FieldCount == 0);
                            //!!! Fault in Read makes this not yet possible.
                            // These entities may have children, so don't allow them to present as empty.
                            retval = false;
                            break;
                        case SfmState.Field:
                            string vt = _sfmReader.Value(_fieldIndex);
                            retval = (vt == String.Empty || vt == null);
                            retval = false; //!!!
                            break;
                    }
                //}
                return retval;
            }
        }

        public override bool IsDefault
        {
            get
            {
                return false;
            }
        }

        public override XmlSpace XmlSpace
        {
            get
            {
                return XmlSpace.Default;
            }
        }

        public override string XmlLang
        {
            get
            {
                return String.Empty;
            }
        }

        public override int AttributeCount
        {
            get
            {
                int retval = 0;
                switch (_sfmContext)
                {
                    case SfmState.Root:
                        retval = _sfmReader.Header.Count;
                        break;
                    default:
                        string[] attributes = _sfmStateInfo[(int)_sfmContext].attributes;
                        if (attributes != null)
                        {
                            retval = attributes.Length;
                        }
                        break;
                }

                return retval;
            }
        }

        public override string GetAttribute(string name)
        {
            //!!! Current theory is that all attributes come from the solid file, not the sfm file.
            //!!! With the notable exception of line
            throw new Exception("NYI GetAttribute(string name)");
        }
        /*
        int GetOrdinal(string name)
        {
            if (_names != null)
            {
                string n = _nt.Add(name);
                for (int i = 0; i < _names.Length; i++)
                {
                    if ((object)_names[i] == (object)n)
                        return i;
                }
                throw new Exception("Attribute '" + name + "' not found.");
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

        public override string this[int i]
        {
            get
            {
                return GetAttribute(i);
            }
        }

        public override string this[string name]
        {
            get
            {
                return GetAttribute(name);
            }
        }

        public override string this[string name, string namespaceURI]
        {
            get
            {
                return GetAttribute(name, namespaceURI);
            }
        }

        public override bool MoveToAttribute(string name)
        {
            throw new Exception("NYI MoveToAttribute(string name)");
            /*
            bool retval = false;
            switch (_sfmContext)
            {
                case SfmState.Record:
                    _attributeIndex = 0;
                    retval = true;
                    break;
            }
            return retval;
            */
        }

        public override void MoveToAttribute(int i)
        {
            switch (_sfmContext)
            {
                case SfmState.Root:
                    _xmlState = XmlState.Attribute;
                    _attributeIndex = i;
                    break;
            }
        }

        public override bool MoveToFirstAttribute()
        {
            bool retval = HasAttributes;
            if (retval)
            {
                MoveToAttribute(0);
            }
            return retval;
        }

        public override bool MoveToNextAttribute()
        {
            bool retval = false;
            if (_attributeIndex < AttributeCount - 1)
            {
                _xmlState = XmlState.Attribute; //???
                _attributeIndex++;
                retval = true;
            }
            return retval;
        }

        public override bool MoveToElement()
        {
            bool retval = false;
            switch (_xmlState)
            {
                default:
                    _xmlState = XmlState.Element;
                    _attributeIndex = 0;
                    retval = true;
                    break;
            }
            return retval;
        }

        public override bool Read()
        {
            bool retval = false;
            string[] attributes = _sfmStateInfo[(int)_sfmContext].attributes;
            switch (_xmlState)
            {
                case XmlState.Start:
                    if (_sfmReader == null)
                    {
                        if (_href == null)
                        {
                            throw new Exception("You must provide an input location via the Href property, or provide an input stream via the TextReader property.");
                        }
                        //!!! Change this sort of thing to fn InitDefaultReader or similar
                        _sfmReader = new SfmRecordReader(_href, Encoding.Default/*!!!_encoding*/, _proxy, 4096);
                    }
                    _sfmReader.Read();
                    _sfmContext = SfmState.Root;
                    _xmlState = XmlState.Element;
                    retval = true;
                    break;
                case XmlState.Eof:
                    retval = false;
                    break;
                case XmlState.Element:
                    string value = ElementValue();
                    if (value != String.Empty)
                    {
                        _xmlState = XmlState.ElementValue;
                    }
                    else
                    {
                        if (IsEmptyElement)
                        {
                            goto case XmlState.EndElement;
                        }
                        else
                        {
                            goto case XmlState.ElementValue;
                        }
                    }
                    retval = true;
                    break;
                case XmlState.Attribute:
                    _xmlState = XmlState.AttributeValue;
                    retval = true;
                    break;
                case XmlState.AttributeValue:
                    if (attributes == null || _attributeIndex == attributes.Length) //??? -1
                    {
                        if (!IsEmptyElement)
                        {
                            _xmlState = XmlState.ElementValue;
                        }
                        else 
                        {
                            goto case XmlState.EndElement;
                        }
                    }
                    else
                    {
                        _xmlState = XmlState.Attribute;
                    }
                    retval = true;
                    break;
                case XmlState.ElementValue:
                    switch (_sfmContext)
                    {
                        case SfmState.Root:
                            if (_sfmReader.FieldCount > 0)
                            {
                                _sfmContext = SfmState.Record;
                                _xmlState = XmlState.Element;
                                _fieldIndex = 0;
                            }
                            else
                            {
                                _sfmContext = SfmState.Root;
                                _xmlState = XmlState.EndElement;
                            }
                            break;
                        case SfmState.Record:
                            if (_sfmReader.FieldCount > 0)
                            {
                                _sfmContext = SfmState.Field;
                                _xmlState = XmlState.Element;
                            }
                            else
                            {
                                _sfmContext = SfmState.Record;
                                _xmlState = XmlState.EndElement;
                            }
                            break;
                        case SfmState.Field:
                            _xmlState = XmlState.EndElement;
                            break;
                    }
                    retval = true;
                    break;
                case XmlState.EndElement:
                    _xmlState = XmlState.Element;
                    switch (_sfmContext)
                    {
                        case SfmState.Field:
                            if (_fieldIndex < _sfmReader    .FieldCount - 1)
                            {
                                Console.Error.WriteLine(String.Format("Done field {0:D}", _fieldIndex));
                                _fieldIndex++;
                                
                            }
                            else
                            {
                                _sfmContext = SfmState.Record;
                                _xmlState = XmlState.EndElement;
                            }
                            retval = true;
                            break;
                        case SfmState.Record:
                            if (_sfmReader.Read())
                            {
                                _sfmContext = SfmState.Record;
                                _fieldIndex = 0;
                            }
                            else
                            {
                                _sfmContext = SfmState.Root;
                                _xmlState = XmlState.EndElement;
                            }
                            retval = true;
                            break;
                        case SfmState.Root:
                            _xmlState = XmlState.Eof;
                            retval = false;
                            break;
                        default:
                            Console.Error.WriteLine("boo!!!");
                            break;
                    }
                    break;
            }
            Console.Error.WriteLine(
                String.Format("Read c {0:D}", _sfmContext)
                + String.Format(" x {0:D} ", _xmlState)
                + LocalName
            );
            return retval;
        }

        public override bool EOF
        {
            get
            {
                return _xmlState == XmlState.Eof;
            }
        }

        public override void Close()
        {
            _sfmReader.Close();
            _xmlState = XmlState.Closed;
        }

        public override ReadState ReadState
        {
            get
            {
                if (_xmlState == XmlState.Start) return ReadState.Initial;
                else if (_xmlState == XmlState.Eof) return ReadState.EndOfFile;
                else if (_xmlState == XmlState.Closed) return ReadState.Closed;
                return ReadState.Interactive;
            }
        }

        public override string ReadString()
        {
            //if (_state == State.AttributeValue || _state == State.Attribute)
            //{
            //    return _sfmReader[_fieldIndex];
            //}
            return String.Empty;
        }

        public override bool ReadAttributeValue()
        {
            // Just a check of context here.  Should never fail if the state machine is correct.
            bool retval = false;
            string[] attributes = _sfmStateInfo[(int)_sfmContext].attributes;
            switch (_xmlState)
            {
                default:
                    throw new Exception("Not on an attribute.");
                case XmlState.AttributeValue:
                    break;
                case XmlState.Attribute:
                    switch (_sfmContext)
                    {
                        default:
                            if (_attributeIndex < AttributeCount)
                            {
                                _xmlState = XmlState.AttributeValue;
                                retval = true;
                            }
                            break;
                    }
                    break;
                    /*
                case XmlState.AttributeValue:
                    if (attributes != null && _attributeIndex < attributes.Length)
                    {
                        _xmlState = XmlState.Attribute;
                        return true;
                    }
                    _xmlState = XmlState.ElementValue; //???
                    return false;
                     * */
            }
            return retval;
        }

    }
    //TODO Make SFMLexer (which does onKey, onValue) Make SFMParser which does onHeader onRecord
    public class SfmRecordReader
    {
        public class SfmField
        {
            public string key;
            public string value;
            public int sourceLine;
            public int endLine;

            public SfmField()
            {
                key = String.Empty;
                value = String.Empty;
                sourceLine = 0;
                endLine = 0;
            }
        }

        public class SfmRecord : List<SfmField>
        {
            public SfmRecord() 
                :
                base()
            {
            }

            public SfmRecord(SfmRecord rhs)
                :
                base(rhs)
            {
            }

        }

        enum StateLex
        {
            StartFile,
            StartOfRecord, // Holding startkey from previous scan.
            //ScanStartKey,
            //HaveStart,
            //HaveRecord,
            BuildKey,
            BuildValue,
            //EndOfRecord, //!!! This may well be redundant
            EOF
        }

        enum StateParse
        {
            Header,
            Records
        }

        SfmRecord _record = new SfmRecord();
        SfmRecord _header = new SfmRecord();

        string _startKey = "lx";

        TextReader _r;
        StateLex _stateLex = StateLex.StartFile;
        StateParse _stateParse = StateParse.Header;

        public int _recordStartLine;
        public int _recordEndLine;

        // Internal buffer state
        char[] _buffer;
        int _pos;
        int _used;

        // Reading state
        public int _line = 1; //!!! These should be private
        public int _col = 1;

        public SfmRecordReader(Uri location, Encoding encoding, string proxy, int bufsize)
        {  // the location of the file
            if (location.IsFile)
            {
                _r = new StreamReader(location.LocalPath, encoding, true);
            }
            else
            {
                WebRequest wr = WebRequest.Create(location);
                if (proxy != null && proxy != "")
                {
                    wr.Proxy = new WebProxy(proxy);
                }
                wr.Credentials = CredentialCache.DefaultCredentials;
                Stream stm = wr.GetResponse().GetResponseStream();
                _r = new StreamReader(stm, encoding, true);
            }
            _buffer = new char[bufsize];
//            _fields = new SfmRecord();
            _record = new SfmRecord();
        }

        public SfmRecordReader(Stream stm, Encoding encoding, int bufsize)
        {  // the location of the file
            _r = new StreamReader(stm, encoding, true);
            _buffer = new char[bufsize];
            _record = new SfmRecord();
        }

        public SfmRecordReader(TextReader stm, int bufsize)
        {  // the location of the file
            _r = stm;
            _buffer = new char[bufsize];
            _record = new SfmRecord();
        }

        public SfmRecordReader(string fileName, int bufsize)
        {  // the location of the file
            _r = new StreamReader(fileName);
            _buffer = new char[bufsize];
            _record = new SfmRecord();
        }

        public TextReader Reader
        {
            get
            {
                return _r;
            }
        }

        public void Close()
        {
            _r.Close();
        }

        // Read a record.
        public bool Read()
        {
            bool retval = false;
            // Check parse state
            switch (_stateParse)
            {
                case StateParse.Header:
                    _stateParse = StateParse.Records;
                    retval = ReadRecord();
                    // Store the header regardless of what is returned. May only be a header in the file.
                    _header = new SfmRecord(_record); 
                    if (retval)
                    {
                        retval = ReadRecord();
                    }
                    break;
                case StateParse.Records:
                    retval = ReadRecord();
                    break;
            }
            return retval;
        }

        private bool ReadRecord()
        { 
            _record.Clear();

            bool retval = false;
//!!!            int startMatch = 0;
//            int startLimit = _startKey.Length;
            SfmField currentField = new SfmField();
            if (_stateLex == StateLex.StartOfRecord)
            {
                currentField.key = _startKey;
                _stateLex = StateLex.BuildValue;
            }
            StringBuilder sb = new StringBuilder(1024);
            char c1 = '\0';
            char c0 = '\0';
            _recordStartLine = _line;
            while (_stateLex != StateLex.StartOfRecord && _stateLex != StateLex.EOF)
            {
                c1 = c0;
                c0 = ReadChar();

                // Update the line and column statistics
                if (isEOL(c0) && !isEOL(c1))
                {
                    _line++;
                    _col = 1;
                }
                if (c0 == '\\' && _col == 1) //!!! This constrains it to lex style sfm with \\ in col 0.
                {
                    if (_stateLex == StateLex.BuildValue)
                    {
                        // Store the key and value.
                        currentField.value = sb.ToString();
                        onField(currentField);
                        currentField = new SfmField();
                    }
                    _stateLex = StateLex.BuildKey;
                    sb.Length = 0;
//                    currentField.key = "";
  //                  currentField.value = "";
                    currentField.sourceLine = _line;
                }
                // Scan for the start of record and update state if found
                else
                {
                    switch (_stateLex)
                    {
                        case StateLex.BuildKey:
                            if (c0 == ' ' || isEOL(c0))
                            {
                                // push into sb and then store
                                currentField.key = sb.ToString();
                                _stateLex = StateLex.BuildValue;
                                sb.Length = 0;
                                if (currentField.key == _startKey) 
                                {
                                    _stateLex = StateLex.StartOfRecord;
                                    _recordEndLine = _line - 1; //??? -2?
                                    retval = true;
                                }
                            }
                            else
                            {
                                sb.Append(c0);
                            }
                            break;
                        case StateLex.BuildValue:
                            //??? Should we strip cr/lf
                            if (!isEOL(c0))
                            {
                                sb.Append(c0);
                            }
                            break;
                        case StateLex.EOF:
                            if (currentField.key != String.Empty)
                            {
                                currentField.value = sb.ToString();
                                onField(currentField);
                                currentField = new SfmField();
                                _recordEndLine = _line - 1; //??? -2?
                                retval = true;
                            }
                            break;
                    }
                    if (!isEOL(c0))
                    {
                        _col++;
                    }
                }
                // If there are two consequtive EOL then 'reset' c0 so that subsequent EOL are counted correctly.
                if (isEOL(c0) && isEOL(c1))
                {
                    c0 = '\0';
                }

            }

            return retval;
        }

        bool isEOL(char c)
        {
            if (c == 0x0d || c == 0x0a)
            {
                return true;
            }
            return false;
        }

        void onField(SfmField field)
        {
            _record.Add(field);
        }

        public int FieldCount 
        {
            get 
            {
                return _record.Count; 
            }
        }

        public string this[int i] 
        { 
            get 
            {
                return Value(i); 
            }
        }

        public SfmRecord Header
        {
            get
            {
                return _header;
            }
        }

        public SfmField Field(int i)
        {
            return _record[i];
        }

        public string Key(int i)
        {
            return _record[i].key;
        }

        public string Value(int i)
        {
            return _record[i].value;
        }

        public string Value(string key)
        {
            SfmField result = _record.Find(delegate(SfmField item) { return item.key == key; });
            if (result == null)
            {
                throw new ArgumentOutOfRangeException("key");
            }
            return result.value;
        }

        char ReadChar()
        {
            if (_pos == _used)
            {
                _pos = 0;
                _used = _r.Read(_buffer, 0, _buffer.Length);
            }
            if (_pos == _used) //??? Is their a better test for effectively EOF here?
            {
                _buffer[_pos] = '\0';
                _stateLex = StateLex.EOF;
            }
            return _buffer[_pos++];
        }

    }

}