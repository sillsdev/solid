using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace SolidEngine
{
    public class XmlHelper
    {
        private XmlDocument _xmlDoc;
        private XmlNode _node;

        /*
        public XmlHelper(XmlDocument xmlDoc)
        {
            _xmlDoc = xmlDoc;
        }
        */

        public XmlHelper(XmlNode node)
        {
            _node = node;
            _xmlDoc = _node.OwnerDocument;
        }

        public XmlNode AppendElementWithText(XmlNode parent, string name, string text)
        {
            XmlNode element = _xmlDoc.CreateElement(name);
            element.InnerText = text;
            return parent.AppendChild(element);
        }

        public void AppendAttribute(XmlNode parent, string name, string value)
        {
            XmlNode attribute = parent.Attributes.Append(_xmlDoc.CreateAttribute(name));
            attribute.Value = value;
        }

        public string GetAttribute(string attribute)
        {
            string retval = string.Empty;
            try
            {
                XmlNode xmlAttribute = _node.Attributes.GetNamedItem(attribute);
                if (xmlAttribute != null)
                {
                    retval = xmlAttribute.Value;
                }
            }
            catch
            {
            }
            return retval;
        }

        public bool AttributeEquals(string attribute, string value)
        {
            bool retval = false;
            XmlNode xmlAttribute = _node.Attributes.GetNamedItem(attribute);
            if (xmlAttribute != null)
            {
                if (xmlAttribute.Value == value)
                {
                    retval = true;
                }
            }
            return retval;
        }
    }
}
