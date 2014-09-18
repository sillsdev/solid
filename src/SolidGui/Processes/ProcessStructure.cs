// Copyright (c) 2007-2014 SIL International
// Licensed under the MIT license: opensource.org/licenses/MIT

using System;
using System.Collections.Generic;
using System.Drawing.Text;
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
            UpdateScope(scope, scope.Count, source);
//            TruncateScope(scope.Count - 1, scope);
            outputEntry.AppendField(source);
        }

        private bool InsertInTree(SfmFieldModel source, SolidReport report, List<SfmFieldModel> scope, SfmLexEntry outputEntry)
        {
            int index = CanInsertInTree(source, scope);
            if (index < 0)
            {
                return false;
            }
            scope[index].AppendChild(source);  // Add the node under this parent
            UpdateScope(scope, index, source); // Add to list of possible parents

            //source.Depth = scope.Count - 1;
            
            outputEntry.AppendField(source);
            return true;
        }

        private int CanInsertInTree(SfmFieldModel source, List<SfmFieldModel> scope)
        {
            // Get the marker settings for this node.
            SolidMarkerSetting sourceSetting = _settings.FindOrCreateMarkerSetting(source.Marker);
            bool foundParent = false;
            int i = scope.Count;
            // Check for record marker (assume is root)
            while (i > 0 && !foundParent)
            {
                i--;
                SolidStructureProperty sourceStructurePropertiesForParent = sourceSetting.GetStructurePropertiesForParent(scope[i].Marker);
                if (sourceStructurePropertiesForParent != null)
                {
                    switch (sourceStructurePropertiesForParent.Multiplicity)
                    {
                        case MultiplicityAdjacency.Once:
                            foundParent = true;
                            foreach (SfmFieldModel childNode in scope[i].Children)
                            {
                                if (childNode.Marker == source.Marker)
                                    foundParent = false;
                            }
                            break;
                        case MultiplicityAdjacency.MultipleTogether:
                            {
                                foundParent = true;
                                bool foundSelfAsSibling = false;

                                //make sure the parent doesn't allready contain the node we want to add
                                foreach (SfmFieldModel childNode in scope[i].Children)
                                {
                                    if (childNode.Marker == source.Marker)
                                    {
                                        foundSelfAsSibling = true;
                                    }
                                    else
                                    {
                                        if (foundSelfAsSibling)
                                        {
                                            foundParent = false;
                                        }
                                    }
                                }
                            }
                            break;
                        case MultiplicityAdjacency.MultipleApart:
                            foundParent = true;
                            break;
                    }
                }
            }
            return foundParent ? i : -1;
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
//!!!                inferredNode.AppendChild(sourceField);

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
                        //??? This is bordering on an exception. It indicates that there is an inconsistency with the setings file, which the UI shouldn't make possible.
                        // Error.
                        report.AddEntry(
                            SolidReport.EntryType.StructureInsertInInferredFailed,
                            outputEntry,
                            sourceField,
                            String.Format("ERROR: Inferred marker \\{0} is not a valid parent of \\{1}. Someone may have manually edited your settings file.", setting.InferedParent, sourceField.Marker)
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
                                    String.Format("ERROR: Inferred marker \\{0} is not a valid parent of \\{1}", setting.InferedParent, sourceField.Marker)
                                    );
                                InsertInTreeAnyway(sourceField, report, scope, outputEntry);
                            }
                        }
                        else
                        {
                            if (InsertInTree(sourceField, report, scope, outputEntry))
                            {
                                retval = true;
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
            bool passedFirst = false;  // adding a check; don't want to bother/risk refactoring this. -JMC Mar 2014
            foreach (SfmFieldModel sfmField in lexEntry.Fields)
            {
                if(sfmField != lexEntry.FirstField)
                {
                    passedFirst = true;
                    if (!InsertInTree(sfmField, report, scope, outputEntry))
                    {
                        int recurseCount = 0;
                        InferNode(sfmField, report, scope, outputEntry, ref recurseCount);
                    }
                }
                else
                {
                    if (passedFirst)
                    {
                        throw new DataMisalignedException ("The marker " + lexEntry.FirstField + " was found in the middle of a field.");
                    }
                }
                
            }
            
            // Walk the tree of nodes, appending closing tags as needed.
            AddClosers(outputEntry.Fields[0]);
            return outputEntry;
        }

        //private static Stack<string> _stack;

/*
        private static void AddClosers(SfmLexEntry lexEntry)
        {
            SfmFieldModel root = lexEntry.Fields[0];

            _stack = new Stack<string>();
            _stack.Push(root.Marker);
        }
*/
        

        private static SfmFieldModel _appendTo;

        // Recursive
        private static void AddClosers(SfmFieldModel node)
        {
            if (node == null)
            {
                return;
            }

            if (node.Closers == null)
            {
                node.Closers = new List<string>();
            }
            _appendTo = node;
            foreach (SfmFieldModel child in node.Children)
            {
                AddClosers(child);
            }
            _appendTo.Closers.Add(node.Marker);
        }
    }

    
}