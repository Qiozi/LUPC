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

public partial class Q_Admin_UC_Navigation : System.Web.UI.UserControl
{
    string _navigation_text = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.SetNavigationText();
        }
    }

    public void SetNavigationText()
    {
        this.lbl_navigation.Text = NavigationText;
    }

    public string NavigationText
    {
        get { return _navigation_text; }
        set { this._navigation_text = value; }
    }
}
