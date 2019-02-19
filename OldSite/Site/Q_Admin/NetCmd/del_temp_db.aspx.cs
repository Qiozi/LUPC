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

public partial class Q_Admin_NetCmd_del_temp_db : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if ("qiozi@msn.com" == Util.GetStringSafeFromQueryString(Page, "cmd"))
        {
            this.DeleteCartTemp();
            this.DelCartTempPrice();
            this.DelSpTmp();
            this.DelSpTmpDetail();


            Response.Write("delete temp info is complate.");
        }
    }


    private void DeleteCartTemp()
    {
        Config.ExecuteNonQuery(@"
INSERT INTO tb_cart_temp_del_backup 
	(cart_temp_serial_no, 
	cart_temp_code, 
	product_serial_no, 
	menu_child_serial_no, 
	create_datetime, 
	ip, 
	customer, 
	cart_temp_Quantity, 
	customer_serial_no, 
	shipping_company, 
	state_shipping, 
	shipping_charge, 
	sale_tax, 
	price, 
	is_noebook, 
	product_name, 
	old_price, 
	country_id, 
	pay_method, 
	pick_datetime_1, 
	pick_datetime_2, 
	save_price, 
	price_rate, 
	cost, 
	shipping_state_code, 
	shipping_country_code, 
	price_unit, 
	current_system
)
SELECT
	cart_temp_serial_no, 
	cart_temp_code, 
	product_serial_no, 
	menu_child_serial_no, 
	create_datetime, 
	ip, 
	customer, 
	cart_temp_Quantity, 
	customer_serial_no, 
	shipping_company, 
	state_shipping, 
	shipping_charge, 
	sale_tax, 
	price, 
	is_noebook, 
	product_name, 
	old_price, 
	country_id, 
	pay_method, 
	pick_datetime_1, 
	pick_datetime_2, 
	save_price, 
	price_rate, 
	cost, 
	shipping_state_code, 
	shipping_country_code, 
	price_unit, 
	current_system
	
FROM tb_cart_temp
WHERE 
DATE_FORMAT(create_datetime,'%Y%j') < DATE_FORMAT(DATE_SUB(CURRENT_DATE, INTERVAL 30 DAY), '%Y%j');

DELETE FROM  tb_cart_temp
WHERE 
DATE_FORMAT(create_datetime,'%Y%j') < DATE_FORMAT(DATE_SUB(CURRENT_DATE, INTERVAL 30 DAY), '%Y%j');");
    }

    private void DelSpTmp()
    {
        Config.ExecuteNonQuery(@"
INSERT INTO tb_sp_tmp_del_backup 
	(sys_tmp_serial_no, 
	sys_tmp_code, 
	sys_tmp_price, 
	create_datetime, 
	tag, 
	ip, 
	system_templete_serial_no, 
	email, 
	system_category_serial_no, 
	is_noebook, 
	sys_tmp_cost, 
	sys_tmp_product_name, 
	save_price, 
	old_price, 
	is_old, 
	old_part_id, 
	syst_tmp_price_rate, 
	is_customize, 
	is_templete, 
	price_unit, 
	ebay_number
	)
SELECT sys_tmp_serial_no, 
	sys_tmp_code, 
	sys_tmp_price, 
	create_datetime, 
	tag, 
	ip, 
	system_templete_serial_no, 
	email, 
	system_category_serial_no, 
	is_noebook, 
	sys_tmp_cost, 
	sys_tmp_product_name, 
	save_price, 
	old_price, 
	is_old, 
	old_part_id, 
	syst_tmp_price_rate, 
	is_customize, 
	is_templete, 
	price_unit, 
	ebay_number
FROM tb_sp_tmp
WHERE 
DATE_FORMAT(create_datetime,'%Y%j') < DATE_FORMAT(DATE_SUB(CURRENT_DATE, INTERVAL 30 DAY), '%Y%j');

DELETE FROM  tb_sp_tmp
WHERE 
DATE_FORMAT(create_datetime,'%Y%j') < DATE_FORMAT(DATE_SUB(CURRENT_DATE, INTERVAL 30 DAY), '%Y%j');

");
    }

    private void DelSpTmpDetail()
    {
        Config.ExecuteNonQuery(@"
INSERT INTO tb_sp_tmp_detail_del_backup 
	(sys_tmp_detail, 
	sys_tmp_code, 
	product_serial_no, 
	product_name, 
	cate_name, 
	part_quantity, 
	part_max_quantity, 
	product_current_price, 
	product_current_cost, 
	product_order, 
	system_templete_serial_no, 
	system_product_serial_no, 
	part_group_id, 
	save_price, 
	old_price, 
	re_sys_tmp_detail, 
	product_current_price_rate, 
	product_current_sold, 
	is_lock, 
	ebay_number
	)
SELECT 
	d.sys_tmp_detail, 
	d.sys_tmp_code, 
	d.product_serial_no, 
	d.product_name, 
	d.cate_name, 
	d.part_quantity, 
	d.part_max_quantity, 
	d.product_current_price, 
	d.product_current_cost, 
	d.product_order, 
	d.system_templete_serial_no, 
	d.system_product_serial_no, 
	d.part_group_id, 
	d.save_price, 
	d.old_price, 
	d.re_sys_tmp_detail, 
	d.product_current_price_rate, 
	d.product_current_sold, 
	d.is_lock, 
	d.ebay_number
	
FROM tb_sp_tmp_detail d left JOIN tb_sp_tmp sp on sp.sys_tmp_code=d.sys_tmp_code
WHERE 
sp.create_datetime IS NULL OR 
DATE_FORMAT(sp.create_datetime,'%Y%j') < DATE_FORMAT(DATE_SUB(CURRENT_DATE, INTERVAL 30 DAY), '%Y%j');

DELETE FROM tb_sp_tmp_detail WHERE sys_tmp_detail IN (SELECT sys_tmp_detail FROM tb_sp_tmp_detail_del_backup);");
    }

    private void DelCartTempPrice()
    {
        Config.ExecuteNonQuery(@"
INSERT INTO tb_cart_temp_price_del_backup 
	(tmp_price_serial_no, 
	sub_total, 
	shipping_and_handling, 
	sales_tax, 
	grand_total, 
	order_code, 
	create_datetime, 
	gst, 
	pst, 
	hst, 
	sur_charge_rate, 
	sur_charge, 
	gst_rate, 
	pst_rate, 
	hst_rate, 
	sub_total_rate, 
	shipping_and_handling_rate, 
	sales_tax_rate, 
	grand_total_rate, 
	gst_charge_rate, 
	pst_charge_rate, 
	hst_charge_rate, 
	cost, 
	taxable_total, 
	price_unit
	)
SELECT tmp_price_serial_no, 
	sub_total, 
	shipping_and_handling, 
	sales_tax, 
	grand_total, 
	order_code, 
	create_datetime, 
	gst, 
	pst, 
	hst, 
	sur_charge_rate, 
	sur_charge, 
	gst_rate, 
	pst_rate, 
	hst_rate, 
	sub_total_rate, 
	shipping_and_handling_rate, 
	sales_tax_rate, 
	grand_total_rate, 
	gst_charge_rate, 
	pst_charge_rate, 
	hst_charge_rate, 
	cost, 
	taxable_total, 
	price_unit
	
FROM tb_cart_temp_price
WHERE 
DATE_FORMAT(create_datetime,'%Y%j') < DATE_FORMAT(DATE_SUB(CURRENT_DATE, INTERVAL 30 DAY), '%Y%j');

DELETE FROM tb_cart_temp_price WHERE DATE_FORMAT(create_datetime,'%Y%j') < DATE_FORMAT(DATE_SUB(CURRENT_DATE, INTERVAL 30 DAY), '%Y%j');");
    }
}
