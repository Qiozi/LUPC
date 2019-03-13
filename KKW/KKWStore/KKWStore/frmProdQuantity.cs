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

    public partial class frmProdQuantity : Form
    {

        public class ProductQty
        {
            public string p_code { get; set; }

            public string wholesalercode { get; set; }

            public string p_name { get; set; }

            public int q_quantity { get; set; }

            public int w1 { get; set; }

            public int w9 { get; set; }
        }
        db.qstoreEntities context = new db.qstoreEntities();
        public frmProdQuantity()
        {
            InitializeComponent();
            this.Shown += FrmProdQuantity_Shown;
        }

        private void FrmProdQuantity_Shown(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            BindList();
            this.Cursor = Cursors.Default;

        }

        void BindList()
        {
            var keyword = this.textBoxKeyword.Text.Trim();
            var query = context.Database.SqlQuery<ProductQty>(@"
select ifnull(p_code, '') p_code, 
       ifnull(wholesalercode,'') wholesalercode, 
       ifnull(p_name, '') p_name, 
       ifnull(p_quantity,0) p_quantity, 
       (select count(id) from tb_serial_no_and_p_code where p_id=p.id and out_regdate like '00%' and curr_warehouse_id=1) w1, 
       (select count(id) from tb_serial_no_and_p_code where p_id=p.id and out_regdate like '00%' and curr_warehouse_id=9) w9 
from tb_product p where p_quantity>0 and (p_name like '%"+ keyword + "%' or p_code like '%"+ keyword + "%')").ToList();
            this.listView1.Items.Clear();
            var index = 0;
            foreach (var item in query)
            {
                index++;
                var li = new ListViewItem(index.ToString());
                li.SubItems.Add(item.p_code);
                li.SubItems.Add(item.wholesalercode);
                li.SubItems.Add(item.p_name);
                li.SubItems.Add((item.w1+item.w9).ToString());
                li.SubItems.Add(item.w1.ToString());
                li.SubItems.Add(item.w9.ToString());
                this.listView1.Items.Add(li);
            }
            this.label1.Text = "列表产品数量：" + query.Count.ToString();
        }

        private void buttonQuery_Click(object sender, EventArgs e)
        {
            BindList();
        }

        private void buttonExport_Click(object sender, EventArgs e)
        {
            KKWStore.Helper.ExportExcel.Export(listView1, "产品库存列表");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            context.Database.ExecuteSqlCommand(@"update tb_product , tb_serial_no_and_p_code
       set tb_product.p_quantity =
           (select count(id) from tb_serial_no_and_p_code where p_id = tb_product.id and out_regdate like '00%' and IsReturnWholesaler = 0); ");
            this.Cursor = Cursors.Default;
            MessageBox.Show("OK");
        }

        private void textBoxKeyword_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                BindList();
            }
        }
    }
}
