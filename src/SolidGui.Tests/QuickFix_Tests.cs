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
        private List<string> _markers = new List<string>(new string[] { "bw", "hm" });

        [Test]
        public void MoveCommonItemsUp()
        {
            var dict = MakeDictionary("lx", "ps", "bw");
            
            new QuickFixer(dict).MoveCommonItemsUp(_markers);           
            AssertFieldOrder(dict.Records[0],"lx", "bw","ps");
        }

        [Test]
        public void MoveCommonItemsUp_FirstTwoFieldsAreTopLevelOnes()
        {
            var dict = MakeDictionary("lx", "bw", "hm");
            new QuickFixer(dict).MoveCommonItemsUp(_markers);
            AssertFieldOrder(dict.Records[0], "lx", "bw", "hm");
        }

        [Test]
        public void MoveCommonItemsUp_NothingToMove()
        {
            var dict = MakeDictionary("lx", "ps");
            new QuickFixer(dict).MoveCommonItemsUp(_markers);
            AssertFieldOrder(dict.Records[0], "lx", "ps");
        }

        [Test]
        public void MoveCommonItemsUp_OnlyHasLx()
        {
            var dict = MakeDictionary("lx");
            new QuickFixer(dict).MoveCommonItemsUp(_markers);
            AssertFieldOrder(dict.Records[0], "lx");
        }

        [Test]
        public void RemoveEmptyFields_LastOne_Ok()
        {
            var dict = MakeDictionary("lx", "ps", "co");
            new QuickFixer(dict).RemoveEmptyFields(new List<string>(new []{ "co" }));
            AssertFieldOrder(dict.Records[0], "lx", "ps");
            
        }
        [Test]
        public void RemoveEmptyFields_FirstLineIsEmpty_Ok()
        {
            var dict = MakeDictionary("lx", "ps", "co");
            new QuickFixer(dict).RemoveEmptyFields(new List<string>(new []{ "ps" }));
            AssertFieldOrder(dict.Records[0], "lx", "co");
            
        }
        [Test]
        public void RemoveEmptyFields_NotEmpty_NotTouched()
        {
            var dict = MakeDictionary("lx", "ps noun", "co");
            new QuickFixer(dict).RemoveEmptyFields(new List<string>(new[] { "ps" }));
            AssertFieldOrder(dict.Records[0], "lx", "ps", "co");

        }

        [Test]
        public void RemoveEmptyFields_MultipleSpecifiec_AllUsed()
        {
            var dict = MakeDictionary("lx", "a", "b", "b", "c", "d");
            new QuickFixer(dict).RemoveEmptyFields(new List<string>(new[] { "a", "b", "c", }));
            AssertFieldOrder(dict.Records[0], "lx", "d");

        }

        private SfmDictionary MakeDictionary(params string[] fields)
        {
            var b = new StringBuilder();
            foreach (var s in fields)
            {
                b.AppendLine("\\" + s);
            }

            var dictionary = new SfmDictionary();
            var r = new Record(1);
            SolidSettings solidSettings = new SolidSettings();

            r.SetRecordContents(b.ToString(), solidSettings);
            dictionary.AddRecord(r);
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

    }
}