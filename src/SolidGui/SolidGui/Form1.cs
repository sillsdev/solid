using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SolidGui
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            List<string> masterRecordList = new List<string>();
            masterRecordList.Add("something0");
            masterRecordList.Add("something1");
            masterRecordList.Add("something2");
            masterRecordList.Add("something3");

            RecordNavigatorPresentationModel navigator = new RecordNavigatorPresentationModel();
            navigator.MasterRecordList = masterRecordList;

            navigator.ActiveFilter = new RecordFilter();

            _recordNavigatorView1.Model = navigator;
        }
    }
}