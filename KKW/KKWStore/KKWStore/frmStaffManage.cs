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
    public partial class frmStaffManage : Form
    {
        db.qstoreEntities context = new db.qstoreEntities();
        public frmStaffManage()
        {
            InitializeComponent();
            this.Shown += new EventHandler(frmStaffManage_Shown);
        }

        void frmStaffManage_Shown(object sender, EventArgs e)
        {
            BindList();
            BindPermanent();
        }

        void BindPermanent()
        {
            this.checkedListBox1.Items.Clear();
            foreach (int i in Enum.GetValues(typeof(enums.PermanentModel)))
            {
                this.checkedListBox1.Items.Add(Enum.GetName(typeof(enums.PermanentModel), i));
            }
        }

        void BindList()
        {
            var um = context.tb_user.ToList();// UserModel.FindAll();
            listView1.Items.Clear();
            foreach (var u in um)
            {
                ListViewItem li = new ListViewItem(u.user_code);
                li.Tag = u.id;
                li.SubItems.Add(u.user_name);
                li.SubItems.Add(u.section);
                li.SubItems.Add(u.phone);
                listView1.Items.Add(li);
            }
        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                var uid = int.Parse(listView1.SelectedItems[0].Tag.ToString());
                frmInStore.StoreUserModel = context.tb_user.Single(u => u.id.Equals(uid));
                Helper.Config.CurrentUser.user_name = frmInStore.StoreUserModel.user_name;
                this.DialogResult = System.Windows.Forms.DialogResult.Yes;
                this.Close();
            }
            else
            {
                MessageBox.Show("请选择内部职员", "警告", MessageBoxButtons.OK);
            }
        }

        private void toolStripLabel2_Click(object sender, EventArgs e)
        {
            frmStaffEdit fse = new frmStaffEdit(-1);
            fse.StartPosition = FormStartPosition.CenterParent;
            if (fse.ShowDialog() == System.Windows.Forms.DialogResult.Yes)
            {
                BindList();
            }
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems.Count > 0)
            {
                frmStaffEdit fse = new frmStaffEdit(int.Parse(this.listView1.SelectedItems[0].Tag.ToString()));
                fse.StartPosition = FormStartPosition.CenterParent;
                if (fse.ShowDialog() == System.Windows.Forms.DialogResult.Yes)
                {
                    BindList();
                }
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems != null)
            {
                if (listView1.SelectedItems.Count == 1)
                {
                    int id;
                    int.TryParse(listView1.SelectedItems[0].Tag.ToString(), out id);
                    setPermanentChecked(id);
                }
            }
        }

        void setPermanentChecked(int UserId)
        {
            var umm = context.tb_user_model.Where(u => u.UserId.Equals(UserId)).ToList();// UserModelModel.FindAllByProperty("UserId", UserId);
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                checkedListBox1.SetItemChecked(i, false);
                if (umm != null)
                {
                    for (int j = 0; j < umm.Count; j++)
                    {
                        if (Enum.GetName(typeof(enums.PermanentModel), umm[j].ModelId) == checkedListBox1.GetItemText(checkedListBox1.Items[i]))
                        {
                            checkedListBox1.SetItemChecked(i, true);
                        }
                    }
                }
            }
        }

        private void checkedListBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            int uid = 0;
            if (listView1.SelectedItems == null)
                return;
            if (listView1.SelectedItems.Count == 1)
            {

                int.TryParse(listView1.SelectedItems[0].Tag.ToString(), out uid);
            }
            else
            {
                return;
            }
            //UserModelModel.DeleteAll(" UserId=" + uid);
            var query = context.tb_user_model.Where(p => p.UserId.Equals(uid)).ToList();
            foreach (var model in query)
            {
                context.tb_user_model.Remove(model);
            }
            context.SaveChanges();
                
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                if (checkedListBox1.GetItemChecked(i))
                {
                    string txt = checkedListBox1.GetItemText(checkedListBox1.Items[i]);
                    int pid = (int)(enums.PermanentModel)Enum.Parse(typeof(enums.PermanentModel), txt);

                    var umm = new db.tb_user_model();
                    umm.ModelId = pid;
                    umm.UserId = uid;
                    
                    context.tb_user_model.Add(umm);
                    context.SaveChanges();
                }
            }
        }

        private void toolStripLabel2_Click_1(object sender, EventArgs e)
        {
            frmStaffEdit fse = new frmStaffEdit(-1);
            fse.StartPosition = FormStartPosition.CenterParent;
            fse.ShowDialog();
        }

        private void listView1_DoubleClick_1(object sender, EventArgs e)
        {
            if (listView1.SelectedItems == null)
                return;
            if (listView1.SelectedItems.Count == 1)
            {
                int uid;
                int.TryParse(listView1.SelectedItems[0].Tag.ToString(), out uid);
                frmStaffEdit fse = new frmStaffEdit(uid);
                fse.StartPosition = FormStartPosition.CenterParent;
                fse.ShowDialog();
            }
        }
    }
}
