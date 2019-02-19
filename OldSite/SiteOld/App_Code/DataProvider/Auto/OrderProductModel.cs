// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	2007-6-4 23:45:26
//
// // // // // // // // // // // // // // // //
using Castle.ActiveRecord;
using System;
using System.Data;


[ActiveRecord("tb_order_product")]
[Serializable]
public class OrderProductModel : ActiveRecordBase<OrderProductModel>
{
    int _serial_no;
    string _order_code;
    int _product_serial_no;
    int _order_product_sum;
    decimal _order_product_price;
    byte _tag;
    decimal _order_product_cost;
    string _sku;
    int _menu_pre_serial_no;
    int _menu_child_serial_no;
    int _product_type;
    string _product_name;
    decimal _order_product_sold;
    decimal _product_current_price_rate;
    string _product_type_name;
    string _ebayItemID;
    string _prodType;
    public OrderProductModel()
    {

    }


    /// <summary>
    /// 
    /// </summary>
    [PrimaryKey(PrimaryKeyType.Identity)]
    public int serial_no
    {
        get { return _serial_no; }
        set { _serial_no = value; }
    }
    public static OrderProductModel GetOrderProductModel(int _serial_no)
    {
        OrderProductModel[] models = OrderProductModel.FindAllByProperty("serial_no", _serial_no);
        if (models.Length == 1)
            return models[0];
        else
            return new OrderProductModel();
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public string order_code
    {
        get { return _order_code; }
        set { _order_code = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public int product_serial_no
    {
        get { return _product_serial_no; }
        set { _product_serial_no = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public int order_product_sum
    {
        get { return _order_product_sum; }
        set { _order_product_sum = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public decimal order_product_price
    {
        get { return _order_product_price; }
        set { _order_product_price = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public byte tag
    {
        get { return _tag; }
        set { _tag = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public decimal order_product_cost
    {
        get { return _order_product_cost; }
        set { _order_product_cost = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public string sku
    {
        get { return _sku; }
        set { _sku = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public int menu_pre_serial_no
    {
        get { return _menu_pre_serial_no; }
        set { _menu_pre_serial_no = value; }
    }
    /// <summary>
    ///
    /// </summary>
    [Property]
    public int menu_child_serial_no
    {
        get { return _menu_child_serial_no; }
        set { _menu_child_serial_no = value; }
    }

    [Property]
    public int product_type
    {
        get { return _product_type; }
        set { _product_type = value; }
    }
    
    [Property]
    public string product_name
    {
        get { return _product_name; }
        set { _product_name = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public decimal order_product_sold
    {
        get { return _order_product_sold; }
        set { _order_product_sold = value; }
    }
    /// <summary>
    /// 
    /// </summary>
    [Property]
    public decimal product_current_price_rate
    {
        get { return _product_current_price_rate; }
        set { _product_current_price_rate = value; }
    }
    [Property]
    public string product_type_name
    {
        get { return _product_type_name; }
        set { _product_type_name = value; }
    }
    [Property]
    public string ebayItemID
    {
        get { return _ebayItemID; }
        set { _ebayItemID = value; }
    }
    [Property]
    public string prodType
    {
        get { return _prodType; }
        set { _prodType = value; }
    }

    public static OrderProductModel[] GetModelsByOrderCode(string order_code)
    {
        return OrderProductModel.FindAllByProperty("order_code", order_code);
    }

    public static OrderProductModel[] GetModelsByProductCode(int product_serial_no)
    {
        return OrderProductModel.FindAllByProperty("product_serial_no", product_serial_no);
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
