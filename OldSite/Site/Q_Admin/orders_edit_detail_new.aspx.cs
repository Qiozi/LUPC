using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using LU.Data;

public partial class Q_Admin_orders_edit_detail_new : PageBase
{



    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitPage();
            CH.CloseParentWatting(this.lv_sys_list);
        }
    }


    /// <summary>
    /// 初始化界面数据
    /// </summary>
    void InitPage()
    {
        if (ReqOrderCode > 0)
        {

            OH = OrderHelperModel.GetModelByOrderCode(DBContext, ReqOrderCode);
            CS = CustomerStoreModel.FindByOrderCode(DBContext, ReqOrderCode.ToString());
            if (OH == null
            || CS == null)
            {
                Response.Write("Order is not find.");
                Response.End();
            }

            IsZoomIn = true;
            //if (CS.customer_first_name == CS.customer_shipping_first_name
            //    && CS.customer_shipping_last_name == CS.customer_last_name)
            //{
            //    IsZoomIn = true;
            //    btnZoomInOut_Click(null, null);
            //}
            //else
            //    IsZoomIn = false;


            ViewPrice();
            BindPartList(ReqOrderCode.ToString());
            BindSysList(ReqOrderCode.ToString());
            BindMsgDG();
            ZoomButton();
            BindPayRecordLV(ReqOrderCode);
        }
    }

    /// <summary>
    /// 绑定支付纪录
    /// </summary>
    /// <param name="order_code"></param>
    void BindPayRecordLV(int order_code)
    {
        this.lv_pay_record.DataSource = Config.ExecuteDataTable("select pay_cash, date_format(pay_regdate, \"%b-%d-%Y\") pay_regdate,pay_record_name, balance from tb_order_pay_record opr inner join tb_pay_record_method prm on prm.pay_record_id=opr.pay_record_id where order_code='" + order_code.ToString() + "' order by regdate desc ");
        this.lv_pay_record.DataBind();

        //DataTable dt = Config.ExecuteDataTable(string.Format(@"select grand_total, grand_total- (select ifnull(sum(pay_cash), 0) from tb_order_pay_record where order_code='{0}') blance from tb_order_helper where order_code='{0}'", order_code));
        //if (dt.Rows.Count > 0)
        //{
        //    this.txt_pay_cash.Text = dt.Rows[0]["blance"].ToString();
        //    //this.lbl_pay_sold.Text = dt.Rows[0]["grand_total"].ToString();
        //    this.lbl_pay_balance.Text = dt.Rows[0]["blance"].ToString();
        //}
        //else
        //{
        //    this.txt_pay_cash.Text = "";
        //    //this.lbl_pay_sold.Text = "";
        //}
    }

    /// <summary>
    /// 绑定客户信息
    /// 
    /// </summary>
    public void BindMsgDG()
    {
        this.dl_msg_list.DataSource = ChatMsgModel.FindModelsByOrderCode(DBContext, ReqOrderCode.ToString());
        this.dl_msg_list.DataBind();
    }

    /// <summary>
    /// 绑定系统列表
    /// </summary>
    /// <param name="order_code"></param>
    void BindSysList(string order_code)
    {
        try
        {
            // bind sysetm product 
            DataTable syseteDT = Config.ExecuteDataTable(string.Format(@"select op.*, (op.order_product_sold * order_product_sum) subtotal_2  from tb_order_product op where 
         order_code='{0}' and length(op.product_serial_no) = 8 ", order_code));

            this.lv_sys_list.DataSource = syseteDT;
            this.lv_sys_list.DataBind();

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    /// <summary>
    /// 
    /// </summary>
    void ViewPrice()
    {
        PriceInfo = new OrderPriceViewInfo();

        {
            PriceInfo.ShipCharge = OH.shipping_charge ?? 0M;// ConvertPrice.RoundPrice(dr["shipping_charge"].ToString());
            // _priceInfo this.lbl_order_code.Text = order_code;
            PriceInfo.GrandTotal = OH.grand_total ?? 0M;// this.lbl_grand_total.Text = ConvertPrice.RoundPrice(dr["grand_total"].ToString());
            PriceInfo.SubTotal = OH.sub_total ?? 0M;//  this.lbl_sub_total.Text = ConvertPrice.RoundPrice(dr["sub_total"].ToString());
            PriceInfo.SpecialCashDiscount = OH.input_order_discount.HasValue
                                                ? OH.input_order_discount.Value
                                                : 0M;//  this.lbl_special_cash_discount.Text = ConvertPrice.RoundPrice(dr["input_order_discount"].ToString());
            PriceInfo.InputDiscount = OH.input_order_discount ?? 0M; ;//
            PriceInfo.TaxableTotal = OH.taxable_total ?? 0M; ;// this.lbl_taxable_total.Text = ConvertPrice.RoundPrice(dr["taxable_total"].ToString());
            PriceInfo.Weee = OH.weee_charge.HasValue ? OH.weee_charge.Value : 0M;// this.lbl_weee.Text = ConvertPrice.RoundPrice(dr["weee_charge"].ToString());
            PriceInfo.PriceUnit = OH.price_unit;
            PriceInfo.Hst = OH.hst.HasValue
                                ? OH.hst.Value
                                : 0M;
            PriceInfo.Hst_rate = OH.hst_rate.HasValue
                ? OH.hst_rate.Value
                : 0M;
            PriceInfo.Gst = OH.gst.HasValue
                ? OH.gst.Value
                : 0M;
            PriceInfo.Gst_rate = OH.gst_rate.HasValue
                ? OH.gst_rate.Value
                : 0M;
            PriceInfo.Pst = OH.pst.HasValue
                ? OH.pst.Value
                : 0M;
            PriceInfo.Pst_rate = OH.pst_rate.HasValue ? OH.pst_rate.Value : 0M;
            PriceInfo.PartDiscount = OH.discount.HasValue ? OH.discount.Value : 0M;

        }
    }

    void ZoomButton()
    {
        if (IsZoomIn)
        {
            btnZoomInOut.CssClass = "zoomin";
        }
        else
            btnZoomInOut.CssClass = "zoomout";
    }

    #region 方法
    string Tr1(string title, string text)
    {
        return string.Format("<tr><td class='title'>{0}</td><td>{1}</td></tr>", title, text);
    }

    string Tr2(string title, string text, string note, string btn, string prevChar = "")
    {
        return string.Format(@"
                <tr>
                    <td class='titlePrice'>{0}</td>
                    <td class='price1'>{4}${1}</td>
                    <td>{2}</td>
                    <td>{3}</td>
                </tr>"
            , title
            , text
            , note
            , btn
            , prevChar);
    }
    ///// <summary>
    ///// 电话格式
    ///// </summary>
    ///// <param name="phone_d"></param>
    ///// <param name="phone_n"></param>
    ///// <param name="phone_c"></param>
    ///// <returns></returns>
    //string CustomerPhone(string phone_d, string phone_n, string phone_c)
    //{
    //    string phone = phone_d;
    //    if (string.IsNullOrEmpty(phone))
    //    {
    //        phone = phone_n;
    //        if (string.IsNullOrEmpty(phone))
    //            phone = phone_c;
    //        else
    //        {
    //            if (phone.IndexOf(phone_c) == -1)
    //            {
    //                phone += "," + phone_c;
    //            }
    //        }
    //    }
    //    else
    //    {
    //        if (phone.IndexOf(phone_n) == -1)
    //        {
    //            phone += "," + phone_n;
    //        }
    //        if (phone.IndexOf(phone_c) == -1)
    //        {
    //            phone += "," + phone_c;
    //        }
    //    }
    //    return phone;
    //}

    /// <summary>
    /// 地址格式
    /// </summary>
    /// <param name="country"></param>
    /// <param name="state"></param>
    /// <param name="city"></param>
    /// <param name="zipCode"></param>
    /// <param name="address"></param>
    /// <returns></returns>
    string Address(string country, string state, string city, string zipCode, string address)
    {
        if (string.IsNullOrEmpty(address))
            return "";
        return string.Format("{0} {1} {2} {3} {4}"
            , address + "<br>"
            , city
            , state
            , zipCode
            , country);
    }

    private void BindPartList(string order_code)
    {
        DataTable dt = Config.ExecuteDataTable(string.Format(@"select op.*, (op.order_product_sold * order_product_sum) subtotal_2  from tb_order_product op where 
         order_code='{0}' and length(op.product_serial_no) <>8 ", order_code));

        this.lv_part_list.DataSource = dt;
        this.lv_part_list.DataBind();

        //BindOrderProductHistory(order_code);
    }
    #endregion

    #region 属性
    public bool IsZoomIn
    {
        get
        {
            object obj = Session["iszoomin"];
            if (obj != null)
                return (bool)obj;
            Session["iszoomin"] = false;
            return false;
        }
        set { Session["iszoomin"] = value; }
    }
    /// <summary>
    /// email 
    /// </summary>
    public string SendEmail
    {
        get
        {
            string mail = CS.customer_email2 == "" ? CS.customer_email1 : CS.customer_email2;
            if (mail == "")
                mail = CS.customer_login_name;
            return mail;
        }
    }
    public OrderPriceViewInfo PriceInfo
    {
        get
        {
            object obj = ViewState["PriceInfo"];
            if (obj == null)
            {
                return null;
            }
            else
                return (OrderPriceViewInfo)obj;

        }
        set { ViewState["PriceInfo"] = value; }
    }
    /// <summary>
    /// 
    /// </summary>
    public int ReqOrderCode
    {
        get { return Util.GetInt32SafeFromQueryString(this, "order_code", -1); }
    }
    /// <summary>
    /// 个人信息
    /// </summary>
    public string ViewPersonalInfomation
    {
        get
        {

            string address = Address(CS.customer_country_code, CS.state_code, CS.customer_city, CS.zip_code, CS.customer_address1);

            string str = string.Format(@"
            <table>
                    {0}
                    {1}
                    {2}
                    {3}
                    {4}
                    {5}
            </table>
            "
                , string.IsNullOrEmpty(CS.customer_first_name) ? "" : Tr1("Name", string.Format("{0} {1}", CS.customer_first_name, CS.customer_last_name).Trim())
                , string.IsNullOrEmpty(CS.customer_company) ? "" : Tr1("Company", CS.customer_company)
                , string.IsNullOrEmpty(CS.phone_n) ? "" : Tr1("Home Phone", CS.phone_n)
                , string.IsNullOrEmpty(CS.phone_d) ? "" : Tr1("Business Phone", CS.phone_d)
                , string.IsNullOrEmpty(CS.phone_c) ? "" : Tr1("Mobile Phone", CS.phone_c)
                , string.IsNullOrEmpty(address.Replace("<br>", "").Trim()) ? "" : Tr1("Address", address)
                );
            return str;
        }
    }

    /// <summary>
    /// 显示运输地址
    /// </summary>
    public string ViewShippingAddress
    {
        get
        {

            string address = Address(CS.shipping_country_code, CS.shipping_state_code, CS.customer_shipping_city, CS.customer_shipping_zip_code, CS.customer_shipping_address);

            string str = string.Format(@"
            <table>
                    {0}
                    {1}
            </table>
            "
                , string.IsNullOrEmpty(CS.customer_shipping_last_name) ? "" : Tr1("Name", string.Format("{0} {1}", CS.customer_shipping_first_name, CS.customer_shipping_last_name).Trim())
                , string.IsNullOrEmpty(address.Replace("<br>", "").Trim()) ? "" : Tr1("Address", address)
                );
            return str;
        }
    }

    /// <summary>
    /// 显示历史数量
    /// </summary>
    public string ViewHistoryCount
    {
        get
        {
            int historyCount = OrderProductHistoryModel.FindModelsCountByOrder(ReqOrderCode.ToString());

            return historyCount.ToString();
        }
    }



    /// <summary>
    /// 
    /// </summary>
    public string ViewCustomerMsg
    {
        get
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            var cmms = ChatMsgModel.FindModelsByOrderCode(DBContext, ReqOrderCode.ToString());
            if (cmms != null)
            {
                for (int i = 0; i < cmms.Length; i++)
                {
                    sb.Append(string.Format(@"<div class='msgTitle'>{0}<br/><i>{2}</i></div><div class='msgContent'>{1}</div>"
                        , cmms[i].msg_author
                        , cmms[i].msg_content_text
                        , cmms[i].regdate
                        ));
                }
            }
            return string.Format(@"
            
            ");
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public string ViewCustomerID
    {
        get { return CS.customer_serial_no.ToString(); }
    }

    /// <summary>
    /// 显示信用卡地址
    /// </summary>
    public string ViewCreditCard
    {
        get
        {

            string address = Address(CS.customer_card_country_code, CS.customer_card_state_code, CS.customer_card_city, CS.customer_card_zip_code, CS.customer_card_billing_shipping_address);

            string str = string.Format(@"
            <table>
                    {0}
                    {1}
                    {2}
                    {3}
                    {4}
                    {5}
                    {6}
            </table>
            "
                , string.IsNullOrEmpty(CS.customer_card_first_name) ? "" : Tr1("Name", string.Format("{0} {1}", CS.customer_card_first_name, CS.customer_card_last_name).Trim())
                , string.IsNullOrEmpty(CS.customer_credit_card) ? "" : Tr1("Card Number", CS.customer_credit_card)
                , string.IsNullOrEmpty(CS.card_verification_number) ? "" : Tr1("Verification", CS.card_verification_number)
                , string.IsNullOrEmpty(CS.customer_expiry) ? "" : Tr1("Expiry", CS.customer_expiry)
                , string.IsNullOrEmpty(CS.customer_card_issuer) ? "" : Tr1("Issuer", CS.customer_card_issuer)
                , string.IsNullOrEmpty(CS.customer_card_phone) ? "" : Tr1("Phone", CS.customer_card_phone)
                , string.IsNullOrEmpty(address.Replace("<br>", "").Trim()) ? "" : Tr1("Address", address)
                );
            return str;
        }
    }

    /// <summary>
    /// 显示其他信息
    /// </summary>
    public string ViewOtherInfo
    {
        get
        {
            string shipCompanyName = "None";
            var scm = DBContext.tb_shipping_company.SingleOrDefault(me => me.shipping_company_id == OH.shipping_company);// ShippingCompanyModel.GetShippingCompanyModel(OH.shipping_company);
            if (scm != null)
                shipCompanyName = scm.shipping_company_name;

            string preStatusName = "";
            var psm = DBContext.tb_pre_status.SingleOrDefault(me => me.pre_status_serial_no == OH.pre_status_serial_no);// PreStatusModel.GetPreStatusModel(OH.pre_status_serial_no);
            if (psm != null)
                preStatusName = psm.pre_status_name;

            string backStatusName = "";
            var fsm = FactureStateModel.GetFactureStateModel(DBContext, OH.out_status);
            if (fsm != null)
                backStatusName = fsm.facture_state_name;

            string str = string.Format(@"
            <table>
<tr>
    <td class='title'>TAX EXAMP</TD><td>{0}</td>
    <td class='title'>SHIP METHOD</td><td>{3}</td>
</tr>
<tr>
    <td class='title'>PAYMENT</TD><td>{1}</td>
    <td class='title'>WEB STATUS</td><td>{4}</td>
</tr>
<tr>
    <td class='title'>Piup up schedule</TD><td>{2}</td>
    <td class='title'><td></td>
</tr>
            </table>
            "
             , string.IsNullOrEmpty(CS.tax_execmtion) ? "None" : string.Format("{0}", CS.tax_execmtion).Trim()
             , (CS.pay_method ?? 0) < 1 ? "" : PayMethodNewModel.GetPayMethodNewModel(DBContext, CS.pay_method ?? 0).pay_method_name.ToString()
             , (OH.prick_up_datetime1 == null || OH.prick_up_datetime1.Value.Year == 1 || (OH.prick_up_datetime1.Value.Month == 1 && OH.prick_up_datetime1.Value.Day == 1 && OH.prick_up_datetime1.Value.Hour == 11)) ? "" : Config.pay_method_pick_up_ids.IndexOf("[" + CS.pay_method.ToString() + "]") > -1 ? string.Format("{0:t},{0:D}", OH.prick_up_datetime1) : ""
             , string.Format("{0} ({1}) {2}", shipCompanyName, (OH.shipping_charge ?? 0M).ToString("$0.00"), (OH.is_lock_shipping_charge ?? false) ? "<span style='color:blue;'>Locked</span>" : "")
             , preStatusName
              );
            return str;
        }
    }

    /// <summary>
    /// 价格列表
    /// </summary>
    public string ViewPriceArea
    {
        get
        {
            string str = "";
            str = string.Format(@"
                <table align='right'>
                    {0}
                    {1}
                    {2}
                    {3}
                    {4}
                    {5}
                    {6}
                    {7}
                    {8}
                    {9}
                </table>
"
                , Tr2("Sub Total", ConvertPrice.RoundPrice(PriceInfo.SubTotal).ToString(), "", "")
                , Tr2("Special Cash Discount", ConvertPrice.RoundPrice(PriceInfo.SpecialCashDiscount).ToString(), (OH.is_lock_input_order_discount.HasValue ? "<span title='Locked' style='color:blue'>L</span>" : ""), BtnInputDiscount, "-")
                , Tr2("Ship Charge", ConvertPrice.RoundPrice(PriceInfo.ShipCharge).ToString(), (OH.is_lock_shipping_charge.HasValue ? "<span title='Locked' style='color:blue'>L</span>" : ""), BtnInputShipCharge)
                , Tr2("Taxable Total", ConvertPrice.RoundPrice(PriceInfo.TaxableTotal).ToString(), "", "")
                , PriceInfo.Hst_rate > 0M ? Tr2("HST", ConvertPrice.RoundPrice(PriceInfo.Hst).ToString(), "(" + PriceInfo.Hst_rate.ToString("0") + "%)", BtnInputPriceUnit) : ""
                , PriceInfo.Pst > 0M ? Tr2("PST", ConvertPrice.RoundPrice(PriceInfo.Pst).ToString(), "(" + PriceInfo.Pst_rate.ToString("0") + "%)", BtnInputPriceUnit) : ""
                , PriceInfo.Gst > 0M ? Tr2("GST", ConvertPrice.RoundPrice(PriceInfo.Gst).ToString(), "(" + PriceInfo.Gst_rate.ToString("0") + "%)", BtnInputPriceUnit) : ""
                , PriceInfo.Hst == 0M && PriceInfo.Gst == 0M && PriceInfo.Pst == 0M ? Tr2("Sale Tax", "0", "", BtnInputPriceUnit) : ""
                , Tr2("WEEE", ConvertPrice.RoundPrice(PriceInfo.Weee).ToString(), "", BtnInputWEEE)
                , Tr2("Grand Total", ConvertPrice.RoundPrice(PriceInfo.GrandTotal).ToString(), PriceInfo.PriceUnit, BtnInputPriceUnit)
                );
            return str;
        }
    }

    public tb_order_helper OH
    {
        get
        {
            object obj = ViewState["OrderHelperModel"];
            if (obj != null)
            {
                return (tb_order_helper)obj;
            }
            else
                return null;
        }
        set { ViewState["OrderHelperModel"] = value; }
    }

    public tb_customer_store CS
    {
        get
        {
            object obj = ViewState["CustomerStoreModel"];
            if (obj != null)
            {
                return (tb_customer_store)obj;
            }
            else
                return null;
        }
        set { ViewState["CustomerStoreModel"] = value; }
    }

    /// <summary>
    /// 输入现金折扣按钮
    /// </summary>
    string BtnInputDiscount
    {
        get
        {
            return string.Format(@"<a 
title=""Modify Fee""
onclick=""ShowIframe('Modify fee','/q_admin/orders_edit_detail_modify_fee.aspx?is_new=1&OrderCode='+ $('#htmlOrderCode').val(),800,450); return false;""
>Modify</a>");
        }
    }

    /// <summary>
    /// 输入运费按钮
    /// </summary>
    string BtnInputShipCharge
    {
        get { return BtnInputDiscount; }
    }

    /// <summary>
    /// 输入WEEE税按钮
    /// </summary>
    string BtnInputWEEE
    {
        get { return BtnInputDiscount; }
    }
    /// <summary>
    /// 输入价格单位，美金或加币
    /// </summary>
    string BtnInputPriceUnit
    {
        get { return BtnInputDiscount; }
    }
    /// <summary>
    /// 无税提醒
    /// </summary>
    public string ViewWarn
    {
        get
        {
            if (OH.gst_rate == 0M
                && OH.pst_rate == 0M
                && OH.hst_rate == 0M)
            {
                return "<div style='background:red;padding: 10px; color:white'>此订单无税</div>";
            }
            if (OH.shipping_charge < 1)
            {
                return "<div style='background:red;padding: 10px; color:white'>此订单没有运费</div>";
            }
            return "";
        }

    }

    #endregion

    protected void lv_part_list_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        try
        {
            TextBox __txt_sum_part = (TextBox)e.Item.FindControl("_txt_sum_part");
            Literal _literal_sub_total = (Literal)e.Item.FindControl("literal_sub_total");
            TextBox __txt_sold_part = (TextBox)e.Item.FindControl("_txt_sold_part");
            Literal luc_SKU = (Literal)e.Item.FindControl("_literal_system_code");
            HiddenField idc = (HiddenField)e.Item.FindControl("_hf_serial_no");

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
                    Config.ExecuteNonQuery(string.Format("update tb_order_product set order_product_sum='{1}' where serial_no='{0}'", idc.Value, _p_sum));

                    break;
                case "DeletePart":
                    int _serial_no;
                    int.TryParse(e.CommandArgument.ToString(), out _serial_no);

                    Config.ExecuteNonQuery(string.Format("delete from tb_order_product where serial_no='{0}'", _serial_no));
                    //BindPartList(ReqOrderCode.ToString());
                    //AccountOrder(OrderCode);
                    break;

                case "SavePart":
                    int serial_no;
                    int.TryParse(e.CommandArgument.ToString(), out serial_no);

                    Config.ExecuteNonQuery(string.Format("Update tb_order_product set order_product_sum='{0}', order_product_sold='{1}' where serial_no='{2}'", _sum, _sold, serial_no));

                    //AccountOrder(OrderCode);
                    //CH.Alert(KeyFields.SaveIsOK, this.lv_sys_list);
                    break;
            }
            OrdersSavePageRedirect();

        }
        catch (Exception ex)
        {
            CH.Alert(ex.Message, this.lv_part_list);
        }
    }


    protected void btn_Add_Part_Click(object sender, EventArgs e)
    {
        try
        {
            int _product_id = 0;
            int.TryParse(this.txt_input_part.Text, out _product_id);

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

                    var product = ProductModel.GetProductModel(DBContext, _product_id);
                    var pc = ProductCategoryModel.GetProductCategoryModel(DBContext, product.menu_child_serial_no.Value);
                    var order = new tb_order_product();// OrderProductModel();

                    order.menu_child_serial_no = product.menu_child_serial_no;
                    order.order_code = ReqOrderCode.ToString();
                    order.order_product_cost = ConvertPrice.Price(OH.price_unit.ToLower().ToString() == "cad" ? CountryCategory.CA : CountryCategory.US, (product.product_current_cost??0));
                    order.order_product_price = ConvertPrice.Price(OH.price_unit.ToLower().ToString() == "cad" ? CountryCategory.CA : CountryCategory.US, (product.product_current_price??0));

                    order.order_product_sum = 1;
                    order.product_name = product.product_name + (product.prodType.ToLower() != "new" ? " (" + product.prodType + ")" : "");
                    order.product_serial_no = _product_id;
                    order.sku = _product_id.ToString();

                    order.order_product_sold = ConvertPrice.Price(OH.price_unit.ToLower().ToString() == "cad" ? CountryCategory.CA : CountryCategory.US, (product.product_current_price ?? 0) - (product.product_current_discount ?? 0));// ProductModel.FindOnSaleDiscountByPID(_product_id);
                    //throw new Exception(CC.ToString());
                    order.tag = 1;
                    order.menu_pre_serial_no = product.menu_child_serial_no;
                    order.product_type = Product_category_helper.product_category_value(pc.is_noebook == byte.Parse("1") ? product_category.noebooks : product_category.part_product);
                    order.product_type_name = pc.is_noebook == byte.Parse("1") ? "Noebook" : "Unit";
                    order.product_current_price_rate = ConvertPrice.Price(OH.price_unit.ToLower().ToString() == "cad" ? CountryCategory.CA : CountryCategory.US,(product.product_current_price??0));
                    order.prodType = product.prodType;
                    DBContext.tb_order_product.Add(order);
                    DBContext.SaveChanges();

                    CH.Alert(KeyFields.SaveIsOK, this.lv_part_list);
                    InsertTraceInfo(DBContext, string.Format("insert part({0}) in order({1})", _product_id, ReqOrderCode.ToString()));
                    //
                    //  if the order is OK then save a product after create.
                    //
                    if (OH.is_ok.Value)
                    {
                        string error = "";
                        var oh = new OrderHelper(DBContext);
                        if (!oh.CopyProductToHistoryStore(order, true, ref error))
                            throw new Exception(error);

                    }

                    OrdersSavePageRedirect();
                    // CH.Alert("DD", this.lv_part_list);
                }
            }

        }
        catch (Exception ex)
        {
            CH.Alert(ex.Message, this.lv_part_list);
            CH.CloseParentWatting(this.lv_part_list);
        }
    }


    void OrdersSavePageRedirect()
    {
        OrdersSavePageRedirect(ReqOrderCode, false);
    }


    protected void btn_Add_Part_Name_Click(object sender, EventArgs e)
    {
        try
        {
            string part_name = this.txt_input_part_name.Text.Trim();
            if (part_name.Length == 0)
            {
                CH.Alert("请输入产品名称", this.lv_part_list);
                this.txt_input_part_name.Focus();
                return;
            }

            decimal part_sell;
            decimal.TryParse(this.txt_input_part_sell.Text, out part_sell);
            if (part_sell == 0M)
            {
                CH.Alert("请输入产品价格.", this.lv_part_list);
                this.txt_input_part_sell.Text = "";
                this.txt_input_part_sell.Focus();
                return;
            }

            Config cc = new Config();
            decimal special_cash = part_sell * 1.1M;
            var pm = new tb_product();// ProductModel();
            pm.menu_child_serial_no = cc.other_category_id;
            pm.product_short_name = part_name;
            pm.product_name = part_name;
            pm.product_img_sum = 1;
            pm.product_name_long_en = part_name;
            pm.product_order = 1;
            pm.is_non = 0;
            pm.last_regdate = DateTime.Now;
            pm.@new = 1;
            pm.other_product_sku = 999999;
            pm.product_current_cost = part_sell;
            pm.product_current_price = ConvertPrice.SpecialCashPriceConvertToCardPrice(special_cash);
            pm.product_current_special_cash_price = special_cash;
            pm.regdate = DateTime.Now;
            pm.split_line = 0;
            pm.tag = 1;
            pm.issue = false;
            pm.real_cost_regdate = DateTime.Now;
            pm.adjustment_regdate = DateTime.Now;
            pm.product_size_id = 1;
            pm.prodType = "NEW";
            DBContext.tb_product.Add(pm);
            DBContext.SaveChanges();

            InsertTraceInfo(DBContext, "Insert new part to IssueStore (in order[" + ReqOrderCode.ToString() + "]): " + pm.product_serial_no.ToString());

            int _product_id = pm.product_serial_no;

            var product = pm;
            var pc = ProductCategoryModel.GetProductCategoryModel(DBContext, product.menu_child_serial_no.Value);
            var order = new tb_order_product();// OrderProductModel();
            order.menu_child_serial_no = product.menu_child_serial_no;
            order.order_code = ReqOrderCode.ToString();
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
            DBContext.tb_order_product.Add(order);
            DBContext.SaveChanges();

            InsertTraceInfo(DBContext, string.Format("insert part({0}) in order({1})", _product_id, ReqOrderCode));
            //
            //  if the order is OK then save a product after create.
            //
            if (OH.is_ok.Value)
            {
                string error = "";
                var oh = new OrderHelper(DBContext);
                if (!oh.CopyProductToHistoryStore(order, true, ref error))
                    throw new Exception(error);
            }

            OrdersSavePageRedirect();
            // CH.Alert(KeyFields.SaveIsOK, this.lv_part_list);
        }
        catch (Exception ex)
        {
            CH.Alert(ex.Message, this.lv_part_list);
            CH.CloseParentWatting(this.lv_part_list);
        }
    }

    protected void btn_add_sys_Click(object sender, EventArgs e)
    {
        try
        {

            if (ReqOrderCode < 1)
            {
                CH.Alert("order number is lost.", this.lv_sys_list);
                return;
            }

            string error = "";
            string system_tmp_sku = this.txt_sys_sku.Text.Trim();

            var oh = new OrderHelper(DBContext);
            XmlStore xs = new XmlStore();
            DataTable partGroup = xs.FindPartGroupComment();
            oh.CopySystemToOrder(system_tmp_sku, true, ReqOrderCode.ToString(), partGroup, OH.price_unit.ToLower().ToLower() == "cad" ? CountryCategory.CA : CountryCategory.US, ref error);

            InsertTraceInfo(DBContext, string.Format("add system{1} to order({0})", ReqOrderCode, system_tmp_sku));
            CH.CloseParentWatting(this.lv_sys_list);

            OrdersSavePageRedirect();
            // CH.Alert(KeyFields.SaveIsOK, this.lv_sys_list);
        }
        catch (Exception ex)
        {
            CH.CloseParentWatting(this.lv_sys_list);
            CH.Alert(ex.Message, this.lv_sys_list);
        }
    }

    protected void btn_duplicate_sys_Click(object sender, EventArgs e)
    {
        try
        {

            if (ReqOrderCode < 1)
            {
                CH.Alert("order number is lost.", this.lv_sys_list);
                return;
            }
            var oh = new OrderHelper(DBContext);
            //string error = "";
            string system_tmp_sku = oh.CopySystemToOrderReturnNewCode(this.txt_sys_sku.Text.Trim(), ReqOrderCode.ToString());
            // throw new Exception(system_tmp_sku);

            XmlStore xs = new XmlStore();
            DataTable partGroup = xs.FindPartGroupComment();

            //oh.CopySystemToOrder(system_tmp_sku, true, ReqOrderCode.ToString(), partGroup, OH.price_unit.ToLower().ToLower() == "cad" ? CountryCategory.CA : CountryCategory.US, ref error);

            InsertTraceInfo(DBContext, string.Format("Duplicate system{1} to order({0})", ReqOrderCode, system_tmp_sku));
            CH.CloseParentWatting(this.lv_sys_list);

            OrdersSavePageRedirect();
            // CH.Alert(KeyFields.SaveIsOK, this.lv_sys_list);
        }
        catch (Exception ex)
        {
            CH.CloseParentWatting(this.lv_sys_list);
            CH.Alert(ex.Message, this.lv_sys_list);
        }
    }

    protected void lv_sys_list_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        Repeater rpt = (Repeater)e.Item.FindControl("_rpt_sys_detail");

        TextBox __txt_sum_part = (TextBox)e.Item.FindControl("_txt_sum_part");
        Literal _literal_sub_total = (Literal)e.Item.FindControl("literal_sub_total");
        TextBox __txt_sold_part = (TextBox)e.Item.FindControl("_txt_sold_part");
        Literal _literal_system_code = (Literal)e.Item.FindControl("_literal_system_code");

        int _sum;
        int.TryParse(__txt_sum_part.Text, out _sum);


        decimal _sold;
        decimal.TryParse(__txt_sold_part.Text.ToString(), out _sold);
        int system_code;
        int.TryParse(_literal_system_code.Text, out system_code);
        Panel _panel_detail = (Panel)e.Item.FindControl("_panel_sys_detail");
        _panel_detail.Visible = !_panel_detail.Visible;

        //Repeater rpt = (Repeater)e.Item.FindControl("_rpt_sys_detail");
        //if (_panel_detail.Visible)
        BindSystemDetail(rpt, system_code, false);
        // CH.Alert("OK", this.lv_sys_list);
    }

    protected void lv_sys_list_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        try
        {
            Repeater rpt = (Repeater)e.Item.FindControl("_rpt_sys_detail");

            TextBox __txt_sum_part = (TextBox)e.Item.FindControl("_txt_sum_part");
            Literal _literal_sub_total = (Literal)e.Item.FindControl("literal_sub_total");
            TextBox __txt_sold_part = (TextBox)e.Item.FindControl("_txt_sold_part");
            HiddenField _hf_serial_no = (HiddenField)e.Item.FindControl("_hf_serial_no");
            int _sum;
            int.TryParse(__txt_sum_part.Text, out _sum);


            decimal _sold;
            decimal.TryParse(__txt_sold_part.Text.ToString(), out _sold);

            switch (e.CommandName)
            {

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
                    var p = ProductModel.GetProductModel(DBContext, product_id);
                    // throw new Exception((_sold + ((p.product_current_price - p.product_current_discount) * part_quantity)).ToString());

                    ChangeSystemPrice(e.Item, _system_code, OH.order_source == 3, _sold + ((p.product_current_price.Value - p.product_current_discount.Value) * part_quantity));

                    break;

                case "SetPartSum":
                    int _p_sum;
                    int.TryParse(e.CommandArgument.ToString(), out _p_sum);
                    __txt_sum_part.Text = _p_sum.ToString();
                    _literal_sub_total.Text = (_p_sum * _sold).ToString();
                    __txt_sum_part.ForeColor = System.Drawing.Color.FromName("red");
                    Config.ExecuteNonQuery(string.Format("Update tb_order_product set order_product_sum='{0}' where serial_no='{1}'", _p_sum, _hf_serial_no.Value));

                    break;

                case "SaveSys":
                    int serial_no;
                    int.TryParse(e.CommandArgument.ToString(), out serial_no);

                    Config.ExecuteNonQuery(string.Format("Update tb_order_product set order_product_sum='{0}', order_product_sold='{1}' where serial_no='{2}'", _sum, _sold, serial_no));

                    break;

                case "DeleteSys":
                    int _serial_no;
                    int.TryParse(e.CommandArgument.ToString(), out _serial_no);
                    string ___system_code = ((Literal)e.Item.FindControl("_literal_system_code")).Text;
                    Config.ExecuteNonQuery(string.Format("delete from tb_order_product where serial_no='{0}'; delete from tb_order_product_sys_detail where sys_tmp_code='{1}'", _serial_no, ___system_code));

                    InsertTraceInfo(DBContext, string.Format("delete to system({1}) in order({0})", ReqOrderCode, _serial_no));
                    break;
            }
            OrdersSavePageRedirect();
            // CH.Alert("OK", this.lv_sys_list);
            CH.CloseParentWatting(this.lv_sys_list);
        }
        catch (Exception ex)
        {
            CH.Alert(ex.Message, this.lv_sys_list);
        }
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
                    int part_quantity = 0;
                    //decimal partSold = 0M;
                    string error = "";

                    int sum = SystemSum(system_code.ToString());
                    var m = OrderProductSysDetailModel.GetOrderProductSysDetailModel(DBContext, sys_detail_id);
                    //SpTmpDetailModel m = SpTmpDetailModel.GetSpTmpDetailModel(sys_detail_id);
                    var pm = ProductModel.GetProductModel(DBContext, part_sku);
                    var ohelper = new OrderHelper(DBContext);
                    if (!ohelper.CopyProductToHistoryStore(pm, m, ReqOrderCode.ToString(), sum, false, ref error))
                        throw new Exception(error);
                    m = OrderProductSysDetailModel.GetOrderProductSysDetailModel(DBContext, sys_detail_id);
                    part_quantity = m.part_quantity.Value;

                    DBContext.tb_order_product_sys_detail.Remove(m);
                    DBContext.SaveChanges();

                    Config.ExecuteNonQuery("Delete from tb_sp_tmp_detail Where sys_tmp_code='" + m.sys_tmp_code.ToString() + "' and part_quantity='" + m.part_quantity.ToString() + "' and product_serial_no='" + m.product_serial_no.ToString() + "'");
                    for (int i = 0; i < this.lv_sys_list.Items.Count; i++)
                    {
                        Repeater _rpt = (Repeater)this.lv_sys_list.Items[i].FindControl("_rpt_sys_detail");
                        if (rpt == _rpt)
                        {

                            decimal orderProductPrice = 0M;
                            if (OH.order_source == 3)
                            {
                                var ops = OrderProductModel.GetModelsByProductCode(DBContext, system_code);
                                orderProductPrice = ops[0].order_product_sold.Value;
                            }
                            ChangeSystemPrice(this.lv_sys_list.Items[i], system_code, OH.order_source == 3, orderProductPrice - (pm.product_current_price.Value - pm.product_current_discount.Value) * part_quantity);
                        }
                    }
                    break;
            }
            OrdersSavePageRedirect();
            CH.CloseParentWatting(this.lv_sys_list);
        }
        catch (Exception ex)
        {
            CH.CloseParentWatting(this.lv_sys_list);
            CH.Alert(ex.Message, this.lv_sys_list);
        }
    }

    public int SystemSum(string system_code)
    {
        return Config.ExecuteScalarInt32(string.Format(@"Select sum(order_product_sum) from tb_order_product where product_serial_no='{0}'", system_code));
    }

    private void BindSystemDetail(Repeater gv, int system_code, bool bind)
    {
        if (gv.Items.Count < 2 || bind)
        {
            //gv.DataSource = SystemProductModel.FindSystemDetail(system_code);
            gv.DataSource = Config.ExecuteDataTable("Select * from tb_order_product_sys_detail WHere sys_tmp_code='" + system_code.ToString() + "' and product_name<>'none selected' order by product_order asc");

            gv.DataBind();
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

        var m = new tb_sp_tmp_detail();// SpTmpDetailModel();
        var sdms = SpTmpDetailModel.GetModelsBySysTmpCode(DBContext, system_code.ToString());

        var singlem = new tb_sp_tmp_detail();// SpTmpDetailModel();
        if (sdms.Length > 0)
            singlem = sdms[0];

        var p = ProductModel.GetProductModel(DBContext, product_id);
        var pc = ProductCategoryModel.GetProductCategoryModel(DBContext, p.menu_child_serial_no.Value);
        m.product_current_cost = p.product_current_cost.Value;
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

        var pgdm = new tb_part_group_detail();
        DataTable pgdt = new PartGroupDetailModel().FindPartGroupNameByPartSku(product_id);
        if (pgdt.Rows.Count > 0)
        {
            int part_group_id;
            int.TryParse(pgdt.Rows[0]["part_group_id"].ToString(), out part_group_id);
            m.part_group_id = part_group_id;
            m.cate_name = pgdt.Rows[0]["part_group_name"].ToString();
        }

        DBContext.tb_sp_tmp_detail.Add(m);
        DBContext.SaveChanges();

        var opsdm = new tb_order_product_sys_detail();// OrderProductSysDetailModel();
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
        // opsdm.Create();
        DBContext.tb_order_product_sys_detail.Add(opsdm);
        DBContext.SaveChanges();

        InsertTraceInfo(DBContext, string.Format("add part({2}) to system({1}) in order({0})", ReqOrderCode, system_code, product_id));

        string error = "";
        var oh = new OrderHelper(DBContext);

        int sum = SystemSum(system_code.ToString());

        if (!oh.CopyProductToHistoryStore(p, opsdm, ReqOrderCode.ToString(), sum, true, ref error))
            throw new Exception(error);

    }

    /// <summary>
    /// 改变系统价格
    /// </summary>
    /// <param name="lvi"></param>
    /// <param name="system_code"></param>
    private void ChangeSystemPrice(ListViewItem lvi, int system_code, bool isEbay, decimal charge)
    {

        DataTable dt;
        if (isEbay)
        {
            //throw new Exception(system_code.ToString());

            dt = Config.ExecuteDataTable(string.Format(@"update tb_order_product set 
order_product_price=(select sum(product_current_price*part_quantity) from tb_sp_tmp_detail where sys_tmp_code='{0}')
, order_product_cost = (select sum(product_current_cost*part_quantity) from tb_sp_tmp_detail where sys_tmp_code='{0}')
, order_product_sold = '{1}'
, save_price = (select sum(save_price*part_quantity) from tb_sp_tmp_detail where sys_tmp_code='{0}')
, product_current_price_rate =(select sum(product_current_price*part_quantity) from tb_sp_tmp_detail where sys_tmp_code='{0}') 

where product_serial_no='{0}';

select order_product_price, order_product_cost, order_product_sold, save_price, product_current_price_rate from tb_order_product where  product_serial_no='{0}';"
                , system_code
                , charge));

        }
        else
        {

            dt = Config.ExecuteDataTable(string.Format(@"update tb_order_product set 
order_product_price=(select sum(product_current_price*part_quantity) from tb_order_product_sys_detail where sys_tmp_code='{0}')
, order_product_cost = (select sum(product_current_cost*part_quantity) from tb_order_product_sys_detail where sys_tmp_code='{0}')
, order_product_sold = (select sum(product_current_sold*part_quantity) from tb_order_product_sys_detail where sys_tmp_code='{0}')
, save_price = (select sum(save_price*part_quantity) from tb_order_product_sys_detail where sys_tmp_code='{0}')
, product_current_price_rate =(select sum(product_current_price*part_quantity) from tb_order_product_sys_detail where sys_tmp_code='{0}') 

where product_serial_no='{0}';

select order_product_price, order_product_cost, order_product_sold, save_price, product_current_price_rate from tb_order_product where  product_serial_no='{0}';", system_code));
        }

        if (dt.Rows.Count > 0)
        {
            Literal _literal_order_product_cost = (Literal)lvi.FindControl("_literal_order_product_cost");
            Literal _literal_order_product_price = (Literal)lvi.FindControl("_literal_order_product_price");
            TextBox _txt_sold_part = (TextBox)lvi.FindControl("_txt_sold_part");
            TextBox _txt_sum_part = (TextBox)lvi.FindControl("_txt_sum_part");
            Literal literal_sub_total = (Literal)lvi.FindControl("literal_sub_total");
            HiddenField _hf_serial_no = (HiddenField)lvi.FindControl("_hf_serial_no");

            DataRow dr = dt.Rows[0];

            int sum;
            int.TryParse(_txt_sum_part.Text, out sum);

            decimal sold;
            decimal.TryParse(dr["order_product_sold"].ToString(), out sold);


            _literal_order_product_cost.Text = dr["order_product_cost"].ToString();
            _literal_order_product_price.Text = dr["order_product_price"].ToString();
            _txt_sold_part.Text = sold.ToString();
            literal_sub_total.Text = (sum * sold).ToString();
            if (!isEbay)
            {
                var opm = OrderProductModel.GetOrderProductModel(DBContext, int.Parse(_hf_serial_no.Value));
                opm.order_product_sold = sold;
                opm.order_product_sum = sum;
                DBContext.SaveChanges();
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btn_prev_Click(object sender, EventArgs e)
    {
        CH.RunJavaScript("window.location.href='/q_admin/orders_modify_paymethod.aspx?order_code=" + ReqOrderCode.ToString() + "';", this.lv_part_list);
    }
    /// <summary>
    /// 使客户可以下载发票，订单默认只下载order form
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btn_accept_down_invoice_Click(object sender, EventArgs e)
    {
        if (OH != null)
        {

            if (string.IsNullOrEmpty(OH.order_invoice))
                CH.Alert("此订单没有发票号码(Invoice No.)", this.lv_sys_list);
            else
            {
                var model = DBContext.tb_order_helper.Single(me => me.order_helper_serial_no.Equals(OH.order_helper_serial_no));

                model.is_download_invoice = true;
                DBContext.SaveChanges();
                CH.Alert("前台已可下载", this.lv_sys_list);
            }
        }
    }
    protected void btnZoomInOut_Click(object sender, EventArgs e)
    {
        if (IsZoomIn)
        {
            personalInfoArea.Style.Add("display", "None");
            creditCardArea.Style.Add("display", "None");
            IsZoomIn = false;

        }
        else
        {
            personalInfoArea.Style.Add("display", "");
            creditCardArea.Style.Add("display", "");
            IsZoomIn = true;
        }
        ZoomButton();
    }
}