// Copyright (c) 2007-2014 SIL International
// Licensed under the MIT license: opensource.org/licenses/MIT

using System;
using System.Diagnostics;
using System.Text;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Serialization;
using Palaso.Reporting;
using SolidGui.Migration;

namespace SolidGui.Engine
{
    public class SolidSettings
    {

        private readonly List<SolidMarkerSetting> _markerSettings;
        private List<SolidMarkerSetting> _newlyAdded;
        private string _recordMarker = "lx";
        public static readonly int LatestVersion = 2; // Seems safer to use readonly rather than const here; it will eventually change. -JMC
        //public static readonly Encoding LegacyEncoding = Encoding.GetEncoding("iso-8859-1"); //the original
        public static readonly Encoding LegacyEncoding = Encoding.GetEncoding(1252); //my preference -JMC Feb 2014

        public SolidSettings()
        {
            Version = LatestVersion.ToString();
            DefaultEncodingUnicode = false;
            _markerSettings = new List<SolidMarkerSetting>();
            _newlyAdded = new List<SolidMarkerSetting>();
        }

        // JMC: Candidates that could be global 'constants' (or public static...): "lx", "entry", ".solid", "infer ", "Report Error"
        // e.g. public static readonly string DotSolid = ".solid"

        private static List<string> _fileExtensions = new List<string> { ".db", ".sfm", ".mdf", ".dic", ".txt", ".lex" };  // added by JMC 2013-09

        public static List<string> FileExtensions  // added by JMC 2013-09; note that it's not read-only; might perhaps have made more sense to have this property under MainWindowPM?
        {
            get { return _fileExtensions; }
        }

        public static string extsAsString(IEnumerable<string> exts) // added by JMC 2013-09
        {
            var x = new StringBuilder();
            foreach (string extension in exts)
            {
                x.Append(extension + " ");
            }
            return x.ToString().Trim();
        }

        // Returns the known dictionary extensions as a mask; baseFileName can simply be "*"
        public static string extsAsMask(IEnumerable<string> exts, string baseFileName) // added by JMC 2013-09
        {
            var x2 = new StringBuilder();
            foreach (string extension in exts)
            {
                x2.Append(baseFileName + extension + ";");
            }
            return x2.ToString();
        }

        public override string ToString()
        {
            string s = "";
            IEnumerable<SolidMarkerSetting> a;
            if (_markerSettings.Count > 4)
            {
                a = _markerSettings.Take(4);
                s = "...";
            }
            else
            {
                a = _markerSettings;
            }
            s = string.Join(" ", a) + s;
            return string.Format("{{{0} setts: {1}; {2}}}", 
                _markerSettings.Count, s, GetHashCode());
        }

        public string RecordMarker
        {
            get { return _recordMarker; }
            set { _recordMarker = value; }
        }

        public string Version { get; set; } // set needs to be public for XmlSerialize to work CP.

        public IEnumerable<string> Markers
        {
            get 
            {
                return _markerSettings.Select(item => item.Marker);   //LINQ
            }
        }

        public List<SolidMarkerSetting> MarkerSettings // TODO review: this shouldn't be visible
        {
            get { return _markerSettings; }
        }

        [XmlIgnore]
        public string FilePath { get; set; }

        [XmlIgnore]
        public bool DefaultEncodingUnicode { get; private set; }

        [XmlIgnore] // JMC: Probably not worth saving this into the (next version of the) .solid file.
        // NewLine should save as "\r\n" on Windows, but now that it's centralized here, and less is hard-coded, 
        // maybe it can flex. Really, "\n" would be nicer (esp. given RichTextBox's behavior), and only using
        // \r\n when saving to disk on Windows.
        // JMC: But it would be good to first do full testing (both unit and UI) using "\n" as the value.
        public static string NewLine = "\r\n";  //Static for now; maybe shouldn't be. And maybe should just return System.Environment.NewLine. -JMC
        
        public bool HasMarker(string marker)
        {
            return FindMarkerSetting(marker) != null;
        }

        private SolidMarkerSetting FindMarkerSetting(string marker)
        {
            return _markerSettings.Find(item => item.Marker == marker);
        }

        /// <summary>
        /// Look for the marker. If necessary, add it.
        /// </summary>
        /// <param name="marker">The marker to search for.</param>
        /// <returns>Returns an existing setting if possible; otherwise a newly created one.</returns>
        public SolidMarkerSetting FindOrCreateMarkerSetting(string marker) //JMC: I think this is maybe overused and could likely mask error conditions; replace some calls with plain FindMarkerSetting()?
        {
            // Search for the marker. If not found return default marker settings.
            SolidMarkerSetting result = FindMarkerSetting(marker);
            if (result == null)
            {
                result = new SolidMarkerSetting(marker, DefaultEncodingUnicode);
                _markerSettings.Add(result);
                _newlyAdded.Add(result);  // In some cases, the calling code should notify the user of new fields detected.
            }
            return result;
        }

        /// <summary>
        /// Sets all marker settings to unicode. Typically called just after the user has selected a template and encoding.
        /// </summary>
        public void SetToUnicode()
        {
            DefaultEncodingUnicode = true;
            foreach (SolidMarkerSetting markerSetting in this.MarkerSettings)
            {
                markerSetting.Unicode = true;
            }
        } 

        public int FindReplaceWs(string fromWritingSystem, string toWritingSystem)
        {
            int count = 0;
            foreach (SolidMarkerSetting markerSetting in this.MarkerSettings)
            {
                if (markerSetting.WritingSystemRfc4646 == fromWritingSystem)
                {
                    markerSetting.WritingSystemRfc4646 = toWritingSystem;
                    count++;
                }
            }
            // TODO: I think we want something like the following any time anything is modified. But we'd need a handle on the model. -JMC Jan 2014
            /* 
            if (count > 0)
            {
                _mainWindowPM.needsSave = true;
            }
             */

            return count;
        }


        public void NotifyIfNewMarkers()  // Added -JMC 2013-09 ; JMC: The MessageBox part probably belongs in a "View" class instead, but the logic should stay here.
        {
            if (_newlyAdded == null || _newlyAdded.Count < 1) return;
            var sb = new StringBuilder("New marker(s) added: ");
            foreach (SolidMarkerSetting marker in _newlyAdded)
            {
                sb.Append(string.Format("{0} ({1}) ", marker.Marker, marker.Unicode ? "u" : "Legacy!")); 
            }
            sb.Append(". Will appear upon Recheck.\n");
            MessageBox.Show(sb.ToString(), "New Marker(s) Added", MessageBoxButtons.OK, MessageBoxIcon.Information);
            _newlyAdded = new List<SolidMarkerSetting>(); //clear it out (don't keep notifying)

        }

        // JMC: This should probably be called once after every File Open. Especially because, even though most users don't need mixed encodings,
        // Solid used to silently create legacy-encoded markers whenever a new marker was identified in any file (even a unicode one).
        public string NotifyIfMixedEncodings()
        {
            int uni = 0;
            var legacy = new List<string>();
            foreach (SolidMarkerSetting marker in _markerSettings)
            {
                if (marker.Unicode)
                {
                    uni++;
                }
                else
                {
                    legacy.Add(marker.Marker);
                }
            }
            if (uni > 0 && uni < _markerSettings.Count)  // non-mixed is ideal: all or nothing in unicode
            {
                string msg = string.Join(" ", legacy.ToArray());
                msg = "Warning: the marker settings have a mix of unicode and legacy specified."
                    + "\nLegacy markers: " + msg 
                    + "\nNote: settings are invisible for markers not currently in use.";
                return msg;
            }
            return "";
        }

        /// <summary>
        /// Will silently rename existing file to name.solid.bak, if necessary.
        /// </summary>
        /// <param name="templateFilePath"></param>
        /// <param name="outputFilePath"></param>
        /// <returns></returns>
        public static SolidSettings CreateSolidFileFromTemplate(string templateFilePath, string outputFilePath)
        {
            string needsBackup = outputFilePath;
            try
            {
                // File.Delete(outputFilePath);
                if (File.Exists(outputFilePath)) // added by JMC
                {
                    string backupPath = outputFilePath + ".bak";
                    int i = 2;
                    while (File.Exists(backupPath))
                    {
                        backupPath = outputFilePath + ".bak" + i++.ToString();
                    }
                    File.Move(outputFilePath, backupPath);
                }

                File.Copy(templateFilePath, outputFilePath);
            }
            catch (Exception e)
            {
                ErrorReport.NotifyUserOfProblem(
                    "There was a problem opening that settings file.  The error was\r\n" + e.Message);
                return null;
            }

            return OpenSolidFile(outputFilePath);
        }

        /// <summary>
        /// Determine the default encoding, based on record marker if possible; otherwise based on the majority of markers (tiebreaker goes false)
        /// (I initially wrote the "pick majority" code thinking it best and not realizing that legacy is better at preserving mixed encodings; 
        /// the "pick majority" code could probably be removed now--less to maintain--since vernacular should take priority. -JMC)
        /// </summary>
        /// <param name="settings">A valid, already loaded set of settings, preferably including the record marker.</param>
        /// <returns>true (unicode) or false (legacy)</returns>
        public static bool DetermineDefaultEncoding(SolidSettings settings) // Added by JMC 2013-09
        {
            SolidMarkerSetting recordMarker = settings.FindMarkerSetting(settings.RecordMarker);
            if (!(recordMarker == null))
            {
                return recordMarker.Unicode;
            }
            else
            {
                int countTrue = 0;
                int countFalse = 0;
                foreach (string m in settings.Markers)
                {
                    if (settings.FindMarkerSetting(m).Unicode)
                    {
                        countTrue++;
                    }
                    else
                    {
                        countFalse++;
                    }
                }
                if (countTrue > countFalse)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Open existing .solid file.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns>Notifies user and returns null if file can't be opened for any reason</returns>
        public static SolidSettings OpenSolidFile(string filePath)
        {

            // Migrate before we load the model.
            var migrator = new SolidSettingsMigrator(filePath);
            if (migrator.NeedsMigration())
            {
                migrator.Migrate();
            }
            
            // Review: Why do we create a file if it doesn't exist? CP 2011-05 ; JMC: delete this?
            if(!File.Exists(filePath))
            {
                using(File.Create(filePath))
                {}
            }
            // Deserialize 
            SolidSettings settings;
            var settingsDataMapper = new XmlSerializer(typeof(SolidSettings));
            using (var reader = new StreamReader(filePath))
            {
                settings = (SolidSettings) settingsDataMapper.Deserialize(reader);
            }
            // If we don't replace the deserializer with custom code (Data Mapper pattern?), then we 
            // need a fix here to remove duplicates (multiple marker settings using the same marker); see bug #  -JMC Jul 2014

            // Set properties that aren't serialized.
            settings.FilePath = filePath;
            settings.DefaultEncodingUnicode = DetermineDefaultEncoding(settings);
            // Fix settings for the record marker.
            SolidMarkerSetting markerSetting = settings.FindOrCreateMarkerSetting(settings.RecordMarker);
            //!!! Assert if null
            // Check that it has 'entry' as the parent.
            if (!markerSetting.ParentExists("entry"))
            {
                markerSetting.StructureProperties.Add(new SolidStructureProperty("entry"));
            }
            return settings;
        }

        public bool Save()
        {
            return SaveAs(FilePath);
        }

        /// <summary>
        /// Saves the .solid settings file. Overwrites without prompting if the file exists.
        /// </summary>
        /// <param name="filePath"></param>
        public bool SaveAs(string filePath)
        {
            Logger.WriteEvent("Saving {0}", filePath);
            try
            {
                var xs = new XmlSerializer(typeof(SolidSettings));
                using (var writer = new StreamWriter(filePath))
                {
                    xs.Serialize(writer, this);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(null, exception.Message + "\r\n\r\nYou might try saving to a different location.", "Error on saving file.");
                return false;
            }
            Logger.WriteEvent("Done saving settings file.");
            return true;
        }

        public static string GetSettingsFilePathFromDictionaryPath(string dataFilePath)
        {
            int lastDot = dataFilePath.LastIndexOf('.');
            if (lastDot < 0)
            {
                lastDot = dataFilePath.Length;
            }
            string retval = dataFilePath.Substring(0, lastDot) + ".solid";
            return retval;
        }
        
        // Assuming some file.solid exists and was passed in, return any other file.* that exists; 
        // Return just one path, preferring certain extensions. Created by JMC 2013-09
        public static string GetDictionaryFilePathFromSettingsPath(string settingsFilePath)
        {

            var fileInfo = new FileInfo(settingsFilePath);
            DirectoryInfo dirInfo = fileInfo.Directory;

            string f = fileInfo.Name;
            string ext = fileInfo.Extension; //Path.GetExtension(settingsFilePath);
            if (ext.Length > 0)
            {
                f = f.Substring(0, f.Length - ext.Length); //get the base filename without the extension
            }
            else
            {
                throw new ArgumentException("The settings file path ought to end in .solid");
            }

            //JMC:! check for null dirInfo ?

            List<string> extensions = FileExtensions; // was: new string[]{".db", ".sfm", ".mdf", ".dic", ".txt"};
            string mask = extsAsMask(extensions, f);
            FileInfo[] matches = dirInfo.GetFiles(mask, SearchOption.TopDirectoryOnly); //find likely matches
            if (matches.Length < 1)
            {
                matches = dirInfo.GetFiles(f + ".*"); //nothing yet; find all possible matches

                if (matches.Length < 2)
                {
                    //still nothing beyond the .solid file; fail
                    var x = new StringBuilder();
                    x.AppendFormat("Solid could not find a matching dictionary for {0}. Please try again via the File menu.", settingsFilePath);
                    ErrorReport.NotifyUserOfProblem(x.ToString());
                    return ""; 
                }

            }

            foreach (FileInfo match in matches)   // pick the first (or only) match
            {
                if (match.Extension != ".solid")
                {
                    if (!extensions.Contains(match.Extension))
                    {
                        extensions.Add(match.Extension); // adaptive: makes the next File Open dialog friendlier (not saved) -JMC
                    }
                    return Path.Combine(dirInfo.ToString(), match.ToString());
                }
                
            }

            return ""; // fail
        }

    }
}