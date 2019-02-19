using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LUComputers
{
    public partial class Ltd_DanDh : Form
    {
        public Ltd_DanDh()
        {
            InitializeComponent();
        }

        private void button_brower_Click(object sender, EventArgs e)
        {
            DialogResult dr = openFileDialog1.ShowDialog();
            if (dr == DialogResult.OK)
            {
                this.textBox1.Text = this.openFileDialog1.FileName;          
                
            }
        }

        public void LoadToDB(string filepath)
        {
            //if (filepath.Length > 0)
            //    new LUComputers.Watch.DanDh(filepath);
            //else
            //    MessageBox.Show("please input file name");
        }

        private void button_upload_Click(object sender, EventArgs e)
        {
            LoadToDB(this.textBox1.Text.Trim());



            MessageBox.Show("OK");
        }
    }
}
