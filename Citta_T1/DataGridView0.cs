﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Citta_T1
{
    public partial class DataGridView0 : UserControl
    {
        private string overViewFilePath = "D:/work/Citta_T1/datas/text.txt";
        public DataGridView0()
        {
            InitializeComponent();
            InitializeDgv(overViewFilePath);
        }

        private void DataGridView_Load(object sender, EventArgs e)
        {
            //_InitializeRows();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
