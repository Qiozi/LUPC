using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Q_Admin_other_inc_asi : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            //
            //
            //this.ddl_asi_category.DataSource = Config.ExecuteDataTable(@"select distinct category from tb_asi_key_feature order by category asc ");
            //this.ddl_asi_category.DataTextField = "category";
            //this.ddl_asi_category.DataValueField = "category";
            //this.ddl_asi_category.DataBind();

            this.ddl_vendor.DataSource = Config.ExecuteDataTable(@"
select '' vendor
union all
select distinct vendor from tb_asi_store_new order by vendor asc ");
            this.ddl_vendor.DataTextField = "vendor";
            this.ddl_vendor.DataValueField = "vendor";
            this.ddl_vendor.DataBind();
        }
    }

    protected void btn_search_Click(object sender, EventArgs e)
    {
        string mainCatCode = this.CategoryDropDownLoad1.CategoryValue.ToString();
        string vendor = this.ddl_vendor.SelectedValue.ToString();
        DataTable dt = Config.ExecuteDataTable(string.Format(@"select tmp.lu_sku,tmp.priority, ass.id,asi_sku, itmeid, description, vendor ,ass.status, ass.price, ass.quantity, mainCatName, concat(mainCatName,' ', SubCatName,' ', itmeid) `name` from tb_asi_store_new ass left join tb_asi_category ac
on ac.subcatcode=ass.sub_category 
left join (select distinct p.product_serial_no lu_sku, other_inc_sku, p.product_order priority from tb_other_inc_match_lu_sku oi
inner join tb_product p on p.product_serial_no=oi.lu_sku and oi.other_inc_type=3) tmp on tmp.other_inc_sku=ass.asi_sku  where 1=1 {0} {1} "
            , vendor.Length >0 ? "  and vendor='"+ vendor+"' " : ""
            , mainCatCode .Length >0 ? " and sub_category='"+ mainCatCode+"'" :""));
        this.lv_asi.DataSource = dt;
        this.lv_asi.DataBind();
    }
    protected void btn_search_asi_sku_Click(object sender, EventArgs e)
    {
        string asi_sku = this.txt_asi_sku.Text.Trim() ;
        DataTable dt = Config.ExecuteDataTable(string.Format(@"select ifnull(tmp.lu_sku, '') lu_sku,tmp.priority, ass.id,asi_sku, itmeid, description, vendor ,ass.status, ass.price, ass.quantity, mainCatName, concat(mainCatName,' ', SubCatName,' ', itmeid) `name` from tb_asi_store_new ass left join tb_asi_category ac
on ac.subcatcode=ass.sub_category 
left join (select distinct p.product_serial_no lu_sku, other_inc_sku, p.product_order priority from tb_other_inc_match_lu_sku oi
inner join tb_product p on p.product_serial_no=oi.lu_sku and oi.other_inc_type=3) tmp on tmp.other_inc_sku=ass.asi_sku  where ass.asi_sku='{0}'"
            , asi_sku));
        this.lv_asi.DataSource = dt;
        this.lv_asi.DataBind();
    }
    protected void btn_search_mfp_Click(object sender, EventArgs e)
    {
        string asi_sku = this.txt_manufacturer_part_number.Text.Trim();
        DataTable dt = Config.ExecuteDataTable(string.Format(@"select tmp.lu_sku,tmp.priority, ass.id,asi_sku, itmeid, description, vendor ,ass.status, ass.price, ass.quantity, mainCatName, concat(mainCatName,' ', SubCatName,' ', itmeid) `name` from tb_asi_store_new ass left join tb_asi_category ac
on ac.subcatcode=ass.sub_category 
left join (select distinct p.product_serial_no lu_sku, other_inc_sku, p.product_order priority from tb_other_inc_match_lu_sku oi
inner join tb_product p on p.product_serial_no=oi.lu_sku and oi.other_inc_type=3) tmp on tmp.other_inc_sku=ass.asi_sku  where ass.itmeid='{0}'"
            , asi_sku));
        this.lv_asi.DataSource = dt;
        this.lv_asi.DataBind();
    }
    protected void lv_asi_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        int lu_sku;
        int.TryParse(((Label)e.Item.FindControl("_lbl_lu_sku")).Text, out lu_sku);

        if (lu_sku != 0)
        {
            ((CheckBox)e.Item.FindControl("_cb_checked")).Enabled = false;
        }
        else
        {
            ((LinkButton)e.Item.FindControl("_lb_edit")).Visible = false;

            ((Literal)e.Item.FindControl("_literal_winopen")).Text = "<a href='/q_admin/other_inc_asi_edit.aspx?id=" + ((HiddenField)e.Item.FindControl("_hf_id")).Value.ToString() + "' onclick=\"winOpen(this.href, 'edit_asi', 880, 800, 120, 200);return false;\">ADD</a>";
        }
        //Label lbl_stock = (Label)e.Item.FindControl("_lbl_f4");
        //if (lbl_stock.Text.ToUpper().IndexOf("AVAILABLE") != -1)
        //{
        //    //lbl_stock.ForeColor = System.Drawing.Color.FromName("green");
        //    ((Label)e.Item.FindControl("_lbl_f0")).ForeColor = System.Drawing.Color.FromName("green");
        //}
    }
}
