using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace SolidGui.Tests
{
    [TestFixture]
    public class RecordNavigatorPresentationModelTests
    {
        private RecordNavigatorPM _navigator;
        private Record _recordWeGotFromRecordChagnedChangedEvent;


        [SetUp]
        public void Setup()
        {
            _navigator = new RecordNavigatorPM();

            List<Record> masterRecordList = new List<Record>();
            masterRecordList.Add(new Record("something0"));
            masterRecordList.Add(new Record("something1"));
            masterRecordList.Add(new Record("something2 X"));
            masterRecordList.Add(new Record("something3 X"));

            _navigator.MasterRecordList = masterRecordList;

            _navigator.ActiveFilter = new RegExRecordFilter("Has X", "X",masterRecordList);
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

            Assert.AreEqual(finishIndex - 1, startingIndex);
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
            _navigator.Next();
            _navigator.Next();
            _navigator.Next();
            
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
            Record correct=_navigator.MasterRecordList[2];
            Assert.AreEqual(correct, _navigator.CurrentRecord);
        }


        [Test]
        public void WhenFilterChangesShowFirst()
        {
            _navigator.ActiveFilter = new NullRecordFilter();
            _navigator.ActiveFilter = new AllRecordFilter(new List<Record>(_navigator.MasterRecordList));
            Assert.AreEqual(4, _navigator.Count);
            Assert.AreEqual(0, _navigator.CurrentIndexIntoFilteredRecords);
            Assert.IsNotNull(_navigator.CurrentRecord);
        }

        [Test]
        public void WhenEmptyFilterChangesShowFirst()
        {
            _navigator.ActiveFilter = new NullRecordFilter();
            Assert.AreEqual(0, _navigator.Count);
            Assert.AreEqual(-1, _navigator.CurrentIndexIntoFilteredRecords);
            Assert.IsNull (_navigator.CurrentRecord);
        }
        
        [Test]
        public void NavigationTriggersCurrentChanged()
        {
            _navigator.RecordChanged += OnNavigator_RecordChanged;
            _navigator.Next();
            Assert.IsNotNull(_recordWeGotFromRecordChagnedChangedEvent);
            Assert.AreEqual(_navigator.CurrentRecord, _recordWeGotFromRecordChagnedChangedEvent);
        }

        void OnNavigator_RecordChanged(object sender, RecordNavigatorPM.RecordChangedEventArgs e)
        {
            _recordWeGotFromRecordChagnedChangedEvent = e._record;
        }
    }

}