using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;

namespace TransitShipment
{
    class Dandh
    {


        public static bool DanDhWatch(string filename, bool IsSaveAll)
        {

            if (!System.IO.File.Exists(filename))
            {
                Util.Logs.WriteLog("dandh 文件不存");
                return false;
            }

            int ltd_id = (int)Ltd.wholesaler_dandh;
            Config.ExecuteNonQuery("delete from tb_other_inc_part_info where other_inc_id='" + ltd_id.ToString() + "'");
            Util.Logs.WriteLog("Dandh Watch Begin...");

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

                        int LU_sku = 0;// GetSKUByMfp(mfr_item_number, "NEW");

                        sbSQL.Append(string.Format(@",('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}',now(),now())"
                            , LU_sku
                            , (int)Ltd.wholesaler_dandh
                            , item_number
                            , mfr_item_number
                            , cost
                            , quantity_on_hand
                            , 1
                            , "NEW"
                            , ""
                            ));

                        //luc_sku,
                        //other_inc_id,
                        //other_inc_sku,
                        //manufacture_part_number,
                        //other_inc_price,
                        //other_inc_store_sum,
                        //tag,
                        //prodType,
                        //ETA,
                        //Regdate,
                        //last_regdate
                    }
                }
                catch (Exception ex)
                {
                    Util.Logs.WriteErrorLog(ex);
                }
            }
            // 保存
            Util.Logs.WriteLog("Dandh 拼完，将执行保存..;");
            Config.ExecuteNonQuery(string.Format(@"
insert into {0} (luc_sku,other_inc_id,other_inc_sku,manufacture_part_number,other_inc_price,other_inc_store_sum,tag,prodType,ETA,Regdate,last_regdate)
values {1}"
                          , "tb_other_inc_part_info"
                          , sbSQL.ToString().Substring(1) + ";"
                          ));

            Util.Logs.WriteLog("Dandh 匹配;");
            // 拼SKU
            Config.ExecuteNonQuery(string.Format(@"
            update {0} a, tb_product b
set a.luc_sku = b.product_serial_no where b.manufacturer_part_number=a.manufacture_part_number and a.manufacture_part_number<>'' and other_inc_id='{1}'"
, "tb_other_inc_part_info"
                         , sbSQL.ToString().Substring(1) + ";"
                         , (int)Ltd.wholesaler_dandh));
            // 删除多余的
            Config.ExecuteNonQuery("Delete from tb_other_inc_part_info where other_inc_id='" + ((int)Ltd.wholesaler_dandh) + "' and luc_sku=0");
            Util.Logs.WriteLog("Dandh 完成");
            return true;
        }

        public static int GetSKUByMfp(string mfp, string prodType)
        {

            string sku = "";
            try
            {
                DataTable dts = Config.ExecuteDateTable(string.Format("select product_serial_no from tb_product where manufacturer_part_number='{0}' and prodType='{1}' order by product_serial_no desc limit 0,1", mfp, prodType));
                //MessageBox.Show(string.Format("select lu_sku from tb_other_inc_valid_lu_sku where manufacturer_part_number='{0}' order by lu_sku desc limit 0,1", mfp));

                if (dts.Rows.Count > 0)
                {
                    sku = dts.Rows[0][0].ToString();

                }
            }
            catch (Exception ex) { Util.Logs.WriteErrorLog(ex); }


            int lu_sku; int.TryParse(sku, out lu_sku);

            return lu_sku;
        }
    }
}
