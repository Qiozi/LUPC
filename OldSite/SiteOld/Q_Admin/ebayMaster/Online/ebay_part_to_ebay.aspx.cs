using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Q_Admin_ebayMaster_Online_ebay_part_to_ebay : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitialDatabase();
        }
    }

    public override void InitialDatabase()
    {
        base.InitialDatabase();

        InitPage();
    }

    void InitPage()
    {
        if (EbayHelper.ValidateItemIssue(ReqSKU, false))
        {
            Response.Write("part is issue, no duplicate.");
            Response.End();
            return;
        }


        ProductModel pm = ProductModel.GetProductModel(ReqSKU);
        ProductCategoryModel pc = ProductCategoryModel.GetProductCategoryModel(pm.menu_child_serial_no);
        int ebayCategoryID;
        int.TryParse(pc.eBayCategoryID, out ebayCategoryID);

        EbayItem ei = new EbayItem();
        var partSpecifics = GetItemSpecifics.GetPartSpecifics(pm);
        if (partSpecifics.Count > 0)
            ei.item_specifics = partSpecifics;

        decimal shipping_fee = 0M;
        decimal ebay_fee = 0M;
        decimal profit = 0M;
        decimal bank_fee = 0M;


        ei.Buy_it_now_price = !ProductCategoryModel.IsNotebook(pm.menu_child_serial_no) ?
            GetEbayPrice.GetPartEbayPrice(pm, GeteBayOnsalePriceAdjust.GetEbayOnsalePrice(pm.product_serial_no), ref shipping_fee, ref profit, ref ebay_fee, ref bank_fee) : GetNotebookPrice(pm);

        ei.AutoPay = ei.Buy_it_now_price >= 2000 ? false : true;
        ei.Category = eBayCategoryID.ToString();
        ei.cutom_label = GetCustomLabel(pm.menu_child_serial_no, ReqSKU, pm.producter_serial_no);
        ei.Description = new eBayPageHelper(this).GetPageString(ReqSKU);
        ei.Quantity = (pm.menu_child_serial_no == 350 || pm.menu_child_serial_no == 358) ? 6 : 10;
        if (StoreCategory1 != "0" && StoreCategory1 != "")
            ei.Store_category = StoreCategory1;
        if (StoreCategory2 != "0" && StoreCategory2 != "")
            ei.Store_category2 = StoreCategory2;
        // ei.Title = (pm.menu_child_serial_no == 350 || pm.menu_child_serial_no == 358) ? pm.product_ebay_name_2 : pm.product_ebay_name;
        ei.Title = pm.product_ebay_name;



        ei.Pictures_url1 = "http://www.lucomputers.com/pro_img/COMPONENTS/" + (pm.other_product_sku > 0 ? pm.other_product_sku.ToString() : pm.product_serial_no.ToString()) + "_g_1.jpg";

        string shippingString = eBayShipping.GetPartShippingFeeString(pm);

        EbayItemGenerate eig = new EbayItemGenerate();
        ei.Upc = pm.UPC;
        string code = eig.AddItem(ei, shippingString, ReqSKU, 0, false, false);
        EbayCodeAndLucSkuModel ecalsm = new EbayCodeAndLucSkuModel();
        ecalsm.ebay_code = code;
        ecalsm.is_sys = false;
        ecalsm.SKU = ReqSKU;
        ecalsm.is_online = true;
        ecalsm.Create();

        DataTable dt = Config.ExecuteDataTable("select * from tb_ebay_category_and_product where Sku='" + ReqSKU + "'");
        if (dt.Rows.Count > 0)
        {
            Config.ExecuteNonQuery("update tb_ebay_category_and_product set eBayCateID_1='" + eBayCategoryID + "',eBayMyCateID_1='" + StoreCategory1 + "' where sku='" + ReqSKU + "'");
        }
        else
        {
            Config.ExecuteNonQuery(string.Format(@"insert into tb_ebay_category_and_product 
	( Sku, ProdType, eBayCateID_1, eBayMyCateID_1,   regdate
	)
	values
	( '{0}', '{1}', '{2}', '{3}', now())"
                           , ReqSKU
                           , pc.is_noebook == 1 ? "N" : "P"
                           , eBayCategoryID
                           , StoreCategory1));
        }

        Response.Clear();
        Response.Write(code);
        Response.End();
    }

    /// <summary>
    /// 取得notebook 的ebay 价格
    /// </summary>
    /// <param name="pm"></param>
    /// <returns></returns>
    decimal GetNotebookPrice(ProductModel pm)
    {
        decimal shipping_fee = 0M;
        decimal profit = 0M;
        decimal ebay_fee = 0M;
        decimal bank_fee = 0M;

        decimal ebayPrice = new eBayPriceHelper().eBayNetbookPartPrice(pm.product_current_cost, pm.screen_size, pm.adjustment
                          , ref shipping_fee, ref profit, ref ebay_fee, ref bank_fee);
        return ebayPrice + EbaySettings.ebayAccessoriesPrice;
    }

    public int ReqSKU
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "sku", -1); }
    }

    public int eBayCategoryID
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "ebayCategoryID", -1); }
    }

    string StoreCategory1
    {
        get { return Util.GetStringSafeFromQueryString(Page, "storeCategory1"); }
    }
    string StoreCategory2
    {
        get { return Util.GetStringSafeFromQueryString(Page, "storeCategory2"); }
    }

    string GetCustomLabel(int categoryID, int sku, string mfpName)
    {
        string shortName = "";
        string shipCate = "";
        DataTable shipDt = Config.ExecuteDataTable("select ShippingCategoryId from tb_part_and_shipping where sku='" + sku.ToString() + "' limit 1");
        if (shipDt.Rows.Count == 1)
        {
            shipCate = shipDt.Rows[0][0].ToString();
        }
        EbayShippingSettingsModel[] list = EbayShippingSettingsModel.FindAllByProperty("CategoryID", shipCate);
        if (list != null)
            shortName = list[0].ShortCategoryName;
        return shortName + " " + mfpName + " " + sku.ToString();
    }
}