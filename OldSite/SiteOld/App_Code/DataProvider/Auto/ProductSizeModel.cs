// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	2007-6-10 19:50:50
//
// // // // // // // // // // // // // // // //
using Castle.ActiveRecord;
using System;
using System.Data;

[ActiveRecord("tb_product_size")]
[Serializable]
public class ProductSizeModel : ActiveRecordBase<ProductSizeModel>
{
    int _product_size_id;
    string _product_size_name;
    decimal _begin_price;
    decimal _end_price;

    public ProductSizeModel()
    {

    }


    /// <summary>
    /// 
    /// </summary>
    [PrimaryKey(PrimaryKeyType.Identity)]
    public int product_size_id
    {
        get { return _product_size_id; }
        set { _product_size_id = value; }
    }
    public static ProductSizeModel GetProductSizeModel(int _product_size_id)
    {
        ProductSizeModel[] models = ProductSizeModel.FindAllByProperty("product_size_id", _product_size_id);
        if (models.Length == 1)
            return models[0];
        else
            return new ProductSizeModel();
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public string product_size_name
    {
        get { return _product_size_name; }
        set { _product_size_name = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public decimal begin_price
    {
        get { return _begin_price; }
        set { _begin_price = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public decimal end_price
    {
        get { return _end_price; }
        set { _end_price = value; }
    }

    public static DataTable GetModelByPrice(decimal price, product_category pc)
    {
        string sql = "select * from tb_product_size where  "+price+" between begin_price and end_price";
        if (pc != product_category.entityAll)
            sql += " and product_type = " + Product_category_helper.product_category_value(pc);
        return Config.ExecuteDataTable(sql);
    }
}
