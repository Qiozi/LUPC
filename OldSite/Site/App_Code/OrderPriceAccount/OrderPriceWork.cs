using LU.Data;
using System;
using System.Data;

/// <summary>
/// Summary description for OrderAccount
/// </summary>
public class OrderPriceWork
{
    LU.Data.nicklu2Entities _context;
    tb_order_helper OH = null;
    tb_customer_store CS = null;

    string _OrderCode = "";

    public OrderPriceWork(nicklu2Entities context, int OrderCode)
    {
        _context = context;
        _OrderCode = OrderCode.ToString();

        OH = OrderHelperModel.GetModelByOrderCode(_context, OrderCode);
        CS = CustomerStoreModel.FindByOrderCode(_context, OrderCode.ToString());

        //
        // TODO: Add constructor logic here
        //
    }

    public decimal special_cash_discount_recommend
    {
        get;
        set;
    }

    public decimal ship_charge_recommend
    {
        get;
        set;
    }

    #region 属性
    /// <summary>
    /// 洲ID
    /// </summary>
    int StateId { get; set; }

    #endregion

    public void AccountOrder(string order_code)
    {
        AccountOrder(order_code, true);
    }

    public void AccountOrder()
    {
        AccountOrder(_OrderCode, true);
    }


    void InitTax(tb_state_shipping state)
    {
        if ((OH.is_lock_tax_change.HasValue && !OH.is_lock_tax_change.Value) && !OH.is_lock_tax_change.HasValue)
        {
            OH.hst_rate = 0M;
            OH.gst_rate = 0M;
            OH.pst_rate = 0M;
            if (Config.tax_hsts.IndexOf("[" + state.state_serial_no.ToString() + "]") > -1)
            {
                OH.hst_rate = state.gst + state.pst;
            }
            else
            {
                OH.gst_rate = state.gst;
                OH.pst_rate = state.pst;
            }
            //throw new Exception(string.Format("{0}||{1}||{2}||{3}", state.gst, state.pst, OH.hst_rate, state.state_code));
        }
    }

    public void AccountOrder(string order_code, bool is_save)
    {

        string error = "";

        DataTable partDT = OrderProductModel.GetModelsBySearch(order_code, product_category.part_product);
        DataTable noebookDT = OrderProductModel.GetModelsBySearch(order_code, product_category.noebooks);
        DataTable systemDT = OrderProductModel.GetModelsBySearch(order_code, product_category.system_product);

        int part_count = partDT.Rows.Count;
        int noebook_count = noebookDT.Rows.Count;
        int system_count = systemDT.Rows.Count;

        int count = 0;
        int sum_count = OrderProductModel.GetPartSum(order_code);
        decimal part_sum = 0;
        decimal noebook_sum = 0;
        decimal system_sum = 0;
        decimal _cost = 0;
        decimal sale_promotion_charge = 0M;

        //
        // 得到洲ID
        int stateid = CS.customer_shipping_state ?? 0;
        if (stateid < 1)
        {
            stateid = StateShippingModel.FindStatIDByCode(_context, CS.shipping_state_code);
            if (stateid > 1)
            {
                CS.customer_shipping_state = stateid;
                _context.SaveChanges();
            }
        }

        if (sum_count == -1) return;
        if (sum_count == 0)
        {
            OH.sub_total = 0M;
            OH.tax_charge = 0M;
            OH.gst_rate = 0M;
            OH.gst = 0M;
            OH.pst = 0M;
            OH.pst_rate = 0M;
            OH.hst = 0M;
            OH.hst_rate = 0M;
            OH.weee_charge = 0M;
            OH.shipping_charge = 0M;
            OH.sub_total_rate = 0M;
            OH.sur_charge = 0M;
            OH.sur_charge_rate = 0M;
            OH.grand_total = 0M;
        }

        //
        // 判断有几个是促 销运费的产品
        //
        int sale_promotion_count = Config.ExecuteScalarInt32(string.Format(@"select count(product_serial_no)  from (select product_serial_no  from tb_order_product op where order_code='{0}')  c  inner join tb_product_shipping_fee ps 
on ps.prod_sku=c.product_serial_no and ps.is_system=0", order_code));

        int ap_real_count = sum_count - (Config.sale_promotion_compay_id.IndexOf("[" + OH.shipping_company + "]") == -1 ? 0 : sale_promotion_count);


        AccountProduct[] aps = new AccountProduct[ap_real_count];
        DataTable shippingCompDt = Config.ExecuteDataTable(string.Format(@"select system_category from tb_shipping_company where shipping_company_id='{0}'", OH.shipping_company));



        //  -----------------------------------------------------------------------------------------------------
        // part
        //  -----------------------------------------------------------------------------------------------------
        #region part
        for (int i = 0; i < partDT.Rows.Count; i++)
        {
            DataRow dr = partDT.Rows[i];
            int pCount;
            int.TryParse(dr["order_product_sum"].ToString(), out pCount);

            for (int x = 0; x < pCount; x++)
            {
                DataTable sales_promotion_dt = Config.ExecuteDataTable(string.Format(@"
select 	prod_shipping_fee_id, prod_Sku, is_system, shipping_fee_us, shipping_fee_ca, regdate
	 
	from 
	tb_product_shipping_fee 
	where prod_sku='{0}' and is_system='{1}'", dr["product_serial_no"].ToString(), 0));

                int product_id = int.Parse(dr["product_serial_no"].ToString());
                var p = ProductModel.GetProductModel(_context, product_id);

                _cost += p.product_current_cost.Value;
                part_sum += decimal.Parse(dr["order_product_sold"].ToString());


                //
                //  是否特殊运费
                //
                if (sales_promotion_dt.Rows.Count == 0 || Config.sale_promotion_compay_id.IndexOf("[" + OH.shipping_company + "]") == -1)
                {

                    AccountProduct model = new AccountProduct();

                    model.price = decimal.Parse(dr["order_product_sold"].ToString());
                    model.product_size = p.product_size_id.Value;
                    model.shipping_company_id = OH.shipping_company.Value;
                    model.product_id = product_id;
                    model.product_cate = product_category.part_product;
                    model.sum = 1;


                    aps[count] = model;
                    count += 1;
                }
                else
                {
                    decimal sale_promotion_shipping_charge_single = 0M;

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
                    sale_promotion_charge += sale_promotion_shipping_charge_single;
                }
            }
        }
        #endregion

        //CH.Alert(string.Format("{0}|{1}", part_sum, product_sum, this.lv_sys_list);
        //  -----------------------------------------------------------------------------------------------------
        // noebook
        //  -----------------------------------------------------------------------------------------------------
        #region Notebook
        for (int i = 0; i < noebookDT.Rows.Count; i++)
        {
            DataRow dr = noebookDT.Rows[i];
            decimal product_sum = decimal.Parse(dr["order_product_sum"].ToString());

            for (int x = 0; x < product_sum; x++)
            {
                int product_id = int.Parse(dr["product_serial_no"].ToString());
                var p = ProductModel.GetProductModel(_context, product_id);
                _cost += p.product_current_cost.Value * 1;

                noebook_sum += decimal.Parse(dr["order_product_sold"].ToString()) * 1;

                DataTable sales_promotion_dt = Config.ExecuteDataTable(string.Format(@"
select 	prod_shipping_fee_id, prod_Sku, is_system, shipping_fee_us, shipping_fee_ca, regdate
	 
	from 
	tb_product_shipping_fee 
	where prod_sku='{0}' and is_system='{1}'", product_id, 0));
                //
                //  是否特殊运费
                //
                if (sales_promotion_dt.Rows.Count == 0 || Config.sale_promotion_compay_id.IndexOf("[" + OH.shipping_company + "]") == -1)
                {
                    AccountProduct model = new AccountProduct();

                    model.price = decimal.Parse(dr["order_product_sold"].ToString());
                    model.product_size = AccountHelper.GetSystemSize(p.product_current_price.Value, product_category.noebooks); ;
                    model.shipping_company_id = OH.shipping_company.Value;
                    model.product_id = product_id;
                    model.product_cate = product_category.noebooks;
                    model.sum = 1;

                    //_cost += p.product_current_cost * product_sum;
                    //part_sum += decimal.Parse(product_id.ToString()) * product_sum;

                    aps[count] = model;
                    count += 1;
                }
                else
                {
                    decimal sale_promotion_shipping_charge_single = 0M;

                    //DataTable shippingCompDt = Config.ExecuteDataTable(string.Format(@"select system_category from tb_shipping_company where shipping_company_id='{0}'", ShippingCompanyID));
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
                    sale_promotion_charge += sale_promotion_shipping_charge_single;
                }
            }
        }
        #endregion

        //  -----------------------------------------------------------------------------------------------------
        // system
        //  -----------------------------------------------------------------------------------------------------

        #region System
        for (int i = 0; i < systemDT.Rows.Count; i++)
        {
            DataRow dr = systemDT.Rows[i];
            decimal product_sum = decimal.Parse(dr["order_product_sum"].ToString());


            AccountProduct model = new AccountProduct();
            int product_id = int.Parse(dr["product_serial_no"].ToString());
            //ProductModel p = ProductModel.GetProductModel(product_id);

            model.price = decimal.Parse(dr["order_product_sold"].ToString()); //SpDetailModel.GetPriceSUM(product_id);

            model.product_size = AccountHelper.GetSystemSize(model.price, product_category.system_product);
            model.shipping_company_id = OH.shipping_company.Value;
            model.product_id = product_id;
            model.product_cate = product_category.system_product;
            model.sum = int.Parse(dr["order_product_sum"].ToString());
            aps[count] = model;

            system_sum += model.price * product_sum;
            _cost += SpTmpDetailModel.GetPriceSUM(product_id) * product_sum;
            count += 1;

        }
        #endregion

        //
        //
        // charge 
        string _charge_result = "";
        decimal _result = 0;

        try
        {
            if (OH.order_source != 3)
            {
                if (ap_real_count > 0 && !(OH.is_lock_shipping_charge ?? false))
                {

                    if (OH.shipping_company != -1)
                    {
                        //throw new Exception(aps.Length.ToString());
                        var ac = new Account(_context, aps, stateid);

                        _result = ac.getResult() + sale_promotion_charge;
                        _result = ConvertPrice.Price(OH.price_unit.ToLower() != "USD".ToLower() ? CountryCategory.CA : CountryCategory.US, _result);
                        _charge_result = Config.ConvertPrice2(_result);
                    }
                    else
                    {
                        _result = 0;
                        _charge_result = "0";
                    }
                }
                else
                {

                    _result = sale_promotion_charge;
                    _charge_result = Config.ConvertPrice2(_result);
                }
            }
            else
            {
                _result = OH.shipping_charge.Value;
                _charge_result = Config.ConvertPrice2(_result);
            }
        }
        catch (Exception ex)
        {
            _result = 0;
            error = ex.Message;
            throw ex;
        }

        //sub total 
        decimal _price_sum = 0;
        _price_sum = part_sum + noebook_sum + system_sum;

        //CH.Alert(string.Format("{0}|{1}|{2}", part_sum, noebook_sum, system_sum), this.lv_sys_list);
        //return;
        // this.lbl_sub_total.UpdateAfterCallBack = true;

        // special cash discount
        decimal special_cash_discount;

        if (Config.pay_method_use_card_rate.IndexOf("[" + OH.pay_method + "]") == -1)
        {
            special_cash_discount = ConvertPrice.SpecialCashPriceDiscount(_price_sum);
        }
        else
            special_cash_discount = 0M;

        // tax
        var state = StateShippingModel.GetStateShippingModel(_context, stateid);

        InitTax(state);

        decimal _sale_tax = 0;
        int tax_rate = 0;

        //AnthemHelper.Alert(tax_execmtion.ToString() + this.StateID .ToString());
        // 有个洲经销商税免8%

        if (CS.is_all_tax_execmtion != true)
        {
            if ((CS.tax_execmtion ?? "").Trim().Length > 2)// && stateid == Config.tax_execmtion_state)
            {
                if ((state.gst + state.pst) > Config.tax_execmtion_state_save_money)
                {
                    tax_rate = (int)(state.gst + state.pst - Config.tax_execmtion_state_save_money);
                    if (Config.pay_method_use_card_rate.IndexOf("[" + OH.pay_method + "]") != -1)
                        _sale_tax = (_price_sum + _result) * (tax_rate) / 100;
                    else
                        _sale_tax = (_price_sum + _result - special_cash_discount) * (tax_rate) / 100;
                }
            }
            else
            {
                tax_rate = (int)(OH.pst_rate ?? 0 + OH.gst_rate ?? 0 + OH.hst_rate ?? 0);
                if (Config.pay_method_use_card_rate.IndexOf("[" + OH.pay_method + "]") != -1)
                    _sale_tax = (_price_sum + _result) * tax_rate / 100;
                else
                    _sale_tax = (_price_sum + _result - special_cash_discount) * tax_rate / 100;
            }
        }
        else
        {

        }
        //Response.Write(_sale_tax.ToString() + "<br>" + _price_sum.ToString() + " <br>" + _result.ToString() + "<br>" + special_cash_discount.ToString());
        //_sale_tax = decimal.Parse((_state_shipping).ToString());

        //
        // total 
        // 
        bool is_lock_input_order_discount = false;
        bool is_lock_shipping_charge = false;
        decimal input_order_discount = 0M;
        decimal shipping_charge = 0M;
        decimal weee_charge = 0M;

        //        DataTable dt = Config.ExecuteDataTable(@"select is_lock_input_order_discount, input_order_discount, shipping_charge, is_lock_shipping_charge
        //        ,weee_charge from tb_order_helper where order_code='" + order_code + "'");
        if (OH != null)
        {
            //DataRow dr = dt.Rows[0];
            is_lock_input_order_discount = OH.is_lock_input_order_discount ?? false;
            is_lock_shipping_charge = OH.is_lock_shipping_charge ?? false;

            if (is_lock_input_order_discount == true)
                input_order_discount = OH.input_order_discount ?? 0M;
            if (is_lock_shipping_charge)
                shipping_charge = OH.shipping_charge ?? 0M;
            else
                shipping_charge = _result;

            weee_charge = OH.weee_charge ?? 0M;

        }
        decimal _total;
        if (!is_lock_shipping_charge)
        {
            _total = _result + _sale_tax + _price_sum;
        }
        else
        {
            _total = shipping_charge + _sale_tax + _price_sum;
            _result = shipping_charge;
        }

        // 非信用卡
        if (Config.pay_method_use_card_rate.IndexOf("[" + OH.pay_method + "]") == -1)
        {
            _total = _total - (is_lock_input_order_discount ? input_order_discount : special_cash_discount);
            if (is_lock_input_order_discount)
            {
                special_cash_discount_recommend = ConvertPrice.RoundPrice(special_cash_discount);
            }
            else
            {
                input_order_discount = special_cash_discount;
                special_cash_discount_recommend = 0M;
            }
        }
        // 信用卡
        else
        {
            _total = _total - (is_lock_input_order_discount ? input_order_discount : 0);
            special_cash_discount_recommend = 0M;
        }


        if (is_lock_shipping_charge)
        {
            ship_charge_recommend = ConvertPrice.RoundPrice(_result);

            // 输出到界面后， 把运费传输入保存到数据库
            _result = shipping_charge;
        }

        SaveToDB(stateid, OH.shipping_company.Value, _price_sum, _total, _result, _cost, _sale_tax
            , tax_rate, int.Parse(order_code), input_order_discount, weee_charge);

    }

    public void SaveToDB(int state_shipping, int shipping_company, decimal sub_total, decimal total,
        decimal shipping_charge, decimal _cost, decimal _sale_tax
        , int _tax_rate, int order_code, decimal input_order_discount
        , decimal weee_charge)
    {
        try
        {
            var state = StateShippingModel.GetStateShippingModel(_context, state_shipping);
            OH.sub_total = sub_total;


            // 有个洲经销商税免8%
            int sale_tax_rate = 0;

            //CH.Alert(Tax_execmtion, this.lv_sys_list);
            if (CS.is_all_tax_execmtion == true)
            {
                OH.gst_rate = 0M;
                OH.pst_rate = 0M;
                OH.hst_rate = 0M;
            }
            else
            {
                if ((CS.tax_execmtion ?? "").Trim().Length > 2)
                {
                    if ((state.gst + state.pst) > Config.tax_execmtion_state_save_money)
                    {
                        sale_tax_rate = (int)(state.gst + state.pst - Config.tax_execmtion_state_save_money);

                        if (Config.tax_hsts.IndexOf("[" + state.state_serial_no + "]") == -1)
                        {
                            OH.gst_rate = decimal.Parse(state.gst.ToString());
                            OH.hst_rate = 0M;
                            OH.pst_rate = 0M;
                        }
                        //else
                        //{
                        //    OH.hst_rate = decimal.Parse(sale_tax_rate.ToString());
                        //    OH.gst_rate = 0M;
                        //    OH.pst_rate = 0M;
                        //}
                    }
                }
                //else
                //{
                //    sale_tax_rate = (int)(state.gst + state.pst);
                //    if (Config.tax_hsts.IndexOf("[" + state.state_serial_no + "]") == -1)
                //    {
                //        OH.gst_rate = decimal.Parse(state.gst.ToString());
                //        OH.pst_rate = decimal.Parse(state.pst.ToString());
                //        OH.hst_rate = 0M;
                //    }
                //    else
                //    {
                //        OH.hst_rate = decimal.Parse(sale_tax_rate.ToString());
                //        OH.gst_rate = 0M;
                //        OH.pst_rate = 0M;
                //    }
                //}
            }
            OH.shipping_charge = shipping_charge;

            decimal _sub_charge = sub_total + shipping_charge - input_order_discount;
            //decimal _gst = (_sub_charge) * state.gst / 100;
            //decimal _pst = (_sub_charge) * state.pst / 100;
            //OH.gst = 0M;
            //OH.pst = 0M;
            //OH.hst = 0M;
            OH.gst = (_sub_charge) * (OH.gst_rate ?? 0) / 100;
            OH.pst = (_sub_charge) * (OH.pst_rate ?? 0) / 100;
            OH.hst = (_sub_charge) * (OH.hst_rate ?? 0) / 100;

            //if (OH.gst_rate > 0M)
            //{
            //    OH.gst = _gst;
            //}
            //if (OH.pst_rate > 0M)
            //{
            //    OH.pst = _pst;
            //}
            //if (OH.hst_rate > 0M)
            //{
            //    OH.hst = _sub_charge * OH.hst_rate / 100;// _gst + _pst;
            //}

            OH.input_order_discount = input_order_discount;
            OH.total = _sub_charge + OH.gst + OH.pst + OH.hst;
            //
            // 因为税分开，四舍五入后，会有一分钱的误差。
            _sale_tax = OH.gst.Value + OH.pst.Value + OH.hst.Value;
            OH.shipping_company = shipping_company;
            OH.cost = _cost;
            OH.taxable_total = _sub_charge;
            OH.tax_rate = _tax_rate;
            OH.tax_charge = OH.gst + OH.pst + OH.hst + weee_charge;
            OH.sur_charge = ConvertPrice.SpecialCashPriceDiscount(sub_total);
            OH.grand_total = OH.taxable_total + OH.tax_charge;
            OH.sub_total_rate = Config.is_card_rate - 1;
            OH.sub_total_rate = sub_total;
            OH.Is_Modify = true;
            //  OH.Update();
            //  _context.SaveChanges();

            CS.state_serial_no = state_shipping;
            CS.customer_shipping_state = state_shipping;
            // CS.Update();
            _context.SaveChanges();
        }
        catch
        {
            throw;
        }
    }
}