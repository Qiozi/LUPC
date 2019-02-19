using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;

/// <summary>
/// Summary description for EbayItem
/// </summary>
public class EbayItem
{
    public EbayItem()
    {
        //
        // TODO: Add constructor logic here
        //

    }

    public string Upc { get; set; }

    string _payPalEmailAddress = "qiozi4_1265526925_biz@163.com";

    public string PayPalEmailAddress
    {
        get
        {
            if (Config.http_domain.ToLower().IndexOf("lucomputer") != -1)
                return "paypal@lucomputers.com";
            else
                return _payPalEmailAddress;
        }
        set { _payPalEmailAddress = value; }
    }


    public string Duration { set; get; }
    string _paymentMethods = "PayPal";

    public string PaymentMethods
    {
        get { return _paymentMethods; }
        set { _paymentMethods = value; }
    }

    string _cutom_label = "";
    public string cutom_label
    {
        get { return _cutom_label; }
        set { _cutom_label = value; }
    }
    string _title = "";

    public string Title
    {
        get { return _title; }
        set { _title = value; }
    }
    string _subtitle = "";

    public string Subtitle
    {
        get { return _subtitle; }
        set { _subtitle = value; }
    }
    string _category;

    public string Category
    {
        get { return _category; }
        set { _category = value; }
    }
    string _store_category = "";

    public string Store_category
    {
        get { return _store_category; }
        set { _store_category = value; }
    }
    string _store_category2 = "";

    public string Store_category2
    {
        get { return _store_category2; }
        set { _store_category2 = value; }
    }

    string _pictures_url1 = "";

    public string Pictures_url1
    {
        get { return _pictures_url1; }
        set { _pictures_url1 = value; }
    }
    string _pictures_url2 = "";

    public string Pictures_url2
    {
        get { return _pictures_url2; }
        set { _pictures_url2 = value; }
    }
    string _pictures_url3 = "";

    public string Pictures_url3
    {
        get { return _pictures_url3; }
        set { _pictures_url3 = value; }
    }

    string _description = "";

    public string Description
    {
        get
        {
            if (_description.Length > 10) { 
                _description
                    .Replace("<a ", "<luComputer ")
                    .Replace("</a>", "</luComputer>")
                    .Replace("<A ", "<luComputer ")
                    .Replace("</A>", "</luComputer>")
                    .Replace(" onclick", " lucomputer")
                    .Replace("window.open", "lucomputer");
            }
            return _description;
        }
        set { _description = value; }
    }

    SellingFormat _selling_format = SellingFormat.fixed_price;

    public SellingFormat Selling_format
    {
        get { return _selling_format; }
        set { _selling_format = value; }
    }
    decimal _buy_it_now_price = 10000;

    public decimal Buy_it_now_price
    {
        get { return _buy_it_now_price; }
        set { _buy_it_now_price = value; }
    }
    bool _private_Listiing = false;

    public bool Private_Listiing
    {
        get { return _private_Listiing; }
        set { _private_Listiing = value; }
    }

    int _domestic_services1 = -1;

    public int Domestic_services1
    {
        get { return _domestic_services1; }
        set { _domestic_services1 = value; }
    }
    decimal _domestic_cost1 = 10000M;

    public decimal Domestic_cost1
    {
        get { return _domestic_cost1; }
        set { _domestic_cost1 = value; }
    }

    int _domestic_services2 = -1;

    public int Domestic_services2
    {
        get { return _domestic_services2; }
        set { _domestic_services2 = value; }
    }
    decimal _domestic_cost2 = 10000M;

    public decimal Domestic_cost2
    {
        get { return _domestic_cost2; }
        set { _domestic_cost2 = value; }
    }

    int _domestic_services3 = -1;

    public int Domestic_services3
    {
        get { return _domestic_services3; }
        set { _domestic_services3 = value; }
    }
    decimal _domestic_cost3 = 10000M;

    public decimal Domestic_cost3
    {
        get { return _domestic_cost3; }
        set { _domestic_cost3 = value; }
    }

    bool _local_pickup = false;

    public bool Local_pickup
    {
        get { return _local_pickup; }
        set { _local_pickup = value; }
    }

    int _domestic_handling_time = 3; //days

    public int Domestic_handling_time
    {
        get { return _domestic_handling_time; }
        set { _domestic_handling_time = value; }
    }

    int _international_services1 = -1;

    public int International_services1
    {
        get { return _international_services1; }
        set { _international_services1 = value; }
    }
    decimal _international_cost1 = 10000M;

    public decimal International_cost1
    {
        get { return _international_cost1; }
        set { _international_cost1 = value; }
    }

    int _international_services2 = -1;

    public int International_services2
    {
        get { return _international_services2; }
        set { _international_services2 = value; }
    }
    decimal _international_cost2 = 10000M;

    public decimal International_cost2
    {
        get { return _international_cost2; }
        set { _international_cost2 = value; }
    }

    int _international_services3 = -1;

    public int International_services3
    {
        get { return _international_services3; }
        set { _international_services3 = value; }
    }
    decimal _international_cost3 = 10000M;

    public decimal International_cost3
    {
        get { return _international_cost3; }
        set { _international_cost3 = value; }
    }

    int _quantity = 1;

    public int Quantity
    {
        get { return _quantity; }
        set { _quantity = value; }
    }

    public bool? AutoPay { get; set; }


    AttrSubItem[] _attribates;
    public AttrSubItem[] Attribates
    {
        get
        {
            return _attribates;
        }
        set
        {
            _attribates = value;
        }
    }

    #region Item Specifics
    List<KeyValuePair<string, string>> _item_specifics = new List<KeyValuePair<string, string>>();
    string _item_specifics_label = "";
    public List<KeyValuePair<string, string>> item_specifics
    {
        get { return _item_specifics; }
        set { _item_specifics = value; }
    }

    public string item_specifics_label
    {
        get { return _item_specifics_label; }
        set { _item_specifics_label = value; }
    }

    public string ItemSpecificsString()
    {
        if (item_specifics.Count > 1)
        {
            string str = "<ItemSpecifics>";
            foreach (var k in item_specifics)
            {
                str += string.Format(@"
        <NameValueList>
            <Name>{0}</Name>
            <Value><![CDATA[{1}]]></Value>
        </NameValueList>
  ", k.Key, k.Value);
            }
            str += "</ItemSpecifics>";
            return str;
        }
        else
            return "";
    }
    #endregion

    //string _attribatesXml = "";
    /// <summary>
    /// <AttributeSetArray>
    ///  <AttributeSet attributeSetID=""1857"" attributeSetVersion=""98186""> 
    ///    <Attribute attributeID=""26443"">
    ///       <Value>
    ///         <ValueID>49920</ValueID>
    ///          <ValueLiteral>attr1858_26443</ValueLiteral>
    ///       </Value>
    ///       </Attribute>
    ///     </AttributeSet>
    ///    </AttributeSetArray>
    /// </summary>
    public string AttriatesXml(int attributeSetID, int attributeSetVersion)
    {

        if (attributeSetID < 1)
            return "";
        if (Attribates.Length > 0)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<AttributeSetArray>");
            sb.Append(string.Format(@"<AttributeSet attributeSetID=""{0}"" attributeSetVersion=""{1}""> "
                , attributeSetID
                , attributeSetVersion));

            //throw new Exception(Attribates.Length.ToString());
            for (int i = 0; i < Attribates.Length; i++)
            {
                if (Attribates[i] != null)
                {
                    if ("attr1858_26444" == Attribates[i].attributeID)
                        continue;
                    if (Attribates[i].valueID.IndexOf(",") == -1)
                    {
                        sb.Append(string.Format(@"
<Attribute attributeID=""{0}"">
    <Value>
        <ValueID>{1}</ValueID>
        <ValueLiteral>{2}</ValueLiteral>
    </Value>
</Attribute>
"
                            , Attribates[i].attributeID
                            , Attribates[i].valueID
                            , Attribates[i].valueLiteral));
                        //if (i == 5)
                        //    break;
                    }
                    else
                    {
                        string[] vs = Attribates[i].valueID.Split(new char[] { ',' });
                        string vsString = "";
                        for (int j = 0; j < vs.Length; j++)
                        {
                            vsString += "<Value><ValueID>" + vs[j].Trim() + "</ValueID><ValueLiteral></ValueLiteral></Value>";
                        }

                        sb.Append(string.Format(@"
<Attribute attributeID=""{0}"">    
        {1}    
</Attribute>
"
                           , Attribates[i].attributeID
                           , vsString
                           , Attribates[i].valueLiteral));
                    }
                }
            }
            sb.Append("</AttributeSet></AttributeSetArray>");
            return sb.ToString();
        }
        return "";

    }

    public string GetPrictureDetail(EbayItem ei)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        if (ei.Pictures_url1.Length > 10)
            sb.Append(string.Format("<PictureURL>{0}</PictureURL>", ei.Pictures_url1.Replace("http://","https://")));

        if (ei.Pictures_url2.Length > 10)
            sb.Append(string.Format("<PictureURL>{0}</PictureURL>", ei.Pictures_url2.Replace("http://", "https://")));

        if (ei.Pictures_url3.Length > 10)
            sb.Append(string.Format("<PictureURL>{0}</PictureURL>", ei.Pictures_url3.Replace("http://", "https://")));

        return sb.ToString();
    }
    /// <summary>
    /// Get Flash String
    /// </summary>
    /// <param name="system_sku"></param>
    /// <param name="flash_templete_area"></param>
    /// <param name="partCount"></param>
    /// <returns></returns>
    public string GetFlashString(string system_sku
        , string flash_templete_area
        , int partCount
        , bool is_shrink)
    {
        FileHelper fh = new FileHelper();

        string s = flash_templete_area;//

        if (s.IndexOf("[system_sku]") != -1)
        {
            string height = "";

            {
                if (partCount == 20)
                    height = " height=\"850\" ";
                else if (partCount == 10)
                    height = " height=\"520\" ";
                else if (partCount == 15)
                    height = " height=\"660\" ";

                else if (partCount == 16)
                    height = " height=\"710\" ";
                else if (partCount == 17)
                    height = " height=\"750\" ";
                else if (partCount == 18)
                    height = " height=\"760\" ";
                else if (partCount == 19)
                    height = " height=\"800\" ";
                else if (partCount == 21)
                    height = " height=\"880\" ";
                else
                    height = "";
            }

            //if (is_shrink)
            {
                s = s.Replace("[system_sku]", system_sku + "&host=" + Config.http_domain)
                   .Replace("[lu_web_build_flash_height]", height);
                //.Replace("FlashEbayCreateSystem", "FlashEbayCreateSystemLabel" + partCount.ToString());
            }
            //else
            //{
            //    s = s.Replace("[system_sku]", system_sku + "&host=" + Config.http_domain)
            //        .Replace("[lu_web_build_flash_height]", height)
            //        .Replace("FlashEbayCreateSystem", "FlashEbayCreateSystem" + partCount.ToString());
            //}
        }
        //}

        return s;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sku"></param>
    /// <param name="PT"></param>
    /// <param name="shippingDetailString"></param>
    /// <param name="IsChild"></param>
    /// <returns></returns>
    public string GetShippingDetail(int sku
        , eBayProdType PT
        , string shippingDetailString
        , bool IsChild)
    {
        string shippDetail = "";
        eBayPriceHelper eH = new eBayPriceHelper();

        DataTable dt = Config.ExecuteDataTable(string.Format("Select ebay_system_price from tb_ebay_system where id='{0}'", sku));
        decimal price;
        if (dt.Rows.Count == 1)
            decimal.TryParse(dt.Rows[0][0].ToString(), out price);
        else
            price = 0M;

        if (price == 0M)
            throw new Exception("No Find System.");

        if (PT == eBayProdType.system) // system
        {
            decimal baseShippingFee = eBayShipping.SysShippingCa(price);
            decimal CA_UPSStandardCanada_price = 0M;// IsChild ? 0M : eBayShipping.SysShippingCa(price);
            decimal CA_UPSStandardCanada_price_add = 0M;// IsChild ? 0M : eH.eachAddItemShipping(CA_UPSStandardCanada_price);
            decimal CA_UPSExpeditedCanada_price = eBayShipping.SysShippingScLvlCA(price);// IsChild ? 0M : eBayShipping.SysShippingScLvlCA(price);
            decimal CA_UPSExpeditedCanada_price_add = IsChild ? 0M : eH.eachAddItemShipping(CA_UPSExpeditedCanada_price);

            decimal CA_UPSStandardUnitedStates_price = 0M;// IsChild ? 0M : eBayShipping.SysShippingUS(price);
            decimal CA_UPSStandardUnitedStates_price_add = 0M;// IsChild ? 0M : eH.eachAddItemShipping(CA_UPSStandardUnitedStates_price);
            decimal CA_UPS3DaySelectUnitedStates_price = eBayShipping.SysShippingScLvlUS(price);// IsChild ? 0M : eBayShipping.SysShippingScLvlUS(price);
            decimal CA_UPS3DaySelectUnitedStates_price_add = IsChild ? 0M : eH.eachAddItemShipping(CA_UPS3DaySelectUnitedStates_price);

            shippDetail += string.Format(@"
                <ShippingServiceOptions>
                        <FreeShipping>true</FreeShipping>  
                        <ShippingService>CA_Pickup</ShippingService>
                        <ShippingServicePriority>3</ShippingServicePriority>
                        <ShippingServiceCost>0</ShippingServiceCost>
                        <ShippingServiceAdditionalCost>0</ShippingServiceAdditionalCost>
                </ShippingServiceOptions>
                <ShippingServiceOptions>
                        <ShippingService>CA_UPSStandardCanada</ShippingService>
                        <ShippingServicePriority>1</ShippingServicePriority>
                        <ShippingServiceCost>{0}</ShippingServiceCost>
                        <ShippingServiceAdditionalCost>{1}</ShippingServiceAdditionalCost>
                </ShippingServiceOptions>
                <ShippingServiceOptions>
                        <ShippingService>CA_UPSExpeditedCanada</ShippingService>
                        <ShippingServicePriority>2</ShippingServicePriority>
                        <ShippingServiceCost>{2}</ShippingServiceCost>
                        <ShippingServiceAdditionalCost>{3}</ShippingServiceAdditionalCost>
                      </ShippingServiceOptions>
                <InternationalShippingServiceOption>
                        <ShippingService>CA_UPSStandardUnitedStates</ShippingService>
                        <ShippingServicePriority>0</ShippingServicePriority>
                        <ShippingServiceCost>{4}</ShippingServiceCost>
                        <ShippingServiceAdditionalCost>{5}</ShippingServiceAdditionalCost>
                        <ShipToLocation>US</ShipToLocation>
                </InternationalShippingServiceOption>
                <InternationalShippingServiceOption>
                        <ShippingService>CA_UPS3DaySelectUnitedStates</ShippingService>
                        <ShippingServicePriority>1</ShippingServicePriority>
                        <ShippingServiceCost>{6}</ShippingServiceCost>
                        <ShippingServiceAdditionalCost>{7}</ShippingServiceAdditionalCost>
                        <ShipToLocation>US</ShipToLocation>
                </InternationalShippingServiceOption>
                <ShippingType>Flat</ShippingType>
                "
                    , CA_UPSStandardCanada_price
                    , CA_UPSStandardCanada_price_add
                    , CA_UPSExpeditedCanada_price
                    , CA_UPSExpeditedCanada_price_add
                    , CA_UPSStandardUnitedStates_price
                    , CA_UPSStandardUnitedStates_price_add
                    , CA_UPS3DaySelectUnitedStates_price
                    , CA_UPS3DaySelectUnitedStates_price_add);

            //}
        }

        return shippDetail;
    }

    public void SplitAttribatesStr(string attribatesStr)
    {
        AttrSubItem[] si = new AttrSubItem[50];
        int siIndex = 0;
        if (attribatesStr.IndexOf("[Q]") != -1)
        {
            string[] attrs = attribatesStr.Split(new string[] { "[Q]" }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < attrs.Length; i++)
            {
                if (attrs[i].IndexOf('|') != -1)
                {


                    //throw new Exception(attrs[i].Split(new char[] { '|' })[0]);
                    string attributeID = attrs[i].Split(new char[] { '|' })[0].ToString();
                    //
                    // 带“T”的是文本输入框
                    if (attributeID.ToLower().IndexOf("t") != -1)
                    {
                        AttrSubItem attr = new AttrSubItem();
                        attr.attributeID = attributeID.Replace("t", "");
                        attr.valueLiteral = attrs[i].Split(new char[] { '|' })[1].ToString();
                        attr.valueID = "-6";//"-3 or -6 or -100"
                        si[siIndex] = attr;
                        siIndex += 1;
                    }
                    else
                    {

                        string valueIDS = attrs[i].Split(new char[] { '|' })[1].ToString();

                        if (valueIDS.IndexOf(",") != -1)
                        {
                            //string[] vs = valueIDS.Split(new char[] { ',' });
                            //for (int j = 0; j < vs.Length; j++)
                            //{
                            //    AttrSubItem attr = new AttrSubItem();
                            //    attr.attributeID = attributeID;
                            //    attr.valueID = vs[j].Trim();
                            //    si[siIndex] = attr;
                            //    siIndex += 1;
                            //}
                            AttrSubItem attr = new AttrSubItem();
                            attr.attributeID = attributeID;
                            attr.valueID = valueIDS;
                            si[siIndex] = attr;
                            siIndex += 1;
                        }
                        else
                        {
                            AttrSubItem attr = new AttrSubItem();
                            attr.attributeID = attributeID;
                            attr.valueID = valueIDS;
                            si[siIndex] = attr;
                            siIndex += 1;
                        }



                    }

                }
            }
        }
        else
        {
            if (attribatesStr.IndexOf('|') != -1)
            {
                AttrSubItem attr = new AttrSubItem();

                string attributeID = attribatesStr.Split(new char[] { '|' })[0].ToString();
                if (attributeID.ToLower().IndexOf("t") != -1)
                {
                    attr.attributeID = attributeID.Replace("t", "");
                    attr.valueLiteral = attribatesStr.Split(new char[] { '|' })[1].ToString();
                }
                else
                {
                    attr.attributeID = attributeID;
                    attr.valueID = attribatesStr.Split(new char[] { '|' })[1].ToString();
                }
                si[siIndex] = attr;
                siIndex += 1;
            }
        }
        Attribates = si;
    }
}

public enum SellingFormat
{
    auction,
    fixed_price,
    ebay_store
}

public class AttrSubItem
{
    public AttrSubItem() { }

    string _valueLiteral = "";
    string _valueID = "";
    string _attributeID = "";

    public string attributeID
    {
        get { return _attributeID; }
        set { _attributeID = value; }
    }

    public string valueLiteral
    {
        get { return _valueLiteral; }
        set { _valueLiteral = value; }
    }

    public string valueID
    {
        get { return _valueID; }
        set { _valueID = value; }
    }
}
