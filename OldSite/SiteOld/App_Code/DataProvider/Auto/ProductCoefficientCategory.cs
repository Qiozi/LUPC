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
/// Summary description for ProductCoefficientCategory
/// </summary>
[ActiveRecord("tb_product_coefficient_category")]
[Serializable]
public class ProductCoefficientCategory : ActiveRecordBase<ProductCoefficientCategory>
{
	public ProductCoefficientCategory()
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
    int _menu_child_serial_no;
    [Property]
    public int menu_child_serial_no
    {
        get { return _menu_child_serial_no; }
        set { _menu_child_serial_no = value; }
    }
    decimal _coefficient;
    [Property]
    public decimal coefficient
    {
        get { return _coefficient; }
        set { _coefficient = value; }
    }

    public static ProductCoefficientCategory GetProductCoefficientCategory(int _id)
    {
        ProductCoefficientCategory[] models = ProductCoefficientCategory.FindAllByProperty("id", _id);
        if (models.Length == 1)
            return models[0];
        else
            return new ProductCoefficientCategory();
    }

    public ProductCoefficientCategory[] FindModelsByCategoryID(int categoryID)
    {
        return ProductCoefficientCategory.FindAllByProperty("menu_child_serial_no", categoryID);
    }
}
