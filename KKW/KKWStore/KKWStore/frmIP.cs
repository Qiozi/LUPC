using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using System.IO;

namespace KKWStore
{
    public partial class frmIP : Form
    {
        db.qstoreEntities context = new db.qstoreEntities();

        public frmIP()
        {
            InitializeComponent();
            this.Shown += new EventHandler(frmIP_Shown);
        }

        void frmIP_Shown(object sender, EventArgs e)
        {
            Helper.Config.IsAdmin = false;

            BindUserList();
            Helper.Config.UserPermanentList = new List<db.tb_user_model>(); // 初始化权限列表对象
        }

        void BindUserList()
        {
            var list = context.tb_user.Where(p=>p.showit.HasValue && p.showit.Value.Equals(true)).ToList();
            comboBox1.DataSource = list;
            comboBox1.DisplayMember = "user_name";
            comboBox1.ValueMember = "id";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.No;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var um = (db.tb_user)comboBox1.SelectedItem;

            string pwd = this.textBoxPwd.Text;
            if (pwd == "")
            {
                textBoxPwd.Focus();
                MessageBox.Show("请输入密码", "提示", MessageBoxButtons.OK);
                return;
            }

            string md = Helper.MD5.Encode(pwd);
            // MessageBox.Show(md);
            if (Helper.MD5.Encode(pwd) == um.user_pwd
                && um.showit.Value)
            {
                Helper.Config.IsVisitor = false;
                if (um.is_admin.HasValue && um.is_admin.Value)
                {
                    Helper.Config.IsAdmin = true;
                    //Helper.Config.IsVisitor = true;
                }
                else
                {
                    if (um.id == 14)
                    {
                        Helper.Config.IsVisitor = true;
                    }

                    // 缓存用户权限列表
                    var list = context.tb_user_model.Where(p => p.UserId.Equals(um.id)).ToList(); // UserModelModel.FindAllByProperty("UserId", um.id);
                    if (list != null)
                    {
                        for (int i = 0; i < list.Count; i++)
                            Helper.Config.UserPermanentList.Add(new db.tb_user_model
                            {
                                ModelId = list[i].ModelId,
                                UserId = um.id
                            });
                    }
                }
                Helper.Config.CurrentUser = um;
                
                this.DialogResult = System.Windows.Forms.DialogResult.Yes;
                this.Close();
            }
            else
            {
                // this.DialogResult = System.Windows.Forms.DialogResult.No;
                MessageBox.Show("密码不对", "提示", MessageBoxButtons.OK);
                return;
            }
        }

        private void frmIP_Load(object sender, EventArgs e)
        {

        }

        private void frmIP_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void textBoxPwd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button5_Click(null, null);
            }
        }
    }
}
