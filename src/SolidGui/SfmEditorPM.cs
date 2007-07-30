using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Xml;
using Palaso.WritingSystems;
using SolidEngine;

namespace SolidGui
{
    public class SfmEditorPM
    {
        private SolidSettings _solidSettings;

        public class RecordEditedEventArgs:EventArgs
        {
            public string _record;

            public RecordEditedEventArgs(string record)
            {
                _record = record;
            }
        }

        public SolidSettings Settings
        {
            set
            {
                _solidSettings = value; 
            }
        }

        public void UpdateCurrentRecord(Record record, string update)
        {
            record.SetRecord(update, _solidSettings);
        }

        public Font CorrectFont(string marker)
        {
            Palaso.WritingSystems.LdmlInFolderWritingSystemRepository repository =
                new LdmlInFolderWritingSystemRepository();
            string writingSystemId = _solidSettings.FindMarkerSetting(marker).WritingSystem;
            Palaso.WritingSystems.WritingSystemDefinition definition = repository.LoadDefinition(writingSystemId);

            return new Font(definition.DefaultFontName, 13);
        }
    }
}
