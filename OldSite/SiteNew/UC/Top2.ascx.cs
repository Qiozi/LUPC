using LU.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UC_Top2 : UserControlBase
{
    public string WebLogo
    {
        get { return LU.BLL.ImageHelper.Get("/images/logo1.png"); }
    }

    public bool IsCanada = true;
    public string HideUS { get; set; }

    public string HideCA { get; set; }

    public int DefaultCateId = 1;

    public string badge { get; set; }

    public string CustName { get; set; }

    public bool IsLogin { get; set; }

    public string MyAccount { get; set; }

    public int CurrSearchCateId
    {
        get
        {
            return new CookiesHelper(Context).SearchCategoryId;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            IsCanada = new CookiesHelper(Context).CurrSiteCountry == LU.Model.Enums.CountryType.CAD;


            //var cartQty = cookiesHelper.CartQty;
            //badge = "<span class='badge'>" + cartQty + "</span>";
            CustName = CurrCustomerName;
            if (!string.IsNullOrEmpty(CustName))
            {
                IsLogin = true;
                MyAccount = " | My Account";
            }
            else
            {
                MyAccount = string.Empty;
                IsLogin = false;
            }

            // 继用上次使用的搜索类型
            DefaultCateId = Util.GetInt32SafeFromQueryString(Page, "cate", 1);

            InitCates();
        }
    }

    void InitCates()
    {
        var cates = LU.BLL.CacheProvider.GetAllCates(DBContext);
        for (var i = 0; i < cates.Count; i++)
        {
            if (cates[i].Id == 52)
            {
                for (var j = 0; j < cates[i].SubCates.Count; j++)
                {

                    if (cates[i].SubCates[j].Id == 413 || cates[i].SubCates[j].Id == 414)
                    {
                        cates[i].SubCates.RemoveAt(j);
                        j--;
                    }
                    else if (cates[i].SubCates[j].Id == 412)
                    {
                        cates[i].SubCates[j].Title = "Gaming, Business, Home, BarebonePC";
                    }
                }
            }
        }

        this.rptList.DataSource = cates;
        this.rptList.DataBind();
    }

    protected void rptList_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType != ListItemType.Header && e.Item.ItemType != ListItemType.Footer)
        {
            var data = (LU.Model.Cate)e.Item.DataItem;
            var subRpt = e.Item.FindControl("_rpSub") as Repeater;
            subRpt.DataSource = data.SubCates;
            subRpt.DataBind();
        }
    }
}