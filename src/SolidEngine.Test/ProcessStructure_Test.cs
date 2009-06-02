using System;
using System.IO;
using System.Text;
using System.Xml;
using NUnit.Framework;
using SolidEngine;

namespace SolidTests
{
    [TestFixture]
    public class ProcessStructureTest
    {
        ProcessStructure _p;
        SolidSettings _settings;

        [SetUp]
        public void Setup()
        {
            _settings = new SolidSettings();
            SolidMarkerSetting lxSetting = _settings.FindOrCreateMarkerSetting("lx");
            lxSetting.StructureProperties.Add(new SolidStructureProperty("entry", MultiplicityAdjacency.Once));
            SolidMarkerSetting geSetting = _settings.FindOrCreateMarkerSetting("ge");
            geSetting.StructureProperties.Add(new SolidStructureProperty("sn", MultiplicityAdjacency.MultipleApart));
            SolidMarkerSetting snSetting = _settings.FindOrCreateMarkerSetting("sn");
            snSetting.StructureProperties.Add(new SolidStructureProperty("lx", MultiplicityAdjacency.MultipleApart));
            SolidMarkerSetting bbSetting = _settings.FindOrCreateMarkerSetting("bb");
            bbSetting.StructureProperties.Add(new SolidStructureProperty("lx", MultiplicityAdjacency.MultipleApart));

            SolidMarkerSetting rfSetting = _settings.FindOrCreateMarkerSetting("rf");
            rfSetting.StructureProperties.Add(new SolidStructureProperty("sn", MultiplicityAdjacency.MultipleTogether));
            rfSetting.InferedParent = "sn";
            SolidMarkerSetting xeSetting = _settings.FindOrCreateMarkerSetting("xe");
            xeSetting.StructureProperties.Add(new SolidStructureProperty("rf", MultiplicityAdjacency.Once));
            xeSetting.InferedParent = "rf";

            _p = new ProcessStructure(_settings);
        }

        [TearDown]
        public void TearDown()
        {
        }

        [Test]
        public void InfersNodeForEverySeperateChildWhenChildCanAppearUnderParentOnce()
        {
            string xmlIn = "<entry record=\"4\"><lx field=\"1\">test1</lx><cc>fire</cc><sn></sn><cc>foo</cc><sn></sn><cc>bar</cc></entry>";
            string xmlEx = "<entry record=\"4\"><lx field=\"1\"><data>test1</data><bb inferred=\"true\"><data /><cc><data>fire</data></cc></bb><sn><data /></sn><bb inferred=\"true\"><data /><cc><data>foo</data></cc></bb><sn><data /></sn><bb inferred=\"true\"><data /><cc><data>bar</data></cc></bb></lx></entry>";

            SolidMarkerSetting ccSetting = _settings.FindOrCreateMarkerSetting("cc");
            ccSetting.StructureProperties.Add(new SolidStructureProperty("bb", MultiplicityAdjacency.Once));
            
            Assert.IsNotNull(_settings.FindOrCreateMarkerSetting("cc"));
            ccSetting.InferedParent = "bb";

            XmlDocument entry = new XmlDocument();
            entry.LoadXml(xmlIn);
            SolidReport report = new SolidReport();
            XmlNode xmlResult = _p.Process(entry.DocumentElement, report);
            string xmlOut = xmlResult.OuterXml;
            Assert.AreEqual(xmlEx, xmlOut);
        }

        [Test]
        public void InfersNodeForEveryChildTogetherWhenChildCanAppearUnderParentOnce()
        {
            string xmlIn = "<entry record=\"5\"><lx field=\"1\">test2</lx><cc>fire</cc><cc>foo</cc><sn></sn><cc>bar</cc></entry>";
            string xmlEx = "<entry record=\"5\"><lx field=\"1\"><data>test2</data><bb inferred=\"true\"><data /><cc><data>fire</data></cc></bb><bb inferred=\"true\"><data /><cc><data>foo</data></cc></bb><sn><data /></sn><bb inferred=\"true\"><data /><cc><data>bar</data></cc></bb></lx></entry>";

            SolidMarkerSetting ccSetting = _settings.FindOrCreateMarkerSetting("cc");
            ccSetting.StructureProperties.Add(new SolidStructureProperty("bb", MultiplicityAdjacency.Once));

            Assert.IsNotNull(_settings.FindOrCreateMarkerSetting("cc"));
            ccSetting.InferedParent = "bb";

            XmlDocument entry = new XmlDocument();
            entry.LoadXml(xmlIn);
            SolidReport report = new SolidReport();
            XmlNode xmlResult = _p.Process(entry.DocumentElement, report);
            string xmlOut = xmlResult.OuterXml;
            Assert.AreEqual(xmlEx, xmlOut);
        }

        [Test]
        public void InfersNodeForEverySeperateChildWhenChildCanAppearUnderParentMultipleTogether()
        {
            string xmlIn = "<entry record=\"4\"><lx field=\"1\">test1</lx><cc>fire</cc><sn></sn><cc>foo</cc><sn></sn><cc>bar</cc></entry>";
            string xmlEx = "<entry record=\"4\"><lx field=\"1\"><data>test1</data><bb inferred=\"true\"><data /><cc><data>fire</data></cc></bb><sn><data /></sn><bb inferred=\"true\"><data /><cc><data>foo</data></cc></bb><sn><data /></sn><bb inferred=\"true\"><data /><cc><data>bar</data></cc></bb></lx></entry>";

            SolidMarkerSetting ccSetting = _settings.FindOrCreateMarkerSetting("cc");
            ccSetting.StructureProperties.Add(new SolidStructureProperty("bb", MultiplicityAdjacency.MultipleTogether));

            Assert.IsNotNull(_settings.FindOrCreateMarkerSetting("cc"));
            ccSetting.InferedParent = "bb";

            XmlDocument entry = new XmlDocument();
            entry.LoadXml(xmlIn);
            SolidReport report = new SolidReport();
            XmlNode xmlResult = _p.Process(entry.DocumentElement, report);
            string xmlOut = xmlResult.OuterXml;
            Assert.AreEqual(xmlEx, xmlOut);
        }

        [Test]
        public void InfersNodeOnceForEveryChildTogetherWhenChildCanAppearUnderParentMultipleTogether()
        {
            string xmlIn = "<entry record=\"5\"><lx field=\"1\">test2</lx><cc>fire</cc><cc>foo</cc><sn></sn><cc>bar</cc></entry>";
            string xmlEx = "<entry record=\"5\"><lx field=\"1\"><data>test2</data><bb inferred=\"true\"><data /><cc><data>fire</data></cc><cc><data>foo</data></cc></bb><sn><data /></sn><bb inferred=\"true\"><data /><cc><data>bar</data></cc></bb></lx></entry>";

            SolidMarkerSetting ccSetting = _settings.FindOrCreateMarkerSetting("cc");
            ccSetting.StructureProperties.Add(new SolidStructureProperty("bb", MultiplicityAdjacency.MultipleTogether));

            Assert.IsNotNull(_settings.FindOrCreateMarkerSetting("cc"));
            ccSetting.InferedParent = "bb";

            XmlDocument entry = new XmlDocument();
            entry.LoadXml(xmlIn);
            SolidReport report = new SolidReport();
            XmlNode xmlResult = _p.Process(entry.DocumentElement, report);
            string xmlOut = xmlResult.OuterXml;
            Assert.AreEqual(xmlEx, xmlOut);
        }

        [Test]
        public void InfersNodeOnceForAllChildrenSeperatedWhenChildCanAppearUnderParentMultipleApart()
        {
            string xmlIn = "<entry record=\"4\"><lx field=\"1\">test1</lx><cc>fire</cc><sn></sn><cc>foo</cc><sn></sn><cc>bar</cc></entry>";
            string xmlEx = "<entry record=\"4\"><lx field=\"1\"><data>test1</data><bb inferred=\"true\"><data /><cc><data>fire</data></cc><sn><data /></sn><cc><data>foo</data></cc><sn><data /></sn><cc><data>bar</data></cc></bb></lx></entry>";

            SolidMarkerSetting ccSetting = _settings.FindOrCreateMarkerSetting("cc");
            ccSetting.StructureProperties.Add(new SolidStructureProperty("bb", MultiplicityAdjacency.MultipleApart));

            SolidMarkerSetting snSetting = _settings.FindOrCreateMarkerSetting("sn");
            snSetting.StructureProperties.Add(new SolidStructureProperty("bb",MultiplicityAdjacency.MultipleApart));
            Assert.IsNotNull(_settings.FindOrCreateMarkerSetting("cc"));
            ccSetting.InferedParent = "bb";

            XmlDocument entry = new XmlDocument();
            entry.LoadXml(xmlIn);
            SolidReport report = new SolidReport();
            XmlNode xmlResult = _p.Process(entry.DocumentElement, report);
            string xmlOut = xmlResult.OuterXml;
            Assert.AreEqual(xmlEx, xmlOut);
        }

        [Test]
        public void InfersNodeOnceForAllChildrenTogetherAndSeperatedWhenChildCanAppearUnderParentMultipleApart()
        {
            string xmlIn = "<entry record=\"5\"><lx field=\"1\">test2</lx><cc>fire</cc><cc>foo</cc><sn></sn><cc>bar</cc></entry>";
            string xmlEx = "<entry record=\"5\"><lx field=\"1\"><data>test2</data><bb inferred=\"true\"><data /><cc><data>fire</data></cc><cc><data>foo</data></cc><sn><data /></sn><cc><data>bar</data></cc></bb></lx></entry>";

            SolidMarkerSetting ccSetting = _settings.FindOrCreateMarkerSetting("cc");
            ccSetting.StructureProperties.Add(new SolidStructureProperty("bb", MultiplicityAdjacency.MultipleApart));

            SolidMarkerSetting snSetting = _settings.FindOrCreateMarkerSetting("sn");
            snSetting.StructureProperties.Add(new SolidStructureProperty("bb", MultiplicityAdjacency.MultipleApart));
            
            Assert.IsNotNull(_settings.FindOrCreateMarkerSetting("cc"));
            ccSetting.InferedParent = "bb";

            XmlDocument entry = new XmlDocument();
            entry.LoadXml(xmlIn);
            SolidReport report = new SolidReport();
            XmlNode xmlResult = _p.Process(entry.DocumentElement, report);
            string xmlOut = xmlResult.OuterXml;
            Assert.AreEqual(xmlEx, xmlOut);
        }

        [Test]
        public void ProcessStructure_InferNode_Correct()
        {
            string xmlIn = "<entry record=\"4\"><lx field=\"1\">a</lx><ge>g</ge></entry>";
            string xmlEx = "<entry record=\"4\"><lx field=\"1\"><data>a</data><sn inferred=\"true\"><data /><ge><data>g</data></ge></sn></lx></entry>";

            SolidMarkerSetting setting =  _settings.FindOrCreateMarkerSetting("ge");
            Assert.IsNotNull(setting);
            setting.InferedParent = "sn";

            XmlDocument entry = new XmlDocument();
            entry.LoadXml(xmlIn);
            SolidReport report = new SolidReport();
            XmlNode xmlResult = _p.Process(entry.DocumentElement, report);
            string xmlOut = xmlResult.OuterXml;
            Assert.AreEqual(xmlEx, xmlOut);
            
        }

        [Test]
        public void ProcessStructure_NoInferReqd_Correct()
        {
            string xmlIn = "<entry record=\"4\"><lx field=\"1\">a</lx><sn></sn><ge>g</ge></entry>";
            string xmlEx = "<entry record=\"4\"><lx field=\"1\"><data>a</data><sn><data /><ge><data>g</data></ge></sn></lx></entry>";

            SolidMarkerSetting setting = _settings.FindOrCreateMarkerSetting("ge");
            Assert.IsNotNull(setting);
            setting.InferedParent = "";

            XmlDocument entry = new XmlDocument();
            entry.LoadXml(xmlIn);
            SolidReport report = new SolidReport();
            XmlNode xmlResult = _p.Process(entry.DocumentElement, report);
            string xmlOut = xmlResult.OuterXml;
            Assert.AreEqual(xmlEx, xmlOut);

        }

        [Test]
        public void ProcessStructure_RecursiveInfer_Correct()
        {
            string xmlIn = "<entry record=\"4\"><lx field=\"1\">a</lx><xe field=\"2\">b</xe></entry>";
            string xmlEx = "<entry record=\"4\"><lx field=\"1\"><data>a</data><sn inferred=\"true\"><data /><rf inferred=\"true\"><data /><xe field=\"2\"><data>b</data></xe></rf></sn></lx></entry>";

            SolidMarkerSetting setting = _settings.FindOrCreateMarkerSetting("ge");
            Assert.IsNotNull(setting);
            setting.InferedParent = "";

            XmlDocument entry = new XmlDocument();
            entry.LoadXml(xmlIn);
            SolidReport report = new SolidReport();
            XmlNode xmlResult = _p.Process(entry.DocumentElement, report);
            string xmlOut = xmlResult.OuterXml;
            Assert.AreEqual(xmlEx, xmlOut);
            Assert.AreEqual(0, report.Count);

        }

        [Test]
        public void ProcessStructure_RecursiveInferIssue144_MarkersNotDuplicated()
        {
            string xmlIn = "<entry record=\"4\"><lx field=\"1\">a</lx><xe field=\"2\">b</xe></entry>";
//            string xmlEx = "<entry record=\"4\"><lx field=\"1\"><data>a</data><ps inferred=\"true\"><data /><sn inferred=\"true\"><data /><rf inferred=\"true\"><data /><xe field=\"2\"><data>b</data></xe></rf></sn></ps></lx></entry>";
            string xmlEx = "<entry record=\"4\"><lx field=\"1\"><data>a</data><sn inferred=\"true\"><data /><rf inferred=\"true\"><data /><xe field=\"2\"><data>b</data></xe></rf></sn></lx></entry>";

            _settings = new SolidSettings();
            SolidMarkerSetting lxSetting = _settings.FindOrCreateMarkerSetting("lx");
            lxSetting.StructureProperties.Add(new SolidStructureProperty("entry", MultiplicityAdjacency.Once));

            SolidMarkerSetting psSetting = _settings.FindOrCreateMarkerSetting("ps");
            psSetting.StructureProperties.Add(new SolidStructureProperty("lx", MultiplicityAdjacency.MultipleApart));
            
            SolidMarkerSetting snSetting = _settings.FindOrCreateMarkerSetting("sn");
            snSetting.StructureProperties.Add(new SolidStructureProperty("ps", MultiplicityAdjacency.MultipleApart));
            snSetting.InferedParent = "";
            
            SolidMarkerSetting rfSetting = _settings.FindOrCreateMarkerSetting("rf");
            rfSetting.StructureProperties.Add(new SolidStructureProperty("sn", MultiplicityAdjacency.MultipleTogether));
            rfSetting.InferedParent = "sn";
            
            SolidMarkerSetting xeSetting = _settings.FindOrCreateMarkerSetting("xe");
            xeSetting.StructureProperties.Add(new SolidStructureProperty("rf", MultiplicityAdjacency.Once));
            xeSetting.InferedParent = "rf";

            _p = new ProcessStructure(_settings);
            
            XmlDocument entry = new XmlDocument();
            entry.LoadXml(xmlIn);
            SolidReport report = new SolidReport();
            XmlNode xmlResult = _p.Process(entry.DocumentElement, report);
            string xmlOut = xmlResult.OuterXml;
            Assert.AreEqual(xmlEx, xmlOut);
            Assert.AreEqual(5, report.Count);
        }

        [Test]
        public void ProcessStructure_ErrorRecordID145_RecordIDValid()
        {
            string xmlIn = "<entry record=\"4\"><lx field=\"1\">a</lx><xe field=\"2\">b</xe></entry>";
            //            string xmlEx = "<entry record=\"4\"><lx field=\"1\"><data>a</data><ps inferred=\"true\"><data /><sn inferred=\"true\"><data /><rf inferred=\"true\"><data /><xe field=\"2\"><data>b</data></xe></rf></sn></ps></lx></entry>";
            string xmlEx = "<entry record=\"4\"><lx field=\"1\"><data>a</data><xe field=\"2\"><data>b</data></xe></lx></entry>";

            _settings = new SolidSettings();
            SolidMarkerSetting lxSetting = _settings.FindOrCreateMarkerSetting("lx");
            lxSetting.StructureProperties.Add(new SolidStructureProperty("entry", MultiplicityAdjacency.Once));

            SolidMarkerSetting xeSetting = _settings.FindOrCreateMarkerSetting("xe");
            xeSetting.StructureProperties.Add(new SolidStructureProperty("rf", MultiplicityAdjacency.Once));
            xeSetting.InferedParent = "";

            _p = new ProcessStructure(_settings);

            XmlDocument entry = new XmlDocument();
            entry.LoadXml(xmlIn);
            SolidReport report = new SolidReport();
            XmlNode xmlResult = _p.Process(entry.DocumentElement, report);
            string xmlOut = xmlResult.OuterXml;
            Assert.AreEqual(xmlEx, xmlOut);
            Assert.AreEqual(1, report.Count);
            Assert.AreEqual(4, report.Entries[0].RecordID);

        }

        [Test]
        public void ProcessStructure_NoInferInsertAnyway_Correct()
        {
            string xmlIn = "<entry record=\"4\"><lx field=\"1\">a</lx><sn field=\"2\"></sn><ge field=\"3\">g</ge><zz field=\"4\">z</zz></entry>";
            string xmlEx = "<entry record=\"4\"><lx field=\"1\"><data>a</data><sn field=\"2\"><data /><ge field=\"3\"><data>g</data><zz field=\"4\"><data>z</data></zz></ge></sn></lx></entry>";

            SolidMarkerSetting setting = _settings.FindOrCreateMarkerSetting("ge");
            Assert.IsNotNull(setting);
            setting.InferedParent = "";

            XmlDocument entry = new XmlDocument();
            entry.LoadXml(xmlIn);
            SolidReport report = new SolidReport();
            XmlNode xmlResult = _p.Process(entry.DocumentElement, report);
            string xmlOut = xmlResult.OuterXml;
            Assert.AreEqual(xmlEx, xmlOut);

        }

        [Test]
        public void ProcessStructure_LiftMapping_Correct()
        {
            string xmlIn = "<entry record=\"4\"><lx field=\"1\">b</lx></entry>";
            string xmlEx = "<entry record=\"4\"><lx field=\"1\" lift=\"a\" writingsystem=\"zxx\"><data>b</data></lx></entry>";

            SolidMarkerSetting setting = _settings.FindOrCreateMarkerSetting("lx");
            Assert.IsNotNull(setting);
            setting.Mapping[(int)SolidMarkerSetting.MappingType.Lift] = "a";

            XmlDocument entry = new XmlDocument();
            entry.LoadXml(xmlIn);
            SolidReport report = new SolidReport();
            XmlNode xmlResult = _p.Process(entry.DocumentElement, report);
            string xmlOut = xmlResult.OuterXml;
            Assert.AreEqual(xmlEx, xmlOut);
        }

//        [Test]
//        public void ProcessStructure_FlexMapping_Correct()
//        {
//            string xmlIn = "<entry record=\"5\"><lx field=\"1\">b</lx></entry>";
//            string xmlEx = "<entry record=\"5\"><lx field=\"1\" flex=\"a\" writingsystem=\"zxx\"><data>b</data></lx></entry>";
//
//            SolidMarkerSetting setting = _settings.FindOrCreateMarkerSetting("lx");
//            Assert.IsNotNull(setting);
//            setting.Mapping[(int)SolidMarkerSetting.MappingType.Flex] = "a";
//
//            XmlDocument entry = new XmlDocument();
//            entry.LoadXml(xmlIn);
//            SolidReport report = new SolidReport();
//            XmlNode xmlResult = _p.Process(entry.DocumentElement, report);
//            string xmlOut = xmlResult.OuterXml;
//            Assert.AreEqual(xmlEx, xmlOut);
//
//        }

        [Test]
        public void MultpleErrorMarkers_AreSiblings()
        {
            string xmlIn = "<entry record=\"4\"><lx field=\"1\">a</lx><xx field=\"2\">xx</xx><yy field=\"3\">yy</yy><zz field=\"4\">zz</zz></entry>";
            string xmlEx = "<entry record=\"4\"><lx field=\"1\"><data>a</data><xx field=\"2\"><data>xx</data></xx><yy field=\"3\"><data>yy</data></yy><zz field=\"4\"><data>zz</data></zz></lx></entry>";

            XmlDocument entry = new XmlDocument();
            entry.LoadXml(xmlIn);
            SolidReport report = new SolidReport();
            XmlNode xmlResult = _p.Process(entry.DocumentElement, report);
            string xmlOut = xmlResult.OuterXml;
            Assert.AreEqual(xmlEx, xmlOut);
        }

    }
}
