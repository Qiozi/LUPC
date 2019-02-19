using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Q_Admin_NetCmd_GetEbayActiveItems : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (ReqCMD == "Qiozi@msn.com")
            {
                GetEbayActiveItems();
                Response.Write("OK, " + DateTime.Now.ToString());

                if (ReqIsClose)
                {
                    Response.Write("<html><head></head><body><script>this.close();</script></body></html>");
                    Response.End();
                }
            }
        }
    }

    void GetEbayActiveItems()
    {
        eBayGetActiveItems egai = new eBayGetActiveItems(this);
        egai.GetMySelling(1);
        egai.ReadPage();
        // egai.analyse();
        egai.UpdateDB();
        System.IO.StreamWriter sw = new System.IO.StreamWriter(Server.MapPath("~/soft_img/match_ebay_price.txt"),false);        
        sw.WriteLine(DateTime.Now.ToString());
        sw.Close();
    }

    string ReqCMD
    {
        get { return Util.GetStringSafeFromQueryString(Page, "cmd"); }
    }
    bool ReqIsClose
    {
        get { return Util.GetStringSafeFromQueryString(Page, "isclose") == "1"; }
    }
}