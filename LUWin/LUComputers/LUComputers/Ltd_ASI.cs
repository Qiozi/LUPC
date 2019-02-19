using LUComputers.DBProvider;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LUComputers
{
    public partial class Ltd_ASI : Form
    {

        public Ltd_ASI()
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

        private void button_update_Click(object sender, EventArgs e)
        {
            string table_name = "";
            LoadToDB(this.textBox1.Text.Trim(), ref table_name);
            this.dataGridView1.DataSource = Config.ExecuteDateTable("Select * from " + table_name);
        }

        public void LoadToDB(string filepath, ref string table_name)
        {
            if (filepath.Length > 0)
            {
                Watch.ASI asi = new LUComputers.Watch.ASI();
                
                asi.Run(filepath, true, ref table_name);
            }
            else
                MessageBox.Show("please input file name");
        }

        void asi_Runing(LUComputers.Watch.ASI sender, LUComputers.Watch.LoadRunEventArgs e)
        {
            string str = e.viewString;
            this.button_update.Text = str;
            this.toolStripProgressBar1.Maximum = int.Parse(str.Split(new char[] { '|' })[1].ToString());
            this.toolStripProgressBar1.Value = int.Parse(str.Split(new char[] { '|' })[0].ToString());
        }

     
    }
}
