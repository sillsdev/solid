using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace SolidEngine
{
    class ExportStructuredXml : IExporter
    {

        public ExportStructuredXml()
        {
        }

        public void Export(string srcFile, string desFile)
        {
            SolidXmlReader xmlReader = new SolidXmlReader(srcFile);
            XmlTextWriter xmlWriter = new XmlTextWriter(desFile, Encoding.UTF8);
            xmlWriter.Formatting = Formatting.Indented;
            xmlWriter.WriteStartDocument();
            try
            {
                xmlReader.Read();
                xmlWriter.WriteNode(xmlReader, true);
                xmlWriter.Flush();
                xmlWriter.Close();
            }
            catch
            {
                xmlWriter.Flush();
            }
        }
        
    }
}
