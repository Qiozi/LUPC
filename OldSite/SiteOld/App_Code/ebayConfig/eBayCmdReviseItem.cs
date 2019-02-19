using System;
using System.Collections.Generic;
using System.Xml;
using System.Web;
using System.Net;
using System.IO;
using System.Text;

/// <summary>
/// Summary description for eBayCmdReviseItem
/// </summary>
public class eBayCmdReviseItem
{
    public eBayCmdReviseItem()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    /// <summary>
    /// revise ebay item 
    /// </summary>
    /// <param name="itemid"></param>
    /// <param name="isSys"></param>
    /// <param name="Description"></param>
    /// <param name="buyItNowPrice"></param>
    /// <param name="sku"></param>
    /// <param name="shippingString"></param>
    /// <returns></returns>
    public static string Revise(string itemid, bool isSys
         , string Description
         , decimal buyItNowPrice
         , int sku
         , string shippingString
         , bool? autoPay
         )
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
<ReviseItemRequest xmlns=""urn:ebay:apis:eBLBaseComponents"">
  <RequesterCredentials>
    <eBayAuthToken>{0}</eBayAuthToken>
  </RequesterCredentials>
  <ErrorLanguage>en_US</ErrorLanguage>
  <WarningLevel>High</WarningLevel>
  <Item>
    <ItemID>{1}</ItemID>
     {2}
     {3}
     {4}
     {5}
  </Item>
</ReviseItemRequest>
", userToken
 , itemid
 , buyItNowPrice < 5M ? "" : "<StartPrice>" + buyItNowPrice.ToString() + "</StartPrice>"
 , Description == null ? "" : @"<Description><![CDATA[" + Description + "]]></Description>"
 , (shippingString == null || shippingString == "") ? "" : "<ShippingDetails>" + shippingString + "</ShippingDetails>"
 , autoPay == null ? "" : ("<AutoPay>" + (autoPay == true ? "True" : "False") + "</AutoPay>"));

        // save to db.
        EbaySendXmlHistoryModel esxhm = new EbaySendXmlHistoryModel();
        esxhm.Content = sendXml;
        esxhm.is_sys = isSys;
        esxhm.SKU = sku;
        esxhm.is_modify = true;
        esxhm.Create();


        xmlDoc.LoadXml(sendXml);
        //Get XML into a string for use in encoding
        string xmlText = xmlDoc.InnerXml;
        //eBay.Service.Call.AddItemCall aic = new eBay.Service.Call.AddItemCall();


        //Put the data into a UTF8 encoded  byte array
        System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
        int dataLen = encoding.GetByteCount(xmlText);
        byte[] utf8Bytes = new byte[dataLen];
        System.Text.Encoding.UTF8.GetBytes(xmlText, 0, xmlText.Length, utf8Bytes, 0);
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
        request.Headers.Add("X-EBAY-API-COMPATIBILITY-LEVEL: 691");

        //Add function name, SiteID and Detail Level to HTTP Headers
        request.Headers.Add("X-EBAY-API-CALL-NAME: ReviseItem");
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
                throw new Exception("Request Timed-Out.");
            else
                throw new Exception(wEx.Message);

            throw new Exception("Press Enter to Continue...");
        }
        #endregion

        //StreamWriter sw = new StreamWriter(Server.MapPath("/q_admin/ebayMaster/Online/Xml/additem.txt"));
        //sw.Write(str);
        //sw.Close();
        //sw.Dispose();


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

        EbaySendXmlResultHistoryModel esrhm = new EbaySendXmlResultHistoryModel();
        esrhm.Content = sssss;
        esrhm.is_sys = isSys;
        esrhm.SKU = sku;
        esrhm.is_modify = true;
        esrhm.Create();

        //get the root node, for ease of use
        XmlNode root = xmlDoc["ReviseItemResponse"];

        //There have been Errors
        if (root["Errors"] != null)
        {
            //Response.Write(root["Ack"].InnerText);
            string errorCode = root["Errors"]["ErrorCode"].InnerText;
            string errorShort = root["Errors"]["ShortMessage"].InnerText;
            string errorLong = root["Errors"]["LongMessage"].InnerText;

            //Output the error message
            return (errorCode + " ERROR: " + errorShort);

        }
        else
        {
            //Get Result String
            return root["ItemID"].InnerText;
        }
        #endregion
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="itemid"></param>
    /// <param name="isSys"></param>
    /// <param name="Description"></param>
    /// <param name="buyItNowPrice"></param>
    /// <param name="sku"></param>
    /// <param name="shippingString"></param>
    /// <param name="logoPrictureUrl"></param>
    /// <param name="itemSpecifics"></param>
    /// <param name="eBayTitle"></param>
    /// <param name="ebayType"></param>
    /// <returns></returns>
    public static string ReviseItem(string itemid, bool isSys
    , string Description
    , decimal buyItNowPrice
    , int sku
    , string shippingString
    , string logoPrictureUrl
    , string itemSpecifics
    , string eBayTitle
    , eBayModifyType ebayType)
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
<ReviseItemRequest xmlns=""urn:ebay:apis:eBLBaseComponents"">

  <RequesterCredentials>
    <eBayAuthToken>{0}</eBayAuthToken>
  </RequesterCredentials>
  <ErrorLanguage>en_US</ErrorLanguage>
  <WarningLevel>High</WarningLevel>
  <Item>
    <ItemID>{1}</ItemID>
     
     {2}
     {3}
     {4}
     {5}
     {6}
     {7}
     {8}
     {9}
     {10}
     {11}
     {12}
  </Item>
</ReviseItemRequest>
", userToken
 , itemid
 , (ebayType == eBayModifyType.price || ebayType == eBayModifyType.priceAndDesc) ? (buyItNowPrice < 5M ? "" : "<StartPrice>" + buyItNowPrice.ToString() + "</StartPrice>") : ""
 , (ebayType == eBayModifyType.description || ebayType == eBayModifyType.priceAndDesc) ? (Description == null ? "" : @"<Description><![CDATA[" + Description + "]]></Description>") : ""
 , ebayType == eBayModifyType.shippingFee ? shippingString ?? "" : ""
 , EbaySettings.eBayModifyPartQuantity > 0 ? "<Quantity>" + EbaySettings.eBayModifyPartQuantity.ToString() + "</Quantity>" : ""
 , (ebayType == eBayModifyType.price || ebayType == eBayModifyType.priceAndDesc) ? ((buyItNowPrice >= 2000 || buyItNowPrice < 5M) && Description == null) ? "<AutoPay>False</AutoPay>" : (Description != null ? "" : "<AutoPay>True</AutoPay>") : ""
 , ebayType == eBayModifyType.logo ? (string.IsNullOrEmpty(logoPrictureUrl) ? "" : (string.Format("<PictureDetails><PictureURL>{0}</PictureURL></PictureDetails>{1}", logoPrictureUrl, string.IsNullOrEmpty(eBayTitle) ? "" : "<Title><![CDATA[" + eBayTitle + "]]></Title>"))) : ""
 , !string.IsNullOrEmpty(itemSpecifics) ? itemSpecifics : ""
 , ebayType == eBayModifyType.storeCategory ? "<Storefront><StoreCategoryID>" + eBayTitle + "</StoreCategoryID></Storefront>" : ""
 , EbayHelper.GetReturnPolicy(isSys ? 0 : sku)
 , ebayType == eBayModifyType.logo ? (string.IsNullOrEmpty(Description) ? "" : "<Storefront><StoreCategoryID>" + Description + "</StoreCategoryID></Storefront>") : ""
 , isSys ? "<ProductListingDetails><UPC>Does not apply</UPC><BrandMPN><MPN>Does not apply</MPN></BrandMPN></ProductListingDetails>" : ""
 );


        // save to db.
        EbaySendXmlHistoryModel esxhm = new EbaySendXmlHistoryModel();
        esxhm.Content = sendXml;
        esxhm.is_sys = isSys;
        esxhm.SKU = sku;
        esxhm.is_modify = true;
        esxhm.Create();


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
        request.Headers.Add("X-EBAY-API-COMPATIBILITY-LEVEL: 691");

        //Add function name, SiteID and Detail Level to HTTP Headers
        request.Headers.Add("X-EBAY-API-CALL-NAME: ReviseItem");
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
                throw new Exception("Request Timed-Out.");
            else
                throw new Exception(wEx.Message);

            //Response.Write("Press Enter to Continue...");


        }
        #endregion

        //StreamWriter sw = new StreamWriter(Server.MapPath("/q_admin/ebayMaster/Online/Xml/additem.txt"));
        //sw.Write(str);
        //sw.Close();
        //sw.Dispose();


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

        EbaySendXmlResultHistoryModel esrhm = new EbaySendXmlResultHistoryModel();
        esrhm.Content = sssss;
        esrhm.is_sys = isSys;
        esrhm.SKU = sku;
        esrhm.is_modify = true;
        esrhm.Create();

        //get the root node, for ease of use
        XmlNode root = xmlDoc["ReviseItemResponse"];

        //There have been Errors
        if (root["Errors"] != null)
        {
            //Response.Write(root["Ack"].InnerText);
            string errorCode = root["Errors"]["ErrorCode"].InnerText;
            string errorShort = root["Errors"]["ShortMessage"].InnerText;
            string errorLong = root["Errors"]["LongMessage"].InnerText;

            //Output the error message
            return (errorCode + " ERROR: " + errorShort);

        }
        else
        {
            return root["ItemID"].InnerText;
            //Get Result String
            //string theTime = root["Timestamp"].InnerText;
            //Put into a DateTime object
            // DateTime dt = DateTime.Parse(theTime);
            //Display the result
            //Response.Write("Current eBay time is: " + DateTime.Now.ToString("dd-MMM-yyyy  HH:mm:ss") + " GMT");
        }
        #endregion
        //return "Error...";
        //Response.Write("Press Enter to Close.");
    }
}