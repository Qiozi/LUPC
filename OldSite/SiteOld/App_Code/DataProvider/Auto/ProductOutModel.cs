// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	2007-6-26 22:20:23
//
// // // // // // // // // // // // // // // //
using Castle.ActiveRecord;
using System;

[ActiveRecord("tb_product_out")]
[Serializable]
public class ProductOutModel : ActiveRecordBase<ProductOutModel>
{
    int _product_out_serial_no;
    int _product_serial_no;
    DateTime _product_out_date;
    int _product_out_sale_staff;
    int _product_out_staff;
    DateTime _product_out_end_date;
    decimal _product_detail_out_price;
    int _product_out_client;
    string _product_out_receipt;
    int _system_category_serial_no;
    DateTime _create_datetime;
    string _product_sn;

    public ProductOutModel()
    {

    }


    /// <summary>
    /// 
    /// </summary>
    [PrimaryKey(PrimaryKeyType.Identity)]
    public int product_out_serial_no
    {
        get { return _product_out_serial_no; }
        set { _product_out_serial_no = value; }
    }
    public static ProductOutModel GetProductOutModel(int _product_out_serial_no)
    {
        ProductOutModel[] models = ProductOutModel.FindAllByProperty("product_out_serial_no", _product_out_serial_no);
        if (models.Length == 1)
            return models[0];
        else
            return new ProductOutModel();
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
    public DateTime product_out_date
    {
        get { return _product_out_date; }
        set { _product_out_date = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public int product_out_sale_staff
    {
        get { return _product_out_sale_staff; }
        set { _product_out_sale_staff = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public int product_out_staff
    {
        get { return _product_out_staff; }
        set { _product_out_staff = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public DateTime product_out_end_date
    {
        get { return _product_out_end_date; }
        set { _product_out_end_date = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public decimal product_detail_out_price
    {
        get { return _product_detail_out_price; }
        set { _product_detail_out_price = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public int product_out_client
    {
        get { return _product_out_client; }
        set { _product_out_client = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public string product_out_receipt
    {
        get { return _product_out_receipt; }
        set { _product_out_receipt = value; }
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
    public DateTime create_datetime
    {
        get { return _create_datetime; }
        set { _create_datetime = value; }
    }
    [Property]
    public string product_sn
    {
        get { return _product_sn; }
        set { _product_sn = value; }
    }
}
