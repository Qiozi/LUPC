using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Q_Admin_product_part_download : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitialDatabase();
        }
    }
    protected void btn_download_Click(object sender, EventArgs e)
    {
        DataTable dt = Config.ExecuteDataTable("select product_serial_no sku, manufacturer_part_number mfp, product_name middle_name, round(((product_current_price-product_current_discount) / 1.022),2) price from tb_product where tag=1 and split_line=0 and is_non=0 and issue=1 and menu_child_serial_no in (" + new GetAllValidCategory().ToString() + ") ");
        ExcelHelper eh = new ExcelHelper(dt);
        eh.FileName = "all_part.xls";
        eh.MaxRecords = 20000;
        eh.Export();
    }
}