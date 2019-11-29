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
using YunStore.Model.Enum;

namespace YunStore
{
    public partial class Form1 : Form
    {
        DB.qstoreEntities _context = new DB.qstoreEntities();
        bool _isClose = true;

        public Form1()
        {
            Init();

            InitializeComponent();
            if (!_isClose)
            {
                this.Shown += Form1_Shown;
            }
            else
            {
                this.Close();
            }
        }

        void Init()
        {
            if (Guid.Empty == BLL.Config.StaffGid ||
                null == BLL.Config.StaffGid)
            {
                frmLogin fip = new frmLogin();
                fip.StartPosition = FormStartPosition.CenterParent;
                _isClose = fip.ShowDialog() != DialogResult.Yes;
            }

            var dbPath = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\') + "\\db";
            if (!Directory.Exists(dbPath))
            {
                Directory.CreateDirectory(dbPath);
            }

            BLL.Config.DBFullname = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "db\\kkw.db");
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            BindInfo();
        }

        void BindInfo()
        {
            List<Model.Stat.HomeStat> homeInfo = new List<Model.Stat.HomeStat>();


            var query1 = _context.tb_yun_fileinfo_company_stock.ToList();

            homeInfo.Add(new Model.Stat.HomeStat
            {
                WarehouseName = "公司",
                Qty = query1 == null ? 0 : query1.Count,
                Total1 = query1 == null ? 0 : query1.Sum(me => (decimal?)me.Qty).GetValueOrDefault(),
                Total2 = query1 == null ? 0 : query1.Sum(me => (decimal?)me.Qty * me.Cost).GetValueOrDefault()
            });


            var query2 = _context.tb_yun_fileinfo_stock_main.OrderByDescending(me => me.Regdate).FirstOrDefault();
            homeInfo.Add(new Model.Stat.HomeStat
            {
                WarehouseName = "云仓",
                Qty = query2 == null ? 0 : (query2.AllProdQty),
                Total1 = query2 == null ? 0 : query2.AllProdStock,
                Total2 = query2 == null ? 0 : query2.AllProdTotal
            });

            homeInfo.Add(new Model.Stat.HomeStat
            {
                WarehouseName = "所有",
                Qty = homeInfo[0].Qty + homeInfo[1].Qty,
                Total1 = homeInfo[0].Total1 + homeInfo[1].Total1,
                Total2 = homeInfo[0].Total2 + homeInfo[1].Total2
            });

            this.listView1.Items.Clear();
            foreach (var item in homeInfo)
            {
                ListViewItem li = new ListViewItem(item.WarehouseName);
                li.SubItems.Add(item.Qty.ToString());
                li.SubItems.Add(item.Total1.ToString());
                li.SubItems.Add(item.Total2.ToString("##,###,###,##0.00"));
                this.listView1.Items.Add(li);
            }
        }

        private void buttonUpStock_Click(object sender, EventArgs e)
        {
            frmStockList frm = new frmStockList();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.Show();

        }

        private void buttonUpSaleInfo_Click(object sender, EventArgs e)
        {
            frmUpSaleInfo frm = new frmUpSaleInfo();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            frmStockHistory frm = new frmStockHistory();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            frmUpSaleInfoHistory frm = new frmUpSaleInfoHistory();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.Show();
        }

        private void buttonProfirt_Click(object sender, EventArgs e)
        {
            frmProfirtManager frm = new frmProfirtManager();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.Show();
        }

        private void buttonReadProfit_Click(object sender, EventArgs e)
        {
            frmProfitDetail frm = new frmProfitDetail();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.Show();
        }

        private void buttonCompWarehouse_Click(object sender, EventArgs e)
        {
            frmCompManager frm = new frmCompManager();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.Show();
        }
    }
}
