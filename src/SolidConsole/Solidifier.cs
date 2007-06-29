using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace SolidConsole
{
    public class Solidifier
    {
        string _reportFile;
        string _outputFile;

        SolidFile _solidFile = new SolidFile();
        SolidReport _solidReport = new SolidReport();

        public Solidifier()
        {
        
        }

        public void Process(string file)
        {
            SfmXmlReader reader = new SfmXmlReader(file);
            Process(reader);
        }

        public void Process(XmlReader r)
        {
            while (r.ReadToFollowing("entry"))
            {
                XmlReader entryReader = r.ReadSubtree();
                ProcessEntry(entryReader);
            }
        }

        private void ProcessEntry(XmlReader r)
        {
        }

    }
}
