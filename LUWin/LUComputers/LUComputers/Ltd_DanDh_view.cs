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
    public partial class Ltd_DanDh_view : Form
    {
        public Ltd_DanDh_view()
        {
            InitializeComponent();
            LoadWin();
        }

        private void LoadWin()
        {
            // 
            // load DanDh Category
            //
            string table_name = new LtdHelper().GetLastStoreTableName(Ltd.wholesaler_dandh);
            DataTable dt = Config.ExecuteDateTable(@"select distinct categoryID,category_name from tb_other_inc_dandh_category order by category_name asc ");
            this.toolStripComboBox_dh_category_1.Items.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                this.toolStripComboBox_dh_category_1.Items.Add(string.Format("{0}||({1})", dr[0].ToString(), dr[1].ToString()));
            }


            //
            //
            // load luc Category
            dt = Config.RemoteExecuteDateTable("Select menu_child_name, menu_child_serial_no from tb_product_category where menu_pre_serial_no=0 and tag=1 order by menu_child_order asc ");
            if (this.toolStripComboBox_luc_category1.Items.Count == 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    this.toolStripComboBox_luc_category1.Items.Add(string.Format("{0}|{1}", dr[0].ToString()
                        , dr[1].ToString()));
                }
            }
        }
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Search(toolStripTextBoxKeyword.Text.Trim());
        }

        private void Search(string keyword)
        {
            string table_name = DBProvider.TableName.DandhAll;// new LtdHelper().GetLastStoreTableName(Ltd.wholesaler_dandh);

            int id = 0;
            int.TryParse(this.toolStripComboBox_dh_category_2.Text.Split(new char[] { '|' })[0], out id);

            if (keyword == "1")
            {
                this.dataGridView1.DataSource = Config.ExecuteDateTable(string.Format(@"select
luc_sku
,	stock_status
,  quantity_on_hand,  
	item_number, 
	mfr_item_number, 
	vendor_name, 
	cost, 
	rebate_amount, 
	short_description, 
	long_description,
    concat(c.category_name , '===', c.sub_category_name) catName
    ,d.upc_code upc
    ,d.weight
	 
	from {0} d inner join tb_other_inc_dandh_category c on c.categoryid= d.subcategory_code or c.sub_categoryid=d.subcategory_code where quantity_on_hand>0 order by subcategory_code asc, vendor_name asc,short_description asc", table_name));


            }
            else
            {
                this.dataGridView1.DataSource = Config.ExecuteDateTable(string.Format(@"
                select
luc_sku
,	stock_status
,  quantity_on_hand,  
	item_number, 
	mfr_item_number, 
	vendor_name, 
	cost, 
	rebate_amount, 
	short_description, 
	long_description,
    concat(c.category_name , '===', c.sub_category_name) catName
    ,d.upc_code upc
    ,d.weight
	 
	from {0} d inner join tb_other_inc_dandh_category c on c.categoryid= d.subcategory_code or c.sub_categoryid=d.subcategory_code
where subcategory_code='{1}'
order by subcategory_code asc, vendor_name asc,short_description asc"
                    , table_name
                    , id));
            }
        }

        private void toolStripComboBox_dh_category_1_SelectedIndexChanged(object sender, EventArgs e)
        {

            string table_name = new LtdHelper().GetLastStoreTableName(Ltd.wholesaler_dandh);

            int cid = 0;
            int.TryParse(this.toolStripComboBox_dh_category_1.Text.Split(new char[] { '|' })[0], out cid);

            DataTable dt = Config.ExecuteDateTable(@"select sub_categoryID,sub_category_name 
from tb_other_inc_dandh_category where sub_categoryID>" + cid.ToString() + " and sub_categoryID<" + (cid + 100) + @" 
            order by category_name asc ");

            this.toolStripComboBox_dh_category_2.Items.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                this.toolStripComboBox_dh_category_2.Items.Add(string.Format("{0}||({1})", dr[0].ToString(), dr[1].ToString()));
            }
        }

        private void toolStripComboBox_luc_category1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str = this.toolStripComboBox_luc_category1.Text;
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
                this.toolStripComboBox_lu_category2.Items.Add(string.Format("{0}|{1}", dr[0].ToString()
                    , dr[1].ToString()));

            }

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            if (dataGridView1.Columns[e.ColumnIndex].Name == "mfr_item_number")
            {

                //int luc_sku;
                //int.TryParse(o.Rows[0][0].ToString(), out luc_sku);

                string mfp = dataGridView1.Rows[e.RowIndex].Cells["mfr_item_number"].Value.ToString();
                if (0 < Config.RemoteExecuteDateTable("Select * from tb_product where manufacturer_part_number='" + mfp + "'").Rows.Count)
                {
                    MessageBox.Show("Exist.....");
                    return;
                }
                int categoryID = GetLUCategoryID();
                decimal price;
                decimal cost;
                decimal.TryParse(dataGridView1.Rows[e.RowIndex].Cells["cost"].Value.ToString(), out cost);
                //price = price * 1.06M;
                price = EditPriceToRemote.GetNewSpecial(cost, 0, categoryID);
                string mfp_name = dataGridView1.Rows[e.RowIndex].Cells["vendor_name"].Value.ToString();

                string LTD_sku = dataGridView1.Rows[e.RowIndex].Cells["mfr_item_number"].Value.ToString();


                if (categoryID != 0)
                {
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
                        , categoryID
                        , dataGridView1.Rows[e.RowIndex].Cells["long_description"].Value.ToString()
                        , dataGridView1.Rows[e.RowIndex].Cells["long_description"].Value.ToString()
                        , 1
                        , dataGridView1.Rows[e.RowIndex].Cells["long_description"].Value.ToString()
                        , 0
                        , mfp
                        , categoryID == 350 ? "HP" : dataGridView1.Rows[e.RowIndex].Cells["vendor_name"].Value.ToString()
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
                        , categoryID == 350 ? "[HP]" : "[" + dataGridView1.Rows[e.RowIndex].Cells["vendor_name"].Value.ToString() + "]"
                        , LTD_sku
                        , dataGridView1.Rows[e.RowIndex].Cells["upc"].Value.ToString()
                         , dataGridView1.Rows[e.RowIndex].Cells["weight"].Value.ToString()
                        ));

                    int luc_sku;
                    int.TryParse(o.Rows[0][0].ToString(), out luc_sku);


                    LtdHelper LH = new LtdHelper();
                    int ltd_id = LH.LtdHelperValue(Ltd.wholesaler_dandh);


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
        private int GetLUCategoryID()
        {
            string str = this.toolStripComboBox_lu_category2.Text;
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
    }
}
