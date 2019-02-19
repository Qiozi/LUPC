using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LU.BLL
{
    public class OrderProvider
    {
        /// <summary>
        /// 保存订单中的客户留言信息
        /// </summary>
        /// <returns></returns>
        public static bool SaveMessageOnOrder(Data.nicklu2Entities context, string msg, int orderCode)
        {
            OrderHelper.SaveOrderNote(msg
                           , orderCode
                           , false
                           , context);
            return true;
        }
    }
}
