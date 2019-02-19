﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Q_Admin_orders_edit_detail_modify_fee : PageBase
{
    OrderHelperModel OH 
    {
        get
        {
            object obj = ViewState["OrderHelperModel"];
            if (obj != null)
                return (OrderHelperModel)obj;
            else
            {
                OrderHelperModel m = OrderHelperModel.GetModelByOrderCode(ReqOrderCode);
                ViewState["OrderHelperModel"] = m;
                return m;
            }
        }
        set { ViewState["OrderHelperModel"] = value; }
    }        
        
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitialDatabase();
        }
    }

    public override void InitialDatabase()
    {
        base.InitialDatabase();

        if (OH != null)
        {
            BindShippingCompany(OH.shipping_company);
            this.txt_ship_charge.Text = OH.shipping_charge.ToString();
            this.txt_special_discount.Text = OH.input_order_discount.ToString();
            this.txt_weee.Text = OH.weee_charge.ToString();
            if (OH.price_unit.ToLower() == "cad")
                this.RadioButtonList1.SelectedValue = "CAD";
            else
                this.RadioButtonList1.SelectedValue = "USD";
            this.CheckBox_lock_input_discount.Checked = OH.is_lock_input_order_discount;
            this.CheckBox_lock_input_ship_charge.Checked = OH.is_lock_shipping_charge;
            this.CheckBox_lock_tax_change.Checked = OH.is_lock_tax_change;

            BindTaxRate((int)(OH.gst_rate + OH.pst_rate + OH.hst_rate));
        }
    }

    void BindTaxRate(int tax)
    {
        DataTable dt = new StateShippingModel().GetAllTaxList();
        this.ddl_tax.Items.Clear();
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataRow dr = dt.Rows[i];

            int pst;
            int gst;
            int.TryParse(dr["pst"].ToString(), out pst);
            int.TryParse(dr["gst"].ToString(), out gst);
            ListItem li = new ListItem();
            li.Value = (pst + gst).ToString();
            li.Text = string.Format("{0}% : {1}", pst + gst, (pst + gst == 0 ? "Freign Country" : dr["stateNames"].ToString()));
           
            if (tax == pst + gst )
                li.Selected = true;
            this.ddl_tax.Items.Add(li);
        }
    }

    int ReqOrderCode
    {
        get { return Util.GetInt32SafeFromQueryString(this, "OrderCode", -1); }
    }

    bool ReqIsNew
    {
        get { return Util.GetInt32SafeFromQueryString(this, "is_new", -1) == 1; }
    }

    protected void bt_save_credit_card_Click(object sender, EventArgs e)
    {
        OH.price_unit = this.RadioButtonList1.SelectedItem.Text;
        decimal weee;
        decimal.TryParse(this.txt_weee.Text, out weee);

        decimal special_discount;
        decimal.TryParse(this.txt_special_discount.Text, out special_discount);

        decimal ship_charge;
        decimal.TryParse(this.txt_ship_charge.Text, out ship_charge);

        OH.weee_charge = weee;
        OH.is_lock_input_order_discount = this.CheckBox_lock_input_discount.Checked;
        OH.is_lock_shipping_charge = this.CheckBox_lock_input_ship_charge.Checked;
        if (OH.is_lock_shipping_charge == true)
            OH.shipping_charge = ship_charge;
        OH.input_order_discount = special_discount;
        OH.shipping_company = int.Parse(this.ddl_ship_method.SelectedValue);
        OH.is_lock_tax_change = CheckBox_lock_tax_change.Checked;

        int tax = int.Parse(ddl_tax.SelectedValue.ToString());
        if (tax == 5)
        {
            OH.gst_rate = tax;
            OH.pst_rate = 0M;
            OH.hst_rate = 0M;
        }
        else if (tax > 5 && tax < 10)
        {

            OH.pst_rate = tax;
            OH.gst_rate = 0M;
            OH.hst_rate = 0M;
        }
        else if (tax > 10)
        {
            OH.pst_rate = 0M;
            OH.gst_rate = 0M;
            OH.hst_rate = tax;
        }
        else if (tax == 0)
        {
            OH.pst_rate = 0M;
            OH.gst_rate = 0M;
            OH.hst_rate = 0M;
        }

        OH.Update();
        InsertTraceInfo(string.Format("Save Order Price({0}) fee.", ReqOrderCode));
        OrdersSavePageRedirect(ReqOrderCode);

        Response.Write(OH.shipping_charge.ToString());
       // Response.End();
        if (ReqIsNew)
        {
            Response.Write("<script>parent.location.href= '/q_admin/orders_edit_detail_new.aspx?order_code=" + ReqOrderCode.ToString() + "'; </script>");
        }
        else
            Response.Write("<script>parent.location.href= '/q_admin/orders_edit_detail.aspx?order_code=" + ReqOrderCode.ToString() + "'; </script>");
  
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
   
}