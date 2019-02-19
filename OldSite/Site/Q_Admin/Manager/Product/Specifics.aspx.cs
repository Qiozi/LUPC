using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Q_Admin_Manager_Product_Specifics : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            string filename = "C:\\Workspaces\\Web\\Part_Comment\\" + ReqSKU + "_comment.html";
            if (File.Exists(filename))
            {
                string cont = File.ReadAllText(filename);

                this.ltDesc.Text = cont;
            }
        }
    }

    public int ReqSKU
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "sku", 0); }
    }
}