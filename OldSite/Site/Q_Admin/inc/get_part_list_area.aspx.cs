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

public partial class Q_Admin_inc_get_part_list_area : PageBase
{

    const int PAGE_SIZE = 10;

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            // Response.ClearContent();
            //Response.Clear();
            //Response.Write(Request.Form["split_name"].ToString());
            try
            {
                if (ReqSort == PartListSort.DeleteNoneWholesaler && ReqPageViewCmd == PageViewSelectCmd.NoneWholesaler)
                {
                    // 删除所有没有经销商的商品

                    Config.ExecuteNonQuery(@"update tb_product set tag=0 
where is_fixed=0 and menu_child_serial_no='" + ReqCID.ToString() + @"' and product_serial_no not in (select distinct luc_sku from tb_other_inc_part_info op inner join tb_other_inc oi on oi.id=op.other_inc_id where oi.other_inc_type=1)
");
                    Response.Write("<p style='padding:10em;color:blue; text-align:center;'>Delete is OK</p>");

                }
                else
                {
                    WritePartListArea();
                }
            }
            catch (Exception ex) { Response.Write(ex.Message); }
            Response.End();
        }
    }
    

    private void WritePartListArea()
    {


        string sql = "";
        string sql_count = "";
        string sql_limit = "";
        string sql_other_inc = "";
        string sql_orderby = "";
        if (ReqISDownload)
        {
            sql = @"
SET SQL_BIG_SELECTS=1;
select 	p.product_serial_no sku	, p.product_order priority
	, p.product_name middle_name
	, p.product_short_name short_name
	, p.tag showit
	, p.producter_serial_no manufacturer
	, p.producter_url manufacturer_url
	, p.manufacturer_part_number 
	, p.supplier_sku 
 	,p.hot, 
	p.new, 
	p.split_line, 
	p.product_name_long_en long_name, 
	p.product_img_sum img_sum, 
	p.keywords, 
	p.other_product_sku, 
	p.export,
    p.product_current_cost cost,
    round(p.product_current_price/1.022, 2) special_cost_price
    ,p.product_store_sum store_sum
    ,p.model
    ,p.adjustment";

        }
        else if (ReqPageViewCmd == PageViewSelectCmd.ModifyEbayName
            || ReqPageViewCmd == PageViewSelectCmd.ModifyLongName
            || ReqPageViewCmd == PageViewSelectCmd.ModifyMiddleName
            || ReqPageViewCmd == PageViewSelectCmd.ModifyShortName)
        {
            sql = @" ";
            sql_count = @"Select count(p.product_serial_no) as c ";
            if (!ReqISDownload)
                sql_limit = string.Format(" limit {0},{1}", (ReqCurrentPage - 1) * PAGE_SIZE, PAGE_SIZE);

        }
        else
        {
            sql = @"
SET SQL_BIG_SELECTS=1;
Select p.keywords
, product_current_real_cost
, real_cost_regdate
, product_serial_no
, product_name
, product_short_name
, manufacturer_part_number
, product_store_sum
, product_current_price
, product_current_cost
, product_name_long_en 
, case when product_store_sum >2 then 2 
        when ltd_stock >2 then 2 
        when product_store_sum + ltd_stock >2 then 2 
        when product_store_sum  <=2 and product_store_sum >0 then 3
        when product_store_sum +ltd_stock <=2 and product_store_sum +ltd_stock >0 then 3
        when ltd_stock <=2 and ltd_stock >0 then 3
        when product_store_sum+ltd_stock =0 then 4 
        when product_store_sum+ltd_stock <0 then 5 end as ltd_stock 
        
, case when other_product_sku > 0 then other_product_sku
            else product_serial_no end as img_sku, date_format(regdate, '%Y-%m-%d') regdate, date_format(last_regdate, '%Y-%m-%d') last_regdate        
, (product_current_price-product_current_discount) product_current_sold        
,round((product_current_price-product_current_discount ) / " + Config.is_card_rate.ToString() + @", 2) product_current_special_cash_price        
,p.menu_child_serial_no        
,split_line        
,p.tag        
,p.model
,p.adjustment
,p.product_current_discount
,case when other_product_sku >0 then other_product_sku else product_serial_no end as img_sku
,ifnull(w_sku,0) w_sku
,ifnull(e_sku,0) e_sku
,ifnull(r_sku,0) r_sku
,case when pncp.luc_sku>1 then 0 else 1 end as connect_change
,date_format(pncp.endDate, '%Y-%m-%d') connectEndDate
,ifnull(is_ebay_online, 0) is_ebay_online
,ifnull(esp.system_sku, 0) e_sys_sku
,e_online.st
,p.price_sku
,p.curr_change_ltd
,p.curr_change_cost
,p.curr_change_regdate
,p.split_name
,p.is_top
,ebaySysCount.ebayOldCount
,UPC
,p.ETA
,p.weight
,e_online.itemid
,(select ifnull(sum(quantity),0) from tb_order_ebay_quantity where itemid=e_online.itemid) eBaySoldQuantity
,e_online.quantityavailable
,date_format(p.adjustment_enddate, '%Y-%m-%d') adjustment_enddate
";

            sql_count = @"Select count(p.product_serial_no) as c ";
            if (!ReqISDownload)
                sql_limit = string.Format(" limit {0},{1}", (ReqCurrentPage - 1) * PAGE_SIZE, PAGE_SIZE);
        }

        //if (OtherINC != -1)
        //{
        //    sql_orderby = " order by product_current_special_cash_price-other_inc_price asc ";
        //    sql_limit = sql_orderby + sql_limit;
        //}
        //else
        //{
        //    sql_orderby = " order by regdate desc";
        //    sql_limit = sql_orderby + sql_limit;
        //}

        #region sort
        if (ReqSort == PartListSort.SelectSort)
        {
            sql_orderby = " order by product_serial_no desc";
            sql_limit = sql_orderby + sql_limit;
        }
        else if (ReqSort == PartListSort.HighestPriceFirst)
        {
            sql_orderby = " order by p.product_current_price-p.product_current_discount desc, p.product_serial_no desc";
            sql_limit = sql_orderby + sql_limit;
        }

        else if (ReqSort == PartListSort.HighestStockQuantity)
        {
            sql_orderby = " order by p.ltd_stock desc, p.product_serial_no desc";
            sql_limit = sql_orderby + sql_limit;
        }
        else if (ReqSort == PartListSort.LowestPriceFirst)
        {
            sql_orderby = " order by p.product_current_price-p.product_current_discount asc";
            sql_limit = sql_orderby + sql_limit;
        }
        else if (ReqSort == PartListSort.LowestStockQuantity)
        {
            sql_orderby = " order by p.product_store_sum , p.ltd_stock asc, p.product_serial_no desc";
            sql_limit = sql_orderby + sql_limit;
        }
        else if (ReqSort == PartListSort.LowestLastModifyFirst)
        {
            sql_orderby = " order by p.last_regdate desc, p.product_serial_no asc";
            sql_limit = sql_orderby + sql_limit;
        }
        else if (ReqSort == PartListSort.eBayDiffBuyItNowPrice)
        {
            sql_orderby = " order by eBayBuyItNow.diffPrice desc, p.product_serial_no desc";
            sql_limit = sql_orderby + sql_limit;
        }
        else if (ReqSort == PartListSort.ETA)
        {
            sql_orderby = " and p.ETA <> '' order by p.ETA , p.product_serial_no desc";
            sql_limit = sql_orderby + sql_limit;
        }
        #endregion

        string sql_exec = "";
        string sql_other_table = "";
        string sql_exec_count = "";
        //
        // other inc
        //
        if (ReqOtherINC != -1)
        {
            sql_other_inc = " inner join (select distinct luc_sku, other_inc_price  from tb_other_inc_part_info where other_inc_id='" + ReqOtherINC.ToString() + "') inc on inc.luc_sku=p.product_serial_no ";
        }

        //
        // if exist Web Sys and eBay Sys.
        //
        sql_other_table += @" 
left join (select distinct sp.luc_sku w_sku from tb_ebay_system st 
    inner join tb_ebay_system_parts sp on sp.system_sku=st.id 
    and st.showit=1) wsp on wsp.w_sku=p.product_serial_no

left join (select distinct sp.luc_sku e_sku, max(sp.system_sku) system_sku from tb_ebay_system_parts sp inner join tb_ebay_system es 
        on es.id=sp.system_sku and es.is_from_ebay=0 and es.showit=1 group by sp.luc_sku ) esp on esp.e_sku=p.product_serial_no

left join (select distinct product_serial_no r_sku from tb_sale_promotion where 
        TO_DAYS(now()) between TO_DAYS(begin_datetime) and TO_DAYS(end_datetime) and promotion_or_rebate=2 )r
        on r.r_sku=p.product_serial_no

left join (select luc_sku, endDate from tb_part_not_change_price) pncp on pncp.luc_sku=p.product_serial_no

left join (Select distinct luc_sku is_ebay_online,
max(cast(replace(replace(left(timeleft,instr(TimeLeft, ""D"")), ""P"",""""),""D"","""") as signed)  -
(cast(date_format(now(), ""%j"") as signed) -
 cast(date_format(regdate, ""%j"") as signed))) st, max(WatchCount) as WatchCount
,max(QuantityAvailable) as QuantityAvailable
,max(Quantity) as Quantity
,max(ItemID) as ItemId
 from tb_ebay_selling where luc_sku>0 group by luc_sku,regdate order by regdate desc ) e_online 
        on e_online.is_ebay_online=p.product_serial_no

left join (select count(id) ebayOldCount, max(sku) Sku from tb_ebay_code_and_luc_sku where is_sys=0 group by sku) ebaySysCount on ebaySysCount.Sku = p.product_serial_no";





        if (ReqSort == PartListSort.eBayDiffBuyItNowPrice)
        {
            sql_other_table += @" inner join (select distinct luc_sku, max(diffPrice) diffPrice from (select e.luc_sku, NewBuyItNowPrice-e.BuyItNowPrice diffPrice from tb_ebay_selling e inner join tb_ebay_selling_buyitnowprice_tmp t
on e.luc_sku=t.luc_sku and NewBuyItNowPrice<>e.BuyItNowPrice) t group by luc_sku) eBayBuyItNow on eBayBuyItNow.luc_sku=p.product_serial_no";
            sql_other_inc += @" inner join (select distinct luc_sku, max(diffPrice) diffPrice from (select e.luc_sku, NewBuyItNowPrice-e.BuyItNowPrice diffPrice from tb_ebay_selling e inner join tb_ebay_selling_buyitnowprice_tmp t
on e.luc_sku=t.luc_sku and NewBuyItNowPrice<>e.BuyItNowPrice) t group by luc_sku) ebi on ebi.luc_sku=p.product_serial_no";

        }

        sql += " from tb_product p inner join tb_product_category pc on pc.menu_child_serial_no=p.menu_child_serial_no " + sql_other_inc + sql_other_table + " where 1=1 {0} {1} ";
        sql_count += " from tb_product p inner join tb_product_category pc on pc.menu_child_serial_no=p.menu_child_serial_no " + sql_other_inc + "  where 1=1 {0} {1} ";

        string sql_keyword = " and p.split_line=0 ";

        //
        // show it
        //
        if (ReqShowit)
            sql_keyword += " and (p.tag=1 or (p.tag=0 and p.issue=0)) ";

        //
        // keyword is null
        //
        if (ReqKeywordIsNull)
        {
            sql_keyword += " and p.keywords='' ";
        }
        //
        // not wholesaler
        if (ReqPageViewCmd == PageViewSelectCmd.NoneWholesaler)
        {
            sql_keyword += " and p.product_serial_no not in (select distinct luc_sku from tb_other_inc_part_info op inner join tb_other_inc oi on oi.id=op.other_inc_id where oi.other_inc_type=1) ";
        }
        if (ReqPageViewCmd == PageViewSelectCmd.NotEbaySite)
        {
            sql_keyword += " and p.product_serial_no not in (Select distinct luc_sku from tb_ebay_selling where luc_sku>0) ";
        }
        if (ReqPageViewCmd == PageViewSelectCmd.OnSale)
        {
            sql_keyword += " and p.product_serial_no in (select product_serial_no from tb_on_sale where date_format(now(), '%y%j') <= date_format(end_datetime, '%y%j')) ";
        }
        if (ReqPageViewCmd == PageViewSelectCmd.ViewTop)
        {
            sql_keyword += " and p.is_top = 1 ";
        }
        if (ReqPageViewCmd == PageViewSelectCmd.eBayInThePast)
        {
            //sql_keyword += " and e_online.is_ebay_online <>0 ";
        }
        //
        // single part search
        //
        if (ReqKeywordSingle.Length > 0)
        {
            // sql_keyword += " and p.menu_child_serial_no='" + CID.ToString() + "' ";
            sql_keyword += " and p.product_serial_no= '" + ReqKeywordSingle + "' or p.manufacturer_part_number='" + ReqKeywordSingle + "' or p.product_name like '%" + ReqKeywordSingle + "%' or p.product_name_long_en like '%" + ReqKeywordSingle + "%' or p.keywords like '%" + ReqKeywordSingle + "%' or p.model = '" + ReqKeywordSingle + "' or p.UPC = '" + ReqKeywordSingle + "'";
            //Response.Write(string.Format(string.Format(sql, sql_keyword, sql_limit)));
            sql_exec = string.Format(string.Format(sql, sql_keyword, sql_limit));
            sql_exec_count = string.Format(string.Format(sql_count, sql_keyword, ""));
        }
        else if (ReqKeywords.Length > 0)
        {
            //
            //  keywords
            //
            if (ReqKeywords.IndexOf("|") != -1)
            {
                string[] ks = ReqKeywords.Substring(1).Split(new char[] { '|' });
                sql_keyword += " and p.menu_child_serial_no='" + ReqCID.ToString() + "' ";
                for (int i = 0; i < ks.Length; i++)
                {
                    if (ks[i].Trim() != "" && ks[i].Trim().ToLower() != "ALL".ToLower())
                        sql_keyword += " and p.keywords like '%[" + ks[i] + "]%'";
                }
            }

            sql_exec = string.Format(string.Format(sql, sql_keyword, sql_limit));
            sql_exec_count = string.Format(string.Format(sql_count, sql_keyword, ""));
        }
        else if (ReqCID > 0)
        {
            //
            // category 
            //
            sql_keyword = " and p.menu_child_serial_no='" + ReqCID.ToString() + "' " + sql_keyword;
            sql_exec = string.Format(sql, sql_keyword, sql_limit);
            sql_exec_count = string.Format(string.Format(sql_count, sql_keyword, ""));
        }
        else
        {
            Response.Write("<div style='text-align:center; line-height: 50px;'>No Match Data</div>");
            Response.End();
        }

        if (sql_exec == "")
        {
            Response.Write("Error: params is lost.");
            Response.End();
        }
        //Response.Write(sql_exec);
        //Response.End();

        if (!ReqISDownload)
        {
            int recordcount = Config.ExecuteScalarInt32(sql_exec_count);
            WritePageArea(recordcount);
        }
        if (ReqPageViewCmd == PageViewSelectCmd.ModifyEbayName
            || ReqPageViewCmd == PageViewSelectCmd.ModifyLongName
            || ReqPageViewCmd == PageViewSelectCmd.ModifyMiddleName
            || ReqPageViewCmd == PageViewSelectCmd.ModifyShortName)
        {
            WriteModifyNames(ReqPageViewCmd, ReqCID, sql_keyword, sql_limit);
            return;
        }


        DataTable dt = Config.ExecuteDataTable(sql_exec);

        if (dt.Rows.Count == 0)
        {
            Response.Write("<div style='text-align:center; line-height: 50px;'>No Match Data</div>");
            //Response.Write(sql_exec);
            Response.End();
        }

        //Response.Write(showit.ToString());
        //Response.Write(sql_keyword);
        //throw new Exception(sql_exec);
        if (ReqISDownload)
        {
            ExcelHelper eh = new ExcelHelper(dt);
            eh.FileName = "table.xls";
            eh.MaxRecords = 10000;
            eh.Export();
            Response.End();
        }


        if (ReqISplit)
        {
            if (ReqSplitName.Trim().Length > 0 && ReqSplitName.Trim() != "undefined" && ReqSplitName.Trim().ToLower() != "null" && sql_keyword.Length > 5)
            {
                Config.ExecuteNonQuery("Update tb_product p Set split_name='" + ReqSplitName + "',is_modify=1 where 1=1 " + sql_keyword);
            }
            string split_strings = "";
            // Response.Write("Select distinct IFNULL(split_name, 'N/A') from tb_product p where 1=1 " + sql_keyword);
            DataTable splitDT = Config.ExecuteDataTable("Select distinct IFNULL(split_name, 'N/A') from tb_product p where 1=1 " + sql_keyword);
            for (int i = 0; i < splitDT.Rows.Count; i++)
            {
                split_strings += string.Format("<div style='width: 300px; text-align:left;color:green' >{0}. {1}</div>", i + 1, splitDT.Rows[i][0].ToString());

            }

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<div style='background:#f2f2f2; border:1px solid #cccccc;padding:2px;'>");
            sb.Append("split name:<input type='text' size='50' name='split_name'>");
            sb.Append("<input type='button' value='submit' onclick='setPartListSplitName();'>");
            sb.Append(split_strings);
            sb.Append("</div>");
            sb.Append("<table id='part_list' cellpadding='0' cellspacing='0'>");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                int sku;
                int.TryParse(dr["product_serial_no"].ToString(), out sku);

                int category_id;
                int.TryParse(dr["menu_child_serial_no"].ToString(), out category_id);

                sb.Append("<tr tag='" + dr["tag"].ToString() + "'>");
                sb.Append("     <td style='text-align:center;width: 70px;' rowspan='2'>");
                sb.Append("         <img src='" + Config.part_img_path + dr["img_sku"].ToString() + "_t.jpg' style='width:50px;' >");
                sb.Append("     </td>");
                sb.Append("     <td valign='top'>");
                sb.Append("         <b title='sku'>[" + sku.ToString() + "]</b><span name='connect_change' tag='" + sku.ToString() + "'>" + dr["connect_change"].ToString() + "</span><span title='mfp'>(Model:" + dr["model"].ToString() + ")&nbsp;&nbsp;&nbsp;MFP:--" + dr["manufacturer_part_number"].ToString() + "&nbsp;&nbsp;&nbsp;UPC:" + dr["UPC"].ToString() + "</span>");
                sb.Append("         <div title='part_name'><a href='/site/product_parts_detail.asp?id=" + sku.ToString() + "&cid=" + category_id.ToString() + "' target='_blank'>" + dr["product_name"].ToString() + (dr["product_name_long_en"].ToString().Trim().Length > 0 ? "</a><br/><i>" + dr["product_name_long_en"].ToString() + "</i>" : "</a>") + "</div>");
                sb.Append("         <div title='date' style='color:#cccccc;'>create date: " + dr["regdate"].ToString() + "  last modify date: " + dr["last_regdate"].ToString() + "</div>");
                //sb.Append("         <span title='on_sale'></span>");                
                //sb.Append("         <div title='Groups'><a href=\"part_and_group.aspx?partid=" + sku.ToString() + "\" onclick=\"js_callpage_cus(this.href, 'edit_group', 620, 600);return false\" title=\"Modify Detail\">Groups:</a> ");
                //sb.Append("         <span style='font-size:7.5pt;color:#D17446' title='groups_detail' tag='" + sku.ToString() + "'>");
                //sb.Append("         </span>");
                //sb.Append("         </div>");
                sb.Append("     </td>");
                sb.Append("     <td valign='top' style='width:120px;'>");
                //sb.Append("         进&nbsp;&nbsp;&nbsp;价:<input name='import_price' size='10' valu='" + dr["product_current_cost"].ToString() + "' class='input_right_line' />");
                //sb.Append("                         <br />");
                //sb.Append("         原&nbsp;&nbsp;&nbsp;价:<input name='regular_price'  size='10' value='" + dr["product_current_price"].ToString() + "' class='input_right_line' />");
                //sb.Append("                         <br/>");
                //sb.Append("         信用卡:<input name='card_price' size='10' value='" + dr["product_current_sold"].ToString() + "' class='input_right_line' />");
                //sb.Append("                         <br/> ");
                //sb.Append("         现&nbsp;&nbsp;&nbsp;金:<input name='cash_price' size='10' value='" + dr["product_current_special_cash_price"].ToString() + "' class='input_right_line' />");

                sb.Append("     </td>");
                sb.Append("     <td style='text-align:right;width: 120px;' valign='top'>");
                sb.Append("          <a href=\"/q_admin/editPartDetail.aspx?id=" + sku.ToString() + "\" onclick=\"js_callpage_cus(this.href, 'modify_detail', 880, 800);return false;\" title=\"Modify Detail\">Edit</a><br /> ");
                sb.Append("          <a href=\"/q_admin/ebayMaster/ebay_part_temp_page_view.aspx?sku=" + sku.ToString() + "\" target=\"_blank\" title=\"view ebay\">view ebay</a>");
                sb.Append("          <br /><a href=\"/q_admin/product_part_move_or_copy.aspx?cmd=Move&sku=" + sku.ToString() + "\"  onclick=\"js_callpage_cus(this.href, 'move_copy_part', 520, 300);return false;\" title=\"Move\">Move</a>|<a href=\"/q_admin/product_part_move_or_copy.aspx?cmd=Copy&sku=" + sku.ToString() + "\"  onclick=\"js_callpage_cus(this.href, 'move_copy_part', 520, 300);return false;\" title=\"Copy\">Copy</a> ");
                sb.Append("          <br/><span class='part_delete_btn' tag='" + sku.ToString() + "'></span>");
                sb.Append("     </td>");
                sb.Append("</tr>");
                sb.Append("<tr><td colspan='4' title='line'>&nbsp;</td></tr>");
            }
            sb.Append("</table>");

            Response.Write(sb.ToString());
        }
        else
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<table id='part_list' cellpadding='0' cellspacing='0' border='0'>");
            ProductModel newPm = new ProductModel();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];


                if (dr["adjustment_enddate"].ToString().IndexOf("1971") > -1)
                    dr["adjustment_enddate"] = "";

                int sku;
                int.TryParse(dr["product_serial_no"].ToString(), out sku);

                int category_id;
                int.TryParse(dr["menu_child_serial_no"].ToString(), out category_id);

                decimal discount;
                decimal.TryParse(dr["product_current_discount"].ToString(), out discount);

                string ebay_style = "";

                if (dr["st"].ToString().Length > 0)
                {
                    if (dr["st"].ToString() == "0" || dr["st"].ToString().Substring(0, 1) == "-")
                    {
                        ebay_style = " style='border: 1px solid red;' ";
                    }
                }

                string connEndDate = "";
                if (!string.IsNullOrEmpty(dr["connectEndDate"].ToString()))
                    connEndDate = "End Date: " + dr["connectEndDate"].ToString();

                sb.Append("<tr tag='" + dr["tag"].ToString() + "'>");
                sb.Append("     <td style='text-align:center;width: 70px;' rowspan='2'>");
                sb.Append("         <img src='" + Config.part_img_path + dr["img_sku"].ToString() + "_t.jpg' style='width:50px;' >");
                sb.Append("     </td>");
                sb.Append("     <td valign='top'>");
                sb.Append("         <b title='sku'>[" + sku.ToString() + "]</b><span name='connect_change' tag='" + sku.ToString() + "' >" + dr["connect_change"].ToString() + "</span><span>" + connEndDate + "</span>&nbsp;<span title='mfp'>(Model:" + dr["model"].ToString() + ")&nbsp;&nbsp;&nbsp;MFP: " + dr["manufacturer_part_number"].ToString() + "&nbsp;&nbsp;&nbsp;UPC: " + dr["UPC"].ToString() + (!string.IsNullOrEmpty(dr["ETA"].ToString()) ? "&nbsp;&nbsp;&nbsp;ETA: " + dr["ETA"].ToString() : "") + "</span>");
                sb.Append("         <div title='part_name'><a href='/site/product_parts_detail.asp?id=" + sku.ToString() + "&cid=" + category_id.ToString() + "' target='_blank'>" + dr["product_name"].ToString() + (dr["product_name_long_en"].ToString().Trim().Length > 0 ? "</a><br/><i style='color:#000000;'>" + dr["product_name_long_en"].ToString() + "</i>" : "</a>"));
                sb.Append(discount > 0M ? "(<span class='on_sale'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;-" + ConvertPrice.RoundPrice(discount.ToString()) + "</span>)" : "");
                sb.Append("                 <span title='eBaysys'>" + dr["e_sys_sku"].ToString() + "</span>");
                sb.Append("                 <span title='webSys'>" + dr["w_sku"].ToString() + "</span>");
                sb.Append("                 <span title='rebate'>" + dr["r_sku"].ToString() + "</span>");
                sb.Append("                 <span title='eBaySysCount'>" + (dr["is_ebay_online"].ToString() == "0" ? dr["ebayOldCount"].ToString() : "0") + "</span>");
                sb.Append("                 <span title='eBayOnline' " + (ebay_style) + " data-itemid='" + (dr["itemid"].ToString()) + "' data-sku='" + (sku.ToString()) + "'>" + dr["is_ebay_online"].ToString() + "</span>");

                if (dr["price_sku"].ToString() != "0")
                {
                    sb.Append("<span title='" + dr["price_sku"].ToString() + "' style='color:red;' name='relevancePrice'>RP</span>");
                }
                sb.Append("</div>");
                sb.Append("         <div title='date' style='color:#666666;'>create date: " + dr["regdate"].ToString() + "  last modify date: " + dr["last_regdate"].ToString());
                sb.Append(string.Format(@"&nbsp;&nbsp;&nbsp;({0}|{1}|{2})"
                        , dr["curr_change_ltd"].ToString()
                        , dr["curr_change_cost"].ToString()
                        , dr["curr_change_regdate"].ToString()));
                sb.Append("             &nbsp;&nbsp;&nbsp;" + newPm.GetShowStockString(int.Parse(dr["ltd_stock"].ToString())));
                sb.Append("</div>");
                sb.Append("         <span title='on_sale'></span>");

                sb.Append("         <div title='keyword'>");
                sb.Append("             <a href=\"/q_admin/product_helper_part_keywords.asp?cid=" + category_id.ToString() + "&id=" + sku.ToString() + "\" onclick=\"js_callpage_cus(this.href, 'edit_keyword', 780, 600);return false\">");
                sb.Append("             Keyword: " + dr["keywords"].ToString() + "");
                sb.Append("             </a> &nbsp;&nbsp;&nbsp;<i style='color:#466B46;'>" + dr["split_name"].ToString() + "</i> </div>");
                sb.Append("         <div title='Groups'><a href=\"part_and_group.aspx?partid=" + sku.ToString() + "\" onclick=\"js_callpage_cus(this.href, 'edit_group', 620, 600);return false\" title=\"Modify Detail\">Groups:</a> ");
                sb.Append("                 <span title='SysTopView' sku='" + sku.ToString() + "'>" + dr["is_top"].ToString() + "</span>");
                sb.Append("         <span style='font-size:7.5pt;color:#D17446' title='groups_detail' tag='" + sku.ToString() + "'>");
                //sb.Append("     <script>");
                //sb.Append("           <script type=\"text/javascript\" src='/site/inc/inc_get_part_groups_name.asp?id=" + dr["product_serial_no"].ToString() + "'></script>");
                //sb.Append("     </script>");

                sb.Append("         </span>");
                sb.Append("         </div>");
                sb.Append("     </td>");
                sb.Append("     <td valign='top' style='width:200px;' class='part_edit_price_area'>");
                sb.Append("         进&nbsp;&nbsp;&nbsp;价:<input name='import_price' size='10' value='" + dr["product_current_cost"].ToString() + "' class='input_right_line' />");
                sb.Append("         <span class='save_btn' style='display:none;'><img src='images/save_1.gif' style='height:12px;border:1px solid white; cursor:pointer;' onclick=\"part_save_cost_price($(this), '" + sku.ToString() + "');\"></span>");
                sb.Append("                         <br />");
                sb.Append("         原&nbsp;&nbsp;&nbsp;价:<input name='regular_price'  size='10' value='" + dr["product_current_price"].ToString() + "' class='input_right_line' />");
                sb.Append("                         <br/>");
                sb.Append("         折&nbsp;&nbsp;&nbsp;扣:<input name='discount_price'  size='10' value='" + dr["product_current_discount"].ToString() + "' class='input_right_line' />");
                sb.Append("         <span class='save_discount_btn' style='display:none;'><img src='images/save_1.gif' style='height:12px;border:1px solid white; cursor:pointer;' onclick=\"part_save_discount_price($(this), '" + sku.ToString() + "');\"></span>");
                sb.Append("                         <br/>");
                sb.Append("         信用卡:<input name='card_price' size='10' value='" + dr["product_current_sold"].ToString() + "' class='input_right_line' />");
                sb.Append("                         <br/> ");
                sb.Append("         现&nbsp;&nbsp;&nbsp;金:<input name='cash_price' size='10' value='" + dr["product_current_special_cash_price"].ToString() + "' class='input_right_line' />");
                sb.Append("         <sspan class='save_btn' style='display:none;'><img src='images/save_1.gif' style='height:12px;border:1px solid white; cursor:pointer;' onclick=\"part_save_cash_price($(this), '" + sku.ToString() + "');\"></sspan>");

                sb.Append("                         <br/> ");
                sb.Append("         adjust:<input name='adjustment' size='10' value='" + dr["adjustment"].ToString() + "' class='input_right_line' />");
                sb.Append("         <span class='save_btn' style='display:none;' title='adjustment'><img src='images/save_1.gif' style='height:12px;border:1px solid white; cursor:pointer;' onclick=\"part_save_adjustment($(this), '" + sku.ToString() + "');\"></span>");
                sb.Append("         <br/>&nbsp;&nbsp;date:<input type='text' name='adjustment_enddate' size='10' value='" + dr["adjustment_enddate"].ToString() + "' class='input_right_line'/>");


                sb.Append("     </td>");
                sb.Append("     <td style='text-align:right;width: 100px;' valign='top'>");
                sb.Append("          <a href=\"/q_admin/editPartDetail.aspx?id=" + sku.ToString() + "\" onclick=\"js_callpage_cus(this.href, 'modify_detail', 1080, 800);return false;\" title=\"Modify Detail\">Edit</a>|<a href=\"/q_admin/editPartDetail.aspx?cmd=1&id=" + sku.ToString() + "\" onclick=\"js_callpage_cus(this.href, 'modify_detail', 1080, 800);return false;\" title=\"Modify Detail Comment\">Edit Comm</a><br /> ");
                sb.Append("          <a href=\"/q_admin/ebayMaster/ebay_part_comment_edit.asp?sku=" + sku.ToString() + "\" onclick=\"js_callpage_cus(this.href, 'ebay_part_comment_edit', 1080, 700);return false;\" title='Edit Ebay'>Edit</a>|<a href=\"/q_admin/ebayMaster/ebay_part_temp_page_view.aspx?sku=" + sku.ToString() + "\" target=\"_blank\" title=\"view ebay\">view ebay</a>");
                sb.Append("          <br /><a href=\"/q_admin/product_part_cmd.aspx?showit=" + dr["tag"].ToString() + "&cmd=close&sku=" + sku.ToString() + "\"  onclick=\"js_callpage_cus(this.href, 'part_cmd', 320, 300);return false;\" title=\"Close\">" + (dr["tag"].ToString() == "0" ? "Show" : "Hide") + "</a>");
                sb.Append("          <br /><a href=\"/q_admin/product_part_rebate.aspx?keyword=" + sku.ToString() + "\"  onclick=\"js_callpage_cus(this.href, 'part_cmd', 520, 500);return false;\" title=\"edit rebate\">edit rebate</a>");
                sb.Append("          <br /><a href=\"/q_admin/product_part_move_or_copy.aspx?cmd=Move&sku=" + sku.ToString() + "\"  onclick=\"js_callpage_cus(this.href, 'move_copy_part', 520, 300);return false;\" title=\"Move\">Move</a>|<a href=\"/q_admin/product_part_move_or_copy.aspx?cmd=Copy&sku=" + sku.ToString() + "\"  onclick=\"js_callpage_cus(this.href, 'move_copy_part', 520, 300);return false;\" title=\"Copy\">Copy</a> ");


                sb.Append("          <br/><span class='part_delete_btn' tag='" + sku.ToString() + "'></span>");
                sb.Append("     </td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("     <td colspan='3'>");
                sb.Append("         <table width='100%'><tr><td width=180 style='border:1px dotted #ccc;' valign='top'>");
                sb.Append("     <div>Weight: " + dr["weight"].ToString() + "</div>");
                sb.Append("     <div>eBay Itemid: <a href=\"https://cgi.ebay.ca/ws/eBayISAPI.dll?ViewItem&amp;Item=" + dr["ItemID"].ToString() + "\" target=\"_blank\">" + dr["ItemID"].ToString() + "</a></div>");
                sb.Append("    <div>eBay Stock: " + dr["QuantityAvailable"].ToString() + "</div>");
                sb.Append("     <div>eBay Sold Qty: " + dr["eBaySoldQuantity"].ToString() + "</div>");
                sb.Append("     <div><input type='button' value='add to ebay on sale' onclick=\"AddPartToEbaySale('" + sku.ToString() + "');\"></div>");
                sb.Append("     <div>eBay on sale: <span class='ebayOnSalePrice' data-sku='" + sku.ToString() + "'></span></div>");
                sb.Append("             </td><td width=\"150\" style='border:1px dotted #ccc;' valign='top' name='ebayPriceInfo' sku='" + sku.ToString() + "'>...</td><td title='shopbot_area' tag='" + sku.ToString() + "'  valign='top'></td></tr></table>");
                sb.Append("     </td>");
                sb.Append("</tr>");
                sb.Append("<tr><td colspan='4' title='line'>&nbsp;</td></tr>");
            }
            sb.Append("</table>");

            Response.Write(sb.ToString());
        }
    }

    private void WritePageArea(int recordcount)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        if (recordcount % PAGE_SIZE == 0)
        {
            for (int i = 0; i < int.Parse((recordcount / PAGE_SIZE).ToString()); i++)
            {
                if ((i + 1) == ReqCurrentPage)
                    sb.Append("<span title=\\'page\\' class=\\'selected\\'>" + (i + 1).ToString() + "</span>");
                else
                    sb.Append("<span title=\\'page\\'>" + (i + 1).ToString() + "</span>");
            }
        }
        else
        {
            for (int i = 0; i <= int.Parse((recordcount / PAGE_SIZE).ToString()); i++)
            {
                if ((i + 1) == ReqCurrentPage)
                {
                    sb.Append("<span title=\\'page\\' class=\\'selected\\'>" + (i + 1).ToString() + "</span>");
                }
                else
                    sb.Append("<span title=\\'page\\'>" + (i + 1).ToString() + "</span>");
            }
        }
        Response.Write(string.Format("<script> $('#page_area').html('<div><span style=\\'display:block; float:left;width: 80px;\\'>Page(" + recordcount.ToString() + "):</span> {0}</div>');</script>", sb.ToString()));
    }

    void WriteModifyNames(PageViewSelectCmd pageCmd, int CategoryID
        , string sql_keyword, string sql_limit)
    {
        string modifyNameText = "";
        string sql = @"
Select {0} name, product_name_long_en
, product_serial_no
, case when other_product_sku > 0 then other_product_sku
            else product_serial_no end as img_sku,manufacturer_part_number, tag from tb_product p where 1=1" + sql_keyword + " " + sql_limit;
        switch (pageCmd)
        {
            case PageViewSelectCmd.ModifyEbayName:
                sql = string.Format(sql, "product_ebay_name", CategoryID);
                modifyNameText = "Modify eBay Name:";
                break;
            case PageViewSelectCmd.ModifyLongName:
                sql = string.Format(sql, " product_name_long_en ", CategoryID);
                modifyNameText = "Modify Long Name:";
                break;
            case PageViewSelectCmd.ModifyMiddleName:
                sql = string.Format(sql, "product_name", CategoryID);
                modifyNameText = "Modify Middle Name:";
                break;
            case PageViewSelectCmd.ModifyShortName:
                sql = string.Format(sql, "product_short_name", CategoryID);
                modifyNameText = "Modify Short Name:";
                break;
            default:
                return;

        }
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("<table id='part_list' cellpadding='0' cellspacing='0' border='0'>");

        //throw new Exception(sql);

        DataTable dt = Config.ExecuteDataTable(sql);

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataRow dr = dt.Rows[i];
            int sku;
            int.TryParse(dr["product_serial_no"].ToString(), out sku);


            sb.Append("<tr tag='" + dr["tag"].ToString() + "'>");
            sb.Append("     <td style='text-align:center;width: 70px;' rowspan='2'>");
            sb.Append("         <img src='" + Config.part_img_path + dr["img_sku"].ToString() + "_t.jpg' style='width:50px;' >");
            sb.Append("     </td>");
            sb.Append("     <td>");
            sb.Append("         <b title='sku'>[" + sku.ToString() + "]</b><br/> MFP: <span style='color:green;'>" + Server.HtmlEncode(dr["manufacturer_part_number"].ToString()) + "</span>");

            sb.Append("<br/><span style='color:#666'>" + modifyNameText + "</span></td>");
            sb.Append("     <td valign='top'>");
            sb.Append("         <div title='part_name' style='padding:5px;'><input type='text' name='part_modify_name' tag='" + dr["product_serial_no"].ToString() + "' size='150' value=\"" + dr["name"].ToString().Replace("\"", "'") + "\"" + (dr["product_name_long_en"].ToString().Trim().Length > 0 ? "<a href='/site/product_parts_detail.asp?id=" + sku.ToString() + "&cid=" + CategoryID.ToString() + "' target='_blank'><span name='loading' tag='" + dr["product_serial_no"].ToString() + "'></span><br/><i style='color:#999999;'>" + dr["product_name_long_en"].ToString() + "</i>" : ""));
            sb.Append("</div>");
            sb.Append("     </td>");
            sb.Append("     <td valign='top' style='width:190px;' class='part_edit_price_area'>");
            sb.Append("     <input type='button' name='Edit' value='Edit' disabled='true' tag='" + dr["product_serial_no"].ToString() + "'>");
            sb.Append("     <input type='button' name='Save' value='Save' disabled='true' tag='" + dr["product_serial_no"].ToString() + "'>");
            sb.Append("     <input type='button' name='SaveALL' value='Save ALL' disabled='true' tag='" + dr["product_serial_no"].ToString() + "'>");
            sb.Append("     </td>");
            sb.Append("</tr>");
            sb.Append("<tr><td colspan='4' title='line'>&nbsp;</td></tr>");
        }
        sb.Append("</table>");

        Response.Write(sb.ToString());

    }

    #region properties

    public PartListSort ReqSort
    {
        get
        {
            //return PartListSort.SelectSort;
            int _sort = Util.GetInt32SafeFromString(Page, "sort", -1);
            return (PartListSort)Enum.Parse(typeof(PartListSort), Enum.GetName(typeof(PartListSort), _sort));
            //return PartListSort.SelectSort;
            //try
            //{
            //    return (PartListSort)Enum.Parse(typeof(PartListSort), Enum.GetName(typeof(PartListSort), Sort));
            //}
            //catch { return PartListSort.SelectSort; }
        }

    }

    /// <summary>
    /// 
    /// </summary>
    public PageViewSelectCmd ReqPageViewCmd
    {
        get
        {
            //if (Util.GetStringSafeFromString(Page, "PageViewCmd") == "1")
            //    return PageViewSelectCmd.NoneWholesaler;
            //else
            //    return PageViewSelectCmd.NotEbaySite;
            int _sort = Util.GetInt32SafeFromString(Page, "PageViewCmd", -1);
            return (PageViewSelectCmd)Enum.Parse(typeof(PageViewSelectCmd), Enum.GetName(typeof(PageViewSelectCmd), _sort));
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public string ReqKeywords
    {
        get { return Util.GetStringSafeFromString(Page, "keywords"); }
    }

    public string ReqKeywordSingle
    {
        get { return Util.GetStringSafeFromString(Page, "keyword_single"); }
    }

    public int ReqCID
    {
        get
        {
            int cid;
            int.TryParse(Request["cid"], out cid);
            return cid;
        }
    }

    public bool ReqShowit
    {
        get
        {

            return Request["showit"] == "1";
        }
    }

    public bool ReqISDownload
    {
        get { return Request["down"] == "true"; }
    }

    public int ReqCurrentPage
    {
        get { return Util.GetInt32SafeFromString(Page, "page", 1); }
    }

    public bool ReqKeywordIsNull
    {
        get { return Util.GetStringSafeFromString(Page, "is_null_keyword") == "true"; }
    }


    public bool ReqISplit
    {
        get { return Util.GetStringSafeFromString(Page, "is_split") == "true"; }
    }

    public string ReqSplitName
    {
        get { return Util.GetStringSafeFromString(Page, "split_name"); }
    }

    public int ReqOtherINC
    {
        get
        {
            string other_inc = Util.GetStringSafeFromString(Page, "other_inc");

            DataTable dt = new LtdHelper().LtdHelperToDataTableNoLU();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (other_inc.ToLower() == dt.Rows[i]["text"].ToString().ToLower())
                {
                    return int.Parse(dt.Rows[i]["id"].ToString());
                }
            }
            return -1;
        }

    }
    #endregion
}

public enum PartListSort
{
    SelectSort = -1
    ,
    HighestPriceFirst = 1
        ,
    LowestPriceFirst = 2
        ,
    HighestStockQuantity = 3
        ,
    LowestStockQuantity = 4
        ,
    LowestLastModifyFirst = 5
        ,
    eBayDiffBuyItNowPrice = 6
        ,
    ETA = 7
        , DeleteNoneWholesaler
}


