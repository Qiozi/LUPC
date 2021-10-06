using SiteApi.Filters;
using SiteApi.Models.Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Xml;

namespace SiteApi.Controllers
{
    [CmdFilter]
    public class SetPromotionalSaleListingsController : BaseApiController
    {
        //
        // GET: /SetPromotionalSaleListings/

        public class Item
        {
            public string t { get; set; }

            public int Sku { get; set; }

            public string ItemIds { get; set; }

            public bool IsSys { get; set; }

            public int SaleId { get; set; }

            public bool AutoRun { get; set; }

            public decimal AutoRunPrice { get; set; }
        }

        public Models.PostResult Post([FromBody] Item itemInfo)
        {
            var valid = Validate(itemInfo.t);
            if (!valid.Success)
            {
                return valid;
            }

            if (itemInfo.AutoRun)
            {
                return new Models.PostResult
                {
                    Success = AutoRunItem()
                };
            }
            else
            {
                var sale = DBContext.tb_ebay_promotional_sale_id.Single(p => p.Id.Equals(itemInfo.SaleId));

                var result = RunOne(itemInfo, sale.PromotionalSaleID);

                return new Models.PostResult
                {
                    Success = result,
                    ErrMsg = result == false ? "set promotional is error." : string.Empty
                };
            }
        }

        string GetItemIdXmlString(List<string> itemIds)
        {
            var result = string.Empty;
            foreach (var itemid in itemIds)
            {
                result += string.Concat("<ItemID>", itemid, "</ItemID>");
            }
            return result;
        }

        bool AutoRunItem()
        {
            var saleIds = DBContext.tb_ebay_promotional_sale_id.Where(p => p.auto.Equals(true) &&
                p.beginDate > DateTime.Now).ToList();
            foreach (var saleIdInfo in saleIds)
            {
                var item = GetItemInfo(saleIdInfo);
                RunOne(item, saleIdInfo.PromotionalSaleID);
            }
            return true;
        }

        bool RunOne(Item item, string promotionalSaleId)
        {
            if (!string.IsNullOrEmpty(item.ItemIds))
            {
                var itemString = GetItemIdXmlString(item.ItemIds.Split(new char[] { ',' }).ToList());
                var result = SetPromotionalSaleListings(DBContext, item.Sku, itemString, item.IsSys, promotionalSaleId);
                return true;
            }
            return false;
        }

        Item GetItemInfo(LU.Data.tb_ebay_promotional_sale_id saleId)
        {
            return new Item
            {
                IsSys = false,
                AutoRun = false,
                AutoRunPrice = 0M,
                SaleId = saleId.Id,
                Sku = 0,
                ItemIds = string.Join(",", GetSaleItem(saleId.price, saleId.PromotionalSaleID).ToArray())
            };
        }

        List<string> GetSaleItem(decimal discount, string ebaySaleId)
        {
            var query = DBContext.tb_ebay_promotional_items.Where(p => p.SaleId.Equals(ebaySaleId)).ToList();
            return query.Select(p => p.ItemId).ToList();
        }

        List<string> GetSaleItem(decimal price)
        {
            var prodIds = DBContext.tb_product.Where(p => p.menu_child_serial_no.HasValue &&
                p.menu_child_serial_no.Value.Equals(350) &&
                p.adjustment_regdate.HasValue &&
                p.adjustment_regdate.Value.Year == DateTime.Now.Year &&
                p.adjustment_regdate.Value.Month == DateTime.Now.Month &&
                p.adjustment_regdate.Value.Day == DateTime.Now.Day &&
                p.adjustment.HasValue && p.adjustment.Value.Equals(price)).Select(p => p.product_serial_no).ToList();

            return DBContext.tb_ebay_selling.Where(p => p.luc_sku.HasValue && prodIds.Contains(p.luc_sku.Value)).Select(p => p.ItemID).ToList();
        }

        string SetPromotionalSaleListings(LU.Data.nicklu2Entities context, int sku, string itemIdString, bool isSys, string promotionalSaleId)
        {
            #region settings

            string devID = Models.Shared.eBaySettings.devId;
            string appID = eBaySettings.appId;
            string certID = eBaySettings.certId;

            //Get the Server to use (Sandbox or Production)
            string serverUrl = eBaySettings.serverUrl;

            //Get the User Token to Use
            string userToken = eBaySettings.userToken;

            //SiteID = 0  (US) - UK = 3, Canada = 2, Australia = 15, ....
            //SiteID Indicates the eBay site to associate the call with
            int siteID = eBaySettings.siteId;
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

            LU.Data.eBay.eBayOperationHistory.SaveSendXml(context, sendXml, isSys, -1);

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
                return wEx.Message;
            }
            #endregion

            #region Process Response
            // Get Response into String
            StreamReader sr = new StreamReader(str);
            string resultString = sr.ReadToEnd();
            xmlDoc.LoadXml(resultString);
            sr.Close();
            str.Close();

            LU.Data.eBay.eBayOperationHistory.SaveSendXmlResult(context, resultString, isSys, -1);

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
                return (resultString);
            }
            else
            {
                return root["Ack"].InnerText;
            }
            #endregion
        }
    }
}
