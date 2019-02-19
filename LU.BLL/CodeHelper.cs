using LU.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LU.BLL
{
    public class CodeHelper
    {
        /// <summary>
        /// 新的客户编号
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        public static int NewCustomerCode(nicklu2Entities db)
        {
            int code = 0;
            var c = db.tb_store_customer_code.FirstOrDefault(p => true);
            if (c != null)
                code = int.Parse(c.CustomerCode);
            db.tb_store_customer_code.Remove(c);
            db.SaveChanges();
            return code;
        }

        /// <summary>
        /// 新的系统号
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        public static int NewSysCode(nicklu2Entities db)
        {
            int code = 0;
            var c = db.tb_store_sys_code.FirstOrDefault(p => true);
            if (c != null)
                code = int.Parse(c.SysCode);
            db.tb_store_sys_code.Remove(c);
            db.SaveChanges();
            return code;
        }

        /// <summary>
        /// 新的订单号
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        public static int NewOrderCode(nicklu2Entities db)
        {

            int code = 0;// socm.OrderCode;
            var c = db.tb_store_order_code.FirstOrDefault(p => true);
            if (c != null)
                code = c.OrderCode.Value;
            db.tb_store_order_code.Remove(c);
            db.SaveChanges();

            if (db.tb_customer_store.FirstOrDefault(p => p.order_code.HasValue
                && p.order_code.Value.Equals(code)) == null)
                return code;
            else
                return NewOrderCode(db);
        }
    }
}
