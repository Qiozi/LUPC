using System;
using System.Text;
using System.Windows.Forms;
using System.IO;
using LUComputers.DBProvider;
using System.Data;

namespace LUComputers.Watch
{
    public class ASI
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
                ltd = Ltd.wholesaler_asi,
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
        public ASI() { }


        /// <summary>
        /// 比较结果
        /// </summary>
        public void ViewCompare()
        {
            SetStatus(null, "Compare ASI begin.");
            Helper.Compare.ViewCompare(Ltd.wholesaler_asi);
            SetStatus(string.Format(@"{0}\{1}", Application.StartupPath, "_s.html"), "Compare ASI end.");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="table_name"></param>
        /// <returns></returns>
        public bool Run(string filepath, bool IsSaveAll, ref string table_name)
        {
            //tspb.Maximum = 3;
            SetStatus(null, "ASI watch begin." + DateTime.Now.ToString());
            SetStatus("ASI watch begin." + DateTime.Now.ToString());
            Watch.LU L = new LU();

            if (!File.Exists(filepath))
            {
                SetStatus(null, "Asi File isn't exist.");
                return false;
            }

            LtdHelper lh = new LtdHelper();
            int ltd_id = lh.LtdHelperValue(Ltd.wholesaler_asi);

            table_name = CreateTable();
            string tableNameAll = DBProvider.TableName.AsiAll;
            if (IsSaveAll)
                DBProvider.CreateTable.Table_ASI(tableNameAll);

            string sku = "";
            string itemid = "";
            string description = "";
            string vendor = "";
            string cat = "";
            string quantity = "";
            string price = "";
            string weight = "";
            string size = "";
            string unit = "";
            string sub_category = "";
            string status = "";
            string upc = "";

            string[] lines = File.ReadAllLines(filepath);
            System.Text.StringBuilder sbSQL = new StringBuilder();
            int count = 0;
            int countAll = lines.Length;
            var sqlAllPart = string.Empty;

            DataTable luSkuDT = Config.ExecuteDateTable(string.Format("select lu_sku,manufacturer_part_number from tb_other_inc_valid_lu_sku where prodType='{0}' ", "NEW"));

            foreach (var s in lines)
            {
                try
                {
                    count++;
                    string[] ls = s.Replace("", "").Split(new char[] { ',' });
                    sku = ls[0];

                    itemid = ls[1].Length > 100 ? ls[1].Substring(0, 100) : ls[1];
                    description = ls[2].Length > 300 ? ls[2].Substring(0, 300) : ls[2];
                    vendor = ls[3];
                    cat = ls[4];
                    quantity = ls[5];
                    price = ls[6];
                    weight = ls[7];
                    size = ls[8];
                    unit = ls[9];
                    sub_category = ls[10];
                    status = ls[11].Trim();
                    upc = ls[12].Trim() == "NA" ? "" : ls[12].Trim();
                    //SetStatus(null, itemid + ":::" + description);

                    if (status == "N"
                            || status == "V"
                            || status == "A"
                            || status == "F"
                            || status == "ACTIVE")
                    {
                        int LU_sku = Watch.LU.GetSKUByMfp(itemid, luSkuDT);
                        //if (LU_sku < 1)
                        //   LU_sku = Watch.LU.GetSKUByltdSku(sku, ltd_id, "NEW");

                        if (LU_sku > 0)
                            sbSQL.Append(string.Format(@",('{0}','{1}','{2}','{3}','{4}','{5}')"
                              , sku
                              , price
                              , itemid
                              , upc
                              , quantity
                              , LU_sku));


                        if (IsSaveAll)
                        {
                            SetStatus(null, string.Format(":::({0}/{1})", count.ToString("00000"), countAll.ToString()) + "=============" + DateTime.Now.ToString() + ":::::" + itemid);


                            sqlAllPart = (string.Format(@"
('{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}', '{12}', '{13}', '{14}'),"
                                                                              , tableNameAll
                                                                              , sku
                                                                              , itemid
                                                                              , description.Replace("\\", "\\\\").Replace("'", "\'")
                                                                              , vendor
                                                                              , cat
                                                                              , quantity
                                                                              , price
                                                                              , weight
                                                                              , size
                                                                              , unit
                                                                              , sub_category
                                                                              , status
                                                                              , LU_sku
                                                                              , upc));

                            Config.ExecuteNonQuery(string.Format(@"
insert into {0}(asi_sku, itmeid, description, vendor, cat, quantity, price,weight, size, unit, sub_category, status, luc_sku, upc) values 
{1}"
                                                                                , tableNameAll
                                                                                , sqlAllPart.Substring(0, sqlAllPart.Length - 1) + ";"));
                        }
                    }


                }
                catch (Exception ex)
                {
                    if (ex.Message.IndexOf("\\") == -1)
                        Helper.Logs.WriteErrorLog(ex);
                }
            }

            // 一次性插入所有。
            try
            {
                //                if (!string.IsNullOrEmpty(sqlAllPart))
                //                {
                //                    Config.ExecuteNonQuery(string.Format(@"
                //insert into {0}(asi_sku, itmeid, description, vendor, cat, quantity, price,weight, size, unit, sub_category, status, luc_sku, upc) values 
                //{1}"
                //                                                                                , tableNameAll
                //                                                                                , sqlAllPart.Substring(0, sqlAllPart.Length - 1) + ";"));
                //                }
            }
            catch (Exception ex)
            {
                Helper.Logs.WriteErrorLog(ex);
            }

            Config.ExecuteNonQuery(string.Format(@"
insert into {0} (sku,price,mfp,upc,quantity,luc_sku)
values {1}"
                         , table_name
                         , sbSQL.ToString().Substring(1) + ";"));
            SetStatus(null, "ASI Watch End.");
            //                     

            Config.ExecuteNonQuery(string.Format(@"
Delete from tb_other_inc_match_lu_sku where other_inc_type='{0}';

insert into tb_other_inc_match_lu_sku 
	(lu_sku, other_inc_sku, other_inc_type)
select luc_sku, sku, '{0}' from {1} where luc_sku >0
", ltd_id, table_name));

            SetStatus(null, "Delete asi Old Cost::tb_other_inc_part_info;");

            SetStatus(null, "Update asi::tb_other_inc_part_info;");
            Config.ExecuteNonQuery(string.Format(@"
delete from tb_other_inc_part_info where other_inc_id='" + ltd_id.ToString() + @"';

insert into tb_other_inc_part_info 
    (luc_sku, other_inc_id, other_inc_sku, manufacture_part_number, 
	other_inc_price, 
	other_inc_store_sum, 
	tag, 	 
	last_regdate
	)
select luc_sku, {1}, sku, mfp, price, quantity, 1, now() from {0}", table_name, ltd_id));
            SetStatus(null, "Update End ASI::tb_other_inc_part_info;");

            return true;
        }
        //tspb.Value = 3;


        public string CreateTable()
        {
            //DataTable dt = Config.ExecuteDateTable("show tables like '%store_asi_part_%'");
            //for (int i = 0; i < dt.Rows.Count - G.SaveTableCount; i++)
            //{
            //    Config.ExecuteNonQuery("drop table " + dt.Rows[i][0].ToString() + ";");
            //}
            LtdHelper LH = new LtdHelper();
            int ltd_id = LH.LtdHelperValue(Ltd.wholesaler_asi);
            string date_short_name = DateTime.Now.ToString("yyyyMMdd");
            string talbe_name = DBProvider.TableName.GetPriceTableNamePart(new LtdHelper().FilterText(Ltd.wholesaler_asi.ToString())) + date_short_name;

            DBProvider.CreateTable.PriceTable(talbe_name);
            return talbe_name;
        }

    }
}
