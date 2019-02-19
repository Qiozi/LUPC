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

public partial class Q_Admin_sale_order_msg : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindMsgDG();
        }
    }

    #region Methods

    private void BindMsgDG()
    {
        this.DataList1.DataSource = ChatMsgModel.FindModelsByOrderCode(DBContext, this.OrderCode);
        this.DataList1.DataBind();

    }
    #endregion

    #region Porperites
    public string OrderCode
    {
        get { return Util.GetStringSafeFromQueryString(Page, "order_code"); }
    }
    #endregion
}
