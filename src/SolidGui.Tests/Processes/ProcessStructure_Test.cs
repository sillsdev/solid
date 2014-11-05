using System;
using System.Collections.Generic;
using NUnit.Framework;
using SolidGui.Engine;
using SolidGui.Model;
using SolidGui.Processes;

namespace SolidGui.Tests.Processes
{
    //TODO: need a test for detection of missing required field.

    [TestFixture]
    public class ProcessStructureTest
    {
        private class EnvironmentForTest : IDisposable
        {
            private readonly SolidSettings _settings;

            public void Dispose()
            {
            }

            public EnvironmentForTest()
            {
                _settings = new SolidSettings();
                var lxSetting = _settings.FindOrCreateMarkerSetting("lx");
                lxSetting.StructureProperties.Add(new SolidStructureProperty("entry", MultiplicityAdjacency.Once));
                var geSetting = _settings.FindOrCreateMarkerSetting("ge");
                geSetting.StructureProperties.Add(new SolidStructureProperty("sn", MultiplicityAdjacency.MultipleApart));
                var snSetting = _settings.FindOrCreateMarkerSetting("sn");
                snSetting.StructureProperties.Add(new SolidStructureProperty("lx", MultiplicityAdjacency.MultipleApart));
                var bbSetting = _settings.FindOrCreateMarkerSetting("bb");
                bbSetting.StructureProperties.Add(new SolidStructureProperty("lx", MultiplicityAdjacency.MultipleApart));

                SetMarkerSettings(_settings, "rf", "sn", "sn", MultiplicityAdjacency.MultipleTogether);
                var xeSetting = _settings.FindOrCreateMarkerSetting("xe");
                xeSetting.StructureProperties.Add(new SolidStructureProperty("rf", MultiplicityAdjacency.Once));
                xeSetting.InferedParent = "rf";
            }

            

            public SolidSettings SettingsForTest
            {
                get { return _settings; }
            }

            public SolidReport CreateReportForTest()
            {
                return new SolidReport();
            }
        }

        private static string ParentMarkerForField(SfmFieldModel field)
        {
            if (field.Parent == null)
            {
                return null;
            }
            return field.Parent.Marker;
        }

        private static string ChildMarkerForField(SfmFieldModel field, int n)
        {
            var list = new List<SfmFieldModel>(field.Children);
            return list[n].Marker;
        }

        [Test]
        public void Multiplicity_WhenChildCanAppearUnderParentOnce_InfersNodeForEverySeperateChild()
        {
            const string sfmIn = @"
\lx test1
\cc fire
\sn
\cc foo";

            //expecting:

            /*
             * \lx test1
             *    \bb
             *      \cc fire
             *    \sn
             *    \bb
             *      \cc foo
             *
             */

            //const string xmlEx = "<entry record=\"4\"><lx field=\"1\"><data>test1</data><bb inferred=\"true\"><data /><cc><data>fire</data></cc></bb><sn><data /></sn><bb inferred=\"true\"><data /><cc><data>foo</data></cc></bb><sn><data /></sn><bb inferred=\"true\"><data /><cc><data>bar</data></cc></bb></lx></entry>";


            using (var environment = new EnvironmentForTest())
            {
                var settings = environment.SettingsForTest;
                
                var ccSetting = settings.FindOrCreateMarkerSetting("cc");
                ccSetting.StructureProperties.Add(new SolidStructureProperty("bb", MultiplicityAdjacency.Once));

                Assert.IsNotNull(settings.FindOrCreateMarkerSetting("cc"));
                ccSetting.InferedParent = "bb";

                SfmLexEntry entry = SfmLexEntry.CreateFromText(sfmIn);

                var process = new ProcessStructure(settings);
                var report = environment.CreateReportForTest();
                var outputEntry = process.Process(entry, report);

                Assert.AreEqual(null, outputEntry[0].Parent);
                Assert.AreEqual("lx", ParentMarkerForField(outputEntry[1]));
                Assert.AreEqual("bb", ParentMarkerForField(outputEntry[2]));
                Assert.AreEqual("lx", ParentMarkerForField(outputEntry[3]));
                Assert.AreEqual("lx", ParentMarkerForField(outputEntry[4]));
                Assert.AreEqual("bb", ParentMarkerForField(outputEntry[5]));

                Assert.AreEqual("bb", ChildMarkerForField(entry[0], 0));
                Assert.AreEqual("sn", ChildMarkerForField(entry[0], 1));
                Assert.AreEqual("bb", ChildMarkerForField(entry[0], 2));
            }
        }

        [Test]
        public void MultiplicityOnce_WhenChildCanAppearUnderParentOnce_InfersNodeForEachChildTogether()
        {
            //const string xmlIn = @"<entry record=\"5\"><lx field=\"1\">test2</lx><cc>fire</cc><cc>foo</cc><sn></sn><cc>bar</cc></entry>";

            const string sfmIn = @"
\lx test2
\cc fire
\cc foo
\sn
\cc bar";

            //expecting:

            /*
             * \lx test2
             *    \bb
             *      \cc fire
             *    \bb
             *      \cc foo
             *    \sn
             *    \bb
             *      \cc bar
             * 
             */

            using (var environment = new EnvironmentForTest())
            {
                var settings = environment.SettingsForTest;
                //const string xmlEx = "<entry record=\"5\"><lx field=\"1\"><data>test2</data><bb inferred=\"true\"><data /><cc><data>fire</data></cc></bb><bb inferred=\"true\"><data /><cc><data>foo</data></cc></bb><sn><data /></sn><bb inferred=\"true\"><data /><cc><data>bar</data></cc></bb></lx></entry>";

                var ccSetting = settings.FindOrCreateMarkerSetting("cc");
                ccSetting.StructureProperties.Add(new SolidStructureProperty("bb", MultiplicityAdjacency.Once));

                Assert.IsNotNull(settings.FindOrCreateMarkerSetting("cc"));
                ccSetting.InferedParent = "bb";

                SfmLexEntry entry = SfmLexEntry.CreateFromText(sfmIn);

                var process = new ProcessStructure(settings);
                var report = environment.CreateReportForTest();
                var outputEntry = process.Process(entry, report);

                Assert.AreEqual(null, outputEntry[0].Parent);
                Assert.AreEqual("lx", ParentMarkerForField(outputEntry[1]));
                Assert.AreEqual("bb", ParentMarkerForField(outputEntry[2]));
                Assert.AreEqual("lx", ParentMarkerForField(outputEntry[3]));
                Assert.AreEqual("bb", ParentMarkerForField(outputEntry[4]));
                Assert.AreEqual("lx", ParentMarkerForField(outputEntry[5]));
                Assert.AreEqual("lx", ParentMarkerForField(outputEntry[6]));
                Assert.AreEqual("bb", ParentMarkerForField(outputEntry[7]));

                Assert.AreEqual("bb", ChildMarkerForField(entry[0], 0));
                Assert.AreEqual("bb", ChildMarkerForField(entry[0], 1));
                Assert.AreEqual("sn", ChildMarkerForField(entry[0], 2));
                Assert.AreEqual("bb", ChildMarkerForField(entry[0], 3));


            }
        }

        [Test]
        public void MultiplicityTogether_WhenChildCanAppearUnderParentMultipleTogether_InfersNodeForEverySeperateChild()
        {
            //const string xmlIn = "<entry record=\"4\"><lx field=\"1\">test1</lx><cc>fire</cc><sn></sn><cc>foo</cc><sn></sn><cc>bar</cc></entry>";

            const string sfmIn = @"
\lx test1
\cc fire
\sn
\cc foo
\sn
\cc bar";

            //expecting:

            /*
             * \lx test1
             *    \bb
             *      \cc fire
             *    \sn
             *    \bb
             *      \cc foo
             *    \sn
             *    \bb
             *      \cc bar
             */
            
            //const string xmlEx = "<entry record=\"4\"><lx field=\"1\"><data>test1</data><bb inferred=\"true\"><data /><cc><data>fire</data></cc></bb><sn><data /></sn><bb inferred=\"true\"><data /><cc><data>foo</data></cc></bb><sn><data /></sn><bb inferred=\"true\"><data /><cc><data>bar</data></cc></bb></lx></entry>";
            using (var environment = new EnvironmentForTest())
            {
                var settings = environment.SettingsForTest;

                var ccSetting = settings.FindOrCreateMarkerSetting("cc");
                ccSetting.StructureProperties.Add(new SolidStructureProperty("bb", MultiplicityAdjacency.MultipleTogether));

                Assert.IsNotNull(settings.FindOrCreateMarkerSetting("cc"));
                ccSetting.InferedParent = "bb";

                SfmLexEntry entry = SfmLexEntry.CreateFromText(sfmIn);
                
                var process = new ProcessStructure(settings);
                var report = environment.CreateReportForTest();
                var outputEntry = process.Process(entry, report);

                Assert.AreEqual(null, outputEntry[0].Parent);
                Assert.AreEqual("lx", ParentMarkerForField(outputEntry[1]));
                Assert.AreEqual("bb", ParentMarkerForField(outputEntry[2]));
                Assert.AreEqual("lx", ParentMarkerForField(outputEntry[3]));
                Assert.AreEqual("lx", ParentMarkerForField(outputEntry[4]));
                Assert.AreEqual("bb", ParentMarkerForField(outputEntry[5]));
                Assert.AreEqual("lx", ParentMarkerForField(outputEntry[6]));
                Assert.AreEqual("lx", ParentMarkerForField(outputEntry[7]));
                Assert.AreEqual("bb", ParentMarkerForField(outputEntry[8]));

                Assert.AreEqual("bb", ChildMarkerForField(entry[0], 0));
                Assert.AreEqual("sn", ChildMarkerForField(entry[0], 1));
                Assert.AreEqual("bb", ChildMarkerForField(entry[0], 2));
                Assert.AreEqual("sn", ChildMarkerForField(entry[0], 3));
                Assert.AreEqual("bb", ChildMarkerForField(entry[0], 4));
            }
        }

        [Test]
        public void MultiplicityTogether_WhenChildCanAppearUnderParentMultipleTogether_InfersNodeOnceForEveryChildTogether()
        {
            const string sfmIn = @"
\lx test2
\cc fire
\cc foo
\sn
\cc bar";

            //expecting:

            /*
             * \lx test2
             *    \bb
             *      \cc fire
             *      \cc foo
             *    \sn
             *    \bb
             *      \cc bar
             */

            //const string xmlEx = "<entry record=\"5\"><lx field=\"1\"><data>test2</data><bb inferred=\"true\"><data /><cc><data>fire</data></cc><cc><data>foo</data></cc></bb><sn><data /></sn><bb inferred=\"true\"><data /><cc><data>bar</data></cc></bb></lx></entry>";
            using (var environment = new EnvironmentForTest())
            {

                var settings = environment.SettingsForTest;

                var ccSetting = settings.FindOrCreateMarkerSetting("cc");
                ccSetting.StructureProperties.Add(new SolidStructureProperty("bb",
                                                                             MultiplicityAdjacency.MultipleTogether));

                Assert.IsNotNull(settings.FindOrCreateMarkerSetting("cc"));
                ccSetting.InferedParent = "bb";

                SfmLexEntry entry = SfmLexEntry.CreateFromText(sfmIn);

                var process = new ProcessStructure(settings);
                var report = environment.CreateReportForTest();
                var outputEntry = process.Process(entry, report);

                Assert.AreEqual(null, outputEntry[0].Parent);
                Assert.AreEqual("lx", ParentMarkerForField(outputEntry[1]));
                Assert.AreEqual("bb", ParentMarkerForField(outputEntry[2]));
                Assert.AreEqual("bb", ParentMarkerForField(outputEntry[3]));
                Assert.AreEqual("lx", ParentMarkerForField(outputEntry[4]));
                Assert.AreEqual("lx", ParentMarkerForField(outputEntry[5]));
                Assert.AreEqual("bb", ParentMarkerForField(outputEntry[6]));
                
                Assert.AreEqual("bb", ChildMarkerForField(entry[0], 0));
                Assert.AreEqual("sn", ChildMarkerForField(entry[0], 1));
                Assert.AreEqual("bb", ChildMarkerForField(entry[0], 2));
               
            }
        }

        [Test]
        public void MultiplicityApart_WhenChildCanAppearUnderParentMultipleApart_InfersNodeOnceForAllChildrenSeperated()
        {
            //const string xmlIn = "<entry record=\"4\"><lx field=\"1\">test1</lx><cc>fire</cc><sn></sn><cc>foo</cc><sn></sn><cc>bar</cc></entry>";

            const string sfmIn = @"
\lx test1
\cc fire
\sn
\cc foo
\sn
\cc bar";

            //expecting:

            /*
             * \lx test1
             *    \bb
             *      \cc fire
             *      \sn
             *      \cc foo
             *      \sn
             *      \cc bar
             */

            //const string xmlEx = "<entry record=\"4\"><lx field=\"1\"><data>test1</data><bb inferred=\"true\"><data /><cc><data>fire</data></cc><sn><data /></sn><cc><data>foo</data></cc><sn><data /></sn><cc><data>bar</data></cc></bb></lx></entry>";

            using (var environment = new EnvironmentForTest())
            {
                var settings = environment.SettingsForTest;

                var ccSetting = settings.FindOrCreateMarkerSetting("cc");
                ccSetting.StructureProperties.Add(new SolidStructureProperty("bb", MultiplicityAdjacency.MultipleApart));

                var snSetting = settings.FindOrCreateMarkerSetting("sn");
                snSetting.StructureProperties.Add(new SolidStructureProperty("bb", MultiplicityAdjacency.MultipleApart));
                Assert.IsNotNull(settings.FindOrCreateMarkerSetting("cc"));
                ccSetting.InferedParent = "bb";

                SfmLexEntry entry = SfmLexEntry.CreateFromText(sfmIn);

                var process = new ProcessStructure(settings);
                var report = environment.CreateReportForTest();
                var outputEntry = process.Process(entry, report);

                Assert.AreEqual("lx", outputEntry[0].Marker);
                Assert.AreEqual("bb", outputEntry[1].Marker);
                Assert.AreEqual("cc", outputEntry[2].Marker);
                Assert.AreEqual("sn", outputEntry[3].Marker);
                Assert.AreEqual("cc", outputEntry[4].Marker);
                Assert.AreEqual("sn", outputEntry[5].Marker);
                Assert.AreEqual("cc", outputEntry[6].Marker);

                Assert.AreEqual(null, outputEntry[0].Parent);
                Assert.AreEqual("lx", ParentMarkerForField(outputEntry[1]));
                Assert.AreEqual("bb", ParentMarkerForField(outputEntry[2]));
                Assert.AreEqual("bb", ParentMarkerForField(outputEntry[3]));
                Assert.AreEqual("bb", ParentMarkerForField(outputEntry[4]));
                Assert.AreEqual("bb", ParentMarkerForField(outputEntry[5]));
                Assert.AreEqual("bb", ParentMarkerForField(outputEntry[6]));

                Assert.AreEqual("bb", ChildMarkerForField(outputEntry[0], 0));
                Assert.AreEqual("cc", ChildMarkerForField(outputEntry[1], 0));
                Assert.AreEqual("sn", ChildMarkerForField(outputEntry[1], 1));
                Assert.AreEqual("cc", ChildMarkerForField(outputEntry[1], 2));
                Assert.AreEqual("sn", ChildMarkerForField(outputEntry[1], 3));
                Assert.AreEqual("cc", ChildMarkerForField(outputEntry[1], 4));
            }
        }

        [Test]
        public void MultiplicityApart_WhenChildCanAppearUnderParentMultipleApart_InfersNodeOnceForAllChildrenTogetherAndSeperated()
        {
            //const string xmlIn = "<entry record=\"5\"><lx field=\"1\">test2</lx><cc>fire</cc><cc>foo</cc><sn></sn><cc>bar</cc></entry>";

            const string sfmIn = @"
\lx test2
\cc fire
\cc foo
\sn
\cc bar";

            //expecting:

            /*
             * \lx test1
             *    \bb
             *      \cc fire
             *      \cc foo
             *      \sn
             *      \cc bar
             */
            
            //const string xmlEx = "<entry record=\"5\"><lx field=\"1\"><data>test2</data><bb inferred=\"true\"><data /><cc><data>fire</data></cc><cc><data>foo</data></cc><sn><data /></sn><cc><data>bar</data></cc></bb></lx></entry>";

            using (var environment = new EnvironmentForTest())
            {
                var settings = environment.SettingsForTest;

                var ccSetting = settings.FindOrCreateMarkerSetting("cc");
                ccSetting.StructureProperties.Add(new SolidStructureProperty("bb", MultiplicityAdjacency.MultipleApart));

                var snSetting = settings.FindOrCreateMarkerSetting("sn");
                snSetting.StructureProperties.Add(new SolidStructureProperty("bb", MultiplicityAdjacency.MultipleApart));

                Assert.IsNotNull(settings.FindOrCreateMarkerSetting("cc"));
                ccSetting.InferedParent = "bb";

                SfmLexEntry entry = SfmLexEntry.CreateFromText(sfmIn);

                var process = new ProcessStructure(settings);
                var report = environment.CreateReportForTest();
                var outputEntry = process.Process(entry, report);

                Assert.AreEqual(null, outputEntry[0].Parent);
                Assert.AreEqual("lx", ParentMarkerForField(outputEntry[1]));
                Assert.AreEqual("bb", ParentMarkerForField(outputEntry[2]));
                Assert.AreEqual("bb", ParentMarkerForField(outputEntry[3]));
                Assert.AreEqual("bb", ParentMarkerForField(outputEntry[4]));
                Assert.AreEqual("bb", ParentMarkerForField(outputEntry[5]));
 

                Assert.AreEqual("bb", ChildMarkerForField(outputEntry[0], 0));
                Assert.AreEqual("cc", ChildMarkerForField(outputEntry[1], 0));
                Assert.AreEqual("cc", ChildMarkerForField(outputEntry[1], 1));
                Assert.AreEqual("sn", ChildMarkerForField(outputEntry[1], 2));
                Assert.AreEqual("cc", ChildMarkerForField(outputEntry[1], 3));
            }
        }

        [Test]
        public void ProcessStructure_InferNode_Correct()
        {
            //const string xmlIn = "<entry record=\"4\"><lx field=\"1\">a</lx><ge>g</ge></entry>";

            const string sfmIn = @"
\lx a
\ge g";

            //expecting:

            /*
             * \lx a
             *    \sn
             *      \ge g     
             */
            
            //const string xmlEx = "<entry record=\"4\"><lx field=\"1\"><data>a</data><sn inferred=\"true\"><data /><ge><data>g</data></ge></sn></lx></entry>";

            using (var environment = new EnvironmentForTest())
            {
                var settings = environment.SettingsForTest;


                var setting = settings.FindOrCreateMarkerSetting("ge");
                Assert.IsNotNull(setting);
                setting.InferedParent = "sn";

                SfmLexEntry entry = SfmLexEntry.CreateFromText(sfmIn);

                var process = new ProcessStructure(settings);
                var report = environment.CreateReportForTest();
                var outputEntry = process.Process(entry, report);

                Assert.AreEqual(null, outputEntry[0].Parent);
                Assert.AreEqual("lx", ParentMarkerForField(outputEntry[1]));
                Assert.AreEqual("sn", ParentMarkerForField(outputEntry[2]));

                Assert.AreEqual("sn", ChildMarkerForField(outputEntry[0], 0));
                Assert.AreEqual("ge", ChildMarkerForField(outputEntry[1], 0));
            }
        }

        [Test]
        public void ProcessStructure_NoInferReqd_Correct()
        {
            //const string xmlIn = "<entry record=\"4\"><lx field=\"1\">a</lx><sn></sn><ge>g</ge></entry>";

            const string sfmIn = @"
\lx a
\sn
\ge g";
            //expecting:

            /*
             * \lx a
             *    \sn
             *      \ge g     
             */

            //const string xmlEx = "<entry record=\"4\"><lx field=\"1\"><data>a</data><sn><data /><ge><data>g</data></ge></sn></lx></entry>";

            using (var environment = new EnvironmentForTest())
            {
                var settings = environment.SettingsForTest;

                var setting = settings.FindOrCreateMarkerSetting("ge");
                Assert.IsNotNull(setting);
                setting.InferedParent = "";

                SfmLexEntry entry = SfmLexEntry.CreateFromText(sfmIn);

                var process = new ProcessStructure(settings);
                var report = environment.CreateReportForTest();
                var outputEntry = process.Process(entry, report);

                Assert.AreEqual(null, outputEntry[0].Parent);
                Assert.AreEqual("lx", ParentMarkerForField(outputEntry[1]));
                Assert.AreEqual("sn", ParentMarkerForField(outputEntry[2]));

                Assert.AreEqual("sn", ChildMarkerForField(outputEntry[0], 0));
                Assert.AreEqual("ge", ChildMarkerForField(outputEntry[1], 0));

            }
        }

        [Test]
        public void ProcessStructure_RecursiveInfer_Correct()
        {
            //const string xmlIn = "<entry record=\"4\"><lx field=\"1\">a</lx><xe field=\"2\">b</xe></entry>";
            
            const string sfmIn = @"
\lx a
\xe b";
            //expecting:

            /*
             * \lx a
             *    \sn
             *      \rf
             *        \xe b
             */

            //const string xmlEx = "<entry record=\"4\"><lx field=\"1\"><data>a</data><sn inferred=\"true\"><data /><rf inferred=\"true\"><data /><xe field=\"2\"><data>b</data></xe></rf></sn></lx></entry>";
            
            using (var environment = new EnvironmentForTest())
            {
                var settings = environment.SettingsForTest;

                var setting = settings.FindOrCreateMarkerSetting("ge");  // TODO create 'ge'?? no 'ge' in xml output
                Assert.IsNotNull(setting);
                setting.InferedParent = "";

                SfmLexEntry entry = SfmLexEntry.CreateFromText(sfmIn);

                var process = new ProcessStructure(settings);
                var report = environment.CreateReportForTest();
                var outputEntry = process.Process(entry, report);

                Assert.AreEqual(null, outputEntry[0].Parent);
                Assert.AreEqual("lx", ParentMarkerForField(outputEntry[1]));
                Assert.AreEqual("sn", ParentMarkerForField(outputEntry[2]));
                Assert.AreEqual("rf", ParentMarkerForField(outputEntry[3]));

                Assert.AreEqual("sn", ChildMarkerForField(outputEntry[0], 0));
                Assert.AreEqual("rf", ChildMarkerForField(outputEntry[1], 0));
                Assert.AreEqual("xe", ChildMarkerForField(outputEntry[2], 0));

            }

        }

        [Test]
        public void ProcessStructure_RecursiveInferIssue144_MarkersNotDuplicated()
        {
            //const string xmlIn = "<entry record=\"4\"><lx field=\"1\">a</lx><xe field=\"2\">b</xe></entry>";

            const string sfmIn = @"
\lx a
\xe b";

            //expecting:

            /*
             * \lx a
             * \sn
             *    \rf
             *       \xe b
             */
            
            //string xmlEx = "<entry record=\"4\"><lx field=\"1\"><data>a</data><ps inferred=\"true\"><data /><sn inferred=\"true\"><data /><rf inferred=\"true\"><data /><xe field=\"2\"><data>b</data></xe></rf></sn></ps></lx></entry>";
            
            using (var environment = new EnvironmentForTest())
            {
                
                var settings = new SolidSettings();
                //var lxSetting = settings.FindOrCreateMarkerSetting("lx");
                //lxSetting.StructureProperties.Add(new SolidStructureProperty("entry", MultiplicityAdjacency.Once));

                var psSetting = settings.FindOrCreateMarkerSetting("ps");
                psSetting.StructureProperties.Add(new SolidStructureProperty("lx", MultiplicityAdjacency.MultipleApart));

                var snSetting = settings.FindOrCreateMarkerSetting("sn");
                snSetting.StructureProperties.Add(new SolidStructureProperty("ps", MultiplicityAdjacency.MultipleApart));
                snSetting.InferedParent = "";

                var rfSetting = settings.FindOrCreateMarkerSetting("rf");
                rfSetting.StructureProperties.Add(new SolidStructureProperty("sn",
                                                                             MultiplicityAdjacency.MultipleTogether));
                rfSetting.InferedParent = "sn";

                var xeSetting = settings.FindOrCreateMarkerSetting("xe");
                xeSetting.StructureProperties.Add(new SolidStructureProperty("rf", MultiplicityAdjacency.Once));
                xeSetting.InferedParent = "rf";

                SfmLexEntry entry = SfmLexEntry.CreateFromText(sfmIn);

                var process = new ProcessStructure(settings);
                var report = environment.CreateReportForTest();
                var outputEntry = process.Process(entry, report);

                Assert.AreEqual("lx", outputEntry[0].Marker);
                Assert.AreEqual("sn", outputEntry[1].Marker);
                Assert.AreEqual("rf", outputEntry[2].Marker);
                Assert.AreEqual("xe", outputEntry[3].Marker);

                Assert.AreEqual(null, outputEntry[0].Parent);
                Assert.AreEqual(null, outputEntry[1].Parent);
                //Assert.AreEqual("lx", ParentMarkerForField(outputEntry[1]));
                Assert.AreEqual("sn", ParentMarkerForField(outputEntry[2]));
                Assert.AreEqual("rf", ParentMarkerForField(outputEntry[3]));

                //Assert.AreEqual("ps", ChildMarkerForField(outputEntry[0], 0));
                //Assert.AreEqual("sn", ChildMarkerForField(outputEntry[1], 0));
                //Assert.AreEqual("rf", ChildMarkerForField(outputEntry[2], 0));
                //Assert.AreEqual("xe", ChildMarkerForField(outputEntry[3], 0));
            }
        }

        [Test]
        [Ignore("Record ID is being deprecated - hopefully we don't need it anymore CP 2010-09")]
        public void ProcessStructure_ErrorRecordID145_RecordIDValid()
        {
            //const string xmlIn = "<entry record=\"4\"><lx field=\"1\">a</lx><xe field=\"2\">b</xe></entry>";

            const string sfmIn = @"
\lx a
\ge g";

            //expecting:

            /*
             * \lx a
             *    \xe b
             */

            //or expecting: ???

            /*
             * \lx a
             *    \ps
             *      \sn
             *        \rf
             *          \xe b
             */

            
            //            string xmlEx = "<entry record=\"4\"><lx field=\"1\"><data>a</data><ps inferred=\"true\"><data /><sn inferred=\"true\"><data /><rf inferred=\"true\"><data /><xe field=\"2\"><data>b</data></xe></rf></sn></ps></lx></entry>";
            //const string xmlEx = "<entry record=\"4\"><lx field=\"1\"><data>a</data><xe field=\"2\"><data>b</data></xe></lx></entry>";

            using (var environment = new EnvironmentForTest())
            {
                var settings = new SolidSettings();
                var lxSetting = settings.FindOrCreateMarkerSetting("lx");
                lxSetting.StructureProperties.Add(new SolidStructureProperty("entry", MultiplicityAdjacency.Once));

                var xeSetting = settings.FindOrCreateMarkerSetting("xe");
                xeSetting.StructureProperties.Add(new SolidStructureProperty("rf", MultiplicityAdjacency.Once));
                xeSetting.InferedParent = "";

                SfmLexEntry entry = SfmLexEntry.CreateFromText(sfmIn);

                var process = new ProcessStructure(settings);
                var report = environment.CreateReportForTest();
                var outputEntry = process.Process(entry, report);

                Assert.AreEqual(null, ParentMarkerForField(outputEntry[0]));
                Assert.AreEqual("lx", ParentMarkerForField(outputEntry[1]));
                Assert.AreEqual("ps", ParentMarkerForField(outputEntry[2]));
                Assert.AreEqual("sn", ParentMarkerForField(outputEntry[3]));
                Assert.AreEqual("rf", ParentMarkerForField(outputEntry[4]));

                Assert.AreEqual("ps", ChildMarkerForField(outputEntry[0], 0));
                Assert.AreEqual("sn", ChildMarkerForField(outputEntry[1], 0));
                Assert.AreEqual("rf", ChildMarkerForField(outputEntry[2], 0));
                Assert.AreEqual("xe", ChildMarkerForField(outputEntry[3], 0));
         
            }
        }

        [Test]
        public void ProcessStructure_NoInferInsertAnyway_AllFieldsExistInOutputEntry()
        {
            //const string xmlIn = "<entry record=\"4\"><lx field=\"1\">a</lx><sn field=\"2\"></sn><ge field=\"3\">g</ge><zz field=\"4\">z</zz></entry>";

            const string sfmIn = @"
\lx a
\sn
\ge g
\zz z";
            //expecting: ???

            /*
             * \lx a
             *   \sn
             * \ge g
             * \zz z
             */

            //const string xmlEx = "<entry record=\"4\"><lx field=\"1\"><data>a</data><sn field=\"2\"><data /><ge field=\"3\"><data>g</data><zz field=\"4\"><data>z</data></zz></ge></sn></lx></entry>";

            using (var environment = new EnvironmentForTest())
            {
                var settings = environment.SettingsForTest;

                var markerSettings = settings.FindOrCreateMarkerSetting("ge");
                Assert.IsNotNull(markerSettings);
                markerSettings.InferedParent = "";

                SfmLexEntry entry = SfmLexEntry.CreateFromText(sfmIn);

                var process = new ProcessStructure(settings);
                var report = environment.CreateReportForTest();
                var outputEntry = process.Process(entry, report);

               
                Assert.AreEqual("lx", outputEntry[0].Marker);
                Assert.AreEqual("sn", outputEntry[1].Marker);
                Assert.AreEqual("ge", outputEntry[2].Marker);
                Assert.AreEqual("zz", outputEntry[3].Marker);
            }
        }

        [Test]
        public void ProcessStructure_NoInferInsertAnyway_MarkersWithoutParentsShowNullParent()
        {
            //const string xmlIn = "<entry record=\"4\"><lx field=\"1\">a</lx><sn field=\"2\"></sn><ge field=\"3\">g</ge><zz field=\"4\">z</zz></entry>";

            const string sfmIn = @"
\lx a
\sn
\ge g
\zz z";
            //expecting:

            /*
             * \lx a
             *   \sn
             *     \ge g
             * \zz z
             */

            //const string xmlEx = "<entry record=\"4\"><lx field=\"1\"><data>a</data><sn field=\"2\"><data /><ge field=\"3\"><data>g</data><zz field=\"4\"><data>z</data></zz></ge></sn></lx></entry>";

            using (var environment = new EnvironmentForTest())
            {
                var settings = environment.SettingsForTest;

                var markerSettings = settings.FindOrCreateMarkerSetting("ge");
                Assert.IsNotNull(markerSettings);
                markerSettings.InferedParent = "";

                SfmLexEntry entry = SfmLexEntry.CreateFromText(sfmIn);

                var process = new ProcessStructure(settings);
                var report = environment.CreateReportForTest();
                var outputEntry = process.Process(entry, report);

                Assert.AreEqual("lx", outputEntry[0].Marker);
                Assert.AreEqual("sn", outputEntry[1].Marker);
                Assert.AreEqual("ge", outputEntry[2].Marker);
                Assert.AreEqual("zz", outputEntry[3].Marker);

                Assert.AreEqual(null, ParentMarkerForField(outputEntry[0]));
                Assert.AreEqual("lx", ParentMarkerForField(outputEntry[1]));
                Assert.AreEqual("sn", ParentMarkerForField(outputEntry[2]));
                Assert.AreEqual(null, ParentMarkerForField(outputEntry[3]));


                Assert.AreEqual("sn", ChildMarkerForField(outputEntry[0], 0));
                
            }
        }

        [Test] // TODO this test does not have structure
        public void ProcessStructure_LiftMapping_Correct()
        {
            //const string xmlIn = "<entry record=\"4\"><lx field=\"1\">b</lx></entry>";

            const string sfmIn = @"
\lx b";

            //expecting:

            /*
             * \lx b
             */
            
            
            //const string xmlEx = "<entry record=\"4\"><lx field=\"1\" lift=\"a\" writingsystem=\"zxx\"><data>b</data></lx></entry>";

            using (var environment = new EnvironmentForTest())
            {
                var settings = environment.SettingsForTest;

                var markerSetttings = settings.FindOrCreateMarkerSetting("lx");
                Assert.IsNotNull(markerSetttings);
                markerSetttings.Mappings[(int) SolidMarkerSetting.MappingType.Lift] = "a";

                SfmLexEntry entry = SfmLexEntry.CreateFromText(sfmIn);

                var process = new ProcessStructure(settings);
                var report = environment.CreateReportForTest();
                process.Process(entry, report);

                //string xmlOut = xmlResult.OuterXml;
                //Assert.AreEqual(xmlEx, xmlOut);

            }
        }

//        [Test]
//        public void ProcessStructure_FlexMapping_Correct()
//        {
//            string xmlIn = "<entry record=\"5\"><lx field=\"1\">b</lx></entry>";
//            string xmlEx = "<entry record=\"5\"><lx field=\"1\" flex=\"a\" writingsystem=\"zxx\"><data>b</data></lx></entry>";
//
//            SolidMarkerSetting setting = _settings.FindOrCreateMarkerSetting("lx");
//            Assert.IsNotNull(setting);
//            setting.Mappings[(int)SolidMarkerSetting.MappingType.Flex] = "a";
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
        public void MultipleErrorMarkers_AreSiblings()
        {
            //const string xmlIn = "<entry record=\"4\"><lx field=\"1\">a</lx><xx field=\"2\">xx</xx><yy field=\"3\">yy</yy><zz field=\"4\">zz</zz></entry>";
            const string sfmIn = @"
\lx a
\xx x
\yy y
\zz z";

            //expecting:

            /*
             * \lx a
             * \xx xx
             * \yy yy
             * \zz zz
             */

            //const string xmlEx = "<entry record=\"4\"><lx field=\"1\"><data>a</data><xx field=\"2\"><data>xx</data></xx><yy field=\"3\"><data>yy</data></yy><zz field=\"4\"><data>zz</data></zz></lx></entry>";


            using (var environment = new EnvironmentForTest())
            {
                var settings = environment.SettingsForTest;

                SfmLexEntry entry = SfmLexEntry.CreateFromText(sfmIn);

                var process = new ProcessStructure(settings);
                var report = environment.CreateReportForTest();
                var outputEntry = process.Process(entry, report);

                Assert.AreEqual("lx", outputEntry[0].Marker);
                Assert.AreEqual("xx", outputEntry[1].Marker);
                Assert.AreEqual("yy", outputEntry[2].Marker);
                Assert.AreEqual("zz", outputEntry[3].Marker);

                Assert.AreEqual(null, outputEntry[1].Parent);
                Assert.AreEqual(null, outputEntry[2].Parent);
                Assert.AreEqual(null, outputEntry[3].Parent);

                //Assert.AreEqual("xx", ChildMarkerForField(outputEntry[0], 0));
                //Assert.AreEqual("yy", ChildMarkerForField(outputEntry[0], 1));
                //Assert.AreEqual("zz", ChildMarkerForField(outputEntry[0], 2));
            }
        }

        [Test]
        public void ProcessStructure_WithoutInferredNode_IsCorrect()
        {
            const string sfmIn = @"
\lx a
\sn
\ge g";
            //expecting:

            /*
             * \lx a
             *    \sn
             *      \ge g     
             */

            //const string xmlEx = "<entry record=\"4\"><lx field=\"1\"><data>a</data><sn><data /><ge><data>g</data></ge></sn></lx></entry>";

            using (var environment = new EnvironmentForTest())
            {
                var settings = environment.SettingsForTest;

                var setting = settings.FindOrCreateMarkerSetting("ge");
                Assert.IsNotNull(setting);
                setting.InferedParent = "";


                SfmLexEntry entry = SfmLexEntry.CreateFromText(sfmIn);

                var process = new ProcessStructure(settings);
                var report = environment.CreateReportForTest();
                var outputEntry = process.Process(entry, report);

                Assert.AreEqual(entry[0], outputEntry[0]);
                Assert.AreEqual(entry[1], outputEntry[1]);
                Assert.AreEqual(entry[2], outputEntry[2]);
            }
        }

        [Test]
        public void ProcessStructure_WithInferredNode_IsCorrect()
        {
            const string sfmIn = @"
\lx test1
\cc fire
\sn
\cc foo
\sn
\cc bar";

            //expecting:

            /*
             * \lx test1
             *    \bb
             *      \cc fire
             *      \sn
             *      \cc foo
             *      \sn
             *      \cc bar
             */

            //const string xmlEx = "<entry record=\"4\"><lx field=\"1\"><data>test1</data><bb inferred=\"true\"><data /><cc><data>fire</data></cc><sn><data /></sn><cc><data>foo</data></cc><sn><data /></sn><cc><data>bar</data></cc></bb></lx></entry>";

            using (var environment = new EnvironmentForTest())
            {
                var settings = environment.SettingsForTest;

                var ccSetting = settings.FindOrCreateMarkerSetting("cc");
                ccSetting.StructureProperties.Add(new SolidStructureProperty("bb", MultiplicityAdjacency.MultipleApart));

                var snSetting = settings.FindOrCreateMarkerSetting("sn");
                snSetting.StructureProperties.Add(new SolidStructureProperty("bb", MultiplicityAdjacency.MultipleApart));
                Assert.IsNotNull(settings.FindOrCreateMarkerSetting("cc"));
                ccSetting.InferedParent = "bb";

                SfmLexEntry entry = SfmLexEntry.CreateFromText(sfmIn);

                var process = new ProcessStructure(settings);
                var report = environment.CreateReportForTest();
                SfmLexEntry outputEntry = process.Process(entry, report);

                Assert.AreEqual(entry[0], outputEntry[0]);
                Assert.AreEqual("bb", outputEntry[1].Marker); // compare markers; SfmFieldModels will not be equal
                Assert.AreEqual(entry[1], outputEntry[2]);
                Assert.AreEqual(entry[2], outputEntry[3]);
                Assert.AreEqual(entry[3], outputEntry[4]);
                Assert.AreEqual(entry[4], outputEntry[5]);
                Assert.AreEqual(entry[5], outputEntry[6]);

            }

        }

        #region New comprehensive test coverage

        [Test]
        public void ProcessStructure_NoInferMultiplicityOnce_MarkersInOutputAndParentsCorrect()
        {
            const string sfmIn = @"
\lx test
\aa
\bb
\aa
\bb";
            //expecting:

            /*
             * \lx test
             *    \aa
             *      \bb
             *    \aa
             *      \bb
             */

            var settings = new SolidSettings();

            SetMarkerSettings(settings, "aa", "lx", "", MultiplicityAdjacency.MultipleApart);
            SetMarkerSettings(settings, "bb", "aa", "", MultiplicityAdjacency.Once);

            SfmLexEntry entry = SfmLexEntry.CreateFromText(sfmIn);

            var process = new ProcessStructure(settings);
            var report = new SolidReport();
            SfmLexEntry outputEntry = process.Process(entry, report);

            Assert.AreEqual("lx", outputEntry[0].Marker);
            Assert.AreEqual("aa", outputEntry[1].Marker); 
            Assert.AreEqual("bb", outputEntry[2].Marker);
            Assert.AreEqual("aa", outputEntry[3].Marker);
            Assert.AreEqual("bb", outputEntry[4].Marker);

            Assert.AreEqual("lx", ParentMarkerForField(outputEntry[1]));
            Assert.AreEqual("aa", ParentMarkerForField(outputEntry[2]));
            Assert.AreEqual("lx", ParentMarkerForField(outputEntry[3]));
            Assert.AreEqual("aa", ParentMarkerForField(outputEntry[4]));
        }

        [Test]
        public void ProcessStructure_NoInferMultiplicityTogether_MarkersInOutputAndParentsCorrect()
        {
            const string sfmIn = @"
\lx test
\aa
\bb
\aa
\bb";
            //expecting:

            /*
             * \lx test
             *    \aa
             *      \bb
             *    \aa
             *      \bb
             */

            var settings = new SolidSettings();

            SetMarkerSettings(settings, "aa", "lx", "", MultiplicityAdjacency.MultipleTogether);
            SetMarkerSettings(settings, "bb", "aa", "", MultiplicityAdjacency.MultipleTogether);

            SfmLexEntry entry = SfmLexEntry.CreateFromText(sfmIn);

            var process = new ProcessStructure(settings);
            var report = new SolidReport();
            SfmLexEntry outputEntry = process.Process(entry, report);

            Assert.AreEqual("lx", outputEntry[0].Marker);
            Assert.AreEqual("aa", outputEntry[1].Marker);
            Assert.AreEqual("bb", outputEntry[2].Marker);
            Assert.AreEqual("aa", outputEntry[3].Marker);
            Assert.AreEqual("bb", outputEntry[4].Marker);

            Assert.AreEqual("lx", ParentMarkerForField(outputEntry[1]));
            Assert.AreEqual("aa", ParentMarkerForField(outputEntry[2]));
            Assert.AreEqual("lx", ParentMarkerForField(outputEntry[3]));
            Assert.AreEqual("aa", ParentMarkerForField(outputEntry[4]));
        }

        [Test]
        public void ProcessStructure_NoInferMultiplicityApart_MarkersInOutputAndParentsCorrect()
        {
            const string sfmIn = @"
\lx test
\aa
\bb
\aa
\bb";
            //expecting:

            /*
             * \lx test
             *    \aa
             *      \bb
             *    \aa
             *      \bb
             */

            var settings = new SolidSettings();

            SetMarkerSettings(settings, "aa", "lx", "", MultiplicityAdjacency.MultipleApart);
            SetMarkerSettings(settings, "bb", "aa", "", MultiplicityAdjacency.MultipleApart);

            SfmLexEntry entry = SfmLexEntry.CreateFromText(sfmIn);

            var process = new ProcessStructure(settings);
            var report = new SolidReport();
            SfmLexEntry outputEntry = process.Process(entry, report);

            Assert.AreEqual("lx", outputEntry[0].Marker);
            Assert.AreEqual("aa", outputEntry[1].Marker);
            Assert.AreEqual("bb", outputEntry[2].Marker);
            Assert.AreEqual("aa", outputEntry[3].Marker);
            Assert.AreEqual("bb", outputEntry[4].Marker);

            Assert.AreEqual("lx", ParentMarkerForField(outputEntry[1]));
            Assert.AreEqual("aa", ParentMarkerForField(outputEntry[2]));
            Assert.AreEqual("lx", ParentMarkerForField(outputEntry[3]));
            Assert.AreEqual("aa", ParentMarkerForField(outputEntry[4]));
        }

        [Test]
        public void ProcessStructure_InferMultiplicityOnce_MarkersInOutputAndParentsCorrect()
        {
            const string sfmIn = @"
\lx test
\aa
\aa";
            //expecting:

            /*
             * \lx test
             *    \inferred
             *      \aa
             *    \inferred
             *      \aa
             */

            var settings = new SolidSettings();

            SetMarkerSettings(settings, "aa", "inferred", "inferred", MultiplicityAdjacency.Once);
            SetMarkerSettings(settings, "inferred", "lx", "", MultiplicityAdjacency.MultipleTogether);
            
            SfmLexEntry entry = SfmLexEntry.CreateFromText(sfmIn);

            var process = new ProcessStructure(settings);
            var report = new SolidReport();
            SfmLexEntry outputEntry = process.Process(entry, report);

            Assert.AreEqual("lx", outputEntry[0].Marker);
            Assert.AreEqual("inferred", outputEntry[1].Marker);
            Assert.AreEqual("aa", outputEntry[2].Marker);
            Assert.AreEqual("inferred", outputEntry[3].Marker);
            Assert.AreEqual("aa", outputEntry[4].Marker);

            Assert.AreEqual("lx", ParentMarkerForField(outputEntry[1]));
            Assert.AreEqual("inferred", ParentMarkerForField(outputEntry[2]));
            Assert.AreEqual("lx", ParentMarkerForField(outputEntry[3]));
            Assert.AreEqual("inferred", ParentMarkerForField(outputEntry[4]));
        }

        [Test]
        public void ProcessStructure_InferMultiplicityOnceWithSpacer_MarkersInOutputAndParentsCorrect()
        {
            const string sfmIn = @"
\lx test
\aa
\spacer
\aa";
            //expecting:

            /*
             * \lx test
             *    \inferred
             *      \aa
             *      \spacer
             *    \inferred
             *      \aa
             */

            var settings = new SolidSettings();

            SetMarkerSettings(settings, "aa", "inferred", "inferred", MultiplicityAdjacency.Once);
            SetMarkerSettings(settings, "inferred", "lx", "", MultiplicityAdjacency.MultipleApart);
            SetMarkerSettings(settings, "spacer", "inferred", "", MultiplicityAdjacency.MultipleApart);

            SfmLexEntry entry = SfmLexEntry.CreateFromText(sfmIn);

            var process = new ProcessStructure(settings);
            var report = new SolidReport();
            SfmLexEntry outputEntry = process.Process(entry, report);

            Assert.AreEqual("lx", outputEntry[0].Marker);
            Assert.AreEqual("inferred", outputEntry[1].Marker);
            Assert.AreEqual("aa", outputEntry[2].Marker);
            Assert.AreEqual("spacer", outputEntry[3].Marker);
            Assert.AreEqual("inferred", outputEntry[4].Marker);
            Assert.AreEqual("aa", outputEntry[5].Marker);

            Assert.AreEqual("lx", ParentMarkerForField(outputEntry[1]));
            Assert.AreEqual("inferred", ParentMarkerForField(outputEntry[2]));
            Assert.AreEqual("inferred", ParentMarkerForField(outputEntry[3]));
            Assert.AreEqual("lx", ParentMarkerForField(outputEntry[4]));
            Assert.AreEqual("inferred", ParentMarkerForField(outputEntry[5]));
        }

        [Test]
        public void ProcessStructure_InferMultiplicityTogether_MarkersInOutputAndParentsCorrect()
        {
            const string sfmIn = @"
\lx test
\aa
\aa";
            //expecting:

            /*
             * \lx test
             *    \inferred
             *      \aa
             *      \aa
             */

            var settings = new SolidSettings();

            SetMarkerSettings(settings, "aa", "inferred", "inferred", MultiplicityAdjacency.MultipleTogether);
            SetMarkerSettings(settings, "inferred", "lx", "", MultiplicityAdjacency.MultipleTogether);

            SfmLexEntry entry = SfmLexEntry.CreateFromText(sfmIn);

            var process = new ProcessStructure(settings);
            var report = new SolidReport();
            SfmLexEntry outputEntry = process.Process(entry, report);

            Assert.AreEqual("lx", outputEntry[0].Marker);
            Assert.AreEqual("inferred", outputEntry[1].Marker);
            Assert.AreEqual("aa", outputEntry[2].Marker);
            Assert.AreEqual("aa", outputEntry[3].Marker);

            Assert.AreEqual("lx", ParentMarkerForField(outputEntry[1]));
            Assert.AreEqual("inferred", ParentMarkerForField(outputEntry[2]));
            Assert.AreEqual("inferred", ParentMarkerForField(outputEntry[3]));
            
        }

        [Test]
        public void ProcessStructure_InferMultiplicityApartWithoutSpacer_MarkersInOutputAndParentsCorrect()
        {
            const string sfmIn = @"
\lx test
\aa
\aa";
            //expecting:

            /*
             * \lx test
             *    \inferred
             *      \aa
             *      \aa
             */

            var settings = new SolidSettings();

            SetMarkerSettings(settings, "aa", "inferred", "inferred", MultiplicityAdjacency.MultipleApart);
            SetMarkerSettings(settings, "inferred", "lx", "", MultiplicityAdjacency.MultipleTogether);

            SfmLexEntry entry = SfmLexEntry.CreateFromText(sfmIn);

            var process = new ProcessStructure(settings);
            var report = new SolidReport();
            SfmLexEntry outputEntry = process.Process(entry, report);

            Assert.AreEqual("lx", outputEntry[0].Marker);
            Assert.AreEqual("inferred", outputEntry[1].Marker);
            Assert.AreEqual("aa", outputEntry[2].Marker);
            Assert.AreEqual("aa", outputEntry[3].Marker);

            Assert.AreEqual("lx", ParentMarkerForField(outputEntry[1]));
            Assert.AreEqual("inferred", ParentMarkerForField(outputEntry[2]));
            Assert.AreEqual("inferred", ParentMarkerForField(outputEntry[3]));

        }

        [Test]
        public void ProcessStructure_InferMultiplicityTogetherWithSpacer_MarkersInOutputAndParentsCorrect()
        {
            const string sfmIn = @"
\lx test
\aa
\spacer
\aa";
            //expecting:

            /*
             * \lx test
             *    \inferred
             *      \aa
             *      \spacer
             *    \inferred
             *      \aa
             */

            var settings = new SolidSettings();

            SetMarkerSettings(settings, "aa", "inferred", "inferred", MultiplicityAdjacency.MultipleTogether);
            SetMarkerSettings(settings, "inferred", "lx", "", MultiplicityAdjacency.MultipleApart);
            SetMarkerSettings(settings, "spacer", "inferred", "", MultiplicityAdjacency.MultipleApart);

            SfmLexEntry entry = SfmLexEntry.CreateFromText(sfmIn);

            var process = new ProcessStructure(settings);
            var report = new SolidReport();
            SfmLexEntry outputEntry = process.Process(entry, report);

            Assert.AreEqual("lx", outputEntry[0].Marker);
            Assert.AreEqual("inferred", outputEntry[1].Marker);
            Assert.AreEqual("aa", outputEntry[2].Marker);
            Assert.AreEqual("spacer", outputEntry[3].Marker);
            Assert.AreEqual("inferred", outputEntry[4].Marker);
            Assert.AreEqual("aa", outputEntry[5].Marker);

            Assert.AreEqual("lx", ParentMarkerForField(outputEntry[1]));
            Assert.AreEqual("inferred", ParentMarkerForField(outputEntry[2]));
            Assert.AreEqual("inferred", ParentMarkerForField(outputEntry[3]));
            Assert.AreEqual("lx", ParentMarkerForField(outputEntry[4]));
            Assert.AreEqual("inferred", ParentMarkerForField(outputEntry[5]));

        }

        [Test]
        public void ProcessStructure_InferMultiplicityTogether_AAHasInferredAsParent()
        {
            const string sfmIn = @"
\lx test
\spacer
\aa
\spacer";
            //expecting:

            /*
             * \lx test
             *    \inferred
             *      \spacer
             *      \aa
             *      \spacer
             */

            var settings = new SolidSettings();

            SetMarkerSettings(settings, "aa", "inferred", "", MultiplicityAdjacency.MultipleTogether);
            SetMarkerSettings(settings, "inferred", "lx", "", MultiplicityAdjacency.MultipleApart);
            SetMarkerSettings(settings, "spacer", "inferred", "inferred", MultiplicityAdjacency.MultipleApart);

            SfmLexEntry entry = SfmLexEntry.CreateFromText(sfmIn);

            var process = new ProcessStructure(settings);
            var report = new SolidReport();
            SfmLexEntry outputEntry = process.Process(entry, report);

            Assert.AreEqual("lx", outputEntry[0].Marker);
            Assert.AreEqual("inferred", outputEntry[1].Marker);
            Assert.AreEqual("spacer", outputEntry[2].Marker);
            Assert.AreEqual("aa", outputEntry[3].Marker);
            Assert.AreEqual("spacer", outputEntry[4].Marker);

            Assert.AreEqual("lx", ParentMarkerForField(outputEntry[1]));
            Assert.AreEqual("inferred", ParentMarkerForField(outputEntry[2]));
            Assert.AreEqual("inferred", ParentMarkerForField(outputEntry[3]));
            Assert.AreEqual("inferred", ParentMarkerForField(outputEntry[4]));

        }

        [Test]
        public void ProcessStructure_InferMultiplicityApart_MarkersInOutputAndParentsCorrect()
        {
            const string sfmIn = @"
\lx test
\aa
\spacer
\aa";
            //expecting:

            /*
             * \lx test
             *    \inferred
             *      \aa
             *      \spacer
             *      \aa
             */

            var settings = new SolidSettings();

            SetMarkerSettings(settings, "aa", "inferred", "inferred", MultiplicityAdjacency.MultipleApart);
            SetMarkerSettings(settings, "inferred", "lx", "", MultiplicityAdjacency.MultipleApart);
            SetMarkerSettings(settings, "spacer", "inferred", "", MultiplicityAdjacency.MultipleApart); // NOTE Fails with MultipleTogether

            SfmLexEntry entry = SfmLexEntry.CreateFromText(sfmIn);

            var process = new ProcessStructure(settings);
            var report = new SolidReport();
            SfmLexEntry outputEntry = process.Process(entry, report);

            Assert.AreEqual("lx", outputEntry[0].Marker);
            Assert.AreEqual("inferred", outputEntry[1].Marker);
            Assert.AreEqual("aa", outputEntry[2].Marker);
            Assert.AreEqual("spacer", outputEntry[3].Marker);
            Assert.AreEqual("aa", outputEntry[4].Marker);

            Assert.AreEqual("lx", ParentMarkerForField(outputEntry[1]));
            Assert.AreEqual("inferred", ParentMarkerForField(outputEntry[2]));
            Assert.AreEqual("inferred", ParentMarkerForField(outputEntry[3]));
            Assert.AreEqual("inferred", ParentMarkerForField(outputEntry[4]));

        }

        private static void SetMarkerSettings(SolidSettings settings, string marker, string parentMarker, string inferredMarker, MultiplicityAdjacency multiplicity)
        {
            var setting = settings.FindOrCreateMarkerSetting(marker);
            setting.StructureProperties.Add(new SolidStructureProperty(parentMarker, multiplicity));
            setting.InferedParent = inferredMarker;
        }
        #endregion


        [Test]
        public void ProcessStructure_DepthInferredWithSpacerTest_CalculatesCorrectly()
        {
            const string sfmIn = @"
\lx test
\aa
\spacer
\aa";
            //expecting:

            /*
             * \lx test
             *    \inferred
             *      \aa
             *      \spacer
             *      \aa
             */

            var settings = new SolidSettings();

            SetMarkerSettings(settings, "aa", "inferred", "inferred", MultiplicityAdjacency.MultipleApart);
            SetMarkerSettings(settings, "inferred", "lx", "", MultiplicityAdjacency.MultipleApart);
            SetMarkerSettings(settings, "spacer", "inferred", "", MultiplicityAdjacency.MultipleApart);

            SfmLexEntry entry = SfmLexEntry.CreateFromText(sfmIn);

            var process = new ProcessStructure(settings);
            var report = new SolidReport();
            SfmLexEntry outputEntry = process.Process(entry, report);

            Assert.AreEqual(0, outputEntry[0].Depth);
            Assert.AreEqual(1, outputEntry[1].Depth);
            Assert.AreEqual(2, outputEntry[2].Depth);
            Assert.AreEqual(2, outputEntry[3].Depth);
            Assert.AreEqual(2, outputEntry[4].Depth);

           
            
        }

        [Test]
        public void ProcessStructure_DepthSingleField_CalculatesCorrectly()
        {
            const string sfmIn = @"
\lx test";
            //expecting:

            /*
             * \lx test
             */

            var settings = new SolidSettings();

            SfmLexEntry entry = SfmLexEntry.CreateFromText(sfmIn);

            var process = new ProcessStructure(settings);
            var report = new SolidReport();
            SfmLexEntry outputEntry = process.Process(entry, report);

            Assert.AreEqual(0, outputEntry[0].Depth);
            
        }


        [Test]
        public void ProcessStructure_DepthRecursiveTest_CalculatesCorrectly()
        {

            const string sfmIn = @"
\lx a
\xe b";
            //expecting:

            /*
             * \lx a
             *    \sn
             *      \rf
             *        \xe b
             */


            using (var environment = new EnvironmentForTest())
            {
                var settings = environment.SettingsForTest;

                var setting = settings.FindOrCreateMarkerSetting("ge");
                Assert.IsNotNull(setting);
                setting.InferedParent = "";

                SfmLexEntry entry = SfmLexEntry.CreateFromText(sfmIn);

                var process = new ProcessStructure(settings);
                var report = environment.CreateReportForTest();
                var outputEntry = process.Process(entry, report);

                Assert.AreEqual(0, outputEntry[0].Depth);
                Assert.AreEqual(1, outputEntry[1].Depth);
                Assert.AreEqual(2, outputEntry[2].Depth);
                Assert.AreEqual(3, outputEntry[3].Depth);
            }

        }

        [Test]
        public void ProcessStructure_DepthMultiplicityTogetherWithSpacer_CalculatesCorrectly()
        {
            const string sfmIn = @"
\lx test
\aa
\spacer
\aa";
            //expecting:

            /*
             * \lx test
             *    \inferred
             *      \aa
             *      \spacer
             *    \inferred
             *      \aa
             */

            var settings = new SolidSettings();

            SetMarkerSettings(settings, "aa", "inferred", "inferred", MultiplicityAdjacency.MultipleTogether);
            SetMarkerSettings(settings, "inferred", "lx", "", MultiplicityAdjacency.MultipleApart);
            SetMarkerSettings(settings, "spacer", "inferred", "", MultiplicityAdjacency.MultipleApart);

            SfmLexEntry entry = SfmLexEntry.CreateFromText(sfmIn);

            var process = new ProcessStructure(settings);
            var report = new SolidReport();
            SfmLexEntry outputEntry = process.Process(entry, report);

            Assert.AreEqual(0, outputEntry[0].Depth);
            Assert.AreEqual(1, outputEntry[1].Depth);
            Assert.AreEqual(2, outputEntry[2].Depth);
            Assert.AreEqual(2, outputEntry[3].Depth);
            Assert.AreEqual(1, outputEntry[4].Depth); 
            Assert.AreEqual(2, outputEntry[5].Depth);


        }

        [Test]
        public void ProcessStructure_DepthWhenChildCanAppearUnderParentMultipleTogether_CalculateCorrectly()
        {
            const string sfmIn = @"
\lx test2
\cc fire
\cc foo
\sn
\cc bar";

            //expecting:

            /*
             * \lx test2
             *    \bb
             *      \cc fire
             *      \cc foo
             *    \sn
             *    \bb
             *      \cc bar
             */

            using (var environment = new EnvironmentForTest())
            {

                var settings = environment.SettingsForTest;

                var ccSetting = settings.FindOrCreateMarkerSetting("cc");
                ccSetting.StructureProperties.Add(new SolidStructureProperty("bb",
                                                                             MultiplicityAdjacency.MultipleTogether));

                Assert.IsNotNull(settings.FindOrCreateMarkerSetting("cc"));
                ccSetting.InferedParent = "bb";

                SfmLexEntry entry = SfmLexEntry.CreateFromText(sfmIn);

                var process = new ProcessStructure(settings);
                var report = environment.CreateReportForTest();
                var outputEntry = process.Process(entry, report);

                Assert.AreEqual(0, outputEntry[0].Depth);
                Assert.AreEqual(1, outputEntry[1].Depth);
                Assert.AreEqual(2, outputEntry[2].Depth);
                Assert.AreEqual(2, outputEntry[3].Depth);
                Assert.AreEqual(1, outputEntry[4].Depth);
                Assert.AreEqual(1, outputEntry[5].Depth);
                Assert.AreEqual(2, outputEntry[6].Depth);

            }
        }




        // TODO This is an example of another axis of testing. Need to also test the NoInfer with bad input expecting an error for each Multiplicity ??
        [Test]
        public void ProcessStructure_NoInferMultiplicityTogether_SecondFieldReportsError()
        {
            const string sfmIn = @"
\lx test
\aa
\bb
\spacer
\bb";
            //expecting:

            /*
             * \lx test
             *    \aa
             *      \bb
             * \spacer
             * \bb
             */

            var settings = new SolidSettings();

            SetMarkerSettings(settings, "aa", "lx", "", MultiplicityAdjacency.MultipleApart);
            SetMarkerSettings(settings, "spacer", "aa", "", MultiplicityAdjacency.MultipleApart);
            SetMarkerSettings(settings, "bb", "aa", "", MultiplicityAdjacency.MultipleTogether);

            SfmLexEntry outputEntry = SfmLexEntry.CreateFromText(sfmIn);

            var process = new ProcessStructure(settings);
            var report = new SolidReport();
            process.Process(outputEntry, report);

            Assert.AreEqual("lx", outputEntry[0].Marker);
            Assert.AreEqual("aa", outputEntry[1].Marker);
            Assert.AreEqual("bb", outputEntry[2].Marker);
            Assert.AreEqual("spacer", outputEntry[3].Marker);
            Assert.AreEqual("bb", outputEntry[4].Marker);

            Assert.AreEqual("lx", ParentMarkerForField(outputEntry[1]));
            Assert.AreEqual("aa", ParentMarkerForField(outputEntry[2]));
            Assert.AreEqual("aa", ParentMarkerForField(outputEntry[3]));
            Assert.AreEqual(null, ParentMarkerForField(outputEntry[4]));
        }

        [Test]
        public void ProcessStructure_ValidMarkerUnderInferredMarkerAfterError_HasCorrectParent()
        {

            const string sfmIn = @"
\lx a
\ge b";

            //expecting:

            /*
             * \lx a
             * \sn
             *    \ge
             */

            using (var environment = new EnvironmentForTest())
            {

                var settings = new SolidSettings();

                var snSetting = settings.FindOrCreateMarkerSetting("sn");
                snSetting.StructureProperties.Add(new SolidStructureProperty("noparent",
                                                                             MultiplicityAdjacency.MultipleApart));
                snSetting.InferedParent = "";

                var geSetting = settings.FindOrCreateMarkerSetting("ge");
                geSetting.StructureProperties.Add(new SolidStructureProperty("sn", MultiplicityAdjacency.MultipleApart));
                geSetting.InferedParent = "sn";

                SfmLexEntry entry = SfmLexEntry.CreateFromText(sfmIn);

                var process = new ProcessStructure(settings);
                var report = environment.CreateReportForTest();
                var outputEntry = process.Process(entry, report);

                Assert.AreEqual("lx", outputEntry[0].Marker);
                Assert.AreEqual("sn", outputEntry[1].Marker);
                Assert.AreEqual("ge", outputEntry[2].Marker);

                Assert.AreEqual(null, outputEntry[0].Parent);
                Assert.AreEqual(null, outputEntry[1].Parent);
                Assert.AreEqual("sn", ParentMarkerForField(outputEntry[2]));


            }
        }

  

        [Test]
        public void ProcessStructure_MarkerCannotBePlacedInStructureAndNothingCouldBeInferred_GeneratesReport()
        {

            const string sfmIn = @"
\lx a
\sn
\ge g
\zz z";
            //expecting:

            /*
             * \lx a
             *   \sn
             *     \ge g
             * \zz z
             */

            using (var environment = new EnvironmentForTest())
            {
                var settings = new SolidSettings();

                var geSettings = settings.FindOrCreateMarkerSetting("ge");
                geSettings.StructureProperties.Add(new SolidStructureProperty("sn", MultiplicityAdjacency.MultipleApart));
                geSettings.InferedParent = "";

                var snSettings = settings.FindOrCreateMarkerSetting("sn");
                snSettings.StructureProperties.Add(new SolidStructureProperty("lx", MultiplicityAdjacency.MultipleApart));
                snSettings.InferedParent = "";

                SfmLexEntry entry = SfmLexEntry.CreateFromText(sfmIn);

                var process = new ProcessStructure(settings);
                var report = environment.CreateReportForTest();
                var outputEntry = process.Process(entry, report);
               
                Assert.AreEqual(0, outputEntry[0].Depth);
                Assert.AreEqual(1, outputEntry[1].Depth);
                Assert.AreEqual(2, outputEntry[2].Depth);
                Assert.AreEqual(0, outputEntry[3].Depth);

                
                Assert.AreEqual("lx", outputEntry[0].Marker);
                Assert.AreEqual("sn", outputEntry[1].Marker);
                Assert.AreEqual("ge", outputEntry[2].Marker);
                Assert.AreEqual("zz", outputEntry[3].Marker);


                // test that report was generated

                var expectedReportEntry = new ReportEntry(
                    SolidReport.EntryType.StructureParentNotFound,
                    outputEntry,
                    outputEntry[3],
                    string.Format("Marker \\{0} could not be placed in structure, and nothing could be inferred.", outputEntry[3].Marker)
                    );

                Assert.AreEqual(1, report.Count);

                Assert.AreEqual(SolidReport.EntryType.StructureParentNotFound, report.Entries[0].EntryType);
                Assert.AreEqual("zz", report.Entries[0].Marker);
            }
        }

        [Test]
        public void ProcessStructure_MarkerCannotBePlacedInStructure_GeneratesReportEntry()
        {
            const string sfmIn = @"
\lx a
\ge b";

            //expecting:

            /*
             * \lx a
             * \sn
             *    \ge
             */

            using (var environment = new EnvironmentForTest())
            {

                var settings = new SolidSettings();
                
                var snSetting = settings.FindOrCreateMarkerSetting("sn");
                snSetting.StructureProperties.Add(new SolidStructureProperty("noparent", MultiplicityAdjacency.MultipleApart));
                snSetting.InferedParent = "";

                var geSetting = settings.FindOrCreateMarkerSetting("ge");
                geSetting.StructureProperties.Add(new SolidStructureProperty("sn", MultiplicityAdjacency.MultipleApart));
                geSetting.InferedParent = "sn";

                SfmLexEntry entry = SfmLexEntry.CreateFromText(sfmIn);

                var process = new ProcessStructure(settings);
                var report = environment.CreateReportForTest();
                var outputEntry = process.Process(entry, report);

                //depth check

                Assert.AreEqual(0, outputEntry[0].Depth);
                Assert.AreEqual(0, outputEntry[1].Depth);
                Assert.AreEqual(1, outputEntry[2].Depth);


                Assert.AreEqual("lx", outputEntry[0].Marker);
                Assert.AreEqual("sn", outputEntry[1].Marker);
                Assert.AreEqual("ge", outputEntry[2].Marker);

                Assert.AreEqual(null, outputEntry[0].Parent);
                Assert.AreEqual(null, outputEntry[1].Parent);
                Assert.AreEqual("sn", ParentMarkerForField(outputEntry[2]));


                Assert.AreEqual(1, report.Count);

                Assert.AreEqual(SolidReport.EntryType.StructureParentNotFound, report.Entries[0].EntryType);
                Assert.AreEqual("sn", report.Entries[0].Marker);
           }
        }

        [Test]
        public void ProcessStructure_MarkerCannotBePlacedWithMultipleEntries_HasMultpleEntriesInReport()
        {
            const string sfmIn1 = @"
\lx Head1
\xx b1";
            const string sfmIn2 = @"
\lx Head2
\xx b2";

            using (var environment = new EnvironmentForTest())
            {
                var entries = new List<SfmLexEntry>();
                entries.Add(SfmLexEntry.CreateFromText(sfmIn1));
                entries.Add(SfmLexEntry.CreateFromText(sfmIn2));

                var settings = new SolidSettings();
                var process = new ProcessStructure(settings);
                var report = environment.CreateReportForTest();
                foreach (var entry in entries)
                {
                    process.Process(entry, report);
                }

                Assert.AreEqual(2, report.Count);

                Assert.AreEqual(SolidReport.EntryType.StructureParentNotFound, report.Entries[0].EntryType);
                Assert.AreEqual(SolidReport.EntryType.StructureParentNotFound, report.Entries[1].EntryType);
            }
        }
    }
}