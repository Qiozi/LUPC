using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;
using System.Data;
using System.IO;
using System.Windows.Forms;
using NPOI.HSSF.UserModel;
using LUComputers.DBProvider;

namespace LUComputers.Watch
{
    public class Eprom
    {
        public event Events.UrlEventHandler WatchE;

        protected void InvokeWatchE(Events.UrlEventArgs e)
        {
            Events.UrlEventHandler watch = WatchE;
            if (watch != null) watch(this, e);
        }

        public void SetStatus(string url, string comment, string result, Ltd ltd)
        {
            LUComputers.Events.UrlEventArgs ua = new LUComputers.Events.UrlEventArgs(new Events.UrlEventModel()
            {
                comment = comment,
                url = url,
                ltd = ltd,
                result = result
            });
            InvokeWatchE(ua);
        }

        public void SetStatus(string url, string comment, Ltd ltd, string result)
        {
            SetStatus(url, comment, result, ltd);
        }
        public void SetStatus(string url, string comment, Ltd ltd)
        {
            SetStatus(url, comment, null, ltd);
        }

        public Eprom()
        {

            //            LtdHelper lh = new LtdHelper();
            //            int ltd_id = lh.LtdHelperValue(ltd);


            //            table_name = CreateTable(ltd);

            //            using (OleDbConnection conn = new OleDbConnection(Config.ExcelConnstring(filename)))
            //            {
            //                conn.Open();
            //                OleDbDataAdapter da = new OleDbDataAdapter(@"select * FROM [table$]", conn);
            //                DataTable dt = new DataTable();
            //                da.Fill(dt);
            //                conn.Close();

            //                tspb.Maximum = dt.Rows.Count;
            //                Watch.LU LU = new LU();
            //                for (int i = 0; i < dt.Rows.Count; i++)
            //                {
            //                    DataRow dr = dt.Rows[i];
            //                    int luc_sku = LU.GetSKUByltdSku(dr["ltd_sku"].ToString(), ltd_id);

            //                    if (luc_sku == 0)
            //                    {
            //                        luc_sku = LU.GetSKUByMfp(dr["ltd_manufacture_code"].ToString());

            //                        if (luc_sku != 0)
            //                        {
            //                            Config.ExecuteDateTable(string.Format(@"insert into tb_other_inc_match_lu_sku (lu_sku , other_inc_sku, other_inc_type) values 
            //                                        ('{0}', '{1}', '{2}')", luc_sku, dr["ltd_sku"].ToString().Trim(), ltd_id));

            //                            //
            //                            // save Match LU SKU.
            //                            // 
            //                            //if (luc_sku > 0)
            //                            //{
            //                            //    Helper.SaveNewMatch snm = new LUComputers.Helper.SaveNewMatch(null, null);
            //                            //    snm.SaveNewMatchSKU(ltd_id, dr["ltd_sku"].ToString().Trim(), luc_sku);
            //                            //}
            //                        }

            //                    }

            //                    string stock_str = dr["ltd_stock"].ToString().Trim();
            //                    if (stock_str != "")
            //                    {
            //                        int stock;
            //                        int.TryParse(stock_str, out stock);
            //                        if (stock_str == "Yes")
            //                            stock = 3;
            //                        if (stock_str == "Call")
            //                            stock = 1;
            //                        if (stock_str == "No")
            //                            stock = 0;

            //                        Config.ExecuteNonQuery(string.Format(@"insert into {0} (part_sku, part_cost, store_quantity, mfp, part_name, luc_sku) values 
            //                    ( '{1}', '{2}', '{3}', '{4}', '{5}', '{6}')", table_name
            //                                                             , dr["ltd_sku"].ToString().Trim()
            //                                                             , dr["ltd_cost"].ToString() == "" ? "0" : dr["ltd_cost"].ToString()
            //                                                             , stock
            //                                                             , dr["ltd_manufacture_code"].ToString().Trim()
            //                                                             , dr["ltd_part_name"].ToString().Replace("'", "\\'").Trim()
            //                                                             , luc_sku));

            //                        tspb.Value = i;
            //                    }

            //                }


            //            }

        }

        /// <summary>
        /// 比较结果
        /// </summary>
        public void ViewCompare(Ltd ltd)
        {
            SetStatus(null, "Compare begin.", ltd);
            Helper.Compare.ViewCompare(ltd);
            SetStatus(string.Format(@"{0}\{1}", Application.StartupPath, "_s.html"), "Compare end.", ltd);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ltd"></param>
        /// <param name="filename"></param>
        public bool Run(Ltd ltd, string filename)
        {

            if (!File.Exists(filename))
            {
                SetStatus(null, ltd.ToString() + " File isn't exist.", ltd);
                return false;
            }
            SetStatus(null, "begin", ltd);

            LtdHelper lh = new LtdHelper();
            int ltd_id = lh.LtdHelperValue(ltd);

            string table_name = CreateTable(ltd);
            DataTable luSkuDT = Config.ExecuteDateTable(string.Format("select lu_sku,manufacturer_part_number from tb_other_inc_valid_lu_sku where prodType='{0}' ", "NEW"));

           
            {
                // using (OleDbConnection conn = new OleDbConnection(Config.ExcelConnstring(filename)))
                {
                    //conn.Open();
                    //OleDbDataAdapter da = new OleDbDataAdapter(@"select * FROM [table$]", conn);
                    //DataTable dt = new DataTable();
                    //da.Fill(dt);
                    //conn.Close();


                    DataTable dt = Util.HSSFExcel.ToDataTable(filename);
                    
                    Watch.LU LU = new LU();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow dr = dt.Rows[i];//.GetRow(i);

                        int luc_sku = LU.GetSKUByltdSku(dr["ltd_sku"].ToString(), ltd_id);

                        if (luc_sku == 0)
                        {
                            luc_sku = LU.GetSKUByMfp(dr["ltd_sku"].ToString(), luSkuDT);

                            if (luc_sku != 0)
                            {

                                Config.ExecuteDateTable(string.Format(@"insert into tb_other_inc_match_lu_sku (lu_sku , other_inc_sku, other_inc_type) values 
                                        ('{0}', '{1}', '{2}')", luc_sku, dr["ltd_sku"].ToString().Trim(), ltd_id));

                                //
                                // save Match LU SKU.
                                // 
                                //if (luc_sku > 0)
                                //{
                                //    Helper.SaveNewMatch snm = new LUComputers.Helper.SaveNewMatch(null, null);
                                //    snm.SaveNewMatchSKU(ltd_id, dr["ltd_sku"].ToString().Trim(), luc_sku);
                                //}
                            }

                        }
                        SetStatus(null, dr["ltd_sku"].ToString(), ltd);
                        string stock_str = dr["ltd_stock"].ToString().Trim();
                        if (stock_str != "")
                        {
                            if (ltd == Ltd.wholesaler_d2a ||
                                ltd == Ltd.wholesaler_MMAX)
                            {
                                Config.ExecuteNonQuery(string.Format(@"insert into {0} (part_sku, part_cost, store_quantity, mfp, part_name, luc_sku) values 
                    ( '{1}', '{2}', '{3}', '{4}', '{5}', '{6}')", table_name
                                                                     , dr["ltd_sku"].ToString().Trim()
                                                                     , dr["ltd_cost"].ToString() == "" ? "0" : dr["ltd_cost"].ToString()
                                                                     , dr["ltd_cost"].ToString() == "" ? "0" : stock_str
                                                                     , dr["ltd_sku"].ToString().Trim()
                                                                     , dr["ltd_sku"].ToString().Trim()
                                                                     , luc_sku));

                            }
                            else
                            {
                                Config.ExecuteNonQuery(string.Format(@"insert into {0} (part_sku, part_cost, store_quantity, mfp, part_name, luc_sku) values 
                    ( '{1}', '{2}', '{3}', '{4}', '{5}', '{6}')", table_name
                                                                    , dr["ltd_sku"].ToString().Trim()
                                                                    , dr["ltd_cost"].ToString() == "" ? "0" : dr["ltd_cost"].ToString()
                                                                    , stock_str
                                                                    , dr["ltd_sku"].ToString().Trim()
                                                                    , dr["ltd_part_name"].ToString().Replace("'", "\\'").Trim()
                                                                    , luc_sku));
                            }
                        }
                    }
                }
            }


            SetStatus(null, "Delete Old Cost::tb_other_inc_part_info;", ltd);
            Config.ExecuteNonQuery("delete from tb_other_inc_part_info where other_inc_id='" + ltd_id.ToString() + "'");
            SetStatus(null, "Update::tb_other_inc_part_info;", ltd);
            Config.ExecuteNonQuery(string.Format(@"insert into tb_other_inc_part_info 
	(luc_sku, other_inc_id, other_inc_sku, manufacture_part_number, 
	other_inc_price, 
	other_inc_store_sum, 
	tag, 	 
	last_regdate
	)
select luc_sku, {1}, part_sku, mfp, part_cost, store_quantity, 1, now() from {0}", table_name, ltd_id));
            SetStatus(null, "Update End::tb_other_inc_part_info;", ltd);

            return true;
        }


        public string CreateTable(Ltd ltd)
        {
            LtdHelper LH = new LtdHelper();
            DataTable dt = Config.ExecuteDateTable("show tables like '%" + (LH.FilterText(ltd.ToString())) + "_part_%'");
            for (int i = 0; i < dt.Rows.Count - G.SaveTableCount; i++)
            {
                Config.ExecuteNonQuery("drop table " + dt.Rows[i][0].ToString() + ";");
            }

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
                    `part_sku` varchar(50) default NULL,        
                    `part_cost` decimal(8,2) default NULL,            
                    `store_quantity` int(6) default 0,    
                    `mfp` varchar(50) default NULL,  
		            `part_name` varchar(120) default Null,
                    `regdate` timestamp NULL default CURRENT_TIMESTAMP,              
                    PRIMARY KEY  (`serial_no`)         
                    ) ENGINE=MyISAM DEFAULT CHARSET=latin1           

");
            return talbe_name;
        }
    }
}
