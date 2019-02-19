using System;
using System.Collections;
using System.Configuration;
using System.Data;

using System.Data.OleDb;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class Q_Admin_orders_ebay_import : PageBase
{
    ArrayList ERRORAL = new ArrayList();
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
        BindNOOrderCode();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (this.FileUpload1.PostedFile != null)
        {
            string newFilename = Server.MapPath(string.Format("{0}{1}",
              Config.update_product_data_excel_path,
              string.Format("e_{0}.xls", DateTime.Now.ToString("yyyyMMddhhmmss"))));
            this.FileUpload1.PostedFile.SaveAs(newFilename);

            int upload_error = 0;
            int upload_success = 0;
            int success = 0;
            int error = 0;
            int exist_count = 0;
            using (OleDbConnection conn = new OleDbConnection(Config.ExcelConnstring(newFilename)))
            {
                conn.Open();
                // [Ltd_code],[Ltd_sku],[Ltd_cost],[Ltd_stock],[Ltd_manufacture_code],[Ltd_part_name]
                OleDbDataAdapter da = new OleDbDataAdapter("SELECT * FROM [table$]", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                conn.Close();
                int sale_record_id = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    try
                    {
                        DataRow dr = dt.Rows[i];

                        int sales_record_number;
                        int.TryParse(dr["Sales Record Number"].ToString().Trim(), out sales_record_number);
                        sale_record_id = sales_record_number;
                        if (sales_record_number == 0)
                        {
                            upload_error += 1;
                            throw new Exception("sales record number is zone.");

                        }



                        int up_count = Config.ExecuteScalarInt32("Select count(id) from tb_order_ebay where sales_record_number='" + sales_record_number.ToString() + "'");
                        if (up_count != 0)
                        {
                            upload_error += 1;
                            throw new Exception("ebay order is exist, don't upload");
                        }

                        OrderEbayModel oem = new OrderEbayModel();
                        oem.buyer_address1 = dr["Buyer Address 1"].ToString().Trim();
                        oem.buyer_address2 = dr["Buyer Address 2"].ToString().Trim();
                        oem.buyer_city = dr["Buyer City"].ToString().Trim();
                        oem.buyer_country = dr["Buyer Country"].ToString().Trim();
                        oem.buyer_email = dr["Buyer Email"].ToString().Trim();
                        oem.buyer_phone_number = dr["Buyer Phone Number"].ToString().Trim();
                        oem.buyer_postal_code = dr["Buyer Postal Code"].ToString().Trim();
                        oem.buyer_province = dr["Buyer Province"].ToString().Trim();
                        if (dr["Cash on delivery fee"].ToString().Trim() != "")
                        {
                            string[] cash = dr["Cash on delivery fee"].ToString().Trim().Split(new char[] { '$' });
                            decimal cash_price;
                            decimal.TryParse(cash[1], out cash_price);

                            oem.cash_on_delivery_fee = cash_price;
                            oem.cash_on_delivery_fee_unit = cash[0].Trim();
                        }

                        oem.cash_on_delivery_option = dr["Cash on delivery option"].ToString().Trim();
                        DateTime checkout_date;
                        DateTime.TryParse(dr["Checkout Date"].ToString().Trim(), out checkout_date);
                        oem.checkout_date = checkout_date;
                        oem.custom_label = dr["Custom Label"].ToString().Trim();
                        oem.feedback_left = dr["Feedback left"].ToString().Trim();
                        oem.feedback_received = dr["Feedback received"].ToString().Trim();
                        if (dr["Insurance"].ToString().Trim() != "")
                        {
                            string[] Insurance = dr["Insurance"].ToString().Trim().Split(new char[] { '$' });
                            decimal Insurance_price;
                            decimal.TryParse(Insurance[1], out Insurance_price);

                            oem.insurance = Insurance_price;
                            oem.insurance_unit = Insurance[0].Trim();
                        }
                        oem.item_number = dr["Item Number"].ToString().Trim();
                        oem.item_title = dr["Item Title"].ToString().Trim();
                        oem.notes_to_yourself = dr["Notes to yourself"].ToString().Trim();
                        oem.order_id = dr["Order ID"].ToString().Trim();
                        DateTime paid_on_date;
                        DateTime.TryParse(dr["Paid on Date"].ToString().Trim(), out paid_on_date);
                        oem.paid_on_date = paid_on_date;
                        oem.payment_method = dr["Payment Method"].ToString().Trim();
                        oem.paypal_transaction_id = dr["PayPal Transaction ID"].ToString().Trim();
                        int quantity;
                        int.TryParse(dr["Quantity"].ToString().Trim(), out quantity);
                        oem.quantity = quantity;
                        DateTime sale_date;
                        DateTime.TryParse(dr["Sale Date"].ToString().Trim(), out sale_date);
                        oem.sale_date = sale_date;
                        if (dr["Sale Price"].ToString().Trim() != "")
                        {
                            string[] Sale = dr["Sale Price"].ToString().Trim().Split(new char[] { '$' });
                            decimal Sale_price;
                            decimal.TryParse(Sale[1], out Sale_price);

                            oem.sale_price = Sale_price;
                            oem.sale_price_unit = Sale[0].Trim();
                        }
                       
                        oem.sales_record_number = sales_record_number;
                        DateTime shipped_on_sale;
                        DateTime.TryParse(dr["Shipped on Date"].ToString(), out shipped_on_sale);
                        oem.shipped_on_date = shipped_on_sale;
                        if (dr["Shipping and Handling"].ToString().Trim() != "")
                        {
                            string[] Shipping = dr["Shipping and Handling"].ToString().Trim().Split(new char[] { '$' });
                            decimal Shipping_price;
                            decimal.TryParse(Shipping[1], out Shipping_price);

                            oem.shipping_and_handling = Shipping_price;
                            oem.shipping_and_handling_unit = Shipping[0].Trim();
                        }
                        oem.shipping_service = dr["Shipping Service"].ToString().Trim();
                        if (dr["Total Price"].ToString().Trim() != "")
                        {
                            string[] Total = dr["Total Price"].ToString().Trim().Split(new char[] { '$' });
                            decimal Total_price;
                            decimal.TryParse(Total[1], out Total_price);

                            oem.total_price = Total_price;
                            oem.total_price_unit = Total[0].Trim();
                        }
                        oem.transaction_id = dr["Transaction ID"].ToString().Trim();
                        oem.user_id = dr["User Id"].ToString().Trim();
                        oem.buyer_fullname = dr["Buyer Fullname"].ToString().Trim();
                        oem.Create();
                        upload_success += 1;
                    }
                    catch (Exception ex)
                    {
                        upload_error += 1;
                    ERRORAL.Add(string.Format("<tr><td>{0}</td><td>{1}</td></tr>", sale_record_id, ex.Message));
                    }
                }
            }

            #region save to db.

            DataTable ormDT = Config.ExecuteDataTable(@"select 	id, sales_record_number, user_id, buyer_fullname, buyer_phone_number, 
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
	order_code, 
	regdate
	 
	from 
	tb_order_ebay  where order_code<1");

            for (int i = 0; i < ormDT.Rows.Count; i++)
            {
                DataRow dr = ormDT.Rows[i];
                int error_id = 0;
                try
                {
                    int id;
                    int.TryParse(dr["id"].ToString(), out id);
                    string user_id = dr["user_id"].ToString();

                    DataTable cdt = Config.ExecuteDataTable(string.Format("select customer_serial_no, source from tb_customer where customer_login_name='{0}' ", user_id));

                    int customer_serial_no = 0;

                    if (cdt.Rows.Count == 0)
                    {
                        CustomerModel cm = new CustomerModel();
                        cm.create_datetime = DateTime.Now;
                        cm.customer_address1 = dr["buyer_address1"].ToString() + " " + dr["buyer_address2"].ToString();
                        

                        if (dr["buyer_country"].ToString() == "CA" || dr["buyer_country"].ToString() == "Canada")
                        {
                            cm.customer_country = "1";
                            cm.customer_shipping_country = 1;
                            cm.customer_country_code = "CA";
                            cm.shipping_country_code = "CA";
                        }
                        if (dr["buyer_country"].ToString() == "US" || dr["buyer_country"].ToString() == "United States")
                        {
                            cm.customer_country = "2";
                            cm.customer_shipping_country = 2;
                            cm.customer_country_code = "US";
                            cm.shipping_country_code = "US";
                        }
                        cm.customer_email1 =dr["buyer_email"].ToString();
                        cm.customer_first_name =dr["buyer_fullname"].ToString();
                        cm.customer_last_name = "";
                        cm.customer_city =dr["buyer_city"].ToString();
                        cm.customer_password =dr["buyer_email"].ToString();
                        cm.customer_shipping_address =dr["buyer_address1"].ToString() +" "+dr["buyer_address2"].ToString();
                        cm.customer_shipping_city =dr["buyer_city"].ToString();
                        cm.customer_shipping_first_name =dr["buyer_fullname"].ToString();
                        cm.customer_shipping_last_name = "";

                        int state_id= 0;
                        DataTable stdt = Config.ExecuteDataTable("select state_serial_no from tb_state_shipping where state_code='" + dr["buyer_province"].ToString() + "' or state_name='" + dr["buyer_province"].ToString() + "'");
                        if (stdt.Rows.Count > 0)
                            int.TryParse(stdt.Rows[0][0].ToString(), out state_id);

                        cm.customer_shipping_state = state_id;
                        cm.shipping_country_code = dr["buyer_province"].ToString();
                        cm.customer_login_name =dr["user_id"].ToString();
                        cm.customer_shipping_zip_code =dr["buyer_postal_code"].ToString();
                        cm.EBay_ID =dr["user_id"].ToString();
                        cm.news_latter_subscribe = 1;
                        cm.phone_d =dr["buyer_phone_number"].ToString();
                        cm.phone_n =dr["buyer_phone_number"].ToString();
                        cm.phone_c =dr["buyer_phone_number"].ToString();
                        cm.source = 3;
                        cm.state_serial_no = state_id;
                        cm.state_code = dr["buyer_province"].ToString();
                        cm.tag = 1;
                        cm.zip_code =dr["buyer_postal_code"].ToString();
						cm.customer_serial_no = Code.NewCustomerCode();
                        cm.Create();

                        customer_serial_no = cm.customer_serial_no;
                    }
                    else
                    {
                        if (cdt.Rows[0]["source"].ToString() != "3")
                        {
                            exist_count += 1;
                            throw new Exception("exist");
                        }
                        int.TryParse(cdt.Rows[0][0].ToString(), out customer_serial_no);

                    } 
                  //   int order_code =  OrderHelperModel.GetNewOrderCode();
                    OrderHelperModel oh = new OrderHelperModel();
                    if (dr["sales_record_number"].ToString() != "")
                    {
                        int order_invoice;
                        int.TryParse(dr["sales_record_number"].ToString(), out order_invoice);
                        oh.order_invoice = order_invoice.ToString();
                    }
                    int order_code_sale_record;
                    int.TryParse( oh.order_invoice, out order_code_sale_record);
                   
                    int.TryParse(oh.order_invoice, out error_id);
                   
                    if (dr["sale_date"].ToString() != "")
                    {
                        DateTime _cd;
                        DateTime.TryParse(dr["sale_date"].ToString(), out _cd);
                        oh.create_datetime = _cd;
                    }
                    oh.customer_serial_no = customer_serial_no;

                    if (dr["total_price"].ToString() != "")
                    {
                        decimal total_price;
                        decimal.TryParse(dr["total_price"].ToString(), out total_price);
                        oh.grand_total = total_price;
                        oh.price_unit = dr["total_price_unit"].ToString();
                        oh.current_system = oh.price_unit == "CAD" ? "1" : "2";
                    }

                    oh.is_download_invoice = false;
                    oh.is_lock_input_order_discount = false;
                    oh.is_lock_shipping_charge = false;
                    oh.is_ok = true;
                    
                    oh.order_code = order_code_sale_record;
                    if (dr["sale_date"].ToString() != "")
                    {
                        DateTime _sale_date;
                        DateTime.TryParse(dr["sale_date"].ToString(), out _sale_date);
                        oh.order_date = _sale_date;
                    }
                   
                    if (int.Parse(dr["paid_on_date"].ToString().Substring(0, 4)) > 0)
                    {
                        oh.order_pay_status_id = 2;
                        Config.ExecuteNonQuery(string.Format(@" insert into tb_order_pay_record 
	( order_code, pay_regdate, pay_cash, regdate,pay_record_id)
	values
	( '{0}', '{1}', '{2}', now(), '{3}')", order_code_sale_record, oh.order_date.ToString("yyyy-MM-dd"), oh.grand_total, 15));
                    }
                    else
                        oh.order_pay_status_id = 1;
                    oh.order_source = 3;
                    oh.out_status = byte.Parse(Config.DefaultOutStatus.ToString());
                    oh.pre_status_serial_no = int.Parse(Config.new_order_status);
                    if (dr["payment_method"].ToString() == "PayPal")
                    {
                       
                        oh.pay_method = "15";
                   }

                    if (dr["shipping_and_handling"].ToString() != "")
                    {
                        decimal shipping_and_handling;
                        decimal.TryParse(dr["shipping_and_handling"].ToString(), out shipping_and_handling);
                        oh.shipping_charge = shipping_and_handling;
                    }
                    oh.tag = byte.Parse("1");
                    oh.Create();
                    Config.ExecuteNonQuery("Update tb_order_ebay set order_code='" + order_code_sale_record.ToString() + "' where id='" + id.ToString() + "'");
                    success += 1;
                    CustomerHelper ch = new CustomerHelper();
                    ch.CopyCustomer(order_code_sale_record.ToString(), customer_serial_no);

                    if(oh.pay_method == "15")
                        Config.ExecuteNonQuery("Update tb_customer_store set pay_method=15 where order_code='" + order_code_sale_record.ToString() + "'");
                 
                    this.InsertTraceInfo("Create ebay Order (" + order_code_sale_record.ToString() + ")");

                    //
                    // copy product to store.
                    //
                    int quantity;
                    int.TryParse(ormDT.Rows[0]["quantity"].ToString(), out quantity);
                    CopyProduct(ormDT.Rows[0]["item_number"].ToString(), oh.order_code.ToString(), quantity, ormDT.Rows[0]["item_title"].ToString()); 
                    
                }
                catch(Exception ex) {
                    ERRORAL.Add(string.Format("<tr><td>{0}</td><td>{1}</td></tr>", error_id, ex.Message));
                    error += 1; }
            }

            #endregion

            CH.Alert(string.Format("上传成功：{0}<b>|</b>上传错误: {1}<b>|</b>导入成功: {2}<b>|</b>导入错误：{3} <br>user id已存在:{4}", upload_success, upload_error, success, error, exist_count), this.Literal1);
            this.Literal2.Text = "<table>";
            for (int i = 0; i < ERRORAL.Count; i++)
            {
                this.Literal2.Text += ERRORAL[i].ToString();
            }
            System.IO.File.Delete(newFilename);
            this.Literal2.Text += "</table>";
            BindNOOrderCode();
        }


    }

    private void CopyProduct(string item_number, string order_code, int sum, string product_name)
    {
        DataTable dt = Config.ExecuteDataTable("Select * from tb_ebay_item_number where item_number='" + item_number + "'");
        if (dt.Rows.Count > 0)
        {
            DataRow dr = dt.Rows[0];
            if (dr["luc_type"].ToString() == "system")
            {
                int system_sku;
                int.TryParse(dr["luc_sku"].ToString(), out system_sku);
                string system_code8 = SpTmpDetailModel.GetNewCode(system_sku).ToString();
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                DataTable sysDT = Config.ExecuteDataTable("Select id, ebay_system_name , ebay_system_price, category_id from tb_ebay_system where id='" + system_sku.ToString() + "'");
                if (sysDT.Rows.Count > 0)
                {
                    OrderProductModel opm = new OrderProductModel();
                    opm.order_code = order_code;
                    decimal sell;
                    decimal.TryParse(sysDT.Rows[0]["ebay_system_price"].ToString(), out sell);
                    opm.order_product_sold = sell;
                    opm.order_product_sum = sum;
                    opm.product_name = product_name;
                    
                    opm.product_serial_no = int.Parse(system_code8);
                    opm.product_type_name = "System";
                    opm.product_type = 2; // system;
                    opm.tag = byte.Parse("1");
                    int category_id;
                    int.TryParse(sysDT.Rows[0]["category_id"].ToString(), out category_id);
                    opm.menu_child_serial_no = category_id;
                    opm.Create();
                }

                DataTable detailDT = Config.ExecuteDataTable(@"
Select esp.luc_sku, p.product_ebay_name, esp.part_quantity, ec.comment
, ec.id comment_id
, esp.max_quantity
, ec.priority
, esp.id system_product_serial_no
from tb_ebay_system_parts esp
	    inner join tb_ebay_system_part_comment ec on ec.id=esp.comment_id
	    inner join tb_product p on p.product_serial_no=esp.luc_sku
	    where esp.system_sku='" + system_sku.ToString() + "'");
                sb.Append("<table>");
                for (int i = 0; i < detailDT.Rows.Count; i++)
                {
                    int quantity;
                    int.TryParse(detailDT.Rows[i]["part_quantity"].ToString(), out quantity);

                    OrderProductSysDetailModel opsdm = new OrderProductSysDetailModel();
                    opsdm.cate_name = detailDT.Rows[i]["comment"].ToString();
                    opsdm.ebay_number = item_number;
                    int comment_id;
                    int.TryParse(detailDT.Rows[i]["comment_id"].ToString(), out comment_id);
                    opsdm.part_group_id = comment_id;
                    int part_quantity ;
                    int.TryParse(detailDT.Rows[i]["part_quantity"].ToString(), out part_quantity);
                    int max_quantity;
                    int.TryParse(detailDT.Rows[i]["max_quantity"].ToString(), out max_quantity);
                    opsdm.part_quantity = part_quantity;
                    opsdm.product_name = detailDT.Rows[i]["product_ebay_name"].ToString();
                    int priority;
                    int.TryParse(detailDT.Rows[i]["priority"].ToString(), out priority);
                    opsdm.product_order = priority;
                    int lu_sku;
                    int.TryParse(detailDT.Rows[i]["luc_sku"].ToString(), out lu_sku);
                    opsdm.product_serial_no = lu_sku;
                    int system_product_serial_no;
                    int.TryParse(detailDT.Rows[i]["system_product_serial_no"].ToString(),out system_product_serial_no);
                    opsdm.system_product_serial_no = system_product_serial_no;
                    opsdm.sys_tmp_code = system_code8;
                    opsdm.Create();
                }
                //SystemCodeStoreModel scs = new SystemCodeStoreModel();
                //scs.system_code = int.Parse(system_code8);
                //scs.create_datetime = DateTime.Now;
                //scs.is_buy = true;
                //scs.system_templete_serial_no = system_sku;
                //scs.Create();

            }
            else
            {
               
                int sku;
                int.TryParse(dr["luc_sku"].ToString(), out sku);
                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                DataTable partDT = Config.ExecuteDataTable(@"Select product_serial_no
, case when product_ebay_name <>'' then product_ebay_name else product_name_long_en end as product_ebay_name
, product_current_cost
, product_current_price
, menu_child_serial_no
                    , product_current_price-product_current_discount sell 
from tb_product where product_serial_no='" + sku.ToString() + "'");
                if (partDT.Rows.Count > 0)
                {
                    OrderProductModel opm = new OrderProductModel();
                    opm.order_code = order_code;
                    decimal sell;
                    decimal.TryParse(partDT.Rows[0]["sell"].ToString(), out sell);
                    opm.order_product_sold = sell;
                    opm.order_product_sum = sum;
                    opm.product_name = product_name;

                    decimal cost;
                    decimal.TryParse(partDT.Rows[0]["product_current_cost"].ToString(), out cost);
                    opm.order_product_cost = cost;

                    decimal price;
                    decimal.TryParse(partDT.Rows[0]["product_current_price"].ToString(), out price);
                    opm.order_product_price = price;

                    opm.product_serial_no = sku;
                    opm.product_type_name = "Unit";
                    opm.product_type = 1; // system;
                    opm.tag = byte.Parse("1");
                    int category_id;
                    int.TryParse(partDT.Rows[0]["menu_child_serial_no"].ToString(), out category_id);
                    opm.menu_child_serial_no = category_id;
                    opm.Create();
                }

            }
        }
        else
        {
            //this.Literal1.Text = "<div style='line-height:50px;text-align:center;'>No Data Match</div>";
        }
    }

    public void BindNOOrderCode()
    {
        this.GridView1.DataSource = Config.ExecuteDataTable(@"select 	id, sales_record_number, user_id, buyer_fullname, buyer_phone_number, 
	buyer_email, 
	buyer_address1, 
	buyer_address2, 
	buyer_city, 
	buyer_province, 
	buyer_postal_code, 
	buyer_country, 
	item_number, 
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
	order_code, 
	regdate
	 
	from 
	tb_order_ebay  where order_code<1");
        this.GridView1.DataBind();
    }
   
}
