using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class Q_Admin_product_edit_parts : PageBase
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.lv_category_btn.DataSource = Config.ExecuteDataTable("select distinct position, count(id) c from tb_other_inc_shopbot where other_inc_name='lucomputers.com' and position>0 group by position");
            this.lv_category_btn.DataBind();
        }
    }
    

    public void BindPartListLV(int position)
    {
        this.lv_part_list.DataSource = Config.ExecuteDataTable( string.Format(@"select distinct case when product_name_long_en='' then product_name else product_name_long_en end as product_name,
product_current_price-product_current_discount sell, product_current_cost cost, oi.price, oi.position
,menu_child_serial_no, oi.lu_sku, 
(select count(id) from tb_other_inc_shopbot where lu_sku=oi.lu_sku) c
 from tb_other_inc_shopbot oi inner join tb_product p on p.product_serial_no=oi.lu_sku where  position='{0}' order by product_name asc  ", position));
        this.lv_part_list.DataBind();
    }

    protected void lv_category_btn_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "ViewParts":
                BindPartListLV(int.Parse(e.CommandArgument.ToString()));
                break;

        }
    }
}

