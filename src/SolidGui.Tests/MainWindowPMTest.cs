using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace SolidGui.Tests
{
    [TestFixture]
    public class MainWindowPMTest
    {
        private MainWindowPM _mainWindowPM;
        
        [SetUp]
        public void Setup()
        {
            _mainWindowPM = new MainWindowPM();
        }

        [TearDown]
        public void TearDown()
        {

        }

        [Test]
        public void RecordFilter_RecordFilterList_ReturnsList()
        {
            Assert.IsNotNull(_mainWindowPM.RecordFilters);
        }

    }

}