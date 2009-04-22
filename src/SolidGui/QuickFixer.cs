using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Xml;
using SolidGui;
using System.Linq;

namespace SolidEngine
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
        public void RemoveEmptyFields(List<string> markers)
        {
            foreach (var record in _dictionary.AllRecords)
            {
                for (int i = record.Fields.Count-1;
                    i > 0 ; // don't even look at record marker field
                    i--)
                {
                    if (markers.Contains(record.Fields[i].Marker))
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
        /// <param name="markers"></param>
        /// <returns>log of what it did</returns>
        public string MakeEntriesForReferredItems(List<string> markers)
        {
            var log = new StringBuilder();
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
                        if(!additions.Any(x=>x.targetHeadWord == headword))
                        {
                            additions.Add(new RecordAdddition(headword, record.Fields[0].Value, field.Marker, lastPOS, field));
                        }
                    }
                }
            }
            SolidSettings nullSettings = new SolidSettings();
            foreach (var addition in additions)
            {
                string switchToCitationForm;
                var targetRecord = FindRecordByCitationFormOrLexemeForm(addition.targetHeadWord, out switchToCitationForm);
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
            return log.ToString();
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
    }
}
