using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Palaso.Code;
using Palaso.UI.WindowsForms.WritingSystems;

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

		public void NationalWritingSystemBindPresenter(WritingSystemSetupModel model)
		{
			_wscNational.BindToModel(model);
		}

		public void RegionalWritingSystemBindPresenter(WritingSystemSetupModel model)
		{
			_wscRegional.BindToModel(model);
		}

		public void VernacularWritingSystemBindPresenter(WritingSystemSetupModel model)
		{
			_wscVernacular.BindToModel(model);
		}

		public void ToWritingSystemBindPresenter(WritingSystemSetupModel model)
		{
			_wscTo.BindToModel(model);
		}

		public string FromMatching
		{
			get { return _tbFieldsMatching.Text; }
			set { _tbFieldsMatching.Text = value; }
		}

		public void ShowAdvanced()
		{
			throw new NotImplementedException();
		}

		public void HideAdvanced()
		{
			throw new NotImplementedException();
		}

		public void CloseForm()
		{
			var form = ParentForm;
			if (form != null)
			{
				form.Close();
			}
		}

		private void OnApply_Click(object sender, EventArgs e)
		{
			Presenter.OnApplyClick();
		}

		private void OnAdvanced_Click(object sender, EventArgs e)
		{
			Presenter.OnAdvancedClick();
		}
	}
}
