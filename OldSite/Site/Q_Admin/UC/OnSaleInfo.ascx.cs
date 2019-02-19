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

public partial class Q_Admin_UC_OnSaleInfo : CtrlBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitialDatabase();
        }
    }

    #region Methods
    public void InitialDatabase()
    {
        SetSumLBL();
    }

    private void SetSumLBL()
    {
        OnSaleModel os = new OnSaleModel();
        DataTable dt = os.FindYestodayAndTodaySum();
        this.lbl_today_sum.Text = dt.Rows[0][1].ToString();
        this.lbl_yestoday_sum.Text = dt.Rows[0][0].ToString();
    }
    #endregion
}
