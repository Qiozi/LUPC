using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for OrderHelper
/// </summary>
public class OrderHelper
{
    public OrderHelper()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static void CopyToOrder(int temp_order_code
        , int CustomerSerialNo
        , bool IsPayEnd
        , CountryType CT
        , decimal CountryRate
        , nicklu2Model.nicklu2Entities db)
    {
        if (temp_order_code < 1)
        {
            throw new Exception("Order code is not find.");
        }

        var CurrSys = 0;
        var PrickUpDatetime1 = DateTime.MinValue;
        var CurrCustomerSerialNo = 0;
        var CurrPayment = 0;
        var CurrShippingCompany = 0;
        var StatId = 0;

        // 取得一个订单的商品里的数据
        var ct = (from c in db.tb_cart_temp
                  where c.cart_temp_code.HasValue
            && c.cart_temp_code.Value.Equals(temp_order_code)
                  select new
                  {
                      ShippingCompany = c.shipping_company.Value,
                      PaymentID = c.pay_method.Value,
                      StateId = c.state_shipping.Value
                  }).Take(1).ToList();

        if (ct.Count != 1)
            throw new Exception("Order code is not find.");

        CurrPayment = ct[0].PaymentID;
        CurrShippingCompany = ct[0].ShippingCompany;
        StatId = ct[0].StateId;

        // 重新计算订单价格，后面只需要赋值保存
        AccountOrderPrice(temp_order_code
            , StatId
            , CurrShippingCompany
            , CT
            , CountryRate
            , db);

        var customer = db.tb_customer.FirstOrDefault(p => p.customer_serial_no.HasValue
            && p.customer_serial_no.Value.Equals(CustomerSerialNo));
        if (customer == null)
            throw new Exception("Customer is not find.");

        var cartTempPriceList = db.tb_cart_temp.Where(p => p.cart_temp_code.HasValue
            && p.cart_temp_code.Value.Equals(temp_order_code)).ToList();
        if (cartTempPriceList.Count == 0)
            throw new Exception("Order code is not find.");

        string oc = temp_order_code.ToString();
        var cartPrice = db.tb_cart_temp_price.FirstOrDefault(p => p.order_code.Equals(oc));
        if (cartPrice == null)
            throw new Exception("Order price is error.");

        #region 添加客户

        var custStore = db.tb_customer_store.FirstOrDefault(p => p.order_code.HasValue
            && p.order_code.Value.Equals(temp_order_code));
        if (custStore == null)
        {
            custStore = nicklu2Model.tb_customer_store.Createtb_customer_store(0, 0);
            custStore.create_datetime = DateTime.Now;
            custStore.store_create_datetime = DateTime.Now;
        }
        else
        {
            throw new Exception("Order code is exist customer.");
        }
        custStore.busniess_website = customer.busniess_website;
        custStore.card_verification_number = customer.card_verification_number;
        custStore.create_datetime = customer.create_datetime;
        custStore.customer_address1 = customer.customer_address1;
        custStore.customer_business_address = customer.customer_business_address;
        custStore.customer_business_city = customer.customer_business_city;
        custStore.customer_business_country_code = customer.customer_business_country_code;
        custStore.customer_business_state_code = customer.customer_business_state_code;
        custStore.customer_business_telephone = customer.customer_business_telephone;
        custStore.customer_business_zip_code = customer.customer_business_zip_code;
        custStore.customer_card_billing_shipping_address = customer.customer_card_billing_shipping_address;
        custStore.customer_card_city = customer.customer_card_city;
        custStore.customer_card_country = customer.customer_card_country;
        custStore.customer_card_country_code = customer.customer_card_country_code;
        custStore.customer_card_first_name = customer.customer_card_first_name;
        custStore.customer_card_issuer = customer.customer_card_issuer;
        custStore.customer_card_last_name = customer.customer_card_last_name;
        custStore.customer_card_phone = customer.customer_card_phone;
        custStore.customer_card_state = customer.customer_card_state;
        custStore.customer_card_state_code = customer.customer_card_state_code;
        custStore.customer_card_type = customer.customer_card_type;
        custStore.customer_card_zip_code = customer.customer_card_zip_code;
        custStore.customer_city = customer.customer_city;
        custStore.customer_comment_note = customer.customer_comment_note;
        custStore.customer_company = customer.customer_company;
        custStore.customer_country = customer.customer_country;
        custStore.customer_country_code = customer.customer_country_code;
        custStore.customer_credit_card = customer.customer_credit_card;
        custStore.customer_email1 = customer.customer_email1;
        custStore.customer_email2 = customer.customer_email2;
        custStore.customer_expiry = customer.customer_expiry;
        custStore.customer_fax = customer.customer_fax;
        custStore.customer_first_name = customer.customer_first_name;
        custStore.customer_last_name = customer.customer_last_name;
        custStore.customer_login_name = customer.customer_login_name;
        custStore.customer_note = customer.customer_note;
        custStore.customer_password = customer.customer_password;
        custStore.customer_rumor = customer.customer_rumor;
        custStore.customer_serial_no = customer.customer_serial_no;
        custStore.customer_shipping_address = customer.customer_shipping_address;
        custStore.customer_shipping_city = customer.customer_shipping_city;
        custStore.customer_shipping_country = customer.customer_shipping_country;
        custStore.customer_shipping_first_name = customer.customer_shipping_first_name;
        custStore.customer_shipping_last_name = customer.customer_shipping_last_name;
        custStore.customer_shipping_state = customer.customer_shipping_state;
        custStore.customer_shipping_zip_code = customer.customer_shipping_zip_code;
        custStore.EBay_ID = customer.EBay_ID;
        custStore.is_all_tax_execmtion = customer.is_all_tax_execmtion;
        custStore.is_old = customer.is_old;
        custStore.my_purchase_order = customer.my_purchase_order;
        custStore.news_latter_subscribe = (sbyte)customer.news_latter_subscribe;
        custStore.order_code = temp_order_code;
        custStore.pay_method = customer.pay_method;
        custStore.phone_c = customer.phone_c;
        custStore.phone_d = customer.phone_d;
        custStore.phone_n = customer.phone_n;
        custStore.shipping_country_code = customer.shipping_country_code;
        custStore.shipping_state_code = customer.shipping_state_code;
        custStore.source = customer.source;
        custStore.state_code = customer.state_code;
        custStore.state_serial_no = customer.state_serial_no;
        custStore.store_create_datetime = DateTime.Now;
        custStore.system_category_serial_no = customer.system_category_serial_no;
        custStore.tag = (sbyte)customer.tag;
        custStore.tax_execmtion = customer.tax_execmtion;
        custStore.zip_code = customer.zip_code;
        custStore.source = 1;
        db.AddTotb_customer_store(custStore);
        db.SaveChanges();
        #endregion

        #region 添加商品

        // 删除已存在的
        var orderParts = db.tb_order_product.Where(p => p.order_code.Equals(oc)).ToList();
        foreach (var op in orderParts)
        {
            db.DeleteObject(op);
        }
        db.SaveChanges();

        foreach (var cart in cartTempPriceList)
        {
            decimal cost = 0M;
            decimal price = 0M;
            decimal sold = 0M;
            decimal discount = 0M;
            CurrSys = cart.price_unit.ToLower() == "cad" ? 1 : 2;
            CurrCustomerSerialNo = cart.customer_serial_no.Value;
            CurrPayment = cart.pay_method.Value;
            if (cart.pick_datetime_1 == null)
            {
                PrickUpDatetime1 = DateTime.MinValue;
            }
            else
            {
                PrickUpDatetime1 = cart.pick_datetime_1.Value;
            }
            CurrShippingCompany = cart.shipping_company.Value;

            int prodID = cart.product_serial_no.Value;

            if (prodID.ToString().Length == 8)
            {
                var sysCode = prodID.ToString();
                var sysDetailList = (from sp in db.tb_sp_tmp_detail
                                     join p in db.tb_product on sp.product_serial_no.Value equals p.product_serial_no
                                     where sp.sys_tmp_code.Equals(sysCode)
                                     select new
                                     {
                                         item = sp,
                                         Cost = p.product_current_cost.Value,
                                         Price = p.product_current_price.Value,
                                         Sold = p.product_current_price.Value - p.product_current_discount.Value,
                                         Discount = p.product_current_discount.Value
                                     }).ToList();

                foreach (var sysDetail in sysDetailList)
                {
                    var sysDetId = sysDetail.item.system_templete_serial_no.Value;
                    nicklu2Model.tb_order_product_sys_detail orderSysPart = nicklu2Model.tb_order_product_sys_detail.Createtb_order_product_sys_detail(sysDetId);
                    orderSysPart.cate_name = sysDetail.item.cate_name;
                    orderSysPart.ebay_number = sysDetail.item.ebay_number;
                    orderSysPart.is_lock = sysDetail.item.is_lock;
                    orderSysPart.old_price = sysDetail.item.old_price;
                    orderSysPart.part_group_id = sysDetail.item.part_group_id;
                    orderSysPart.part_max_quantity = sysDetail.item.part_max_quantity;
                    orderSysPart.part_quantity = sysDetail.item.part_quantity;
                    orderSysPart.product_current_cost = sysDetail.Cost;
                    orderSysPart.product_current_price = sysDetail.Price;
                    orderSysPart.product_current_price_rate = sysDetail.item.product_current_price_rate;
                    orderSysPart.product_current_sold = sysDetail.Sold;
                    orderSysPart.product_name = sysDetail.item.product_name;
                    orderSysPart.product_order = sysDetail.item.product_order;
                    orderSysPart.product_serial_no = sysDetail.item.product_serial_no;
                    orderSysPart.re_sys_tmp_detail = sysDetail.item.re_sys_tmp_detail;
                    orderSysPart.save_price = sysDetail.item.save_price;
                    orderSysPart.sys_tmp_code = sysDetail.item.sys_tmp_code;
                    orderSysPart.system_product_serial_no = sysDetail.item.system_product_serial_no;
                    orderSysPart.system_templete_serial_no = sysDetail.item.system_templete_serial_no;
                    db.AddTotb_order_product_sys_detail(orderSysPart);
                }
                db.SaveChanges();

                cost = sysDetailList.Select(p => p.Cost).Sum();
                price = sysDetailList.Select(p => p.Price).Sum();// cart.price.Value;
                sold = sysDetailList.Select(p => p.Sold).Sum();// cart.price.Value - cart.save_price.Value;
                discount = sysDetailList.Select(p => p.Discount).Sum();// cart.save_price.Value;
            }
            else
            {
                var prod = db.tb_product.Single(p => p.product_serial_no.Equals(cart.product_serial_no.Value));
                cost = prod.product_current_cost.Value;
                price = prod.product_current_price.Value;
                sold = prod.product_current_price.Value - prod.product_current_discount.Value;
                discount = prod.product_current_discount.Value;
            }

            var orderProd = nicklu2Model.tb_order_product.Createtb_order_product(0, 1);
            orderProd.ebayItemID = "";
            orderProd.is_old = false;
            orderProd.menu_child_serial_no = cart.menu_child_serial_no;
            orderProd.menu_pre_serial_no = 0;// cart.menu_child_serial_no;
            orderProd.old_price = cart.old_price;
            orderProd.order_code = oc;
            orderProd.order_product_cost = cost;////
            orderProd.order_product_price = price;////
            orderProd.order_product_sold = sold;////
            orderProd.order_product_sum = cart.cart_temp_Quantity;////
            orderProd.prodType = cart.prodType;
            orderProd.product_current_price_rate = cart.price_rate;
            orderProd.product_name = cart.product_name;
            orderProd.product_serial_no = cart.product_serial_no;
            orderProd.product_type = prodID.ToString().Length == 8
                ? (int)ProdType.system_product :
                (cart.is_noebook.Value == 1
                ? (int)ProdType.noebooks : (int)ProdType.part_product);

            orderProd.product_type_name = prodID.ToString().Length == 8 ? "System" : "Unit";
            orderProd.save_price = discount;
            orderProd.sku = "0";
            orderProd.tag = 1;
            db.AddTotb_order_product(orderProd);
        }
        db.SaveChanges();

        #endregion

        #region 添加订单主表
        var orders = db.tb_order_helper.Where(p => p.order_code.HasValue
            && p.order_code.Value.Equals(temp_order_code));

        foreach (var o in orders)
        {
            db.DeleteObject(o);
        }
        db.SaveChanges();

        var oh = nicklu2Model.tb_order_helper.Createtb_order_helper(0, DateTime.Now, setting.ORDER_BACK_STATUS, setting.ORDER_PRE_STATUS);
        oh.call_me = 0;
        oh.cost = cartPrice.cost;
        oh.discount = cartPrice.sub_total + cartPrice.shipping_and_handling - cartPrice.taxable_total;
        oh.shipping_charge = cartPrice.shipping_and_handling;
        oh.pst = cartPrice.pst;
        oh.hst = cartPrice.hst;
        oh.gst = cartPrice.gst;
        oh.gst_rate = cartPrice.gst_rate;
        oh.hst_rate = cartPrice.hst_rate;
        oh.pst_rate = cartPrice.pst_rate;
        oh.grand_total = cartPrice.grand_total;

        oh.sub_total = cartPrice.sub_total;
        oh.sub_total_rate = cartPrice.sub_total_rate;
        oh.sur_charge = cartPrice.sur_charge;
        oh.sur_charge_rate = cartPrice.sur_charge_rate;
        oh.system_category_serial_no = (sbyte)CurrSys;
        oh.tag = 1;
        oh.tax_charge = cartPrice.sales_tax;
        oh.tax_rate = (int)cartPrice.sales_tax_rate;
        oh.taxable_total = cartPrice.taxable_total;
        oh.total = cartPrice.grand_total;
        oh.total_rate = cartPrice.grand_total_rate;
        oh.weee_charge = 0M;
        oh.input_order_discount = oh.discount;// 0M;// cartPrice.sub_total - cartPrice.taxable_total;

        oh.tax_export = false;
        oh.create_datetime = DateTime.Now;
        oh.current_system = CurrSys;
        oh.customer_serial_no = CurrCustomerSerialNo;
  
        oh.is_download_invoice = false;
        oh.is_lock_input_order_discount = false;
        oh.is_lock_shipping_charge = false;
        oh.is_lock_tax_change = false;
        oh.Is_Modify = true;
        oh.is_ok = true;
        oh.is_old = false;
        oh.is_pay_end = IsPayEnd ? (sbyte)1 : (sbyte)0;
        oh.Is_Pick_Up_Effective = false;
        oh.is_send_email = false;
        oh.Msg_from_Seller = "";
        oh.note = "";
        oh.order_code = temp_order_code;
        oh.order_date = DateTime.Now;
        oh.order_invoice = "";
        oh.order_pay_status_id = (int)PaypalPayStatus.paypal_no_paed;
        oh.order_source = 1;
        oh.out_note = "";
        oh.out_status = setting.ORDER_BACK_STATUS;
        oh.pay_method = CurrPayment.ToString();
        customer.pay_method = CurrPayment;  // save payment
        custStore.pay_method = CurrPayment; // save payment
        oh.pre_status_serial_no = setting.ORDER_PRE_STATUS;
        oh.price_unit = cartPrice.price_unit;
        oh.prick_up_datetime1 = PrickUpDatetime1;

        oh.rush = 0;

        oh.shipping_company = CurrShippingCompany;

        db.AddTotb_order_helper(oh);
        db.SaveChanges();

        #endregion

        #region 删除临时订单数据

        db.tb_cart_temp_price.DeleteObject(cartPrice);
        foreach (var pd in cartTempPriceList)
        {
            db.tb_cart_temp.DeleteObject(pd);
        }

        db.SaveChanges();
        // 删除旧数据
        var oldList = db.tb_cart_temp.Where(p => (p.customer_serial_no.HasValue && p.customer_serial_no.Value.Equals(customer.customer_serial_no.Value)) ||
            p.customer_serial_no.Value < 2);
        foreach (var pd in oldList)
        {
            db.tb_cart_temp.DeleteObject(pd);
        }
        db.SaveChanges();
        #endregion
    }




    /// <summary>
    /// 计算订单价格，保存到 cart temp price里
    /// </summary>
    /// <param name="OrderCode"></param>
    /// <param name="StatID"></param>
    /// <param name="ShippingCompany"></param>
    /// <param name="CT"></param>
    /// <param name="CountryRate">汇率</param>
    /// <param name="db"></param>
    public static void AccountOrderPrice(int OrderCode
         , int StatID
         , int ShippingCompany
         , CountryType CT
         , decimal CountryRate
         , nicklu2Model.nicklu2Entities db)
    {
        var subTotal = 0M;
        var costTotal = 0M;
        var priceTotal = 0M;
        var soldTotal = 0M;

        var paymentID = 0;
        var subList = db.tb_cart_temp.Where(p => p.cart_temp_code.HasValue && p.cart_temp_code.Value.Equals(OrderCode)).ToList();
        //var subList = (from c in db.tb_cart_temp
        //               where c.cart_temp_code.HasValue && c.cart_temp_code.Value.Equals(OrderCode)
        //               select new
        //               {
        //                   sku = c.product_serial_no.Value,
        //                   paymentId = c.pay_method.Value,
        //                   qty = c.cart_temp_Quantity.Value
        //               }).ToList();

        // 为了统一计算方式， 不用数据库的价格*数量
        // 取得即时价格
        foreach (var product in subList)
        {
            var cost = 0M;
            var price = 0M;
            var sold = 0M;
                       
            if (product.product_serial_no.Value.ToString().Length == 8)
            {
                var sysCode = product.product_serial_no.Value.ToString();
                var sysDetailList = (from sp in db.tb_sp_tmp_detail
                                     join p in db.tb_product on sp.product_serial_no.Value equals p.product_serial_no
                                     where sp.sys_tmp_code.Equals(sysCode)
                                     select new
                                     {
                                         Cost = p.product_current_cost.Value,
                                         Price = p.product_current_price.Value,
                                         Sold = p.product_current_price.Value - p.product_current_discount.Value,
                                         Discount = p.product_current_discount.Value
                                     }).ToList();

                foreach (var item in sysDetailList)
                {
                    cost += item.Cost;
                    price += item.Price;
                    sold += item.Price - item.Discount;
                }
            }
            else
            {
                var prod = db.tb_product.Single(p => p.product_serial_no.Equals(product.product_serial_no.Value));
                cost = prod.product_current_cost.Value;
                price = prod.product_current_price.Value;
                sold = prod.product_current_price.Value - prod.product_current_discount.Value;

                product.cost = cost;
                product.price = sold;
                product.save_price = prod.product_current_discount.Value;
                product.old_price = price;
            }

            subTotal += PriceRate.Multiply(sold, product.cart_temp_Quantity.Value);
            costTotal += PriceRate.Multiply(cost, product.cart_temp_Quantity.Value);
            priceTotal += PriceRate.Multiply(price, product.cart_temp_Quantity.Value);
            soldTotal += PriceRate.Multiply(sold, product.cart_temp_Quantity.Value);
            paymentID = product.pay_method.Value;

        }
        // throw new Exception(subTotal.ToString());
        if (subTotal > 0)
        {
            bool IsAdd = false;
            string oc = OrderCode.ToString();
            var cartPrice = db.tb_cart_temp_price.FirstOrDefault(p => p.order_code.Equals(oc));
            if (cartPrice == null)
            {
                cartPrice = nicklu2Model.tb_cart_temp_price.Createtb_cart_temp_price(0);
                cartPrice.create_datetime = DateTime.Now;
                cartPrice.order_code = OrderCode.ToString();
                IsAdd = true;
            }
            var stateModel = db.tb_state_shipping.FirstOrDefault(p => p.state_serial_no.Equals(StatID));
            if (stateModel == null)
                return;

            cartPrice.sub_total = subTotal;
            if (!setting.PriceIsCard.Contains(paymentID))
                cartPrice.sur_charge = PriceRate.Multiply(subTotal, setting.CardRate);
            else
                cartPrice.sur_charge = 0M;
            cartPrice.sur_charge_rate = setting.CardRate;

            cartPrice.cost = costTotal;
            cartPrice.create_datetime = DateTime.Now;

            cartPrice.shipping_and_handling = new AccountOrder().AccountCharge(ShippingCompany, OrderCode, StatID, ShippingCompany, db);
            cartPrice.shipping_and_handling = PriceRate.ConvertPrice(cartPrice.shipping_and_handling.Value
                , CT
                , CountryRate);
            cartPrice.shipping_and_handling_rate = 0M;

            cartPrice.taxable_total = cartPrice.sub_total + cartPrice.shipping_and_handling - cartPrice.sur_charge;

            cartPrice.gst_rate = setting.HstStates.Contains(StatID) ? 0M : stateModel.gst.Value;
            cartPrice.hst_rate = setting.HstStates.Contains(StatID) ? stateModel.gst.Value + stateModel.pst.Value : 0M;
            cartPrice.pst_rate = setting.HstStates.Contains(StatID) ? 0M : stateModel.pst.Value;

            cartPrice.hst = PriceRate.Multiply(cartPrice.taxable_total.Value, cartPrice.hst_rate.Value / 100);
            cartPrice.gst = PriceRate.Multiply(cartPrice.taxable_total.Value, cartPrice.gst_rate.Value / 100);
            cartPrice.pst = PriceRate.Multiply(cartPrice.taxable_total.Value, cartPrice.pst_rate.Value / 100);

            cartPrice.gst_charge_rate = 0m;
            cartPrice.hst_charge_rate = 0M;
            cartPrice.pst_charge_rate = 0M;

            cartPrice.price_unit = CT.ToString();

            cartPrice.sales_tax = cartPrice.hst + cartPrice.pst + cartPrice.gst;
            cartPrice.sales_tax_rate = cartPrice.hst_rate + cartPrice.gst_rate + cartPrice.pst_rate;

            cartPrice.sub_total_rate = cartPrice.sub_total;

            cartPrice.grand_total = cartPrice.taxable_total + cartPrice.sales_tax;
            cartPrice.grand_total_rate = cartPrice.grand_total;

            if (IsAdd)
            {
                db.AddTotb_cart_temp_price(cartPrice);
            }
            db.SaveChanges();
        }
    }



    /// <summary>
    /// 临时订单，，不是正式订单，，删除已有的备注。
    /// </summary>
    /// <param name="content"></param>
    /// <param name="orderCode"></param>
    /// <param name="isDelExist"></param>
    /// <param name="db"></param>
    public static void SaveOrderNote(string content, int orderCode, bool isDelExist, nicklu2Model.nicklu2Entities db)
    {
        if (string.IsNullOrEmpty(content))
        {
            return;
        }
        string oc = orderCode.ToString();
        if (isDelExist) // 临时订单，删除已存在的
        {
            var chatList = db.tb_chat_msg.Where(p => p.msg_order_code.Equals(oc)).ToList();
            foreach (var c in chatList)
            {
                db.DeleteObject(c);
            }
            db.SaveChanges();
        }
        var chat = nicklu2Model.tb_chat_msg.Createtb_chat_msg(0);
        chat.msg_order_code = orderCode.ToString();
        chat.msg_content_text = content;
        chat.msg_type = 1;
        chat.regdate = DateTime.Now;
        chat.msg_author = "Me";
        db.AddTotb_chat_msg(chat);
        db.SaveChanges();
    }
}