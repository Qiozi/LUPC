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

public partial class GetExpressCheckoutDetails : System.Web.UI.Page
{
    PaypalStoreInfo psi = new PaypalStoreInfo(System.Web.HttpContext.Current);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string url = "https://api.sandbox.paypal.com/nvp?user=qiozi4_1206899598_biz_api1.163.com&pwd=1206899607&signature=Ab.--knS-exvZZP3DONGdTh.Z32eAmLcNYNggqRzRox4Ft.eAAPdAar-&token=" + Request.QueryString["token"].ToString() + "&METHOD=GetExpressCheckoutDetails";
            HttpWebRequest httpr = (HttpWebRequest)WebRequest.Create(url);
            System.IO.StreamReader sw = new System.IO.StreamReader(httpr.GetResponse().GetResponseStream(), System.Text.Encoding.UTF8);
            //Response.Write(Server.UrlDecode(sw.ReadToEnd()));
            string return_response = Server.UrlDecode(sw.ReadToEnd());
            //PaypalTokenModel ptm = new PaypalTokenModel();
            //ptm.create_datetime = DateTime.Now;
            //ptm.customer_serial_no = psi.CustomerSerialNo;
            //ptm.order_code = psi.OrderCode.ToString();
            //ptm.return_response = return_response;
            //ptm.Create();

            Response.Redirect("SavePaypalShippingInfo.aspx?" + return_response);
            sw.Close();
            
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
string url = "https://api.sandbox.paypal.com/nvp?user=qiozi4_1206899598_biz_api1.163.com&pwd=1206899607&signature=Ab.--knS-exvZZP3DONGdTh.Z32eAmLcNYNggqRzRox4Ft.eAAPdAar-&token=" + Request.QueryString["token"].ToString() + "&METHOD=GetExpressCheckoutDetails";
            HttpWebRequest httpr = (HttpWebRequest)WebRequest.Create(url);
            System.IO.StreamReader sw = new System.IO.StreamReader(httpr.GetResponse().GetResponseStream(), System.Text.Encoding.UTF8);
            //Response.Write(Server.UrlDecode(sw.ReadToEnd()));
            
            Response.Redirect("SavePaypalShippingInfo.aspx?" + Server.UrlDecode(sw.ReadToEnd()));
            sw.Close();
    }
}
