using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace KKWStore
{
    public partial class frmProductExport : Form
    {
        public frmProductExport()
        {
            InitializeComponent();
        }

        private void button_export_all_Click(object sender, EventArgs e)
        {

        }

        private void button_export_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Vers.ExportMaxQuantity = (int)numericUpDown1.Value;
            this.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
