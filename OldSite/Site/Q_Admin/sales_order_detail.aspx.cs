using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Linq;
using LU.Data;

public partial class Q_Admin_sales_order_detail : PageBase
{
    LtdHelper LH = new LtdHelper();
    bool show_price = false;
    bool show_history = false;
    //bool show_msg = true;
    int _webStatus = 0;
    int OrderHelperID
    {
        get
        {
            object _id = ViewState["OrderHelperID"];
            if (_id != null)
                return int.Parse(_id.ToString());
            return -1;
        }
        set { ViewState["OrderHelperID"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int order_code;
            int.TryParse(ReqOrderCode, out order_code);

            if (order_code == 0)
            {
                ltNote.Text = "<div style='padding:5em; color:red;'>no find order.</div><script>$('#showarea').css('display','none');</script> ";
            }
            else
            {
                var count = Config.ExecuteScalarInt32("Select count(*) from tb_order_helper where order_code='" + order_code + "'");
                if (count > 0)
                {
                    new OrderHelperModel().SetInvoiceToOrder(order_code);
                    this.cb_show_price_print.Checked = show_price;
                    this.cb_show_history.Checked = show_history;
                    this.ValidateLoginRule(Role.order_list);

                    SetControls(ReqOrderCode);
                    GetOrderListString(ReqOrderCode);
                    this.CustomerMsg1.Visible = this.cb_show_customer_msg.Checked;

                    BindOrderProductHistory(ReqOrderCode);
                    BindList();
                    BindOrderStatus();
                    this.DataBind();

                }
                else
                {
                    ltNote.Text = "<div style='padding:5em; color:red;'>no find order.</div><script>$('#showarea').css('display','none');</script> ";
                }
            }
        }
    }

    void BindList()
    {
        DataTable dt = Config.ExecuteDataTable("Select * from tb_order_notepad where OrderCode='" + ReqOrderCode.ToString() + "'");
        this.dl_msg_list.DataSource = dt;
        this.dl_msg_list.DataBind();
    }

    void BindOrderStatus()
    {
        DataTable dt = new XmlStore().FindPreStatus();
        ddl_order_status.DataSource = dt;
        ddl_order_status.DataTextField = "pre_status_name";
        ddl_order_status.DataValueField = "pre_status_serial_no";
        try
        {
            ddl_order_status.SelectedValue = _webStatus.ToString();
        }
        catch { }
    }

    private void SetControls(string order_code)
    {
        var orderCodeInt = int.Parse(order_code);
        var model = OrderHelperModel.GetModelsByOrderCode(DBContext, int.Parse(order_code));
        var css = CustomerStoreModel.FindModelsByOrderCode(DBContext, order_code);

        if (model.Length == 1 && css.Length == 1)
        {
            var orderHelper = model[0];
            var customer = css[0];
            if (string.IsNullOrEmpty(customer.customer_shipping_first_name))
            {
                customer.customer_shipping_first_name = customer.customer_first_name;
            }
            if (string.IsNullOrEmpty(customer.customer_shipping_last_name))
            {
                customer.customer_shipping_last_name = customer.customer_last_name;
            }
           
            var state = customer.customer_shipping_state.HasValue
                            ? StateShippingModel.GetStateShippingModel(DBContext, customer.customer_shipping_state.Value)
                            : null;

            OrderHelperID = orderHelper.order_helper_serial_no;
            _webStatus = orderHelper.pre_status_serial_no;
            string price_unit_string = string.Format(" <span class='price_unit' style='color:blue;'>{0}</span>", orderHelper.price_unit);

            this.lbl_customer_name.Text = string.Format("{0}&nbsp;{1}", customer.customer_first_name, customer.customer_last_name);

            try
            {
                if ((customer.customer_card_first_name ?? "").Length > 0 || (customer.customer_card_last_name ?? "").Length > 0)
                {
                    this.lbl_billing_address.Text = string.Format("{0}{1}{2}", string.IsNullOrEmpty((customer.customer_company ?? "")) ? (customer.customer_card_first_name ?? "")
                        + " " + (customer.customer_card_last_name ?? "") + "<br/>"
                        : (customer.customer_company ?? "") + "<br/>"
                        + (customer.customer_card_first_name ?? "") + " " + (customer.customer_card_last_name ?? "")
                        + "<br/>"
                        , (customer.customer_card_billing_shipping_address ?? "") + "<br/>"
                    , string.IsNullOrEmpty((customer.customer_card_billing_shipping_address ?? "")) ? " "
                    : (customer.customer_card_city ?? "") + ", " + (customer.customer_card_state_code ?? "")
                    + "<br>" + (customer.customer_card_zip_code ?? "")).ToUpper();
                }
                else
                {
                    this.lbl_billing_address.Text = string.Format("{0}{1}{2}"
                        , string.IsNullOrEmpty(customer.customer_company ?? "") ? (customer.customer_first_name ?? "")
                        + " " + (customer.customer_last_name ?? "") + "<br/>"
                        : (customer.customer_company ?? "")
                        + "<br/>" + (customer.customer_first_name ?? "")
                        + " " + (customer.customer_last_name ?? "") + "<br/>"
                                  , (customer.customer_address1 ?? "") + "<br/>"
                              , string.IsNullOrEmpty((customer.customer_address1 ?? "")) ? " "
                              : (customer.customer_city ?? "") + ", " + (customer.state_code ?? "")
                              + "<br>" + (customer.zip_code ?? "")).ToUpper();

                }
            }
            catch
            {
                //  CH.Alert(ex.Message, this.lbl_shipping_address);

            }


            //if (customer.phone_d != "" || customer.phone_c != "")
            //this.lbl_billing_phone.Text = " Phone # " + customer.phone_d + "," + customer.phone_c;
            if (customer.phone_d != "" && customer.phone_d != null)
            {
                this.lbl_billing_phone.Text = string.Format("Phone # {0}", customer.phone_d);
            }
            if (customer.phone_c != "" && customer.phone_c != null)
            {
                if (customer.phone_d != "" && customer.phone_d != null)
                    this.lbl_billing_phone.Text += string.Format(", {0}", customer.phone_c);
                else
                    this.lbl_billing_phone.Text = string.Format("Phone #{0}", customer.phone_c);
            }
            if (customer.phone_n != "" && customer.phone_n != null)
                this.lbl_shipping_phone.Text = " Phone # " + customer.phone_n;

            // this.lbl_customer_name.Text = customer.customer_shipping_first_name.Trim() + "&nbsp;" + customer.customer_shipping_last_name.Trim();
            this.lbl_customer_number.Text = Code.FilterCustomerCode(customer.customer_serial_no.ToString());
            this.lbl_grand_total.Text = string.Format("{0}{1}", Config.ConvertPrice(orderHelper.grand_total.Value), price_unit_string);
            this.lbl_order_date.Text = orderHelper.create_datetime.ToString();


            this.lbl_order_number.Text = string.Format("<a href='http://www.ebay.ca/itm/ws/eBayISAPI.dll?ViewItem&Item={0}' target='_blank' style='color:#000000;'>{0}</a>", ReqOrderCode.ToString());
            if (orderHelper.order_invoice != null)
            {
                this.lbl_invoice_number.Text = orderHelper.order_invoice.ToString();
                this.lbl_invoice_number_title.Visible = true;
            }
            else
            {
                this.lbl_invoice_number_title.Visible = false;
            }

            int pay_method_id = 0;

            int.TryParse(orderHelper.pay_method, out pay_method_id);
            if (pay_method_id != 0)
                this.lbl_payment.Text = PayMethodNewModel.GetPayMethodNewModel(DBContext, pay_method_id).pay_method_short_name;
            if (Config.pay_method_pick_up_ids.IndexOf("[" + pay_method_id.ToString() + "]") != -1)// == Config.pay_method_pick_up_id)
            {
                this.lbl_prick_time.Text = "<table cellspacing='0' cellpadding='0'><tr><td><strong><span style=\"font-size: 9pt\">Pick Up Schedule:</strong></span></td><td>" + 
                    (!orderHelper.prick_up_datetime1.HasValue || orderHelper.prick_up_datetime1.Value.Year == 1 || 
                        (orderHelper.prick_up_datetime1.Value.Month == 1 && 
                            orderHelper.prick_up_datetime1.Value.Day == 1 && 
                            orderHelper.prick_up_datetime1.Value.Hour == 11) 
                        ? "" 
                        : string.Format("{0:t},{0:D}", orderHelper.prick_up_datetime1)) + "</td></tr><tr><td>&nbsp;</td><td>" + 
                            (!orderHelper.prick_up_datetime2.HasValue || orderHelper.prick_up_datetime2.Value.Year == 1 || (orderHelper.prick_up_datetime2.Value.Month == 1 && orderHelper.prick_up_datetime2.Value.Day == 1 && orderHelper.prick_up_datetime2.Value.Hour == 11) 
                            ? "" 
                            : string.Format("{0:t},{0:D}", orderHelper.prick_up_datetime2)) + "</td></tr></table>";

                if (orderHelper.shipping_charge == 0M)
                    this.lbl_shipping_and_handling.Text = "$0.00" + price_unit_string;
                else
                {
                    var scm = ShippingCompanyModel.GetShippingCompanyModel(DBContext, orderHelper.shipping_company.Value);
                    if(scm == null)
                    {

                    }
                    this.lbl_shipping_company.Text = scm != null ? scm.shipping_company_name : "";


                    this.lbl_shipping_and_handling.Text = string.Format("{0}{1}", Config.ConvertPrice(orderHelper.shipping_charge.Value), price_unit_string);

                    this.lbl_shipping_address.Text = string.Format("{0}{1}{2}", customer.customer_shipping_first_name + " " + customer.customer_shipping_last_name
                        + " <br/>"
                    , customer.customer_shipping_address + "<br/>"
                    , customer.customer_shipping_address != null && customer.customer_shipping_address != "" ? customer.customer_shipping_city + " " + customer.shipping_state_code + "<br>" + (state != null ? state.Country : customer.shipping_country_code) + "<br>" +
                    customer.customer_shipping_zip_code : "").ToUpper();
                }
                //Response.Write(customer.customer_shipping_first_name);
            }
            else
            {
                var scm = ShippingCompanyModel.GetShippingCompanyModel(DBContext, orderHelper.shipping_company.HasValue ? orderHelper.shipping_company.Value: 0);
                this.lbl_shipping_company.Text = scm != null ? scm.shipping_company_name : "";

                this.lbl_shipping_and_handling.Text = string.Format("{0}{1}", Config.ConvertPrice(orderHelper.shipping_charge.Value), price_unit_string);

                this.lbl_shipping_address.Text = string.Format("{0}{1}{2}", customer.customer_shipping_first_name + " " + customer.customer_shipping_last_name
                    + " <br/>"
                , customer.customer_shipping_address + "<br/>"
                , customer.customer_shipping_address != null && customer.customer_shipping_address != "" ? customer.customer_shipping_city + " " + customer.shipping_state_code + "<br>" + (state != null ? state.Country : customer.shipping_country_code) + "<br>" +
                customer.customer_shipping_zip_code : "").ToUpper();


            }
            this.lbl_special_cash_discount.Text = string.Format("{0}{1}", orderHelper.input_order_discount.Value.ToString("$0.##"), price_unit_string);
            //    this.lbl_special_cash_discount.ForeColor = System.Drawing.Color.FromName("red");

            //
            //
            //
            this.lbl_card_info.Text = GetCardInfo(pay_method_id, customer.customer_credit_card, customer.customer_card_issuer, customer.customer_card_phone, customer.customer_expiry);

            if (orderHelper.order_pay_status_id == 2 || orderHelper.order_pay_status_id == 3)
            {
                DataTable odt = Config.ExecuteDataTable("select transaction from tb_order_paypal_record where order_code='" + orderHelper.order_code.ToString() + "'");
                string transactions = "";
                for (int x = 0; x < odt.Rows.Count; x++)
                {
                    transactions += "," + odt.Rows[x][0].ToString();
                }
                if (transactions != "")
                    this.lbl_card_info.Text += "<br> <b>Transaction:</b>&nbsp;&nbsp;&nbsp;&nbsp;" + transactions.Substring(1);

            }
            //
            //  card price
            //if (Config.pay_method_use_card_rate.IndexOf("[" + this.Pay_method.ToString() + "]") != -1 || this.Pay_method == -1)
            //if (Config.pay_method_use_card_rate.IndexOf("[" + pay_method_id + "]") != -1
            if (orderHelper.input_order_discount == 0)
            {
                this.lbl_special_cash_discount.Text = "$0.00" + price_unit_string;
                this.panel_special_cash_discount.Visible = false;
            }

            //if (orderHelper.tax_charge == 0M)
            //    this.lbl_sales_tax.Text = Config.ConvertPrice(orderHelper.gst + orderHelper.hst + orderHelper.pst);
            //else
            //    this.lbl_sales_tax.Text = Config.ConvertPrice(orderHelper.tax_charge);
            //this.lbl_tax_rate.Text = (orderHelper.tax_rate).ToString("0") + "%";

            string tax_string = "";

            #region Tax
            // BC 洲的税分开写
            if (customer.shipping_state_code.Trim().ToLower() == "BRITISH COLUMBIA".ToLower() || customer.shipping_state_code == "BC")
            {
                if (orderHelper.gst_rate > 0M)
                {
                    tax_string += string.Format("<b>GST({0}%)</b></td><td style='text-align:right;'>{1}{2}</td>", orderHelper.gst_rate.Value.ToString("##"), Config.ConvertPrice(orderHelper.gst.Value), price_unit_string);
                }
                if (orderHelper.pst_rate > 0M)
                {
                    tax_string += string.Format("</tr><tr><td><b>PST-BC({0}%)</b></td><td style='text-align:right;'>{1}{2}</td>", orderHelper.pst_rate.Value.ToString("##"), Config.ConvertPrice(orderHelper.pst.Value), price_unit_string);
                }
                if (orderHelper.hst_rate > 0M)
                {
                    var gstRate = 5;
                    var pstRate = orderHelper.hst_rate - gstRate;
                    var gst = decimal.Parse((orderHelper.taxable_total.Value * gstRate / 100).ToString("0.00"));

                    tax_string += string.Format("<b>GST({0}%)</b></td><td style='text-align:right;'>{1}{2}</td>"
                        , gstRate.ToString("##")
                        , Config.ConvertPrice(gst)
                        , price_unit_string);
                    tax_string += string.Format("</tr><tr><td style='text-align:right;'><b>PST-BC({0}%)</b></td><td style='text-align:right;'>{1}{2}</td>"
                        , pstRate.Value.ToString("##")
                        , Config.ConvertPrice(orderHelper.hst.Value - gst)
                        , price_unit_string);
                }
            }
            else
            {
                if (orderHelper.hst_rate > 0M)
                {
                    tax_string += string.Format("<b>HST({0}%)</b></td><td style='text-align:right;'>{1}{2}</td>", orderHelper.hst_rate.Value.ToString("##"), Config.ConvertPrice(orderHelper.hst.Value), price_unit_string);
                }
                else
                {
                    if (orderHelper.gst_rate > 0M)
                    {
                        tax_string += string.Format("<b>GST({0}%)</b></td><td style='text-align:right;'>{1}{2}</td>", orderHelper.gst_rate.Value.ToString("##"), Config.ConvertPrice(orderHelper.gst.Value), price_unit_string);
                    }
                    if (orderHelper.pst_rate > 0M)
                    {
                        tax_string += string.Format("</tr><tr><td style='text-align:right;'><b>PST({0}%)</b></td><td style='text-align:right;'>{1}{2}</td>", orderHelper.pst_rate.Value.ToString("##"), Config.ConvertPrice(orderHelper.pst.Value), price_unit_string);
                    }
                }
                if ((orderHelper.pst_rate == 0M &&
                    orderHelper.gst_rate == 0M
                    && orderHelper.hst_rate == 0M))
                {
                    tax_string = string.Format("<b>Sales Tax</b></td><td style='text-align:right;'>{0}{1}</td>", Config.ConvertPrice(0M), price_unit_string);
                }
                else if (orderHelper.pst_rate + orderHelper.gst_rate > 10M)
                {
                    tax_string = string.Format("<b>Sales Tax</b></td><td style='text-align:right;'>{0}{1}</td>", Config.ConvertPrice(orderHelper.gst.Value + orderHelper.pst.Value), price_unit_string);
                }
            }
            #endregion
            // tax
            this.lbl_tax_rate.Text = tax_string;
            this.lbl_sub_total.Text = string.Format("{0}{1}", Config.ConvertPrice(orderHelper.sub_total_rate.Value), price_unit_string);

            if (this.lbl_payment.Text == "-1")
            {
                this.lbl_payment.Text = "NONE";
            }

            if (orderHelper.is_old.Value)
            {
                this.lbl_is_old_order.Text = "This is Old Order.";
            }
            this.lbl_taxable_total.Text = string.Format("{0}{1}", orderHelper.taxable_total.Value.ToString("$0.00"), price_unit_string);


            this.lbl_email.Text = "<strong><span style=\"font-size: 9pt\">Email:&nbsp;&nbsp;</strong></span>";

            if ((customer.customer_email2 != "" && customer.customer_email2 != null) && (customer.customer_email1 != "" && customer.customer_email1 != null))
            {
                if (customer.customer_email1 != customer.customer_email2)
                {
                    this.lbl_email.Text += customer.customer_email1 + ",&nbsp;" + customer.customer_email2;
                }
                else
                {
                    this.lbl_email.Text += customer.customer_email2;
                }
            }
            else if ((customer.customer_email1 != "" && customer.customer_email1 != null) && (customer.customer_email2 == null || customer.customer_email2 == ""))
            {
                this.lbl_email.Text += customer.customer_email1;
            }
            else if ((customer.customer_email1 == null || customer.customer_email1 == "") && (customer.customer_email2 != "" && customer.customer_email2 != null))
            {
                this.lbl_email.Text += customer.customer_email2;
            }
            else
            {
                this.lbl_email.Text += (customer.customer_login_name.IndexOf('@') != -1 ? customer.customer_login_name : "");
            }
            this.lbl_phone.Text = string.Format("{0}{1}{2}", ((customer.phone_d ?? "").Length > 4 ? " Business Phone: " + PhoneFormat.Format(customer.phone_d) : ""), ((customer.phone_n ?? "").Length > 4 ? " Home Phone: " + PhoneFormat.Format(customer.phone_n) : ""), ((customer.phone_c ?? "").Length > 4 ? " Mobile Phone: " + PhoneFormat.Format(customer.phone_c) : ""));

            //}        
            this.lbl_weee_charge.Text = string.Format("{0}{1}", ConvertPrice.RoundPrice(orderHelper.weee_charge.ToString()), price_unit_string);


            var oem = DBContext.tb_order_ebay.Where(me =>me.order_code.HasValue && me.order_code.Value.Equals(orderCodeInt)).ToList();//                OrderEbayModel.FindAllByProperty("order_code", ReqOrderCode);
            if (oem != null && oem.Count > 0)
            {
                this.lbl_shipping_service.Text = string.Format(@"<br><span style='font-weight:bold;'>Shipping Service:&nbsp;&nbsp;</span> {0}
<br><span style='font-weight:bold;'>Item number:&nbsp;&nbsp;</span><a href='http://www.ebay.ca/itm/ws/eBayISAPI.dll?ViewItem&Item={1}' target='_blank' style='color:#000000;'>{1}</a>
", oem[0].shipping_service
 , oem[0].item_number);
                this.lbl_ebayUserId.Text = oem[0].user_id;
            }
        }
        else
        {
            Response.Write("database is error.");
        }
    }

    private void GetOrderListString(string order_code)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("<table width=\"100%\"  border=\"0\" cellpadding=\"2\" cellspacing=\"1\" bgcolor=\"#eeeeee\">");
        var opm = OrderProductModel.GetModelsByOrderCode(DBContext, order_code);
        var pm = new ProductModel();
        for (int i = 0; i < opm.Length; i++)
        {

            sb.Append("<tr>");
            if (opm[i].product_serial_no.ToString().Length == 8)
                sb.Append("<td > System# " + opm[i].product_serial_no.ToString() + "</td>  ");
            else
                sb.Append("<td >" + opm[i].product_name + "(<a href=\"editPartDetail.aspx?id=" + opm[i].product_serial_no.ToString() + "\" onclick=\"winOpen(this.href, 'right_m',880,800,120,200);return false;\"><span style='color:#000000;'>" + opm[i].product_serial_no.ToString() + "</span></a>)" + (opm[i].product_serial_no.ToString().Length == 8 ? "" : pm.FindStockByLuSkuForOrder(opm[i].product_serial_no.Value, -1)) + "</td>  ");
            sb.Append("<td width=\"9%\" align=\"center\">" + opm[i].order_product_sum.ToString() + "</td> ");
            sb.Append("<td width=\"11%\" align=\"right\" >" + Config.ConvertPrice(opm[i].order_product_sold.Value) + "</td> ");
            sb.Append("<td width=\"9%\" align=\"right\" >" + Config.ConvertPrice(opm[i].order_product_sold.Value * opm[i].order_product_sum.Value) + "</td>  ");
            sb.Append("</tr>");

            if (show_price)
            {
                sb.Append("<tr>");
                sb.Append("<td colspan=\"4\">");

                //sb.Append(string.Format("<i><span style='{2};'  class=\"td_price_area\"><b>购买时价格</b> : ${0}<b>|</b>${1}</span></i><br/>", opm[i].order_product_cost, opm[i].order_product_sold
                //    , opm[i].order_product_cost < opm[i].order_product_sold ? " color: green; " : "color: red;"));
                if (opm[i].product_serial_no.ToString().Length != 8)
                {
                    DataTable LUDT = Config.ExecuteDataTable(@"select (product_current_price - product_current_discount) product_current_price, product_current_cost from tb_product 
where product_serial_no='" + opm[i].product_serial_no.ToString() + "'");
                    //if (LUDT.Rows.Count != 1)
                    //{
                    decimal sold;
                    decimal.TryParse(LUDT.Rows[0]["product_current_price"].ToString(), out sold);

                    decimal cost;
                    decimal.TryParse(LUDT.Rows[0]["product_current_cost"].ToString(), out cost);
                    sb.Append(string.Format("<table class='table_small_font' style='margin-left: 50px;background:#F1ECEC; width: 500px;'><tr><td style='width: 100px;'><span style='{2};'  class=\"ispan\"><b>LU</b> :</td><td style='width: 70px;text-align:right'><i>${0}&nbsp;&nbsp;</td><td style='text-align:center'>|&nbsp;&nbsp;${1}</span></i></td><td></td><td></td></tr>", cost
                       , sold
              , cost < sold ? " color: #000000; " : "color: red;"));
                    //}
                    // part price

                    DataTable dt = Config.ExecuteDataTable(string.Format(@"select distinct other_inc_id product_store_category, other_inc_store_sum product_store_sum,other_inc_price  product_cost, last_regdate from tb_other_inc_part_info oi
where oi.luc_sku='{0}' and tag=1 and datediff(now(),last_regdate)<30", opm[i].product_serial_no));
                    for (int n = 0; n < dt.Rows.Count; n++)
                    {

                        int product_store_category;
                        int.TryParse(dt.Rows[n]["product_store_category"].ToString(), out product_store_category);
                        string product_store_category_name = LH.FilterText(LH.LtdModelByValue(product_store_category).ToString());

                        decimal voder_cost;
                        decimal.TryParse(dt.Rows[n]["product_cost"].ToString(), out voder_cost);
                        //     sb.Append(string.Format("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<i><span style='{2};'  class=\"ispan\"><b>{1}</b> : &nbsp;&nbsp;${0}&nbsp;&nbsp;|&nbsp;&nbsp;<b>{3}</b></span></i>", voder_cost
                        //         , product_store_category_name
                        //, voder_cost < sold ? " color:#8B8BD1;" : "color: red;", dt.Rows[n]["product_store_sum"].ToString()));

                        sb.Append(string.Format("<tr><td><b>{1}</b> : </td><td><span style='{2};text-align: right'  class=\"ispan\"><i>${0}&nbsp;&nbsp;</i></span></td><td>|&nbsp;&nbsp;{3}</td><td><i>{4}</i></td><td></td></tr>", voder_cost
                          , product_store_category_name
                 , voder_cost < sold ? " color:#000000;" : "color: red;", dt.Rows[n]["product_store_sum"].ToString(), dt.Rows[n]["last_regdate"].ToString()));
                    }
                    sb.Append("</table>");
                }
                sb.Append("</td></tr>");
            }
            if (opm[i].product_serial_no.ToString().Length == 8)
            {

                sb.Append("<tr>");
                sb.Append("<td colspan='4' style='background: white;'>");
                DataTable sdm = OrderProductSysDetailModel.GetModelsBySysCode(opm[i].product_serial_no.ToString());

                sb.Append("<table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">");
                for (int j = 0; j < sdm.Rows.Count; j++)
                {
                    if ((sdm.Rows[j]["product_name"].ToString().ToLower() != "none selected"
                        && sdm.Rows[j]["product_name"].ToString().ToLower().IndexOf("onboard") == -1
                        && sdm.Rows[j]["product_serial_no"].ToString() != "0")
                        && Config.notShowOnSysListSKUs.IndexOf("[" + sdm.Rows[j]["product_serial_no"].ToString() + "]") == -1)
                    {
                        sb.Append(@"
<tr>
    <td style='width: 100px;font-weight:bold; text-align:right;font-size:7pt; color:#000000; display:none'  nowrap='nowrap'>
        " + sdm.Rows[j]["cate_name"].ToString().ToUpper() + @"
    </td>
    <td style='width: 50px;'>
        &nbsp;&nbsp;&nbsp;(<a href=""editPartDetail.aspx?id=" + sdm.Rows[j]["product_serial_no"].ToString() + @"""
                        onclick=""winOpen(this.href, 'right_m',880,800,120,200);return false;""><span style='color:#000000;'>" + sdm.Rows[j]["product_serial_no"].ToString() + @"</span></a>)
    </td>
    <td style='width: 20px;'> " + (int.Parse(sdm.Rows[j]["part_quantity"].ToString()) > 1 ? sdm.Rows[j]["part_quantity"].ToString() + "x " : "") + @"
    </td>
    <td style='font-size:7pt;'>" + (sdm.Rows[j]["product_name"].ToString() ?? "") + "" + (sdm.Rows[j]["product_serial_no"].ToString().Length == 8 ? "" : pm.FindStockByLuSkuForOrder(int.Parse(sdm.Rows[j]["product_serial_no"].ToString()), -1)) + "</td></tr>");

                        sb.Append("<tr><td colspan=\"3\">");
                        #region system part price
                        if (show_price)
                        {

                            decimal lu_cost;
                            decimal.TryParse(sdm.Rows[j]["product_current_cost"].ToString(), out lu_cost);
                            decimal lu_price;
                            decimal.TryParse(sdm.Rows[j]["product_current_price"].ToString(), out lu_price);
                            sb.Append("<div>");

                            //sb.Append(string.Format("<i><span style='{2};'  class=\"td_price_area\"><b>购买时价格</b> : ${0}<b>|</b>${1}</span></i><br/>", lu_cost, lu_price
                            //    , lu_cost < lu_price ? " color: green; " : "color: red;"));
                            if (sdm.Rows[j]["product_serial_no"].ToString().Length != 8)
                            {
                                DataTable LUDT = Config.ExecuteDataTable(@"select (product_current_price - product_current_discount) product_current_price, product_current_cost from tb_product 
where product_serial_no='" + sdm.Rows[j]["product_serial_no"].ToString() + "'");
                                //if (LUDT.Rows.Count != 1)
                                //{
                                if (LUDT.Rows.Count > 0)
                                {
                                    decimal sold;
                                    decimal.TryParse(LUDT.Rows[0]["product_current_price"].ToString(), out sold);

                                    decimal cost;
                                    decimal.TryParse(LUDT.Rows[0]["product_current_cost"].ToString(), out cost);
                                    //     sb.Append(string.Format("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<i><span style='{2};'  class=\"ispan\"><b>LU</b> : &nbsp;&nbsp;${0}<b>&nbsp;&nbsp;|&nbsp;&nbsp;</b>${1}</span></i>", cost
                                    //         , sold
                                    //, cost < sold ? " color: #749074; " : "color: red;"));
                                    sb.Append(string.Format("<table class='table_small_font' style='margin-left: 50px;background:#F1ECEC; width: 500px;'><tr><td style='width: 100px;'><span style='{2};'  class=\"ispan\"><b>LU</b> :</td><td style='width: 70px;text-align:right'><i>${0}&nbsp;&nbsp;</i></td><td style='text-align:center'>|<i>&nbsp;&nbsp;${1}</span></i></td><td></td><td></td></tr>", cost
                           , sold
                  , cost < sold ? " color: #000000; " : "color: red;"));
                                    //}
                                    // part price
                                    DataTable dt = Config.ExecuteDataTable(string.Format(@" select  distinct other_inc_id product_store_category, other_inc_store_sum product_store_sum,other_inc_price  product_cost,last_regdate from tb_other_inc_match_lu_sku ol inner join tb_other_inc_part_info oi
 on oi.other_inc_sku=ol.other_inc_sku and ol.other_inc_type=oi.other_inc_id and ol.lu_sku='{0}' and tag=1", sdm.Rows[j]["product_serial_no"].ToString()));
                                    for (int n = 0; n < dt.Rows.Count; n++)
                                    {

                                        int product_store_category;
                                        int.TryParse(dt.Rows[n]["product_store_category"].ToString(), out product_store_category);
                                        string product_store_category_name = LH.FilterText(LH.LtdModelByValue(product_store_category).ToString());

                                        decimal voder_cost;
                                        decimal.TryParse(dt.Rows[n]["product_cost"].ToString(), out voder_cost);
                                        //     sb.Append(string.Format("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<i><span style='{2};'  class=\"ispan\"><b>{1}</b> : &nbsp;&nbsp;${0}&nbsp;&nbsp;|&nbsp;&nbsp;<b>{3}</b></span></i>", voder_cost
                                        //         , product_store_category_name
                                        //, voder_cost < sold ? " color: #8B8BD1; " : "color: red;", dt.Rows[n]["product_store_sum"].ToString()));
                                        sb.Append(string.Format("<tr><td><b>{1}</b> : </td><td style='text-align: right'><span style='{2};'  class=\"ispan\"><i>${0}&nbsp;&nbsp;</i></span></td><td>|&nbsp;&nbsp;{3}</td><td><i>{4}</i></td><td></td></tr>", voder_cost
                               , product_store_category_name
                      , voder_cost < sold ? " color:#000000;" : "color: red;", dt.Rows[n]["product_store_sum"].ToString(), dt.Rows[n]["last_regdate"].ToString()));
                                    }

                                    sb.Append("</table>");
                                }
                            }
                            sb.Append("</div>");
                        }
                        #endregion
                        sb.Append("</td></tr>");
                    }
                }
                sb.Append("</table>");
                sb.Append("</td>  ");
                sb.Append("</tr>");

            }

        }
        sb.Append("</table>");

        this.lbl_order_list.Text = sb.ToString();
    }
    public string ReqOrderCode
    {
        get { return Util.GetStringSafeFromQueryString(Page, "order_code"); }
    }

    public string GetCardInfo(int pay_method_id, string card_number, string card_issuer, string Telephone, string expiry)
    {
        if (pay_method_id == Config.pay_method_card || Config.pay_method_pick_up_ids.IndexOf("[" + pay_method_id.ToString() + "]") != -1)
        {

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<table><tr><td width='120'>Card Number</td><td>" + FormatCardNumber(card_number) + "</td></tr>");
            sb.Append("<tr><td width='120'>Card Issuer</td><td>" + card_issuer + "</td></tr>");
            sb.Append("<tr><td width='120'>Telephone</td><td>" + PhoneFormat.Format(Telephone) + "</td></tr>");
            sb.Append("<tr><td width='120'>Expiry Date</td><td>" + expiry + "</td></tr>");
            sb.Append("</table>");
            return sb.ToString();
        }
        else
            return "";
    }

    string FormatCardNumber(string cardNumber)
    {
        if (string.IsNullOrEmpty(cardNumber))
            return "";
        if (cardNumber.Length > 6)
        {
            string card = cardNumber.Substring(0, 2);
            for (int i = 0; i < cardNumber.Length - 6; i++)
                card += "*";
            card += cardNumber.Substring(cardNumber.Length - 4);
            return card;
        }
        else
            return cardNumber;

    }


    private void BindOrderProductHistory(string order_code)
    {
        var ophm = new tb_order_product_history[] { };
        if (show_history)
        {
            ophm = OrderProductHistoryModel.FindModelsByOrder(DBContext, order_code);
        }


        this.gv_order_product_history.DataSource = ophm;
        this.gv_order_product_history.DataBind();

    }

    protected void gv_order_product_history_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType != DataControlRowType.Pager &&
            e.Row.RowType != DataControlRowType.Header &&
             e.Row.RowType != DataControlRowType.Footer)
        {
            string cmd = e.Row.Cells[4].Text;
            if (cmd == "True")
            {
                e.Row.Cells[4].Text = "Add";
                e.Row.Cells[2].Text = "$" + e.Row.Cells[2].Text;
            }
            else
            {
                e.Row.Cells[4].Text = "Delete";
                e.Row.Cells[2].Text = "-$" + e.Row.Cells[2].Text;
                e.Row.BackColor = System.Drawing.Color.FromName("#f2f2f2");
            }
        }

    }
    protected void cb_show_price_print_CheckedChanged(object sender, EventArgs e)
    {
        show_price = this.cb_show_price_print.Checked;
        GetOrderListString(ReqOrderCode);

    }
    protected void cb_show_history_CheckedChanged(object sender, EventArgs e)
    {
        show_history = this.cb_show_history.Checked;

        BindOrderProductHistory(ReqOrderCode);
    }
    protected void cb_show_customer_msg_CheckedChanged(object sender, EventArgs e)
    {
        this.CustomerMsg1.Visible = this.cb_show_customer_msg.Checked;

    }

    protected void ddl_order_status_SelectedIndexChanged(object sender, EventArgs e)
    {
        int orderCode;
        int.TryParse(ReqOrderCode, out orderCode);
        if (orderCode > 0)
        {
            DropDownList webStatus = sender as DropDownList;
            string webStatusValue = webStatus.SelectedValue;
            bool colorStatus = false;

            int intValue;
            int.TryParse(webStatusValue, out intValue);

            DataTable dt = new XmlStore().FindPreStatus();
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["back_color"].ToString().ToLower().IndexOf("green") > -1 && webStatus.SelectedItem.Text == dr["pre_status_name"].ToString())
                    colorStatus = true;
            }

            new OrderHelperModel().UpdateOutStatus(DBContext, -1, intValue, "", OrderHelperID, -1);
            this.InsertTraceInfo(DBContext, "Save Order Note And Status:" + ReqOrderCode.ToString());
            Response.Write("<script>alert('Order status is save.');window.opener.changeBgLineColor('" + OrderHelperID.ToString() + "', '" + webStatus.SelectedItem.Text + "'," + colorStatus.ToString().ToLower() + ");</script>");
        }
    }
}
