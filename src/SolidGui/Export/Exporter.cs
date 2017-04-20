// Copyright (c) 2007-2014 SIL International
// Licensed under the MIT license: opensource.org/licenses/MIT

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using SIL.Progress;
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
        public IProgress progress;
        public MarkerSettingsPM markerSettings;
    }

    public interface IExporter
    {
        //ExportSetting ExportSettings;
        void Export(IEnumerable<Record> records, SolidSettings solidSettings, string outputFilePath, IProgress progress);

        /// <summary>
        /// This runs as a background worker.
        /// </summary>
        void ExportAsync(object sender, DoWorkEventArgs args);

        string ModifyDestinationIfNeeded(string destinationFilePath);
    }
}