using SiteApi.Filters;
using SiteApi.Models.Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Http;
using System.Xml;

namespace SiteApi.Controllers
{
    [CmdFilter]
    public class SetPromotionalSaleController : BaseApiController
    {
        //
        // GET: /SetPromotionalSale/

        public class SaleInfo
        {
            public string t { get; set; }
            public DateTime BeginDate { get; set; }

            public DateTime EndDate { get; set; }

            public string Title { get; set; }

            public decimal Discount { get; set; }

            public bool AutoRun { get; set; }
        }

        public Models.PostResult Post([FromBody] SaleInfo saleInfo)
        {
            var valid = Validate(saleInfo.t);
            if (!valid.Success)
            {
                return valid;
            }
            if (saleInfo.AutoRun)
            {
                CanAutoRun();
                return new Models.PostResult
                {
                    Success = true
                };
            }
            else
            {
                var saleId = SeteBayOnSale(DBContext, saleInfo.Discount, saleInfo.BeginDate, saleInfo.EndDate, saleInfo.Title, false);
                return new Models.PostResult
                {
                    Data = saleId,
                    Success = saleId.Length < 20,
                    ErrMsg = saleId.Length < 20 ? string.Empty : saleId
                };
            }
        }

        bool CanAutoRun()
        {
            string[] titles = new string[] { "Laptop On Sale(50$)", "Laptop On Sale(100$)", "Laptop On Sale(180$)"
                , "Part On Sale(10$)"
                , "Part On Sale(20$)"
                , "Part On Sale(30$)"
                , "Part On Sale(40$)"};
            decimal[] discounts = new decimal[] { 50M, 100M, 180M, 10M, 20M, 30M, 40M };

            for (int i = 0; i < titles.Length; i++)
            {
                var title = titles[i];
                var discount = discounts[i];

                if (DBContext.tb_ebay_promotional_sale_id.Count(p => p.title.Equals(title) &&
                        (p.endDate > DateTime.Now || p.beginDate > DateTime.Now)) > 0)
                {
                    continue;
                }
                var saleInfo = new SaleInfo
                {
                    Discount = discount,
                    AutoRun = false,
                    BeginDate = DateTime.Now.AddDays(1),
                    EndDate = DateTime.Parse(DateTime.Now.AddMonths(1).ToString("yyyy-MM-01")).AddDays(-2),
                    Title = title
                };
                var lucSkus = GenerateSaleItem(discount);
                if (lucSkus.Count > 0)
                {
                    var saleId = SeteBayOnSale(DBContext, saleInfo.Discount, saleInfo.BeginDate, saleInfo.EndDate, saleInfo.Title, true);
                    foreach (var sku in lucSkus)
                    {
                        var saleItems = DBContext.tb_ebay_promotional_items.Single(p => p.luc_sku.Equals(sku));
                        saleItems.SaleId = saleId;
                    }
                    DBContext.SaveChanges();
                }
            }
            return true;
        }

        public

        List<int> GenerateSaleItem(decimal discount)
        {
            var resultSku = new List<int>();

            var priceItem = Models.eBayOnSale.eBayOnSalePriceArea.PriceArea(discount);

            if (discount < 50M)
            {
                return DBContext.tb_ebay_promotional_items
                    .Where(p => p.SavePrice.Equals(priceItem.RealPrice))
                    .Select(p => p.luc_sku).ToList();
            }
            else
            {
                return GenerateSaleItemGreaterThen50M(priceItem.MinPrice
                    , priceItem.MaxPrice
                    , priceItem.RealPrice);
            }
        }

        List<int> GenerateSaleItemGreaterThen50M(decimal minPrice, decimal maxPrice, decimal realDiscount)
        {
            var resultSku = new List<int>();
            //var query = DBContext.tb_on_sale.Where(p => p.product_serial_no.HasValue &&
            //   p.end_datetime.HasValue && p.end_datetime.Value > DateTime.Now &&
            //   p.price.HasValue && p.price >= minPrice && p.price <= maxPrice)
            //   .Select(p => p.product_serial_no.Value)
            //   .ToList();

            var query = (from c in DBContext.tb_product
                         join s in DBContext.tb_other_inc_part_info on c.product_serial_no equals s.luc_sku.Value
                         where c.menu_child_serial_no.HasValue && c.menu_child_serial_no.Value.Equals(350) &&
                         s.other_inc_id.HasValue && s.other_inc_store_sum.HasValue &&
                         s.other_inc_id.Value.Equals(20) && s.other_inc_store_sum.Value > 10
                         select new
                         {
                             sku = c.product_serial_no
                         }).Distinct().ToList();
            foreach (var product in query)
            {
                var sku = product.sku;
                var existItem = DBContext.tb_ebay_promotional_items.FirstOrDefault(p => p.luc_sku.Equals(sku));
                var exist = false;
                if (existItem != null)
                {
                    if (!string.IsNullOrEmpty(existItem.SaleId))
                    {
                        var saleModel = DBContext.tb_ebay_promotional_sale_id.FirstOrDefault(p => p.PromotionalSaleID.Equals(existItem.SaleId));
                        if (saleModel.endDate.Date <= DateTime.Now.Date)
                        {
                            DBContext.tb_ebay_promotional_items.Remove(existItem);
                        }
                        else
                        {
                            exist = true;
                        }
                    }
                }
                if (!exist)
                {
                    var ebaySellingModel = DBContext.tb_ebay_selling.FirstOrDefault(p => p.luc_sku.HasValue && p.BuyItNowPrice.HasValue &&
                        p.luc_sku.Value.Equals(sku));
                    if (ebaySellingModel != null)
                    {

                        if (!ebaySellingModel.BuyItNowPrice.HasValue || ebaySellingModel.BuyItNowPrice.Value < minPrice || ebaySellingModel.BuyItNowPrice.Value > maxPrice)
                        {
                            continue;
                        }
                        var item = new LU.Data.tb_ebay_promotional_items
                        {
                            IsSys = false,
                            ItemId = ebaySellingModel.ItemID,//TODO
                            luc_sku = product.sku,
                            Regdate = DateTime.Now,
                            SaleId = string.Empty,
                            SavePrice = realDiscount
                        };
                        resultSku.Add(product.sku);
                        DBContext.tb_ebay_promotional_items.Add(item);
                    }
                }
            }
            DBContext.SaveChanges();
            return resultSku;
        }

        string SeteBayOnSale(LU.Data.nicklu2Entities context, decimal discount, DateTime begin, DateTime end, string title, bool isAuto)
        {
            #region settings

            string devID = Models.Shared.eBaySettings.devId;
            string appID = Models.Shared.eBaySettings.appId;
            string certID = Models.Shared.eBaySettings.certId;

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
<SetPromotionalSaleRequest xmlns=""urn:ebay:apis:eBLBaseComponents"">
  <RequesterCredentials>
    <eBayAuthToken>{0}</eBayAuthToken>
  </RequesterCredentials>
  <ErrorLanguage>en_US</ErrorLanguage>
  <WarningLevel>High</WarningLevel>
  <Action>Add</Action>
  <PromotionalSaleDetails>
    <PromotionalSaleName>{4}</PromotionalSaleName>
    <DiscountType>Price</DiscountType>
    <DiscountValue>{1}</DiscountValue>
    <PromotionalSaleType>PriceDiscountOnly</PromotionalSaleType>
    <PromotionalSaleStartTime>{2}T01:00:00.000Z</PromotionalSaleStartTime>
    <PromotionalSaleEndTime>{3}T23:00:00.000Z</PromotionalSaleEndTime>
  </PromotionalSaleDetails>
</SetPromotionalSaleRequest>", userToken
                      , discount
                      , begin.ToString("yyyy-MM-dd")
                      , end.ToString("yyyy-MM-dd")
                      , title);

            //throw new Exception(sendXml);
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
            request.Headers.Add("X-EBAY-API-COMPATIBILITY-LEVEL: 761");

            //Add function name, SiteID and Detail Level to HTTP Headers
            request.Headers.Add("X-EBAY-API-CALL-NAME: SetPromotionalSale");
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

                return wEx.Message;
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
            XmlNode root = xmlDoc["SetPromotionalSaleResponse"];

            //There have been Errors
            if (root["Errors"] != null)
            {
                try
                {
                    return root["PromotionalSaleID"].InnerText;
                }
                catch { }
                return (resultString);
            }
            else
            {
                var promotionalSaleId = root["PromotionalSaleID"].InnerText;

                var saleIdModel = new LU.Data.tb_ebay_promotional_sale_id
                {
                    price = discount,
                    beginDate = begin,
                    endDate = end,
                    PromotionalSaleID = promotionalSaleId,
                    title = title,
                    regdate = DateTime.Now,
                    auto = isAuto
                };
                context.tb_ebay_promotional_sale_id.Add(saleIdModel);
                context.SaveChanges();

                return promotionalSaleId;
            }
            #endregion
        }
    }
}
