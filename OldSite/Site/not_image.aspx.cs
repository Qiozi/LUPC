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
using System.IO;

public partial class not_image : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            DirectoryInfo dir = new DirectoryInfo(Server.MapPath("~/pro_img/components/"));
            DataTable pm = ProductModel.FindAllProductID();
            string id = "";
            string other_id = "";
            int sum = 0;
            for(int i=0;i<pm.Rows.Count; i++)
            {
                id = pm.Rows[i][0].ToString();
                FileInfo[] fi = dir.GetFiles(id + "_*", SearchOption.TopDirectoryOnly);
                if (fi.Length == 0)
                {
                    other_id = pm.Rows[i][1].ToString();
                    if (other_id != "0")
                    {
                        FileInfo[] fi2 = dir.GetFiles(other_id + "_*", SearchOption.TopDirectoryOnly);
                        if (fi2.Length == 0)
                        {
                            Response.Write(id + ",");
                            sum += 1;
                            if (sum % 10 == 0)
                                Response.Write("<br>");
                        }
                        
                    }
                }
                

            }
            Response.Write("<br>---------------------------------------" + sum.ToString() + "-----------------");
            

        
        }
    }
}
