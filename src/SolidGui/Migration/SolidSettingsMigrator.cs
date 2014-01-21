// Copyright (c) 2007-2014 SIL International
// Licensed under the MIT license: opensource.org/licenses/MIT

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
            //JMC: Presumably the following is all we'll need for the next migration? (plus the xslt file, of course, and changing SolidSettings.LatestVersion = 3)
            //AddMigrationStrategy(new XslFromResourceMigrator(2, 3, "SolidSettings2To3.xslt")); 
        }

        private static int ParseVersion(string version)
        {
            double v = double.Parse(version);
            return (int)v;
        }

    }
}
