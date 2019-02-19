using System;
using System.IO;
using System.Xml;
using System.Text;
using System.Net;

/// <summary>
/// Summary description for EbayGetXmlHelper
/// </summary>
public class EbayGetXmlHelper : PageBase
{

    string XmlPath = EbaySettings.ebayMasterXmlPath;
    string CategoryAttributeXmlPath = EbaySettings.ebayMasterXmlPath + "categoryAttribute/";
    string CategorySpecificsXmlPath = EbaySettings.ebayMasterXmlPath + "ItemSpecifics/";
    string CategoryFeaturesXmlPath = EbaySettings.ebayMasterXmlPath + "CategoryFeatures/";
    string Category2CSXmlPath = EbaySettings.ebayMasterXmlPath + "Category2CS/";
    string AttributeSetVersionPath = EbaySettings.ebayMasterXmlPath + "AttributeSetVersion/";
    //string GetOrders = "";

    //XmlDocument localCatTree = null;

    public EbayGetXmlHelper()
    {
        //
        // TODO: Add constructor logic here
        //
    }



    #region methods
    /// <summary>
    /// 
    /// </summary>
    /// <param name="method_name"></param>
    /// <param name="requestXmlString"></param>
    /// <param name="responseXmlSavePath"></param>
    /// <returns></returns>
    public string GetEbayXml(string method_name, string requestXmlString, string responseXmlSavePath)
    {
        string result = "";

        #region Load The XML Document Template and Set the Neccessary Values
        //Load the XML Document to Use for this Request
        XmlDocument xmlDoc = new XmlDocument();

        xmlDoc.LoadXml(requestXmlString);

        //Get XML into a string for use in encoding
        string xmlText = xmlDoc.InnerXml;

        //Put the data into a UTF8 encoded  byte array
        UTF8Encoding encoding = new UTF8Encoding();
        int dataLen = encoding.GetByteCount(xmlText);
        byte[] utf8Bytes = new byte[dataLen];
        Encoding.UTF8.GetBytes(xmlText, 0, xmlText.Length, utf8Bytes, 0);
        #endregion

        #region Setup The Request (inc. HTTP Headers)
        //Create a new HttpWebRequest object for the ServerUrl
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(EbaySettings.serverUrl);

        //Set Request Method (POST) and Content Type (text/xml)
        request.Method = "POST";
        request.ContentType = "text/xml";
        request.ContentLength = utf8Bytes.Length;

        //Add the Keys to the HTTP Headers
        request.Headers.Add("X-EBAY-API-DEV-NAME: " + EbaySettings.devID);
        request.Headers.Add("X-EBAY-API-APP-NAME: " + EbaySettings.appID);
        request.Headers.Add("X-EBAY-API-CERT-NAME: " + EbaySettings.certID);

        //Add Compatability Level to HTTP Headers
        //Regulates versioning of the XML interface for the API
        request.Headers.Add("X-EBAY-API-COMPATIBILITY-LEVEL: " + EbaySettings.API_COMPATIBILITY_LEVEL);

        //Add function name, SiteID and Detail Level to HTTP Headers
        request.Headers.Add("X-EBAY-API-CALL-NAME: " + method_name);
        request.Headers.Add("X-EBAY-API-SITEID: " + EbaySettings.siteID.ToString());

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
            //if (wEx.Status == WebExceptionStatus.Timeout)
            //    Response.Write("Request Timed-Out.");
            //else
            //    Response.Write(wEx.Message);
            throw wEx;
        }
        #endregion

        #region Process Response
        // Get Response into String
        StreamReader sr = new StreamReader(str);
        result = sr.ReadToEnd();
        if (responseXmlSavePath != null)
        {
            StreamWriter sw = new StreamWriter(responseXmlSavePath);
            sw.Write(result);
            //xmlDoc.LoadXml(sr.ReadToEnd());
            sw.Close();
        }
        sr.Close();
        str.Close();

        //get the root node, for ease of use
        //XmlNode root = xmlDoc["GetCategoriesResponse"];

        ////There have been Errors
        //if (root["Errors"] != null)
        //{
        //    string errorCode = root["Errors"]["ErrorCode"].InnerText;
        //    string errorShort = root["Errors"]["ShortMessage"].InnerText;
        //    string errorLong = root["Errors"]["LongMessage"].InnerText;

        //    //Output the error message
        //    Response.Write(errorCode + " ERROR: " + errorShort);
        //    Response.Write(errorLong + "\n");

        //    return false;
        //}
        //else
        //{
        //    //Save the file to disk
        //    StreamWriter sw = new StreamWriter(Server.MapPath(this.XmlPath + "CatTree.xml"), false, Encoding.UTF8);
        //    xmlDoc.Save(sw);
        //    sw.Close();
        //}

        return result;
        #endregion

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="xmlString"></param>
    /// <param name="xmlNodePath"></param>
    /// <returns></returns>
    public string GetAPIVision(string xmlString, string xmlNodePath)
    {
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(xmlString);
        string vision = doc[xmlNodePath]["Version"].InnerText;
        return vision;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="categoryID"></param>
    /// <param name="method_name"></param>
    /// <param name="rootVersionString"></param>
    /// <param name="xmlCategorypath"></param>
    /// <param name="xmlPath"></param>
    /// <returns></returns>
    private string GetXml(int categoryID, string method_name, string rootVersionString, string xmlCategorypath, string xmlPath)
    {
        //string method_name = "GetCategoryFeatures";
        string rootNodeString = rootVersionString;
        string result = "";

        string fullFile = Server.MapPath(xmlCategorypath + categoryID.ToString() + ".xml");
        string apiXMLFullFile = Server.MapPath(xmlPath + method_name + ".xml");


        if (File.Exists(fullFile))
        {
            FileInfo fi = new FileInfo(fullFile);
            DateTime lastWriteTime = fi.LastWriteTime;
            if (DateTime.Now.DayOfYear - lastWriteTime.DayOfYear > EbaySettings.ebayAPIUpdateDateDiff ||
                DateTime.Now.DayOfYear - lastWriteTime.DayOfYear < int.Parse("-" + EbaySettings.ebayAPIUpdateDateDiff.ToString()))
            {
                string xmlString = FileHelper.ReadFile(apiXMLFullFile);

                if (xmlString.Trim().Length < 30)
                    throw new Exception("File is null.");

                //Get XML Document from Embedded  Resources
                string requestXmlString = string.Format(xmlString, categoryID, EbaySettings.userToken);

                string oldXmlString = FileHelper.ReadFile(fullFile);

                result = GetEbayXml(method_name
                    , requestXmlString
                    , null
                    );

                //
                // Compare Vision
                //
                if (GetAPIVision(result, rootNodeString) != GetAPIVision(oldXmlString, rootNodeString))
                {
                    FileHelper.GenerateFile(fullFile, result);
                }
                else
                    FileHelper.GenerateFile(fullFile, oldXmlString);
            }
            else
            {

                result = FileHelper.ReadFile(fullFile);

            }
        }
        else
        {
            string xmlString = FileHelper.ReadFile(apiXMLFullFile);
            string requestXmlString = string.Format(xmlString, categoryID, EbaySettings.userToken);

            result = GetEbayXml(method_name
               , requestXmlString
               , fullFile);
        }
        //if (result != "")
        //    IsExcludeCategoryID = GetCategory2CSIsExcludeCategoryID(result, categoryID);

        return result;
    }

    

    #endregion

    public string GetCategoryTree()
    {
        string result = "";
        string method_name = "GetCategories";
        XmlDocument localCatTree = new XmlDocument();

        string fullFile = Server.MapPath(XmlPath + "CatTree.xml");

        string storeFullFile = fullFile;
        string rootNodeString = "GetCategoriesResponse";

        if (File.Exists(fullFile))
        {
            FileInfo fi = new FileInfo(fullFile);
            DateTime lastWriteTime = fi.LastWriteTime;
            if (DateTime.Now.DayOfYear - lastWriteTime.DayOfYear > EbaySettings.ebayAPIUpdateDateDiff ||
                DateTime.Now.DayOfYear - lastWriteTime.DayOfYear < int.Parse("-" + EbaySettings.ebayAPIUpdateDateDiff.ToString()))
            {

                string xmlString = FileHelper.ReadFile(Server.MapPath(this.XmlPath + method_name + ".xml"));
                string requestXmlString = string.Format(xmlString, EbaySettings.userToken);

                result = GetEbayXml(method_name
                   , requestXmlString
                   , null);

                string oldCatTreeString = FileHelper.ReadFile(fullFile);

                //
                // Compare Vision
                //
                if (GetAPIVision(result, rootNodeString) != GetAPIVision(oldCatTreeString, rootNodeString))
                {
                    FileHelper.GenerateFile(fullFile, result);
                }
                else
                    FileHelper.GenerateFile(fullFile, oldCatTreeString);
            }
            else
            {
                result = FileHelper.ReadFile(fullFile);

            }
        }
        else
        {
            string xmlString = FileHelper.ReadFile(Server.MapPath(this.XmlPath + method_name + ".xml"));
            string requestXmlString = string.Format(xmlString, EbaySettings.userToken);

            result = GetEbayXml(method_name
               , requestXmlString
               , fullFile);
        }
        return result;
    }

    #region Category Features
    /// <summary>
    /// 
    /// </summary>
    /// <param name="categoryID"></param>
    /// <returns></returns>
    public string GetCategoryFeatures(int categoryID)
    {
        string method_name = "GetCategoryFeatures";
        string rootNodeString = "GetCategoryFeaturesResponse";

        return GetXml(categoryID, method_name, rootNodeString, this.CategoryFeaturesXmlPath, this.XmlPath);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="xmlStr"></param>
    /// <returns></returns>
    public bool IsItemSpecificsEnabled(string xmlStr)
    {
        if (xmlStr.Length > 10)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlStr);

            if (doc["GetCategoryFeaturesResponse"]["Category"]["ItemSpecificsEnabled"] != null)
            {
                return doc["GetCategoryFeaturesResponse"]["Category"]["ItemSpecificsEnabled"].InnerText == "Enabled";

            }
        }
        else
            throw new ArgumentException("String is null of IsItemSpecificsEnabled");
            return false;
    }
    #endregion

    public string GetCategorySpecifics(int categoryID)
    {
        string method_name = "GetCategorySpecifics";
        string xmlString = FileHelper.ReadFile(Server.MapPath(this.XmlPath + method_name + ".xml"));

        if (xmlString.Trim().Length < 30)
            throw new Exception("File is null.");

        //Get XML Document from Embedded  Resources
        string requestXmlString = string.Format(xmlString, categoryID, EbaySettings.userToken);

        return GetEbayXml(method_name
            , requestXmlString
            , Server.MapPath(this.CategorySpecificsXmlPath + categoryID.ToString() + ".xml")
            );
    }

    public string GetItemRecommendations(int categoryID)
    {
        string method_name = "GetItemRecommendations";
        string xmlString = FileHelper.ReadFile(Server.MapPath(this.XmlPath + method_name + ".xml"));

        if (xmlString.Trim().Length < 30)
            throw new Exception("File is null.");

        //Get XML Document from Embedded  Resources
        string requestXmlString = string.Format(xmlString, categoryID, EbaySettings.userToken);

        return GetEbayXml(method_name
            , requestXmlString
            , Server.MapPath(this.CategoryAttributeXmlPath + categoryID.ToString() + ".xml")
            );
    }

    public string GeteBayDetails()
    {
        string method_name = "GeteBayDetails";
        string xmlString = FileHelper.ReadFile(Server.MapPath(this.XmlPath + method_name + ".xml"));

        if (xmlString.Trim().Length < 30)
            throw new Exception("File is null.");

        //Get XML Document from Embedded  Resources
        string requestXmlString = string.Format(xmlString, EbaySettings.userToken);

        return GetEbayXml(method_name
            , requestXmlString
            , Server.MapPath(XmlPath + method_name + "Response.xml")
            );
    }

    public int GetAttributesCS(string category2CS)
    {
        int setID = -1;
        string ReturnResult = "";
        string method_name = "GetAttributesCS";
        string xmlString = FileHelper.ReadFile(Server.MapPath(this.XmlPath + method_name + ".xml"));
        string rootNodeString = "GetAttributesCSResponse";

        //
        // get AttributesCS Set ID.
        //

        if (category2CS.Length > 100)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(category2CS);


            try
            {
                int.TryParse(doc["GetCategory2CSResponse"]["MappedCategoryArray"]["Category"]["CharacteristicsSets"]["AttributeSetID"].InnerText
                    , out setID);
            }
            catch {
                int.TryParse(doc["GetCategory2CSResponse"]["SiteWideCharacteristicSets"]["CharacteristicsSet"]["AttributeSetID"].InnerText
                        , out setID);
            }
            try
            {
                SaveAttributeSetVersion(setID, doc["GetCategory2CSResponse"]["MappedCategoryArray"]["Category"]["CharacteristicsSets"]["AttributeSetVersion"].InnerText);
            }
            catch
            {
                SaveAttributeSetVersion(setID, doc["GetCategory2CSResponse"]["SiteWideCharacteristicSets"]["CharacteristicsSet"]["AttributeSetVersion"].InnerText);
            }

            string fullFile = Server.MapPath(this.CategoryAttributeXmlPath + setID.ToString() + ".xml");
            string fullFileEbayTag = Server.MapPath(this.CategoryAttributeXmlPath + setID.ToString() + "_ebay.xml");

            if (File.Exists(fullFile))
            {
                FileInfo fi = new FileInfo(fullFile);
                DateTime lastWriteTime = fi.LastWriteTime;
                if (DateTime.Now.DayOfYear - lastWriteTime.DayOfYear > EbaySettings.ebayAPIUpdateDateDiff ||
                    DateTime.Now.DayOfYear - lastWriteTime.DayOfYear < int.Parse("-" + EbaySettings.ebayAPIUpdateDateDiff.ToString()))
                {

                    if (xmlString.Trim().Length < 30)
                        throw new Exception("GetAttributesCS File is null.");

                    string oldXmlString = FileHelper.ReadFile(fullFile);

                    //Get XML Document from Embedded  Resources
                    string requestXmlString = string.Format(xmlString, setID, EbaySettings.userToken);

                    ReturnResult = GetEbayXml(method_name
                        , requestXmlString
                        , null
                        );

                    //
                    // Compare Vision
                    //
                    if (GetAPIVision(ReturnResult, rootNodeString) != GetAPIVision(oldXmlString, rootNodeString))
                    {
                        FileHelper.GenerateFile(fullFile, ReturnResult);
                        SaveAttributesCSEbayTag(ReturnResult, fullFileEbayTag, setID);
                    }
                    else
                        FileHelper.GenerateFile(fullFile, oldXmlString);
                }
                else
                {
                    ReturnResult = FileHelper.ReadFile(fullFile);

                }
            }
            else
            {

                string requestXmlString = string.Format(xmlString, setID, EbaySettings.userToken);

                ReturnResult = GetEbayXml(method_name
                   , requestXmlString
                   , fullFile);
                SaveAttributesCSEbayTag(ReturnResult, fullFileEbayTag, setID);
            }
        }
        return setID;
    }

    public void SaveAttributeSetVersion(int setID, string VID)
    {
        FileHelper.GenerateFile(Server.MapPath(AttributeSetVersionPath + setID.ToString() + ".txt"), VID);
    }

    public int GetAttributeSetVersion(int setID)
    {
        if (setID > 0)
        {
            string s = FileHelper.ReadFile(Server.MapPath(AttributeSetVersionPath + setID.ToString() + ".txt"));
            int id;
            int.TryParse(s, out id);
            return id;
        }
        return setID;
    }

    public void SaveAttributesCSEbayTag(string attributesCSString, string fullFileEbayTag, int setID)
    {
        if (attributesCSString.Length > 100)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(attributesCSString);
            string ebayTabData = doc["GetAttributesCSResponse"]["AttributeData"].InnerText;

            FileHelper.GenerateFile(fullFileEbayTag, ebayTabData);

            string xmlString = FileHelper.ReadFile(fullFileEbayTag);
            string result = string.Format(@"<?xml version=""1.0"" encoding=""utf-8"" ?><eBay><SelectedAttributes><AttributeSet id='{0}'/></SelectedAttributes>{1}"
                , setID, xmlString.Substring(6));
            FileHelper.GenerateFile(fullFileEbayTag, result);
        }
    }

    public string GetCategory2CS(int categoryID, ref bool IsExcludeCategoryID)
    {
        string method_name = "GetCategory2CS";
        string rootNodeString = "GetCategory2CSResponse";
        string result = "";

        string fullFile = Server.MapPath(Category2CSXmlPath + categoryID.ToString() + ".xml");
        string apiXMLFullFile = Server.MapPath(this.XmlPath + method_name + ".xml");


        if (File.Exists(fullFile))
        {
            FileInfo fi = new FileInfo(fullFile);
            DateTime lastWriteTime = fi.LastWriteTime;
            if (DateTime.Now.DayOfYear - lastWriteTime.DayOfYear > EbaySettings.ebayAPIUpdateDateDiff ||
                DateTime.Now.DayOfYear - lastWriteTime.DayOfYear < int.Parse("-" + EbaySettings.ebayAPIUpdateDateDiff.ToString()))
            {
                string xmlString = FileHelper.ReadFile(apiXMLFullFile);

                if (xmlString.Trim().Length < 30)
                    throw new Exception("File is null.");

                //Get XML Document from Embedded  Resources
                string requestXmlString = string.Format(xmlString, categoryID, EbaySettings.userToken);

                string oldXmlString = FileHelper.ReadFile(fullFile);

                result = GetEbayXml(method_name
                    , requestXmlString
                    , null
                    );

                //
                // Compare Vision
                //
                if (GetAPIVision(result, rootNodeString) != GetAPIVision(oldXmlString, rootNodeString))
                {
                    FileHelper.GenerateFile(fullFile, result);
                }
                else
                    FileHelper.GenerateFile(fullFile, oldXmlString);
            }
            else
            {

                result = FileHelper.ReadFile(fullFile);

            }
        }
        else
        {
            string xmlString = FileHelper.ReadFile(apiXMLFullFile);
            string requestXmlString = string.Format(xmlString, categoryID, EbaySettings.userToken);

            result = GetEbayXml(method_name
               , requestXmlString
               , fullFile);
        }
        if (result != "")
            IsExcludeCategoryID = GetCategory2CSIsExcludeCategoryID(result, categoryID);

        return result;
    }

    public bool GetCategory2CSIsExcludeCategoryID(string cat2CSString, int categoryID)
    {
        if (cat2CSString.Length > 100)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(cat2CSString);

            foreach (XmlNode n in doc["GetCategory2CSResponse"]["SiteWideCharacteristicSets"].ChildNodes)
            {
                if (n.Name == "ExcludeCategoryID")
                {
                    if (n.InnerText == categoryID.ToString())
                    {
                        return true;
                    }
                }
            }
        }
        else
            throw new Exception("GetCategory2CSIsExcludeCategoryID is error");
        return false;
    }

    public string GetAttributesXSL()
    {
        string method_name = "GetAttributesXSL";
        string xmlString = FileHelper.ReadFile(Server.MapPath(this.XmlPath + method_name + ".xml"));

        if (xmlString.Trim().Length < 30)
            throw new Exception("File is null.");

        //Get XML Document from Embedded  Resources
        string requestXmlString = string.Format(xmlString, EbaySettings.userToken);

       string returnXmlString = GetEbayXml(method_name
            , requestXmlString
            , Server.MapPath(XmlPath + method_name + "Response.xml")
            );
       XmlDocument doc = new XmlDocument();
       doc.LoadXml(returnXmlString);

       if (doc["GetAttributesXSLResponse"]["Ack"].InnerText.ToLower() == "success")
       {
           string filename = doc["GetAttributesXSLResponse"]["XSLFile"]["FileName"].InnerText;
           string fileContent = doc["GetAttributesXSLResponse"]["XSLFile"]["FileContent"].InnerText;
           fileContent = Encoding.ASCII.GetString(Convert.FromBase64String(fileContent));
           FileHelper.GenerateFile(Server.MapPath(CategoryAttributeXmlPath + filename), fileContent);

       }
       return returnXmlString;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public XmlNode GetShippingServiceXML()
    {
        XmlDocument localCatTree = new XmlDocument();
        bool isGetNew = false; // 
        string filename = Server.MapPath(XmlPath + "ShippingServiceDetails.xml");


        if (!File.Exists(filename))
        {
            isGetNew = true;
        }
        else
        {
            FileInfo fi = new FileInfo(filename);
            if ((DateTime.Now - fi.LastWriteTime).TotalDays > 1)
            {
                isGetNew = true;
            }
        }

        if (isGetNew)
        {
            string method_name = "GeteBayDetails";
            string xmlString = @"<?xml version=""1.0"" encoding=""utf-8""?>
<GeteBayDetailsRequest xmlns=""urn:ebay:apis:eBLBaseComponents"">
  <RequesterCredentials>
    <eBayAuthToken>{0}</eBayAuthToken>
  </RequesterCredentials>
  <WarningLevel>High</WarningLevel>
  <DetailName>ShippingServiceDetails</DetailName>
</GeteBayDetailsRequest>
";

            if (xmlString.Trim().Length < 30)
                throw new Exception("File is null.");

            //Get XML Document from Embedded  Resources
            string requestXmlString = string.Format(xmlString, EbaySettings.userToken);

            GetEbayXml(method_name
                , requestXmlString
                , filename
                );
        }

        StreamReader xmlSr = File.OpenText(filename);
        localCatTree = new XmlDocument();
        localCatTree.LoadXml(xmlSr.ReadToEnd());
        xmlSr.Close();
        XmlNode categoriesNode = localCatTree["GeteBayDetailsResponse"];
        return categoriesNode;
    }

    /// <summary>
    /// 取得自定义类型
    /// </summary>
    /// <returns></returns>
    public string GetStoreRequest()
    {
        string method_name = "GetStore";
        string xmlString = FileHelper.ReadFile(Server.MapPath(this.XmlPath + method_name + ".xml"));

        if (xmlString.Trim().Length < 30)
            throw new Exception("File is null.");

        //Get XML Document from Embedded  Resources
        string requestXmlString = string.Format(xmlString, EbaySettings.userToken);

        return GetEbayXml(method_name
            , requestXmlString
            , Server.MapPath(XmlPath + method_name + "Response.xml")
            );
    } 
}
