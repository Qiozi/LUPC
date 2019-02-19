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

public partial class Q_Admin_NetCmd_Create_Part_Json : System.Web.UI.Page
{
    const string JSON_PART = "PART_JSON";
    protected void Page_Load(object sender, EventArgs e)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        if (!IsPostBack)
        {
            if (Request.QueryString["QioziCommand"].ToString() == "qiozi@msn.com")
            {
                DataTable dt = Config.ExecuteDataTable("select * from tb_ebay_system_part_comment where showit=1 order by priority asc");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    int commentID;
                    int.TryParse(dt.Rows[i]["id"].ToString(), out commentID);

                    DataTable pdt = Config.ExecuteDataTable(string.Format(@"select product_serial_no,  case when product_ebay_name <> '' then product_ebay_name 
    when product_name_long_en <> '' then product_name_long_en
    when product_name <> '' then product_name 
    else
    product_short_name end as product_ebay_name 
, menu_child_name
    from tb_product p inner join tb_product_category pc on pc.menu_child_serial_no=p.menu_child_serial_no where p.menu_child_serial_no in ({0}) and ((p.tag=1 and p.split_line=0 and p.is_non=0 and pc.tag=1) or p.is_non=1) order by p.product_current_price asc  ", dt.Rows[i]["category_ids"].ToString().Replace("|",",")));
                    sb.Append("var "+ JSON_PART+"_"+ commentID.ToString()+" = [ ");
                    for (int j = 0; j < pdt.Rows.Count; j++)
                    {
                        sb.Append(string.Format("{{ name: \"{0}\", sku: \"{1}\" }},", pdt.Rows[j]["product_ebay_name"].ToString().Replace("\\", "\\\\").Replace("\"", "\\\"").Replace("'", ""), pdt.Rows[j]["product_serial_no"].ToString()));
                    }
                    sb.Remove(sb.ToString().Length - 1, 1);
                    sb.Append("];");


                }

                System.IO.StreamWriter sw = new System.IO.StreamWriter(Server.MapPath("/q_admin/xmlStore/part_name_data.js"));
                sw.Write(sb.ToString());
                sw.Close();
                sw.Dispose();
                //Response.Write(sb.ToString());
            }
        }
    }
}
