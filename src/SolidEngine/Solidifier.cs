using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace SolidEngine
{
    public class Solidifier
    {
        public Solidifier()
        {        
        }

        public SolidReport Process(string filePath)
        {
            SolidSettings solidSettings = SolidSettings.OpenSolidFile(SolidSettings.SettingsFilePath(filePath));
            return Process(filePath, solidSettings);
        }

        public SolidReport Process(string filePath, SolidSettings solidSettings)
        {
            SfmXmlReader reader = new SfmXmlReader(filePath);
            return Process(reader, solidSettings);
        }

        public SolidReport Process(XmlReader xr, SolidSettings solidSettings)
        {
            SolidReport solidReport = new SolidReport();
            ProcessStructure process = new ProcessStructure(solidReport, solidSettings);
            while (xr.ReadToFollowing("entry"))
            {
                XmlReader entryReader = xr.ReadSubtree();
                // Load the current record from xr into an XmlDocument
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.Load(entryReader);
                process.Process(xmldoc.DocumentElement);
            }
            return solidReport;
        }

    }
}
