using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Q_Admin_ebayMaster_Online_ModifyOnlineAutoPay : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            eBayCmdReviseItem.Revise(DBContext, ReqItemid, ReqIsSys, null, 0M, ReqSku, null, (ReqPrice >= 2000 ? false : true));

        }
    }

    string ReqItemid
    {
        get { return Util.GetStringSafeFromQueryString(Page, "itemid"); }
    }

    decimal ReqPrice
    {
        get { return Util.GetDecimalSafeFromQueryString(Page, "price", 0); }
    }

    int ReqSku
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "sku", 0); }
    }

    bool ReqIsSys
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "issystem", 0) == 1; }
    }
}