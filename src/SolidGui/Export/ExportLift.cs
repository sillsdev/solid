﻿// Copyright (c) 2007-2014 SIL International
// Licensed under the MIT license: opensource.org/licenses/MIT

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using SIL.DictionaryServices.Lift;
using SIL.DictionaryServices.Model;
using SIL.Lift.Validation;
using SIL.Progress;
using SIL.Reporting;
using SIL.WritingSystems;
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
            string logPath = outputFilePath + ".exportErrors.txt";
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
                Dictionary<string, SfmLiftLexEntryAdapter> index = ConvertAllEntriesToLift(sfmLexEntries, dm, solidSettings, liftLexEntries, progress);

                // second pass
                ResolveTargetsOfRelations(liftLexEntries, progress, index);
                dm.SaveItems(from x in dm.GetAllItems() select dm.GetItem(x));
            }

            if (!string.IsNullOrEmpty(stringBuilderProgress.Text))
            {
                using (StreamWriter log = File.AppendText(logPath))
                {
                    log.WriteLine(stringBuilderProgress.Text);
                }
            }
            outerProgress.WriteMessage("");
            outerProgress.WriteMessage("Checking result to make sure it is valid LIFT XML...");
            try
            {
                Validator.CheckLiftWithPossibleThrow(outputFilePath);
            }
            catch (Exception e)
            {
                outerProgress.WriteError(e.Message);
            }
            WriteWritingSystemFolder(outputFilePath, solidSettings.MarkerSettings, outerProgress);
            outerProgress.WriteMessage("Done");
        }

        private static void WriteWritingSystemFolder(string outputFilePath, IEnumerable<SolidMarkerSetting> markerSettings, IProgress outerProgress)
        {
            outerProgress.WriteMessage("Writing of LDML files in LIFT export needs to be re-implemented to work with libpalaso 3.1");
//# if needsToBeFIxed
            outerProgress.WriteMessage("Copying Writing System files...");
            try
            {
                // ReSharper disable AssignNullToNotNullAttribute
                //var repository = AppWritingSystems.WritingSystems as LdmlInFolderWritingSystemRepository; //<---- this is broken because AppWritingSystems.WritingSystems is no longer an instance of LdmlInFolderWritingSystemRepository
                var repository = (GlobalWritingSystemRepository) AppWritingSystems.WritingSystems;

                string dir = Path.GetDirectoryName(outputFilePath);
                string writingSystemsPath = Path.Combine(dir, "WritingSystems");
                if (!Directory.Exists(writingSystemsPath))
                {
                    Directory.CreateDirectory(writingSystemsPath);
                }
                
                //only copy the ones being used
                foreach (WritingSystemDefinition definition in repository.AllWritingSystems)
                {
                    WritingSystemDefinition definition1 = definition; //avoid "access to modified closure"
                    if (null != markerSettings.FirstOrDefault(m => m.WritingSystemRfc4646 == definition1.LanguageTag))
                    {
                        string existing = repository.GetFilePathFromLanguageTag(definition.LanguageTag);
                        string path = Path.Combine(writingSystemsPath, Path.GetFileName(existing));
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
            foreach (Record sfmLexEntry in sfmLexEntries)
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
                    string sfmId = adaptedEntry.SfmID;
                    int homograph = 0;
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
                foreach (SfmLiftLexEntryAdapter adaptedEntry in liftLexEntries)
                {
                    try
                    {
                        foreach (Relation relation in adaptedEntry.Relations)
                        {
                            ResolveTargetOfRelation(index, relation, adaptedEntry, progress);
                        }

                        foreach (KeyValuePair<LexSense, Relation> senseAndRelation in adaptedEntry.SenseRelations)
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
            foreach (LiftLexEntryAdapter subEntry in adaptedEntry.SubEntries)
            {
                foreach (Relation relation in subEntry.Relations)
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
            SolidSettings solidSettings = SolidSettings.OpenSolidFile(SolidSettings.GetSettingsFilePathFromDictionaryPath(exportArguments.inputFilePath));
            dictionary.Open(exportArguments.inputFilePath, solidSettings, new RecordFilterSet());  //JMC:! Will this inputFilePath pick up any "latest edits"--including any preprocessing cleanup? (See MainWindowPM; look for "cleanup".)
            Export(dictionary.AllRecords, solidSettings, exportArguments.outputFilePath, exportArguments.progress);
        }

        public string ModifyDestinationIfNeeded(string destinationFilePath)
        {
            // ReSharper disable AssignNullToNotNullAttribute
            string destDirectory = Path.GetDirectoryName(destinationFilePath);
            bool foundUnknown = false;
            foreach(string file in Directory.GetFiles(destDirectory))
            {
                if (file == destinationFilePath)
                    continue;
                if(Path.GetExtension(file)==".WeSayConfig")
                    return destinationFilePath;//this is already a wesay lift folder, so go ahead.
                foundUnknown = true;
            }
            foreach (string dir in Directory.GetDirectories(destDirectory))
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

            string suggested = Path.Combine(destDirectory, Path.GetFileNameWithoutExtension(destinationFilePath));
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
