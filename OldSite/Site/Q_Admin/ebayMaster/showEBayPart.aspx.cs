using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class Q_Admin_ebayMaster_showEBayPart : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsCallback)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            DirectoryInfo dir = new DirectoryInfo(Server.MapPath("~/pro_img/components/"));
            FileInfo[] fis = dir.GetFiles();
            foreach (FileInfo f in fis)
            {
                if (f.Name.ToLower().IndexOf("_g_1.") > -1)
                {
                    string sku = f.Name.Split(new char[] { '_' })[0];
                    sb.Append("<span class='showpart' imgsku='" + sku + "'>" + f.Name + "</span><span></span><br>");
                }
            }
            ltFilename.Text = sb.ToString();
        }

    }
}