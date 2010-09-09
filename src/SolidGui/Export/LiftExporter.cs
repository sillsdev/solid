using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Palaso.DictionaryServices.Lift;

namespace SolidGui.Export
{
    public class LiftExporter : IExporter
    {

        // TODO change this to IEnumerable<SfmLexEntry> or similar
        public void Export(string inputFilePath, string outputFilePath)
        {
            var dm = new LiftDataMapper(outputFilePath);
            var entry = new SfmLiftLexEntryAdapter(null);
            dm.SaveItem(entry);
        }

        public void ExportAsync(object sender, DoWorkEventArgs args)
        {
            throw new System.NotImplementedException();
        }
    }
}
