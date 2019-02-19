// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	2008-08-05 19:50:14
//
// // // // // // // // // // // // // // // //
using Castle.ActiveRecord;
using System;
using System.Data;

/// <summary>
/// Summary description for ProductCoefficientPart
/// </summary>
[ActiveRecord("tb_product_coefficient_part")]
[Serializable]
public class ProductCoefficientPart : ActiveRecordBase<ProductCoefficientPart>
{
	public ProductCoefficientPart()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    int _id;
    [PrimaryKey(PrimaryKeyType.Identity)]
    public int id
    {
        get { return _id; }
        set { _id = value; }
    }
    int _product_serial_no;
    [Property]
    public int product_serial_no
    {
        get { return _product_serial_no; }
        set { _product_serial_no = value; }
    }
    decimal _coefficient;
    [Property]
    public decimal coefficient
    {
        get { return _coefficient; }
        set { _coefficient = value; }
    }

    public static ProductCoefficientPart GetProductCoefficientPart(int _id)
    {
        ProductCoefficientPart[] models = ProductCoefficientPart.FindAllByProperty("id", _id);
        if (models.Length == 1)
            return models[0];
        else
            return new ProductCoefficientPart();
    }
}
