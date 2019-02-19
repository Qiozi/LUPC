using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class list_cate : PageBase
{


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            var query = db.tb_product_category.Where(me => 
                                               me.menu_pre_serial_no.HasValue &&
                                               me.menu_pre_serial_no.Value.Equals(ReqCid) &&
                                               me.tag.HasValue &&
                                               me.tag.Value.Equals(1) &&
                                               me.menu_child_serial_no != 378 

                                               )
                                               .OrderBy(me=>me.menu_child_order)
                                               .ToList();

            this.rptList.DataSource = query;
            this.rptList.DataBind();           
        }
    }    

    int ReqCid
    {
        get
        {
            return Util.GetInt32SafeFromQueryString(Page, "cid", 0);
        }
    }
}