// Copyright (c) 2007-2014 SIL International
// Licensed under the MIT license: opensource.org/licenses/MIT

using System;
using System.IO;
using System.Windows.Forms;
using SIL.Reporting;
using SolidGui.Properties;
using SolidGui.Engine;
using SIL.Windows.Forms.Keyboarding;
using SIL.WritingSystems;
using SolidGui.Setup;
using SIL.Windows.Forms.Reporting;

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

            L10NSharp.LocalizationManager.StrictInitializationMode = false;

            Sldr.Initialize();

            MainWindowPM model = new MainWindowPM();  
            MainWindowView form = new MainWindowView(model);
            form.BindModels(model);


            KeyboardController.Initialize();  //TODO: verify that calling this repeatedly is ok -JMC

            if (args.Length > 0) TryToOpen(args[0], model, form);           

            //form.UpdateDisplay();
            try
            {
                Application.Run(form); 
            }

            catch (Exception error)
            {
                string msg = "There was an unexpected error:\r\n" + error.Message;
                SIL.Reporting.ErrorReport.ReportFatalMessageWithStackTrace(msg, error);
            }
            finally
            {
                KeyboardController.Shutdown();
            }
            Settings.Default.Save();
            Sldr.Cleanup();
        }

        static void TryToOpen(string fileName, MainWindowPM model, MainWindowView form)
        {
            bool forceUnicode = false;
            if (!File.Exists(fileName)) return;

            if (fileName.EndsWith(".solid"))
            {
                fileName = SolidSettings.GetDictionaryFilePathFromSettingsPath(fileName);
                if (String.IsNullOrEmpty(fileName)) return;  // fixes bug #1285
            }
            string templatePath = null;
            if (model.ShouldAskForTemplateBeforeOpening(fileName))  //check validity of .solid file
            {
                forceUnicode = EncodingChooser.UserWantsUnicode(fileName);
                templatePath = form.RequestTemplatePath(fileName, false);
                if (string.IsNullOrEmpty(templatePath)) { return; } //they cancelled
            }
            if (model.OpenDictionary(fileName, templatePath, forceUnicode))
            {
                form.OnFileLoaded(fileName);
            }
        }

        private static void SetupErrorHandling()
        {
            ExceptionHandler.Init(new WinFormsExceptionHandler());
            Logger.Init();
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