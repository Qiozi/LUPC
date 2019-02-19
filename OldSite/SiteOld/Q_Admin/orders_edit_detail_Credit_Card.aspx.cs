using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Q_Admin_orders_edit_detail_Credit_Card : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            otherCountryArea.Visible = false;
            InitialDatabase();
        }
    }

    public override void InitialDatabase()
    {
        base.InitialDatabase();
        InitPage(OrderAddressType.CreditCard);
    }

    void InitPage(OrderAddressType oat)
    {
        CustomerStoreModel customer = CustomerStoreModel.FindByOrderCode(ReqOrderCode.ToString());
        if (customer != null)
        {
            //
            // shipping info
            //           
            CustomerID = customer.customer_serial_no;
            StoreCustomerSerialNo = customer.serial_no;

            string countryCode = "";//cm.customer_country_code;
            string stateCode="";//;// = cm.state_code;
            string address = "";
            string city = "";
            string zipCode = "";
            string firstName = "";
            string lastName = "";
            switch (oat)
            {
                case OrderAddressType.CreditCard:
                    countryCode = customer.customer_card_country_code;
                    stateCode = customer.customer_card_state_code;
                    address = customer.customer_card_billing_shipping_address;
                    city = customer.customer_card_city;
                    zipCode = customer.customer_card_zip_code;
                    firstName = customer.customer_card_first_name;
                    lastName = customer.customer_card_last_name;
                    break;
                case OrderAddressType.Person:
                    countryCode = customer.customer_country_code;
                    stateCode = customer.state_code;
                    address = customer.customer_address1;
                    city = customer.customer_city;
                    zipCode = customer.zip_code;
                    firstName = customer.customer_first_name;
                    lastName = customer.customer_last_name;
                    break;
                case OrderAddressType.Shipping:
                    countryCode = customer.shipping_country_code;
                    stateCode = customer.shipping_state_code;
                    address = customer.customer_shipping_address;
                    city = customer.customer_shipping_city;
                    zipCode = customer.customer_shipping_zip_code;
                    firstName = customer.customer_shipping_first_name;
                    lastName = customer.customer_shipping_last_name;
                    break;
            }

        //cm.customer_card_billing_shipping_address = cm.customer_address1;
        //cm.customer_card_city = cm.customer_city;
        //cm.customer_card_zip_code = cm.zip_code;
        //cm.customer_card_first_name = cm.customer_first_name;
        //cm.customer_card_last_name = cm.customer_last_name;
            //
            // card info
            //
            this.txt_card_billing_shipping_address.Text = address;
            this.txt_card_city.Text = city;
            this.txt_card_number.Text = customer.customer_credit_card;
            this.txt_card_expiry.Text = customer.customer_expiry;
            this.txt_card_zip_code.Text = zipCode;
            this.txt_card_isssuer.Text = customer.customer_card_issuer;
            this.txt_card_phone.Text = customer.customer_card_phone;
            this.txt_verification_number.Text = customer.card_verification_number;
            this.txt_card_first_name.Text = firstName;
            this.txt_card_last_name.Text = lastName;

            if (countryCode.ToLower() == "us"
                || countryCode.ToLower() == "ca")
            {
                BindCardCountry(countryCode ?? "");

                if ((countryCode ?? "").Length == 2)
                {
                    BindCardState((CountryCategory)Enum.Parse(typeof(CountryCategory), countryCode), stateCode ?? "");
                }
                else
                    BindCardState(CountryCategory.CA, "");
                otherCountryArea.Visible = false;
            }
            else
            {
                BindCardCountry(countryCode ?? "");
                this.ddl_card_country.SelectedValue = "Other";
                this.txt_inputCountry.Text = countryCode ?? "";
                this.txt_inputState.Text = stateCode ?? "";
                otherCountryArea.Visible = true;
            }

        }
    }

    private void BindCardCountry(string selectedValue)
    {
        XmlStore xs = new XmlStore();
        this.ddl_card_country.DataSource = CountryCategoryHelper.CountryCategoryToDataTable();
        this.ddl_card_country.DataTextField = "text";
        this.ddl_card_country.DataValueField = "text";
        this.ddl_card_country.DataBind();
        if (selectedValue.Length == 2)
            this.ddl_card_country.SelectedValue = selectedValue;
        else
            this.ddl_card_country.SelectedIndex = 0;
       
    }

    private void BindCardState(CountryCategory cc, string selectedValue)
    {
        //CH.Alert(countryID.ToString(), this.lv_part_list);
        XmlStore xs = new XmlStore();
        DataTable ssm = xs.FindStateByCountry(cc);
        if (ssm != null)
        {
            this.ddl_card_state.DataSource = ssm;
            this.ddl_card_state.DataTextField = "state_name";
            this.ddl_card_state.DataValueField = "state_code";
            this.ddl_card_state.DataBind();
            if (selectedValue.Length == 2)
                this.ddl_card_state.SelectedValue = selectedValue;
            else
            {
                this.ddl_card_state.SelectedIndex = 0;
            }
        }
        else
            this.ddl_card_state.Items.Clear();
    }

    #region request field
    /// <summary>
    /// 是否返回到新编辑界面
    /// </summary>
    bool ReqIsNew
    {
        get { return Util.GetInt32SafeFromQueryString(this, "is_new", -1) == 1; }
    }
    /// <summary>
    /// 订单号
    /// </summary>
    private int ReqOrderCode
    {
        get { return Util.GetInt32SafeFromQueryString(this, "OrderCode", -1); }
    }
    #endregion

    #region fields
    /// <summary>
    /// 客户ID
    /// </summary>
    int CustomerID
    {
        get
        {
            int id;
            int.TryParse((ViewState["CustomerID"] ?? "").ToString(), out id);
            return id;
        }
        set { ViewState["CustomerID"] = value; }
    }

    /// <summary>
    /// 客户在customer store 里的serial no.
    /// </summary>
    int StoreCustomerSerialNo
    {
        get
        {
            int id;
            int.TryParse((ViewState["StoreCustomerSerialNo"] ?? "").ToString(), out id);
            return id;
        }
        set { ViewState["StoreCustomerSerialNo"] = value; }
    }
    #endregion

    protected void ddl_card_country_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_card_country.SelectedValue.ToLower() == "other")
        {
            otherCountryArea.Visible = true;
            this.ddl_card_state.Visible = false;
        }
        else
        {
            otherCountryArea.Visible = false;
            ddl_card_state.Visible = true;
            BindCardState((CountryCategory)Enum.Parse(typeof(CountryCategory), this.ddl_card_country.SelectedValue.ToString()), "");
        }
    }
    protected void bt_save_credit_card_Click(object sender, EventArgs e)
    {

        CustomerModel cm = CustomerModel.GetCustomerModel(CustomerID);
        cm.customer_credit_card = this.txt_card_number.Text.Trim();
        cm.customer_card_issuer = this.txt_card_isssuer.Text.Trim();
        cm.customer_card_phone = this.txt_card_phone.Text.Trim();
        cm.customer_expiry = this.txt_card_expiry.Text.Trim();
        cm.card_verification_number = this.txt_verification_number.Text.Trim();
        if (ddl_card_country.SelectedValue.ToLower() == "other")
        {
            cm.customer_card_country_code = txt_inputCountry.Text.Trim();
            cm.customer_card_state_code = txt_inputState.Text.Trim();
            new StateShippingModel().SaveNewState(cm.customer_card_country_code, cm.customer_card_state_code);
        }
        else
        {
            cm.customer_card_country_code = this.ddl_card_country.SelectedValue;
            cm.customer_card_state_code = this.ddl_card_state.SelectedValue;
        }
        cm.customer_card_billing_shipping_address = this.txt_card_billing_shipping_address.Text.Trim();
        cm.customer_card_city = this.txt_card_city.Text.Trim();
        cm.customer_card_zip_code = this.txt_card_zip_code.Text.Trim();
        cm.customer_card_first_name = this.txt_card_first_name.Text.Trim();
        cm.customer_card_last_name = this.txt_card_last_name.Text.Trim();

        if (CustomerID != 7888888)
        {
            cm.Update();
        }

        CustomerStoreModel csm = CustomerStoreModel.GetCustomerStoreModel(StoreCustomerSerialNo);
        csm.customer_credit_card = cm.customer_credit_card;
        csm.customer_card_issuer = cm.customer_card_issuer;
        csm.customer_card_phone = cm.customer_card_phone;
        csm.customer_expiry = cm.customer_expiry;
        csm.card_verification_number = cm.card_verification_number;
        if (ddl_card_country.SelectedValue.ToLower() == "other")
        {
            csm.customer_card_country_code = txt_inputCountry.Text.Trim();
            csm.customer_card_state_code = txt_inputState.Text.Trim();
        }
        else
        {
            csm.customer_card_country_code = cm.customer_card_country_code;
            csm.customer_card_state_code = cm.customer_card_state_code;
        }
        csm.customer_card_billing_shipping_address = cm.customer_card_billing_shipping_address;
        csm.customer_card_city = cm.customer_card_city;
        csm.customer_card_zip_code = cm.customer_card_zip_code;
        csm.customer_card_first_name = cm.customer_card_first_name;
        csm.customer_card_last_name = cm.customer_card_last_name;
        csm.Update();

        InsertTraceInfo(string.Format("Save Order Price({0}) credit card.", ReqOrderCode));

        if (ReqIsNew)
        {
            Response.Write("<script>parent.location.href= '/q_admin/orders_edit_detail_new.aspx?order_code=" + ReqOrderCode.ToString() + "'; </script>");
        }
        else
            Response.Write("<script>parent.location.href= '/q_admin/orders_edit_detail.aspx?order_code=" + ReqOrderCode.ToString() + "'; </script>");
    }
    protected void btn_same_personal_address_Click(object sender, EventArgs e)
    {
        InitPage(OrderAddressType.Person);
    }
    protected void btn_same_shipping_address_Click(object sender, EventArgs e)
    {
        InitPage(OrderAddressType.Shipping);
    }
}