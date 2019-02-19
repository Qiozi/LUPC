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
    /// <summary>
    /// 显示所有supercom 产品
    /// </summary>
    public partial class SupercomALL_View : Form
    {
        public class Cate
        {
            public string Id { get; set; }

            public string Txt { get; set; }
        }

        List<Cate> Cates = new List<Cate>();

        public SupercomALL_View()
        {
            Cates.Add(new Cate
            {
                Id = "007121000",
                Txt = "显卡"
            });
            Cates.Add(new Cate
            {
                Id = "013062240",
                Txt = "主板"
            });
            Cates.Add(new Cate
            {
                Id = "012047239",
                Txt = "硬盘"
            });

            Cates.Add(new Cate
            {
                Id = "002057",
                Txt = "电脑"
            });
            Cates.Add(new Cate
            {
                Id = "008086000",
                Txt = "电源"
            });
            Cates.Add(new Cate
            {
                Id = "005038126",
                Txt = "LCD"
            });
            Cates.Add(new Cate
            {
                Id = "003056000",
                Txt = "键盘"
            });
            Cates.Add(new Cate
            {
                Id = "013114000",
                Txt = "Case"
            });
            Cates.Add(new Cate
            {
                Id = "01308948",
                Txt = "Cooler"
            });
            Cates.Add(new Cate
            {
                Id = "013320240",
                Txt = "CPU"
            });
            Cates.Add(new Cate
            {
                Id = "013090240",
                Txt = "CPU2"
            });
            Cates.Add(new Cate
            {
                Id = "002662000",
                Txt = "Tablet"
            });
            Cates.Add(new Cate
            {
                Id = "009058309",
                Txt = "Print"
            });
            Cates.Add(new Cate
            {
                Id = "012635000",
                Txt = "SSD"
            });
            Cates.Add(new Cate
            {
                Id = "013519523",
                Txt = "USB Flash"
            });
            InitializeComponent();
            this.Shown += SupercomALL_View_Shown;

        }

        void SupercomALL_View_Shown(object sender, EventArgs e)
        {

            BindCate1();
            LoadWin();
        }

        void BindCate1()
        {
            foreach (var item in this.Cates)
            {
                this.toolStripDropDownCate1.Items.Add(string.Concat(item.Id, "|", item.Txt));
            }
        }

        void BindCate2(string idtxt)
        {
            var id = idtxt.Split(new char[] { '|' })[0];
            var txt = idtxt.Split(new char[] { '|' })[1];
            // 
            // load supercom Category
            //
            string table_name = new LtdHelper().GetLastStoreTableName(Ltd.supercom_all);

            DataTable dt = new DataTable();
            switch (id)
            {
                case "002057":
                    dt = Config.ExecuteDateTable(@"select distinct mfp_name from store_all_synnex where cat_code like '002057%' and cat_code <> '002057146' and cat_code<> '002057021' order by mfp_name asc , part_name asc");
                    break;
                default:
                    dt = Config.ExecuteDateTable(@"select distinct mfp_name from store_all_synnex where cat_code like '" + id + "%'   order by mfp_name asc, part_name asc ");
                    break;
            }

            this.toolStripComboBox1.Items.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                this.toolStripComboBox1.Items.Add(string.Format("{0}", dr[0].ToString()));
            }
        }


        private void LoadWin()
        {


            //
            //
            // load luc Category
            DataTable dt = Config.RemoteExecuteDateTable("Select menu_child_name, menu_child_serial_no from tb_product_category where menu_pre_serial_no=0 and tag=1 order by menu_child_order asc ");
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
            Search(this.toolStripComboBox1.Text);
            dataGridView1.Columns["store_quantity"].ValueType = typeof(int);
        }

        private void Search(string keyword)
        {
            var id = this.toolStripDropDownCate1.Text.Split(new char[] { '|' })[0];
            DataTable dt = new DataTable();
            if (keyword == "")
            {
                if (id == "002057")
                    dt = Config.ExecuteDateTable(string.Format(@"select * from store_all_synnex 
where cat_code like '002057%' and cat_code <> '002057146' and cat_code<> '002057021' and mfp_name='lenovo' and store_quantity>0 and luc_sku=0
"));
                else
                {
                    dt = Config.ExecuteDateTable(string.Format(@"select * from store_all_synnex 
where cat_code like '" + id + @"%' and mfp_name='lenovo' and store_quantity>0 and luc_sku=0
"));
                }

            }
            else
            {
                if (id == "002057")
                {
                    dt = Config.ExecuteDateTable(string.Format(@"
select * from store_all_synnex 
where cat_code like '002057%' and cat_code <> '002057146' and cat_code<> '002057021' and mfp_name='{0}' and store_quantity>0 and luc_sku=0
", keyword));
                }
                else
                {
                    dt = Config.ExecuteDateTable(string.Format(@"
select * from store_all_synnex 
where cat_code like '" + id + @"%'  and mfp_name='{0}' and store_quantity>0 and luc_sku=0
", keyword));
                }


            }
            //dt.Columns["store_quantity"].DataType = typeof(int);
            this.dataGridView1.DataSource = dt;
        }

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            //int kk = dataGridView1.Rows.Count - 1;
            //for (int i = 0; i < kk; i++)
            //{
            //    if (dataGridView1.Rows[i].Cells["available"].Value.ToString() == "0")
            //    {
            //        dataGridView1.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.Gray;
            //    }
            //    else if (dataGridView1.Rows[i].Cells["prodType"].Value.ToString() != "NEW")
            //    {
            //        dataGridView1.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.Green;
            //    }

            //}
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Search(this.toolStripComboBox1.Text);
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            if (dataGridView1.Columns[e.ColumnIndex].Name == "serial_no")
            {
                //int lu_sku;
                //int.TryParse((dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value ?? "").ToString(), out lu_sku);

                string mfp = dataGridView1.Rows[e.RowIndex].Cells["mfp"].Value.ToString();
                string prodType = "NEW";// dataGridView1.Rows[e.RowIndex].Cells["prodType"].Value.ToString();

                int categoryID = GetLUCategoryID();// 350;
                decimal price;
                decimal cost;
                decimal.TryParse(dataGridView1.Rows[e.RowIndex].Cells["cost"].Value.ToString(), out cost);
                //price = price * 1.06M;
                price = EditPriceToRemote.GetNewSpecial(cost, 0, categoryID);

                if (0 < Config.RemoteExecuteDateTable("Select * from tb_product where manufacturer_part_number='" + mfp + "' and prodType='" + prodType + "'").Rows.Count)
                {
                    MessageBox.Show("Exist.....");
                    if (mfp.Length > 2)
                    {
                        Config.RemoteExecuteNonQuery("update tb_product set tag=1,product_current_cost='" + cost.ToString() + "', product_current_price='" + EditPriceToRemote.GetNewPrice(price) + "' where manufacturer_part_number='" + mfp + "' and prodType='" + prodType + "'");
                    }
                    new Watch.LU().deleteExistNoMatch(mfp, prodType);
                    return;
                }

                string mfp_name = dataGridView1.Rows[e.RowIndex].Cells["mfp_name"].Value.ToString();

                string LTD_sku = dataGridView1.Rows[e.RowIndex].Cells["part_sku"].Value.ToString();
                string UPC = dataGridView1.Rows[e.RowIndex].Cells["upc"].Value.ToString();
                string weight = dataGridView1.Rows[e.RowIndex].Cells["ship_weight"].Value.ToString();
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
,UPC
,weight
)
values
('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}',now(),now(), '{20}', '{21}', '{22}');

select last_insert_id();"
                        , categoryID
                        , mfp_name + " " + mfp + (categoryID == 350 ? " Notebook " : " ")//ReplaceSupercomSKU(dataGridView1.Rows[e.RowIndex].Cells["short_name"].Value.ToString(), categoryID)
                        , mfp_name + " " + mfp + (categoryID == 350 ? " Notebook " : " ")//ReplaceSupercomSKU(dataGridView1.Rows[e.RowIndex].Cells["short_name"].Value.ToString(), categoryID)
                        , 1
                        , dataGridView1.Rows[e.RowIndex].Cells["part_name"].Value.ToString().Replace("'", "\\'") + (categoryID == 350 ? (mfp_name.ToLower() == "lenovo" ? (mfp.Substring(mfp.Length - 1, 1).ToUpper() == "F" ? "" : "") : "") : "")
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
                        , UPC
                        , weight
                        ));

                    int luc_sku;
                    int.TryParse(o.Rows[0][0].ToString(), out luc_sku);


                    LtdHelper LH = new LtdHelper();
                    int ltd_id = LH.LtdHelperValue(Ltd.wholesaler_supercom);


                    Config.RemoteExecuteNonQuery(string.Format(@"insert into tb_other_inc_match_lu_sku 
	( lu_sku, other_inc_sku, other_inc_type, prodType)
	values
	( '{0}', '{1}', '{2}','{3}')"
                        , luc_sku, LTD_sku, ltd_id, prodType));
                    MessageBox.Show(luc_sku.ToString());
                }
            }

        }

        private void dataGridView1_SortCompare(object sender, DataGridViewSortCompareEventArgs e)
        {
            if (e.Column.Name == "store_quantity")
            {
                e.SortResult = (Convert.ToInt32(e.CellValue1) - Convert.ToInt32(e.CellValue2) > 0) ? 1 : (Convert.ToInt32(e.CellValue1) - Convert.ToInt32(e.CellValue2) < 0) ? -1 : 0;

            }
            else
                e.SortResult = System.String.Compare(Convert.ToString(e.CellValue1), Convert.ToString(e.CellValue2));
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            string mfp1 = this.toolStripTextBoxMFPName1.Text.Trim();
            string mfp2 = this.toolStripTextBoxMFPName2.Text.Trim();

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Cells["mfp_name"].Value == null) continue;

                if (dataGridView1.Rows[i].Cells["mfp_name"].Value.ToString() == mfp1)
                    dataGridView1.Rows[i].Cells["mfp_name"].Value = mfp2;
            }
        }

        private void toolStripDropDownCate1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripDropDownCate1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var cate = sender as ToolStripComboBox;
            if (!string.IsNullOrEmpty(cate.Text))
            {
                BindCate2(cate.Text);
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

            this.toolStripComboBox_lu_category2.Items.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                this.toolStripComboBox_lu_category2.Items.Add(string.Format("{0}|{1}", dr[0].ToString()
                    , dr[1].ToString()));

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
