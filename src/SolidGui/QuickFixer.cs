﻿using System;
using System.Collections.Generic;
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

        public void MoveCommonItemsUp(List<string> markers)
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
    }
}