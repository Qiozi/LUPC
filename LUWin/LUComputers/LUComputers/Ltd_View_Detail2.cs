using LUComputers.DBProvider;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LUComputers
{
    public partial class Ltd_View_Detail2 : Form
    {
        public Ltd_View_Detail2()
        {
            InitializeComponent();

            WinLoad();
        }

        private void WinLoad()
        {
            //
            // load Ltd info 
            //
            this.toolStripComboBox_Ltd.Items.Clear();
            LtdHelper LH = new LtdHelper();
            DataTable dt = LH.LtdHelperToDataTable();
            this.toolStripComboBox_Ltd.Items.Add("Select Ltd");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                this.toolStripComboBox_Ltd.Items.Add(dt.Rows[i]["text"].ToString());
            }

            //
            //
            // load luc Category
            dt = Config.RemoteExecuteDateTable("Select menu_child_name, menu_child_serial_no from tb_product_category where menu_pre_serial_no=0 and tag=1 order by menu_child_order asc ");
            if (this.toolStripComboBox_lu_category1.Items.Count == 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    this.toolStripComboBox_lu_category1.Items.Add(string.Format("{0}|{1}", dr[0].ToString()
                        , dr[1].ToString()));

                }
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            string ltd_name = this.toolStripComboBox_Ltd.Text;
            LtdHelper LH = new LtdHelper();
            int ltd_id = LH.GetLtdIdByText(ltd_name);
            Ltd ltd = LH.LtdModelByValue(ltd_id);

            string table_name = this.toolStripComboBox1.Text;

            if (ltd_id > 0)
            {
                if (table_name.Length >5)
                {
                    string sql = "";
                    switch (ltd)
                    {

                        case Ltd.wholesaler_asi:
                            sql = string.Format(@"
select 
    luc_sku
    ,asi_sku part_sku
    ,itmeid mfp
    ,description part_name
    ,vendor mfp_name
    ,cat
    ,quantity
    ,price part_cost
    ,sub_category
    ,status
    ,weight
    ,'' upc
    ,regdate
from {0}
{1}
order by cat,sub_category, description asc "
                                , DBProvider.TableName.AsiAll
                                , toolStripTextBoxCmd.Text.Trim() == "1" ? " where quantity <> 0" : ""
                                );

                            break;
                        default:
                            sql = string.Format(" Select * from {0} ", table_name);
                            break;
                    }
                    DataTable dt = Config.ExecuteDateTable(sql);
                    this.dataGridView1.DataSource = dt;
                    this.toolStripTextBox_record_count.Text = "record: " + dt.Rows.Count.ToString();

                    ReadLUCMatchSKU();
                }
                else
                {
                    this.dataGridView1.Rows.Clear();
                    this.toolStripTextBox_record_count.Text = "record: 0" ;
                }
            }
            else
                MessageBox.Show("Please select LTD");
        }

        public void ReadLUCMatchSKU()
        {
            int count = 0;
            for (int i = 0; i < this.dataGridView1.Rows.Count; i++)
            {
                int luc_sku = 0;
                try
                {
                    int.TryParse(this.dataGridView1.Rows[i].Cells["luc_sku"].Value.ToString(), out luc_sku);
                }
                catch { }
                if (luc_sku > 0)
                {
                    count += 1;
                }

            }
            this.toolStripStatusLabel_luc_sku_record.Text = count.ToString();
        }

        private void toolStripComboBox_Ltd_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.toolStripComboBox1.Items.Clear();
            string ltd_name = this.toolStripComboBox_Ltd.Text;
            LtdHelper LH = new LtdHelper();
            int ltd_id = LH.GetLtdIdByText(ltd_name);

            DataTable dt = Config.ExecuteDateTable(string.Format(@"Select db_table_name 
from tb_other_inc_run_date where other_inc_id='{0}' order by id desc", ltd_id));

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                this.toolStripComboBox1.Items.Add(dt.Rows[i][0].ToString());
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "serial_no"
                || dataGridView1.Columns[e.ColumnIndex].Name == "part_sku")
            {
                string ltd_name = this.toolStripComboBox_Ltd.Text;
                LtdHelper LH = new LtdHelper();
                int ltd_id = LH.GetLtdIdByText(ltd_name);

                string mfp = dataGridView1.Rows[e.RowIndex].Cells["mfp"].Value.ToString();

                DataTable dt = Config.RemoteExecuteDateTable("Select product_serial_no from tb_product where manufacturer_part_number='" + mfp + "'");
                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show("Exist.");
                }
                else
                {
                    int cateID = GetLUCategoryID();
                    decimal price;
                    decimal cost;
                    decimal.TryParse(dataGridView1.Rows[e.RowIndex].Cells["part_cost"].Value.ToString(), out cost);
                    //price = new EditPriceToRemote().GetNewSpecial(cost);
                    //price = price * 1.06M;
                    price = EditPriceToRemote.GetNewSpecial(cost, 0, cateID);
                    string mfp_name =  dataGridView1.Rows[e.RowIndex].Cells["mfp_name"].Value.ToString();

                    string LTD_sku = dataGridView1.Rows[e.RowIndex].Cells["part_sku"].Value.ToString();

                    if (cateID < 1)
                    {
                        MessageBox.Show("Select Category");
                        return;
                    }
                    
                    DataTable o = Config.RemoteExecuteDateTable(string.Format(@"
Insert into tb_product (
menu_child_serial_no
,product_short_name
,product_name
,product_img_sum
,product_name_long_en
,is_non
,manufacturer_part_number
,producter_serial_no
,new
,other_product_sku
,export
,product_current_cost
,product_current_price
,product_current_special_cash_price
,split_line
,tag
,issue
,product_size_id
,keywords
,supplier_sku
,last_regdate
,regdate
,UPC
,weight
)
values
('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}',now(),now(), '{20}','{21}');

select last_insert_id();"
                        , cateID
                        , GetShortName(cateID, mfp, dataGridView1.Rows[e.RowIndex].Cells["part_name"].Value.ToString())
                        , dataGridView1.Rows[e.RowIndex].Cells["part_name"].Value.ToString()
                        , 1
                        , dataGridView1.Rows[e.RowIndex].Cells["part_name"].Value.ToString()
                        , 0
                        , mfp
                        , mfp_name
                        , 1
                        , 999999
                        , 1
                        , cost
                        , EditPriceToRemote.GetNewPrice(price)
                        , price
                        , 0
                        , 1
                        , 0
                        , 1
                        , "["+mfp_name+"]"
                        , LTD_sku
                         , dataGridView1.Rows[e.RowIndex].Cells["upc"].Value.ToString()
                         , dataGridView1.Rows[e.RowIndex].Cells["weight"].Value.ToString() 
                        ));

                    int luc_sku;
                    int.TryParse(o.Rows[0][0].ToString(), out luc_sku);


                    Config.RemoteExecuteNonQuery(string.Format(@"insert into tb_other_inc_match_lu_sku 
	( lu_sku, other_inc_sku, other_inc_type
	)
	values
	( '{0}', '{1}', '{2}'
	)"
                        , luc_sku, LTD_sku, ltd_id));
                    MessageBox.Show(luc_sku.ToString());
                }
            
            }
        }

        public string GetShortName(int categoryID, string mfp, string short_name)
        {

            return short_name;
        }

        private void toolStripComboBox_luc_category1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str = this.toolStripComboBox_lu_category1.Text;
            int id;
            int.TryParse(str.Split(new char[] { '|' })[1].ToString(), out id);
            BindLUCategory2(id);
        }
        private void BindLUCategory2(int parentID)
        {


            DataTable dt = Config.RemoteExecuteDateTable("Select menu_child_name, menu_child_serial_no from tb_product_category where menu_pre_serial_no='" + parentID.ToString() + "' and tag=1 order by menu_child_order asc ");


            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                this.toolStripComboBox_luc_category2.Items.Add(string.Format("{0}|{1}", dr[0].ToString()
                    , dr[1].ToString()));

            }

        }
        private int GetLUCategoryID()
        {
            string str = this.toolStripComboBox_luc_category2.Text;
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

        private void toolStripComboBox_luc_category1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripComboBox_Ltd_Click(object sender, EventArgs e)
        {

        }
    }
}
