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

public partial class Q_Admin_product_view_count_sum : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.lv_cate_view_count_list.DataSource = Config.ExecuteDataTable("select menu_child_name name, view_count from tb_product_category where menu_child_serial_no in (" + new GetAllValidCategory().ToString() + ") order by view_count desc");
            this.lv_cate_view_count_list.DataBind();

            this.lv_part_view_count_list.DataSource = Config.ExecuteDataTable(@"select distinct product_name name , sum(pv.view_count) view_count, max(p.view_count) from tb_part_cate_view_count pv inner join 
tb_product p on p.product_serial_no=pv.luc_sku
 where luc_sku>0 group by luc_sku order by sum(pv.view_count) desc limit 0,100 ");
            this.lv_part_view_count_list.DataBind();
        }
    }
    
}
