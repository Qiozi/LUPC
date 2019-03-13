using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KKWStore
{
    public partial class frmStaffSelected : Form
    {
        db.qstoreEntities context = new db.qstoreEntities();

        public frmStaffSelected()
        {
            InitializeComponent();
            this.Shown += new EventHandler(frmStaffSelected_Shown);
        }

        void frmStaffSelected_Shown(object sender, EventArgs e)
        {
            BindList();
        }

        void BindList()
        {
            var um = context.tb_user.ToList();
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
                var um = context.tb_user.Single(u => u.id.Equals(uid));
                Helper.TempInfo.StaffName = um.user_name;
                this.DialogResult = System.Windows.Forms.DialogResult.Yes;
                this.Close();
            }
            else
                MessageBox.Show("请选择内部职员", "警告", MessageBoxButtons.OK);
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
    }
}
