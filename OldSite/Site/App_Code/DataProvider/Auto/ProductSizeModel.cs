// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	2007-6-10 19:50:50
//
// // // // // // // // // // // // // // // //
using LU.Data;
using System;
using System.Linq;
using System.Data;

[Serializable]
public class ProductSizeModel
{
    public static tb_product_size GetProductSizeModel(nicklu2Entities context, int id)
    {
        return context.tb_product_size.Single(me => me.product_size_id.Equals(id));
    }

    public static DataTable GetModelByPrice(decimal price, product_category pc)
    {
        string sql = "select * from tb_product_size where  " + price + " between begin_price and end_price";
        if (pc != product_category.entityAll)
            sql += " and product_type = " + Product_category_helper.product_category_value(pc);
        return Config.ExecuteDataTable(sql);
    }
}
