using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SolidGui
{
    public class Dictionary
    {
        private List<Record> _recordList;
        private string _filePath;
        private List<string> _allMarkers;
        private DateTime _lastWrittenTo;

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

        public string GetDirectoryPath()
        {
            return _filePath.Substring(0, _filePath.LastIndexOf(@"\"));
        }
 
        public string GetFileNameNoExtension()
        {
            return Path.GetFileNameWithoutExtension(_filePath);
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

                    StringBuilder recordContents = new StringBuilder();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        recordContents.AppendLine("\\" + reader.Key(i) + " " + reader.Value(i));
                        if (!_allMarkers.Contains(reader.Key(i)))
                        {
                            _allMarkers.Add(reader.Key(i));
                        }
                    }
                    _recordList.Add(new Record(recordContents.ToString()));
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
                        builder.Append(_recordList[i].Value);
                    }
                    File.WriteAllText(_filePath, builder.ToString());
                    _lastWrittenTo = File.GetLastWriteTime(_filePath);
                    return true;
                }
            }
            Reporting.ErrorReporter.ReportNonFatalMessage("The file has been altered outside of Solid");
            return false;
        }

        public Dictionary SaveAs(string path)
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
