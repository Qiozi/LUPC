using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for PageBase
/// </summary>

public class PageBase : System.Web.UI.Page
{
    public nicklu2Model.nicklu2Entities db = new nicklu2Model.nicklu2Entities();

    public PageBase()
    {

    }

    public int CurrOrderCode
    {
        get
        {
            var obj = Request.Cookies["orderCode"];
            if (obj != null)
            {
                int oc;
                int.TryParse(obj.Value.ToString(), out oc);
                return oc;
            }
            else
                return 0;
        }
        set
        {
            SetCookiesValue("orderCode", value.ToString());
        }
    }

    public int ReqSKU
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "sku", 0); }
    }

    public string ReqCmd
    {
        get { return Util.GetStringSafeFromQueryString(Page, "cmd"); }
    }
    public string ReqU
    {
        get { return Util.GetStringSafeFromQueryString(Page, "u"); }
    }

    /// <summary>
    ///  当前网站国家
    /// </summary>
    public CountryType CurrSiteCountry
    {
        get
        {
            if (Request.Url.ToString().ToLower().IndexOf("localhost") > -1)
            {
                return CountryType.CAD;
            }
            if (Request.Url.ToString().ToLower().IndexOf("us.lucomputers.com") > -1)
            {
                //Response.Cookies["CurrSiteCountry"].Value = ((int)CountryType.USD).ToString();
                //Response.Cookies["CurrSiteCountry"].Domain = Variable.Domain;
                SetCookiesValue("CurrSiteCountry", ((int)CountryType.USD).ToString());
                return CountryType.USD;
            }
            if (Request.Url.ToString().ToLower().IndexOf("ca.lucomputers.com") > -1)
            {
                //Response.Cookies["CurrSiteCountry"].Value = ((int)CountryType.CAD).ToString();
                //Response.Cookies["CurrSiteCountry"].Domain = Variable.Domain;
                SetCookiesValue("CurrSiteCountry", ((int)CountryType.CAD).ToString());
                return CountryType.CAD;
            }
            else
            {
                if (Request.Cookies["CurrSiteCountry"] == null)
                {
                    //Response.Cookies["CurrSiteCountry"].Value = ((int)CountryType.CAD).ToString();
                    //Response.Cookies["CurrSiteCountry"].Domain = Variable.Domain;
                    SetCookiesValue("CurrSiteCountry", ((int)CountryType.CAD).ToString());
                    return CountryType.CAD;
                }
                else
                {
                    var currCountryValue = Request.Cookies["CurrSiteCountry"].Value;
                    return (CountryType)(Enum.Parse(typeof(CountryType), currCountryValue));
                }
            }
        }
    }

    public bool IsLocal
    {
        get { return Request.Url.ToString().ToLower().IndexOf("localhost") > -1; }
    }

    /// <summary>
    /// 是否从本站进入
    /// </summary>
    public bool IsLocalHostFrom
    {
        get
        {
            if (Request.ServerVariables["HTTP_REFERER"] == null)
            {
                return false;
            }

            string refererStr = Request.ServerVariables["HTTP_REFERER"].ToString();
            return IsLocal ? true : refererStr.ToLower().IndexOf(System.Configuration.ConfigurationManager.AppSettings["hostName"].ToString()) > -1;
        }
    }

    decimal? _prate;
    /// <summary>
    /// price rate
    /// </summary>
    public decimal PRate
    {
        get
        {
            if (_prate == null || _prate == 0)
            {
                var rate = db.tb_currency_convert.Single(p => p.is_current.HasValue && p.is_current.Value.Equals(true));
                _prate = rate.currency_usd.Value;
            }
            return _prate.Value;
        }
    }

    /// <summary>
    /// 判断当前是CAD 还是USD ，
    /// 如果是美金，
    /// 然后加币转换为美金
    /// </summary>
    /// <param name="CADPrice"></param>
    /// <returns></returns>
    public decimal ConvertPrice(decimal CADPrice)
    {
        if (CurrSiteCountry == CountryType.CAD)
            return CADPrice;
        else
            return PriceRate.ConvertPrice(CADPrice, CurrSiteCountry, PRate);
    }


    #region 客户信息
    /// <summary>
    /// 客户是否登入
    /// </summary>
    public bool IsLogin
    {
        get { return CustomerID > 0; }

    }

    /// <summary>
    /// 客户ID
    /// </summary>
    public int CustomerID
    {
        get
        {
            int customerID = 0;
            if (Request.Cookies["CustomerID"] != null)
            {
                int.TryParse(Request.Cookies["CustomerID"].Value, out customerID);
            }

            return customerID;
        }
    }
    /// <summary>
    /// 保存客户ID号
    /// </summary>
    /// <param name="v"></param>
    public void SetCustomerID(int v)
    {
        SetCookiesValue("CustomerID", v.ToString());
    }

    public void SetCustomerSerialNo(string serialNO)
    {
        SetCookiesValue("CustomerSerialNo", serialNO);
    }

    public void SetCookiesValue(string key, string value)
    {
        if (Response.Cookies[key].Value != null)
        {
            Response.Cookies.Remove(key);
        }
        Response.Cookies[key].Value = value.ToString();
        if (!IsLocal)
        {
            Response.Cookies[key].Domain = Variable.Domain;
        }
    }

    public string CustomerSerialNo
    {
        get { return Request.Cookies["CustomerSerialNo"].Value; }
    }
    /// <summary>
    /// 保存用户名
    /// </summary>
    /// <param name="name"></param>
    public void SetCustomerName(string name)
    {
        //Response.Cookies["CustomerName"].Value = name;
        //Response.Cookies["CustomerName"].Domain = Variable.Domain;
        SetCookiesValue("CustomerName", name);
    }
    /// <summary>
    /// 取得用户名称 用于网页头部显示
    /// </summary>
    public string CustomerName
    {
        get
        {
            if (Request.Cookies["CustomerName"] != null)
            {
                return Request.Cookies["CustomerName"].Value;
            }

            return "";
        }
    }
    /// <summary>
    /// 当前客户信息
    /// </summary>
    public nicklu2Model.tb_customer CurrCustomer
    {
        get
        {
            if (IsLogin)
            {
                return db.tb_customer.FirstOrDefault(p => p.ID.Equals(CustomerID));
            }
            else
                return null;
        }
    }
    #endregion

    /// <summary>
    /// 购物车数量
    /// </summary>
    public int CartQty
    {
        get
        {
            var obj = Request.Cookies["CartQty"].Value;
            if (obj != null)
                return int.Parse(obj.ToString());
            else
                return 0;
        }
        set
        {
            //Response.Cookies["CartQty"].Value = value.ToString();
            //Response.Cookies["CartQty"].Domain = Variable.Domain;
            SetCookiesValue("CartQty", value.ToString());
        }
    }
}