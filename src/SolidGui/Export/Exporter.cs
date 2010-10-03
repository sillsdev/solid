using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using SolidGui.Engine;
using SolidGui.MarkerSettings;
using SolidGui.Model;

namespace SolidGui.Export
{
    public struct ExportArguments
    {
        public string inputFilePath;
        public string outputFilePath;
        public int countHint;

        public MarkerSettingsPM markerSettings;
    }

    public interface IExporter
    {
        //ExportSetting ExportSettings;
        void Export(IEnumerable<Record> records, SolidSettings solidSettings, string outputFilePath);

        /// <summary>
        /// This runs as a background worker.
        /// </summary>
        void ExportAsync(object sender, DoWorkEventArgs args);

    }
}