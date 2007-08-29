using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace SolidEngine
{
    public class ExportFlatXml : IExporter
    {

        public static ExportFlatXml Create()
        {
            return new ExportFlatXml();
        }

        private ExportFlatXml()
        {
        }

        public void Export(string srcFile, string desFile)
        {
            SfmXmlReader xmlReader = new SfmXmlReader(srcFile);
            XmlTextWriter xmlWriter = new XmlTextWriter(desFile, Encoding.UTF8);
            xmlWriter.Formatting = Formatting.Indented;
            xmlWriter.WriteStartDocument();
            try
            {
                xmlReader.Read();
                xmlWriter.WriteNode(xmlReader, true);
                xmlWriter.Flush();
                xmlWriter.Close();
                xmlReader.Close();
            }
            catch
            {
                xmlWriter.Flush();
                xmlReader.Close();
            }
        }

    }
}
