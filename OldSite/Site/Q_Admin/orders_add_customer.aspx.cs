using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using LU.Data;

public partial class Q_Admin_orders_add_customer : OrderPageBase
{
    string y_vali = "有效";
    string n_vali = "无效";
    XmlStore XS = new XmlStore();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.ValidateLoginRule(Role.add_order);
            CurrentCustomerID = -1;
            BindCountryDDL();

            // 绑定洲
            this.BindBussessStateDDL(CountryCategory.CA);
            this.BindShippingStateDDL(CountryCategory.CA);
            this.BindCustomerStateDDL(CountryCategory.CA);
            this.BindCardStateDDL(CountryCategory.CA);

            if (!string.IsNullOrEmpty(ReqCid))
            {
                BindCustomerInfo(ReqCid);
            }
            txt_password.Focus();
        }
    }


    #region Methods

    /// <summary>
    /// 如果有ID，则显示值
    /// </summary>
    /// <param name="cid"></param>
    void BindCustomerInfo(string cid)
    {
        this.panel_business.Visible = true;
        this.panel_shipping_address.Visible = true;
        this.panel_shipping_and_credit_card.Visible = true;
        this.panel_simple_info.Visible = true;


        CurrentCustomerID = int.Parse(cid);

        // 绑定用户信息
        BindModifyCustomer(CurrentCustomerID);

        //
        this.cb_set_same.Checked = false;
    }
    #region Bind State 

    private void BindCardStateDDL(CountryCategory cc)
    {
        BindCardStateDDL(cc, "-1");
    }
    private void BindCardStateDDL(CountryCategory cc, string selectedvalue)
    {
        DataTable ssm = XS.FindStateByCountry(cc);
        if (ssm != null)
        {
            this.ddl_customer_card_state.DataSource = ssm;
            this.ddl_customer_card_state.DataTextField = "state_name";
            this.ddl_customer_card_state.DataValueField = "state_code";
            this.ddl_customer_card_state.DataBind();
            if (selectedvalue != "-1")
            {
                try
                {
                    this.ddl_customer_card_state.SelectedValue = selectedvalue;
                }
                catch { }
            }
        }
        else
        {
            this.ddl_customer_card_state.Items.Clear();
            this.ddl_customer_card_state.DataBind();
        }
    }

    private void BindBussessStateDDL(CountryCategory cc)
    {
        BindBussessStateDDL(cc, "-1");
    }
    private void BindBussessStateDDL(CountryCategory cc, string selectedvalue)
    {
        DataTable ssm = XS.FindStateByCountry(cc);
        if (ssm != null)
        {
            this.ddl_business_state.DataSource = ssm;
            this.ddl_business_state.DataTextField = "state_name";
            this.ddl_business_state.DataValueField = "state_code";
            this.ddl_business_state.DataBind();
            //this.btn_cancel.Text = selectedvalue;
            if (selectedvalue != "-1")
            {
                try
                {
                    this.ddl_business_state.SelectedValue = selectedvalue;
                }
                catch { }
            }
        }
        else
        {
            this.ddl_business_state.Items.Clear();
            this.ddl_business_state.DataBind();
        }
    }
    private void BindShippingStateDDL(CountryCategory cc)
    {
        BindShippingStateDDL(cc, "-1");
    }
    private void BindShippingStateDDL(CountryCategory cc, string selectedvalue)
    {
        DataTable ssm = XS.FindStateByCountry(cc);
        if (ssm != null)
        {
            this.ddl_customer_shipping_state.DataSource = ssm;
            this.ddl_customer_shipping_state.DataTextField = "state_name";
            this.ddl_customer_shipping_state.DataValueField = "state_code";
            this.ddl_customer_shipping_state.DataBind();
            if (selectedvalue != "-1")
            {
                try
                {
                    this.ddl_customer_shipping_state.SelectedValue = selectedvalue;
                }
                catch { }
            }

        }
        else
        {
            this.ddl_customer_shipping_state.Items.Clear();
            this.ddl_customer_shipping_state.DataBind();
        }
    }
    private void BindCustomerStateDDL(CountryCategory cc)
    { BindCustomerStateDDL(cc, "-1"); }

    private void BindCustomerStateDDL(CountryCategory cc, string selectedvalue)
    {
        DataTable ssm = XS.FindStateByCountry(cc);
        if (ssm != null)
        {
            this.ddl_customer_state.DataSource = ssm;
            this.ddl_customer_state.DataTextField = "state_name";
            this.ddl_customer_state.DataValueField = "state_code";
            this.ddl_customer_state.DataBind();
            if (selectedvalue != "-1")
            {
                try
                {
                    this.ddl_customer_state.SelectedValue = selectedvalue;
                }
                catch { }
            }
        }
        else
        {
            this.ddl_customer_state.Items.Clear();
            this.ddl_customer_state.DataBind();
        }
    }
    #endregion 

    #region Bind Country
    private void BindCountryDDL()
    {
        //CountryModel[] cms = CountryModel.FindAll();

        DataTable cms = CountryCategoryHelper.CountryCategoryToDataTable();
        this.ddl_customer_card_country.DataSource = cms;
        this.ddl_customer_card_country.DataTextField = "text";
        this.ddl_customer_card_country.DataValueField = "text";
        this.ddl_customer_card_country.DataBind();


        this.ddl_customer_shipping_country.DataSource = cms;
        this.ddl_customer_shipping_country.DataTextField = "text";
        this.ddl_customer_shipping_country.DataValueField = "text";
        this.ddl_customer_shipping_country.DataBind();

        this.ddl_business_country.DataSource = cms;
        this.ddl_business_country.DataTextField = "text";
        this.ddl_business_country.DataValueField = "text";
        this.ddl_business_country.DataBind();

        this.ddl_customer_country.DataSource = cms;
        this.ddl_customer_country.DataTextField = "text";
        this.ddl_customer_country.DataValueField = "text";
        this.ddl_customer_country.DataBind();
    }
    #endregion

    /// <summary>
    /// 绑定用户信息
    /// </summary>
    /// <param name="customer_id"></param>
    public void BindModifyCustomer(int customer_id)
    {
        var model = CustomerModel.GetCustomerModel(DBContext, customer_id);
        if (customer_id > 0)
        {
            Cmd = Command.modif;
        }
        this.BindBussessStateDDL(CountryCategory.CA);
        this.BindShippingStateDDL(CountryCategory.CA);
        this.BindCustomerStateDDL(CountryCategory.CA);
        this.BindCardStateDDL(CountryCategory.CA);


        this.txt_customer_login_name.Text = model.customer_login_name;
        this.txt_password.Text = model.customer_password;
        this.txtcustomer_first_name.Text = model.customer_first_name;
        this.txtcustomer_last_name.Text = model.customer_last_name;
        this.txt_phone_d.Text = model.phone_d;
        this.txt_phone_n.Text = model.phone_n;
        this.txt_phone_c.Text = model.phone_c;
        this.txtcustomer_email1.Text = model.customer_email1;
        this.txtcustomer_email2.Text = model.customer_email2;
        this.txt_customer_address.Text = model.customer_address1;
        this.txt_customer_city.Text = model.customer_city;
        this.txt_customer_zip_code.Text = model.zip_code;
        try
        {
            if (model.customer_country_code.ToLower() == "ca"
                || model.customer_country_code.ToLower() == "us")
            {
                this.ddl_customer_country.SelectedValue = model.customer_country_code;
                if (model.state_code.Length == 2)
                    BindCustomerStateDDL((CountryCategory)Enum.Parse(typeof(CountryCategory), model.customer_country_code), model.state_code);
            }
            else
            {
                this.ddl_customer_country.SelectedValue = "Other";
                this.otherCountryArea1.Visible = true;
                this.panel_province1.Visible = false;
                this.txt_OtherCountryName1.Text = model.customer_country_code;
                this.txt_OtherCountryProvince1.Text = model.state_code;
            }
        }
        catch { }

        this.txt_customer_card_billing_shipping_address.Text = model.customer_card_billing_shipping_address;
        this.txt_customer_card_zip_code.Text = model.customer_card_zip_code;
        this.txt_customer_card_city.Text = model.customer_card_city;
        this.txt_customer_card_first_name.Text = model.customer_card_first_name;
        this.txt_customer_card_last_name.Text = model.customer_card_last_name;
        this.txt_customer_credit_card.Text = model.customer_credit_card;
        this.txt_customer_expiry.Text = model.customer_expiry;
        this.ddl_customer_card_type.SelectedValue = model.customer_card_type;
        this.txt_customer_card_phone.Text = model.customer_card_phone;
        this.txt_customer_card_issuer.Text = model.customer_card_issuer;
        this.txt_verify_code.Text = model.card_verification_number;
        try
        {
            if (model.customer_card_country_code.ToLower() == "ca"
                || model.customer_card_country_code.ToLower() == "us")
            {
                this.ddl_customer_card_country.SelectedValue = model.customer_card_country_code.ToString();
                if (model.customer_card_state_code.Length == 2)
                    BindCardStateDDL((CountryCategory)Enum.Parse(typeof(CountryCategory), model.customer_card_country_code), model.customer_card_state_code);
            }
            else
            {
                this.ddl_customer_card_country.SelectedValue = "Other";
                this.otherCountryArea3.Visible = true;
                this.panel_Province3.Visible = false;
                this.txt_OtherCountry3.Text = model.customer_card_country_code;
                this.txt_otherProvince3.Text = model.customer_card_state_code;
            }
        }
        catch { }

        this.txt_customer_shipping_address.Text = model.customer_shipping_address;
        this.txt_customer_shipping_city.Text = model.customer_shipping_city;
        this.txt_shipping_first_name.Text = model.customer_shipping_first_name;
        this.txt_shipping_last_name.Text = model.customer_shipping_last_name;
        this.txt_customer_shipping_zip_code.Text = model.customer_shipping_zip_code;
        try
        {
            if (model.shipping_country_code.ToLower() == "ca"
                || model.shipping_country_code.ToLower() == "us")
            {
                this.ddl_customer_shipping_country.SelectedValue = model.shipping_country_code;
                if (model.shipping_state_code.Length == 2)
                    BindShippingStateDDL((CountryCategory)Enum.Parse(typeof(CountryCategory), model.shipping_country_code), model.shipping_state_code);
            }
            else
            {
                this.ddl_customer_shipping_country.SelectedValue = "Other";
                this.otherCountryArea2.Visible = true;
                this.panel_province2.Visible = false;
                this.txt_OtherCountry2.Text = model.shipping_country_code;
                this.txt_OtherProvince2.Text = model.shipping_state_code;
            }
        }
        catch { }

        this.txt_customer_company.Text = model.customer_company;
        this.txt_customer_business_telephone.Text = model.customer_business_telephone;
        this.txt_customer_fax.Text = model.customer_fax;
        this.txt_business_address1.Text = model.customer_business_address;
        this.txt_business_city.Text = model.customer_business_city;
        this.txt_business_zip_code.Text = model.customer_business_zip_code;
        this.txt_busniess_website.Text = model.busniess_website;
        this.txt_tax_execmtion.Text = model.tax_execmtion;
        this.txt_customer_fax.Text = model.customer_fax;
        try
        {
            if (model.customer_business_country_code.ToLower() == "ca"
                || model.customer_business_country_code.ToLower() == "us")
            {
                this.ddl_business_country.SelectedValue = model.customer_business_country_code;
                if (model.customer_business_state_code.Length == 2)
                    BindBussessStateDDL((CountryCategory)Enum.Parse(typeof(CountryCategory), model.customer_business_country_code), model.customer_business_state_code);
            }
            else
            {
                this.ddl_business_country.SelectedValue = "Other";
                this.otherCountryArea4.Visible = true;
                this.panel_Province4.Visible = false;
                this.txt_OtherCountry4.Text = model.shipping_country_code;
                this.txt_OtherProvince4.Text = model.shipping_state_code;
            }
        }
        catch { }

        WriteOrders(CurrentCustomerID.ToString());
    }

    public void SetControlsNull()
    {
        this.txt_customer_card_zip_code.Text = "";
        this.txt_customer_login_name.Text = "";
        this.txt_password.Text = "1111";
        this.txt_phone_d.Text = "";
        this.txt_phone_n.Text = "";
        this.txt_phone_c.Text = "";
        this.txt_customer_card_billing_shipping_address.Text = "";

        this.txt_customer_card_city.Text = "";

        this.txtcustomer_email1.Text = "";
        this.txtcustomer_email2.Text = "";
        this.txt_customer_credit_card.Text = "";
        this.txt_customer_expiry.Text = "";
        this.txt_customer_company.Text = "";
        this.txt_customer_business_telephone.Text = "";
        this.txt_customer_fax.Text = "";


        this.txtcustomer_first_name.Text = "";
        this.txtcustomer_last_name.Text = "";

        this.txt_customer_card_phone.Text = "";

        this.txt_tax_execmtion.Text = "";

        this.txt_customer_shipping_address.Text = "";

        this.txt_customer_shipping_city.Text = "";

        this.txt_shipping_first_name.Text = "";
        this.txt_shipping_last_name.Text = "";

        this.txt_customer_shipping_zip_code.Text = "";
        this.txt_customer_card_issuer.Text = "";
        this.txt_customer_card_last_name.Text = "";
        this.txt_customer_card_first_name.Text = "";
        this.txt_verify_code.Text = "";

        this.txt_business_address1.Text = "";
        this.txt_business_city.Text = "";
        this.txt_business_zip_code.Text = "";
        this.txt_busniess_website.Text = "";
        this.txt_tax_execmtion.Text = "";
        this.txt_customer_fax.Text = "";
        this.RadioButtonList_shipping.Items[0].Selected = false;
        this.RadioButtonList_shipping.Items[1].Selected = false;
        this.RadioButtonList_card.Items[0].Selected = false;
        this.RadioButtonList_card.Items[1].Selected = false;

        this.BindBussessStateDDL(CountryCategory.CA);
        this.BindShippingStateDDL(CountryCategory.CA);
        this.BindCustomerStateDDL(CountryCategory.CA);
        this.BindCardStateDDL(CountryCategory.CA);
    }

    #endregion


    /// <summary>
    /// 对客户信息进行操作
    /// </summary>
    /// <param name="cmd"></param>
    private void CustomerCmd(Command cmd)
    {
        try
        {
            var model = new tb_customer();// CustomerModel();
            if (cmd == Command.modif)
            {
                model = CustomerModel.GetCustomerModel(DBContext, CurrentCustomerID);
            }
            else
            {
                if (this.txt_password.Text.Trim() == "")
                    model.customer_password = Config.defaultPassword;
                else
                    model.customer_password = this.txt_password.Text.Trim();

                model.create_datetime = DateTime.Now;
            }

            model.customer_first_name = this.txtcustomer_first_name.Text.Trim();
            model.customer_last_name = this.txtcustomer_last_name.Text.Trim();
            model.customer_login_name = this.txt_customer_login_name.Text.Trim();
            model.phone_d = this.txt_phone_d.Text.Trim();
            model.phone_n = this.txt_phone_n.Text.Trim();
            model.phone_c = this.txt_phone_c.Text.Trim();
            model.customer_address1 = this.txt_customer_address.Text.Trim();

            model.customer_city = this.txt_customer_city.Text.Trim();
            model.zip_code = this.txt_customer_zip_code.Text.Trim();

            if (ddl_customer_country.SelectedValue.ToLower() == "other")
            {
                model.customer_country_code = this.txt_OtherCountryName1.Text.Trim();
                model.state_code = this.txt_OtherCountryProvince1.Text.Trim();
                new StateShippingModel().SaveNewState(DBContext, model.customer_country_code, model.state_code);
            }
            else
            {
                model.customer_country_code = this.ddl_customer_country.SelectedValue;
                model.state_code = this.ddl_customer_state.SelectedValue;
            }

            model.customer_email1 = this.txtcustomer_email1.Text.Trim();
            model.customer_email2 = this.txtcustomer_email2.Text.Trim();

            model.customer_card_billing_shipping_address = this.txt_customer_card_billing_shipping_address.Text.Trim();
            model.customer_card_city = this.txt_customer_card_city.Text.Trim();
            model.customer_card_phone = this.txt_customer_card_phone.Text.Trim();
            model.customer_card_zip_code = this.txt_customer_card_zip_code.Text.Trim();
            model.customer_card_type = this.ddl_customer_card_type.SelectedValue.ToString();
            model.customer_expiry = this.txt_customer_expiry.Text.Trim();
            model.customer_credit_card = this.txt_customer_card_city.Text.Trim();
            if (ddl_customer_card_country.SelectedValue.ToLower() == "other")
            {
                model.customer_card_country_code = txt_OtherCountry3.Text.Trim();
                model.customer_card_state_code = txt_otherProvince3.Text.Trim();
                new StateShippingModel().SaveNewState(DBContext, model.customer_card_country_code, model.customer_card_state_code);
            }
            else
            {
                model.customer_card_country_code = this.ddl_customer_card_country.SelectedValue;
                model.customer_card_state_code = this.ddl_customer_card_state.SelectedValue;
            }
            model.customer_credit_card = this.txt_customer_credit_card.Text.Trim();

            model.customer_card_phone = this.txt_customer_card_phone.Text.Trim();
            model.customer_card_issuer = this.txt_customer_card_issuer.Text.Trim();
            model.customer_card_first_name = this.txt_customer_card_first_name.Text.Trim();
            model.customer_card_last_name = this.txt_customer_card_last_name.Text.Trim();
            model.card_verification_number = this.txt_verify_code.Text.Trim();

            model.customer_shipping_address = this.txt_customer_shipping_address.Text.Trim();
            model.customer_shipping_city = this.txt_customer_shipping_city.Text.Trim();

            if (ddl_customer_shipping_country.SelectedValue.ToLower() == "other")
            {
                model.shipping_country_code = txt_OtherCountry2.Text.Trim();
                model.shipping_state_code = txt_OtherProvince2.Text.Trim();
                new StateShippingModel().SaveNewState(DBContext, model.shipping_country_code, model.shipping_state_code);
            }
            else
            {
                model.shipping_country_code = this.ddl_customer_shipping_country.SelectedValue;
                model.shipping_state_code = this.ddl_customer_shipping_state.SelectedValue.ToString();
            }
            model.customer_shipping_first_name = this.txt_shipping_first_name.Text.Trim();
            model.customer_shipping_last_name = this.txt_shipping_last_name.Text.Trim();
            model.customer_shipping_zip_code = this.txt_customer_shipping_zip_code.Text.Trim();


            model.customer_company = this.txt_customer_company.Text.Trim();
            model.customer_fax = this.txt_customer_fax.Text.Trim();
            model.customer_business_telephone = this.txt_customer_business_telephone.Text.Trim();
            model.tax_execmtion = this.txt_tax_execmtion.Text.Trim();
            model.busniess_website = this.txt_busniess_website.Text.Trim();
            if (ddl_business_country.SelectedValue.ToLower() == "other")
            {
                model.customer_business_country_code = txt_OtherCountry4.Text.Trim();
                model.customer_business_state_code = txt_OtherProvince4.Text.Trim();
                new StateShippingModel().SaveNewState(DBContext, model.customer_business_country_code, model.customer_business_state_code);
            }
            else
            {
                model.customer_business_country_code = this.ddl_business_country.SelectedValue;
                model.customer_business_state_code = this.ddl_business_state.SelectedValue;
            }

            model.customer_business_zip_code = this.txt_business_zip_code.Text;
            model.customer_business_city = this.txt_business_city.Text;
            model.customer_business_address = this.txt_business_address1.Text;
            model.tag = 1;

            model.system_category_serial_no = Config.SystemCategory;

            if (cmd == Command.modif)
            {
                DBContext.SaveChanges();
                this.InsertTraceInfo(DBContext, "Modify Customer Info([" + model.customer_serial_no.ToString() + "]" + model.customer_first_name + " " + model.customer_last_name + ")");
            }
            else
            {

                model.customer_serial_no = Code.NewCustomerCode(DBContext);
                CurrentCustomerID = model.customer_serial_no.Value;
                DBContext.tb_customer.Add(model);
                DBContext.SaveChanges();
                this.InsertTraceInfo(DBContext, "Create Customer Info([" + model.customer_serial_no.ToString() + "]" + model.customer_first_name + " " + model.customer_last_name + ")");
            }
            CurrentCustomerID = model.customer_serial_no.Value;

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void dg_customer_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        //LinkButton lb = (LinkButton)e.CommandSource;


    }
    protected void NewOrder1_Load(object sender, EventArgs e)
    {

    }

    /// <summary>
    /// 新建客户信息
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lb_new_customer_Click(object sender, EventArgs e)
    {
        SetControlsNull();

        Cmd = Command.create;
        CurrentCustomerID = -1;
    }

    protected void txtcustomer_credit_card_TextChanged(object sender, EventArgs e)
    {
        string cord = this.txt_customer_credit_card.Text.Trim();
        if (cord == "")
            return;

        string value = "";
        cord = Helper.RemoveSpace(cord);

        ValidateCard(cord);
        for (int i = 0; i < cord.Length; i++)
        {

            //AnthemHelper.Alert((i % 4).ToString());
            if (i % 4 == 0 && i != 0)
            {
                value += " ";
                value += cord[i].ToString();
            }
            else
                value += cord[i].ToString();
        }
        this.txt_customer_credit_card.Text = value;
        //this.txt_customer_credit_card.UpdateAfterCallBack = true;
    }

    /// <summary>
    /// 判断Card 长度
    /// </summary>
    /// <param name="text"></param>
    private void ValidateCard(string text)
    {
        if (text.Length > 16 || text.Length < 15)
        {
            this.lbl_watch_card.Visible = true;
            //this.lbl_watch_card.UpdateAfterCallBack = true;
        }
        else
        {
            this.lbl_watch_card.Visible = false;
            //this.lbl_watch_card.UpdateAfterCallBack = true;
        }
    }

    private void SetValidateName(string text, System.Drawing.Color c)
    {
        this.lbl_validate_customer.Text = text;
        this.lbl_validate_customer.ForeColor = c;
        //this.lbl_validate_customer.UpdateAfterCallBack = true;
    }

    private void SetValidateLoginName(string text, System.Drawing.Color c)
    {
        this.lbl_validate_login.Text = text;
        this.lbl_validate_login.ForeColor = c;
        //this.lbl_validate_login.UpdateAfterCallBack = true;
    }

    private void SetValidateFirstName(string text, System.Drawing.Color c)
    {
        this.lbl_validate_first_name.Text = text;
        this.lbl_validate_first_name.ForeColor = c;
        //this.lbl_validate_first_name.UpdateAfterCallBack = true;
    }

    private void ValidateCustomerName(bool is_first_name)
    {

        try
        {
            SetValidateName("", System.Drawing.Color.Red);

            string first_name = this.txtcustomer_first_name.Text.Trim();
            string last_name = this.txtcustomer_last_name.Text.Trim();


            DataTable models = CustomerModel.GetModelsByFirstLastname(first_name, last_name);
            if (models.Rows.Count > 0)
            {
                if (first_name != "")
                    this.SetValidateFirstName(n_vali, System.Drawing.Color.Red);
                if (last_name != "")
                    this.SetValidateName(n_vali, System.Drawing.Color.Red);

                //if (is_first_name)
                //    this.txtcustomer_first_name.Focus();
                //else
                this.txtcustomer_last_name.Focus();
            }
            else
            {
                if (is_first_name)
                {
                    if (this.txt_shipping_first_name.Text.Trim() == "")
                        this.txt_shipping_first_name.Text = this.txtcustomer_first_name.Text;
                    this.txtcustomer_last_name.Focus();
                }
                else
                {
                    if (this.txt_shipping_last_name.Text.Trim() == "")
                        this.txt_shipping_last_name.Text = this.txtcustomer_last_name.Text;
                    this.txt_phone_d.Focus();
                }
                this.panel_shipping_address.Visible = true;
                this.SetValidateName(y_vali, System.Drawing.Color.Green);
                this.SetValidateFirstName(y_vali, System.Drawing.Color.Green);
            }

        }
        catch
        {
            //AnthemHelper.Alert(ex.Message);
        }
    }
    protected void txtcustomer_last_name_TextChanged(object sender, EventArgs e)
    {
        ValidateCustomerName(false);
    }
    protected void txtcustomer_fullname_TextChanged(object sender, EventArgs e)
    {
        SetValidateLoginName("", System.Drawing.Color.Red);

        DataTable dt = CustomerModel.GetModelsByLoginName(this.txt_customer_login_name.Text.Trim());

        if (dt.Rows.Count > 0)
        {
            this.SetValidateLoginName(n_vali, System.Drawing.Color.Red);

            TextBox txt = (TextBox)sender;
            txt.Focus();
            //this.dg_customer.UpdateAfterCallBack = true;
        }
        else
        {
            this.SetValidateLoginName(y_vali, System.Drawing.Color.Green);
            this.txtcustomer_first_name.Focus();
        }

    }

    protected void txtcustomer_first_name_TextChanged(object sender, EventArgs e)
    {
        ValidateCustomerName(true);

    }

    protected void lb_order_Click(object sender, EventArgs e)
    {
        if (CurrentCustomerID == -1)
        {
            AnthemHelper.Alert("请选择客户");
        }
        else
            RedirectEditOrder();
    }


    protected void txtcustomer_home_tel_TextChanged(object sender, EventArgs e)
    {
        FormatPhone(sender);
    }
    protected void txtcustomer_work_tel_TextChanged(object sender, EventArgs e)
    {
        FormatPhone(sender);
    }
    protected void txtcustomer_cell_tel_TextChanged(object sender, EventArgs e)
    {
        FormatPhone(sender);
    }

    private void FormatPhone(object sender)
    {
        TextBox tb = (TextBox)sender;

        string text = Helper.RemoveSpace(tb.Text).Replace("-", "");

        if (text == "")
            return;
        string v = "";

        // 电话开头如果是1,则自动去掉
        if (text.Substring(0, 1) == "1")
        {
            text = text.Substring(1, text.Length - 1);
        }

        // 电话号码只能是10位
        //if (text.Length != 10)
        //{
        //    AnthemHelper.Alert("电话格式错误");
        //    return;
        //}
        for (int i = 0; i < text.Length; i++)
        {
            if (i == 3)
            {
                v += "-";
                //v += text[i].ToString();

            }
            if (i == 6)
            {
                v += "-";
                //v += text[i].ToString();
            }
            if (i == 10)
            {
                v += "-";
                // v += text[i].ToString();
            }
            v += text[i].ToString();
        }


        tb.Text = v;

    }
    protected void dg_customer_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType != ListItemType.Footer && e.Item.ItemType != ListItemType.Header)
        {
            // customer
            int cumtomer_id = int.Parse(e.Item.Cells[1].Text);
            DataTable cm = CustomerModel.GetModelsByCustomerID(cumtomer_id);
            if (cm.Rows.Count == 1)
            {
                DataRow dr = cm.Rows[0];
                e.Item.Cells[2].Text = dr["customer_first_name"].ToString() + "&nbsp;" + dr["customer_last_name"].ToString();
            }

            // state
            string state = e.Item.Cells[5].Text;
            if (state != "")
            {
                // 不能传换为数字,表示没有值
                try
                {
                    var m = StateShippingModel.GetStateShippingModel(DBContext, int.Parse(state));
                    e.Item.Cells[5].Text = m.state_short_name.ToString();
                }
                catch { }
            }
        }
    }
    protected void ddl_shipping_country_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_customer_shipping_country.SelectedValue.ToLower() == "other")
        {
            otherCountryArea2.Visible = true;
            panel_province2.Visible = false;
        }
        else
        {
            otherCountryArea2.Visible = false;
            panel_province2.Visible = true;
            BindShippingStateDDL((CountryCategory)Enum.Parse(typeof(CountryCategory), this.ddl_customer_shipping_country.SelectedValue));

        }


    }

    protected void cb_set_same_CheckedChanged(object sender, EventArgs e)
    {

        if (this.cb_set_same.Checked)
        {
            this.cb_set_same_base_info.Checked = !this.cb_set_same.Checked;
            this.txt_customer_shipping_address.Text = this.txt_customer_card_billing_shipping_address.Text;

            this.txt_customer_shipping_city.Text = this.txt_customer_card_city.Text;

            this.txt_shipping_first_name.Text = this.txt_customer_card_first_name.Text;
            this.txt_shipping_last_name.Text = this.txt_customer_card_last_name.Text;
            this.txt_customer_shipping_zip_code.Text = this.txt_customer_card_zip_code.Text;


            this.ddl_customer_shipping_country.SelectedIndex = this.ddl_customer_card_country.SelectedIndex;

            //this.BindShippingStateDDL(int.Parse(this.ddl_customer_card_country.SelectedValue.ToString()));
            this.BindShippingStateDDL((CountryCategory)Enum.Parse(typeof(CountryCategory), this.ddl_customer_card_country.SelectedValue.ToString()));
            this.ddl_customer_shipping_state.SelectedIndex = this.ddl_customer_card_state.SelectedIndex;
        }
    }

    #region View Panel
    protected void lb_shipping_address_Click(object sender, EventArgs e)
    {
        this.panel_shipping_address.Visible = !this.panel_shipping_address.Visible;
    }
    protected void lb_shipping_and_credit_card_Click(object sender, EventArgs e)
    {
        this.panel_shipping_and_credit_card.Visible = !this.panel_shipping_and_credit_card.Visible;
    }
    protected void lb_business_Click(object sender, EventArgs e)
    {
        this.panel_business.Visible = !this.panel_business.Visible;
    }
    protected void lb_simple_info_Click(object sender, EventArgs e)
    {
        this.panel_simple_info.Visible = !this.panel_simple_info.Visible;
    }

    #endregion


    protected void btn_new_customer_Click(object sender, EventArgs e)
    {
        try
        {
            this.CurrentCustomerID = -1;
            Cmd = Command.create;
            SetControlsNull();
            CH.CloseParentWatting(this.Literal1);
        }
        catch (Exception ex)
        {
            CH.CloseParentWatting(this.Literal1);
            CH.Alert(ex.Message, this.Literal1);
        }
    }


    protected void btn_new_sales_order_Click(object sender, EventArgs e)
    {

        try
        {

            if (CurrentCustomerID == -1 && Cmd != Command.create)
            {
                throw new Exception("请选择一个客户");
            }
            else if (Cmd == Command.create)
            {
                CustomerCmd(Command.create);

            }
            else
            {
                CustomerCmd(Command.modif);
            }

            OrderCode = OrderHelperModel.GetNewOrderCode(DBContext).ToString();
            var ohm = new tb_order_helper();// OrderHelperModel();
            ohm.order_code = int.Parse(OrderCode);
            ohm.customer_serial_no = CurrentCustomerID;
            //throw new Exception(CurrentCustomerID.ToString());
            ohm.create_datetime = DateTime.Now;
            ohm.order_date = DateTime.Now;
            ohm.out_status = sbyte.Parse(Config.DefaultOutStatus.ToString());
            ohm.pre_status_serial_no = int.Parse(Config.new_order_status);
            ohm.tag = 1;
            ohm.is_ok = true;
            ohm.order_source = 2;

            ohm.pay_method = Config.pay_method_pick_up_ids.ToString();
            ohm.shipping_company = -1;
            DBContext.tb_order_helper.Add(ohm);
            DBContext.SaveChanges();

            var ch = new CustomerHelper(DBContext);
            ch.CopyCustomer(OrderCode, CurrentCustomerID);
            this.InsertTraceInfo(DBContext, "Create One Order (" + OrderCode.ToString() + ")");
            //AnthemHelper.OpenWin("sale_add_order_pay_method.aspx?order_code=" + OrderCode, 1000, 800,10,10);
            CH.CloseParentWatting(this.Literal1);
            CH.Redirect("orders_modify_paymethod.aspx?order_code=" + OrderCode, this.Literal1);

        }
        catch (Exception ex)
        {
            CH.CloseParentWatting(this.Literal1);
            CH.Alert(ex.Message, this.Literal1);
        }

    }
    protected void btn_save_change_Click(object sender, EventArgs e)
    {
        try
        {
            if (CurrentCustomerID == -1)
            {
                CustomerCmd(Command.create);
            }
            else
            {
                CustomerCmd(Command.modif);
            }

            CH.CloseParentWatting(this.Literal1);
            CH.Alert(KeyFields.SaveIsOK, this.btn_save_change);
        }
        catch (Exception ex)
        {
            CH.CloseParentWatting(this.Literal1);
            CH.Alert(ex.Message, this.Literal1);
        }
    }
    protected void ddl_business_country_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_business_country.SelectedValue.ToLower() == "other")
        {
            otherCountryArea4.Visible = true;
            panel_Province4.Visible = false;
        }
        else
        {
            otherCountryArea4.Visible = false;
            panel_Province4.Visible = true;
            BindBussessStateDDL((CountryCategory)Enum.Parse(typeof(CountryCategory), this.ddl_business_country.SelectedValue));
        }
    }
    /// <summary>
    /// base info country 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddl_customer_country_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_customer_country.SelectedValue.ToLower() == "other")
        {
            otherCountryArea1.Visible = true;
            panel_province1.Visible = false;
        }
        else
        {
            otherCountryArea1.Visible = false;
            panel_province1.Visible = true;
            BindCustomerStateDDL((CountryCategory)Enum.Parse(typeof(CountryCategory), this.ddl_customer_country.SelectedValue));
        }
    }
    protected void ddl_customer_card_country_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_customer_card_country.SelectedValue.ToLower() == "other")
        {
            otherCountryArea3.Visible = true;
            panel_Province3.Visible = false;
        }
        else
        {
            otherCountryArea3.Visible = false;
            panel_Province3.Visible = true;
            BindCardStateDDL((CountryCategory)Enum.Parse(typeof(CountryCategory), this.ddl_customer_card_country.SelectedValue));
        }
    }
    protected void cb_set_same_base_info_CheckedChanged(object sender, EventArgs e)
    {
        if (this.cb_set_same_base_info.Checked)
        {
            this.cb_set_same.Checked = !this.cb_set_same_base_info.Checked;
            this.txt_customer_shipping_address.Text = this.txt_customer_address.Text;

            this.txt_customer_shipping_city.Text = this.txt_customer_city.Text;

            this.txt_shipping_first_name.Text = this.txtcustomer_first_name.Text;
            this.txt_shipping_last_name.Text = this.txtcustomer_last_name.Text;
            this.txt_customer_shipping_zip_code.Text = this.txt_customer_zip_code.Text;


            this.ddl_customer_shipping_country.SelectedIndex = this.ddl_customer_country.SelectedIndex;

            //this.BindShippingStateDDL(int.Parse(this.ddl_customer_card_country.SelectedValue.ToString()));
            this.BindShippingStateDDL((CountryCategory)Enum.Parse(typeof(CountryCategory), this.ddl_customer_country.SelectedValue.ToString()));
            this.ddl_customer_shipping_state.SelectedIndex = this.ddl_customer_state.SelectedIndex;


        }
    }
    protected void CheckBoxList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.CheckBoxList_card.SelectedValue == "2")
        {
            this.CheckBoxList_card.Items[0].Selected = false;

            this.txt_customer_card_billing_shipping_address.Text = this.txt_customer_shipping_address.Text;

            this.txt_customer_card_city.Text = this.txt_customer_shipping_city.Text;

            this.txt_customer_card_first_name.Text = this.txt_shipping_first_name.Text;
            this.txt_customer_card_last_name.Text = this.txt_shipping_last_name.Text;
            this.txt_customer_card_zip_code.Text = this.txt_customer_shipping_zip_code.Text;


            this.ddl_customer_card_country.SelectedIndex = this.ddl_customer_shipping_country.SelectedIndex;

            //this.BindShippingStateDDL(int.Parse(this.ddl_customer_card_country.SelectedValue.ToString()));
            this.BindCardStateDDL((CountryCategory)Enum.Parse(typeof(CountryCategory), this.ddl_customer_shipping_country.SelectedValue.ToString()));
            this.ddl_customer_card_state.SelectedIndex = this.ddl_customer_shipping_state.SelectedIndex;
        }
        else
        {
            this.CheckBoxList_card.Items[1].Selected = false;

            this.txt_customer_card_billing_shipping_address.Text = this.txt_customer_address.Text;

            this.txt_customer_card_city.Text = this.txt_customer_city.Text;

            this.txt_customer_card_first_name.Text = this.txtcustomer_first_name.Text;
            this.txt_customer_card_last_name.Text = this.txtcustomer_last_name.Text;
            this.txt_customer_card_zip_code.Text = this.txt_customer_zip_code.Text;


            this.ddl_customer_card_country.SelectedIndex = this.ddl_customer_country.SelectedIndex;

            //this.BindShippingStateDDL(int.Parse(this.ddl_customer_card_country.SelectedValue.ToString()));
            this.BindCardStateDDL((CountryCategory)Enum.Parse(typeof(CountryCategory), this.ddl_customer_country.SelectedValue.ToString()));
            this.ddl_customer_card_state.SelectedIndex = this.ddl_customer_state.SelectedIndex;
        }
    }
    protected void RadioButtonList_card_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.RadioButtonList_card.SelectedValue == "2")
        {
            this.txt_customer_card_billing_shipping_address.Text = this.txt_customer_shipping_address.Text;

            this.txt_customer_card_city.Text = this.txt_customer_shipping_city.Text;

            this.txt_customer_card_first_name.Text = this.txt_shipping_first_name.Text;
            this.txt_customer_card_last_name.Text = this.txt_shipping_last_name.Text;
            this.txt_customer_card_zip_code.Text = this.txt_customer_shipping_zip_code.Text;

            if (this.ddl_customer_shipping_country.SelectedItem.Text.ToLower() == "other")
            {
                this.ddl_customer_card_country.SelectedIndex = this.ddl_customer_shipping_country.SelectedIndex;
                this.txt_OtherCountry3.Text = this.txt_OtherCountry2.Text;
                this.txt_OtherProvince4.Text = this.txt_OtherProvince2.Text;
                this.otherCountryArea3.Visible = true;
                this.panel_Province3.Visible = false;
            }
            else
            {
                this.ddl_customer_card_country.SelectedIndex = this.ddl_customer_shipping_country.SelectedIndex;

                //this.BindShippingStateDDL(int.Parse(this.ddl_customer_card_country.SelectedValue.ToString()));
                this.BindCardStateDDL((CountryCategory)Enum.Parse(typeof(CountryCategory), this.ddl_customer_shipping_country.SelectedValue.ToString()));
                this.ddl_customer_card_state.SelectedIndex = this.ddl_customer_shipping_state.SelectedIndex;
                this.otherCountryArea3.Visible = false;
                this.panel_Province3.Visible = true;
            }
        }
        else
        {

            this.txt_customer_card_billing_shipping_address.Text = this.txt_customer_address.Text;

            this.txt_customer_card_city.Text = this.txt_customer_city.Text;

            this.txt_customer_card_first_name.Text = this.txtcustomer_first_name.Text;
            this.txt_customer_card_last_name.Text = this.txtcustomer_last_name.Text;
            this.txt_customer_card_zip_code.Text = this.txt_customer_zip_code.Text;

            if (this.ddl_customer_country.SelectedItem.Text.ToLower() == "other")
            {
                this.ddl_customer_card_country.SelectedIndex = this.ddl_customer_country.SelectedIndex;
                this.txt_OtherCountry3.Text = this.txt_OtherCountryName1.Text;
                this.txt_otherProvince3.Text = this.txt_OtherCountryProvince1.Text;
                this.otherCountryArea3.Visible = true;
                this.panel_Province3.Visible = false;
            }
            else
            {
                this.ddl_customer_card_country.SelectedIndex = this.ddl_customer_country.SelectedIndex;

                //this.BindShippingStateDDL(int.Parse(this.ddl_customer_card_country.SelectedValue.ToString()));
                this.BindCardStateDDL((CountryCategory)Enum.Parse(typeof(CountryCategory), this.ddl_customer_country.SelectedValue.ToString()));
                this.ddl_customer_card_state.SelectedIndex = this.ddl_customer_state.SelectedIndex;
                this.otherCountryArea3.Visible = false;
                this.panel_Province3.Visible = true;
            }
        }
    }
    protected void RadioButtonList_shipping_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RadioButtonList_shipping.SelectedValue == "1")
        {

            this.txt_customer_shipping_address.Text = this.txt_customer_address.Text;

            this.txt_customer_shipping_city.Text = this.txt_customer_city.Text;

            this.txt_shipping_first_name.Text = this.txtcustomer_first_name.Text;
            this.txt_shipping_last_name.Text = this.txtcustomer_last_name.Text;
            this.txt_customer_shipping_zip_code.Text = this.txt_customer_zip_code.Text;

            if (this.ddl_customer_country.SelectedItem.Text.ToLower() == "other")
            {
                this.ddl_customer_shipping_country.SelectedIndex = this.ddl_customer_country.SelectedIndex;
                this.txt_OtherCountry2.Text = this.txt_OtherCountryName1.Text;
                this.txt_OtherProvince2.Text = this.txt_OtherCountryProvince1.Text;
                this.otherCountryArea2.Visible = true;
                this.panel_province2.Visible = false;
            }
            else
            {
                this.ddl_customer_shipping_country.SelectedIndex = this.ddl_customer_country.SelectedIndex;

                //this.BindShippingStateDDL(int.Parse(this.ddl_customer_card_country.SelectedValue.ToString()));
                this.BindShippingStateDDL((CountryCategory)Enum.Parse(typeof(CountryCategory), this.ddl_customer_country.SelectedValue.ToString()));
                this.ddl_customer_shipping_state.SelectedIndex = this.ddl_customer_state.SelectedIndex;
                this.otherCountryArea2.Visible = false;
                this.panel_province2.Visible = true;
            }
        }
        else
        {

            this.txt_customer_shipping_address.Text = this.txt_customer_card_billing_shipping_address.Text;

            this.txt_customer_shipping_city.Text = this.txt_customer_card_city.Text;

            this.txt_shipping_first_name.Text = this.txt_customer_card_first_name.Text;
            this.txt_shipping_last_name.Text = this.txt_customer_card_last_name.Text;
            this.txt_customer_shipping_zip_code.Text = this.txt_customer_card_zip_code.Text;

            if (this.ddl_customer_card_country.SelectedItem.Text.ToLower() == "other")
            {
                this.ddl_customer_shipping_country.SelectedIndex = this.ddl_customer_card_country.SelectedIndex;
                this.txt_OtherCountry2.Text = this.txt_OtherCountry3.Text;
                this.txt_OtherProvince2.Text = this.txt_otherProvince3.Text;
                this.otherCountryArea2.Visible = true;
                this.panel_province2.Visible = false;
            }
            else
            {
                this.ddl_customer_shipping_country.SelectedIndex = this.ddl_customer_card_country.SelectedIndex;

                //this.BindShippingStateDDL(int.Parse(this.ddl_customer_card_country.SelectedValue.ToString()));
                this.BindShippingStateDDL((CountryCategory)Enum.Parse(typeof(CountryCategory), this.ddl_customer_card_country.SelectedValue.ToString()));
                this.ddl_customer_shipping_state.SelectedIndex = this.ddl_customer_card_state.SelectedIndex;
                this.otherCountryArea2.Visible = false;
                this.panel_province2.Visible = true;
            }
        }
    }

    string ReqCid
    {
        get { return Util.GetStringSafeFromQueryString(this.Page, "cid"); }
    }

    protected void btnGo_Click(object sender, EventArgs e)
    {
        string keyword = txtKeyword.Text.Trim();
        lblGoNote.Text = "";

        DataTable dt = CustomerModel.GetModelsByKeyword(keyword);
        if (dt == null)
        {
            lblGoNote.Text = "no data match.";
        }
        else if (dt.Rows.Count == 1)
        {
            Response.Redirect("orders_add_customer.aspx?cid=" + dt.Rows[0]["customer_serial_no"].ToString(), true);
        }
        else
        {
            lblGoNote.Text = "\"" + keyword + "\" no data match.";
            txtKeyword.Text = "";
        }
        // Response.Write(dt.Rows.Count.ToString());
    }

    void WriteOrders(string customerID)
    {
        ltOrderHistory.Text = "";

        if (!string.IsNullOrEmpty(customerID))
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            List<KeyValuePair<string, string>> orders = CustomerStoreModel.GetOrders(customerID);

            //sb.Append(orders.Count > 0 ? "" : "");
            foreach (var o in orders)
            {
                sb.Append(string.Format(@"<a href='/q_admin/sales_order_detail.aspx?order_code=" + o.Key + "' title='" + o.Value + "' target='_blank'>" + o.Key + "</a>"));
            }
            ltOrderHistory.Text = sb.ToString();
        }
    }
}