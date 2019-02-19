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

public partial class Q_Admin_ebay_list_templete : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitialDatabase();
        }
    }

    public override void InitialDatabase()
    {
        base.InitialDatabase();
        BindGV();
    }

    #region methods

    private void BindGV()
    {
        this.GridView1.DataSource = Config.ExecuteDataTable(@"select id,templete_comment,last_regdate,case 
templete_type when  2 then 'System' when 1 then 'Unit' else '' end as templete_type from tb_ebay_templete");
        this.GridView1.DataBind();
    }
    #endregion

}
