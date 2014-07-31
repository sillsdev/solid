﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

using Palaso.TestUtilities;
using Palaso.IO;
using SolidGui.Engine;

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
          <MultipleAdjacent>MultipleTogether</MultipleAdjacent>
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
      <StructureProperties>
        <SolidStructureProperty>
          <Parent>entry</Parent>
          <Multiplicity>MultipleTogether</Multiplicity>
        </SolidStructureProperty>
      </StructureProperties>
      <InferedParent />
      <Mappings>
        <string>lex</string>
        <string>lexicalUnit</string>
      </Mappings>
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
                SolidSettings ss = SolidSettings.OpenSolidFile(f.Path);
                Assert.AreEqual(ss.Version, SolidSettings.LatestVersion.ToString(CultureInfo.InvariantCulture));
                string fResult = System.IO.Path.GetTempFileName();
                ss.SaveAs(fResult);

				AssertThatXmlIn.File(fResult).HasAtLeastOneMatchForXpath(
					string.Format("/SolidSettings/Version[text()='{0}']", SolidSettings.LatestVersion)
				);
				AssertThatXmlIn.File(fResult).HasAtLeastOneMatchForXpath("//Multiplicity");
            }
        }

        [Test]
        public void CanReadV1AndSaveAsCurrent()
        {
            MigrateFromTo(_v1Content, _v2Content);
        }


        private void MigrateFromTo(string origData, string newData)
        {
            // load orig, save it to disk, load it again and verify it matches target
            SolidSettings ss;
            using (var f = new TempFile(origData))
            {
                ss = SolidSettings.OpenSolidFile(f.Path);
            }
            SolidSettings ss2;

            var f2Path = System.IO.Path.GetTempFileName();
            ss.SaveAs(f2Path);
            var tmp = SolidSettings.OpenSolidFile(f2Path); //just making sure this doesn't crash
            var s = File.ReadAllText(f2Path);
            Assert.AreEqual(s, newData);
            File.Delete(f2Path);
        }

        // Similar content, in v1 format and missing various bits
        // Omitted: <Version>, <RecordMarker>
        private readonly string _omissionsV1 = @"<?xml version='1.0' encoding='utf-8'?>
<SolidSettings xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema'>
  <MarkerSettings>
    <SolidMarkerSetting>
      <WritingSystem>lbw</WritingSystem>
      <Unicode>true</Unicode>
      <StructureProperties>
        <SolidStructureProperty>
          <Parent>entry</Parent>
          <MultipleAdjacent>MultipleTogether</MultipleAdjacent>
        </SolidStructureProperty>
      </StructureProperties>
      <Marker>lx</Marker>
    </SolidMarkerSetting>
    <SolidMarkerSetting>
      <WritingSystem>lbw</WritingSystem>
      <StructureProperties>
        <SolidStructureProperty>
          <Parent>lx</Parent>
          <MultipleAdjacent>XXMultipleXXTogetherXX</MultipleAdjacent>
        </SolidStructureProperty>
        <SolidStructureProperty>
          <Parent>se</Parent>
        </SolidStructureProperty>
      </StructureProperties>
      <Marker>nt</Marker>
    </SolidMarkerSetting>
  </MarkerSettings>
</SolidSettings>".Replace('\'', '"');

        private readonly string _omissionsCorrected = @"<?xml version='1.0' encoding='utf-8'?>
<SolidSettings xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema'>
  <RecordMarker>lx</RecordMarker>
  <Version>2</Version>
  <MarkerSettings>
    <SolidMarkerSetting>
      <Marker>lx</Marker>
      <WritingSystem>lbw</WritingSystem>
      <Unicode>true</Unicode>
      <StructureProperties>
        <SolidStructureProperty>
          <Parent>entry</Parent>
          <Multiplicity>MultipleTogether</Multiplicity>
        </SolidStructureProperty>
      </StructureProperties>
      <InferedParent />
      <Mappings>
        <string />
        <string />
      </Mappings>
    </SolidMarkerSetting>
    <SolidMarkerSetting>
      <Marker>nt</Marker>
      <WritingSystem>lbw</WritingSystem>
      <Unicode>true</Unicode>
      <StructureProperties>
        <SolidStructureProperty>
          <Parent>lx</Parent>
          <Multiplicity>Once</Multiplicity>
        </SolidStructureProperty>
        <SolidStructureProperty>
          <Parent>se</Parent>
          <Multiplicity>Once</Multiplicity>
        </SolidStructureProperty>
      </StructureProperties>
      <InferedParent />
      <Mappings>
        <string />
        <string />
      </Mappings>
    </SolidMarkerSetting>
  </MarkerSettings>
</SolidSettings>".Replace('\'', '"');

        [Test]
        public void CanHandleOmissionsAndTypos()
        {
            MigrateFromTo(_omissionsV1, _omissionsCorrected);
        }


    }
}
