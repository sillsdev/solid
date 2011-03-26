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
            ErrorReport.EmailAddress = "solid@projects.palaso.org";


            if (Settings.Default.Reporting == null)
            {
                Settings.Default.Reporting = new ReportingSettings();
                Settings.Default.Save();
            }
            UsageReporter.AppReportingSettings = Settings.Default.Reporting;
            UsageReporter.GetUserIdentifierIfNeeded();//todo this should be automatic
            ErrorReport.AddStandardProperties();


        }

        private static void SetupUsageTracking()
        {
            Palaso.Reporting.UsageReporter.ReportLaunchesAsync();
        }
    
    }
}