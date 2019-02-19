using System.Xml;
using System.Text;
using System.Net;
using System.IO;
using LU.Data;

/// <summary>
/// Summary description for EbayItemGenerate
/// </summary>
public class EbayItemGenerate : PageBase
{

    public EbayItemGenerate()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public string AddItem(nicklu2Entities context, EbayItem ei, string shippingDetail, int SKU, int Vcsid
        , bool IsSystem
        , bool IsChild)
    {
        #region settings
        try
        {
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

            string ConditionIDString = "<ConditionID>1000</ConditionID>";

            #region new or old
            try
            {
                var pm = ProductModel.GetProductModel(context, SKU);
                if (pm != null)
                {

                    if (pm.prodType != null)
                    {
                        if (pm.prodType.ToLower() == "Refurbished".ToLower())
                        {
                            ConditionIDString = "<ConditionID>2000</ConditionID>";
                        }
                    }
                }
            }
            catch { }

            #endregion


            #region Load The XML Document Template and Set the Neccessary Values
            //Load the XML Document to Use for this Request
            XmlDocument xmlDoc = new XmlDocument();

            ////Get XML Document from Embedded  Resources
            //xmlDoc.Load((Server.MapPath("/q_admin/ebayMaster/Online/Xml/GeteBayOfficialTimeRequest.xml")));

            ////Set the various node values   attr1858_26443
            //xmlDoc["GeteBayOfficialTimeRequest"]["RequesterCredentials"]["eBayAuthToken"].InnerText = userToken;


            string sendXml = string.Format(@"<?xml version=""1.0"" encoding=""utf-8""?>
<AddItemRequest xmlns=""urn:ebay:apis:eBLBaseComponents"">
  <RequesterCredentials>
    <eBayAuthToken>{0}</eBayAuthToken>
  </RequesterCredentials>
  <ErrorLanguage>en_US</ErrorLanguage>
  <WarningLevel>High</WarningLevel>
  <Item>
    {10}
    <Title><![CDATA[{2}]]></Title>
    <SubTitle><![CDATA[{9}]]></SubTitle>
    <SKU><![CDATA[{11}]]></SKU>
    {12}
    {13}
    <Description>
        {3}
    </Description>
    <PrimaryCategory>
      <CategoryID>{4}</CategoryID>
    </PrimaryCategory>
    <CategoryMappingAllowed>true</CategoryMappingAllowed>
    <Site>US</Site>
    <Quantity>{16}</Quantity>
    <StartPrice>{1}</StartPrice>
    <ListingDuration>{15}</ListingDuration>
    <ListingType>FixedPriceItem</ListingType>
    <DispatchTimeMax>3</DispatchTimeMax>
    <ShippingDetails>
      {5}
    </ShippingDetails>
    
    <Country>CA</Country>
    <Currency>CAD</Currency>
    <PostalCode>M3B 2M5</PostalCode>
    <PaymentMethods>{6}</PaymentMethods>
    <PayPalEmailAddress>{7}</PayPalEmailAddress>
    <PictureDetails>
      {8}
    </PictureDetails>
    <UseTaxTable>true</UseTaxTable>
    {17}
    {14}
    {18}
    {19}
    {20}
  </Item>
</AddItemRequest>"
                      , userToken
                      , ei.Buy_it_now_price
                      , ei.Title
                      , @"<![CDATA[" + ei.Description + "]]>"
                      , ei.Category
                      , shippingDetail == "" ? ei.GetShippingDetail(SKU, eBayProdType.system, string.Empty, IsChild) : shippingDetail
                      , ei.PaymentMethods
                      , ei.PayPalEmailAddress
                      , ei.GetPrictureDetail(ei)
                      , ei.Subtitle
                      , (Vcsid > 0 ? ei.AttriatesXml(Vcsid, new EbayGetXmlHelper().GetAttributeSetVersion(Vcsid)) : "")
                      , ei.cutom_label
                      , ei.ItemSpecificsString()
                      , ei.Store_category.Length > 0 ? string.Format("<Storefront><StoreCategoryID>{0}</StoreCategoryID></Storefront>", ei.Store_category) : ""
                      //, eBayCmdReviseItem.GetBaseOfferEnable(true) //ei.AutoPay == true ? "<AutoPay>True</AutoPay>" : (ei.AutoPay == false ? "<AutoPay>False</AutoPay>" : "")
                      , ei.AutoPay == true ? "<AutoPay>True</AutoPay>" : (ei.AutoPay == false ? "<AutoPay>False</AutoPay>" : "")
                      , ei.Duration == "" ? "Days_7" : "GTC"
                      , ei.Quantity <= 1 ? "6" : ei.Quantity.ToString()
                      , ConditionIDString
                      , EbayHelper.GetReturnPolicy(IsSystem ? 0 : SKU)
                      , IsSystem ? "" : string.IsNullOrEmpty(ei.Upc) ? "" : "<ProductListingDetails><UPC>" + ei.Upc + "</UPC></ProductListingDetails>"
                      , IsSystem ? "<ProductListingDetails><UPC>Does not apply</UPC></ProductListingDetails>" : ""
                      );

            //throw new Exception(sendXml);
            var esxhm = new tb_ebay_send_xml_history(); // EbaySendXmlHistoryModel();
            esxhm.Content = sendXml;
            esxhm.is_sys = IsSystem;
            esxhm.SKU = SKU;
            //esxhm.Create();
            context.tb_ebay_send_xml_history.Add(esxhm);
            context.SaveChanges();

            xmlDoc.LoadXml(sendXml);

            //throw new Exception(ItemAttribates);


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
            request.Headers.Add("X-EBAY-API-COMPATIBILITY-LEVEL: 685");

            //Add function name, SiteID and Detail Level to HTTP Headers
            request.Headers.Add("X-EBAY-API-CALL-NAME: AddItem");
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

            var esrhm = new tb_ebay_send_xml_result_history();// EbaySendXmlResultHistoryModel();
            esrhm.Content = sssss;
            esrhm.is_sys = IsSystem;
            esrhm.SKU = SKU;
            // esrhm.Create();
            context.tb_ebay_send_xml_result_history.Add(esrhm);
            context.SaveChanges();

            //get the root node, for ease of use
            XmlNode root = xmlDoc["AddItemResponse"];

            //There have been Errors
            if (root["Errors"] != null)
            {
                if (root["ItemID"].InnerText.Length == 12)
                    return root["ItemID"].InnerText;
                //Response.Write(root["Ack"].InnerText);
                string errorCode = root["Errors"]["ErrorCode"].InnerText;
                string errorShort = root["Errors"]["ShortMessage"].InnerText;
                string errorLong = root["Errors"]["LongMessage"].InnerText;

                //Output the error message
                return (errorCode + " ERROR: " + errorShort);
                //Response.Write(errorLong + "\n");
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
        catch
        {
            //System.IO.StreamWriter sw = new System.IO.StreamWriter(Server.MapPath("~/e.txt"));
            //sw.Write(e.StackTrace);
            //sw.Close();
        }
        return "";
    }


    public static string GetItem(nicklu2Entities context, string itemId, string outputSelector, int SKU, bool IsSys)
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
<GetItemRequest xmlns=""urn:ebay:apis:eBLBaseComponents"">
  <ItemID>{1}</ItemID>
  <RequesterCredentials>
    <eBayAuthToken>{0}</eBayAuthToken>
  </RequesterCredentials>
  <WarningLevel>High</WarningLevel>
  {2}
</GetItemRequest>
", userToken
 , itemId
 , outputSelector);

        // save to db.
        var esxhm = new tb_ebay_send_xml_history();// EbaySendXmlHistoryModel();
        esxhm.Content = sendXml;
        esxhm.is_sys = IsSys;
        esxhm.SKU = SKU;
        // esxhm.Create();
        context.tb_ebay_send_xml_history.Add(esxhm);
        context.SaveChanges();

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
        request.Headers.Add("X-EBAY-API-COMPATIBILITY-LEVEL: 765");

        //Add function name, SiteID and Detail Level to HTTP Headers
        request.Headers.Add("X-EBAY-API-CALL-NAME: GetItem");
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
                return "Request Timed-Out.";
            else
                return wEx.Message;



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

        var esrhm = new tb_ebay_send_xml_result_history();// EbaySendXmlResultHistoryModel();
        esrhm.Content = sssss;
        esrhm.is_sys = IsSys;
        esrhm.SKU = SKU;
        // esrhm.Create();
        context.tb_ebay_send_xml_result_history.Add(esrhm);
        context.SaveChanges();
        return sssss;
        ////get the root node, for ease of use
        //XmlNode root = xmlDoc["AddItemResponse"];

        ////There have been Errors
        //if (root["Errors"] != null)
        //{
        //    //if (root["ItemID"].InnerText.Length == 12)
        //    //    return root["ItemID"].InnerText;
        //    ////Response.Write(root["Ack"].InnerText);
        //    //string errorCode = root["Errors"]["ErrorCode"].InnerText;
        //    //string errorShort = root["Errors"]["ShortMessage"].InnerText;
        //    //string errorLong = root["Errors"]["LongMessage"].InnerText;

        //    ////Output the error message
        //    //return (errorCode + " ERROR: " + errorShort);
        //    //Response.Write(errorLong + "\n");
        //}
        //else
        //{
        //    //return root["ItemID"].InnerText;
        //    //Get Result String
        //    //string theTime = root["Timestamp"].InnerText;
        //    //Put into a DateTime object
        //    // DateTime dt = DateTime.Parse(theTime);
        //    //Display the result
        //    //Response.Write("Current eBay time is: " + DateTime.Now.ToString("dd-MMM-yyyy  HH:mm:ss") + " GMT");
        //}
        #endregion
        //return "Error...";
        //Response.Write("Press Enter to Close.");
    }
}
