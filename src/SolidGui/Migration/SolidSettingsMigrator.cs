using Palaso.Migration;
using SolidGui.Engine;

namespace SolidGui.Migration
{
    public class SolidSettingsMigrator : FileMigrator
    {
        public SolidSettingsMigrator(string sourceFilePath) : 
            base(SolidSettings.LatestVersion, sourceFilePath)
        {
            AddVersionStrategy(
                new XPathVersion(SolidSettings.LatestVersion, "/SolidSettings/Version")
                {
                    VersionParser = ParseVersion
                }
            );
            AddMigrationStrategy(new XslFromResourceMigrator(1, 2, "SolidSettings1To2.xslt"));
        }

        private static int ParseVersion(string version)
        {
            double v = double.Parse(version);
            return (int)v;
        }

    }
}
