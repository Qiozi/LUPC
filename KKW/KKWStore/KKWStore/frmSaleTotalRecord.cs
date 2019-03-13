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
    public partial class frmSaleTotalRecord : Form
    {
        db.qstoreEntities context = new db.qstoreEntities();
        public frmSaleTotalRecord()
        {
            InitializeComponent();
            this.Shown += FrmSaleTotalRecord_Shown;
        }

        private void FrmSaleTotalRecord_Shown(object sender, EventArgs e)
        {
            BindList();
        }

        void BindList()
        {
            var beginTime = DateTime.Parse("2016-08-01");
            var query = context.tb_sales_total
                               .Where(me => me.SaleDate.HasValue &&
                                            me.SaleDate.Value >= beginTime)
                               .OrderByDescending(me=>me.ID)
                               .ToList();

            var total = 0M;
            foreach (var item in query)
            {
                total += item.SaleTotal.Value;

                var li = new ListViewItem(item.StoreName);
                li.Tag = item.ID;

                li.SubItems.Add(item.SaleDate.Value.ToString("yyyy 年 MM月"));
                li.SubItems.Add(item.SaleTotal.Value.ToString());
                this.listView1.Items.Add(li);
            }
            totalText.Text = total.ToString("￥##,###,###,##0.00");
        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            frmUploadSale fus = new frmUploadSale();
            fus.StartPosition = FormStartPosition.CenterParent;
            fus.ShowDialog();
        }
    }
}
