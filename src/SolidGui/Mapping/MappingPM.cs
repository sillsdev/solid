// Copyright (c) 2007-2014 SIL International
// Licensed under the MIT license: opensource.org/licenses/MIT

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;
using SIL.Code;
using SIL.Xml;
using SolidGui.Engine;
using SolidGui.Export;
using SolidGui.MarkerSettings;

namespace SolidGui.Mapping
{
    /// <summary>
    /// The mapping control allows the user to connect markers to concepts in other
    /// formats/applications.
    /// This class is the Presentation Model(ui-agnostic) half of this control
    /// </summary>
    public class MappingPM
    {
        private List<MappingSystem> _systems;
        private MappingSystem _targetSystem;
        private Concept _selectedConcept;  // e.g. a LIFT field
        public MarkerSettingsPM MarkerModel;  //added so we can support WillNeedSave -JMC Feb 2014

        private const string MappingsFolder = "mappings";

        public MappingPM(MarkerSettingsPM markerSettings)
        {
            MarkerModel = markerSettings;
            Type = SolidMarkerSetting.MappingType.Lift;  //LIFT by default
            _systems = new List<MappingSystem>();

            foreach (string path in Directory.GetFiles(PathToMappingsDirectory, "*.mappingSystem"))   
            {
                _systems.Add(new MappingSystem(path));
            }

            if (TargetChoices.Count > 1)
            {
                _targetSystem = TargetChoices[1];  //LIFT by default
            }
            else if (TargetChoices.Count > 0)
            {
                _targetSystem = TargetChoices[0];
            }
        }

        public SolidMarkerSetting.MappingType Type { get; set; }

        public SolidMarkerSetting MarkerSetting { get; set; }

        public MappingSystem TargetSystem
        {
            get
            {
                return _targetSystem;
            }
            set
            {
                _targetSystem = value;
            }
        }

        public IList<MappingSystem> TargetChoices
        {
            get
            {
                return _systems;
            }
        }



        public string PathToMappingsDirectory
        {
            get
            {
                return Path.Combine(MainWindowPM.TopAppDirectory, MappingsFolder);
            }
        }

        public string TransformInformationToHtml(XmlNode informationNode)
        {
            if (informationNode != null)
            {
                XslCompiledTransform transform = new XslCompiledTransform();
                transform.Load(Path.Combine(this.PathToMappingsDirectory, "MappingXmlToHtml.xsl"));
                StringBuilder builder = new StringBuilder();
                StringWriter st = new StringWriter(builder);
                //            string temp = Path.GetTempFileName();
                //            File.WriteAllText(temp, informationNode.OuterXml);
                //this transformed whole document, ignoring the fact that we gave it one node
                //      transform.Transform(informationNode,new XsltArgumentList(), st);
                XPathDocument doc = new XPathDocument(new XmlTextReader(new StringReader(informationNode.OuterXml)));

                transform.Transform(doc, new XsltArgumentList(), st);
                return builder.ToString();
            }
            return "";
        }

        public Concept SelectedConcept
        {
            get
            {
                return _selectedConcept;
            }
            set
            {
                if (_selectedConcept == value) return; // not really an edit
                _selectedConcept = value;
                MarkerModel.WillNeedSave();
                if (MappingSettingChanged != null) MappingSettingChanged.Invoke(this, EventArgs.Empty);
            }
        }

        public void Init(SolidMarkerSetting markerSetting)
        {
            var concept = GetConcept(markerSetting);
            _selectedConcept = concept;
        }

        private MappingPM.Concept GetConcept(SolidMarkerSetting markerSetting)
        {
            var mappingSystem = TargetChoices[(int)Type];
            var id = markerSetting.GetMappingConceptId(Type);
            MappingPM.Concept concept = mappingSystem.GetConceptById(id);
            return concept;
        }

        public class MappingSystem
        {
            private string _path;
            private List<Concept> _concepts = new List<Concept>();

            public MappingSystem(string path)
            {
                _path = path;

                XmlDocument doc = new XmlDocument();
                doc.Load(_path);
                foreach (XmlNode node in doc.SelectNodes("//Field"))
                {
                    _concepts.Add(new Concept(node));
                }
            }

            public override string ToString()
            {
                return Path.GetFileNameWithoutExtension(_path);
            }

            public IList<Concept> Concepts
            {
                get
                {
                    return _concepts;
                }
            }

            public Concept GetConceptById(string id)
            {
                return _concepts.Find(concept => concept.GetId() == id);
            }
        }

        public class Concept
        {
            private readonly XmlNode _node;
            public Concept(XmlNode node)
            {
                if (node != null)
                {
                    _node = node;
                }
            }

            public XmlNode InformationAsXml
            {
                get
                {
                    return _node;
                }
            }

            public string Label()
            {
                if (_node == null)
                {
                    return "";
                }
                return _node.GetOptionalStringAttribute("uiname", null);
            }

            public override string ToString()
            {
                return Label();
            }
            
            public string GetId()
            {
                if (_node == null)
                {
                    return "";
                }
                return _node.GetOptionalStringAttribute("id", null);
            }
        }

        public event EventHandler MappingSettingChanged;

    }
}