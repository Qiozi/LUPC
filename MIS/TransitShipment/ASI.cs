using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace TransitShipment
{
    public class ASI
    {
        public static bool Run(string filepath)
        {
            //tspb.Maximum = 3;
            Util.Logs.WriteLog("ASI watch begin." + DateTime.Now.ToString());

            if (!File.Exists(filepath))
            {
                Util.Logs.WriteLog("Asi File isn't exist.");
                return false;
            }

            Config.ExecuteNonQuery("delete from tb_other_inc_part_info where other_inc_id=" + ((int)Ltd.wholesaler_asi).ToString());

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
                        int LU_sku = 0;// Dandh.GetSKUByMfp(itemid, "NEW");

                        //if (LU_sku > 0)
                        sbSQL.Append(string.Format(@",('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}',now(),now())"
                      , LU_sku
                      , (int)Ltd.wholesaler_asi
                      , sku
                      , itemid
                      , price
                      , quantity
                      , 1
                      , "NEW"
                      , ""
                      ));


                    }
                }
                catch (Exception ex)
                {
                    Util.Logs.WriteErrorLog(ex);
                }
            }

            // 保存
            Util.Logs.WriteLog("ASI 拼完，将执行保存..;");
            Config.ExecuteNonQuery(string.Format(@"
insert into tb_other_inc_part_info (luc_sku,other_inc_id,other_inc_sku,manufacture_part_number,other_inc_price,other_inc_store_sum,tag,prodType,ETA,Regdate,last_regdate)
values {0}"

                          , sbSQL.ToString().Substring(1) + ";"));

            Util.Logs.WriteLog("ASI 匹配");
            // 拼SKU
            Config.ExecuteNonQuery(string.Format(@"
            update tb_other_inc_part_info a, tb_product b
set a.luc_sku = b.product_serial_no where b.manufacturer_part_number=a.manufacture_part_number and a.manufacture_part_number<>'' and other_inc_id='{0}'"
                         , (int)Ltd.wholesaler_asi));
            // 删除多余的
            Util.Logs.WriteLog("ASI 删除多余");
            Config.ExecuteNonQuery("Delete from tb_other_inc_part_info where other_inc_id='" + ((int)Ltd.wholesaler_asi) + "' and luc_sku=0");
            Util.Logs.WriteLog("ASI 完成");
            return true;
        }
    }
}
