using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using SolidEngine;

namespace SolidGui
{
    public class Dictionary : RecordManager
    {
        private List<Record> _recordList;
        private string _filePath;
        private Dictionary<string, int> _markerFrequencies;
        private Dictionary<string, int> _markerErrors;
        private DateTime _lastWrittenTo;

        private int _currentIndex;

        public Dictionary()
        {
            _currentIndex = 0;
            _recordList = new List<Record>();
            _markerFrequencies = new Dictionary<string, int>();
            _markerErrors = new Dictionary<string, int>();
            _filePath = Path.Combine(Path.GetTempPath(), Path.GetTempFileName());
            _lastWrittenTo = File.GetLastWriteTime(_filePath);
        }
        /*
        public Dictionary(string path)
        {
            _recordList = new List<Record>();
            _allMarkers = new List<string>();
            
            Open(path);
        }
        */
       
        public List<Record> Records
        {
            get { return _recordList; }
        }

        public override int Count
        {
            get { return _recordList.Count; }
        }
        /*
        public override IEnumerator<Record> GetEnumerator()
        {
            return _recordList.GetEnumerator();
        }
        */

        public override Record Current
        {
            get{ 
                    if(_recordList.Count > 0)
                        return _recordList[_currentIndex];
                    return null;
               }
        }

        public override int CurrentIndex
        {
            get
            {
                return _currentIndex;
            }
            set
            {
                MoveTo(value);
            }
        }

        public override bool HasPrevious()
        {
            return _currentIndex > 0;
        }

        public override bool HasNext()
        {
            return _currentIndex < _recordList.Count - 1;
        }

        public override bool MoveToNext()
        {
            bool retval = HasNext();
            if (retval)
            {
                _currentIndex++;
            }
            return retval;
        }

        public override bool MoveToPrevious()
        {
            bool retval = HasPrevious();
            if (retval)
            {
                _currentIndex--;
            }
            return retval;
        }

        /*
        public Record Get(int index)
        {
            return _recordList[index];
        }
        */

        public override bool MoveTo(int index)
        {
            bool retval = false;
            if (index >= 0 && index < Count)
            {
                _currentIndex = index;
                retval = true;
            }
            return retval;
        }

        public string GetDirectoryPath()
        {
            return _filePath.Substring(0, _filePath.LastIndexOf(@"\"));
        }
 
        public string GetFileNameNoExtension()
        {
            return Path.GetFileNameWithoutExtension(_filePath);
        }

        public void Clear()
        {
            _recordList.Clear();
            _markerFrequencies.Clear();
        }

        public void AddRecord(XmlNode entry, SolidReport report)
        {
//            _recordList.Add(new Record(entry, report));
            Record record = Record.CreateFromXml(entry, report);
            record.AddMarkerStatistics(_markerFrequencies, _markerErrors);
            _recordList.Add(record);
        }

        public void AddRecord(Record record)
        {
            _recordList.Add(record);
        }

        /* not used ???
        public void AddRecord(List<string> fieldValues)
        {
            _recordList.Add(new Record(fieldValues));
        }
        */

        public void Open(string path, SolidSettings solidSettings, RecordFilterSet filterSet)
        {
            Palaso.Reporting.Logger.WriteEvent("Openning {0}",path);

            filterSet.BeginBuild(this);

            _filePath = path;
            _lastWrittenTo = File.GetLastWriteTime(_filePath);

            _recordList.Clear();
            _markerFrequencies.Clear();
            _markerErrors.Clear();

            IProcess process = new ProcessStructure(solidSettings);
            using (XmlReader xr = new SfmXmlReader(_filePath))
            {
                while (xr.ReadToFollowing("entry"))
                {
                    XmlReader entryReader = xr.ReadSubtree();
                    // Load the current record from xr into an XmlDocument
                    XmlDocument xmldoc = new XmlDocument();
                    xmldoc.Load(entryReader);
                    SolidReport recordReport = new SolidReport();
                    XmlNode xmlResult = process.Process(xmldoc.DocumentElement, recordReport);
                    AddRecord(xmlResult, recordReport);
                    if (filterSet != null)
                    {
                        filterSet.AddRecord(Count - 1, recordReport);
                    }
                    //!!!_recordFilters.AddRecord(report);
                }
            }
            filterSet.EndBuild();
            if (_currentIndex > _recordList.Count - 1)
            {
                _currentIndex = 0;
            }
            Palaso.Reporting.Logger.WriteEvent("Done Openning.");
        }

        public bool Save()
        {
            SaveAs(_filePath);
            //if (_lastWrittenTo == File.GetLastWriteTime(_filePath) ||
            //    !File.Exists(_filePath))
            //{
            //    if (_recordList != null)
            //    {
            //        StringBuilder builder = new System.Text.StringBuilder();
            //        for (int i = 0; i < _recordList.Count; i++)
            //        {
            //            builder.Append(_recordList[i].ToStringWithoutInferred());
            //        }
            //        File.WriteAllText(_filePath, builder.ToString());
            //        _lastWrittenTo = File.GetLastWriteTime(_filePath);
            //        return true;
            //    }
            //}
            //Reporting.ErrorReporter.ReportNonFatalMessage("The file has been altered outside of Solid");
            return false;
        }

        public void SaveAs(string path)
        {
            Palaso.Reporting.Logger.WriteEvent("Saving {0}", path);
            _filePath = path;
            try
            {
                using (StreamWriter writer = new StreamWriter(new FileStream(_filePath, FileMode.Create, FileAccess.Write), Encoding.GetEncoding("iso-8859-1")))
                {
                    foreach (Record record in _recordList)
                    {
                        foreach (Record.Field field in record.Fields)
                        {
                            if (!field.Inferred)
                            {
                                writer.Write("\\");
                                writer.Write(field.Marker);
                                writer.Write(" ");
                                writer.Write(field.Value);
                                writer.Write("\r\n");
                            }
                        }
                    }
                    writer.Close();
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(null, exception.Message, "Solid Save Error");
            }
            Palaso.Reporting.Logger.WriteEvent("Done Saving.");
        }

        public IEnumerable<string> AllMarkers
        {
            get
            {
                return _markerFrequencies.Keys;
            }
        }

        public Dictionary<string,int> MarkerFrequencies
        {
            get
            {
                return _markerFrequencies;
            }
        }

        public Dictionary<string, int> MarkerErrors
        {
            get
            {
                return _markerErrors;
            }
        }

        public List<Record> AllRecords
        {
            get { return _recordList; }
            set { _recordList = value; }
        }

        public string FilePath
        {
            get { return _filePath; }
            set { _filePath = value; }
        }

        public override Record GetRecord(int index)
        {
            return _recordList[index];
        }
    }
}
