using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Q_Admin_orders_edit_detail_personal_information : PageBase
{

    XmlStore XS = new XmlStore();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            otherCountryArea.Visible = false;

            InitPage();
        }
    }
    

    void InitPage()
    {

        //
        // personal information
        //

        // CustomerModel model = CustomerModel.GetCustomerModel(cutomer_id);
        var customers = CustomerStoreModel.FindModelsByOrderCode(DBContext, ReqOrderCode.ToString());
        if (customers.Length > 0)
        {
            var customer = customers[0];
            StoreCustomerSerialNo = customer.serial_no;
            CustomerID = customer.customer_serial_no.Value;
            this.txt_phone_c.Text = customer.phone_c;
            this.txt_email1.Text = customer.customer_email1;
            this.txt_email2.Text = customer.customer_email2;
            this.txt_customer_company.Text = customer.customer_company;
            this.txt_first_name.Text = customer.customer_first_name;
            this.txt_phone_d.Text = customer.phone_d;
            this.txt_last_name.Text = customer.customer_last_name;
            this.txt_phone_n.Text = customer.phone_n;

            BindCustomerCountry(customer.customer_country_code ?? "");

            if (customer.customer_country_code.ToLower() == "us"
               || customer.customer_country_code.ToLower() == "ca")
            {
                ddl_customer_country_SelectedIndexChanged(null, null);
                ddl_customer_state.SelectedValue = customer.state_code;
            }
            else
            {
                otherCountryArea.Visible = true;
                txt_inputCountry.Text = customer.customer_country_code;
                txt_inputState.Text = customer.state_code;
            }

            this.txt_customer_address.Text = customer.customer_address1;
            this.txt_customer_city.Text = customer.customer_city;
            this.txt_customer_zip_code.Text = customer.zip_code;
        }
    }

    private void BindCustomerCountry(string selectedValue)
    {

        XmlStore xs = new XmlStore();
        this.ddl_customer_country.DataSource = CountryCategoryHelper.CountryCategoryToDataTable();
        this.ddl_customer_country.DataTextField = "text";
        this.ddl_customer_country.DataValueField = "text";
        this.ddl_customer_country.DataBind();
        if (selectedValue.ToLower() == "us" || selectedValue.ToLower() == "ca")
            this.ddl_customer_country.SelectedValue = selectedValue;
        else
            this.ddl_customer_country.SelectedValue = "Other";
    }

    /// <summary>
    /// 保存用户信息
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btn_save_personal_info_Click(object sender, EventArgs e)
    {

        var cm = CustomerModel.GetCustomerModel(DBContext, CustomerID);
        cm.customer_company = this.txt_customer_company.Text.Trim();
        cm.customer_first_name = this.txt_first_name.Text.Trim();
        cm.customer_last_name = txt_last_name.Text.Trim();
        cm.customer_city = this.txt_customer_city.Text.Trim();
        cm.customer_address1 = this.txt_customer_address.Text.Trim();
        cm.zip_code = this.txt_customer_zip_code.Text.Trim();
        cm.customer_country_code = this.ddl_customer_country.SelectedValue;
        cm.state_code = this.ddl_customer_state.SelectedValue;
        cm.phone_d = this.txt_phone_d.Text.Trim();
        cm.phone_n = this.txt_phone_n.Text.Trim();
        cm.phone_c = this.txt_phone_c.Text.Trim();
        cm.customer_email1 = this.txt_email1.Text.Trim();
        cm.customer_email2 = this.txt_email2.Text.Trim();
        if (this.ddl_customer_country.SelectedValue.ToLower() == "other")
        {
            cm.customer_country_code = txt_inputCountry.Text.Trim();
            cm.state_code = txt_inputState.Text.Trim();
            new StateShippingModel().SaveNewState(DBContext, cm.customer_country_code, cm.state_code);
        }
        else
        {
            cm.customer_country_code = this.ddl_customer_country.SelectedValue.ToString();
            cm.state_code = this.ddl_customer_state.SelectedValue.ToString();
        }
        if (CustomerID != 7888888 && CustomerID > 0)
        {
            DBContext.SaveChanges();
        }

        bool isNew = false;
        var csm = DBContext.tb_customer_store.FirstOrDefault(me=>me.serial_no.Equals(StoreCustomerSerialNo));
        if (csm == null)
        {
            isNew = true;
               csm = new LU.Data.tb_customer_store(); // CustomerStoreModel.GetCustomerStoreModel(DBContext,  StoreCustomerSerialNo);
        }
        csm.customer_company = cm.customer_company;
        csm.customer_first_name = cm.customer_first_name;
        csm.customer_last_name = cm.customer_last_name;
        csm.customer_city = cm.customer_city;
        csm.customer_address1 = cm.customer_address1;
        csm.zip_code = cm.zip_code;
        if (this.ddl_customer_country.SelectedValue.ToLower() == "other")
        {
            csm.customer_country_code = txt_inputCountry.Text.Trim();
            csm.state_code = txt_inputState.Text.Trim();
            new StateShippingModel().SaveNewState(DBContext, cm.customer_country_code, cm.state_code);
        }
        else
        {

            csm.customer_country_code = cm.customer_country_code;
            csm.state_code = cm.state_code;
        }
        csm.phone_d = cm.phone_d;
        csm.phone_n = cm.phone_n;
        csm.phone_c = cm.phone_c;
        csm.customer_email1 = cm.customer_email1;
        csm.customer_email2 = cm.customer_email2;
        csm.customer_country_code = cm.customer_country_code;

        if (isNew)
        {
            DBContext.tb_customer_store.Add(csm);
        }
        DBContext.SaveChanges();

        InsertTraceInfo(DBContext, string.Format("Save Order Price({0}) personial info...", ReqOrderCode));
        if (ReqIsNew)
        {
            Response.Write("<script>parent.location.href= '/q_admin/orders_edit_detail_new.aspx?order_code=" + ReqOrderCode.ToString() + "'; this.close();</script>");
        }
        else
            Response.Write("<script>parent.location.href= '/q_admin/orders_edit_detail.aspx?order_code=" + ReqOrderCode.ToString() + "';this.close(); </script>");
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

    protected void ddl_customer_country_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (this.ddl_customer_country.SelectedValue.ToLower() == "other")
        {
            otherCountryArea.Visible = true;
            ddl_customer_state.Visible = false;
        }
        else
        {
            otherCountryArea.Visible = false;
            ddl_customer_state.Visible = true;
            BindStateDDL((CountryCategory)Enum.Parse(typeof(CountryCategory), this.ddl_customer_country.SelectedValue));
        }
    }

    private void BindStateDDL(CountryCategory cc)
    {
        BindStateDDL(cc, "-1");
    }

    private void BindStateDDL(CountryCategory cc, string selectedvalue)
    {

        DataTable ssm = XS.FindStateByCountry(cc);
        if (ssm != null)
        {
            this.ddl_customer_state.DataSource = ssm;
            this.ddl_customer_state.DataTextField = "state_name";
            this.ddl_customer_state.DataValueField = "state_code";
            this.ddl_customer_state.DataBind();
            //this.btn_cancel.Text = selectedvalue;
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
}