using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Palaso.TestUtilities;
using Palaso.IO;
using SolidGui.Engine;
using SolidGui.Migration;

using NUnit.Framework;

namespace SolidGui.Tests.Migration
{
    [TestFixture]
    public class SolidSettingsMigratorTests
    {
        [Test]
        public void Migrate_FromVersion1_ChangesFileToLatest()
        {
            string oldContent = @"<?xml version='1.0' encoding='utf-8'?>
<SolidSettings xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema'>
  <RecordMarker>lx</RecordMarker>
  <Version>1.0</Version>
  <MarkerSettings>
    <SolidMarkerSetting>
      <WritingSystem>lbw</WritingSystem>
      <Unicode>true</Unicode>
      <InferedParent />
      <Mappings>
        <string>lex</string>
        <string>lexicalUnit</string>
      </Mappings>
      <StructureProperties>
        <SolidStructureProperty>
          <Parent>entry</Parent>
          <MultipleAdjacent>Once</MultipleAdjacent>
        </SolidStructureProperty>
      </StructureProperties>
      <Marker>lx</Marker>
    </SolidMarkerSetting>
  </MarkerSettings>
</SolidSettings>".Replace('\'', '"');
            using (var f = new TempFile(oldContent))
            {
                var migrator = new SolidSettingsMigrator(f.Path);
                migrator.Migrate();
				AssertThatXmlIn.File(f.Path).HasAtLeastOneMatchForXpath(
					string.Format("/SolidSettings/Version[text()='{0}']", SolidSettings.LatestVersion)
				);
				AssertThatXmlIn.File(f.Path).HasAtLeastOneMatchForXpath("//Multiplicity[text()='Once']");
            }
        }
    }
}
