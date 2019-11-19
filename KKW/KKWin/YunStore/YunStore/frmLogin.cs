using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace YunStore
{
    public partial class frmLogin : Form
    {
        DB.qstoreEntities context = new DB.qstoreEntities();

        public frmLogin()
        {
            InitializeComponent();
            this.Shown += FrmLogin_Shown;
        }

        private void FrmLogin_Shown(object sender, EventArgs e)
        {
            BindUserList();
        }

        void BindUserList()
        {
            var list = context.tb_user.Where(p => p.showit.HasValue && p.showit.Value.Equals(true)).ToList();
            comboBox1.DataSource = list;
            comboBox1.DisplayMember = "user_name";
            comboBox1.ValueMember = "id";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var um = (DB.tb_user)comboBox1.SelectedItem;

            string pwd = this.textBoxPwd.Text;
            if (pwd == "")
            {
                textBoxPwd.Focus();
                MessageBox.Show("请输入密码", "提示", MessageBoxButtons.OK);
                return;
            }

            string md = Toolkits.MD5Checker.Encode(pwd);
            // MessageBox.Show(md);
            if (Toolkits.MD5Checker.Encode(pwd) == um.user_pwd
                && um.showit.Value)
            {
                BLL.Config.StaffGid = um.Gid;
                BLL.Config.StaffName = um.user_name;

                this.DialogResult = DialogResult.Yes;
                this.Close();
            }
            else
            {
                // this.DialogResult = System.Windows.Forms.DialogResult.No;
                MessageBox.Show("密码不对", "提示", MessageBoxButtons.OK);
                return;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
