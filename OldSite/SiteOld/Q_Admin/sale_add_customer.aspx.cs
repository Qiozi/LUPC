using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class Q_Admin_sale_add_customer : OrderPageBase
{
     string y_vali = "有效";
    string n_vali = "无效";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.ValidateLoginRule(Role.add_order);
            CurrentCustomerID = -1;
            InitialDatabase();
        }
        //NewOrder1.onCliekNewOrderCode += new OnClickSearchOrderCode(NewOrder1_onCliekNewOrderCode);
        //NewOrder1.onCliekSearchOrderCode += new OnClickSearchOrderCode(NewOrder1_onCliekSearchOrderCode);

    }
    void NewOrder1_onCliekSearchOrderCode()
    {
        //AnthemHelper.Alert(OrderCode.ToString());
        DataTable dt = OrderHelperModel.GetModelsDTByOrderCode(int.Parse(OrderCode));
        //AnthemHelper.Alert(dt.Rows[0]["customer_serial_no"].ToString());

        if (dt.Rows.Count > 1)
        {
            AnthemHelper.Alert("订单编号有误");
            return;
        }
        else if (dt.Rows.Count < 1)
        {
            //AnthemHelper.Alert("未查到此订单编号");
            return;
        }
        else
        {
            //Edit_order1.BindOrderProductDG(true, OrderCode, true);
            CurrentCustomerID = int.Parse(dt.Rows[0]["customer_serial_no"].ToString());
        }
        //Panels(PanelType.edit_order);
       // Edit_order1.SetCustomerDetail(CurrentCustomerID);
        RedirectEditOrder();
        
    }


    void NewOrder1_onCliekNewOrderCode()
    {
        //Panels( PanelType.edit_order);
        // Edit_order1.SetCustomerDetail(CurrentCustomerID);
        RedirectEditOrder();
    }

    //private void Panels( PanelType pt)
    //{
    //    switch(pt)
    //    {
    //        case PanelType.edit_order:
    //            if (OrderCode != "")
    //            {
    //                this.panel_order.Visible = false;
    //                this.panel_order.UpdateAfterCallBack = true;

    //                this.panel_customer.Visible = false;
    //                this.panel_customer.UpdateAfterCallBack = true;

    //                this.panel_edit_order.Visible = true;
    //                this.panel_edit_order.UpdateAfterCallBack = true;
    //            }
    //            break;
    //        case PanelType.customer:
    //            this.panel_order.Visible = false;
    //            this.panel_order.UpdateAfterCallBack = true;

    //            this.panel_customer.Visible = true;
    //            this.panel_customer.UpdateAfterCallBack = true;

    //            this.panel_edit_order.Visible = false;
    //            this.panel_edit_order.UpdateAfterCallBack = true;
    //            break;

    //        case PanelType.order:
    //            if (OrderCode != "")
    //            {
    //                Panels(PanelType.edit_order);
    //                return;
    //            }
    //            this.panel_order.Visible = true;
    //            this.panel_order.UpdateAfterCallBack = true;

    //            this.panel_customer.Visible = false;
    //            this.panel_customer.UpdateAfterCallBack = true;

    //            this.panel_edit_order.Visible = false;
    //            this.panel_edit_order.UpdateAfterCallBack = true;
    //            break;

    //    }
    //}

    #region Methods
    public override void InitialDatabase()
    {

        base.InitialDatabase();

        if (OrderCodeRequest != -1)
        {
            OrderCode = OrderCodeRequest.ToString();
            DataTable model = OrderHelperModel.GetModelsDTByOrderCode(OrderCodeRequest);
            if(model.Rows.Count == 1)
                CurrentCustomerID = int.Parse(model.Rows[0]["customer_serial_no"].ToString());
            //Panels(PanelType.edit_order);
            NewOrder1_onCliekSearchOrderCode();
        }
        BindCustomerDG(false);  
        BindCountryDDL();

        // 绑定洲
        BindStateDDL(Config.SystemCategory);
        BindBussessStateDDL(Config.SystemCategory);
        BindShippingStateDDL(Config.SystemCategory);
    }
    private void BindCustomerDG(bool autoUpdate)
    {
        BindCustomerDG(autoUpdate, true);
    }

    private void BindCustomerDG(bool autoUpdate, bool is_have_keyword)
    {
        string keyword = this.txt_customer_search_keyword.Text.Trim();

        // 是否有查询条件
        if (!is_have_keyword)
        {
            keyword = "";
        }
        DataTable dt= CustomerModel.GetModelsBySearch(keyword );
        this.dg_customer.DataSource = dt;
        this.dg_customer.DataBind();
        this.dg_customer.AutoUpdateAfterCallBack = autoUpdate;
        SetCustomerCountLabel(dt.Rows.Count.ToString());
    }

    private void SetCustomerCountLabel(string count)
    {
        AnthemHelper.SetLabel(this.lbl_customer_count, count);
    }

    private void BindStateDDL(int country_id)
    {
        StateShippingModel[]  ssm = StateShippingModel.GetModelsBySystemCategory(country_id);
        this.ddl_customer_card_state.DataSource = ssm;
        this.ddl_customer_card_state.DataTextField = "state_name";
        this.ddl_customer_card_state.DataValueField = "state_serial_no";
        this.ddl_customer_card_state.DataBind();
        AnthemHelper.SetDropDownListCheckItem(this.ddl_customer_card_state);
        this.ddl_customer_card_state.UpdateAfterCallBack = true;

    }

    private void BindBussessStateDDL(int country_id)
    {
        StateShippingModel[] ssm = StateShippingModel.GetModelsBySystemCategory(country_id);
        this.ddl_state_serial_no.DataSource = ssm;
        this.ddl_state_serial_no.DataTextField = "state_name";
        this.ddl_state_serial_no.DataValueField = "state_serial_no";
        this.ddl_state_serial_no.DataBind();
        AnthemHelper.SetDropDownListCheckItem(this.ddl_state_serial_no);
        this.ddl_state_serial_no.UpdateAfterCallBack = true;

    }

    private void BindShippingStateDDL(int country_id)
    {
        StateShippingModel[] ssm = StateShippingModel.GetModelsBySystemCategory(country_id);
        this.ddl_customer_shipping_state.DataSource = ssm;
        this.ddl_customer_shipping_state.DataTextField = "state_name";
        this.ddl_customer_shipping_state.DataValueField = "state_serial_no";
        this.ddl_customer_shipping_state.DataBind();
        AnthemHelper.SetDropDownListCheckItem(this.ddl_customer_shipping_state);
        this.ddl_customer_shipping_state.UpdateAfterCallBack = true;
    }

    private void BindCountryDDL()
    {
        CountryModel[] cms = CountryModel.FindAll();
        this.ddl_customer_card_country.DataSource = cms;
        this.ddl_customer_card_country.DataTextField = "name";
        this.ddl_customer_card_country.DataValueField = "id";
        this.ddl_customer_card_country.DataBind();


        this.ddl_customer_shipping_country.DataSource = cms;
        this.ddl_customer_shipping_country.DataTextField = "name";
        this.ddl_customer_shipping_country.DataValueField = "id";
        this.ddl_customer_shipping_country.DataBind();

        this.ddl_customer_country.DataSource = cms;
        this.ddl_customer_country.DataTextField = "name";
        this.ddl_customer_country.DataValueField = "id";
        this.ddl_customer_country.DataBind();
    }

    public void BindModifyCustomer(bool autoUpdate, int customer_id)
    {
        CustomerModel model = CustomerModel.GetCustomerModel(customer_id);

        AnthemHelper.SetAnthenTextBox(this.txt_customer_card_zip_code, model.customer_card_zip_code);
        AnthemHelper.SetAnthenTextBox(this.txt_customer_login_name, model.customer_login_name);
        AnthemHelper.SetAnthenTextBox(this.txt_phone_d, model.phone_d);
        AnthemHelper.SetAnthenTextBox(this.txt_phone_n, model.phone_n);
        AnthemHelper.SetAnthenTextBox(this.txt_phone_c, model.phone_c);
        AnthemHelper.SetAnthenTextBox(this.txt_customer_card_billing_shipping_address, model.customer_card_billing_shipping_address);

        AnthemHelper.SetAnthenTextBox(this.txt_customer_card_city, model.customer_card_city);
        try
        {
            AnthemHelper.SetDropDownListValue(this.ddl_customer_card_country, model.customer_card_country.ToString());
        }
        catch { }
        AnthemHelper.SetAnthenTextBox(this.txtcustomer_email1, model.customer_email1);
        AnthemHelper.SetAnthenTextBox(this.txtcustomer_email2, model.customer_email2);
        AnthemHelper.SetAnthenTextBox(this.txt_customer_credit_card, model.customer_credit_card);
        AnthemHelper.SetAnthenTextBox(this.txt_customer_expiry, model.customer_expiry);
        AnthemHelper.SetAnthenTextBox(this.txt_customer_company, model.customer_company);
        AnthemHelper.SetAnthenTextBox(this.txt_customer_business_telephone, model.customer_business_telephone);
        AnthemHelper.SetAnthenTextBox(this.txt_customer_fax, model.customer_fax);
        AnthemHelper.SetAnthenTextBox(this.txt_customer_note, model.customer_note);
        AnthemHelper.SetAnthenTextBox(this.txt_password, model.customer_password);
                                                
        AnthemHelper.SetAnthenTextBox(this.txtcustomer_first_name, model.customer_first_name);
        AnthemHelper.SetAnthenTextBox(this.txtcustomer_last_name, model.customer_last_name);
        try
        {
            AnthemHelper.SetDropDownListValue(this.ddl_customer_card_type, model.customer_card_type);
        }
        catch { }
        AnthemHelper.SetAnthenTextBox(this.txt_customer_card_phone, model.customer_card_phone);
        AnthemHelper.SetAnthenTextBox(this.txt_EBay_ID, model.EBay_ID);
        AnthemHelper.SetCheckBox(this.cb_email_tag, model.news_latter_subscribe == 1 ? true : false);

        AnthemHelper.SetAnthenTextBox(this.txt_customer_shipping_address, model.customer_shipping_address);
        AnthemHelper.SetAnthenTextBox(this.txt_customer_shipping_city, model.customer_shipping_city);
       
        AnthemHelper.SetAnthenTextBox(this.txt_shipping_first_name, model.customer_shipping_first_name);
        AnthemHelper.SetAnthenTextBox(this.txt_shipping_last_name, model.customer_shipping_last_name);
        
        AnthemHelper.SetAnthenTextBox(this.txt_customer_shipping_zip_code, model.customer_shipping_zip_code);
        AnthemHelper.SetAnthenTextBox(this.txt_customer_card_issuer, model.customer_card_issuer);

        BindShippingStateDDL(model.customer_shipping_country)  ;
        try
        {
           // AnthemHelper.Alert(model.customer_shipping_state.ToString());
            //AnthemHelper.SetDropDownListValue(this.ddl_shipping_state, model.customer_shipping_state.ToString());
            for (int i = 0; i < this.ddl_customer_shipping_state.Items.Count; i++)
            {
                this.ddl_customer_shipping_state.Items[i].Selected = false;
                if (this.ddl_customer_shipping_state.Items[i].Value.ToString() == model.customer_shipping_state.ToString())
                {
                    this.ddl_customer_shipping_state.Items[i].Selected = true;
                    this.ddl_customer_shipping_state.UpdateAfterCallBack = true;
                    //AnthemHelper.Alert(this.ddl_shipping_state.Items[i].Value.ToString());
                }
            }
        }
        catch
        {
            ListItem li = new ListItem("None", "-1");
            this.ddl_customer_shipping_state.Items.Insert(0, li);
            this.ddl_customer_shipping_state.UpdateAfterCallBack = true;
        }
        AnthemHelper.SetAnthenTextBox(this.txt_tax_execmtion, model.tax_execmtion);

        // 因为旧数据没有值
        try
        {
            AnthemHelper.SetDropDownListValue(this.ddl_customer_shipping_country, model.customer_shipping_country.ToString());
        }
        catch { }
        try
        {
            BindStateDDL(int.Parse(model.customer_country));
            AnthemHelper.SetDropDownListValue(this.ddl_customer_card_state, model.state_serial_no.ToString());
        }
        catch { }

        try
        {
            AnthemHelper.SetDropDownListValue(this.ddl_customer_country, model.customer_country.ToString());
        }
        catch { }
        try
        {
            BindBussessStateDDL(int.Parse(model.customer_country));
            AnthemHelper.SetDropDownListValue(this.ddl_state_serial_no, model.state_serial_no.ToString());
        }
        catch { }

        //AnthemHelper.Alert(model.state_serial_no.ToString());
        //this.btn_save.Text = "Modify";
       //this.btn_save.UpdateAfterCallBack = true;

        AnthemHelper.SetAnthenTextBox(this.txt_customer_address1, model.customer_address1);
        AnthemHelper.SetAnthenTextBox(this.txt_customer_city, model.customer_card_city);
        AnthemHelper.SetAnthenTextBox(this.txt_zip_code, model.zip_code);
        AnthemHelper.SetAnthenTextBox(this.txt_busniess_website, model.busniess_website);
        AnthemHelper.SetAnthenTextBox(this.txt_tax_execmtion, model.tax_execmtion);
        AnthemHelper.SetAnthenTextBox(this.txt_customer_fax, model.customer_fax);
        AnthemHelper.SetAnthenTextBox(this.txt_customer_rumor, model.customer_rumor);
    }

    public void SetControlsNull()
    {
        AnthemHelper.SetAnthenTextBoxNULL(this.txt_customer_card_zip_code);
        AnthemHelper.SetAnthenTextBoxNULL(this.txt_customer_login_name);
        AnthemHelper.SetAnthenTextBoxNULL(this.txt_password);
        AnthemHelper.SetAnthenTextBoxNULL(this.txt_phone_d);
        AnthemHelper.SetAnthenTextBoxNULL(this.txt_phone_n);
        AnthemHelper.SetAnthenTextBoxNULL(this.txt_phone_c);
        AnthemHelper.SetAnthenTextBoxNULL(this.txt_customer_card_billing_shipping_address);
       
        AnthemHelper.SetAnthenTextBoxNULL(this.txt_customer_card_city);
        //AnthemHelper.SetDropDownListValue(this.ddl_customer_country);
        //AnthemHelper.SetDropDownListValue(this.ddl_customer_tax_status);
        AnthemHelper.SetAnthenTextBoxNULL(this.txtcustomer_email1);
        AnthemHelper.SetAnthenTextBoxNULL(this.txtcustomer_email2);
        AnthemHelper.SetAnthenTextBoxNULL(this.txt_customer_credit_card);
        AnthemHelper.SetAnthenTextBoxNULL(this.txt_customer_expiry);
        AnthemHelper.SetAnthenTextBoxNULL(this.txt_customer_company);
        AnthemHelper.SetAnthenTextBoxNULL(this.txt_customer_business_telephone);
        AnthemHelper.SetAnthenTextBoxNULL(this.txt_customer_fax);
        AnthemHelper.SetAnthenTextBoxNULL(this.txt_customer_note);

        //AnthemHelper.SetDropDownListValue(this.ddl_state);

        AnthemHelper.SetAnthenTextBoxNULL(this.txtcustomer_first_name);
        AnthemHelper.SetAnthenTextBoxNULL(this.txtcustomer_last_name);
        //AnthemHelper.SetDropDownListValue(this.txtcustomer_card_type);
        AnthemHelper.SetAnthenTextBoxNULL(this.txt_customer_card_phone);
        AnthemHelper.SetAnthenTextBoxNULL(this.txt_EBay_ID);
        AnthemHelper.SetAnthenTextBoxNULL(this.txt_tax_execmtion);

        AnthemHelper.SetAnthenTextBoxNULL(this.txt_customer_shipping_address);
     
        AnthemHelper.SetAnthenTextBoxNULL(this.txt_customer_shipping_city);
       
        AnthemHelper.SetAnthenTextBoxNULL(this.txt_shipping_first_name);
        AnthemHelper.SetAnthenTextBoxNULL(this.txt_shipping_last_name);
       
        AnthemHelper.SetAnthenTextBoxNULL(this.txt_customer_shipping_zip_code);
        AnthemHelper.SetAnthenTextBoxNULL(this.txt_customer_card_issuer);


        AnthemHelper.SetAnthenTextBoxNULL(this.txt_customer_address1);
        AnthemHelper.SetAnthenTextBoxNULL(this.txt_customer_city);
        AnthemHelper.SetAnthenTextBoxNULL(this.txt_zip_code);
        AnthemHelper.SetAnthenTextBoxNULL(this.txt_busniess_website);
        AnthemHelper.SetAnthenTextBoxNULL(this.txt_tax_execmtion);
        AnthemHelper.SetAnthenTextBoxNULL(this.txt_customer_fax);
        AnthemHelper.SetAnthenTextBoxNULL(this.txt_customer_rumor);
        this.BindBussessStateDDL(-1);
        this.BindShippingStateDDL(-1);
        this.BindStateDDL(-1);
        //AnthemHelper.SetCheckBox(this.cb_email_tag);
    }

#endregion


    /// <summary>
    /// 修改客户信息
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btn_save_Click(object sender, EventArgs e)
    {
        if (CurrentCustomerID == -1)
        {
            CustomerCmd(Command.create);
        }
        else
        {
            CustomerCmd(Command.modif);
        }

        SetCustomerCheck(CurrentCustomerID);
    }

    /// <summary>
    /// 对客户信息进行操作
    /// </summary>
    /// <param name="cmd"></param>
    private void CustomerCmd(Command cmd)
    {
        try
        {
            CustomerModel model = new CustomerModel();
            if (cmd == Command.modif)
                model = CustomerModel.GetCustomerModel(CurrentCustomerID);
            else
            {
                if (this.txt_password.Text.Trim() == "")
                    model.customer_password = Config.defaultPassword;
                else
                    model.customer_password = this.txt_password.Text.Trim();
            }

            model.create_datetime = DateTime.Now;
            model.customer_card_billing_shipping_address = AnthemHelper.GetAnthemTextBox(this.txt_customer_card_billing_shipping_address).ToUpper();

            model.customer_card_phone = AnthemHelper.GetAnthemTextBox(this.txt_customer_card_phone).ToUpper();
            model.customer_card_type = this.ddl_customer_card_type.SelectedValue.ToString();
            model.phone_c = AnthemHelper.GetAnthemTextBox(this.txt_phone_c).ToUpper();
            model.customer_credit_card = AnthemHelper.GetAnthemTextBox(this.txt_customer_card_city).ToUpper();
            model.customer_company = AnthemHelper.GetAnthemTextBox(this.txt_customer_company).ToUpper();
            model.customer_card_country = int.Parse(this.ddl_customer_card_country.SelectedValue.ToString());
            model.customer_credit_card = AnthemHelper.GetAnthemTextBox(this.txt_customer_credit_card).ToUpper();
            model.customer_email1 = AnthemHelper.GetAnthemTextBox(this.txtcustomer_email1);
            model.customer_email2 = AnthemHelper.GetAnthemTextBox(this.txtcustomer_email2);
            model.customer_expiry = AnthemHelper.GetAnthemTextBox(this.txt_customer_expiry).ToUpper();
            model.customer_fax = AnthemHelper.GetAnthemTextBox(this.txt_customer_fax).ToUpper();
            model.customer_first_name = AnthemHelper.GetAnthemTextBox(this.txtcustomer_first_name).ToUpper();
            model.customer_login_name = AnthemHelper.GetAnthemTextBox(this.txt_customer_login_name).ToUpper();
            model.phone_d = AnthemHelper.GetAnthemTextBox(this.txt_phone_d).ToUpper();
            model.customer_last_name = AnthemHelper.GetAnthemTextBox(this.txtcustomer_last_name).ToUpper();
            model.customer_note = AnthemHelper.GetAnthemTextBox(this.txt_customer_note).ToUpper();

            model.customer_card_city = AnthemHelper.GetAnthemTextBox(this.txt_customer_card_city).ToUpper();

            //model.customer_tax_status = this.ddl_customer_tax_status.SelectedItem.Text;
            model.phone_n = AnthemHelper.GetAnthemTextBox(this.txt_phone_n).ToUpper();
            model.customer_business_telephone = AnthemHelper.GetAnthemTextBox(this.txt_customer_business_telephone).ToUpper();
            model.EBay_ID = AnthemHelper.GetAnthemTextBox(this.txt_EBay_ID).ToUpper();
            model.news_latter_subscribe = byte.Parse(this.cb_email_tag.Checked == true ? "1" : " 0");
            model.system_category_serial_no = Config.SystemCategory;
            model.customer_card_state = int.Parse(this.ddl_customer_card_state.SelectedValue.ToString());
            model.tag = 1;
            model.customer_card_zip_code = AnthemHelper.GetAnthemTextBox(this.txt_customer_card_zip_code).ToUpper();

            model.customer_shipping_address = AnthemHelper.GetAnthemTextBox(this.txt_customer_shipping_address).ToUpper();

            model.customer_shipping_city = AnthemHelper.GetAnthemTextBox(this.txt_customer_shipping_city).ToUpper();
            model.customer_shipping_state = int.Parse(this.ddl_customer_shipping_state.SelectedValue.ToString());
            model.customer_shipping_country = int.Parse(this.ddl_customer_shipping_country.SelectedValue.ToString());
            //AnthemHelper.Alert(this.ddl_shipping_country.SelectedValue.ToString());

            model.customer_shipping_first_name = AnthemHelper.GetAnthemTextBox(this.txt_shipping_first_name).ToUpper();
            model.customer_shipping_last_name = AnthemHelper.GetAnthemTextBox(this.txt_shipping_last_name).ToUpper();

            model.customer_shipping_zip_code = AnthemHelper.GetAnthemTextBox(this.txt_customer_shipping_zip_code).ToUpper();
            model.tax_execmtion = AnthemHelper.GetAnthemTextBox(this.txt_tax_execmtion).ToUpper();

            model.customer_card_phone = AnthemHelper.GetAnthemTextBox(this.txt_customer_card_phone).ToUpper();

            model.customer_card_issuer = AnthemHelper.GetAnthemTextBox(this.txt_customer_card_issuer).ToUpper();
            model.customer_rumor = AnthemHelper.GetAnthemTextBox(this.txt_customer_rumor).ToUpper();
            
            // AnthemHelper.Alert(this.ddl_shipping_state.SelectedValue.ToString());
            if (cmd == Command.modif)
            {
                model.Update();
                this.InsertTraceInfo("Modify Customer Info([" + model.customer_serial_no.ToString() + "]" + model.customer_first_name + " " + model.customer_last_name + ")");
            }
            else
            {
				model.customer_serial_no = Code.NewCustomerCode();
                model.Create();
                this.InsertTraceInfo("Create Customer Info([" + model.customer_serial_no.ToString() + "]" + model.customer_first_name + " " + model.customer_last_name + ")");
            }
            CurrentCustomerID = model.customer_serial_no;
            AnthemHelper.Alert(KeyFields.SaveIsOK);
            this.BindCustomerDG(true);
            // SetControlsNull();
            if (OrderCode != "")
                NewOrder1_onCliekSearchOrderCode();
        }
        catch (Exception ex)
        {
            AnthemHelper.Alert(ex.Message);
        }
    }
    protected void lb_search_customer_Click(object sender, EventArgs e)
    {
        try
        {
            this.dg_customer.CurrentPageIndex = 0;
            BindCustomerDG(true);
        }
        catch (Exception ex)
        {
            AnthemHelper.Alert(ex.Message);
        }
    }

    protected void dg_customer_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        this.dg_customer.CurrentPageIndex = e.NewPageIndex;
        this.BindCustomerDG(true);
    }

    protected void dg_customer_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        //LinkButton lb = (LinkButton)e.CommandSource;

        //try
        //{
        //    switch (lb.Text)
        //    {
        //        case "Checked":
        //            Panels(PanelType.order);
        //             int customer_id = AnthemHelper.GetAnthemDataGridCellText(e.Item, 0);
        //             CurrentCustomerID = customer_id;
        //            break;

        //        case "Modify":
        //            Cmd = Command.modif;
        //            int customerID = AnthemHelper.GetAnthemDataGridCellText(e.Item, 0);
        //            CurrentCustomerID = customerID;
        //            BindModifyCustomer(true, customerID);
        //            break;
        //    }
        //}
        //catch (Exception ex)
        //{
        //    AnthemHelper.Alert(ex.Message);
        //}
    }
    protected void NewOrder1_Load(object sender, EventArgs e)
    {

    }

    /// <summary>
    ///  选择用户事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void _cb_customer_CheckedChanged(object sender, EventArgs e)
    {
        bool b = false;
        try
        {
            Anthem.CheckBox radio = (Anthem.CheckBox)sender;

            Anthem.DataGrid dg = (Anthem.DataGrid)this.dg_customer;
            for (int i = 0; i < dg.Items.Count; i++)
            {
                Anthem.CheckBox r = (Anthem.CheckBox)dg.Items[i].Cells[0].FindControl("_cb_customer");
                if (r == radio && r.Checked)
                {
                    CurrentCustomerID = AnthemHelper.GetAnthemDataGridCellText(dg.Items[i], 1);
                    b = true;
                   // AnthemHelper.Alert(CurrentCustomerID.ToString());
                    // 绑定用户信息
                    BindModifyCustomer(true, CurrentCustomerID);
                    
                    //
                    this.cb_set_same.Checked = false;
                    this.cb_set_same.UpdateAfterCallBack = true;
                }
                else
                {
                    r.Checked = false;
                    r.UpdateAfterCallBack = true;
                }
            }
        }
        catch (Exception ex)
        {
            AnthemHelper.Alert(ex.Message);
        }
        // 如果没有选中用户，给于值-1
        if (!b)
            CurrentCustomerID = -1;
    }
    /// <summary>
    /// 新建客户信息
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lb_new_customer_Click(object sender, EventArgs e)
    {
        SetControlsNull();
       // CustomerCmd(Command.create);
        Cmd = Command.create;
        CurrentCustomerID = -1;
    }
    protected void lb_reset_customer_Click(object sender, EventArgs e)
    {
        this.CurrentCustomerID = -1;
        Cmd = Command.create;
        SetCustomerCheck(-1);
        SetControlsNull();
    }
    protected void txtcustomer_credit_card_TextChanged(object sender, EventArgs e)
    {
        string cord = this.txt_customer_credit_card.Text.Trim();
        if (cord == "")
            return;

        string value = "";
        cord = Helper.RemoveSpace(cord);
        //if (cord.Length != 16)
        //{
        //    AnthemHelper.Alert("卡号长度出错");
        //    this.txt_customer_credit_card.Text = "";
        //    this.txt_customer_credit_card.UpdateAfterCallBack = true;
        //    return;
        //}
        //if (cord.Substring(0, 1) == "4" || cord.Substring(0, 1) == "5")
        //{
        //    AnthemHelper.Alert("卡号格式错误");
        //    this.txt_customer_credit_card.Text = "";
        //    this.txt_customer_credit_card.UpdateAfterCallBack = true;
        //    return;
        //}
        ValidateCard(cord);
        for (int i = 0; i < cord.Length; i++)
        {
            
            //AnthemHelper.Alert((i % 4).ToString());
            if (i % 4 == 0 && i!=0)
            {
                value += " ";
                value += cord[i].ToString(); 
            }
            else
                value += cord[i].ToString(); 
        }
        this.txt_customer_credit_card.Text = value;
        this.txt_customer_credit_card.UpdateAfterCallBack = true;
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
            this.lbl_watch_card.UpdateAfterCallBack = true;
        }
        else
        {
            this.lbl_watch_card.Visible = false;
            this.lbl_watch_card.UpdateAfterCallBack = true;
        }
    }

    private void SetValidateName(string text, System.Drawing.Color c)
    {
        this.lbl_validate_customer.Text = text;
        this.lbl_validate_customer.ForeColor = c;
        this.lbl_validate_customer.UpdateAfterCallBack = true;
    }

    private void SetValidateLoginName(string text, System.Drawing.Color c)
    {
        this.lbl_validate_login.Text = text;
        this.lbl_validate_login.ForeColor = c;
        this.lbl_validate_login.UpdateAfterCallBack = true;
    }

    private void SetValidateFirstName(string text, System.Drawing.Color c)
    {
        this.lbl_validate_first_name.Text = text;
        this.lbl_validate_first_name.ForeColor = c;
        this.lbl_validate_first_name.UpdateAfterCallBack = true;
    }

    private void ValidateCustomerName()
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
                this.dg_customer.DataSource = models;
                this.dg_customer.DataBind();
                this.dg_customer.UpdateAfterCallBack = true;
            }
            else
            {
                this.SetValidateName(y_vali, System.Drawing.Color.Green);
                this.SetValidateFirstName(y_vali, System.Drawing.Color.Green);
            }
           
        }
        catch (Exception ex)
        {
            AnthemHelper.Alert(ex.Message);
        }
    }
    protected void txtcustomer_last_name_TextChanged(object sender, EventArgs e)
    {
        ValidateCustomerName();
    }
    protected void txtcustomer_fullname_TextChanged(object sender, EventArgs e)
    {
        SetValidateLoginName("", System.Drawing.Color.Red);

        DataTable dt = CustomerModel.GetModelsByLoginName(this.txt_customer_login_name.Text.Trim());

        if (dt.Rows.Count > 0)
        {
            this.SetValidateLoginName(n_vali, System.Drawing.Color.Red);
            this.dg_customer.DataSource = dt;
            this.dg_customer.DataBind();
            this.dg_customer.UpdateAfterCallBack = true;
        }
        else
        {
            this.SetValidateLoginName(y_vali, System.Drawing.Color.Green);
        }
        
    }
    protected void lb_clear_search_Click(object sender, EventArgs e)
    {
        this.BindCustomerDG(true, false);
    }
    protected void txtcustomer_first_name_TextChanged(object sender, EventArgs e)
    {
        ValidateCustomerName();
    }
    protected void ddl_customer_country_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindStateDDL(int.Parse(this.ddl_customer_card_country.SelectedValue));
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
    protected void lb_new_Order_Click(object sender, EventArgs e)
    {
        try
        {
            if (CurrentCustomerID == -1)
            {
                AnthemHelper.Alert("请选择一个客户");
                return;
            }
            else
            {
                OrderCode = OrderHelperModel.GetNewOrderCode().ToString();
                OrderHelperModel ohm = new OrderHelperModel();
                ohm.order_code = int.Parse(OrderCode);
                ohm.customer_serial_no = CurrentCustomerID;
                ohm.create_datetime = DateTime.Now;
                ohm.order_date = DateTime.Now;
                ohm.tag = 1;
                ohm.is_ok = true;
                ohm.Create();
                CustomerHelper ch = new CustomerHelper();
                ch.CopyCustomer(OrderCode, CurrentCustomerID);
                this.InsertTraceInfo("Create One Order (" + OrderCode.ToString() + ")");
                //AnthemHelper.OpenWin("sale_add_order_pay_method.aspx?order_code=" + OrderCode, 1000, 800,10,10);
                AnthemHelper.Redirect("sale_add_order_pay_method.aspx?order_code=" + OrderCode);
            }
        }
        catch (Exception ex)
        {
            AnthemHelper.Alert(ex.Message);
        }
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
        Anthem.TextBox tb = (Anthem.TextBox)sender;

        string text = Helper.RemoveSpace(tb.Text).Replace("-","");

        if (text == "")
            return ;
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
        tb.UpdateAfterCallBack = true;
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
                    StateShippingModel m = StateShippingModel.GetStateShippingModel(int.Parse(state));
                    e.Item.Cells[5].Text = m.state_short_name.ToString();
                }
                catch { }
            }
        }
    }
    protected void ddl_shipping_country_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindShippingStateDDL(int.Parse(this.ddl_customer_shipping_country.SelectedValue.ToString()));
    }

    protected void cb_set_same_CheckedChanged(object sender, EventArgs e)
    {
        if (this.cb_set_same.Checked)
        {
            this.txt_customer_shipping_address.Text = this.txt_customer_card_billing_shipping_address.Text;
            
            this.txt_customer_shipping_city.Text = this.txt_customer_card_city.Text;
     
            this.txt_shipping_first_name.Text = this.txtcustomer_first_name.Text;
            this.txt_shipping_last_name.Text = this.txtcustomer_last_name.Text;
            this.txt_customer_shipping_zip_code.Text = this.txt_customer_card_zip_code.Text;
           

            this.ddl_customer_shipping_country.SelectedIndex = this.ddl_customer_card_country.SelectedIndex;
            this.ddl_customer_shipping_country.UpdateAfterCallBack = true;
            this.BindShippingStateDDL(int.Parse(this.ddl_customer_card_country.SelectedValue.ToString()));
            this.ddl_customer_shipping_state.SelectedIndex = this.ddl_customer_card_state.SelectedIndex;
            this.ddl_customer_shipping_state.UpdateAfterCallBack = true;

            this.txt_customer_shipping_address.UpdateAfterCallBack = true;
           
            this.txt_customer_shipping_city.UpdateAfterCallBack = true;
            
            this.txt_shipping_first_name.UpdateAfterCallBack = true;
            this.txt_shipping_last_name.UpdateAfterCallBack = true;
            this.txt_customer_shipping_zip_code.UpdateAfterCallBack = true;

        }
    }

    private void SetCustomerCheck(int customer_id)
    {
        Anthem.DataGrid dg = (Anthem.DataGrid)this.dg_customer;
        for (int i = 0; i < dg.Items.Count; i++)
        {
            DataGridItem item = dg.Items[i];
            int cid = int.Parse(item.Cells[1].Text);

            Anthem.CheckBox cb = (Anthem.CheckBox)item.Cells[0].FindControl("_cb_customer");
            if (cid == customer_id)
                cb.Checked = true;
            else
                cb.Checked = false;
            cb.UpdateAfterCallBack = true;
        }

    }
    protected void ddl_customer_country_SelectedIndexChanged1(object sender, EventArgs e)
    {
        BindBussessStateDDL(int.Parse(this.ddl_customer_country.SelectedValue.ToString()));
    }
}