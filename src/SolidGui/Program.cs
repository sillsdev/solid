using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SolidGui
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            MainWindowPM model = new MainWindowPM();
            MainWindowView form = new MainWindowView(model);
            model.DictionaryProcessed += form.OnDictionaryProcessed;
            Application.Run(form);
        }
    }
}