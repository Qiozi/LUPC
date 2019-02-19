using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;

namespace DownloadEBayOrder.BLL
{
    public class SysKeyword
    {
        public static void Run(int cateId = 0)
        {
            //            string json = "";
            //            if (cateId == 0)
            //            {
            //                #region cateid  = 0
            //                DataTable cpuDt = Config.ExecuteDateTable(@"SELECT DISTINCT  p.short_name_for_sys, MAX(p.product_serial_no) FROM 
            //tb_ebay_selling es 
            //INNER JOIN tb_ebay_system_parts esp ON esp.system_sku = es.sys_sku AND es.sys_sku>0
            //INNER JOIN tb_product p ON  p.product_serial_no = esp.luc_sku
            // WHERE p.menu_child_serial_no=22 AND p.tag=1 AND p.short_name_for_sys IS NOT NULL 
            //GROUP BY p.short_name_for_sys
            //ORDER BY p.short_name_for_sys");

            //                DataTable vcDT = Config.ExecuteDateTable(@"SELECT DISTINCT  p.short_name_for_sys, MAX(p.product_serial_no) FROM 
            //tb_ebay_selling es 
            //INNER JOIN tb_ebay_system_parts esp ON esp.system_sku = es.sys_sku AND es.sys_sku>0
            //INNER JOIN tb_product p ON  p.product_serial_no = esp.luc_sku
            // WHERE p.menu_child_serial_no=41 AND p.tag=1 AND p.short_name_for_sys IS NOT NULL 
            //GROUP BY p.short_name_for_sys
            //ORDER BY p.short_name_for_sys");

            //                DataTable memoryDT = Config.ExecuteDateTable(@"SELECT DISTINCT  p.short_name_for_sys, MAX(p.product_serial_no) FROM 
            //tb_ebay_selling es 
            //INNER JOIN tb_ebay_system_parts esp ON esp.system_sku = es.sys_sku AND es.sys_sku>0
            //INNER JOIN tb_product p ON  p.product_serial_no = esp.luc_sku
            // WHERE p.menu_child_serial_no=29 AND p.tag=1 AND p.short_name_for_sys IS NOT NULL 
            //GROUP BY p.short_name_for_sys
            //ORDER BY p.short_name_for_sys");

            //                DataTable ssdDT = Config.ExecuteDateTable(@"SELECT DISTINCT  p.short_name_for_sys, MAX(p.product_serial_no) FROM 
            //tb_ebay_selling es 
            //INNER JOIN tb_ebay_system_parts esp ON esp.system_sku = es.sys_sku AND es.sys_sku>0
            //INNER JOIN tb_product p ON  p.product_serial_no = esp.luc_sku
            // WHERE p.menu_child_serial_no=456 AND p.tag=1 AND p.short_name_for_sys IS NOT NULL 
            //GROUP BY p.short_name_for_sys
            //ORDER BY p.short_name_for_sys");

            //                DataTable hdDT = Config.ExecuteDateTable(@"SELECT DISTINCT  p.short_name_for_sys, MAX(p.product_serial_no) FROM 
            //tb_ebay_selling es 
            //INNER JOIN tb_ebay_system_parts esp ON esp.system_sku = es.sys_sku AND es.sys_sku>0
            //INNER JOIN tb_product p ON  p.product_serial_no = esp.luc_sku
            // WHERE p.menu_child_serial_no=25 AND p.tag=1 AND p.short_name_for_sys IS NOT NULL 
            //GROUP BY p.short_name_for_sys
            //ORDER BY p.short_name_for_sys");


            //                string cpuTempStr = "";
            //                string vcTempStr = "";
            //                string memoryTempStr = "";
            //                string ssdTempStr = "";
            //                string hdTempStr = "";
            //                for (int i = 0; i < cpuDt.Rows.Count; i++)
            //                {
            //                    cpuTempStr += (i != 0 ? "," : "") + "\"" + cpuDt.Rows[i][0].ToString() + "\"";
            //                }
            //                for (int i = 0; i < vcDT.Rows.Count; i++)
            //                {
            //                    vcTempStr += (i != 0 ? "," : "") + "\"" + vcDT.Rows[i][0].ToString() + "\"";
            //                }

            //                for (int i = 0; i < memoryDT.Rows.Count; i++)
            //                {
            //                    memoryTempStr += (i != 0 ? "," : "") + "\"" + memoryDT.Rows[i][0].ToString() + "\"";
            //                }
            //                for (int i = 0; i < ssdDT.Rows.Count; i++)
            //                {
            //                    ssdTempStr += (i != 0 ? "," : "") + "\"" + ssdDT.Rows[i][0].ToString() + "\"";
            //                }
            //                for (int i = 0; i < hdDT.Rows.Count; i++)
            //                {
            //                    hdTempStr += (i != 0 ? "," : "") + "\"" + hdDT.Rows[i][0].ToString() + "\"";
            //                }

            //                json = string.Format(@"{{""cpu"":[{0}]
            //                ,""vc"":[{1}]
            //                ,""memory"":[{2}]
            //                ,""ssd"":[{3}]
            //                ,""hd"":[{4}]}}"
            //                    , cpuTempStr
            //                    , vcTempStr
            //                    , memoryTempStr
            //                    , ssdTempStr
            //                    , hdTempStr);
            //                #endregion
            //            }
            var json = GetKeyJson(cateId);
            var sw = new StreamWriter("C:\\Workspaces\\Web1.0\\Computer\\sys_key" + (cateId == 0 ? "" : cateId.ToString()) + ".txt", false);
            sw.Write(json);
            sw.Close();
        }

        public class SysMiniBase
        {
            public int SysSku { get; set; }

            public string eBayId { get; set; }

            public decimal eBayPrice { get; set; }

            public string eBayTitle { get; set; }

            public decimal Price { get; set; }

            public decimal Discount { get; set; }

            public decimal Sold { get; set; }
        }

        public static string GetKeyJson(int cateId = 0)
        {
            if (cateId == 0)
            {
                #region cateid  = 0
                DataTable cpuDt = Config.ExecuteDataTable(@"SELECT DISTINCT  p.short_name_for_sys, MAX(p.product_serial_no) FROM 
tb_ebay_selling es 
INNER JOIN tb_ebay_system_parts esp ON esp.system_sku = es.sys_sku AND es.sys_sku>0
INNER JOIN tb_product p ON  p.product_serial_no = esp.luc_sku
 WHERE p.menu_child_serial_no=22 AND p.tag=1 AND p.short_name_for_sys IS NOT NULL 
GROUP BY p.short_name_for_sys
ORDER BY p.short_name_for_sys");

                DataTable vcDT = Config.ExecuteDataTable(@"SELECT DISTINCT  p.short_name_for_sys, MAX(p.product_serial_no) FROM 
tb_ebay_selling es 
INNER JOIN tb_ebay_system_parts esp ON esp.system_sku = es.sys_sku AND es.sys_sku>0
INNER JOIN tb_product p ON  p.product_serial_no = esp.luc_sku
 WHERE p.menu_child_serial_no=41 AND p.tag=1 AND p.short_name_for_sys IS NOT NULL 
GROUP BY p.short_name_for_sys
ORDER BY p.short_name_for_sys");

                DataTable memoryDT = Config.ExecuteDataTable(@"SELECT DISTINCT  p.short_name_for_sys, MAX(p.product_serial_no) FROM 
tb_ebay_selling es 
INNER JOIN tb_ebay_system_parts esp ON esp.system_sku = es.sys_sku AND es.sys_sku>0
INNER JOIN tb_product p ON  p.product_serial_no = esp.luc_sku
 WHERE p.menu_child_serial_no=29 AND p.tag=1 AND p.short_name_for_sys IS NOT NULL 
GROUP BY p.short_name_for_sys
ORDER BY p.short_name_for_sys");

                DataTable ssdDT = Config.ExecuteDataTable(@"SELECT DISTINCT  p.short_name_for_sys, MAX(p.product_serial_no) FROM 
tb_ebay_selling es 
INNER JOIN tb_ebay_system_parts esp ON esp.system_sku = es.sys_sku AND es.sys_sku>0
INNER JOIN tb_product p ON  p.product_serial_no = esp.luc_sku
 WHERE p.menu_child_serial_no=456 AND p.tag=1 AND p.short_name_for_sys IS NOT NULL 
GROUP BY p.short_name_for_sys
ORDER BY p.short_name_for_sys");

                DataTable hdDT = Config.ExecuteDataTable(@"SELECT DISTINCT  p.short_name_for_sys, MAX(p.product_serial_no) FROM 
tb_ebay_selling es 
INNER JOIN tb_ebay_system_parts esp ON esp.system_sku = es.sys_sku AND es.sys_sku>0
INNER JOIN tb_product p ON  p.product_serial_no = esp.luc_sku
 WHERE p.menu_child_serial_no=25 AND p.tag=1 AND p.short_name_for_sys IS NOT NULL 
GROUP BY p.short_name_for_sys
ORDER BY p.short_name_for_sys");


                string cpuTempStr = "";
                string vcTempStr = "";
                string memoryTempStr = "";
                string ssdTempStr = "";
                string hdTempStr = "";
                for (int i = 0; i < cpuDt.Rows.Count; i++)
                {
                    cpuTempStr += (i != 0 ? "," : "") + "\"" + cpuDt.Rows[i][0].ToString() + "\"";
                }
                for (int i = 0; i < vcDT.Rows.Count; i++)
                {
                    vcTempStr += (i != 0 ? "," : "") + "\"" + vcDT.Rows[i][0].ToString() + "\"";
                }

                for (int i = 0; i < memoryDT.Rows.Count; i++)
                {
                    memoryTempStr += (i != 0 ? "," : "") + "\"" + memoryDT.Rows[i][0].ToString() + "\"";
                }
                for (int i = 0; i < ssdDT.Rows.Count; i++)
                {
                    ssdTempStr += (i != 0 ? "," : "") + "\"" + ssdDT.Rows[i][0].ToString() + "\"";
                }
                for (int i = 0; i < hdDT.Rows.Count; i++)
                {
                    hdTempStr += (i != 0 ? "," : "") + "\"" + hdDT.Rows[i][0].ToString() + "\"";
                }

                return string.Format(@"{{""cpu"":[{0}]
                ,""vc"":[{1}]
                ,""memory"":[{2}]
                ,""ssd"":[{3}]
                ,""hd"":[{4}]}}"
                    , cpuTempStr
                    , vcTempStr
                    , memoryTempStr
                    , ssdTempStr
                    , hdTempStr);

                #endregion
            }
            else
            {

                var sysSkus = new List<int>();
                var filename = "C:\\Workspaces\\Web1.0\\Computer\\systems\\" + cateId + ".txt";
                var content = File.ReadAllText(filename);
                var sysminis = Newtonsoft.Json.JsonConvert.DeserializeObject<List<SysMiniBase>>(content);
                sysSkus = sysminis.Select(p => p.SysSku).ToList();
               // File.WriteAllText("C:\\Workspaces\\Web1.0\\Computer\\systems\\ttt.txt", content);

                DataTable cpuDt = Config.ExecuteDataTable(@"SELECT DISTINCT  p.short_name_for_sys, MAX(p.product_serial_no) FROM 
tb_ebay_selling es 
INNER JOIN tb_ebay_system_parts esp ON esp.system_sku = es.sys_sku AND es.sys_sku>0 and es.sys_sku in (" + (string.Join(",", sysSkus)) + @")
INNER JOIN tb_product p ON  p.product_serial_no = esp.luc_sku
 WHERE p.menu_child_serial_no=22 AND p.tag=1 AND p.short_name_for_sys IS NOT NULL 
GROUP BY p.short_name_for_sys
ORDER BY p.short_name_for_sys");

                DataTable vcDT = Config.ExecuteDataTable(@"SELECT DISTINCT  p.short_name_for_sys, MAX(p.product_serial_no) FROM 
tb_ebay_selling es 
INNER JOIN tb_ebay_system_parts esp ON esp.system_sku = es.sys_sku AND es.sys_sku>0 and es.sys_sku in (" + (string.Join(",", sysSkus)) + @")
INNER JOIN tb_product p ON  p.product_serial_no = esp.luc_sku
 WHERE p.menu_child_serial_no=41 AND p.tag=1 AND p.short_name_for_sys IS NOT NULL 
GROUP BY p.short_name_for_sys
ORDER BY p.short_name_for_sys");

                DataTable memoryDT = Config.ExecuteDataTable(@"SELECT DISTINCT  p.short_name_for_sys, MAX(p.product_serial_no) FROM 
tb_ebay_selling es 
INNER JOIN tb_ebay_system_parts esp ON esp.system_sku = es.sys_sku AND es.sys_sku>0 and es.sys_sku in (" + (string.Join(",", sysSkus)) + @")
INNER JOIN tb_product p ON  p.product_serial_no = esp.luc_sku
 WHERE p.menu_child_serial_no=29 AND p.tag=1 AND p.short_name_for_sys IS NOT NULL 
GROUP BY p.short_name_for_sys
ORDER BY p.short_name_for_sys");

                DataTable ssdDT = Config.ExecuteDataTable(@"SELECT DISTINCT  p.short_name_for_sys, MAX(p.product_serial_no) FROM 
tb_ebay_selling es 
INNER JOIN tb_ebay_system_parts esp ON esp.system_sku = es.sys_sku AND es.sys_sku>0 and es.sys_sku in (" + (string.Join(",", sysSkus)) + @")
INNER JOIN tb_product p ON  p.product_serial_no = esp.luc_sku
 WHERE p.menu_child_serial_no=456 AND p.tag=1 AND p.short_name_for_sys IS NOT NULL 
GROUP BY p.short_name_for_sys
ORDER BY p.short_name_for_sys");

                DataTable hdDT = Config.ExecuteDataTable(@"SELECT DISTINCT  p.short_name_for_sys, MAX(p.product_serial_no) FROM 
tb_ebay_selling es 
INNER JOIN tb_ebay_system_parts esp ON esp.system_sku = es.sys_sku AND es.sys_sku>0 and es.sys_sku in (" + (string.Join(",", sysSkus)) + @")
INNER JOIN tb_product p ON  p.product_serial_no = esp.luc_sku
 WHERE p.menu_child_serial_no=25 AND p.tag=1 AND p.short_name_for_sys IS NOT NULL 
GROUP BY p.short_name_for_sys
ORDER BY p.short_name_for_sys");


                string cpuTempStr = "";
                string vcTempStr = "";
                string memoryTempStr = "";
                string ssdTempStr = "";
                string hdTempStr = "";
                for (int i = 0; i < cpuDt.Rows.Count; i++)
                {
                    cpuTempStr += (i != 0 ? "," : "") + "\"" + cpuDt.Rows[i][0].ToString() + "\"";
                }
                for (int i = 0; i < vcDT.Rows.Count; i++)
                {
                    vcTempStr += (i != 0 ? "," : "") + "\"" + vcDT.Rows[i][0].ToString() + "\"";
                }

                for (int i = 0; i < memoryDT.Rows.Count; i++)
                {
                    memoryTempStr += (i != 0 ? "," : "") + "\"" + memoryDT.Rows[i][0].ToString() + "\"";
                }
                for (int i = 0; i < ssdDT.Rows.Count; i++)
                {
                    ssdTempStr += (i != 0 ? "," : "") + "\"" + ssdDT.Rows[i][0].ToString() + "\"";
                }
                for (int i = 0; i < hdDT.Rows.Count; i++)
                {
                    hdTempStr += (i != 0 ? "," : "") + "\"" + hdDT.Rows[i][0].ToString() + "\"";
                }

                return string.Format(@"{{""cpu"":[{0}]
                ,""vc"":[{1}]
                ,""memory"":[{2}]
                ,""ssd"":[{3}]
                ,""hd"":[{4}]}}"
                    , cpuTempStr
                    , vcTempStr
                    , memoryTempStr
                    , ssdTempStr
                    , hdTempStr);
            }
        }
    }
}
