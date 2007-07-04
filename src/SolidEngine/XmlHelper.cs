using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace SolidEngine
{
    public class XmlHelper
    {
        private XmlDocument _xmlDoc;

        public XmlHelper(XmlDocument xmlDoc)
        {
            _xmlDoc = xmlDoc;
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
    }
}
