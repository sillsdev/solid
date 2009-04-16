using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
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


    }
}
