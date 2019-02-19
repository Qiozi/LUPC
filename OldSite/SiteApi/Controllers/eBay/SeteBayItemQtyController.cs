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
    public class SeteBayItemQtyController : BaseApiController
    {
        public Models.PostResult Get(string t, int sku)
        {
            var prod = DBContext.tb_product.SingleOrDefault(p => p.product_serial_no.Equals(sku));
            return prod == null ? new Models.PostResult
            {
                ErrMsg = "no find product.",
                Success = false
            } : Exec(prod);
        }

        Models.PostResult Exec(SiteDB.tb_product prod)
        {
            if (prod.curr_change_quantity < 3)
            {
                return new Models.PostResult
                {
                    Success = false,
                    ErrMsg = "stock is less."
                };
            }
            if (string.IsNullOrEmpty(prod.curr_change_regdate))
            {
                return new Models.PostResult
                {
                    Success = false,
                    ErrMsg = "price date is expired."
                };
            }

            var ebaySellItem = DBContext.tb_ebay_selling.SingleOrDefault(s => s.luc_sku.HasValue &&
                s.luc_sku.Value.Equals(prod.product_serial_no));
            if (ebaySellItem != null)
            {
                ExecEbayApi(DBContext, prod.product_serial_no, ebaySellItem.ItemID, 6, SiteDB.eBay.GetItemSpecifics.GetPartSpecifics(DBContext, prod));
                return new Models.PostResult
                {
                    Success = true
                };
            }

            return new Models.PostResult
            {
                Success = false,
                ErrMsg = "not on ebay.ca"
            };
        }

        bool ExecEbayApi(SiteDB.nicklu2Entities context, int sku, string itemid, int quantity, string itemSpecifics)
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
            var xmlDoc = new XmlDocument();

            string sendXml = string.Format(@"<?xml version=""1.0"" encoding=""utf-8""?>
    <ReviseItemRequest xmlns=""urn:ebay:apis:eBLBaseComponents"">
      <RequesterCredentials>
        <eBayAuthToken>{0}</eBayAuthToken>
      </RequesterCredentials>
      <ErrorLanguage>en_US</ErrorLanguage>
      <WarningLevel>High</WarningLevel>
      <Item>
        <ItemID>{1}</ItemID>
        <Quantity>{2}</Quantity>
        {3}
      </Item>
    </ReviseItemRequest>
    ", userToken
         , itemid
         , quantity
         , itemSpecifics);

            var sendXmlModel = new SiteDB.tb_ebay_send_xml_history
            {
                Content = sendXml,
                is_sys = false,
                SKU = sku,
                regdate = DateTime.Now,
                is_modify = true,
                comm = string.Empty
            };
            context.tb_ebay_send_xml_history.Add(sendXmlModel);
            context.SaveChanges();
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
            catch
            {
                return false;
            }
            #endregion

            #region Process Response
            // Get Response into String
            var sr = new StreamReader(str);
            string sssss = sr.ReadToEnd();
            xmlDoc.LoadXml(sssss);
            sr.Close();
            str.Close();

            var esrhm = new SiteDB.tb_ebay_send_xml_result_history
            {
                Content = sssss,
                is_sys = false,
                is_modify = true,
                comm = string.Empty,
                regdate = DateTime.Now,
                SKU = sku
            };
            context.tb_ebay_send_xml_result_history.Add(esrhm);
            context.SaveChanges();
            return true;
            #endregion
        }
    }
}
