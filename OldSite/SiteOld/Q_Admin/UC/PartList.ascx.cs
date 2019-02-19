using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Q_Admin_UC_PartList : CtrlBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
        }
    }


    #region properties
    public int PageSize
    {
        get { return (int)ViewState["PageSize"]; }
        set { ViewState["PageSize"] = value; }
    }

    public int StartRecord
    {
        get { return (int)ViewState["StartRecord"]; }
        set { ViewState["StartRecord"] = value; }
    }
    #endregion

    public DataTable FindPartInfo(int current_category_id, int split_line_id, bool is_new, bool is_hot, bool is_on_sale
        , string keyword, string keyword_field, bool showit, bool is_and_or)
    {
        System.Text.StringBuilder sb_skus = new System.Text.StringBuilder();
        string sku_list = string.Empty;

        if (current_category_id < 1)
            current_category_id = 999999;

        if (split_line_id != -1)
        {
            DataTable dt_split = Config.ExecuteDataTable(string.Format(@"select product_serial_no, split_line from tb_product 
where menu_child_serial_no='{0}' 
and product_order > (select max(product_order) from tb_product where product_serial_no='{1}') order by product_order asc ", current_category_id, split_line_id));
            for (int i = 0; i < dt_split.Rows.Count; i++)
            {
                DataRow dr = dt_split.Rows[i];
                if (i == 0)
                {
                    sb_skus.Append(split_line_id + ",");
                }
                else
                {
                    if (dr["split_line"].ToString() == "1")
                        break;
                    sb_skus.Append(dr["product_serial_no"].ToString() + ",");
                }
            }
            if (sb_skus.ToString().Length > 2)
            {
                sku_list = sb_skus.ToString().Substring(0, sb_skus.ToString().Length - 1);
            }
        }

        string search_if = string.Format(" {0} {1} {2} {3} {4} {5} {6} ", showit ? " and p.tag=1 " : ""
                                           , is_hot ? " and p.hot=1 " : ""
                                           , is_new ? " and p.new=1 " : ""
                                           , is_on_sale ? " and p.product_current_discount > 0" : ""
                                           , sku_list != string.Empty ? string.Format(" {1} product_serial_no in ({0})", sku_list, is_and_or ? " and " : " or ") :
                                           (current_category_id != -1 ? string.Format(" {1} p.menu_child_serial_no ='{0}'", current_category_id, is_and_or ? " and " : " or ") : "")
                                           , keyword != "" && keyword_field != "None" ?
                                           (keyword_field == "p.keywords" ? string.Format(@" and ({0} like '%{1}%'  or p.product_name like '%{1}%') ", keyword_field, keyword) : string.Format(@" and {0} like '%{1}%' ", keyword_field, keyword)) : ""
                                           , string.Format(" and p.menu_child_serial_no in ({0})", new GetAllValidCategory().ToString()));

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
            else product_serial_no end as img_sku, regdate, last_regdate
        , (product_current_price-product_current_discount) product_current_sold
        ,p.menu_child_serial_no
        ,split_line
        from tb_product p inner join tb_product_category pc on pc.menu_child_serial_no=p.menu_child_serial_no where 1=1 {0} order by pc.menu_child_order,p.product_order asc {1} ",
                                          search_if
                                           , string.Format(" limit {0}, {1}", StartRecord, PageSize)
        ));

        Config.ExecuteScalar(string.Format(@"Select count(p.product_serial_no) c
        from tb_product p where 1=1 {0} ",
                                           search_if
        ));


        int count = int.Parse(Config.ExecuteScalar(string.Format(@"Select count(p.product_serial_no) c
        from tb_product p where 1=1  {0} ",
                                           search_if
        )).ToString());

        return dt;
    }

    protected void ListView1_ItemCommand(object sender, ListViewCommandEventArgs e)
    {

    }
    protected void ListView1_ItemDataBound(object sender, ListViewItemEventArgs e)
    {

    }
}
