using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Xml;
using SolidGui;
using System.Linq;
using Palaso.Extensions;

namespace Solid.Engine
{
    public class QuickFixer
    {
        private readonly SfmDictionary _dictionary;

        public QuickFixer(SfmDictionary dictionary)
        {
            _dictionary = dictionary;
        }

        public void MoveCommonItemsUp(List<string>roots,List<string> markers)
        {
            /* bw any other non-bundle fields are the only safe ones
             * to use with this method
             * 
             * ph isn't safe, because it could be in an \se
             */
            foreach (var record in _dictionary.AllRecords)
            {
                int indexToMoveAfter = -1;//-1 means we haven't found a root yet
                for (int i = 0; 
                    i < record.Fields.Count; i++) 
                {
                    if (roots.Contains(record.Fields[i].Marker))
                    {
                        indexToMoveAfter = i; // found a new root (e.g. \se or \sn)
                    }
                    if (indexToMoveAfter>-1 && markers.Contains(record.Fields[i].Marker))
                    {
                        Debug.Assert(i > indexToMoveAfter);

                        record.MoveField(record.Fields[i], indexToMoveAfter);
                        
                        ++indexToMoveAfter; // the next guy goes after, so they stay in relative order
                    }
                
                }
            }
        }

        public void MoveCommonItemsUpToLx(List<string> markers)
        {
            /* bw any other non-bundle fields are the only safe ones
             * to use with this method
             * 
             * ph isn't safe, because it could be in an \se
             */
            foreach (var record in _dictionary.AllRecords)
            {
                int indexToMoveAfter = 0;
                for (int i = 1; //start with the 2nd line, even though we wouldn't move it
                    i < record.Fields.Count; i++)
                {
                    if (markers.Contains(record.Fields[i].Marker))
                    {
                        if (i > indexToMoveAfter)
                        {
                            record.MoveField(record.Fields[i], indexToMoveAfter);
                        }
                        ++indexToMoveAfter; // the next guy goes after, so they stay in relative order
                    }

                }
            }
        }
        public void RemoveEmptyFields(List<string> markersToLeaveAlone)
        {
            if(!markersToLeaveAlone.Contains("lx"))//that'd be too dangerous
            {
                markersToLeaveAlone.Add("lx");
            }

            foreach (var record in _dictionary.AllRecords)
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
            foreach (var record in _dictionary.AllRecords)
            {
                foreach (var field in record.Fields)
                {
                    if(field.Inferred && markers.Contains(field.Marker))
                        field.Inferred = false;
                    
                }
            }          
        }

        struct RecordAdddition
        {

            public string targetHeadWord;
            public string fromHeadWord;
            public string fromMarker;
            public string pos;
            public readonly Field sourceField;

            public RecordAdddition(string targetHeadWord, string fromHeadWord, string fromMarker, string POS, Field sourceField)
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
            List<RecordAdddition> additions = FindNeededEntryAdditions(markers);
            AddNewEntries(additions, log);
            return log.ToString();
        }

        public string MakeEntriesForReferredItemsOfLv()
        {
            var log = new StringBuilder();

            //this isn't safe with \lv, 'cause each needs an \lf preceding.  That code
            //could be written, but hasn't been.  SplitFieldsWithMultipleItems(markers, log);
            PropogateField("lf", "lv", log);
            List<RecordAdddition> additions = FindNeededEntryAdditions(new List<string>{"lv"});
            AddNewEntries(additions, log);
            return log.ToString();
        }

        private void PropogateField(string markerOfFieldToPropogate, string markerOfFieldToPlaceBefore, StringBuilder log)
        {
            foreach (var record in _dictionary.Records)
            {
                Field fieldToCopy = null;
                for (int i = 0; i < record.Fields.Count; i++)
                {
                    var field = record.Fields[i];
                    if (field.Marker == markerOfFieldToPropogate)
                    {
                        fieldToCopy = field;
                        ++i;//skip the next line, since it is *already* preceded by this field
                        continue;
                    }
                    else if(field.Marker == markerOfFieldToPlaceBefore)
                    {
                        if(fieldToCopy !=null)
                        {
                            var f = new Field(fieldToCopy.Marker, fieldToCopy.Value, fieldToCopy.Depth, false, -1 /*review*/);
                            record.InsertFieldAt(f, i); 
                            ++i;//skip the next line, since not is is preceded by this field
                        }
                    }
                }
            }
        }

        private List<RecordAdddition> FindNeededEntryAdditions(List<string> markers)
        {
            var additions = new List<RecordAdddition>();
            foreach (var record in _dictionary.AllRecords)
            {
                string lastPOS = "FIXME";
                foreach (var field in record.Fields)
                {
                    //nb: this isn't going to work when the refering marker
                    //comes before any ps
                    if(field.Marker =="ps" && !string.IsNullOrEmpty(field.Value))
                    {
                        lastPOS = field.Value;
                    }
                    if(markers.Contains(field.Marker))
                    {
                        var headword = field.Value.Trim();
                        if(!string.IsNullOrEmpty(headword) &&
                            !additions.Any(x=>x.targetHeadWord == headword))
                        {
                            additions.Add(new RecordAdddition(headword, record.Fields[0].Value, field.Marker, lastPOS, field));
                        }
                    }
                }
            }
            return additions;
        }

        private void AddNewEntries(List<RecordAdddition> additions, StringBuilder log)
        {
            SolidSettings nullSettings = new SolidSettings();
            foreach (var addition in additions)
            {
                string switchToCitationForm;
                var targetRecord = FindRecordByCitationFormOrLexemeForm(addition.targetHeadWord, out switchToCitationForm);
                if (targetRecord == null)
                {
                    targetRecord = FindRecordContainingVariantOrSubEntry(addition.targetHeadWord);
                }
                if (null == targetRecord)
                {
                    Record r = new Record(-1);
                    var b = new StringBuilder();
                    b.AppendLine("\\lx " + addition.targetHeadWord);
                    b.AppendLine("\\ps " + addition.pos); //without this, flex balks
                    b.AppendFormat(
                        "\\CheckMe Created by SOLID Quickfix because '{0}' referred to it in the \\{1} field.\r\n",
                        addition.fromHeadWord, addition.fromMarker);


                    r.SetRecordContents(b.ToString(), nullSettings);
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
                    log.AppendFormat("***Switched  \\{3} target of '{0}' from '{1}' to the citation form '{2}' to get around Flex 5.4 limitation (only links to the 'headword', not the lx)\r\n",
                                     addition.fromHeadWord, addition.targetHeadWord, switchToCitationForm, addition.fromMarker);

                }
            }
        }

        private void SplitFieldsWithMultipleItems(List<string> markers, StringBuilder log)
        {
            foreach (var record in _dictionary.Records)
            {
                for (int i = 0; i < record.Fields.Count; i++)
                {
                    var field = record.Fields[i];
                    if (markers.Contains(field.Marker))
                    {
                        var parts = field.Value.SplitTrimmed(',');
                        if (parts.Count > 1)
                        {
                            parts.Reverse();
                            record.RemoveField(i);

                            log.AppendFormat("Splitting '\\{0} {1}' into multiple fields\r\n", field.Marker, field.Value);
                            foreach (var headword in parts)
                            {
                                var f = new Field(field.Marker, headword, field.Depth, false, -1 /*review*/);
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


            foreach (var record in _dictionary.Records)
            {
                if (record.HasMarker("lc"))
                {
                    var citationField = record.GetFirstFieldWithMarker("lc");
                    if (citationField != null && citationField.Value == form)
                    {
                        return record;
                    }
                }
            }

            foreach (var record in _dictionary.Records)
            {
                if (record.Fields[0].Value == form)
                {
                    //do we need to switch to the lc so it links?
                    if (record.HasMarker("lc"))
                    {
                        var citationField = record.GetFirstFieldWithMarker("lc");
                        if (citationField != null)
                        {
                            switchToCitationForm = citationField.Value;
                        }
                    }
                    return record;
                }
            }

            return null;
        }
        private Record FindRecordContainingVariantOrSubEntry(string form)
        {

            foreach (var record in _dictionary.Records)
            {
                foreach (var field in record.Fields)
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

        public string PropogatePartOfSpeech()
        {
            var sensesEffected = 0;

            StringBuilder logBuilder= new StringBuilder();
            foreach (var record in _dictionary.Records)
            {
                var encounteredSn = false;

                //in the format we're changing to, \ps comes after \sn, so 
                //we should remove one which appears before the first sn (it will have been copied in)
                var indexOfPsFieldToRemoveAtEnd = 0;
                
                Field fieldToCopy = null;
                for (int i = 0; i < record.Fields.Count; i++)
                {
                    var field = record.Fields[i];
                    if (field.Marker == "ps")
                    {
                        if (!encounteredSn)
                        {
                            indexOfPsFieldToRemoveAtEnd = i;
                        }
                        fieldToCopy = field;
                        continue;
                    }
                    else if (field.Marker == "sn")
                    {
                        
                        encounteredSn = true;
                        if (fieldToCopy != null)
                        {
                            if (!LevelHasMarker(record, i, "ps", "sn"))
                            {
                                var f = new Field(fieldToCopy.Marker, fieldToCopy.Value, fieldToCopy.Depth, false, -1
                                    /*review*/);
                                record.InsertFieldAt(f, i+1);
                                ++i;//skip over what we just inserted
                                sensesEffected++;
                            }
                        }
                    }
                }
                if(encounteredSn && indexOfPsFieldToRemoveAtEnd>0)
                {
                    Debug.Assert(record.Fields[indexOfPsFieldToRemoveAtEnd].Marker == "ps");
                    //just one last sanity check
                    if (record.Fields[indexOfPsFieldToRemoveAtEnd].Marker == "ps")
                    {
                        record.RemoveField(indexOfPsFieldToRemoveAtEnd);
                    }
                }
            }
            logBuilder.AppendFormat("ps was copied to {0} senses.", sensesEffected);
            return logBuilder.ToString();
        }

        public bool LevelHasMarker(Record record, int startBelowMarkerIndex, string markerToFind, string markerToStopAt)
        {
            for (int i = startBelowMarkerIndex+1; i < record.Fields.Count; i++)
            {
                var field = record.Fields[i];
                if (field.Marker == markerToStopAt)
                {
                    return false;
                }
                if(field.Marker == markerToFind)
                    return true;
            }
            return false;
        }
    }
}
