using NUnit.Framework;
using Palaso.IO;
using Palaso.TestUtilities;
using SolidGui.Engine;
using SolidGui.Migration;


namespace SolidGui.Tests.Engine
{
    [TestFixture]
    public class SolidSettingsTest
    {

        [Test]
        public void SolidSettings_FindOrCreateMarkerSetting_CreatesMarker()
        {
            var f = new SolidSettings();
            f.FindOrCreateMarkerSetting("mk");

            Assert.IsTrue(f.HasMarker("mk"));
        }

        [Test]
        public void SolidSettings_DetectsDefaultEncoding() // lx rules. Added by JMC 2013-09
        {
            var f = new SolidSettings();
            var s = f.FindOrCreateMarkerSetting("lx");
            s.Unicode = true;
            var s2 = f.FindOrCreateMarkerSetting("gn");
            s2.Unicode = false;
            var s3 = f.FindOrCreateMarkerSetting("dn");
            s3.Unicode = false;
            Assert.IsTrue(f.RecordMarker == "lx");
            Assert.IsTrue(SolidSettings.DetermineDefaultEncoding(f));
        }

        [Test]
        public void SolidSettings_DetectsDefaultEncoding2() // if no lx, majority rules. Added by JMC 2013-09
        {
            var f = new SolidSettings();
            var s = f.FindOrCreateMarkerSetting("lex");
            s.Unicode = true;
            var s2 = f.FindOrCreateMarkerSetting("gn");
            s2.Unicode = false;
            var s3 = f.FindOrCreateMarkerSetting("dn");
            s3.Unicode = false;
            Assert.IsFalse(f.RecordMarker == "lex");
            Assert.IsFalse(SolidSettings.DetermineDefaultEncoding(f));
        }
        [Test]
        public void SolidSettings_WriteRead_HasMarker()
        {
            var f = new SolidSettings();
            f.FilePath = "myfile.solid";
            f.FindOrCreateMarkerSetting("mk");
            f.Save();

            f = SolidSettings.OpenSolidFile("myfile.solid");

            Assert.IsTrue(f.HasMarker("mk"));
        }

        [Test]
        public void SolidSettings_SettingsFilePath_Correct()
        {
            string result = SolidSettings.GetSettingsFilePathFromDictionaryPath("mydatafile.txt");
            Assert.AreEqual("mydatafile.solid", result);
        }

        [Test]
        // http://projects.palaso.org/issues/show/404
        public void SolidSettings_SettingsFilePathWithNoExtension_Correct()
        {
            string result = SolidSettings.GetSettingsFilePathFromDictionaryPath("mydatafile");
            Assert.AreEqual("mydatafile.solid", result);
        }

		[Test]
		public void SaveAs_CurrentModel_WritesLatestVersion()
		{
			using (var f = new TempFile())
			{
				var settings = new SolidSettings();
				settings.SaveAs(f.Path);
				AssertThatXmlIn.File(f.Path).HasAtLeastOneMatchForXpath(
					string.Format("/SolidSettings/Version[text()='{0}']", SolidSettings.LatestVersion)
				);
			}
		}

		[Test]
		// http://projects.palaso.org/issues/612
		public void OpenSolidFile_FileVersion1_MigratesToLatest()
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
				var settings = SolidSettings.OpenSolidFile(f.Path);

				Assert.That(settings.Version, Is.EqualTo("2"));
				var markerSettings = settings.FindOrCreateMarkerSetting("lx");
				Assert.That(markerSettings.StructureProperties[0].Multiplicity, Is.EqualTo(MultiplicityAdjacency.Once));

				AssertThatXmlIn.File(f.Path).HasAtLeastOneMatchForXpath(
					string.Format("/SolidSettings/Version[text()='{0}']", SolidSettings.LatestVersion)
				);
				AssertThatXmlIn.File(f.Path).HasAtLeastOneMatchForXpath("//Multiplicity[text()='Once']");
			}
		}

    }
}