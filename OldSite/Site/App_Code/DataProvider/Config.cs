using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using MySql.Data;
using MySql.Data.MySqlClient;

/// <summary>
/// Config 的摘要说明
/// </summary>
public class Config
{
    public Config()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }
    /// <summary>
    /// 网站根目录
    /// </summary>
    public static string AppPath { get; set; }

    public static string ConnString
    {
        get { return ConfigurationManager.ConnectionStrings["LU"].ConnectionString.ToString(); }
    }

    public static string ExcelConnstring(string filename)
    {
        return "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filename + ";Extended Properties=Excel 8.0;";
    }
    /// <summary>
    /// 2. 美国，1. 加拿大，法语
    /// </summary>
    public static int SystemCategory
    {
        get { return 1; }
    }



    public static string defaultPassword { get { return ConfigurationManager.AppSettings["defaultPassword"].ToString(); } }

    public static int product_size { get { return int.Parse(ConfigurationManager.AppSettings["sizeXXL"].ToString()); } }

    public static DataTable ExecuteDataTable(string sql)
    {
        DataTable dt = new DataTable();
        using (MySqlConnection conn = new MySqlConnection(ConnString))
        {
            MySqlDataAdapter adapter = new MySqlDataAdapter(sql, conn);
            conn.Open();
            adapter.Fill(dt);
            conn.Close();
        }
        return dt;
    }

    public static int ExecuteNonQuery(string sql)
    {
        int count = -1;

        using (MySqlConnection conn = new MySqlConnection(ConnString))
        {
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            conn.Open();
            MySqlTransaction mt = conn.BeginTransaction();
            try
            {
                count = cmd.ExecuteNonQuery();
                mt.Commit();

            }
            catch (Exception ex)
            {
                mt.Rollback();
                throw ex;
            }
            conn.Close();
        }

        return count;
    }

    public static object ExecuteScalar(string sql)
    {
        object o = -1;
        using (MySqlConnection conn = new MySqlConnection(ConnString))
        {
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            conn.Open();
            o = cmd.ExecuteScalar();
            conn.Close();
        }
        return o;
    }

    public static int ExecuteScalarInt32(string sql)
    {
        return int.Parse(ExecuteScalar(sql).ToString());
    }

    public static DataTable GetTestDT()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("name");
        dt.Columns.Add("nums");

        for (int i = 0; i < 5; i++)
        {
            DataRow dr = dt.NewRow();
            dr[0] = 1;
            dr[1] = 2;
            dt.Rows.Add(dr);
        }
        return dt;
    }

    //public static int sixCode
    //{
    //    get
    //    {
    //        Random rnd = new Random();
    //        return rnd.Next(100000, 999999);
    //    }
    //}

    //public static int eightCode
    //{
    //    get
    //    {
    //        Random rnd = new Random();
    //        return rnd.Next(10000000, 99999999);
    //    }
    //}

    public static string ConvertPrice(decimal price)
    {
        return price.ToString("$###,###.00");
    }

    public static string ConvertPrice2(decimal price)
    {
        return price.ToString("###,###.00");
    }

    public static string GetDateTimeString
    {
        get { return DateTime.Now.ToString("yyyyMMddhhmmss"); }
    }

    public static int OrderInvoiceLength
    {
        get { return 7; }
    }

    /// <summary>
    /// 美国Ground运输公司ID
    /// </summary>
    public static int GroundCompanyID
    {
        get { return int.Parse(ConfigurationManager.AppSettings["groundCompany"].ToString()); }
    }

    /// <summary>
    /// 美国Ground运输入公司最大金额
    /// </summary>
    public static decimal GroundCompanyTotal
    {
        get { return decimal.Parse(ConfigurationManager.AppSettings["groundCompanyTotal"].ToString()); }
    }

    /// <summary>
    /// 默认对外状态值
    /// </summary>
    public static byte DefaultOutStatus
    {
        get { return byte.Parse(ConfigurationManager.AppSettings["out_status"].ToString()); }
    }
    /// <summary>
    /// 默认的后台支付方式
    /// </summary>
    public static int pay_method_card
    {
        get { return int.Parse(ConfigurationManager.AppSettings["pay_method_card_id"].ToString()); }
    }

    /// <summary>
    /// 免税洲ID
    /// </summary>
    public static int tax_execmtion_state
    {
        get { return int.Parse(ConfigurationManager.AppSettings["tax_execmtion_state"].ToString()); }
    }

    /// <summary>
    /// 免税洲减掉百分比          
    /// </summary>
    public static byte tax_execmtion_state_save_money
    {
        get { return byte.Parse(ConfigurationManager.AppSettings["tax_execmtion_state_save_money"].ToString()); }
    }

    public static string army_state
    {
        get { return ConfigurationManager.AppSettings["army_state"].ToString(); }
    }

    public static int army_shipping_company
    {
        get { return int.Parse(ConfigurationManager.AppSettings["army_shipping_company"].ToString()); }
    }

    public static decimal is_card_rate
    {
        get { return decimal.Parse(ConfigurationManager.AppSettings["is_card_rate"].ToString()); }
    }

    public static string pay_method_pick_up_ids
    {
        get { return ConfigurationManager.AppSettings["pay_method_pick_up_ids"].ToString(); }
    }

    public static int pay_method_pick_up_id_default
    {
        get { return int.Parse(ConfigurationManager.AppSettings["pay_method_pick_up_id_default"].ToString()); }
    }

    /// <summary>
    /// CPU logo图片路径
    /// </summary>
    public static string cpu_logo_image_path
    {
        get { return ConfigurationManager.AppSettings["logo_image_path"].ToString(); }
    }

    /// <summary>
    /// 订单生成pdf文件所放的相对路径
    /// </summary>
    public static string path_pdf_order
    {
        get { return ConfigurationManager.AppSettings["path_pdf_order"].ToString(); }
    }

    /// <summary>
    /// HST TAX
    /// </summary>
    public static string tax_hsts
    {
        get { return ConfigurationManager.AppSettings["tax_hst"].ToString(); }
    }

    /// <summary>
    /// 发票的头两位
    /// </summary>
    public static int invoice_head
    {
        get { return int.Parse(ConfigurationManager.AppSettings["invoice_head"].ToString()); }
    }
    /// <summary>
    /// 发票上客户编号前两位
    /// </summary>
    public static int invoice_customer_head
    {
        get { return int.Parse(ConfigurationManager.AppSettings["invoice_customer_head"].ToString()); }
    }

    /// <summary>
    /// jmail 
    /// </summary>
    public static string mailUserName
    {
        get { return ConfigurationManager.AppSettings["mailUserName"].ToString(); }
    }
    /// <summary>
    /// jmail 
    /// </summary>
    public static string mailPassword
    {
        get { return ConfigurationManager.AppSettings["mailPassword"].ToString(); }
    }
    /// <summary>
    /// jmail 
    /// </summary>
    public static string mailServer
    {
        get { return ConfigurationManager.AppSettings["mailServer"].ToString(); }
    }

    /// <summary>
    /// update excel file store path 
    /// </summary>
    public static string update_product_data_excel_path
    {
        get { return ConfigurationManager.AppSettings["update_product_data_excel_path"].ToString(); }
    }


    // // // // // /// // // // // // 
    //
    //   update database 
    //
    // // // // // // // // // // //
    public static string update_table_name
    {
        get { return "tb_update_product_store"; }
    }

    public static string product_table_name
    {
        get { return "tb_product"; }
    }


    public static string StoreProductRebatePdfPath
    {
        get { return ConfigurationManager.AppSettings["StoreProductRebatePdfPath"].ToString(); }
    }

    public static string back_order_status
    { get { return ConfigurationManager.AppSettings["back_order_status"].ToString(); } }

    public static string porder_order_status
    {
        get { return ConfigurationManager.AppSettings["porder_order_status"].ToString(); }
    }

    public static string new_order_status
    {
        get { return ConfigurationManager.AppSettings["new_order_status"].ToString(); }
    }
    public static string pay_method_use_card_rate
    {
        get { return ConfigurationManager.AppSettings["pay_method_use_card_rate"].ToString(); }
    }
    /// <summary>
    /// paymethod id -- paypal
    /// </summary>
    public static string pay_method_paypal
    {
        get { return ConfigurationManager.AppSettings["pay_method_paypal"].ToString(); }
    }

    /// <summary>
    /// it is login redirect path.
    /// </summary>
    public static string login_redirect_path
    {
        get { return ConfigurationManager.AppSettings["login_redirect_path"].ToString(); }
    }
    /// <summary>
    /// order list top count default value
    /// </summary>
    public static int order_list_top_count_default_value
    {
        get { return int.Parse(ConfigurationManager.AppSettings["order_list_top_count_default_value"].ToString()); }
    }

    public static int system_product_sku_length
    {
        get { return int.Parse(ConfigurationManager.AppSettings["system_product_sku_length"].ToString()); }
    }

    public static int default_order_list_record_count
    {
        get { return int.Parse(ConfigurationManager.AppSettings["default_order_list_record_count"].ToString()); }
    }


    public static string http_domain
    {
        get { return ConfigurationManager.AppSettings["http_domain"].ToString(); }
    }

    public static string Part_Comment_Path
    {
        get { return ConfigurationManager.AppSettings["Part_Comment_Path"].ToString(); }
    }

    public static string notStatOrderStatus
    {
        get { return ConfigurationManager.AppSettings["notStatOrderStatus"].ToString(); }
    }
    public static string notStatOrderStatus_back_status
    {
        get { return ConfigurationManager.AppSettings["notStatOrderStatus_back_status"].ToString(); }
    }

    public static string other_inc_id_watch
    {
        get { return ConfigurationManager.AppSettings["other_inc_id_watch"].ToString(); }
    }

    public static string sale_promotion_compay_id
    {
        get { return ConfigurationManager.AppSettings["sale_promotion_compay_id"].ToString(); }
    }

    public static string order_complete
    {
        get { return ConfigurationManager.AppSettings["order_complete"].ToString(); }
    }
    public int other_category_id
    {
        get { return int.Parse(ConfigurationManager.AppSettings["other_category_id"].ToString()); }
    }

    public static string part_img_path
    {
        get { return string.Concat(http_domain, ConfigurationManager.AppSettings["part_img_path"].ToString()); }
    }

    public static string ProImgBasePath
    {
        get { return ConfigurationManager.AppSettings["ProImgBasePath"].ToString(); }
    }

    public string do_not_watch_category_ids
    {
        get { return ConfigurationManager.AppSettings["do_not_watch_category_ids"].ToString(); }
    }


    public static string pay_method_credit_ids
    {
        get { return ConfigurationManager.AppSettings["pay_method_credit_ids"].ToString(); }
    }

    /// <summary>
    /// 可用于本机测试等方面
    /// </summary>
    public static bool isLocalhost
    {
        get { return http_domain.ToLower().IndexOf("localhost") != -1; }
    }

    public static string notShowOnSysListSKUs
    {
        get { return ConfigurationManager.AppSettings["notShowOnSysListSKUs"].ToString(); }
    }

    /// <summary>
    /// 
    /// </summary>
    public static int PartCategoryIsNotebook
    {
        get { return int.Parse(ConfigurationManager.AppSettings["PartCategoryIsNotebook"].ToString()); }
    }

    public static int part_motherboard_ID
    {
        get { return int.Parse(ConfigurationManager.AppSettings["part_motherboard_ID"].ToString()); }
    }

    public static decimal OtherCountryOneNotebookShippingCharge
    {
        get { return decimal.Parse(ConfigurationManager.AppSettings["OtherCountryOneNotebookShippingCharge"].ToString()); }
    }

    /// <summary>
    /// 系统里没有显卡，主板也不集成，显卡使用的ID
    /// 
    /// </summary>
    public static int noVideoCardSku = 25700;


    public static string etcSysListPath = "~/ebay_page_store/system/";
}

