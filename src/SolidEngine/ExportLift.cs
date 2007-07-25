using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Xsl;

namespace SolidEngine
{
    public class ExportLift : IExporter
    {
        public ExportLift()
        {
        }

        private XmlReader CreateXslReader()
        {
            string filePath = "../../../SolidEngine/ExportLift.xsl";
            //all this just to allow a DTD statement in the source xslt
            XmlReaderSettings readerSettings = new XmlReaderSettings();
            readerSettings.ProhibitDtd = false;
            XmlReader xslReader = XmlReader.Create(new StreamReader(filePath), readerSettings);
            return xslReader;
        }

        public void Export(string srcFile, string desFile)
        {
            //SolidXmlReader xmlReader = new SolidXmlReader(srcFile);
            XmlReader xmlReader = new XmlTextReader(new StreamReader("../../../../data/dict-s.xml"));
            XmlTextWriter xmlWriter = new XmlTextWriter(desFile, Encoding.UTF8);

            XslCompiledTransform transform = new XslCompiledTransform();
            using (XmlReader xslReader = CreateXslReader())
            {
                transform.Load(xslReader);
                xslReader.Close();
            }
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
