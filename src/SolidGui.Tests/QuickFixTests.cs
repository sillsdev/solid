using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Solid.Engine;
using SolidGui.Engine;
using SolidGui.Model;


namespace SolidGui.Tests
{
    [TestFixture]
    public class QuickFixTests
    {

        private List<string> M(params string[] args)
        {
            return new List<string>(args);
        }


        [Test]
        public void AddGuids_NoGuid_AddsOne()
        {
            string s1 = @"\lx \ps";
            string s2 = @"\lx \ps \guid";
            var dict = MakeDictionary(new SolidSettings(), s1 );

            new QuickFixer(dict).AddGuids();
            AssertFieldOrder(dict.Records[0], s2);
        }

        [Test]
        public void AddGuids_HasGuid_DoesNotAddOne()
        {
            string s1 = @"\lx \guid \ps";
            string s2 = s1;
            var dict = MakeDictionary(new SolidSettings(), s1);

            new QuickFixer(dict).AddGuids();
            AssertFieldOrder(dict.Records[0], s2);
        }

        [Test]
        public void MoveCommonItemsUp()
        {
            string s1 = @"\lx \ps \bw";
            string s2 = @"\lx \bw \ps";
            var dict = MakeDictionary(new SolidSettings(), s1);
            
            new QuickFixer(dict).MoveCommonItemsUp(M("lx"),M("bw", "hm"));           
            AssertFieldOrder(dict.Records[0], s2);
        }

        [Test]
        public void MoveCommonItemsUp_FirstTwoFieldsAreTopLevelOnes()
        {
            string s1 = @"\lx \bw \hm";
            string s2 = s1;
            var dict = MakeDictionary(new SolidSettings(), s1);
            new QuickFixer(dict).MoveCommonItemsUp(M("lx"),M("bw", "hm"));
            AssertFieldOrder(dict.Records[0], s2);
        }

        [Test]
        public void MoveCommonItemsUp_NothingToMove()
        {
            string s1 = @"\lx \ps";
            string s2 = s1;
            var dict = MakeDictionary(new SolidSettings(), s1);
            new QuickFixer(dict).MoveCommonItemsUp(M("lx"),M("bw", "hm"));
            AssertFieldOrder(dict.Records[0], s2);
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
            string s1 = @"\lx \a \ge";
            string s2 = s1;
            var dict = MakeDictionary(new SolidSettings(), s1);
            new QuickFixer(dict).MoveCommonItemsUp(M("sn"), M("ge"));
            AssertFieldOrder(dict.Records[0], s2);
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
            string s1 = @"\lx \sn \co b";
            string s2 = s1;
            var dict = MakeDictionary(new SolidSettings(), s1);
            new QuickFixer(dict).RemoveEmptyFields(new List<string>(new[] { "sn" }));
            AssertFieldContents(dict.Records[0], s2);

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
        public void RemoveEmptyFields_MultipleSpecified_AllUsed()
        {
            var dict = MakeDictionary(new SolidSettings(), "lx", "sn", "rf");
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

        [Ignore("But we need this, to support MDF well. -JMC 2014-03")]
        [Test]
        public void MakeEntriesForReferredItems_HomTargetExists_DoesNothing() 
        {
            string s1 = @"\lx a \cf b2 ";
            string s2 = @"\lx b \hm 1 ";
            string s3 = @"\lx b \hm 2";
            var dict = MakeDictionary(new SolidSettings(), s1+s2+s3);
            new QuickFixer(dict).MakeEntriesForReferredItems(M("cf"));
            Assert.AreEqual(3, dict.Records.Count);
            AssertFieldContents(dict.Records[0], s1);
            AssertFieldContents(dict.Records[1], s2);
            AssertFieldContents(dict.Records[2], s3);
        }

        [Ignore("But we need this, to support MDF well. -JMC 2014-03")]
        [Test]
        public void MakeEntriesForReferredItems_SenseTargetExists_DoesNothing()
        {
            string s1 = @"\lx a \cf b 2 ";
            string s2 = @"\lx b \sn 1 \ge x \sn 2 \ge y";
            var dict = MakeDictionary(new SolidSettings(), s1+s2);
            new QuickFixer(dict).MakeEntriesForReferredItems(M("cf"));
            Assert.AreEqual(2, dict.Records.Count);
            AssertFieldContents(dict.Records[0], s1);
            AssertFieldContents(dict.Records[1], s2);
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
            AssertFieldContents(dict.Records[2], "lx x", "ps verb", "CheckMe Created by Solid Quickfix because 'y' referred to it in the \\sy field.");
            AssertFieldContents(dict.Records[3], "lx z", "ps verb", "CheckMe Created by Solid Quickfix because 'y' referred to it in the \\sy field.");
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
            AssertFieldContents(dict.Records[1], "lx b", "ps verb", "CheckMe Created by Solid Quickfix because 'a' referred to it in the \\cf field.");
        }

        [Test]
        public void MakeEntriesForReferredItems_NoPos_FixMePOSCreated()
        {
            var dict = MakeDictionary(new SolidSettings(), "lx a", "cf b");
            new QuickFixer(dict).MakeEntriesForReferredItems(M("cf"));
            AssertFieldContents(dict.Records[0], "lx a", "cf b");
            AssertFieldContents(dict.Records[1], "lx b", "ps FIXME", "CheckMe Created by Solid Quickfix because 'a' referred to it in the \\cf field.");
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
            string s1 = @"\lx a \cf b \lx b \lc x";
            var dict = MakeDictionary(new SolidSettings(), s1); //"lx a", "cf b", "lx b", "lc x");
            new QuickFixer(dict).MakeEntriesForReferredItems(M("cf"));
            Assert.AreEqual(2, dict.Records.Count);
            AssertFieldContents(dict.Records[0], "lx a", "cf x");
            SfmLexEntry e2 = CreateEntry(s1);
            Assert.IsFalse(e2.SameAs(dict.Records[0].LexEntry));
        }

        // Support syntax/usage like the following, which will become an array 
        // of length 4:  @"\lx a \ps noun \sn 1 \ge cat"
        private static string[] splitSingle(string[] fields)
        {
            if (fields.Length == 1)
            {
                fields = fields[0].TrimStart('\\').Split('\\').Select(s => s.Trim()).ToArray();
                //was: fields = fields[0].TrimStart('\\').Split('\\');
            }
            return fields;
        }

        private SfmDictionary MakeDictionary(SolidSettings settings, params string[] fields)
        {
            fields = splitSingle(fields);

            var dictionary = new SfmDictionary();
            for (int i = 0; i < fields.Length; )
            {
                var b = new StringBuilder();
                do
                {
                    b.AppendLine("\\" + fields[i]);  //JMC: probably should globally find all .AppendLine and instead use .Append(SolidSettings.Newline)
                    ++i;
                } while (i < fields.Length && !fields[i].StartsWith("lx"));
 
                var r = new Record();
                r.SetRecordContents(b.ToString(), settings);
                dictionary.AddRecord(r);
            }
            return dictionary;
      }

        // In this case, the expectation is that markers without values are passed in
        private void AssertFieldOrder(Record record, params string[] markers)
        {
            markers = splitSingle(markers);

            for (int i = 0; i < markers.Length; i++)
            {
                Assert.AreEqual(markers[i].Trim(), record.Fields[i].Marker);                
            }
            Assert.AreEqual(markers.Length, record.Fields.Count);
        }

        private SfmLexEntry CreateEntry(params string[] fields)
        {
            fields = splitSingle(fields);

            var sb = new StringBuilder();
            foreach (string f in fields)
            {
                sb.Append("\\");
                sb.Append(f);
                sb.Append(SolidSettings.NewLine);
            }

            string s2 = sb.ToString();
            var lex2 = SfmLexEntry.CreateFromText(s2);
            return lex2;
        }

        // Given a Record, and a string or array of strings representing the fields of a second record, first add
        // newlines to the second one, then make it into a Record and verify that they are the same.
        // Assumptions: this method should insert leading backslashes and trailing field-separating newlines.
        private void AssertFieldContents(Record record,  params string[] fields)
        {
            SfmLexEntry lex2 = CreateEntry(fields);
            Assert.IsTrue(lex2.SameAs(record.LexEntry));

            fields = splitSingle(fields);

            // Do some bonus checking, so that these unit tests will exercise RecordFormatter too.
            Record record2 = new Record(lex2, null);
            var rf = new RecordFormatter();
            rf.SetDefaultsDisk(); //flat is fine
            string out1 = rf.Format(record, null);
            string out2 = rf.Format(record2, null);
            Assert.AreEqual(out1, out2);
            Assert.AreEqual(record.Fields.Count, record2.Fields.Count);

        }

        // Similar, but allows an asterisk to mean "any field value".
        private void AssertFieldContentsWild(Record record, params string[] fields)
        {
            for (int i = 0; i < fields.Length; i++)
            {
                if (fields[i].Trim() != "*")
                {
                    string tmp = record.Fields[i].Marker + " " + record.Fields[i].Value;
                    Assert.AreEqual(tmp, fields[i]);
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
            AssertFieldContentsWild(dict.Records[1], "lx x", "*", "*");  // Is this really useful? Adding more below. -JMC 2014-03
            Assert.IsTrue(dict.Records[1].Fields[1].Marker == "ps");
            Assert.IsTrue(dict.Records[1].Fields[1].Value == "FIXME");
            Assert.IsTrue(dict.Records[1].Fields[2].Marker == "CheckMe");
            Assert.IsTrue(dict.Records[1].Fields[2].Value.StartsWith("Created by"));

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
			Assert.IsTrue(new QuickFixer(dict).LevelHasMarker(dict.Records[0], 1, "ps", new[] { "sn", "se" }));
			Assert.IsFalse(new QuickFixer(dict).LevelHasMarker(dict.Records[0], 4, "ps", new[] { "sn", "se" }));
			Assert.IsFalse(new QuickFixer(dict).LevelHasMarker(dict.Records[0], 6, "ps", new[] { "sn", "se" }));
        }

        [Test]
        public void PropagatePartOfSpeech_SecondSenseLacksPs_Propagated()
        {
            var dict = MakeDictionary(new SolidSettings(),
                        @"\lx a \sn 1 \ps noun \ge cat \sn 2 \ge lion");
            new QuickFixer(dict).PropagatePartOfSpeech();
            AssertFieldContents(dict.Records[0], @"\lx a \sn 1 \ps noun \ge cat \sn 2 \ps noun \ge lion");
        }

        [Test]
        public void PropagatePartOfSpeech_PsBeforeSn_Switches()
        {
            string s1 = @"\lx a \ps noun \sn 1 \ge cat";
            string s2 = @"\lx a \sn 1 \ps noun \ge cat";
            var dict = MakeDictionary(new SolidSettings(), s1);
            new QuickFixer(dict).PropagatePartOfSpeech();
            AssertFieldContents(dict.Records[0], s2);
        }
        
        [Test]
        public void PropagatePartOfSpeech_StopsAtEndOfEntry()
        {
            string s1 = @"\lx a \ps noun \lx b \ge dog";
            string s2 = @"\lx b \ge dog";
            var dict = MakeDictionary(new SolidSettings(), s1);
            new QuickFixer(dict).PropagatePartOfSpeech();
            Assert.AreEqual(2, dict.Records.Count);
            AssertFieldContents(dict.Records[1], s2);
        }

        [Test]
        public void PropagatePartOfSpeech_PsPropgatedToNextSense()
        {
            string s1 = @"\lx a \ps noun \sn 1 \ge cat \sn 2 \ge lion";
            string s2 = @"\lx a \sn 1 \ps noun \ge cat \sn 2 \ps noun \ge lion";
            var dict = MakeDictionary(new SolidSettings(), s1);
            new QuickFixer(dict).PropagatePartOfSpeech();
            AssertFieldContents(dict.Records[0], s2);
        }

        [Test]
        public void PropagatePartOfSpeech_NextSenseHasPOS_DoesntPropagate()
        {
            string s1 = @"\lx a \ps noun \sn 1 \ge cat \sn 2 \ge foo \ps verb";
            string s2 = @"\lx a \sn 1 \ps noun \ge cat \sn 2 \ge foo \ps verb";
            var dict = MakeDictionary(new SolidSettings(), s1);
            new QuickFixer(dict).PropagatePartOfSpeech();
            AssertFieldContents(dict.Records[0], s2);
        }

		[Test] // http://projects.palaso.org/issues/514
		public void PropagatePartOfSpeech_WithSubEntry_PsPropagatesToLexemeLevelSense()
		{
			var dict = MakeDictionary(new SolidSettings(),
						@"\lx a \ps noun \sn 1 \ge cat \se aa \ps verb \sn 2 \ge foo");
			new QuickFixer(dict).PropagatePartOfSpeech();
			AssertFieldContents(dict.Records[0], @"\lx a \sn 1 \ps noun \ge cat \se aa \sn 2 \ps verb \ge foo");
		}

	}
}