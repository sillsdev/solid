using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace SolidEngine
{
    public class SolidReport
    {
        public enum EntryType
        {
            StructureInsertInInferredFailed,
            StructureParentNotFound,
            StructureParentNotFoundForInferred,
            Max
        }

        private XmlDocument _xmlDoc = new XmlDocument();
        private int _entryID = 0;

        [XmlIgnore]
        private string _filePath;

        public string FilePath
        {
            get { return _filePath; }
            set { _filePath = value; }
        }
	
        public int Count
        {
            get { return _xmlDoc.ChildNodes.Count; }
        }

        public SolidReport()
        {
        }

        private SolidReport(string filePath)
        {
            _filePath = filePath;
            _xmlDoc.Load(_filePath);
        }

        public void Reset()
        {
            _entryID = 0;
        }

        public void AddEntry(EntryType type, XmlNode entry, XmlNode field, string description)
        {
            XmlHelper xmlHelp = new XmlHelper(_xmlDoc);
            XmlNode reportEntry = _xmlDoc.CreateElement("entry");
            xmlHelp.AppendAttribute(reportEntry, "id", String.Format("{0:D}", _entryID));
            if (entry != null)
            {
                xmlHelp.AppendAttribute(reportEntry, "record", entry.Attributes["record"].Value);
            }
            if (field != null)
            {
                xmlHelp.AppendAttribute(reportEntry, "field", field.Attributes["field"].Value); //!!! TODO
                xmlHelp.AppendAttribute(reportEntry, "marker", field.Name);
            }
            reportEntry.InnerText = description;
            _xmlDoc.AppendChild(reportEntry);

            _entryID++;
        }

        public XmlNode AllEntries()
        {
            return _xmlDoc;
        }

        public XmlNode EntriesForRecord(int record)
        {
            return null;
        }

        public XmlNode EntriesForMarker(string marker)
        {
            return null;
        }

        public XmlNode EntriesForXPath(string xpath)
        {
            return null;
        }

        public static SolidReport OpenSolidReport(string filePath)
        {
            SolidReport r = new SolidReport(filePath);
            return r;
        }

        public void Save()
        {
            SaveAs(_filePath);
        }

        public void SaveAs(string filePath)
        {
            _filePath = filePath;
            _xmlDoc.Save(_filePath);
        }

    }

}
