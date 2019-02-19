using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using LU.Data;

public partial class Q_Admin_product_part_new_list : PageBase
{
    LtdHelper LH = new LtdHelper();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewAll = false;

            //
            // load ...
            //
            BindListView();

            BindCategoryLV();
        }
    }


    #region properties
    public bool ViewAll
    {
        get { return (bool)ViewState["ViewAll"]; }
        set { ViewState["ViewAll"] = value; }
    }
    #endregion

    private void BindCategoryLV()
    {
        this.lv_category_btn.DataSource = Config.ExecuteDataTable(@"select distinct p.menu_child_serial_no, pc.menu_child_name, count(p.product_serial_no) c from tb_product p inner join tb_product_category pc on pc.menu_child_serial_no= p.menu_child_serial_no where issue=0
group by p.menu_child_serial_no");
        this.lv_category_btn.DataBind();
    }

    private void BindListView()
    {
        BindListView(-1);
    }
    private void BindListView(int category_id)
    {

        DataTable dt = Config.ExecuteDataTable(string.Format(@"Select product_serial_no, product_name,product_short_name, manufacturer_part_number, product_store_sum, product_current_price, product_current_cost
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
        ,product_current_special_cash_price
        ,p.menu_child_serial_no
        ,split_line
        ,p.tag
        from tb_product p inner join tb_product_category pc on pc.menu_child_serial_no=p.menu_child_serial_no where issue=0 {0} order by pc.menu_child_order,p.product_order asc", category_id == -1 ? "" : " and p.menu_child_serial_no='" + category_id.ToString() + "'"));
        if (!ViewAll)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string filename = string.Format("{0}{1}_comment.html", Config.Part_Comment_Path, dt.Rows[i]["product_serial_no"].ToString()); //Server.MapPath(string.Format("/part_comment/{0}_comment.html", dt.Rows[i]["product_serial_no"].ToString())).ToLower();
               
                if (File.Exists(filename))
                {
                    StreamReader sr = new StreamReader(filename);
                    string c = sr.ReadToEnd().Trim();
                    if (c.Length < 10)
                    {
                        dt.Rows[i].Delete();
                        //i -= 1;

                    }

                }
                else
                {
                    dt.Rows[i].Delete();
                    // i -= 1;
                }
                //Response.Write(dt.Rows.Count.ToString());
            }
        }
        else
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string filename = string.Format("{0}{1}_comment.html", Config.Part_Comment_Path, dt.Rows[i]["product_serial_no"].ToString()); // Server.MapPath(string.Format("/part_comment/{0}_comment.html", dt.Rows[i]["product_serial_no"].ToString())).ToLower();
                
                if (File.Exists(filename))
                {
                    StreamReader sr = new StreamReader(filename);
                    string c = sr.ReadToEnd().Trim();
                    if (c.Length < 10)
                    {
                        dt.Rows[i]["manufacturer_part_number"] = dt.Rows[i]["manufacturer_part_number"] + "<span style=\"color: red;\">没有描述</span>";
                    }
                }
                else
                {
                    dt.Rows[i]["manufacturer_part_number"] = dt.Rows[i]["manufacturer_part_number"] + "<span style=\"color: red;\">没有描述</span>";
                }
            }
        }
        this.ListView1.DataSource = dt;
        this.ListView1.DataBind();
    }
    protected void ListView1_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        // split_line
        int split_line;
        int.TryParse(((HiddenField)e.Item.FindControl("_hiddenField_split_line")).Value, out split_line);
        //this.btn_search.Text += split_line.ToString();

        int lu_sku;
        int.TryParse(((Label)e.Item.FindControl("_lbl_lu_sku")).Text, out lu_sku);

        string manufacture_code = ((Label)e.Item.FindControl("_lbl_lu_manufacture")).Text;


        if (split_line == 1)
        {
            ((Panel)e.Item.FindControl("_panel_part_title")).Visible = true;
            ((Panel)e.Item.FindControl("_panel_part_commont")).Visible = false;
            return;
        }
        else
        {
            ((Panel)e.Item.FindControl("_panel_part_commont")).Visible = true;
            ((Panel)e.Item.FindControl("_panel_part_title")).Visible = false;
        }



        // store sum
        int lu_store_sum;
        TextBox _txt_store_sum = (TextBox)e.Item.FindControl("_txt_part_sum");
        int.TryParse(_txt_store_sum.Text, out lu_store_sum);
        if (lu_store_sum > 0)
        {
            _txt_store_sum.CssClass = "input_right_line_green";
        }
        // price,cost
        TextBox _txt_cost = (TextBox)e.Item.FindControl("_txt_lu_cost");
        TextBox _txt_price = (TextBox)e.Item.FindControl("_txt_lu_price");

        string rival_string = this.GenerateRivalString(lu_sku, manufacture_code, _txt_cost, _txt_price);
        Literal li = (Literal)e.Item.FindControl("_literal_ravil_vendor");
        li.Text = rival_string;

        // stock
        Literal li_store_status = (Literal)e.Item.FindControl("_lbl_stock");
        int store_status;
        int.TryParse(li_store_status.Text, out store_status);
        li_store_status.Text = new ProductModel().FindStockByLuSku(-1, store_status);


        // onsale , rebate
        ((Literal)e.Item.FindControl("_literal_on_sale_rebate")).Text = GenerateOnsaleRebateString(lu_sku);


        // order part sum


        //
        // bind special_cash_price
        //
        //TextBox _txt_special_cash_price = (TextBox)e.Item.FindControl("_txt_special_cash_price");

        //TextBox _txt_sold = (TextBox)e.Item.FindControl("_txt_lu_sold");
        //decimal special_cash_price;
        //decimal.TryParse(_txt_sold.Text, out special_cash_price);
        //_txt_special_cash_price.Text = ConvertPrice.ChangePriceToNotCard(special_cash_price).ToString();


    }
    protected void ListView1_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        try
        {
            int lu_sku;
            int.TryParse(((Label)e.Item.FindControl("_lbl_lu_sku")).Text, out lu_sku);

            decimal lu_cost;
            decimal.TryParse(((TextBox)e.Item.FindControl("_txt_lu_cost")).Text, out lu_cost);

            decimal lu_price;
            decimal special_cash_price;
            decimal.TryParse(((TextBox)e.Item.FindControl("_txt_special_cash_price")).Text, out special_cash_price);
            lu_price = ConvertPrice.SpecialCashPriceConvertToCardPrice(special_cash_price);

            int store_sum;
            int.TryParse(((TextBox)e.Item.FindControl("_txt_part_sum")).Text, out store_sum);


            int current_index = 0;
            for (int i = 0; i < this.ListView1.Items.Count; i++)
            {
                int _current_sku;
                int.TryParse(((Label)this.ListView1.Items[i].FindControl("_lbl_lu_sku")).Text, out _current_sku);
                if (lu_sku == _current_sku)
                    current_index = i;
            }


            switch (e.CommandName)
            {
                case "Modify":
                    var pm = ProductModel.GetProductModel(DBContext, lu_sku);
                    pm.product_current_price = lu_price;
                    pm.product_current_cost = lu_cost;
                    pm.product_current_special_cash_price = special_cash_price;
                    pm.product_store_sum = store_sum;
                    DBContext.SaveChanges();
                    //BindListView();
                    InsertTraceInfo(DBContext, string.Format("modify (price,cost,store_sum) {0}", lu_sku));
                    CH.CloseParentWatting(this.ListView1);
                    CH.Alert(KeyFields.SaveIsOK, this.ListView1);

                    ((TextBox)e.Item.FindControl("_txt_lu_price")).Text = lu_price.ToString();
                    ((TextBox)e.Item.FindControl("_txt_lu_sold")).Text = (lu_price - pm.product_current_discount).ToString();
                    break;
                case "UP":
                    if (current_index == 0)
                    {
                        CH.Alert("this is first, don't UP", this.ListView1);
                        return;
                    }
                    int sku2;
                    int.TryParse(((Label)this.ListView1.Items[current_index - 1].FindControl("_lbl_lu_sku")).Text, out sku2);
                    if (sku2 == 0)
                        int.TryParse(((Label)this.ListView1.Items[current_index - 1].FindControl("_lbl_lu_sku_title")).Text, out sku2);
                    //CH.Alert(string.Format("{0}|{1}", current_sku, sku2), this.ListView1);
                    ChangeVirtualDB(lu_sku, sku2);
                    CH.CloseParentWatting(this.ListView1);
                    CH.Alert(string.Format("<span style='color:green'>{0}|{1}</span> priority is change.", lu_sku, sku2), this.ListView1);
                    this.BindListView();
                    break;

                case "SetPartSum":
                    int _p_sum;
                    int.TryParse(e.CommandArgument.ToString(), out _p_sum);

                    TextBox _txt_part_sum = (TextBox)e.Item.FindControl("_txt_part_sum");

                    _txt_part_sum.Text = _p_sum.ToString();
                    _txt_part_sum.ForeColor = System.Drawing.Color.FromName("red");
                    break;

                case "Issue":
                    Config.ExecuteNonQuery("update tb_product set tag=1 , issue=1 , last_regdate=now(),is_modify=1 where product_serial_no='" + lu_sku.ToString() + "'");
                    InsertTraceInfo(DBContext, "issue and save (" + lu_sku.ToString() + ")");
                    BindListView();
                    break;
            }
        }
        catch (Exception ex)
        {
            CH.CloseParentWatting(this.ListView1);
            CH.Alert(ex.Message, this.ListView1);
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="lu_sku"></param>
    /// <returns></returns>
    private string GenerateOnsaleRebateString(int lu_sku)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("<table cellspacing=\"0\" cellpadding=\"0\"><tr><td valign=\"top\" style='color:#ccc; font-size:8pt'>");
        var osm = new tb_on_sale();// OnSaleModel();
        var osms = new OnSaleModel().FindModelByProductSerialNo(DBContext, lu_sku);
        if (osms.Length == 1)
        {
            sb.Append(string.Format("On Sale : <i style='color:#ff9900'>{0}</i> to <i style='color:#ff9900'>{1}</i>", osms[0].begin_datetime, osms[0].end_datetime));
        }
        sb.Append("</td><tr><td valign=\"top\" style='color:#ccc; font-size:8pt'>");
        SalePromotionModel spm = new SalePromotionModel();
        DataTable dt = spm.FindDateTimeByPartID(lu_sku);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            sb.Append(string.Format("rebate : <i style='color:#ff9900'>{0}</i> to <i style='color:#ff9900'>{1}</i><br/>", dt.Rows[i]["begin_datetime"].ToString(), dt.Rows[i]["end_datetime"].ToString()));
        }
        sb.Append("</td></tr></table>");
        if (osms.Length < 1 && dt.Rows.Count < 1)
            return "";
        return sb.ToString();
    }
    /// <summary>
    /// 通过绑定，取得库存，进从，竞争对手价格等情况
    /// </summary>
    /// <param name="lu_sku"></param>
    /// <param name="manufacture_code"></param>
    /// <param name="_cost"></param>
    /// <param name="_price"></param>
    /// <returns></returns>
    private string GenerateRivalString(int lu_sku, string manufacture_code, TextBox _cost, TextBox _price)
    {

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append(@"<table cellpadding=""0"" cellspacing=""1"" class=""table_td_white"" style=""width:100%"">
                            <tr>
                                <td style=""padding-right: 2px; width: 50%"">
                                    <!-- vendor begin -->
                                    <table cellpadding=""0"" cellspacing=""0""  style=""width:100%"">");
        ProductStoreSumModel pssm = new ProductStoreSumModel();
        DataTable dt = Config.ExecuteDataTable(string.Format(@"select distinct ol.other_inc_sku, ol.lu_sku, oi.last_regdate, other_inc_store_sum,other_inc_price, other_inc_id, tag from tb_other_inc_match_lu_sku ol inner join tb_other_inc_part_info oi
 on oi.other_inc_sku=ol.other_inc_sku and ol.other_inc_type=oi.other_inc_id and ol.lu_sku='{0}'", lu_sku));
        DataTable LtdDT = LH.LtdHelperWholesalerToDT();
        DataTable rivalDT = LH.LtdHelperRivalToDT();

        decimal lu_cost;
        decimal lu_price;
        decimal.TryParse(_cost.Text, out lu_cost);
        decimal.TryParse(_price.Text, out lu_price);

        decimal ltd_cost = lu_cost;
        decimal rival_price = lu_price;

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataRow dr = dt.Rows[i];
            int ltd_id;
            int.TryParse(dr["other_inc_id"].ToString(), out ltd_id);

            string ltd_sku;
            decimal ltd_part_cost;
            string ltd_last_datetime;
            ltd_sku = dr["other_inc_sku"].ToString();
            decimal.TryParse(dr["other_inc_price"].ToString(), out ltd_part_cost);
            ltd_last_datetime = dr["last_regdate"].ToString();

            if (ltd_id < 1)
                break;

            for (int j = 0; j < LtdDT.Rows.Count; j++)
            {

                if (ltd_id.ToString() == LtdDT.Rows[j]["id"].ToString())
                {
                    sb.Append(string.Format(@"<tr>
                                            <td style='text-align:right'>
                                                <span style='color:#993300'>{0}</span>
                                            </td>
                                            <td style=""width:50px ; text-align:center"">
                                                {1}
                                            </td>
                                            <td style=""width:60px;text-align:right"">
                                                {2}
                                            </td>
                                            <td style=""width: 150px;text-align:right"">
                                
                                               <span style='color:#CCC'> {3}</span>
                                            </td>                                            
                                        </tr>", LtdDT.Rows[j]["text"].ToString()
                                             , dr["tag"].ToString() == "1" ? StoreSumColor(dr["other_inc_store_sum"].ToString()) : "Discontinued"
                                              , ltd_part_cost.ToString("$###.###.##")
                                              , ltd_last_datetime));

                    // use compare price
                    if (ltd_cost > ltd_part_cost)
                    {
                        ltd_cost = ltd_part_cost;
                    }
                }
            }
        }
        sb.Append(@" </tr>
                                </table>
                                <!-- vendor end -->
                            </td>
                            <td valign=""top"" style=""padding-left: 5px ; border-left: 1px dotted #D5582E"">
                                <!-- rival begin -->
                                    <table cellpadding=""0"" cellspacing=""0"" style=""width: 100%;"">");
        RivalStoreModel rs = new RivalStoreModel();
        //DataTable rsms = rs.FindModelsByManufactureCode(lu_sku,manufacture_code);

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataRow dr = dt.Rows[i];

            decimal _rival_price;
            decimal.TryParse(dr["other_inc_price"].ToString(), out _rival_price);

            if (_rival_price != 0M)
            {
                for (int j = 0; j < rivalDT.Rows.Count; j++)
                {
                    if (rivalDT.Rows[j]["id"].ToString() == dr["other_inc_id"].ToString())
                    {
                        sb.Append(string.Format(@"<tr>
                                            <td>
                                                <span style='color:#993300'>{0}</span>
                                            </td>
                                            <td style='text-align:right; width: 60px'>
                                                {1}
                                            </td>
                                            <td>
                                                <span style='color:#CCC'> {2}</span>
                                            </td>
                                         </tr>", rivalDT.Rows[j]["text"].ToString(), _rival_price.ToString("$###,###.##"), dr["last_regdate"].ToString()));
                        // use compare price
                        if (_rival_price < rival_price)
                        {
                            rival_price = _rival_price;
                        }
                    }
                }
            }
        }
        sb.Append(@"</table>
                                    <!-- rival end -->
                                </td>
                            </tr>
                        </table>");

        if (lu_cost > ltd_cost && ltd_cost > 0)
        {
            _cost.CssClass = "input_right_line_red";
        }
        else
            _cost.CssClass = "input_right_line_green";
        if (lu_price >= rival_price)
        {
            _price.CssClass = "input_right_line_red";
        }
        else
            _price.CssClass = "input_right_line_green";
        return sb.ToString();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sum"></param>
    /// <returns></returns>
    private string StoreSumColor(string sum)
    {
        int c;
        int.TryParse(sum, out c);
        if (c > 0)
        {
            return "<span style='color:green'>" + c.ToString() + "</span>";
        }
        if (c < 0)
        {
            return "<span style='color:red'>" + c.ToString() + "</span>";
        }
        return sum;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="lu_sku1"></param>
    /// <param name="lu_sku2"></param>
    private void ChangeVirtualDB(int lu_sku1, int lu_sku2)
    {

        Config.ExecuteNonQuery(string.Format("update tb_temp_exchange set exchange_value=(select max(product_order) from tb_product where product_serial_no='{0}')", lu_sku1));
        Config.ExecuteNonQuery(string.Format("update tb_product p, tb_product pp set p.product_order=pp.product_order where p.product_serial_no='{0}' and pp.product_serial_no='{1}';", lu_sku1, lu_sku2));
        Config.ExecuteNonQuery(string.Format("update tb_product set product_order=(select max(exchange_value) from tb_temp_exchange) where product_serial_no='{0}';", lu_sku2));
    }

    protected void btn_View_OK_Click(object sender, EventArgs e)
    {
        ViewAll = false;
        BindListView();
        CH.CloseParentWatting(this.ListView1);
    }
    protected void btn_view_all_Click(object sender, EventArgs e)
    {
        ViewAll = true;
        BindListView();
        CH.CloseParentWatting(this.ListView1);
    }
    protected void btn_issue_all_ok_Click(object sender, EventArgs e)
    {
        System.Text.StringBuilder skus = new System.Text.StringBuilder();
        ListView gv = (ListView)this.ListView1;
        for (int i = 0; i < gv.Items.Count; i++)
        {
            bool issue = ((CheckBox)gv.Items[i].FindControl("_checkBox_showit")).Checked;
            if (issue)
            {
                int lu_sku;
                int.TryParse(((Label)gv.Items[i].FindControl("_lbl_lu_sku")).Text, out lu_sku);
                skus.Append(lu_sku.ToString() + ",");
                decimal lu_cost;
                decimal.TryParse(((TextBox)gv.Items[i].FindControl("_txt_lu_cost")).Text, out lu_cost);

                decimal lu_price;
                decimal special_cash_price;
                decimal.TryParse(((TextBox)gv.Items[i].FindControl("_txt_special_cash_price")).Text, out special_cash_price);
                lu_price = ConvertPrice.SpecialCashPriceConvertToCardPrice(special_cash_price);

                int store_sum;
                int.TryParse(((TextBox)gv.Items[i].FindControl("_txt_part_sum")).Text, out store_sum);



                var pm = ProductModel.GetProductModel(DBContext, lu_sku);
                pm.product_current_price = lu_price;
                pm.product_current_cost = lu_cost;
                pm.product_current_special_cash_price = special_cash_price;
                pm.product_store_sum = store_sum;

                pm.issue = ((CheckBox)gv.Items[i].FindControl("_checkBox_showit")).Checked;
                pm.tag = sbyte.Parse("1");
                DBContext.SaveChanges();
                InsertTraceInfo(DBContext, "issue and save (" + lu_sku.ToString() + ")");

                ((TextBox)gv.Items[i].FindControl("_txt_lu_price")).Text = lu_price.ToString();
                ((TextBox)gv.Items[i].FindControl("_txt_lu_sold")).Text = (lu_price - pm.product_current_discount).ToString();
            }
        }
    }
    protected void lv_category_btn_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        ViewAll = true;
        BindListView(int.Parse(e.CommandArgument.ToString()));
        CH.CloseParentWatting(this.ListView1);
    }
}
