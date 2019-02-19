using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SiteDB
{
     public class Codes
    {
        public Codes() { }


        /// <summary>
        /// 取得新的客户号
        /// </summary>
        /// <returns></returns>
        public static int NewCustomerCode(nicklu2Entities db)
        {
            string code = "0";
            var m = db.tb_store_customer_code.FirstOrDefault(p => true);
            if (m != null)
            {
                code = m.CustomerCode;
                db.DeleteObject(m);
                db.SaveChanges();
            }
            else
                throw new Exception("store_customer_code is null");
            return int.Parse(code);
        }

        /// <summary>
        /// 取得新的系统号
        /// </summary>
        /// <returns></returns>
        public static int NewSysCode(nicklu2Entities db)
        {
            string code = "0";
            var m = db.tb_store_sys_code.FirstOrDefault(p => true);
            if (m != null)
            {
                code = m.SysCode;
                db.DeleteObject(m);
                db.SaveChanges();
            }
            else
                throw new Exception("store_sys_code is null");
            return int.Parse(code);
        }
        /// <summary>
        /// 取得新的订单号
        /// </summary>
        /// <returns></returns>
        public static int NewOrderCode(nicklu2Entities db)
        {
            int code = 0;
            var m = db.tb_store_order_code.FirstOrDefault(p => true);
            if (m != null)
            {
                code = m.OrderCode.Value;
                db.DeleteObject(m);
                db.SaveChanges();
            }
            else
                throw new Exception("store_order_code is null");
            
            return code;
        }

    }
}
