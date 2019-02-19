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
using System.Collections.Generic;
using System.Linq;

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

    decimal ConfigGroundCompanyTotal = 900M;// Ground 公司， 超过900 不运输
    int ConfigGroundCompanyID = 4;
    int ConfigArmyShippingCompany = 8;// 美国陆军邮局
    List<int> ConfiArmyState = new List<int>() { 65, 66, 67 }; // 陆军邮局所到的洲ID


    /// <summary>
    /// 洲运费百分比
    /// </summary>
    public decimal StateShippingFeePencent {set;get;} 

    public Account(List<AccountProduct> ap, int state_shipping, nicklu2Model.nicklu2Entities db)
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
        _state_shipping = state_shipping;
        accountProduct = ap.ToArray();
        for (int i = 0; i < ap.Count; i++)
        {
            if (accountProduct[i] == null)
                continue;

            accountProduct[i].ap_id = i;
            // 取得每次产品运费
            AccountPart(accountProduct[i], db);
            // 运输公司
            _shiping_company = accountProduct[i].shipping_company_id;
            // 总价格
            _price_total += accountProduct[i].price * accountProduct[i].sum;

            if (accountProduct[i].ProductType == ProdType.noebooks)
            {
                _noebook_count += 1;
                //   取得最大价格
                if (accountProduct[i].charge > _noebook_max_charge)
                {
                    _noebook_max_charge = accountProduct[i].charge;
                    _noebook_top_price_ap_id = accountProduct[i].ap_id;
                }
            }
            if (accountProduct[i].ProductType == ProdType.part_product)
            {
                _part_count += 1;
                //   取得最大价格
                if (accountProduct[i].charge > _part_max_charge)
                {
                    _part_max_charge = accountProduct[i].charge;
                    _part_top_price_ap_id = accountProduct[i].ap_id;
                }
            }
            if (accountProduct[i].ProductType == ProdType.system_product)
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
        if (_price_total >= ConfigGroundCompanyTotal && _shiping_company == ConfigGroundCompanyID)
        {
            throw new Exception("未找到配置价格.ground 运输公司， 超过900");
        }

        // 路军邮局只能到自己的地方
        if (ConfigArmyShippingCompany == _shiping_company)
        {
            if (!ConfiArmyState.Contains(_state_shipping))
            {
                throw new Exception("未找到配置价格..");
            }
        }

        // 洲运费百分比，，各洲运费不同。
        if (StateShippingFeePencent / 100 == 0)
            return charge - dy_charge;
        return (charge - dy_charge) * StateShippingFeePencent / 100;
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
                //if (accountProduct[i].product_cate == ProdType.system_product && accountProduct[i].ap_id != _system_top_price_ap_id)
                //{
                //    // 子产品取一半
                //    charge += accountProduct[i].charge / 2;

                //}
                for (int j = 0; j < accountProduct.Length; j++)
                {
                    if (accountProduct[j].ProductType == ProdType.system_product)
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
        if (_noebook_count > 0)
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
                    //if (accountProduct[i].product_cate == ProdType.noebooks && accountProduct[i].ap_id !=_noebook_top_price_ap_id)
                    //{
                    //    charge += accountProduct[i].charge / 2;

                    //}

                    if (accountProduct[i].ProductType == ProdType.noebooks)
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
        if (_part_count > 0)
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
                    //if (accountProduct[i].product_cate == ProdType.part_product && accountProduct[i].ap_id != _part_top_price_ap_id)
                    //{
                    //    charge += accountProduct[i].charge / 2;
                    //}
                    //_part_price_sum += accountProduct[i].price;
                    if (accountProduct[i].ProductType == ProdType.part_product)
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

    /// <summary>
    /// 计算单个商品的运费 
    /// </summary>
    /// <param name="ap"></param>
    private void AccountPart(AccountProduct ap, nicklu2Model.nicklu2Entities db)
    {
        int productType = (int)ap.ProductType;
        // throw new Exception(productType.ToString());
        var amModel = db.tb_account.Where(p => p.product_size_id.HasValue
            && p.product_size_id.Value.Equals(ap.product_size)
            && p.shipping_company_id.HasValue
            && p.shipping_company_id.Value.Equals(ap.shipping_company_id)
            && p.product_category.HasValue
            && p.product_category.Value.Equals(productType)).ToList();
        //throw new Exception(amModel.Count.ToString());
        if (amModel.Count != 1)
            throw new Exception("未找到配置价格");

        if (amModel[0].charge.HasValue && amModel[0].charge == 0M)
            throw new Exception("配置价格0");


        ap.charge = amModel[0].charge.Value;// +ap.price / 100;
    }
}

