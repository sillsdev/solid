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
            List<String> fields = new List<string>();
            fields.Add("\\lx foo");
            fields.Add("\\ps noun");
            fields.Add("\\ge bar");

            _record = new Record(fields);
        }

        [TearDown]
        public void TearDown()
        {
 
 
        }
 

        
        [Test]
        public void FieldCountReturnsCorrectNumberOfFields()
        {
            Assert.AreEqual(3, _record.FieldCount);
            
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
