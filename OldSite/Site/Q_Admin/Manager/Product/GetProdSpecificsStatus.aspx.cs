using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Q_Admin_Manager_Product_GetProdSpecificsStatus : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            var sku = Util.GetInt32SafeFromQueryString(Page, "sku", 0);
            if (sku > 0)
            {
                if (DBContext.tb_ebay_system_item_specifics.Count(p => p.system_sku.Equals(sku)) > 0)
                {
                    Response.Write("spec");
                }
                else
                {
                    Response.Write("");
                }
            }
            else
            {
                Response.Write("");
            }
            Response.End();
        }
    }
}