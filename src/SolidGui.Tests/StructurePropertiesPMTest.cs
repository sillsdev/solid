using System.Collections.Generic;
using System.IO;
using System.Xml;
using SolidEngine;
using NUnit.Framework;

namespace SolidGui.Tests
{
    [TestFixture]
    public class StructurePropertiesPMTest
    {

        StructurePropertiesPM _model = new StructurePropertiesPM();
        SolidMarkerSetting _markerSetting = new SolidMarkerSetting();

        [SetUp]
        public void Setup()
        {
        
            _markerSetting.StructureProperties.Add(new SolidStructureProperty("nt"));
            _model.MarkerSetting = _markerSetting;
        }

        [TearDown]
        public void TearDown()
        {
        }

        [Test]
        public void UpdateInferedParentChangesMarkerSettingInfereParentProperty()
        {
            _model.UpdateInferedParent("lx");
            Assert.AreEqual("lx",_markerSetting.InferedParent);
        }

        [Test]
        public void UpdateMultilplicityChangesTheMultiplicityEnumToOnce()
        {
            SolidStructureProperty sp = new SolidStructureProperty();
            _model.UpdateMultiplicity(sp, true, false, false);

            Assert.AreEqual(MultiplicityAdjacency.Once, sp.MultipleAdjacent);
        }

        [Test]
        public void UpdateMultilplicityChangesTheMultiplicityEnumToMultipleApart()
        {
            SolidStructureProperty sp = new SolidStructureProperty();
            _model.UpdateMultiplicity(sp, false, true, false);

            Assert.AreEqual(MultiplicityAdjacency.MultipleApart, sp.MultipleAdjacent);
        }

        [Test]
        public void UpdateMultilplicityChangesTheMultiplicityEnumToMultipleTogether()
        {
            SolidStructureProperty sp = new SolidStructureProperty();
            _model.UpdateMultiplicity(sp, false, false, true);

            Assert.AreEqual(MultiplicityAdjacency.MultipleTogether, sp.MultipleAdjacent);
        }


        [Test]
        public void RemoveStructurePropertyRemovesStructurePropertyFromStructurePropertyList()
        {
            List<SolidStructureProperty> ssp = new List<SolidStructureProperty>();

            ssp.Add(new SolidStructureProperty("nt"));
            ssp.Add(new SolidStructureProperty("lx"));

            _markerSetting.StructureProperties = ssp;

            _model.RemoveStructureProperty("nt");

            Assert.AreEqual(1, _markerSetting.StructureProperties.Count);
            Assert.AreEqual("lx", _markerSetting.StructureProperties[0].Parent);
        }

        [Test]
        public void UpdateParentMarkersGivenAListOfITemsUpdatesTheStructurePropertiesList()
        {

        }

        [Test]
        public void GetSelectedTextReturnsSelectedLabelInAListView()
        {
            
        }


            
    }
}
