using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YunStore
{
    public partial class frmStockDetail : Form
    {
        DB.qstoreEntities _context = new DB.qstoreEntities();
        Guid _gid = Guid.Empty;

        public frmStockDetail(Guid gid)
        {
            _gid = gid;

            InitializeComponent();
            this.Shown += FrmUpSaleInfoDetail_Shown;
        }
        private void FrmUpSaleInfoDetail_Shown(object sender, EventArgs e)
        {
            BindList();
        }

        void BindList(string keyword = "")
        {
            var queryMain = _context
                .tb_yun_fileinfo_stock_main
                .Single(me => me.Gid.Equals(_gid));
            this.Text = string.Format("{0} 在 {1} 上传的库存数据",

                queryMain.StaffName,
                Toolkits.Util.FormatDateTime(queryMain.Regdate));

            var query = _context
                .tb_yun_fileinfo_stock_child
                .Where(me =>
                    me.ParentId.Equals(_gid) && (
                    me.ProdName.Contains(keyword) ||
                    me.ProdCode.Contains(keyword)))
                    .OrderBy(me => me.ProdCode)
                    .ToList();

            this.listView1.Items.Clear();
            foreach (var item in query)
            {
                var li = new ListViewItem(item.ProdCode);
                li.Tag = item.Gid;
                li.SubItems.Add(item.SpecCode);
                li.SubItems.Add(item.ProdName);
                li.SubItems.Add(item.ProdSpec);
                li.SubItems.Add(item.ProdSN);
                li.SubItems.Add(item.QtyAll.ToString());
                li.SubItems.Add(item.QtyStock.ToString());
                li.SubItems.Add(item.QtyOn.ToString());
                li.SubItems.Add(item.QtyUsed.ToString());
                li.SubItems.Add(item.Total.ToString());
                li.SubItems.Add(item.Qty30DaySale.ToString());
                li.SubItems.Add(item.QtyWarn.ToString());
                if (item.QtyStock <= 0M)
                {
                    li.BackColor = Color.LightPink;
                }
                this.listView1.Items.Add(li);
            }

            var allProdSaleCost = query.Sum(me => (decimal?)me.Total).GetValueOrDefault();
            var allProdSaleQty = query.Sum(me => (int?)me.QtyStock).GetValueOrDefault();
            var allProdQty = query.Count;

            this.label1.Text = string.Format(@"商品数量：{0}   总库存数量：{1}   总库存货值：{2}",
                allProdQty,
                allProdSaleQty,
                allProdSaleCost);
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            var keyword = this.textBox1.Text.Trim();
            BindList(keyword);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                buttonSearch_Click(null, null);
            }
        }

        private void buttonExport_Click(object sender, EventArgs e)
        {
            if (listView1.Items != null && listView1.Items.Count > 0)
            {

                DataTable dt = new DataTable();
                for (int i = 0; i < listView1.Columns.Count; i++)
                {
                    dt.Columns.Add(listView1.Columns[i].Text);
                }

                foreach (ListViewItem li in listView1.Items)
                {
                    DataRow dr = dt.NewRow();
                    for (int i = 0; i < listView1.Columns.Count; i++)
                    {
                        dr[i] = li.SubItems[i].Text;
                    }
                    dt.Rows.Add(dr);

                }
                var sheetName = this.Text.Replace(" ", "_").Replace(":", "_");
                var fileName = Environment.GetFolderPath(Environment.SpecialFolder.Desktop).ToString() + "\\" + sheetName + ".xls";
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }
                Toolkits.ExcelHelper.ToExcel(dt, sheetName, fileName);

                MessageBox.Show("数据已生成在桌面", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("没有需要导出的数据", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private void 添加此商品到公司库存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems != null && listView1.SelectedItems.Count > 0)
            {
                var gid = Guid.Parse(this.listView1.SelectedItems[0].Tag.ToString());

                frmStockCopyToCompany frm = new frmStockCopyToCompany(gid);
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.ShowDialog();               
            }

        }
    }
}
