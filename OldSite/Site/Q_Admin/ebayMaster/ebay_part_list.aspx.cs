using LU.Data;
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml;
using System.Xml.Linq;

public partial class Q_Admin_ebayMaster_ebay_part_list : PageBase
{
    public string Token = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Token = WebapiToken.GetToken();           
        }
    }
    
    protected void btnEbayOnsale_Click(object sender, EventArgs e)
    {
        var dt = Config.ExecuteDataTable(@"select e.itemid, o.begin_datetime, o.end_datetime from tb_on_sale o inner join tb_product p on o.product_serial_no=p.product_serial_no
inner join tb_ebay_selling e on e.luc_sku = p.product_serial_no
where p.menu_child_serial_no = 350 and save_price >=100 and save_price<=120 ");
        if (dt.Rows.Count > 2)
        {
            var promotionalSaleId = SeteBayOnSale(100
                , DateTime.Parse(dt.Rows[0]["begin_datetime"].ToString()).AddDays(1)
                , DateTime.Parse(dt.Rows[0]["end_datetime"].ToString()));
            if (promotionalSaleId.Length < 12)
            {
                var itemIdString = string.Empty;
                foreach (DataRow dr in dt.Rows)
                {
                    itemIdString += "<ItemID>" + dr["itemid"].ToString() + "</ItemID>";
                }
                var result = SetPromotionalSaleListings(itemIdString, promotionalSaleId);
                //this.btnEbayOnsale.Text = result;
            }
            else
            {
                //this.btnEbayOnsale.Text = "Error";
            }
        }
    }
    string SetPromotionalSaleListings(string itemIdString, string promotionalSaleId)
    {
        #region settings

        string devID = EbaySettings.devID;
        string appID = EbaySettings.appID;
        string certID = EbaySettings.certID;

        //Get the Server to use (Sandbox or Production)
        string serverUrl = EbaySettings.serverUrl;

        //Get the User Token to Use
        string userToken = EbaySettings.userToken;

        //SiteID = 0  (US) - UK = 3, Canada = 2, Australia = 15, ....
        //SiteID Indicates the eBay site to associate the call with
        int siteID = EbaySettings.siteID;
        #endregion


        #region Load The XML Document Template and Set the Neccessary Values
        //Load the XML Document to Use for this Request
        var xmlDoc = new XmlDocument();

        ////Get XML Document from Embedded  Resources
        //xmlDoc.Load((Server.MapPath("/q_admin/ebayMaster/Online/Xml/GeteBayOfficialTimeRequest.xml")));

        ////Set the various node values   attr1858_26443
        //xmlDoc["GeteBayOfficialTimeRequest"]["RequesterCredentials"]["eBayAuthToken"].InnerText = userToken;

        string sendXml = string.Format(@"<?xml version=""1.0"" encoding=""utf-8""?>
<SetPromotionalSaleListingsRequest xmlns=""urn:ebay:apis:eBLBaseComponents"">
  <RequesterCredentials>
    <eBayAuthToken>{0}</eBayAuthToken>
  </RequesterCredentials>
   <Action>Add</Action>
  <PromotionalSaleID>{1}</PromotionalSaleID>
  <PromotionalSaleItemIDArray>
    {2}
  </PromotionalSaleItemIDArray>
  <AllFixedPriceItems>false</AllFixedPriceItems>
  <AllStoreInventoryItems>false</AllStoreInventoryItems>
</SetPromotionalSaleListingsRequest>", userToken
                  , promotionalSaleId
                  , itemIdString);

        //throw new Exception(sendXml);
        var esxhm = new tb_ebay_send_xml_history();// EbaySendXmlHistoryModel();
        esxhm.Content = sendXml;
        esxhm.is_sys = false;
        esxhm.SKU = 0;
        DBContext.tb_ebay_send_xml_history.Add(esxhm);
        DBContext.SaveChanges();

        //throw new Exception(ItemAttribates);

        xmlDoc.LoadXml(sendXml);
        //Get XML into a string for use in encoding
        string xmlText = xmlDoc.InnerXml;
        //eBay.Service.Call.AddItemCall aic = new eBay.Service.Call.AddItemCall();


        //Put the data into a UTF8 encoded  byte array
        var encoding = new UTF8Encoding();
        int dataLen = encoding.GetByteCount(xmlText);
        byte[] utf8Bytes = new byte[dataLen];
        Encoding.UTF8.GetBytes(xmlText, 0, xmlText.Length, utf8Bytes, 0);
        #endregion


        #region Setup The Request (inc. HTTP Headers
        //Create a new HttpWebRequest object for the ServerUrl
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(serverUrl);

        //Set Request Method (POST) and Content Type (text/xml)
        request.Method = "POST";
        request.ContentType = "text/xml";
        request.ContentLength = utf8Bytes.Length;

        //Add the Keys to the HTTP Headers
        request.Headers.Add("X-EBAY-API-DEV-NAME: " + devID);
        request.Headers.Add("X-EBAY-API-APP-NAME: " + appID);
        request.Headers.Add("X-EBAY-API-CERT-NAME: " + certID);

        //Add Compatability Level to HTTP Headers
        //Regulates versioning of the XML interface for the API
        request.Headers.Add("X-EBAY-API-COMPATIBILITY-LEVEL: 761");

        //Add function name, SiteID and Detail Level to HTTP Headers
        request.Headers.Add("X-EBAY-API-CALL-NAME: SetPromotionalSaleListings");
        request.Headers.Add("X-EBAY-API-SITEID: " + siteID.ToString());

        //Time out = 15 seconds,  set to -1 for no timeout.
        //If times-out - throws a WebException with the
        //Status property set to WebExceptionStatus.Timeout.
        request.Timeout = 15000;

        #endregion


        #region Send The Request
        Stream str = null;
        try
        {
            //Set the request Stream
            str = request.GetRequestStream();
            //Write the equest to the Request Steam
            str.Write(utf8Bytes, 0, utf8Bytes.Length);
            str.Close();
            //Get response into stream
            WebResponse resp = request.GetResponse();
            str = resp.GetResponseStream();
        }
        catch (WebException wEx)
        {
            //Error has occured whilst requesting
            //Display error message and exit.
            if (wEx.Status == WebExceptionStatus.Timeout)
                Response.Write("Request Timed-Out.");
            else
                Response.Write(wEx.Message);

            Response.Write("Press Enter to Continue...");

            return wEx.Message;
        }
        #endregion

        #region Process Response
        // Get Response into String
        StreamReader sr = new StreamReader(str);
        string sssss = sr.ReadToEnd();
        xmlDoc.LoadXml(sssss);
        sr.Close();
        str.Close();

        var esrhm = new tb_ebay_send_xml_result_history();// EbaySendXmlResultHistoryModel();
        esrhm.Content = sssss;
        esrhm.is_sys = false;
        esrhm.SKU = 0;
        DBContext.tb_ebay_send_xml_result_history.Add(esrhm);
        DBContext.SaveChanges();

        //get the root node, for ease of use
        XmlNode root = xmlDoc["SetPromotionalSaleListingsResponse"];

        //There have been Errors
        if (root["Errors"] != null)
        {
            try
            {
                return root["Ack"].InnerText;
            }
            catch { }
            return (sssss);
        }
        else
        {
            return root["Ack"].InnerText;
        }
        #endregion
    }
    string SeteBayOnSale(decimal discount, DateTime begin, DateTime end)
    {
        #region settings

        string devID = EbaySettings.devID;
        string appID = EbaySettings.appID;
        string certID = EbaySettings.certID;

        //Get the Server to use (Sandbox or Production)
        string serverUrl = EbaySettings.serverUrl;

        //Get the User Token to Use
        string userToken = EbaySettings.userToken;

        //SiteID = 0  (US) - UK = 3, Canada = 2, Australia = 15, ....
        //SiteID Indicates the eBay site to associate the call with
        int siteID = EbaySettings.siteID;
        #endregion


        #region Load The XML Document Template and Set the Neccessary Values
        //Load the XML Document to Use for this Request
        var xmlDoc = new XmlDocument();

        ////Get XML Document from Embedded  Resources
        //xmlDoc.Load((Server.MapPath("/q_admin/ebayMaster/Online/Xml/GeteBayOfficialTimeRequest.xml")));

        ////Set the various node values   attr1858_26443
        //xmlDoc["GeteBayOfficialTimeRequest"]["RequesterCredentials"]["eBayAuthToken"].InnerText = userToken;

        string sendXml = string.Format(@"<?xml version=""1.0"" encoding=""utf-8""?>
<SetPromotionalSaleRequest xmlns=""urn:ebay:apis:eBLBaseComponents"">
  <RequesterCredentials>
    <eBayAuthToken>{0}</eBayAuthToken>
  </RequesterCredentials>
  <ErrorLanguage>en_US</ErrorLanguage>
  <WarningLevel>High</WarningLevel>
  <Action>Add</Action>
  <PromotionalSaleDetails>
    <PromotionalSaleName>OnSale</PromotionalSaleName>
    <DiscountType>Price</DiscountType>
    <DiscountValue>{1}</DiscountValue>
    <PromotionalSaleType>PriceDiscountOnly</PromotionalSaleType>
    <PromotionalSaleStartTime>{2}T01:00:00.000Z</PromotionalSaleStartTime>
    <PromotionalSaleEndTime>{3}T23:00:00.000Z</PromotionalSaleEndTime>
  </PromotionalSaleDetails>
</SetPromotionalSaleRequest>", userToken
                  , discount
                  , begin.ToString("yyyy-MM-dd")
                  , end.ToString("yyyy-MM-dd"));

        //throw new Exception(sendXml);
        var esxhm = new tb_ebay_send_xml_history();// EbaySendXmlHistoryModel();
        esxhm.Content = sendXml;
        esxhm.is_sys = false;
        esxhm.SKU = 0;
        DBContext.tb_ebay_send_xml_history.Add(esxhm);
        DBContext.SaveChanges();

        //throw new Exception(ItemAttribates);

        xmlDoc.LoadXml(sendXml);
        //Get XML into a string for use in encoding
        string xmlText = xmlDoc.InnerXml;
        //eBay.Service.Call.AddItemCall aic = new eBay.Service.Call.AddItemCall();


        //Put the data into a UTF8 encoded  byte array
        var encoding = new UTF8Encoding();
        int dataLen = encoding.GetByteCount(xmlText);
        byte[] utf8Bytes = new byte[dataLen];
        Encoding.UTF8.GetBytes(xmlText, 0, xmlText.Length, utf8Bytes, 0);
        #endregion


        #region Setup The Request (inc. HTTP Headers
        //Create a new HttpWebRequest object for the ServerUrl
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(serverUrl);

        //Set Request Method (POST) and Content Type (text/xml)
        request.Method = "POST";
        request.ContentType = "text/xml";
        request.ContentLength = utf8Bytes.Length;

        //Add the Keys to the HTTP Headers
        request.Headers.Add("X-EBAY-API-DEV-NAME: " + devID);
        request.Headers.Add("X-EBAY-API-APP-NAME: " + appID);
        request.Headers.Add("X-EBAY-API-CERT-NAME: " + certID);

        //Add Compatability Level to HTTP Headers
        //Regulates versioning of the XML interface for the API
        request.Headers.Add("X-EBAY-API-COMPATIBILITY-LEVEL: 761");

        //Add function name, SiteID and Detail Level to HTTP Headers
        request.Headers.Add("X-EBAY-API-CALL-NAME: SetPromotionalSale");
        request.Headers.Add("X-EBAY-API-SITEID: " + siteID.ToString());

        //Time out = 15 seconds,  set to -1 for no timeout.
        //If times-out - throws a WebException with the
        //Status property set to WebExceptionStatus.Timeout.
        request.Timeout = 15000;

        #endregion


        #region Send The Request
        Stream str = null;
        try
        {
            //Set the request Stream
            str = request.GetRequestStream();
            //Write the equest to the Request Steam
            str.Write(utf8Bytes, 0, utf8Bytes.Length);
            str.Close();
            //Get response into stream
            WebResponse resp = request.GetResponse();
            str = resp.GetResponseStream();
        }
        catch (WebException wEx)
        {
            //Error has occured whilst requesting
            //Display error message and exit.
            if (wEx.Status == WebExceptionStatus.Timeout)
                Response.Write("Request Timed-Out.");
            else
                Response.Write(wEx.Message);

            Response.Write("Press Enter to Continue...");

            return wEx.Message;
        }
        #endregion

        #region Process Response
        // Get Response into String
        StreamReader sr = new StreamReader(str);
        string sssss = sr.ReadToEnd();
        xmlDoc.LoadXml(sssss);
        sr.Close();
        str.Close();

        var esrhm = new tb_ebay_send_xml_result_history();// EbaySendXmlResultHistoryModel();
        esrhm.Content = sssss;
        esrhm.is_sys = false;
        esrhm.SKU = 0;
        DBContext.tb_ebay_send_xml_result_history.Add(esrhm);
        DBContext.SaveChanges();

        //get the root node, for ease of use
        XmlNode root = xmlDoc["SetPromotionalSaleResponse"];

        //There have been Errors
        if (root["Errors"] != null)
        {
            try
            {
                return root["PromotionalSaleID"].InnerText;
            }
            catch { }
            return (sssss);
        }
        else
        {
            var promotionalSaleId = root["PromotionalSaleID"].InnerText;
            Config.ExecuteNonQuery(string.Format(@"insert into tb_ebay_promotional_sale_id 
	( price, beginDate, endDate, PromotionalSaleID, regdate)
	values
	( '{0}', '{1}', '{2}', '{3}', now())", discount, begin, end, promotionalSaleId));
            return promotionalSaleId;
        }
        #endregion
    }
    protected void btnEbayOnsaleHaveSaleId_Click(object sender, EventArgs e)
    {
        var ids = Config.ExecuteScalar("Select PromotionalSaleID from tb_ebay_promotional_sale_id order by id desc limit 1");
        var dt = Config.ExecuteDataTable(@"select e.itemid, o.begin_datetime, o.end_datetime from tb_on_sale o inner join tb_product p on o.product_serial_no=p.product_serial_no
inner join tb_ebay_selling e on e.luc_sku = p.product_serial_no
where p.menu_child_serial_no = 350 and save_price >=100 and save_price<=120 ");
        if (dt.Rows.Count > 2)
        {
            var promotionalSaleId = ids.ToString();
            if (promotionalSaleId.Length < 12)
            {
                var itemIdString = string.Empty;
                foreach (DataRow dr in dt.Rows)
                {
                    itemIdString += "<ItemID>" + dr["itemid"].ToString() + "</ItemID>";
                }
                var result = SetPromotionalSaleListings(itemIdString, promotionalSaleId);
               // this.btnEbayOnsale.Text = result;
            }
            else
            {
              //  this.btnEbayOnsale.Text = "Error";
            }
        }
    }
}
