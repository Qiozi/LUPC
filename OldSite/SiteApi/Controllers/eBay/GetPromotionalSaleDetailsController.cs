using SiteApi.Filters;
using SiteApi.Models.Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace SiteApi.Controllers
{
    [CmdFilter]
    public class GetPromotionalSaleDetailsController : BaseApiController
    {
        //
        // GET: /GetPromotionalSaleDetails/

        public Models.PostResult Get(string t)
        {
            var result = GetPromotionalSaleDetailListings(DBContext);
            DeleteNoExistSaleItem();
            AddOnSaleNote();
            return new Models.PostResult
            {
                Success = result,
                ErrMsg = string.Empty
            };
        }

        bool GetPromotionalSaleDetailListings(LU.Data.nicklu2Entities context)
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
<GetPromotionalSaleDetailsRequest xmlns=""urn:ebay:apis:eBLBaseComponents"">
  <RequesterCredentials>
    <eBayAuthToken>{0}</eBayAuthToken>
  </RequesterCredentials>
  <ErrorLanguage>en_US</ErrorLanguage>
  <Version>653</Version>
  <WarningLevel>High</WarningLevel>
  <PromotionalSaleStatus>Active</PromotionalSaleStatus>
  <PromotionalSaleStatus>Scheduled</PromotionalSaleStatus>
  <PromotionalSaleStatus>Processing</PromotionalSaleStatus>
</GetPromotionalSaleDetailsRequest>", userToken);

            LU.Data.eBay.eBayOperationHistory.SaveSendXml(context, sendXml, false, -1);

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
            request.Headers.Add("X-EBAY-API-COMPATIBILITY-LEVEL: 653");

            //Add function name, SiteID and Detail Level to HTTP Headers
            request.Headers.Add("X-EBAY-API-CALL-NAME: GetPromotionalSaleDetails");
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
            catch
            {
                // return wEx.Message;
                return false;
            }
            #endregion

            #region Process Response
            // Get Response into String
            var sr = new StreamReader(str);
            string resultString = sr.ReadToEnd();
            xmlDoc.LoadXml(resultString);
            sr.Close();
            str.Close();

            LU.Data.eBay.eBayOperationHistory.SaveSendXmlResult(context, resultString, false, -1);

            //get the root node, for ease of use
            XmlNode root = xmlDoc["GetPromotionalSaleDetailsResponse"];

            return "Success" == root["Ack"].InnerText ?
                ReadXml(context, root) : false;

            #endregion
        }

        bool ReadXml(LU.Data.nicklu2Entities context, XmlNode root)
        {
            DeleteAll(context);

            foreach (XmlNode item in root["PromotionalSaleDetails"].ChildNodes)
            {
                var saleId = item["PromotionalSaleID"].InnerText;
                var saleName = item["PromotionalSaleName"].InnerText;
                var status = item["Status"].InnerText;
                var discountType = item["DiscountType"].InnerText;
                var discountValue = item["DiscountValue"].InnerText;
                var startTime = item["PromotionalSaleStartTime"].InnerText;
                var endTime = item["PromotionalSaleEndTime"].InnerText;
                var saleType = item["PromotionalSaleType"].InnerText;


                foreach (XmlNode idItem in item["PromotionalSaleItemIDArray"].ChildNodes)
                {
                    var id = idItem.InnerText;

                    var detailModel = new LU.Data.tb_ebay_promotional_sale_details
                    {
                        DiscountType = discountType,
                        DiscountValue = decimal.Parse(discountValue),
                        EndTime = endTime,
                        regdate = DateTime.Now,
                        SaleID = saleId,
                        SaleItemID = id,
                        SaleName = saleName,
                        SaleType = saleType,
                        StartTime = startTime,
                        Status = status
                    };
                    context.tb_ebay_promotional_sale_details.Add(detailModel);
                }
            }
            context.SaveChanges();
            return true;
        }


        bool DeleteAll(LU.Data.nicklu2Entities context)
        {
            var query = context.tb_ebay_promotional_sale_details.ToList();
            foreach (var item in query)
            {
                context.tb_ebay_promotional_sale_details.Remove(item);
            }
            return true;
        }

        bool DeleteNoExistSaleItem()
        {
            var query = DBContext.tb_ebay_promotional_items.ToList();
            foreach (var item in query)
            {
                if (DBContext.tb_ebay_promotional_sale_details.Count(p => p.SaleItemID.Equals(item.ItemId)) == 0)
                {
                    DBContext.tb_ebay_promotional_items.Remove(item);
                }
            }
            DBContext.SaveChanges();
            return true;
        }

        bool AddOnSaleNote()
        {
            var query = DBContext.tb_ebay_part_comment.Where(p => p.part_sku.HasValue && p.ebay_note.Equals("ebay on sale")).ToList();
            foreach (var item in query)
            {
                item.ebay_note = string.Empty;
            }
            DBContext.SaveChanges();
            var items = DBContext.tb_ebay_promotional_items.ToList();
            foreach (var item in items)
            {
                var commentModels = DBContext.tb_ebay_part_comment.Where(p => p.part_sku.HasValue && p.part_sku.Value.Equals(item.luc_sku));
                foreach (var comm in commentModels)
                {
                    DBContext.tb_ebay_part_comment.Remove(comm);
                }
                var newItem = new LU.Data.tb_ebay_part_comment
                {
                    part_sku = item.luc_sku,
                    ebay_note = "ebay on sale",
                    ebay_comment = commentModels.Single(p => p.part_sku.HasValue && p.part_sku.Value.Equals(item.luc_sku)).ebay_comment,
                    regdate = DateTime.Now,
                    showit = true,
                    tpl_summary_id = commentModels.Single(p => p.part_sku.HasValue && p.part_sku.Value.Equals(item.luc_sku)).tpl_summary_id
                };
                DBContext.tb_ebay_part_comment.Add(newItem);
            }
            DBContext.SaveChanges();
            return true;
        }
    }
}
