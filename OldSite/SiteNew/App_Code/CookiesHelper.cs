using LU.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Cookies 的摘要说明
/// </summary>
public class CookiesHelper
{
    HttpContext _context;
    public CookiesHelper(HttpContext context)
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
        _context = context;
    }

    public int SearchCategoryId
    {
        get
        {
            int searchCategoryId = 0;
            if (_context.Request.Cookies["SearchCategoryId"] != null)
            {
                int.TryParse(_context.Request.Cookies["SearchCategoryId"].Value, out searchCategoryId);
            }

            return searchCategoryId;
        }
        set
        {
            SetCookiesValue("SearchCategoryId", value.ToString());
        }
    }

    public CountryType CurrSiteCountry
    {
        get
        {
            if (_context.Request.Url.ToString().ToLower().IndexOf("localhost") > -1)
            {
                return CountryType.CAD;
            }
            if (_context.Request.Url.ToString().ToLower().IndexOf("us.lucomputers.com") > -1)
            {
                //Response.Cookies["CurrSiteCountry"].Value = ((int)CountryType.USD).ToString();
                //Response.Cookies["CurrSiteCountry"].Domain = Variable.Domain;
                SetCookiesValue("CurrSiteCountry", ((int)CountryType.USD).ToString());
                return CountryType.USD;
            }
            if (_context.Request.Url.ToString().ToLower().IndexOf("ca.lucomputers.com") > -1)
            {
                //Response.Cookies["CurrSiteCountry"].Value = ((int)CountryType.CAD).ToString();
                //Response.Cookies["CurrSiteCountry"].Domain = Variable.Domain;
                SetCookiesValue("CurrSiteCountry", ((int)CountryType.CAD).ToString());
                return CountryType.CAD;
            }
            else
            {
                if (_context.Request.Cookies["CurrSiteCountry"] == null)
                {
                    //Response.Cookies["CurrSiteCountry"].Value = ((int)CountryType.CAD).ToString();
                    //Response.Cookies["CurrSiteCountry"].Domain = Variable.Domain;
                    SetCookiesValue("CurrSiteCountry", ((int)CountryType.CAD).ToString());
                    return CountryType.CAD;
                }
                else
                {
                    var currCountryValue = _context.Request.Cookies["CurrSiteCountry"].Value;
                    return (CountryType)(Enum.Parse(typeof(CountryType), currCountryValue));
                }
            }
        }
    }

    public int CurrOrderCode
    {
        get
        {
            var obj = _context.Request.Cookies["orderCode"];
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

    public Guid UserToken
    {
        get
        {          
            if (_context.Request.Cookies["Token"] != null)
            {
                var guid = _context.Request.Cookies["Token"].Value;
                if (!string.IsNullOrEmpty(guid))
                    return LU.Toolkit.GuidExtension.Base64ToGuid(guid);
            }
            return Guid.Empty;
        }
        set
        {
            SetCookiesValue("Token", value == Guid.Empty ? "" : LU.Toolkit.GuidExtension.GuidToBase64(value));
        }
    }


    /// <summary>
    /// 购物车数量
    /// </summary>
    public int CartQty
    {
        get
        {
            if (_context.Request.Cookies["CartQty"] == null)
                return 0;
            var obj = _context.Request.Cookies["CartQty"].Value;
            if (obj != null)
                return int.Parse(obj.ToString());
            else
                return 0;
        }
        set
        {
            SetCookiesValue("CartQty", value.ToString());
        }
    }
    /// <summary>
    /// 取得用户名称 用于网页头部显示
    /// </summary>
    //public string CustomerName
    //{
    //    get
    //    {
    //        if (_context.Request.Cookies["CustomerName"] != null)
    //        {
    //            return _context.Request.Cookies["CustomerName"].Value;
    //        }

    //        return "";
    //    }
    //    set
    //    {
    //        SetCookiesValue("CustomerName", value);
    //    }
    //}

    //public string CustomerSerialNo
    //{
    //    get
    //    {
    //        if (_context.Request.Cookies["CustomerSerialNo"] == null)
    //            return "";
    //        return _context.Request.Cookies["CustomerSerialNo"].Value;
    //    }
    //    set
    //    {
    //        SetCookiesValue("CustomerSerialNo", value);
    //    }
    //}

    public void SetCookiesValue(string key, string value)
    {
        if (_context.Response.Cookies[key].Value != null)
        {
            _context.Response.Cookies.Remove(key);
        }
        _context.Response.Cookies[key].Value = value.ToString();

        _context.Response.Cookies[key].Domain = Variable.Domain;
        _context.Response.Cookies[key].Expires = DateTime.Now.AddDays(1);
    }

    public bool IsLocal
    {
        get
        {
            return _context
              .Request.Url
              .ToString()
              .ToLower()
              .IndexOf(LU.BLL.Config.IsLocalHost) > -1;
        }
    }
}