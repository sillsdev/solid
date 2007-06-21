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

namespace SolidConsole
{

    /// <summary>
    /// Summary description for XmlSfmReader.
    /// </summary>
    public class SfmXmlReader : CustomXmlReader
    {
        enum State
        {
            Start,
            Root,
            Record,
            RecordAttribute,
            RecordAttributeValue,
            Field,
            FieldValue,
            EndField,
            EndRecord,
//            Attribute, //!!! Not sure about the attr states. These are node 'state' specific.
//            AttributeValue,
            EndRoot,
            Eof
        }

        SfmRecordReader _sfmReader;
        string _root = "root";
        string _recordName = "entry";
        State _state = State.Start;
        int _fieldIndex = 0;
        int _attributeIndex = 0;
        Encoding _encoding;

        string[] _recordAttributes = new string[] {
            "startline",
            "endline"
        };

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
            _state = State.Start;
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
                return _root;
            }
            set
            {
                _root = _nt.Add(value);
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
                return _recordName;
            }
            set
            {
                _recordName = _nt.Add(value);
            }
        }

        public override XmlNodeType NodeType
        {
            get
            {
                XmlNodeType retval = XmlNodeType.EndElement;
                switch (_state)
                {
                    case State.Start:
                    case State.Eof:
                        retval = XmlNodeType.None;
                        break;
                    case State.Root:
                    case State.Record:
                    case State.Field:
                        retval = XmlNodeType.Element;
                        break;
                    case State.RecordAttribute:
                        retval = XmlNodeType.Attribute;
                        break;
                    case State.RecordAttributeValue:
                    case State.FieldValue:
                        retval = XmlNodeType.Text;
                        break;
                }
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
                switch (_state)
                {
                    case State.Field:
                    case State.EndField:
                        retval = XmlConvert.EncodeLocalName(_sfmReader.Key(_fieldIndex));
                        break;
                    case State.Root:
                    case State.EndRoot:
                        retval = _root;
                        break;
                    case State.Record:
                    case State.EndRecord:
                        retval = _recordName;
                        break;
                    case State.RecordAttribute:
                        switch (_attributeIndex)
                        {
                            case 0:
                                retval = "startline";
                                break;
                            case 1:
                                retval = "endline";
                                break;
                        }
                        break;

                }
                return retval;
            }
        }

        public override bool HasValue
        {
            get
            {
                if (_state == State.FieldValue)
                {
                    return Value != String.Empty;
                }
                return false;
            }
        }

        public override string Value
        {
            get
            {
                if (_state == State.FieldValue)
                {
                    return _sfmReader.Value(_fieldIndex);
                }
                return null;
            }
        }

        public override int Depth
        {
            get
            {
                int retval = 0;
                switch (_state)
                {
                    case State.Record:
                    case State.EndRecord:
                        retval = 1;
                        break;
                    case State.RecordAttribute:
                    case State.Field:
                    case State.EndField:
                        retval = 2;
                        break;
                    case State.RecordAttributeValue:
                    case State.FieldValue:
                        retval = 3;
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
                return false;
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
            //!!! Needs to be state specific. fields and entries have lines.
            get
            {
                return 0;
            }
        }

        public override string GetAttribute(string name)
        {
            //!!! Current theory is that all attributes come from the solid file, not the sfm file.
            //!!! With the notable exception of line
            return "";
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
            Debug.Assert(false, "NYI");
            return false;
        }

        public override void MoveToAttribute(int i)
        {
            Debug.Assert(false, "NYI");
        }

        public override bool MoveToFirstAttribute()
        {
            return false;
        }

        public override bool MoveToNextAttribute()
        {
            return false;
        }

        public override bool MoveToElement()
        {
            return false;
        }

        public override bool Read()
        {
            switch (_state)
            {
                case State.Start:
                    if (_sfmReader == null)
                    {
                        if (_href == null)
                        {
                            throw new Exception("You must provide an input location via the Href property, or provide an input stream via the TextReader property.");
                        }
                        //!!! Change this sort of thing to fn InitDefaultReader or similar
                        _sfmReader = new SfmRecordReader(_href, Encoding.Default/*!!!_encoding*/, _proxy, 4096);
                    }
                    _state = State.Root;
                    return true;
                case State.Eof:
                    return false;
                case State.Root:
                case State.EndRecord:
                    if (_sfmReader.Read())
                    {
                        _state = State.Record;
                        return true;
                    }
                    _state = State.EndRoot;
                    return true;
                case State.EndRoot:
                    _state = State.Eof;
                    return false;
                case State.Record:
//!!!                    _state = State.Field;
                    _state = State.RecordAttribute;
                    _attributeIndex = 0; //??? Do we need to do this, or do consumers set via MoveToFirstAttribute?
                    return true;
                case State.RecordAttribute:
                    _state = State.RecordAttributeValue;
                    return true;
                case State.RecordAttributeValue:
                    if (_attributeIndex < _recordAttributes.Length - 1)
                    {
                        _attributeIndex++;
                        _state = State.RecordAttribute;
                        return true;
                    }
                    _state = State.Field;
                    _fieldIndex = 0;
                    return true;
                case State.Field:
                    if (!IsEmptyElement)
                    {
                        _state = State.FieldValue;
                    }
                    else
                    {
                        goto case State.EndField;
                    }
                    return true;
                case State.FieldValue:
                    _state = State.EndField;
                    return true;
                case State.EndField:
                    if (_fieldIndex < _sfmReader.FieldCount - 1)
                    {
                        _fieldIndex++;
                        _state = State.Field;
                        return true;
                    }
                    _state = State.EndRecord;
                    return true;
            }
            return false;
        }

        public override bool EOF
        {
            get
            {
                return _state == State.Eof;
            }
        }

        public override void Close()
        {
            _sfmReader.Close();
        }

        public override ReadState ReadState
        {
            get
            {
                if (_state == State.Start) return ReadState.Initial;
                else if (_state == State.Eof) return ReadState.EndOfFile;
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
            switch (_state)
            {
                case State.RecordAttribute:
                    _state = State.RecordAttributeValue;
                    return true;
                case State.RecordAttributeValue:
                    return false;
            }
            throw new Exception("Not on an attribute.");
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
        }

        public class SfmRecord : List<SfmField>
        {
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

        SfmRecord _record;

        string _startKey = "lx";

        TextReader _r;
        StateLex _stateLex = StateLex.StartFile;
        //!!! StateParse _stateParse = StateParse.Header;

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
                            currentField.value = sb.ToString();
                            onField(currentField);
                            currentField = new SfmField();
                            _recordEndLine = _line - 1; //??? -2?
                            retval = true;
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