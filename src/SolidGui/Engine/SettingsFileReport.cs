// Copyright (c) 2007-2014 SIL International
// Licensed under the MIT license: opensource.org/licenses/MIT

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace SolidGui.Engine
{
    /// <summary>
    /// Stores info about what the auto-fix and auto-migration code did while opening a .solid file.
    /// Intended to be displayed to the user immediately after opening a file, if any settings existed and were tweaked.
    /// Note: Unknown elements will still be dropped silently, and tweaking the order of XML elements will not be reported.
    /// </summary>
    public class SettingsFileReport  // Feature #1298 : http://projects.palaso.org/issues/1298 -created by JMC July 2014
    {
        public class LineItem
        {
            private int _tally = 0;
            private int _maxToList;
            private string _markers = "";
            private string _messageTemplate = "Error affecting {0} marker(s):{1}"; // needs to have at least these two slots

            public LineItem(string message, int maxToList) : this(message)
            {
                _maxToList = maxToList;
            }

            public LineItem(string message)
            {
                _tally = 0;
                _messageTemplate = message;
            }

            public void Tally(string marker)
            {
                _tally++;
                if (_tally > _maxToList)
                {
                    return;
                }

                _markers += " " + marker;
                if (_tally == _maxToList) _markers += ("...");
            }

            public string Report()
            {
                if (_tally > 0)
                {
                    return String.Format(_messageTemplate, _tally, _markers);
                }
                return "";
            }

            public override string ToString()
            {
                return Report();
            }
        
        } 

       
        private StringBuilder _reportBuilder = new StringBuilder();

        private IDictionary<string, LineItem>  Messages = new Dictionary<string, LineItem>();

        //private int _count

        public string GetReport()
        {
            var sb = new StringBuilder();
            sb.AppendLine(_reportBuilder.ToString().Trim());
            foreach(var key in Messages.Keys)
            {
                string s = Messages[key].ToString();
                if (!String.IsNullOrEmpty(s)) sb.AppendLine(s);
            }
            return sb.ToString().Trim();
        }

        public override string ToString()
        {
            return GetReport();
        }

        public void AppendLine(string s)
        {
            _reportBuilder.AppendLine(s);
        }

        /// <summary>
        /// Adds the key and initializes the value. Or, does nothing if the key already exists.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="message"></param>
        /// <param name="maxToList"></param>
        public void AddKey(string key, string message, int maxToList)
        {
            var val = new LineItem(message, maxToList);
            if (!Messages.ContainsKey(key))
            {
                Messages.Add(key, val);
            }
        }

        public void AddKey(string key, string message)
        {
            AddKey(key, message, 4);
        }

        /// <summary>
        /// Can be used in two ways:
        /// 1. Define a long message and a short key in advance (AddKey), then report using the short key.
        /// 2. Directly report, without a short key. The long message itself will be the key.
        /// Have mostly transitioned from 1 to 2.
        /// </summary>
        /// <param name="key">The error to be tallied.</param>
        /// <param name="marker">The marker whose settings it occurred under.</param>
        public void ReportProblem(string key, string marker)
        {
            if (!Messages.ContainsKey(key))
            {
                AddKey(key, key);
            }
            Messages[key].Tally(marker);
        }
    }
}
