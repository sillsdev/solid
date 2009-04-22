using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using NUnit.Framework;
using SolidEngine;

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
            new QuickFixer(dict).RemoveEmptyFields(new List<string>(new []{ "co" }));
            AssertFieldOrder(dict.Records[0], "lx", "ps");
            
        }
        [Test]
        public void RemoveEmptyFields_FirstLineIsEmpty_Ok()
        {
            var dict = MakeDictionary(new SolidSettings(), "lx", "ps", "co");
            new QuickFixer(dict).RemoveEmptyFields(new List<string>(new []{ "ps" }));
            AssertFieldOrder(dict.Records[0], "lx", "co");
            
        }
        [Test]
        public void RemoveEmptyFields_NotEmpty_NotTouched()
        {
            var dict = MakeDictionary(new SolidSettings(), "lx", "ps noun", "co");
            new QuickFixer(dict).RemoveEmptyFields(new List<string>(new[] { "ps" }));
            AssertFieldOrder(dict.Records[0], "lx", "ps", "co");

        }

        [Test]
        public void RemoveEmptyFields_MultipleSpecifiec_AllUsed()
        {
            var dict = MakeDictionary(new SolidSettings(), "lx", "a", "b", "b", "c", "d");
            new QuickFixer(dict).RemoveEmptyFields(new List<string>(new[] { "a", "b", "c", }));
            AssertFieldOrder(dict.Records[0], "lx", "d");

        }

        [Test]
        public void MakeInferedMarkersReal_hasVirtualSn_Becomes_Real()
        {
            SolidMarkerSetting psSetting = new SolidMarkerSetting("ps");
            psSetting.InferedParent = "sn";
            psSetting.StructureProperties.Add(new SolidStructureProperty("sn", MultiplicityAdjacency.MultipleApart));
            var settings = new SolidSettings();
            settings.MarkerSettings.Add(psSetting);

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
        public void MakeEntriesForReferredItems_HasDifferentLc_ReferrerSwitchedToIt()
        {
            var dict = MakeDictionary(new SolidSettings(), "lx a", "cf b", "lx b", "lc x");
            new QuickFixer(dict).MakeEntriesForReferredItems(M("cf"));
            Assert.AreEqual(2, dict.Records.Count);
            AssertFieldContents(dict.Records[0], "lx a", "cf x");
        }


        private SfmDictionary MakeDictionary(SolidSettings settings, params string[] fields)
        {
            var dictionary = new SfmDictionary();
            for (int i = 0; i < fields.Length; )
            {
                var b = new StringBuilder();
                do
                {
                    b.AppendLine("\\" + fields[i]);
                    ++i;
                } while (i < fields.Length && !fields[i].StartsWith("lx"));
 
                var r = new Record(i);
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
            for (int i = 0; i < fields.Length; i++)
            {
                Assert.AreEqual("\\"+fields[i], record.Fields[i].ToStructuredString());
            }
            Assert.AreEqual(fields.Length, record.Fields.Count);
        }

    }
}