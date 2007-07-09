/*
* 
* An XmlReader implementation for loading SFM delimited files
* 
*/

using System;
using System.Xml;
using System.IO;

namespace SolidEngine
{
    //!!! TODO: Protect with if (_d != null) _d... else base....

    /// <summary>
    /// Summary description for XmlReaderDecorator.
    /// </summary>
    public class XmlReaderDecorator : CustomXmlReader
    {
        protected XmlReader _d = null;

        /// <summary>
        /// Construct XmlReaderDecorator.  You must specify an HRef
        /// location or a TextReader before calling Read().
        /// </summary>
        protected XmlReaderDecorator(XmlReader d)
            : base()
        {
            _d = d;
        }

        public override XmlNodeType NodeType
        {
            get
            {
                return _d.NodeType;
            }
        }

        public override string LocalName
        {
            get
            {
                return _d.LocalName;
            }
        }

        public override string Value
        {
            get
            {
                return _d.Value;
            }
        }

        public override int Depth
        {
            get
            {
                return _d.Depth;
            }
        }

        public override bool IsEmptyElement
        {
            get
            {
                return _d.IsEmptyElement;
            }
        }

        public override bool IsDefault
        {
            get
            {
                return _d.IsDefault;
            }
        }

        public override XmlSpace XmlSpace
        {
            get
            {
                return _d.XmlSpace;
            }
        }

        public override string XmlLang
        {
            get
            {
                return _d.XmlLang;
            }
        }

        public override int AttributeCount
        {
            get
            {
                return _d.AttributeCount;
            }
        }

        public override string GetAttribute(string name)
        {
            return _d.GetAttribute(name);
        }

        public override string GetAttribute(string name, string namespaceURI)
        {
            return _d.GetAttribute(name, namespaceURI);
        }

        public override string GetAttribute(int i)
        {
            return _d.GetAttribute(i);
        }

        public override string this[int i]
        {
            get
            {
                return _d.GetAttribute(i);
            }
        }

        public override string this[string name]
        {
            get
            {
                return _d.GetAttribute(name);
            }
        }

        public override string this[string name, string namespaceURI]
        {
            get
            {
                return _d.GetAttribute(name, namespaceURI);
            }
        }

        public override bool MoveToAttribute(string name)
        {
            return _d.MoveToAttribute(name);
        }

        public override void MoveToAttribute(int i)
        {
            _d.MoveToAttribute(i);
        }

        public override bool MoveToFirstAttribute()
        {
            return _d.MoveToFirstAttribute();
        }

        public override bool MoveToNextAttribute()
        {
            return _d.MoveToNextAttribute();
        }

        public override bool MoveToElement()
        {
            return _d.MoveToElement();
        }

        public override bool Read()
        {
            return _d.Read();
        }

        public override bool EOF
        {
            get
            {
                return _d.EOF;
            }
        }

        public override void Close()
        {
            _d.Close();
        }

        public override ReadState ReadState
        {
            get
            {
                return _d.ReadState;
            }
        }

        public override string ReadString()
        {
            return _d.ReadString();
        }

        public override bool ReadAttributeValue()
        {
            return _d.ReadAttributeValue();
        }

    }

}