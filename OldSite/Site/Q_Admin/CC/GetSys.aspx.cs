using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Q_Admin_CC_GetSys : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadWeb();
        }
    }

    void loadWeb()
    {
        Response.Clear();
        switch (ReqCmd)
        {
            case "getsys":
                WriteSys();
                break;
        }

        Response.End();

    }

    void WriteSys()
    {
        DataTable dt = Config.ExecuteDataTable(@"select luc_sku, epc.comment, p.product_ebay_name
, case when p.other_product_sku>0 then p.other_product_sku else p.product_serial_no end img_sku
,esp.part_group_id
,p.product_img_sum imageQty
,p.product_current_price price
 from tb_ebay_system_parts esp inner join tb_product p on esp.luc_sku=p.product_serial_no
inner join tb_ebay_system_part_comment epc on epc.id=esp.comment_id 
where esp.system_sku='" + ReqSKU + "' and is_belong_price =1 order by epc.priority asc ");
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("<?xml version=\"1.0\" encording = \"utf-8\" ?>");
        foreach (DataRow dr in dt.Rows)
        {
            sb.Append("<Part>");
            sb.Append(string.Format("<sku>{0}</sku>", dr["luc_sku"].ToString()));
            sb.Append(string.Format("<title>{0}</title>", dr["product_ebay_name"].ToString()));
            sb.Append(string.Format("<price>{0}</price>", dr["price"].ToString()));
            sb.Append(string.Format("<commentName>{0}</commentName>", dr["comment"].ToString()));
            sb.Append(string.Format("<partGroupID>{0}</partGroupID>", dr["part_group_id"].ToString()));
            sb.Append(string.Format("<logo>{0}</logo>", (dr["img_sku"].ToString() == "16684") ? "" : "http://localhost/gallery/" + dr["img_sku"].ToString() + "_t.jpg"));
            sb.Append(string.Format("<image>{0}</image>", (dr["img_sku"].ToString() == "16684") ? "" : "http://localhost/gallery/" + dr["img_sku"].ToString() + "_t.jpg"));
            //sb.Append("<gallery>");
            //int imageQty;
            //int.TryParse(dr["imageQty"].ToString(), out imageQty);
            //for (int i = 1; i <= imageQty; i++)
            //    sb.Append(string.Format("<image>{0}</image>", "http://50.63.54.200/pro_img/components/" + dr["img_sku"].ToString() + "_list_" + i.ToString() + ".jpg"));

            //sb.Append("</gallery>");
            sb.Append("</Part>");
        }
        sb.Append(string.Format(@"<option
viewImgNum="""+ (dt.Rows.Count-2) +@"""
detailImgNum=""4""
xMain=""570""
yMain=""450""
xThumb=""55""
yThumb=""315""
xSpaceThumb=""13""
thumbXsize=""90""
thumbYsize=""70""
maskMargin=""30""
xDetail=""570""
yDetail=""737""
xSpaceDetail=""83""
/>"));
        Response.Write(sb.ToString());
    }

    string ReqCmd
    {
        get { return Util.GetStringSafeFromQueryString(Page, "cmd"); }
    }

    string ReqSKU
    {
        get { return Util.GetStringSafeFromQueryString(Page, "sku"); }
    }
}