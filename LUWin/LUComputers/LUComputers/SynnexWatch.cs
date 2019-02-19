using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.IO;
using LUComputers.DBProvider;

namespace LUComputers
{
    public partial class SynnexWatch : Form
    {
        public SynnexWatch()
        {
            InitializeComponent();
            this.Shown += new EventHandler(SynnexWatch_Shown);
        }

        void SynnexWatch_Shown(object sender, EventArgs e)
        {
            DataTable dt = Config.RemoteExecuteDateTable("Select menu_child_name, menu_child_serial_no from tb_product_category where menu_pre_serial_no=0 and tag=1 order by menu_child_order asc ");
            if (this.comboBox1.Items.Count == 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    this.comboBox1.Items.Add(string.Format("{0}|{1}", dr[0].ToString()
                        , dr[1].ToString()));

                }
            }
        }

        private void button_brower_Click(object sender, EventArgs e)
        {
            DialogResult dr = this.openFileDialog1.ShowDialog();
            if (dr == DialogResult.OK)
            {
                this.textBox1.Text = this.openFileDialog1.FileName;
            }
        }

        private void button_update_Click(object sender, EventArgs e)
        {
            string table_name = "";
            LoadToDB(this.textBox1.Text.Trim(), ref table_name);
            //this.dataGridView1.DataSource = Config.ExecuteDateTable("Select * from " + table_name);
        }

        void ReplaceMFPName(DataRow dr)
        {
            string mfpName = dr["Manufacturer name"].ToString();
            if (mfpName == "ASUS COMPUTER INTERNATIONAL")
            {
                dr["Manufacturer name"] = "Asus";
            }
            if (mfpName == "GIGA-BYTE")
            {
                dr["Manufacturer name"] = "Gigabyte";
            }
            if (mfpName == "CREATIVE TECHNOLOGY")
            {
                dr["Manufacturer name"] = "CREATIVE";
            }
            if (mfpName == "ADVANCED MICRO DEVICES")
            {
                dr["Manufacturer name"] = "AMD";
            }
            if (mfpName == "KINGSTON TECHNOLOGY")
            {
                dr["Manufacturer name"] = "Kingston";
            }
            if (mfpName == "HP INC.")
            {
                dr["Manufacturer name"] = "HP";
            }
        }
        /// <summary>
        /// 002057021, 002057146, 002057152, 002057184, 002057240, 002057245, 002057246, 002057247, 002057249
        /// 002057253, 002057446, 002057447, 002057448, 002057509, 002057530, 002057542
        /// </summary>
        /// <param name="fullFilename"></param>
        /// <param name="table_name"></param>
        public void LoadToDB(string fullFilename, ref string table_name)
        {
            table_name = DBProvider.TableName.SynnexAll2;// CreateTable(Ltd.wholesaler_Synnex);

            DataTable dt = Util.HSSFExcel.ToDataTable(textBox1.Text);

            dt.Columns.Add("luc_sku");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string mfp = dt.Rows[i]["MFG Part#"].ToString();
                if (!string.IsNullOrEmpty(mfp))
                {
                    DataTable subDt = Config.ExecuteDateTable("select lu_sku from tb_other_inc_valid_lu_sku where manufacturer_part_number='" + mfp + "'");
                    if (subDt.Rows.Count > 0)
                        dt.Rows[i]["luc_sku"] = subDt.Rows[0][0].ToString();
                }

                ReplaceMFPName(dt.Rows[i]);
            }

            if (checkBox1.Checked)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["Avail"].ToString() == "0")
                    {
                        dt.Rows.RemoveAt(i);
                        i--;
                    }
                }
            }
            this.dataGridView1.DataSource = dt;

        }

        #region Create Table
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ltd"></param>
        /// <returns></returns>
        public string CreateTable(Ltd ltd)
        {
            LtdHelper LH = new LtdHelper();
            int ltd_id = LH.LtdHelperValue(ltd);
            string date_short_name = DateTime.Now.ToString("yyyyMMdd");
            string talbe_name = "store_" + (LH.FilterText(ltd.ToString())) + "_part_" + date_short_name;

            //
            // record table Name and Datatime.
            //
            if (Config.ExecuteScalarInt("select count(*) from tb_other_inc_run_date where date_format(run_date,'%Y%m%d')='" + date_short_name + "' and other_inc_id='" + ltd_id.ToString() + "'") == 0)
            {
                Config.ExecuteNonQuery(string.Format(@"
insert into tb_other_inc_run_date 
	( other_inc_id, run_date,db_table_name)
	values
	( '{0}', now(),'{1}')
", ltd_id, talbe_name));
            }
            //
            // create table
            //
            Config.ExecuteNonQuery(@"
 drop table  IF EXISTS `" + talbe_name + @"`;

 CREATE TABLE `" + talbe_name + @"` (              
                    `serial_no` int(6) NOT NULL auto_increment,  
		            `luc_sku`   int(6) default Null,
                    `cost`      decimal(10,2) default Null,
                    `msrp`      varchar(20) default NULL,
                    `part_sku` varchar(30) default NULL,        
                    `part_cost` varchar(15) default NULL,    
                    `status_code` varchar(5) default NULL,        
                    `store_quantity` varchar(10) default 0,    
                    `mfp` varchar(100) default NULL,  
                    `mfp_name` varchar(40) default NULL, 
		            `part_name` varchar(100) default Null,
                    `long_name` varchar(200) default Null,
                    `regdate` timestamp NULL default CURRENT_TIMESTAMP,              
                    PRIMARY KEY  (`serial_no`)         
                    ) ENGINE=InnoDB DEFAULT CHARSET=latin1           

");
            return talbe_name;
        }
        #endregion


        #region Load Notebook to DB.

        /// <summary>
        /// 获取价格值，其他非 TOSHIBA 价格，另存在库里
        /// </summary>
        /// <param name="table_name"></param>
        private void MatchPriceToNotebookDB(string table_name)
        {
            Config.ExecuteNonQuery(string.Format(@"
update tb_supercom_notebook n, {0} s 
set n.cost= s.cost, n.quantity= s.store_quantity, n.synnex_cost= s.cost, n.luc_sku=s.luc_sku
where n.mfp=s.mfp and s.mfp<>'' and length(s.mfp)>1  and mfp_name ='TOSHIBA'", table_name));
            Config.ExecuteNonQuery(string.Format(@"
update tb_supercom_notebook n, {0} s 
set n.synnex_cost= s.cost
where n.mfp=s.mfp and s.mfp<>'' and length(s.mfp)>1 ", table_name));
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string filepath = this.textBox1.Text;

            //using (OleDbConnection conn = new OleDbConnection(Config.ExcelConnstring(filepath)))
            //{
            //    try
            //    {
            //        conn.Open();
            //        // SKU, itmeid, description, vendor, cat, quantity, price,weight, size, unit, sub_category, status
            //        OleDbDataAdapter da = new OleDbDataAdapter(@"select * FROM [table$]", conn);
            //        DataTable dt = new DataTable();
            //        da.Fill(dt);
            //        conn.Close();

            DataTable dt = Util.HSSFExcel.ToDataTable(filepath);

            //
            // Delete  Notebook
            Config.ExecuteNonQuery("delete from tb_supercom_notebook where brand = 'TOSHIBA'");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                Config.ExecuteNonQuery(string.Format(@"
insert into tb_supercom_notebook(
mfp, name, brand, ltd)
values ('{0}', '{1}', '{2}', 'Synnex')
", dr["mfp"].ToString()
, dr["long_description"].ToString()
, dr["Manufacturer_name"].ToString()));
            }

            this.dataGridView1.DataSource = Config.ExecuteDateTable("Select * from tb_supercom_notebook where brand= 'TOSHIBA'");
            //}
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.Message);
            //    }
            //}
        }
        #endregion

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str = this.comboBox1.Text;
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
                this.comboBox2.Items.Add(string.Format("{0}|{1}", dr[0].ToString()
                    , dr[1].ToString()));

            }
        }

        private int GetLUCategoryID()
        {
            string str = this.comboBox2.Text;
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
            if (e.ColumnIndex == 0)
            {

                int ltd_id = (int)Ltd.wholesaler_Synnex;

                string mfp = dataGridView1.Rows[e.RowIndex].Cells["MFG Part#"].Value.ToString();

                DataTable dt = Config.RemoteExecuteDateTable("Select product_serial_no from tb_product where manufacturer_part_number='" + mfp + "'");
                if (dt.Rows.Count > 0)
                {
                    int cateID = GetLUCategoryID();
                    if (cateID < 1)
                    {
                        MessageBox.Show("Select Category");
                        return;
                    }

                    MessageBox.Show("Exist.....");
                    if (mfp.Length > 2)
                    {
                        decimal cost;
                        decimal.TryParse(dataGridView1.Rows[e.RowIndex].Cells["Promotion"].Value.ToString().Replace("$", "").Replace(",", ""), out cost);
                        Config.RemoteExecuteNonQuery("update tb_product set tag=1,product_current_cost='" + cost.ToString() + "', product_current_price='" + EditPriceToRemote.GetNewSpecial(cost, 0, cateID) + "' where manufacturer_part_number='" + mfp + "' and prodType='NEW'");
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

                    decimal.TryParse(dataGridView1.Rows[e.RowIndex].Cells["Promotion"].Value.ToString().Replace("$", "").Replace(",", ""), out cost);

                    pm.product_current_cost = cost;

                    if (pm.product_current_cost == 0M)
                    {
                        MessageBox.Show("Cost is 0");
                        return;
                    }
                    pm.manufacturer_part_number = mfp;
                    price = EditPriceToRemote.GetNewSpecial(pm.product_current_cost.Value, 0, pm.menu_child_serial_no.Value);
                    pm.producter_serial_no = dataGridView1.Rows[e.RowIndex].Cells["Manufacturer name"].Value.ToString();
                    if (pm.producter_serial_no.ToLower().IndexOf("GIGA-BYTE".ToLower()) > -1)
                        pm.producter_serial_no = "Gigabyte";
                    pm.supplier_sku = dataGridView1.Rows[e.RowIndex].Cells["SKU#"].Value.ToString();
                    pm.product_short_name = GetShortName(pm.producter_serial_no, pm.manufacturer_part_number, pm.menu_child_serial_no.Value);
                    pm.product_current_special_cash_price = price;
                    pm.product_current_price = EditPriceToRemote.GetNewPrice(price);
                    pm.UPC = dataGridView1.Rows[e.RowIndex].Cells["UPC"].Value.ToString();
                    decimal weight;
                    decimal.TryParse(dataGridView1.Rows[e.RowIndex].Cells["Weight(lb.)"].Value.ToString(), out weight);
                    pm.weight = weight;
                    pm.product_short_name = GetShortName(pm.producter_serial_no, pm.manufacturer_part_number, pm.menu_child_serial_no.Value);
                    pm.product_name = pm.product_short_name;
                    pm.product_name_long_en = dataGridView1.Rows[e.RowIndex].Cells["Long Description"].Value.ToString();

                    addProduct pf = new addProduct(pm, ltd_id);
                    pf.StartPosition = FormStartPosition.CenterParent;
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



        string GetShortName(string mfp_name, string mfp, int cateID)
        {
            if (cateID == 350)
            {
                return string.Format("{0} {1} {2}", mfp_name, "Notebook", mfp);
            }
            else if (cateID == 31)
            {
                return string.Format("{0} {1} {2}", mfp_name, mfp, "Motherboard");
            }
            else if (cateID == 41)
            {
                return string.Format("{0} {1} {2}", mfp_name, mfp, "Video Card");
            }
            else if (cateID == 29)
            {
                return string.Format("{0} {1} {2}", mfp_name, mfp, "Memory");
            }

            return "";
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (textBoxMFP_Name.Text.Trim() == "")
                return;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Cells["Manufacturer name"].Value == null) continue;

                string oldMFPName = dataGridView1.Rows[i].Cells["Manufacturer name"].Value.ToString();
                if (oldMFPName.Trim() == textBoxMFP_Name.Text.Trim())
                    dataGridView1.Rows[i].Cells["Manufacturer name"].Value = textBoxMFP_Name2.Text.Trim();
            }
        }
    }
}
