using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;
using SolidEngine;

namespace SolidGui
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
        private SolidMarkerSetting _markerSetting;
        private Concept _selectedConcept;
        private SolidMarkerSetting.MappingType _type;

        public MappingPM()
        {
            _systems = new List<MappingSystem>();

            foreach (string path in Directory.GetFiles(PathToMappingsDirectory, "*.mappingSystem"))   
            {
                _systems.Add(new MappingSystem(path));
            }
            if (TargetChoices.Count > 0)
            {
                _targetSystem = TargetChoices[0];
            }
        }

        public SolidMarkerSetting.MappingType Type
        {
            get
            {
                return _type;
            }
            set
            {
                _type = value;
            }
        }

        public SolidMarkerSetting MarkerSetting
        {
            set
            {
                _markerSetting = value;
            }
            get
            {
                return _markerSetting;
            }
        }

        public MappingSystem TargetSystem
        {
            get
            {
                return _targetSystem;
            }
            set
            {
                _targetSystem = value;

                //hack
                _selectedConcept = TargetSystem.Concepts[0];
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
                return Path.Combine(MainWindowPM.TopAppDirectory, "mappings");
            }
        }

        public string TransformInformationToHtml(XmlNode informationNode)
        {
             XslTransform transform = new XslTransform();
            transform.Load(Path.Combine(this.PathToMappingsDirectory, "MappingXmlToHtml.xsl"));
            StringBuilder builder = new StringBuilder();
            StringWriter st = new StringWriter(builder);
//            string temp = Path.GetTempFileName();
//            File.WriteAllText(temp, informationNode.OuterXml);
            //this transformed whole document, ignoring the fact that we gave it one node
            //      transform.Transform(informationNode,new XsltArgumentList(), st);
             XPathDocument doc = new XPathDocument(new XmlTextReader(new StringReader(informationNode.OuterXml)));
            
            transform.Transform(doc,new XsltArgumentList(), st);
            return builder.ToString();

        }

        public Concept SelectedConcept
        {
            get
            {
                return _selectedConcept;
            }
            set
            {
                _selectedConcept = value;
            }
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
        }

        public class Concept
        {
            private readonly XmlNode _node;
            public Concept(XmlNode node)
            {
                _node = node;
            }


            public XmlNode InformationAsXml
            {
                get
                {
                    return _node;
                }
            }

            public override string ToString()
            {
                return _node.Attributes["uiname"].Value;
            }
        }
    }
}
