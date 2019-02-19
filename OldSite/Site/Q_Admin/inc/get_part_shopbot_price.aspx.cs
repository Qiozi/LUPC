using System;
using System.Data;

public partial class Q_Admin_inc_get_part_shopbot_price : PageBase
{
    LtdHelper LH = new LtdHelper();
    RivalHelper RH = new RivalHelper();
   
    const int SUPERCOM_ID = 2;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            WritePriceArea();
            Response.End();
        }
    }


    public void WritePriceArea()
    {
        DataTable dt = Config.ExecuteDataTable(@"select manufacturer_part_number
, product_current_price-product_current_discount sell
, product_current_cost
from tb_product where product_serial_no='" + SKU.ToString() + "'");
        if (dt.Rows.Count > 0)
        {
            if (OnlyWholesaler)
            {
                Response.Write(GenerateRivalString2(SKU, dt.Rows[0]["manufacturer_part_number"].ToString()
                    , dt.Rows[0]["product_current_cost"].ToString()
                    , dt.Rows[0]["sell"].ToString()));

            }
            else
            {
                Response.Write(GenerateRivalString(SKU, dt.Rows[0]["manufacturer_part_number"].ToString()
                    , dt.Rows[0]["product_current_cost"].ToString()
                    , dt.Rows[0]["sell"].ToString()));
            }
        }
    }
    /// <summary>
    /// only wholesaler
    /// 
    /// </summary>
    /// <param name="lu_sku"></param>
    /// <param name="manufacture_code"></param>
    /// <param name="_cost"></param>
    /// <param name="_price"></param>
    /// <returns></returns>
    private string GenerateRivalString2(int lu_sku, string manufacture_code, string _cost, string _price)
    {

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append(@" <!-- vendor begin -->
                       <table cellpadding=""0"" cellspacing=""0""  style=""width:100%"">");
        ProductStoreSumModel pssm = new ProductStoreSumModel();
        DataTable dt = Config.ExecuteDataTable(string.Format(@"select distinct  oi.luc_sku, oi.last_regdate, other_inc_store_sum,other_inc_price, other_inc_id, tag, oi.other_inc_sku from tb_other_inc_part_info oi
 where oi.luc_sku='{0}'", lu_sku));
        DataTable LtdDT = LH.LtdHelperWholesalerToDT();
        DataTable rivalDT = LH.LtdHelperRivalToDT();

        decimal lu_cost;
        decimal lu_price;
        decimal.TryParse(_cost, out lu_cost);
        decimal.TryParse(_price, out lu_price);

        decimal ltd_cost = lu_cost;
        decimal rival_price = lu_price;

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataRow dr = dt.Rows[i];
            int ltd_id;
            int.TryParse(dr["other_inc_id"].ToString(), out ltd_id);

            //int lu_sku;
            //int.TryParse(dr["lu_sku"].ToString(), out lu_sku);
            //
            // only supercom
            //
            string mfp = dr["other_inc_sku"].ToString();

            //string ltd_sku;
            decimal ltd_part_cost;
            string ltd_last_datetime;
            //ltd_sku = dr["other_inc_sku"].ToString();
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
                                              , StoreSumColor(dr["other_inc_store_sum"].ToString())
                                              , ltd_part_cost.ToString("$###,###.00")
                                              , ltd_last_datetime));


                    if (ltd_cost > ltd_part_cost)
                    {
                        ltd_cost = ltd_part_cost;
                    }
                }
            }
        }
        sb.Append(@"</table>&nbsp;
                                <!-- vendor end -->                            ");

        //if (lu_cost > ltd_cost && ltd_cost > 0)
        //{
        //    _cost.CssClass = "input_right_line_red";
        //}
        //else
        //    _cost.CssClass = "input_right_line_green";
        //if (lu_price >= rival_price)
        //{
        //    _price.CssClass = "input_right_line_red";
        //}
        //else
        //    _price.CssClass = "input_right_line_green";
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
    private string GenerateRivalString(int lu_sku, string manufacture_code, string _cost, string _price)
    {

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append(@"<table cellpadding=""0"" cellspacing=""0"" class=""part_list_shopbot_area"" style=""width:100%"">
                            <tr>
                                <td width='70' valign='top' style='border-top: 1px dotted #cccccc;'>
                                    <a href=""/q_admin/other_inc_view_compare.aspx?categoryid=" + CID.ToString() + @"&id=" + SKU.ToString() + @"""
                                        onclick=""js_callpage_cus(this.href, 'shopbotView', 880, 800);return false;"" title=""Modify Detail"">Shopbot</a></td>
                                <td style=""padding-right: 2px; width: 50%;border-top: 1px dotted #cccccc;border-right: 0px dotted #cccccc;"" valign=""Top"">
                                    <!-- vendor begin -->
                                    
                                    <table cellpadding=""0"" cellspacing=""0""  style=""width:100%"">");
        ProductStoreSumModel pssm = new ProductStoreSumModel();
        DataTable dt = Config.ExecuteDataTable(string.Format(@"select distinct  oi.luc_sku, oi.last_regdate, other_inc_store_sum,other_inc_price, other_inc_id, tag, oi.other_inc_sku, ETA from tb_other_inc_part_info oi
 where oi.luc_sku='{0}'", lu_sku));
        DataTable LtdDT = LH.LtdHelperWholesalerToDT();

        decimal lu_cost;
        decimal lu_price;
        decimal.TryParse(_cost, out lu_cost);
        decimal.TryParse(_price, out lu_price);

        decimal ltd_cost = lu_cost;
        decimal rival_price = lu_price;

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataRow dr = dt.Rows[i];
            int ltd_id;
            int.TryParse(dr["other_inc_id"].ToString(), out ltd_id);

            //int lu_sku;
            //int.TryParse(dr["lu_sku"].ToString(), out lu_sku);
            //
            // only supercom
            //
            string mfp = dr["other_inc_sku"].ToString();

            //string ltd_sku;
            decimal ltd_part_cost;
            string ltd_last_datetime;

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
                                            <td style=""text-align:right; padding-right:1em"">
                                                {2}
                                            </td>
                                            <td style=""text-align:right; padding-right:1em"">                                
                                               <span style='color:#CCC'> {3}</span>
                                            </td> 
                                            <td style=""width:90px;"">                                
                                               <span style='color:#CCC;'>ETA: {4}</span>
                                            </td>                             
                                        </tr>", LtdDT.Rows[j]["text"].ToString()
                                              , StoreSumColor(dr["other_inc_store_sum"].ToString())
                                              , ltd_part_cost.ToString("$###,###.00")
                                              , ltd_last_datetime
                                              , dr["ETA"].ToString()));

                    if (ltd_cost > ltd_part_cost)
                    {
                        ltd_cost = ltd_part_cost;
                    }
                }
            }
        }
        sb.Append(@"            </table>&nbsp;
                                <!-- vendor end -->
                            </td>
                            <td valign=""top"" style=""padding-left: 5px ; border-left: 1px dotted #cccccc;border-top: 1px dotted #cccccc;"">
                                <!-- rival begin -->
                               
                                    <table cellpadding=""0"" cellspacing=""0"" style=""width:350px"" align=""left"">");

        DataTable rivalDT = Config.ExecuteDataTable("Select * from tb_other_inc_shopbot where lu_sku='" + lu_sku.ToString() + "'");
        for (int i = 0; i < rivalDT.Rows.Count; i++)
        {
            DataRow dr = rivalDT.Rows[i];

            decimal _rival_price;
            decimal.TryParse(dr["price"].ToString(), out _rival_price);


            sb.Append(string.Format(@"<tr>
                                            <td>
                                                <span style='color:#993300'>{0}</span>
                                            </td>
                                            <td style='text-align:right; width: 80px;padding-right: 15px'>
                                                {1}
                                            </td>
                                            <td style='width: 150px;'>
                                                <span style='color:#CCC'> {2}</span>
                                            </td>
                                         </tr>", dr["other_inc_name"].ToString()
                                   , _rival_price.ToString("$###,###.00")
                                   , dr["regdate"].ToString()));


        }
        sb.Append(@"</table>&nbsp;
                                    <!-- rival end -->
                                </td>
                            </tr>
                        </table>");

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

    #region properties
    public int SKU
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "sku", -1); }
    }

    public int CID
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "cid", -1); }
    }

    public bool OnlyWholesaler
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "OnlyWholesaler", -1) == 1; }
    }
    #endregion


}
