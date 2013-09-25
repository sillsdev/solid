using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Palaso.Reporting;
using SolidGui.Properties;
using SolidGui.Engine;

namespace SolidGui
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// Can optionally take one command-line argument, representing either the dictionary file itself, or its .solid file. -JMC
        /// </summary>
        [STAThread]
        static void Main(params string[]args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


            //bring in settings from any previous version
            if (Settings.Default.NeedUpgrade)
            {
                //see http://stackoverflow.com/questions/3498561/net-applicationsettingsbase-should-i-call-upgrade-every-time-i-load
                Settings.Default.Upgrade();
                Settings.Default.NeedUpgrade = false;
                Settings.Default.Save();
            }

            SetupErrorHandling(); 
            SetupUsageTracking();

            MainWindowPM model = new MainWindowPM();
            MainWindowView form = new MainWindowView(model);
            if(args.Length > 0)
            {
                string fileName = args[0];
                //string solidFileName = "";
                if (fileName.EndsWith(".solid"))
                {
                    //solidFileName = fileName;
                    fileName = SolidSettings.GetDictionaryFilePathFromSettingsPath(fileName);
                }
                string templatePath = null;
                if (model.ShouldAskForTemplateBeforeOpening(fileName))  //check validity of .solid file
                {
                    templatePath = form.RequestTemplatePath(fileName, false);
                    if (string.IsNullOrEmpty(templatePath))
                    {
                        return; //they cancelled
                    }
                }
                if (model.OpenDictionary(fileName, templatePath))
                {
                    form.OnFileLoaded(fileName);
                }
            }            
            Application.Run(form);
            Settings.Default.Save();
            model.Settings.NotifyIfNewMarkers();
        }

        private static void SetupErrorHandling()
        {
            ExceptionHandler.Init();
            Logger.Init();
            ErrorReport.Init("solid@projects.palaso.org");
        }

        private static void SetupUsageTracking()
        {
            if (Settings.Default.Reporting == null)
            {
                Settings.Default.Reporting = new ReportingSettings();
                Settings.Default.Save();
            }
           UsageReporter.Init(Settings.Default.Reporting, "solid.palaso.org", "UA-22170471-4",
#if DEBUG
 true
#else
                false
#endif
);
        }
    }
}