using LUComputers.DBProvider;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Xml;
namespace LUComputers
{
    public class ValidLuSku
    {
        EditPriceToRemote EPTR = new EditPriceToRemote();
        public bool OK1 = false;
        public bool OK2 = false;
        public ValidLuSku() { }

        public override string ToString()
        {
            HttpHelper hh = new HttpHelper();
            return hh.HttpGet(string.Format("{0}q_admin/netcmd/export_other_inc_sku.aspx?cmd=qiozi@msn.com", Config.http_url));
        }

        public string GetValidLuSkuAndManufacture()
        {
            HttpHelper hh = new HttpHelper();
            return hh.HttpGet(string.Format("{0}q_admin/netcmd/export_valid_lu_sku_and_manufacture.aspx?cmd=qiozi@msn.com", Config.http_url));
        }

        public event Events.UrlEventHandler WatchE;

        protected void InvokeWatchE(Events.UrlEventArgs e)
        {
            Events.UrlEventHandler watch = WatchE;
            if (watch != null) watch(this, e);
        }

        #region setStatus
        public void SetStatus(string url, string comment, string result)
        {
            LUComputers.Events.UrlEventArgs ua = new LUComputers.Events.UrlEventArgs(new Events.UrlEventModel()
            {
                comment = comment,
                url = url,
                ltd = Ltd.lu,
                result = result
            });
            InvokeWatchE(ua);
        }
        public void SetStatus(string url, string comment)
        {
            SetStatus(url, comment, null);
        }
        public void SetStatus(string result)
        {
            SetStatus(null, null, result);
        }
        #endregion

        string GetLtdChangeSKUSql(Ltd[] ltds)
        {

            StringBuilder sb = new StringBuilder();
            List<string> sqls = new List<string>();

            foreach (var ltd in ltds)
            {
                LtdHelper LH = new LtdHelper();
                int ltd_id = LH.LtdHelperValue(ltd);

                string index_fields_name = "";
                string cost_fields_name = "";
                string quantity_fields_name = "";
                new Watch.ComparePrice().CompareDBFields(ltd, ref index_fields_name, ref cost_fields_name, ref quantity_fields_name);

                if (index_fields_name == "" || cost_fields_name == "")
                {
                    SetStatus(null, ltd.ToString() + " no Index Fields............");
                    continue;
                }

                DataTable tnDT = Config.ExecuteDateTable(string.Format(@"select db_table_name from tb_other_inc_run_date  where other_inc_id='{0}' order by id desc limit 0,2", ltd_id));
                if (tnDT.Rows.Count == 2)
                {
                    string table_name1 = tnDT.Rows[0][0].ToString();
                    string table_name2 = tnDT.Rows[1][0].ToString();

                    sqls.Add(string.Format(@"
select t2.luc_sku 
from {0} t1 inner join {1} t2
on  t1.{2}=t2.{2} where t1.{3}<>t2.{3}
"
                        , table_name2
                        , table_name1
                        , index_fields_name
                        , cost_fields_name));
                }
            }
            for (int i = 0; i < sqls.Count; i++)
            {
                if (i == 0)
                {
                    sb.Append("select distinct luc_sku from (" + sqls[i]);
                }
                else
                    sb.Append(@"
union all
" + sqls[i]);

            }
            return sb.Append(") t").ToString();

        }

        /// <summary>
        /// 取得最优的价格
        /// </summary>
        /// <returns></returns>
        public bool MatchBasePrice(int page)
        {
            ltd_infoEntities1 context = new ltd_infoEntities1();

            string ltds = new LtdHelper().GetLtdIds(new Ltd[]{Ltd.wholesaler_asi
                , Ltd.wholesaler_dandh
                , Ltd.wholesaler_Synnex
                , Ltd.wholesaler_d2a
            });

            var prodType = "New";

            DataTable dt = Config.ExecuteDateTable(string.Format(@"Select lu_sku, menu_child_serial_no from tb_other_inc_valid_lu_sku 
where isOk=0 and lu_sku not in (select luc_sku from tb_dont_update) and prodType='" + prodType + "'"));
            int recordCount = dt.Rows.Count;
            int splitInt = recordCount / 2 + 1;
            var lucSKUs = new List<SkuCate>();
            if (page == 1)
            {
                for (int i = 0; i < splitInt; i++)
                {
                    lucSKUs.Add(new SkuCate() { SKU = dt.Rows[i][0].ToString(), CategoryID = dt.Rows[i]["menu_child_serial_no"].ToString() });
                }
            }
            else
            {
                for (int i = splitInt; i < recordCount; i++)
                {
                    lucSKUs.Add(new SkuCate() { SKU = dt.Rows[i][0].ToString(), CategoryID = dt.Rows[i]["menu_child_serial_no"].ToString() });
                }
            }


            var subDT1 = Config.ExecuteDateTable(string.Format(@"
                                select other_inc_id, other_inc_price, other_inc_store_sum, regdate ,luc_sku
                                from tb_other_inc_part_info 
                                where tag=1 and other_inc_price>0 and other_inc_id in ({0})
                                and prodType='" + prodType + @"'
                                and date_format(now(), '%Y%j')- date_format(regdate, '%Y%j')<15
                                order by other_inc_price asc 
                                ", ltds));

            var subDTList = new List<tb_other_inc_part_info>();
            foreach (DataRow dr in subDT1.Rows)
            {
                subDTList.Add(new tb_other_inc_part_info
                {
                    luc_sku = int.Parse(dr["luc_sku"].ToString()),
                    other_inc_id = int.Parse(dr["other_inc_id"].ToString()),
                    other_inc_store_sum = int.Parse(dr["other_inc_store_sum"].ToString()),
                    regdate = DateTime.Parse(dr["regdate"].ToString()),
                    other_inc_price = decimal.Parse(dr["other_inc_price"].ToString())
                });
            }

            int allCount = lucSKUs.Count;
            int n = 0;
            foreach (var s in lucSKUs)
            {
                try
                {
                    n += 1;
                    var luc_sku = int.Parse(s.SKU);
                    SetStatus(null, string.Format("{0}:: {1} of {2}::page={3}",
                        luc_sku
                        , n
                        , allCount
                        , page.ToString()));

                    //DataTable subDT = Config.ExecuteDateTable(string.Format(@"
                    //            select other_inc_id, other_inc_price, other_inc_store_sum, regdate 
                    //            from tb_other_inc_part_info 
                    //            where luc_sku = '{0}' and tag=1 and other_inc_price>0 and other_inc_id in ({1})
                    //            and prodType='"+ prodType + @"'
                    //            and date_format(now(), '%Y%j')- date_format(regdate, '%Y%j')<15
                    //            order by other_inc_price asc 
                    //            ", luc_sku, ltds));
                    var subDT = subDTList.Where(me => me.luc_sku.HasValue &&
                                                me.luc_sku.Value.Equals(luc_sku))
                                                .OrderBy(me => me.other_inc_price).ToList();
                    if (subDT.Count > 0)
                    {
                        //if (dr["luc_sku"].ToString() == luc_sku.ToString())
                        {
                            string ltd_id = "";
                            string regdate = "";
                            decimal cost = 0M;
                            int quantity = 0;
                            if (subDT.Count == 1)
                            {
                                UpdateLtdCost(subDT[0].other_inc_id.ToString()
                                    , subDT[0].other_inc_price.ToString()
                                    , subDT[0].regdate.ToString()
                                    , subDT[0].other_inc_store_sum.ToString()
                                    , luc_sku.ToString());
                            }
                            else
                            {


                                // foreach (DataRow sdr in subDT.Rows)
                                foreach (var sdr in subDT)
                                {
                                    ltd_id = sdr.other_inc_id.ToString();
                                    regdate = sdr.regdate.ToString();

                                    cost = sdr.other_inc_price.HasValue
                                                ? sdr.other_inc_price.Value
                                                : 0M;
                                    // decimal.TryParse(sdr.other_inc_price.ToString(), out cost);
                                    // int.TryParse(sdr["other_inc_store_sum"].ToString(), out quantity);
                                    quantity = sdr.other_inc_store_sum.HasValue
                                                ? sdr.other_inc_store_sum.Value
                                                : 0;

                                    if (quantity > 0)
                                    {
                                        break;
                                    }
                                }

                                UpdateLtdCost(ltd_id, cost.ToString(), regdate, quantity.ToString(), luc_sku.ToString());
                            }
                        }
                    }


                }
                catch (Exception ex) { Helper.Logs.WriteErrorLog(ex); }

                Config.ExecuteNonQuery("Update tb_other_inc_valid_lu_sku set isOk='1' where lu_sku='" + s.SKU + "'");
            }
            return true;
        }

        /// <summary>
        /// 更新到远程服务器
        /// </summary>
        public void UpdatePriceToRemote()
        {
            int page_size = 100;
            Config.ExecuteNonQuery(@"update tb_other_inc_valid_lu_sku set price = price + 1");
            int recordCount = Config.ExecuteScalarInt(@"Select count(*) from tb_other_inc_valid_lu_sku
where curr_change_price > 0  and curr_change_price <> price - discount");

            int pageCount = ((recordCount / page_size) + (recordCount % page_size > 0 ? 1 : 0));

            Config.RemoteExecuteNonQuery("Delete from tb_product_price_tmp");   // 清空远程数据表
            for (int i = 0; i < pageCount; i++)
            {
                DataTable dt = Config.ExecuteDateTable(string.Format(@"select lu_sku
,curr_change_cost
,curr_change_price
,other_inc_name curr_change_ltd
,curr_change_quantity
,curr_change_regdate
,ifnull(vc.categoryid, 0) isChange
from tb_other_inc_valid_lu_sku vls inner join tb_other_inc oi on oi.id=vls.curr_change_ltd
left join tb_valid_category vc on vc.categoryid=vls.menu_child_serial_no
where curr_change_price > 0 and curr_change_price <> price - discount limit {0},{1}", i * page_size, page_size));

                System.Text.StringBuilder sb = new StringBuilder();
                sb.Append(string.Format(@" insert into tb_product_price_tmp 
	                                        ( curr_change_cost, curr_change_price, curr_change_ltd, 
	                                        curr_change_quantity, 
	                                        curr_change_regdate, 
	                                        luc_sku,
                                            is_auto_change
	                                        ) values                                      
 "));
                foreach (DataRow dr in dt.Rows)
                {
                    sb.Append(string.Format(@" ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}'),"
                       , dr["curr_change_cost"].ToString()
                       , dr["curr_change_price"].ToString()
                       , dr["curr_change_ltd"].ToString()
                       , dr["curr_change_quantity"].ToString()
                       , dr["curr_change_regdate"].ToString()
                       , dr["lu_sku"].ToString()
                       , dr["isChange"].ToString() == "0" ? 0 : 1));
                }
                SetStatus(null, "page: " + (i + 1).ToString() + "/" + pageCount.ToString());
                string sql = sb.ToString().TrimEnd(new char[] { ',' }) + ";";
                Config.RemoteExecuteNonQuery(sql);
            }

            //
            // 更新到产品表

            // 变化价格
            Config.RemoteExecuteNonQuery(@"update tb_product p , tb_product_price_tmp pt set
p.curr_change_cost = pt.curr_change_cost
, p.curr_change_price=pt.curr_change_price
, p.curr_change_ltd=pt.curr_change_ltd
, p.curr_change_quantity = pt.curr_change_quantity
, p.curr_change_regdate=pt.curr_change_regdate
, p.last_regdate=now()
, p.is_modify = 1
, p.product_current_discount=0
where p.product_serial_no=pt.luc_sku and pt.is_auto_change=1;
delete from tb_on_sale where product_serial_no in (select luc_sku from tb_product_price_tmp);
");
            Config.RemoteExecuteNonQuery(@"update tb_product set product_current_cost = curr_change_cost,
product_current_price = curr_change_price + product_current_discount where curr_change_price > 0.5 and date_format(last_regdate, '%j')=date_format(now(),'%j')");


            // 只变化cost
            Config.RemoteExecuteNonQuery(@"update tb_product p , tb_product_price_tmp pt set
p.curr_change_cost = pt.curr_change_cost
, p.curr_change_price=pt.curr_change_price
, p.curr_change_ltd=pt.curr_change_ltd
, p.curr_change_quantity = pt.curr_change_quantity
, p.curr_change_regdate=pt.curr_change_regdate
, p.last_regdate=now()
, p.is_modify = 1
where p.product_serial_no=pt.luc_sku and pt.is_auto_change=0
");

            Config.RemoteExecuteNonQuery(@"update tb_product set product_current_cost = curr_change_cost
where curr_change_price > 0.5 and date_format(last_regdate, '%j')=date_format(now(),'%j')");

            ChangeConnPartPrice();
        }

        /// <summary>
        /// 更新远程关联产品价格
        /// </summary>
        public void ChangeConnPartPrice()
        {
            DataTable dt = Config.RemoteExecuteDateTable("Select product_serial_no, price_sku, price_sku_quantity, menu_child_serial_no from tb_product where tag=1 and price_sku>0");

            foreach (DataRow dr in dt.Rows)
            {
                SetStatus(null, "connection price..." + dr["product_serial_no"].ToString());
                int quantity;
                int.TryParse(dr["price_sku_quantity"].ToString(), out quantity);

                DataTable subDT = Config.RemoteExecuteDateTable("Select product_current_cost, tag from tb_product where product_serial_no ='" + dr["price_sku"].ToString() + "'");
                if (subDT.Rows.Count == 1)
                {
                    int tag;
                    int.TryParse(subDT.Rows[0]["tag"].ToString(), out tag);
                    if (tag == 0)
                    {
                        Config.RemoteExecuteNonQuery("Update tb_product set tag=0 where product_serial_no='" + dr["product_serial_no"].ToString() + "'");

                    }
                    else
                    {
                        decimal cost;
                        decimal.TryParse(subDT.Rows[0]["product_current_cost"].ToString(), out cost);

                        decimal price;
                        //decimal.TryParse(subDT.Rows[0]["price"].ToString(), out price);

                        cost = cost * decimal.Parse(quantity.ToString());
                        if (cost > 0M)
                        {
                            price = EditPriceToRemote.GetNewPrice(EditPriceToRemote.GetNewSpecial(cost, 0, int.Parse(dr["menu_child_serial_no"].ToString())));
                            if (price > 0M)
                                Config.RemoteExecuteNonQuery(string.Format(@"Update tb_product set product_current_cost ='{0}'
, product_current_price='{1}', product_current_discount=0 where product_serial_no='{2}'"
                                    , cost
                                    , price
                                    , dr["product_serial_no"].ToString()));
                        }
                    }
                }

            }
        }

        void UpdateLtdCost(string ltdID, string cost, string regdate, string quantity, string luc_sku)
        {

            decimal special = EditPriceToRemote.GetNewSpecial(decimal.Parse(cost), int.Parse(luc_sku), 0);
            decimal new_price = EditPriceToRemote.GetNewPrice(special);

            Config.ExecuteNonQuery(string.Format(@"Update tb_other_inc_valid_lu_sku set 
                curr_change_cost='{0}',
                curr_change_price='{1}',
                curr_change_ltd='{2}',
                curr_change_quantity='{3}',
                curr_change_regdate='{4}' where lu_sku='{5}'"
                , cost
                , new_price
                , ltdID
                , quantity
                , regdate
                , luc_sku));
        }
    }

    public class SkuCate
    {
        public string SKU { get; set; }
        public string CategoryID { set; get; }

        public SkuCate() { }
    }
}
