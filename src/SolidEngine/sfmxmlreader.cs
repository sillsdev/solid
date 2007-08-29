/*
* 
* An XmlReader implementation for loading SFM delimited files
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
using System.Text.RegularExpressions;
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
                new string[] {"record", "startline", "endline"}
            ),
            new SfmStateInfo(
                2,
                "field", // Not its real name
                true,
                new string[] {"field"}
            )
        };

        SfmRecordReader _sfmReader;
        
        SfmState _sfmContext = SfmState.Root;
        int _fieldIndex = 0;

        XmlState _xmlState = XmlState.Start;
        int _attributeIndex = 0;

        Encoding _encoding;

        private static Regex _startWithNumber = new Regex(@"^\d");

        /// <summary>
        /// Construct XmlSfmReader.  You must specify an HRef
        /// location or a TextReader before calling Read().
        /// </summary>
        public SfmXmlReader() :
            base()
        {
            _encoding = Encoding.GetEncoding("iso-8859-1");
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
            _sfmReader = new SfmRecordReader(input);
        }

        /// <summary>
        /// Construct an XmlSfmReader.
        /// </summary>
        /// <param name="input">The uri of the input stream</param>
        public SfmXmlReader(string uri)
            :
            base(new Uri(uri))
        {
            _encoding = Encoding.GetEncoding("iso-8859-1");
        }

        /// <summary>
        /// Construct an XmlSfmReader.
        /// </summary>
        /// <param name="input">The input text reader</param>
        public SfmXmlReader(TextReader input)
            :
            base()
        {
            _encoding = Encoding.GetEncoding("iso-8859-1");
            _sfmReader = new SfmRecordReader(input);
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
            _encoding = Encoding.GetEncoding("iso-8859-1");
            _sfmReader = new SfmRecordReader(input);
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
                _sfmReader = new SfmRecordReader(value);
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
            }
        }

        public override XmlNodeType NodeType
        {
            get
            {
                //Console.Error.WriteLine("Node " + String.Format("{0:D}", _xmlState));
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
                //Console.Error.WriteLine("XNT: " + String.Format("{0:D}", retval));
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
                                // The local name of a field is it's sfm key with the leading \.
                                string tagName = _sfmReader.Key(_fieldIndex);
                                tagName.Trim();
                                if (tagName == string.Empty)
                                {
                                    tagName = "_";
                                }
                                else if (_startWithNumber.IsMatch(tagName))
                                {
                                    tagName = "_" + tagName;
                                }
                                retval = XmlConvert.EncodeLocalName(tagName);
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
                                        retval = String.Format("{0:D}", _sfmReader.RecordID);
                                        break;
                                    case 1:
                                        retval = String.Format("{0:D}", _sfmReader.RecordStartLine);
                                        break;
                                    case 2:
                                        retval = String.Format("{0:D}", _sfmReader.RecordEndLine);
                                        break;
                                }
                                break;
                            case SfmState.Field:
                                retval = String.Format("{0:D}", _fieldIndex);
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
                case SfmState.Record:
                case SfmState.Field:
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
                case XmlState.Attribute:
                case XmlState.AttributeValue:
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
                        _sfmReader = new SfmRecordReader(_href, _proxy);
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
                                //Console.Error.WriteLine(String.Format("Done field {0:D}", _fieldIndex));
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
                            break;
                    }
                    break;
            }
            //Console.Error.WriteLine(
            //    String.Format("Read c {0:D}", _sfmContext)
            //    + String.Format(" x {0:D} ", _xmlState)
            //    + LocalName
            //);
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

}