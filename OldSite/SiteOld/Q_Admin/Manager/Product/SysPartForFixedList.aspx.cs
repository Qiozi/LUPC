using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Q_Admin_Manager_Product_SysPartForFixedList : PageBase
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitialDatabase();
            BindData();
        }
    }
    void BindData()
    {
        var query = Config.ExecuteDataTable(@"Select product_serial_no Sku, product_ebay_name Name
, product_current_price-product_current_discount Price
, product_store_sum Stock1
, curr_change_quantity Stock2 
from tb_product where is_fixed=1 order by menu_child_serial_no asc, product_ebay_name asc ");
        this.rptList.DataSource = query;
        this.rptList.DataBind();
    }
    protected void rptList_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName.Equals("Cancel"))
        {
            Config.ExecuteNonQuery("Update tb_product set is_fixed=0 where product_serial_no ='" + e.CommandArgument + "'");
            BindData();
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        var sku = txtSku.Text.Trim();
        Config.ExecuteNonQuery("Update tb_product set is_fixed=1 where product_serial_no ='" + sku + "'");
        BindData();
    }
}