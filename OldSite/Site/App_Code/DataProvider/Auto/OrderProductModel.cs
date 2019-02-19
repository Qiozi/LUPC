// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	2007-6-4 23:45:26
//
// // // // // // // // // // // // // // // //
using LU.Data;
using System;
using System.Data;
using System.Linq;


[Serializable]
public class OrderProductModel 
{
   public static tb_order_product GetOrderProductModel(nicklu2Entities context, int id)
    {
        return context.tb_order_product.Single(me => me.serial_no.Equals(id));
    }

    public static tb_order_product[] GetModelsByOrderCode(nicklu2Entities context, string order_code)
    {
        // return OrderProductModel.FindAllByProperty("order_code", order_code);
        var query = context.tb_order_product.Where(me => me.order_code.Equals(order_code)).ToList().ToArray();
        return query;
    }

    public static tb_order_product[] GetModelsByProductCode(nicklu2Entities context, int product_serial_no)
    {
        // return OrderProductModel.FindAllByProperty("product_serial_no", product_serial_no);
        var query = context.tb_order_product.Where(me => me.product_serial_no.Value.Equals(product_serial_no)).ToList().ToArray();
        return query;
    }

    public static DataTable GetModelsBySearch(string order_code, product_category pc)
    {
        string sql = "select op.*, (op.order_product_sold * order_product_sum) subtotal_2  from tb_order_product op where 1=1";
        if (order_code != "")
            sql += " and order_code='" + order_code + "'";
        if (pc != product_category.entityAll)
            sql += " and product_type=" + Product_category_helper.product_category_value(pc);
        sql += " order by op.serial_no asc";
        return Config.ExecuteDataTable(sql);

    }

    /// <summary>
    /// 取得产品数量
    /// 系统当成一个产品。
    /// 如果一个笔记本数量是2，表示是两个产品
    /// 注：用于价格计算
    /// </summary>
    /// <param name="order_code"></param>
    /// <param name="pc"></param>
    /// <returns></returns>
    public static int GetPartSum(string order_code)
    {
        string sql = "select sum(order_product_sum) subtotal_2  from tb_order_product op where 1=1";
        if (order_code != "")
            sql += " and order_code='" + order_code + "'";

        DataTable dt = Config.ExecuteDataTable(sql);
        if (dt.Rows.Count == 1)
        {
            if (string.IsNullOrEmpty(dt.Rows[0][0].ToString()))
                return 0;
            return int.Parse(dt.Rows[0][0].ToString());
        }
        else
            return 0;
    }

    public static void DeleteByOrderCodeAndProduct(int product_id, int order_code)
    {
        string sql = "delete from tb_order_product where product_serial_no=" + product_id + " and order_code='" + order_code + "'";
        Config.ExecuteNonQuery(sql);
    }

    /// <summary>
    /// 删除产品数量为零
    /// </summary>
    /// <param name="product_id"></param>
    /// <param name="order_code"></param>
    public static void DeleteByOrderCodeAndZone(int order_code)
    {
        string sql = "delete from tb_order_product where order_product_sum=0 and order_code='" + order_code + "'";
        Config.ExecuteNonQuery(sql);
    }

    public static int GetProductCount(int order_code)
    {
        DataTable dt = Config.ExecuteDataTable("select sum(order_product_sum) from tb_order_product where order_code='" + order_code + "'");
        if (dt.Rows[0][0] != System.DBNull.Value)
            return int.Parse(dt.Rows[0][0].ToString());
        else
            return -1;
    }

    /// <summary>
    /// 根据系统变化价格
    /// </summary>
    /// <param name="order_code"></param>
    /// <param name="rate"></param>
    public static void ChangePriceByRate(int order_code, decimal rate)
    {
        Config.ExecuteNonQuery("Update tb_order_product set order_product_sold=order_product_sold*" + rate + " where order_code='" + order_code + "'");
    }

    public static void ChangePriceCost(int system_code, int order_code, decimal price, decimal cost)
    {
        Config.ExecuteNonQuery("update tb_order_product set order_product_sold='" + price.ToString() + "', order_product_price='" + price.ToString() + "', order_product_cost='" + cost.ToString() + "'  where product_serial_no=" + system_code + " and order_code='" + order_code + "'");
    }
    /// <summary>
    /// system list 
    /// </summary>
    /// <returns></returns>
    public DataTable FindModelsForSystemList(int startIndex,int pagesize,ref int count)
    {

        count = int.Parse(Config.ExecuteScalar(@"select count(op.serial_no)
from tb_order_product op inner join tb_order_helper oh on oh.order_code=op.order_code and oh.tag=1 and oh.Is_ok=1 
where length(product_serial_no)=8 and op.tag=1").ToString());

        return Config.ExecuteDataTable(string.Format(@"select op.product_serial_no system_templete_serial_no, op.order_code, op.product_name, oh.create_datetime
from tb_order_product op inner join tb_order_helper oh on oh.order_code=op.order_code and oh.tag=1 and oh.Is_ok=1 
where length(product_serial_no)=8 and op.tag=1 order by oh.create_datetime desc limit {0},{1}", startIndex, pagesize));

    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="keyword"></param>
    /// <returns></returns>
    public DataTable FindModelsForSystemList(string keyword, int startIndex, int pagesize, ref int count)
    {
        count = int.Parse(Config.ExecuteScalar(string.Format(@"select count(op.serial_no) from tb_order_product op inner join tb_order_helper oh on oh.order_code=op.order_code and oh.tag=1 and oh.Is_ok=1 
where length(product_serial_no)=8 and op.tag=1 and (op.order_code='{0}' or op.product_serial_no='{0}') ", keyword)).ToString());

        return Config.ExecuteDataTable(string.Format(@"select op.product_serial_no system_templete_serial_no, op.order_code, op.product_name, oh.create_datetime
from tb_order_product op inner join tb_order_helper oh on oh.order_code=op.order_code and oh.tag=1 and oh.Is_ok=1 
where length(product_serial_no)=8 and op.tag=1 and (op.order_code='{0}' or op.product_serial_no='{0}') order by oh.create_datetime desc limit {1},{2}", keyword, startIndex, pagesize));

    }
}
