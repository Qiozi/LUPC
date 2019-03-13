using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;

namespace KKWStore
{
    public partial class frmStaffEdit : Form
    {
        db.qstoreEntities context = new db.qstoreEntities();
        int Uid = -1;
        db.tb_user UM = null;
        public frmStaffEdit(int id)
        {
            Uid = id;
            InitializeComponent();
            this.Shown += new EventHandler(frmStaffEdit_Shown);
        }

        void frmStaffEdit_Shown(object sender, EventArgs e)
        {
            if (Uid > 0)
            {
                this.btn_Save.Text = "修改(&M)";
                UM = context.tb_user.Single(u => u.id.Equals(Uid));
                textBox_code.Text = UM.user_code;
                textBox_comment.Text = UM.comment;
                textBox_name.Text = UM.user_name;
                textBox_phone.Text = UM.phone;
                textBox_section.Text = UM.section;
                checkBoxShowit.Checked = !UM.showit.Value;
            }
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            if (Uid < 0)
                UM = new db.tb_user();
            UM.user_name = this.textBox_name.Text.Trim();
            UM.user_code = this.textBox_code.Text.Trim();
            UM.phone = this.textBox_phone.Text.Trim();
            UM.section = this.textBox_section.Text.Trim();
            UM.comment = this.textBox_comment.Text.Trim();
            UM.showit = !this.checkBoxShowit.Checked;
            if (Uid < 0)
            {
                UM.regdate = DateTime.Now;
                context.tb_user.Add(UM);
            }
            context.SaveChanges();
            //if (Uid > 0)
            //    UM.Save();
            //else
            //    UM.Create();
            this.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox_name_TextChanged(object sender, EventArgs e)
        {
            TextBox TB = (TextBox)sender;
            string text = TB.Text;
            TB.Text = Helper.CharacterHelper.ToDBC(text);
        }

        private void textBox_code_TextChanged(object sender, EventArgs e)
        {
            TextBox TB = (TextBox)sender;
            string text = TB.Text;
            TB.Text = Helper.CharacterHelper.ToDBC(text);
        }

        private void textBox_phone_TextChanged(object sender, EventArgs e)
        {
            TextBox TB = (TextBox)sender;
            string text = TB.Text;
            TB.Text = Helper.CharacterHelper.ToDBC(text);
        }
    }
}
