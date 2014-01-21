// Copyright (c) 2007-2014 SIL International
// Licensed under the MIT license: opensource.org/licenses/MIT

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SolidGui.Engine;

namespace SolidGui.Export
{
    public class ExportFactory
    {
        private List<ExportHeader> _exportSettings = new List<ExportHeader>();
        private static ExportFactory _instance = null;

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
            _exportSettings.Add(ExportLift.GetHeader());
            _exportSettings.Add(ExportSummary.GetHeader());
        }

        public IExporter CreateFromSettings(ExportHeader header)
        {
            IExporter retval = null;
            switch (header.Driver)
            {
                case ExportLift.DriverName:
                    retval = ExportLift.Create();
                    break;
                case ExportSummary.DriverName:
                    retval = ExportSummary.Create();
                    break;
            }
            return retval;
        }
    }
}