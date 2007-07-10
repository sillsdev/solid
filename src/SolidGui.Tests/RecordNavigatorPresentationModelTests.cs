using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace SolidGui.Tests
{
    [TestFixture]
    public class RecordNavigatorPresentationModelTests
    {
        private RecordNavigatorPM _navigator;
        private Record _recordWeGotFromRecordChangedChangedEvent;

        [SetUp]
        public void Setup()
        {
            int recordID = 0;
            Dictionary dictionary = new Dictionary();
            Record record = new Record(recordID++);
            dictionary.AddRecord(new Record(recordID++));
            dictionary.AddRecord(new Record(recordID++));
            dictionary.AddRecord(new Record(recordID++));
            dictionary.AddRecord(new Record(recordID++));
            RecordFilter recordFilter = new AllRecordFilter(dictionary);
            _navigator = new RecordNavigatorPM();

            _navigator.ActiveFilter = recordFilter;
                        
        }

        [TearDown]
        public void TearDown()
        {

        }

        [Test]
        public void NextIncreasesIndex()
        {
            int startingIndex = _navigator.CurrentIndexIntoFilteredRecords;
            _navigator.Next();
            int finishIndex = _navigator.CurrentIndexIntoFilteredRecords;

            Assert.AreEqual(startingIndex, finishIndex - 1);
        }

        //can I go previous no at first
        [Test]
        public void CanGoPrevious_Index0_False()
        {
            Assert.IsFalse(_navigator.CanGoPrev());

        }

        [Test]
        public void CanGoPrevious_IndexNot0_True()
        {
            _navigator.Next();
            Assert.IsTrue(_navigator.CanGoPrev());
        }

        [Test]
        public void CanGoNext_IndexLast_False()
        {
            for (int i = 0; i < _navigator.Count - 1; i++)
            {
                _navigator.Next();

            }
            Assert.IsFalse(_navigator.CanGoNext());
        }

        [Test]
        public void CanGoNext_IndexNotLast_True()
        {
            Assert.IsTrue(_navigator.CanGoNext());
        }

        //current gives current
        [Test]
        public void CurrentIndex_SameIndex()
        {
            Assert.AreEqual(_navigator.CurrentIndexIntoFilteredRecords, _navigator.CurrentIndexIntoFilteredRecords);
        }

        [Test]
        public void InitialCurrentRecordIsCorrectOne()
        {
            //!!!Record correct=_navigator.  .MasterRecordList[2];
            Assert.AreEqual(0, _navigator.CurrentIndexIntoFilteredRecords);
        }


        [Test]
        public void WhenFilterChangesShowFirst()
        {
            //_navigator.ActiveFilter = new NullRecordFilter();
            _navigator.ActiveFilter = new AllRecordFilter(new Dictionary());
            Assert.AreEqual(0, _navigator.Count);
            Assert.AreEqual(0, _navigator.CurrentIndexIntoFilteredRecords);
            Assert.IsNull(_navigator.CurrentRecord);
        }

        [Test]
        public void WhenEmptyFilterChangesShowFirst()
        {
            _navigator.ActiveFilter = new NullRecordFilter();
            Assert.AreEqual(0, _navigator.Count);
            Assert.AreEqual(0, _navigator.CurrentIndexIntoFilteredRecords);
            Assert.IsNull (_navigator.CurrentRecord);
        }
        
        [Test]
        public void NavigationTriggersCurrentChanged()
        {
            _navigator.RecordChanged += OnNavigator_RecordChanged;
            _navigator.Next();
            Assert.IsNotNull(_recordWeGotFromRecordChangedChangedEvent);
            Assert.AreEqual(_navigator.CurrentRecord, _recordWeGotFromRecordChangedChangedEvent);
        }

        void OnNavigator_RecordChanged(object sender, RecordNavigatorPM.RecordChangedEventArgs e)
        {
            _recordWeGotFromRecordChangedChangedEvent = e._record;
        }
    }

}