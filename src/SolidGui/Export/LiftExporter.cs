using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Palaso.DictionaryServices.Lift;
using Palaso.DictionaryServices.Model;
using SolidGui.Engine;
using SolidGui.Model;

namespace SolidGui.Export
{
    public class LiftExporter : IExporter
    {

        public void Export(IEnumerable<Record> sfmLexEntries, SolidSettings solidSettings, string outputFilePath)
        {
            var index = new Dictionary<string, SfmLiftLexEntryAdapter>();
            var liftLexEntries = new List<SfmLiftLexEntryAdapter>();

            using (var dm = new LiftDataMapper(outputFilePath))
            {
                // first pass
                foreach (var sfmLexEntry in sfmLexEntries)
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

                SfmLiftLexEntryAdapter targetLexEntry;

                // second pass
                foreach (var adaptedEntry in liftLexEntries)
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

                    dm.SaveItem(adaptedEntry.LiftLexEntry);

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

                        dm.SaveItem(subEntry.LiftLexEntry);
                    }

                    // Got any potential relations?
                    // if so
                    // foreach one find your target in the index, get it's id, and make the relation

                    
                }
            }

        }

        public void ExportAsync(object sender, DoWorkEventArgs args)
        {
            throw new System.NotImplementedException();
        }
    }
}
