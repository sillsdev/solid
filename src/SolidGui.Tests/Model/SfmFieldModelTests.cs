using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using SolidGui.Model;

namespace SolidGui.Tests.Model
{
    [TestFixture]
    public class SfmFieldModelTests
    {

        private static string ParentMarkerForField(SfmFieldModel field)
        {
            return field.Parent.Marker;
        }

        private static string ChildMarkerForField(SfmFieldModel field, int n)
        {
            var list = new List<SfmFieldModel>(field.Children);
            return list[n].Marker;
        }

        [Test]
        public void Enumerable_WithOneChild_EnumeratesOne()
        {
            var field = CreateFieldForTest();
            int count = 0;
            field.AppendChild(new SfmFieldModel("aa"));
            foreach(var c in field.Children)
            {
                count++;
            }
            Assert.AreEqual(1, count);
        }

        [Test]
        public void AppendChild_AddChild_ChildPresent()
        {
            var field = CreateFieldForTest();
            field.AppendChild(new SfmFieldModel("aa"));
            Assert.AreEqual("aa", ChildMarkerForField(field, 0));
        }

        [Test]
        public void AppendChild_AddChild_ParentCorrect()
        {
            var field = CreateFieldForTest();
            field.AppendChild(new SfmFieldModel("aa"));
            Assert.AreEqual("zz test", ParentMarkerForField(field.Children[0]));
        }

        [Test]
        public void AddReportEntry_AddOneEntry_ReportEntriesIsIterable()
        {
            

        }



        private static SfmFieldModel CreateFieldForTest()
        {
            return new SfmFieldModel("zz test");
        }
    }
}
