using System;
using System.Data;

/// <summary>
/// Product_category 的摘要说明
/// </summary>
public class Product_category_helper
{
    public Product_category_helper()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    public static DataTable product_category_ToDataTable()
    {
        return PropertiesAttributeHelper.EnumToDatatable(typeof(product_category));
    }

    public static int product_category_value(product_category ct)
    {
        foreach (int i in Enum.GetValues(typeof(product_category)))
        {
            if (Enum.GetName(typeof(product_category), i) == ct.ToString())
            {
                return i;
            }
        }
        return -1;
    }

    public static product_category GetProductCategoryByValue(int id)
    {
        try
        {
            return (product_category)Enum.Parse(typeof(product_category), Enum.GetName(typeof(product_category), id));
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

}

public enum product_category
{
    /// <summary>
    /// Part Product
    /// </summary>
    [PropertiesAttribute("part_product", "Part Product")]
    part_product =1,
    /// <summary>
    /// System Product
    /// </summary>
    [PropertiesAttribute("system_product", "System Product")]
    system_product =2,
    /// <summary>
    /// NoeBooks
    /// </summary>
    [PropertiesAttribute("noebooks", "NoeBooks")]
    noebooks =3,
    [PropertiesAttribute("system_product_virtual", "System Product")]
    system_virtual=4,

    [PropertiesAttribute("part_product_virtual", "part_product_virtual")]
    part_product_virtual = 5,
    [PropertiesAttribute("parent", "parent")]
    parent = 100,
    [PropertiesAttribute("AllAll", "AllAll")]
    AllAll = 200,
    /// <summary>
    /// all
    /// </summary>
    [PropertiesAttribute("all", "all")]
    entityAll = 999
}
