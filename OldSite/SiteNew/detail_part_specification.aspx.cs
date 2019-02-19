using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class detail_part_specification : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (ReqSKU > 0)
            {
                //string filename = Server.MapPath("~/Computer/Part_comment/" + ReqSKU + "_comment.html");
                string filename = "C:\\Workspaces\\Web\\Part_Comment\\" + ReqSKU + "_comment.html";
                if (File.Exists(filename))
                {
                    string cont = File.ReadAllText(filename);
                    if (cont.IndexOf("width=\"565\"") > 1)
                        cont = cont.Replace("width=\"565\"", "width='100%'");
                    if (cont.IndexOf("width=\"575\"") > 1)
                        cont = cont.Replace("width=\"575\"", "width='100%'");
                    ltSpecinfication.Text = cont;
                }
            }
        }
    }
}