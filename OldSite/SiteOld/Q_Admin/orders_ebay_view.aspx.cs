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

public partial class Q_Admin_orders_ebay_view : PageBase
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

        DataTable dt = Config.ExecuteDataTable(@"select 	sales_record_number, user_id, buyer_fullname, buyer_phone_number, 
	buyer_email, 
	buyer_address1, 
	buyer_address2, 
	buyer_city, 
	buyer_province, 
	buyer_postal_code, 
	buyer_country, 
	item_number, 
	item_title, 
	custom_label, 
	quantity, 
	sale_price, 
	sale_price_unit, 
	shipping_and_handling, 
	shipping_and_handling_unit, 
	insurance, 
	insurance_unit, 
	cash_on_delivery_fee, 
	cash_on_delivery_fee_unit, 
	total_price, 
	total_price_unit, 
	payment_method, 
	date_format(sale_date, '%Y-%b-%d') sale_date, 
	date_format(checkout_date, '%Y-%b-%d') checkout_date, 
	date_format(paid_on_date, '%Y-%b-%d') paid_on_date,  
	date_format(shipped_on_date, '%Y-%b-%d') shipped_on_date,  
	feedback_left, 
	feedback_received, 
	notes_to_yourself, 
	paypal_transaction_id, 
	shipping_service, 
	cash_on_delivery_option, 
	transaction_id, 
	order_id, 
	order_code

	 
	from 
	tb_order_ebay where sales_record_number='" + Util.GetInt32SafeFromQueryString(Page, "sales_record_number", -1).ToString() + "'");
        this.rpt_order_info.DataSource = dt;
        this.rpt_order_info.DataBind();

        if (dt.Rows.Count > 0)
        {
            BindProduct(dt.Rows[0]["item_number"].ToString());
        }

    }

    private void BindProduct(string item_number)
    {
        DataTable dt = Config.ExecuteDataTable("Select * from tb_ebay_item_number where item_number='" + item_number + "'");
        if (dt.Rows.Count > 0)
        {
            DataRow dr = dt.Rows[0];
            if (dr["luc_type"].ToString() == "system")
            {
                int system_sku;
                int.TryParse(dr["luc_sku"].ToString(), out system_sku);

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                DataTable sysDT = Config.ExecuteDataTable("Select id, ebay_system_name , ebay_system_price from tb_ebay_system where id='"+ system_sku.ToString()+"'");
                if(sysDT.Rows.Count>0)
                    sb.Append(string.Format("<div style='margin-bottom:5px; padding-bottom:5px;border-bottom:1px dotted #ccc; color:#582EAC'>[{0}]{1}(${2})</div>"
                        , system_sku
                        , sysDT.Rows[0]["ebay_system_name"].ToString()
                        , sysDT.Rows[0]["ebay_system_price"].ToString()));

                DataTable detailDT = Config.ExecuteDataTable(@"
Select esp.luc_sku, p.product_ebay_name, esp.part_quantity from tb_ebay_system_parts esp
	    inner join tb_ebay_system_part_comment ec on ec.id=esp.comment_id
	    inner join tb_product p on p.product_serial_no=esp.luc_sku
	    where esp.system_sku='"+system_sku.ToString()+"'");
                sb.Append("<table>");
                for(int i=0; i<detailDT.Rows.Count; i++)
                {
                    int quantity;
                    int.TryParse(detailDT.Rows[i]["part_quantity"].ToString(), out quantity);

                    sb.Append("     <tr>");

                    sb.Append(string.Format("<td style='width:30px;'>&nbsp;</td><td>({0})</td><td>{1}</td><td>{2}</td>", detailDT.Rows[i]["luc_sku"].ToString()
                        , quantity > 1 ? quantity.ToString()+"X" : "", detailDT.Rows[i]["product_ebay_name"].ToString()));
                  
                }

                this.Literal1.Text = sb.ToString();
                this.lbl_prod_type.Text = "System:";
            }
            else
            {
                this.lbl_prod_type.Text = "Part:";

                int sku;
                int.TryParse(dr["luc_sku"].ToString(), out sku);
                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                DataTable partDT = Config.ExecuteDataTable("Select product_serial_no, product_ebay_name , product_current_price-product_current_discount sell from tb_product where product_serial_no='" + sku.ToString() + "'");
                if (partDT.Rows.Count > 0)
                    sb.Append(string.Format("<div style='margin-bottom:5px; padding-bottom:5px;border-bottom:1px dotted #ccc; color:#582EAC'>[{0}]{1}(${2})</div>"
                        , sku
                        , partDT.Rows[0]["product_ebay_name"].ToString()
                        , partDT.Rows[0]["sell"].ToString()));

                this.Literal1.Text = sb.ToString();

            }
        }
        else
        {
            this.Literal1.Text = "<div style='line-height:50px;text-align:center;'>No Data Match</div>";
        }
    }


}
