using System;
using System.Xml;
using System.Data;
using System.Text.RegularExpressions;

public partial class Q_Admin_ebayMaster_Online_ModifyOnlinePrice : PageBase
{
    /// <summary>
    /// Part Parice: /q_admin/ebayMaster/Online/ModifyOnlinePrice.aspx?Price=100.00&IsDesc=0&onlyprice=1&itemid=************&issystem=0
    /// Part Desc  : /q_admin/ebayMaster/Online/ModifyOnlinePrice.aspx?Price=100.00&IsDesc=1&onlyprice=0&itemid=************&issystem=0
    /// Part       : /q_admin/ebayMaster/Online/ModifyOnlinePrice.aspx?Price=100.00&IsDesc=0&onlyprice=0&itemid=************&issystem=0
    /// sys  Parice: /q_admin/ebayMaster/Online/ModifyOnlinePrice.aspx?Price=100.00&IsDesc=0&onlyprice=1&itemid=************&issystem=1
    /// sys  Desc  : /q_admin/ebayMaster/Online/ModifyOnlinePrice.aspx?Price=100.00&IsDesc=1&onlyprice=0&itemid=************&issystem=1
    /// sys        : /q_admin/ebayMaster/Online/ModifyOnlinePrice.aspx?Price=100.00&IsDesc=0&onlyprice=0&itemid=************&issystem=1
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //GetItem("221017932328", false);

            InitialDatabase();
        }
    }

    /// <summary>
    /// 修改价格与logo, 如果是系统 ，还修改ebay category
    /// </summary>
    void ChangePrice()
    {
        string result = "";

        if (ReqPrice == 0M && string.IsNullOrEmpty(ReqLogoPrictureUrl))
        {
            Response.Write("error: price is 0M.");
            return;
        }

        bool isSys = ReqIsSystem;
        int sku = SKU;
        string itemSpecificsString = null;

        DataTable sDT = Config.ExecuteDataTable("Select luc_sku, sys_sku from tb_ebay_selling where itemid='" + ReqItemID + "'");
        if (sDT.Rows.Count == 0)
        {
            DataTable dt = Config.ExecuteDataTable(string.Format("select * from tb_ebay_code_and_luc_sku where ebay_code ='{0}' order by id desc limit 0,1", ReqItemID));
            if (dt.Rows.Count == 1)
            {
                //isSys = dt.Rows[0]["is_sys"].ToString() == "1";
                sku = int.Parse(dt.Rows[0]["SKU"].ToString());
            }
        }
        else
        {
            if (ReqIsSystem)
            {
                sku = int.Parse(sDT.Rows[0]["sys_sku"].ToString());

            }
            else
            {
                sku = int.Parse(sDT.Rows[0]["luc_sku"].ToString());

            }
        }

        SKU = sku;

        if (ReqIsSystem)
        {
            EbayItem item = new EbayItem();
            item.item_specifics = GetItemSpecifics.GetSystemSpecifics(SKU);
            itemSpecificsString = item.ItemSpecificsString();
        }
        else
        {
            EbayItem item = new EbayItem();
            item.item_specifics = GetItemSpecifics.GetPartSpecifics(ProductModel.GetProductModel(SKU));
            itemSpecificsString = item.ItemSpecificsString();
        }
        //GetItem(ItemID, isSys);


        if (!string.IsNullOrEmpty(ReqLogoPrictureUrl))
        {

            if (ReqIsSystem)
            {

                DataTable dt = Config.ExecuteDataTable("select sp.luc_sku from tb_ebay_system_parts sp inner join tb_ebay_system_part_comment spc on spc.id=sp.comment_id where system_sku='" + sku + "' and spc.e_field_name='case'");
                DataTable dtTitle = Config.ExecuteDataTable("select system_title1 from tb_ebay_system where id='" + sku + "'");
                if (dt.Rows.Count > 0)
                {

                    string logoPrictureFilename = "http://www.lucomputers.com/pro_img/components/" + dt.Rows[0][0].ToString() + "_g_1.jpg";
                    //string tempFile = "http://www.lucomputers.com/ebay/temp.jpg";
                    //result = eBayCmdReviseItem.ReviseItem(ReqItemID, isSys, null, 0M, sku, null, tempFile, null, null, eBayModifyType.logo);

                    string sysEbayCategory = eBaySysCategory.GetSysOnEbayCategory(dtTitle.Rows[0][0].ToString());

                    // 如果是修改logo，　description　就是 ebay  上的类 
                    result = eBayCmdReviseItem.ReviseItem(ReqItemID
                        , isSys
                        , sysEbayCategory
                        , 0M
                        , sku
                        , null
                        , logoPrictureFilename
                        , itemSpecificsString
                        , dtTitle.Rows[0][0].ToString()
                        , eBayModifyType.logo);
                }

                Response.Write("<script>this.close();</script>");
                return;
            }
            else
            {
                // 临时使用， 超过这个是2012-05 发后的产品
                //if (SKU > 22000)
                {
                    DataTable dt = Config.ExecuteDataTable("select case when other_product_sku > 0 then other_product_sku else product_serial_no end product_serial_no, product_short_name_f, product_ebay_name from tb_product where product_serial_no='" + SKU + "'");
                    string logoPrictureFilename = "http://www.lucomputers.com/pro_img/components/" + dt.Rows[0][0].ToString() + "_g_1.jpg";

                    if (dt.Rows[0]["product_short_name_f"].ToString().Length > 5)
                    {
                        logoPrictureFilename = "http://www.lucomputers.com/pro_img/components/" + dt.Rows[0]["product_short_name_f"].ToString();
                    }
                    //string tempFile = "http://www.lucomputers.com/ebay/temp.jpg";
                    // result = eBayCmdReviseItem.ReviseItem(ReqItemID, false, null, 0M, sku, null, tempFile, null, null, eBayModifyType.logo);

                    result = eBayCmdReviseItem.ReviseItem(ReqItemID
                        , false
                        , null
                        , 0M
                        , sku
                        , null
                        , logoPrictureFilename
                        , itemSpecificsString
                        , dt.Rows[0]["product_ebay_name"].ToString()
                        , eBayModifyType.logo);
                }
                Response.Write("<script>this.close();</script>");
                return;
            }
        }


        if (ReqIsDescription)
        {
            if (sku < 1)
            {
                Response.Write("SKU is ERROR.");
                Response.End();
            }
            // only description
            result = eBayCmdReviseItem.ReviseItem(ReqItemID, isSys, GetDescript(sku, isSys), 0M, sku, null, null, itemSpecificsString, null, eBayModifyType.description);
        }
        else if (ReqOnlyPrice)
        {   // only price
            result = eBayCmdReviseItem.ReviseItem(ReqItemID, isSys, null, ReqPrice, sku, null, null, itemSpecificsString, null, eBayModifyType.price);
        }
        else
        {
            // all
            result = eBayCmdReviseItem.ReviseItem(ReqItemID, isSys, GetDescript(sku, isSys), ReqPrice, sku, null, null, itemSpecificsString, null, eBayModifyType.priceAndDesc);
        }


        if (result.Length == 12)
        {

            Response.Write(sku.ToString() + "<br>");
            Response.Write(result);
            if (isSys)
                Response.Write("<script>window.open('/q_admin/ebaymaster/Online/get_system_configuration.aspx?cmd=GenerateXmlFile&IsClose=true&Version=3&system_sku=" + sku + "', 'generateXml" + sku + "','height=100, width=400');this.close();</script>");
            else
                Response.Write("<script>this.close();</script>");
        }
        else
        {
            Response.Write(sku.ToString() + "<br>");
            Response.Write(result);
            if (result.IndexOf("21915465") > -1)
            {
                if (isSys)
                    Response.Write("<script>window.open('/q_admin/ebaymaster/Online/get_system_configuration.aspx?cmd=GenerateXmlFile&IsClose=true&Version=3&system_sku=" + sku + "', 'generateXml" + sku + "','height=100, width=400');this.close();</script>");
                else
                    Response.Write("<script>this.close();</script>");
            }
        }

    }

    void ModifyItemSpecificsMPN(string itemID)
    {
        string itemSpecificString = GetItemSpecificsString(GetItem(ReqItemID, false), ReqItemID);
        string result = eBayCmdReviseItem.ReviseItem(ReqItemID, false, null, ReqPrice, 0, null, null, itemSpecificString, null, eBayModifyType.itemSpecific);
    }

    void SaveHistory()
    {
        if (ReqIsSystem)
        {
            //
            // Save system price history records.

            eBaySystemWorker.SaveHistory(ReqItemID, SKU, false);
        }
        else
        {
            //
            // Save parts price history records.
            ProductModel pm = ProductModel.GetProductModel(SKU);

            eBayPartWorker.SaveHistory(SKU
                , ReqCost
                , ""
                , ""
                , ReqeBayFee
                , ReqItemID
                , ReqPrice
                , pm.manufacturer_part_number
                , ReqProfit
                , ReqShippingFee
                , pm.product_current_price - pm.product_current_discount
                );

        }
    }
    /// <summary>
    /// get eBay description
    /// </summary>
    /// <param name="sku"></param>
    /// <param name="isSys"></param>
    /// <returns></returns>
    string GetDescript(int sku, bool isSys)
    {
        string desc = "";
        if (!isSys)
        {
            desc = new eBayPageHelper(this).GetPageString(sku);
        }
        else
        //return new eBaySysDescription().GetDescription(sku, true, this.Server); 
        {
            EbaySystemModel esm = EbaySystemModel.GetEbaySystemModel(sku);

            desc =
            new EbayPageText().GetEbayPageHtml(sku
               , eBayProdType.system
               , false
               , eBaySystemWorker.GetFlashType(esm));
        }

        string re = @"[^\x09\x0A\x0D\x20-\xD7FF\xE000-\xFFFD\x10000-x10FFFF]";
        return Regex.Replace(desc, re, "");

    }



    /// <summary>
    /// 
    /// </summary>
    /// <param name="eBayItemString"></param>
    /// <returns></returns>
    string GetItemSpecificsString(string eBayItemString, string itemid)
    {
        if (eBayItemString.IndexOf("ItemSpecifics") == -1)
            return null;

        XmlDocument doc = new XmlDocument();
        doc.LoadXml(eBayItemString);

        string str = doc["GetItemResponse"]["Item"]["ItemSpecifics"].InnerXml;
        if (str.IndexOf("MPN") == -1)
        {
            string typestr = "";
            string mfp = EbayCodeAndLucSkuModel.GetMFP(itemid, ref typestr);
            if (!string.IsNullOrEmpty(mfp))
                str += string.Format("<NameValueList><Name>MPN</Name><Value>{0}</Value><Source>ItemSpecific</Source></NameValueList>",
                    mfp);
            if (str.IndexOf("<Name>Type</Name>") == -1)
            {
                str += string.Format("<NameValueList><Name>Type</Name><Value>{0}</Value><Source>ItemSpecific</Source></NameValueList>",
                   typestr);
            }
        }
        return string.Format("<ItemSpecifics>{0}</ItemSpecifics>", str);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="itemId"></param>
    /// <param name="isSys"></param>
    string GetItem(string itemId, bool isSys)
    {
        return EbayItemGenerate.GetItem(itemId, "<IncludeItemSpecifics>true</IncludeItemSpecifics>", SKU, isSys);
    }

    #region Properties
    bool ReqOnlyPrice
    {
        get { return Util.GetStringSafeFromQueryString(Page, "onlyprice") == "1"; }
    }

    public bool ReqIsSystem
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "issystem", -1) == 1; }
    }

    /// <summary>
    /// ebay logo
    /// </summary>
    string ReqLogoPrictureUrl
    {
        get { return Util.GetStringSafeFromQueryString(Page, "LogoPrictureUrl"); }
    }

    //int SKU
    //{
    //    get { return Util.GetInt32SafeFromQueryString(Page, "sku", -1); }
    //}

    int SKU { get; set; }

    string ReqCmd
    {
        get { return Util.GetStringSafeFromQueryString(Page, "cmd"); }
    }

    string ReqItemID
    {
        get { return Util.GetStringSafeFromQueryString(Page, "itemid"); }
    }

    decimal ReqPrice
    {
        get { return Util.GetDecimalSafeFromQueryString(Page, "price", 0M); }
    }

    decimal ReqCost
    {
        get { return Util.GetDecimalSafeFromQueryString(Page, "Cost", 0M); }
    }

    decimal ReqProfit
    {
        get { return Util.GetDecimalSafeFromQueryString(Page, "Profit", 0M); }
    }

    decimal ReqeBayFee
    {
        get { return Util.GetDecimalSafeFromQueryString(Page, "eBayFee", 0M); }
    }

    decimal ReqShippingFee
    {
        get { return Util.GetDecimalSafeFromQueryString(Page, "ShippingFee", 0M); }
    }

    bool ReqIsDescription
    {
        get { return Util.GetStringSafeFromQueryString(Page, "IsDesc") == "1"; }
    }

    #endregion

    public override void InitialDatabase()
    {
        base.InitialDatabase();

        if (ReqCmd == "modifyItemSpecific")
        {
            ModifyItemSpecificsMPN(ReqItemID);
            Response.Write("<script>this.close();</script>");
        }
        else
        {
            ChangePrice();

            if (!ReqIsDescription)
            {
                // Update flash Price

                if (ReqIsSystem && string.IsNullOrEmpty(ReqLogoPrictureUrl))
                {
                    decimal cost = 0M;
                    decimal ebayFee = 0M;
                    decimal ebayShippingFee = 0M;
                    decimal profit = 0M;

                    cost = GetEbayPrice.GetEbaySysCost(SKU);

                    EbaySystemModel esm = EbaySystemModel.GetEbaySystemModel(SKU);
                    decimal all_ebay_price = GetEbayPrice.GetEbaySysPrice(cost
                    , esm.adjustment
                    , ref profit
                    , ref ebayFee
                    , ref ebayShippingFee
                    , true);


                    eBaySystemWorker.UpdateFlashPrice(esm
                        , ReqPrice
                        , esm.parentID > 0 && esm.is_shrink && esm.is_from_ebay
                        , cost
                        , ebayFee
                        , ebayShippingFee
                        , profit);
                }
                //
                SaveHistory();
            }
        }
    }
}
