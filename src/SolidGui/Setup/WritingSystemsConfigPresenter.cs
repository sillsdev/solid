using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Palaso.UI.WindowsForms.WritingSystems;
using Palaso.WritingSystems;
using SolidGui.Engine;

namespace SolidGui.Setup
{
	public class WritingSystemsConfigPresenter
	{
		public interface IView
		{
			void BindToPresenter(WritingSystemsConfigPresenter presenter);

			void NationalWritingSystemBindPresenter(WritingSystemSetupModel model);
			void RegionalWritingSystemBindPresenter(WritingSystemSetupModel model);
			void VernacularWritingSystemBindPresenter(WritingSystemSetupModel model);
			void ToWritingSystemBindPresenter(WritingSystemSetupModel model);

			string FromMatching { get; set; }

			void ShowAdvanced();
			void HideAdvanced();

			void CloseForm();
		}

		private readonly WritingSystemSetupModel _nationalWritingSystemSetupModel;
		private readonly WritingSystemSetupModel _regionalWritingSystemSetupModel;
		private readonly WritingSystemSetupModel _vernacularWritingSystemSetupModel;
		private readonly WritingSystemSetupModel _toWritingSystemSetupModel;

		public WritingSystemsConfigPresenter(SolidSettings solidSettings, IWritingSystemRepository writingSystemRepository, IView view)
		{
			View = view;
			View.BindToPresenter(this);
			SolidSettings = solidSettings;
			_nationalWritingSystemSetupModel = new WritingSystemSetupModel(writingSystemRepository);
			View.NationalWritingSystemBindPresenter(_nationalWritingSystemSetupModel);
			_regionalWritingSystemSetupModel = new WritingSystemSetupModel(writingSystemRepository);
			View.RegionalWritingSystemBindPresenter(_regionalWritingSystemSetupModel);
			_vernacularWritingSystemSetupModel = new WritingSystemSetupModel(writingSystemRepository);
			View.VernacularWritingSystemBindPresenter(_vernacularWritingSystemSetupModel);
			_toWritingSystemSetupModel = new WritingSystemSetupModel(writingSystemRepository);
			View.ToWritingSystemBindPresenter(_toWritingSystemSetupModel);
		}

		private SolidSettings SolidSettings { get; set; }

		private IView View { get; set; }

		public void OnApplyClick()
		{

			// TODO
			// Load up the template
			// Find markers for nat, reg, and vern
			// Rename each marker as per the view
			// or
			// Rename markers as per the advance view
			foreach (var markerSetting in SolidSettings.MarkerSettings)
			{
				if (markerSetting.WritingSystemRfc4646 == "nat")
				{
					markerSetting.WritingSystemRfc4646 = _nationalWritingSystemSetupModel.CurrentRFC4646;
				} else if (markerSetting.WritingSystemRfc4646 == "reg")
				{
					markerSetting.WritingSystemRfc4646 = _regionalWritingSystemSetupModel.CurrentRFC4646;
				} else if (markerSetting.WritingSystemRfc4646 == "vern")
				{
					markerSetting.WritingSystemRfc4646 = _vernacularWritingSystemSetupModel.CurrentRFC4646;
				}
			}
			View.CloseForm();

		}

		public void OnAdvancedClick()
		{
			throw new NotImplementedException();
		}
	}
}
