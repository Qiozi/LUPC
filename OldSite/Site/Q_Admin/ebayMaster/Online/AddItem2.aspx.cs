using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using LU.Data;

public partial class Q_Admin_ebayMaster_Online_AddItem2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (ReqSysSku > 0)
            {
                InitPage();
            }
        }
    }

    void InitPage()
    {
        Response.Clear();
        switch (ReqCmd)
        {
            case "addsys":
                AddSysToeBay();
                break;

            case "modifyItemSpecifics":
                ModifyItemSpecifics(ReqSysSku);
                break;
        }
        Response.End();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sysSku"></param>
    void ModifyItemSpecifics(int sysSku)
    {
        var context = new LU.Data.nicklu2Entities();

        var pc = EbaySystemModel.GetEbaySystemModel(context, sysSku);
        if (pc == null)
            return;

        // DataTable dt = Config.ExecuteDataTable("Select ItemSpecificsName, ItemSpecificsValue from tb_ebay_system_item_specifics where system_sku='" + sysSku + "'");

        var sysSpecifics = GetItemSpecifics.GetSystemSpecifics(sysSku);
        EbayItem ei = new EbayItem();
        if (sysSpecifics.Count > 0)
        {
            ei.item_specifics = sysSpecifics;
        }

        DataTable itemidDT = Config.ExecuteDataTable("Select ItemID, BuyItNowPrice, quantity - quantityavailable sellQty from tb_ebay_selling where sys_sku='" + sysSku + "'");
        if (itemidDT.Rows.Count == 1)
        {
            eBayCmdReviseItem.ReviseItem(context, itemidDT.Rows[0]["itemid"].ToString(), false, null, 0M, 0, null, null, ei.ItemSpecificsString(), null, string.Empty, eBayModifyType.itemSpecific);
            Response.Write("OK");
        }
        else
        {
            itemidDT = Config.ExecuteDataTable("select ebay_code itemid  from tb_ebay_code_and_luc_sku where sku='" + sysSku + "' order by id desc limit 1");
            if (itemidDT.Rows.Count > 0)
            {
                eBayCmdReviseItem.ReviseItem(context, itemidDT.Rows[0]["itemid"].ToString(), false, null, 0M, 0, null, null, ei.ItemSpecificsString(), null, string.Empty, eBayModifyType.itemSpecific);
                Response.Write("OK");
            }
            else
                Response.Write("itemid no exist.");
        }
    }

    void AddSysToeBay()
    {
        var context = new nicklu2Entities();

        var pc = EbaySystemModel.GetEbaySystemModel(context, ReqSysSku);

        if (pc == null)
            return;

        EbayItem ei = new EbayItem();
        ei.item_specifics = GetItemSpecifics.GetSystemSpecifics(ReqSysSku);

        #region sys price
        decimal all_web_price = 0M;
        decimal all_cost = 0M;
        //Response.Write(all_cost.ToString());

        decimal selected_cost = 0M;
        decimal selected_web_price = 0M;
        GetEbayPrice.GetEbaySysCost(ReqSysSku
        , ref selected_cost
        , ref selected_web_price
        , ref all_cost
        , ref all_web_price);

        var ESM = new tb_ebay_system();// EbaySystemModel();
        decimal adjustment = GetEbayPrice.GetEbaySysAdjustment(context, ReqSysSku, ref ESM);
        decimal all_profits = 0M;
        decimal all_ebay_fee = 0M;
        decimal shipping_Fee = 0M;
        decimal all_ebay_price = GetEbayPrice.GetEbaySysPrice(all_cost
            , adjustment
            , ref all_profits
            , ref all_ebay_fee
            , ref shipping_Fee
            , true);
        #endregion

        ei.Buy_it_now_price = (ConvertPrice.RoundPrice2(all_ebay_price) - 0.01M) + EbaySettings.ebayAccessoriesPrice;
        ei.AutoPay = ei.Buy_it_now_price >= 2000 ? false : true;
        ei.Category = ReqEbayStoreCateId.ToString();
        ei.cutom_label = pc.cutom_label + " " + ReqSysSku.ToString();

        #region description
        var esm = EbaySystemModel.GetEbaySystemModel(context, ReqSysSku);
        string description = new EbayPageText()
                                    .GetEbayPageHtml(context, ReqSysSku
                                                    , eBayProdType.system
                                                    , false
                                                    , eBaySystemWorker.GetFlashType(esm));
        #endregion

        ei.Description = description;

        ei.Quantity = 10;
        if (!string.IsNullOrEmpty(ReqCategory1))
            ei.Store_category = ReqCategory1.ToString();
        if (!string.IsNullOrEmpty(ReqCategory2))
            ei.Store_category2 = ReqCategory2.ToString();
        ei.Title = pc.system_title1;
        DataTable imgdt = Config.ExecuteDataTable("select sp.luc_sku from tb_ebay_system_parts sp inner join tb_ebay_system_part_comment spc on spc.id=sp.comment_id where system_sku='" + ReqSysSku + "' and spc.e_field_name='case'");
        if (imgdt.Rows.Count == 1)
        {
            string logoPrictureFilename = "https://www.lucomputers.com/pro_img/components/" + imgdt.Rows[0][0].ToString() + "_g_1.jpg";
            ei.Pictures_url1 = logoPrictureFilename;
        }
        else
            ei.Pictures_url1 = "https://www.lucomputers.com/ebay/" + pc.logo_filenames + ".jpg";

        string shippingString = eBayShipping
            .GetPartShippingFeeString(
            context,
            new tb_product() { product_serial_no = ReqSysSku },
            this.Server,
            true,
            ei.Buy_it_now_price);// 

        EbayItemGenerate eig = new EbayItemGenerate();

        string code = eig.AddItem(context, ei, shippingString, ReqSysSku, 0, true, false);
        var ecalsm = new tb_ebay_code_and_luc_sku();// EbayCodeAndLucSkuModel();
        ecalsm.ebay_code = code;
        ecalsm.is_sys = true;
        ecalsm.SKU = ReqSysSku;
        ecalsm.is_online = true;
        ecalsm.regdate = DateTime.Now;
        context.tb_ebay_code_and_luc_sku.Add(ecalsm);
        context.SaveChanges();
        Response.Clear();
        Response.Write(code);

        // 保存目录与产品
        Config.ExecuteNonQuery(@"insert into tb_ebay_category_and_product 
	(Sku, ProdType, eBayCateID_1, eBayCateText_1, eBayMyCateID_1, 
	eBayMyCateText_1, 
	regdate
	)
	values
	('" + ReqSysSku + "', 'S', '" + ReqEbayStoreCateId.ToString() + "', '', '" + ReqCategory1 + @"','',now())");
        Response.End();
    }

    #region properties

    string ReqCmd
    {
        get { return Util.GetStringSafeFromQueryString(Page, "cmd"); }
    }

    int ReqSysSku
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "system_sku", -1); }
    }

    string ReqCategory1
    {
        get { return Util.GetStringSafeFromQueryString(Page, "storeCategory1"); }
    }

    string ReqCategory2
    {
        get { return Util.GetStringSafeFromQueryString(Page, "storeCategory2"); }
    }

    int ReqEbayStoreCateId
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "eBayStoreCateId", -1); }
    }

    #endregion
}