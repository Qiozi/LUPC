using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Q_Admin_Manager_eBay_FormEbayGetCategorySpecifices : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && ReqCateId > 0)
        {
            EbayGetXmlHelper egxh = new EbayGetXmlHelper();
            egxh.GetCategoryFeatures(ReqCateId);
            egxh.GetCategorySpecifics(ReqCateId);
        }
    }
    

    public int ReqCateId
    {
        get
        {
            return Util.GetInt32SafeFromQueryString(Page, "cid", 0);
        }
    }
}