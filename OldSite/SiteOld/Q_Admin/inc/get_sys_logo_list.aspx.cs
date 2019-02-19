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

public partial class Q_Admin_inc_get_sys_logo_list : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            System.IO.DirectoryInfo dirinfo = new System.IO.DirectoryInfo(Server.MapPath("/pro_img/logo/"));
            System.IO.FileInfo[] fileinfo = dirinfo.GetFiles();

            for (int i = 0; i < fileinfo.Length; i++)
            {
                string filename = Config.cpu_logo_image_path + fileinfo[i].Name;
                //Response.Write(@"document.write("""+fileinfo[i].Length.ToString()+@""");");
                if (fileinfo[i].Length < 2500)
                {
                    //Response.Write(@"document.write(""<div style='float:left;margin:1em;'>"");");
                    //Response.Write(@"document.write(""    <img src='" + filename + @"' title='"+fileinfo[i].Name+@"'>"");");
                    //Response.Write(@"document.write(""</div>"");");
                    Response.Write(@"<div style='float:right;margin:1em;padding:1px;'>");
                    Response.Write(@"    <img src='" + filename + @"' title='" + fileinfo[i].Name + @"'>");
                    Response.Write(@"</div>");
                }
            }
            Response.End();
        }
    }
}
