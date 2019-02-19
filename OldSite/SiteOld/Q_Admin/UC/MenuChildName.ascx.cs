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

public partial class Q_Admin_UC_MenuChildName : CtrlBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            WebLoad();
        }
    }

    private void WebLoad()
    {
        this.lbl_menu_child_name.Text = ProductCategoryModel.GetProductCategoryModel(menu_child_serial_no).menu_child_name;

    }

    int _menu_child_serial_no;

    public int menu_child_serial_no
    {
        get { return _menu_child_serial_no; }
        set { _menu_child_serial_no = value; }
    }
}
