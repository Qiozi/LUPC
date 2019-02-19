using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

/// <summary>
/// Summary description for PaypalStoreInfo
/// </summary>
public class PaypalStoreInfo
{
     System.Web.HttpContext hc;
	public PaypalStoreInfo(System.Web.HttpContext _hc)
	{
		//
		// TODO: Add constructor logic here
		//
         hc = _hc;
    }

    #region perporties
    public int OrderCode
    {
        get
        {
            if (hc.Request.Cookies["pay_order_code"] != null)
                return int.Parse(hc.Request.Cookies["pay_order_code"].Value);
            else
                return -1;
        }
        set { hc.Response.Cookies["pay_order_code"].Value = value.ToString(); }
    }

    public decimal AMT
    {
        get
        {
            if (hc.Request.Cookies["pay_AMT"] != null)
                return decimal.Parse(hc.Request.Cookies["pay_AMT"].Value);
            else
                return -1;
        }
        set { hc.Response.Cookies["pay_AMT"].Value = value.ToString(); }
    }

    public int CustomerSerialNo
    {
        get
        {
            if (hc.Request.Cookies["pay_CustomerSerialNo"] != null)
                return int.Parse(hc.Request.Cookies["pay_CustomerSerialNo"].Value);
            else
                return -1;
        }
        set { hc.Response.Cookies["pay_CustomerSerialNo"].Value = value.ToString(); }
    }


    public string PAYERID
    {
        get
        {
            if (hc.Request.Cookies["PAYERID"] != null)
                return hc.Request.Cookies["PAYERID"].Value;
            else
                return "";
        }
        set { hc.Response.Cookies["PAYERID"].Value = value; }
    }
    #endregion

    //public bool SaveToken(PaypalStoreInfo psi, string return_response,string token, ref string error)
    //{
    //    try
    //    {
    //        PaypalTokenModel ptm = new PaypalTokenModel();
    //        ptm.amt = psi.AMT;
    //        ptm.create_datetime = DateTime.Now;
    //        ptm.customer_serial_no = psi.CustomerSerialNo;
    //        ptm.order_code = psi.OrderCode.ToString();
    //        ptm.return_response = return_response;
    //        ptm.token = token;
    //        ptm.Create();
    //    }
    //    catch (Exception ex)
    //    {
    //        error = ex.Message;
    //        return false;
    //    }
    //    return true;
    //}
}
