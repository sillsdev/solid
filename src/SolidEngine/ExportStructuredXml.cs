using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml;
using Palaso.Progress;

namespace SolidEngine
{
    public class ExportStructuredXml : IExporter
    {

        private ProgressState _progressState = null;

        public static ExportStructuredXml Create()
        {
            return new ExportStructuredXml();
        }

        private ExportStructuredXml()
        {
        }

        public void Export(string srcFile, string desFile)
        {
        }
        
        public void OnDoWork(object sender, DoWorkEventArgs args)
        {
            _progressState = (ProgressState)args.Argument;
            _progressState.TotalNumberOfSteps = 0;
            ExportArguments exportArguments = (ExportArguments)_progressState.Arguments;
            SolidXmlReader xmlReader = new SolidXmlReader(exportArguments.inputFilePath);
            XmlTextWriter xmlWriter = new XmlTextWriter(exportArguments.outputFilePath, Encoding.UTF8);
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
