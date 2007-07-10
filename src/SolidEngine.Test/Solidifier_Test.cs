using System;
using System.IO;
using System.Text;
using System.Xml;
using NUnit.Framework;
using SolidEngine;

namespace SolidTests
{
    [TestFixture]
    public class SolidifierTest
    {
        class Observer : Solidifier.Observer
        {
            SolidifierTest _o;
            public Observer(SolidifierTest o)
            {
                _o = o;
            }

            public override void OnRecordProcess(XmlNode structure, SolidReport report)
            {
                _o.onRecord(structure, report);
            }
        }

        private SolidSettings InitSettings()
        {
            SolidSettings solidSettings = new SolidSettings();
            SolidMarkerSetting lxSetting = new SolidMarkerSetting("lx");
            lxSetting.StructureProperties.Add(new SolidStructureProperty("entry", SolidGui.MultiplicityAdjacency.Once));
            SolidMarkerSetting geSetting = new SolidMarkerSetting("ge");
            geSetting.StructureProperties.Add(new SolidStructureProperty("sn", SolidGui.MultiplicityAdjacency.MultipleApart));
            SolidMarkerSetting snSetting = new SolidMarkerSetting("sn");
            snSetting.StructureProperties.Add(new SolidStructureProperty("lx", SolidGui.MultiplicityAdjacency.MultipleApart));

            solidSettings.MarkerSettings.Add(lxSetting);
            solidSettings.MarkerSettings.Add(snSetting);
            solidSettings.MarkerSettings.Add(geSetting);

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
            string sfmIn =
                "\\_sh v3.0  269  MDF 4.0 (alternate hierarchy)\n" +
                "\\_DateStampHasFourDigitYear\n" +
                "\\lx a\n" +
                "\\ge b\n";
//            string xmlEx = "<root><lx><data>a</data><sn inferred=\"true\"><data /><ge><data>g</data></ge></sn></lx></root>";

            SolidSettings settings = InitSettings();
            Solidifier solid = new Solidifier();

            solid.Attach(new Observer(this));
            solid.Process(CreateSfmXmlReader(sfmIn), settings);

//            SolidMarkerSetting setting = _settings.FindMarkerSetting("ge");
//            Assert.IsNotNull(setting);
//            setting.InferedParent = "sn";

//            Assert.IsNotNull(report);

        }

    }
}
