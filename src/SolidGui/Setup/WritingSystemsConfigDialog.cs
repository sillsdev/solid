// Copyright (c) 2007-2014 SIL International
// Licensed under the MIT license: opensource.org/licenses/MIT

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SolidGui.Setup
{
    public partial class WritingSystemsConfigDialog : Form
    {

        public WritingSystemsConfigDialog()
        {
            InitializeComponent();
        }

        public WritingSystemsConfigPresenter.IView WritingSystemsConfigView
        {
            get { return _writingSystemsConfigView; }
        }

    }
}
