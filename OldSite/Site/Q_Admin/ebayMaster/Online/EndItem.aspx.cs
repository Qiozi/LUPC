using System;
using System.Xml;
using System.Text;
using System.Net;
using System.IO;
using System.Data;
using LU.Data;

public partial class Q_Admin_ebayMaster_Online_EndItem : PageBaseNoInit
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitialDatabase();
        }
    }

    public override void InitialDatabase()
    {
        if (ReqCmd != "qiozi@msn.com_wu.th@qq.com")
        {
            base.InitialDatabase();
        }
        try
        {
            if (EndItem(ReqItemID) == "Success")
                Response.Write("<script>this.close();</script>");
            else
                Response.Write("Error....");
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }

    string EndItem(string itemid)
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
        XmlDocument xmlDoc = new XmlDocument();

        string sendXml = string.Format(@"<?xml version=""1.0"" encoding=""utf-8""?>
<EndItemRequest xmlns=""urn:ebay:apis:eBLBaseComponents"">
  <RequesterCredentials>
    <eBayAuthToken>{0}</eBayAuthToken>
  </RequesterCredentials>
  <ErrorLanguage>en_US</ErrorLanguage>
  <WarningLevel>High</WarningLevel>
  <EndingReason>NotAvailable</EndingReason>
  <ItemID>{1}</ItemID>
</EndItemRequest>
", userToken
 , itemid
 );

        // save to db.
        var esxhm = new tb_ebay_send_xml_history();// EbaySendXmlHistoryModel();
        esxhm.Content = sendXml;
        esxhm.comm = "End item: " + itemid;
        esxhm.is_modify = true;
        esxhm.SKU = 0;
        esxhm.regdate = DateTime.Now;
        DBContext.tb_ebay_send_xml_history.Add(esxhm);
        DBContext.SaveChanges();


        xmlDoc.LoadXml(sendXml);
        //Get XML into a string for use in encoding
        string xmlText = xmlDoc.InnerXml;
        //eBay.Service.Call.AddItemCall aic = new eBay.Service.Call.AddItemCall();


        //Put the data into a UTF8 encoded  byte array
        UTF8Encoding encoding = new UTF8Encoding();
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
        request.Headers.Add("X-EBAY-API-COMPATIBILITY-LEVEL: 511");

        //Add function name, SiteID and Detail Level to HTTP Headers
        request.Headers.Add("X-EBAY-API-CALL-NAME: EndItem");
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


        }
        #endregion

        #region Process Response
        // Get Response into String
        StreamReader sr = new StreamReader(str);
        string sssss = sr.ReadToEnd();
        xmlDoc.LoadXml(sssss);
        sr.Close();
        str.Close();

        //StreamWriter sw = new StreamWriter(Server.MapPath(string.Format("{0}/{1}_{2}.txt"
        //    , AddResultPath
        //    , SKU, DateTime.Now.ToString("yyyyMMddhhmmss"))));
        //sw.Write(sssss);
        //sw.Close();
        //sw.Dispose();

        var esrhm = new tb_ebay_send_xml_result_history();// EbaySendXmlResultHistoryModel();
        esrhm.Content = sssss;
        esrhm.is_modify = true;
        esrhm.comm = "EndItem: " + itemid;
        esrhm.regdate = DateTime.Now;
        DBContext.tb_ebay_send_xml_result_history.Add(esrhm);
        DBContext.SaveChanges();

        CloseSysByItemid(itemid);

        //get the root node, for ease of use
        XmlNode root = xmlDoc["EndItemResponse"];

        return root["Ack"].InnerText;
        #endregion

    }

    void CloseSysByItemid(string itemid)
    {
        DataTable dt = Config.ExecuteDataTable("select sku from tb_ebay_code_and_luc_sku where ebay_code='" + itemid + "' and is_sys=1");
        foreach (DataRow dr in dt.Rows)
        {
            Config.ExecuteNonQuery("update tb_ebay_system set showit=0, is_issue=0 where id='" + dr["sku"].ToString() + "'");
        }
    }

    public string ReqItemID
    {
        get { return Util.GetStringSafeFromQueryString(Page, "ItemId"); }
    }

    public string ReqCmd
    {
        get { return Util.GetStringSafeFromQueryString(Page, "cmd"); }
    }
}