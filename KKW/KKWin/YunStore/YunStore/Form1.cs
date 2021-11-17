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
            var queryMain = _context.tb_yun_fileinfo_company_stock_main
                .OrderByDescending(me => me.Regdate)
                .FirstOrDefault();
            if (queryMain == null)
            {
                MessageBox.Show("无数据.");
                return;
            }

            var query1 = _context.tb_yun_fileinfo_company_stock_child.Where(me => me.ParentId.Equals(queryMain.Gid)).ToList();

            homeInfo.Add(new Model.Stat.HomeStat
            {
                WarehouseName = "公司",
                Qty = query1 == null ? 0 : query1.Count,
                Total1 = query1 == null ? 0 : query1.Sum(me => (decimal?)me.Qty).GetValueOrDefault(),
                Total2 = query1 == null ? 0 : query1.Sum(me => (decimal?)me.Qty * me.Cost).GetValueOrDefault(),
                LastUpdateTime = query1.Max(me => me.Regdate).ToString(),

            });


            var query2 = _context.tb_yun_fileinfo_stock_main.OrderByDescending(me => me.Regdate).FirstOrDefault();
            homeInfo.Add(new Model.Stat.HomeStat
            {
                WarehouseName = "秒仓",
                Qty = query2 == null ? 0 : (query2.AllProdQty),
                Total1 = query2 == null ? 0 : query2.AllProdStock,
                Total2 = query2 == null ? 0 : query2.AllProdTotal,
                LastUpdateTime = query2.Regdate,
            });

            homeInfo.Add(new Model.Stat.HomeStat
            {
                WarehouseName = "所有",
                Qty = homeInfo[0].Qty + homeInfo[1].Qty,
                Total1 = homeInfo[0].Total1 + homeInfo[1].Total1,
                Total2 = homeInfo[0].Total2 + homeInfo[1].Total2,
                LastUpdateTime = ""
            });

            this.listView1.Items.Clear();
            foreach (var item in homeInfo)
            {
                ListViewItem li = new ListViewItem(item.WarehouseName);
                li.SubItems.Add(item.Qty.ToString());
                li.SubItems.Add(item.Total1.ToString());
                li.SubItems.Add(item.Total2.ToString("##,###,###,##0.00"));
                li.SubItems.Add(item.LastUpdateTime);
                this.listView1.Items.Add(li);
            }

            //
            // 秒仓商品分类统计
            #region 秒仓商品分类统计
            var query3 = _context.view_get_miao_chang_total_by_brand.Where(me => me.ParentID == query2.Gid).ToList();
            this.listView2.Items.Clear();
            foreach(var item in query3)
            {
                ListViewItem li = new ListViewItem(item.brand ?? "--");
                li.SubItems.Add((item.Stock.ToString("0")));
                li.SubItems.Add(item.Total.ToString("##,###,###,##0.00"));
                li.SubItems.Add(query2.Regdate);
                this.listView2.Items.Add(li);
            }
            #endregion
        }

        private void buttonUpStock_Click(object sender, EventArgs e)
        {
            var f = Application.OpenForms["frmStockList"];
            if (f == null)
            {
                frmStockList frm = new frmStockList();
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.Show();

            }
            else
            {
                f.Focus();
            }
        }


        private void buttonUpSaleInfo_Click(object sender, EventArgs e)
        {
            var f = Application.OpenForms["frmUpSaleInfo"];
            if (f == null)
            {
                frmUpSaleInfo frm = new frmUpSaleInfo();
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.Show();
            }
            else
            {
                f.Focus();
            }
        }


        private void button3_Click(object sender, EventArgs e)
        {
            var f = Application.OpenForms["frmStockHistory"];
            if (f == null)
            {
                frmStockHistory frm = new frmStockHistory();
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.Show();
            }
            else
            {
                f.Focus();
            }
        }


        private void button4_Click(object sender, EventArgs e)
        {
            var f = Application.OpenForms["frmUpSaleInfoHistory"];
            if (f == null)
            {
                frmUpSaleInfoHistory frm = new frmUpSaleInfoHistory();
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.Show();
            }
            else
            {
                f.Focus();
            }
        }

        private void buttonProfirt_Click(object sender, EventArgs e)
        {
            var f = Application.OpenForms["frmProfirtManager"];
            if (f == null)
            {
                frmProfirtManager frm = new frmProfirtManager();
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.Show();
            }
            else
            {
                f.Focus();
            }
        }

        private void buttonReadProfit_Click(object sender, EventArgs e)
        {
            var f = Application.OpenForms["frmProfitDetail"];
            if (f == null)
            {
                frmProfitDetail frm = new frmProfitDetail();
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.Show();
            }
            else
            {
                f.Focus();
            }
        }

        private void buttonCompWarehouse_Click(object sender, EventArgs e)
        {
            var f = Application.OpenForms["frmCompManager"];
            if (f == null)
            {
                frmCompManager frm = new frmCompManager();
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.Show();
            }
            else
            {
                f.Focus();
            }
        }

        private void buttonProfitStat_Click(object sender, EventArgs e)
        {
            var f = Application.OpenForms["frmProfitStat"];
            if (f == null)
            {
                frmProfitStat frm = new frmProfitStat();
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.Show();
            }
            else
            {
                f.Focus();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var f = Application.OpenForms["frmUpCompInfo"];
            if (f == null)
            {
                frmUpCompInfo frm = new frmUpCompInfo();
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.Show();
            }
            else
            {
                f.Focus();
            }
        }
    }
}
