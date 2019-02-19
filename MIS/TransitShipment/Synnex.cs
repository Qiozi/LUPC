using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;

namespace TransitShipment
{
    public class Synnex
    {
        public static bool WatchRun(string fullFilename)
        {
            if (!File.Exists(fullFilename))
            {
                Util.Logs.WriteLog("Synnex File isn't exist.");
                return false;
            }


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

            int pageSize = 3000;
            int count = 0;
            string sql_part = "";

            #region only price
            sql_part = "";
            count = 0;
            foreach (string s in lines)
            {

                string[] fs = s.Split(new char[] { '~' });
                if (fs.Length > 19)
                {
                    bool IsPromotionCostEnd = false;

                    mfp = fs[0];
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
                    decimal.TryParse(fs[10].Replace("$", "").Replace(",", ""), out discountCost);
                    decimal.TryParse(fs[18].Replace("$", "").Replace(",", ""), out Cost);

                    if (!IsPromotionCostEnd)
                        part_cost = discountCost.ToString();// after rebate
                    else
                        part_cost = Cost.ToString();

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
                                Util.Logs.WriteErrorLog(e);
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
                                              , Cost - discountCost
                                              , promotion_expiration_date
                                              , DateTime.Now.ToString("yyyy-MM-dd")
                                              , mfp
                                              , fs[4].Replace("'", "\\'")));
                    }


                    if (name.ToLower().IndexOf("refurb") > -1)
                        continue;


                    sql_part += string.Format(@"{0}('{1}','{2}','{3}','{4}','{5}') "
                        , ","
                        , part_sku
                        , part_cost
                        , mfp
                        , UPC
                        , store_quantity
                        );
                    if (count % pageSize == 0 && count / pageSize > 0)
                    {
                        Util.Logs.WriteLog( "Synnex insert part." + DateTime.Now.ToString());
                        Config.ExecuteNonQuery(string.Format(@"
insert into {0} (sku,price,mfp,upc,quantity)
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
insert into {0} (sku,price,mfp,upc,quantity)
values {1}"
                          , table_name
                          , sql_part.Substring(1) + ";"));
            }

            SetStatus(null, lines.Length.ToString() + " Synnex only price");
            #endregion

            DataTable validDT = Config.ExecuteDateTable("select lu_sku, manufacturer_part_number mfp from tb_other_inc_valid_lu_sku");

            int mindex = 0;
            int mindexCount = validDT.Rows.Count;
            foreach (DataRow dr in validDT.Rows)
            {
                mindex++;
                // if (matchIndex == 200)
                {
                    Config.ExecuteNonQuery(string.Format("Update {0} set luc_sku='{1}' where mfp='{2}';"
                    , table_name
                    , dr[0].ToString()
                    , dr[1].ToString()));

                    SetStatus(null, string.Format("Synnex match count: ({2}) ({0}/{1})", mindex, mindexCount, DateTime.Now.ToString()));

                }

                if (IsSaveAll)
                    Config.ExecuteNonQuery(string.Format("Update {0} set luc_sku='{1}' where mfp='{2}'"
                   , tableNameAll
                   , dr[0].ToString()
                   , dr[1].ToString()));

            }

            SetStatus(null, "Synnex match count: (" + validDT.Rows.Count.ToString() + ") ");

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
    }
}
