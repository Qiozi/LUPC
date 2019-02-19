using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class Q_Admin_product_helper_factory_search : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string ids = "";
        if (this.TextBox1.Text.Trim() != "")
            ids = this.TextBox1.Text.Trim();
        else
            return;
        int typeid = int.Parse(this.RadioButtonList1.SelectedValue);
        if (typeid != 1)
        {
            ids = "'" + ids.Replace(",", "','") + "'";

        
        }
        DataTable dt = ProductModel.FindInfoBySkuFactorySupliter(ids, typeid);
        this.rpt_product_list.DataSource = dt;
        this.rpt_product_list.DataBind();

    }
}
