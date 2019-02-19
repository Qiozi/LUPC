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

public partial class Q_Admin_UC_CustomerMsgList : CtrlBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitialDatabase();
        }
    }
    #region Methods

    protected void InitialDatabase()
    {
        BindMsgDG();
    }


    private void BindMsgDG()
    {
        this.DataList1.DataSource = ChatMsgModel.FindModelsByCount(Count);
        this.DataList1.DataBind();

    }
    #endregion

    #region Porperites

    int _count = 20;

    public int Count
    {
        get { return _count; }
        set { _count = value; }
    }

    #endregion
}
