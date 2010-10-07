using System;
using System.Collections.Generic;
using NUnit.Framework;
using SolidGui.Model;

namespace SolidGui.Tests
{
    [TestFixture]
    public class RecordNavigatorPresentationModelTests
    {

        private class EnvironmentForTest
        {
            public static SfmDictionary CreateDictionaryWith4Records()
            {
                var dictionary = new SfmDictionary();
                dictionary.AddRecord(new Record());
                dictionary.AddRecord(new Record());
                dictionary.AddRecord(new Record());
                dictionary.AddRecord(new Record());
                return dictionary;
            }

            public void OnNavigatorRecordChanged(object sender, RecordNavigatorPM.RecordChangedEventArgs e)
            {
                RecordWeGotFromRecordChangedChangedEvent = e.Record;
            }

            public Record RecordWeGotFromRecordChangedChangedEvent { get; private set; }

        }

        private static RecordNavigatorPM RecordNavigatorForTest(SfmDictionary dictionary)
        {
            RecordFilter recordFilter = AllRecordFilter.CreateAllRecordFilter(dictionary);
            var navigator = new RecordNavigatorPM();
            navigator.ActiveFilter = recordFilter;
            return navigator;
        }

        [Test]
        public void MoveToNext_DictionaryHas4Records_IndexIncrease1()
        {
            var navigator = RecordNavigatorForTest(EnvironmentForTest.CreateDictionaryWith4Records());
            int startingIndex = navigator.CurrentRecordIndex;
            navigator.MoveToNext();
            int finishIndex = navigator.CurrentRecordIndex;

            Assert.AreEqual(startingIndex, finishIndex - 1);
        }

        [Test]
        public void CanGoPrevious_Index0_False()
        {
            var navigator = RecordNavigatorForTest(EnvironmentForTest.CreateDictionaryWith4Records());
            Assert.IsFalse(navigator.CanGoPrev());
        }

        [Test]
        public void CanGoPrevious_IndexNot0_True()
        {
            var navigator = RecordNavigatorForTest(EnvironmentForTest.CreateDictionaryWith4Records());
            navigator.MoveToNext();
            Assert.IsTrue(navigator.CanGoPrev());
        }

        [Test]
        public void CanGoNext_IndexLast_False()
        {
            var navigator = RecordNavigatorForTest(EnvironmentForTest.CreateDictionaryWith4Records());
            for (int i = 0; i < navigator.Count - 1; i++)
            {
                navigator.MoveToNext();

            }
            Assert.IsFalse(navigator.CanGoNext());
        }

        [Test]
        public void CanGoNext_IndexNotLast_True()
        {
            var navigator = RecordNavigatorForTest(EnvironmentForTest.CreateDictionaryWith4Records());
            Assert.IsTrue(navigator.CanGoNext());
        }

        //current gives current
        [Test]
        public void CurrentIndex_SameIndex()
        {
            var navigator = RecordNavigatorForTest(EnvironmentForTest.CreateDictionaryWith4Records());
            Assert.AreEqual(navigator.CurrentRecordIndex, navigator.CurrentRecordIndex);
        }

        [Test]
        public void InitialCurrentRecordIsCorrectOne()
        {
            //!!!Record correct=navigator.  .MasterRecordList[2];
            var navigator = RecordNavigatorForTest(EnvironmentForTest.CreateDictionaryWith4Records());
            Assert.AreEqual(0, navigator.CurrentRecordIndex);
        }


        [Test, Ignore("This functionality has been changed. Now current record remains")]
        public void WhenFilterChangesShowFirst()
        {
            //navigator.ActiveFilter = new NullRecordFilter();
            var navigator = RecordNavigatorForTest(EnvironmentForTest.CreateDictionaryWith4Records());
            navigator.ActiveFilter = AllRecordFilter.CreateAllRecordFilter(new SfmDictionary());
            Assert.AreEqual(0, navigator.Count);
            Assert.AreEqual(0, navigator.CurrentRecordIndex);
            Assert.IsNull(navigator.CurrentRecord);
        }

        [Test, Ignore("This functionality has been changed. Now current record remains")]
        public void WhenEmptyFilterChangesShowFirst()
        {
            var navigator = RecordNavigatorForTest(EnvironmentForTest.CreateDictionaryWith4Records());
            navigator.ActiveFilter = new NullRecordFilter();
            Assert.AreEqual(0, navigator.Count);
            Assert.AreEqual(0, navigator.CurrentRecordIndex);
            Assert.IsNull (navigator.CurrentRecord);
        }
        
        [Test]
        public void NavigationTriggersCurrentChanged()
        {
            var e = new EnvironmentForTest();
            var navigator = RecordNavigatorForTest(EnvironmentForTest.CreateDictionaryWith4Records());
            navigator.RecordChanged += e.OnNavigatorRecordChanged;
            navigator.MoveToNext();
            Assert.IsNotNull(e.RecordWeGotFromRecordChangedChangedEvent);
            Assert.AreEqual(navigator.CurrentRecord, e.RecordWeGotFromRecordChangedChangedEvent);
        }

    }

}