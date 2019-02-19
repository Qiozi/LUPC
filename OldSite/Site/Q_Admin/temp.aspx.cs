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

public partial class Q_Admin_temp : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //DataTable dt = Config.ExecuteDataTable("select distinct case when other_product_sku >0 then other_product_sku else product_serial_no end img_sku, product_serial_no  from tb_product where tag=1 and product_serial_no <27000 ");
            //foreach (DataRow dr in dt.Rows)
            //{
            //    int imgsku;
            //    int.TryParse(dr[0].ToString(), out imgsku);
            //    if (imgsku == 0) continue;

            //    string filename = Server.MapPath(string.Format("~/pro_img/components/{0}_g_1.jpg",imgsku));

            //    if (File.Exists(filename))
            //    {
            //        if (!File.Exists(filename.Replace("g_1", "g_99")))
            //            File.Copy(filename, filename.Replace("g_1", "g_99"), true);
            //        Config.ExecuteNonQuery("Update tb_product set product_short_name_f='" + string.Format("{0}_g_99.jpg", imgsku) + "' where product_serial_no='" + dr["product_serial_no"].ToString() + "'");
            //    }
            //    Response.Write("OK<br/>");
            //}
            new EbayGetXmlHelper().GeteBayDetails();
        }
    }
}
