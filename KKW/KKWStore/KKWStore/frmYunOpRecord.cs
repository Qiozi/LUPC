using KKWStore.db;
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
    public partial class frmYunOpRecord : Form
    {
        qstoreEntities context = new db.qstoreEntities();

        int _id = 0;

        public frmYunOpRecord(int id)
        {
            _id = id;
            InitializeComponent();
            this.Shown += FrmYunOpRecord_Shown;
        }

        private void FrmYunOpRecord_Shown(object sender, EventArgs e)
        {
            var query = context.tb_yun_stock_async_code
                .Where(me => me.yun_stock_async_id.Equals(_id))
                .ToList();
            var prodTitle = string.Empty;
            if (query.Count > 0)
            {
                var pid = query[0].p_id;
                var pm = context.tb_product.Single(p => p.id.Equals(pid));

                prodTitle = pm.p_name;
            }
            this.Text += ": " + prodTitle;
            foreach (var item in query)
            {
                var li = new ListViewItem(item.regdate.ToString("yyyy-MM-dd HH:mm"));
                li.SubItems.Add(prodTitle);
                li.SubItems.Add(item.p_code);
                li.SubItems.Add(item.serial_no);
                li.SubItems.Add(item.cmd == "in" ? "入库" : "出库");
                listViewHistory2.Items.Add(li);
            }
        }
    }
}
