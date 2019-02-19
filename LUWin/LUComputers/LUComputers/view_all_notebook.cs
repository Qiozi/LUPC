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
    public partial class view_all_notebook : Form
    {
        public view_all_notebook()
        {
            InitializeComponent();
            this.Shown += new EventHandler(view_all_notebook_Shown);
        }

        void view_all_notebook_Shown(object sender, EventArgs e)
        {
            this.dataGridView1.DataSource = Config.ExecuteDateTable(@"
select luc_sku, n.* from tb_supercom_notebook n where cost>0 and (synnex_cost=0 or synnex_cost>280) order by brand,category, mfp, name asc 
");
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (dataGridView1.Columns[e.ColumnIndex].Name == "luc_sku")
            //{
            //    int lu_sku;
            //    int.TryParse((dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value ?? "").ToString(), out lu_sku);

            //    string name = dataGridView1.Rows[e.RowIndex].Cells["name"].Value.ToString();
            //    DialogResult dr = MessageBox.Show("Are you sure", "conform", MessageBoxButtons.OKCancel);
            //    if (dr == DialogResult.OK)
            //    {
            //        Config.RemoteExecuteNonQuery(string.Format("Update tb_product set product_name_long_en='{0}' where product_serial_no='{1}'", name.Replace("'", "\\'"), lu_sku));
            //        this.toolStripLabel2.Text = lu_sku.ToString() + " OK";
            //    }
            //}
            if (dataGridView1.Columns[e.ColumnIndex].Name == "id")
            {
                //int lu_sku;
                //int.TryParse((dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value ?? "").ToString(), out lu_sku);

                string mfp = dataGridView1.Rows[e.RowIndex].Cells["mfp"].Value.ToString();
                string prodType = dataGridView1.Rows[e.RowIndex].Cells["prodType"].Value.ToString();

                if (mfp.Trim().Length < 1)
                    return;
                if (0 < Config.RemoteExecuteDateTable("Select * from tb_product where manufacturer_part_number='" + mfp + "' and prodType='"+ prodType +"'").Rows.Count)
                {
                    MessageBox.Show("Exist.....");
                    if (mfp.Length > 2)
                    {
                        Config.RemoteExecuteNonQuery("update tb_product set tag=1 where manufacturer_part_number='" + mfp + "' and prodType='" + prodType + "'");
                    }
                    new Watch.LU().deleteExistNoMatch(mfp, prodType);
                    return;
                }
                int categoryID = 350;// GetLUCategoryID();
                decimal price;
                decimal cost;
                decimal.TryParse(dataGridView1.Rows[e.RowIndex].Cells["cost"].Value.ToString(), out cost);
                //price = price * 1.06M;
                price = EditPriceToRemote.GetNewSpecial(cost, 0, categoryID);
                string mfp_name = dataGridView1.Rows[e.RowIndex].Cells["brand"].Value.ToString();

                string LTD_sku = dataGridView1.Rows[e.RowIndex].Cells["supercom_sku"].Value.ToString();


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
,prodType
)
values
('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}',now(),now(),'{20}');

select last_insert_id();"
                        , categoryID
                        , mfp_name + " Notebook " + mfp

                        , mfp_name + " Notebook " + mfp
                        , 1
                        , dataGridView1.Rows[e.RowIndex].Cells["name"].Value.ToString().Replace("'", "\\'")
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
                        , "[" + mfp_name + "]"
                        , LTD_sku
                        , prodType
                        ));

                    int luc_sku;
                    int.TryParse(o.Rows[0][0].ToString(), out luc_sku);


                    //LtdHelper LH = new LtdHelper();
                    //int ltd_id = LH.LtdHelperValue(Ltd.wholesaler_supercom);


//                    Config.RemoteExecuteNonQuery(string.Format(@"insert into tb_other_inc_match_lu_sku 
//	( lu_sku, other_inc_sku, other_inc_type
//	)
//	values
//	( '{0}', '{1}', '{2}'
//	)"
                        //, luc_sku, LTD_sku, ltd_id));
                    MessageBox.Show(luc_sku.ToString());
                }
            }

        }
    }
}
