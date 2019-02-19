using LUComputers.DBProvider;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LUComputers
{
    public partial class ltd_ASI_View : Form
    {
        public ltd_ASI_View()
        {
            InitializeComponent();
            this.Shown += new EventHandler(ltd_ASI_View_Shown);
        }

        void BindList(string catString)
        {
            DataTable dt = Config.ExecuteDateTable("Select * from " + DBProvider.TableName.AsiAll + " where cat = '" + catString + "' " + (checkBox1.Checked ? " and quantity >0" : "") + " order by vendor asc, quantity asc");
            this.dataGridView1.DataSource = dt;
        }

        void ltd_ASI_View_Shown(object sender, EventArgs e)
        {
            DataTable dt = Config.ExecuteDateTable("Select distinct cat from " + DBProvider.TableName.AsiAll +" order by cat asc ");
            comboBox1.DataSource = dt;
            comboBox1.DisplayMember = "cat";
            comboBox1.ValueMember = "cat";


            DataTable sdt = Config.RemoteExecuteDateTable("Select menu_child_name, menu_child_serial_no from tb_product_category where menu_pre_serial_no=0 and tag=1 order by menu_child_order asc ");
            if (this.comboBoxCate1.Items.Count == 0)
            {
                for (int i = 0; i < sdt.Rows.Count; i++)
                {
                    DataRow dr = sdt.Rows[i];
                    this.comboBoxCate1.Items.Add(string.Format("{0}|{1}", dr[0].ToString()
                        , dr[1].ToString()));

                }
            }
        }

        private void comboBoxCate1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str = this.comboBoxCate1.Text;
            int id;
            int.TryParse(str.Split(new char[] { '|' })[1].ToString(), out id);
            BindLUCategory2(id);
        }
        public void BindLUCategory2(int id)
        {

            DataTable dt = Config.RemoteExecuteDateTable("Select menu_child_name, menu_child_serial_no from tb_product_category where menu_pre_serial_no='" + id.ToString() + "' and tag=1 order by menu_child_order asc ");


            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                this.comboBoxCate2.Items.Add(string.Format("{0}|{1}", dr[0].ToString()
                    , dr[1].ToString()));

            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindList((sender as ComboBox).SelectedValue.ToString());
        }

        private int GetLUCategoryID()
        {
            string str = this.comboBoxCate2.Text;
            int id = 0;
            if (str.Length > 0)
            {
                int.TryParse(str.Split(new char[] { '|' })[1].ToString(), out id);
                if (id == 0)
                {
                    MessageBox.Show("Error");

                }
            }
            else
                MessageBox.Show("Please select Category");
            return id;
        }
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "id"
               || dataGridView1.Columns[e.ColumnIndex].Name == "asi_sku")
            {
                string ltd_name = Ltd.wholesaler_asi.ToString();
                LtdHelper LH = new LtdHelper();
                int ltd_id = LH.GetLtdIdByText(ltd_name);

                string mfp = dataGridView1.Rows[e.RowIndex].Cells["itmeid"].Value.ToString();

                DataTable dt = Config.RemoteExecuteDateTable("Select product_serial_no from tb_product where manufacturer_part_number='" + mfp + "' and prodType='New'");

                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show("Exist.");
                    if (mfp.Length > 2)
                    {
                        decimal cost;
                        decimal.TryParse(dataGridView1.Rows[e.RowIndex].Cells["price"].Value.ToString().Replace("$", "").Replace(",", ""), out cost);
                        Config.RemoteExecuteNonQuery("update tb_product set tag=1,product_current_cost='" + cost.ToString() + "', product_current_price='" + EditPriceToRemote.GetNewSpecial(cost, 0, GetLUCategoryID()) + "' where manufacturer_part_number='" + mfp + "' and prodType='NEW'");
                    }
                }
                else
                {
                    tb_product pm = new tb_product();

                    pm.menu_child_serial_no = GetLUCategoryID();

                    if (pm.menu_child_serial_no < 1)
                    {
                        MessageBox.Show("Select Category");
                        return;
                    }

                    decimal price;
                    decimal cost;

                    decimal.TryParse(dataGridView1.Rows[e.RowIndex].Cells["price"].Value.ToString().Replace("$", "").Replace(",", ""), out cost);

                    pm.product_current_cost = cost;

                    if (pm.product_current_cost == 0M)
                    {
                        MessageBox.Show("Cost is 0");
                        return;
                    }
                    pm.manufacturer_part_number = mfp;
                    price = EditPriceToRemote.GetNewSpecial(pm.product_current_cost.Value, 0, pm.menu_child_serial_no.Value);
                    pm.producter_serial_no = dataGridView1.Rows[e.RowIndex].Cells["vendor"].Value.ToString();

                    pm.supplier_sku = dataGridView1.Rows[e.RowIndex].Cells["asi_sku"].Value.ToString();
                    pm.product_short_name = GetShortName(pm.menu_child_serial_no.Value
                        , pm.manufacturer_part_number
                        , dataGridView1.Rows[e.RowIndex].Cells["description"].Value.ToString());
                    pm.product_current_special_cash_price = price;
                    pm.product_current_price = EditPriceToRemote.GetNewPrice(price);
                    pm.UPC = "";// dataGridView1.Rows[e.RowIndex].Cells["upc"].Value.ToString();
                    decimal weight;
                    decimal.TryParse(dataGridView1.Rows[e.RowIndex].Cells["weight"].Value.ToString(), out weight);
                    pm.weight = weight;
                    pm.product_short_name = GetShortName(pm.menu_child_serial_no.Value
                        , pm.manufacturer_part_number
                        , dataGridView1.Rows[e.RowIndex].Cells["description"].Value.ToString());
                    pm.product_name = pm.product_short_name;
                    pm.product_name_long_en = dataGridView1.Rows[e.RowIndex].Cells["description"].Value.ToString();
                    pm.UPC = dataGridView1.Rows[e.RowIndex].Cells["upc"].Value.ToString();
                    addProduct pf = new addProduct(pm, ltd_id);
                    if (pf.ShowDialog() == System.Windows.Forms.DialogResult.Yes)
                    {
                        //MessageBox.Show(pm.product_serial_no.ToString());
                    }
                    else
                    {
                        MessageBox.Show("Error");
                    }
                }
            }
        }

        public string GetShortName(int categoryID, string mfp, string short_name)
        {
            return short_name;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.textBoxMFPName1.Text.Trim() == "")
                return;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Cells["vendor"].Value == null) continue;

                string oldMFPName = dataGridView1.Rows[i].Cells["vendor"].Value.ToString();
                if (oldMFPName.Trim() == textBoxMFPName1.Text.Trim())
                {
                    dataGridView1.Rows[i].Cells["vendor"].Value = textBoxMFPName2.Text.Trim();
                }
            }
        }
    }
}
