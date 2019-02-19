
// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	12/7/2008 12:19:03 PM
//
// // // // // // // // // // // // // // // //
using Castle.ActiveRecord;
using System;
using System.Data;

[ActiveRecord("tb_product_shipping_fee")]
[Serializable]
public class ProductShippingFeeModel : ActiveRecordBase<ProductShippingFeeModel>
{

    public ProductShippingFeeModel()
    {

    }

    public static ProductShippingFeeModel GetProductShippingFeeModel(int _prod_shipping_fee_id)
    {
        ProductShippingFeeModel[] models = ProductShippingFeeModel.FindAllByProperty("prod_shipping_fee_id", _prod_shipping_fee_id);
        if (models.Length == 1)
            return models[0];
        else
            return new ProductShippingFeeModel();
    }

    int _prod_shipping_fee_id;

    [PrimaryKey(PrimaryKeyType.Identity)]
    public int prod_shipping_fee_id
    {
        get { return _prod_shipping_fee_id; }
        set { _prod_shipping_fee_id = value; }
    }

    int _prod_Sku;

    [Property]
    public int prod_Sku
    {
        get { return _prod_Sku; }
        set { _prod_Sku = value; }
    }

    bool _is_system;

    [Property]
    public bool is_system
    {
        get { return _is_system; }
        set { _is_system = value; }
    }

    decimal _shipping_fee_us;

    [Property]
    public decimal shipping_fee_us
    {
        get { return _shipping_fee_us; }
        set { _shipping_fee_us = value; }
    }

    decimal _shipping_fee_ca;

    [Property]
    public decimal shipping_fee_ca
    {
        get { return _shipping_fee_ca; }
        set { _shipping_fee_ca = value; }
    }


}

