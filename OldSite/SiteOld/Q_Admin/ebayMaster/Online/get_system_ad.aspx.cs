using System;
using System.Data;
using System.IO;

public partial class Q_Admin_ebayMaster_Online_get_system_ad : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (CMD.ToLower() == "qiozi@msn.com")
            {
                string filename = Server.MapPath("/soft_img/eBayXml/ebayFlashAD.xml");
                if (System.IO.File.Exists(filename))
                {
                    FileInfo fi = new FileInfo(filename);
                    if (fi.LastWriteTime.ToShortDateString() == DateTime.Now.ToShortDateString())
                    {
                        Response.Write(FileHelper.ReadFile(filename));
                    }
                    else
                    {
                        Response.Write(CreateFile(filename));
                    }
                }
                else
                {
                    Response.Write(CreateFile(filename));
                }
            }
        }
        Response.End();
    }

    string CreateFile(string filename)
    {
        //        DataTable dt = Config.ExecuteDataTable(@"select itemid, title
        //, (select max(ep.luc_sku) from tb_ebay_system_parts ep inner join tb_ebay_system_part_comment ec on ec.id=ep.comment_id and is_case=1 where system_sku = s.id) caseSKU from tb_ebay_selling e inner join tb_ebay_system s on s.id=e.sys_sku where is_from_ebay=0 and sys_sku>0 order by quantityavailable asc limit 10");

        var dt = Config.ExecuteDataTable("select distinct ebayItemid, product_name, product_serial_no from tb_order_product where menu_child_serial_no=350 and length(order_code)=5 order by serial_no desc limit 10");
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("<result>");
        foreach (DataRow dr in dt.Rows)
        {
            var sku = int.Parse(dr["product_serial_no"].ToString());
            var pm = ProductModel.GetProductModel(sku);
            sb.Append(string.Format("<item><itemid>{0}</itemid><comment><![CDATA[{1}]]></comment><imgSKU>http://www.lucomputers.com//pro_img/ebay_gallery/{3}/{2}_ebay_list_t_1.jpg</imgSKU></item>"
                , dr["ebayItemid"].ToString()
                , dr["product_name"].ToString()
                , pm.other_product_sku > 0 ? pm.other_product_sku : pm.product_serial_no
                , pm.other_product_sku > 0 ? pm.other_product_sku.ToString().Substring(0, 1) : pm.product_serial_no.ToString().Substring(0, 1)));
        }
        sb.Append("</result>");

        //if (File.Exists(filename))
        {
            StreamWriter sw = new StreamWriter(filename, false);
            sw.Write(sb.ToString());
            sw.Close();

        }
        return sb.ToString();
    }

    public string CMD
    {
        get { return Util.GetStringSafeFromQueryString(this, "cmd"); }
    }
}