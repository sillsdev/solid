using System;
using System.Collections.Generic;
using System.Text;

namespace SolidEngine
{
    public interface IExporter
    {
        //ExportSetting ExportSettings;
        void Export(string srcFile, string desFile);
    }
}
