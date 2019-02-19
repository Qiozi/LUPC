using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Q_Admin_orders_edit_detail : PageBase
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ShippingCompanyID = -1;
            InitialDatabase();

            RunModify();
        }
    }

    public override void InitialDatabase()
    {
        base.InitialDatabase();

        BindShippingCompany();
        BindOrderStatus();

        if (OrderCodeRequest != "")
        {
            OrderCode = OrderCodeRequest.ToString();          
            //FindPayMethodByOrderCode(int.Parse(OrderCode));

            BindBaseInfo();
            BindPartList(OrderCode);
            BindSystemList(OrderCode);
            AccountOrder(OrderCode, false);

            BindMsgDG();

            BindPayRecordLV(int.Parse(OrderCode));
        }

        // load payment pay method
        this.ddl_pay_pay_method.DataSource = Config.ExecuteDataTable("select pay_record_id, pay_record_name from tb_pay_record_method ");
        this.ddl_pay_pay_method.DataTextField = "pay_record_name";
        this.ddl_pay_pay_method.DataValueField = "pay_record_id";
        this.ddl_pay_pay_method.DataBind();


        // 2008 order
        //DataTable order2008 = Config.ExecuteDataTable("select date_format(create_datetime, '%Y') d from tb_order_helper where order_code='" + OrderCode + "'");
        //if (order2008.Rows.Count > 0)
        //{
        //    if (order2008.Rows[0][0].ToString() == "2008")
        //    {
        //        this.btn_view_order.Style.Add("display", "none");
        //        this.btn_email_order_confirm.Style.Add("display", "none");
        //        btn_print_invoice.Style.Add("display", "none");
        //        btn_accept_down_invoice.Visible = false;
        //    }
        //}
    }
    
    #region paymethod
    //private void FindPayMethodByOrderCode(int order_code)
    //{
    //    PayMethod = Util.GetInt32SafeFromQueryString(Page, "pay_method", -1);
    //    if (PayMethod == -1)
    //    {
    //        //OrderHelperModel[] ohm = OrderHelperModel.GetModelsByOrderCode(order_code);
    //        CustomerStoreModel[] csm = CustomerStoreModel.FindModelsByOrderCode(order_code.ToString());
    //        if (csm.Length == 1)
    //            PayMethod = csm[0].pay_method;
    //        else
    //            PayMethod = -1;
    //    }
    //    // Response.Write(PayMethod.ToString());
    //}
    #endregion

    #region properties

    public CountryCategory CC
    {
        get { object o = ViewState["CC"];
        if (o != null)
            return (CountryCategory)Enum.Parse(typeof(CountryCategory), o.ToString());
        else
            return CountryCategory.CA;
        }
        set { ViewState["CC"] = value; }
        
    }

    public int CurrentCustomerID
    {
        get { return (int)ViewState["CurrentCustomerID"]; }
        set { ViewState["CurrentCustomerID"] = value; }
    }

    public int CurrentCUstomerStoreID
    {
        get { return (int)ViewState["CurrentCUstomerStoreID"]; }
        set { ViewState["CurrentCUstomerStoreID"] = value; }
    }

    public string OrderCodeRequest
    {
        get { return Util.GetStringSafeFromQueryString(Page, "order_code"); }
    }
    public string OrderCode
    {
        get { return ViewState["OrderCode"].ToString(); }
        set { ViewState["OrderCode"] = value; }
    }
    /// <summary>
    /// store facture state datatable.
    /// </summary>
    public FactureStateModel[] FactureStateDB
    {
        get { return (FactureStateModel[])ViewState["FactureStateDB"]; }
        set { ViewState["FactureStateDB"] = value; }
    }

    /// <summary>
    /// store pre atatus datatable.
    /// </summary>
    public PreStatusModel[] PreStatusDB
    {
        get { return (PreStatusModel[])ViewState["PreStatusDB"]; }
        set { ViewState["PreStatusDB"] = value; }
    }
    public string StateID
    {
        get
        {
            object o = ViewState["StateID"];

            return o.ToString();
        }
        set { ViewState["StateID"] = value; }
    }

    public string Tax_execmtion
    {
        get
        {
            object o = ViewState["tax_execmtion"];
            if (o != null)
                return o.ToString();
            return "";
        }
        set { ViewState["tax_execmtion"] = value; }
    }

    /// <summary>
    /// 信用卡系数
    /// </summary>
    public decimal CardRate
    {
        get
        {
            object o = ViewState["CardRate"];
            if (o != null)
            {
                return decimal.Parse(o.ToString());
            }
            return 1;
        }
        set { ViewState["CardRate"] = value; }
    }

    /// <summary>
    /// email send to 
    /// </summary>
    public string SendEmail
    {
        get
        {
            object o = ViewState["SendEmail"];
            if (o != null)
            {
                return o.ToString();
            }
            return "";
        }
        set { ViewState["SendEmail"] = value; }
    }
    /// <summary>
    /// PayMethod
    /// </summary>
    public int PayMethod
    {
        get
        {
            object o = ViewState["PayMethod"];
            if (o != null)
            {
                return int.Parse(o.ToString());
            }
            return -1;
            //throw new Exception ("PayMethod isn't exist.");
        }
        set { ViewState["PayMethod"] = value; }
    }

    /// <summary>
    /// if isOK value is true, that record order product change
    /// 
    /// </summary>
    bool isOK
    {
        get { return (bool)ViewState["isOK"]; }
        set { ViewState["isOK"] = true; }
    }

    int ShippingCompanyID
    {
        get { return (int)ViewState["ShippingCompanyID"]; }
        set { ViewState["ShippingCompanyID"] = value; }
    }

    /// <summary>
    /// 是否执行修改， 
    /// 修改完后重新转回本界面
    /// </summary>
    bool IsRunModify
    {
        get { return Util.GetInt32SafeFromQueryString(this.Page, "isrunmodify", 0) == 1; }
    }
    #endregion

    /// <summary>
    /// 如果是需要执行修改，修改总价后重新返回到订单
    /// </summary>
    void RunModify()
    {
        if (IsRunModify)
        {
            AccountOrder(OrderCodeRequest, true);
            Response.Write("<script>location.href=\"/q_admin/orders_edit_detail.aspx?order_code="+ OrderCodeRequest + "\";</script>");
            Response.End();
        }
    }

    #region history
    private void BindOrderProductHistory(string order_code)
    {
        OrderProductHistoryModel[] ophm = OrderProductHistoryModel.FindModelsByOrder(order_code);
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
                e.Row.Cells[4].Text = "Add";
            else
            {
                e.Row.Cells[4].Text = "Delete";
                e.Row.Cells[2].Text = "-" + e.Row.Cells[2].Text;
            }
        }
    }
    #endregion

    private void BindBaseInfo()
    {
        
        // CustomerModel model = CustomerModel.GetCustomerModel(cutomer_id);
        CustomerStoreModel[] customers = CustomerStoreModel.FindModelsByOrderCode(OrderCode);
        if (customers.Length > 0)
        {
            CustomerStoreModel customer = customers[0];
            CurrentCustomerID = customer.customer_serial_no;
            CurrentCUstomerStoreID = customer.serial_no;
            StateID = Config.pay_method_pick_up_ids.IndexOf("[" + customer.pay_method.ToString() + "]") == -1 ? customer.shipping_state_code : "ON";
                        
            // email send to 
            SendEmail = customer.customer_email2 == "" ? customer.customer_email1 : customer.customer_email2;
            if (SendEmail == "")
                SendEmail = customer.customer_login_name;

           
            this.lbl_shipping_zip_code.Text = customer.customer_shipping_zip_code;

            //
            // card info
            //
            this.lbl_card_billing_shipping_address.Text = customer.customer_card_billing_shipping_address;
            this.lbl_card_city.Text = customer.customer_card_city;
            this.lbl_card_number.Text = customer.customer_credit_card;
            this.lbl_card_expiry.Text = customer.customer_expiry;
            this.lbl_card_zip_code.Text = customer.customer_card_zip_code;
            this.lbl_card_issuer.Text = customer.customer_card_issuer;
            this.lbl_customer_card_phone.Text = customer.customer_card_phone;
            this.lbl_verification_number.Text = customer.card_verification_number;
            this.lbl_card_state.Text = customer.customer_card_state_code;
            this.lbl_card_name.Text = customer.customer_card_first_name + "&nbsp;" + customer.customer_card_last_name;
                      
            //
            // shipping info
            //
            this.lbl_shipping_address1.Text = customer.customer_shipping_address;
            this.lbl_shipping_city.Text = customer.customer_shipping_city;
            this.lbl_shipping_state.Text = customer.shipping_state_code;
            
            this.lbl_shipping_first_name.Text = customer.customer_shipping_first_name;
            this.lbl_shipping_last_name.Text = customer.customer_shipping_last_name;
 
            //
            // personal information
            //
            this.lbl_phone_c.Text = customer.phone_c;          
            this.lbl_customer_email1.Text = customer.customer_email1;
            this.lbl_customer_email2.Text = customer.customer_email2;

            this.lblcustomer_first_name.Text = customer.customer_first_name;
            this.lbl_phone_d.Text = customer.phone_d;
            this.lblcustomer_last_name.Text = customer.customer_last_name;
            //this.lblcustomer_note.Text = customer.customer_note;
            this.lbl_phone_n.Text = customer.phone_n;

            this.lbl_customer_address.Text = customer.customer_address1;
            this.lbl_customer_city.Text = customer.customer_city;
            this.lbl_customer_zip_code.Text = customer.zip_code;
            this.lbl_customer_state_code.Text = customer.state_code;

            //
            //  business
            // 
            this.lblcustomer_company.Text = customer.customer_company;
            //try
            //{
            //    if (customer.customer_country_code.Length == 2)
            //        BindCustomerState((CountryCategory)Enum.Parse(typeof(CountryCategory), customer.customer_country_code), customer.state_code);
            //    else
            //        BindCustomerState(CountryCategory.CA, "");
            //}
            //catch (Exception ex) { CH.Alert(ex.Message, this.lv_part_list); }

            //this.lbl_purchase_order.Text = customer.my_purchase_order;
            //this.txt_purchase_order.Text = customer.my_purchase_order;
            //this.lbl_password.Text = customer.customer_password;
            this.lbl_tax_exemption.Text = customer.tax_execmtion;
            
            Tax_execmtion = "";// 20100701 cancel Tax; // customer.tax_execmtion;

            if (this.PayMethod == Config.pay_method_card)
            {
                this.lbl_pay_method.Text = customer.customer_card_type;
            }

            this.lbl_pay_method_text.Text = PayMethodNewModel.GetPayMethodNewModel(customer.pay_method).pay_method_name;      
           
        }

        DataTable dt = Config.ExecuteDataTable(string.Format(@"select tag
,is_ok
,shipping_company
,prick_up_datetime1
,prick_up_datetime2
,pay_method
,out_status
,pre_status_serial_no
,order_invoice
,weee_charge
,price_unit
,input_order_discount
from tb_order_helper where order_code='{0}'", OrderCode));
        if (dt.Rows.Count > 0)
        {
            if (dt.Rows[0][0].ToString() == "1" && dt.Rows[0][1].ToString() == "1")
                isOK = true;
            else
                isOK = false;

            txt_order_discount.Text = dt.Rows[0]["input_order_discount"].ToString();


            ChangeSetInvoiceDownBtn(dt.Rows[0]["pre_status_serial_no"].ToString());
            SetInvoideTitleCode(dt.Rows[0]["order_invoice"].ToString());

            this.txt_weee_charge.Text = dt.Rows[0]["weee_charge"].ToString();
            this.lbl_weee.Text = ConvertPrice.RoundPrice(dt.Rows[0]["weee_charge"].ToString());
            int _shipping_company_id;
            int.TryParse(dt.Rows[0][2].ToString(), out _shipping_company_id);
            ShippingCompanyID = _shipping_company_id;
            this.ddl_ship_method.SelectedValue = ShippingCompanyID.ToString();

            //
            // pick up time 
            //
            this.lbl_pick_up_time_1.Text = dt.Rows[0]["prick_up_datetime1"].ToString().IndexOf("1/1/0001") == -1 ? dt.Rows[0]["prick_up_datetime1"].ToString() : "";
            this.lbl_pick_up_time_2.Text = dt.Rows[0]["prick_up_datetime2"].ToString().IndexOf("1/1/0001") == -1 ? dt.Rows[0]["prick_up_datetime2"].ToString() : "";

            if (dt.Rows[0]["prick_up_datetime2"].ToString().IndexOf("1/1/0001") == -1 && dt.Rows[0]["prick_up_datetime1"].ToString().IndexOf("1/1/0001") == -1)
                lbl_pick_up_time_chart.Visible = true;
            else
                lbl_pick_up_time_chart.Visible = false;

            //
            // paymethod
            //
            int pay_method_id;
            int.TryParse(dt.Rows[0]["pay_method"].ToString(), out pay_method_id);

            PayMethod = pay_method_id;
           
            //
            // status
            //
            DataTable out_status_dt = Config.ExecuteDataTable("select facture_state_name from tb_facture_state where facture_state_serial_no='" + dt.Rows[0]["out_status"].ToString() + "'");
            if (out_status_dt.Rows.Count > 0)
            {
                this.lbl_out_status.Text = out_status_dt.Rows[0][0].ToString();

                //for (int i = 0; i < this.ddl_back_status.Items.Count; i++)
                //{
                //    this.ddl_back_status.Items[i].Selected = false;
                //    if(this.ddl_back_status.Items[i].Value == 
                //}
                this.ddl_back_status.SelectedValue = dt.Rows[0]["out_status"].ToString();
            }
            DataTable pre_status_dt = Config.ExecuteDataTable("select pre_status_name from tb_pre_status where pre_status_serial_no='" + dt.Rows[0]["pre_status_serial_no"].ToString() + "' limit 0,1");
            if (out_status_dt.Rows.Count > 0)
            {
                this.lbl_pre_status.Text = pre_status_dt.Rows[0][0].ToString();
                this.ddl_pre_status.SelectedValue = dt.Rows[0]["pre_status_serial_no"].ToString();
            }

            CC = dt.Rows[0]["price_unit"].ToString() != "CAD" ? CountryCategory.US : CountryCategory.CA;
            this.lbl_price_unit.Text = CC.ToString() + "D";
        }
        else
            isOK = false;
    }

    public void ChangeSetInvoiceDownBtn(string pre_status)
    {
        
        this.btn_accept_down_invoice.Enabled = Config.order_complete.IndexOf("[" + pre_status.ToString() + "]") != -1;
   
    }
    public void SetInvoideTitleCode(string order_invoice)
    {
        if (order_invoice.Length == Config.OrderInvoiceLength)
            this.literal_invoice_code.Text = "Invoice No. &nbsp;&nbsp;<b>" + order_invoice + "</b>";
    }
    private void ChangeOrderPriceView(string order_code)
    {
        DataTable dt = Config.ExecuteDataTable(string.Format(@"select gst, gst_rate, pst, pst_rate, hst, hst_rate
, tax_charge, tax_rate, taxable_total, grand_total, sub_total
, discount,shipping_charge,shipping_company,input_order_discount 
, weee_charge
from tb_order_helper where order_code='{0}'", order_code));
        if (dt.Rows.Count == 1)
        {
            DataRow dr = dt.Rows[0];
            this.lbl_ship_charge.Text = ConvertPrice.RoundPrice(dr["shipping_charge"].ToString());
            this.lbl_order_code.Text = order_code;
            this.lbl_grand_total.Text = ConvertPrice.RoundPrice(dr["grand_total"].ToString());
            this.lbl_sub_total.Text = ConvertPrice.RoundPrice(dr["sub_total"].ToString());
            this.lbl_special_cash_discount.Text = ConvertPrice.RoundPrice(dr["input_order_discount"].ToString());
            this.lbl_taxable_total.Text = ConvertPrice.RoundPrice(dr["taxable_total"].ToString());
            this.lbl_weee.Text = ConvertPrice.RoundPrice(dr["weee_charge"].ToString());
            decimal pst_rate;
            decimal.TryParse(dr["pst_rate"].ToString(), out pst_rate);

            decimal gst_rate;
            decimal.TryParse(dr["gst_rate"].ToString(), out gst_rate);

            decimal hst_rate;
            decimal.TryParse(dr["hst_rate"].ToString(), out hst_rate);

            if (hst_rate > 0M)
            {
                this.lbl_sale_tax.Text = ConvertPrice.RoundPrice(dr["hst"].ToString());
                this.lbl_sale_tax_rate_text.Text = "HST(" + hst_rate.ToString("##") + "%)";// ConvertPrice.RoundPrice(dr["tax_rate"].ToString());
            }
            else
            {
                if (gst_rate > 0M)
                {
                    this.lbl_sale_tax.Text = ConvertPrice.RoundPrice(dr["gst"].ToString());
                    this.lbl_sale_tax_rate_text.Text = "GST(" + gst_rate.ToString("##") + "%)";// ConvertPrice.RoundPrice(dr["tax_rate"].ToString());
         
                }
                if (pst_rate > 0M)
                {
                    if (gst_rate > 0M)
                    {
                        this.lbl_sale_tax.Text += "<br/>" + ConvertPrice.RoundPrice(dr["pst"].ToString());
                        this.lbl_sale_tax_rate_text.Text += "<br/>PST(" + pst_rate.ToString("##") + "%)";// ConvertPrice.RoundPrice(dr["tax_rate"].ToString());

                    }
                    else
                    {
                        this.lbl_sale_tax.Text = ConvertPrice.RoundPrice(dr["pst"].ToString());
                        this.lbl_sale_tax_rate_text.Text = "PST(" + pst_rate.ToString("##") + "%)";// ConvertPrice.RoundPrice(dr["tax_rate"].ToString());
         
                    }
                }
            }
            if (hst_rate == 0M
                && gst_rate == 0M
                && hst_rate == 0M)
            {
                this.lbl_sale_tax.Text = ConvertPrice.RoundPrice("0");
                this.lbl_sale_tax_rate_text.Text = "Sales Tax";// ConvertPrice.RoundPrice(dr["tax_rate"].ToString());
         
            }
            

            int shipping_company_id;
            int.TryParse(dr["shipping_company"].ToString(), out shipping_company_id);
            ShippingCompanyID = shipping_company_id;

            SetShippingCompanyName(shipping_company_id);
        }
    }

    #region Bind Shipping company 

    private void SetShippingCompanyName(int id)
    {
        XmlStore xs = new XmlStore();
        this.lbl_ship_method.Text = xs.FindShippingCompanyName(id);
    }
    private void BindShippingCompany()
    {
        XmlStore xs = new XmlStore();
        this.ddl_ship_method.DataSource = xs.FindShippingCompany();
        this.ddl_ship_method.DataTextField = "shipping_company_name";
        this.ddl_ship_method.DataValueField = "shipping_company_id";
        this.ddl_ship_method.DataBind();
        this.ddl_ship_method.Items.Insert(0, new ListItem("NONE", "-1"));
        this.ddl_ship_method.SelectedValue = ShippingCompanyID.ToString();
    }

    #endregion

    #region Msg
    protected void btn_submit_to_customer_Click(object sender, EventArgs e)
    {
        try
        {
            if (this.txt_msg_from_seller.Text.Trim().Length > 0)
            {
                ChatMsgModel cmm = new ChatMsgModel();
                cmm.msg_author = "Seller";
                cmm.msg_content_text = this.txt_msg_from_seller.Text;
                cmm.msg_order_code = OrderCode;
                cmm.msg_type = 1;
                cmm.regdate = DateTime.Now;
                cmm.staff_id = LoginUser.LoginIDInt;
                cmm.Create();
                CH.Alert(KeyFields.SaveIsOK, this.btn_save);
            }
            //ASP.testcontrol_ascx TestControl1 = (ASP.testcontrol_ascx)Page.LoadControl("TestControl.ascx");
            BindMsgDG();
            //ReLoadIframe();
        }
        catch (Exception ex)
        {
            CH.Alert(ex.Message, this.btn_save);
        }
    }
    public void BindMsgDG()
    {
        this.dl_msg_list.DataSource = ChatMsgModel.FindModelsByOrderCode(OrderCode);
        this.dl_msg_list.DataBind();
    }
    #endregion

    #region Product List 
    private void BindPartList(string order_code)
    {
        DataTable dt = Config.ExecuteDataTable(string.Format(@"select op.*, (op.order_product_sold * order_product_sum) subtotal_2  from tb_order_product op where 
         order_code='{0}' and length(op.product_serial_no) <>8 ", order_code));

        this.lv_part_list.DataSource = dt;
        this.lv_part_list.DataBind();

        BindOrderProductHistory(order_code);
    } 
    
    protected void dg_order_product_ItemDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.Footer
            && e.Row.RowType != DataControlRowType.Header
            && e.Row.RowType != DataControlRowType.Pager)
        {
            
        }
    } 
    
    protected void btn_add_part_Click(object sender, EventArgs e)
    {
        try
        {
            int _product_id = 0;
            int.TryParse(this.txt_part_sku.Text, out _product_id);

            //if (!SetSystemProduct(_product_id))
            {



                int part_count = Config.ExecuteScalarInt32(string.Format("select count(product_serial_no) from tb_product where product_serial_no ='{0}'", _product_id));
                if (part_count == 0)
                {
                    CH.Alert(" it is not exist.", this.lv_part_list);
                    return;
                }
                else
                {
                    bool isValid = ProductModel.IsValid(_product_id);
                    if (!isValid)
                    {
                        CH.Alert(" this is a invalid part.", this.lv_part_list);
                        return;
                    }

                    ProductModel product = ProductModel.GetProductModel(_product_id);
                    ProductCategoryModel pc = ProductCategoryModel.GetProductCategoryModel(product.menu_child_serial_no);
                    OrderProductModel order = new OrderProductModel();
                    
                    order.menu_child_serial_no = product.menu_child_serial_no;
                    order.order_code = OrderCode;
                    order.order_product_cost = ConvertPrice.Price(CC, product.product_current_cost);
                    order.order_product_price = ConvertPrice.Price(CC,product.product_current_price);
                    
                    order.order_product_sum = 1;
                    order.product_name = product.product_name;
                    order.product_serial_no = _product_id;
                    order.sku = _product_id.ToString();

                    order.order_product_sold = ConvertPrice.Price(CC, product.product_current_price - product.product_current_discount);// ProductModel.FindOnSaleDiscountByPID(_product_id);
                    //throw new Exception(CC.ToString());
                    order.tag = 1;
                    order.menu_pre_serial_no = product.menu_child_serial_no;
                    order.product_type = Product_category_helper.product_category_value(pc.is_noebook == byte.Parse("1") ? product_category.noebooks : product_category.part_product);
                    order.product_type_name = pc.is_noebook == byte.Parse("1") ? "Noebook" : "Unit";
                    order.product_current_price_rate = ConvertPrice.Price(CC,product.product_current_price);
                    order.Create();

                    CH.Alert(KeyFields.SaveIsOK, this.lv_part_list);
                    InsertTraceInfo(string.Format("insert part({0}) in order({1})", _product_id, OrderCode));
                    //
                    //  if the order is OK then save a product after create.
                    //
                    if (isOK)
                    {
                        string error = "";
                        OrderHelper oh = new OrderHelper();
                        if (!oh.CopyProductToHistoryStore(order, true, ref error))
                            throw new Exception(error);
                        BindMsgDG();
                    }
                    BindPartList(OrderCode);
                }
            }

            AccountOrder(OrderCode);
        }
        catch (Exception ex)
        {
            CH.Alert(ex.Message, this.lv_part_list);
            CH.CloseParentWatting(this.lv_part_list);
        }
    }

    protected void lv_part_list_ItemCommand(object sender, ListViewCommandEventArgs e)
    {

        try
        {
            TextBox __txt_sum_part = (TextBox)e.Item.FindControl("_txt_sum_part");
            Literal _literal_sub_total = (Literal)e.Item.FindControl("literal_sub_total");
            TextBox __txt_sold_part = (TextBox)e.Item.FindControl("_txt_sold_part");

            int _sum;
            int.TryParse(__txt_sum_part.Text, out _sum);


            decimal _sold;
            decimal.TryParse(__txt_sold_part.Text.ToString(), out _sold);

            switch (e.CommandName)
            {
                case "SetPartSum":
                    int _p_sum;
                    int.TryParse(e.CommandArgument.ToString(), out _p_sum);
                    __txt_sum_part.Text = _p_sum.ToString();
                    _literal_sub_total.Text = (_p_sum * _sold).ToString();
                    __txt_sum_part.ForeColor = System.Drawing.Color.FromName("red");
                    break;
                case "DeletePart":
                    int _serial_no;
                    int.TryParse(e.CommandArgument.ToString(), out _serial_no);
                    Config.ExecuteNonQuery(string.Format("delete from tb_order_product where serial_no='{0}'", _serial_no));
                    BindPartList(OrderCode);
                    AccountOrder(OrderCode);
                    break;

                case "SavePart":
                    int serial_no;
                    int.TryParse(e.CommandArgument.ToString(), out serial_no);

                    Config.ExecuteNonQuery(string.Format("Update tb_order_product set order_product_sum='{0}', order_product_sold='{1}' where serial_no='{2}'", _sum, _sold, serial_no));

                    AccountOrder(OrderCode);
                    CH.Alert(KeyFields.SaveIsOK, this.lv_sys_list);
                    break;
            }
            CH.CloseParentWatting(this.lv_sys_list);
        }
        catch (Exception ex)
        {
            
            CH.Alert(ex.Message, this.lv_part_list);
        }
    }
    #endregion

    #region System List 
    public void BindSystemList( string order_code)
    {
        try
        {
            
            // bind sysetm product 
            DataTable syseteDT = Config.ExecuteDataTable(string.Format(@"select op.*, (op.order_product_sold * order_product_sum) subtotal_2  from tb_order_product op where 
         order_code='{0}' and length(op.product_serial_no) = 8 ", OrderCode));

            this.lv_sys_list.DataSource = syseteDT;
            this.lv_sys_list.DataBind();

            BindOrderProductHistory(order_code);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void btn_duplicate_sys_Click(object sender, EventArgs e)
    {
        try
        {
        
            if (OrderCode == "")
            {
                CH.Alert("order number is lost.,", this.lv_sys_list);
                return;
            }


            string system_tmp_sku = this.txt_sys_sku.Text.Trim();

            if (system_tmp_sku.Length != 8)
            {
                CH.Alert(" 请输入8位的系统号", this.lv_sys_list);
                this.txt_sys_sku.Focus();
                return;
            }
            OrderHelper oh = new OrderHelper();

            system_tmp_sku = oh.CopySystemToOrderReturnNewCode(system_tmp_sku, OrderCode);

            BindSystemList(OrderCode);

            AccountOrder(OrderCode);
            InsertTraceInfo(string.Format("duplicate system{1} to order({0})", OrderCode, system_tmp_sku));
            CH.CloseParentWatting(this.lv_sys_list);
            CH.Alert("系统已创建:" + system_tmp_sku, this.lv_sys_list);
        }

        catch (Exception ex)
        {
            CH.CloseParentWatting(this.lv_sys_list);
            CH.Alert(ex.Message, this.lv_sys_list);
            //OrderPageBase.CH.Alert(ex.Message, this.btn_btn_duplicate);
        }
        this.BindOrderProductHistory(OrderCode);
    }

    protected void btn_add_sys_Click(object sender, EventArgs e)
    {

        try
        {

            if (OrderCode == "")
            {
                CH.Alert("order number is lost.", this.lv_sys_list);
                return;
            }

            string error = "";
            string system_tmp_sku = this.txt_sys_sku.Text.Trim();

            OrderHelper oh = new OrderHelper();
            XmlStore xs = new XmlStore();
            DataTable partGroup = xs.FindPartGroupComment();
            oh.CopySystemToOrder(system_tmp_sku, true, OrderCode,partGroup,CC, ref error);

            BindSystemList(OrderCode);

            AccountOrder(OrderCode);

            InsertTraceInfo(string.Format("add system{1} to order({0})", OrderCode, system_tmp_sku));
            CH.CloseParentWatting(this.lv_sys_list);
            // CH.Alert(KeyFields.SaveIsOK, this.lv_sys_list);
        }
        catch (Exception ex)
        {
            CH.CloseParentWatting(this.lv_sys_list);
            CH.Alert(ex.Message, this.lv_sys_list);
        }
    }

    /// <summary>
    /// 把产品插入system 产品明细表中
    /// </summary>
    /// <param name="system_code"></param>
    /// <param name="product_id"></param>
    private void SetProductToSystemDetail(int system_code, int product_id, int part_quantity)
    {
        // SpDetailModel m = new SpDetailModel();
        if (part_quantity < 1)
            part_quantity = 1;

        SpTmpDetailModel m = new SpTmpDetailModel();
        SpTmpDetailModel[] sdms = SpTmpDetailModel.GetModelsBySysTmpCode(system_code.ToString());
        SpTmpDetailModel singlem = new SpTmpDetailModel();
        if (sdms.Length > 0)
            singlem = sdms[0];

        ProductModel p = ProductModel.GetProductModel(product_id);
        ProductCategoryModel pc = ProductCategoryModel.GetProductCategoryModel(p.menu_child_serial_no);
        m.product_current_cost = p.product_current_cost;
        m.product_current_price = p.product_current_price;
        m.save_price = ProductModel.FindOnSaleDiscountByPID(product_id);
        m.product_current_price_rate = p.product_current_price;
        m.product_current_sold = m.product_current_price - m.save_price;
        m.product_order = pc.menu_child_order;
        m.product_serial_no = product_id;
        m.sys_tmp_code = system_code.ToString();
        m.system_templete_serial_no = singlem.system_templete_serial_no;
        m.system_product_serial_no = 0;
        m.part_quantity = part_quantity;
        m.part_max_quantity = part_quantity > 1 ? part_quantity : 1;
        m.product_name = p.product_name_long_en != "" ? p.product_name_long_en : p.product_name;

        PartGroupDetailModel pgdm = new PartGroupDetailModel();
        DataTable pgdt = pgdm.FindPartGroupNameByPartSku(product_id);
        if (pgdt.Rows.Count > 0)
        {
            int part_group_id;
            int.TryParse( pgdt.Rows[0]["part_group_id"].ToString(), out part_group_id);
            m.part_group_id =part_group_id;
            m.cate_name = pgdt.Rows[0]["part_group_name"].ToString();
        }
        
        m.Create();

        OrderProductSysDetailModel opsdm = new OrderProductSysDetailModel();
        opsdm.cate_name = m.cate_name;
        opsdm.ebay_number = m.ebay_number;
        opsdm.is_lock = m.is_lock;
        opsdm.old_price = m.old_price;
        opsdm.part_group_id = m.part_group_id;
        opsdm.part_max_quantity = m.part_max_quantity;
        opsdm.part_quantity = m.part_quantity;
        opsdm.product_current_cost = m.product_current_cost;
        opsdm.product_current_price = m.product_current_price;
        opsdm.product_current_price_rate = m.product_current_price_rate;
        opsdm.product_current_sold = m.product_current_sold;
        opsdm.product_name = m.product_name;
        opsdm.product_order = m.product_order;
        opsdm.product_serial_no = m.product_serial_no;
        opsdm.re_sys_tmp_detail = m.re_sys_tmp_detail;
        opsdm.save_price = m.save_price;
        opsdm.sys_tmp_code = m.sys_tmp_code;
        opsdm.sys_tmp_detail = m.sys_tmp_detail;
        opsdm.system_product_serial_no = m.system_product_serial_no;
        opsdm.system_templete_serial_no = m.system_templete_serial_no;
        opsdm.Create();

        InsertTraceInfo(string.Format("add part({2}) to system({1}) in order({0})", OrderCode, system_code, product_id));
 
        string error = "";
        OrderHelper oh = new OrderHelper();

        int sum = SystemSum(system_code.ToString());

        if (!oh.CopyProductToHistoryStore(p, opsdm, OrderCode.ToString(), sum, true, ref error))
            throw new Exception(error);
        BindMsgDG();

    }


    private void BindSystemDetail(Repeater gv, int system_code,bool bind)
    {
        if (gv.Items.Count < 2 || bind)
        {
            //gv.DataSource = SystemProductModel.FindSystemDetail(system_code);
            gv.DataSource = Config.ExecuteDataTable("Select * from tb_order_product_sys_detail WHere sys_tmp_code='" + system_code.ToString() + "' and product_name<>'none selected' order by product_order asc");

            gv.DataBind();
            BindOrderProductHistory(OrderCode);
        }
    }

    protected void lv_sys_list_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        if (e.Item.ItemType != ListViewItemType.EmptyItem
            && e.Item.ItemType != ListViewItemType.InsertItem)
        {
            int system_code;
            int.TryParse(((Literal)e.Item.FindControl("_literal_system_code")).Text, out system_code);
            Repeater rpt = (Repeater)e.Item.FindControl("_rpt_sys_detail");

            //BindSystemDetail(rpt, system_code, false);
            //rpt.ItemCommand += new RepeaterCommandEventHandler(rpt_ItemCommand);
        }
    }

    public int SystemSum(string system_code)
    {
     
        return Config.ExecuteScalarInt32(string.Format(@"Select sum(order_product_sum) from tb_order_product where product_serial_no='{0}'", system_code));
    }

    protected void rpt_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {
            switch (e.CommandName)
            {

                case "DelSysDetail":
                    Repeater rpt = (Repeater)source;
                    string[] ps = e.CommandArgument.ToString().Split(new char[] { '|' });

                    int system_code = int.Parse(ps[1]);
                    int part_sku = int.Parse(ps[2]);
                    int sys_detail_id = int.Parse(ps[0]);

                    string error = "";

                    int sum = SystemSum(system_code.ToString());
                    OrderProductSysDetailModel m = OrderProductSysDetailModel.GetOrderProductSysDetailModel(sys_detail_id);
                    //SpTmpDetailModel m = SpTmpDetailModel.GetSpTmpDetailModel(sys_detail_id);
                    ProductModel pm = ProductModel.GetProductModel(part_sku);
                    OrderHelper ohelper = new OrderHelper();
                    if (!ohelper.CopyProductToHistoryStore(pm, m, OrderCode, sum, false, ref error))
                        throw new Exception(error);
                    m = OrderProductSysDetailModel.GetOrderProductSysDetailModel(sys_detail_id);
                   
                    m.Delete();
                    Config.ExecuteNonQuery("Delete from tb_sp_tmp_detail Where sys_tmp_code='"+ m.sys_tmp_code.ToString()+"' and part_quantity='"+ m.part_quantity.ToString()+"' and product_serial_no='"+ m.product_serial_no.ToString()+"'");

                    BindSystemDetail(rpt, system_code, true);

                    //
                    // change price
                    //
                    for (int i = 0; i < this.lv_sys_list.Items.Count; i++)
                    {
                        Repeater _rpt = (Repeater)this.lv_sys_list.Items[i].FindControl("_rpt_sys_detail");
                        if (rpt == _rpt)
                        {
                            ChangeSystemPrice(this.lv_sys_list.Items[i], system_code);
                        }
                    }

                    AccountOrder(OrderCode.ToString());
                    break;
            }
            CH.CloseParentWatting(this.lv_sys_list);
        }
        catch (Exception ex)
        {
            CH.CloseParentWatting(this.lv_sys_list);
            CH.Alert(ex.Message, this.lv_sys_list);
        }
    }

    private void ChangeSystemPrice(ListViewItem lvi, int system_code)
    {
        DataTable dt = Config.ExecuteDataTable(string.Format(@"update tb_order_product set 
order_product_price=(select sum(product_current_price*part_quantity) from tb_sp_tmp_detail where sys_tmp_code='{0}')
, order_product_cost = (select sum(product_current_cost*part_quantity) from tb_sp_tmp_detail where sys_tmp_code='{0}')
, order_product_sold = (select sum(product_current_sold*part_quantity) from tb_sp_tmp_detail where sys_tmp_code='{0}')
, save_price = (select sum(save_price*part_quantity) from tb_sp_tmp_detail where sys_tmp_code='{0}')
, product_current_price_rate =(select sum(product_current_price*part_quantity) from tb_sp_tmp_detail where sys_tmp_code='{0}') 

where product_serial_no='{0}';

select order_product_price, order_product_cost, order_product_sold, save_price, product_current_price_rate from tb_order_product where  product_serial_no='{0}';", system_code));
        if (dt.Rows.Count > 0)
        {
            Literal _literal_order_product_cost = (Literal)lvi.FindControl("_literal_order_product_cost");
            Literal _literal_order_product_price = (Literal)lvi.FindControl("_literal_order_product_price");
            TextBox _txt_sold_part = (TextBox)lvi.FindControl("_txt_sold_part");
            TextBox _txt_sum_part = (TextBox)lvi.FindControl("_txt_sum_part");
            Literal literal_sub_total = (Literal)lvi.FindControl("literal_sub_total");

            DataRow dr = dt.Rows[0];

            int sum;
            int.TryParse(_txt_sum_part.Text, out sum);

            decimal sold;
            decimal.TryParse(dr["order_product_sold"].ToString(), out sold);


            _literal_order_product_cost.Text = dr["order_product_cost"].ToString();
            _literal_order_product_price.Text = dr["order_product_price"].ToString();
            _txt_sold_part.Text = sold.ToString();
            literal_sub_total.Text = (sum * sold).ToString();
        }
    }

    protected void lv_sys_list_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        try
        {
            Repeater rpt = (Repeater)e.Item.FindControl("_rpt_sys_detail");

            TextBox __txt_sum_part = (TextBox)e.Item.FindControl("_txt_sum_part");
            Literal _literal_sub_total = (Literal)e.Item.FindControl("literal_sub_total");
            TextBox __txt_sold_part = (TextBox)e.Item.FindControl("_txt_sold_part");

            int _sum;
            int.TryParse(__txt_sum_part.Text, out _sum);


            decimal _sold;
            decimal.TryParse(__txt_sold_part.Text.ToString(), out _sold);

            switch (e.CommandName)
            {
                case "ViewSystemDetail":
                    int system_code;
                    int.TryParse(e.CommandArgument.ToString(), out system_code);
                    Panel _panel_detail = (Panel)e.Item.FindControl("_panel_sys_detail");
                    _panel_detail.Visible = !_panel_detail.Visible;

                    //Repeater rpt = (Repeater)e.Item.FindControl("_rpt_sys_detail");
                    if (_panel_detail.Visible)
                        BindSystemDetail(rpt, system_code, false);
                   // CH.Alert("OK", this.lv_sys_list);
                    break;

                
                case "AddPartToSys":
                    int _system_code;
                    int.TryParse(e.CommandArgument.ToString(), out _system_code);

                    int product_id;
                    TextBox _txt_part_sku = (TextBox)e.Item.FindControl("_txt_part_sku");
                    int.TryParse(_txt_part_sku.Text, out product_id);
                    int part_quantity;
                    TextBox _txt_part_quantity = (TextBox)e.Item.FindControl("_txt_part_quantity");
                    int.TryParse(_txt_part_quantity.Text, out part_quantity);
                    SetProductToSystemDetail(_system_code, product_id, part_quantity);

                    BindSystemDetail(rpt, _system_code, true);

                    //
                    // change price
                    //
                    ChangeSystemPrice(e.Item, _system_code);

                    AccountOrder(OrderCode);
                    //
                    // 
                   
                    break;

                case "SetPartSum":
                    int _p_sum;
                    int.TryParse(e.CommandArgument.ToString(), out _p_sum);
                    __txt_sum_part.Text = _p_sum.ToString();
                    _literal_sub_total.Text = (_p_sum * _sold).ToString();
                    __txt_sum_part.ForeColor = System.Drawing.Color.FromName("red");
                    break;

                case "SaveSys":
                    int serial_no;
                    int.TryParse(e.CommandArgument.ToString(), out serial_no);

                    Config.ExecuteNonQuery(string.Format("Update tb_order_product set order_product_sum='{0}', order_product_sold='{1}' where serial_no='{2}'", _sum, _sold, serial_no));

                    AccountOrder(OrderCode);
                    CH.Alert(KeyFields.SaveIsOK, this.lv_sys_list);
                    break;

                case "DeleteSys":
                    int _serial_no;
                    int.TryParse(e.CommandArgument.ToString(), out _serial_no);
                    string ___system_code = ((Literal)e.Item.FindControl("_literal_system_code")).Text;
                    Config.ExecuteNonQuery(string.Format("delete from tb_order_product where serial_no='{0}'; delete from tb_order_product_sys_detail where sys_tmp_code='{1}'", _serial_no, ___system_code));
                    BindSystemList(OrderCode);
                    AccountOrder(OrderCode);

                    InsertTraceInfo(string.Format("delete to system({1}) in order({0})", OrderCode, _serial_no));
                    break;
            }
           // CH.Alert("OK", this.lv_sys_list);
            CH.CloseParentWatting(this.lv_sys_list);
        }
        catch (Exception ex)
        {
            CH.Alert(ex.Message, this.lv_sys_list);
        }
    }  
    #endregion


    #region save order
    private void SaveListViewValue(ListView lv)
    {
        for (int i = 0; i < lv.Items.Count; i++)
        {
            TextBox __txt_sum_part = (TextBox)lv.Items[i].FindControl("_txt_sum_part");
            Literal _literal_sub_total = (Literal)lv.Items[i].FindControl("literal_sub_total");
            TextBox __txt_sold_part = (TextBox)lv.Items[i].FindControl("_txt_sold_part");
            HiddenField _hf_serial_no = (HiddenField)lv.Items[i].FindControl("_hf_serial_no");
            int serial_no;
            int.TryParse(_hf_serial_no.Value, out serial_no);

            int _sum;
            int.TryParse(__txt_sum_part.Text, out _sum);

            decimal _sold;
            decimal.TryParse(__txt_sold_part.Text.ToString(), out _sold);


            Config.ExecuteNonQuery(string.Format("Update tb_order_product set order_product_sum='{0}', order_product_sold='{1}' where serial_no='{2}'", _sum, _sold, serial_no));
        }
    }

    protected void btn_save_Click(object sender, EventArgs e)
    {
        try
        {


            //
            // save part list
            //
            SaveListViewValue(this.lv_part_list);
            BindPartList(OrderCode);
            //
            // save system list 
            //
            SaveListViewValue(this.lv_sys_list);
            BindSystemList(OrderCode);
            //
            // save price to order.
            //

            InsertTraceInfo(string.Format("Save Order({0}).", OrderCode));
            AccountOrder(OrderCode);

            isOK = true;

            OrderHelperModel ohm = new OrderHelperModel();
            string new_invoice = ohm.SetInvoiceToOrder(int.Parse(OrderCode));
            SetInvoideTitleCode(new_invoice);

            CH.Alert(KeyFields.SaveIsOK, this.lv_sys_list);
            CH.CloseParentWatting(this.lv_sys_list);
        }
        catch (Exception ex)
        {
            CH.CloseParentWatting(this.lv_sys_list);
            CH.Alert(ex.Message, this.lv_sys_list);
        }
    }
    private void AccountOrder(string order_code)
    {
        AccountOrder(order_code, true);
    }
    private void AccountOrder(string order_code, bool is_save)
    {
        
        string error = "";

        DataTable partDT = OrderProductModel.GetModelsBySearch(order_code, product_category.part_product);
        DataTable noebookDT = OrderProductModel.GetModelsBySearch(order_code, product_category.noebooks);
        DataTable systemDT = OrderProductModel.GetModelsBySearch(order_code, product_category.system_product);

        int part_count = partDT.Rows.Count;
        int noebook_count = noebookDT.Rows.Count;
        int system_count = systemDT.Rows.Count;
        int count = 0;
        int sum_count = OrderProductModel.GetModelsBySearch(order_code, product_category.entityAll).Rows.Count;
        decimal part_sum = 0;
        decimal noebook_sum = 0;
        decimal system_sum = 0;
        decimal _cost = 0;
        decimal sale_promotion_charge = 0M;

        int stateid = StateShippingModel.FindStatIDByCode(StateID);

        if (sum_count == -1) return;

       
        //
        // 判断有几个是促 销运费的产品
        //
        int sale_promotion_count = Config.ExecuteScalarInt32(string.Format(@"select count(product_serial_no)  from (select product_serial_no  from tb_order_product op where order_code='{0}')  c  inner join tb_product_shipping_fee ps 
on ps.prod_sku=c.product_serial_no and ps.is_system=0", order_code));

        int ap_real_count = sum_count - (Config.sale_promotion_compay_id.IndexOf("[" + ShippingCompanyID + "]") == -1 ? 0 : sale_promotion_count);


        AccountProduct[] aps = new AccountProduct[ap_real_count];
        DataTable shippingCompDt = Config.ExecuteDataTable(string.Format(@"select system_category from tb_shipping_company where shipping_company_id='{0}'", ShippingCompanyID));
               
        //  -----------------------------------------------------------------------------------------------------
        // part
        //  -----------------------------------------------------------------------------------------------------
        for (int i = 0; i < partDT.Rows.Count; i++)
        {

            DataRow dr = partDT.Rows[i];
            decimal product_sum = decimal.Parse(dr["order_product_sum"].ToString());

            DataTable sales_promotion_dt = Config.ExecuteDataTable(string.Format(@"
select 	prod_shipping_fee_id, prod_Sku, is_system, shipping_fee_us, shipping_fee_ca, regdate
	 
	from 
	tb_product_shipping_fee 
	where prod_sku='{0}' and is_system='{1}'", dr["product_serial_no"].ToString(), 0));

            int product_id = int.Parse(dr["product_serial_no"].ToString());
            ProductModel p = ProductModel.GetProductModel(product_id);

            _cost += p.product_current_cost * product_sum;
            part_sum += decimal.Parse(dr["order_product_sold"].ToString())* product_sum;

           
            //
            //  是否特殊运费
            //
            if (sales_promotion_dt.Rows.Count == 0 || Config.sale_promotion_compay_id.IndexOf("[" + ShippingCompanyID + "]") == -1)
            {

                AccountProduct model = new AccountProduct();
                
                model.price = decimal.Parse(dr["order_product_sold"].ToString());
                model.product_size = p.product_size_id;
                model.shipping_company_id = ShippingCompanyID;
                model.product_id = product_id;
                model.product_cate = product_category.part_product;
                model.sum = int.Parse(product_sum.ToString());

               
                aps[count] = model;
                count += 1;
            }
            else
            {
                decimal sale_promotion_shipping_charge_single = 0M;

                 if (shippingCompDt.Rows.Count > 0)
                {
                    if (shippingCompDt.Rows[0][0].ToString() == "1")
                    {
                        // ca
                        decimal.TryParse(sales_promotion_dt.Rows[0]["shipping_fee_ca"].ToString(), out sale_promotion_shipping_charge_single);
                    }
                    else
                    {
                        // us
                        decimal.TryParse(sales_promotion_dt.Rows[0]["shipping_fee_us"].ToString(), out sale_promotion_shipping_charge_single);
                    }
                }
                 sale_promotion_charge += sale_promotion_shipping_charge_single * int.Parse(product_sum.ToString());
            }
        }
        //CH.Alert(string.Format("{0}|{1}", part_sum, product_sum, this.lv_sys_list);
        //  -----------------------------------------------------------------------------------------------------
        // noebook
        //  -----------------------------------------------------------------------------------------------------
        for (int i = 0; i < noebookDT.Rows.Count; i++)
        {
            DataRow dr = noebookDT.Rows[i];
            decimal product_sum = decimal.Parse(dr["order_product_sum"].ToString());

            int product_id = int.Parse(dr["product_serial_no"].ToString());
            ProductModel p = ProductModel.GetProductModel(product_id);
            _cost += p.product_current_cost * product_sum;

            noebook_sum += decimal.Parse(dr["order_product_sold"].ToString()) * product_sum;

            DataTable sales_promotion_dt = Config.ExecuteDataTable(string.Format(@"
select 	prod_shipping_fee_id, prod_Sku, is_system, shipping_fee_us, shipping_fee_ca, regdate
	 
	from 
	tb_product_shipping_fee 
	where prod_sku='{0}' and is_system='{1}'", product_id, 0));
            //
            //  是否特殊运费
            //
            if (sales_promotion_dt.Rows.Count == 0 || Config.sale_promotion_compay_id.IndexOf("[" + ShippingCompanyID + "]") == -1)
            {
                AccountProduct model = new AccountProduct();

                model.price = decimal.Parse(dr["order_product_sold"].ToString());
                model.product_size = AccountHelper.GetSystemSize(p.product_current_price, product_category.noebooks); ;
                model.shipping_company_id = ShippingCompanyID;
                model.product_id = product_id;
                model.product_cate = product_category.noebooks;
                model.sum = int.Parse(product_sum.ToString());

                //_cost += p.product_current_cost * product_sum;
                //part_sum += decimal.Parse(product_id.ToString()) * product_sum;

                aps[count] = model;                
                count += 1;
            }
            else
            {
                decimal sale_promotion_shipping_charge_single = 0M;

                //DataTable shippingCompDt = Config.ExecuteDataTable(string.Format(@"select system_category from tb_shipping_company where shipping_company_id='{0}'", ShippingCompanyID));
                if (shippingCompDt.Rows.Count > 0)
                {
                    if (shippingCompDt.Rows[0][0].ToString() == "1")
                    {
                        // ca
                        decimal.TryParse(sales_promotion_dt.Rows[0]["shipping_fee_ca"].ToString(), out sale_promotion_shipping_charge_single);
                    }
                    else
                    {
                        // us
                        decimal.TryParse(sales_promotion_dt.Rows[0]["shipping_fee_us"].ToString(), out sale_promotion_shipping_charge_single);
                    }
                }
                sale_promotion_charge += sale_promotion_shipping_charge_single * int.Parse(product_sum.ToString());
            }
        }
        //  -----------------------------------------------------------------------------------------------------
        // system
        //  -----------------------------------------------------------------------------------------------------
        for (int i = 0; i < systemDT.Rows.Count; i++)
        {
            DataRow dr = systemDT.Rows[i];
            decimal product_sum = decimal.Parse(dr["order_product_sum"].ToString());

            AccountProduct model = new AccountProduct();
            int product_id = int.Parse(dr["product_serial_no"].ToString());
            //ProductModel p = ProductModel.GetProductModel(product_id);

            model.price = decimal.Parse(dr["order_product_sold"].ToString()); //SpDetailModel.GetPriceSUM(product_id);

            model.product_size = AccountHelper.GetSystemSize(model.price, product_category.system_product);
            model.shipping_company_id = ShippingCompanyID;
            model.product_id = product_id;
            model.product_cate = product_category.system_product;
            model.sum = int.Parse(dr["order_product_sum"].ToString());
            aps[count] = model;

            system_sum += model.price * product_sum;
            _cost += SpTmpDetailModel.GetPriceSUM(product_id) * product_sum;
            count += 1;

        }
        //
        //
        // charge 
        string _charge_result = "";
        decimal _result = 0;

        try
        {
            if (ap_real_count > 0)
            {
                if (ShippingCompanyID != -1)
                {

                    Account ac = new Account(aps, stateid);
                    _result = ac.getResult() + sale_promotion_charge;
                    _result = ConvertPrice.Price(CC, _result);
                    _charge_result = Config.ConvertPrice2(_result);
                }
                else
                {
                    _result = 0;
                    _charge_result = "0";
                }
            }
            else
            {
                _result =  sale_promotion_charge;
                _charge_result = Config.ConvertPrice2(_result);
            }



            
        }
        catch (Exception ex)
        {
            _result = 0;
            error = ex.Message;
        }
       
        //this.lbl_shipping_charge.UpdateAfterCallBack = true;

        //sub total 
        decimal _price_sum = 0;
        _price_sum = part_sum + noebook_sum + system_sum;
      
        //CH.Alert(string.Format("{0}|{1}|{2}", part_sum, noebook_sum, system_sum), this.lv_sys_list);
        //return;
        // this.lbl_sub_total.UpdateAfterCallBack = true;

        // special cash discount
        decimal special_cash_discount;
        special_cash_discount = ConvertPrice.SpecialCashPriceDiscount(_price_sum);

        // tax
        StateShippingModel state = StateShippingModel.GetStateShippingModel(stateid);


        decimal _sale_tax = 0;
        int tax_rate = 0;

        //AnthemHelper.Alert(tax_execmtion.ToString() + this.StateID .ToString());
        // 有个洲经销商税免8%

        if (Tax_execmtion.Trim().Length > 2 && stateid == Config.tax_execmtion_state)
        {

            if ((state.gst + state.pst) > Config.tax_execmtion_state_save_money)
            {
                tax_rate = (int)(state.gst + state.pst - Config.tax_execmtion_state_save_money);
                if (Config.pay_method_use_card_rate.IndexOf("[" + this.PayMethod.ToString() + "]") != -1)
                    _sale_tax = (_price_sum + _result) * (tax_rate) / 100;
                else
                    _sale_tax = (_price_sum + _result - special_cash_discount) * (tax_rate) / 100;
            }
        }
        else
        {
            tax_rate = (int)(state.gst + state.pst);
            if (Config.pay_method_use_card_rate.IndexOf("[" + this.PayMethod.ToString() + "]") != -1)
                _sale_tax = (_price_sum + _result) * tax_rate / 100;
            else
                _sale_tax = (_price_sum + _result - special_cash_discount) * tax_rate / 100;
        }
        //Response.Write(_sale_tax.ToString() + "<br>" + _price_sum.ToString() + " <br>" + _result.ToString() + "<br>" + special_cash_discount.ToString());
        //_sale_tax = decimal.Parse((_state_shipping).ToString());

        //
        // total 
        // 
        bool is_lock_input_order_discount = false;
        bool is_lock_shipping_charge = false;
        decimal input_order_discount = 0M;
        decimal shipping_charge = 0M;
        decimal weee_charge = 0M;

        DataTable dt = Config.ExecuteDataTable(@"select is_lock_input_order_discount, input_order_discount, shipping_charge, is_lock_shipping_charge
        ,weee_charge from tb_order_helper where order_code='" + order_code + "'");
        if (dt.Rows.Count > 0)
        {
            DataRow dr = dt.Rows[0];
            is_lock_input_order_discount = dr["is_lock_input_order_discount"].ToString() == "1";
            is_lock_shipping_charge = dr["is_lock_shipping_charge"].ToString() == "1";

            decimal.TryParse(dr["input_order_discount"].ToString(), out input_order_discount);
            decimal.TryParse(dr["shipping_charge"].ToString(), out shipping_charge);
            decimal.TryParse(dr["weee_charge"].ToString(), out weee_charge);
        }
        decimal _total;
        if (!is_lock_shipping_charge)
        {
            _total=  _result + _sale_tax + _price_sum;
        }
        else
        {
            _total = shipping_charge  + _sale_tax + _price_sum;
            _result = shipping_charge;
        }
        
        // 非信用卡
        if (Config.pay_method_use_card_rate.IndexOf("[" + this.PayMethod.ToString() + "]") == -1)
        {
            _total = _total - (is_lock_input_order_discount ? input_order_discount : special_cash_discount);
            if (is_lock_input_order_discount)
            {
                this.lbl_special_cash_discount_recommend.Text = string.Format("Listed: <span style='color:red;'>${0}</span>", ConvertPrice.RoundPrice(special_cash_discount));
            }
            else
            {
                input_order_discount = special_cash_discount;
                this.lbl_special_cash_discount_recommend.Text = "";
            }
        }
        // 信用卡
        else
        {
            _total = _total - (is_lock_input_order_discount ? input_order_discount : 0);
            if (is_lock_input_order_discount)
            {
                this.lbl_special_cash_discount_recommend.Text = string.Format("Listed: <span style='color:red;'>${0}</span>", ConvertPrice.RoundPrice(0M));
            }
            else
            {
                input_order_discount = 0M;
                this.lbl_special_cash_discount_recommend.Text = "";
            }
        }
        //CH.Alert(special_cash_discount.ToString(), this.lv_part_list);

        
        if (is_lock_shipping_charge)
        {
            this.lbl_ship_charge_recommend.Text = string.Format("Listed: <span style='color:red;'>${0}</span>", ConvertPrice.RoundPrice(_result));

            // 输出到界面后， 把运费传输入保存到数据库
            _result = shipping_charge;
        }

        // taxable total
       // this.lbl_taxable_total.Text = Config.ConvertPrice2(_total - _sale_tax);

        // 把数据保存到数据库存
        //throw new Exception(_result.ToString());
  

        SaveToOrderHelper(stateid, ShippingCompanyID, _price_sum, _total, _result, _cost, _sale_tax
            , tax_rate, int.Parse(order_code), input_order_discount, weee_charge);

        ChangeOrderPriceView(order_code);
    }
    public void SaveToOrderHelper(int state_shipping, int shipping_company, decimal sub_total, decimal total,
        decimal shipping_charge, decimal _cost, decimal _sale_tax
        , int _tax_rate, int order_code, decimal input_order_discount
        , decimal weee_charge)
    {
        try
        {
            StateShippingModel state = StateShippingModel.GetStateShippingModel(state_shipping);
            OrderHelperModel[] model = OrderHelperModel.GetModelsByOrderCode(order_code);

            for (int i = 0; i < model.Length; i++)
            {
                CustomerStoreModel[] css = CustomerStoreModel.FindModelsByOrderCode(order_code.ToString());
                model[i].sub_total = sub_total;
                
                //
                // 手动修改过运费
                //
                //if (model[i].is_lock_shipping_charge)
                //{
                   
                   
                //    total = total - shipping_charge + model[i].shipping_charge;
                //    shipping_charge = model[i].shipping_charge;
                //}
                //else

                //StateShippingModel state = StateShippingModel.GetStateShippingModel(stateid);

                // 有个洲经销商税免8%
                int sale_tax_rate = 0;
                model[i].hst_rate = 0M;
                model[i].gst_rate = 0M;
                model[i].pst_rate = 0M;
                //CH.Alert(Tax_execmtion, this.lv_sys_list);
                if (Tax_execmtion.Trim().Length > 2 && state.state_serial_no == Config.tax_execmtion_state)
                {
                    
                    if ((state.gst + state.pst) > Config.tax_execmtion_state_save_money)
                    {
                        sale_tax_rate = (int)(state.gst + state.pst - Config.tax_execmtion_state_save_money);

                        if (Config.tax_hsts.IndexOf("[" + state.state_serial_no + "]") == -1)
                        {
                            model[i].gst_rate = decimal.Parse(state.gst.ToString());
                            model[i].hst_rate = 0M;
                            model[i].pst_rate = 0M;
                        }
                        else
                        {
                            model[i].hst_rate = decimal.Parse(sale_tax_rate.ToString());
                            model[i].gst_rate = 0M;
                            model[i].pst_rate = 0M;

                        }
                    }
                }
                else
                {
                    sale_tax_rate = (int)(state.gst + state.pst);
                    if (Config.tax_hsts.IndexOf("[" + state.state_serial_no + "]") == -1)
                    {
                        model[i].gst_rate = decimal.Parse(state.gst.ToString());
                        model[i].pst_rate = decimal.Parse(state.pst.ToString());
                        model[i].hst_rate = 0M;
                    }
                    else
                    {
                        model[i].hst_rate = decimal.Parse(sale_tax_rate.ToString());
                        model[i].gst_rate = 0M;
                        model[i].pst_rate = 0M;
                    }
                }

                model[i].shipping_charge = shipping_charge;

                decimal _sub_charge = sub_total + shipping_charge-input_order_discount;
                decimal _gst = (_sub_charge) * state.gst / 100;
                decimal _pst = (_sub_charge) * state.pst / 100;
                model[i].gst = 0M;
                model[i].pst = 0M;
                model[i].hst = 0M;

                if (model[i].gst_rate > 0M)
                {
                    model[i].gst = _gst;
                }
                if (model[i].pst_rate > 0M)
                {
                    model[i].pst = _pst;
                }
                if (model[i].hst_rate > 0M)
                {
                    model[i].hst = _gst + _pst;
                }

                model[i].input_order_discount = input_order_discount;

                model[i].total = _sub_charge + model[i].gst + model[i].pst + model[i].hst;
                //
                // 因为税分开，四舍五入后，会有一分钱的误差。
                 
                _sale_tax = model[i].gst + model[i].pst + model[i].hst;
                model[i].shipping_company = shipping_company;
                model[i].cost = _cost;
                model[i].taxable_total = _sub_charge;
                model[i].tax_rate = _tax_rate;
                model[i].tax_charge = model[i].gst + model[i].pst + model[i].hst + weee_charge;

                //if (model[i].is_lock_input_order_discount)
                {
                    //model[i].taxable_total = model[i].taxable_total - ConvertPrice.SpecialCashPriceDiscount(sub_total) + model[i].sur_charge;
                    //model[i].grand_total = total - ConvertPrice.SpecialCashPriceDiscount(sub_total) + model[i].sur_charge;
                }
                //else
                {
                    model[i].sur_charge = ConvertPrice.SpecialCashPriceDiscount(sub_total);
                    model[i].grand_total = model[i].taxable_total + model[i].tax_charge;
                }
                model[i].sub_total_rate = Config.is_card_rate - 1;
                
                model[i].sub_total_rate = sub_total;
                model[i].Is_Modify = true;
                model[i].Update();

                for (int j = 0; j < css.Length; j++)
                {
                    css[j].state_serial_no = state_shipping;
                    css[j].customer_shipping_state = state_shipping;
                    css[j].Update();
                }
                BindPayRecordLV(model[i].order_code);
                InsertTraceInfo(string.Format("Save Order Price({0}).", OrderCode));
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    protected void btn_save_ship_method_charge_discount_Click(object sender, EventArgs e)
    {
        try
        {
            decimal discount;
            decimal.TryParse(this.txt_order_discount.Text, out discount);

            decimal shipping_charge;
            decimal.TryParse(this.txt_order_ship_charge.Text, out shipping_charge);

            ShippingCompanyID = int.Parse(this.ddl_ship_method.SelectedValue.ToString());
            string sql = "update tb_order_helper set ";
            if(discount != 0)
            {
                sql += string.Format(" input_order_discount='{0}', is_lock_input_order_discount=1,", discount);
            }
            if(shipping_charge != 0)
            {
                sql += string.Format(" shipping_charge='{0}', is_lock_shipping_charge=1,", shipping_charge);
            }

            sql += string.Format(" shipping_company ='{0}' ,Is_Modify=1 where order_code='{1}' ", this.ddl_ship_method.SelectedValue.ToString(), OrderCode);

            Config.ExecuteNonQuery(sql);

            
            AccountOrder(OrderCode);
            CH.CloseParentWatting(this.lv_sys_list);
            CH.Alert(KeyFields.SaveIsOK, this.lv_sys_list);
        }
        catch (Exception ex)
        {
            CH.CloseParentWatting(this.lv_sys_list);
            CH.Alert(ex.Message, this.lv_sys_list);
        }
    }

    #region remove order shipping charge custom
    protected void lb_remove_order_ship_charge_Click(object sender, EventArgs e)
    {
        try
        {
            Config.ExecuteNonQuery(string.Format(@"Update tb_order_helper set is_lock_shipping_charge=0,Is_Modify=1 where order_code='{0}'", OrderCode));
            InsertTraceInfo(string.Format("remove lock_shipping_charge in Order({0}).", OrderCode));
            AccountOrder(OrderCode);
        }
        catch (Exception ex)
        {
            CH.CloseParentWatting(this.lv_sys_list);
            CH.Alert(ex.Message, this.lv_sys_list);
        }
    }
    protected void lb_remove_order_discount_Click(object sender, EventArgs e)
    {
        try
        {
            Config.ExecuteNonQuery(string.Format(@"Update tb_order_helper set is_lock_input_order_discount=0 ,Is_Modify=1 where order_code='{0}'", OrderCode));
            InsertTraceInfo(string.Format("remove lock_input_order_discount in Order ({0}).", OrderCode));
            AccountOrder(OrderCode);
        }
        catch (Exception ex)
        {
            CH.CloseParentWatting(this.lv_sys_list);
            CH.Alert(ex.Message, this.lv_sys_list);
        }
    }
    #endregion

    #region save other

    //protected void btn_save_other_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (CurrentCustomerID > 0)
    //        {
    //            int pay_method_id;
               
    //            CustomerModel cm = CustomerModel.GetCustomerModel(CurrentCustomerID);
              
    //            //cm.my_purchase_order = this.txt_purchase_order.Text.Trim();
    //            cm.pay_method = pay_method_id;

    //            if (CurrentCustomerID != 7888888)
    //            {
    //                cm.Update();
    //            }

    //            CustomerStoreModel csm = CustomerStoreModel.GetCustomerStoreModel(CurrentCUstomerStoreID);
    //            csm.tax_execmtion = cm.tax_execmtion;
    //            csm.my_purchase_order = cm.my_purchase_order;
    //            csm.pay_method = cm.pay_method;
    //            csm.Update();

    //            InsertTraceInfo(string.Format("Save Order({0}) Other info.", OrderCode));
    //            if (PayMethod != pay_method_id)
    //            {                    
    //                PayMethod = pay_method_id;
    //                AccountOrder(OrderCode);
    //            }
                
    //            OrderHelperModel[] hm = OrderHelperModel.GetModelsByOrderCode(int.Parse(OrderCode));
    //            for (int i = 0; i < hm.Length; i++)
    //            {
    //                hm[i].prick_up_datetime1 = DateTime.Parse(pick_up_time);
    //                hm[i].pay_method = pay_method_id.ToString();
    //                hm[i].Update();
    //            }


    //            BindBaseInfo();
    //        }
    //        else
    //        {
    //            CH.Alert("当前用户ID值丢失", this.lv_sys_list);
    //        }
    //        CH.CloseParentWatting(this.lv_part_list);
    //    }
    //    catch (Exception ex)
    //    {
    //        CH.CloseParentWatting(this.lv_part_list);
    //        CH.Alert(ex.Message, this.lv_part_list);
    //    }
    //}

    #endregion

    #region Order Status
    protected void btn_save_status_click(object sender, EventArgs e)
    {
       
        try
        {
            DataTable dt = Config.ExecuteDataTable(string.Format(@"select tag, is_ok, shipping_company,prick_up_datetime1,prick_up_datetime2,pay_method
,out_status, pre_status_serial_no
from tb_order_helper where order_code='{0}'", OrderCode));

            this.lbl_out_status.Text = this.ddl_back_status.SelectedItem.Text;
            this.lbl_pre_status.Text = this.ddl_pre_status.SelectedItem.Text;

            Config.ExecuteNonQuery(string.Format("Update tb_order_helper set out_status='{0}', pre_status_serial_no='{1}',Is_Modify=1 where order_code='{2}'"
                , this.ddl_back_status.SelectedValue.ToString()
                , this.ddl_pre_status.SelectedValue.ToString()
                , this.OrderCode));
            //if (Config.order_complete.IndexOf("[" + this.ddl_pre_status.SelectedValue.ToString() + "]") != -1)
            //{
            //    new OrderHelperModel().SetInvoiceToOrder(int.Parse(OrderCode));
            //}
            ChangeSetInvoiceDownBtn(this.ddl_pre_status.SelectedValue.ToString());

        }
        catch (Exception ex)
        {
            CH.Alert(ex.Message, this.lv_part_list);
        }


    }

    private void BindOrderStatus()
    {
         
        this.ddl_back_status.DataSource = FactureStateModel.FindModelsByShowit();
        this.ddl_back_status.DataTextField = "facture_state_name";
        this.ddl_back_status.DataValueField = "facture_state_serial_no";
        this.ddl_back_status.DataBind();

        this.ddl_pre_status.DataSource = PreStatusModel.FindModelsByShowit();
        this.ddl_pre_status.DataTextField = "pre_status_name";
        this.ddl_pre_status.DataValueField = "pre_status_serial_no";
        this.ddl_pre_status.DataBind();

    }
    #endregion

    #region Calendar
    /// <summary>
    /// 日期控件 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Calendar3_SelectionChanged(object sender, EventArgs e)
    {
        Calendar c = (Calendar)sender;
        this.txt_pay_date.Text = c.SelectedDate.ToString("yyyy-MM-dd");
    }
    protected void Calendar3_DayRender(object sender, DayRenderEventArgs e)
    {
        if (e.Day.IsToday)
        {
            e.Cell.ForeColor = System.Drawing.Color.Blue;
            e.Cell.BackColor = System.Drawing.Color.Pink;
        }
    }
    #endregion

    #region pay record
    protected void btn_pay_cash_Click(object sender, EventArgs e)
    {
        try
        {
            decimal cash;
            decimal.TryParse(this.txt_pay_cash.Text, out cash);

            if (cash > 0M || this.ddl_pay_pay_method.SelectedItem.Text.ToUpper() == "OFFSET")
            {
                if (this.ddl_pay_pay_method.SelectedItem.Text.ToUpper() == "OFFSET")
                    if (cash > 0M)
                        cash = 0 - cash;
                int pay_record_id;
                int.TryParse(this.ddl_pay_pay_method.SelectedValue.ToString(), out pay_record_id);
                string date = this.txt_pay_date.Text.Trim();
                decimal balance_parent;
                decimal.TryParse(this.lbl_pay_balance.Text, out balance_parent);


                if (date != "")
                {
                    Config.ExecuteNonQuery(string.Format(@" insert into tb_order_pay_record 
	( order_code, pay_regdate, pay_cash, regdate, pay_record_id, balance)
	values
	( '{0}', '{1}', '{2}', now(), '{3}', '{4}')", OrderCode, date, cash, pay_record_id, balance_parent - cash));
                    if ((balance_parent - cash) > 0M)
                    {
                        // paid partial
                        Config.ExecuteNonQuery("Update tb_order_helper set order_pay_status_id=3,Is_Modify=1 where order_code='" + OrderCode.ToString() + "'");
                    }
                    else
                    {
                        //paid
                        Config.ExecuteNonQuery("Update tb_order_helper set order_pay_status_id=2,Is_Modify=1 where order_code='" + OrderCode.ToString() + "'");
                    }
                    this.txt_pay_date.Text = "";
                    this.txt_pay_cash.Text = "";
                    BindPayRecordLV(int.Parse(OrderCode));


                }
                else
                {
                    CH.Alert("please input Date.", this.lv_pay_record);
                }
            }
            //CH.Alert(this.ddl_pay_pay_method.SelectedItem.Text, this.lv_sys_list);
        }
        catch (Exception ex)
        {
            CH.Alert(ex.Message, this.lv_sys_list);
        }
    }

    private void BindPayRecordLV(int order_code)
    {
        this.lv_pay_record.DataSource = Config.ExecuteDataTable("select pay_cash, date_format(pay_regdate, \"%b-%d-%Y\") pay_regdate,pay_record_name, balance from tb_order_pay_record opr inner join tb_pay_record_method prm on prm.pay_record_id=opr.pay_record_id where order_code='" + order_code.ToString() + "' order by regdate desc ");
        this.lv_pay_record.DataBind();

        DataTable dt = Config.ExecuteDataTable(string.Format(@"select grand_total, grand_total- (select ifnull(sum(pay_cash), 0) from tb_order_pay_record where order_code='{0}') blance from tb_order_helper where order_code='{0}'", order_code));
        if (dt.Rows.Count > 0)
        {
            this.txt_pay_cash.Text = dt.Rows[0]["blance"].ToString();
            //this.lbl_pay_sold.Text = dt.Rows[0]["grand_total"].ToString();
            this.lbl_pay_balance.Text = dt.Rows[0]["blance"].ToString();
        }
        else
        {
            this.txt_pay_cash.Text = "";
            //this.lbl_pay_sold.Text = "";
        }
    }
    #endregion

    protected void btn_accept_down_invoice_Click(object sender, EventArgs e)
    {
        OrderHelperModel[] ohm = OrderHelperModel.GetModelsByOrderCode(int.Parse(OrderCode));
        if (ohm.Length > 0)
        {
            if (ohm[0].order_invoice.ToString().Length != Config.OrderInvoiceLength)
                CH.Alert("此订单没有发票号码(Invoice No.)", this.lv_sys_list);
            else
            {
                //if (Config.order_complete.IndexOf("[" + ohm[0].pay_method.ToString() + "]") == -1)
                //{
                //    CH.Alert("订单状态不是完成已可下载", this.lv_sys_list);
                //}
                //else
                {
                    ohm[0].is_download_invoice = true;
                    ohm[0].Update();
                    CH.Alert("前台已可下载", this.lv_sys_list);
                }
            }
        }
    }
    protected void btn_add_part_by_name_Click(object sender, EventArgs e)
    {
        try
        {
            string part_name = this.txt_part_name.Text.Trim();
            if (part_name.Length == 0)
            {
                CH.Alert("请输入产品名称", this.lv_sys_list);
                this.txt_part_name.Focus();
                return;
            }
            decimal part_sell;
            decimal.TryParse(this.txt_part_sell.Text, out part_sell);

            if (part_sell == 0M)
            {
                CH.Alert("请输入产品价格.", this.lv_sys_list);
                this.txt_part_sell.Text = "";
                this.txt_part_sell.Focus();
                return;
            }

            Config cc = new Config();
            decimal special_cash = part_sell * 1.1M;
            ProductModel pm = new ProductModel();
            pm.menu_child_serial_no = cc.other_category_id;
            pm.product_short_name = part_name;
            pm.product_name = part_name;
            pm.product_img_sum = 1;
            pm.product_name_long_en = part_name;
            pm.product_order = 1;
            pm.is_non = 0;
            pm.last_regdate = DateTime.Now;
            pm.new_product = 1;
            pm.other_product_sku = 999999;
            pm.product_current_cost = part_sell;
            pm.product_current_price = ConvertPrice.SpecialCashPriceConvertToCardPrice(special_cash);
            pm.product_current_special_cash_price = special_cash;
            pm.regdate = DateTime.Now;
            pm.split_line = 0;
            pm.tag = 1;
            pm.issue = false;
            pm.product_size_id = 1;
            pm.Create();
            
            InsertTraceInfo("Insert new part to IssueStore (in order["+OrderCode+"]): " + pm.product_serial_no.ToString());


            int _product_id = pm.product_serial_no;
          
            //if (!SetSystemProduct(_product_id))
            {



                //int part_count = Config.ExecuteScalarInt32(string.Format("select count(*) from tb_product where product_serial_no ='{0}'", _product_id));
                //if (part_count == 0)
                //{
                //    CH.Alert(" it is not exist.", this.lv_part_list);
                //    return;
                //}
                //else
                {
                    //bool isValid = ProductModel.IsValid(_product_id);
                    //if (!isValid)
                    //{
                    //    CH.Alert(" this is a invalid part.", this.lv_part_list);
                    //    return;
                    //}

                    ProductModel product = pm;
                    ProductCategoryModel pc = ProductCategoryModel.GetProductCategoryModel(product.menu_child_serial_no);
                    OrderProductModel order = new OrderProductModel();
                    order.menu_child_serial_no = product.menu_child_serial_no;
                    order.order_code = OrderCode;
                    order.order_product_cost = product.product_current_cost;
                    order.order_product_price = product.product_current_cost;
                    order.order_product_sum = 1;
                    order.product_name = product.product_name;
                    order.product_serial_no = _product_id;
                    order.sku = _product_id.ToString();

                    order.order_product_sold = product.product_current_cost;// ProductModel.FindOnSaleDiscountByPID(_product_id);
                    order.tag = 1;
                    order.menu_pre_serial_no = product.menu_child_serial_no;
                    order.product_type = Product_category_helper.product_category_value(pc.is_noebook == byte.Parse("1") ? product_category.noebooks : product_category.part_product);
                    order.product_type_name = pc.is_noebook == byte.Parse("1") ? "Noebook" : "Unit";
                    order.product_current_price_rate = product.product_current_price;
                    order.Create();

                    CH.Alert(KeyFields.SaveIsOK, this.lv_part_list);
                    InsertTraceInfo(string.Format("insert part({0}) in order({1})", _product_id, OrderCode));
                    //
                    //  if the order is OK then save a product after create.
                    //
                    if (isOK)
                    {
                        string error = "";
                        OrderHelper oh = new OrderHelper();
                        if (!oh.CopyProductToHistoryStore(order, true, ref error))
                            throw new Exception(error);
                        BindMsgDG();
                    }
                    BindPartList(OrderCode);
                }
            }

            AccountOrder(OrderCode);
        }
        catch (Exception ex)
        {
            CH.Alert(ex.Message, this.lv_part_list);
            CH.CloseParentWatting(this.lv_part_list);
        }
    }

    #region save weee
    protected void btn_save_weee_Click(object sender, EventArgs e)
    {
        try
        {
            decimal weee_charge;

            decimal.TryParse(this.txt_weee_charge.Text, out weee_charge);

            if (weee_charge >= 0M)
            {
                Config.ExecuteNonQuery("Update Tb_order_helper set weee_charge='" + weee_charge.ToString() + "',Is_Modify=1 where order_code='" + OrderCode.ToString() + "'");

                AccountOrder(OrderCode);
                InsertTraceInfo(string.Format("Update WEEE ({0}) in order({1})", weee_charge, OrderCode));
            }
            CH.CloseParentWatting(this.lv_part_list);
        }
        catch (Exception ex)
        {
            CH.CloseParentWatting(this.lv_part_list);
            CH.Alert(ex.Message, this.lv_part_list);
        }
    }
    #endregion


    protected void btn_prev_Click(object sender, EventArgs e)
    {
        //CH.Alert("DD", this.lv_part_list);
        CH.RunJavaScript("window.location.href='/q_admin/orders_modify_paymethod.aspx?order_code=" + OrderCodeRequest + "';", this.lv_part_list);
    }
}
