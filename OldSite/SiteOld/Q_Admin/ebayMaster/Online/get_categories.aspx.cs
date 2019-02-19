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

public partial class Q_Admin_ebayMaster_Online_get_categories : PageBase
{
    string XmlPath = EbaySettings.ebayMasterXmlPath;
    //XmlDocument localCatTree = null;

    //string devID = ConfigurationManager.AppSettings["EbayDevID"];
    //string appID = ConfigurationManager.AppSettings["EbayAppID"];
    //string certID = ConfigurationManager.AppSettings["EbayCertID"];

    ////Get the Server to use (Sandbox or Production)
    //string serverUrl = ConfigurationManager.AppSettings["EbayServerUrl"];

    ////Get the User Token to Use
    //string userToken = ConfigurationManager.AppSettings["EbayUserToken"];

    ////SiteID = 0  (US) - UK = 3, Canada = 2, Australia = 15, ....
    ////SiteID Indicates the eBay site to associate the call with
    //int siteID = 2;


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitialDatabase();
        }
    }


    #region eBay Category Method
    private void LoadCategory(XmlNode categoriesNode)
    {

        //get the "Categories" node    
        foreach (XmlNode cat in categoriesNode.ChildNodes)
        {
            //if this category has a categoryID equal to its parent category id
            //then it is a main category
            //if (cat.Name == "Category" && cat["CategoryParentID"].InnerText == cat["CategoryID"].InnerText)
            //{
            //    Response.Write(string.Format("{0} ({1})", cat["CategoryName"].InnerText, cat["CategoryID"].InnerText));
            //}
            if (cat["CategoryLevel"].InnerText == "1")
                this.lb_category1.Items.Add(new ListItem(cat["CategoryName"].InnerText, cat["CategoryID"].InnerText));
        }
    }
    private void LoadCategory2(int categoryID)
    {

        this.lb_category2.Items.Clear();

        XmlNode categoriesNode = GetCategoryXML();
        //get the "Categories" node    
        foreach (XmlNode cat in categoriesNode.ChildNodes)
        {
            //if this category has a categoryID equal to its parent category id
            //then it is a main category
            //if (cat.Name == "Category" && cat["CategoryParentID"].InnerText == cat["CategoryID"].InnerText)
            //{
            //    Response.Write(string.Format("{0} ({1})", cat["CategoryName"].InnerText, cat["CategoryID"].InnerText));
            //}
            if (cat.Name == "Category" && cat["CategoryParentID"].InnerText == categoryID.ToString())
                this.lb_category2.Items.Add(new ListItem(cat["CategoryName"].InnerText, cat["CategoryID"].InnerText));
        }
    }
    private void LoadCategory3(int categoryID)
    {
        this.lb_category3.Items.Clear();

        XmlNode categoriesNode = GetCategoryXML();
        //get the "Categories" node    
        foreach (XmlNode cat in categoriesNode.ChildNodes)
        {
            if (cat.Name == "Category" && cat["CategoryParentID"].InnerText == categoryID.ToString())
                this.lb_category3.Items.Add(new ListItem(cat["CategoryName"].InnerText, cat["CategoryID"].InnerText));
        }
    }
    private void LoadCategory4(int categoryID)
    {
        this.lb_category4.Items.Clear();

        XmlNode categoriesNode = GetCategoryXML();
        //get the "Categories" node    
        foreach (XmlNode cat in categoriesNode.ChildNodes)
        {
            if (cat.Name == "Category" && cat["CategoryParentID"].InnerText == categoryID.ToString())
                this.lb_category4.Items.Add(new ListItem(cat["CategoryName"].InnerText, cat["CategoryID"].InnerText));
            //else
            //    Response.Write(cat["CategoryParentID"].InnerText);
        }
    }
    private void LoadCategory5(int categoryID)
    {
        this.lb_category5.Items.Clear();

        XmlNode categoriesNode = GetCategoryXML();
        //get the "Categories" node    
        foreach (XmlNode cat in categoriesNode.ChildNodes)
        {
            if (cat.Name == "Category" && cat["CategoryParentID"].InnerText == categoryID.ToString())
                this.lb_category5.Items.Add(new ListItem(cat["CategoryName"].InnerText, cat["CategoryID"].InnerText));
            //else
            //    Response.Write(cat["CategoryParentID"].InnerText);
        }
    }
    private void LoadCategory6(int categoryID)
    {
        this.lb_category6.Items.Clear();

        XmlNode categoriesNode = GetCategoryXML();
        //get the "Categories" node    
        foreach (XmlNode cat in categoriesNode.ChildNodes)
        {
            if (cat.Name == "Category" && cat["CategoryParentID"].InnerText == categoryID.ToString())
                this.lb_category6.Items.Add(new ListItem(cat["CategoryName"].InnerText, cat["CategoryID"].InnerText));
            //else
            //    Response.Write(cat["CategoryParentID"].InnerText);
        }
    }
    #endregion


    protected void lb_category1_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.lb_category3.Items.Clear();
        this.lb_category4.Items.Clear();
        this.lb_category5.Items.Clear();
        this.lb_category6.Items.Clear();

        int categoryID = int.Parse(lb_category1.SelectedValue.ToString());
        LoadCategory2(categoryID);

        ChangeNextButton(!IsExistSubCategory(this.lb_category2));
    }

    protected void lb_category2_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.lb_category4.Items.Clear();
        this.lb_category5.Items.Clear();
        this.lb_category6.Items.Clear();

        int categoryID = int.Parse(lb_category2.SelectedValue.ToString());
        LoadCategory3(categoryID);

        ChangeNextButton(!IsExistSubCategory(this.lb_category3));
    }

    protected void lb_category3_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.lb_category5.Items.Clear();
        this.lb_category6.Items.Clear();

        int categoryID = int.Parse(lb_category3.SelectedValue.ToString());
        LoadCategory4(categoryID);

        ChangeNextButton(!IsExistSubCategory(this.lb_category4));
    }
    protected void lb_category4_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.lb_category6.Items.Clear();

        int categoryID = int.Parse(lb_category4.SelectedValue.ToString());
        LoadCategory5(categoryID);
        
        ChangeNextButton(!IsExistSubCategory(this.lb_category5));
    }
    protected void lb_category5_SelectedIndexChanged(object sender, EventArgs e)
    {
        int categoryID = int.Parse(lb_category5.SelectedValue.ToString());
        LoadCategory6(categoryID);
        ChangeNextButton(!IsExistSubCategory(this.lb_category6));
    }

    public int GetEbayCategoryID
    {
        get
        {
            int id = -1;

            if (this.lb_category1.Items.Count > 0)
            {
                int.TryParse(this.lb_category1.SelectedValue.ToString(), out id);
                CurrCateName = this.lb_category1.SelectedItem.Text;
            }
            if (this.lb_category2.Items.Count > 0)
            {
                int.TryParse(this.lb_category2.SelectedValue.ToString(), out id);
                CurrCateName = this.lb_category2.SelectedItem.Text;
            }
            if (this.lb_category3.Items.Count > 0)
            {
                int.TryParse(this.lb_category3.SelectedValue.ToString(), out id);
                CurrCateName = this.lb_category3.SelectedItem.Text;
            }
            if (this.lb_category4.Items.Count > 0)
            {
                int.TryParse(this.lb_category4.SelectedValue.ToString(), out id);
                CurrCateName = this.lb_category4.SelectedItem.Text;

            }
            if (this.lb_category5.Items.Count > 0)
            {
                int.TryParse(this.lb_category5.SelectedValue.ToString(), out id);
                CurrCateName = this.lb_category5.SelectedItem.Text;
            }
            if (this.lb_category6.Items.Count > 0)
            {
                int.TryParse(this.lb_category6.SelectedValue.ToString(), out id);
                CurrCateName = this.lb_category6.SelectedItem.Text;
            }

            return id;
        }
    }

     

    private bool IsExistSubCategory(ListBox lb)
    {
        return lb.Items.Count > 0;
    }

    private void ChangeNextButton(bool isEnabled)
    {
        this.btn_Next.Enabled = isEnabled;
    }

    #region Get XML

    public XmlNode GetCategoryXML()
    {

        EbayGetXmlHelper egxh = new EbayGetXmlHelper();

        XmlDocument localCatTree = new XmlDocument();
        //string fullFile = Server.MapPath(XmlPath + "CatTree.xml");

        //StreamReader xmlSr = File.OpenText(fullFile);
        localCatTree = new XmlDocument();
        localCatTree.LoadXml(egxh.GetCategoryTree());
        //xmlSr.Close();
        XmlNode categoriesNode = localCatTree["GetCategoriesResponse"]["CategoryArray"];
        return categoriesNode;
    }
    #endregion

    public override void InitialDatabase()
    {

        if (PartSku == -1 && SystemSKU == string.Empty && ReqIsChild==false)
        {
            Response.Write("<h1>Params is errir.</h1>");
            Response.End();
        }
        
        base.InitialDatabase();


        LoadCategory(GetCategoryXML());
        //#region settings


        //#endregion


        //#region Check for newer category tree version
        ////If local version cannot be found: get and save entire category tree
        //if (File.Exists(Server.MapPath(XmlPath + "CatTree.xml")) == false)
        //{
        //    Response.Write("Application Run for first time, downloading entire category tree...");
        //    if (GetEntireCategoryTree())
        //    {
        //        //Successfully downloaded and saved category tree
        //        Response.Write("Category tree retrieved and saved locally.");
        //    }
        //    else
        //    {
        //        //Could not obtain category tree
        //        Response.Write("Press Enter to Continue...");
        //        return;
        //    }
        //}
        //else //Xml file already exists locally
        //{
        //    //Open Xml File
        //    StreamReader sr = File.OpenText(Server.MapPath(XmlPath + "CatTree.xml"));
        //    localCatTree = new XmlDocument();
        //    localCatTree.LoadXml(sr.ReadToEnd());
        //    sr.Close();

        //    //Get the latest version number
        //    string onlineVersion = GetCurrentAPICategoryVersion();

        //    if (onlineVersion == null)
        //    {
        //        Response.Write("Could not establish latest version number");
        //        // Response.Write("Press Enter to Continue...");

        //        return;
        //    }


        //    if (localCatTree["GetCategoriesResponse"]["CategoryVersion"].InnerText != onlineVersion)
        //    {
        //        //Version numbers are different, so update
        //        Response.Write("Updating Local Category Tree...");
        //        if (GetEntireCategoryTree())
        //        {
        //            //Successfully downloaded and saved category tree
        //            Response.Write("Category tree retrieved and saved locally.");
        //        }
        //        else
        //        {
        //            Response.Write("Could not obtain category tree");
        //            //Response.Write("Press Enter to Continue...");
                
        //            return;
        //        }
        //    }
        //    else
        //    {
        //        Response.Write("Category Tree is latest version");
        //    }

        //}// if(local XML file does not exist)
        //#endregion


        //#region Category Selection
        ////Load the local XML document if it is not already loaded
        //if (localCatTree == null)
        //{
        //    StreamReader xmlSr = File.OpenText(Server.MapPath(XmlPath + "CatTree.xml"));
        //    localCatTree = new XmlDocument();
        //    localCatTree.LoadXml(xmlSr.ReadToEnd());
        //    xmlSr.Close();
        //}

        ////get the "Categories" node
        //XmlNode categoriesNode = localCatTree["GetCategoriesResponse"]["CategoryArray"];

        //Response.Write("\nMAIN CATEGORIES");
        //Response.Write("***************");

        ////go through each category
        //foreach (XmlNode cat in categoriesNode.ChildNodes)
        //{
        //    //if this category has a categoryID equal to its parent category id
        //    //then it is a main category
        //    if (cat.Name == "Category" && cat["CategoryParentID"].InnerText == cat["CategoryID"].InnerText)
        //    {
        //        Response.Write(string.Format("{0} ({1})", cat["CategoryName"].InnerText, cat["CategoryID"].InnerText));
        //    }
        //}

        //int selectedCategoryID;
        ////user must enter a category id to submit to
        //Response.Write("\nEnter Category ID number: ");
        //selectedCategoryID = 37908; // Int32.Parse(Response.ReadLine());


        ////keep asking for a sub category until they select a category they can submit to (a leaf)
        //while (!IsCategoryLeaf(selectedCategoryID))
        //{
        //    //Diaply the sub categories of the selected category
        //    Response.Write("\nSUBCATEGORIES");
        //    Response.Write("***************");
        //    foreach (XmlNode cat in categoriesNode.ChildNodes)
        //    {
        //        if (cat.Name == "Category" && cat["CategoryParentID"].InnerText == selectedCategoryID.ToString())
        //        {
        //            Response.Write(string.Format("{0} ({1})", cat["CategoryName"].InnerText, cat["CategoryID"].InnerText));
        //        }
        //    }

        //    //ask to select a subcategory
        //    Response.Write("\nEnter SubCategory ID number: ");
        //    selectedCategoryID = 37908;
        //}

        ////Output the name and ID of the category they can submit to
        //Response.Write(string.Format("YOU MAY SUBMIT TO: {0} ({1})\n", GetCategoryName(selectedCategoryID), selectedCategoryID.ToString()));

        //#endregion

        //Response.Write("Press Enter to Close.");

    }

    ///// <summary>
    ///// Get the Name of a category from the XMLDocument
    ///// </summary>
    ///// <param name="CategoryID">The ID of the category to get the name for</param>
    ///// <returns>Name of the Category</returns>
    //private string GetCategoryName(int CategoryID)
    //{
    //    //go through each category
    //    foreach (XmlNode cat in localCatTree["GetCategoriesResponse"]["CategoryArray"].ChildNodes)
    //    {
    //        //if this cat is the one, then return its CategoryName value
    //        if (cat.Name == "Category" && cat["CategoryID"].InnerText == CategoryID.ToString())
    //        {
    //            return cat["CategoryName"].InnerText;
    //        }
    //    }

    //    return null; //not found
    //}


    ///// <summary>
    ///// Requests the entire category tree from the eBay API and saves it locally.
    ///// </summary>
    ///// <returns>True if successful, false otherwise.</returns>
    //private bool GetEntireCategoryTree()
    //{

    //    #region Load The XML Document Template and Set the Neccessary Values
    //    //Load the XML Document to Use for this Request
    //    XmlDocument xmlDoc = new XmlDocument();

    //    //Get XML Document from Embedded  Resources
    //    xmlDoc.Load(Server.MapPath(this.XmlPath + "GetCategoriesRequest.xml"));

    //    //Set the various node values
    //    xmlDoc["GetCategoriesRequest"]["RequesterCredentials"]["eBayAuthToken"].InnerText = userToken;
    //    //Tell API to return entire tree
    //    xmlDoc["GetCategoriesRequest"]["DetailLevel"].InnerText = "ReturnAll";
    //    xmlDoc["GetCategoriesRequest"]["ViewAllNodes"].InnerText = "1";

    //    //Get XML into a string for use in encoding
    //    string xmlText = xmlDoc.InnerXml;

    //    //Put the data into a UTF8 encoded  byte array
    //    UTF8Encoding encoding = new UTF8Encoding();
    //    int dataLen = encoding.GetByteCount(xmlText);
    //    byte[] utf8Bytes = new byte[dataLen];
    //    Encoding.UTF8.GetBytes(xmlText, 0, xmlText.Length, utf8Bytes, 0);
    //    #endregion

    //    #region Setup and Send The Request
    //    HttpWebRequest request = GetHttpRequestWithHeaders(utf8Bytes.Length);


    //    Stream str = null;
    //    try
    //    {
    //        //Set the request Stream
    //        str = request.GetRequestStream();
    //        //Write the equest to the Request Steam
    //        str.Write(utf8Bytes, 0, utf8Bytes.Length);
    //        str.Close();

    //        //Get response into stream
    //        WebResponse resp = request.GetResponse();
    //        str = resp.GetResponseStream();
    //    }
    //    catch (WebException wEx)
    //    {
    //        //Error has occured whilst requesting
    //        //Display error message and exit.
    //        if (wEx.Status == WebExceptionStatus.Timeout)
    //            Response.Write("Request Timed-Out.");
    //        else
    //            Response.Write(wEx.Message);
    //        return false;
    //    }
    //    #endregion

    //    #region Process Response
    //    // Get Response into String
    //    StreamReader sr = new StreamReader(str);
    //    xmlDoc.LoadXml(sr.ReadToEnd());

    //    sr.Close();
    //    str.Close();

    //    //get the root node, for ease of use
    //    XmlNode root = xmlDoc["GetCategoriesResponse"];

    //    //There have been Errors
    //    if (root["Errors"] != null)
    //    {
    //        string errorCode = root["Errors"]["ErrorCode"].InnerText;
    //        string errorShort = root["Errors"]["ShortMessage"].InnerText;
    //        string errorLong = root["Errors"]["LongMessage"].InnerText;

    //        //Output the error message
    //        Response.Write(errorCode + " ERROR: " + errorShort);
    //        Response.Write(errorLong + "\n");

    //        return false;
    //    }
    //    else
    //    {
    //        //Save the file to disk
    //        StreamWriter sw = new StreamWriter(Server.MapPath(this.XmlPath + "CatTree.xml"), false, Encoding.UTF8);
    //        xmlDoc.Save(sw);
    //        sw.Close();
    //    }

    //    return true;
    //    #endregion
    //}


    ///// <summary>
    ///// Requests the current version number of the Category Tree that
    ///// is available online.
    ///// </summary>
    ///// <returns>Version number as string, null if there is an error</returns>
    //private string GetCurrentAPICategoryVersion()
    //{

    //    #region Load The XML Document Template and Set the Neccessary Values
    //    //Load the XML Document to Use for this Request
    //    XmlDocument xmlDoc = new XmlDocument();

    //    //Get XML Document from Embedded Resources
    //    xmlDoc.Load(Server.MapPath(this.XmlPath + "GetCategoriesRequest.xml"));

    //    //Set the various node values
    //    xmlDoc["GetCategoriesRequest"]["RequesterCredentials"]["eBayAuthToken"].InnerText = userToken;
    //    //Tell API to return just the category version number (remove DetailLevel node)
    //    xmlDoc["GetCategoriesRequest"].RemoveChild(xmlDoc["GetCategoriesRequest"]["DetailLevel"]);
    //    xmlDoc["GetCategoriesRequest"]["ViewAllNodes"].InnerText = "0";

    //    //Get XML into a string for use in encoding
    //    string xmlText = xmlDoc.InnerXml;

    //    //Put the data into a UTF8 encoded  byte array
    //    UTF8Encoding encoding = new UTF8Encoding();
    //    int dataLen = encoding.GetByteCount(xmlText);
    //    byte[] utf8Bytes = new byte[dataLen];
    //    Encoding.UTF8.GetBytes(xmlText, 0, xmlText.Length, utf8Bytes, 0);
    //    #endregion

    //    #region Setup and Send The Request
    //    HttpWebRequest request = GetHttpRequestWithHeaders(utf8Bytes.Length);


    //    Stream str = null;
    //    try
    //    {
    //        //Set the request Stream
    //        str = request.GetRequestStream();
    //        //Write the request to the Request Steam
    //        str.Write(utf8Bytes, 0, utf8Bytes.Length);
    //        str.Close();
    //        //Get response into stream
    //        WebResponse resp = request.GetResponse();
    //        str = resp.GetResponseStream();
    //    }
    //    catch (WebException wEx)
    //    {
    //        //Error has occured whilst requesting
    //        //Display error message and exit.
    //        if (wEx.Status == WebExceptionStatus.Timeout)
    //            Response.Write("Request Timed-Out.");
    //        else
    //            Response.Write(wEx.Message);
    //        return null;
    //    }
    //    #endregion

    //    #region Process Response
    //    // Get Response into String
    //    StreamReader sr = new StreamReader(str);
    //    xmlDoc.LoadXml(sr.ReadToEnd());

    //    sr.Close();
    //    str.Close();

    //    //get the root node, for ease of use
    //    XmlNode root = xmlDoc["GetCategoriesResponse"];

    //    //There have been Errors
    //    if (root["Errors"] != null)
    //    {
    //        string errorCode = root["Errors"]["ErrorCode"].InnerText;
    //        string errorShort = root["Errors"]["ShortMessage"].InnerText;
    //        string errorLong = root["Errors"]["LongMessage"].InnerText;

    //        //Output the error message
    //        Response.Write(errorCode + " ERROR: " + errorShort);
    //        Response.Write(errorLong + "\n");

    //        return null;
    //    }
    //    else
    //    {
    //        //return the version number
    //        return root["CategoryVersion"].InnerText;
    //    }
    //    #endregion
    //}
    ///// <summary>
    ///// Returns a HTTP Request object with all the neccessary headers set.
    ///// </summary>
    ///// <param name="dataLength">Length of the XML data being sent</param>
    ///// <param name="detailLevel">Detail Level Required</param>
    ///// <returns></returns>
    //private HttpWebRequest GetHttpRequestWithHeaders(long dataLength)
    //{
        

    //    //Create a new HttpWebRequest object for the ServerUrl
    //    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(serverUrl);

    //    //Set Request Method (POST) and Content Type (text/xml)
    //    request.Method = "POST";
    //    request.ContentType = "text/xml";
    //    request.ContentLength = dataLength;

    //    //Add the Keys to the HTTP Headers
    //    request.Headers.Add("X-EBAY-API-DEV-NAME: " + devID);
    //    request.Headers.Add("X-EBAY-API-APP-NAME: " + appID);
    //    request.Headers.Add("X-EBAY-API-CERT-NAME: " + certID);

    //    //Add Compatability Level to HTTP Headers
    //    //Regulates versioning of the XML interface for the API
    //    request.Headers.Add("X-EBAY-API-COMPATIBILITY-LEVEL: 551");

    //    //Add function name, SiteID and Detail Level to HTTP Headers
    //    request.Headers.Add("X-EBAY-API-CALL-NAME: GetCategories");
    //    request.Headers.Add("X-EBAY-API-SITEID: " + siteID.ToString());

    //    //Time out = 15 seconds,  set to -1 for no timeout.
    //    //If times-out - throws a WebException with the
    //    //Status property set to WebExceptionStatus.Timeout.
    //    request.Timeout = 15000;

    //    return request;
    //}

    ///// <summary>
    ///// Whether or not the specified category is a Leaf category
    ///// (so it can be submitted to)
    ///// </summary>
    ///// <param name="CategoryID">The ID of the category to check</param>
    ///// <returns>True if it is a Leaf category, false otherwise</returns>
    //private bool IsCategoryLeaf(int CategoryID)
    //{
    //    // go through each category
    //    foreach (XmlNode cat in localCatTree["GetCategoriesResponse"]["CategoryArray"].ChildNodes)
    //    {
    //        //if this cat is the one, then return its LeafCategory value
    //        if (cat.Name == "Category" && cat["CategoryID"].InnerText == CategoryID.ToString())
    //        {
    //            if (cat["LeafCategory"].InnerText == "1" || cat["LeafCategory"].InnerText == "true")
    //                return true;
    //            else
    //                return false;
    //        }
    //    }

    //    return true; //not found
    //}

    private string SystemSKU
    {
        get { return Util.GetStringSafeFromQueryString(this, "system_sku"); }
    }

    public int PartSku
    {
        get { return Util.GetInt32SafeFromQueryString(this, "part_sku", -1); }
    }

    public bool IsSystem
    {
        get { return Util.GetStringSafeFromQueryString(this, "type") != "part"; }
    }

    bool ReqIsChild
    {
        get { return Util.GetInt32SafeFromQueryString(this, "IsChild", -1) == 1; }
    }

    string ParentElement
    {
        get { return Util.GetStringSafeFromQueryString(this, "ParentElement"); }
    }

    string CurrCateName
    {
        get
        {
            object obj = ViewState["CurrCateName"];
            if (obj != null)
                return obj.ToString();
            return "";
        }
        set { ViewState["CurrCateName"] = value; }

    }
    protected void btn_Next_Click(object sender, EventArgs e)
    {
        int categoryID = GetEbayCategoryID;


        if (ReqIsChild)
        {
            Literal1.Text = "<script>$('#" + ParentElement + "', parent.document).val('" + categoryID.ToString() + "');$('#" + ParentElement + "Name', parent.document).val('" + CurrCateName + "');parent.window.ClosePop();</script>";
            return;
        }

        bool isExcludeCategoryID = false;
        int attributeSetId = -1;
        EbayGetXmlHelper egxh = new EbayGetXmlHelper();
        string category2CSString = egxh.GetCategory2CS(categoryID, ref isExcludeCategoryID);
        if (!isExcludeCategoryID)   //  Exclude Category ID
           attributeSetId = egxh.GetAttributesCS(category2CSString);

        string str = egxh.GetCategoryFeatures(categoryID);

        string paramsString = "xmlStoreFile=" + attributeSetId.ToString() + "&cid=" + categoryID.ToString() + "&system_sku=" + SystemSKU + "&part_sku=" + PartSku.ToString();
        if (egxh.IsItemSpecificsEnabled(str))
        {
            paramsString += "&IsItemSpecificsEnabled=1";
        }


        

        CH.Redirect("/q_admin/ebayMaster/online/itemSpecial.asp?" + paramsString, this.Literal1);
    }

    


    public void GetAttrXmlByCategory(int category)
    {

    }
}
