using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using SolidGui.Model;

namespace SolidGui.Tests
{
    [TestFixture]
    public class RecordTests
    {
        private static Record CreateDefaultRecord()
        {
            return new Record();
        }


        [Test]
        public void MoveField_FieldIsBelowTarget_MovesAfterGivenIndex()
        {
            var r = CreateDefaultRecord();
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
            var r = CreateDefaultRecord();
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
            var r = CreateDefaultRecord();
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
            var record = CreateDefaultRecord();
            int fieldID = 0;
            record.Fields.Add(new Field("lx", "foo", 0, false, fieldID++));
            record.Fields.Add(new Field("ps", "noun", 0, false, fieldID++));
            record.Fields.Add(new Field("ge", "bar", 0, false, fieldID++));
            Assert.AreEqual(3, record.Fields.Count);
            
        }

        [Test]
        public void HasMarkerIndicateItHasCorrectMarker()
        {
            var record = CreateDefaultRecord();
            int fieldID = 0;
            record.Fields.Add(new Field("lx", "foo", 0, false, fieldID++));
            record.Fields.Add(new Field("ps", "noun", 0, false, fieldID++));
            record.Fields.Add(new Field("ge", "bar", 0, false, fieldID++));
            Assert.IsTrue(record.HasMarker("lx"));   
        }

        [Test]
        public void HasMarkerIndicatesItDoesntHaveIncorrectMarker()
        {
            var record = CreateDefaultRecord();
            int fieldID = 0;
            record.Fields.Add(new Field("lx", "foo", 0, false, fieldID++));
            record.Fields.Add(new Field("ps", "noun", 0, false, fieldID++));
            record.Fields.Add(new Field("ge", "bar", 0, false, fieldID++));
            Assert.IsFalse(record.HasMarker(string.Empty));   
        }
    }
}
