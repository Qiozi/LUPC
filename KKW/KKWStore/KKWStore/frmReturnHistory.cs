using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace KKWStore
{
    public partial class frmReturnHistory : Form
    {
        public frmReturnHistory()
        {
            InitializeComponent();
            this.Shown += new EventHandler(frmReturnHistory_Shown);
            this.FormClosed += new FormClosedEventHandler(frmReturnHistory_FormClosed);
        }

        void frmReturnHistory_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Yes;
        }

        void BindList()
        {
            string sn = this.toolStripTextBox1.Text.Trim();
            if (sn.IndexOf("A") > -1)
                sn = sn.Replace("A", "");
            DataTable dt = db.ReturnHistoryModel_p.GetModelsBySearch(sn);
            this.listView1.Items.Clear();
            foreach (DataRow dr in dt.Rows)
            {
                ListViewItem li = new ListViewItem(dr["SerialNo"].ToString());
                li.Tag = dr["id"].ToString();
                li.SubItems.Add(dr["receiverName"].ToString());
                li.SubItems.Add(dr["p_name"].ToString());
                li.SubItems.Add(dr["Return_regdate"].ToString());
                listView1.Items.Add(li);
            }
            this.toolStripStatusLabel1.Text = string.Format("共有 {0} 条退货纪录", dt.Rows.Count);
        }

        void frmReturnHistory_Shown(object sender, EventArgs e)
        {
            // BindList();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            frmReturnEdit fre = new frmReturnEdit();
            fre.StartPosition = FormStartPosition.CenterParent;
            if (fre.ShowDialog() == System.Windows.Forms.DialogResult.Yes)
            {
                BindList();
            }
        }

        private void toolStripTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                BindList();
            }
        }

        private void toolStripTextBox1_Click(object sender, EventArgs e)
        {
           
        }

        private void toolStripTextBox1_TextChanged(object sender, EventArgs e)
        {
            ToolStripTextBox TB = (ToolStripTextBox)sender;
            string text = TB.Text;
            TB.Text = Helper.CharacterHelper.ToDBC(text);
        }
    }
}
