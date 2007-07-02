using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace SolidEngine
{
    public class Solidifier
    {
        string _reportFile;
        string _outputFile;

      //  SolidSettings _solidFile = new SolidSettings();
        SolidReport _solidReport = new SolidReport();

        public Solidifier()
        {
        
        }

        public void Process(string file)
        {
            SfmXmlReader reader = new SfmXmlReader(file);
            Process(reader);
        }

        public void Process(XmlReader xr)
        {
            ProcessStructure process = new ProcessStructure(_solidReport, null); //!!!
            while (xr.ReadToFollowing("entry"))
            {
                XmlReader entryReader = xr.ReadSubtree();
                // Load the current record from xr into an XmlDocument
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.Load(xr);
                process.Process(xmldoc);
            }
        }

        private void ProcessEntry(XmlReader r)
        {

        }

    }
}
