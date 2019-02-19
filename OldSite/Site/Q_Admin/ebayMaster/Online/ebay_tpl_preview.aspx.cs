using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Q_Admin_ebayMaster_Online_ebay_tpl_preview : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.literal1.Text = FileHelper.ReadFile(Server.MapPath(EbaySettings.ebayMasterXmlPath + "tpl_comment/" + SystemSKU.ToString() + ".htm"));

        }

    }
    


    public int SystemSKU
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "system_sku", -1); }
    }
}
