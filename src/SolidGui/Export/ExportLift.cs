// Copyright (c) 2007-2014 SIL International
// Licensed under the MIT license: opensource.org/licenses/MIT

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using Palaso.DictionaryServices.Lift;
using Palaso.DictionaryServices.Model;
using Palaso.Lift.Validation;
using Palaso.Progress;
using Palaso.Reporting;
using Palaso.WritingSystems;
using SolidGui.Engine;
using SolidGui.Filter;
using SolidGui.Model;
using System.Linq;

namespace SolidGui.Export
{
    public class ExportLift : IExporter
    {

        public void Export(IEnumerable<Record> sfmLexEntries, SolidSettings solidSettings, string outputFilePath, IProgress outerProgress)
        {
            UsageReporter.SendNavigationNotice("QuickFix/MoveUp");

            var liftLexEntries = new List<SfmLiftLexEntryAdapter>();
            var logPath = outputFilePath + ".exportErrors.txt";
            if (File.Exists(outputFilePath))
            {
                File.Delete(outputFilePath);
            }
            if (File.Exists(logPath))
            {
                File.Delete(logPath);
            }

            var stringBuilderProgress = new StringBuilderProgress();
            //our overal progress reports both to the UI and to this file we want to write out
            var progress = new MultiProgress(new[] { outerProgress, stringBuilderProgress });

            using (var dm = new LiftDataMapper(outputFilePath))
            {
                // first pass
                var index = ConvertAllEntriesToLift(sfmLexEntries, dm, solidSettings, liftLexEntries, progress);

                // second pass
                ResolveTargetsOfRelations(liftLexEntries, progress, index);
                dm.SaveItems(from x in dm.GetAllItems() select dm.GetItem(x));
            }

            if (!string.IsNullOrEmpty(stringBuilderProgress.Text))
            {
                using (var log = File.AppendText(logPath))
                {
                    log.WriteLine(stringBuilderProgress.Text);
                }
            }
            outerProgress.WriteMessage("");
            outerProgress.WriteMessage("Checking result to make sure it is valid LIFT XML...");
            var result = Validator.GetAnyValidationErrors(outputFilePath, new NullValidationProgress(), ValidationOptions.All);
            if(!string.IsNullOrEmpty(result))
                outerProgress.WriteError(result);
            WriteWritingSystemFolder(outputFilePath, solidSettings.MarkerSettings, outerProgress);
            outerProgress.WriteMessage("Done");
        }

        private static void WriteWritingSystemFolder(string outputFilePath, IEnumerable<SolidMarkerSetting> markerSettings, IProgress outerProgress)
        {
            outerProgress.WriteMessage("Copying Writing System files...");
            try
            {
                // ReSharper disable AssignNullToNotNullAttribute
                var repository = AppWritingSystems.WritingSystems as LdmlInFolderWritingSystemRepository;
                var dir = Path.GetDirectoryName(outputFilePath);
                var writingSystemsPath = Path.Combine(dir, "WritingSystems");
                if (!Directory.Exists(writingSystemsPath))
                {
                    Directory.CreateDirectory(writingSystemsPath);
                }
                
                //only copy the ones being used
                foreach (var definition in repository.AllWritingSystems)
                {
                    IWritingSystemDefinition definition1 = definition;//avoid "access to modified closure"
                    if (null != markerSettings.FirstOrDefault(m => m.WritingSystemRfc4646 == definition1.Bcp47Tag))
                    {
                        var existing = repository.GetFilePathFromIdentifier(definition.StoreID);
                        var path = Path.Combine(writingSystemsPath, Path.GetFileName(existing));
                        File.Copy(existing, path, true);
                    }
                }
                // ReSharper restore AssignNullToNotNullAttribute
            }
            catch(Exception e)
            {
                outerProgress.WriteError(e.Message);
            }
        }

        private static Dictionary<string, SfmLiftLexEntryAdapter> ConvertAllEntriesToLift(IEnumerable<Record> sfmLexEntries, LiftDataMapper dm, SolidSettings solidSettings, List<SfmLiftLexEntryAdapter> liftLexEntries, MultiProgress progress)
        {
            var index = new Dictionary<string, SfmLiftLexEntryAdapter>();
            foreach (var sfmLexEntry in sfmLexEntries)
            {
                try
                {
                    var adaptedEntry = new SfmLiftLexEntryAdapter(dm, sfmLexEntry.LexEntry, solidSettings);
                    liftLexEntries.Add(adaptedEntry);
                    adaptedEntry.PopulateEntry(progress);

                    /* this worked for one homograph with no actual ordernumber, but would crash when you hit a second one 
                         *
                         * if (index.ContainsKey(sfmId)) // is a duplicate
                          {
                              // get the duplicated entry from index and change its name to SfmID_HomonymNumber
                              var entry = index[sfmId];
                              entry.SfmID = entry.SfmID + "_" + entry.HomonymNumber;

                              // add the new adapted entry to the index with name SfmID_HomonymNumber
                              index.Add(sfmId + "_" + adaptedEntry.HomonymNumber, adaptedEntry);
                          }
                          */
                    var sfmId = adaptedEntry.SfmID;
                    var homograph = 0;
                    while (index.ContainsKey(sfmId)) // is a duplicate
                    {
                        ++homograph;
                        sfmId = adaptedEntry.SfmID + "_" + homograph;
                    }

                    // add to dictionary
                    index.Add(sfmId, adaptedEntry);

                }
                catch (Exception error)
                {
                    progress.WriteError(sfmLexEntry.LexEntry.GetLexemeForm(solidSettings) + ": " + error);
                }
            }
            return index;
        }

            private static void ResolveTargetsOfRelations(IEnumerable<SfmLiftLexEntryAdapter> liftLexEntries, MultiProgress progress, Dictionary<string, SfmLiftLexEntryAdapter> index)
            {
                foreach (var adaptedEntry in liftLexEntries)
                {
                    try
                    {
                        foreach (var relation in adaptedEntry.Relations)
                        {
                            ResolveTargetOfRelation(index, relation, adaptedEntry, progress);
                        }
                        
                        foreach (var senseAndRelation in adaptedEntry.SenseRelations)
                        {
                            ResolveTargetOfSenseRelation(index, senseAndRelation.Key, senseAndRelation.Value, adaptedEntry, progress);
                        }

                        ProcessSubentryRelations(adaptedEntry, index, progress);
                    }
                    catch (Exception error)
                    {
                        progress.WriteError(adaptedEntry.SfmID + ": " + error);
                    }
                }
            }

        private static void ResolveTargetOfRelation(Dictionary<string, SfmLiftLexEntryAdapter> index,  Relation relation, SfmLiftLexEntryAdapter adaptedEntry, MultiProgress progress)
        {
            SfmLiftLexEntryAdapter targetLexEntry;
            if (index.ContainsKey(relation.TargetForm))
            {
                targetLexEntry = index[relation.TargetForm];
                string guidOfTargetEntry = targetLexEntry.GUID;
                adaptedEntry.MakeRelationFromEntryToEntry(guidOfTargetEntry, relation.Type);
            }
            else
            {
                adaptedEntry.AddSolidNote(String.Format("Cannot find the {0} lexical relation target: {1}", relation.Type, relation.TargetForm));
                progress.WriteWarning("Cannot find the {0} lexical relation target: {1}, referenced from {2}",relation.Type, relation.TargetForm, adaptedEntry.LiftLexEntry.GetSimpleFormForLogging());
            }
        }

        private static void ResolveTargetOfSenseRelation(Dictionary<string, SfmLiftLexEntryAdapter> index, LexSense sense, Relation relation, SfmLiftLexEntryAdapter adaptedEntry, MultiProgress progress)
        {
            SfmLiftLexEntryAdapter targetLexEntry;
            if (index.ContainsKey(relation.TargetForm))
            {
                targetLexEntry = index[relation.TargetForm];
                string guidOfTargetEntry = targetLexEntry.GUID;
                adaptedEntry.MakeRelationFromSenseToEntry(sense, guidOfTargetEntry, relation.Type);
            }
            else
            {
                adaptedEntry.AddSolidNote(String.Format("Cannot find the {0} lexical relation target: {1}",  relation.Type,relation.TargetForm));
                progress.WriteWarning("Cannot find {0} lexical relation target: {1}, referenced from {2}", relation.Type, relation.TargetForm, adaptedEntry.LiftLexEntry.GetSimpleFormForLogging());
            }
        }
        private static void ProcessSubentryRelations(SfmLiftLexEntryAdapter adaptedEntry, Dictionary<string, SfmLiftLexEntryAdapter> index, MultiProgress progress)
        {
            SfmLiftLexEntryAdapter targetLexEntry;
            foreach (var subEntry in adaptedEntry.SubEntries)
            {
                foreach (var relation in subEntry.Relations)
                {
                    // if the target is in the dictionary, add the relation, else post a note
                    if (index.ContainsKey(relation.TargetForm))
                    {
                        targetLexEntry = index[relation.TargetForm];
                        string guidOfTargetEntry = targetLexEntry.GUID;
                        adaptedEntry.MakeRelationFromEntryToEntry(guidOfTargetEntry, relation.Type);

                    }
                    else
                    {
                        adaptedEntry.AddSolidNote(String.Format("Cannot find subentry target: {0}", relation.TargetForm));
                        progress.WriteWarning("Cannot find subentry target: {0}, referenced from {1}", relation.TargetForm, adaptedEntry.LiftLexEntry.GetSimpleFormForLogging());
                    }
                }

                // dm.SaveItem(subEntry.LiftLexEntry);
            }
        }


        public void ExportAsync(object sender, DoWorkEventArgs args)
        {
            var exportArguments = (ExportArguments)args.Argument;

            var dictionary = new SfmDictionary();
            var solidSettings = SolidSettings.OpenSolidFile(SolidSettings.GetSettingsFilePathFromDictionaryPath(exportArguments.inputFilePath));
            dictionary.Open(exportArguments.inputFilePath, solidSettings, new RecordFilterSet());
            Export(dictionary.AllRecords, solidSettings, exportArguments.outputFilePath, exportArguments.progress);
        }

        public string ModifyDestinationIfNeeded(string destinationFilePath)
        {
            // ReSharper disable AssignNullToNotNullAttribute
            var destDirectory = Path.GetDirectoryName(destinationFilePath);
            bool foundUnknown = false;
            foreach(var file in Directory.GetFiles(destDirectory))
            {
                if (file == destinationFilePath)
                    continue;
                if(Path.GetExtension(file)==".WeSayConfig")
                    return destinationFilePath;//this is already a wesay lift folder, so go ahead.
                foundUnknown = true;
            }
            foreach (var dir in Directory.GetDirectories(destDirectory))
            {
                if (Path.GetFileName(dir) == "WritingSystems")
                    continue;
                if (Path.GetFileName(dir) == "export")
                    continue; 
                if (Path.GetFileName(dir) == "audio")
                    continue; 

                foundUnknown = true;
            }
            if(!foundUnknown)
                return destinationFilePath;

            var suggested = Path.Combine(destDirectory, Path.GetFileNameWithoutExtension(destinationFilePath));
            var s = string.Format("The folder you've selected has other files in it. Would you rather Solid export all the lift files into their own folder at {0}?", suggested);
            if(DialogResult.Yes == MessageBox.Show(s, "Destination check", MessageBoxButtons.YesNo))
            {
                if(!Directory.Exists(suggested))
                    Directory.CreateDirectory(suggested);
                return Path.Combine(suggested, Path.GetFileName(destinationFilePath));
            }
            return destinationFilePath;
            // ReSharper restore AssignNullToNotNullAttribute
        }

        public static IExporter Create()
        {
            return new ExportLift();
        }

        public const string DriverName = "Lift";

        public static ExportHeader GetHeader()
        {
            return new ExportHeader { Driver = DriverName, FileNameFilter = "LIFT (*.lift)|*.lift", Name = "LIFT" };
        }

    }
}
