using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YunStore.Toolkits;

namespace YunStore
{
    public partial class frmCompManager : Form
    {
        DB.qstoreEntities _context = new DB.qstoreEntities();

        public frmCompManager()
        {
            InitializeComponent();
            this.Shown += FrmCompManager_Shown;
        }

        private void FrmCompManager_Shown(object sender, EventArgs e)
        {
            BindList1("");
            BindList2("");
        }

        void BindList1(string keyword = "")
        {
            var query = _context.tb_yun_fileinfo_company_stock
                .Where(me =>
                    me.ProdCode.Contains(keyword) ||
                    me.ProdName.Contains(keyword))
                    .OrderBy(me => me.ProdCode)
                    .ToList();

            this.listView1.Items.Clear();
            foreach (var item in query)
            {
                var li = new ListViewItem(item.ProdCode);
                li.Tag = item.Gid;
                li.SubItems.Add(item.ProdName);
                li.SubItems.Add(item.StaffName);
                li.SubItems.Add(Util.FormatDateTime(item.Regdate));
                li.SubItems.Add(item.Qty.ToString());
                li.SubItems.Add(item.Remark);

                listView1.Items.Add(li);
            }

            this.label1.Text = string.Format(@"一共有 {0} 个商品， {1} 个库存，总价：{2}",
                query.Count.ToString(),
                query.Sum(me => (int?)me.Qty).GetValueOrDefault().ToString(),
                query.Sum(me => (decimal?)me.Qty * me.Cost).GetValueOrDefault().ToString());
        }

        void BindList2(string keyword = "")
        {
            bool allStock = this.radioButtonAll.Checked;
            bool inStock = this.radioButtonIN.Checked;
            bool outStock = this.radioButtonOut.Checked;

            // mysql 5.6 + conn 6.10.8  ， string.isnullorempty + Contains 包含 不能使用。
            var query = string.IsNullOrEmpty(keyword)
                ? _context
                    .tb_yun_fileinfo_company_stock_record
                        .Where(me => true)
                        .OrderBy(me => me.ProdCode)
                        .ToList()
                : _context
                    .tb_yun_fileinfo_company_stock_record
                        .Where(me => me.ProdCode.Equals(keyword))
                        .OrderBy(me => me.ProdCode)
                        .ToList();

            if (inStock)
            {
                query = query.Where(me => me.InOut.Equals("IN")).ToList();
            }
            if (outStock)
            {
                query = query.Where(me => me.InOut.Equals("OUT")).ToList();
            }

            this.listView2.Items.Clear();
            foreach (var item in query)
            {
                var li = new ListViewItem(item.ProdCode);
                li.Tag = item.Gid;
                li.SubItems.Add(item.ProdName);
                li.SubItems.Add(item.StaffName);
                li.SubItems.Add(item.InOut == "IN" ? "入库" : "出库");
                li.SubItems.Add(Util.FormatDateTime(item.Regdate));
                li.SubItems.Add(item.Qty.ToString());
                li.SubItems.Add(item.Remark);

                listView2.Items.Add(li);
            }

            this.label2.Text = "";
        }

        private void radioButtonAll_CheckedChanged(object sender, EventArgs e)
        {


        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButtonOut_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButtonAll_MouseClick(object sender, MouseEventArgs e)
        {
            if (radioButtonAll.Checked)
            {
                radioButtonIN.Checked = false;
                radioButtonOut.Checked = false;

                BindList2(this.textBox2Keyword.Text.Trim());
            }
        }

        private void radioButtonIN_MouseClick(object sender, MouseEventArgs e)
        {
            if (radioButtonIN.Checked)
            {
                radioButtonAll.Checked = false;
                radioButtonOut.Checked = false;

                BindList2(this.textBox2Keyword.Text.Trim());
            }
        }

        private void radioButtonOut_MouseClick(object sender, MouseEventArgs e)
        {
            if (radioButtonOut.Checked)
            {
                radioButtonAll.Checked = false;
                radioButtonIN.Checked = false;

                BindList2(this.textBox2Keyword.Text.Trim());
            }
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.listView1.SelectedItems != null &&
                this.listView1.SelectedItems.Count > 0)
            {
                var code = this.listView1.SelectedItems[0].SubItems[0].Text;
                this.textBox2Keyword.Text = code;

                BindList2(code);
            }
        }

        private void 出库ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems != null &&
              this.listView1.SelectedItems.Count > 0)
            {
                Guid gid = Guid.Parse(this.listView1.SelectedItems[0].Tag.ToString());
                frmCompManagerOut frm = new frmCompManagerOut(gid);
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.ShowDialog();


                BindList2(this.textBox2Keyword.Text.Trim());
            }
            else
            {
                MessageBox.Show("请选择需要出库的商品.");
            }
        }

        private void buttonSearch1_Click(object sender, EventArgs e)
        {
            BindList1(this.textBox1Keyword.Text.Trim());
        }

        private void buttonSearch2_Click(object sender, EventArgs e)
        {
            BindList2(this.textBox2Keyword.Text.Trim());
        }

        private void textBox1Keyword_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BindList1(this.textBox1Keyword.Text.Trim());
            }
        }

        private void textBox2Keyword_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BindList2(this.textBox2Keyword.Text.Trim());
            }
        }
    }
}
