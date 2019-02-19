using System;

public partial class Q_Admin_UC_CustomerMsg : CtrlBase
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

    public void BindMsgDG()
    {
        var context = new LU.Data.nicklu2Entities();
        this.DataList1.DataSource = ChatMsgModel.FindModelsByOrderCode(context, this.OrderCode);
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
