// Copyright (c) 2007-2014 SIL International
// Licensed under the MIT license: opensource.org/licenses/MIT

// Created for #1218 "Implement the next version of the .solid settings file, and migration code" 
// and the many other features waiting on this.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Linq;
using SolidGui.Engine;

namespace SolidGui.MarkerSettings
{
    /// <summary>
    /// Reads XML data from a .solid file into C# objects representing marker settings. Handles any needed
    /// migration at the same time.
    /// FUTURE: Maybe will also be used to write them back out as XML. However, this is a more trivial
    /// task, and serialize() is working fine for now and is less code to maintain.
    /// </summary>
    class MarkerSettingsMapper
    {

        // Static methods only, so far. -JMC Aug 2014

        // TODO: Bucket list for version 3 of the .solid format. See also: all issues blocked by #1218 (http://projects.palaso.org/issues/1218)
        // See bug: #
        // Note: no need to "correct" infered to inferred; http://www.verbix.com/webverbix/English/infer.html

        private const string MissingMultiplicity = "MissingMultiplicity";

        /// <summary>
        /// Get the first child element of the provided XML element that matches the provided name.
        /// </summary>
        /// <param name="elem">parent element</param>
        /// <param name="xname">name of child</param>
        /// <param name="defaultValue">what to return if not found</param>
        /// <param name="rep">what to report to if default had to be used</param>
        /// <param name="key">which key to report under</param>
        /// <param name="marker">which marker had the missing setting</param>
        /// <returns></returns>
        private static string GetElementValue(XElement elem, string xname, string defaultValue, SettingsFileReport rep, string key, string marker)
        {
            XElement tmp = elem.Element(xname);
            if (tmp != null)
            {
                return tmp.Value;
            }
            else
            {
                rep.ReportProblem(key, marker);
                return defaultValue;
            }
        }

        /// <summary>
        /// DELETE ME WHEN FEASIBLE
        /// </summary>
/*
        private static string GetElementValue(XElement elem, string xname, string defaultValue)
        {
            XElement tmp = elem.Element(xname);
            if (tmp != null)
            {
                return tmp.Value;
            }
            else
            {
                return defaultValue;
            }
        }
        */
        public static SolidSettings LoadMarkerSettings(XDocument xdoc)
        {
            XElement elem = xdoc.Element("SolidSettings");

            var ss = new SolidSettings();

            // TODO: wrap all this body in a try/catch, and rethrow any exceptions after adding the ss.FileStatusReport to it

            ss.Version = GetElementValue(elem, "Version", "1", ss.FileStatusReport, "No version indicated{1}; assuming v1. (x{0})", ""); // we'll "tally up" just zero or one of these; hence the "" below.

            ss.RecordMarker = GetElementValue(elem, "RecordMarker", ss.RecordMarker, ss.FileStatusReport, "No record marker specified{1}; assuming " + ss.RecordMarker + ". (x{0})", "");

            XElement xParent = elem.Element("MarkerSettings"); // TODO: if missing (XML file is bad) we'll crash on null here

            ss.FileStatusReport.AddKey(MissingMultiplicity, "Found {0} marker(s) with no Multiplicity node; using Once: {1}");

            var msets = new List<SolidMarkerSetting>();
            ss.MarkerSettings = msets; // Doing this up front and iteratively (not assigning from ToList() from linq) because DetermineDefaultEncoding may need to run part-way through. -JMC
            foreach (var mset in xParent.Elements("SolidMarkerSetting"))
            {
                msets.Add(LoadOneMarkerSettingFromXml(mset, ss));
            }

            // TODO: We may need a fix here, or in the method called above, to report any ignored duplicates 
            // (multiple marker settings using the same marker); see bug #1292  
            // Likewise, can we identify any other extraneous markers and inform the user of exactly what was dropped?
            // Long-term, however, if inter-operating with FLEx (e.g. if .solid and .map formats are merged), 
            // then preserving unused elements could become important. -JMC Jul 2014

            string latest = SolidSettings.LatestVersion.ToString();
            if (ss.Version != latest)
            {
                ss.FileStatusReport.AppendLine(String.Format("Migrated from v{0} to v{1} of the .solid format.", ss.Version, latest));
                ss.Version = latest;
            }

            return ss;
        }

        // TODO: Consider returning a string instead. -JMC
        private static SolidMarkerSetting LoadOneMarkerSettingFromXml(XElement elem, SolidSettings ss)
        {

            var ms = new SolidMarkerSetting();

            ms.Marker = GetElementValue(elem, "Marker", "??", ss.FileStatusReport, "Found {0} marker settings with no marker name: {1}", "??");

            //if (solidSettings.getVersionNum() <= 8) // at some point we could maybe allow omitting this per-field (fall back to solidSettings.DefaultEncodingUnicode) -JMC


            string uni = GetElementValue(elem, "Unicode", "false", ss.FileStatusReport, "Found {0} marker(s) with no Encoding: using legacy: {1}", ms.Marker);
            ms.Unicode = Convert.ToBoolean(uni) ? (uni != null) : SolidSettings.DetermineDefaultEncoding(ss);

            ms.WritingSystemRfc4646 = GetElementValue(elem, "WritingSystem", "", ss.FileStatusReport, "Found {0} marker(s) with no WS; using '': {1}", ms.Marker);

            ms.InferedParent = GetElementValue(elem, "InferedParent", "", ss.FileStatusReport, "Found {0} marker(s) with no InferedParent node; using '': {1}", ms.Marker);

            string tmp = GetElementValue(elem, "Required", "false", ss.FileStatusReport, "Found {0} marker(s) with no Required node; using false: {1}", ms.Marker);
            if (tmp != "")  // else will default to false
            {
                ms.IsRequired = Convert.ToBoolean(tmp);  // could crash
            }

            ms.NotesOrComments = GetElementValue(elem, "Comments", "", ss.FileStatusReport, "Found {0} marker(s) with no Comment node; using '': {1}", ms.Marker);

            ms.StructureProperties = LoadStructureProperties(elem.Element("StructureProperties"), ms.Marker, ss);

            ms.Mappings = LoadOneMappingSet(elem.Element("Mappings"), ms.Marker, ss);

            return ms;
        }

        // TODO: Use a specific XML element (and object property) for LIFT, a different one for FLEX, etc. Or, simplify down to one. -JMC
        // A string array is brittle--it relies on the ordering of the XML values, and an unchanging enum: 
        // Versions 1-2 would need to be auto-migrated from string[]
        private static string[] LoadOneMappingSet(XElement oneMappingPair, string marker, SolidSettings ss)
        {
            if (oneMappingPair != null)
            {
                return oneMappingPair.Elements("string").Select(m => m.Value).ToArray();  // linq (lambda / method syntax)                
                // TODO: verify there were exactly two returned values; report if not
            }
            // TODO: report a problem?
            ss.FileStatusReport.ReportProblem("Found {0} marker(s) with no Mappings; using ('', ''): {1}", marker);
            return new string[] { "", "" };
        }

        private static List<SolidStructureProperty> LoadStructureProperties(XElement elem, string marker, SolidSettings solidSettings)
        {
            return elem.Elements("SolidStructureProperty").Select(e => LoadOneStructureProperty(e, marker, solidSettings)).ToList();
        }

        private static SolidStructureProperty LoadOneStructureProperty(XElement e, string marker, SolidSettings ss)
        {
            var parent = new SolidStructureProperty();
            parent.Parent = GetElementValue(e, "Parent", ss.RecordMarker, ss.FileStatusReport, "Found {0} marker(s) with no Parent node; using '': {1}", marker);  // default to @"\lx"

            string mult; // <Multiplicity> , or in v1, <MultipleAdjacent>
            if (ss.getVersionNum() < 2)
            {

                mult = GetElementValue(e, "MultipleAdjacent", "Once", ss.FileStatusReport, MissingMultiplicity, marker); // default to "Once"
            }
            else
            {
                mult = GetElementValue(e, "Multiplicity", "Once", ss.FileStatusReport, MissingMultiplicity, marker); // default to "Once"
            }

            MultiplicityAdjacency multAdj;
            bool success = Enum.TryParse(mult, true, out multAdj);
            if (!success)
            {
                ss.FileStatusReport.ReportProblem("Found {0} marker(s) with bad Multiplicity node; using Once: {1}", marker);
                multAdj = MultiplicityAdjacency.Once;
            }

            parent.Multiplicity = multAdj;
            return parent;
        }


    }
}
