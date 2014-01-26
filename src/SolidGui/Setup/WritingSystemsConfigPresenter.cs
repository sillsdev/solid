// Copyright (c) 2007-2014 SIL International
// Licensed under the MIT license: opensource.org/licenses/MIT

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

            void ToWritingSystemBindPresenter(WritingSystemSetupModel model);

            // string FromMatching { get; set; } // Removed this and the corresponding UI field since they weren't doing anything. -JMC Jan 2014
            string FromWritingSystem { get; set; }
            void SetFromItems(string[] items);

            void ShowAdvanced();
            void HideAdvanced();

            void CloseForm();
            void ShowWritingSystemSetupDialog(WritingSystemSetupModel model);
        }

        private readonly WritingSystemSetupModel _toWritingSystemSetupModel;

        public WritingSystemsConfigPresenter(SolidSettings solidSettings, IWritingSystemRepository writingSystemRepository, IView view)
        {
            View = view;
            View.BindToPresenter(this);
            SolidSettings = solidSettings;
            WritingSystemRepository = writingSystemRepository;
            _toWritingSystemSetupModel = new WritingSystemSetupModel(writingSystemRepository);
            View.ToWritingSystemBindPresenter(_toWritingSystemSetupModel);
            View.SetFromItems(FromWritingSystems());
        }

        private IWritingSystemRepository WritingSystemRepository { get; set; }

        private SolidSettings SolidSettings { get; set; }

        private IView View { get; set; }

        public string[] FromWritingSystems()
        {
            return SolidSettings.MarkerSettings.Select(markerSetting => markerSetting.WritingSystemRfc4646).Distinct().ToArray();
        }

        public void OnSetupWritingSystemsClick()
        {
            View.ShowWritingSystemSetupDialog(_toWritingSystemSetupModel);
        }

        public void OnApplyClick()
        {
            string fromWritingSystem = View.FromWritingSystem;

            // TODO Rename markers as per the advance view
            foreach (var markerSetting in SolidSettings.MarkerSettings)
            {
                if (markerSetting.WritingSystemRfc4646 == fromWritingSystem)
                {
                    markerSetting.WritingSystemRfc4646 = _toWritingSystemSetupModel.CurrentRFC4646;
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
