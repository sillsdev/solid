using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace SolidEngine
{
    public class ProcessStructure
    {
        SolidReport _report;
        SolidMarkerSettings _markerSettings;

        XmlDocument _xmlDoc;

        public ProcessStructure(SolidReport report, SolidMarkerSettings markerSettings)
        {
            _report = report;
            _markerSettings = markerSettings;
        }

        private void Reset()
        {
            _xmlDoc = new XmlDocument();
        }

        public void Process(XmlDocument d)
        {
            Reset();

            // Iterate through each (flat) node in the src d
            XmlNode n = d.FirstChild;
            XmlNode parent = null;
            while (n != null)
            {
                // Get the marker settings for this node.
                SolidMarkerSetting setting = _markerSettings.Find(n.Name);
                // Check for record marker (assume is root)
                //!!!if (n.Name == 

                n = n.NextSibling;
            }

            // Check that it's parent is valid, if so add it to the current parent.

            // Otherwise, check up the tree until a valid parent is found.

            // Otherwise, check to see if a parent can be inferred at this point.

            _report.Add(new SolidReport.Entry(

            ));



        }
    }
}
