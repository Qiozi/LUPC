using LU.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LU.BLL
{
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
            , Data.nicklu2Entities db)
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
                custStore = new LU.Data.tb_customer_store
                {
                    create_datetime = DateTime.Now,
                    store_create_datetime = DateTime.Now
                };
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
            db.tb_customer_store.Add(custStore);
            db.SaveChanges();
            #endregion

            #region 添加商品

            // 删除已存在的
            var orderParts = db.tb_order_product.Where(p => p.order_code.Equals(oc)).ToList();
            foreach (var op in orderParts)
            {
                db.tb_order_product.Remove(op);
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
                        LU.Data.tb_order_product_sys_detail orderSysPart = new LU.Data.tb_order_product_sys_detail
                        {
                            sys_tmp_detail = sysDetId
                        };
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
                        db.tb_order_product_sys_detail.Add(orderSysPart);
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

                var orderProd = new LU.Data.tb_order_product
                {
                    tag = 1,
                    ebayItemID = "",
                    is_old = false,
                    menu_child_serial_no = cart.menu_child_serial_no,
                    menu_pre_serial_no = 0,// cart.menu_child_serial_no,
                    old_price = cart.old_price,
                    order_code = oc,
                    order_product_cost = cost,////
                    order_product_price = price,////
                    order_product_sold = sold,////
                    order_product_sum = cart.cart_temp_Quantity,////
                    prodType = cart.prodType,
                    product_current_price_rate = cart.price_rate,
                    product_name = cart.product_name,
                    product_serial_no = cart.product_serial_no,
                    product_type = prodID.ToString().Length == 8
                    ? (int)ProdType.system_product :
                    (cart.is_noebook.Value == 1
                    ? (int)ProdType.noebooks : (int)ProdType.part_product),

                    product_type_name = prodID.ToString().Length == 8 ? "System" : "Unit",
                    save_price = discount,
                    sku = "0"
                };
                db.tb_order_product.Add(orderProd);
            }
            db.SaveChanges();

            #endregion

            #region 添加订单主表
            var orders = db.tb_order_helper
                .Where(p => p.order_code.HasValue
                    && p.order_code.Value.Equals(temp_order_code));

            foreach (var o in orders)
            {
                db.tb_order_helper.Remove(o);
            }
            db.SaveChanges();

            var oh = new Data.tb_order_helper
            {
                out_status = ConfigV1.ORDER_BACK_STATUS,
                pre_status_serial_no = ConfigV1.ORDER_PRE_STATUS,
                create_datetime = DateTime.Now,

                call_me = 0,
                cost = cartPrice.cost,
                discount = cartPrice.sub_total + cartPrice.shipping_and_handling - cartPrice.taxable_total,
                shipping_charge = cartPrice.shipping_and_handling,
                pst = cartPrice.pst,
                hst = cartPrice.hst,
                gst = cartPrice.gst,
                gst_rate = cartPrice.gst_rate,
                hst_rate = cartPrice.hst_rate,
                pst_rate = cartPrice.pst_rate,
                grand_total = cartPrice.grand_total,

                sub_total = cartPrice.sub_total,
                sub_total_rate = cartPrice.sub_total_rate,
                sur_charge = cartPrice.sur_charge,
                sur_charge_rate = cartPrice.sur_charge_rate,
                system_category_serial_no = (sbyte)CurrSys,
                tag = 1,
                tax_charge = cartPrice.sales_tax,
                tax_rate = (int)cartPrice.sales_tax_rate,
                taxable_total = cartPrice.taxable_total,
                total = cartPrice.grand_total,
                total_rate = cartPrice.grand_total_rate,
                weee_charge = 0M,
                input_order_discount = 0M,// discount,// 0M,// cartPrice.sub_total - cartPrice.taxable_total,
                tax_export = false,
                current_system = CurrSys,
                customer_serial_no = CurrCustomerSerialNo,

                is_download_invoice = false,
                is_lock_input_order_discount = false,
                is_lock_shipping_charge = false,
                is_lock_tax_change = false,
                Is_Modify = true,
                is_ok = true,
                is_old = false,
                is_pay_end = IsPayEnd ? (sbyte)1 : (sbyte)0,
                Is_Pick_Up_Effective = false,
                is_send_email = false,
                Msg_from_Seller = "",
                note = "",
                order_code = temp_order_code,
                order_date = DateTime.Now,
                order_invoice = "",
                order_pay_status_id = (int)PaypalPayStatus.paypal_no_paed,
                order_source = 1,
                out_note = "",
                pay_method = CurrPayment.ToString(),

                price_unit = cartPrice.price_unit,
                prick_up_datetime1 = PrickUpDatetime1,
                rush = 0,
                shipping_company = CurrShippingCompany > 0 ? CurrShippingCompany : -1
            };
            customer.pay_method = CurrPayment;// save payment
            custStore.pay_method = CurrPayment; // save payment
            db.tb_order_helper.Add(oh);
            db.SaveChanges();

            #endregion

            #region 删除临时订单数据

            db.tb_cart_temp_price.Remove(cartPrice);
            foreach (var pd in cartTempPriceList)
            {
                db.tb_cart_temp.Remove(pd);
            }

            db.SaveChanges();
            // 删除旧数据
            var oldList = db.tb_cart_temp.Where(p => (p.customer_serial_no.HasValue && p.customer_serial_no.Value.Equals(customer.customer_serial_no.Value)) ||
                p.customer_serial_no.Value < 2);
            foreach (var pd in oldList)
            {
                db.tb_cart_temp.Remove(pd);
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
             , Data.nicklu2Entities db)
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

                subTotal += LU.BLL.PRateProvider.Multiply(sold, product.cart_temp_Quantity.Value);
                costTotal += LU.BLL.PRateProvider.Multiply(cost, product.cart_temp_Quantity.Value);
                priceTotal += LU.BLL.PRateProvider.Multiply(price, product.cart_temp_Quantity.Value);
                soldTotal += LU.BLL.PRateProvider.Multiply(sold, product.cart_temp_Quantity.Value);
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
                    cartPrice = new LU.Data.tb_cart_temp_price();
                    cartPrice.create_datetime = DateTime.Now;
                    cartPrice.order_code = OrderCode.ToString();
                    IsAdd = true;
                }
                var stateModel = db.tb_state_shipping.FirstOrDefault(p => p.state_serial_no.Equals(StatID));
                if (stateModel == null)
                    return;

                cartPrice.sub_total = subTotal;
                if (!ConfigV1.PriceIsCard.Contains(paymentID))
                    cartPrice.sur_charge = LU.BLL.PRateProvider.Multiply(subTotal, ConfigV1.CardRate);
                else
                    cartPrice.sur_charge = 0M;
                cartPrice.sur_charge_rate = ConfigV1.CardRate;

                cartPrice.cost = costTotal;
                cartPrice.create_datetime = DateTime.Now;

                cartPrice.shipping_and_handling = new AccountOrder().AccountCharge(ShippingCompany, OrderCode, StatID, ShippingCompany, db);
                cartPrice.shipping_and_handling = LU.BLL.PRateProvider.ConvertPrice(cartPrice.shipping_and_handling.Value
                    , CT
                    , CountryRate);
                cartPrice.shipping_and_handling_rate = 0M;

                cartPrice.taxable_total = cartPrice.sub_total + cartPrice.shipping_and_handling - cartPrice.sur_charge;

                cartPrice.gst_rate = ConfigV1.HstStates.Contains(StatID) ? 0M : stateModel.gst.Value;
                cartPrice.hst_rate = ConfigV1.HstStates.Contains(StatID) ? stateModel.gst.Value + stateModel.pst.Value : 0M;
                cartPrice.pst_rate = ConfigV1.HstStates.Contains(StatID) ? 0M : stateModel.pst.Value;

                cartPrice.hst = LU.BLL.PRateProvider.Multiply(cartPrice.taxable_total.Value, cartPrice.hst_rate.Value / 100);
                cartPrice.gst = LU.BLL.PRateProvider.Multiply(cartPrice.taxable_total.Value, cartPrice.gst_rate.Value / 100);
                cartPrice.pst = LU.BLL.PRateProvider.Multiply(cartPrice.taxable_total.Value, cartPrice.pst_rate.Value / 100);

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
                    db.tb_cart_temp_price.Add(cartPrice);
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
        public static void SaveOrderNote(string content, int orderCode, bool isDelExist, Data.nicklu2Entities db)
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
                    db.tb_chat_msg.Remove(c);
                }
                db.SaveChanges();
            }
            var chat = new LU.Data.tb_chat_msg();
            chat.msg_order_code = orderCode.ToString();
            chat.msg_content_text = content;
            chat.msg_type = 1;
            chat.regdate = DateTime.Now;
            chat.msg_author = "Me";
            db.tb_chat_msg.Add(chat);
            db.SaveChanges();
        }
    }
}
