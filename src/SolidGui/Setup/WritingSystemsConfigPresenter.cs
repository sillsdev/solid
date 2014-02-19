// Copyright (c) 2007-2014 SIL International
// Licensed under the MIT license: opensource.org/licenses/MIT

// (A better name would be SpecifyWritingSystems, or some such. Maybe should name any unit tests accordingly.)
// This is the Presenter part of a Model-View-Presenter (MVP) implementation. It seems to me to be the one in
// which "Both hold a reference to each other forming a circular dependency... The view responds to 
// events by calling methods in the presenter. The presenter read/modifies data from the view through 
// exposed properties." http://programmers.stackexchange.com/questions/60774/model-view-presenter-implementation-thoughts?rq=1 (and also Bil's answer there)
// This dialog was Cambell's latest addition prior to my involvement, so its MVP is likely to be preferred on those grounds too.
// Presumably unit tests should be added as a parallel implementation of WritingSystemsConfigPresenter.IView.

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

            // Disabling for now. -JMC Jan 2014
            //void ShowAdvanced();
            //void HideAdvanced();

            void CloseForm();
            void ShowWritingSystemSetupDialog(WritingSystemSetupModel model);
        }

        private readonly WritingSystemSetupModel _toWritingSystemSetupModel;

        private MainWindowPM _mainWindowPm { get; set; }

        public WritingSystemsConfigPresenter(MainWindowPM mainWindowPm, IWritingSystemRepository writingSystemRepository, IView view)
        {
            _mainWindowPm = mainWindowPm;
            View = view;
            View.BindToPresenter(this);
            SolidSettings = mainWindowPm.Settings;
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


        public int OnApplyClick()
        // Note that I've removed that unimplemented field from the dialog for now. I've also
        // moved the actual work into the model. -JMC Jan 2014
        {
            string fromWritingSystem = View.FromWritingSystem;

            // TODO Rename markers as per the advanced view
            int c = SolidSettings.FindReplaceWs(fromWritingSystem, _toWritingSystemSetupModel.CurrentRFC4646);
            if (c > 0)
            {
                _mainWindowPm.needsSave = true;  // JMC: Should we also trigger a MarkerSettingPossiblyChanged event? Probably no need.
            }
            return c;
        }

        public void OnAdvancedClick()
        {
            throw new NotImplementedException();
        }
    }
}
