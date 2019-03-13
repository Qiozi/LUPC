using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using KKWStore.db;
using System.Linq;

namespace KKWStore
{
    public partial class frmProductCheckStore : Form
    {
        qstoreEntities context = new qstoreEntities();
        db.tb_product PM = null;

        public frmProductCheckStore(db.tb_product product)
        {
            PM = context.tb_product.Single(p => p.id.Equals(product.id)); ;
            InitializeComponent();
            this.Shown += new EventHandler(frmProductCheckStore_Shown);
        }

        void frmProductCheckStore_Shown(object sender, EventArgs e)
        {

            //this.comboBox1.DataSource = WarehouseModel.FindAll();
            //this.comboBox1.DisplayMember = "WarehouseName";
            //this.comboBox1.ValueMember = "ID";
            InitWin();
        }

        void InitWin()
        {
            if (PM != null)
            {
                this.textBox_code.Text = PM.p_code;
                this.textBox_name.Text = PM.p_name;
                this.textBox_quantity.Text = PM.p_quantity.ToString();
                this.Text = PM.p_name;
            }

            BindStoreSN();
            BindSellHistory();

            BindInputHistory();
        }
        /// <summary>
        /// 显示当前库存的SN
        /// </summary>
        void BindStoreSN()
        {

            if (PM.p_code == null)
            {
                MessageBox.Show("产品编号错误.", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //WarehouseModel wm = (WarehouseModel)comboBox1.SelectedItem;
            //if (wm == null)
            //    return;
            DataTable dt = db.SerialNoAndPCodeModel_p.GetValidListByProdCode(PM.p_code);
            this.listBox_SN.Items.Clear();
            foreach (DataRow dr in dt.Rows)
            {
                this.listBox_SN.Items.Add(dr["SerialNO"].ToString());
            }
            //if (Helper.Config.IsAdmin)
            {
                this.listBox_SN.Enabled = true;
                this.listBox_sell_history.Enabled = true;
            }
            this.label_Store_Quantity.Text = dt.Rows.Count.ToString();
            this.textBox_quantity.Text = dt.Rows.Count.ToString();

            if (this.label_Store_Quantity.Text != this.textBox_quantity.Text)
            {
                this.label_Store_Quantity.ForeColor = System.Drawing.Color.Red;
            }
        }
        /// <summary>
        /// 显示当前已售的SN
        /// </summary>
        void BindSellHistory()
        {
            if (PM.p_code.Trim().Length == 0)
            {
                // MessageBox.Show("产品编号错误.", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DataTable dt = db.SerialNoAndPCodeModel_p.GetInValidListByProdCode(PM.p_code);
            listBox_sell_history.Items.Clear();
            foreach (DataRow dr in dt.Rows)
            {
                listBox_sell_history.Items.Add(string.Concat(dr["SerialNO"].ToString(), " :: ",dr["out_regdate"].ToString()));
            }
            this.label_sell_quantity.Text = dt.Rows.Count.ToString();
        }
        /// <summary>
        /// 
        /// </summary>
        void BindInputHistory()
        {
            bool havePermantent = Helper.PermantentHelper.Ok(enums.PermanentModel.查看进价);
            if (PM.p_code.Trim().Length == 0)
            {
                // MessageBox.Show("产品编号错误.", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DataTable all = db.InInvoiceProductModel_p.GetALLByPCode(PM.p_code);
            listView1.Items.Clear();
            foreach (DataRow dr in all.Rows)
            {
                ListViewItem li = new ListViewItem(dr["in_invoice_code"].ToString());
                li.Tag = dr["id"].ToString();
                li.SubItems.Add(dr["quantity"].ToString());
                li.SubItems.Add(havePermantent ? dr["cost"].ToString() : "--");
                li.SubItems.Add(havePermantent ? (decimal.Parse(dr["quantity"].ToString()) * decimal.Parse(dr["cost"].ToString())).ToString() : "--");
                li.SubItems.Add(dr["regdate"].ToString());

                listView1.Items.Add(li);
            }
        }

        private void button_save_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("您确认修正此产品的数量?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information)
                == System.Windows.Forms.DialogResult.OK)
            {
                int old_quantity = PM.p_quantity.Value;

                int quantity;
                int.TryParse(this.textBox_quantity.Text, out quantity);

                PM.p_quantity = quantity;


                var pqchm = new tb_product_quantity_change_history();
                pqchm.p_code = PM.p_code;
                pqchm.new_quantity = quantity;
                pqchm.old_quantity = old_quantity;
                pqchm.regdate = DateTime.Now;
                context.tb_product_quantity_change_history.Add(pqchm);
                context.SaveChanges();
                this.DialogResult = System.Windows.Forms.DialogResult.Yes;
                this.Close();
            }
        }

        private void textBox_quantity_TextChanged(object sender, EventArgs e)
        {
            string text = this.textBox_quantity.Text.Trim();
            this.textBox_quantity.Text = Helper.CharacterHelper.ToDBC(text);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listBox_SN.SelectedItem != null)
            {
                for (int i = 0; i < listBox_SN.SelectedItems.Count; i++)
                {
                    string sn = listBox_SN.SelectedItems[i].ToString();
                    if (MessageBox.Show(string.Format("你是否需要出库\r\n条码：{0} \r\n", sn), "询问", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.OK)
                    {
                        if (db.SerialNoAndPCodeModel_p.AutoOut2(context, sn))
                        {
                            MessageBox.Show("出库成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                            MessageBox.Show("失败，也许条形码不存在", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                frmProductCheckStore_Shown(null, null);
            }
            else
            {
                MessageBox.Show("请选中需要删除的纪录");
            }
        }

        private void 添加ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 删除ToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void 添加ToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            btn.Enabled = false;
            string[] sns = textBoxAdd.Text.Split(new char[] { '\n' });
            for (int i = 0; i < sns.Length; i++)
            {
                string sn = sns[i].Trim();
                if (sn.Trim().Length == 10)
                {
                    if (SerialNoAndPCodeModel_p.ExistSN(context, sn))
                        continue;

                    var snpm = new tb_serial_no_and_p_code();
                    snpm.p_code = PM.p_code;
                    snpm.SerialNO = sn;
                    snpm.in_regdate = DateTime.Now;
                    snpm.in_cost = PM.p_cost;
                    snpm.p_id = PM.id;
                    snpm.in_invoice_code = "000000000000"; // 无发票
                    snpm.curr_warehouse_id = 1; // 公司仓库
                    snpm.curr_warehouse_date = DateTime.Now;
                    context.tb_serial_no_and_p_code.Add(snpm);
                    context.SaveChanges();
                  //  snpm.Create();
                }
            }
            db.ProductModel_p.ChangeQuantity(context, PM.p_code);
            textBoxAdd.Text = "";
            btn.Enabled = true;
            frmProductCheckStore_Shown(null, null);
        }
    }
}
