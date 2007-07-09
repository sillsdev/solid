using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using SolidEngine;

namespace SolidGui
{
    public class Dictionary : RecordManager
    {
        private List<Record> _recordList;
        private string _filePath;
        private List<string> _allMarkers;
        private DateTime _lastWrittenTo;

        private int _currentIndex;

        public Dictionary()
        {
            _recordList = new List<Record>();
            _allMarkers = new List<string>();
            _filePath = Path.Combine(Path.GetTempPath(), Path.GetTempFileName());
            _lastWrittenTo = File.GetLastWriteTime(_filePath);
        }

        public Dictionary(string path)
        {
            _recordList = new List<Record>();
            _allMarkers = new List<string>();
            
            Open(path);
        }

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

        
        public Record Get(int index)
        {
            return _recordList[index];
        }

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
            _allMarkers.Clear();
        }

        public void AddRecord(XmlNode entry, SolidReport report)
        {
            _recordList.Add(new Record(entry, report));
        }

        public void AddRecord(List<string> fieldValues)
        {
            _recordList.Add(new Record(fieldValues));
        }

        public void Open(string path)
        {
            _filePath = path;
            _lastWrittenTo = File.GetLastWriteTime(_filePath);

            _recordList.Clear();
            _allMarkers.Clear();
            using (TextReader dictReader = File.OpenText(_filePath))
            {
                SolidEngine.SfmRecordReader reader = new SolidEngine.SfmRecordReader(dictReader, 10000);
                while (reader.Read())
                {
                    if (reader.FieldCount == 0)
                        continue;

                    List<string> fieldValues = new List<string>();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        fieldValues.Add("\\" + reader.Key(i) + " " + reader.Value(i));
                        if (!_allMarkers.Contains(reader.Key(i)))
                        {
                            _allMarkers.Add(reader.Key(i));
                        }
                    }
                    _recordList.Add(new Record(fieldValues));
                }
            }
        }

        public bool Save()
        {
            if (_lastWrittenTo == File.GetLastWriteTime(_filePath) ||
                !File.Exists(_filePath))
            {
                if (_recordList != null)
                {
                    StringBuilder builder = new System.Text.StringBuilder();
                    for (int i = 0; i < _recordList.Count; i++)
                    {
                        builder.Append(_recordList[i].ToStringWithoutInferred());
                    }
                    File.WriteAllText(_filePath, builder.ToString());
                    _lastWrittenTo = File.GetLastWriteTime(_filePath);
                    return true;
                }
            }
            Reporting.ErrorReporter.ReportNonFatalMessage("The file has been altered outside of Solid");
            return false;
        }

        public Dictionary CopyTo(string path)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < _recordList.Count; i++)
            {
                builder.Append(_recordList[i].Value);
            }
            File.WriteAllText(path, builder.ToString());
            _lastWrittenTo = File.GetLastWriteTime(_filePath);

            return new Dictionary(path);
        }

        public List<string> AllMarkers
        {
            get { return _allMarkers; }
            set { _allMarkers = value; }
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

    }
}
