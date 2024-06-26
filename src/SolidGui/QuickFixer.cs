﻿// Copyright (c) 2007-2014 SIL International
// Licensed under the MIT license: opensource.org/licenses/MIT

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Xml;
using SolidGui;
using System.Linq;
using SIL.Extensions;
using SolidGui.Engine;
using SolidGui.Export;
using SolidGui.Model;

namespace Solid.Engine
{
    public class QuickFixer
    {
        private readonly SfmDictionary _dictionary;

        public int ChangesMade = 0;

        public QuickFixer(SfmDictionary dictionary)
        {
            _dictionary = dictionary;
        }

        //JMC: Issue #1243. All of these need to increment a counter representing how many changes the quick fix made, to decide whether a Save will be needed or not


        public void MoveCommonItemsUp(List<string> roots, List<string> markers)
        {
            MoveCommonItemsUp(roots, markers, true);
        }

        public void MoveCommonItemsUp(List<string> roots, List<string> markers, bool minimal)
        {
            /* non-bundle fields are the only safe ones to use with this method
             * 
             * E.g. \ph isn't safe to move up under \lx, because it could be in an \se
             * \bw is safe, assuming it only occurs under \lx
             */
            int count = 0;
            int rootDepth = 0;
            bool stillShallow = false;
            foreach (Record record in _dictionary.AllRecords)
            {
                int indexToMoveAfter = -1;//-1 means we haven't found a root yet
                int lastRootFound = -1;
                for (int i = 0; i < record.Fields.Count; i++)
                {
                    var f = record.Fields[i];
                    if (roots.Contains(f.Marker))
                    {   
                        rootDepth = f.Depth;
                        stillShallow = true;
                        indexToMoveAfter = i; // found a new root (e.g. \se or \sn)
                        lastRootFound = i;
                    }
                    else if (minimal && stillShallow) 
                    {   //possible sibling
                        if ((f.Depth != rootDepth + 1) && (f.Depth != 0))  //ignore unplaceable markers
                        {
                            // too deep or shallow; any markers should be moved up to above this position, and above any non-root parent it may have
                            stillShallow = false;
                            indexToMoveAfter = Math.Max(indexToMoveAfter-1, lastRootFound); 
                        }
                        else
                        {
                            //after me is fine, as I am a sibling that precedes all nephews
                            indexToMoveAfter = i;  // implements #1223: minimal Move Up
                        }
                    }

                    if (indexToMoveAfter > -1 && markers.Contains(f.Marker))
                    {
                        if (indexToMoveAfter >= i - 1)
                        {
                            // no-op 'move up' to own location
                            //count = count;
                        }
                        else
                        {
                            // move up
                            count++;
                            record.MoveField(f, indexToMoveAfter);
                        }
                        ++indexToMoveAfter; // the next guy goes after, so the fields being moved stay in relative order
                    }
                
                }
            }
            ChangesMade += count;
        }


        public void RemoveEmptyFields(List<string> markersToLeaveAlone)
        {
            if(!markersToLeaveAlone.Contains("lx"))//that'd be too dangerous
            {
                markersToLeaveAlone.Add("lx");
            }

            foreach (Record record in _dictionary.AllRecords)
            {
                for (int i = record.Fields.Count-1;
                    i > 0 ; // don't even look at record marker field
                    i--)
                {
                    if (!markersToLeaveAlone.Contains(record.Fields[i].Marker))
                    {
                        if(record.Fields[i].Value.Trim() == string.Empty)
                        {
                            record.RemoveField(i);
                        }
                    }

                }
            }
        }


        public void MakeInferedMarkersReal(List<string> markers)
        {
            foreach (Record record in _dictionary.AllRecords)
            {
                foreach (SfmFieldModel field in record.Fields)
                {
                    if(field.Inferred && markers.Contains(field.Marker))
                    {
                        field.Inferred = false;
                        ChangesMade++;
                    }
                    
                }
            }          
        }

        struct RecordAddition
        {

            public string targetHeadWord;
            public string fromHeadWord;
            public string fromMarker;
            public string pos;
            public readonly SfmFieldModel sourceField;

            public RecordAddition(string targetHeadWord, string fromHeadWord, string fromMarker, string POS, SfmFieldModel sourceField)
            {
                this.targetHeadWord = targetHeadWord;
                this.fromMarker = fromMarker;
                pos = POS;
                this.sourceField = sourceField;
                this.fromHeadWord = fromHeadWord;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>log of what it did</returns>
        public string MakeEntriesForReferredItems(List<string> markers)
        {
            var log = new StringBuilder();

            SplitFieldsWithMultipleItems(markers, log);
            List<RecordAddition> additions = FindNeededEntryAdditions(markers);
            AddNewEntries(additions, log);
            return log.ToString();
        }

        public string MakeEntriesForReferredItemsOfLv()
        {
            var log = new StringBuilder();

            //this isn't safe with \lv, 'cause each needs an \lf preceding.  That code
            //could be written, but hasn't been.  SplitFieldsWithMultipleItems(markers, log);
            PropagateField("lf", "lv", log);
            List<RecordAddition> additions = FindNeededEntryAdditions(new List<string>{"lv"});
            AddNewEntries(additions, log);
            return log.ToString();
        }

        private void PropagateField(string markerOfFieldToPropagate, string markerOfFieldToPlaceBefore, StringBuilder log)
        {
            foreach (Record record in _dictionary.Records)
            {
                SfmFieldModel fieldToCopy = null;
                for (int i = 0; i < record.Fields.Count; i++)
                {
                    SfmFieldModel field = record.Fields[i];
                    if (field.Marker == markerOfFieldToPropagate)
                    {
                        fieldToCopy = field;
                        ++i;//skip the next line, since it is *already* preceded by this field
                        continue;
                    }
                    else if(field.Marker == markerOfFieldToPlaceBefore)
                    {
                        if(fieldToCopy !=null)
                        {
                            var f = new SfmFieldModel(fieldToCopy.Marker, fieldToCopy.Value, fieldToCopy.Trailing, fieldToCopy.Depth, false);
                            record.InsertFieldAt(f, i); 
                            ++i;//skip the next line, since not is is preceded by this field
                        }
                    }
                }
            }
        }

        private List<RecordAddition> FindNeededEntryAdditions(List<string> markers)
        {
            var additions = new List<RecordAddition>();
            foreach (Record record in _dictionary.AllRecords)
            {
                string lastPOS = "FIXME";
                foreach (SfmFieldModel field in record.Fields)
                {
                    //nb: this isn't going to work when the refering marker
                    //comes before any ps
                    if(field.Marker =="ps" && !string.IsNullOrEmpty(field.Value))
                    {
                        lastPOS = field.Value;
                    }
                    if(markers.Contains(field.Marker))
                    {
                        string headword = field.Value.Trim();
                        if( !string.IsNullOrEmpty(headword) &&
                            !additions.Any( x => x.targetHeadWord == headword) )
                        {
                            additions.Add(new RecordAddition(headword, record.Fields[0].Value, field.Marker, lastPOS, field));
                            // TODO: #1231 Right there, we just lost encoding information by assuming these bytes in memory are plain strings. -JMC Nov 2014
                        }
                    }
                }
            }
            return additions;
        }

        private void AddNewEntries(List<RecordAddition> additions, StringBuilder log)
        {
            SolidSettings nullSettings = new SolidSettings();  // JMC: why a new bunch?
            foreach (RecordAddition addition in additions)
            {
                string switchToCitationForm;
                Record targetRecord = FindRecordByCitationFormOrLexemeForm(addition.targetHeadWord, out switchToCitationForm);
                if (targetRecord == null)
                {
                    targetRecord = FindRecordContainingVariantOrSubEntry(addition.targetHeadWord);
                }
                if (null == targetRecord)
                {
                    Record r = new Record();
                    var b = new StringBuilder();
                    b.AppendLine("\\" + nullSettings.RecordMarker + " " + addition.targetHeadWord); // JMC: Will this Record marker necessarily match?? 
                    b.AppendLine("\\ps " + addition.pos); //without this, flex balks
                    b.AppendFormat(
                        "\\CheckMe Created by Solid Quickfix because '{0}' referred to it in the \\{1} field.{2}",
                        addition.fromHeadWord, addition.fromMarker, SfmField.DefaultTrailing);


                    r.SetRecordContents(b.ToString(), nullSettings); //Wait--aren't we passing UTF16 as if it were UTF8 ? -JMC Mar 2014
                    _dictionary.AddRecord(r);
                    log.AppendFormat("Added {0} because '{1}' referred to it in the \\{2} field.\r\n",
                                     addition.targetHeadWord, addition.fromHeadWord, addition.fromMarker);
                }
                else if(!string.IsNullOrEmpty(switchToCitationForm))
                {
                    //ok, now we're in the FLEx 5.4 situation where it
                    //it's not going to link to the \lx because there is a different
                    // \lc in there (only matches to the headword, which is
                    //lx unless there is an lc. So now we switch the referrer to the lc.

                    addition.sourceField.Value = switchToCitationForm;
                    log.AppendFormat("***Switched  \\{3} target of '{0}' from '{1}' to the citation form '{2}' to get around Flex import limitation (LT-14442; only links to the 'headword', not the lx)\r\n",
                                     addition.fromHeadWord, addition.targetHeadWord, switchToCitationForm, addition.fromMarker);

                }
            }
        }

        private void SplitFieldsWithMultipleItems(List<string> markers, StringBuilder log)
        {
            foreach (Record record in _dictionary.Records)
            {
                for (int i = 0; i < record.Fields.Count; i++)
                {
                    SfmFieldModel field = record.Fields[i];
                    if (markers.Contains(field.Marker))
                    {
                        List<string> parts = field.Value.SplitTrimmed(',');
                        if (parts.Count > 1)
                        {
                            parts.Reverse();
                            record.RemoveField(i);

                            log.AppendFormat("Splitting '\\{0} {1}' into multiple fields\r\n", field.Marker, field.Value);
                            foreach (string headword in parts)
                            {
                                var f = new SfmFieldModel(field.Marker, headword, field.Trailing, field.Depth, false);
                                record.InsertFieldAt(f, i);
                            }
                        }
                    }
                }
            }
        }


        private Record FindRecordByCitationFormOrLexemeForm(string form, out string switchToCitationForm)
        {
            switchToCitationForm = null;
            form = form.Trim();


            foreach (Record record in _dictionary.Records)
            {
                if (record.HasMarker("lc"))
                {
                    SfmFieldModel citationField = record.GetFirstFieldWithMarker("lc");
                    if (citationField != null && citationField.Value.Trim() == form)
                    {
                        return record;
                    }
                }
            }

            //not found? Now look at the \lx's

            foreach (Record record in _dictionary.Records)
            {
                if (record.Fields[0].Value.Trim() == form)
                {
                    //do we need to switch to the lc so it links?
                    if (record.HasMarker("lc"))
                    {
                        SfmFieldModel citationField = record.GetFirstFieldWithMarker("lc");
                        if (citationField != null)
                        {
                            switchToCitationForm = citationField.Value.Trim();
                        }
                    }
                    return record;
                }
            }

            return null;
        }
        private Record FindRecordContainingVariantOrSubEntry(string form)
        {

            foreach (Record record in _dictionary.Records)
            {
                foreach (SfmFieldModel field in record.Fields)
                {
                    if (field.Marker == "va" || field.Marker == "se")
                    {
                        if (field.Value.Trim() == form)
                            return record;
                    }
                }
            }
            return null;
        }

        //JMC: Issue #1219. Remove hard-coded references to markers (e.g. in the link-checking quick fix)
        // Instead, we should make sure the Mapping property is always correct, and use that if it's set.
        // That is, push whatever is mapped to "Part of Speech" down. If null, default to hard-coded "ps" marker.
        // Ditto for "Sense" and "sn", in this particular quick fix.
        public string PropagatePartOfSpeech()
        {
            int sensesEffected = 0;

            StringBuilder logBuilder= new StringBuilder();
            foreach (Record record in _dictionary.Records)
            {
                var encounteredSn = false;

                //in the format we're changing to, \ps comes after \sn, so 
                //we should remove one which appears before the first sn (it will have been copied in)
                var psFieldsToRemoveAtEnd = new List<int>();
                SfmFieldModel fieldToCopy = null;
                for (int i = 0; i < record.Fields.Count; i++)
                {
                    SfmFieldModel field = record.Fields[i];
                    if (field.Marker == "ps")
                    {
                        if (!encounteredSn)
                        {
                            psFieldsToRemoveAtEnd.Add(i);
                        }
                        fieldToCopy = field;
                        continue;
                    }
                    else if (field.Marker == "sn")
                    {
                        
                        encounteredSn = true;
                        if (fieldToCopy != null)
                        {
                            if (!LevelHasMarker(record, i, "ps", new[] { "sn", "se" }))
                            {
                                var f = new SfmFieldModel(fieldToCopy.Marker, fieldToCopy.Value, fieldToCopy.Trailing, fieldToCopy.Depth, false);
                                record.InsertFieldAt(f, i+1);
                                ++i;//skip over what we just inserted
                                sensesEffected++;
                            }
                        }
                    }
                    else if (field.Marker == "se")
                    {
                        encounteredSn = false;
                    }
                }
                if(encounteredSn && psFieldsToRemoveAtEnd.Count > 0)
                {
                    // Remove from last to first to ensure that the index to delete remains valid.
                    for (int i = psFieldsToRemoveAtEnd.Count - 1; i >= 0; --i)
                    {
                        int index = psFieldsToRemoveAtEnd[i];
                        Debug.Assert(record.Fields[index].Marker == "ps", "There is a bug in PropagatePartOfSpeech; please let the developers know.");
                        //just one last sanity check
                        if (record.Fields[index].Marker == "ps")
                        {
                            record.RemoveField(index);
                        }
                    }
                }
            }
            logBuilder.AppendFormat("ps was copied to {0} senses.", sensesEffected);
            return logBuilder.ToString();
        }

        public bool LevelHasMarker(Record record, int startBelowMarkerIndex, string markerToFind, string[] markersToStopAt)
        {
            for (int i = startBelowMarkerIndex+1; i < record.Fields.Count; i++)
            {
                SfmFieldModel field = record.Fields[i];
                if (markersToStopAt.Contains(field.Marker))
                //if (field.Marker == markerToStopAt)
                {
                    return false;
                }
                if(field.Marker == markerToFind)
                    return true;
            }
            return false;
        }

        public void AddGuids()
        {
            foreach (Record record in _dictionary.Records)
            {
                if (record.GetFirstFieldWithMarker("guid") != null)
                    continue;

                var f = new SfmFieldModel("guid", Guid.NewGuid().ToString());
                record.InsertFieldAt(f, record.Fields.Count);
            }
        }
    }
}
