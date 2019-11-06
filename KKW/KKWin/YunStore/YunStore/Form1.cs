using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YunStore.Model.Enum;

namespace YunStore
{
    public partial class Form1 : Form
    {
        DB.kkwEntities _context = new DB.kkwEntities();

        public Form1()
        {
            InitializeComponent();
            this.Shown += Form1_Shown;
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            BindInfo();
        }

        void BindInfo()
        {
            List<Model.Stat.HomeStat> homeInfo = new List<Model.Stat.HomeStat>();

            {
                var qty = _context
                         .tb_product_serial
                         .Count(me =>
                             me.StockType.Equals((int)StockType.On) &&
                             me.WarehouseId.Equals((int)WareHouseType.Comp));

                var total1 = _context
                        .tb_product_serial
                        .Where(me =>
                            me.StockType.Equals((int)StockType.On) &&
                            me.WarehouseId.Equals((int)WareHouseType.Comp))
                        .Sum(me => (decimal?)me.Cost).GetValueOrDefault();

                var total2 = (from c1 in _context.tb_product_serial
                              join c2 in _context.tb_product on c1.ProdCode equals c2.ProdCode
                              select new
                              {
                                  price = c2.Cost
                              }).Sum(me => (decimal?)me.price).GetValueOrDefault();

                homeInfo.Add(new Model.Stat.HomeStat
                {
                    WarehouseName = "公司",
                    Qty = qty,
                    Total1 = total1,
                    Total2 = total2
                });
            }
            homeInfo.Add(new Model.Stat.HomeStat
            {
                WarehouseName = "云仓",
                Qty = 0,
                Total1 = 0,
                Total2 = 0
            });
            homeInfo.Add(new Model.Stat.HomeStat
            {
                WarehouseName = "所有",
                Qty = 0,
                Total1 = 0,
                Total2 = 0
            });

            this.listView1.Items.Clear();
            foreach (var item in homeInfo)
            {
                ListViewItem li = new ListViewItem(item.WarehouseName);
                li.SubItems.Add(item.Qty.ToString());
                li.SubItems.Add(item.Total1.ToString("##,###,###,##0.00"));
                li.SubItems.Add(item.Total2.ToString("##,###,###,##0.00"));
                this.listView1.Items.Add(li);
            }
        }

        private void buttonProdList_Click(object sender, EventArgs e)
        {

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
    }
}
