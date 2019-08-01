﻿using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using LU.Data;

public partial class AccountCharge : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        try
        {
            var context = new LU.Data.nicklu2Entities();
            HstRate = 0M;
            GstRate = 0M;
            PstRate = 0M;

            UpdateStateCompanyCountry(context);
            UpdateCompanyCountry(context);
            accountCharge(context);
            if (RedirectCheckOut)
                Response.Redirect("/shopping_check_out.asp?country=1&state_shipping=8&pickup_checked=true&Pay_method=21");
        }
        catch (Exception ex)
        {
            Response.Write("<h2 >Service not available.</h2>  <!-- Please select Destination and Shipping Method //--><br/><br/><br/><br/><br/><br/><span style='color:#f2f2f2'>" + ex.Message + "</span>");

        }
    }

    public void WriteInputValue(decimal sub_total, decimal tax, decimal shipping_charge,
        decimal sub_total_rate, decimal sale_tax_rate, decimal cost)
    {
        var context = new LU.Data.nicklu2Entities();

        CartTempPriceModel.DeleteByOrderCode(TmpCode.ToString());
        var mm = new tb_cart_temp_price();// CartTempPriceModel();
        mm.create_datetime = DateTime.Now;

        mm.order_code = TmpCode.ToString();
        // pick up paymeth charge 0$
        if (Config.pay_method_pick_up_ids.IndexOf("[" + Pay_method.ToString() + "]") != -1)
            mm.shipping_and_handling = 0M;
        else
            mm.shipping_and_handling = ConvertPrice.RoundPrice(shipping_charge);
        mm.sub_total = sub_total;
        mm.sur_charge_rate = Config.is_card_rate - 1;
        // mm.sur_charge = sub_total_rate - decimal.Parse((sub_total_rate / (Config.is_card_rate )).ToString("#######.00"));

        mm.sur_charge = ConvertPrice.SpecialCashPriceDiscount(sub_total_rate);

        mm.sub_total_rate = ConvertPrice.RoundPrice(sub_total_rate);
        if (Config.pay_method_use_card_rate.IndexOf("[" + this.Pay_method.ToString() + "]") != -1 || this.Pay_method == -1)
            mm.taxable_total = mm.shipping_and_handling + mm.sub_total_rate;
        else
            mm.taxable_total = mm.shipping_and_handling + mm.sub_total_rate - mm.sur_charge;


        mm.pst = mm.taxable_total * PstRate / 100;
        mm.pst_rate = this.PstRate;
        mm.gst = mm.taxable_total * GstRate / 100;
        mm.gst_rate = this.GstRate;
        mm.hst = mm.taxable_total * HstRate / 100;
        mm.hst_rate = this.HstRate;
        mm.sales_tax = mm.pst + mm.gst + mm.hst;

        mm.sales_tax_rate = sale_tax_rate;
        //}
        //}

        mm.grand_total = mm.shipping_and_handling + mm.sub_total_rate + mm.sales_tax - mm.sur_charge;
        mm.grand_total_rate = mm.shipping_and_handling + mm.sub_total_rate + mm.sales_tax;

        mm.cost = cost;

        context.tb_cart_temp_price.Add(mm);
        context.SaveChanges();


        if (SetParentPrice == 1)
        {
            Response.Write("<script>parent.document.getElementById(\"sub_total_input\").value='" + sub_total_rate.ToString() + "';");
            Response.Write("parent.document.getElementById(\"shipping_charge_input\").value='" + shipping_charge.ToString() + "';");
            Response.Write("parent.document.getElementById(\"tax_input\").value='" + tax.ToString() + "';");
            Response.Write("parent.document.getElementById(\"total_input\").value='" + mm.grand_total_rate.ToString() + "';</script>");

            this.lbl_sales_tax.Text = "";
            this.lbl_state_tax.Text = "";
            if (CountryID == CountryCategoryHelper.CountryCategory_value(CountryCategory.CA))
            {
                if (PstRate > 0)
                {
                    if (lbl_sales_tax.Text.Length > 2)
                    {
                        lbl_state_tax.Text += "<br/>PST(" + PstRate.ToString() + "%)：";
                        lbl_sales_tax.Text += "&nbsp; &nbsp; &nbsp;<br/>" + mm.pst.Value.ToString("$###,###.00");
                    }
                    else
                    {
                        lbl_sales_tax.Text = mm.pst.Value.ToString("$###,###.00");
                        lbl_state_tax.Text = "PST(" + PstRate.ToString() + "%)：";

                    }
                }
                if (GstRate > 0)
                {
                    if (lbl_sales_tax.Text.Length > 2)
                    {
                        lbl_sales_tax.Text += "&nbsp; &nbsp; &nbsp;<br/>" + mm.gst.Value.ToString("$###,###.00");
                        lbl_state_tax.Text += "<br/>GST(" + GstRate.ToString() + "%)：";
                    }
                    else
                    {
                        lbl_sales_tax.Text = mm.gst.Value.ToString("$###,###.00");
                        lbl_state_tax.Text = "GST(" + GstRate.ToString() + "%)：";

                    }
                }
                if (HstRate > 0)
                {
                    if (lbl_sales_tax.Text.Length > 2)
                    {
                        lbl_sales_tax.Text += "&nbsp; &nbsp; &nbsp;<br/>" + mm.hst.Value.ToString("$###,###.00");
                        lbl_state_tax.Text += "<br/>HST(" + HstRate.ToString() + "%)：";
                    }
                    else
                    {
                        lbl_sales_tax.Text = mm.hst.Value.ToString("$###,###.00");
                        lbl_state_tax.Text = "HST(" + HstRate.ToString() + "%)：";
                    }
                }
            }
            else
            {
                this.lbl_sales_tax.Text = "$0.00";
                this.lbl_state_tax.Text = "Sales Tax (0%)：";
            }
        }
        if (SetParentPrice == 2)
        {
            Response.Write("<script>parent.window.location.href='shopping_check_out.asp?Pay_method=" + this.Pay_method + "';</script>");

        }
    }

    public void accountCharge(LU.Data.nicklu2Entities context)
    {
        string error_msg = "";// "Service not available.";
        if (TmpCode.ToString().Length != 6)
            return;
        DataTable dt = CartTempModel.GetModelsDTByTmeCode(TmpCode);
        int sale_promotion_count = Config.ExecuteScalarInt32(@"select count(c.cart_temp_serial_no)  from tb_cart_temp  c  inner join tb_product_shipping_fee ps 
on ps.prod_sku=c.product_serial_no and ps.is_system=0 where c.cart_temp_code='" + TmpCode + "' ");

        int ap_real_count = dt.Rows.Count - (Config.sale_promotion_compay_id.IndexOf("[" + ShippingCompany + "]") == -1 ? 0 : sale_promotion_count);

        AccountProduct[] ap = new AccountProduct[ap_real_count];
        decimal sale_promotion_charge = 0M;

        int ap_count = 0;

        int _state_shipping = 0;
        decimal _price_sum = 0;
        decimal _price_sum_rate = 0M;
        decimal _sale_tax = 0;
        decimal result = 0;
        decimal _cost = 0M;
        // 税百分比
        int sale_tax_rate = 0;

        for (int i = 0; i < dt.Rows.Count; i++)
        {

            DataRow dr = dt.Rows[i];


            DataTable sales_promotion_dt = Config.ExecuteDataTable(string.Format(@"
select 	prod_shipping_fee_id, prod_Sku, is_system, shipping_fee_us, shipping_fee_ca, regdate
	 
	from 
	tb_product_shipping_fee 
	where prod_sku='{0}' and is_system='{1}'", dr["product_serial_no"].ToString(), dr["product_serial_no"].ToString().Length == 8 ? 1 : 0));

            int count = int.Parse(dr["cart_temp_Quantity"].ToString());
            _state_shipping = int.Parse(dr["state_shipping"].ToString());
            _price_sum += decimal.Parse(dr["price"].ToString()) * count;
            _price_sum_rate += decimal.Parse(dr["price_rate"].ToString()) * count;

            if (sales_promotion_dt.Rows.Count == 0 || Config.sale_promotion_compay_id.IndexOf("[" + ShippingCompany + "]") == -1)
            {


                AccountProduct model = new AccountProduct();
                model.product_id = int.Parse(dr["product_serial_no"].ToString());
                model.shipping_company_id = int.Parse(dr["shipping_company"].ToString());
                model.price = decimal.Parse(dr["price"].ToString());
                model.sum = count;
                _cost += decimal.Parse(dr["cost"].ToString());

                // 信用卡，涨价
                if (IsCard == 1)
                {
                    //model.price *=  Config.is_card_rate; 
                }

                if (dr["product_serial_no"].ToString().Length == 8)
                {
                    model.product_cate = dr["is_noebook"].ToString() == "1" ? product_category.noebooks : product_category.system_product;
                    model.product_size = AccountHelper.GetSystemSize(model.price, product_category.system_product);
                }
                else if (dr["is_noebook"].ToString() == "1")
                {
                    model.product_cate = product_category.noebooks;
                    model.product_size = AccountHelper.GetSystemSize(model.price, product_category.noebooks);
                }
                else
                {

                    var pm = ProductModel.GetProductModel(context, int.Parse(dr["product_serial_no"].ToString()));
                    model.product_cate = product_category.part_product;
                    model.product_size = pm.product_size_id.Value;
                }


                ap[ap_count] = model;
                ap_count += 1;
                // for (int j = 0; j < int.Parse(dr["cart_temp_Quantity"].ToString()); j++)
                //{}
            }
            else
            {
                decimal sale_promotion_shipping_charge_single = 0M;

                DataTable shippingCompDt = Config.ExecuteDataTable(string.Format(@"select system_category from tb_shipping_company where shipping_company_id='{0}'", ShippingCompany));
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
                sale_promotion_charge += sale_promotion_shipping_charge_single * count;
            }
        }


        var state = StateShippingModel.GetStateShippingModel(context, _state_shipping);
        // 有个洲经销商税免8%
        PstRate = 0M;
        GstRate = 0M;
        HstRate = 0M;

        if (tax_execmtion.Length > 0 && _state_shipping == Config.tax_execmtion_state)
        {

            if ((state.gst + state.pst) > Config.tax_execmtion_state_save_money)
            {
                sale_tax_rate = (int)(state.gst + state.pst - Config.tax_execmtion_state_save_money);

                if (Config.tax_hsts.IndexOf("[" + state.state_serial_no + "]") == -1)
                {
                    GstRate = decimal.Parse(sale_tax_rate.ToString());
                    PstRate = 0;
                }
                else
                {
                    HstRate = decimal.Parse(sale_tax_rate.ToString());
                }
            }
        }
        else
        {
            sale_tax_rate = (int)(state.gst + state.pst);
            if (Config.tax_hsts.IndexOf("[" + state.state_serial_no + "]") == -1)
            {
                GstRate = decimal.Parse(state.gst.ToString());
                PstRate = decimal.Parse(state.pst.ToString());
            }
            else
            {
                HstRate = decimal.Parse(sale_tax_rate.ToString());
            }
        }
        //
        // if shipping country is US, set tax rate 0%.
        //
        if (state.system_category_serial_no == CountryCategoryHelper.CountryCategory_value(CountryCategory.US))
        {
            this.lbl_state_tax.Text = "0%";
        }
        else
            this.lbl_state_tax.Text = (sale_tax_rate).ToString("##") + "%";

        // 费用计算
        try
        {
            if (ap_real_count > 0)
            {
                var a = new Account(context, ap, _state_shipping);
                result = a.getResult() + sale_promotion_charge;

            }
            else
                result = sale_promotion_charge;

        }
        catch
        {
            this.lbl_shipping_charge.Text = error_msg;
            this.lbl_sales_tax.Text = error_msg;
            this.lbl_total.Text = error_msg;
            result = 0;
            _sale_tax = 0;
        }
        //
        //  
        //        
        if (Config.pay_method_use_card_rate.IndexOf("[" + this.Pay_method.ToString() + "]") != -1 || this.Pay_method == -1)
        {
            _sale_tax = (_price_sum_rate + result) * (sale_tax_rate) / 100;
        }
        else
        {
            _sale_tax = (ConvertPrice.ChangePriceToNotCard(_price_sum_rate) + result) * (sale_tax_rate) / 100;
        }


        this.lbl_shipping_charge.Text = result.ToString("$###,##0.00");//== 0 ? error_msg : result.ToString("$###,###.00");
        //
        // if shipping country is US, set tax rate 0%.
        //
        if (state.system_category_serial_no == CountryCategoryHelper.CountryCategory_value(CountryCategory.US))
        {
            this.lbl_sales_tax.Text = "0.00";
            _sale_tax = 0;
        }
        else
            this.lbl_sales_tax.Text = _sale_tax == 0 ? error_msg : _sale_tax.ToString("$###,###.00");
        this.lbl_total.Text = (_price_sum_rate + result + _sale_tax).ToString("$###,###.00");

        this.lbl_sub_total.Text = _price_sum_rate.ToString("$###,###.00");
        //this.lbl_state_tax.Text = (_price_sum_rate).ToString();
        WriteInputValue(_price_sum, _sale_tax, result, _price_sum_rate, decimal.Parse(sale_tax_rate.ToString()), _cost);

    }

    public int TmpCode
    {
        get
        {
            return Util.GetInt32SafeFromQueryString(Page, "tmp_code", -1);
        }
    }

    public int SetParentPrice
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "SavePrice", -1); }
    }

    public int CountryID
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "country_id", -1); }
    }

    public int StateShipping
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "sate_shipping", -1); }
    }

    public int ShippingCompany
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "shipping_company", -1); }
    }

    public string tax_execmtion
    {
        get { return Util.GetStringSafeFromQueryString(Page, "tax_execmtion"); }
    }
    public int Pay_method
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "Pay_method", -1); }
    }

    public bool RedirectCheckOut
    {
        get { return 1 == Util.GetInt32SafeFromQueryString(Page, "RedirectCheckOut", 0); }
    }

    /// <summary>     
    /// 是否是信用卡， 1 信用卡结帐， 0 非信用卡结帐
    /// </summary>
    public int IsCard
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "is_card", 0); }
    }

    private void UpdateStateCompanyCountry(LU.Data.nicklu2Entities context)
    {

        try
        {
            var cart = CartTempModel.GetModelsByTmeCode(context, TmpCode);
            for (int i = 0; i < cart.Length; i++)
            {
                //if( ShippingCompany!= -1)
                cart[i].shipping_company = ShippingCompany;
                // if(StateShipping != -1)
                cart[i].state_shipping = StateShipping;
                //  if(CountryID != -1)
                cart[i].country_id = CountryID;
                context.SaveChanges();

                //Response.Write(ShippingCompany.ToString() + StateShipping.ToString() + CountryID.ToString());
            }
        }
        catch (Exception ex) { throw ex; }
    }

    /// <summary>
    /// 如果是在Ontario 洲．　自取，　将不需要运输公司这个值
    /// </summary>
    private void UpdateCompanyCountry(LU.Data.nicklu2Entities context)
    {
        
        if (Config.pay_method_pick_up_ids.IndexOf("[" + this.Pay_method.ToString() + "]") != -1 && this.StateShipping == Config.tax_execmtion_state)
        {
           var cart = CartTempModel.GetModelsByTmeCode(context, TmpCode);
            for (int i = 0; i < cart.Length; i++)
            {
                cart[i].shipping_company = -1;
                context.SaveChanges();
            }
        }
        // Response.Write(this.Pay_method.ToString() + Config.pay_method_pick_up_id.ToString() + this.StateShipping.ToString() + Config.tax_execmtion_state.ToString());
    }
    #region properties
    public decimal Gst
    {
        get { return (decimal)ViewState["Gst"]; }
        set { ViewState["Gst"] = value; }
    }
    public decimal Pst
    {
        get { return (decimal)ViewState["Pst"]; }
        set { ViewState["Pst"] = value; }
    }
    public decimal Hst
    {
        get { return (decimal)ViewState["Hst"]; }
        set { ViewState["Hst"] = value; }
    }
    public decimal GstRate
    {
        get { return (decimal)ViewState["GstRate"]; }
        set { ViewState["GstRate"] = value; }
    }
    public decimal PstRate
    {
        get { return (decimal)ViewState["PstRate"]; }
        set { ViewState["PstRate"] = value; }
    }
    public decimal HstRate
    {
        get { return (decimal)ViewState["HstRate"]; }
        set { ViewState["HstRate"] = value; }
    }
    #endregion
}



