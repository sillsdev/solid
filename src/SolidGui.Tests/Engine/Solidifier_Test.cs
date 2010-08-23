using System.IO;
using System.Xml;
using NUnit.Framework;
using SolidGui.Engine;


namespace SolidGui.Tests.Engine
{
    [TestFixture]
    public class SolidifierTest
    {
        class Observer : Solidifier.Observer
        {
            readonly SolidifierTest _o;
            public Observer(SolidifierTest o)
            {
                _o = o;
            }

            public override void OnRecordProcess(XmlNode structure, SolidReport report)
            {
                _o.onRecord(structure, report);
            }
        }

        // TODO Change tests to using (var = Environment) { ... style
        private SolidSettings InitSettings()
        {
            var solidSettings = new SolidSettings();
            var lxSetting = solidSettings.FindOrCreateMarkerSetting("lx");
            lxSetting.StructureProperties.Add(new SolidStructureProperty("entry", MultiplicityAdjacency.Once));
            var geSetting = solidSettings.FindOrCreateMarkerSetting("ge");
            geSetting.StructureProperties.Add(new SolidStructureProperty("sn", MultiplicityAdjacency.MultipleApart));
            var snSetting = solidSettings.FindOrCreateMarkerSetting("sn");
            snSetting.StructureProperties.Add(new SolidStructureProperty("lx", MultiplicityAdjacency.MultipleApart));

            return solidSettings;
        }

        public void onRecord(XmlNode structure, SolidReport report)
        {
            Assert.IsNotNull(structure);
        }

        private SfmXmlReader CreateSfmXmlReader(string sfm)
        {
            return new SfmXmlReader(new StringReader(sfm));
        }

        [Test]
        public void Solidifier_InferNode_Correct()
        {
            const string sfmIn = "\\_sh v3.0  269  MDF 4.0 (alternate hierarchy)\n" +
                                 "\\_DateStampHasFourDigitYear\n" +
                                 "\\lx a\n" +
                                 "\\ge b\n";
//            string xmlEx = "<root><lx><data>a</data><sn inferred=\"true\"><data /><ge><data>g</data></ge></sn></lx></root>";

            var settings = InitSettings();
            var solid = new Solidifier();

            solid.Attach(new Observer(this));
            solid.Process(CreateSfmXmlReader(sfmIn), settings);

//            SolidMarkerSetting setting = _settings.FindOrCreateMarkerSetting("ge");
//            Assert.IsNotNull(setting);
//            setting.InferedParent = "sn";

//            Assert.IsNotNull(report);

        }

    }
}