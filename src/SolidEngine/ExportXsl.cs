using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace SolidEngine
{
    public class ExportXsl : IExporter
    {

        private string _xslFilePath;
        private ExportHeader _header;

        public string XslFilePath
        {
            get { return _xslFilePath; }
            set { _xslFilePath = value; }
        }

        public static ExportXsl Create(ExportHeader header)
        {
            ExportXsl retval = new ExportXsl(header);
            retval.OpenSettings();
            return retval;
        }

        private ExportXsl(ExportHeader header)
        {
            _header = header;
        }

        private XmlReader CreateXslReader()
        {
            //string filePath = "../../../SolidEngine/ExportLift.xsl";
            string filePath = _xslFilePath;
            //all this just to allow a DTD statement in the source xslt
            XmlReaderSettings readerSettings = new XmlReaderSettings();
            readerSettings.ProhibitDtd = false;
            XmlReader xslReader = XmlReader.Create(new StreamReader(filePath), readerSettings);
            return xslReader;
        }

        private void OpenSettings()
        {
            using (StreamReader reader = new StreamReader(_header.FilePath))
            {
                XPathDocument xmlDoc = new XPathDocument(reader);
                XPathNavigator navDoc = xmlDoc.CreateNavigator();
                XPathNodeIterator iterator = navDoc.Select("/exporter/xsl/node()");
                while (iterator.MoveNext())
                {
                    string name = iterator.Current.Name;
                    string value = iterator.Current.Value;
                    switch (name)
                    {
                        case "stylesheet":
                            _xslFilePath = Path.Combine(EngineEnvironment.PathOfExporters, value);
                            break;
                    }
                }
                reader.Close();
            }
        }

        public void SaveSettings()
        {
            using (StreamWriter writer = new StreamWriter(_header.FilePath))
            {
                XmlWriter xmlWriter = new XmlTextWriter(writer);
                xmlWriter.WriteStartElement("exporter");
                _header.Write(xmlWriter);
                xmlWriter.WriteStartElement("xsl");
                xmlWriter.WriteElementString("stylesheet", _xslFilePath);
                xmlWriter.WriteEndElement();
                xmlWriter.WriteEndElement();
                writer.Close();
            }

        }

        private void ExportToTemp(string srcFile, string desFile)
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

        public void Export(string srcFile, string desFile)
        {
            string tempFilePath = Path.GetTempFileName();
            ExportToTemp(srcFile, tempFilePath);

            XmlReader xmlReader = new XmlTextReader(new StreamReader(tempFilePath));

            XslCompiledTransform transform = new XslCompiledTransform();
            using (XmlReader xslReader = CreateXslReader())
            {
                transform.Load(xslReader);
                xslReader.Close();
            }

            XmlTextWriter xmlWriter = new XmlTextWriter(desFile, Encoding.UTF8);
            xmlWriter.Formatting = Formatting.Indented;
            xmlWriter.WriteStartDocument();
            transform.Transform(xmlReader, xmlWriter);
            xmlWriter.Flush();
            xmlWriter.Close();
        }

/*
        protected string TransformLift(ProjectInfo projectInfo, string xsltName, string outputFileSuffix, XsltArgumentList arguments)
        {
            //all this just to allow a DTD statement in the source xslt
            XmlReaderSettings readerSettings = new XmlReaderSettings();
            readerSettings.ProhibitDtd = false;
            XslCompiledTransform transform = new XslCompiledTransform();


            using (Stream stream = GetXsltStream(projectInfo, xsltName))
            {
                using (XmlReader xsltReader = XmlReader.Create(stream, readerSettings))
                {
                    XsltSettings settings = new XsltSettings(true, true);
                    transform.Load(xsltReader, settings, new XmlUrlResolver());
                    xsltReader.Close();
                }
                stream.Close();
            }

            _pathToOutput = Path.Combine(projectInfo.PathToTopLevelDirectory, projectInfo.Name + outputFileSuffix);
            if (File.Exists(_pathToOutput))
            {
                File.Delete(_pathToOutput);
            }

            using (Stream output = File.Create(_pathToOutput))
            {
                transform.Transform(projectInfo.PathToLIFT, arguments, output);
            }
            transform.TemporaryFiles.Delete();

            return _pathToOutput;
        }

        public static Stream GetXsltStream(ProjectInfo projectInfo, string xsltName)
        {
            //xslt can be in one of the project/wesay locations, (so user can override with their own copy)
            //or just in a resource (helps with forgetting to put it in the installer)
            string xsltPath = projectInfo.LocateFile(xsltName);
            if (String.IsNullOrEmpty(xsltPath))
            {
                return Assembly.GetExecutingAssembly().GetManifestResourceStream("Addin.Transform." + xsltName);
            }
            return File.OpenRead(xsltPath);

            //            if (String.IsNullOrEmpty(xsltPath))
            //            {
            //                throw new ApplicationException("Could not find required file, " + xsltName);
            //            }
        }

*/

    }
}
