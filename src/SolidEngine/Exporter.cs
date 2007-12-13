using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace SolidEngine
{
    public struct ExportArguments
    {
        public string inputFilePath;
        public string outputFilePath;
        public int countHint;
    }

    public interface IExporter
    {
        //ExportSetting ExportSettings;
        void Export(string inputFilePath, string outputFilePath);

        /// <summary>
        /// This runs as a background worker.
        /// </summary>
        void OnDoWork(object sender, DoWorkEventArgs args);

    }
}
