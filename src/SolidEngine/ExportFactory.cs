using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SolidEngine
{
    public class ExportFactory
    {
        private List<ExportHeader> _exportSettings = new List<ExportHeader>();
        private static ExportFactory _instance = null;
        private string _exportersPath;

        public string Path
        {
            get { return _exportersPath; }
            set { LoadExportSettingsFromPath(value); }
        }

        public List<ExportHeader> ExportSettings
        {
            get { return _exportSettings; }
        }

        public static ExportFactory Singleton()
        {
            if (_instance == null)
            {
                _instance = new ExportFactory();
            }
            return _instance;
        }

        private ExportFactory()
        {
            LoadExportSettingsFromPath(EngineEnvironment.PathOfExporters);
        }

        private void LoadExportSettingsFromPath(string exportersPath)
        {
            _exportersPath = exportersPath;
            _exportSettings.Clear();
            // Iterate over the directory looking for .xml / .exporter files
            string[] files = Directory.GetFiles(_exportersPath, "*.xml");
            foreach (string file in files)
            {
                ExportHeader header = ExportHeader.CreateFromFilePath(file);
                if (header != null)
                {
                    _exportSettings.Add(header);
                }
            }
            // Sort the list based on the Priority
            _exportSettings.Sort(
                delegate(ExportHeader lhs, ExportHeader rhs)
                {
                    if (lhs.Position < rhs.Position)
                        return -1;
                    if (lhs.Position > rhs.Position)
                        return 1;
                    return 0;
                }
            );
        }

        public IExporter CreateFromFileFilter(string fileFilter)
        {
            ExportHeader header = _exportSettings.Find(
                delegate(ExportHeader h)
                {
                    return h.FileNameFilter == fileFilter;
                }
            );
            IExporter retval = null;
            if (header != null)
            {
                retval = CreateFromSettings(header);
            }
            return retval;
        }

        public IExporter CreateFromSettings(ExportHeader header)
        {
            IExporter retval = null;
            switch (header.Driver)
            {
                case "FlatXml":
                    retval = ExportFlatXml.Create();
                    break;
                case "StructuredXml":
                    retval = ExportStructuredXml.Create();
                    break;
                case "Xsl":
                    retval = ExportXsl.Create(header);
                    break;
            }
            return retval;
        }
    }
}
