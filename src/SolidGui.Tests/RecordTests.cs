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
            r.Fields.Add(new SfmFieldModel("lx", "foo", 0, false));
            r.Fields.Add(new SfmFieldModel("ps", "noun", 0, false));
            r.Fields.Add(new SfmFieldModel("bw", "bar", 0, false));
            r.Fields.Add(new SfmFieldModel("ge", "bar", 0, false));
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
            r.Fields.Add(new SfmFieldModel("lx", "foo", 0, false));
            r.Fields.Add(new SfmFieldModel("ps", "noun", 0, false));
            r.Fields.Add(new SfmFieldModel("bw", "bar", 0, false));
            r.Fields.Add(new SfmFieldModel("ge", "bar", 0, false));
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
            r.Fields.Add(new SfmFieldModel("lx", "foo", 0, false));
            r.Fields.Add(new SfmFieldModel("ps", "noun", 0, false));
            r.Fields.Add(new SfmFieldModel("bw", "bar", 0, false));
            r.Fields.Add(new SfmFieldModel("ge", "bar", 0, false));
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
            record.Fields.Add(new SfmFieldModel("lx", "foo", 0, false));
            record.Fields.Add(new SfmFieldModel("ps", "noun", 0, false));
            record.Fields.Add(new SfmFieldModel("ge", "bar", 0, false));
            Assert.AreEqual(3, record.Fields.Count);
            
        }

        [Test]
        public void HasMarkerIndicateItHasCorrectMarker()
        {
            var record = CreateDefaultRecord();
            record.Fields.Add(new SfmFieldModel("lx", "foo", 0, false));
            record.Fields.Add(new SfmFieldModel("ps", "noun", 0, false));
            record.Fields.Add(new SfmFieldModel("ge", "bar", 0, false));
            Assert.IsTrue(record.HasMarker("lx"));   
        }

        [Test]
        public void HasMarkerIndicatesItDoesntHaveIncorrectMarker()
        {
            var record = CreateDefaultRecord();
            record.Fields.Add(new SfmFieldModel("lx", "foo", 0, false));
            record.Fields.Add(new SfmFieldModel("ps", "noun", 0, false));
            record.Fields.Add(new SfmFieldModel("ge", "bar", 0, false));
            Assert.IsFalse(record.HasMarker(string.Empty));   
        }
    }
}
