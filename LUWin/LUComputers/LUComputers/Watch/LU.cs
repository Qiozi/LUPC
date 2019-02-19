using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.OleDb;
using LUComputers.DBProvider;

namespace LUComputers.Watch
{
    public class LU
    {
        public LU() { }

        public string CreateTable()
        {
            LtdHelper LH = new LtdHelper();
            int ltd_id = LH.LtdHelperValue(Ltd.lu);
            string date_short_name = DateTime.Now.ToString("yyyyMMdd");
            string talbe_name = "store_LU_part_" + date_short_name;

            DataTable dt = Config.ExecuteDateTable("show tables like '%store_LU_part_%'");
            for (int i = 0; i < dt.Rows.Count - 30; i++)
            {
                Config.ExecuteNonQuery("drop table " + dt.Rows[i][0].ToString() + ";");
            }
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
                             `id` int(6) NOT NULL auto_increment,                  
                             `luc_sku` int(6) default NULL,                         
                             `manufacturer_part_number` varchar(50) default NULL,  
                             `is_valid` tinyint(1) default '1',                    
                             `is_ncix_remain` tinyint(1) default '0',              
                             `price` decimal(8,2) default NULL,                    
                             `cost` decimal(8,2) default NULL,                     
                             `discount` decimal(8,2) default NULL,                 
                             `ltd_stock` int(6) default NULL,                      
                             `menu_child_serial_no` int(6) default NULL,
                             `brand` varchar(30) default Null,
                             `regdate` timestamp NULL default CURRENT_TIMESTAMP,   
                             `adjustment` decimal(8,2) default '0',
                             `prodType` varchar(20) default 'NEW',
                             PRIMARY KEY  (`id`)      
                  ) ENGINE=InnoDB DEFAULT CHARSET=latin1  ; 
");
            return talbe_name;
        }

        /// <summary>
        /// 获取产品SKU
        /// </summary>
        /// <param name="mfp"></param>
        /// <param name="prodType"></param>
        /// <returns></returns>
        public static int GetSKUByMfp(string mfp, DataTable dts)
        {

            string sku = "";
            try
            {
                //DataTable dts = Config.ExecuteDateTable(string.Format("select lu_sku from tb_other_inc_valid_lu_sku where manufacturer_part_number='{0}' and prodType='{1}' order by lu_sku desc limit 0,1", mfp, prodType));
                //MessageBox.Show(string.Format("select lu_sku from tb_other_inc_valid_lu_sku where manufacturer_part_number='{0}' order by lu_sku desc limit 0,1", mfp));
                foreach (DataRow dr in dts.Rows)
                {
                    if (dr["manufacturer_part_number"].ToString() == mfp && !string.IsNullOrEmpty(mfp))
                    {
                        return int.Parse(dr["lu_sku"].ToString());
                    }
                }
            }
            catch (Exception ex) { Helper.Logs.WriteErrorLog(ex); }

            return 0;
        }

        /// <summary>
        /// get a mfp by lu sku.
        /// </summary>
        /// <param name="luc_sku"></param>
        /// <returns></returns>
        public static string GetMfp(int luc_sku)
        {
            DataTable dt = Config.ExecuteDateTable("Select manufacturer_part_number mfp from tb_other_inc_valid_lu_sku where lu_sku='" + luc_sku + "'");
            if (dt.Rows.Count > 0)
                return dt.Rows[0][0].ToString();
            return "";
        }
        /// <summary>
        /// 获取产品SKU
        /// </summary>
        /// <param name="mfp">MFP#</param>
        /// <returns></returns>

        public static int GetSKUByltdSku(string ltd_sku, int ltd_id)
        {
            return GetSKUByltdSku(ltd_sku, ltd_id, "NEW");
        }
        /// <summary>
        /// 按其他公司SKU取得LU SKU。
        /// </summary>
        /// <param name="ltd_sku"></param>
        /// <param name="ltd_id"></param>
        /// <param name="prodType"></param>
        /// <returns></returns>
        public static int GetSKUByltdSku(string ltd_sku, int ltd_id, string prodType)
        {
            string sku = "";
            try
            {
                if (ltd_sku.Trim().Length == 0)
                    return 0;
                DataTable dts = Config.ExecuteDateTable(string.Format("select lu_sku from tb_other_inc_match_lu_sku where other_inc_type='{0}' and other_inc_sku='{1}' and prodType='{2}' order by id desc "
                , ltd_id
                , ltd_sku
                , prodType));
                if (dts.Rows.Count > 0)
                {
                    sku = dts.Rows[0][0].ToString();
                }
                else
                    return 0;
            }
            catch (Exception ex) { Helper.Logs.WriteErrorLog(ex); }

            int lu_sku;
            int.TryParse(sku, out lu_sku);
            return lu_sku;
        }

        public bool BindPriceAndLtd()
        {
            //
            // load db from remote mysql
            // 
            DataTable dt = Config.RemoteExecuteDateTable(@"
select 	id, bind_type, category_id, manufactory, priority, other_inc_id, luc_sku, is_single, is_relating	 
	from 
	tb_other_inc_bind_price");

            //
            // delete db and insert db from load mysql
            //
            Config.ExecuteNonQuery("Delete from tb_other_inc_bind_price");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                Config.ExecuteNonQuery(string.Format(@"
insert into tb_other_inc_bind_price(id, bind_type, category_id, manufactory, priority, other_inc_id, luc_sku, is_single, is_relating) values 
('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}') "
                    , dr["id"].ToString()
                    , dr["bind_type"].ToString()
                    , dr["category_id"].ToString()
                    , dr["manufactory"].ToString()
                    , dr["priority"].ToString()
                    , dr["other_inc_id"].ToString()
                    , dr["luc_sku"].ToString()
                    , dr["is_single"].ToString()
                    , dr["is_relation"].ToString()
                    ));
            }

            Config.ExecuteNonQuery(@"update tb_other_inc_valid_lu_sku set 
is_changed_price = 0, other_inc_id=0;

update tb_other_inc_valid_lu_sku o, tb_other_inc_bind_price op set 
o.other_inc_id = op.other_inc_id where manufactory<>'' and bind_type=2 and is_relating =1 
and o.menu_child_serial_no = op.category_id 
and o.brand = op.manufactory;

update tb_other_inc_valid_lu_sku o, tb_other_inc_bind_price op set 
o.other_inc_id = op.other_inc_id where bind_type=1 and is_relating =1 
and o.menu_child_serial_no = op.category_id 
and o.other_inc_id = 0;");

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ltds"></param>
        /// <param name="luc_sku"></param>
        /// <returns></returns>
        public bool ExecStock(string[] ltds, int luc_sku)
        {
            int max_count = 0;
            int min_count = 0;

            for (int i = 0; i < ltds.Length; i++)
            {
                string tableName = ltds[i];
                if (tableName.Length > 5)
                {
                    DataTable dt = new DataTable();
                    if (tableName.ToLower().IndexOf("asi") != -1
                        || tableName.ToLower().IndexOf("dandh") != -1
                        || tableName.ToLower().IndexOf("synnex") != -1)
                    {
                        dt = Config.ExecuteDateTable(string.Format("Select {0} from {1}  where luc_sku='{2}'  and date_format(now(), '%Y%j')- date_format(regdate, '%Y%j')<15", "quantity", tableName, luc_sku));

                        // int quantity = 0;
                        foreach (DataRow sdr in dt.Rows)
                        {
                            //if (dt.Rows.Count > 0)
                            {
                                int qty;
                                int.TryParse(sdr[0].ToString(), out qty);

                                if (qty > 0)
                                    max_count += qty;
                                else if (qty < 0)
                                    min_count += qty;
                            }
                        }
                    }
                    else
                    {
                        dt = Config.ExecuteDateTable(string.Format("Select {0} from {1}  where luc_sku='{2}' and date_format(now(), '%Y%j')- date_format(regdate, '%Y%j')<15", "store_quantity", tableName, luc_sku));
                        if (dt.Rows.Count > 0)
                        {
                            int quantity;
                            int.TryParse(dt.Rows[0][0].ToString(), out quantity);

                            if (quantity > 0)
                                max_count += quantity;
                            else if (quantity < 0)
                                min_count += quantity;
                        }
                    }
                }
            }

            if (max_count > 0)
            {
                Config.ExecuteNonQuery("Update tb_other_inc_valid_lu_sku Set ltd_stock='" + max_count.ToString() + "' where lu_sku = '" + luc_sku.ToString() + "'");
            }
            else if (min_count < 0)
            {
                Config.ExecuteNonQuery("Update tb_other_inc_valid_lu_sku Set ltd_stock='" + min_count.ToString() + "' where lu_sku = '" + luc_sku.ToString() + "'");
            }
            else
            {
                Config.ExecuteNonQuery("Update tb_other_inc_valid_lu_sku Set ltd_stock='0' where lu_sku = '" + luc_sku.ToString() + "'");
            }
            return true;
        }

        public int ChangeNotebookPrice()
        {
            return Config.RemoteExecuteNonQuery(@"
update tb_product p, tb_supercom_notebook s  set 
p.product_current_price = (case 
when (s.cost+p.adjustment) >2200 then round((s.cost+p.adjustment + 120) * 1.022)-0.01
when (s.cost+p.adjustment) >1800 then round((s.cost+p.adjustment + 45) * 1.022)-0.01
when (s.cost+p.adjustment) >1500 then round((s.cost+p.adjustment + 35) * 1.022)-0.01
when (s.cost+p.adjustment) >1100 then round((s.cost+p.adjustment + 30) * 1.022)-0.01
when (s.cost+p.adjustment) >550  then round((s.cost+p.adjustment + 25) * 1.022)-0.01
when (s.cost+p.adjustment) >450  then round((s.cost+p.adjustment + 15) * 1.022)-0.01
when (s.cost+p.adjustment) >300  then round((s.cost+p.adjustment + 13) * 1.022)-0.01
else  round((s.cost+p.adjustment + 10) * 1.022)-0.01 end )
,p.product_current_cost = s.cost
where 
 p.manufacturer_part_number = s.mfp and s.mfp<>'' and s.mfp is not null and s.cost > 100 and p.product_current_discount<1
and p.product_serial_no not in (select luc_sku from tb_part_not_change_price)
;

update tb_product p, tb_supercom_notebook s  set 
p.product_current_special_cash_price = (product_current_price - product_current_discount)/1.022
where 
 p.manufacturer_part_number = s.mfp and s.mfp<>'' and s.mfp is not null  and s.cost > 100 and p.product_current_discount<1;");
        }

        /// <summary>
        /// 从远程加载零件价格配置
        /// </summary>
        /// <returns></returns>
        public bool LoadPartPriceSettings()
        {
            string tableName = "tb_part_price_change_setting";
            DataTable dt = Config.RemoteExecuteDateTable("Select * from " + tableName);
            Config.ExecuteDateTable("Delete from " + tableName);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                Config.ExecuteNonQuery(string.Format(@"insert into {0} (category_id,cost_min,cost_max,rate,is_percent) 
values ('{1}','{2}','{3}','{4}','{5}')", tableName
                                       , dr["category_id"].ToString()
                                       , dr["cost_min"].ToString()
                                       , dr["cost_max"].ToString()
                                       , dr["rate"].ToString()
                                       , dr["is_percent"].ToString() == "False" ? 0 : 1));
            }
            return true;
        }

        /// <summary>
        /// 删除已纪录不抓取的mfp
        /// </summary>
        /// <returns></returns>
        public void deleteExistNoMatch(string mfp, string prodType)
        {
            Config.ExecuteNonQuery(string.Format(@"
delete from tb_other_inc_supercom_mfp_no_exist where mfp='{0}' and prodType='{1}';
delete from tb_other_inc_shopbot_mfp_no_exist where mfp='{0}' and prodType='{1}';", mfp, prodType));
        }

        /// <summary>
        /// 保存LU价格并返回数据表名称
        /// </summary>
        /// <returns></returns>
        public static string CreateLUPriceTable()
        {
            LtdHelper LH = new LtdHelper();
            int ltd_id = LH.LtdHelperValue(Ltd.lu);
            string date_short_name = DateTime.Now.ToString("yyyyMMdd");
            string talbe_name = "store_LU_new_price_" + date_short_name;

            //
            // create table
            //
            Config.ExecuteNonQuery(@"
 drop table  IF EXISTS `" + talbe_name + @"`;
 CREATE TABLE `" + talbe_name + @"` (    
                             `id` int(6) NOT NULL auto_increment,                  
                             `luc_sku` int(6) default NULL,                         
                             `MFP` varchar(50) default NULL, 
                                `old_special_price` decimal(8,2) default NULL, 
                             `new_special_price` decimal(8,2) default NULL,                    
                             `cost` decimal(8,2) default NULL,                     
                             `adjustment` decimal(8,2) default '0',
                             save_price decimal(8,2) default '0',
                             PRIMARY KEY  (`id`)      
                  ) ENGINE=InnoDB DEFAULT CHARSET=latin1  ; 
");

            return talbe_name;
        }

        /// <summary>
        /// 保存新的产品价格
        /// </summary>
        /// <param name="filename"></param>
        public static void SaveLUNewPrice(string filename)
        {
            if (!File.Exists(filename))
                return;

            DataTable dt = Util.HSSFExcel.ToDataTable(filename);

            string tablename = CreateLUPriceTable();

            foreach (DataRow dr in dt.Rows)
            {
                decimal adjustment = 0M;
                decimal newSpecialPrice = 0M;
                decimal oldSpecialPrice = 0M;
                decimal.TryParse(dr["special_price"].ToString(), out oldSpecialPrice);
                decimal.TryParse(dr["adjust_price"].ToString(), out newSpecialPrice);
                adjustment = newSpecialPrice - oldSpecialPrice;
                if (dr["product_serial_no"].ToString().Trim() == "")
                    continue;
                if ("18180" == dr["product_serial_no"].ToString().Trim())
                {

                }
                //if (newSpecialPrice > 0)
                //{
                //    newSpecialPrice *= 1.022M;
                //    newSpecialPrice = decimal.Parse(newSpecialPrice.ToString("000")) - 0.01M;
                //}

                Config.ExecuteDateTable(string.Format(@"insert into {0} (luc_sku , MFP, old_special_price,new_special_price, cost, adjustment, save_price) values 
                                        ('{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}')"
                    , tablename
                    , dr["product_serial_no"].ToString().Trim()
                    , dr["mfp"].ToString().Trim()
                    , oldSpecialPrice
                    , newSpecialPrice
                    , dr["cost"].ToString().Trim()
                    , adjustment
                    , adjustment));

            }
            NewPriceUpdate(tablename);

        }

        /// <summary>
        /// 上传新的价格
        /// </summary>
        /// <param name="tableName"></param>
        public static void NewPriceUpdate(string tableName)
        {
            EditPriceToRemote EPTR = new EditPriceToRemote();
            DataTable newPriceDT = Config.ExecuteDateTable(string.Format(
                "Select luc_sku, new_special_price from {0} where new_special_price>0", tableName));
            foreach (DataRow dr in newPriceDT.Rows)
            {
                DataTable priceDT = Config.ExecuteDateTable(string.Format(@"select max(round(( price - discount)/1.022, 2))  old_special_price
                from tb_other_inc_valid_lu_sku where lu_sku = {0}", dr["luc_sku"].ToString()));
                if (priceDT.Rows.Count == 1)
                {
                    decimal new_special_price;
                    decimal.TryParse(dr["new_special_price"].ToString(), out new_special_price);

                    decimal old_special_price;
                    decimal.TryParse(priceDT.Rows[0]["old_special_price"].ToString(), out old_special_price);
                    if (new_special_price == 0
                        || old_special_price == 0)
                        continue;


                    decimal diff = new_special_price - old_special_price;
                    if (diff > 1 || diff < -1)
                    {
                        decimal newPrice = EditPriceToRemote.GetNewPrice(new_special_price);
                        decimal adjuest = 0M;
                        // decimal special = EPTR.GetNewSpecial(decimal.Parse(cost), int.Parse(luc_sku), 0);
                        decimal old_price = EditPriceToRemote.GetNewPrice(old_special_price);
                        adjuest = newPrice - old_price;
                        Config.RemoteExecuteNonQuery(string.Format(@"Update tb_product 
                        set product_current_price = {0} + product_current_discount
                        ,last_regdate=now()
                        ,adjustment = {2}
                        ,is_modify = 1 where product_serial_no={1}"
                            , newPrice
                            , dr["luc_sku"].ToString()
                            , adjuest));
                    }
                }
            }
        }


        /// <summary>
        /// 变化网站首页零件显示部份
        /// 
        /// 从销售纪录里，取得卖的最多的产品
        /// </summary>
        public static void ChangeWebIndexPagePart()
        {
            DataTable dt = Config.RemoteExecuteDateTable("select distinct cateid from tb_pre_index_page_setting where cateid >0");
            foreach (DataRow dr in dt.Rows)
            {
                int cateid;
                int.TryParse(dr["cateid"].ToString(), out cateid);

                DataTable sdt = Config.RemoteExecuteDateTable("select id from tb_pre_index_page_setting where cateid=" + cateid);

                DataTable pdt = Config.RemoteExecuteDateTable(string.Format(@"select product_serial_no from (
select distinct t.product_serial_no, max(p.product_ebay_name), count(p.product_serial_no) c , menu_child_serial_no, max(ltd_stock) stock from (
	select op.product_serial_no from tb_order_product op inner join tb_order_helper oh on op.order_code=oh.order_code 
	where op.serial_no>0 and oh.order_helper_serial_no>0 and DATEDIFF(now(), create_datetime) <60

	union all 

	select op.product_serial_no from tb_order_product_sys_detail op 
	inner join tb_order_product opp on opp.product_serial_no=op.sys_tmp_code
	inner join tb_order_helper oh on opp.order_code=oh.order_code 

	where op.sys_tmp_detail>0 and oh.order_helper_serial_no>0 and DATEDIFF(now(), create_datetime) <60
) t inner join tb_product p on t.product_serial_no=p.product_serial_no and p.menu_child_serial_no={0} and ltd_stock>3 group by p.product_serial_no, p.menu_child_serial_no
) tt order by c desc, stock desc limit 6", cateid));

                for (int i = 0; i < sdt.Rows.Count; i++)
                {
                    if (i < pdt.Rows.Count)
                    {
                        Config.RemoteExecuteNonQuery("Update tb_pre_index_page_setting set sku='" + pdt.Rows[i]["product_serial_no"].ToString() + "' where id='" + sdt.Rows[i]["id"].ToString() + "'");
                    }
                }
            }
        }
    }
}
