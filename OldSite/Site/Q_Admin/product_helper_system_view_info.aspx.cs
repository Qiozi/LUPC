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

public partial class Q_Admin_product_helper_system_view_info : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.MenuChildName1.menu_child_serial_no = CategoryID;
        }
    }
    
    #region properties
    public int CategoryID
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "categoryid", -1); }
    }
    #endregion

}
