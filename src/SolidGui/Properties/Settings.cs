using System.ComponentModel;
using Palaso.Reporting;

namespace SolidGui.Properties {
    
    
    // This class allows you to handle specific events on the settings class:
    //  The SettingChanging event is raised before a setting's value is changed.
    //  The PropertyChanged event is raised after a setting's value is changed.
    //  The SettingsLoaded event is raised after the setting values are loaded.
    //  The SettingsSaving event is raised before the setting values are saved.
	internal sealed partial class Settings
	{

		public Settings()
		{
			Initialize();
		}

		public Settings(string settingsKey)
			: base(settingsKey)
		{
			Initialize();
		}

		public Settings(IComponent owner)
			: base(owner)
		{
			Initialize();
		}

		public Settings(IComponent owner, string settingsKey)
			: base(owner, settingsKey)
		{
			Initialize();
		}

		private void Initialize()
		{
			if (Reporting == null)
			{
				Reporting = new ReportingSettings();
			}
		}
		private void SettingChangingEventHandler(object sender, System.Configuration.SettingChangingEventArgs e)
		{
			// Add code to handle the SettingChangingEvent event here.
		}

		private void SettingsSavingEventHandler(object sender, System.ComponentModel.CancelEventArgs e)
		{
			// Add code to handle the SettingsSaving event here.
		}


		public override void Upgrade()
		{
			base.Upgrade();

			if (Reporting == null)
			{
				Reporting = new ReportingSettings();
			}

		}
	}

}
