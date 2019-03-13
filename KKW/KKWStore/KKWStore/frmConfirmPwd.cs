using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace KKWStore
{
    public partial class frmConfirmPwd : Form
    {
        public frmConfirmPwd()
        {
            InitializeComponent();

            if (Helper.Config.IsAdmin)
            {
                
                this.DialogResult = System.Windows.Forms.DialogResult.Yes;
                this.Close();
            }
            else
                this.DialogResult = System.Windows.Forms.DialogResult.No;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.No;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string pwd = textBox1.Text;
            //if (Helper.MD5.Encode(pwd) == Helper.)
            //{
            //    this.DialogResult = System.Windows.Forms.DialogResult.Yes;
            //    this.Close();
            //}
            //else
            {
                this.DialogResult = System.Windows.Forms.DialogResult.No;
                MessageBox.Show("密码不对", "提示", MessageBoxButtons.OK);
            }
        }
    }
}
