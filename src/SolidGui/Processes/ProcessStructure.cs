using System;
using System.Collections.Generic;
using SolidGui.Engine;
using SolidGui.Model;

namespace SolidGui.Processes
{
    public class ProcessStructure : IProcess
    {
        readonly SolidSettings _settings;

        public ProcessStructure(SolidSettings settings)
        {
            _settings = settings;
        }

        private static void InsertInTreeAnyway(SfmFieldModel source, SolidReport report, List<SfmFieldModel> scope)
        {
            int level = 1;
            int i = scope.Count - level;
            
            SfmFieldModel field = scope[scope.Count - 1];
            bool inferred = field.Inferred;
            string fieldValue = field.Field; // TODO Find out where this comes from. I suspect it's not really used in any meaningful way, but refers to the origin of the field in the original SFM file. CP
            if (!inferred && fieldValue != string.Empty)
            {
                SolidReport.Entry e = (report.GetEntryById(Convert.ToInt32(fieldValue)));
                level = (e != null) ? 2 : 1;
                if (scope.Count >= level)
                {
                    i = scope.Count - level; //!!! Bit hacky.
                }
            }

            TruncateScope(i, scope);
            // Add the node under scope[i]
            SfmFieldModel n = scope[i].AppendChild(source);
            scope.Add(n);
        }

        private bool InsertInTree(SfmFieldModel source, SolidReport report, List<SfmFieldModel> scope)
        {
            // Get the marker settings for this node.
            SolidMarkerSetting setting = _settings.FindOrCreateMarkerSetting(source.Name);
            bool foundParent = false;
            int i = scope.Count;
            // Check for record marker (assume is root)
            while (i > 0 && !foundParent)
            {
                i--;
                SolidStructureProperty structureProperty = setting.getStructureProperty(scope[i].Name);
                if (structureProperty != null)
                {
                    if (i == scope.Count - 1)
                    {
                        foundParent = true;
                    }
                    else if (scope[i + 1].Name == setting.Marker &&
                             structureProperty.MultipleAdjacent != MultiplicityAdjacency.Once)
                    {
                        foundParent = true;
                    }
                    else if (structureProperty.MultipleAdjacent == MultiplicityAdjacency.Once)
                    {
                        foundParent = true;

                        //make sure the parent doesn't allready contain the node we want to add
                        foreach (SfmFieldModel childNode in scope[i].ChildNodes)
                        {
                            if (childNode.Name == source.Name)
                                foundParent = false;
                        }
                    }
                    else if (structureProperty.MultipleAdjacent == MultiplicityAdjacency.MultipleApart)
                    {
                        foundParent = true;
                    }
                }
            }

            if(foundParent)
            {
                // Add the node under this parent
                SfmFieldModel fieldNodeInTree = scope[i].AppendChild(source);
                UpdateScope(scope, i, fieldNodeInTree);
            }
            
            return foundParent;
        }

        private static void UpdateScope(List<SfmFieldModel> scope, int i, SfmFieldModel n)
        {
            TruncateScope(i, scope);
            if (n != null) scope.Add(n);
        }

        private static void TruncateScope(int i, List<SfmFieldModel> scope)
        {
            if (i < scope.Count - 1)
            {
                scope.RemoveRange(i + 1, scope.Count - i - 1);
            }
        }

        private bool InferNode(SfmLexEntry xmlEntry, SfmFieldModel xmlSourceField, SolidReport report, List<SfmFieldModel> scope, ref int recurseCount)
        {
            // Can we infer a node.
            bool retval = false;
            SolidMarkerSetting setting = _settings.FindOrCreateMarkerSetting(xmlSourceField.Name);
            if (setting.InferedParent != String.Empty)
            {
                var inferredNode = new SfmFieldModel(setting.InferedParent);
                inferredNode.Inferred = true;

                // Attempt to insert the inferred node in the tree.
                // The inferred node needs to find a valid parent
                if (InsertInTree(inferredNode, report, scope))
                {
                    // Now try and add the current node under the inferred node.
                    if (InsertInTree(xmlSourceField, report, scope))
                    {
                        retval = true;
                    }
                    else
                    {
                        //??? This is bordering on an exception. It indicates that there is an inconsistency with the seutp.
                        // Error.
                        report.AddEntry(
                            SolidReport.EntryType.StructureInsertInInferredFailed,
                            xmlEntry,
                            xmlSourceField,
                            String.Format("Inferred marker \\{0} is not a valid parent of \\{1}", setting.InferedParent, xmlSourceField.Name)
                            );
                        InsertInTreeAnyway(xmlSourceField, report, scope);
                    }
                }
                else
                {
                    if (recurseCount < 10)
                    {
                        if (InferNode(xmlEntry, inferredNode, report, scope, ref recurseCount))
                        {
                            // Now try and add the current node under the inferred node.
                            if (InsertInTree(xmlSourceField, report, scope))
                            {
                                retval = true;
                            }
                            else
                            {
                                //??? This is bordering on an exception. It indicates that there is an inconsistency with the seutp.
                                // Error.
                                report.AddEntry(
                                    SolidReport.EntryType.StructureInsertInInferredFailed,
                                    xmlEntry,
                                    xmlSourceField,
                                    String.Format("Inferred marker \\{0} is not a valid parent of \\{1}", setting.InferedParent, xmlSourceField.Name)
                                    );
                                InsertInTreeAnyway(xmlSourceField, report, scope);
                            }
                        }
                        // No else required, the InferNode puts the entries in the tree.
                    }
                    else
                    {
                        throw new Exception("Circular inference rules detected");
                    }
                    if (!retval)
                    {
                        // Error.
                        report.AddEntry(
                            SolidReport.EntryType.StructureParentNotFoundForInferred,
                            xmlEntry,
                            inferredNode,
                            String.Format("Inferred marker \\{0} could not be placed in structure.", inferredNode.Name)
                            );
                        // Error
                        report.AddEntry(
                            SolidReport.EntryType.StructureParentNotFound,
                            xmlEntry,
                            xmlSourceField,
                            string.Format("Marker \\{0} could not be placed in structure", xmlSourceField.Name)
                            );
                        InsertInTreeAnyway(xmlSourceField, report, scope);
                    }
                }
            }
            else
            {
                // Error
                report.AddEntry(
                    SolidReport.EntryType.StructureParentNotFound,
                    xmlEntry,
                    xmlSourceField,
                    string.Format("Marker \\{0} could not be placed in structure, and nothing could be inferred.", xmlSourceField.Name)
                    );
                InsertInTreeAnyway(xmlSourceField, report, scope);
            }
            return retval;
        }

        public SfmLexEntry Process(SfmLexEntry lexEntry, SolidReport report)
        {
            // Iterate through each (flat) node in the src d
            var scope = new List<SfmFieldModel>();
            scope.Add(lexEntry.FirstChild/* The lx field */); 
            SfmFieldModel sfmField = lexEntry.FirstChild;
            while (sfmField != null)
            {
                if (!InsertInTree(sfmField, report, scope))
                {
                    int recurseCount = 0;
                    InferNode(lexEntry, sfmField, report, scope, ref recurseCount);
                }
                sfmField = sfmField.NextSibling;
            }
            return lexEntry;
        }
    }

    
}