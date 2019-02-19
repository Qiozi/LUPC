using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DownloadEBayOrder.BLL
{
    public class NewCustomerCode
    {
        /// <summary>
        /// 取得新的客户编号
        /// </summary>
        /// <returns></returns>
        public static string GetNewCustomerCode(nicklu2Entities context)
        {
            string custCode = "";
            var code = context.tb_store_customer_code.FirstOrDefault(c => c.ID > 0);
            if (code != null)
            {
                custCode = code.CustomerCode;
                context.tb_store_customer_code.Remove(code);
            }
            return custCode;
        }
    }
}
