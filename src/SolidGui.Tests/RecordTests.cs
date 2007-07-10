using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace SolidGui.Tests
{
    [TestFixture]
    public class RecordTests
    {
        private Record _record;

        [SetUp]
        public void SetUp()
        {
            _record = new Record(1);
            int fieldID = 0;
            _record.Fields.Add(new Record.Field("\\lx", "foo", 0, false, fieldID++));
            _record.Fields.Add(new Record.Field("\\ps", "noun", 0, false, fieldID++));
            _record.Fields.Add(new Record.Field("\\ge", "bar", 0, false, fieldID++));
        }

        [TearDown]
        public void TearDown()
        {
 
 
        }
 

        
        [Test]
        public void FieldCountReturnsCorrectNumberOfFields()
        {
            Assert.AreEqual(3, _record.Fields.Count);
            
        }

        [Test]
        public void HasMarkerIndicateItHasCorrectMarker()
        {
            Assert.IsTrue(_record.HasMarker(""));   
        }

        [Test]
        public void HasMarkerIndicatesItDoesntHaveIncorrectMarker()
        {
            
        }
    }
}
