using System;
using System.IO;


public partial class Q_Admin_ebayMaster_Online_get_system_part_short_desc : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Response.ClearContent();
            if ("qiozi@msn.com" == Util.GetStringSafeFromQueryString(this,"cmd"))
            {
                string lu_sku = Util.GetStringSafeFromQueryString(this, "lu_sku");
                string commFullFile = string.Format("{0}{1}_comment_short.html", Server.MapPath(Config.Part_Comment_Path), lu_sku);
                if (System.IO.File.Exists(commFullFile))
                {
                    string desc = FileHelper.ReadFile(commFullFile);
                    if (desc.Trim() != "")
                    {
                        Response.Write(@"<result><desc><![CDATA[" + desc + "]]></desc></result>");
                    }
                    else
                    {
                        int sku;
                        int.TryParse(lu_sku, out sku);
                        ProductModel pm = ProductModel.GetProductModel(sku);
                        Response.Write("<result><desc><![CDATA[" + (pm.product_name_long_en.Length < 6 ? pm.product_ebay_name : pm.product_name_long_en) + "]]></desc></result>");
                    }
                }
                else
                {
                    int sku;
                    int.TryParse(lu_sku, out sku);
                    ProductModel pm = ProductModel.GetProductModel(sku);
                    Response.Write("<result><desc><![CDATA[" + (pm.product_name_long_en.Length < 6 ? pm.product_ebay_name : pm.product_name_long_en) + "]]></desc></result>");
                }
            }
            //sw.Write(str);
            //sw.Close();
            Response.End();

        }
    }
}