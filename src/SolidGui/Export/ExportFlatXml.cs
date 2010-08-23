using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml;
using Palaso.Progress;
using SolidGui.Engine;

namespace SolidGui.Export
{
    public class ExportFlatXml : IExporter
    {

        private ProgressState _progressState = null;

        public static ExportFlatXml Create()
        {
            return new ExportFlatXml();
        }

        private ExportFlatXml()
        {
        }

        public void Export(string srcFile, string desFile)
        {
        }

        public void ExportAsync(object sender, DoWorkEventArgs args)
        {
            _progressState = (ProgressState)args.Argument;
            ExportArguments exportArguments = (ExportArguments)_progressState.Arguments;
            SfmXmlReader xmlReader = new SfmXmlReader(exportArguments.inputFilePath);
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