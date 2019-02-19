using LU.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Q_Admin_orders_edit_detail_change_other : PageBase
{
    public LU.Data.tb_order_helper OH
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

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            OH = OrderHelperModel.GetModelByOrderCode(DBContext, ReqOrderCode);
            InitPage();
        }
    }
    

    void InitPage()
    {
        var customers = CustomerStoreModel.FindModelsByOrderCode(DBContext, ReqOrderCode.ToString());

        if (customers.Length > 0)
        {
            var customer = customers[0];
            //
            // shipping info
            //           
            CustomerID = customer.customer_serial_no.Value;
            StoreCustomerSerialNo = customer.serial_no;

            this.txt_tax_examp.Text = customer.tax_execmtion;
            this.CheckBox1.Checked = customer.is_all_tax_execmtion.Value;
            BindPayMethodDDL(customer.pay_method.Value);

            BindPickUpDay();
            BindPickUpHour();
            BindPickUpMonth();
            BindShippingCompany(OH.shipping_company.Value);

            BindOrderStatus(OH.pre_status_serial_no, OH.out_status);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private void BindOrderStatus(int WebStatus, int WorkStatus)
    {

        //this.ddl_back_status.DataSource = FactureStateModel.FindModelsByShowit();
        //this.ddl_back_status.DataTextField = "facture_state_name";
        //this.ddl_back_status.DataValueField = "facture_state_serial_no";
        //this.ddl_back_status.DataBind();
        //this.ddl_back_status.SelectedValue = WorkStatus.ToString();

        this.ddl_pre_status.DataSource = PreStatusModel.FindModelsByShowit(DBContext);
        this.ddl_pre_status.DataTextField = "pre_status_name";
        this.ddl_pre_status.DataValueField = "pre_status_serial_no";
        this.ddl_pre_status.DataBind();
        this.ddl_pre_status.SelectedValue = WebStatus.ToString();
    }

    private void BindShippingCompany(int ShippingCompanyID)
    {
        XmlStore xs = new XmlStore();
        ddl_ship_method.DataSource = xs.FindShippingCompany();
        this.ddl_ship_method.DataTextField = "shipping_company_name";
        this.ddl_ship_method.DataValueField = "shipping_company_id";
        this.ddl_ship_method.DataBind();
        this.ddl_ship_method.Items.Insert(0, new ListItem("NONE", "-1"));
        this.ddl_ship_method.SelectedValue = ShippingCompanyID.ToString();
    }

    #region year day month
    /// <summary>
    /// 
    /// </summary>
    /// <param name="autoUpdate"></param>
    private void BindPickUpDay()
    {
        this.ddl_pick_up_day.Items.Clear();

        for (int i = 1; i <= 31; i++)
        {
            ListItem li = new ListItem();
            li.Text = i.ToString();
            li.Value = i.ToString();
            this.ddl_pick_up_day.Items.Add(li);
        }
        ddl_pick_up_day.SelectedValue = OH.prick_up_datetime1.Value.Day.ToString();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="autoUpdate"></param>
    private void BindPickUpMonth()
    {
        this.ddl_pick_up_month.Items.Clear();

        for (int i = 1; i <= 12; i++)
        {
            ListItem li = new ListItem();
            li.Text = i.ToString();
            li.Value = i.ToString();
            this.ddl_pick_up_month.Items.Add(li);
        }
        ddl_pick_up_month.SelectedValue = OH.prick_up_datetime1.Value.Month.ToString();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="autoUpdate"></param>
    private void BindPickUpHour()
    {
        this.ddl_pick_up_hour.Items.Clear();

        for (int i = 11; i <= 19; i++)
        {
            ListItem li = new ListItem();
            li.Text = i.ToString();
            li.Value = i.ToString();
            this.ddl_pick_up_hour.Items.Add(li);
        }
        this.ddl_pick_up_hour.SelectedValue = OH.prick_up_datetime1.Value.Hour.ToString();
    }

    #endregion

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

    private void BindPayMethodDDL(int paymethod_id)
    {
        XmlStore xs = new XmlStore();
        if (ddl_paymethod.Items.Count < 1)
        {
            this.ddl_paymethod.DataSource = xs.FindPayMethods();
            this.ddl_paymethod.DataTextField = "pay_method_name";
            this.ddl_paymethod.DataValueField = "pay_method_serial_no";
            this.ddl_paymethod.DataBind();
        }
        if (paymethod_id != 0)
            this.ddl_paymethod.SelectedValue = paymethod_id.ToString();
    }


    protected void btn_save_other_Click(object sender, EventArgs e)
    {

        int pay_method_id;
        int.TryParse(this.ddl_paymethod.SelectedValue, out pay_method_id);
        var cm = CustomerModel.GetCustomerModel(DBContext, CustomerID);
        cm.tax_execmtion = this.txt_tax_examp.Text.Trim();
        //cm.my_purchase_order = this.txt_purchase_order.Text.Trim();
        cm.pay_method = pay_method_id;
        cm.is_all_tax_execmtion = this.CheckBox1.Checked;
        if (CustomerID != 7888888)
        {
            DBContext.SaveChanges();
        }

        var exist = true;
        var csm = DBContext.tb_customer_store.FirstOrDefault(me => me.serial_no.Equals(StoreCustomerSerialNo)); //; CustomerStoreModel.GetCustomerStoreModel(DBContext, StoreCustomerSerialNo);
        if (csm == null)
        {
            exist = false;
            csm = new tb_customer_store();
        }
        csm.tax_execmtion = cm.tax_execmtion;
        csm.my_purchase_order = cm.my_purchase_order;
        csm.pay_method = cm.pay_method;
        csm.is_all_tax_execmtion = this.CheckBox1.Checked;
        if (!exist)
            DBContext.tb_customer_store.Add(csm);
        DBContext.SaveChanges();

        InsertTraceInfo(DBContext, string.Format("Save Order({0}) Other info.", ReqOrderCode));

        string pick_up_time = DateTime.Now.Year.ToString() + "-" + this.ddl_pick_up_month.SelectedValue.ToString()
    + "-" + this.ddl_pick_up_day.SelectedValue.ToString() + " " + this.ddl_pick_up_hour.SelectedValue.ToString() + ":0:0";

        exist = true;
        var hm = OrderHelperModel.GetModelByOrderCode(DBContext, ReqOrderCode);
        if(hm == null)
        {
            exist = false;
            hm = new tb_order_helper();
        }
        hm.prick_up_datetime1 = DateTime.Parse(pick_up_time);
        hm.pay_method = pay_method_id.ToString();
        hm.shipping_company = int.Parse(this.ddl_ship_method.SelectedValue);
        //hm.out_status = int.Parse(this.ddl_back_status.SelectedValue.ToString());
        hm.pre_status_serial_no = int.Parse(this.ddl_pre_status.SelectedValue.ToString());
        if (!exist)
        {
            DBContext.tb_order_helper.Add(hm);
        }
        DBContext.SaveChanges();

        OrdersSavePageRedirect(ReqOrderCode);
        if (ReqIsNew)
        {
            Response.Write("<script>parent.location.href= '/q_admin/orders_edit_detail_new.aspx?order_code=" + ReqOrderCode.ToString() + "';this.close(); </script>");
        }
        else
            Response.Write("<script>parent.location.href= '/q_admin/orders_edit_detail.aspx?order_code=" + ReqOrderCode.ToString() + "'; this.close();</script>");
    }
    protected void ddl_ship_method_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}