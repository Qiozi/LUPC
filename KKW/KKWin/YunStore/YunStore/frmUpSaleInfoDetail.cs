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
    public partial class frmUpSaleInfoDetail : Form
    {
        DB.qstoreEntities _context = new DB.qstoreEntities();
        Guid _gid = Guid.Empty;

        public frmUpSaleInfoDetail(Guid gid)
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
                .tb_yun_fileinfo_sale_main
                .Single(me => me.Gid.Equals(_gid));
            this.Text = string.Format("{0} 在{1} 上传的销售({2}售)数据",

                queryMain.StaffName,
                Toolkits.Util.FormatDateTime(queryMain.Regdate),
                queryMain.SaleMonth);

            var query = _context
                .tb_yun_fileinfo_sale_child
                .Where(me =>
                    me.ParentId.Equals(_gid) && (
                    me.ProdName.Contains(keyword) ||
                    me.ProdCode.Contains(keyword)))
                    .OrderBy(me=>me.ProdCode)
                    .ToList();

            this.listView2.Items.Clear();
            foreach (var item in query)
            {
                var li = new ListViewItem(item.ProdCode);
                li.SubItems.Add(item.ProdName);
                li.SubItems.Add(item.ProdShortName);
                li.SubItems.Add(item.ProdSpec);
                li.SubItems.Add(item.Price.ToString());
                li.SubItems.Add(item.Cost.ToString());
                li.SubItems.Add(item.WarehouseName.ToString());
                li.SubItems.Add(item.StoreName.ToString());
                li.SubItems.Add(item.Qty.ToString());
                if (item.Cost <= 0M)
                {
                    li.BackColor = Color.LightPink;
                }
                this.listView2.Items.Add(li);
            }

            var allProdSaleCost = query.Sum(me => (decimal?)me.Cost * me.Qty).GetValueOrDefault();
            var allProdSaleQty = query.Sum(me => (int?)me.Qty).GetValueOrDefault();
            var allProdQty = query.Count;

            this.label1.Text = string.Format(@"商品数量：{0}   出库总数量：{1}   出库总成本：{2}",
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
            if (listView2.Items != null && listView2.Items.Count > 0)
            {

                DataTable dt = new DataTable();
                for (int i = 0; i < listView2.Columns.Count; i++)
                {
                    dt.Columns.Add(listView2.Columns[i].Text);
                }

                foreach (ListViewItem li in listView2.Items)
                {
                    DataRow dr = dt.NewRow();
                    for (int i = 0; i < listView2.Columns.Count; i++)
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
    }
}
