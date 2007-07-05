using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace SolidEngine
{
    public class Solidifier
    {

        public class SolidifierObserver
        {
            public virtual void onRecord(XmlNode structure, SolidReport report)
            {
            }
        }

        List<SolidifierObserver> _observers = new List<SolidifierObserver>();

        public Solidifier()
        {        
        }

        public void Attach(SolidifierObserver observer)
        {
            _observers.Add(observer);
        }

        public void Process(string filePath)
        {
            SolidSettings solidSettings = SolidSettings.OpenSolidFile(SolidSettings.SettingsFilePath(filePath));
            Process(filePath, solidSettings);
        }

        public void Process(string filePath, SolidSettings solidSettings)
        {
            SfmXmlReader reader = new SfmXmlReader(filePath);
            Process(reader, solidSettings);
        }

        public void Process(XmlReader xr, SolidSettings solidSettings)
        {
            ProcessStructure process = new ProcessStructure(solidSettings);
            while (xr.ReadToFollowing("entry"))
            {
                XmlReader entryReader = xr.ReadSubtree();
                // Load the current record from xr into an XmlDocument
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.Load(entryReader);
                process.Process(xmldoc.DocumentElement);
                OnRecord(process.Document, process.Report);
            }
        }

        public void OnRecord(XmlNode structure, SolidReport report)
        {
            foreach (SolidifierObserver observer in _observers)
            {
                observer.onRecord(structure, report);
            }
        }

    }
}
