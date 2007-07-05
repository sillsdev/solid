using NUnit.Framework;

namespace SolidGui.Tests
{
    [TestFixture]
    public class MappingPMTests
    {
        private MappingPM _model;

        [SetUp]
        public void Setup()
        {
            _model = new MappingPM();
        }

        [TearDown]
        public void TearDown()
        {

        }

        [Test]
        public void FindsAtLeastTwoMappingSystem()
        {
            _model.TargetSystem = _model.TargetChoices[0];
            Assert.Greater(_model.TargetChoices.Count,1);
        }

        [Test]
        public void GivesListOfConceptsForFLEx()
        {
            _model.TargetSystem = _model.TargetChoices[0];
            Assert.Greater(_model.TargetSystem.Concepts.Count, 20);
        }

        [Test]
        public void ChangingTargetChangesConceptList()
        {
            _model.TargetSystem = _model.TargetChoices[0];
            int conceptsInFirstSystem = _model.TargetChoices.Count;
            _model.TargetSystem = _model.TargetChoices[1];
            Assert.AreNotEqual(conceptsInFirstSystem, _model.TargetSystem.Concepts.Count);
        }

        [Test]
        public void SomeConceptIsChosenWhenTargetSelected()
        {
            _model.TargetSystem = _model.TargetChoices[0];
            Assert.IsNotNull(_model.SelectedConcept);
            _model.TargetSystem = _model.TargetChoices[1];
            Assert.IsNotNull(_model.SelectedConcept);
        }

        [Test]
        public void GivesHtmlForConcepts()
        {
            _model.TargetSystem = _model.TargetChoices[0];
            _model.SelectedConcept = _model.TargetSystem.Concepts[0];
            string html = _model.TransformInformationToHtml(_model.SelectedConcept.InformationAsXml);
            Assert.IsFalse(string.IsNullOrEmpty(html))    ;
        }
    }

}