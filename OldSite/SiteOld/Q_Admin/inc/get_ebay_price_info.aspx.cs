using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Q_Admin_inc_get_ebay_price_info : PageBase
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


        if (ReqSku > 0 && string.IsNullOrEmpty(ReqCmd) )
        {
            Response.Clear();

            long sku = ReqSku;
            if (ReqSku.ToString().Length == 12)
            {
                DataTable dt = Config.ExecuteDataTable("Select sku from tb_ebay_code_and_luc_sku where ebay_code='" + ReqSku + "' order by id desc limit 0,1");
                if (dt.Rows.Count == 1)
                    sku = long.Parse(dt.Rows[0][0].ToString());
            }
            //Response.Write("Select sku from tb_ebay_code_and_luc_sku where ebay_code='" + ReqSku + "' order by id desc limit 0,1");
            ProductModel pm = sku.ToString().Length == 6 ? null : ProductModel.GetProductModel((int)sku);
            if (pm != null)
            {
                eBayPriceHelper eH = new eBayPriceHelper();
                if (ProductCategoryModel.IsNotebook(pm.menu_child_serial_no))
                {
                    #region notebook

                    decimal shipping_fee = 0M;
                    decimal profit = 0M;
                    decimal ebay_fee = 0M;
                    decimal bank_fee = 0M;
                    decimal buyItNowPrice = 0M;
                    string itemid = "";
                    DataTable dt = Config.ExecuteDataTable("select buyitnowprice, itemid from tb_ebay_selling where luc_sku='" + pm.product_serial_no + "'");
                    if (dt.Rows.Count > 0)
                    {
                        itemid = dt.Rows[0]["itemid"].ToString();
                        buyItNowPrice = decimal.Parse(dt.Rows[0][0].ToString());
                    }

                    decimal ebayPrice = eH.eBayNetbookPartPrice(pm.product_current_cost, pm.screen_size, pm.adjustment
                        , ref shipping_fee, ref profit, ref ebay_fee, ref bank_fee);
                    if (ReqShowTitle)
                    {
                        Response.Write("<h2>" + pm.product_ebay_name + "</h2>");
                    }
                    Response.Write("<input type='button' value='refresh' onclick=\"refreshEbayPriceArea($(this).parent());\">");
                    if (!string.IsNullOrEmpty(itemid))
                        Response.Write("<input type='button' value='Up eBay Price' title='upload ebay price' onclick=\"js_callpage_cus('/q_admin/ebayMaster/Online/ModifyOnlinePrice.aspx?Cost=" + pm.product_current_cost + "&Price=" + (ebayPrice + EbaySettings.ebayAccessoriesPrice) + "&IsDesc=0&onlyprice=1&itemid=" + itemid + "&issystem=0', 'ebay_" + pm.product_serial_no + "', 300, 200);\">");
                    Response.Write("<table border='0'>");
                    if (!string.IsNullOrEmpty(itemid) && ReqShowEBayCode)
                    {
                        Response.Write("    <tr>");
                        Response.Write("        <td>");
                        Response.Write("              eBay Itemid");
                        Response.Write("        </td>");
                        Response.Write("        <td style='text-align:right;'>");
                        Response.Write("<a href='http://cgi.ebay.ca/ws/eBayISAPI.dll?ViewItem&Item=" + itemid + "' target='_blank'> " + itemid + "</a>");
                        Response.Write("        </td>");
                        Response.Write("    </tr>");
                    }
                    Response.Write("    <tr>");
                    Response.Write("        <td>");
                    Response.Write("              Curr eBay Price ");
                    Response.Write("        </td>");
                    Response.Write("        <td style='text-align:right;'>");
                    Response.Write(ConvertPrice.RoundPrice(buyItNowPrice));
                    Response.Write("        </td>");
                    Response.Write("    </tr>");
                    Response.Write("    <tr>");
                    Response.Write("        <td>");
                    Response.Write("                eBay Price ");
                    Response.Write("        </td>");
                    Response.Write("        <td  style='text-align:right;");
                    if (buyItNowPrice != ebayPrice + EbaySettings.ebayAccessoriesPrice)
                    {
                        Response.Write(" color:red;'");
                    }
                    else
                        Response.Write(" '");
                    Response.Write(">");
                    Response.Write(ConvertPrice.RoundPrice(ebayPrice + EbaySettings.ebayAccessoriesPrice));
                    Response.Write("        </td>");
                    Response.Write("    </tr>");
                    Response.Write("    <tr>");
                    Response.Write("        <td>");
                    Response.Write("                Cost ");
                    Response.Write("        </td>");
                    Response.Write("        <td style='text-align:right;'>");
                    Response.Write(ConvertPrice.RoundPrice(pm.product_current_cost));
                    Response.Write("        </td>");
                    Response.Write("    </tr>");
                    Response.Write("    <tr>");
                    Response.Write("        <td>");
                    Response.Write("                Bank Fee ");
                    Response.Write("        </td>");
                    Response.Write("        <td style='text-align:right;'>");
                    Response.Write(ConvertPrice.RoundPrice(bank_fee));
                    Response.Write("        </td>");
                    Response.Write("    </tr>");
                    Response.Write("    <tr>");
                    Response.Write("        <td>");
                    Response.Write("                Adjustment ");
                    Response.Write("        </td>");
                    Response.Write("        <td style='text-align:right;'>");
                    Response.Write(ConvertPrice.RoundPrice(pm.adjustment));
                    Response.Write("        </td>");
                    Response.Write("    </tr>");
                    Response.Write("    <tr>");
                    Response.Write("        <td>");
                    Response.Write("                shipping fee ");
                    Response.Write("        </td>");
                    Response.Write("        <td style='text-align:right;'>");
                    Response.Write(ConvertPrice.RoundPrice(shipping_fee));
                    Response.Write("        </td>");
                    Response.Write("    </tr>");
                    Response.Write("    <tr>");
                    Response.Write("        <td>");
                    Response.Write("                Profit");
                    Response.Write("        </td>");
                    Response.Write("        <td style='text-align:right;'>");
                    Response.Write(ConvertPrice.RoundPrice(profit));
                    Response.Write("        </td>");
                    Response.Write("    </tr>");
                    Response.Write("    <tr>");
                    Response.Write("        <td>");
                    Response.Write("                eBay Fee");
                    Response.Write("        </td>");
                    Response.Write("        <td style='text-align:right;'>");
                    Response.Write(ConvertPrice.RoundPrice(ebay_fee));
                    Response.Write("        </td>");
                    Response.Write("    </tr>");
                    Response.Write("</table>");

                    #endregion
                }
                else
                {
                    #region part
                    decimal shipping_fee = 0M;
                    decimal profit = 0M;
                    decimal ebay_fee = 0M;
                    decimal bank_fee = 0M;
                    decimal buyItNowPrice = 0M;
                    string itemid = "";

                    DataTable dt = Config.ExecuteDataTable("select buyitnowprice,itemid  from tb_ebay_selling where luc_sku='" + pm.product_serial_no + "'");
                    if (dt.Rows.Count > 0)
                    {
                        itemid = dt.Rows[0]["itemid"].ToString();
                        buyItNowPrice = decimal.Parse(dt.Rows[0][0].ToString());
                    }

                    decimal ebayPrice = GetEbayPrice.GetPartEbayPrice(pm, GeteBayOnsalePriceAdjust.GetEbayOnsalePrice(pm.product_serial_no)
                        , ref shipping_fee, ref profit, ref ebay_fee, ref bank_fee);
                    if (ReqShowTitle)
                    {
                        Response.Write("<h2>" + pm.product_ebay_name + "</h2>");
                    }
                    Response.Write("<input type='button' value='refresh' onclick=\"refreshEbayPriceArea($(this).parent());\">");
                    if (!string.IsNullOrEmpty(itemid))
                    {
                       
                        Response.Write("<input type='button' value='Up eBay Price' title='upload ebay price' onclick=\"js_callpage_cus('/q_admin/ebayMaster/Online/ModifyOnlinePrice.aspx?Cost=" + pm.product_current_cost + "&Price=" + (ebayPrice + EbaySettings.ebayAccessoriesPrice) + "&IsDesc=0&onlyprice=1&itemid=" + itemid + "&issystem=0', 'ebay_" + pm.product_serial_no + "', 300, 200);\">");

                    }
                    
                    Response.Write("<table border='0'>");
                    if (!string.IsNullOrEmpty(itemid) && ReqShowEBayCode)
                    {
                        Response.Write("    <tr>");
                        Response.Write("        <td>");
                        Response.Write("              eBay Itemid");
                        Response.Write("        </td>");
                        Response.Write("        <td style='text-align:right;'>");
                        Response.Write("<a href='http://cgi.ebay.ca/ws/eBayISAPI.dll?ViewItem&Item=" + itemid + "' target='_blank'> " + itemid + "</a>");
                        Response.Write("        </td>");
                        Response.Write("    </tr>");
                    }
                    Response.Write("    <tr>");
                    Response.Write("        <td>");
                    Response.Write("              Curr eBay Price ");
                    Response.Write("        </td>");
                    Response.Write("        <td style='text-align:right;'>");
                    Response.Write(ConvertPrice.RoundPrice(buyItNowPrice));
                    Response.Write("        </td>");
                    Response.Write("    </tr>");
                    Response.Write("    <tr>");
                    Response.Write("        <td>");
                    Response.Write("                eBay Price ");
                    Response.Write("        </td>");
                    Response.Write("        <td  style='text-align:right;");
                    if (buyItNowPrice != ebayPrice + EbaySettings.ebayAccessoriesPrice)
                    {
                        Response.Write(" color:red;'");
                    }
                    else
                        Response.Write(" '");
                    Response.Write(">");
                    Response.Write(ConvertPrice.RoundPrice(ebayPrice + EbaySettings.ebayAccessoriesPrice));
                    Response.Write("        </td>");
                    Response.Write("    </tr>");
                    Response.Write("    <tr>");
                    Response.Write("        <td>");
                    Response.Write("                Cost ");
                    Response.Write("        </td>");
                    Response.Write("        <td style='text-align:right;'>");
                    Response.Write(ConvertPrice.RoundPrice(pm.product_current_cost));
                    Response.Write("        </td>");
                    Response.Write("    </tr>");
                    Response.Write("    <tr>");
                    Response.Write("        <td>");
                    Response.Write("                Bank Fee ");
                    Response.Write("        </td>");
                    Response.Write("        <td style='text-align:right;'>");
                    Response.Write(ConvertPrice.RoundPrice(bank_fee));
                    Response.Write("        </td>");
                    Response.Write("    </tr>");
                    Response.Write("    <tr>");
                    Response.Write("        <td>");
                    Response.Write("                Adjustment ");
                    Response.Write("        </td>");
                    Response.Write("        <td style='text-align:right;'>");
                    Response.Write(ConvertPrice.RoundPrice(pm.adjustment));
                    Response.Write("        </td>");
                    Response.Write("    </tr>");
                    Response.Write("    <tr>");
                    Response.Write("        <td>");
                    Response.Write("                shipping fee ");
                    Response.Write("        </td>");
                    Response.Write("        <td style='text-align:right;'>");
                    Response.Write(ConvertPrice.RoundPrice(shipping_fee));
                    Response.Write("        </td>");
                    Response.Write("    </tr>");
                    Response.Write("    <tr>");
                    Response.Write("        <td>");
                    Response.Write("                Profit");
                    Response.Write("        </td>");
                    Response.Write("        <td style='text-align:right;'>");
                    Response.Write(ConvertPrice.RoundPrice(profit));
                    Response.Write("        </td>");
                    Response.Write("    </tr>");
                    Response.Write("    <tr>");
                    Response.Write("        <td>");
                    Response.Write("                eBay Fee");
                    Response.Write("        </td>");
                    Response.Write("        <td style='text-align:right;'>");
                    Response.Write(ConvertPrice.RoundPrice(ebay_fee));
                    Response.Write("        </td>");
                    Response.Write("    </tr>");
                    Response.Write("</table>");
                    #endregion
                }
            }
            else if (sku.ToString().Length == 6)
            {
                #region system
                decimal profits = 0M;
                decimal ebay_fee = 0M;
                decimal shipping_fee = 0M;
                decimal ebay_price = 0M;

                EbaySystemModel esm = EbaySystemModel.GetEbaySystemModel((int)sku);

                decimal sys_cost = decimal.Parse(Config.ExecuteScalar(@"select sum(product_current_cost*ses.part_quantity) cost from tb_product p 
    inner join  tb_ebay_system_parts ses 
    on ses.luc_sku=p.product_serial_no 
    where p.tag=1 and p.split_line=0 and system_sku='" + sku.ToString() + "'").ToString());

                if (esm.is_shrink)
                {
                    ebay_price = GetEbayPrice.GetEbaySysPrice(sys_cost, esm.adjustment
                        , ref profits
                        , ref ebay_fee
                        , ref shipping_fee
                        , true);
                }
                else
                {
                    ebay_price = GetEbayPrice.GetEbaySysPrice(sys_cost, esm.adjustment
                        , ref profits
                        , ref ebay_fee
                        , ref shipping_fee
                        , true);
                }
               // bool Warn = eBaySystemWorker.WarnPartInvalid((int)sku);

//                Response.Write(string.Format(@"[{{            
//            'ebayPrice':'{0}'
//            ,'shipping_fee':'{1}'
//            ,'profit':'{2}'
//            ,'ebay_fee':'{3}'
//            ,'warn':'{4}'
//            ,'cost':'{5}'
//}}]"
//                    , ConvertPrice.RoundPrice2(ebay_price) - 0.01M + EbaySettings.ebayAccessoriesPrice
//                    , ConvertPrice.RoundPrice(shipping_fee)
//                    , ConvertPrice.RoundPrice(profits)
//                    , ConvertPrice.RoundPrice(ebay_fee)
//                    , Warn
//                    , sys_cost));
                string itemid = "";
                decimal buyItNowPrice = 0M;
                DataTable dt = Config.ExecuteDataTable("select buyitnowprice, itemid from tb_ebay_selling where sys_sku='" +sku + "'");
                if (dt.Rows.Count > 0)
                {
                    itemid = dt.Rows[0]["itemid"].ToString();
                    buyItNowPrice = decimal.Parse(dt.Rows[0][0].ToString());
                }
                if (ReqShowTitle)
                {
                    Response.Write("<h2>" + esm.system_title1 + "</h2>");
                }
                Response.Write("<table border='0'>");
                if (!string.IsNullOrEmpty(itemid) && ReqShowEBayCode)
                {
                    Response.Write("    <tr>");
                    Response.Write("        <td>");
                    Response.Write("              eBay Itemid");
                    Response.Write("        </td>");
                    Response.Write("        <td style='text-align:right;'>");
                    Response.Write("<a href='http://cgi.ebay.ca/ws/eBayISAPI.dll?ViewItem&Item=" + itemid + "' target='_blank'> " + itemid + "</a>");
                    Response.Write("        </td><td></td><td></td>");
                    Response.Write("    </tr>");
                }

                Response.Write("    <tr>");
                Response.Write("        <td width='120'>");
                Response.Write("              Curr eBay Price ");
                Response.Write("        </td>");
                Response.Write("        <td width='120' style='text-align:right;'>");
                Response.Write(ConvertPrice.RoundPrice(buyItNowPrice));
                Response.Write("        </td>");
                //Response.Write("    </tr>");
                //Response.Write("    <tr>");
                Response.Write("        <td width='120' style='text-align:right;'>");
                Response.Write("              eBay Price ");
                Response.Write("        </td>");
                Response.Write("        <td style='width:120px;text-align:right; " + (buyItNowPrice!=(ConvertPrice.RoundPrice2(ebay_price) - 0.01M + EbaySettings.ebayAccessoriesPrice) ? "color: red;":"") + "'>");
                Response.Write(ConvertPrice.RoundPrice2(ebay_price) - 0.01M + EbaySettings.ebayAccessoriesPrice);
                Response.Write("        </td>");
                Response.Write("    </tr>");
                Response.Write("    <tr>");
                Response.Write("        <td>");
                Response.Write("             Cost ");
                Response.Write("        </td>");
                Response.Write("        <td style='text-align:right;'>");
                Response.Write(sys_cost);
                Response.Write("        </td>");
                //Response.Write("    </tr>");
                //Response.Write("    <tr>");
                Response.Write("        <td style='text-align:right;'>");
                Response.Write("              Shipping Fee ");
                Response.Write("        </td>");
                Response.Write("        <td style='text-align:right;'>");
                Response.Write(ConvertPrice.RoundPrice(shipping_fee));
                Response.Write("        </td>");
                Response.Write("    </tr>");
                Response.Write("    <tr>");
                Response.Write("        <td>");
                Response.Write("              Profit ");
                Response.Write("        </td>");
                Response.Write("        <td style='text-align:right;'>");
                Response.Write(ConvertPrice.RoundPrice(profits));
                Response.Write("        </td>");
                //Response.Write("    </tr>");
                //Response.Write("    <tr>");
                Response.Write("        <td style='text-align:right;'>");
                Response.Write("              eBay Fee ");
                Response.Write("        </td>");
                Response.Write("        <td style='text-align:right;'>");
                Response.Write(ConvertPrice.RoundPrice(ebay_fee));
                Response.Write("        </td>");
                Response.Write("    </tr>");
                Response.Write("</table>");
                Response.Write("<hr size=1>");
                DataTable sysdt = Config.ExecuteDataTable(@"select p.product_serial_no,ec.comment,p.product_current_cost,p.product_ebay_name,ses.part_quantity,p.product_current_price from tb_product p 
    inner join  tb_ebay_system_parts ses 
    on ses.luc_sku=p.product_serial_no 
    inner join tb_ebay_system_part_comment ec on ec.id=ses.comment_id
    where p.tag=1 and p.split_line=0 and system_sku='" + sku.ToString() + "' and p.product_serial_no <> 16684 order by ec.priority asc");
                Response.Write("<table width='100%' cellpadding=2 cellspacing=0>");

                decimal scost = 0M;
                decimal sprice = 0M;

                for (int i = 0; i < sysdt.Rows.Count; i++)
                {
                    string color =  i%2==0? " style='background:#f2f2f2;' ":"" ;
                    DataRow dr = sysdt.Rows[i];
                    
                    Response.Write("<tr>");
                    Response.Write("    <td " + color + "><b>" + dr["comment"].ToString() + "</b></td>");
                    Response.Write("    <td " + color + ">" + dr["product_ebay_name"].ToString() + "</td>");
                    Response.Write("    <td " + color + ">" + dr["product_current_cost"].ToString() + "</td>");
                    Response.Write("    <td " + color + ">" + dr["product_current_price"].ToString() + "</td>");
                    Response.Write("    <td " + color + ">x" + dr["part_quantity"].ToString() + "</td>");
                    Response.Write("</tr>");
                    //Response.Write("<tr></tr>");
                    scost += decimal.Parse(dr["product_current_cost"].ToString()) * decimal.Parse(dr["part_quantity"].ToString());
                    sprice += decimal.Parse(dr["product_current_price"].ToString()) * decimal.Parse(dr["part_quantity"].ToString());
                }
                Response.Write("<tr>");
                Response.Write("    <td><b>total</b></td>");
                Response.Write("    <td><b></b></td>");
                Response.Write("    <td><b style='color:blue;'>" + scost + "</b></td>");
                Response.Write("    <td><b style='color:blue;'>" + sprice + "</b></td>");
                Response.Write("    <td></td>");
                Response.Write("</tr>");


                Response.Write("</table>");
                #endregion
            }
            Response.End();
        }
        else if (ReqCmd == "getChinaPrice")
        {
            ProductModel pm = ProductModel.GetProductModel((int)ReqSku);
            eBayPriceHelper eH = new eBayPriceHelper();

            decimal shipping_fee = 0M;
            decimal profit = 0M;
            decimal ebay_fee = 0M;
            decimal bank_fee = 0M;
            decimal buyItNowPrice = 0M;
            string itemid = "";
            DataTable dt = Config.ExecuteDataTable("select buyitnowprice, itemid from tb_ebay_selling where luc_sku='" + pm.product_serial_no + "'");
            if (dt.Rows.Count > 0)
            {
                itemid = dt.Rows[0]["itemid"].ToString();
                buyItNowPrice = decimal.Parse(dt.Rows[0][0].ToString());
            }

            decimal ebayPrice = eH.eBayNetbookPartPrice(pm.product_current_cost, pm.screen_size, pm.adjustment
                , ref shipping_fee, ref profit, ref ebay_fee, ref bank_fee);

            // 人民币汇率
            object rateDT = Config.ExecuteScalar("Select currency_cn from tb_currency_convert_cn where is_current=1 limit 1");
            decimal rate;
            decimal.TryParse(rateDT.ToString(), out rate);

            // 当前
            object currChinaPrice = Config.ExecuteScalar("Select price from tb_product_cn where luc_sku='" + ReqSku + "' order by id desc limit 1");
            decimal currChinaPriceDec;
            decimal.TryParse(currChinaPrice.ToString(), out currChinaPriceDec);

            Response.Write(string.Format(@"{{eBay_name:'{0}'            
            ,eBay_Itemid:'{1}'
            ,Curr_eBay_Price:'{2}'
            ,eBay_Price:'{3}'
            ,Cost:'{4}'
            ,Bank_Fee:'{5}'
            ,Adjustment:'{6}'
            ,Shipping_Fee:'{7}'
            ,Profit:'{8}'
            ,eBay_Fee:'{9}'
            ,Wholesaler:'{10}'
            ,Wholesaler_Cost:'{11}'
            ,Wholesaler_Qty:'{12}'
            ,China_Price_New:'{13}'
            ,China_Price_Current:'{14}'}}"
                , pm.product_short_name.Replace("'", "\\'")
                , itemid
                , buyItNowPrice.ToString("##.00")
                , ebayPrice.ToString("##.00")
                , pm.product_current_cost.ToString("##.00")
                , bank_fee.ToString("##.00")
                , pm.adjustment.ToString("##.00")
                , shipping_fee.ToString("##.00")
                , profit.ToString("##.00")
                , ebay_fee.ToString("##.00")
                , pm.curr_change_ltd
                , pm.curr_change_cost.ToString("##.00")
                , pm.curr_change_quantity
                , (ebayPrice * rate).ToString("##.00")
                , currChinaPriceDec.ToString("##.00")));
            Response.End();
        }
        else
        {
            Response.Write("params error.");
            Response.End();
        }
    }

    long ReqSku
    {
        get { return Util.GetInt64SafeFromQueryString(Page, "sku", -1L); }
    }

    bool ReqShowTitle
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "showtitle", 0) == 1; }
    }

    string ReqCmd
    {
        get { return Util.GetStringSafeFromQueryString(Page, "cmd"); }
    }

    bool ReqShowEBayCode
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "showebaycode", 0) == 1; }
    }
}