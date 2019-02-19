using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class Q_Admin_ebayMaster_copyLogoToFolder : PageBase
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
        CopyFile(ReqSku);
    }

    void CopyFile(int Sku)
    {
        ProductModel pm = ProductModel.GetProductModel(Sku);
        if (pm != null)
        {
            string filename = Server.MapPath(string.Format("~/pro_img/components/{0}_g_1.jpg", pm.other_product_sku > 0 ? pm.other_product_sku : Sku));

            if (File.Exists(filename))
            {
                string tmpFolder = Server.MapPath("~/pro_img/tmpLogo");
                if (!System.IO.Directory.Exists(tmpFolder))
                    Directory.CreateDirectory(tmpFolder);

                File.Copy(filename, Server.MapPath(string.Format("~/pro_img/tmpLogo/{0}.jpg", pm.other_product_sku > 0 ? pm.other_product_sku : Sku)), true);
            }
            else
                throw new Exception("file no exist");
        }
    }

    int ReqSku
    {
        get
        {
            return Util.GetInt32SafeFromQueryString(Page, "sku", -1);
        }
    }
}