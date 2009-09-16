using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Solid.Engine
{
    public class Solidifier
    {

        public class Observer
        {
            public virtual void OnRecordProcess(XmlNode structure, SolidReport report)
            {
            }
        }

        List<Observer> _observers = new List<Observer>();

        public Solidifier()
        {        
        }

        public void Attach(Observer observer)
        {
            _observers.Add(observer);
        }

        public void Process(string filePath)
        {
            SolidSettings solidSettings = SolidSettings.OpenSolidFile(SolidSettings.GetSettingsFilePathFromDictionaryPath(filePath));
            Process(filePath, solidSettings);
        }

        public void Process(string filePath, SolidSettings solidSettings)
        {
            using (SfmXmlReader reader = new SfmXmlReader(filePath))
            {
                Process(reader, solidSettings);
            }
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
                SolidReport report = new SolidReport();
                process.Process(xmldoc.DocumentElement, report);
                //!!!OnRecord(process.Document, process.Report);
            }
        }

        public void OnRecord(XmlNode structure, SolidReport report)
        {
            foreach (Observer observer in _observers)
            {
                observer.OnRecordProcess(structure, report);
            }
        }

    }
}
