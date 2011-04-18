using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Palaso.Reporting;
using SolidGui.Properties;

namespace SolidGui
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
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
           if(args.Length > 0 && args[0].EndsWith(".solid"))
            {
                model.OpenDictionary(args[0]);
               form.OnFileLoaded(args[0]);
            }            
            Application.Run(form);
            Settings.Default.Save();
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