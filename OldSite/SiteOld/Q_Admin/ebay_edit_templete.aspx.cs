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

public partial class Q_Admin_ebay_edit_templete : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            InitialDatabase();
    }

    public override void InitialDatabase()
    {
        base.InitialDatabase();
        BindControls(TempleteID);
    }

    #region Methods
    private void BindControls(int id)
    {

    }
    #endregion

    #region properties
    public int TempleteID
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "id", -1); }
    }
    #endregion

  
}
