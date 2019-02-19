using System.Data;
using System.Xml;
using System.Linq;
using System;
using System.Collections.Generic;
using LU.Data;

/// <summary>
/// Summary description for eBayShipping
/// </summary>
public class eBayShipping
{
    public eBayShipping()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    /// <summary>
    /// Basic shipping
    /// </summary>
    /// <param name="ScanSize">屏目尺寸</param>
    /// <returns></returns>
    public static decimal BasicShippingFee(decimal ScanSize)
    {
        if (EbaySettings.ebayBasicShippingFee != 1000M)
        {
            return EbaySettings.ebayBasicShippingFee;
        }
        if (ScanSize > 1M && ScanSize < 11.99M)
        {
            return 40M;
        }
        else if (ScanSize >= 12 && ScanSize < 14M)
        {
            return 40M;
        }
        else if (ScanSize >= 14 && ScanSize < 17M)
        {
            return 40M;
        }
        else
        {
            return 45M;
        }
    }

    /// <summary>
    /// expedited Canada, UPS 3days
    /// </summary>
    /// <param name="ScanSize">屏目尺寸</param>
    /// <param name="weight"></param>
    /// <returns></returns>
    public static decimal UPS3Days_ShippingFee(decimal ScanSize)
    {
        if (ScanSize > 1M && ScanSize < 11.99M)
        {
            return 80M; // 45m
        }
        else if (ScanSize >= 12M && ScanSize < 14M)
        {
            return 80M; // 45m
        }
        else if (ScanSize >= 14M && ScanSize < 17M)
        {
            return 80M; // 45m
        }
        else
        {
            return 105M; // 55m
        }
    }

    /// <summary>
    /// World expedited
    /// </summary>
    /// <param name="ScanSize">屏目尺寸</param>
    /// <returns></returns>
    public static decimal UPS_WorldExpedited_ShippingFee(decimal ScanSize)
    {
        if (ScanSize > 1M && ScanSize < 11.99M)
        {
            return 95M;
        }
        else if (ScanSize >= 12M && ScanSize < 14M)
        {
            return 95M;
        }
        else if (ScanSize >= 14M && ScanSize < 17M)
        {
            return 105M;
        }
        else
        {
            return 120M;
        }
    }

    public static decimal UPS_WorldExpedited_ShippingFee_Height(decimal ScanSize)
    {
        if (ScanSize > 1M && ScanSize < 11.99M)
        {
            return 145M;
        }
        else if (ScanSize >= 12M && ScanSize < 14M)
        {
            return 145M;
        }
        else if (ScanSize >= 14M && ScanSize < 17M)
        {
            return 145M;
        }
        else
        {
            return 185M;
        }
    }

    //public static decimal UPS_Express_ShippingFee(decimal ScanSize, decimal weight)
    //{
    //    if (ScanSize > 1M && ScanSize < 11.99M)
    //    {
    //        return 115M;
    //    }
    //    else if (ScanSize >= 12M && ScanSize < 14M)
    //    {
    //        return 115M;
    //    }
    //    else if (ScanSize >= 14M && ScanSize < 17M)
    //    {
    //        return 125M;
    //    }
    //    else
    //    {
    //        return 140M;
    //    }
    //}

    public static decimal UPS_Express_ShippingFeeHeight(decimal ScanSize)
    {
        if (ScanSize > 1M && ScanSize < 11.99M)
        {
            return 165M;
        }
        else if (ScanSize >= 12M && ScanSize < 14M)
        {
            return 165M;
        }
        else if (ScanSize >= 14M && ScanSize < 17M)
        {
            return 165M;
        }
        else
        {
            return 205M;
        }
    }
    public static decimal UPS_Express_ShippingFee(decimal ScanSize)
    {
        if (ScanSize > 1M && ScanSize < 11.99M)
        {
            return 115M;
        }
        else if (ScanSize >= 12M && ScanSize < 14M)
        {
            return 115M;
        }
        else if (ScanSize >= 14M && ScanSize < 17M)
        {
            return 125M;
        }
        else
        {
            return 140M;
        }
    }
    /// <summary>
    /// eBay System Shipping To Ca.
    /// base shipping
    /// 2012-07-17 change.
    /// </summary>
    /// <param name="price"></param>
    /// <returns></returns>
    public static decimal SysShippingCa(decimal price)
    {


        if (price > 0M && price <= 500M)
            return 34.9M;
        else if (price > 500.01M && price <= 700M)
            return 39.9M;
        else if (price > 700.01M && price <= 900M)
            return 44.9M;
        else
            return 49.9M;

    }
    /// <summary>
    /// eBay System Shipping To CA 
    /// 3day shipping
    /// </summary>
    /// <param name="price"></param>
    /// <returns></returns>
    public static decimal SysShippingScLvlCA(decimal price)
    {
        return SysShippingScLvlUS(price);
        //        shpScLvl = ""
        //      Select Case a
        //    Case 1 To 700: shpScLvl = 60
        //    Case 700.01 To 1050: shpScLvl = 65
        //    Case 1050.01 To 1440: shpScLvl = 85
        //    Case Is > 1440: shpScLvl = 95
        //    End Select
        //if (price > 0M && price <= 700M)
        //    return 60M;
        //else if (price > 700.01M && price <= 1050M)
        //    return 65M;
        //else if (price > 1050.01M && price <= 1440M)
        //    return 85M;
        //else
        //    return 95M;
    }

    /// <summary>
    /// World Express
    /// </summary>
    /// <param name="price"></param>
    /// <returns></returns>
    public static decimal SysShippingWorldExpress(decimal price)
    {
        if (price > 0M && price <= 500M)
            return 117M;// - 34M;
        else if (price > 500.01M && price <= 700M)
            return 166M;// - 39M;
        else if (price > 700.01M && price <= 900M)
            return 205M;// - 44M;
        else
            return 245M;// -49M;
        //    shipping1 = ""
        //Select Case A
        //    Case 1 To 500: shipping1 = 117 - 34
        //    Case 500.01 To 700: shipping1 = 166 - 39
        //    Case 700.01 To 900: shipping1 = 205 - 44
        //    Case Is > 900: shipping1 = 245 - 49
        //End Select
    }

    /// <summary>
    /// UPS Expedited Canada
    /// UPS Expedited US
    /// </summary>
    /// <param name="price"></param>
    /// <returns></returns>
    public static decimal SysShippingExpeditedCAUS(decimal price)
    {
        if (price > 0M && price <= 500M)
            return 97M;// - 34M;
        else if (price > 500.01M && price <= 700M)
            return 116M;//- 39M;
        else if (price > 700.01M && price <= 900M)
            return 155M;// - 44M;
        else
            return 160M;// -49M;


        //shipping2 = ""
        //    Select Case A
        //        Case 1 To 500: shipping2 = 97 - 34
        //        Case 500.01 To 700: shipping2 = 116 - 39
        //        Case 700.01 To 900: shipping2 = 155 - 44
        //        Case Is > 900: shipping2 = 165 - 49
        //    End Select
    }
    /// <summary>
    /// eBay System Shipping To US.
    /// </summary>
    /// <param name="price"></param>
    /// <returns></returns>
    public static decimal SysShippingUS(decimal price)
    {

        return SysShippingCa(price);
        //if (EbaySettings.ebayBasicShippingFee != 1000M)
        //    return EbaySettings.ebayBasicShippingFee;

        //if (price > 0M && price <= 700M)
        //    return 40M;
        //else if (price > 700.01M && price <= 1050M)
        //    return 45M;
        //else if (price > 1050.01M && price <= 1440M)
        //    return 50M;
        //else
        //    return 55M;
    }
    /// <summary>
    /// eBay System Shipping To US UPS 3Days Expedited US
    /// </summary>
    /// <param name="price"></param>
    /// <returns></returns>
    public static decimal SysShippingScLvlUS(decimal price)
    {
        //if (price > 0M && price <= 700M)
        //    return 60M;
        //else if (price > 700.01M && price <= 1050M)
        //    return 65M;
        //else if (price > 1050.01M && price <= 1440M)
        //    return 75M;
        //else
        //    return 95M;

        if (price > 0M && price <= 500M)
            return 77M - 34M;// - 34M;
        else if (price > 500.01M && price <= 700M)
            return 96M - 39M;// - 39M;
        else if (price > 700.01M && price <= 900M)
            return 135M - 46M;// - 46M;
        else
            return 150M - 51M;// -51M;

        //Function shipping3(A)
        //'expedited Canada, UPS 3 days
        //'based on ground shipping free

        //shipping3 = ""
        //Select Case A
        //    Case 1 To 500: shipping3 = 77 - 34
        //    Case 500.01 To 700: shipping3 = 96 - 39
        //    Case 700.01 To 900: shipping3 = 135 - 46
        //    Case Is > 900: shipping3 = 150 - 51

        //    End Select

        //End Function
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="sku"></param>
    /// <param name="IsSystem"></param>
    /// <param name="is_shrink"></param>
    /// <param name="domestic_service_1"></param>
    /// <param name="domestic_service_1_cost"></param>
    /// <param name="domestic_free_shipping"></param>
    /// <param name="domestic_service_2"></param>
    /// <param name="domestic_service_2_cost"></param>
    /// <param name="domestic_services_3"></param>
    /// <param name="domestic_service_3_cost"></param>
    /// <param name="international_service_1"></param>
    /// <param name="international_service_1_cost"></param>
    /// <param name="international_service_2"></param>
    /// <param name="international_service_2_cost"></param>
    /// <param name="international_service_3"></param>
    /// <param name="international_service_3_cost"></param>
    /// <param name="international_service_4"></param>
    /// <param name="international_service_4_cost"></param>
    /// <returns></returns>
    public static string GetShippingDetail2(nicklu2Entities context,
        bool is_notebook
        , int sku
        , bool IsSystem
        , bool is_shrink
        , string domestic_service_1
        , decimal domestic_service_1_cost
        , bool domestic_free_shipping
        , string domestic_service_2
        , decimal domestic_service_2_cost
        , string domestic_services_3
        , decimal domestic_service_3_cost
        , string domestic_services_4
        , decimal domestic_service_4_cost
        , string international_service_1
        , decimal international_service_1_cost
        , string international_service_2
        , decimal international_service_2_cost
        , string international_service_3
        , decimal international_service_3_cost
        , string international_service_4
        , decimal international_service_4_cost
        , decimal scanSize
        , decimal weight
        , System.Web.HttpServerUtility http)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        //var esm = EbaySystemModel.GetEbaySystemModel(sku);

        decimal screen = 0M;
        if (is_notebook) // 笔记本 澳洲加30元，17以上加50元
        {
            var pm = ProductModel.GetProductModel(context, sku);
            if (pm != null)
                screen = pm.screen_size.Value;
        }

        sb.Append("<PaymentInstructions>Payment must be received within 7 business days of purchase.</PaymentInstructions>");
        int n = 1;

        if (IsSystem)
        {
            #region domestic_service_2_cost
            if (domestic_service_2_cost >= 0M ||
                (is_shrink && domestic_service_2_cost == 0M))
            {
                sb.Append(string.Format(@"
      <ShippingServiceOptions>
        {4}
        <ShippingService>{0}</ShippingService>
        <ShippingServicePriority>{1}</ShippingServicePriority>
        <ShippingServiceCost>{2}</ShippingServiceCost>
        <ShippingServiceAdditionalCost>{3}</ShippingServiceAdditionalCost>
      </ShippingServiceOptions>
"
     , domestic_service_2
     , n
     , domestic_service_2_cost
     , new eBayPriceHelper().eachAddItemShipping(domestic_service_2_cost)
     , domestic_service_2_cost == 0M ? "<FreeShipping>true</FreeShipping>" : ""));
                n += 1;
            }
            #endregion
        }
        else
        {
            #region domestic_service_2_cost
            if (domestic_service_2_cost >= 0M)
            {
                sb.Append(string.Format(@"
      <ShippingServiceOptions>
        {4}
        <ShippingService>{0}</ShippingService>
        <ShippingServicePriority>{1}</ShippingServicePriority>
        <ShippingServiceCost>{2}</ShippingServiceCost>
        <ShippingServiceAdditionalCost>{3}</ShippingServiceAdditionalCost>
      </ShippingServiceOptions>
"
     , domestic_service_2
     , n
     , domestic_service_2_cost
     , new eBayPriceHelper().eachAddItemShipping(domestic_service_2_cost)
     , domestic_service_2_cost == 0M ? "<FreeShipping>true</FreeShipping>" : ""));
                n += 1;
            }
            #endregion
        }

        if (IsSystem)
        {
            #region domestic_service_3_cost
            if (domestic_service_3_cost > 0M ||
                (is_shrink && domestic_service_3_cost == 0M))
            {
                sb.Append(string.Format(@"
      <ShippingServiceOptions>
        {4}
        <ShippingService>{0}</ShippingService>
        <ShippingServicePriority>{1}</ShippingServicePriority>
        <ShippingServiceCost>{2}</ShippingServiceCost>
        <ShippingServiceAdditionalCost>{3}</ShippingServiceAdditionalCost>
      </ShippingServiceOptions>
"
     , domestic_services_3
     , n
     , domestic_service_3_cost
     , new eBayPriceHelper().eachAddItemShipping(domestic_service_3_cost)
     , domestic_service_3_cost == 0M ? "<FreeShipping>true</FreeShipping>" : ""));
                n += 1;
            }
            #endregion
        }
        else
        {
            #region domestic_service_3_cost
            if (domestic_service_3_cost > 0M)
            {
                sb.Append(string.Format(@"
      <ShippingServiceOptions>
        {4}
        <ShippingService>{0}</ShippingService>
        <ShippingServicePriority>{1}</ShippingServicePriority>
        <ShippingServiceCost>{2}</ShippingServiceCost>
        <ShippingServiceAdditionalCost>{3}</ShippingServiceAdditionalCost>
      </ShippingServiceOptions>
"
     , domestic_services_3
     , n
     , domestic_service_3_cost
     , new eBayPriceHelper().eachAddItemShipping(domestic_service_3_cost)
     , ""));
                n += 1;
            }
            #endregion
        }

        n = 1;

        if (IsSystem)
        {
            #region international_service_1_cost
            if (international_service_1_cost >= 0M ||
                (is_shrink && international_service_1_cost == 0M))
            {
                sb.Append(string.Format(@"
      <InternationalShippingServiceOption>
        {4}
        <ShippingService>{0}</ShippingService>
        <ShippingServicePriority>{1}</ShippingServicePriority>
        <ShippingServiceCost>{2}</ShippingServiceCost>
        <ShippingServiceAdditionalCost>{3}</ShippingServiceAdditionalCost>
        <ShipToLocation>US</ShipToLocation>
      </InternationalShippingServiceOption>
"
     , international_service_1
     , n
     , international_service_1_cost
     , new eBayPriceHelper().eachAddItemShipping(international_service_1_cost)
     , international_service_1_cost == 0M ? "<FreeShipping>true</FreeShipping>" : ""));
                n += 1;
            }
            #endregion
        }
        else
        {
            #region international_service_1_cost
            if (international_service_1_cost >= 0M)
            {
                sb.Append(string.Format(@"
      <InternationalShippingServiceOption>
        {4}
        <ShippingService>{0}</ShippingService>
        <ShippingServicePriority>{1}</ShippingServicePriority>
        <ShippingServiceCost>{2}</ShippingServiceCost>
        <ShippingServiceAdditionalCost>{3}</ShippingServiceAdditionalCost>
        <ShipToLocation>US</ShipToLocation>
      </InternationalShippingServiceOption>
"
     , international_service_1
     , n
     , international_service_1_cost
     , new eBayPriceHelper().eachAddItemShipping(international_service_1_cost)
     , international_service_1_cost == 0M ? "" : ""));
                n += 1;
            }
            #endregion
        }

        if (IsSystem)
        {
            #region international_service_2_cost
            if (international_service_2_cost > 0M)
            {
                sb.Append(string.Format(@"
      <InternationalShippingServiceOption>
        {4}
        <ShippingService>{0}</ShippingService>
        <ShippingServicePriority>{1}</ShippingServicePriority>
        <ShippingServiceCost>{2}</ShippingServiceCost>
        <ShippingServiceAdditionalCost>{3}</ShippingServiceAdditionalCost>
        <ShipToLocation>US</ShipToLocation>
      </InternationalShippingServiceOption>
"
     , international_service_2
     , n
     , international_service_2_cost
     , new eBayPriceHelper().eachAddItemShipping(international_service_2_cost)
     , international_service_2_cost == 0M ? "<FreeShipping>true</FreeShipping>" : ""));
                n += 1;
            }
            #endregion
        }
        else
        {
            #region international_service_2_cost
            if (international_service_2_cost > 0M)
            {
                sb.Append(string.Format(@"
      <InternationalShippingServiceOption>
        {4}
        <ShippingService>{0}</ShippingService>
        <ShippingServicePriority>{1}</ShippingServicePriority>
        <ShippingServiceCost>{2}</ShippingServiceCost>
        <ShippingServiceAdditionalCost>{3}</ShippingServiceAdditionalCost>
        <ShipToLocation>US</ShipToLocation>
      </InternationalShippingServiceOption>
"
     , international_service_2
     , n
     , international_service_2_cost
     , new eBayPriceHelper().eachAddItemShipping(international_service_2_cost)
     , ""));
                n += 1;
            }
            #endregion
        }

        if (IsSystem)
        {
            #region international_service_3_cost
            //            if (international_service_3_cost > 0M ||
            //                (is_shrink && international_service_3_cost == 0M))
            //            {
            //                sb.Append(string.Format(@"
            //      <InternationalShippingServiceOption>
            //        {4}
            //        <ShippingService>{0}</ShippingService>
            //        <ShippingServicePriority>{1}</ShippingServicePriority>
            //        <ShippingServiceCost>{2}</ShippingServiceCost>
            //        <ShippingServiceAdditionalCost>{3}</ShippingServiceAdditionalCost>
            //        <ShipToLocation>US</ShipToLocation>
            //      </InternationalShippingServiceOption>
            //"
            //     , international_service_3
            //     , n
            //     , international_service_3_cost
            //     , new eBayPriceHelper().eachAddItemShipping(international_service_3_cost)
            //     , international_service_3_cost == 0M ? "<FreeShipping>true</FreeShipping>" : ""));
            //                n += 1;
            //            }
            #endregion
        }
        else
        {
            #region international_service_3_cost
            if (international_service_3_cost > 0M)
            {
                sb.Append(string.Format(@"
      <InternationalShippingServiceOption>{4}
        <ShippingService>{0}</ShippingService>
        <ShippingServicePriority>{1}</ShippingServicePriority>
        <ShippingServiceCost>{2}</ShippingServiceCost>
        <ShippingServiceAdditionalCost>{3}</ShippingServiceAdditionalCost>
        {5}
      </InternationalShippingServiceOption>
      {6}
"
     , international_service_3
     , n
     , international_service_3_cost
     , new eBayPriceHelper().eachAddItemShipping(international_service_3_cost)
     , ""
     , is_notebook && international_service_3 == "CA_UPSWorldWideExpedited" ? GetNotebookShipTOCountryLess() : "<ShipToLocation>US</ShipToLocation>"
     , is_notebook && international_service_3 == "CA_UPSWorldWideExpedited" ? GetNotebookShipToCountry(context, scanSize, weight, n , "CA_UPSWorldWideExpedited", sku) : ""));
                n += 2;
            }
            #endregion
        }
//        if (!IsSystem)
//        {
//            #region international_service_4_cost
//            if (international_service_4_cost > 0M)
//            {
//                sb.Append(string.Format(@"
//              <InternationalShippingServiceOption>{4}
//                <ShippingService>{0}</ShippingService>
//                <ShippingServicePriority>{1}</ShippingServicePriority>
//                <ShippingServiceCost>{2}</ShippingServiceCost>
//                <ShippingServiceAdditionalCost>{3}</ShippingServiceAdditionalCost>
//                {5}
//              </InternationalShippingServiceOption>
//              {6}
//        "
//         , international_service_4
//         , n
//         , international_service_4_cost
//         , new eBayPriceHelper().eachAddItemShipping(international_service_4_cost)
//         , ""
//         , is_notebook && international_service_4 == "CA_UPSWorldWideExpress" ? GetNotebookShipTOCountryLess() : "<ShipToLocation>US</ShipToLocation>"
//         , is_notebook && international_service_4 == "CA_UPSWorldWideExpress" ? GetNotebookShipToCountry(scanSize, weight, n + 1, "CA_UPSWorldWideExpress", sku) : ""));
//                n += 2;
//            }
//            #endregion
//        }

        #region domestic_service_1_cost
        if (domestic_service_1_cost > 0)
        {
            if (domestic_service_1_cost > 0M
                || domestic_free_shipping)
            {
                sb.Append(string.Format(@"
      <ShippingServiceOptions>
        <FreeShipping>{0}</FreeShipping>
        <ShippingService>{1}</ShippingService>
        <ShippingServicePriority>{2}</ShippingServicePriority>
        <ShippingServiceCost>{3}</ShippingServiceCost>
        <ShippingServiceAdditionalCost>{4}</ShippingServiceAdditionalCost>
      </ShippingServiceOptions>
", domestic_free_shipping
     , domestic_service_1
     , n
     , domestic_service_1_cost
     , new eBayPriceHelper().eachAddItemShipping(domestic_service_1_cost)));
                n += 1;
            }
        }
        #endregion

        sb.Append("<ShippingType>Flat</ShippingType>");
        return sb.ToString();
    }

    /// <summary>
    /// 低
    /// </summary>
    /// <returns></returns>
    public static string GetNotebookShipTOCountryLess()
    {
        string countrys = "";
        countrys += "<ShipToLocation>CA</ShipToLocation>";
        countrys += "<ShipToLocation>US</ShipToLocation>";
        // countrys += "<ShipToLocation>Europe</ShipToLocation>";
       
        countrys += "<ShipToLocation>US</ShipToLocation>";
        countrys += "<ShipToLocation>UK</ShipToLocation>";
        countrys += "<ShipToLocation>ES</ShipToLocation>";
        countrys += "<ShipToLocation>IE</ShipToLocation>";
        countrys += "<ShipToLocation>HK</ShipToLocation>";
        countrys += "<ShipToLocation>JP</ShipToLocation>";
        countrys += "<ShipToLocation>KR</ShipToLocation>";
        countrys += "<ShipToLocation>SG</ShipToLocation>";
        countrys += "<ShipToLocation>FR</ShipToLocation>";
        countrys += "<ShipToLocation>DE</ShipToLocation>";
        countrys += "<ShipToLocation>IT</ShipToLocation>";
        countrys += "<ShipToLocation>LU</ShipToLocation>";
        countrys += "<ShipToLocation>MO</ShipToLocation>";
        countrys += "<ShipToLocation>NL</ShipToLocation>";
        countrys += "<ShipToLocation>TW</ShipToLocation>";
        countrys += "<ShipToLocation>AT</ShipToLocation>";
        countrys += "<ShipToLocation>BE</ShipToLocation>";
        countrys += "<ShipToLocation>Asia</ShipToLocation>";
        //countrys += "<ExcludeShipToLocation>CN</ExcludeShipToLocation>";
        return countrys;
    }

    public static string GetNotebookShipTOCountryHight()
    {
        string countrys = "";
        countrys += "<ShipToLocation>Europe</ShipToLocation>";
        countrys += "<ShipToLocation>Asia</ShipToLocation>";
        countrys += "<ShipToLocation>CN</ShipToLocation>";
        countrys += "<ShipToLocation>MY</ShipToLocation>";
        countrys += "<ShipToLocation>TH</ShipToLocation>";
        countrys += "<ShipToLocation>AU</ShipToLocation>";
        countrys += "<ShipToLocation>DK</ShipToLocation>";
        countrys += "<ShipToLocation>FI</ShipToLocation>";
        countrys += "<ShipToLocation>GR</ShipToLocation>";
        countrys += "<ShipToLocation>NO</ShipToLocation>";
        countrys += "<ShipToLocation>SE</ShipToLocation>";
        return countrys;
    }

    /// <summary>
    ///  notebook worldExpedited shipping fee.
    /// </summary>
    /// <param name="scanSize"></param>
    /// <param name="weight"></param>
    /// <param name="priority"></param>
    /// <returns></returns>
    public static string GetNotebookShipToCountry(nicklu2Entities context, decimal scanSize, decimal weight, int priority, string shippingCompany, int sku)
    {
        var pm = ProductModel.GetProductModel(context, sku);
        decimal minShipping = GetEbayPrice.GetPartMinShippingFee(context, pm);

        decimal shippFee = shippingCompany.Equals("CA_UPSWorldWideExpress") ? UPS_Express_ShippingFeeHeight(scanSize) : UPS_WorldExpedited_ShippingFee_Height(scanSize);
        shippFee -= minShipping;
        decimal additionCode = shippFee * 0.7M;

        return string.Format(@"
<InternationalShippingServiceOption>
     <ShippingService>{0}</ShippingService>
     <ShippingServicePriority>{1}</ShippingServicePriority>
     <ShippingServiceCost>{2}</ShippingServiceCost>
     <ShippingServiceAdditionalCost>{3}</ShippingServiceAdditionalCost>
     {4}
</InternationalShippingServiceOption>
"
            , shippingCompany
            , priority
            , shippFee
            , additionCode
            , GetNotebookShipTOCountryHight());
    }

    /// <summary>
    /// part shipping fee .
    /// </summary>
    /// <param name="pm"></param>
    /// <returns></returns>
    public static string GetPartShippingFeeString(nicklu2Entities context, tb_product pm)
    {
        if (pm.menu_child_serial_no == 350 || pm.menu_child_serial_no == 358)
        {
            //
            // 笔记本先用自提，，然后创建后再用其他的地方修改运费
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(string.Format(@"
                              <ShippingServiceOptions>
                                <FreeShipping>true</FreeShipping>
                                <ShippingService>{0}</ShippingService>
                                <ShippingServicePriority>{1}</ShippingServicePriority>
                                <ShippingServiceCost>{2}</ShippingServiceCost>
                                <ShippingServiceAdditionalCost>{3}</ShippingServiceAdditionalCost>
                              </ShippingServiceOptions>
                        "
                                 , "CA_Pickup"
                                 , 0
                                 , 0
                                 , 0
                                 ));
            return sb.ToString();
        }
        else
        {
            //bool IsStandardExistTwo = false;
            bool IsExistEconomy = false;
            bool IsExistEconomyUS = false;
            bool IsExistEconomyTwo = false;
            bool IsExistEconomyTwoUS = false;
            //decimal shippFeeEconomyCA = 100M;
            //decimal shippFeeUPSStandardCA = 100m;
            //decimal shippFeeEconomyUS = 100M;
            //decimal shippFeeUPSStandardUS = 100m;

            decimal minShipping = GetEbayPrice.GetPartMinShippingFee(context, pm);

            decimal price = pm.product_current_price.Value - pm.product_current_discount.Value;

            string shipCate = "";
            DataTable shipDt = Config.ExecuteDataTable("select ShippingCategoryId from tb_part_and_shipping where sku='" + pm.product_serial_no.ToString() + "' limit 1");
            if (shipDt.Rows.Count == 1)
            {
                shipCate = shipDt.Rows[0][0].ToString();
            }

            var list = context.tb_ebay_shipping_settings.Where(me=>me.CategoryID.Equals(shipCate)).ToList();// EbayShippingSettingsModel.FindAllByProperty("CategoryID", shipCate);

            IsExistEconomy = list.Count(p => p.shippingCompany.Equals("CA_EconomyShipping") ||
                p.shippingCompany.Equals("CA_StandardShipping")) > 0;

            IsExistEconomyTwo = list.Count(p => p.shippingCompany.Equals("CA_EconomyShipping")) > 0
               && list.Count(p => p.shippingCompany.Equals("CA_StandardShipping")) > 0;

            IsExistEconomyUS = list.Count(p => p.shippingCompany.Equals("CA_StandardInternational") ||
                p.shippingCompany.Equals("CA_UPSStandardUnitedStates")) > 0;

            IsExistEconomyTwoUS = list.Count(p => p.shippingCompany.Equals("CA_StandardInternational")) > 0
                && list.Count(p => p.shippingCompany.Equals("CA_UPSStandardUnitedStates")) > 0;

            if (list == null || string.IsNullOrEmpty(shipCate))
            {
                return string.Empty;
            }

            var sb = new System.Text.StringBuilder();

            for (int i = 0; i < list.Count; i++)
            {
                if ("CA_UPSExpeditedCanada" == list[i].shippingCompany ||
                    "CA_UPSExpressCanada" == list[i].shippingCompany)
                {
                    #region canada expedited, express
                    sb.Append(string.Format(@"
                              <ShippingServiceOptions>
                                {4}
                                <ShippingService>{0}</ShippingService>
                                <ShippingServicePriority>{1}</ShippingServicePriority>
                                <ShippingServiceCost>{2}</ShippingServiceCost>
                                <ShippingServiceAdditionalCost>{3}</ShippingServiceAdditionalCost>
                              </ShippingServiceOptions>
                        "
                                           , list[i].shippingCompany
                                           , i
                                           , list[i].IsFree.Value ? "0" : (list[i].shippingFee >= minShipping ? (list[i].shippingFee - minShipping).ToString() : list[i].shippingFee.ToString())
                                           , list[i].IsFree.Value ? "0" : new eBayPriceHelper().eachAddItemShipping(list[i].shippingFee.Value, minShipping).ToString()
                                           , list[i].IsFree.Value ? "<FreeShipping>true</FreeShipping>" : ""));
                    #endregion
                }
                else if ("CA_StandardInternational" == list[i].shippingCompany ||
                    "CA_UPSStandardUnitedStates" == list[i].shippingCompany)
                {
                    #region International & UPS Standard us

                    if (IsExistEconomyTwoUS)
                    {
                        if ((price < 80 && "CA_StandardInternational" == list[i].shippingCompany))
                        {
                            sb.Append(string.Format(@"
                              <InternationalShippingServiceOption>
                                {4}
                                <ShippingService>{0}</ShippingService>
                                <ShippingServicePriority>{1}</ShippingServicePriority>
                                <ShippingServiceCost>{2}</ShippingServiceCost>
                                <ShippingServiceAdditionalCost>{3}</ShippingServiceAdditionalCost>
                                <ShipToLocation>US</ShipToLocation>
                              </InternationalShippingServiceOption>"
                                          , list[i].shippingCompany
                                          , i
                                          , list[i].IsFree.Value ? "0" : (list[i].shippingFee >= minShipping ? (list[i].shippingFee - minShipping).ToString() : list[i].shippingFee.ToString())
                                          , list[i].IsFree.Value ? "0" : new eBayPriceHelper().eachAddItemShipping(list[i].shippingFee.Value, minShipping).ToString()
                                          , (list[i].IsFree.Value || (list[i].shippingFee >= minShipping ? (list[i].shippingFee - minShipping) : list[i].shippingFee) == 0M) ? "<FreeShipping>true</FreeShipping>" : ""));
                        }
                        if (price >= 80 && "CA_UPSStandardUnitedStates" == list[i].shippingCompany)
                        {
                            sb.Append(string.Format(@"
                              <InternationalShippingServiceOption>
                                {4}
                                <ShippingService>{0}</ShippingService>
                                <ShippingServicePriority>{1}</ShippingServicePriority>
                                <ShippingServiceCost>{2}</ShippingServiceCost>
                                <ShippingServiceAdditionalCost>{3}</ShippingServiceAdditionalCost>
                                <ShipToLocation>US</ShipToLocation>
                              </InternationalShippingServiceOption>"
                                            , list[i].shippingCompany
                                            , i
                                            , list[i].IsFree.Value ? "0" : (list[i].shippingFee >= minShipping ? (list[i].shippingFee - minShipping).ToString() : list[i].shippingFee.ToString())
                                            , list[i].IsFree.Value ? "0" : new eBayPriceHelper().eachAddItemShipping(list[i].shippingFee.Value, minShipping).ToString()
                                            , (list[i].IsFree.Value || (list[i].shippingFee >= minShipping ? (list[i].shippingFee - minShipping) : list[i].shippingFee) == 0M) ? "<FreeShipping>true</FreeShipping>" : ""));

                        }

                    }
                    else
                    {
                        sb.Append(string.Format(@"
                              <InternationalShippingServiceOption>
                                {4}
                                <ShippingService>{0}</ShippingService>
                                <ShippingServicePriority>{1}</ShippingServicePriority>
                                <ShippingServiceCost>{2}</ShippingServiceCost>
                                <ShippingServiceAdditionalCost>{3}</ShippingServiceAdditionalCost>
                                <ShipToLocation>US</ShipToLocation>
                              </InternationalShippingServiceOption>"
                                           , list[i].shippingCompany
                                           , i
                                           , list[i].IsFree.Value ? "0" : (list[i].shippingFee >= minShipping ? (list[i].shippingFee - minShipping).ToString() : list[i].shippingFee.ToString())
                                           , list[i].IsFree.Value ? "0" : new eBayPriceHelper().eachAddItemShipping(list[i].shippingFee.Value, minShipping).ToString()
                                           , (list[i].IsFree.Value || (list[i].shippingFee >= minShipping ? (list[i].shippingFee - minShipping) : list[i].shippingFee) == 0M) ? "<FreeShipping>true</FreeShipping>" : ""));
                    }
                    #endregion
                }
                else if (list[i].shippingCompany.ToLower().IndexOf("unitedstates") > -1) // 需要先排除ups standard us, standard intl's 
                {
                    #region United states
                    sb.Append(string.Format(@"
                              <InternationalShippingServiceOption>
                                {4}
                                <ShippingService>{0}</ShippingService>
                                <ShippingServicePriority>{1}</ShippingServicePriority>
                                <ShippingServiceCost>{2}</ShippingServiceCost>
                                <ShippingServiceAdditionalCost>{3}</ShippingServiceAdditionalCost>
                                <ShipToLocation>US</ShipToLocation>
                              </InternationalShippingServiceOption>
                        "
                                  , list[i].shippingCompany
                                  , i
                                  , list[i].IsFree.Value ? "0" : (list[i].shippingFee >= minShipping ? (list[i].shippingFee - minShipping).ToString() : list[i].shippingFee.ToString())
                                  , list[i].IsFree.Value ? "0" : new eBayPriceHelper().eachAddItemShipping(list[i].shippingFee.Value, minShipping).ToString()
                                  , (list[i].IsFree.Value || (list[i].shippingFee >= minShipping ? (list[i].shippingFee - minShipping) : list[i].shippingFee) == 0M) ? "<FreeShipping>true</FreeShipping>" : ""));

                    #endregion
                }
                else if (list[i].shippingCompany.ToLower().IndexOf("world") > -1 ||
                    "CA_ExpeditedInternational" == list[i].shippingCompany) //|| "CA_StandardInternational" == list[i].shippingCompany
                {
                    #region World
                    sb.Append(string.Format(@"
                              <InternationalShippingServiceOption>
                                {4}
                                <ShippingService>{0}</ShippingService>
                                <ShippingServicePriority>{1}</ShippingServicePriority>
                                <ShippingServiceCost>{2}</ShippingServiceCost>
                                <ShippingServiceAdditionalCost>{3}</ShippingServiceAdditionalCost>
                                {5}
                              </InternationalShippingServiceOption>
                        "
              , list[i].shippingCompany
              , i
              , list[i].IsFree.Value ? "0" : (list[i].shippingFee >= minShipping ? (list[i].shippingFee - minShipping).ToString() : list[i].shippingFee.ToString())
              , list[i].IsFree.Value ? "0" : new eBayPriceHelper().eachAddItemShipping(list[i].shippingFee.Value, minShipping).ToString()
              , list[i].IsFree.Value ? "<FreeShipping>true</FreeShipping>" : ""
              , GetNotebookShipTOCountryLess()));

                    #endregion
                }
                else if ("CA_StandardShipping" == list[i].shippingCompany ||
                    "CA_EconomyShipping" == list[i].shippingCompany)
                {
                    #region standard
                    if (IsExistEconomyTwo)
                    {
                        if ((price < 80 && "CA_EconomyShipping" == list[i].shippingCompany) ||
                            (price >= 80 && "CA_StandardShipping" == list[i].shippingCompany))
                        {
                            sb.Append(string.Format(@"
                              <ShippingServiceOptions>
                                {4}
                                <ShippingService>{0}</ShippingService>
                                <ShippingServicePriority>{1}</ShippingServicePriority>
                                <ShippingServiceCost>{2}</ShippingServiceCost>
                                <ShippingServiceAdditionalCost>{3}</ShippingServiceAdditionalCost>
                              </ShippingServiceOptions>
                        "
                                     , list[i].shippingCompany
                                     , i
                                     , list[i].IsFree.Value ? "0" : (list[i].shippingFee >= minShipping ? (list[i].shippingFee - minShipping).ToString() : list[i].shippingFee.ToString())
                                     , list[i].IsFree.Value ? "0" : new eBayPriceHelper().eachAddItemShipping(list[i].shippingFee.Value, minShipping).ToString()
                                    , (list[i].IsFree.Value || (list[i].shippingFee >= minShipping ? (list[i].shippingFee - minShipping) : list[i].shippingFee) == 0M) ? "<FreeShipping>true</FreeShipping>" : ""));
                        }
                    }
                    else
                    {
                        sb.Append(string.Format(@"
                              <ShippingServiceOptions>
                                {4}
                                <ShippingService>{0}</ShippingService>
                                <ShippingServicePriority>{1}</ShippingServicePriority>
                                <ShippingServiceCost>{2}</ShippingServiceCost>
                                <ShippingServiceAdditionalCost>{3}</ShippingServiceAdditionalCost>
                              </ShippingServiceOptions>
                        "
                                                             , list[i].shippingCompany
                                                             , i
                                                             , list[i].IsFree.Value ? "0" : (list[i].shippingFee >= minShipping ? (list[i].shippingFee - minShipping).ToString() : list[i].shippingFee.ToString())
                                                             , list[i].IsFree.Value ? "0" : new eBayPriceHelper().eachAddItemShipping(list[i].shippingFee.Value, minShipping).ToString()
                                                             , (list[i].IsFree.Value || (list[i].shippingFee >= minShipping ? (list[i].shippingFee - minShipping) : list[i].shippingFee) == 0M) ? "<FreeShipping>true</FreeShipping>" : ""));
                    }
                    #endregion
                }
                else if ("CA_Pickup" == list[i].shippingCompany)
                {
                    #region pickup
                    sb.Append(string.Format(@"
                              <ShippingServiceOptions>
                                {4}
                                <ShippingService>{0}</ShippingService>
                                <ShippingServicePriority>{1}</ShippingServicePriority>
                                <ShippingServiceCost>{2}</ShippingServiceCost>
                                <ShippingServiceAdditionalCost>{3}</ShippingServiceAdditionalCost>
                              </ShippingServiceOptions>
                        "
                                            , list[i].shippingCompany
                                            , i
                                            , list[i].IsFree.Value ? "0" : (list[i].shippingFee >= minShipping ? (list[i].shippingFee - minShipping).ToString() : list[i].shippingFee.ToString())
                                            , list[i].IsFree.Value ? "0" : new eBayPriceHelper().eachAddItemShipping(list[i].shippingFee.Value, minShipping).ToString()
                                            , list[i].IsFree.Value ? "<FreeShipping>true</FreeShipping>" : ""));
                    #endregion
                }
                else
                {
                    //                    #region Other
                    //                    if (list[i].shippingCompany.Trim().Length > 5)
                    //                    {
                    //                        sb.Append(string.Format(@"
                    //                              <ShippingServiceOptions>
                    //                                {4}
                    //                                <ShippingService>{0}</ShippingService>
                    //                                <ShippingServicePriority>{1}</ShippingServicePriority>
                    //                                <ShippingServiceCost>{2}</ShippingServiceCost>
                    //                                <ShippingServiceAdditionalCost>{3}</ShippingServiceAdditionalCost>
                    //                              </ShippingServiceOptions>
                    //                        "
                    //                                     , list[i].shippingCompany
                    //                                     , i
                    //                                     , list[i].IsFree ? "0" : (list[i].shippingFee >= minShipping ? (list[i].shippingFee - minShipping).ToString() : list[i].shippingFee.ToString())
                    //                                     , list[i].IsFree ? "0" : new eBayPriceHelper().eachAddItemShipping(list[i].shippingFee, minShipping).ToString()
                    //                                     , list[i].IsFree ? "<FreeShipping>true</FreeShipping>" : ""));
                    //                    }
                    //                    #endregion
                }
            }
            sb.Append("<ShippingType>Flat</ShippingType>");
            return sb.ToString();
        }
    }
}
