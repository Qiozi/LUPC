using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Q_Admin_NetCmd_StatInc : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["QioziCommand"].ToString() == "qiozi@msn.com")
            {
                OtherIncPartInfoModel oipim = new OtherIncPartInfoModel();
                oipim.RunAmount();

                Config.ExecuteNonQuery("Update tb_other_inc_amount_date set run_regdate=now()");
                Response.Write("OK ======" + DateTime.Now.ToString());
       
            }
        }
    }

    
}
