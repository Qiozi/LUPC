using System;
using System.Collections.Generic;
using System.Xml;
using System.Web;
using System.Net;
using System.Text;
using System.IO;
using System.Data;


/// <summary>
/// Summary description for eBayGetActiveItems
/// </summary>
public class eBayGetActiveItems
{
    string storePath = "";
    System.Web.UI.Page The;

	public eBayGetActiveItems(System.Web.UI.Page the)
	{
		//
		// TODO: Add constructor logic here
		//
        The = the;
        storePath = the.Server.MapPath("/soft_img/eBayXml/eBayItems/");

	}

    /// <summary>
    /// Get eBay active Items.
    /// </summary>
    /// <param name="page"></param>
    /// <param name="the"></param>
    public void GetMySelling(int page)
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

        ////Get XML Document from Embedded  Resources
        //xmlDoc.Load((Server.MapPath("/q_admin/ebayMaster/Online/Xml/GeteBayOfficialTimeRequest.xml")));

        ////Set the various node values   attr1858_26443
        //xmlDoc["GeteBayOfficialTimeRequest"]["RequesterCredentials"]["eBayAuthToken"].InnerText = userToken;

        string sendXml = string.Format(@"<?xml version=""1.0"" encoding=""utf-8""?>
<GetMyeBaySellingRequest xmlns=""urn:ebay:apis:eBLBaseComponents"">
  <RequesterCredentials>
    <eBayAuthToken>{0}</eBayAuthToken>
  </RequesterCredentials>
  <Version>677</Version>
  <ActiveList>
    <Sort>TimeLeft</Sort>
    <Pagination>
      <EntriesPerPage>200</EntriesPerPage>
      <PageNumber>{1}</PageNumber>
    </Pagination>
  </ActiveList>
</GetMyeBaySellingRequest>
", userToken, page);

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
        request.Headers.Add("X-EBAY-API-COMPATIBILITY-LEVEL: 677");

        //Add function name, SiteID and Detail Level to HTTP Headers
        request.Headers.Add("X-EBAY-API-CALL-NAME:GetMyeBaySelling");
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

            //MessageBox.Show("Press Enter to Continue...\r\n" + wEx.Message);

        }
        #endregion

        #region Process Response
        // Get Response into String
        StreamReader sr = new StreamReader(str);
        string sssss = sr.ReadToEnd();
        xmlDoc.LoadXml(sssss);
        sr.Close();
        str.Close();

        if (!Directory.Exists(storePath))
            Directory.CreateDirectory(storePath);
        string filefullname = storePath + "\\" + page.ToString() + ".xml";
        if (File.Exists(filefullname))
            File.Delete(filefullname);
        System.IO.StreamWriter sw = new System.IO.StreamWriter(filefullname);
        sw.Write(sssss);
        sw.Close();
        sw.Dispose();

        #endregion

        #region 取得页数，判断如果还有，将再抓取

        int newPage;
        int.TryParse(xmlDoc["GetMyeBaySellingResponse"]["ActiveList"]["PaginationResult"]["TotalNumberOfPages"].InnerText, out newPage);

        if (newPage > page)
            GetMySelling(page + 1);

        #endregion
    }

    /// <summary>
    /// Read the XML retrieving data endures database.
    /// </summary>
    public void ReadPage()
    {
        string path = storePath;
        if (Directory.Exists(path))
        {
            DirectoryInfo dir = new DirectoryInfo(path);
            FileInfo[] fis = dir.GetFiles();
            Config.ExecuteNonQuery("delete from tb_ebay_selling_tmp_db");
            System.Text.StringBuilder sb = new StringBuilder(); ;
            for (int i = 0; i < fis.Length; i++)
            {
                sb = new StringBuilder();

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(fis[i].FullName);
                XmlNodeList list = xmlDoc["GetMyeBaySellingResponse"]["ActiveList"]["ItemArray"].ChildNodes;
                for (int j = 0; j < list.Count; j++)
                {
                    string BuyItNowPrice = "-1";
                    string BuyItNowPrice_currencyID = "";
                    string ItemID = "";
                    string ListingDetails_StartTime = "";
                    string ListingDetails_ViewItemURL = "";
                    string ListingDetails_ViewItemURLForNaturalSearch = "";
                    string ListingType = "";
                    string Quantity = "-1";
                    string SellingStatus_CurrentPrice = "-1";
                    string SellingStatus_CurrentPrice_currencyID = "";
                    string ShippingDetails_ShippingServiceCost = "-1";
                    string TimeLeft = "";
                    string Title = "";
                    string WatchCount = "-1";
                    string QuantityAvailable = "-1";
                    string SKU = "";
                    string PictureDetails_GalleryURL = "";
                    string ClassifiedAdPayPerLeadFee = "-1";
                    string partSKu = "0";
                    string sysSKu = "0";

                    try
                    {
                        BuyItNowPrice = list[j]["BuyItNowPrice"].InnerText ?? "0";
                    }
                    catch { }
                    try
                    {
                        BuyItNowPrice_currencyID = list[j]["BuyItNowPrice"].Attributes["currencyID"].Value ?? "";
                    }
                    catch { }
                    try
                    {
                        ItemID = list[j]["ItemID"].InnerText ?? "";
                    }
                    catch { }
                    try
                    {
                        ListingDetails_StartTime = list[j]["ListingDetails"]["StartTime"].InnerText ?? "";
                    }
                    catch { }
                    try
                    {
                        ListingDetails_ViewItemURL = "";// list[j]["ListingDetails"]["ViewItemURL"].InnerText ?? "";
                    }
                    catch { }
                    try
                    {
                        ListingDetails_ViewItemURLForNaturalSearch = "";// list[j]["ListingDetails"]["ViewItemURLForNaturalSearch"].InnerText ?? "";
                    }
                    catch { }
                    try
                    {
                        ListingType = list[j]["ListingType"].InnerText ?? "";
                    }
                    catch { }
                    try
                    {
                        Quantity = list[j]["Quantity"].InnerText ?? "0";
                    }
                    catch { }
                    try
                    {
                        SellingStatus_CurrentPrice = list[j]["SellingStatus"]["CurrentPrice"].InnerText ?? "0";
                    }
                    catch { }
                    try
                    {
                        SellingStatus_CurrentPrice_currencyID = list[j]["SellingStatus"]["CurrentPrice"].Attributes["currencyID"].Value ?? "";
                    }
                    catch { }
                    try
                    {
                        ShippingDetails_ShippingServiceCost = list[j]["ShippingDetails"]["ShippingServiceCost"].InnerText ?? "0";
                    }
                    catch { }
                    try
                    {
                        TimeLeft = list[j]["TimeLeft"].InnerText ?? "";
                    }
                    catch { }
                    try
                    {
                        Title = list[j]["Title"].InnerText ?? "";
                    }
                    catch { }
                    try
                    {
                        WatchCount = list[j]["WatchCount"].InnerText ?? "";
                    }
                    catch { }

                    try
                    {
                        QuantityAvailable = list[j]["QuantityAvailable"].InnerText ?? "0";
                    }
                    catch { }
                    try
                    {
                        SKU = list[j]["SKU"].InnerText ?? "";
                        partSKu = analyseSKU(SKU);
                        if (partSKu.Length == 6)
                        {
                            sysSKu = partSKu;
                            partSKu = "0";
                        }
                        

                    }
                    catch { }
                    try
                    {
                        PictureDetails_GalleryURL = "";// list[j]["PictureDetails"]["GalleryURL"].InnerText ?? "";
                    }
                    catch { }
                    try
                    {
                        ClassifiedAdPayPerLeadFee = list[j]["ClassifiedAdPayPerLeadFee"].InnerText ?? "";
                    }
                    catch { }

                    sb.Append(string.Format(@",('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}',now())"
                         , BuyItNowPrice
                         , BuyItNowPrice_currencyID
                         , ItemID
                         , ListingDetails_StartTime
                         , ListingDetails_ViewItemURL
                         , ListingDetails_ViewItemURLForNaturalSearch
                         , ListingType
                         , Quantity
                         , SellingStatus_CurrentPrice
                         , SellingStatus_CurrentPrice_currencyID
                         , ShippingDetails_ShippingServiceCost
                         , TimeLeft
                         , Title
                         , WatchCount
                         , QuantityAvailable
                         , SKU
                         , PictureDetails_GalleryURL
                         , ClassifiedAdPayPerLeadFee
                         , partSKu
                         , sysSKu
                                    ));
                    

                }
                if (sb.Length > 3)
                    Config.ExecuteNonQuery(string.Format(@"
insert into tb_ebay_selling_tmp_db 
	(BuyItNowPrice, BuyItNowPrice_currencyID, ItemID, ListingDetails_StartTime, 
	ListingDetails_ViewItemURL, 
	ListingDetails_ViewItemURLForNaturalSearch, 
	ListingType, 
	Quantity, 
	SellingStatus_CurrentPrice, 
	SellingStatus_CurrentPrice_currencyID, 
	ShippingDetails_ShippingServiceCost, 
	TimeLeft, 
	Title, 
	WatchCount, 
	QuantityAvailable, 
	SKU, 
	PictureDetails_GalleryURL, 
	ClassifiedAdPayPerLeadFee,
    luc_sku,
    sys_sku,
    regdate
	) values {0}", sb.ToString().Substring(1) + ";"));

                File.Delete(fis[i].FullName);
            }
            
        }

    }
    /// <summary>
    /// Analyse database connection.
    /// </summary>
    public void analyse()
    {
        DataTable dt = Config.ExecuteDataTable("select id, sku from tb_ebay_selling_tmp_db where id>0 and length(sku)>3");
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataRow dr = dt.Rows[i];
            int id;
            int.TryParse(dr["id"].ToString(), out id);

            string sku = dr["sku"].ToString().Trim();

            if (sku.Length > 0)
            {
                if (sku.IndexOf('-') > -1)
                {
                    string[] skus = sku.Split(new char[] { '-' });
                    string newSku = skus[skus.Length - 1];
                    int nSKU;
                    int.TryParse(newSku, out nSKU);

                    if (nSKU.ToString().Length == 5 || nSKU.ToString().Length == 4)
                    {
                        Config.ExecuteNonQuery("Update tb_ebay_selling_tmp_db set luc_sku='" + nSKU.ToString() + "' where id='" + id.ToString() + "'");
                    }
                    if (nSKU.ToString().Length == 6)
                    {
                        Config.ExecuteNonQuery("Update tb_ebay_selling_tmp_db set sys_sku='" + nSKU.ToString() + "' where id='" + id.ToString() + "'");
                    }
                }

                if (sku.IndexOf(' ') > -1)
                {
                    string[] skus = sku.Split(new char[] { ' ' });
                    string newSku = skus[skus.Length - 1];
                    int nSKU;
                    int.TryParse(newSku, out nSKU);

                    if (nSKU.ToString().Length == 5 || nSKU.ToString().Length == 4)
                    {
                        Config.ExecuteNonQuery("Update tb_ebay_selling_tmp_db set luc_sku='" + nSKU.ToString() + "' where id='" + id.ToString() + "'");
                    }
                    if (nSKU.ToString().Length == 6)
                    {
                        Config.ExecuteNonQuery("Update tb_ebay_selling_tmp_db set sys_sku='" + nSKU.ToString() + "' where id='" + id.ToString() + "'");
                    }
                }
                if (sku.Length > 10)
                {
                    if (sku.Substring(0, 4).ToLower() == "new:")
                    {

                        string newSku = sku.Substring(4, 6);
                        if (newSku.Length == 6)
                        {
                            int ns = 0;
                            int.TryParse(newSku, out ns);
                            if (ns == 0
                                || ns.ToString().Length != 6)
                                continue;
                            Config.ExecuteNonQuery("Update tb_ebay_selling_tmp_db set sys_sku='" + newSku + "' where id='" + id.ToString() + "'");
                        }
                    }
                }
            }
        }

    }

    string analyseSKU(string custom_label)
    {        
        string sku = custom_label;

        if (sku.Length > 0)
        {
           

            if (sku.IndexOf(' ') > -1)
            {
                string[] skus = sku.Split(new char[] { ' ' });
                string newSku = skus[skus.Length - 1];
                int nSKU;
                int.TryParse(newSku, out nSKU);

                return nSKU.ToString();
            }

            if (sku.IndexOf('-') > -1)
            {
                string[] skus = sku.Split(new char[] { '-' });
                string newSku = skus[skus.Length - 1];
                int nSKU;
                int.TryParse(newSku, out nSKU);

                return nSKU.ToString();
            }

            if (sku.Length > 10)
            {
                if (sku.Substring(0, 4).ToLower() == "new:")
                {

                    string newSku = sku.Substring(4, 6);
                    if (newSku.Length == 6)
                    {
                        int ns = 0;
                        int.TryParse(newSku, out ns);
                        if (ns == 0
                            || ns.ToString().Length != 6)
                            return "";
                        return ns.ToString();
                    }
                }
            }
        }
        return "";
    }

    /// <summary>
    /// 
    /// </summary>
    public void UpdateDB()
    {
        Config.ExecuteNonQuery(@"delete from tb_ebay_selling where itemid not in (select ItemID from tb_ebay_selling_tmp_db);

update tb_ebay_selling o , tb_ebay_selling_tmp_db n set
	o.BuyItNowPrice = n.BuyItNowPrice
	, o.BuyItNowPrice_currencyID = n.BuyItNowPrice_currencyID,  
	o.ListingDetails_StartTime = n.ListingDetails_StartTime, 
	o.ListingDetails_ViewItemURL = n.ListingDetails_ViewItemURL, 
	o.ListingDetails_ViewItemURLForNaturalSearch = n.ListingDetails_ViewItemURLForNaturalSearch, 
	o.ListingType = n.ListingType, 
	o.Quantity = n.Quantity, 
	o.SellingStatus_CurrentPrice = n.SellingStatus_CurrentPrice, 
	o.SellingStatus_CurrentPrice_currencyID = n.SellingStatus_CurrentPrice_currencyID, 
	o.ShippingDetails_ShippingServiceCost = n.ShippingDetails_ShippingServiceCost, 
	o.TimeLeft = n.TimeLeft, 
	o.Title = n.Title, 
	o.WatchCount = n.WatchCount, 
	o.QuantityAvailable = n.QuantityAvailable, 
	o.SKU = n.SKU, 
	o.PictureDetails_GalleryURL = n.PictureDetails_GalleryURL, 
	o.ClassifiedAdPayPerLeadFee = n.ClassifiedAdPayPerLeadFee, 
	o.regdate = n.regdate, 
	o.luc_sku = n.luc_sku, 
	o.sys_sku = n.sys_sku, 
	o.BuyItNowPriceNew = n.BuyItNowPrice
	where o.itemid=n.itemid;

insert into tb_ebay_selling 
	( BuyItNowPrice, BuyItNowPrice_currencyID, 
	ItemID, 
	ListingDetails_StartTime, 
	ListingDetails_ViewItemURL, 
	ListingDetails_ViewItemURLForNaturalSearch, 
	ListingType, 
	Quantity, 
	SellingStatus_CurrentPrice, 
	SellingStatus_CurrentPrice_currencyID, 
	ShippingDetails_ShippingServiceCost, 
	TimeLeft, 
	Title, 
	WatchCount, 
	QuantityAvailable, 
	SKU, 
	PictureDetails_GalleryURL, 
	ClassifiedAdPayPerLeadFee, 
	regdate, 
	luc_sku, 
	sys_sku, 
	BuyItNowPriceNew
	
	)
	select 
	  BuyItNowPrice, BuyItNowPrice_currencyID, 
	ItemID, 
	ListingDetails_StartTime, 
	ListingDetails_ViewItemURL, 
	ListingDetails_ViewItemURLForNaturalSearch, 
	ListingType, 
	Quantity, 
	SellingStatus_CurrentPrice, 
	SellingStatus_CurrentPrice_currencyID, 
	ShippingDetails_ShippingServiceCost, 
	TimeLeft, 
	Title, 
	WatchCount, 
	QuantityAvailable, 
	SKU, 
	PictureDetails_GalleryURL, 
	ClassifiedAdPayPerLeadFee, 
	regdate, 
	luc_sku, 
	sys_sku, 
	BuyItNowPrice
	
	from tb_ebay_selling_tmp_db where Itemid not in (Select Itemid from tb_ebay_selling)");

        //Config.ExecuteNonQuery("update tb_ebay_system e, tb_order_ebay s, tb_ebay_code_and_luc_sku k set e.is_online=1 where e.id = k.SKU and k.ebay_code=s.item_number;");
        //
        // 变化标记
        Config.ExecuteNonQuery(@"
update tb_ebay_system e set is_online=0;
update tb_ebay_system_parts set is_online=0;
update tb_ebay_code_and_luc_sku set is_online=0;
");

        DataTable itemDT = Config.ExecuteDataTable("Select itemid, sys_sku, Title from tb_ebay_selling");

        foreach (DataRow dr in itemDT.Rows)
        {

            if (int.Parse(dr["sys_sku"].ToString()) > 0)
            {
                Config.ExecuteNonQuery(string.Format(@"
update tb_ebay_system set is_online=1 where id = {0};
update tb_ebay_system_parts set is_online=1 where system_sku={0};", dr["sys_sku"].ToString()));
                Config.ExecuteNonQuery(string.Format(@"update tb_ebay_system  set system_title1='{1}' where {0}=id;"
                    , dr["sys_sku"].ToString()
                    , dr["Title"].ToString().Replace("\\","\\\\").Replace("'", "\\'")
                    ));
            }
            Config.ExecuteNonQuery(@"update tb_ebay_code_and_luc_sku set is_online=1 where ebay_code ='" + dr["itemid"].ToString() + "';");
        }

        Config.ExecuteNonQuery(@"
insert into tb_ebay_code_and_luc_sku (sku, is_sys, ebay_code, is_online, regdate)
select luc_sku, 0, itemid, 1, now() from tb_ebay_selling where luc_sku>0 and itemid not in (select ebay_code from tb_ebay_code_and_luc_sku);
");
        Config.ExecuteNonQuery(@"
insert into tb_ebay_code_and_luc_sku (sku, is_sys, ebay_code, is_online, regdate)
select sys_sku, 1, itemid, 1, now() from tb_ebay_selling where sys_sku>0 and itemid not in (select ebay_code from tb_ebay_code_and_luc_sku);

delete from tb_order_ebay_quantity;
insert into tb_order_ebay_quantity(itemid, quantity)
select distinct item_number, count(item_number) from tb_order_ebay group by item_number;


");
        DataTable dt = Config.ExecuteDataTable(@"select luc_sku , sku from tb_ebay_selling es 
left join tb_ebay_part_comment epc on epc.part_sku = es.luc_sku 
where luc_sku > 0 and (ebay_comment = """" or ebay_comment is null)");
        foreach (DataRow dr in dt.Rows)
        {
            int count = Config.ExecuteScalarInt32("Select count(id) from tb_ebay_part_comment where part_sku = '" + dr["luc_sku"].ToString() + "'");
            if (count > 0)
                Config.ExecuteNonQuery("Update tb_ebay_part_comment set ebay_comment = '" + dr["sku"].ToString() + "' where part_sku='" + dr["luc_sku"].ToString() + "'");
            else
                Config.ExecuteNonQuery("insert into tb_ebay_part_comment(part_sku, ebay_comment, tpl_summary_id, showit,regdate) values ('" + dr["luc_sku"].ToString() + "', '" + dr["sku"].ToString() + "', 0,0,now())");
        }

    }
}