using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using Palaso.DictionaryServices.Lift;
using Palaso.DictionaryServices.Model;
using Palaso.Progress;
using Palaso.Progress.LogBox;
using SolidGui.Engine;
using SolidGui.Model;
using System.Linq;

namespace SolidGui.Export
{
    public class ExportLift : IExporter
    {

        public void Export(IEnumerable<Record> sfmLexEntries, SolidSettings solidSettings, string outputFilePath, IProgress outerProgress)
        {
            var index = new Dictionary<string, SfmLiftLexEntryAdapter>();
            var liftLexEntries = new List<SfmLiftLexEntryAdapter>();
            var logPath = outputFilePath+ ".exportErrors.txt";
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
            var progress = new MultiProgress(new IProgress[]{outerProgress, stringBuilderProgress});

            using (var dm = new LiftDataMapper(outputFilePath))
            {
                //REVIEW: If I'm (jh) reading this right, it will need a restructuring in order to support
                //relations which point from a sense to... anything.  That's becuase until the index
                //is build, we can't come up with the target id. Seems like we need to introduce a 
                //pre-pass, which only creates the index.  Then, make senses be able to output lexical
                //relations, at least to entries (pointing at specific senses would be harder still).

                // first pass
                foreach (var sfmLexEntry in sfmLexEntries)
                {
                    try
                    {
                        var adaptedEntry = new SfmLiftLexEntryAdapter(dm, sfmLexEntry.LexEntry, solidSettings);
                        liftLexEntries.Add(adaptedEntry);
                        adaptedEntry.PopulateEntry(progress);

                        if (index.ContainsKey(adaptedEntry.SfmID)) // is a duplicate
                        {
                            // get the duplicated entry from index and change its name to SfmID_HomonymNumber
                            var entry = index[adaptedEntry.SfmID];
                            entry.SfmID = entry.SfmID + "_" + entry.HomonymNumber;

                            // add the new adapted entry to the index with name SfmID_HomonymNumber
                            index.Add(adaptedEntry.SfmID + "_" + adaptedEntry.HomonymNumber, adaptedEntry);
                        }
                        else
                        {
                            // add to dictionary
                            index.Add(adaptedEntry.SfmID, adaptedEntry);
                        }
                    }
                    catch (Exception error)
                    {
                        progress.WriteError(sfmLexEntry.LexEntry.Name + ": " + error);
                    }
                }
                SfmLiftLexEntryAdapter targetLexEntry;

                // second pass
                foreach (var adaptedEntry in liftLexEntries)
                {
                    try
                    {
                        foreach (var relation in adaptedEntry.Relations)
                        {
                            // if the target is in the dictionary, add the relation, else post a note
                            if (index.ContainsKey(relation.TargetID))
                            {
                                targetLexEntry = index[relation.TargetID];
                                string guidOfTargetEntry = targetLexEntry.GUID;
                                adaptedEntry.MakeRelation(guidOfTargetEntry, relation.Type);

                            }
                            else
                            {
                                // add an error note
                                adaptedEntry.AddSolidNote(String.Format("Cannot find target: {0}", relation.TargetID));
                                // add a note
                                //adaptedEntry.LiftLexEntry.
                            }
                        }

                    //    dm.SaveItem(adaptedEntry.LiftLexEntry);

                        foreach (var subEntry in adaptedEntry.SubEntries)
                        {
                            foreach (var relation in subEntry.Relations)
                            {
                                // if the target is in the dictionary, add the relation, else post a note
                                if (index.ContainsKey(relation.TargetID))
                                {
                                    targetLexEntry = index[relation.TargetID];
                                    string guidOfTargetEntry = targetLexEntry.GUID;
                                    adaptedEntry.MakeRelation(guidOfTargetEntry, relation.Type);

                                }
                                else
                                {
                                    // add a note
                                    //adaptedEntry.LiftLexEntry.
                                }
                            }

                           // dm.SaveItem(subEntry.LiftLexEntry);
                        }

                        // Got any potential relations?
                        // if so
                        // foreach one find your target in the index, get it's id, and make the relation


                    }
                    catch (Exception error)
                    {
                        progress.WriteError(adaptedEntry.SfmID + ": "+ error);
                    }
                    
                }
                dm.SaveItems(from x in dm.GetAllItems() select dm.GetItem(x));
            }

            if (!string.IsNullOrEmpty(stringBuilderProgress.Text))
            {
                using (var log = System.IO.File.AppendText(logPath))
                {
                    log.WriteLine(stringBuilderProgress.Text);
                }
            }
            outerProgress.WriteMessage("");
            outerProgress.WriteMessage("Done");
        }


        public void ExportAsync(object sender, DoWorkEventArgs args)
        {
             var exportArguments = (ExportArguments)args.Argument;

            var dictionary = new SfmDictionary();
            var solidSettings = SolidSettings.OpenSolidFile(SolidSettings.GetSettingsFilePathFromDictionaryPath(exportArguments.inputFilePath));
            dictionary.Open(exportArguments.inputFilePath, solidSettings, new RecordFilterSet());
            Export(dictionary.AllRecords, solidSettings, exportArguments.outputFilePath, exportArguments.progress);
        }

        public static IExporter Create()
        {
            return new ExportLift();
        }

        public const string DriverName = "Lift";

    }
}
