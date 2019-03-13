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
    public partial class frmYunFileRecord : Form
    {
        public string _filename = string.Empty;
        public int _id = 0;
        public frmYunFileRecord(string filename, int id)
        {
            _id = id;
            _filename = filename;
            InitializeComponent();
            this.Shown += FrmYunFileRecord_Shown;
            this.Text += ": " + filename;
        }

        private void FrmYunFileRecord_Shown(object sender, EventArgs e)
        {
            var context = new db.qstoreEntities();
            var query = context.tb_yun_stock_child.Where(me => me.ParentId.Equals(_id)).ToList();
            foreach(var item in query)
            {
                var li = new ListViewItem(item.ProdCode);
                li.SubItems.Add(item.ProdName);
                li.SubItems.Add(item.ProdSN);
                li.SubItems.Add(item.ProdStandard);
                li.SubItems.Add(item.ProdQtyTotal.ToString());
                li.SubItems.Add(item.ProdQtyUsedOrder.ToString());
                li.SubItems.Add(item.ProdQtyAvailability.ToString());
                listViewHistory2.Items.Add(li);
            }
        }
    }
}
