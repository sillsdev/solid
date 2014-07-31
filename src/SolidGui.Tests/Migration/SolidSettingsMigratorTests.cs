using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
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

        private readonly string _v1Content = @"<?xml version='1.0' encoding='utf-8'?>
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

        // Same content, in v2 format
        private readonly string _v2Content = @"<?xml version='1.0' encoding='utf-8'?>
<SolidSettings xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema'>
  <RecordMarker>lx</RecordMarker>
  <Version>2</Version>
  <MarkerSettings>
    <SolidMarkerSetting>
      <Marker>lx</Marker>
      <WritingSystem>lbw</WritingSystem>
      <Unicode>true</Unicode>
      <Mappings>
        <string>lex</string>
        <string>lexicalUnit</string>
      </Mappings>
      <InferedParent />
      <StructureProperties>
        <SolidStructureProperty>
          <Parent>entry</Parent>
          <Multiplicity>Once</Multiplicity>
        </SolidStructureProperty>
      </StructureProperties>
    </SolidMarkerSetting>
  </MarkerSettings>
</SolidSettings>".Replace('\'', '"');

        // Differences from v1 to v2:
        //   <Version>1.0</Version> -> <Version>2</Version>  // did 1.0 really include a Version element at all? -JMC
        //   all MultipleAdjacent -> Multiplicity

        [Test]
        public void Migrate_FromVersion1_ChangesFileToLatest()
        {

            using (var f = new TempFile(_v1Content))
            {
                var migrator = new SolidSettingsMigrator(f.Path);
                migrator.Migrate();
				AssertThatXmlIn.File(f.Path).HasAtLeastOneMatchForXpath(
					string.Format("/SolidSettings/Version[text()='{0}']", SolidSettings.LatestVersion)
				);
				AssertThatXmlIn.File(f.Path).HasAtLeastOneMatchForXpath("//Multiplicity[text()='Once']");
            }
        }

        [Test]
        public void CanReadV1AndSaveAsCurrent()
        {
            // We no longer need to migrate the file before reading it.
            string vCurrentContent = _v2Content;
            // load v1, save it to disk, load it again and verify it matches current
            SolidSettings ss;
            using (var f = new TempFile(_v1Content))
            {
                ss = SolidSettings.OpenSolidFile(f.Path);
            }
            SolidSettings ss2;

            var f2Path = System.IO.Path.GetTempFileName();
            ss.SaveAs(f2Path);
            var tmp = SolidSettings.OpenSolidFile(f2Path); //just making sure this doesn't crash
            //using (var f2 = File.OpenRead(f2Path))
            var s = File.ReadAllText(f2Path);
            Assert.AreEqual(s, vCurrentContent);
            File.Delete(f2Path);

        }

    }
}
