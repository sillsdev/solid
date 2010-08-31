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

        private static void InsertInTreeAnyway(SfmFieldModel source, SolidReport report, List<SfmFieldModel> scope, SfmLexEntry outputEntry)
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
            scope[i].AppendChild(source);
            scope.Add(source);
            outputEntry.AppendField(source);
        }

        private bool InsertInTree(SfmFieldModel source, SolidReport report, List<SfmFieldModel> scope, SfmLexEntry outputEntry)
        {
            // Get the marker settings for this node.
            SolidMarkerSetting setting = _settings.FindOrCreateMarkerSetting(source.Marker);
            bool foundParent = false;
            int i = scope.Count;
            // Check for record marker (assume is root)
            while (i > 0 && !foundParent)
            {
                i--;
                SolidStructureProperty structureProperty = setting.getStructureProperty(scope[i].Marker);
                if (structureProperty != null)
                {
                    if (i == scope.Count - 1)
                    {
                        foundParent = true;
                    }
                    else if (scope[i + 1].Marker == setting.Marker &&
                             structureProperty.MultipleAdjacent != MultiplicityAdjacency.Once)
                    {
                        foundParent = true;
                    }
                    else if (structureProperty.MultipleAdjacent == MultiplicityAdjacency.Once)
                    {
                        foundParent = true;

                        //make sure the parent doesn't allready contain the node we want to add
                        foreach (SfmFieldModel childNode in scope[i].Children)
                        {
                            if (childNode.Marker == source.Marker)
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
                scope[i].AppendChild(source);
                UpdateScope(scope, i, source);
                outputEntry.AppendField(source);
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

        private bool InferNode(SfmFieldModel sourceField, SolidReport report, List<SfmFieldModel> scope, SfmLexEntry outputEntry, ref int recurseCount)
        {
            // Can we infer a node.
            bool retval = false;
            SolidMarkerSetting setting = _settings.FindOrCreateMarkerSetting(sourceField.Marker);
            if (setting.InferedParent != String.Empty)
            {
                var inferredNode = new SfmFieldModel(setting.InferedParent);
                inferredNode.Inferred = true;

                // Attempt to insert the inferred node in the tree.
                // The inferred node needs to find a valid parent
                if (InsertInTree(inferredNode, report, scope, outputEntry))
                {
                    // Now try and add the current node under the inferred node.
                    if (InsertInTree(sourceField, report, scope, outputEntry))
                    {
                        retval = true;
                    }
                    else
                    {
                        //??? This is bordering on an exception. It indicates that there is an inconsistency with the seutp.
                        // Error.
                        report.AddEntry(
                            SolidReport.EntryType.StructureInsertInInferredFailed,
                            outputEntry,
                            sourceField,
                            String.Format("Inferred marker \\{0} is not a valid parent of \\{1}", setting.InferedParent, sourceField.Marker)
                            );
                        InsertInTreeAnyway(sourceField, report, scope, outputEntry);
                    }
                }
                else
                {
                    if (recurseCount < 10)
                    {
                        if (InferNode(inferredNode, report, scope, outputEntry, ref recurseCount))
                        {
                            // Now try and add the current node under the inferred node.
                            if (InsertInTree(sourceField, report, scope, outputEntry))
                            {
                                retval = true;
                            }
                            else
                            {
                                //??? This is bordering on an exception. It indicates that there is an inconsistency with the seutp.
                                // Error.
                                report.AddEntry(
                                    SolidReport.EntryType.StructureInsertInInferredFailed,
                                    outputEntry,
                                    sourceField,
                                    String.Format("Inferred marker \\{0} is not a valid parent of \\{1}", setting.InferedParent, sourceField.Marker)
                                    );
                                InsertInTreeAnyway(sourceField, report, scope, outputEntry);
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
                            outputEntry,
                            inferredNode,
                            String.Format("Inferred marker \\{0} could not be placed in structure.", inferredNode.Marker)
                            );
                        // Error
                        report.AddEntry(
                            SolidReport.EntryType.StructureParentNotFound,
                            outputEntry,
                            sourceField,
                            string.Format("Marker \\{0} could not be placed in structure", sourceField.Marker)
                            );
                        InsertInTreeAnyway(sourceField, report, scope, outputEntry);
                    }
                }
            }
            else
            {
                // Error
                report.AddEntry(
                    SolidReport.EntryType.StructureParentNotFound,
                    outputEntry,
                    sourceField,
                    string.Format("Marker \\{0} could not be placed in structure, and nothing could be inferred.", sourceField.Marker)
                    );
                InsertInTreeAnyway(sourceField, report, scope, outputEntry);
            }
            return retval;
        }

        public SfmLexEntry Process(SfmLexEntry lexEntry, SolidReport report)
        {
            // Iterate through each (flat) node in the src d
            var scope = new List<SfmFieldModel>();
            scope.Add(lexEntry.FirstField/* The lx field */);
            var outputEntry = SfmLexEntry.CreateDefault(lexEntry.FirstField);
            foreach (var sfmField in lexEntry.Fields)
            {
                if(sfmField != lexEntry.FirstField)
                {
                    if (!InsertInTree(sfmField, report, scope, outputEntry))
                    {
                        int recurseCount = 0;
                        InferNode(sfmField, report, scope, outputEntry, ref recurseCount);
                    }
                }

                
            }
            return outputEntry;
        }
    }

    
}