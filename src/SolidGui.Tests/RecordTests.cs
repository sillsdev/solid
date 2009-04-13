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
            _record.Fields.Add(new Field("lx", "foo", 0, false, fieldID++));
            _record.Fields.Add(new Field("ps", "noun", 0, false, fieldID++));
            _record.Fields.Add(new Field("ge", "bar", 0, false, fieldID++));
        }

        [TearDown]
        public void TearDown()
        {
 
 
        }


        [Test]
        public void MoveField_FieldIsBelowTarget_MovesAfterGivenIndex()
        {
            var r = new Record(1);
            int fieldID = 0;
            r.Fields.Add(new Field("lx", "foo", 0, false, fieldID++));
            r.Fields.Add(new Field("ps", "noun", 0, false, fieldID++));
            r.Fields.Add(new Field("bw", "bar", 0, false, fieldID++));
            r.Fields.Add(new Field("ge", "bar", 0, false, fieldID++));
            r.MoveField(r.Fields[2], 0);
            Assert.AreEqual("lx",r.Fields[0].Marker);
            Assert.AreEqual("bw", r.Fields[1].Marker);
            Assert.AreEqual("ps", r.Fields[2].Marker);
            Assert.AreEqual("ge", r.Fields[3].Marker);
        }

        [Test]
        public void MoveField_FieldIsAlreadyInPosition_DoesNotMove()
        {
            var r = new Record(1);
            int fieldID = 0;
            r.Fields.Add(new Field("lx", "foo", 0, false, fieldID++));
            r.Fields.Add(new Field("ps", "noun", 0, false, fieldID++));
            r.Fields.Add(new Field("bw", "bar", 0, false, fieldID++));
            r.Fields.Add(new Field("ge", "bar", 0, false, fieldID++));
            r.MoveField(r.Fields[2], 1);
            Assert.AreEqual("lx", r.Fields[0].Marker);
            Assert.AreEqual("ps", r.Fields[1].Marker);
            Assert.AreEqual("bw", r.Fields[2].Marker);
            Assert.AreEqual("ge", r.Fields[3].Marker);
        }

        [Test]
        public void MoveField_FieldHigherThanTarget_MovesAfterGivenIndex()
        {
            var r = new Record(1);
            int fieldID = 0;
            r.Fields.Add(new Field("lx", "foo", 0, false, fieldID++));
            r.Fields.Add(new Field("ps", "noun", 0, false, fieldID++));
            r.Fields.Add(new Field("bw", "bar", 0, false, fieldID++));
            r.Fields.Add(new Field("ge", "bar", 0, false, fieldID++));
            r.MoveField(r.Fields[1], 2);//move ps below bw
            Assert.AreEqual("lx", r.Fields[0].Marker);
            Assert.AreEqual("bw", r.Fields[1].Marker);
            Assert.AreEqual("ps", r.Fields[2].Marker);
            Assert.AreEqual("ge", r.Fields[3].Marker);
        }

        [Test]
        public void FieldCountReturnsCorrectNumberOfFields()
        {
            Assert.AreEqual(3, _record.Fields.Count);
            
        }

        [Test]
        public void HasMarkerIndicateItHasCorrectMarker()
        {
            Assert.IsTrue(_record.HasMarker("lx"));   
        }

        [Test]
        public void HasMarkerIndicatesItDoesntHaveIncorrectMarker()
        {
            Assert.IsFalse(_record.HasMarker(string.Empty));   
        }
    }
}
