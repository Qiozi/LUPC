using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using LU.Data;

public partial class Q_Admin_ebayMaster_Online_ebay_part_to_ebay : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitPage();
        }
    }
    

    void InitPage()
    {
        if (EbayHelper.ValidateItemIssue(ReqSKU, false))
        {
            Response.Write("part is issue, no duplicate.");
            Response.End();
            return;
        }

        var pm = ProductModel.GetProductModel(DBContext, ReqSKU);
        var pc = ProductCategoryModel.GetProductCategoryModel(DBContext, pm.menu_child_serial_no.Value);
        int ebayCategoryID;
        int.TryParse(pc.eBayCategoryID, out ebayCategoryID);

        EbayItem ei = new EbayItem();
        var partSpecifics = GetItemSpecifics.GetPartSpecifics(DBContext, pm);
        if (partSpecifics.Count > 0)
            ei.item_specifics = partSpecifics;

        decimal shipping_fee = 0M;
        decimal ebay_fee = 0M;
        decimal profit = 0M;
        decimal bank_fee = 0M;


        ei.Buy_it_now_price = !ProductCategoryModel.IsNotebook(DBContext, pm.menu_child_serial_no.Value)
            ? GetEbayPrice.GetPartEbayPrice(DBContext, pm, GeteBayOnsalePriceAdjust.GetEbayOnsalePrice(pm.product_serial_no), ref shipping_fee, ref profit, ref ebay_fee, ref bank_fee)
            : GetNotebookPrice(pm);

        ei.AutoPay = ei.Buy_it_now_price >= 2000 ? false : true;
        ei.Category = eBayCategoryID.ToString();
        ei.cutom_label = GetCustomLabel(pm.menu_child_serial_no.Value, ReqSKU, pm.producter_serial_no);
        ei.Description = new eBayPageHelper(DBContext, this).GetPageString(ReqSKU);
        ei.Quantity = (pm.menu_child_serial_no == 350 || pm.menu_child_serial_no == 358) ? 6 : 10;
        if (StoreCategory1 != "0" && StoreCategory1 != "")
            ei.Store_category = StoreCategory1;
        if (StoreCategory2 != "0" && StoreCategory2 != "")
            ei.Store_category2 = StoreCategory2;
        // ei.Title = (pm.menu_child_serial_no == 350 || pm.menu_child_serial_no == 358) ? pm.product_ebay_name_2 : pm.product_ebay_name;
        ei.Title = pm.product_ebay_name;



        ei.Pictures_url1 = "http://www.lucomputers.com/pro_img/COMPONENTS/" + (pm.other_product_sku > 0 ? pm.other_product_sku.ToString() : pm.product_serial_no.ToString()) + "_g_1.jpg";

        string shippingString = eBayShipping.GetPartShippingFeeString(DBContext, pm, this.Server);

        EbayItemGenerate eig = new EbayItemGenerate();
        ei.Upc = pm.UPC;
        string code = eig.AddItem(DBContext, ei, shippingString, ReqSKU, 0, false, false);
        var ecalsm = new tb_ebay_code_and_luc_sku();
        ecalsm.ebay_code = code;
        ecalsm.is_sys = false;
        ecalsm.SKU = ReqSKU;
        ecalsm.is_online = true;
        ecalsm.regdate = DateTime.Now;
        DBContext.tb_ebay_code_and_luc_sku.Add(ecalsm);
        DBContext.SaveChanges();

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
    decimal GetNotebookPrice(tb_product pm)
    {
        decimal shipping_fee = 0M;
        decimal profit = 0M;
        decimal ebay_fee = 0M;
        decimal bank_fee = 0M;

        decimal ebayPrice = new eBayPriceHelper().eBayNetbookPartPrice(pm.product_current_cost.Value, pm.screen_size.Value, pm.adjustment.Value
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
        //[] list = EbayShippingSettingsModel.FindAllByProperty("CategoryID", shipCate);
        var list = DBContext.tb_ebay_shipping_settings.FirstOrDefault(me => me.CategoryID.Equals(shipCate));
        if (list != null)
            shortName = list.ShortCategoryName;
        return shortName + " " + mfpName + " " + sku.ToString();
    }
}