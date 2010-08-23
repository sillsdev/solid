using System;
using System.IO;
using System.Reflection;

namespace SolidGui.Engine
{
    public class EngineEnvironment
    {
        public static string PathOfBase
        {
            get
            {
                string path;
                bool isUnitTesting = Assembly.GetEntryAssembly() == null;
                if (isUnitTesting)
                {
                    path = new Uri(Assembly.GetExecutingAssembly().CodeBase).AbsolutePath;
                    path = Uri.UnescapeDataString(path);
                }
                else
                {
                    path = Assembly.GetExecutingAssembly().Location;
                }
                path = Directory.GetParent(path).FullName;
                int index;
                if ((index = path.IndexOf("output", StringComparison.OrdinalIgnoreCase)) >= 0)
                {
                    path = path.Substring(0, index - 1);
                }
                else if ((index = path.IndexOf("bin", StringComparison.OrdinalIgnoreCase)) >= 0)
                {
                    path = path.Substring(0, index - 1);
                    path = Directory.GetParent(path).FullName;
                    path = Directory.GetParent(path).FullName;
                }
                return path;
            }
        }

        public static string PathOfExporters
        {
            get
            {
                return Path.Combine(PathOfBase, "exporters");
            }
        }


    }
}