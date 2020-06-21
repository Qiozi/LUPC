using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO;
using System.Windows.Forms;
using LUComputers.DBProvider;

namespace LUComputers.Watch
{
    public class Synnex
    {
        public event Events.UrlEventHandler WatchE;

        protected void InvokeWatchE(Events.UrlEventArgs e)
        {
            Events.UrlEventHandler watch = WatchE;
            if (watch != null) watch(this, e);
        }

        public void SetStatus(string url, string comment, string result)
        {
            LUComputers.Events.UrlEventArgs ua = new LUComputers.Events.UrlEventArgs(new Events.UrlEventModel()
            {
                comment = comment,
                url = url,
                ltd = Ltd.wholesaler_Synnex,
                result = result
            });
            InvokeWatchE(ua);
        }

        public void SetStatus(string url, string comment)
        {
            SetStatus(url, comment, null);
        }


        public Synnex() { }

        /// <summary>
        /// 比较结果
        /// </summary>
        public void ViewCompare()
        {
            SetStatus(null, "Compare Synnex begin.");
            Helper.Compare.ViewCompare(Ltd.wholesaler_Synnex);
            SetStatus(string.Format(@"{0}\{1}", Application.StartupPath, "_s.html"), "Compare Synnex end.");
        }
        /// <summary>
        /// 把文件内存保存到数据库并执行相关操作
        /// </summary>
        /// <param name="fullFilename"></param>
        /// <returns></returns>
        public bool WatchRun(string fullFilename, bool IsSaveAll)
        {
            if (!File.Exists(fullFilename))
            {
                SetStatus(null, "Synnex File isn't exist.");
                return false;
            }

            string tableNameAll = DBProvider.TableName.SynnexAll;
            if (IsSaveAll)
                DBProvider.CreateTable.Table_Synnex(tableNameAll);
            Config.ExecuteNonQuery("delete from tb_synnex_all_discount");

            LtdHelper lh = new LtdHelper();
            int ltd_id = lh.LtdHelperValue(Ltd.wholesaler_Synnex);

            string table_name = CreateTable(Ltd.wholesaler_Synnex);

            string mfp = "";
            string part_sku = "";
            string status_code = "";
            string mfp_name = "";
            int store_quantity = 0;
            //int store_quantity2 = 0;
            string part_cost = "";
            string msrp = "";
            string name = "";
            string UPC = "";
            string ship_weight = "";
            string cat_code = "";
            //string ETA = "";
            string promotion_expiration_date = "";
            string promotion_Status = "";
            StreamReader sr = new StreamReader(fullFilename);
            string cont = sr.ReadToEnd();
            sr.Close();
            // cont = cont.Replace("1151315~DTL~", "|");
            string[] lines = cont.Split(new string[] { "1151315~DTL~" }, StringSplitOptions.RemoveEmptyEntries);

            int pageSize = 10000;
            int count = 0;
            string sql_part = "";

            DataTable validDT = Config.ExecuteDateTable("select distinct lu_sku, manufacturer_part_number mfp from tb_other_inc_valid_lu_sku");
            #region all info
            Config.ExecuteNonQuery("Delete from tb_discount_end_date");

            if (IsSaveAll)
            {
                #region save all
                foreach (string s in lines)
                {
                    //try
                    //{
                    string[] fs = s.Split(new char[] { '~' });
                    if (fs.Length > 19)
                    {
                        bool IsPromotionCostEnd = false;
                        mfp = fs[0].Trim();
                        if (mfp.Length > 30)
                        {
                            mfp = mfp.Substring(0, 30);
                        }
                        part_sku = fs[2];
                        status_code = fs[37];
                        mfp_name = fs[5];

                        promotion_Status = fs[44].Trim();
                        if (promotion_Status.Equals("Y"))
                        {
                            promotion_expiration_date = fs[46].Trim();
                            if (DateTime.Now.ToString("yyMMdd") == promotion_expiration_date
                                //|| DateTime.Now.AddDays(1).ToString("yyMMdd") == promotion_expiration_date
                                //|| DateTime.Now.AddDays(2).ToString("yyMMdd") == promotion_expiration_date
                                )
                            {
                                IsPromotionCostEnd = true;
                            }
                        }
                        else
                            promotion_expiration_date = "";

                        decimal discountCost = 0M;
                        decimal Cost = 0M;
                        decimal.TryParse(fs[10].Replace("$", "").Replace(",", ""), out discountCost);
                        decimal.TryParse(fs[18].Replace("$", "").Replace(",", ""), out Cost);

                        if (!IsPromotionCostEnd)
                            part_cost = discountCost.ToString();// after rebate
                        else
                            part_cost = Cost.ToString();


                        if (!string.IsNullOrEmpty(promotion_expiration_date))
                        {
                            Config.ExecuteNonQuery(string.Format(@"Insert into tb_discount_end_date(companyname, discount, discountenddate, regdate, MFP, PartName) 
values ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}')", "synnex"
                                                  , Cost - discountCost
                                                  , promotion_expiration_date
                                                  , DateTime.Now.ToString("yyyy-MM-dd")
                                                  , mfp
                                                  , fs[4].Replace("'", "\\'")));
                        }

                        //int.TryParse(fs[19], out store_quantity);
                        //int.TryParse(fs[21], out store_quantity2);
                        //store_quantity += store_quantity2;
                        int.TryParse(fs[7], out store_quantity);



                        msrp = fs[11].Replace("$", "").Replace(",", "");
                        name = fs[4].Replace("\xFFFD", "");
                        UPC = fs[31];
                        ship_weight = fs[25];
                        cat_code = fs[22];
                        //ETA = "";



                        if (name.ToLower().IndexOf("refurb") > -1)
                            continue;
                        //if (name.IndexOf("\\x") > -1)
                        //    continue;

                        if (status_code == "D") continue;
                        //if (status_code == "B") continue;

                        string NNY = fs[35].Trim();

                        if (NNY.Substring(1, 2) != "NY")
                            continue;

                        var luc_sku = 0;

                        foreach (DataRow dr in validDT.Rows)
                        {
                            if (string.IsNullOrEmpty(mfp))
                            {
                                continue;
                            }
                            if (dr["mfp"].ToString() == mfp)
                            {
                                luc_sku = int.Parse(dr["lu_sku"].ToString());
                                break;
                            }
                        }
                        sql_part += string.Format(@"{0}('{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}', '{10}', '{11}', '{12}') "
                            , ","
                            , mfp
                            , part_sku
                            , status_code
                            , mfp_name
                            , store_quantity
                            , part_cost
                            , msrp
                            , UPC
                            , ship_weight
                            , name.Replace("'", "\\'").Replace("\\", "\\\\")
                            , cat_code
                            , luc_sku
                            );
                        if (count % pageSize == 0 && count / pageSize > 0)
                        {
                            SetStatus(null, "Synnex insert part." + DateTime.Now.ToString());
                            Config.ExecuteNonQuery(string.Format(@"
insert into {0} (mfp,part_sku,status_code,mfp_name,store_quantity,part_cost,msrp, upc, ship_weight, part_name, cat_code, luc_sku)
values {1}"
                           , tableNameAll
                           , sql_part.Substring(1) + ";"));
                            sql_part = "";
                            count = 0;
                        }
                        else
                            count += 1;
                    }
                    //}
                    //catch (Exception e)
                    //{

                    //}

                }
                #endregion

                if (count > 0)
                {
                    Config.ExecuteNonQuery(string.Format(@"
insert into {0} (mfp,part_sku,status_code,mfp_name,store_quantity,part_cost,msrp, upc, ship_weight,  part_name, cat_code, luc_sku)
values {1}"
                              , tableNameAll
                              , sql_part.Substring(1) + ";"));
                }
                Config.ExecuteNonQuery("update " + tableNameAll + " set cost=part_cost");
                SetStatus(null, lines.Length.ToString() + " Synnex");
            }
            #endregion

            #region only price
            sql_part = "";
            count = 0;

            foreach (string s in lines)
            {

                string[] fs = s.Split(new char[] { '~' });
                if (fs.Length > 19)
                {
                    bool IsPromotionCostEnd = false;

                    mfp = fs[0].Trim(); 
                    if (mfp.Length > 30)
                    {
                        mfp = mfp.Substring(0, 30);
                    }
                    part_sku = fs[2];
                    status_code = fs[37];

                    if (status_code == "D") continue;
                    // if (status_code == "B") continue;

                    string NNY = fs[35].Trim();

                    if (NNY.Substring(1, 2) != "NY")
                        continue;



                    mfp_name = fs[5];
                    //int.TryParse(fs[19], out store_quantity);
                    //int.TryParse(fs[21], out store_quantity2);
                    //store_quantity += store_quantity2;
                    int.TryParse(fs[7], out store_quantity);

                    //part_cost = fs[10].Replace("$", "");
                    msrp = fs[11].Replace("$", "");
                    name = fs[4];
                    UPC = fs[31];


                    promotion_Status = fs[44].Trim();
                    if (promotion_Status.Equals("Y"))
                    {
                        promotion_expiration_date = fs[46].Trim();
                        if (!string.IsNullOrEmpty(promotion_expiration_date))
                        {
                            if (DateTime.Now.ToString("yyMMdd") == promotion_expiration_date
                                //|| DateTime.Now.AddDays(1).ToString("yyMMdd") == promotion_expiration_date
                                //|| DateTime.Now.AddDays(2).ToString("yyMMdd") == promotion_expiration_date
                                )
                            {
                                IsPromotionCostEnd = true;
                            }
                        }
                    }
                    else
                        promotion_expiration_date = "";

                    decimal discountCost = 0M;
                    decimal Cost = 0M;
                    decimal affterDiscount = 0M;
                    // decimal.TryParse(fs[10].Replace("$", "").Replace(",", ""), out discountCost);
                    decimal.TryParse(fs[10].Replace("$", "").Replace(",", ""), out affterDiscount);
                    decimal.TryParse(fs[18].Replace("$", "").Replace(",", ""), out Cost);
                    discountCost = Cost - affterDiscount;

                    if (IsPromotionCostEnd)
                    {
                        part_cost = Cost.ToString();
                        discountCost = 0M;
                    }
                    else
                    {
                        part_cost = affterDiscount.ToString();// after rebate
                    }
                    #region onsale
                    if (!string.IsNullOrEmpty(promotion_expiration_date) && !IsPromotionCostEnd)
                    {
                        DataTable onsaleDT = Config.ExecuteDateTable("select * from tb_other_inc_valid_lu_sku where discount=0 and manufacturer_part_number='" + mfp + "'");
                        if (onsaleDT.Rows.Count == 1)
                        {
                            try
                            {
                                if (promotion_expiration_date.Trim().Length == 6)
                                {
                                    DateTime onsaleEndDate = DateTime.Parse(string.Format("{0}-{1}-{2}"
                                                   , "20" + promotion_expiration_date.Substring(0, 2)
                                                   , promotion_expiration_date.Substring(2, 2)
                                                   , promotion_expiration_date.Substring(4, 2)));
                                    Config.ExecuteNonQuery(string.Format(@"insert into tb_synnex_all_discount 
	( LUC_Sku, discountCost, Cost, endDate,CategoryID, Stock)
	values
	( '{0}', '{1}', '{2}', '{3}', '{4}', '{5}')"
                                        , onsaleDT.Rows[0]["lu_sku"].ToString()
                                        , discountCost
                                        , Cost
                                        , onsaleEndDate.ToString("yyyy-MM-dd")
                                        , onsaleDT.Rows[0]["menu_child_serial_no"].ToString()
                                        , store_quantity));
                                }
                            }
                            catch (Exception e)
                            {
                                Helper.Logs.WriteErrorLog(e);
                            }
                        }
                    }
                    //                    
                    #endregion

                    if (!string.IsNullOrEmpty(promotion_expiration_date)
                        && DateTime.Now.ToString("yyMM") == promotion_expiration_date.Substring(0, 4))
                    {
                        Config.ExecuteNonQuery(string.Format(@"Insert into tb_discount_end_date(companyname, discount, discountenddate, regdate, MFP, PartName) 
values ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}')", "synnex"
                                              , discountCost
                                              , promotion_expiration_date
                                              , DateTime.Now.ToString("yyyy-MM-dd")
                                              , mfp
                                              , fs[4].Replace("'", "\\'")));
                    }

                    if (name.ToLower().IndexOf("refurb") > -1)
                        continue;

                    var luc_sku = 0;

                    foreach (DataRow dr in validDT.Rows)
                    {
                        if (string.IsNullOrEmpty(mfp))
                        {
                            continue;
                        }
                        if (dr["mfp"].ToString() == mfp)
                        {
                            luc_sku = int.Parse(dr["lu_sku"].ToString());
                            break;
                        }
                    }

                    sql_part += string.Format(@"{0}('{1}','{2}','{3}','{4}','{5}','{6}') "
                        , ","
                        , part_sku
                        , part_cost
                        , mfp
                        , UPC
                        , store_quantity
                        , luc_sku
                        );
                    if (count % pageSize == 0 && count / pageSize > 0)
                    {
                        SetStatus(null, "Synnex insert part." + DateTime.Now.ToString());
                        Config.ExecuteNonQuery(string.Format(@"
insert into {0} (sku,price,mfp,upc,quantity,luc_sku)
values {1}"
                       , table_name
                       , sql_part.Substring(1) + ";"));
                        sql_part = "";
                        count = 0;
                    }
                    else
                        count += 1;
                }

            }
            if (count > 0)
            {
                Config.ExecuteNonQuery(string.Format(@"
insert into {0} (sku,price,mfp,upc,quantity,luc_sku)
values {1}"
                          , table_name
                          , sql_part.Substring(1) + ";"));
            }

            SetStatus(null, lines.Length.ToString() + " Synnex only price");
            #endregion

            //DataTable validDT = Config.ExecuteDateTable("select distinct lu_sku, manufacturer_part_number mfp from tb_other_inc_valid_lu_sku");

            //int mindex = 0;
            //int mindexCount = validDT.Rows.Count;
            //SetStatus(null, string.Format("Synnex match count: ({2}) ({0}/{1})", mindex, mindexCount, DateTime.Now.ToString()));

            //string sqlUpadte = string.Empty;

            //foreach (DataRow dr in validDT.Rows)
            //{
            //    mindex++;

            //    SetStatus(null, string.Format("Synnex match count: ({2}) ({0}/{1})", mindex, mindexCount, DateTime.Now.ToString()));
            //    //replace into test_tbl (id,dr) values (1,'2'),(2,'3'),...(x,'y');
            //    if (IsSaveAll)
            //    {
            //        sqlUpadte += (string.Format("Update {0} set luc_sku='{1}' where mfp='{2}';"
            //              , tableNameAll
            //              , dr[0].ToString()
            //              , dr[1].ToString()));
            //    }
            //    else
            //    {
            //        sqlUpadte += (string.Format("Update {0} set luc_sku='{1}' where mfp='{2}';"
            //          , table_name
            //          , dr[0].ToString()
            //          , dr[1].ToString()));
            //    }
            //}
            //if (sqlUpadte != string.Empty)
            //{
            //    //sqlUpadte = "replace into {0} (" + sqlUpadte;
            //    Config.ExecuteNonQuery(sqlUpadte);
            //}

            //            var sql = string.Format(@"
            //delete from ltd_info.tb_other_inc_valid_lu_sku where manufacturer_part_number = '' or manufacturer_part_number is null;
            //update ltd_info.{0} a, ltd_info.tb_other_inc_valid_lu_sku b
            // set a.luc_sku=b.lu_sku where a.mfp=b.manufacturer_part_number;", IsSaveAll ? tableNameAll : table_name);

            SetStatus(null, "Synnex match count: end ");

            Config.ExecuteNonQuery("Delete from " + table_name + " where luc_sku=0");

            SetStatus(null, "Synnex: Save to Match DB.");
            Config.ExecuteNonQuery(string.Format(@"
delete from tb_other_inc_match_lu_sku where other_inc_type={1};
insert into tb_other_inc_match_lu_sku 
	( lu_sku, other_inc_sku, other_inc_type)
select luc_sku, mfp, '{1}' from {0} where luc_sku >0;
", table_name, ltd_id));
            SetStatus(null, "Update LUc SKU");
            //
            // 获取价格值，其他非 TOSHIBA 价格，另存在库里
            //
            // MatchPriceToNotebookDB(tableNameAll);
            //
            // 删除没有匹配的产品
            //
            // Config.ExecuteNonQuery(string.Format("delete from {0} where luc_sku is null and mfp_name <>'TOSHIBA'  ;", tableNameAll));
            //SetStatus(null, "Delete Synnex Old Cost::tb_other_inc_part_info;");

            Config.ExecuteNonQuery("delete from tb_other_inc_part_info where other_inc_id='" + ltd_id.ToString() + "'");
            SetStatus(null, "Update Synnex::tb_other_inc_part_info;");
            Config.ExecuteNonQuery(string.Format(@"insert into tb_other_inc_part_info 
	(luc_sku, other_inc_id, other_inc_sku, manufacture_part_number, 
	other_inc_price, 
	other_inc_store_sum, 
	tag, 	 
	last_regdate
	)
select luc_sku, {1}, sku, mfp, price, quantity, 1, now() from {0}", table_name, ltd_id));
            SetStatus(null, "Update End Synnex::tb_other_inc_part_info;");
            return true;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="ltd"></param>
        /// <returns></returns>
        public string CreateTable(Ltd ltd)
        {

            string talbe_name = "store_synnex_part_" + DateTime.Now.ToString("yyyyMMdd");
            DBProvider.CreateTable.PriceTable(talbe_name);

            return talbe_name;
        }

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

        #endregion

        public static int OnSale()
        {
            DataTable dt = Config.ExecuteDateTable("select * from tb_synnex_all_discount where stock >10 and categoryId in (350,22,356,377,91,31,29,25,240,241,19,33,36,21) and discountcost>3");
            int allCount = 0;
            foreach (DataRow dr in dt.Rows)
            {
                if (Config.ExecuteScalarInt("select count(id) from tb_other_inc_valid_lu_sku where discount>0 and lu_sku=" + dr["LUC_Sku"].ToString()) != 0) continue;

                #region onsale
                try
                {
                    string onsaleSKU = dr["LUC_Sku"].ToString();

                    DateTime onsaleEndDate = DateTime.Parse(dr["endDate"].ToString());

                    double day_qty = (onsaleEndDate - DateTime.Now).TotalDays;
                    if (day_qty <= 1)
                    {
                        Config.RemoteExecuteDateTable(string.Format(@"
                    delete from tb_on_sale where product_serial_no='{0}';
                    update tb_product set product_current_price=product_current_price-product_current_discount,is_modify=1 where product_serial_no='{0}';
                    update tb_product set product_current_discount=0 where product_serial_no='{0}';", onsaleSKU));

                    }
                    else if (day_qty > 2)
                    {
                        day_qty = day_qty > 7 ? 7 : day_qty;

                        decimal Cost = decimal.Parse(dr["Cost"].ToString());
                        decimal discountCost = decimal.Parse(dr["discountCost"].ToString());
                        decimal discount = Cost - discountCost;
                        if (discount > 0)
                        {

                            DataTable pDt = Config.RemoteExecuteDateTable("Select product_current_price,product_current_discount from tb_product where product_serial_no='" + onsaleSKU + "'");
                            decimal remoteProduct_current_price = decimal.Parse(pDt.Rows[0]["product_current_price"].ToString());
                            decimal remoteProduct_current_discount = decimal.Parse(pDt.Rows[0]["product_current_discount"].ToString());
                            if (remoteProduct_current_discount < 1M)
                            {
                                //if (discount > 2 && discount <= 30)
                                //{
                                //    discount = 5M;
                                //}
                                //else if (discount > 30 && discount < 50)
                                //{
                                //    discount = 30M;
                                //}
                                //else if (discount >= 50M && discount < 100)
                                //{
                                //    discount = 50M;
                                //}
                                //else if (discount >= 100M && discount < 150M)
                                //{
                                //    discount = 100M;
                                //}
                                //else if (discount >= 150M && discount < 300M)
                                //{
                                //    discount = 150M;
                                //}
                                //else if (discount >= 300M)
                                //{
                                //    discount = 200M;
                                //}

                                string sql = string.Format(@"Insert into tb_on_sale(product_serial_no, begin_datetime,end_datetime,cost,modify_datetime,price, sale_price, save_price) values 
                                                                                                            ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}');"
                                    , onsaleSKU
                                    , DateTime.Now.ToString("yyyy-MM-dd")
                                    , DateTime.Now.AddDays(day_qty - 2).ToString("yyyy-MM-dd")
                                    , discount
                                    , DateTime.Now.ToString("yyyy-MM-dd")
                                    , decimal.Parse(pDt.Rows[0]["product_current_price"].ToString()) + discountCost
                                    , decimal.Parse(pDt.Rows[0]["product_current_price"].ToString())
                                    , discountCost
                                   );
                                Config.RemoteExecuteNonQuery(string.Format("Delete from tb_on_sale where product_serial_no='{0}';", onsaleSKU) + sql + "Update tb_product set product_current_price=" + (remoteProduct_current_price + discountCost) + ",product_current_discount='" + (discountCost) + "',product_current_special_cash_price='" + (EditPriceToRemote.ChangePriceToNotCard(remoteProduct_current_price + discountCost, 1.022M)) + "'  where product_serial_no='" + onsaleSKU + "'");

                                allCount++;
                            }
                        }
                        else if (discountCost == 0M || discount == 0M)
                        {
                            Config.RemoteExecuteDateTable(string.Format(@"
                    delete from tb_on_sale where product_serial_no='{0}';
                    update tb_product set product_current_price=product_current_price-product_current_discount where product_serial_no='{0}';
                    update tb_product set product_current_discount=0,is_modify=1 where product_serial_no='{0}';", onsaleSKU));

                        }
                    }

                }
                catch { }

                #endregion
            }
            return allCount;
        }
    }
}
