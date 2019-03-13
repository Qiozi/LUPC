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
    public partial class frmInList : Form
    {
        db.qstoreEntities context = new db.qstoreEntities();

        public frmInList()
        {
            InitializeComponent();
            this.Shown += new EventHandler(frmInList_Shown);
        }

        void frmInList_Shown(object sender, EventArgs e)
        {
            dateTimePicker1.Value = DateTime.Now.AddDays(-15);
            dateTimePicker2.Value = DateTime.Now;

            BindList();
        }

        void BindList()
        {
            DataTable dt = db.InInvoiceModel_p.GetInIvoiceList(dateTimePicker1.Value, dateTimePicker2.Value);
            this.listView1.Items.Clear();
            int total = 0;
            decimal totalCharge = 0M;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];

                ListViewItem li = new ListViewItem(dr["invoice_code"].ToString());
                li.Tag = dr["id"].ToString();
                li.SubItems.Add(DateTime.Parse(dr["regdate"].ToString()).ToString("yyyy-MM-dd"));
                li.SubItems.Add(dr["c"].ToString());
                li.SubItems.Add(dr["s"].ToString());
                li.SubItems.Add("￥" + dr["pay_total"].ToString());
                li.SubItems.Add(dr["note"].ToString());
                listView1.Items.Add(li);

                int t;
                int.TryParse(dr["s"].ToString(), out t);
                total += t;

                decimal c;
                decimal.TryParse(dr["pay_total"].ToString(), out c);
                totalCharge += c;
            }
            toolStripStatusLabelALLQty.Text = total.ToString();
            toolStripStatusLabelCharge.Text = "￥" + totalCharge.ToString("#,##0.00");
            toolStripStatusLabelRecordALLQty.Text = dt.Rows.Count.ToString();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            frmInStore2 fw = new frmInStore2(-1, false);
            fw.StartPosition = FormStartPosition.CenterParent;
            fw.ShowDialog();
            BindList();
        }

        private void 修改ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ListView lv = listView1 as ListView;
            if (lv.SelectedItems != null)
            {
                if (lv.SelectedItems.Count == 1)
                {
                    int id;
                    int.TryParse(lv.SelectedItems[0].Tag.ToString(), out id);
                    frmInStore2 fw = new frmInStore2(id, false);
                    fw.StartPosition = FormStartPosition.CenterParent;
                    fw.ShowDialog();
                    BindList();
                }
            }
        }

        private void 查看明细ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListView lv = listView1 as ListView;
            if (lv.SelectedItems != null)
            {
                if (lv.SelectedItems.Count == 1)
                {
                    int id;
                    int.TryParse(lv.SelectedItems[0].Tag.ToString(), out id);
                    frmInStore2 fw = new frmInStore2(id, true);
                    fw.StartPosition = FormStartPosition.CenterParent;
                    fw.ShowDialog();
                    BindList();
                }
            }
        }

        private void 修改条码所属按产品ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListView lv = listView1 as ListView;
            if (lv.SelectedItems != null)
            {
                if (lv.SelectedItems.Count == 1)
                {
                    int id;
                    int.TryParse(lv.SelectedItems[0].Tag.ToString(), out id);
                    frmModifySNOfProd f = new frmModifySNOfProd(id);
                    f.StartPosition = FormStartPosition.CenterParent;
                    f.ShowDialog();
                }
            }
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {

        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (!Helper.Config.IsAdmin)
            {
                MessageBox.Show("你没有权限，只有管理员才可以删除.", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show("你确定删除此次入库数据？删除将后不可覆原。", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.No)
                return;

            ListView lv = listView1 as ListView;
            if (lv.SelectedItems != null)
            {
                if (lv.SelectedItems.Count == 1)
                {
                    int id;
                    int.TryParse(lv.SelectedItems[0].Tag.ToString(), out id);

                    var iim = context.tb_in_invoice.SingleOrDefault(p => p.id.Equals(id));// InInvoiceModel.GetInInvoiceModel(id);
                    if (iim != null)
                    {
                        if (db.InInvoiceModel_p.SerialnoNoUsed(iim.invoice_code))
                        {
                            string delResult = db.InInvoiceModel_p.DelALLSerialNO(iim.invoice_code);
                            iim.pay_total = 0M;
                            iim.note = delResult;
                            context.SaveChanges();
                            BindList();
                        }
                        else
                        {
                            MessageBox.Show("条码已被使用，不能删除!!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                }
            }
        }

        private void buttonQuery_Click(object sender, EventArgs e)
        {
            BindList();
        }
    }
}
