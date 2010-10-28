using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using Palaso.DictionaryServices.Lift;
using Palaso.DictionaryServices.Model;
using Palaso.Progress;
using SolidGui.Engine;
using SolidGui.Model;
using System.Linq;

namespace SolidGui.Export
{
    public class ExportLift : IExporter
    {

        public void Export(IEnumerable<Record> sfmLexEntries, SolidSettings solidSettings, string outputFilePath)
        {
            var index = new Dictionary<string, SfmLiftLexEntryAdapter>();
            var liftLexEntries = new List<SfmLiftLexEntryAdapter>();
            var logPath = outputFilePath+ ".exportErrors.txt";
            if(File.Exists(logPath))
            {
                File.Delete(logPath);
            }
            using (var dm = new LiftDataMapper(outputFilePath))
            {
                // first pass
                foreach (var sfmLexEntry in sfmLexEntries)
                {
                    try
                    {
                        var adaptedEntry = new SfmLiftLexEntryAdapter(dm, sfmLexEntry.LexEntry, solidSettings);
                        liftLexEntries.Add(adaptedEntry);
                        adaptedEntry.PopulateEntry();

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
                        using (var log = System.IO.File.AppendText(logPath))
                        {
                            log.Write(sfmLexEntry.LexEntry.Name+": ");
                            log.WriteLine(error);
                        }
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
                        using (var log = System.IO.File.AppendText(logPath))
                        {
                            log.Write(adaptedEntry.SfmID + ": ");
                            log.WriteLine(error);
                        }
                    }
                    
                }
                dm.SaveItems(from x in dm.GetAllItems() select dm.GetItem(x));
            }

        }

        public void ExportAsync(object sender, DoWorkEventArgs args)
        {
            var progress = (ProgressState)args.Argument;
            var exportArguments = (ExportArguments)progress.Arguments;

            var dictionary = new SfmDictionary();
            var solidSettings = SolidSettings.OpenSolidFile(SolidSettings.GetSettingsFilePathFromDictionaryPath(exportArguments.inputFilePath));
            dictionary.Open(exportArguments.inputFilePath, solidSettings, new RecordFilterSet());
            Export(dictionary.AllRecords, solidSettings, exportArguments.outputFilePath);
        }

        public static IExporter Create()
        {
            return new ExportLift();
        }

        public const string DriverName = "Lift";

    }
}
