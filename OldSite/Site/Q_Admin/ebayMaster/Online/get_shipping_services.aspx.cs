using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Reflection;
using System.Xml;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Text;
using System.Net;
using System.IO;


public partial class Q_Admin_ebayMaster_Online_get_shipping_services : PageBase
{

    string XML_STORE_PATH = "/q_admin/ebayMaster/Online/Xml/";

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

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) {

            GetEbayXml("ShippingCarrierDetails");
            GetEbayXml("ShippingServiceDetails");
            SetStoreCategories();
            GetStore();
        }
    }
    
    private HttpWebRequest GetHttpWebRequestHeader(int uft8BytesLength, string apiCallName)
    {
        if (apiCallName.Length == 0)
        {
            throw new Exception("API Call Name is null.");
            
        }
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(serverUrl);

        //Set Request Method (POST) and Content Type (text/xml)
        request.Method = "POST";
        request.ContentType = "text/xml";
        request.ContentLength = uft8BytesLength;// utf8Bytes.Length;

        //Add the Keys to the HTTP Headers
        request.Headers.Add("X-EBAY-API-DEV-NAME: " + devID);
        request.Headers.Add("X-EBAY-API-APP-NAME: " + appID);
        request.Headers.Add("X-EBAY-API-CERT-NAME: " + certID);

        //Add Compatability Level to HTTP Headers
        //Regulates versioning of the XML interface for the API
        request.Headers.Add("X-EBAY-API-COMPATIBILITY-LEVEL: 551");

        //Add function name, SiteID and Detail Level to HTTP Headers
        request.Headers.Add("X-EBAY-API-CALL-NAME: " + apiCallName);
        request.Headers.Add("X-EBAY-API-SITEID: " + siteID.ToString());

        //Time out = 15 seconds,  set to -1 for no timeout.
        //If times-out - throws a WebException with the
        //Status property set to WebExceptionStatus.Timeout.
        request.Timeout = 15000;

        return request;
    }
    public void GetEbayXml(string detailName){


        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(string.Format(@"<?xml version=""1.0"" encoding=""utf-8""?> 
<GeteBayDetailsRequest xmlns=""urn:ebay:apis:eBLBaseComponents""> 
  <RequesterCredentials> 
    <eBayAuthToken>{0}</eBayAuthToken> 
  </RequesterCredentials> 
  <DetailName>{1}</DetailName> 
</GeteBayDetailsRequest>", userToken, detailName));

        string xmlText = xmlDoc.InnerXml;
        UTF8Encoding encoding = new UTF8Encoding();
        int dataLen = encoding.GetByteCount(xmlText);
        byte[] utf8Bytes = new byte[dataLen];
        Encoding.UTF8.GetBytes(xmlText, 0, xmlText.Length, utf8Bytes, 0);



        #region Setup The Request (inc. HTTP Headers)
        //Create a new HttpWebRequest object for the ServerUrl
        HttpWebRequest request = GetHttpWebRequestHeader(utf8Bytes.Length, "GeteBayDetails");

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

            return;
        }
        #endregion

        StreamReader sr = new StreamReader(str);
        string sssss = sr.ReadToEnd();
        xmlDoc.LoadXml(sssss);
        sr.Close();
        str.Close();

        StreamWriter sw = new StreamWriter(Server.MapPath(XML_STORE_PATH + detailName + ".xml"));
        sw.Write(sssss);
        sw.Close();
        sw.Dispose();

    }



    #region GetStore
    private void SetStoreCategories()
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(Server.MapPath(XML_STORE_PATH + "SetStoreCategoriesInput.xml"));
        xmlDoc["SetStoreCategoriesRequest"]["RequesterCredentials"]["eBayAuthToken"].InnerText = userToken;
        //xmlDoc["GetStoreRequest"]["CategorySiteID"].InnerText = siteID.ToString();

        string xmlText = xmlDoc.InnerXml;
        UTF8Encoding encoding = new UTF8Encoding();
        int dataLen = encoding.GetByteCount(xmlText);
        byte[] utf8Bytes = new byte[dataLen];
        Encoding.UTF8.GetBytes(xmlText, 0, xmlText.Length, utf8Bytes, 0);



        #region Setup The Request (inc. HTTP Headers)
        //Create a new HttpWebRequest object for the ServerUrl
        HttpWebRequest request = GetHttpWebRequestHeader(utf8Bytes.Length, "SetStoreCategories");

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

            return;
        }
        #endregion
        StreamReader sr = new StreamReader(str);
        string sssss = sr.ReadToEnd();
        xmlDoc.LoadXml(sssss);
        sr.Close();
        str.Close();

        StreamWriter sw = new StreamWriter(Server.MapPath(XML_STORE_PATH + "SetStoreCategoriesOutput.xml"));
        sw.Write(sssss);
        sw.Close();
        sw.Dispose();
    }

    public void GetStore()
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(Server.MapPath(XML_STORE_PATH + "GetStoreInput.xml"));
        xmlDoc["GetStoreRequest"]["RequesterCredentials"]["eBayAuthToken"].InnerText = userToken;
        //xmlDoc["GetStoreRequest"]["CategorySiteID"].InnerText = siteID.ToString();

        string xmlText = xmlDoc.InnerXml;
        UTF8Encoding encoding = new UTF8Encoding();
        int dataLen = encoding.GetByteCount(xmlText);
        byte[] utf8Bytes = new byte[dataLen];
        Encoding.UTF8.GetBytes(xmlText, 0, xmlText.Length, utf8Bytes, 0);



        #region Setup The Request (inc. HTTP Headers)
        //Create a new HttpWebRequest object for the ServerUrl
        HttpWebRequest request = GetHttpWebRequestHeader(utf8Bytes.Length,"GetStore");

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

            return;
        }
        #endregion
        StreamReader sr = new StreamReader(str);
        string sssss = sr.ReadToEnd();
        xmlDoc.LoadXml(sssss);
        sr.Close();
        str.Close();

        StreamWriter sw = new StreamWriter(Server.MapPath(XML_STORE_PATH  + "GetStoreOutput.xml"));
        sw.Write(sssss);
        sw.Close();
        sw.Dispose();
    }
    #endregion
}
