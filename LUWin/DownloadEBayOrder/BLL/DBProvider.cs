using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DownloadEBayOrder
{
    public class DBProvider
    {
        public static nicklu2Entities DB = new nicklu2Entities();

        public DBProvider() { }

        /// <summary>
        /// 取得系统的Cost
        /// </summary>
        /// <param name="systemSKU"></param>
        /// <returns></returns>
        public static decimal GeteBaySysCost(int systemSKU)
        {

            decimal cost = 0M;
            List<int> skus = new List<int>();
            var list = DB.tb_ebay_system_parts.Where(l => l.system_sku == systemSKU && l.is_belong_price == true);
            foreach (var m in list)
            {
                int sku;
                int.TryParse(m.luc_sku.ToString(), out sku);
                skus.Add(sku);
            }
            foreach (var sku in skus)
            {
                tb_product prod = DB.tb_product.FirstOrDefault(p => p.product_serial_no == sku);
                if (prod != null)
                {
                    decimal c;
                    decimal.TryParse(prod.product_current_cost.ToString(), out c);
                    cost += c;
                }
            }

            return cost;
        }

        /// <summary>
        /// 取得新的系统编号
        /// </summary>
        /// <returns></returns>
        public static int NewSysCode()
        {
            
            tb_store_sys_code ssc = DB.tb_store_sys_code.FirstOrDefault(s => s.ID > 0);
            if (ssc != null)
            {
                int code;
                int.TryParse(ssc.SysCode, out code);

                DB.tb_store_sys_code.Remove(ssc);
                DB.SaveChanges();
                return code;
            }

            throw new Exception("System code is null.");
        }
    }
}
