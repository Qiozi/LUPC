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
    public partial class frmOutRecord : Form
    {
        db.qstoreEntities context = new db.qstoreEntities();

        public frmOutRecord()
        {
            InitializeComponent();
        }

        private void buttonQuery_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            BindList();
            this.Cursor = Cursors.Default;

        }

        void BindList()
        {
            var wareIds = new List<int>();
            if (checkBox1.Checked)
                wareIds.Add(1);
            if (checkBox9.Checked)
                wareIds.Add(9);

            var date = this.dateTimePicker1.Value;
            var startDate = DateTime.Parse(date.ToString("yyyy-MM-01"));
            var endDate = startDate.AddMonths(1).AddMinutes(-1);
            var serialNOs = context
                                .tb_serial_no_and_p_code
                                .Where(me => me.out_regdate.HasValue &&
                                           me.out_regdate.Value >= startDate &&
                                           me.out_regdate.Value <= endDate &&
                                           wareIds.Contains(me.curr_warehouse_id.Value))
                                .OrderBy(me => me.out_regdate)
                                .ToList();


            var prodList = (from c in context.tb_product
                            select new
                            {
                                c.id,
                                c.p_name,
                                c.WholesalerCode
                            }).ToList();

            this.listView1.Items.Clear();
            var i = 0;
            foreach (var item in serialNOs)
            {
                i++;
                var prod = prodList.SingleOrDefault(p => p.id.Equals(item.p_id.Value));

                var li = new ListViewItem(i.ToString());
                li.Tag = item.id;
                li.SubItems.Add(item.curr_warehouse_id == 1 ? "公司" : (item.curr_warehouse_id == 9 ? "云仓" : "--"));
                li.SubItems.Add(item.out_regdate.Value.ToString("yyyy-MM-dd"));
                li.SubItems.Add(item.p_code.ToString());
                li.SubItems.Add(prod == null ? "" : prod.WholesalerCode);
                li.SubItems.Add(item.in_cost.ToString());

                li.SubItems.Add(prod == null ? "" : prod.p_name.ToString());

                this.listView1.Items.Add(li);
            }
            this.labelNote.Text = string.Format("{0} 一共出库了{1}个商品， 总成本{2}元;",
                date.ToString("yyyy年MM月"),
                serialNOs.Count.ToString(),
                serialNOs.Sum(me => me.in_cost).ToString());
        }
    }
}
