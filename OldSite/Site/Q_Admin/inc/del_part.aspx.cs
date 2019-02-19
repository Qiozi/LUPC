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

public partial class Q_Admin_inc_del_part : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            switch (CMD)
            {
                case "viewdel":
                    if (this.GetPartIncludeQuantity(SKU) > 0)
                    {
                       // Response.Write(" <a href='' onclick=\"alert('it is used, don't delete.');return false;\" title='del_part'>Del</a>");
                    }
                    else
                        Response.Write(" <a href='' onclick=\"if(confirm('Sure')){js_callpage_cus('/q_admin/inc/del_part.aspx?cmd=is_del&sku="+ SKU.ToString()+"','del_part', 200,200);};return false;\" title='del_part'>Del Part</a>");
                    Response.End();
                    break;
                case "is_del":
                    if (this.GetPartIncludeQuantity(SKU) > 0)
                    {

                        Response.Write("<script>alert('The part don't delete.'); window.close();");
                    }
                    else
                    {
                        DelPartToBackup(SKU);
                        Response.Write("<script>alert('"+SKU.ToString()+" is deleted. ');window.close();</script>");
                    }
                    Response.End();
                    break;
                case "get_quantity":
                    Response.Write(GetPartSaleQuantity(SKU).ToString());
                    Response.End();
                    break;
            }
        }
    }

    public int GetPartSaleQuantity(int sku)
    {
        return Config.ExecuteScalarInt32(string.Format(@"SELECT SUM(c) FROM 
(
	SELECT COUNT(serial_no) c FROM tb_order_product WHERE product_serial_no = '{0}' 
	UNION 
	SELECT COUNT(sys_tmp_detail) FROM tb_order_product_sys_detail WHERE product_serial_no= '{0}' 

) tmp ", sku));
    }

    public int GetPartIncludeQuantity(int sku)
    {
        return Config.ExecuteScalarInt32(string.Format(@"
SELECT SUM(c) FROM 
(
	SELECT COUNT(serial_no) c FROM tb_order_product WHERE product_serial_no = '{0}'  
	UNION 
	SELECT COUNT(sys_tmp_detail) FROM tb_order_product_sys_detail WHERE product_serial_no= '{0}' 
	UNION
	SELECT COUNT(id) FROM tb_ebay_system_parts WHERE luc_sku= '{0}' 
) tmp  ", sku));
    }

    public int SKU
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "sku", -1); }
    }

    public string CMD
    {
        get { return Util.GetStringSafeFromQueryString(Page, "cmd"); }
    }

    public void DelPartToBackup(int sku)
    {
        Config.ExecuteNonQuery(string.Format(@"
INSERT INTO tb_product_del_backup 
	(product_serial_no, 
	product_name, 
	product_short_name, 
	product_store_sum, 
	product_sale_sum, 
	menu_child_serial_no, 
	product_filename, 
	tag, 
	product_current_real_cost, 
	product_current_special_cash_price, 
	product_current_price, 
	product_current_cost, 
	product_ship_price, 
	producter_url, 
	product_order, 
	supplier_sku, 
	manufacturer_part_number, 
	ltd_stock, 
	product_sale_sum_2, 
	product_current_cost_2, 
	product_ship_price_2, 
	product_ebay_name, 
	product_short_name_f, 
	hot, 
	NEW, 
	producter_serial_no_bak, 
	product_compatibility, 
	split_line, 
	product_name_long_en, 
	shopbot_info, 
	product_img_sum, 
	product_size_id, 
	is_non, 
	keywords, 
	shortcomment, 
	COMMENT, 
	producter_serial_no, 
	old_db_id, 
	agent_stock, 
	regdate, 
	other_product_sku, 
	category_idss, 
	export, 
	product_current_discount, 
	last_regdate, 
	part_ebay_price, 
	real_cost_regdate, 
	issue, 
	view_count, 
	model, 
	split_name,
    adjustment
	)
SELECT product_serial_no, 
	product_name, 
	product_short_name, 
	product_store_sum, 
	product_sale_sum, 
	menu_child_serial_no, 
	product_filename, 
	tag, 
	product_current_real_cost, 
	product_current_special_cash_price, 
	product_current_price, 
	product_current_cost, 
	product_ship_price, 
	producter_url, 
	product_order, 
	supplier_sku, 
	manufacturer_part_number, 
	ltd_stock, 
	product_sale_sum_2, 
	product_current_cost_2, 
	product_ship_price_2, 
	product_ebay_name, 
	product_short_name_f, 
	hot, 
	NEW, 
	producter_serial_no_bak, 
	product_compatibility, 
	split_line, 
	product_name_long_en, 
	shopbot_info, 
	product_img_sum, 
	product_size_id, 
	is_non, 
	keywords, 
	shortcomment, 
	COMMENT, 
	producter_serial_no, 
	old_db_id, 
	agent_stock, 
	regdate, 
	other_product_sku, 
	category_idss, 
	export, 
	product_current_discount, 
	last_regdate, 
	part_ebay_price, 
	real_cost_regdate, 
	issue, 
	view_count, 
	model, 
	split_name,
	adjustment

	FROM tb_product WHERE product_serial_no='{0}';

DELETE FROM tb_product 
	WHERE
	product_serial_no = '{0}';
", sku));
    }
}
