// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	2007-6-4 23:45:26
//
// // // // // // // // // // // // // // // //
using LU.Data;
using System;
using System.Linq;

[Serializable]
public class OrderProductHistoryModel
{

    /// <summary>
    /// 
    /// </summary>
    /// <param name="order_code"></param>
    /// <returns></returns>
    public static tb_order_product_history[] FindModelsByOrder(nicklu2Entities context, string order_code)
    {
        return context.tb_order_product_history.Where(me => me.order_code.Equals(order_code)).ToList().ToArray();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="order_code"></param>
    /// <returns></returns>
    public static int FindModelsCountByOrder(string order_code)
    {
        return Config.ExecuteScalarInt32(string.Format("select count(serial_no) from tb_order_product_history where order_code='{0}'", order_code));
    }
}
