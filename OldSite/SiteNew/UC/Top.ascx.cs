using LU.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UC_Top : UserControlBase
{
    public string WebLogo
    {
        get { return LU.BLL.ImageHelper.Get("/images/logo1.png"); }
    }

    public string HideUS { get; set; }

    public string HideCA { get; set; }

    public int DefaultCateId = 1;

    public string badge { get; set; }

    public string CustName { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (IsLogin)
            {
                var cartQty = cookiesHelper.CartQty;               
                badge = "<span class='badge'>" + cartQty + "</span>";
                CustName = CurrCustomerName;

                placeMyAccountLogout.Visible = true;
                placeMyAccountLogin.Visible = false;
            }
            else
            {
                placeMyAccountLogout.Visible = false;
                placeMyAccountLogin.Visible = true;
            }

            // 继用上次使用的搜索类型
            DefaultCateId = Util.GetInt32SafeFromQueryString(Page, "cate", 1);

            if (cookiesHelper.CurrSiteCountry == CountryType.CAD)
            {
                HideUS = "hide";
                HideCA = string.Empty;
            }
            else
            {
                HideUS = string.Empty;
                HideCA = "hide";
            }
        }
    }
}