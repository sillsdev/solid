using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace SolidGui.Tests
{
    [TestFixture]
    public class RecordNavigatorPresentationModelTests
    {
        private RecordNavigatorPM _navigator;
        private String _recordWeGotFromRecordChagnedChangedEvent;


        [SetUp]
        public void Setup()
        {
            _navigator = new RecordNavigatorPM();
            List<string> masterRecordList = new List<string>();
            masterRecordList.Add("something0");
            masterRecordList.Add("something1");
            masterRecordList.Add("something2");
            masterRecordList.Add("something3");

            _navigator.MasterRecordList = masterRecordList;

            _navigator.ActiveFilter = new RecordFilter();
        }

        [TearDown]
        public void TearDown()
        {

        }

        [Test]
        public void NextIncreasesIndex()
        {
            int startingIndex = _navigator.CurrentIndex;
            _navigator.Next();
            int finishIndex = _navigator.CurrentIndex;

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
            Assert.AreEqual(_navigator.CurrentIndex, _navigator.CurrentIndex);
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
            _recordWeGotFromRecordChagnedChangedEvent = e.Record;
        }
    }

}