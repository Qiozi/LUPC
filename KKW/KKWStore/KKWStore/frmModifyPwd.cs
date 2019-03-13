using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;

namespace KKWStore
{
    public partial class frmModifyPwd : Form
    {
        db.qstoreEntities context = new db.qstoreEntities();

        public frmModifyPwd()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string oldPwd = textBox1.Text;
            string pwd1 = textBox2.Text;
            string pwd2 = textBox3.Text;
            if (pwd1 != pwd2)
            {
                MessageBox.Show("输入的密码不相同，请重新输入", "提示");
                textBox2.Text = "";
                textBox3.Text = "";
                textBox2.Focus();
                return;
            }

            if (db.UserModel.ChangePwd(context, oldPwd, pwd1,Helper.Config.CurrentUser))
            {
                MessageBox.Show("密码修改完成", "提示");
                this.Close();
            }
            else
            {
                MessageBox.Show("旧密码输入错误，请重新输入", "提示");
                textBox1.Text = "";
                textBox1.Focus();
                return;
            }

        }
    }
}
