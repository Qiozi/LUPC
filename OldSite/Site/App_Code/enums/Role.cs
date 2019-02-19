using System;
using System.Data;

/// <summary>
/// Role 的摘要说明
/// </summary>
public class RoleHelper
{
    public RoleHelper()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
    }
    public static DataTable RoleToDataTable()
    {
        return PropertiesAttributeHelper.EnumToDatatable(typeof(Role));
    }

    public static int Rolevalue(Role ct)
    {
        foreach (int i in Enum.GetValues(typeof(Role)))
        {
            if (Enum.GetName(typeof(Role), i) == ct.ToString())
            {
                return i;
            }
        }
        return -1;
    }

    public static Role GetRoleByValue(int id)
    {
        try
        {
            return (Role)Enum.Parse(typeof(Role), Enum.GetName(typeof(Role), id));
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
}

public enum Role
{
    /// <summary>
    /// Product Manage
    /// </summary>
    [PropertiesAttribute("product_manage", "Product Manage")]
    product_manage = 1,

    /// <summary>
    /// Parts Group Settings
    /// </summary>
    [PropertiesAttribute("parts_group_settings", "Parts Group Settings")]
    parts_group_settings = 2,
    /// <summary>
    /// State & Shipping Company & Product Size
    /// </summary>
    [PropertiesAttribute("shipping_settings", "State & Shipping Company & Product Size")]
    shipping_settings = 3,

    /// <summary>
    /// Configure Helper
    /// </summary>
    [PropertiesAttribute("configure_helper", "Configure Helper")]
    configure_helper = 4,

    /// <summary>
    /// Product Category Settings
    /// </summary>
    [PropertiesAttribute("product_category_settings", "Product Category Settings")]
    product_category_settings = 5,



    /// <summary>
    /// Search Keywords Manage
    /// </summary>
    [PropertiesAttribute("search_keywords_manage", "Search Keywords Manage")]
    search_keywords_manage = 7,

    /// <summary>
    /// Promotion & Rebate
    /// </summary>
    [PropertiesAttribute("sales_promotion_rebate", "Promotion & Rebate")]
    sales_promotion_rebate = 8,

    /// <summary>
    /// Check Out
    /// </summary>
    [PropertiesAttribute("check_out", "Check Out")]
    check_out = 9,

    /// <summary>
    /// Order List
    /// </summary>
    [PropertiesAttribute("Order_list", "Order List")]
    order_list = 10,

    /// <summary>
    /// add order
    /// </summary>
    [PropertiesAttribute("add_order", "add order")]
    add_order = 11,

    /// <summary>
    /// Purchase
    /// </summary>
    [PropertiesAttribute("Purchase", "Purchase")]
    Purchase = 12,
    /// <summary>
    /// Question  
    /// </summary>
    [PropertiesAttribute("Question ", "Question ")]
    Question = 13,

    /// <summary>
    /// Import product  
    /// </summary>
    [PropertiesAttribute("import_product ", "Import product ")]
    import_product = 14,
    /// <summary>
    /// import_product_settings  
    /// </summary>
    [PropertiesAttribute("import_product_settings ", "import product settings ")]
    import_product_settings = 15,

    /// <summary>
    /// default  
    /// </summary>
    [PropertiesAttribute("default_index_page", "default index page")]
    default_index_page = 16,


    /// <summary>
    /// New & Feedback  
    /// </summary>
    [PropertiesAttribute("news_feedback", "New & Feedback")]
    news_feedback = 17,

    /// <summary>
    /// Track
    /// </summary>
    [PropertiesAttribute("Track", "Track")]
    Track = 18
}