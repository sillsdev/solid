using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.XPath;

namespace SolidGui.Export
{
    public class ExportHeader
    {
        private string _name;
        private string _fileNameFilter;
        private string _driver;
        private string _filePath;
        private int _position;

        public string FilePath
        {
            get { return _filePath; }
            set { _filePath = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string FileNameFilter
        {
            get { return _fileNameFilter; }
            set { _fileNameFilter = value; }
        }

        public string Driver
        {
            get { return _driver; }
            set { _driver = value; }
        }

        public int Position
        {
            get { return _position; }
            set { _position = value; }
        }

        public static ExportHeader CreateFromFilePath(string filePath)
        {
            ExportHeader header = null;
            using (StreamReader reader = new StreamReader(filePath))
            {
                header = new ExportHeader();
                header.Read(reader);
                header.FilePath = filePath;
                reader.Close();
            }
            return header;
        }

        public ExportHeader()
        {
        }

        public void Read(StreamReader reader)
        {
            XPathDocument xmlDoc = new XPathDocument(reader);
            XPathNavigator navDoc = xmlDoc.CreateNavigator();
            XPathNodeIterator iterator = navDoc.Select("/exporter/exportheader/node()");
            while (iterator.MoveNext())
            {
                string name = iterator.Current.Name;
                string value = iterator.Current.Value;
                switch (name)
                {
                    case "name":
                        _name = value;
                        break;
                    case "driver":
                        _driver = value;
                        break;
                    case "filter":
                        _fileNameFilter = value;
                        break;
                    case "position":
                        _position = Convert.ToInt32(value);
                        break;
                }
            }
        }

        public void Write(XmlWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("exportheader");
            xmlWriter.WriteElementString("name", _name);
            xmlWriter.WriteElementString("driver", _driver);
            xmlWriter.WriteElementString("filter", _fileNameFilter);
            xmlWriter.WriteEndElement();
        }

        public void Save(string filePath)
        {
            _filePath = filePath;
            using (StreamWriter writer = new StreamWriter(_filePath))
            {
                XmlWriter xmlWriter = new XmlTextWriter(writer);
                xmlWriter.WriteStartElement("exporter");
                Write(xmlWriter);
                xmlWriter.WriteEndElement();
                writer.Close();
            }
        }

    }
}