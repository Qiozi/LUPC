// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	2007-5-27 14:09:12
//
// // // // // // // // // // // // // // // //
using Castle.ActiveRecord;
using System;

[ActiveRecord("tb_product_in")]
[Serializable]
public class ProductInModel : ActiveRecordBase<ProductInModel>
{
    int _product_in_serial_no;
    double _product_in_cost;
    int _product_serial_no;
    DateTime _product_in_date;
    string _purchase_serial_no;
    int _product_in_staff;
    double _product_in_price;
    int _product_in_sum;
    DateTime _product_in_end_date;
    double _tag;
    string _product_sns;
    int _system_category_serial_no;
    int _vendor_serial_no;

    public ProductInModel()
    {

    }


    /// <summary>
    /// 
    /// </summary>
    [PrimaryKey(PrimaryKeyType.Identity)]
    public int product_in_serial_no
    {
        get { return _product_in_serial_no; }
        set { _product_in_serial_no = value; }
    }
    public static ProductInModel GetProductInModel(int _product_in_serial_no)
    {
        ProductInModel[] models = ProductInModel.FindAllByProperty("product_in_serial_no", _product_in_serial_no);
        if (models.Length == 1)
            return models[0];
        else
            return new ProductInModel();
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public double product_in_cost
    {
        get { return _product_in_cost; }
        set { _product_in_cost = value; }
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
    public DateTime product_in_date
    {
        get { return _product_in_date; }
        set { _product_in_date = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public string purchase_serial_no
    {
        get { return _purchase_serial_no; }
        set { _purchase_serial_no = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public int product_in_staff
    {
        get { return _product_in_staff; }
        set { _product_in_staff = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public double product_in_price
    {
        get { return _product_in_price; }
        set { _product_in_price = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public int product_in_sum
    {
        get { return _product_in_sum; }
        set { _product_in_sum = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public DateTime product_in_end_date
    {
        get { return _product_in_end_date; }
        set { _product_in_end_date = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public double tag
    {
        get { return _tag; }
        set { _tag = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public string product_sns
    {
        get { return _product_sns; }
        set { _product_sns = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public int system_category_serial_no
    {
        get { return _system_category_serial_no; }
        set { _system_category_serial_no = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public int vendor_serial_no
    {
        get { return _vendor_serial_no; }
        set { _vendor_serial_no = value; }
    }

    public static ProductInModel[] GetProductInModelsByPurchase(int purchase_serial_no)
    {
        return ProductInModel.FindAllByProperty("purchase_serial_no", purchase_serial_no);
    }
}
