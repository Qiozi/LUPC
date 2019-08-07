using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Data;
using System.Windows.Forms;
using System.IO;
using LUComputers.DBProvider;

namespace LUComputers.Watch
{
    public class DanDh
    {
        #region Event
        public event Events.UrlEventHandler WatchE;

        protected void InvokeWatchE(Events.UrlEventArgs e)
        {
            Events.UrlEventHandler watch = WatchE;
            if (watch != null) watch(this, e);
        }

        void SetStatus(string url, string comment, Ltd ltd, string result)
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
        void SetStatus(string url, string comment)
        {
            SetStatus(url, comment, Ltd.wholesaler_dandh, null);
        }
        public void SetStatus(string result)
        {
            SetStatus(null, null, Ltd.wholesaler_dandh, result);
        }
        #endregion

        public DanDh() { }

        /// <summary>
        /// 比较结果
        /// </summary>
        public void ViewCompare()
        {
            SetStatus(null, "Compare DanDh begin.");
            Helper.Compare.ViewCompare(Ltd.wholesaler_dandh);
            SetStatus(string.Format(@"{0}\{1}", Application.StartupPath, "_s.html"), "Compare DanDh end.");
        }


        //ftp://8018640000:comben@ftp.dandh.com/itemlist
        public bool DanDhWatch(string filename, bool IsSaveAll) {

            if (!System.IO.File.Exists(filename))
            {
                SetStatus(null, " DanDh file isn't exist.");
                return false;
            }

            LtdHelper lh = new LtdHelper();
            int ltd_id = lh.LtdHelperValue(Ltd.wholesaler_dandh);
            //WebClient webclieng = new WebClient();
            //string new_file = string.Format("{0}DanDh\\DanDh_{1}", Config.soft_download_path, DateTime.Now.ToString("yyyyMMddhhmmss"));
            //webclieng.DownloadFile("ftp://8018640000:comben@ftp.dandh.com/itemlist", new_file);
            SetStatus(null, "Dandh Watch Begin...");
            string table_name = CreateTable();
            string tableNameAll = DBProvider.TableName.DandhAll;
            if(IsSaveAll)
                DBProvider.CreateTable.Table_Dandh(DBProvider.TableName.DandhAll);

//            Config.ExecuteNonQuery(string.Format(@"
//LOAD DATA INFILE '{0}' REPLACE INTO TABLE `{1}` 
//FIELDS TERMINATED BY '|' 
//OPTIONALLY ENCLOSED BY '' 
//ESCAPED BY '\\' 
//LINES TERMINATED BY '\r\n'
//(stock_status,quantity_on_hand,rebate_flag,rebate_end_date,item_number,mfr_item_number,upc_code,subcategory_code,vendor_name,cost,rebate_amount,held_for_future_use,freight,ship_via,weight,short_description,long_description);
//", filename.Replace("\\","\\\\"), table_name));
            string stock_status = "";
            string quantity_on_hand = "";
            string rebate_flag = "";
            string rebate_end_date = "";
            string item_number = "";
            string mfr_item_number = "";
            string upc_code = "";
            string subcategory_code = "";
            string vendor_name = "";
            string cost = "";
            string rebate_amount = "";
            string held_for_future_use = "";
            string freight = "";
            string ship_via = "";
            string weight = "";
            string short_description = "";
            string long_description = "";

            string[] lines = File.ReadAllLines(filename);
            System.Text.StringBuilder sbSQL = new StringBuilder();
            foreach (var s in lines)
            {
                try
                {
                    string[] fs = s.Split(new char[] { '|' });
                    if (fs.Length == 17)
                    {
                        stock_status = fs[0];
                        quantity_on_hand = fs[1];
                        rebate_flag = fs[2];
                        rebate_end_date = fs[3];
                        item_number = fs[4];
                        mfr_item_number = fs[5].Replace("\\", "\\\\").Replace("'", "\\'");
                        upc_code = fs[6];
                        subcategory_code = fs[7];
                        vendor_name = fs[8];
                        cost = fs[9];
                        rebate_amount = fs[10];
                        held_for_future_use = fs[11];
                        freight = fs[12];
                        ship_via = fs[13];
                        weight = fs[14];
                        short_description = fs[15].Replace("'", "\\'");
                        long_description = fs[16].Replace("\\", "\\\\").Replace("'", "\\'");
                        if (long_description.Length > 330)
                            long_description = "";
                        //if (long_description.IndexOf("\xEF\xBF") > -1)
                        {
                         //   long_description = "";
                        }

                        //long_description = "";
                        decimal costDecimal = 0M;
                        decimal.TryParse(cost, out costDecimal);
                        if (costDecimal > 100000M)
                        {
                            continue;
                        }
                        sbSQL.Append(string.Format(@",('{0}','{1}','{2}','{3}','{4}')"
                            , item_number
                            , cost
                            , mfr_item_number
                            , upc_code
                            , quantity_on_hand));
                            

                        if (IsSaveAll)
                        {
                            SetStatus(null, mfr_item_number);
                            Config.ExecuteNonQuery(string.Format(@"
insert into {0}(stock_status,quantity_on_hand,rebate_flag,rebate_end_date,item_number,mfr_item_number,upc_code,subcategory_code,vendor_name,cost,rebate_amount,held_for_future_use,freight,ship_via,weight,short_description,long_description)
values ('{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}')
"
                                , tableNameAll
                                , stock_status
                            , quantity_on_hand
                            , rebate_flag
                            , rebate_end_date
                            , item_number
                            , mfr_item_number
                            , upc_code
                            , subcategory_code
                            , vendor_name
                            , cost
                            , rebate_amount
                            , held_for_future_use
                            , freight
                            , ship_via
                            , weight
                            , short_description
                            , long_description));
                        }
                    }
                }
                catch (Exception ex)
                {
                    Util.Logs.WriteErrorLog(ex);
                }
            }
            Config.ExecuteNonQuery(string.Format(@"
insert into {0} (sku,price,mfp,upc,quantity)
values {1}"
                          , table_name
                          , sbSQL.ToString().Substring(1) + ";"));
            SetStatus(null, "Dandh Watch End.");
//            Config.ExecuteNonQuery(string.Format(@"delete from  tb_other_inc_part_info where other_inc_id='{0}';
//
//insert into tb_other_inc_part_info 
//	( other_inc_sku, other_inc_id, other_inc_store_sum, 
//	manufacture_part_number, 
//	other_inc_price, 
//	regdate, 
//	last_regdate, 
//	tag
//	)
// select item_number,  '{0}', quantity_on_hand,mfr_item_number, cost,now(),now(),1
// from tb_other_inc_dandh oi inner join tb_other_inc_valid_lu_sku p on p.manufacturer_part_number=oi.mfr_item_number and p.manufacturer_part_number<> '' ", ltd_id));

            //Helper.SaveNewMatch hsn = new Helper.SaveNewMatch();
            //hsn.FilterSave(Ltd.wholesalerl_dandh);
            //hsn.UpdateToRemote(Ltd.wholesalerl_dandh); 

            SetStatus(null, "Dandh Match LU SKU.");
            Config.ExecuteNonQuery(string.Format(@"
update {0} a, tb_other_inc_valid_lu_sku b
set a.luc_sku = b.lu_sku where b.manufacturer_part_number=a.mfp and a.mfp<>'';

delete from tb_other_inc_match_lu_sku where other_inc_type=16;

insert into tb_other_inc_match_lu_sku 
	( lu_sku, other_inc_sku, other_inc_type
	)
select luc_sku, mfp, '16' from {0} where luc_sku >0", table_name));

            if (IsSaveAll)
                Config.ExecuteNonQuery(string.Format(@"
update {0} a, tb_other_inc_valid_lu_sku b
set a.luc_sku = b.lu_sku where b.manufacturer_part_number=a.mfr_item_number and a.mfr_item_number<>'';
", tableNameAll));

            //SetStatus(null, "Dandh Notebook....");
            ////
            //// 把HP notebook 加入到notebook DB里
            //// 
            //Config.ExecuteNonQuery("Delete from tb_supercom_notebook where brand like 'HP%'");
            
            
//             HP
//            Config.ExecuteNonQuery(string.Format(@"
//insert into tb_supercom_notebook (mfp, cost, dandh_cost, quantity, ETA, name, brand, category, supercom_SKU, luc_sku, ltd)
//select mfr_item_number, cost,cost, quantity_on_hand, '01/01/9999' f13,case when length(long_description)>300 then concat(left(long_description, 297),'...') else long_description end as long_description, vendor_name, 'Laptops' f9, item_number,luc_sku, 'DanDh' from 
//{0} where subcategory_code in ('315') and vendor_name like 'HP%'"
//                , table_name));
            //
            // ALL
//            Config.ExecuteNonQuery(string.Format(@"
//update tb_supercom_notebook n, {0} s 
//set n.dandh_cost= s.cost
//where n.mfp=s.mfr_item_number and s.mfr_item_number<>'' and length(s.mfr_item_number)>1", table_name));

//            SetStatus(null, "Dandh Notebook..End..");
            Config.ExecuteNonQuery("delete from tb_other_inc_part_info where other_inc_id='" + ltd_id.ToString() + "'");
            
            SetStatus(null, "Update Dandh::tb_other_inc_part_info;");
            Config.ExecuteNonQuery(string.Format(@"insert into tb_other_inc_part_info 
	(luc_sku, other_inc_id, other_inc_sku, manufacture_part_number, 
	other_inc_price, 
	other_inc_store_sum, 
	tag, 	 
	last_regdate
	)
select luc_sku, {1}, sku, mfp, price, quantity, 1, now() from {0}", table_name, ltd_id));
            SetStatus(null, "Update End Dandh::tb_other_inc_part_info;");
            return true;
        }

        public string CreateTable()
        {
            //DataTable dt = Config.ExecuteDateTable("show tables like '%store_danDh_part_%'");

            //for (int i = 0; i < dt.Rows.Count - G.SaveTableCount; i++)
            //{
            //    Config.ExecuteNonQuery("drop table " + dt.Rows[i][0].ToString() + ";");
            //}
            LtdHelper LH = new LtdHelper();
            int ltd_id = LH.LtdHelperValue(Ltd.wholesaler_dandh);
            string date_short_name = DateTime.Now.ToString("yyyyMMdd");
           
            string talbe_name = DBProvider.TableName.GetPriceTableNamePart(new LtdHelper().FilterText(Ltd.wholesaler_dandh.ToString())) + date_short_name;// "store_danDh_part_" + date_short_name;
            DBProvider.CreateTable.PriceTable(talbe_name);
               
//            //
//            // create table
//            //
//            Config.ExecuteNonQuery(@"
// drop table  IF EXISTS `" + talbe_name + @"`;
//
// CREATE TABLE `" + talbe_name + @"` (              
//                      `stock_status` varchar(5) default NULL,        
//                      `quantity_on_hand` int(3) default NULL,        
//                      `rebate_flag` varchar(5) default NULL,         
//                      `rebate_end_date` varchar(12) default NULL,    
//                      `item_number` varchar(30) default NULL,        
//                      `mfr_item_number` varchar(30) default NULL,    
//                      `upc_code` varchar(30) default NULL,           
//                      `subcategory_code` int(6) default NULL,        
//                      `vendor_name` varchar(50) default NULL,        
//                      `cost` decimal(8,2) default NULL,              
//                      `rebate_amount` decimal(8,2) default NULL,     
//                      `held_for_future_use` int(6) default NULL,     
//                      `freight` decimal(5,2) default NULL,           
//                      `ship_via` varchar(5) default NULL,            
//                      `weight` decimal(8,2) default NULL,            
//                      `short_description` varchar(150) default NULL,  
//                      `long_description` varchar(350) default NULL,
//                      `luc_sku` int(6) default NULL,
//                      `regdate` timestamp NULL default CURRENT_TIMESTAMP           
//                    ) ENGINE=InnoDB DEFAULT CHARSET=latin1           
//
//");
            return talbe_name;
        }

        /// <summary>
        /// DanDH Notebook
        /// </summary>
        public void UpdataRemote()
        { 
            //
            // 附加DanDh
            string DanDh_table_name = new LtdHelper().GetLastStoreTableName(Ltd.wholesaler_dandh);

            DataTable dt = Config.ExecuteDateTable(string.Format(@"   
select mfr_item_number, cost, quantity_on_hand, '01/01/9999' f13,long_description, vendor_name, 'Laptops' f9, item_number,luc_sku from 
{0} where subcategory_code in ('315') and vendor_name like 'HP%'", DanDh_table_name));
            System.Text.StringBuilder sb = new StringBuilder();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i == 0)
                    Config.RemoteExecuteNonQuery("delete from tb_supercom_notebook where brand like 'hp%'");
                DataRow dr = dt.Rows[i];
                if (i == dt.Rows.Count - 1)
                {
                    sb.Append(string.Format(@"('{0}','{1}','{2}','{3}','{4}', '{5}','{6}','{7}','{8}');"
                        , dr["mfr_item_number"].ToString()
                        , dr["cost"].ToString()
                        , dr["quantity_on_hand"].ToString()
                        , dr["f13"].ToString()
                         , dr["long_description"].ToString().Replace("'", "\'")
                          , dr["vendor_name"].ToString()
                           , dr["f9"].ToString()
                           , dr["item_number"].ToString()
                            , dr["luc_sku"].ToString()
                        ));
                }
                else
                {
                    sb.Append(string.Format(@"('{0}','{1}','{2}','{3}','{4}', '{5}','{6}','{7}','{8}'),"
                        , dr["mfr_item_number"].ToString()
                        , dr["cost"].ToString()
                        , dr["quantity_on_hand"].ToString()
                        , dr["f13"].ToString()
                         , dr["long_description"].ToString().Replace("'", "\'")
                          , dr["vendor_name"].ToString()
                           , dr["f9"].ToString()
                           , dr["item_number"].ToString()
                            , dr["luc_sku"].ToString()
                        ));
                }

            }
            if (sb.ToString().Length > 10)
            {
                string sql = "insert into tb_supercom_notebook (mfp, cost, quantity, ETA, name, brand, category, supercom_SKU,luc_sku) values";
                Config.RemoteExecuteNonQuery(sql + sb.ToString().Substring(0, sb.ToString().Length - 1) + ";");
                sb = new StringBuilder();
            }
        }
    }
}
