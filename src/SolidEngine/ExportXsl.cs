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
        private string _processMethod;
        private ExportHeader _header;
        private List<string> _files = new List<string>();

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
            _processMethod = "file";
        }

        private XmlReader CreateXslReader()
        {
            // All this just to allow a DTD statement in the source xslt
            XmlReaderSettings readerSettings = new XmlReaderSettings();
            readerSettings.ProhibitDtd = false;
            XmlReader xslReader = XmlReader.Create(new StreamReader(_xslFilePath), readerSettings);
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
                        case "file":
                            _files.Add(Path.Combine(EngineEnvironment.PathOfExporters, value));
                            break;
                        case "method":
                            _processMethod = value;
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
                xmlWriter.WriteElementString("method", _processMethod);
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
            // Load the XSL
            XslCompiledTransform transform = new XslCompiledTransform();
            using (XmlReader xslReader = CreateXslReader())
            {
                XsltSettings settings = new XsltSettings();
                settings.EnableDocumentFunction = true;
                transform.Load(xslReader, settings, new XmlUrlResolver());
                xslReader.Close();
            }

            // Prepare the output XML
            XmlTextWriter xmlWriter = new XmlTextWriter(desFile, Encoding.UTF8);
            xmlWriter.Formatting = Formatting.Indented;
            xmlWriter.WriteStartDocument();

            // Setup the parameters to be passed into the XSL
            XsltArgumentList arguments = new XsltArgumentList();
            arguments.AddParam("processmethod", "", _processMethod);
            for (int i = 0; i < _files.Count; i++)
            {
                arguments.AddParam(string.Format("file{0:D}", i), "", _files[i]);
            }
            //TODO Setup the OnXslMessage handler used to feed a progress bar.
            //arguments.XsltMessageEncountered += OnXslMessage;
            if (_processMethod == "file") 
            {
                // Prepare the input XML
                string tempFilePath = Path.GetTempFileName();
                ExportToTemp(srcFile, tempFilePath);
                XmlReader xmlReader = new XmlTextReader(new StreamReader(tempFilePath));
                transform.Transform(xmlReader, arguments, xmlWriter);
                xmlReader.Close();
            }
            else if (_processMethod == "record")
            {
                xmlWriter.WriteStartElement("lift_test");
                // Loop through the input transforming and writing entry by entry.
                using (XmlReader xmlReader = new SolidXmlReader(srcFile))
                {
                    while (xmlReader.ReadToFollowing("entry"))
                    {
                        XmlReader entryReader = xmlReader.ReadSubtree();
                        // There maybe a few holes in the states of the 
                        // SolidXmlReader maing the load into the XmlDocument necessary.
                        // If this is fixed the entryReader could be used directly in the transform.
                        XmlDocument xmldoc = new XmlDocument();
                        xmldoc.Load(entryReader);
                        transform.Transform(entryReader, arguments, xmlWriter);
                    }
                    xmlReader.Close();

                }
                xmlWriter.WriteEndElement();
            }
            else
            {
                throw new Exception(string.Format("XSL unknown process method '{0:s}'", _processMethod));
            }

            xmlWriter.WriteEndDocument();
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
