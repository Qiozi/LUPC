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
    public partial class frmCheckStoreList : Form
    {
        db.qstoreEntities context = new db.qstoreEntities();
        public frmCheckStoreList()
        {
            InitializeComponent();
            this.Shown += new EventHandler(frmCheckStoreList_Shown);
        }

        void frmCheckStoreList_Shown(object sender, EventArgs e)
        {
            BindList();
        }

        void BindList()
        {
            var csms = context.tb_check_store.OrderByDescending(p=>p.id).ToList();// CheckStoreModel.FindAll();
            this.listView1.Items.Clear();
            foreach (var csm in csms)
            {
                var checkStatusTxt = csm.CheckStatus == (int)enums.CheckResultStatus.Normal ? "正常" : "错误";
                var checkStatusTxtForeColor = csm.CheckStatus == (int)enums.CheckResultStatus.Normal ? Color.Green : Color.Red;
                var autoRunStatusTxt = csm.AutoRunStatus == (int)enums.CheckAutoRunStatus.Wait ? "待处理" : "已处理";
                var autoRunStatusTxtForeColor = csm.AutoRunStatus == (int)enums.CheckAutoRunStatus.Wait ? Color.Red : Color.Green;

                var li = new ListViewItem(csm.check_regdate.ToString("yyyy-MM-dd"));
                li.Tag = csm.id;
                li.SubItems.Add(csm.staff);
                li.SubItems.Add(checkStatusTxt);
                li.SubItems.Add(autoRunStatusTxt);
                li.SubItems.Add(csm.valid_quantity.ToString());
                li.SubItems.Add(csm.err_quantity.ToString());
                li.SubItems.Add(csm.comment);
                li.SubItems[2].ForeColor = checkStatusTxtForeColor;
                li.SubItems[3].ForeColor = autoRunStatusTxtForeColor;
                this.listView1.Items.Add(li);
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems.Count > 0)
            {
                int id = int.Parse(this.listView1.SelectedItems[0].Tag.ToString());
                frmCheckStoreEdit fcse = new frmCheckStoreEdit(id , new List<string>());
                fcse.StartPosition = FormStartPosition.CenterParent;
                fcse.ShowDialog();
            }
            else
            {
                MessageBox.Show("请选择需要查看的纪录", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            frmCheckStoreEdit fcse = new frmCheckStoreEdit(-1, new List<string>());
            fcse.StartPosition = FormStartPosition.CenterParent;
            if (fcse.ShowDialog() == System.Windows.Forms.DialogResult.Yes)
            {
                BindList();
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {

        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
