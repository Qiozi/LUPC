// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	2007-9-12 23:30:44
//
// // // // // // // // // // // // // // // //
using LU.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

[Serializable]
public class CustomerStoreModel
{

    public static tb_customer_store GetCustomerStoreModel(nicklu2Entities context, int serial)
    {
        return context.tb_customer_store.Single(me => me.serial_no.Equals(serial));
    }

    public static tb_customer_store[] FindModelsByOrderCode(nicklu2Entities context, string order_code)
    {
        var oc = 0;
        int.TryParse(order_code, out oc);
        // return CustomerStoreModel.FindAllByProperty("order_code", order_code);
        return context.tb_customer_store.Where(me => me.order_code.Value.Equals(oc)).ToList().ToArray();
    }
    /// <summary>
    /// 查找订单用户信息
    /// </summary>
    /// <param name="order_code"></param>
    /// <returns></returns>
    public static tb_customer_store FindByOrderCode(nicklu2Entities context, string order_code)
    {
        var oc = 0;
        int.TryParse(order_code, out oc);
        //CustomerStoreModel[] ms = CustomerStoreModel.FindAllByProperty("order_code", order_code);
        //if (ms != null)
        //    return ms[0];
        //return null;
        return context.tb_customer_store.FirstOrDefault(me => me.order_code.Value.Equals(oc));
    }

    /// <summary>
    /// 取得客户所有的订单编号
    /// 
    /// </summary>
    /// <param name="customer_serial_no"></param>
    /// <returns></returns>
    public static List<KeyValuePair<string, string>> GetOrders(string customer_serial_no)
    {
        List<KeyValuePair<string, string>> orders = new List<KeyValuePair<string, string>>();
        if (string.IsNullOrEmpty(customer_serial_no))
            return orders;

        DataTable dt = Config.ExecuteDataTable("select order_code,date_format(store_create_datetime, '%Y-%d-%m') store_create_datetime from tb_customer_store where customer_serial_no='" + customer_serial_no + "'");
        foreach (DataRow dr in dt.Rows)
        {
            KeyValuePair<string, string> k = new KeyValuePair<string, string>(dr["order_code"].ToString(), dr["store_create_datetime"].ToString());
            orders.Add(k);
        }
        return orders;
    }
}
