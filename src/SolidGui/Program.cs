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
            SetupErrorHandling();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
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
            Logger.Init();
            ErrorReport.EmailAddress = "solid@projects.palaso.org";//TODO Change this address
            ErrorReport.AddStandardProperties();
            ExceptionHandler.Init();
        }

        private static void SetupUsageTracking()
        {
			UsageReporter.AppReportingSettings = Settings.Default.Reporting;
            UsageReporter.AppNameToUseInDialogs = "SOLID";
            UsageReporter.AppNameToUseInReporting = "SOLID";
            UsageReporter.RecordLaunch();
            //UsageReporter.DoTrivialUsageReport("cambell_prince@sil.org" /*CHANGE THIS*/, "", new int[] { 1, 5, 20, 40, 60, 80, 100 });
        }
    
    }
}