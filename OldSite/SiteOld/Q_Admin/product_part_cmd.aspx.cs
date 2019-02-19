using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class Q_Admin_product_part_cmd : PageBase
{
    PriceHelper PH = new PriceHelper();
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
        Response.Clear();
        
        try
        {
            switch (ReqCMD.ToLower())
            {
                case "close":
                    #region Close
                    if (Util.GetStringSafeFromQueryString(Page, "showit") == "0")
                    {
                        Config.ExecuteNonQuery("Update tb_product set tag=1,is_modify=1 where product_serial_no='" + ReqSKU.ToString() + "' or price_sku='" + ReqSKU.ToString() + "'");

                        Response.Write("<script>alert('SKU " + ReqSKU.ToString() + " is show.');window.opener.window.partShowGray('" + ReqSKU.ToString() + "', " + (Util.GetStringSafeFromQueryString(Page, "showit") != "0").ToString().ToLower() + " );window.close();</script>");
                    }
                    else
                    {
                        Config.ExecuteNonQuery("Update tb_product set tag=0,is_modify=1 where product_serial_no='" + ReqSKU.ToString() + "' or price_sku='" + ReqSKU.ToString() + "'");
                        Response.Write("<script>alert('SKU " + ReqSKU.ToString() + " is close.');window.opener.window.partShowGray('" + ReqSKU.ToString() + "', " + (Util.GetStringSafeFromQueryString(Page, "showit") != "0").ToString().ToLower() + " );window.close();</script>");
                    }
                    InsertTraceInfo("hide part: " + ReqSKU.ToString());
                    Response.End();
                    #endregion

                    break;
                case "save_cash_price":
                    #region save_cash_price
                    Response.ClearContent();
                    Response.Clear();
                    if (ReqSKU > 0)
                    {
                        //Response.Write(Cash_price.ToString());
                        //Response.Write("Hello World");

                        //ProductModel pm = ProductModel.GetProductModel(SKU);
                        //pm.product_current_price = ConvertPrice.SpecialCashPriceConvertToCardPrice(Cash_price) + pm.product_current_discount;
                        //pm.product_current_special_cash_price = ConvertPrice.ChangePriceToNotCard(pm.product_current_price - pm.product_current_discount);
                        //pm.Update();


                        PH.SaveSpecialCash(ReqSKU, ReqCash_price);
                        ProductModel pm = ProductModel.GetProductModel(ReqSKU);
                        PH.SaveAdjust(ReqSKU, pm.adjustment, pm, pm.adjustment_enddate);
                        PH.ModifyRelevancePrice(ReqSKU, pm.product_current_cost, pm.adjustment, pm.adjustment_enddate);
                        pm = ProductModel.GetProductModel(ReqSKU);

                        Response.Write(string.Format("{0}|{1}|{2}|{3}|{4}|{5}"
                            , pm.product_current_price.ToString()
                            , pm.product_current_price - pm.product_current_discount
                            , pm.product_current_special_cash_price
                            , pm.adjustment
                            , pm.product_current_discount
                            , pm.product_current_cost));

                        InsertTraceInfo("save part cash price($" + pm.product_current_special_cash_price.ToString() + ") SKU: " + ReqSKU.ToString());
                    }
                    else
                    {
                        Response.Write("params is error.");
                    }

                    Response.End();
                    #endregion
                    break;

                case "onsale":
                    #region onsale
                    try
                    {
                        double day_qty = Util.GetDoubleSafeFromString(Page, "day_qty", 0);
                        if (day_qty == 0)
                        {
                            Config.ExecuteNonQuery(string.Format(@"
delete from tb_on_sale where product_serial_no='{0}';
update tb_product set product_current_price=product_current_price-product_current_discount,is_modify=1 where product_serial_no='{0}';
update tb_product set product_current_discount=0 where product_serial_no='{0}';", ReqSKU));
                            InsertTraceInfo("on sale DEL SKU: " + ReqSKU.ToString());
                        }
                        else if (day_qty > 0)
                        {
                            if (ReqDiscount_price > 0)
                            {
                                Config.ExecuteNonQuery(string.Format("Delete from tb_on_sale where product_serial_no='{0}'", ReqSKU));
                                OnSaleModel os = new OnSaleModel();
                                ProductModel pm = ProductModel.GetProductModel(ReqSKU);
                                os.begin_datetime = DateTime.Now;
                                os.end_datetime = DateTime.Now.AddDays(day_qty - 1);
                                os.cost = pm.product_current_cost;
                                os.modify_datetime = DateTime.Now;
                                os.price = pm.product_current_price + ReqDiscount_price;
                                os.sale_price = pm.product_current_price;
                                os.save_price = ReqDiscount_price;
                                os.product_serial_no = ReqSKU;
                                os.Create();


                                pm.product_current_price = pm.product_current_price + ReqDiscount_price - pm.product_current_discount;
                                pm.product_current_discount = ReqDiscount_price;
                                pm.product_current_special_cash_price = ConvertPrice.ChangePriceToNotCard(pm.product_current_price - ReqDiscount_price);
                                pm.Update();

                                InsertTraceInfo("on sale ADD SKU: " + ReqSKU.ToString());
                            }
                            else if (ReqDiscount_price == 0M)
                            {
                                Config.ExecuteNonQuery(string.Format(@"
delete from tb_on_sale where product_serial_no='{0}';
update tb_product set product_current_price=product_current_price-product_current_discount where product_serial_no='{0}';
update tb_product set product_current_discount=0,is_modify=1 where product_serial_no='{0}';", ReqSKU));
                                InsertTraceInfo("on sale DEL SKU: " + ReqSKU.ToString());
                            }
                        }
                        else
                            Response.Write("促销天数小于0");

                        ProductModel _pm = ProductModel.GetProductModel(ReqSKU);
                        Response.Write(string.Format("{0}|{1}|{2}|{3}|{4}|{5}"
                            , _pm.product_current_price.ToString()
                            , _pm.product_current_price - _pm.product_current_discount
                            , _pm.product_current_special_cash_price
                            , _pm.adjustment
                            , _pm.product_current_discount
                            , _pm.product_current_cost));
                    }
                    catch (Exception ex) { Response.Write(ex.Message); }
                    Response.End();
                    #endregion
                    break;

                case "save_cost_price":
                    #region save_cost_price
                    Response.ClearContent();
                    Response.Clear();
                    if (ReqSKU > 0)
                    {
                        //Response.Write(Cash_price.ToString());
                        //Response.Write("Hello World");

                        ProductModel pm = ProductModel.GetProductModel(ReqSKU);
                        pm.product_current_cost = ReqCost_Price;
                        pm.Update();

                        PH.SaveAdjust(ReqSKU, pm.adjustment, pm, pm.adjustment_enddate);
                        PH.ModifyRelevancePrice(ReqSKU, ReqCost_Price, pm.adjustment, pm.adjustment_enddate);

                        pm = ProductModel.GetProductModel(ReqSKU);

                        Response.Write(string.Format("{0}|{1}|{2}|{3}|{4}|{5}"
                            , pm.product_current_price.ToString()
                            , pm.product_current_price - pm.product_current_discount
                            , pm.product_current_special_cash_price
                            , pm.adjustment
                            , pm.product_current_discount
                            , pm.product_current_cost));

                        InsertTraceInfo("save part Cost price($" + pm.product_current_cost.ToString() + ") SKU: " + ReqSKU.ToString());
                    }
                    else
                    {
                        Response.Write("params is error.");
                    }

                    Response.End();
                    #endregion
                    break;
                case "save_adjust_price":
                    #region save_adjust_price
                    Response.ClearContent();
                    Response.Clear();
                    if (ReqSKU > 0)
                    {
                        DateTime adjustEndDate;
                        if (ReqAdjustEndDate == "")
                            adjustEndDate = DateTime.MinValue;
                        else
                            DateTime.TryParse(ReqAdjustEndDate, out adjustEndDate);

                        PH.SaveAdjust(ReqSKU, ReqAdjust, adjustEndDate);
                        ProductModel pm = ProductModel.GetProductModel(ReqSKU);
                        PH.ModifyRelevancePrice(ReqSKU, pm.product_current_cost, ReqAdjust, adjustEndDate);
                        //throw new Exception(ReqAdjust.ToString());

                        Response.Write(string.Format("{0}|{1}|{2}|{3}|{4}|{5}"
                            , pm.product_current_price.ToString()
                            , pm.product_current_price - pm.product_current_discount
                            , pm.product_current_special_cash_price
                            , pm.adjustment
                            , pm.product_current_discount
                            , pm.product_current_cost));

                        InsertTraceInfo("save part cash price($" + pm.product_current_special_cash_price.ToString() + ") SKU: " + ReqSKU.ToString());
                    }
                    else
                    {
                        Response.Write("params is error.");
                    }

                    Response.End();
                    #endregion
                    break;

                case "splitconnectpartchange":
                    Response.Write((1 == Config.ExecuteNonQuery("Insert into tb_part_not_change_price(luc_sku, regdate, endDate) values ('" + ReqSKU.ToString() + "', now(), '" + (ReqDays > 0 ? DateTime.Now.AddDays(ReqDays).ToString("yyyy-MM-dd") : "") + "')")).ToString());
                    break;

                case "connectpartchange":
                    Response.Write((1 == Config.ExecuteNonQuery("delete from tb_part_not_change_price where luc_sku='" + ReqSKU.ToString() + "'")).ToString());
                    break;

                case "ModifyNamesEbayShortMiddleLong":
                    #region ModifyNamesEbayShortMiddleLong
                    if (ReqSKU > 0)
                    {
                        ProductModel pm = ProductModel.GetProductModel(ReqSKU);
                        switch (ReqPageViewCmd)
                        {
                            case PageViewSelectCmd.ModifyShortName:
                                pm.product_short_name = ReqModifyName;
                                break;
                            case PageViewSelectCmd.ModifyMiddleName:
                                pm.product_name = ReqModifyName;
                                break;
                            case PageViewSelectCmd.ModifyLongName:
                                pm.product_name_long_en = ReqModifyName;
                                break;
                            case PageViewSelectCmd.ModifyEbayName:
                                pm.product_ebay_name = ReqModifyName;
                                break;
                        }
                        pm.Update();
                        Response.Write("OK");
                    }
                    #endregion
                    break;

                case "settosystop":
                    #region top
                    Response.ClearContent();
                    Response.Clear();
                    ProductModel part = ProductModel.GetProductModel(ReqSKU);
                    part.is_top = true;
                    part.Update();
                    Response.Write("OK");
                    Response.End();
                    #endregion
                    break;

                case "settosysuntop":
                    #region top
                    Response.ClearContent();
                    Response.Clear();
                    part = ProductModel.GetProductModel(ReqSKU);
                    part.is_top = false;
                    part.Update();
                    Response.Write("OK");
                    Response.End();
                    #endregion
                    break;

                case "gettopsalelist":
                    WriteTopSaleList(ReqCateId);
                    break;

                case "getconnectiondays":
                    DataTable cdt = Config.ExecuteDataTable("Select date_format(endDate, '%Y-%m-%d') endDate from tb_part_not_change_price where luc_sku='" + ReqSKU.ToString() + "' limit 1");
                    if (cdt.Rows.Count == 1)
                        Response.Write("End Date: " + cdt.Rows[0][0].ToString());
                    break;

            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
            Response.End();
        } 
        Response.End();
    }


    void WriteTopSaleList(int cateid)
    {
       
        DataTable dt = Config.ExecuteDataTable(@"
select * from (
select max(p.product_serial_no) sku, p.product_ebay_name p_name, sum(oq.quantity) c  from tb_product p inner join tb_ebay_code_and_luc_sku ec on p.product_serial_no=ec.sku
inner join tb_order_ebay_quantity oq on oq.itemid=ec.ebay_code  where p.menu_child_serial_no='" + cateid.ToString()+@"' and p.tag=1 group by p.product_serial_no
) c order by c desc limit 30;");

        Response.Write("<table class='listtable'><tr><th>SKU</th><th>eBay Sold Quantity</th><th>name</th></tr>");
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataRow dr = dt.Rows[i];
            Response.Write("<tr>");
            Response.Write(" <td width='60' name='sku'>" + dr["sku"].ToString() + "</td>");
            Response.Write(" <td width='50'>" + dr["c"].ToString() + "</td>");
            Response.Write(" <td style='text-align:left;'>" + dr["p_name"].ToString() + "</td>");
            Response.Write("</tr>");
        }
        Response.Write("</table>");

    }
    /// <summary>
    /// 
    /// </summary>
    public int ReqCateId
    {
        get
        {
            return Util.GetInt32SafeFromQueryString(Page, "cateid", -1);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public int ReqSKU
    {
        get
        {
            int sku = Util.GetInt32SafeFromQueryString(Page, "sku", -1);
            if (sku == -1)
                sku = Util.GetInt32SafeFromString(Page, "sku", -1);
            return sku;
        }

    }
    /// <summary>
    /// 
    /// </summary>
    public string ReqCMD
    {
        get
        {
            string cmd = Util.GetStringSafeFromQueryString(Page, "cmd").ToLower();
            if (cmd == "")
                cmd = Util.GetStringSafeFromString(Page, "cmd").ToString();
            return cmd;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public decimal ReqCash_price
    {
        get { return Util.GetDecimalSafeFromString(Page, "cash_price", -1M); }
    }
    /// <summary>
    /// 
    /// </summary>
    public decimal ReqDiscount_price
    {
        get { return Util.GetDecimalSafeFromString(Page, "discount_price", 0M); }
    }
    /// <summary>
    /// 
    /// </summary>
    public decimal ReqCost_Price
    {
        get { return Util.GetDecimalSafeFromString(Page, "cost_price", 0M); }
    }
    /// <summary>
    /// 
    /// </summary>
    public decimal ReqAdjust
    {
        get { return Util.GetDecimalSafeFromString(Page, "adjust", 0M); }
    }
    /// <summary>
    /// 
    /// </summary>
    public string ReqModifyName
    {
        get { return Util.GetStringSafeFromString(Page, "modifyName"); }
    }
    /// <summary>
    /// 
    /// </summary>
    string ReqAdjustEndDate
    {
        get { return Util.GetStringSafeFromString(Page, "adjustEnddate"); }
    }

    /// <summary>
    /// 
    /// </summary>
    public PageViewSelectCmd ReqPageViewCmd
    {
        get
        {
            int _sort = Util.GetInt32SafeFromString(Page, "PageViewCmd", -1);
            return (PageViewSelectCmd)Enum.Parse(typeof(PageViewSelectCmd), Enum.GetName(typeof(PageViewSelectCmd), _sort));
        }
    }

    int ReqDays
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "days", -1); }
    }
}
