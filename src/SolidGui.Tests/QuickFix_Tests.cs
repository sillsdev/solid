using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Solid.Engine;
using SolidGui.Engine;
using SolidGui.Model;


namespace SolidGui.Tests
{
    [TestFixture]
    public class QuickFix_Tests
    {

        private List<string> M(params string[] args)
        {
            return new List<string>(args);
        }


        [Test]
        public void AddGuids_NoGuid_AddsOne()
        {
            var dict = MakeDictionary(new SolidSettings(), "lx", "ps" );

            new QuickFixer(dict).AddGuids();
            AssertFieldOrder(dict.Records[0], "lx", "ps", "guid");
        }

        [Test]
        public void AddGuids_HasGuid_DoesNotAddOne()
        {
            var dict = MakeDictionary(new SolidSettings(), "lx", "guid", "ps");

            new QuickFixer(dict).AddGuids();
            AssertFieldOrder(dict.Records[0], "lx", "guid", "ps");
        }

        [Test]
        public void MoveCommonItemsUp()
        {
            var dict = MakeDictionary(new SolidSettings(), "lx", "ps", "bw");
            
            new QuickFixer(dict).MoveCommonItemsUp(M("lx"),M("bw", "hm"));           
            AssertFieldOrder(dict.Records[0],"lx", "bw","ps");
        }

        [Test]
        public void MoveCommonItemsUp_FirstTwoFieldsAreTopLevelOnes()
        {
            var dict = MakeDictionary(new SolidSettings(), "lx", "bw", "hm");
            new QuickFixer(dict).MoveCommonItemsUp(M("lx"),M("bw", "hm"));
            AssertFieldOrder(dict.Records[0], "lx", "bw", "hm");
        }

        [Test]
        public void MoveCommonItemsUp_NothingToMove()
        {
            var dict = MakeDictionary(new SolidSettings(), "lx", "ps");
            new QuickFixer(dict).MoveCommonItemsUp(M("lx"),M("bw", "hm"));
            AssertFieldOrder(dict.Records[0], "lx", "ps");
        }

        [Test]
        public void MoveCommonItemsUp_OnlyHasLx()
        {
            var dict = MakeDictionary(new SolidSettings(), "lx");
            new QuickFixer(dict).MoveCommonItemsUp(M("lx"),M("bw", "hm"));
            AssertFieldOrder(dict.Records[0], "lx");
        }


        [Test]
        public void MoveCommonItemsUp_HasSubEntry_MultipleMovedUpToSubEntry()
        {
            var dict = MakeDictionary(new SolidSettings(), "lx", "a", "se", "b", "p1", "p2");
            new QuickFixer(dict).MoveCommonItemsUp(M("lx", "se"), M("p1", "p2"));
            AssertFieldOrder(dict.Records[0], "lx", "a", "se", "p1", "p2", "b");
        }

        [Test]
        public void MoveCommonItemsUp_SomeToEntrySomeToSubEntry()
        {
            var dict = MakeDictionary(new SolidSettings(), "lx", "a", "ph", "se", "b", "ph", "c");
            new QuickFixer(dict).MoveCommonItemsUp(M("lx", "se"), M("ph"));
            AssertFieldOrder(dict.Records[0], "lx", "ph", "a", "se", "ph","b", "c");
        }

        [Test]
        public void MoveCommonItemsUp_MoveToSnButNoSn_DoesntMove()
        {
            var dict = MakeDictionary(new SolidSettings(), "lx", "a", "ge");
            new QuickFixer(dict).MoveCommonItemsUp(M("sn"), M("ge"));
            AssertFieldOrder(dict.Records[0], "lx", "a", "ge");
        }

        [Test]
        public void RemoveEmptyFields_LastOne_Ok()
        {
            var dict = MakeDictionary(new SolidSettings(), "lx", "ps", "co");
            new QuickFixer(dict).RemoveEmptyFields(new List<string>(new []{ "lx", "ps" }));
            AssertFieldOrder(dict.Records[0], "lx", "ps");
            
        }

        [Test]
        public void RemoveEmptyFields_LxIsEmptyButLxNotSpecified_StillLeavesLx()
        {
            var dict = MakeDictionary(new SolidSettings(), "lx", "sn", "co b");
            new QuickFixer(dict).RemoveEmptyFields(new List<string>(new[] { "sn" }));
            AssertFieldContents(dict.Records[0], "lx", "sn", "co b");

        }

        [Test]
        public void RemoveEmptyFields_NoExceptionsSpecified_Ok()
        {
            var dict = MakeDictionary(new SolidSettings(), "lx a", "sn", "co b");
            new QuickFixer(dict).RemoveEmptyFields(new List<string>());
            AssertFieldContents(dict.Records[0], "lx a", "co b");

        }

        [Test]
        public void RemoveEmptyFields_FirstLineIsEmpty_Ok()
        {
            var dict = MakeDictionary(new SolidSettings(), "lx a", "sn", "co b");
            new QuickFixer(dict).RemoveEmptyFields(new List<string>(new []{ "lx sn" }));
            AssertFieldOrder(dict.Records[0], "lx", "co");
            
        }
        [Test]
        public void RemoveEmptyFields_NotEmpty_NotTouched()
        {
            var dict = MakeDictionary(new SolidSettings(), "lx", "ps noun", "co x");
            new QuickFixer(dict).RemoveEmptyFields(new List<string>(new[] { "sn" }));
            AssertFieldOrder(dict.Records[0], "lx", "ps", "co");

        }

        [Test]
        public void RemoveEmptyFields_MultipleSpecifiec_AllUsed()
        {
            var dict = MakeDictionary(new SolidSettings(), "lx","sn", "rf");
            new QuickFixer(dict).RemoveEmptyFields(new List<string>(new[] { "sn", "rf"}));
            AssertFieldOrder(dict.Records[0], "lx", "sn", "rf");

        }

        [Test]
        public void MakeInferedMarkersReal_hasVirtualSn_Becomes_Real()
        {
            var settings = new SolidSettings();
            var psSetting = settings.FindOrCreateMarkerSetting("ps");
            psSetting.InferedParent = "sn";
            psSetting.StructureProperties.Add(new SolidStructureProperty("sn", MultiplicityAdjacency.MultipleApart));
            var snSetting = settings.FindOrCreateMarkerSetting("sn");
            snSetting.StructureProperties.Add(new SolidStructureProperty("lx", MultiplicityAdjacency.MultipleApart));

            var dict = MakeDictionary(settings, "lx", "ps");
            AssertFieldOrder(dict.Records[0], "lx", "sn", "ps");
            Assert.IsTrue(dict.Records[0].Fields[1].Inferred);
            new QuickFixer(dict).MakeInferedMarkersReal(M("sn"));
            AssertFieldOrder(dict.Records[0], "lx", "sn", "ps");
            Assert.IsFalse(dict.Records[0].Fields[1].Inferred);
        }

        [Test]
        public void MakeEntriesForReferredItems_TargetExists_DoesNothing()
        {
            var dict = MakeDictionary(new SolidSettings(), "lx a", "cf b", "lx b");
            new QuickFixer(dict).MakeEntriesForReferredItems(M("cf"));
            Assert.AreEqual(2, dict.Records.Count);
            AssertFieldContents(dict.Records[0], "lx a", "cf b");
            AssertFieldContents(dict.Records[1], "lx b");
        }

        [Test]
        public void MakeEntriesForReferredItems_SyWithNoValue_DoesNothing()
        {
            var dict = MakeDictionary(new SolidSettings(), "lx a", "sy");
            new QuickFixer(dict).MakeEntriesForReferredItems(M("sy"));
            Assert.AreEqual(1, dict.Records.Count);
            AssertFieldContents(dict.Records[0], "lx a", "sy");
        }

        
        [Test]
        public void MakeEntriesForReferredItems_HasThreeItemsTwoOfWhichAreMissing_TwoAdded()
        {
            var dict = MakeDictionary(new SolidSettings(), 
                        "lx a", "ps noun", 
                        "lx y", "ps verb", "sy x,y,z");
            new QuickFixer(dict).MakeEntriesForReferredItems(M("sy"));
            AssertFieldContents(dict.Records[0], "lx a", "ps noun");
            AssertFieldContents(dict.Records[1], "lx y", "ps verb", "sy x", "sy y", "sy z");
            AssertFieldContents(dict.Records[2], "lx x", "ps verb", "CheckMe Created by SOLID Quickfix because 'y' referred to it in the \\sy field.");
            AssertFieldContents(dict.Records[3], "lx z", "ps verb", "CheckMe Created by SOLID Quickfix because 'y' referred to it in the \\sy field.");
        }

        [Test]
        public void MakeEntriesForReferredItems_HasTwoItems_GetSplitIntoTwoFields()
        {
            var dict = MakeDictionary(new SolidSettings(),
                        "lx a", "sy x,y");
            new QuickFixer(dict).MakeEntriesForReferredItems(M("sy"));
            AssertFieldContents(dict.Records[0], "lx a","sy x", "sy y");
        }



        [Test]
        public void MakeEntriesForReferredItems_HasPos_POSCopied()
        {
            var dict = MakeDictionary(new SolidSettings(), "lx a", "ps verb", "cf b");
             new QuickFixer(dict).MakeEntriesForReferredItems(M("cf"));
             AssertFieldContents(dict.Records[0], "lx a", "ps verb", "cf b");
            AssertFieldContents(dict.Records[1], "lx b", "ps verb", "CheckMe Created by SOLID Quickfix because 'a' referred to it in the \\cf field.");
        }

        [Test]
        public void MakeEntriesForReferredItems_NoPos_FixMePOSCreated()
        {
            var dict = MakeDictionary(new SolidSettings(), "lx a", "cf b");
            new QuickFixer(dict).MakeEntriesForReferredItems(M("cf"));
            AssertFieldContents(dict.Records[0], "lx a", "cf b");
            AssertFieldContents(dict.Records[1], "lx b", "ps FIXME", "CheckMe Created by SOLID Quickfix because 'a' referred to it in the \\cf field.");
        }

        [Test]
        public void MakeEntriesForReferredItems_HasMatchingLc_NoneCreated()
        {
            var dict = MakeDictionary(new SolidSettings(), "lx a", "cf b", "lx x", "lc b");
            new QuickFixer(dict).MakeEntriesForReferredItems(M("cf"));
            Assert.AreEqual(2, dict.Records.Count);
        }

        [Test]
        public void MakeEntriesForReferredItems_HasMatchingVa_NoneCreated()
        {
            var dict = MakeDictionary(new SolidSettings(), "lx a", "cf b", "lx x", "va b");
            new QuickFixer(dict).MakeEntriesForReferredItems(M("cf"));
            Assert.AreEqual(2, dict.Records.Count);
        }

        [Test]
        public void MakeEntriesForReferredItems_HasMatchingSe_NoneCreated()
        {
            var dict = MakeDictionary(new SolidSettings(), "lx a", "cf b", "lx x", "se b");
            new QuickFixer(dict).MakeEntriesForReferredItems(M("cf"));
            Assert.AreEqual(2, dict.Records.Count);
        }

        [Test]
        public void MakeEntriesForReferredItems_HasDifferentLc_ReferrerSwitchedToIt()
        {
            var dict = MakeDictionary(new SolidSettings(), "lx a", "cf b", "lx b", "lc x");
            new QuickFixer(dict).MakeEntriesForReferredItems(M("cf"));
            Assert.AreEqual(2, dict.Records.Count);
            AssertFieldContents(dict.Records[0], "lx a", "cf x");
        }


        private SfmDictionary MakeDictionary(SolidSettings settings, params string[] fields)
        {
            if (fields.Length == 1)  //enable usage like this:  @"\lx a \ps noun \sn 1 \ge cat"
            {
                fields = fields[0].TrimStart('\\').Split('\\');
            }

            var dictionary = new SfmDictionary();
            for (int i = 0; i < fields.Length; )
            {
                var b = new StringBuilder();
                do
                {
                    b.AppendLine("\\" + fields[i]);
                    ++i;
                } while (i < fields.Length && !fields[i].StartsWith("lx"));
 
                var r = new Record();
                r.SetRecordContents(b.ToString(), settings);
                dictionary.AddRecord(r);
            }
            return dictionary;
      }

        private void AssertFieldOrder(Record record, params string[] markers)
        {
            for (int i = 0; i < markers.Length; i++)
            {
                Assert.AreEqual(markers[i], record.Fields[i].Marker);                
            }
            Assert.AreEqual(markers.Length, record.Fields.Count);
        }

        private void AssertFieldContents(Record record,  params string[] fields)
        {
            if (fields.Length == 1)  //enable usage like this:  @"\lx a \ps noun \sn 1 \ge cat"
            {
                fields = fields[0].Trim('\\').Split('\\');
            }

            for (int i = 0; i < fields.Length; i++)
            {
                if(fields[i]!="*")
                {
                    Assert.AreEqual("\\"+fields[i].Trim(), record.Fields[i].ToStructuredString().Trim());
                }
            }
            Assert.AreEqual(fields.Length, record.Fields.Count);
        }

        [Test]
        public void MakeEntriesForReferredItemsOfLv_TargetMissing_TargetAdded()
        {
            var dict = MakeDictionary(new SolidSettings(),
                        "lx a", "lf foo", "lv x");
            new QuickFixer(dict).MakeEntriesForReferredItemsOfLv();
            Assert.AreEqual(2, dict.Records.Count);
            AssertFieldContents(dict.Records[0], "lx a", "lf foo", "lv x");
            AssertFieldContents(dict.Records[1], "lx x", "*", "*");
        }

        [Test]
        public void MakeEntriesForReferredItemsOfLv_LfFollowedByMultipleLvs_LFsAdded()
        {
            var dict = MakeDictionary(new SolidSettings(),
                        "lx a", "lf foo", "lv x", "lv y");
            new QuickFixer(dict).MakeEntriesForReferredItemsOfLv();
            Assert.AreEqual(3, dict.Records.Count);
            AssertFieldContents(dict.Records[0], "lx a", "lf foo", "lv x", "lf foo", "lv y");
        }

        [Test]
        public void MakeEntriesForReferredItemsOfLv_FollowingLvAlreadyHasLf_Untouched()
        {
            var dict = MakeDictionary(new SolidSettings(),
                        "lx a", "lf foo", "lv x", "lf boo", "lv y");
            new QuickFixer(dict).MakeEntriesForReferredItemsOfLv();
            Assert.AreEqual(3, dict.Records.Count);
            AssertFieldContents(dict.Records[0], "lx a", "lf foo", "lv x", "lf boo", "lv y");
        }
        [Test]
        public void MakeEntriesForReferredItemsOfLv_SameMissingTargetRepeated_OnlyCreatedOnce()
        {
            var dict = MakeDictionary(new SolidSettings(),
                        "lx a", "lf foo", "lv x", "lf boo", "lv x");
            new QuickFixer(dict).MakeEntriesForReferredItemsOfLv();
            Assert.AreEqual(2, dict.Records.Count);
        }

        //-------------------------------------------------------------------------------------

        [Test]
        public void LevelHasMarker_DefaultSettings_FindsPsInSenses()
        {
            var dict = MakeDictionary(new SolidSettings(),
                        @"\lx a \sn 1 \ge cat \ps foo \sn 2 \ge goo \s3 zz");
            Assert.IsTrue(new QuickFixer(dict).LevelHasMarker(dict.Records[0], 1,  "ps", "sn"));
            Assert.IsFalse(new QuickFixer(dict).LevelHasMarker(dict.Records[0], 4, "ps", "sn"));
            Assert.IsFalse(new QuickFixer(dict).LevelHasMarker(dict.Records[0], 6, "ps", "sn"));
        }

        [Test]
        public void PropogatePartOfSpeech_SecondSenseLacksPs_Propogated()
        {
            var dict = MakeDictionary(new SolidSettings(),
                        @"\lx a \sn 1 \ps noun \ge cat \sn 2 \ge lion");
            new QuickFixer(dict).PropogatePartOfSpeech();
            AssertFieldContents(dict.Records[0], @"\lx a \sn 1 \ps noun \ge cat \sn 2 \ps noun \ge lion");
        }

        [Test]
        public void PropogatePartOfSpeech_PsBeforeSn_Switches()
        {
            var dict = MakeDictionary(new SolidSettings(),
                        @"\lx a \ps noun \sn 1 \ge cat");
            new QuickFixer(dict).PropogatePartOfSpeech();
            AssertFieldContents(dict.Records[0], @"\lx a \sn 1 \ps noun  \ge cat");
        }
        
        [Test]
        public void PropogatePartOfSpeech_StopsAtEndOfEntry()
        {
            var dict = MakeDictionary(new SolidSettings(),
                        @"\lx a \ps noun \lx b \ge dog");
            new QuickFixer(dict).PropogatePartOfSpeech();
            Assert.AreEqual(2, dict.Records.Count);
            AssertFieldContents(dict.Records[1], "lx b", "ge dog");
        }

        [Test]
        public void PropogatePartOfSpeech_PsPropgatedToNextSense()
        {
            var dict = MakeDictionary(new SolidSettings(),
                        @"\lx a \ps noun \sn 1 \ge cat \sn 2 \ge lion");
            new QuickFixer(dict).PropogatePartOfSpeech();
            AssertFieldContents(dict.Records[0], @"\lx a \sn 1 \ps noun \ge cat \sn 2 \ps noun \ge lion");
        }

        [Test]
        public void PropogatePartOfSpeech_NextSenseHasPOS_DoesntPropogate()
        {
            var dict = MakeDictionary(new SolidSettings(),
                        @"\lx a \ps noun \sn 1 \ge cat \sn 2 \ge foo \ps verb");
            new QuickFixer(dict).PropogatePartOfSpeech();
            AssertFieldContents(dict.Records[0], @"\lx a \sn 1 \ps noun \ge cat \sn 2 \ge foo  \ps verb");
        }
    }
}