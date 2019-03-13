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

    public partial class frmOrdersManage : Form
    {
        public class OrderList
        {
            public string OrderType { get; set; }

            public int Qty { get; set; }

            public decimal Price { get; set; }

            public decimal ReturnFee { get; set; }

            public decimal Total { get; set; }
        }

        db.qstoreEntities context = new db.qstoreEntities();
        List<db.tb_order> QueryResult = new List<db.tb_order>();
        bool IsMonth = false;

        public frmOrdersManage()
        {
            InitializeComponent();
            this.Shown += FrmOrdersManage_Shown;
        }

        private void FrmOrdersManage_Shown(object sender, EventArgs e)
        {

        }

        void BindDetail(string moneyStatus)
        {
            if (moneyStatus == "未交易")
            {
                moneyStatus = "";
            }
            var query = QueryResult.Where(me => me.MoneyStatus.Equals(moneyStatus)).ToList();
            this.listView2.Items.Clear();
            var index = 0;
            foreach (var item in query)
            {
                index++;
                var li = new ListViewItem(index.ToString());
                li.SubItems.Add(item.TraceNo);
                li.SubItems.Add(item.BrandNo);
                li.SubItems.Add(item.CreateTime.ToString());
                li.SubItems.Add(item.PayTime.ToString());
                li.SubItems.Add(item.ModifyTime.ToString());
                li.SubItems.Add(item.Source);
                li.SubItems.Add(item.OrderType);
                li.SubItems.Add(item.CustomerName);
                li.SubItems.Add(item.ProdName);
                li.SubItems.Add(Util.Format.Price.FormatString(item.Total));
                li.SubItems.Add(item.InOut);
                li.SubItems.Add(item.OrderStatus);
                li.SubItems.Add(item.ServiceFee.HasValue ? Util.Format.Price.FormatString(item.ServiceFee.Value) : "0.00");
                li.SubItems.Add(item.ReturnFee.HasValue ? Util.Format.Price.FormatString(item.ReturnFee.Value) : "0.00");
                li.SubItems.Add(item.Note);
                li.SubItems.Add(item.MoneyStatus);
                listView2.Items.Add(li);
            }
            if(moneyStatus == "已收入")
            {
                labelViewNote2.Text = string.Format("商品收入： {0};  非商品收入： {1};",
                                                    Util.Format.Price.FormatString(query
                                                        .Where(me => me.BrandNo.StartsWith("T"))
                                                        .Sum(me => (me.Total - me.ReturnFee))
                                                        .GetValueOrDefault()),
                                                    Util.Format.Price.FormatString(query
                                                        .Where(me => !me.BrandNo.StartsWith("T"))
                                                        .Sum(me => (me.Total - me.ReturnFee))
                                                        .GetValueOrDefault()));
            }
            else
            {
                labelViewNote2.Text = string.Empty;
            }
        }

        void BindStatList()
        {
            var beginTime = DateTime.Parse(dateTimePicker1.Value.ToString("yyy-MM-dd"));
            var endTime = DateTime.Parse(dateTimePicker2.Value.AddDays(1).ToString("yyyy-MM-dd"));
            if (IsMonth)
            {
                beginTime = DateTime.Parse(dateTimePicker1.Value.ToString("yyy-MM-01"));
                endTime = beginTime.AddMonths(1);
            }
            this.labelDateSpan.Text = string.Concat("查询日期范围： ",
                                                    beginTime.ToString("yyyy-MM-dd HH:mm:ss"),
                                                    " 到 ",
                                                    endTime.ToString("yyyy-MM-dd HH:mm:ss"));
            QueryResult = context
                            .tb_order
                            .Where(me =>
                                   me.CreateTime >= beginTime &&
                                   me.CreateTime < endTime)
                            .ToList();

            var moneyType = QueryResult.Select(me => me.MoneyStatus).Distinct().ToList();
            var orderlist = new List<OrderList>();
            foreach (var status in moneyType)
            {
                var subList = QueryResult.Where(me => me.MoneyStatus.Equals(status)).ToList();
                orderlist.Add(new OrderList
                {
                    OrderType = status,
                    Price = subList.Sum(me => (decimal?)me.Total).GetValueOrDefault(),
                    Qty = subList.Count(),
                    ReturnFee = subList.Sum(me => me.ReturnFee).GetValueOrDefault(),
                    Total = subList.Sum(me => (me.Total - me.ReturnFee)).GetValueOrDefault()
                });
            }
            this.listView1.Items.Clear();
            foreach (var item in orderlist)
            {
                var li = new ListViewItem(string.IsNullOrEmpty(item.OrderType.Trim()) ? "未交易" : item.OrderType);
                li.SubItems.Add(item.Qty.ToString());
                li.SubItems.Add(Util.Format.Price.FormatString(item.Price));
                li.SubItems.Add(Util.Format.Price.FormatString(item.ReturnFee));
                li.SubItems.Add(Util.Format.Price.FormatString(item.Total));
                listView1.Items.Add(li);
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            IsMonth = true;
            BindStatList();
            this.Cursor = Cursors.Default;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            IsMonth = false;
            BindStatList();
            this.Cursor = Cursors.Default;
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            frmUpdateOrder f = new frmUpdateOrder();
            f.StartPosition = FormStartPosition.CenterParent;
            f.ShowDialog();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems != null && this.listView1.SelectedItems.Count > 0)
            {
                this.listView2.Items.Clear();
                var txt = this.listView1.SelectedItems[0].SubItems[0].Text;
                if (!string.IsNullOrEmpty(txt))
                {
                    BindDetail(txt);
                }
            }
        }
    }
}
