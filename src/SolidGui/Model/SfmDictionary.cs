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
        private readonly Dictionary<string, int> _markerFrequencies;
        private readonly Dictionary<string, int> _markerErrors;

    	private int _currentIndex;

        public SfmDictionary()
        {
            _currentIndex = 0;
            _recordList = new List<Record>();
            _markerFrequencies = new Dictionary<string, int>();
            _markerErrors = new Dictionary<string, int>();
            _filePath = Path.Combine(Path.GetTempPath(), Path.GetTempFileName());
            File.GetLastWriteTime(_filePath);  //JMC: does this do anything (side effect)? If not, remove it?
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
 
        /*
        public string GetFileNameNoExtension()
        {
            return Path.GetFileNameWithoutExtension(_filePath);
        }
        */

        public void Clear()
        {
            _recordList.Clear();
            _markerFrequencies.Clear();
        }

        public void AddRecord(SfmLexEntry entry, SolidReport report)
        {
//            _recordList.Add(new Record(entry, report));
            var record = new Record(entry, report);
            UpdateMarkerStatistics(record);
            _recordList.Add(record);
        }

        private void UpdateMarkerStatistics(Record record)
        {
            foreach (SfmFieldModel field in record.Fields)
            {
                if (!_markerFrequencies.ContainsKey(field.Marker))
                {
                    _markerFrequencies.Add(field.Marker, 0);
                    _markerErrors.Add(field.Marker, 0);
                }

                _markerFrequencies[field.Marker] += 1;

                if (field.HasReportEntry)
                {
                    _markerErrors[field.Marker] += 1;
                }
            }
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

        private void OnDoOpenWork(Object sender, DoWorkEventArgs args)
        {
            var progressState = (ProgressState)args.Argument;
            var openArguments = (DictionaryOpenArguments)progressState.Arguments;
            openArguments.FilterSet.BeginBuild(this);

            var sfmDataSet = new SfmDictionary();//SfmDataSet();

           
            progressState.TotalNumberOfSteps = sfmDataSet.Count;
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

            openArguments.FilterSet.EndBuild();
        }

        private void ReadDictionary(ProgressState progressState, DictionaryOpenArguments openArguments)
        {
            var processes = new List<IProcess>();
            processes.Add(new ProcessEncoding(openArguments.SolidSettings));
            processes.Add(new ProcessStructure(openArguments.SolidSettings));

            using (var reader = SfmRecordReader.CreateFromFilePath(_filePath))
            {
                while (reader.Read())
                {
                    progressState.NumberOfStepsCompleted += 1; // TODO Fix the progress to use file size and progress through the file from SfmRecordReader CP 2010-08 

                    var lexEntry = SfmLexEntry.CreateFromReader(reader);
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
            	SfmHeader = reader.Header;
            }

        }

    	private SfmRecord SfmHeader { get; set; }

    	public void Open(string path, SolidSettings solidSettings, RecordFilterSet filterSet)
        {
            Palaso.Reporting.Logger.WriteEvent("Opening {0}",path);


            _filePath = path;
            File.GetLastWriteTime(_filePath);  //JMC: does this do anything (side effect)? If not, remove it?

            _recordList.Clear();
            _markerFrequencies.Clear();
            _markerErrors.Clear();

            using (var dlg = new ProgressDialog())
            {
                dlg.Overview = "Opening file...";

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
                    return;
                }
            }

            if (_currentIndex > _recordList.Count - 1)
            {
                _currentIndex = 0;
            }
            Palaso.Reporting.Logger.WriteEvent("Done Opening.");
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
                using (var writer = new StreamWriter(new FileStream(_filePath, FileMode.Create, FileAccess.Write), Encoding.GetEncoding("iso-8859-1")))
                {
					/* TODO One day it might be nice to refactor this to use a (say) SfmRecordWriter, then 
					 * we could keep more info from the SfmRecordReader for use by the writer and do a
					 * better job of 'doing no harm' to the file, by detecting characteristics such as
					 * trailing white space on empty markers, lines between lx, headers etc.
					 */
                	foreach (var field in SfmHeader)
                	{
						writer.Write("\\");
                		writer.Write(field.Marker);
						writer.Write(" ");
						writer.Write(field.Value);
						writer.Write("\r\n");
					}
                    foreach (var record in _recordList)
                    {
						writer.Write("\r\n");
                        foreach (var field in record.Fields)
                        {
                            if (!field.Inferred)
                            {
                                writer.Write("\\");
                                writer.Write(field.Marker.TrimStart('_'));
								if (field.HasValue)
								{
									writer.Write(" ");
									writer.Write(field.Value);
								}
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