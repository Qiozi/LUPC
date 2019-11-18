using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace YunStore
{
    public partial class frmUpSaleInfoHistory : Form
    {
        DB.qstoreEntities _context = new DB.qstoreEntities();

        public frmUpSaleInfoHistory()
        {
            InitializeComponent();
            this.Shown += FrmUpSaleInfoHistory_Shown;
        }

        private void FrmUpSaleInfoHistory_Shown(object sender, EventArgs e)
        {
            BindList();
        }

        void BindList()
        {
            var query = _context
                .tb_yun_fileinfo_sale_main
                .OrderByDescending(me => me.Regdate)
                .ToList();

            this.label1.Text = string.Format(@"一共上传 {0} 次", query.Count.ToString());

            this.listView1.Items.Clear();
            for (var i = 0; i < query.Count; i++)
            {
                var item = query[i];
                var li = new ListViewItem(item.SaleMonth);
                li.Tag = item.Gid;
                li.SubItems.Add(Toolkits.Util.FormatDateTime(item.Regdate));
                li.SubItems.Add(item.StaffName);
                li.SubItems.Add(item.AllProdQty.ToString());
                li.SubItems.Add(item.AllProdSaleQty.ToString());
                li.SubItems.Add(item.AllProdSaleCost.ToString());
                li.SubItems.Add(item.FileName);
                li.SubItems.Add(item.FileMD5);
                this.listView1.Items.Add(li);
            }
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems != null)
            {
                if (this.listView1.SelectedItems.Count == 1)
                {
                    try
                    {
                        var gid = this.listView1.SelectedItems[0].Tag.ToString();
                        if (!string.IsNullOrEmpty(gid))
                        {
                            var frm = new frmUpSaleInfoDetail(Guid.Parse(gid));
                            frm.StartPosition = FormStartPosition.CenterScreen;
                            frm.Show();
                        }
                    }
                    catch
                    {

                    }
                }
            }
        }
    }
}
