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
            var dm = new LiftDataMapper(outputFilePath);
            foreach (var sfmLexEntry in sfmLexEntries)
            {
                var lexEntry = new SfmLiftLexEntryAdapter(sfmLexEntry.LexEntry, solidSettings);
                var createdItem = dm.CreateItem();
                lexEntry.PopulateEntry(createdItem);

                dm.SaveItem(createdItem);
            }

        }

        public void ExportAsync(object sender, DoWorkEventArgs args)
        {
            throw new System.NotImplementedException();
        }
    }
}
