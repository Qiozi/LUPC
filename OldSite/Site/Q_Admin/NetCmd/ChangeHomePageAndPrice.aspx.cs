using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.IO;

public partial class LUAdmin_NetCmd_ChangeHomePageAndPrice : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Response.ClearHeaders();
            

            if (Util.GetStringSafeFromQueryString(Page, "HomePage") == "1")
            {

                StreamWriter sw = new StreamWriter(Server.MapPath("~/index.html"),false ,System.Text.Encoding.UTF8);
                try
                {
                    var context = new LU.Data.nicklu2Entities();
                    HttpHelper hh = new HttpHelper();
                    string s = hh.GetPageString(context, "", Config.http_domain + "/default_templete.asp");
                    //Response.Write(s);
                    sw.Write(s);
                    sw.Close();
                    sw.Dispose();
                    Response.Write("--------------Home Page Change is OK-------------<br/>");
                }
                catch { sw.Close(); sw.Dispose(); }
            }

        }
    }
}
