using System;
using System.Data;
using System.IO;
using System.Collections.Generic;

public partial class Q_Admin_NetCmd_export_all_ebay_sys_to_xml : System.Web.UI.Page
{
    List<string> _partSKU = new List<string>();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            WriteEbaySys();
        }
    }

    void WriteEbaySys()
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();

        DataTable pdt = Config.ExecuteDataTable("select id from tb_ebay_system where showit=1 and is_online=1 order by id desc limit 0,10");
        sb.Append("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
        sb.Append("<data>");
        foreach (DataRow pdr in pdt.Rows)
        {
            string cateName = "";
            string itemID = "";
            string sysSku = pdr["id"].ToString();
            DataTable acronymDT = Config.ExecuteDataTable(@"select menu_child_name 
                                                            from tb_ebay_system_and_category es 
                                                                    inner join tb_product_category pc 
                                                                        on pc.menu_child_Serial_no=es.eBaySysCategoryID 
                                                            where systemsku='" + sysSku + "' limit 0,1");
            if (acronymDT.Rows.Count > 0)
                cateName = acronymDT.Rows[0][0].ToString();

            DataTable ecodeDT = Config.ExecuteDataTable("select ebay_code from tb_ebay_code_and_luc_sku where sku='" + sysSku + "' order by id desc limit 0,1");
            if (ecodeDT.Rows.Count > 0)
                itemID = ecodeDT.Rows[0][0].ToString();

            sb.Append("<item sku='" + pdr["id"].ToString() + "' itemid='" + itemID + "' cateName='" + cateName + "' cateNameAcronym='" + (cateName.Length > 5 ? cateName.Substring(0, 5) : "") + "' adjust='20'>");

            DataTable dt = Config.ExecuteDataTable(string.Format(@"select ep.id detail_id, p.product_serial_no, p.product_current_price-p.product_current_discount price, 
case when length(p.product_ebay_name)>5 then p.product_ebay_name
      else p.product_name end as product_name
, ep.part_group_id, ep.comment_id , c.comment
, case when p.other_product_sku > 1 then p.other_product_sku
    else p.product_serial_no end as img_sku
    ,ep.is_label_of_flash
, c.is_mb, c.is_cpu
, c.is_video
, c.is_audio
, c.is_network
, c.is_cpu_fan
, p.manufacturer_part_number mfp
, p.producter_serial_no brand
, p.UPC
 from tb_ebay_system_parts ep inner join tb_product p on p.product_serial_no=ep.luc_sku 
 inner join tb_ebay_system_part_comment c on c.id=ep.comment_id
where system_sku='{0}' and ep.part_group_id > 0 and ep.is_belong_price=1 order by c.priority asc ", pdr[0].ToString()));

            
            foreach (DataRow dr in dt.Rows)
            {
                AddSKU(dr["product_serial_no"].ToString());

                sb.Append("<ItemRow>");
                sb.Append(string.Format("<SKU>{0}</SKU>", dr["product_serial_no"].ToString()));
                sb.Append(string.Format("<GroupName><![CDATA[{0}]]></GroupName>", dr["comment"].ToString()));
                sb.Append(string.Format("<MFP>{0}</MFP>", dr["mfp"].ToString()));
                sb.Append(string.Format("<Brand><![CDATA[{0}]]></Brand>", dr["brand"].ToString()));
                sb.Append(string.Format("<UPC>{0}</UPC>", dr["UPC"].ToString()));
                sb.Append(string.Format("<eBayItemId>{0}</eBayItemId>", ""));
                sb.Append(string.Format("<Adjustment >{0}</Adjustment>", "0"));
                sb.Append(string.Format("<Price>{0}</Price>", dr["price"].ToString()));
                sb.Append(string.Format("<ImageSmall>{0}</ImageSmall>",""));
                sb.Append(string.Format("<ImageBig>{0}</ImageBig>", "http://www.lucomputers.com/pro_img/source_components/" + dr["img_sku"].ToString() + ".jpg"));
                sb.Append(string.Format("<PartTitle><![CDATA[{0}]]></PartTitle>", dr["product_name"].ToString()));//dr["product_name"].ToString()));

                decimal curr_price;
                decimal.TryParse(dr["price"].ToString(), out curr_price);

                sb.Append("<GroupDetail sku='" + dr["part_group_id"].ToString() + "'>");

                DataTable cdt = Config.ExecuteDataTable(string.Format(@"select p.product_serial_no, p.product_ebay_name, p.product_current_price-p.product_current_discount price 
, case when p.other_product_sku > 1 then p.other_product_sku
    else p.product_serial_no end as img_sku
, p.manufacturer_part_number mfp
, p.producter_serial_no brand
, p.UPC
from tb_product p 
inner join tb_part_group_detail pgd on pgd.part_group_id ='{0}' and pgd.product_serial_no = p.product_serial_no where p.tag=1"
                , dr["part_group_id"].ToString()));

                foreach (DataRow cdr in cdt.Rows)
                {
                    AddSKU(cdr["product_serial_no"].ToString());

                    decimal price;
                    decimal.TryParse(cdr["price"].ToString(), out price);

                    decimal diff_price;
                    decimal.TryParse((price - curr_price).ToString("###.0"), out diff_price);

                    sb.Append("<Part>");
                    sb.Append(string.Format("<SKU>{0}</SKU>", cdr["product_serial_no"].ToString()));
                    sb.Append(string.Format("<eBayItemId>{0}</eBayItemId>", ""));
                    sb.Append(string.Format("<Price>{0}</Price>", price));
                    sb.Append(string.Format("<Adjustment >{0}</Adjustment>", "0"));
                    sb.Append(string.Format("<MFP>{0}</MFP>", cdr["mfp"].ToString()));
                    sb.Append(string.Format("<Brand>{0}</Brand>", cdr["brand"].ToString()));
                    sb.Append(string.Format("<UPC>{0}</UPC>", cdr["UPC"].ToString()));
                    sb.Append(string.Format("<eBayItemId>{0}</eBayItemId>", ""));
                    sb.Append(string.Format("<ImageSmall>{0}</ImageSmall>", ""));
                    sb.Append(string.Format("<ImageBig>{0}</ImageBig>", "http://www.lucomputers.com/pro_img/source_components/" + cdr["img_sku"].ToString() + ".jpg"));
                    sb.Append(string.Format("<PartTitle><![CDATA[{0}]]></PartTitle>", cdr["product_ebay_name"].ToString()));// cdr["product_ebay_name"].ToString()));
                    sb.Append("</Part>");
                }
                sb.Append("</GroupDetail>");

                sb.Append("</ItemRow>");
            }
            sb.Append("</item>");
        }
        sb.Append("</data>");

        StreamWriter sw = new StreamWriter("e:\\ebaySys.xml");
        sw.Write(sb.ToString());
        sw.Close();
        sw.Dispose();
        ExportDesc();
    }

    void ExportDesc()
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
        sb.Append("<data>");
        foreach (var sku in _partSKU)
        {
            sb.Append("<item sku='" + sku + "'>");
            sb.Append(string.Format("<description><![CDATA[{0}]]></description>", GetPartDesc(sku)));
            sb.Append("</item>");
        }
        sb.Append("</data>");

        StreamWriter sw = new StreamWriter("e:\\ebaySysDesc.xml");
        sw.Write(sb.ToString());
        sw.Close();

    }

    string GetPartDesc(string sku)
    {
        return "Location is sku ("+ sku +") description: ..........";
    }

    void AddSKU(string sku)
    {
        if (!_partSKU.Contains(sku))
        {
            _partSKU.Add(sku);
        }
    }
}