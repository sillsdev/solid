﻿// Copyright (c) 2007-2014 SIL International
// Licensed under the MIT license: opensource.org/licenses/MIT

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SIL.Code;
using SIL.Windows.Forms.WritingSystems;

namespace SolidGui.Setup
{
    public partial class WritingSystemsConfigView : UserControl, WritingSystemsConfigPresenter.IView
    {
        public WritingSystemsConfigView()
        {
            InitializeComponent();
            if (DesignMode)
                return;
        }

        public void BindToPresenter(WritingSystemsConfigPresenter presenter)
        {
            Presenter = presenter;
        }

        private WritingSystemsConfigPresenter Presenter { get; set; }

        public void ToWritingSystemBindPresenter(WritingSystemSetupModel model)
        {
            _wscTo.BindToModel(model);
        }

        public string FromWritingSystem
        {
            get { return _cbFrom.SelectedItem as string; }
            set { _cbFrom.SelectedItem = value; }
        }

        public void ShowAdvanced()
        {
            throw new NotImplementedException();
        }

        public void HideAdvanced()
        {
            throw new NotImplementedException();
        }

        public void SetFromItems(string[] items)
        {
            _cbFrom.Items.AddRange(items);
        }

        public void CloseForm()
        {
            Form form = ParentForm;
            if (form != null)
            {
                form.Close();
            }
        }

        public void ShowWritingSystemSetupDialog(WritingSystemSetupModel model)
        {
            var dialog = new WritingSystemSetupDialog(model);
            dialog.ShowDialog(ParentForm);
        }

        private void OnApply_Click(object sender, EventArgs e)
        {
            int c = Presenter.OnApplyClick();
            if (c > 0)
            {
                string msg = String.Format("Changed the writing system in {0} field markers. (Any hidden, zero-count fields that matched were updated too.)", c);
                MessageBox.Show(msg, "Writing Systems Replaced");
            }
            this.CloseForm();
        }

        private void OnAdvanced_Click(object sender, EventArgs e)
        {
            Presenter.OnAdvancedClick();
        }

        private void OnSetupWritingSystems_Click(object sender, EventArgs e)
        {
            Presenter.OnSetupWritingSystemsClick();
        }

    }
}
