/*
* 
* An XmlReader implementation for loading SFM delimited files
* 
*/

using System;
using System.Xml;
using System.IO;

namespace Solid.Engine
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
                return (_d != null) ? _d.NodeType : base.NodeType;
            }
        }

        public override string LocalName
        {
            get
            {
                return (_d != null) ? _d.LocalName : base.LocalName;
            }
        }

        public override string Value
        {
            get
            {
                return (_d != null) ? _d.Value : base.Value;
            }
        }

        public override int Depth
        {
            get
            {
                return (_d != null) ? _d.Depth : base.Depth;
            }
        }

        public override bool IsEmptyElement
        {
            get
            {
                return (_d != null) ? _d.IsEmptyElement : base.IsEmptyElement;
            }
        }

        public override bool IsDefault
        {
            get
            {
                return (_d != null) ? _d.IsDefault : base.IsDefault;
            }
        }

        public override XmlSpace XmlSpace
        {
            get
            {
                return (_d != null) ? _d.XmlSpace : base.XmlSpace;
            }
        }

        public override string XmlLang
        {
            get
            {
                return (_d != null) ? _d.XmlLang : base.XmlLang;
            }
        }

        public override int AttributeCount
        {
            get
            {
                return (_d != null) ? _d.AttributeCount : base.AttributeCount;
            }
        }

        public override string GetAttribute(string name)
        {
            return (_d != null) ? _d.GetAttribute(name) : GetAttribute(name);
        }

        public override string GetAttribute(string name, string namespaceURI)
        {
            return (_d != null) ? _d.GetAttribute(name, namespaceURI) : base.GetAttribute(name, namespaceURI);
        }

        public override string GetAttribute(int i)
        {
            return (_d != null) ? _d.GetAttribute(i) : base.GetAttribute(i);
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
            return (_d != null) ? _d.MoveToAttribute(name) : base.MoveToAttribute(name);
        }

        public override void MoveToAttribute(int i)
        {
            if (_d != null)
            {
                _d.MoveToAttribute(i);
            }
            else
            {
                base.MoveToAttribute(i);
            }
        }

        public override bool MoveToFirstAttribute()
        {
            return (_d != null) ? _d.MoveToFirstAttribute() : base.MoveToFirstAttribute();
        }

        public override bool MoveToNextAttribute()
        {
            return (_d != null) ? _d.MoveToNextAttribute() : base.MoveToNextAttribute();
        }

        public override bool MoveToElement()
        {
            return (_d != null) ? _d.MoveToElement() : base.MoveToElement();
        }

        public override bool Read()
        {
            return (_d != null) ? _d.Read() : base.Read();
        }

        public override bool EOF
        {
            get
            {
                return (_d != null) ? _d.EOF : base.EOF;
            }
        }

        public override void Close()
        {
            if (_d != null)
            {
                _d.Close();
            }
            else
            {
                base.Close();
            }
        }

        public override ReadState ReadState
        {
            get
            {
                return (_d != null) ? _d.ReadState : base.ReadState;
            }
        }

        public override string ReadString()
        {
            return (_d != null) ? _d.ReadString() : base.ReadString();
        }

        public override bool ReadAttributeValue()
        {
            return (_d != null) ? _d.ReadAttributeValue() : base.ReadAttributeValue();
        }

    }

}