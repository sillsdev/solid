// Copyright (c) 2007-2014 SIL International
// Licensed under the MIT license: opensource.org/licenses/MIT

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows.Forms;

using Palaso.Reporting;
using Palaso.Progress;
using Palaso.UI.WindowsForms.Progress;
using SolidGui.Engine;
using SolidGui.Filter;
using SolidGui.Processes;

namespace SolidGui.Model
{
    public struct DictionaryOpenArguments
    {
        public SolidSettings SolidSettings;
        public RecordFilterSet FilterSet;
    }

    public class SfmDictionary : RecordManager
    {
        private List<Record> _recordList;
        private string _filePath;
        private Dictionary<string, int> _markerFrequencies;
        private Dictionary<string, int> _markerErrors;

        private int _currentIndex;

        public SfmDictionary()
        {
            _currentIndex = 0;
            _recordList = new List<Record>();
            _filePath = Path.Combine(Path.GetTempPath(), Path.GetTempFileName());
//            File.GetLastWriteTime(_filePath);  // Not sure where this was heading. Disabled for now. -JMC
            Reset();
        }

        /// <summary>
        /// Call this during construction [JMC: and maybe after cancelling a File Open. Is that really needed? If so, do a Recheck too.]
        /// </summary>
        public void Reset()
        {
            _markerFrequencies = new Dictionary<string, int>();
            _markerErrors = new Dictionary<string, int>();
            // _currentIndex = 0; //add?
        }
       
        public List<Record> Records
        {
            get { return _recordList; }
        }

        public string ShortLabel()
        {
            if (_filePath.Length > 12)
            {
                return System.IO.Path.GetFileName(_filePath);
            }
            return _filePath;
        }


        public override string ToString()
        {
            return string.Format("{{{0} {1}}}", ShortLabel(), GetHashCode());
        }

        public override int Count
        {
            get { return _recordList.Count; }
        }

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
            return Path.GetDirectoryName(_filePath);
            //was return _filePath.Substring(0, _filePath.LastIndexOf(@"\")); -JMC
        }
 
        public void Clear()
        {
            _recordList.Clear();
            _markerFrequencies.Clear();
            _markerErrors.Clear();
        }

        public void AddRecord(SfmLexEntry entry, SolidReport report)
        {
//            _recordList.Add(new Record(entry, report));
            var record = new Record(entry, report);
            UpdateMarkerStatistics(record);
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

        private void UpdateMarkerStatistics(Record record)
        {
            foreach (SfmFieldModel field in record.Fields)
            {
                if (!_markerFrequencies.ContainsKey(field.Marker))
                {
                    _markerFrequencies.Add(field.Marker, 0);
                    _markerErrors.Add(field.Marker, 0);  //can give a "key already exists" error if the code isn't perfect -JMC
                }

                _markerFrequencies[field.Marker] += 1;

                if (field.HasReportEntry)
                {
                    _markerErrors[field.Marker] += 1;
                }
            }
        }

        private void OnDoOpenWork(Object sender, DoWorkEventArgs args)
        {
            var progressState = (ProgressState)args.Argument;
            var openArguments = (DictionaryOpenArguments)progressState.Arguments;
            openArguments.FilterSet.BeginBuild(this);

            // var sfmDataSet = new SfmDictionary();//SfmDataSet();


            progressState.TotalNumberOfSteps = 0; // = sfmDataSet.Count;
            progressState.NumberOfStepsCompleted = 1;
            try
            {
                ReadDictionary(progressState, openArguments);
            }
            catch (FileNotFoundException e)
            {
                ErrorReport.NotifyUserOfProblem(
                    "The specified file was not found. The error was\r\n" + e.Message);
                return;
            }
            catch (DataMisalignedException e)
            {
                ErrorReport.NotifyUserOfProblem(
                    "Unable to finish opening the file. The error was\r\n" + e.Message);
                return;
            }

            openArguments.FilterSet.EndBuild();
        }

        private void ReadDictionary(ProgressState progressState, DictionaryOpenArguments openArguments)
        {
            //TODO! Merge some of this code with ProcessEncoding.Process ("hacked fonts") . See esp FilterSet.AddRecord and CreateSolidErrorRecordFilter  -JMC

            var processes = new List<IProcess>();
            processes.Add(new ProcessEncoding(openArguments.SolidSettings));
            processes.Add(new ProcessStructure(openArguments.SolidSettings));

            using (var reader = SfmRecordReader.CreateFromFilePath(_filePath))
            {
                progressState.TotalNumberOfSteps = reader.SizeEstimate;  // added -JMC 2013-09
                while (reader.ReadRecord())
                {
                    // TODO: Fix the progress to use file size and progress through the file from SfmRecordReader CP 2010-08
                    // Partly done. But maybe should do this: divide elapsed time by what "should" have elapsed, then make a new (separate) estimate using that multiplier. -JMC 2013-09
                    progressState.NumberOfStepsCompleted += 1; 

                    SfmLexEntry lexEntry = SfmLexEntry.CreateFromReaderFields(reader.Fields);
                    var recordReport = new SolidReport();
                    foreach (IProcess process in processes)
                    {
                        lexEntry = process.Process(lexEntry, recordReport);
                    }
                    AddRecord(lexEntry, recordReport);
                    if (openArguments.FilterSet != null)
                    {
                        openArguments.FilterSet.AddRecord(Count - 1, recordReport);
                    }
                }
                SfmHeader = reader.HeaderLinux;
            }

        }

        public string SfmHeader { get; set; }

        public bool Open(string path, SolidSettings solidSettings, RecordFilterSet filterSet)
        {
            Palaso.Reporting.Logger.WriteEvent("Opening {0}",path);

            _filePath = path;
            //            File.GetLastWriteTime(_filePath);  // Not sure where this was heading. Disabled for now. -JMC

            Clear();

            /*
            //TODO: #1291 Show zero-count markers in the UI list too... -JMC
            foreach (string s in solidSettings.Markers)
            {
                try
                {
                    _markerFrequencies.Add(s, 0);
                }
                catch (ArgumentException e)
                {
                    Palaso.Reporting.ErrorReport.ReportFatalException(new ArgumentException("The .solid configuration file appears to have multiple settings for this marker: \\" + s + " \r\n" + e.Message, e));
                }
            }
            */
             
            using (var dlg = new ProgressDialog())  // JMC:! Move this UI stuff elsewhere?
            {
                dlg.Overview = "Loading and checking data...";

                var worker = new BackgroundWorker();
                worker.DoWork += OnDoOpenWork;
                dlg.BackgroundWorker = worker;
                dlg.CanCancel = true;

                var openArguments = new DictionaryOpenArguments();
                openArguments.FilterSet = filterSet;
                openArguments.SolidSettings = solidSettings;

                dlg.ProgressState.Arguments = openArguments;
                dlg.ShowDialog();
                if (dlg.ProgressStateResult != null && dlg.ProgressStateResult.ExceptionThatWasEncountered != null)
                {
                    Palaso.Reporting.ErrorReport.ReportNonFatalException(dlg.ProgressStateResult.ExceptionThatWasEncountered);  
                    //I suppose this is non-fatal because we'll just fall back to whatever was already open, same as with Cancel. -JMC
                    return false;
                }
                if (dlg.ProgressState.Cancel == true) return false;
            }

            if (_currentIndex > _recordList.Count - 1)
            {
                _currentIndex = 0;
            }
            Palaso.Reporting.Logger.WriteEvent("Done Opening.");
            return true;
        }

        public bool Save()
        {
            SaveAs(_filePath, null);
            // JMC: resurrect or delete the following old code? (First, search for other calls to GetLastWriteTime
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

        public bool SaveAs(string path, SolidSettings ss)
        {
            var rf = new RecordFormatter();
            rf.SetDefaultsDisk();
            return SaveAs(path, ss, rf);
        }

        // This may or may not be a real save that the UI would reflect to the user and file system.
        // It does cause _filePath to be set, but the calling code might also keep track of a "real" dictionary path. -JMC
        public bool SaveAs(string path, SolidSettings ss, RecordFormatter rf)
        {
            Logger.WriteEvent("Saving {0}", path);
            _filePath = path; // side effect
            try
            {
                using (var writer = new StreamWriter(new FileStream(_filePath, FileMode.Create, FileAccess.Write), SolidSettings.LegacyEncoding))
                {

                    writer.Write(SfmRecordReader.HeaderToWrite(SfmHeader, rf.NewLine));

                    foreach (Record record in _recordList)
                    {
                        writer.Write(rf.FormatPlain(record, ss));
                        /*
                        foreach (var field in record.Fields)
                        {
                            if (!field.Inferred)
                            {
                                writer.Write("\\");
                                writer.Write(field.Marker); // .TrimStart('_')); //I think this was being trimmed off because the old parser detected the header based on leading underscores. -JMC 2013-10
                                if (field.HasValue)
                                {
                                    writer.Write(" ");
                                    writer.Write(field.Value);
                                }
                                writer.Write(field.Trailing);
                            } 
                        }*/
                    }
                    writer.Close();
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(null, exception.Message + "\r\n\r\nYou might try saving to a different location.", "Error on saving data file.");
                return false;
            }
            Palaso.Reporting.Logger.WriteEvent("Done saving data file.");
            return true;
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

        internal bool DeleteRecord(Record rec)
        {
            //int i = FindRecord(id);
            return _recordList.Remove(rec);
        }
    }
}