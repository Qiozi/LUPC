
// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	2/11/2009 10:31:12 AM
//
// // // // // // // // // // // // // // // //
using Castle.ActiveRecord;
using System;
using System.Data;

[ActiveRecord("tb_product_import_price")]
[Serializable]
public class ProductImportPriceModel : ActiveRecordBase<ProductImportPriceModel>
{

    public ProductImportPriceModel()
    {

    }

    public static ProductImportPriceModel GetProductImportPriceModel(int _id)
    {
        ProductImportPriceModel[] models = ProductImportPriceModel.FindAllByProperty("id", _id);
        if (models.Length == 1)
            return models[0];
        else
            return new ProductImportPriceModel();
    }

    int _id;

    [PrimaryKey(PrimaryKeyType.Identity)]
    public int id
    {
        get { return _id; }
        set { _id = value; }
    }

    string _other_inc_name;

    [Property]
    public string other_inc_name
    {
        get { return _other_inc_name; }
        set { _other_inc_name = value; }
    }

    int _luc_sku;

    [Property]
    public int luc_sku
    {
        get { return _luc_sku; }
        set { _luc_sku = value; }
    }

    decimal _part_real_cost;

    [Property]
    public decimal part_real_cost
    {
        get { return _part_real_cost; }
        set { _part_real_cost = value; }
    }

    decimal _part_sell;

    [Property]
    public decimal part_sell
    {
        get { return _part_sell; }
        set { _part_sell = value; }
    }

    DateTime _part_real_regdate;

    [Property]
    public DateTime part_real_regdate
    {
        get { return _part_real_regdate; }
        set { _part_real_regdate = value; }
    }

    string _part_name;

    [Property]
    public string part_name
    {
        get { return _part_name; }
        set { _part_name = value; }
    }

    int _part_quantity;

    [Property]
    public int part_quantity
    {
        get { return _part_quantity; }
        set { _part_quantity = value; }
    }

    string _vendor_invoice;

    [Property]
    public string vendor_invoice
    {
        get { return _vendor_invoice; }
        set { _vendor_invoice = value; }
    }

    string _order_codes;

    [Property]
    public string order_codes
    {
        get { return _order_codes; }
        set { _order_codes = value; }
    }

    bool _is_order;

    [Property]
    public bool is_order
    {
        get { return _is_order; }
        set { _is_order = value; }
    }

    string _note;

    [Property]
    public string note
    {
        get { return _note; }
        set { _note = value; }
    }


}

