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

public partial class email_templete : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SetControlsLabel();
        }
    }

    public void SetControlsLabel()
    {
        OrderHelperModel[] models = OrderHelperModel.GetModelsByOrderCode(OrderCode);
        CustomerStoreModel[] customers = CustomerStoreModel.FindModelsByOrderCode(OrderCode.ToString());
        if (customers.Length != 1)
            return;
        if (models.Length != 1)
            return;
        CustomerStoreModel customer = customers[0];
        OrderHelperModel model = models[0];
        // this.lbl_status_date.Text = DateTime.Now.ToString();
        this.lbl_cc_code.Text = customer.customer_credit_card;
        this.lbl_cc_phone.Text = customer.customer_card_phone;
        // this.lbl_cc_surcharge.Text = Config.ConvertPrice(model.cc_surcharge);
        this.lbl_cc_type.Text = customer.customer_card_type;
        this.lbl_customer_address1.Text = customer.customer_shipping_address; ;
       // this.lbl_customer_address2 = model.shipping_address;
       // this.lbl_customer_city_state_zip.Text = model.
        this.lbl_customer_country.Text = CountryModel.GetCountryModel(customer.customer_shipping_country).name;

       //  this.lbl_customer_expiry.Text = model.ex
        this.lbl_customer_first_name.Text = customer.customer_shipping_first_name;
       // this.lbl_customer_fullname .Text = model.fu 

        this.lbl_customer_last_name.Text = customer.customer_shipping_last_name;
        // this.lbl_customer_work_tel.Text = model.cu
        // this.lbl_gst.Text = Config.ConvertPrice(model.gst);
        this.lbl_order_code.Text = model.order_code.ToString();
        this.lbl_pay_method.Text = model.pay_method;
        //this.lbl_pst.Text = Config.ConvertPrice(model.pst);
       // this.lbl_shipping.Text = Config.ConvertPrice(model.shipping);
        this.lbl_state_serial_no.Text = StateShippingModel.GetStateShippingModel(customer.customer_shipping_state).state_name;
        //this.lbl_status_name.Text = model.st
        this.lbl_zip_code.Text = customer.customer_shipping_zip_code;
        this.lbl_sub_total.Text = Config.ConvertPrice(model.sub_total);
        this.lbl_sub_total_1.Text = Config.ConvertPrice(model.sub_total);
        this.lbl_sub_total_2.Text = Config.ConvertPrice(model.sub_total);
        this.lbl_sub_total_3.Text = Config.ConvertPrice(model.sub_total);
        this.lbl_total.Text = Config.ConvertPrice(model.total);
       
    }

    public int OrderCode
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "order_code", -1); }
    }
}
