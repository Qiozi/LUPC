//
//
//  history : part 第二件为15%
//              noebook，SYSTEM, 第二件为３０％
//         date:    2008-1-3

//         date:    2008-1-4
//              
//          取消大产品型号多加30元
//          取消超过产品价格超过500元,另再加10元

//          date : 2012-3-24

//          UPS 运费 凡UPS 3 days， 系统都要增加30， 笔记本增加 20
//  13515993791
//


using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using LU.Data;

/// <summary>
/// Account 的摘要说明
/// </summary>
public class Account
{
    AccountProduct[] _accountProduct;
    int _part_count = 0;
    int _part_count_sum = 0;
    int _system_count = 0;
    int _noebook_count = 0;
    int _part_top_price_ap_id = 0; // part 产品的最高价格
    int _system_top_price_ap_id = 0; // system 产品的最高价格
    int _noebook_top_price_ap_id = 0;// noebook产品的最高价格
    decimal charge = 0;
    int _state_shipping = -1;
    
    decimal _part_charge_sum = 0;
    decimal _noebook_charge_sum = 0;
    decimal _system_charge_sum = 0;

    decimal _part_max_charge = 0;
    decimal _noebook_max_charge = 0;
    decimal _system_max_charge = 0;
    decimal _price_total = 0;

    // 第二件运费的百分比
    decimal _PART_SECOND_RATE = 0.15M;
    decimal _NOEBOOK_SECOND_RATE = 0.3M;
    decimal _SYSTEM_SECOND_RATE = 0.3M;


    int UPS_3Day = 5;
    int _shiping_company = -1; //运输公司

    LU.Data.nicklu2Entities _context;
    //decimal _sales_promotion_shipping_charge_sum = 0M;

    public Account(nicklu2Entities context, AccountProduct[] ap, int state_shipping)
    {
        _context = context;
        //
        // TODO: 在此处添加构造函数逻辑
        //
        _state_shipping = state_shipping;
        accountProduct = ap;
        for (int i = 0; i < ap.Length; i++)
        {
            if (accountProduct[i] == null)
                continue;

            accountProduct[i].ap_id = i;
            // 取得每次产品运费
            AccountPart(accountProduct[i]);
            // 运输公司
            _shiping_company = accountProduct[i].shipping_company_id;
            // 总价格
            _price_total += accountProduct[i].price * accountProduct[i].sum;

            if (accountProduct[i].product_cate == product_category.noebooks)
            {
                _noebook_count += 1;
                //   取得最大价格
                if (accountProduct[i].charge > _noebook_max_charge)
                {
                    _noebook_max_charge = accountProduct[i].charge;
                    _noebook_top_price_ap_id = accountProduct[i].ap_id;
                }
            }
            if (accountProduct[i].product_cate == product_category.part_product)
            {
                _part_count += 1;
                //   取得最大价格
                if (accountProduct[i].charge > _part_max_charge)
                {
                    _part_max_charge = accountProduct[i].charge;
                    _part_top_price_ap_id = accountProduct[i].ap_id;
                }
            }
            if (accountProduct[i].product_cate == product_category.system_product)
            {
                _system_count += 1;
                //   取得最大价格
                if (accountProduct[i].charge > _system_max_charge)
                {
                    _system_max_charge = accountProduct[i].charge;
                    _system_top_price_ap_id = accountProduct[i].ap_id;
                }
            }

        } 
        stat();
	}

    public decimal getResult()
    {

        // 排序多出计算最大的价格
        decimal max_charge = 0M;
        decimal dy_charge = 0M;


        if (_noebook_max_charge > max_charge)
        {
            max_charge = _noebook_max_charge;
            dy_charge = _part_max_charge * 0.85M + _system_max_charge * 0.7M;
        }
        if (_system_max_charge > max_charge)
        {
            max_charge = _system_max_charge;
            dy_charge = _part_max_charge * 0.85M + _noebook_max_charge * 0.7M;
        }
        if (_part_max_charge > max_charge)
        {
            max_charge = _part_max_charge;
            dy_charge = _system_max_charge * 0.7M + _noebook_max_charge * 0.7M;
        }

        // return dy_charge;

        // ground 运输公司， 超过900，报错
        if (_price_total >= Config.GroundCompanyTotal && _shiping_company == Config.GroundCompanyID)
        {
            throw new Exception("未找到配置价格.ground 运输公司， 超过900");
        }


        if ((Config.army_shipping_company == _shiping_company &&  Config.army_state.IndexOf( _state_shipping .ToString("[0]")) == -1) || 
            (Config.army_shipping_company != _shiping_company &&  Config.army_state.IndexOf(_state_shipping.ToString("[0]")) != -1))
        {
            throw new Exception("未找到配置价格..");
        }

        //
        // 有特殊运费
        //if (_sales_promotion_shipping_charge_sum != 0M)
        //{
        //    // 多扣除的运费
        //    decimal error_blanace;
        //    if (_sales_promotion_shipping_charge_sum == max_charge)
        //        error_blanace = 0;
        //    else
        //        error_blanace = _sales_promotion_shipping_charge_sum / (1M - 0.7M);

        //    error_blanace = 0;
        //    StateShippingModel model = StateShippingModel.GetStateShippingModel(_state_shipping);
        //    if (decimal.Parse(model.state_shipping.ToString()) / 100 == 0)
        //        return charge - dy_charge + error_blanace;
        //    return (charge - dy_charge) * decimal.Parse(model.state_shipping.ToString()) / 100 + error_blanace * (1M- decimal.Parse(model.state_shipping.ToString()) / 100);

        //}
        //else
        //{

            var model = StateShippingModel.GetStateShippingModel(_context , _state_shipping);
            if (decimal.Parse(model.state_shipping.ToString()) / 100 == 0)
                return charge - dy_charge;
            return (charge - dy_charge) * decimal.Parse(model.state_shipping.ToString()) / 100;
        //}
    }

    private void stat()
    {
        // system  product
        if (_system_count > 0)
        {
            if (_system_count == 1)
            {
                //charge += accountProduct[_system_top_price_ap_id].charge;
                _system_charge_sum = accountProduct[_system_top_price_ap_id].charge;
                for (int i = 1; i < accountProduct[_system_top_price_ap_id].sum; i++)
                {
                    _system_charge_sum += accountProduct[_system_top_price_ap_id].charge * _SYSTEM_SECOND_RATE;
                }
                if (_shiping_company == UPS_3Day)
                    _system_charge_sum += 30M;
            }
            else
            {
                // 最大的费用
                //charge += _system_max_price;
                _system_charge_sum = _system_max_charge;

                if (_shiping_company == UPS_3Day)
                    _system_charge_sum += 30M;

               // for (int i = 0; i < accountProduct.Length; i++)
              //  {
                    //if (accountProduct[i].product_cate == product_category.system_product && accountProduct[i].ap_id != _system_top_price_ap_id)
                    //{
                    //    // 子产品取一半
                    //    charge += accountProduct[i].charge / 2;
                              
                    //}
                    for (int j = 0; j < accountProduct.Length; j++)
                    {
                        if (accountProduct[j].product_cate == product_category.system_product)
                        {
                            if (accountProduct[j].ap_id == _system_top_price_ap_id)
                            {
                                // 因为最大价格已取得
                                for (int x = 1; x < accountProduct[j].sum; x++)
                                {
                                    _system_charge_sum += accountProduct[j].charge * _SYSTEM_SECOND_RATE;
                                    if (_shiping_company == UPS_3Day)
                                        _system_charge_sum += 30M;
                                }
                            }
                            else
                            {
                                for (int x = 0; x < accountProduct[j].sum; x++)
                                {
                                    _system_charge_sum += accountProduct[j].charge * _SYSTEM_SECOND_RATE;
                                    if (x > 0)
                                    {
                                        if (_shiping_company == UPS_3Day)
                                            _system_charge_sum += 30M;
                                    }
                                }
                               
                            }

                        }
                    }
               // }
            }
            
          
        }

        // noebooks 
        if(_noebook_count > 0)
        {
            if (_noebook_count == 1)
            {
                // 最大费用
                // charge += accountProduct[_noebook_top_price_ap_id].charge;
                _noebook_charge_sum += accountProduct[_noebook_top_price_ap_id].charge;
                for (int i = 1; i < accountProduct[_noebook_top_price_ap_id].sum; i++)
                {
                    _noebook_charge_sum += accountProduct[_noebook_top_price_ap_id].charge * _NOEBOOK_SECOND_RATE;
                }
                if (_shiping_company == UPS_3Day)
                    _noebook_charge_sum += 20M;
            }
            else
            {
                // 最大费用
                //charge += accountProduct[_noebook_top_price_ap_id].charge;
                _noebook_charge_sum += accountProduct[_noebook_top_price_ap_id].charge;
                if (_shiping_company == UPS_3Day)
                    _noebook_charge_sum += 20M;
                for (int i = 0; i < accountProduct.Length; i++)
                {
                    //if (accountProduct[i].product_cate == product_category.noebooks && accountProduct[i].ap_id !=_noebook_top_price_ap_id)
                    //{
                    //    charge += accountProduct[i].charge / 2;

                    //}

                    if (accountProduct[i].product_cate == product_category.noebooks)
                    {
                        if (accountProduct[i].ap_id == _noebook_top_price_ap_id)
                        {
                            for (int j = 1; j < accountProduct[i].sum; j++)
                            {
                                _noebook_charge_sum += accountProduct[j].charge * _NOEBOOK_SECOND_RATE;
                                if (_shiping_company == UPS_3Day)
                                    _noebook_charge_sum += 20M;
                            }
                           
                        }
                        else
                        {
                            for (int j = 0; j < accountProduct[i].sum; j++)
                            {
                                _noebook_charge_sum += accountProduct[j].charge * _NOEBOOK_SECOND_RATE;
                                if (j > 0)
                                {
                                    if (_shiping_company == UPS_3Day)
                                        _noebook_charge_sum += 20M;
                                }
                            }
                        }
                    }

                }
            }
            //_noebook_charge_sum = 50M;
        }

        // part product
        if (_part_count > 0 )
        {
            if (_part_count == 1)
            {
                // 最大费用
                //   charge += accountProduct[_part_top_price_ap_id].charge;
                //  如果同时买系统，半价运费 
                //if (_system_count == 0)
                //    _part_charge_sum = _part_max_charge;
                //else
                //    _part_charge_sum = _part_max_charge * _PART_SECOND_RATE;
                _part_charge_sum = _part_max_charge;
                for (int i = 1; i < accountProduct[_part_top_price_ap_id].sum; i++)
                {
                    _part_charge_sum += _part_max_charge * _PART_SECOND_RATE;
                }
            }
            else
            {
                // 最大费用
               // charge += accountProduct[_part_top_price_ap_id].charge;
                _part_charge_sum = _part_max_charge;
                for (int i = 0; i < accountProduct.Length; i++)
                {
                    //if (accountProduct[i].product_cate == product_category.part_product && accountProduct[i].ap_id != _part_top_price_ap_id)
                    //{
                    //    charge += accountProduct[i].charge / 2;
                    //}
                    //_part_price_sum += accountProduct[i].price;
                    if (accountProduct[i].product_cate == product_category.part_product)
                    {
                        if (accountProduct[i].ap_id == _part_top_price_ap_id)
                        {
                            for (int j = 1; j < accountProduct[i].sum; j++)
                            {
                                _part_charge_sum += accountProduct[i].charge * _PART_SECOND_RATE;
                            }
                        }
                        else
                        {
                            for (int j = 0; j < accountProduct[i].sum; j++)
                            {
                                _part_charge_sum += accountProduct[i].charge * _PART_SECOND_RATE;
                            }
                        }
                        _part_count_sum += accountProduct[i].sum;
                    }
                    
                }
            }
              // 如果价格大于500，每件加10元运费
            //if (_part_charge_sum > 500)
            //{
            //    charge += _part_count_sum * 10;
            //}
        }

        charge = _part_charge_sum + _noebook_charge_sum + _system_charge_sum;
        //charge = 50M;
    }

    public AccountProduct[] accountProduct
    {
        get { return _accountProduct; }
        set { _accountProduct = value; }
    }

    private void AccountPart(AccountProduct ap)
    {
//        if (Config.sale_promotion_compay_id.IndexOf("[" + ap.shipping_company_id.ToString() + "]") != -1)
//        {
//            //
//            //
//            // 促销运费
//            DataTable sales_promotion_dt = Config.ExecuteDataTable(string.Format(@"
//select 	prod_shipping_fee_id, prod_Sku, is_system, shipping_fee_us, shipping_fee_ca, regdate
//	 
//	from 
//	tb_product_shipping_fee 
//	where prod_sku='{0}' and is_system='{1}'", ap.product_id, ap.product_cate == product_category.system_product ? 1 : 0));

//            if (sales_promotion_dt.Rows.Count > 0)
//            {
//                decimal charge_price;
//                DataTable shippingCompDt = Config.ExecuteDataTable(string.Format(@"select system_category from tb_shipping_company where shipping_company_id='{0}'", ap.shipping_company_id));
//                if (shippingCompDt.Rows.Count > 0)
//                {
//                    if (shippingCompDt.Rows[0][0].ToString() == "1")
//                    {
//                        // ca
//                        decimal.TryParse(sales_promotion_dt.Rows[0]["shipping_fee_ca"].ToString(), out charge_price);
//                    }
//                    else
//                    {
//                        // us
//                        decimal.TryParse(sales_promotion_dt.Rows[0]["shipping_fee_us"].ToString(), out charge_price);
//                    }
//                }
//                else
//                    throw new Exception("运输公司不正确");
//                ap.charge = charge_price;
//                _sales_promotion_shipping_charge_sum = ap.charge * ap.sum;
//                //throw new Exception(charge_price.ToString());
//                return;
//            }
//        }
//        else
        {
            DataTable dt = AccountModel.GetModelsBySizeAndCompanyProductCategory(ap.product_size, ap.shipping_company_id, Product_category_helper.product_category_value(ap.product_cate));


            if (dt.Rows.Count != 1)
                throw new Exception("未找到配置价格");

            if (dt.Rows[0]["charge"].ToString() == "0")
                throw new Exception("配置价格0");
            if (ap.product_cate == product_category.part_product)
            {

                // 大尺寸要加30元
                // 被撤消
                ap.charge = decimal.Parse(dt.Rows[0]["charge"].ToString()) + (ap.product_size == Config.product_size ? 0 : 0);

                //// 取得最大的价格
                //if (accountProduct[_part_top_price_ap_id].charge < ap.charge)
                //    _part_top_price_ap_id = ap.ap_id;

            }
            else if (ap.product_cate == product_category.system_product)
            {
                ap.charge = decimal.Parse(dt.Rows[0]["charge"].ToString());

                //if (accountProduct[_system_top_price_ap_id].charge < ap.charge)
                //    _system_top_price_ap_id = ap.ap_id;

            }
            else if (ap.product_cate == product_category.noebooks)
            {
                ap.charge = decimal.Parse(dt.Rows[0]["charge"].ToString());// +ap.price / 100;

                //if (accountProduct[_noebook_top_price_ap_id].charge < ap.charge)
                //    _noebook_top_price_ap_id = ap.ap_id;
            }
        }
    }
}

public class AccountProduct
{

    int _product_id;
    product_category _product_cate;
    int _product_size;
    decimal _price;
    decimal _charge;
    int _shipping_company_id;
    int _ap_id;
    int _sum=1;

    public AccountProduct() { }
    public int product_id
    {
        get
        {
            return _product_id;
        }
        set { _product_id = value; }
    }
    public int ap_id
    {
        get { return _ap_id; }
        set { _ap_id = value; }
    }
    public int product_size
    {
        get { return _product_size; }
        set { _product_size = value; }
    }

    public product_category product_cate
    {
        get { return _product_cate; }
        set { _product_cate = value; }
    }

    public decimal price
    {
        get { return _price; }
        set { _price = value; }
    }

    public decimal charge
    {
        get { return _charge; }
        set { _charge = value; }
    }

    public int shipping_company_id
    {
        get { return _shipping_company_id; }
        set { _shipping_company_id = value; }
    }

    public int sum
    {
        get { return _sum; }
        set { _sum = value; }
    }


}
