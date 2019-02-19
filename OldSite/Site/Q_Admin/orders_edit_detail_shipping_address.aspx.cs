using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Q_Admin_orders_edit_detail_shipping_address :PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            otherCountryArea.Visible = false;

            InitPage(OrderAddressType.Shipping);
        }
    }

    void InitPage(OrderAddressType pilt)
    {
        var customer = CustomerStoreModel.FindByOrderCode(DBContext, ReqOrderCode.ToString());


        if (customer !=null)
        {
            string address = "";
            string city = "";
            string firstName = "";
            string lastName = "";
            string zipCode = "";
            string countryCode = "";
            string stateCode = "";
            switch (pilt)
            {
                case OrderAddressType.Shipping:
                    address = customer.customer_shipping_address;
                    city = customer.customer_shipping_city;
                    firstName = customer.customer_shipping_first_name;
                    lastName = customer.customer_shipping_last_name;
                    zipCode = customer.customer_shipping_zip_code;
                    countryCode = customer.shipping_country_code;
                    stateCode = customer.shipping_state_code;
                    break;
                case OrderAddressType.CreditCard:
                    address = customer.customer_card_billing_shipping_address;
                    city = customer.customer_card_city;
                    firstName = customer.customer_card_first_name;
                    lastName = customer.customer_card_last_name;
                    zipCode = customer.customer_card_zip_code;
                    countryCode = customer.customer_card_country_code;
                    stateCode = customer.customer_card_state_code;
                    break;
                case OrderAddressType.Person:
                    address = customer.customer_address1;
                    city = customer.customer_city;
                    firstName = customer.customer_first_name;
                    lastName = customer.customer_last_name;
                    zipCode = customer.zip_code;
                    countryCode = customer.customer_country_code;
                    stateCode = customer.state_code;
                    break;
            }
            //
            // shipping info
            //           
            CustomerID = customer.customer_serial_no.Value;
            StoreCustomerSerialNo = customer.serial_no;

            this.txt_shipping_address.Text = address;
            this.txt_shipping_city.Text = city;
            this.txt_shipping_first_name.Text = firstName;
            this.txt_shipping_last_name.Text = lastName;
            this.txt_shipping_zipcode.Text = zipCode;

            if (countryCode.ToLower() != "ca"
                && "us" != countryCode.ToLower())
            {
                BindShippingCountry(countryCode ?? "");
                this.ddl_shipping_country.SelectedValue = "Other";
                this.txt_inputCountry.Text = countryCode ?? "";
                this.txt_inputState.Text = stateCode ?? "";
                otherCountryArea.Visible = true;                
            }
            else
            {
                otherCountryArea.Visible = false;
                BindShippingCountry(countryCode ?? "");
                if ((countryCode ?? "").ToLower() == "ca"
                    || "us" == (countryCode ?? "").ToLower())
                {
                    BindShippingState((CountryCategory)Enum.Parse(typeof(CountryCategory), countryCode), stateCode ?? "");
                }
                else
                    BindShippingState(CountryCategory.Other, "Other");
            }
           
        }
    }

    private void BindShippingCountry(string selectedValue)
    {
        XmlStore xs = new XmlStore();
        this.ddl_shipping_country.DataSource = CountryCategoryHelper.CountryCategoryToDataTable();
        this.ddl_shipping_country.DataTextField = "text";
        this.ddl_shipping_country.DataValueField = "text";
        this.ddl_shipping_country.DataBind();
        if (selectedValue.Trim().Length == 2)
            this.ddl_shipping_country.SelectedValue = selectedValue;
        else
            this.ddl_shipping_country.SelectedIndex = 0;
    }

    private void BindShippingState(CountryCategory cc, string selectedValue)
    {
        //CH.Alert(countryID.ToString(), this.lv_part_list);
        XmlStore xs = new XmlStore();
        DataTable ssm = xs.FindStateByCountry(cc);
        if (ssm != null)
        {
            this.ddl_shipping_state.DataSource = ssm;
            this.ddl_shipping_state.DataTextField = "state_name";
            this.ddl_shipping_state.DataValueField = "state_code";
            this.ddl_shipping_state.DataBind();
            if (selectedValue.Length == 2)
                this.ddl_shipping_state.SelectedValue = selectedValue;
        }
        else
            this.ddl_shipping_state.Items.Clear();
    }

    protected void btn_save_shipping_address_Click(object sender, EventArgs e)
    {
        var cm = CustomerModel.GetCustomerModel(DBContext, CustomerID);
        cm.customer_shipping_address = this.txt_shipping_address.Text.Trim();
        cm.customer_shipping_city = this.txt_shipping_city.Text.Trim();
       
        cm.customer_shipping_first_name = this.txt_shipping_first_name.Text.Trim();
        cm.customer_shipping_last_name = this.txt_shipping_last_name.Text.Trim();

        //cm.shipping_state_code = StateID;
        cm.customer_shipping_zip_code = this.txt_shipping_zipcode.Text.Trim();
        if (ddl_shipping_country.SelectedValue.ToLower() == "other")
        {
            cm.shipping_state_code = txt_inputState.Text.Trim();
            cm.shipping_country_code = txt_inputCountry.Text.Trim();
            int stateID = new StateShippingModel().SaveNewState(DBContext, cm.shipping_country_code, cm.shipping_state_code).state_serial_no;
            cm.customer_shipping_state = stateID;
        }
        else
        {
            cm.shipping_state_code = ddl_shipping_state.SelectedValue ;
            cm.shipping_country_code = this.ddl_shipping_country.SelectedValue;
            cm.customer_shipping_state = StateShippingModel.FindStatIDByCode(DBContext, cm.shipping_state_code);
        }
        if (CustomerID != 7888888)
        {
            DBContext.SaveChanges();
        }

        var csm = DBContext.tb_customer_store.Single(me => me.serial_no.Equals(StoreCustomerSerialNo));// CustomerStoreModel.GetCustomerStoreModel( StoreCustomerSerialNo);
        csm.customer_shipping_address = cm.customer_shipping_address;
        csm.customer_shipping_city = cm.customer_shipping_city;
        if (ddl_shipping_country.SelectedValue.ToLower() == "other")
        {
            csm.shipping_country_code = txt_inputCountry.Text.Trim();
            csm.shipping_state_code = txt_inputState.Text.Trim();
            csm.customer_shipping_state = cm.customer_shipping_state;
        }
        else
        {
            csm.shipping_country_code = cm.shipping_country_code;
            csm.shipping_state_code = cm.shipping_state_code;
            csm.customer_shipping_state = StateShippingModel.FindStatIDByCode(DBContext, cm.shipping_state_code);
        }
        csm.customer_shipping_first_name = cm.customer_shipping_first_name;
        csm.customer_shipping_last_name = cm.customer_shipping_last_name;        
        csm.customer_shipping_zip_code = cm.customer_shipping_zip_code;
        DBContext.SaveChanges();

        InsertTraceInfo(DBContext, string.Format("Save Order Price({0}) shipping address.", ReqOrderCode));
        //AccountOrder(ReqOrderCode);
        //BindBaseInfo();
        OrdersSavePageRedirect(ReqOrderCode);
        if (ReqIsNew)
        {
            Response.Write("<script>parent.location.href= '/q_admin/orders_edit_detail_new.aspx?order_code=" + ReqOrderCode.ToString() + "'; this.close();</script>");
        }
        else
            Response.Write("<script>parent.location.href= '/q_admin/orders_edit_detail.aspx?order_code=" + ReqOrderCode.ToString() + "&isrunmodify=1';this.close(); </script>");
    }

    protected void ddl_shipping_country_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_shipping_country.SelectedValue.ToLower() == "other")
        {
            otherCountryArea.Visible = true;
            this.ddl_shipping_state.Visible = false;
        }
        else
        {
            otherCountryArea.Visible = false;
            ddl_shipping_state.Visible = true;
            BindShippingState((CountryCategory)Enum.Parse(typeof(CountryCategory), this.ddl_shipping_country.SelectedValue.ToString()), "");
        }
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

    protected void btn_same_personal_address_Click(object sender, EventArgs e)
    {
        InitPage(OrderAddressType.Person); 
    }
 
    protected void btn_same_credit_card_address_Click(object sender, EventArgs e)
    {
        InitPage(OrderAddressType.CreditCard);
    }
}
