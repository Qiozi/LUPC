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

    public partial class frmStoreList : Form
    {
        class StoreItem
        {
            public int Id { get; set; }

            public string Title { get; set; }

            public decimal Total { get; set; }

            public decimal Qty { get; set; }

        }

        db.qstoreEntities context = new db.qstoreEntities();

        public frmStoreList()
        {
            InitializeComponent();
            this.Shown += FrmStoreList_Shown;
        }

        private void FrmStoreList_Shown(object sender, EventArgs e)
        {
            BindList();
        }

        void BindList()
        {
            var list = new List<StoreItem>();

            list = (from me in context.tb_warehouse
                    select new StoreItem
                    {
                        Id = me.ID,
                        Title = me.WarehouseName,
                        Total = 0M,Qty = 0
                    }).ToList();

            for (int i = 0; i < list.Count; i++)
            {
                int qty = 0;
                list[i].Total = db.SerialNoAndPCodeModel_p.GetStoreTotal(context, list[i].Id,ref qty);
                list[i].Qty = qty;
            }

            this.listView1.Items.Clear();
            foreach (var item in list)
            {
                var listViewItem = new ListViewItem(item.Title);
                listViewItem.SubItems.Add(item.Total.ToString());
                listViewItem.SubItems.Add(item.Qty.ToString());
                this.listView1.Items.Add(listViewItem);
            }
        }
    }
}
