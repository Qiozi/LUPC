using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Net;
using System.IO;

namespace DownloadEBayOrder
{
    public class DownloadEbayPartPrice : Events.EventBase
    {
        public static bool RunStatus = false;

        public DownloadEbayPartPrice() { }
        /// <summary>
        /// Get eBay active Items.
        /// </summary>
        /// <param name="page"></param>
        /// <param name="the"></param>
        public void GetMySelling(int page, string storePath)
        {
            SetStatus("begin down ebay price page " + page.ToString());

            #region settings
            string devID = Config.devID;
            string appID = Config.appID;
            string certID = Config.certID;

            //Get the Server to use (Sandbox or Production)
            string serverUrl = Config.serverUrl;

            //Get the User Token to Use
            string userToken = Config.userToken;

            //SiteID = 0  (US) - UK = 3, Canada = 2, Australia = 15, ....
            //SiteID Indicates the eBay site to associate the call with
            int siteID = Config.siteID;
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
      <EntriesPerPage>100</EntriesPerPage>
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
                GetMySelling(page + 1, storePath);

            #endregion
        }

        /// <summary>
        /// Read the XML retrieving data endures database.
        /// </summary>
        public void ReadPage(string storePath)
        {
            var DB = new nicklu2Entities();

            string path = storePath;
            if (Directory.Exists(path))
            {
                DirectoryInfo dir = new DirectoryInfo(path);
                FileInfo[] fis = dir.GetFiles();

                // Config.ExecuteNonQuery("delete from tb_ebay_selling_tmp_db");
                var opList = DB.tb_ebay_selling_tmp_db.Where(p => p.id > 0).ToList();

                foreach (var m in opList)
                {
                    //DB.tb_ebay_selling_tmp_db.DeleteObject(m);
                    DB.tb_ebay_selling_tmp_db.Remove(m);
                }
                DB.SaveChanges();


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

                            if (partSKu == ""
                                || partSKu == "0")
                                continue;

                            if (partSKu.Length == 6)
                            {
                                sysSKu = partSKu;
                                partSKu = "0";
                            }
                            else
                                sysSKu = "0";


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

                        SetStatus("save itemid to db: " + ItemID);

                        // tb_ebay_selling_tmp_db tmpdb = tb_ebay_selling_tmp_db.Createtb_ebay_selling_tmp_db(0, ItemID);
                        var tmpdb = new tb_ebay_selling_tmp_db { ItemID = ItemID };
                        tmpdb.BuyItNowPrice = decimal.Parse(BuyItNowPrice);
                        tmpdb.BuyItNowPrice_currencyID = BuyItNowPrice_currencyID;
                        tmpdb.regdate = DateTime.Now;
                        decimal ClassifiedAdPayPerLeadFeeDecimal;
                        decimal.TryParse(ClassifiedAdPayPerLeadFee, out ClassifiedAdPayPerLeadFeeDecimal);
                        tmpdb.ClassifiedAdPayPerLeadFee = ClassifiedAdPayPerLeadFeeDecimal;

                        tmpdb.ItemID = ItemID.Trim();
                        tmpdb.ListingDetails_StartTime = ListingDetails_StartTime;
                        tmpdb.ListingDetails_ViewItemURL = ListingDetails_ViewItemURL;
                        tmpdb.ListingDetails_ViewItemURLForNaturalSearch = ListingDetails_ViewItemURLForNaturalSearch;
                        tmpdb.ListingType = ListingType;
                        tmpdb.luc_sku = int.Parse(partSKu);
                        tmpdb.PictureDetails_GalleryURL = PictureDetails_GalleryURL;

                        int QuantityInt;
                        int.TryParse(Quantity, out QuantityInt);
                        tmpdb.Quantity = QuantityInt;

                        int QuantityAvailableInt;
                        int.TryParse(QuantityAvailable, out QuantityAvailableInt);

                        tmpdb.QuantityAvailable = QuantityAvailableInt;
                        tmpdb.regdate = DateTime.Now;

                        decimal SellingStatus_CurrentPriceDecimal;
                        decimal.TryParse(SellingStatus_CurrentPrice, out SellingStatus_CurrentPriceDecimal);
                        tmpdb.SellingStatus_CurrentPrice = SellingStatus_CurrentPriceDecimal;


                        tmpdb.SellingStatus_CurrentPrice_currencyID = SellingStatus_CurrentPrice_currencyID;

                        decimal ShippingDetails_ShippingServiceCostDecimal;
                        decimal.TryParse(ShippingDetails_ShippingServiceCost, out ShippingDetails_ShippingServiceCostDecimal);
                        tmpdb.ShippingDetails_ShippingServiceCost = ShippingDetails_ShippingServiceCostDecimal;
                        tmpdb.SKU = SKU;
                        tmpdb.sys_sku = int.Parse(sysSKu);
                        tmpdb.TimeLeft = TimeLeft;
                        tmpdb.Title = Title;

                        int WatchCountInt;
                        int.TryParse(WatchCount, out WatchCountInt);
                        tmpdb.WatchCount = WatchCountInt;

                        //DB.AddTotb_ebay_selling_tmp_db(tmpdb);
                        DB.tb_ebay_selling_tmp_db.Add(tmpdb);
                        DB.SaveChanges();


                    }
                    
                    File.Delete(fis[i].FullName);
                }

            }

        }

        static string analyseSKU(string custom_label)
        {
            string sku = custom_label.Trim();

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

        public void UpdateDB()
        {
            var db = new nicklu2Entities();

            var itemList = db.tb_ebay_selling_tmp_db.Where(p => true).ToList();
            var itemIdlist = itemList.Select(p => p.ItemID).ToList();

            var sellList = db.tb_ebay_selling.Where(p => true).ToList();

            foreach (var m in sellList)
            {
                if (!itemIdlist.Contains(m.ItemID.Trim()))
                {
                    SetStatus("delete ebay selling item: " + m.ItemID);
                    //db.DeleteObject(m);
                    db.tb_ebay_selling.Remove(m);
                }
            }
            db.SaveChanges();

            sellList = db.tb_ebay_selling.Where(p => true).ToList();

            for (int i = 0; i < sellList.Count; i++)
            {
                string itemid = sellList[i].ItemID;
                tb_ebay_selling_tmp_db newModel = itemList.FirstOrDefault(p => p.ItemID.Equals(itemid));
                if (newModel != null)
                {
                    sellList[i].BuyItNowPrice = newModel.BuyItNowPrice;
                    sellList[i].BuyItNowPrice_currencyID = newModel.BuyItNowPrice_currencyID;
                    sellList[i].BuyItNowPriceNew = newModel.BuyItNowPrice;
                    sellList[i].ClassifiedAdPayPerLeadFee = newModel.ClassifiedAdPayPerLeadFee;
                    sellList[i].ItemID = newModel.ItemID;
                    sellList[i].ListingDetails_StartTime = newModel.ListingDetails_StartTime;
                    sellList[i].ListingDetails_ViewItemURL = newModel.ListingDetails_ViewItemURL;
                    sellList[i].ListingDetails_ViewItemURLForNaturalSearch = newModel.ListingDetails_ViewItemURLForNaturalSearch;
                    sellList[i].ListingType = newModel.ListingType;
                    sellList[i].luc_sku = newModel.luc_sku;
                    sellList[i].PictureDetails_GalleryURL = newModel.PictureDetails_GalleryURL;
                    sellList[i].Quantity = newModel.Quantity;
                    sellList[i].QuantityAvailable = newModel.QuantityAvailable;
                    //sellList[i].regdate = DateTime.Parse(newModel.regdate.ToString());
                    sellList[i].SellingStatus_CurrentPrice = newModel.SellingStatus_CurrentPrice;
                    sellList[i].SellingStatus_CurrentPrice_currencyID = newModel.SellingStatus_CurrentPrice_currencyID;
                    sellList[i].ShippingDetails_ShippingServiceCost = newModel.ShippingDetails_ShippingServiceCost;
                    sellList[i].SKU = newModel.SKU;
                    sellList[i].sys_sku = newModel.sys_sku;
                    sellList[i].TimeLeft = newModel.TimeLeft;
                    sellList[i].Title = newModel.Title;
                    sellList[i].WatchCount = newModel.WatchCount;
                    sellList[i].regdate = DateTime.Now;
                }
                SetStatus("update ebay selling item: " + newModel.ItemID);
            }
            db.SaveChanges();


            for (int i = 0; i < itemList.Count; i++)
            {
                string itemid = itemList[i].ItemID;

                #region ebay selling list
                var m = sellList.FirstOrDefault(p => p.ItemID == itemid);
                if (m == null)
                {
                    // m = tb_ebay_selling.Createtb_ebay_selling(0, itemid, itemList[i].ListingDetails_StartTime);
                    m = new tb_ebay_selling
                    {
                        ItemID = itemid,
                        ListingDetails_StartTime = itemList[i].ListingDetails_StartTime
                    };
                    m.regdate = DateTime.Now;
                    m.BuyItNowPrice = itemList[i].BuyItNowPrice;
                    m.BuyItNowPrice_currencyID = itemList[i].BuyItNowPrice_currencyID;
                    m.BuyItNowPriceNew = itemList[i].BuyItNowPrice;
                    m.ClassifiedAdPayPerLeadFee = itemList[i].ClassifiedAdPayPerLeadFee;
                    m.ItemID = itemList[i].ItemID;
                    m.ListingDetails_StartTime = itemList[i].ListingDetails_StartTime;
                    m.ListingDetails_ViewItemURL = itemList[i].ListingDetails_ViewItemURL;
                    m.ListingDetails_ViewItemURLForNaturalSearch = itemList[i].ListingDetails_ViewItemURLForNaturalSearch;
                    m.ListingType = itemList[i].ListingType;
                    m.luc_sku = itemList[i].luc_sku;
                    m.PictureDetails_GalleryURL = itemList[i].PictureDetails_GalleryURL;
                    m.Quantity = itemList[i].Quantity;
                    m.QuantityAvailable = itemList[i].QuantityAvailable;
                    m.regdate = DateTime.Parse(itemList[i].regdate.ToString());
                    m.SellingStatus_CurrentPrice = itemList[i].SellingStatus_CurrentPrice;
                    m.SellingStatus_CurrentPrice_currencyID = itemList[i].SellingStatus_CurrentPrice_currencyID;
                    m.ShippingDetails_ShippingServiceCost = itemList[i].ShippingDetails_ShippingServiceCost;
                    m.SKU = itemList[i].SKU;
                    m.sys_sku = itemList[i].sys_sku;
                    m.TimeLeft = itemList[i].TimeLeft;
                    m.Title = itemList[i].Title;
                    m.WatchCount = itemList[i].WatchCount;
                    //db.AddTotb_ebay_selling(m);
                    db.tb_ebay_selling.Add(m);
                    db.SaveChanges();
                }
                #endregion

                #region ebay code
                tb_ebay_code_and_luc_sku ec = db.tb_ebay_code_and_luc_sku.FirstOrDefault(p => p.ebay_code.Equals(itemid));
                if (ec == null)
                {
                    //ec = tb_ebay_code_and_luc_sku.Createtb_ebay_code_and_luc_sku(0);
                    ec = new tb_ebay_code_and_luc_sku();
                    ec.ebay_code = itemid;
                    ec.SKU = itemList[i].luc_sku > 0 ? itemList[i].luc_sku : itemList[i].sys_sku;
                    ec.is_sys = itemList[i].sys_sku > 0;
                    ec.is_online = true;
                    ec.regdate = DateTime.Now;
                    //db.AddTotb_ebay_code_and_luc_sku(ec);
                    db.tb_ebay_code_and_luc_sku.Add(ec);
                    db.SaveChanges();
                }
                #endregion

                #region ebay part comment (Custom Label)

                //            DataTable dt = Config.ExecuteDataTable(@"select luc_sku , sku from tb_ebay_selling es 
                //left join tb_ebay_part_comment epc on epc.part_sku = es.luc_sku 
                //where luc_sku > 0 and (ebay_comment = """" or ebay_comment is null)");
                //            foreach (DataRow dr in dt.Rows)
                //            {
                //                int count = Config.ExecuteScalarInt32("Select count(id) from tb_ebay_part_comment where part_sku = '" + dr["luc_sku"].ToString() + "'");
                //                if (count > 0)
                //                    Config.ExecuteNonQuery("Update tb_ebay_part_comment set ebay_comment = '" + dr["sku"].ToString() + "' where part_sku='" + dr["luc_sku"].ToString() + "'");
                //                else
                //                    Config.ExecuteNonQuery("insert into tb_ebay_part_comment(part_sku, ebay_comment, tpl_summary_id, showit) values ('" + dr["luc_sku"].ToString() + "', '" + dr["sku"].ToString() + "', 0,0)");
                //            }
                if (itemList[i].luc_sku > 0)
                {
                    int lucSku = (int)itemList[i].luc_sku;
                    tb_ebay_part_comment epc = db.tb_ebay_part_comment.FirstOrDefault(p => p.part_sku == lucSku);
                    if (epc != null)
                    {
                        epc.ebay_comment = itemList[i].SKU;
                        db.SaveChanges();
                    }
                    else
                    {
                        //epc = tb_ebay_part_comment.Createtb_ebay_part_comment(0);
                        epc = new tb_ebay_part_comment();
                        epc.ebay_comment = itemList[i].SKU;
                        epc.part_sku = itemList[i].luc_sku;
                        epc.regdate = DateTime.Now;
                        epc.tpl_summary_id = 0;
                        epc.showit = true;
                        //db.AddTotb_ebay_part_comment(epc);
                        db.tb_ebay_part_comment.Add(epc);
                        db.SaveChanges();

                    }
                }
                #endregion

                SetStatus("update ebay selling item, code, custom label: " + itemid);
            }

            var orderEbayList = db.tb_order_ebay.Where(p => true).OrderByDescending(p => p.id).Take(100).Select(p => p.item_number).ToList();
            foreach (var itemid in orderEbayList)
            {
                if (!string.IsNullOrEmpty(itemid.Trim()))
                {
                    SetStatus("stat ebay order quantity: " + itemid);
                    tb_order_ebay_quantity oeq = db.tb_order_ebay_quantity.FirstOrDefault(p => p.itemid.Equals(itemid));
                    if (oeq != null)
                    {
                        int c = db.tb_order_ebay.Where(p => p.item_number.Equals(itemid)).ToList().Count;
                        oeq.quantity = c;
                        db.SaveChanges();
                    }
                    else
                    {
                        // oeq = tb_order_ebay_quantity.Createtb_order_ebay_quantity(0);
                        oeq = new tb_order_ebay_quantity
                        {
                            quantity = 1,
                            itemid = itemid
                        };

                        //db.AddTotb_order_ebay_quantity(oeq); 
                        db.tb_order_ebay_quantity.Add(oeq);
                    }
                }
            }
            db.SaveChanges();


            var sysList = db.tb_ebay_system.Where(p => true).ToList();

            var sysSkus = sellList.Where(s => s.sys_sku > 0).Select(s => s.sys_sku).ToList();
            for (int i = 0; i < sysList.Count; i++)
            {
                int sysSku = sysList[i].id;
                if (sysSkus.Contains(sysSku))
                {
                    sysList[i].cutom_label = sellList.FirstOrDefault(p => p.sys_sku == sysSku).SKU;
                    sysList[i].is_online = true;
                    sysList[i].system_title1 = sellList.FirstOrDefault(p => p.sys_sku == sysSku).Title;
                }
                else
                    sysList[i].is_online = false;

                SetStatus("update ebay system title: " + sysSku);
            }
            db.SaveChanges();

            var sysPartList = db.tb_ebay_system_parts.Where(p => true).ToList();
            for (int i = 0; i < sysPartList.Count; i++)
            {
                int sysSku = (int)sysPartList[i].system_sku;
                if (sysSkus.Contains(sysSku))
                {
                    sysPartList[i].is_online = true;
                }
                else
                    sysPartList[i].is_online = false;

                SetStatus("update ebay part online: " + sysSku);
            }
            db.SaveChanges();

            var codeList = db.tb_ebay_code_and_luc_sku.Where(p => true).ToList();
            for (int i = 0; i < codeList.Count; i++)
            {
                string ebay_code = codeList[i].ebay_code;
                if (itemIdlist.Contains(ebay_code))
                {
                    codeList[i].is_online = true;
                }
                else
                    codeList[i].is_online = false;

                SetStatus("update ebay code online: " + ebay_code);
            }
            db.SaveChanges();
            SetStatus("end " + DateTime.Now.ToString());
        }
    }
}
