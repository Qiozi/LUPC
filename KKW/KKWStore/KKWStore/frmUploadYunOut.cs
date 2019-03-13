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
    public partial class frmUploadYunOut : Form
    {
        qstoreEntities context = new qstoreEntities();

        public class Item
        {
            public string Code { get; set; }

            public int Qty { get; set; }
        }

        public frmUploadYunOut()
        {
            InitializeComponent();
        }

        private void textBox1_DoubleClick(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.textBox1.Text = openFileDialog1.FileName;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ReadExcel();
        }

        void ReadExcel()
        {
            var dt = new Helper.ExcelHelper(this.textBox1.Text).ExcelToDataTable();

            var list = new List<Item>();
            var list2 = new List<Item>();

            for (var i = 0; i < dt.Rows.Count; i++)
            {
                var dr = dt.Rows[i];
                int qty = 0;
                int.TryParse(dr["数量"].ToString(), out qty);
                list.Add(new Item()
                {
                    Code = dr["商品编码"].ToString(),
                    Qty = qty
                });
            }

            this.listView1.Items.Clear();
            var index = 0;
            foreach (var item in list.Select(me => me.Code).Distinct())
            {
                index++;
                var li = new ListViewItem(index.ToString());
                li.SubItems.Add(item.ToString());
                li.SubItems.Add(list.Where(me => me.Code.Equals(item)).Sum(me => me.Qty).ToString());
                li.SubItems.Add("");
                li.SubItems.Add("");
                li.SubItems.Add("");
                this.listView1.Items.Add(li);
            }
        }

        private void buttonMatch_Click(object sender, EventArgs e)
        {



            for (var i = 0; i < this.listView1.Items.Count; i++)
            {
                var code = this.listView1.Items[i].SubItems[1].Text.Trim();
                var qty = 0;
                int.TryParse(this.listView1.Items[i].SubItems[2].Text.Trim(), out qty);

                if (string.IsNullOrEmpty(code)) continue;
                var qtyStock = db.SerialNoAndPCodeModel_p.GetValidQuantityByYunCode(code, Helper.Config.YunWarehouseId).Rows.Count;
                listView1.Items[i].SubItems[3].Text = qtyStock.ToString();

                var outQty = db.SerialNoAndPCodeModel_p.GetMonthOutQuantityByYunCode(code, Helper.Config.YunWarehouseId, this.dateTimePicker1.Value).Rows.Count;
                listView1.Items[i].SubItems[4].Text = outQty.ToString();

                var needOutQty = qty - outQty;
                listView1.Items[i].SubItems[5].Text = needOutQty.ToString();

                // if (i > 30) return;  
                //if (i > 50) return;
            }
        }

        private void buttonDone_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure!?") == DialogResult.OK)
            {
                for (var i = 0; i < this.listView1.Items.Count; i++)
                {
                    var yunCode = this.listView1.Items[i].SubItems[1].Text.Trim();
                    var needOutQty = 0;
                    int.TryParse(listView1.Items[i].SubItems[5].Text, out needOutQty);

                    var prod = context.tb_product.SingleOrDefault(me => me.yun_code.Equals(yunCode));
                    if (prod == null) continue;


                    #region 补库存

                    var inQty = int.Parse(listView1.Items[i].SubItems[2].Text) - int.Parse(listView1.Items[i].SubItems[3].Text);
                    if (inQty > 0)
                    {
                        new frmYunAsync().InStore(context, inQty, new frmYunAsync.MatchItem
                        {
                            LocalItem = new frmYunAsync.Item
                            {
                                Code = prod.p_code,
                                Qty = int.Parse(listView1.Items[i].SubItems[3].Text)
                            },
                            YunCode = yunCode,
                            YunQty = 0
                        }, this.dateTimePicker1.Value);
                    }
                    #endregion



                    var stockQty = 0;
                    int.TryParse(this.listView1.Items[i].SubItems[3].Text.Trim(), out stockQty);

                    if (needOutQty > 0)
                    {

                        new frmYunAsync().OutStore(context, 0-needOutQty, new frmYunAsync.MatchItem
                        {
                            LocalItem = new frmYunAsync.Item
                            {
                                Code = prod.p_code,
                                Qty = stockQty
                            },
                            YunCode = yunCode,
                            YunQty = 0
                        }, this.dateTimePicker1.Value);
                    }
                    if (needOutQty < 0)
                    {
                        var snDt = db.SerialNoAndPCodeModel_p.GetMonthOutQuantityByYunCode(yunCode, Helper.Config.YunWarehouseId, this.dateTimePicker1.Value);
                        var outIndex = 0;
                        foreach (DataRow dr in snDt.Rows)
                        {                          
                            if (outIndex == (0 - needOutQty))
                                continue;
                            outIndex++;
                            var qid = int.Parse(dr["id"].ToString());
                            var query = context.tb_serial_no_and_p_code.SingleOrDefault(me => me.id.Equals(qid));
                            if (query != null)
                            {
                                context.tb_serial_no_and_p_code.Remove(query);
                                context.SaveChanges();
                            }                          
                        }
                    }

                   // if (i > 50) return;
                }
            }
            MessageBox.Show("OOK");
        }
    }
}
