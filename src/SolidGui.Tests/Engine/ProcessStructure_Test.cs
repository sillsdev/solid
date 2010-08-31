using System;
using System.Collections.Generic;
using System.Xml;
using NUnit.Framework;
using SolidGui.Engine;
using SolidGui.Model;
using SolidGui.Processes;


namespace SolidGui.Tests.Engine
{
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

                var rfSetting = _settings.FindOrCreateMarkerSetting("rf");
                rfSetting.StructureProperties.Add(new SolidStructureProperty("sn", MultiplicityAdjacency.MultipleTogether));
                rfSetting.InferedParent = "sn";
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
                process.Process(entry, report);

                Assert.AreEqual(null, entry[0].Parent);
                Assert.AreEqual("bb", ParentMarkerForField(entry[1]));
                Assert.AreEqual("lx", ParentMarkerForField(entry[2]));
                Assert.AreEqual("bb", ParentMarkerForField(entry[3]));

                //Assert.AreEqual("lx", ParentMarkerForField(entry[1]));
                //Assert.AreEqual("bb", ParentMarkerForField(entry[2]));
                //Assert.AreEqual("lx", ParentMarkerForField(entry[3]));

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
                process.Process(entry, report);

                Assert.AreEqual(null, entry[0].Parent);
                Assert.AreEqual("bb", ParentMarkerForField(entry[1]));
                Assert.AreEqual("bb", ParentMarkerForField(entry[2]));
                Assert.AreEqual("lx", ParentMarkerForField(entry[3]));
                Assert.AreEqual("bb", ParentMarkerForField(entry[4]));

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
                process.Process(entry, report);

                Assert.AreEqual(null, entry[0].Parent);
                Assert.AreEqual("bb", ParentMarkerForField(entry[1]));
                Assert.AreEqual("lx", ParentMarkerForField(entry[2]));
                Assert.AreEqual("bb", ParentMarkerForField(entry[3]));
                Assert.AreEqual("lx", ParentMarkerForField(entry[4]));
                Assert.AreEqual("bb", ParentMarkerForField(entry[5]));

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
                entry = process.Process(entry, report);

                Assert.AreEqual(null, entry[0].Parent);
                Assert.AreEqual("bb", ParentMarkerForField(entry[1]));
                Assert.AreEqual("bb", ParentMarkerForField(entry[2]));
                Assert.AreEqual("lx", ParentMarkerForField(entry[3]));
                Assert.AreEqual("bb", ParentMarkerForField(entry[4]));

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
                process.Process(entry, report);

                Assert.AreEqual(null, entry[0].Parent);
                Assert.AreEqual("bb", ParentMarkerForField(entry[1]));
                Assert.AreEqual("bb", ParentMarkerForField(entry[2]));
                Assert.AreEqual("bb", ParentMarkerForField(entry[3]));
                Assert.AreEqual("bb", ParentMarkerForField(entry[4]));
                Assert.AreEqual("bb", ParentMarkerForField(entry[5]));

                Assert.AreEqual("bb", ChildMarkerForField(entry[0], 0));
                
                //cant view children of inferred node
                /*Assert.AreEqual("cc", ChildMarkerForField(entry[1], 1));
                Assert.AreEqual("sn", ChildMarkerForField(entry[1], 2));*/
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
                process.Process(entry, report);

                Assert.AreEqual("lx", ParentMarkerForField(entry[1]));
                Assert.AreEqual("bb", ParentMarkerForField(entry[2]));
                Assert.AreEqual("bb", ParentMarkerForField(entry[3]));

                Assert.AreEqual("bb", ChildMarkerForField(entry[0], 0));
                Assert.AreEqual("cc", ChildMarkerForField(entry[1], 1));
                Assert.AreEqual("sn", ChildMarkerForField(entry[1], 2));
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
                entry = process.Process(entry, report);

                Assert.AreEqual("lx", ParentMarkerForField(entry[1]));
                Assert.AreEqual("sn", ParentMarkerForField(entry[2]));
                
                Assert.AreEqual("sn", ChildMarkerForField(entry[0], 0));
                Assert.AreEqual("ge", ChildMarkerForField(entry[1], 1));

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
                process.Process(entry, report);

                Assert.AreEqual("lx", ParentMarkerForField(entry[1]));
                Assert.AreEqual("sn", ParentMarkerForField(entry[2]));

                Assert.AreEqual("sn", ChildMarkerForField(entry[0], 0));
                Assert.AreEqual("ge", ChildMarkerForField(entry[1], 1));

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
                process.Process(entry, report);

                Assert.AreEqual("lx", ParentMarkerForField(entry[1]));
                Assert.AreEqual("sn", ParentMarkerForField(entry[2]));
                Assert.AreEqual("rf", ParentMarkerForField(entry[3]));

                Assert.AreEqual("sn", ChildMarkerForField(entry[0], 0));
                Assert.AreEqual("rf", ChildMarkerForField(entry[1], 1));
                Assert.AreEqual("xe", ChildMarkerForField(entry[2], 2));
            }

        }

        [Test]
        public void ProcessStructure_RecursiveInferIssue144_MarkersNotDuplicated()
        {
            //const string xmlIn = "<entry record=\"4\"><lx field=\"1\">a</lx><xe field=\"2\">b</xe></entry>";

            const string sfmIn = @"
\lx a
\ge g";

            //expecting:

            /*
             * \lx a
             *    \sn
             *      \rf
             *        \xe b
             */
            
            // TODO which one is the corrent xmlEx?? using the const one
            //string xmlEx = "<entry record=\"4\"><lx field=\"1\"><data>a</data><ps inferred=\"true\"><data /><sn inferred=\"true\"><data /><rf inferred=\"true\"><data /><xe field=\"2\"><data>b</data></xe></rf></sn></ps></lx></entry>";
            //const string xmlEx = "<entry record=\"4\"><lx field=\"1\"><data>a</data><sn inferred=\"true\"><data /><rf inferred=\"true\"><data /><xe field=\"2\"><data>b</data></xe></rf></sn></lx></entry>";

            using (var environment = new EnvironmentForTest())
            {
                var settings = environment.SettingsForTest;

                settings = new SolidSettings();
                var lxSetting = settings.FindOrCreateMarkerSetting("lx");
                lxSetting.StructureProperties.Add(new SolidStructureProperty("entry", MultiplicityAdjacency.Once));

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
                process.Process(entry, report);

                Assert.AreEqual("lx", ParentMarkerForField(entry[1]));
                Assert.AreEqual("sn", ParentMarkerForField(entry[2]));
                Assert.AreEqual("rf", ParentMarkerForField(entry[3]));

                Assert.AreEqual("sn", ChildMarkerForField(entry[0], 0));
                Assert.AreEqual("rf", ChildMarkerForField(entry[1], 1));
                Assert.AreEqual("xe", ChildMarkerForField(entry[2], 2));

            }
        }

        [Test]
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
                process.Process(entry, report);

                Assert.AreEqual("lx", ParentMarkerForField(entry[1]));
      
                Assert.AreEqual("xe", ChildMarkerForField(entry[0], 0));
         
            }
        }

        [Test]
        public void ProcessStructure_NoInferInsertAnyway_Correct()
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
             *    \xe b
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
                process.Process(entry, report);

                Assert.AreEqual("lx", ParentMarkerForField(entry[1]));
                Assert.AreEqual("sn", ParentMarkerForField(entry[2]));
                Assert.AreEqual("rf", ParentMarkerForField(entry[3]));

                Assert.AreEqual("sn", ChildMarkerForField(entry[0], 0));
                Assert.AreEqual("rf", ChildMarkerForField(entry[1], 1));
                Assert.AreEqual("xe", ChildMarkerForField(entry[2], 2));
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
             *    \xx xx
             *    \yy yy
             *    \zz zz
             */

            //const string xmlEx = "<entry record=\"4\"><lx field=\"1\"><data>a</data><xx field=\"2\"><data>xx</data></xx><yy field=\"3\"><data>yy</data></yy><zz field=\"4\"><data>zz</data></zz></lx></entry>";


            using (var environment = new EnvironmentForTest())
            {
                var settings = environment.SettingsForTest;

                SfmLexEntry entry = SfmLexEntry.CreateFromText(sfmIn);

                var process = new ProcessStructure(settings);
                var report = environment.CreateReportForTest();
                process.Process(entry, report);

                Assert.AreEqual("lx", ParentMarkerForField(entry[1]));
                Assert.AreEqual("lx", ParentMarkerForField(entry[2]));
                Assert.AreEqual("lx", ParentMarkerForField(entry[3]));

                Assert.AreEqual("xx", ChildMarkerForField(entry[0], 0));
                Assert.AreEqual("yy", ChildMarkerForField(entry[0], 1));
                Assert.AreEqual("zz", ChildMarkerForField(entry[0], 2));
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
                SfmLexEntry outputEntry = process.Process(entry, report);

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
                Assert.AreEqual("bb", outputEntry[1]);
                Assert.AreEqual(entry[1], outputEntry[2]);
                Assert.AreEqual(entry[2], outputEntry[3]);
                Assert.AreEqual(entry[3], outputEntry[4]);
                Assert.AreEqual(entry[4], outputEntry[5]);
                Assert.AreEqual(entry[5], outputEntry[6]);

            }

        }

    

    }
}