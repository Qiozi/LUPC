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
using System.Net;

public partial class SavePaypalShippingInfo : System.Web.UI.Page
{
    PaypalStoreInfo psi = new PaypalStoreInfo(System.Web.HttpContext.Current);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SaveInfoToDB();
            //Response.Redirect("Shopping_CheOut_paypal.asp?tx="+ Util.GetStringSafeFromQueryString(Page, "token"));
            string returns = "";
            string url = "https://api.sandbox.paypal.com/nvp?version=2.3&CURRENCYCODE=USD&LOCALECODE=CA&user=qiozi4_1206899598_biz_api1.163.com&pwd=1206899607&signature=Ab.--knS-exvZZP3DONGdTh.Z32eAmLcNYNggqRzRox4Ft.eAAPdAar-&method=DoExpressCheckoutPayment&PAYERID=" + psi.PAYERID + "&PAYMENTACTION=Sale&AMT=" + psi.AMT.ToString() + "&token=" + Util.GetStringSafeFromQueryString(Page, "token");// +"&build=" + Util.GetStringSafeFromQueryString(Page, "build");
            Response.Write(url + "<br>");
            
            HttpWebRequest httpr = (HttpWebRequest)WebRequest.Create(url);
            System.IO.StreamReader sw = new System.IO.StreamReader(httpr.GetResponse().GetResponseStream(), System.Text.Encoding.UTF8);
            returns = Server.UrlDecode(sw.ReadToEnd());
            string token = "";
            if (returns.Length > 20)
                token = returns.Substring(6, 20);
            sw.Close();
            sw.Dispose();

            //PaypalTokenModel.SaveToken(Util.GetStringSafeFromQueryString(Page, "token"), returns, psi.OrderCode.ToString(), psi.CustomerSerialNo, psi.AMT);
            psi.AMT = 0;
            psi.CustomerSerialNo = -1;
            psi.OrderCode = 0;
            psi.PAYERID = "";

            //ptm.create_datetime = DateTime.Now;
            //ptm.customer_serial_no = 0;
            //ptm.order_code = "000000";
            //ptm.return_response = returns;
            //ptm.ip = "0";
            //ptm.token = Util.GetStringSafeFromQueryString(Page, "token");
            //ptm.amt = 10M;
            //ptm.Create();

            Response.Redirect("Shopping_CheOut_paypal.asp?tx=" + Util.GetStringSafeFromQueryString(Page, "token"));

        }
    }

    #region Methods
    public void InitialDatabase()
    {
        SaveInfoToDB();
    }

    private void SaveInfoToDB()
    {
        psi.PAYERID = Util.GetStringSafeFromQueryString(Page, "PAYERID");
        string FIRSTNAME = Util.GetStringSafeFromQueryString(Page, "FIRSTNAME");
        string LASTNAME = Util.GetStringSafeFromQueryString(Page, "LASTNAME");
        string shiptostreet = Util.GetStringSafeFromQueryString(Page, "shiptostreet");
        string shiptocity = Util.GetStringSafeFromQueryString(Page, "shiptocity");
        string shiptozip = Util.GetStringSafeFromQueryString(Page, "shiptozip");
        string shiptocountrycode = Util.GetStringSafeFromQueryString(Page, "shiptocountrycode");
        string countrycode = Util.GetStringSafeFromQueryString(Page, "countrycode");
        string addressstatus = Util.GetStringSafeFromQueryString(Page, "addressstatus");
        string SHIPTOSTATE = Util.GetStringSafeFromQueryString(Page, "SHIPTOSTATE");
        int ship_state = StateShippingModel.FindStatIDByCode(SHIPTOSTATE);
        int country_id = CountryModel.FindIDByCode(countrycode);

        int customer_serial_no = psi.CustomerSerialNo;

        CustomerModel cm = CustomerModel.GetCustomerModel(customer_serial_no);
        if (cm != null)
        {
            cm.customer_shipping_first_name = FIRSTNAME;
            cm.customer_shipping_last_name = LASTNAME;
            cm.customer_shipping_address = shiptostreet;
            cm.customer_shipping_city = shiptocity;
            cm.customer_shipping_zip_code = shiptozip;
            cm.customer_shipping_country = country_id;
            cm.customer_shipping_state = ship_state;
            cm.Update();
        }
    }
    #endregion
}
