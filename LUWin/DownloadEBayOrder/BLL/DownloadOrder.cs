using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Net;
using System.IO;
using System.Threading;

namespace DownloadEBayOrder.BLL
{
    public class DownloadOrder : Events.EventBase
    {

        public bool Download(nicklu2Entities context, int orderid, Logs log)
        {
            try
            {
                if (orderid == 0)
                {
                    Thread.Sleep(30 * 1000);
                }
                else
                {
                    // 删除 旧的数据
                    var customerStore = context.tb_customer_store.FirstOrDefault(me => me.order_code.HasValue && me.order_code.Value.Equals(orderid));
                    if (customerStore != null)
                    {
                        var oc = orderid.ToString();
                        var orderHelper = context.tb_order_helper.FirstOrDefault(me => me.order_code.HasValue && me.order_code.Value.Equals(orderid));
                        var orderProduct = context.tb_order_product.Where(me => me.order_code.Equals(oc)).ToList();
                        foreach (var item in orderProduct)
                        {
                            context.tb_order_product.Remove(item);
                        }
                        var orderEbay = context.tb_order_ebay.Where(me => me.order_code.HasValue && me.order_code.Value.Equals(orderid)).ToList();
                        foreach (var item in orderEbay)
                        {
                            context.tb_order_ebay.Remove(item);
                        }

                        context.tb_customer_store.Remove(customerStore);
                        context.tb_order_helper.Remove(orderHelper);
                        context.SaveChanges();
                        return false;
                    }
                }
                Down(context, orderid);
                //if (DateTime.Now.Hour.Equals(9))
                //    EmailHelper.Send("wu.th@qq.com", "ebay download OK", "LU Computer eBay Order Download OK");
                ChangePaymentStatus(context);
            }
            catch (Exception ex)
            {
                log.WriteErrorLog(ex);
                log.WriteLog(ex.StackTrace);
                EmailHelper.Send("terryeah@gmail.com", "ebay download falid", "LU Computer eBay Order Download falid.");
                return false;
            }
            return true;
        }

        /// <summary>
        /// 用于本地测试
        /// </summary>
        /// <param name="context"></param>
        /// <param name="orderid"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        public bool DownloadTest(nicklu2Entities context, int orderid, Logs log, string filename)
        {
            // 删除 旧的数据
            var customerStore = context.tb_customer_store.FirstOrDefault(me => me.order_code.HasValue && me.order_code.Value.Equals(orderid));
            if (customerStore != null)
            {
                var oc = orderid.ToString();
                var orderHelper = context.tb_order_helper.FirstOrDefault(me => me.order_code.HasValue && me.order_code.Value.Equals(orderid));
                var orderProduct = context.tb_order_product.Where(me => me.order_code.Equals(oc)).ToList();
                foreach (var item in orderProduct)
                {
                    context.tb_order_product.Remove(item);
                }
                var orderEbay = context.tb_order_ebay.Where(me => me.order_code.HasValue && me.order_code.Value.Equals(orderid)).ToList();
                foreach (var item in orderEbay)
                {
                    context.tb_order_ebay.Remove(item);
                }

                context.tb_customer_store.Remove(customerStore);
                context.tb_order_helper.Remove(orderHelper);
                context.SaveChanges();
                // return false;
            }
            //   Down(context, orderid, filename);
            var ebayOrdersString = File.ReadAllText(filename);
            SaveToDB(context, ebayOrdersString, 0);
            return true;
        }

        /// <summary>
        /// change Shipping Company value to :-1;
        /// </summary>
        /// <param name="context"></param>
        void ChangePaymentStatus(nicklu2Entities context)
        {
            var query = context.tb_order_helper
                                    .Where(me => me.shipping_company.HasValue &&
                                                 me.shipping_company.Value.Equals(0))
                                    .ToList();

            for (int i = 0; i < query.Count; i++)
            {
                query[i].shipping_company = -1;
            }
            context.SaveChanges();
        }

        void Down(nicklu2Entities context, int orderid = 0)
        {
            string ebayOrdersString = "";

            string transactionID = "";
            if (orderid > 0)
            {
                try
                {
                    tb_order_ebay oe = context.tb_order_ebay.FirstOrDefault(e => e.order_code == orderid);
                    if (oe != null)
                    {
                        transactionID = oe.order_id;// format: 1111111111-22222222
                    }
                }
                catch { }
            }
            if (Config.DownDays == 0)
            {
                ebayOrdersString = LoadEbayOrder(DateTime.Now.AddDays(-1), DateTime.Now, transactionID);
            }
            else
            {
                ebayOrdersString = LoadEbayOrder(DateTime.Now.AddDays(-Config.DownDays), DateTime.Now, transactionID);
            }

            // 测试
            //if (true)
            //{
            //    string filefullname = BLL.Variable.storePath + "\\_eBayOrders.xml";
            //    ebayOrdersString = File.ReadAllText(filefullname);
            //}

            SaveToDB(context, ebayOrdersString, orderid);
            string paypalTransaction = LoadEBayOrderPaypalTransaction();
            SavePaypalTransaction(context, paypalTransaction);
            return;
        }

        /// <summary>
        /// 保存订单到数据库
        /// </summary>
        /// <param name="str"></param>
        /// <param name="intPtr"></param>
        void SaveToDB(nicklu2Entities context, string str, int orderid)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(str);

            if (xmlDoc["GetOrdersResponse"]["Ack"].InnerText != "Success"
                && xmlDoc["GetOrdersResponse"]["Ack"].InnerText != "Warning")
                return;

            decimal CurrencyUSD;
            tb_currency_convert currency = context.tb_currency_convert.FirstOrDefault(cc => cc.is_current == true);
            if (currency != null)
                CurrencyUSD = decimal.Parse(currency.currency_usd.ToString());
            else
                CurrencyUSD = 1M;

            foreach (XmlElement xe in xmlDoc["GetOrdersResponse"]["OrderArray"].ChildNodes)
            {
                int orderCode;
                int.TryParse(xe["ShippingDetails"]["SellingManagerSalesRecordNumber"].InnerText, out orderCode);

                SetStatus("download " + orderCode);
                int paymeth = xe["PaymentMethods"].InnerText == "PayPal" ? 15 : 0;

                // LoadEBayOrderItemDescription(orderCode);


                string loginName = xe["TransactionArray"]["Transaction"]["Buyer"]["Email"].InnerText;//
                string buyerUserID = xe["BuyerUserID"].InnerText;

                if (orderid > 0 && orderid == orderCode)
                {
                    #region 补充信息

                    tb_customer_store csmm = context.tb_customer_store.FirstOrDefault(c => c.order_code == orderid);

                    string stateName = xe["ShippingAddress"]["StateOrProvince"].InnerText;
                    string countryName = "";

                    if (xe["ShippingAddress"]["CountryName"] != null)
                        countryName = xe["ShippingAddress"]["CountryName"].InnerText;
                    if (xe["ShippingAddress"]["Country"] != null && countryName == "")
                        countryName = xe["ShippingAddress"]["Country"].InnerText;

                    tb_state_shipping statee = BLL.StateShippingHelper.GetStateID(context, stateName, countryName);

                    csmm.customer_business_state_code = statee.state_code;

                    csmm.state_serial_no = statee.state_serial_no;
                    csmm.state_code = statee.state_name;
                    csmm.customer_shipping_state = statee.state_serial_no;
                    csmm.shipping_state_code = statee.state_name;
                    csmm.shipping_country_code = statee.Country;
                    csmm.customer_business_country_code = statee.Country;
                    csmm.customer_country_code = statee.Country;

                    csmm.customer_card_first_name = xe["ShippingAddress"]["Name"].InnerText;
                    csmm.customer_first_name = csmm.customer_card_first_name;

                    csmm.customer_business_city = xe["ShippingAddress"]["CityName"].InnerText;
                    csmm.customer_business_telephone = xe["ShippingAddress"]["Phone"].InnerText;
                    csmm.customer_business_zip_code = xe["ShippingAddress"]["PostalCode"].InnerText;


                    csmm.customer_address1 = xe["ShippingAddress"]["Street1"].InnerText + " " + xe["ShippingAddress"]["Street2"].InnerText;
                    csmm.customer_business_address = csmm.customer_address1;
                    csmm.customer_shipping_address = csmm.customer_address1;
                    csmm.customer_shipping_city = csmm.customer_business_city;
                    csmm.customer_shipping_zip_code = xe["ShippingAddress"]["PostalCode"].InnerText;

                    csmm.zip_code = csmm.customer_business_zip_code;
                    csmm.phone_c = csmm.customer_business_telephone;
                    csmm.phone_d = csmm.customer_business_telephone;
                    csmm.phone_n = csmm.customer_business_telephone;
                    csmm.customer_shipping_first_name = csmm.customer_first_name;
                    csmm.customer_shipping_last_name = csmm.customer_last_name;

                    try
                    {
                        if (countryName == "CA"
                       || countryName == "Canada")
                        {
                            csmm.customer_country = "1";
                            csmm.shipping_country_code = "CA";
                            csmm.customer_country_code = "CA";
                            csmm.customer_shipping_country = 1;
                            csmm.customer_business_country_code = "CA";

                        }
                        else if (countryName == "US"
                            || countryName == "United States")
                        {
                            csmm.customer_country = "2";
                            csmm.shipping_country_code = "US";
                            csmm.customer_country_code = "US";
                            csmm.customer_business_country_code = "US";
                            csmm.customer_shipping_country = 2;
                        }
                        else
                        {
                            //csm.customer_country = oe.BuyerCountry;
                            //c.CustomerShippingCountryCode = oe.BuyerCountry;
                            //c.CustomerCountryCode = oe.BuyerCountry;
                            //c.CustomerShippingCountry = 2;
                        }
                    }
                    catch
                    {

                    }
                    context.SaveChanges();

                    tb_customer css = context.tb_customer.FirstOrDefault(c => c.customer_serial_no == csmm.customer_serial_no);
                    if (css == null)
                    {
                        //css = tb_customer.Createtb_customer(0, 1);
                        css = new tb_customer { news_latter_subscribe = 1 };
                        css.customer_login_name = csmm.customer_login_name;
                        css.customer_serial_no = csmm.customer_serial_no;
                        css.EBay_ID = csmm.EBay_ID;
                        css.create_datetime = DateTime.Now;

                        css.customer_business_state_code = csmm.customer_business_state_code;
                        css.state_serial_no = csmm.state_serial_no;
                        css.state_code = csmm.state_code;
                        css.customer_shipping_state = csmm.customer_shipping_state;
                        css.shipping_state_code = csmm.shipping_state_code;
                        css.shipping_country_code = csmm.shipping_country_code;
                        css.customer_business_country_code = csmm.customer_business_country_code;
                        css.customer_country_code = csmm.customer_country_code;
                        css.busniess_website = csmm.busniess_website;
                        css.card_verification_number = csmm.card_verification_number;
                        css.customer_business_address = csmm.customer_business_address;
                        css.customer_business_city = csmm.customer_business_city;
                        css.customer_business_telephone = csmm.customer_business_telephone;
                        css.customer_business_zip_code = csmm.customer_business_zip_code;
                        css.customer_card_billing_shipping_address = csmm.customer_card_billing_shipping_address;
                        css.customer_card_city = csmm.customer_card_city;
                        css.customer_card_country = csmm.customer_card_country;
                        css.customer_card_country_code = csmm.customer_card_country_code;
                        css.customer_card_first_name = csmm.customer_card_first_name;
                        css.customer_first_name = csmm.customer_first_name;
                        css.customer_card_issuer = csmm.customer_card_issuer;
                        css.customer_card_last_name = csmm.customer_card_last_name;
                        css.customer_card_phone = csmm.customer_card_phone;
                        css.customer_card_state = csmm.customer_card_state;
                        css.customer_card_state_code = csmm.customer_card_state_code;
                        css.customer_card_type = csmm.customer_card_type;
                        css.customer_card_zip_code = csmm.customer_card_zip_code;
                        css.customer_city = csmm.customer_city;
                        css.customer_comment_note = csmm.customer_comment_note;
                        css.customer_company = csmm.customer_company;
                        css.customer_credit_card = csmm.customer_credit_card;
                        css.customer_email1 = csmm.customer_email1;
                        css.customer_email2 = csmm.customer_email2;
                        css.customer_expiry = csmm.customer_expiry;
                        css.customer_fax = csmm.customer_fax;
                        css.customer_last_name = csmm.customer_last_name;
                        css.customer_login_name = csmm.customer_login_name;
                        css.customer_note = csmm.customer_note;
                        css.customer_password = csmm.customer_password;
                        css.customer_rumor = csmm.customer_rumor;
                        css.customer_address1 = csmm.customer_address1;
                        css.is_old = csmm.is_old;
                        css.my_purchase_order = csmm.my_purchase_order;
                        css.news_latter_subscribe = csmm.news_latter_subscribe;
                        css.pay_method = csmm.pay_method;
                        css.source = 3;
                        css.system_category_serial_no = 1;
                        css.tag = 1;
                        css.tax_execmtion = csmm.tax_execmtion;
                        css.zip_code = csmm.zip_code;
                        css.customer_country = csmm.customer_country;
                        css.shipping_country_code = csmm.shipping_country_code;
                        css.customer_country_code = csmm.customer_country_code;
                        css.customer_business_country_code = csmm.customer_business_country_code;
                        css.customer_shipping_country = csmm.customer_shipping_country;

                        context.tb_customer.Add(css);
                        //context.AddTotb_customer(css);
                        context.SaveChanges();

                    }
                    css.customer_email1 = csmm.customer_email1;
                    css.customer_email2 = csmm.customer_email2;
                    css.customer_login_name = csmm.customer_login_name;
                    css.customer_business_state_code = csmm.customer_business_state_code;
                    css.state_serial_no = csmm.state_serial_no;
                    css.state_code = csmm.state_code;
                    css.customer_shipping_state = csmm.customer_shipping_state;
                    css.shipping_state_code = csmm.shipping_state_code;
                    css.shipping_country_code = csmm.shipping_country_code;
                    css.customer_business_country_code = csmm.customer_business_country_code;
                    css.customer_country_code = csmm.customer_country_code;

                    css.customer_business_city = csmm.customer_business_city;
                    css.customer_business_telephone = csmm.customer_business_telephone;
                    css.customer_business_zip_code = csmm.customer_business_zip_code;
                    css.customer_card_first_name = csmm.customer_card_first_name;

                    css.customer_address1 = csmm.customer_address1;
                    css.customer_business_address = csmm.customer_business_address;
                    css.customer_shipping_address = csmm.customer_shipping_address;
                    css.customer_shipping_city = csmm.customer_shipping_city;
                    css.customer_shipping_zip_code = csmm.customer_shipping_zip_code;

                    css.zip_code = csmm.zip_code;
                    css.customer_country = csmm.customer_country;
                    css.shipping_country_code = csmm.shipping_country_code;
                    css.customer_country_code = csmm.customer_country_code;
                    css.customer_shipping_country = csmm.customer_shipping_country;
                    css.customer_business_country_code = csmm.customer_business_country_code;
                    css.phone_c = csmm.phone_c;
                    css.phone_d = csmm.phone_d;
                    css.phone_n = csmm.phone_n;
                    css.customer_shipping_first_name = csmm.customer_shipping_first_name;
                    css.customer_shipping_last_name = csmm.customer_shipping_last_name;
                    css.customer_first_name = csmm.customer_first_name;
                    context.SaveChanges();

                    string ocString = orderCode.ToString();

                    foreach (XmlElement OrderPart in xe["TransactionArray"].ChildNodes)
                    {
                        //  File.WriteAllText("C:\\Program Files (x86)\\LUComputer\\DownloadEBayOrder\\eBayOrders\\" + ocString + "_a.txt", OrderPart["TransactionPrice"].InnerText);

                        var opProds = context.tb_order_product.Where(pp => pp.order_code.Equals(ocString)).ToList();
                        if (opProds != null && opProds.Count > 0)
                        {
                            if (opProds.Count == 1)
                            {
                                opProds[0].order_product_price = decimal.Parse(OrderPart["TransactionPrice"].InnerText);
                                opProds[0].order_product_sold = decimal.Parse(OrderPart["TransactionPrice"].InnerText);
                                context.SaveChanges();
                            }
                        }
                        else
                        {
                            string itemidSingle = OrderPart["Item"]["ItemID"].InnerText;
                            tb_ebay_code_and_luc_sku ecalsSingle = context.tb_ebay_code_and_luc_sku.FirstOrDefault(c => c.ebay_code.Equals(itemidSingle));
                            if (ecalsSingle == null) // 网站不存在的商品
                            {
                                //ecalsSingle = tb_ebay_code_and_luc_sku.Createtb_ebay_code_and_luc_sku(0);
                                ecalsSingle = new tb_ebay_code_and_luc_sku();
                                ecalsSingle.ebay_code = itemidSingle;
                                ecalsSingle.is_online = true;
                                ecalsSingle.SKU = 0;
                                ecalsSingle.is_sys = false;
                                ecalsSingle.regdate = DateTime.Now;
                                //context.AddTotb_ebay_code_and_luc_sku(ecalsSingle);
                                context.tb_ebay_code_and_luc_sku.Add(ecalsSingle);
                                context.SaveChanges();
                            }

                            if (ecalsSingle != null)
                            {
                                int skuSingle;
                                int.TryParse(ecalsSingle.SKU.ToString(), out skuSingle);
                                int qtySingle;
                                int.TryParse(OrderPart["QuantityPurchased"].InnerText, out qtySingle);
                                tb_product prodSingle = context.tb_product.FirstOrDefault(p => p.product_serial_no.Equals(skuSingle));
                                //  File.WriteAllText("C:\\Program Files (x86)\\LUComputer\\DownloadEBayOrder\\eBayOrders\\" + ocString + "_q.txt", opSingle.sku);

                                if (prodSingle != null)
                                {
                                    //opSingle = tb_order_product.Createtb_order_product(0, 1);
                                    var opSingle = new tb_order_product
                                    {
                                        tag = 1
                                    };
                                    opSingle.order_code = ocString;
                                    tb_product_category pc = context.tb_product_category.FirstOrDefault(p => p.menu_child_serial_no == prodSingle.menu_child_serial_no);

                                    opSingle.ebayItemID = itemidSingle;
                                    opSingle.menu_child_serial_no = prodSingle.menu_child_serial_no;
                                    // op.menu_pre_serial_no =
                                    // op.order_code = oh.order_code.ToString();
                                    opSingle.order_product_cost = prodSingle.product_current_cost;
                                    opSingle.order_product_price = decimal.Parse(OrderPart["TransactionPrice"].InnerText);
                                    opSingle.order_product_sold = decimal.Parse(OrderPart["TransactionPrice"].InnerText);
                                    opSingle.order_product_sum = qtySingle;
                                    opSingle.prodType = OrderPart["Item"]["ConditionDisplayName"].InnerText.ToLower().IndexOf("new") > -1 ? "NEW" : "Refurbished";
                                    opSingle.product_current_price_rate = opSingle.order_product_sold;
                                    opSingle.product_name = OrderPart["Item"]["Title"].InnerText;
                                    opSingle.product_serial_no = skuSingle;
                                    opSingle.product_type = pc.is_noebook == 1 ? 3 : 1; // 1 part, 2 system, 3 notebook
                                    opSingle.product_type_name = pc.is_noebook == 1 ? "Notebook" : "Unit";// unit, notebook, system ;
                                    opSingle.save_price = 0;
                                    opSingle.sku = skuSingle.ToString();
                                    // context.AddTotb_order_product(opSingle);
                                    context.tb_order_product.Add(opSingle);
                                    context.SaveChanges();
                                }
                                else if (skuSingle.ToString().Length == 6)
                                {
                                    // is system 
                                    //tb_ebay_system_and_category esc = context.tb_ebay_system_and_category.FirstOrDefault(ec => ec.SystemSku == sku);
                                    //int newCode = DBProvider.NewSysCode();
                                    //op.ebayItemID = oem.item_number;
                                    //op.menu_child_serial_no = esc != null ? esc.eBaySysCategoryID : 0;// prod.menu_child_serial_no;
                                    //                                                                  // op.menu_pre_serial_no =

                                    //op.order_product_cost = DBProvider.GeteBaySysCost(sku);
                                    //op.order_product_price = decimal.Parse(OrderPart["TransactionPrice"].InnerText);
                                    //op.order_product_sold = decimal.Parse(OrderPart["TransactionPrice"].InnerText);
                                    //op.order_product_sum = oem.quantity;
                                    //op.prodType = OrderPart["Item"]["ConditionDisplayName"].InnerText.ToLower().IndexOf("new") > -1 ? "NEW" : "Refurbished";
                                    //op.product_current_price_rate = op.order_product_sold;
                                    //op.product_name = oem.item_title;
                                    //op.product_serial_no = newCode;
                                    //op.product_type = 2; // 1 part, 2 system, 3 notebook
                                    //op.product_type_name = "System";// unit, notebook, system ;
                                    //op.save_price = 0;
                                    //op.sku = sku.ToString();
                                    //// context.AddTotb_order_product(op);
                                    //context.tb_order_product.Add(op);
                                    //context.SaveChanges();

                                    //IQueryable<tb_ebay_system_parts> sysList = context.tb_ebay_system_parts.Where(s => s.system_sku == sku && s.is_belong_price == true);

                                    //for (int i = 0; i < sysList.ToList().Count; i++)
                                    //{
                                    //    var m = sysList.ToList()[i];
                                    //    int commentID;
                                    //    int.TryParse(m.comment_id.ToString(), out commentID);
                                    //    tb_ebay_system_part_comment espc = context.tb_ebay_system_part_comment.FirstOrDefault(e => e.id.Equals(commentID));
                                    //    tb_product prod = context.tb_product.FirstOrDefault(p => p.product_serial_no == m.luc_sku);


                                    //    //tb_order_product_sys_detail sysDetail = tb_order_product_sys_detail.Createtb_order_product_sys_detail(0);
                                    //    var sysDetail = new tb_order_product_sys_detail();
                                    //    sysDetail.cate_name = espc != null ? espc.comment : "";
                                    //    sysDetail.ebay_number = oem.item_number;
                                    //    sysDetail.is_lock = false;
                                    //    sysDetail.old_price = 0M;
                                    //    sysDetail.part_group_id = m.part_group_id;
                                    //    sysDetail.part_max_quantity = m.max_quantity;
                                    //    sysDetail.part_quantity = m.part_quantity;
                                    //    sysDetail.product_current_cost = prod != null ? prod.product_current_cost : 0M;
                                    //    sysDetail.product_current_price = prod != null ? prod.product_current_price : 0M;
                                    //    sysDetail.product_current_price_rate = 1;
                                    //    sysDetail.product_current_sold = prod != null ? prod.product_current_price : 0M;
                                    //    sysDetail.product_name = prod != null ? prod.product_ebay_name : "";
                                    //    sysDetail.product_order = m.id;
                                    //    sysDetail.product_serial_no = m.luc_sku;
                                    //    sysDetail.save_price = 0M;
                                    //    sysDetail.sys_tmp_code = newCode.ToString();
                                    //    sysDetail.system_product_serial_no = sku;
                                    //    sysDetail.system_templete_serial_no = newCode;

                                    //    // context.AddTotb_order_product_sys_detail(sysDetail);
                                    //    context.tb_order_product_sys_detail.Add(sysDetail);
                                    //    context.SaveChanges();
                                    //}
                                }
                                else
                                {
                                    decimal buyItNewPrice;
                                    decimal.TryParse(OrderPart["TransactionPrice"].InnerText, out buyItNewPrice);
                                    tb_ebay_selling esSingle = context.tb_ebay_selling.FirstOrDefault(s => s.ItemID == itemidSingle);
                                    //prodSingle = tb_product.Createtb_product(0);
                                    prodSingle = new tb_product();//
                                    prodSingle.menu_child_serial_no = 339; // other category id.
                                    prodSingle.product_short_name = esSingle != null ? esSingle.Title : "";// oem.item_title;
                                    prodSingle.product_name = esSingle != null ? esSingle.Title : ""; //oem.item_title;
                                    prodSingle.product_img_sum = 1;
                                    prodSingle.product_name_long_en = esSingle != null ? esSingle.Title : ""; //oem.item_title;
                                    prodSingle.product_order = 1;
                                    prodSingle.is_non = 0;
                                    prodSingle.last_regdate = DateTime.Now;
                                    prodSingle.@new = 1;
                                    prodSingle.other_product_sku = 999999;
                                    prodSingle.product_current_cost = buyItNewPrice;
                                    prodSingle.product_current_price = buyItNewPrice;
                                    prodSingle.product_current_special_cash_price = buyItNewPrice;
                                    prodSingle.regdate = DateTime.Now;
                                    prodSingle.split_line = 0;
                                    prodSingle.tag = 1;
                                    prodSingle.issue = false;
                                    prodSingle.adjustment_enddate = DateTime.Now;
                                    prodSingle.real_cost_regdate = DateTime.Now;
                                    prodSingle.adjustment_regdate = DateTime.Now;
                                    prodSingle.product_size_id = 1;
                                    prodSingle.prodType = "NEW";
                                    // context.AddTotb_product(prodSingle);
                                    context.tb_product.Add(prodSingle);
                                    context.SaveChanges();

                                    skuSingle = prodSingle.product_serial_no;

                                    //opSingle = tb_order_product.Createtb_order_product(0, 1);
                                    var opSingle = new tb_order_product
                                    {
                                        tag = 1
                                    };
                                    opSingle.order_code = ocString;
                                    tb_product_category pc = context.tb_product_category.FirstOrDefault(p => p.menu_child_serial_no == prodSingle.menu_child_serial_no);

                                    opSingle.ebayItemID = itemidSingle;
                                    opSingle.menu_child_serial_no = prodSingle.menu_child_serial_no;
                                    // op.menu_pre_serial_no =
                                    // op.order_code = oh.order_code.ToString();
                                    opSingle.order_product_cost = prodSingle.product_current_cost;
                                    opSingle.order_product_price = decimal.Parse(OrderPart["TransactionPrice"].InnerText);
                                    opSingle.order_product_sold = decimal.Parse(OrderPart["TransactionPrice"].InnerText);
                                    opSingle.order_product_sum = qtySingle;
                                    opSingle.prodType = OrderPart["Item"]["ConditionDisplayName"].InnerText.ToLower().IndexOf("new") > -1 ? "NEW" : "Refurbished";
                                    opSingle.product_current_price_rate = opSingle.order_product_sold;
                                    opSingle.product_name = OrderPart["Item"]["Title"].InnerText;
                                    opSingle.product_serial_no = skuSingle;
                                    opSingle.product_type = pc.is_noebook == 1 ? 3 : 1; // 1 part, 2 system, 3 notebook
                                    opSingle.product_type_name = pc.is_noebook == 1 ? "Notebook" : "Unit";// unit, notebook, system ;
                                    opSingle.save_price = 0;
                                    opSingle.sku = skuSingle.ToString();
                                    // context.AddTotb_order_product(opSingle);
                                    context.tb_order_product.Add(opSingle);
                                    context.SaveChanges();
                                    //   File.WriteAllText("C:\\Program Files (x86)\\LUComputer\\DownloadEBayOrder\\eBayOrders\\" + ocString + ".txt", opSingle.sku);

                                    //ecals = tb_ebay_code_and_luc_sku.Createtb_ebay_code_and_luc_sku(0, DateTime.Now);
                                    ecalsSingle.BuyItNowPrice = buyItNewPrice;
                                    ecalsSingle.ebay_code = itemidSingle;
                                    ecalsSingle.is_online = true;
                                    ecalsSingle.is_sys = false;
                                    ecalsSingle.SKU = prodSingle.product_serial_no;
                                    ecalsSingle.regdate = DateTime.Now;
                                    context.SaveChanges();
                                }
                                StreamWriter sw = new StreamWriter(Variable.storePath + "\\_t.txt", false);
                                sw.Write(skuSingle.ToString());
                                sw.Close();

                            }
                        }
                    }

                    tb_order_helper ohSingle = context.tb_order_helper.FirstOrDefault(o => o.order_code == orderid);
                    if (ohSingle != null)
                    {
                        decimal taxPercent;
                        decimal.TryParse(xe["ShippingDetails"]["SalesTax"]["SalesTaxPercent"].InnerText, out taxPercent);

                        //if (xe["ShippingDetails"]["SalesTax"]["ShippingIncludedInTax"].InnerText == "true")

                        if (taxPercent > 0)
                        {
                            if ((statee.gst == null || statee.gst == 0)
                                && taxPercent > 0M)
                            {
                                statee.gst = 5;
                                statee.pst = sbyte.Parse(((int)(taxPercent - 5M)).ToString());
                                context.SaveChanges();
                            }

                            ohSingle.tax_charge = decimal.Parse(xe["ShippingDetails"]["SalesTax"]["SalesTaxAmount"].InnerText);
                            ohSingle.pst = ohSingle.tax_charge - ohSingle.tax_charge / (statee.gst + statee.pst) * statee.gst;
                            ohSingle.pst_rate = statee.pst;
                            ohSingle.gst = ohSingle.tax_charge - ohSingle.pst;
                            ohSingle.gst_rate = statee.gst;

                            // ohSingle.tax_charge = ohSingle.shipping_charge + ohSingle.sub_total;
                            context.SaveChanges();
                        }
                    }//
                    return;
                    #endregion
                }

                IQueryable<tb_order_helper> orderHelperList = context.tb_order_helper.Where(e => e.order_code == orderCode);
                //
                // 不要更新已存在的数据。
                //
                if (orderHelperList.Count() > 0)
                {
                    continue;
                }

                foreach (var m in orderHelperList)
                {
                    //context.tb_order_helper.DeleteObject(m);
                    context.tb_order_helper.Remove(m);
                }

                IQueryable<tb_order_ebay> orderEbayList = context.tb_order_ebay.Where(e => e.order_code == orderCode);
                foreach (var m in orderEbayList)
                {
                    //context.tb_order_ebay.DeleteObject(m);
                    context.tb_order_ebay.Remove(m);
                }

                // 删除已存在的
                var custStoreList = context.tb_customer_store.Where(c => c.order_code == orderCode);
                foreach (var m in custStoreList)
                {
                    // context.tb_customer_store.DeleteObject(m);
                    context.tb_customer_store.Remove(m);
                }

                string oc = orderCode.ToString();
                var opList = context.tb_order_product.Where(p => p.order_code == oc);
                foreach (var m in opList)
                {
                    // context.tb_order_product.DeleteObject(m);
                    context.tb_order_product.Remove(m);
                }
                context.SaveChanges();

                tb_customer cs = context.tb_customer.FirstOrDefault(c => c.customer_login_name.Equals(loginName) || (c.EBay_ID.Equals(buyerUserID) && !string.IsNullOrEmpty(c.EBay_ID)));

                if (cs == null)
                {
                    //cs = tb_customer.Createtb_customer(0, 1);
                    cs = new tb_customer
                    {
                        news_latter_subscribe = 1
                    };
                    cs.create_datetime = DateTime.Now;
                    cs.customer_login_name = loginName;
                    cs.customer_serial_no = int.Parse(BLL.NewCustomerCode.GetNewCustomerCode(context));
                    cs.customer_password = cs.customer_login_name.Split(new char[] { '@' })[0];
                    // context.AddTotb_customer(cs);
                    context.tb_customer.Add(cs);
                    context.SaveChanges();
                }

                string stateNameE = xe["ShippingAddress"]["StateOrProvince"].InnerText;
                string countryNameE = "";

                if (xe["ShippingAddress"]["CountryName"] != null)
                    countryNameE = xe["ShippingAddress"]["CountryName"].InnerText;
                if (xe["ShippingAddress"]["Country"] != null && countryNameE == "")
                    countryNameE = xe["ShippingAddress"]["Country"].InnerText;

                var state = string.IsNullOrEmpty(stateNameE.Trim()) ? BLL.StateShippingHelper.GetStateID(context, xe["ShippingDetails"]["SalesTax"]["SalesTaxState"].InnerText, countryNameE.Trim()) : BLL.StateShippingHelper.GetStateID(context, stateNameE.Trim(), countryNameE.Trim());

                cs.customer_business_state_code = state.state_code;
                cs.state_serial_no = state.state_serial_no;
                cs.state_code = state.state_name;
                cs.customer_shipping_state = state.state_serial_no;
                cs.shipping_state_code = state.state_name;
                cs.shipping_country_code = state.Country;
                cs.customer_business_country_code = state.Country;
                cs.customer_country_code = state.Country;
                cs.create_datetime = DateTime.Now;
                cs.busniess_website = "";
                cs.card_verification_number = "";
                cs.create_datetime = DateTime.Now;

                cs.customer_business_address = cs.customer_address1;
                cs.customer_business_city = xe["ShippingAddress"]["CityName"].InnerText;

                cs.customer_business_telephone = xe["ShippingAddress"]["Phone"].InnerText;
                cs.customer_business_zip_code = xe["ShippingAddress"]["PostalCode"].InnerText;

                cs.customer_card_billing_shipping_address = "";
                cs.customer_card_city = "";
                cs.customer_card_country = null;
                cs.customer_card_country_code = "";
                cs.customer_card_first_name = xe["ShippingAddress"]["Name"].InnerText;
                cs.customer_first_name = cs.customer_card_first_name;
                cs.customer_card_issuer = "";
                cs.customer_card_last_name = "";
                cs.customer_card_phone = "";
                cs.customer_card_state = null;
                cs.customer_card_state_code = "";
                cs.customer_card_type = "";
                cs.customer_card_zip_code = "";
                cs.customer_city = cs.customer_business_city;
                cs.customer_comment_note = "";
                cs.customer_company = "";
                cs.customer_credit_card = "";
                cs.customer_email1 = xe["TransactionArray"]["Transaction"]["Buyer"]["Email"].InnerText;
                cs.customer_email2 = cs.customer_email1;
                cs.customer_expiry = "";
                cs.customer_fax = "";

                cs.customer_last_name = "";
                cs.customer_login_name = xe["BuyerUserID"].InnerText;
                cs.customer_note = "";

                cs.customer_rumor = "";
                cs.customer_address1 = xe["ShippingAddress"]["Street1"].InnerText + " " + xe["ShippingAddress"]["Street2"].InnerText;
                cs.customer_shipping_address = cs.customer_address1;
                cs.customer_shipping_city = cs.customer_business_city;
                cs.customer_shipping_zip_code = xe["ShippingAddress"]["PostalCode"].InnerText;
                cs.customer_shipping_first_name = cs.customer_first_name;
                cs.customer_shipping_last_name = cs.customer_last_name;
                cs.EBay_ID = xe["BuyerUserID"].InnerText;
                cs.is_old = false;
                cs.my_purchase_order = "";
                cs.news_latter_subscribe = 1;

                if (paymeth > 0)
                {
                    cs.pay_method = paymeth;
                }
                cs.phone_c = cs.customer_business_telephone;
                cs.phone_d = cs.customer_business_telephone;
                cs.phone_n = cs.customer_business_telephone;
                cs.source = 3;
                cs.system_category_serial_no = 1;
                cs.tag = 1;
                cs.tax_execmtion = "";
                cs.zip_code = cs.customer_business_zip_code;

                try
                {
                    if (countryNameE == "CA"
                   || countryNameE == "Canada")
                    {
                        cs.customer_country = "1";
                        cs.shipping_country_code = "CA";
                        cs.customer_country_code = "CA";
                        cs.customer_shipping_country = 1;
                        cs.customer_business_country_code = "CA";

                    }
                    else if (countryNameE == "US"
                        || countryNameE == "United States")
                    {
                        cs.customer_country = "2";
                        cs.shipping_country_code = "US";
                        cs.customer_country_code = "US";
                        cs.customer_business_country_code = "US";
                        cs.customer_shipping_country = 2;
                    }
                    else
                    {
                        // TODO
                    }
                }
                catch
                {

                }
                cs.Is_Modify = true;
                context.SaveChanges();

                //tb_order_helper oh = tb_order_helper.Createtb_order_helper(0, DateTime.Now, 12, 8);
                var oh = new tb_order_helper
                {
                    create_datetime = DateTime.Now,
                    out_status = 12,
                    pre_status_serial_no = 8
                };
                oh.call_me = 0;
                oh.cost = 0M;
                oh.create_datetime = DateTime.Now;
                oh.current_system = 1;
                oh.customer_serial_no = cs.customer_serial_no;
                oh.discount = 0M;
                oh.grand_total = decimal.Parse(xe["Total"].InnerText);
                oh.sub_total = decimal.Parse(xe["Subtotal"].InnerText);
                oh.sub_total_rate = oh.sub_total;

                oh.input_order_discount = 0M;
                oh.is_download_invoice = false;
                oh.is_lock_input_order_discount = false;
                oh.is_lock_shipping_charge = false;
                oh.is_ok = true;
                oh.is_old = false;
                oh.is_pay_end = 1;
                oh.is_send_email = false;
                oh.Msg_from_Seller = "";
                oh.order_code = orderCode;
                oh.order_date = DateTime.Parse(xe["CreatedTime"].InnerText.Replace("T", " ").Replace("Z", " ")); ;
                oh.order_invoice = oh.order_code.ToString();
                oh.order_pay_status_id = 2;
                oh.order_source = 3;
                //if ( xe["CheckoutStatus"]["PaymentMethod"].InnerText.Trim().ToLower() == "paypal")
                oh.pay_method = "15";
                //oh.shipping_charge = xe["ShippingServiceSelected"]["ShippingServiceCost"] == null ? 0M : decimal.Parse(xe["ShippingServiceSelected"]["ShippingServiceCost"].InnerText);
                oh.tag = 1;
                oh.Is_Modify = false;
                oh.price_unit = xe["Total"].GetAttribute("currencyID");

                decimal shippingCharge;
                decimal.TryParse(xe["ShippingServiceSelected"]["ShippingServiceCost"] != null ? xe["ShippingServiceSelected"]["ShippingServiceCost"].InnerText : "0", out shippingCharge);
                oh.shipping_charge = shippingCharge;

                decimal orderTaxPercent = 0M;
                if (xe["ShippingDetails"]["SalesTax"]["SalesTaxPercent"] != null)
                {
                    decimal.TryParse(xe["ShippingDetails"]["SalesTax"]["SalesTaxPercent"].InnerText, out orderTaxPercent);
                }
                //if (xe["ShippingDetails"]["SalesTax"]["ShippingIncludedInTax"].InnerText == "true")
                if (orderTaxPercent > 0)
                {
                    if ((state.gst == null || state.gst == 0)
                        && orderTaxPercent > 0M)
                    {
                        state.gst = 5;
                        state.pst = sbyte.Parse(((int)(orderTaxPercent - 5M)).ToString());
                        context.SaveChanges();
                    }

                    if (xe["ShippingDetails"]["SalesTax"]["SalesTaxAmount"] != null)
                    {
                        oh.tax_charge = decimal.Parse(xe["ShippingDetails"]["SalesTax"]["SalesTaxAmount"].InnerText);
                    }
                    else
                    {
                        oh.tax_charge = 0M;
                    }
                    oh.pst = oh.tax_charge - oh.tax_charge / (state.gst + state.pst) * state.gst;
                    oh.pst_rate = state.pst;
                    oh.gst = oh.tax_charge - oh.pst;
                    oh.gst_rate = state.gst;
                }
                oh.taxable_total = oh.sub_total + oh.shipping_charge;

                oh.is_lock_tax_change = true;
                //context.AddTotb_order_helper(oh);
                context.tb_order_helper.Add(oh);
                context.SaveChanges();

                //var csm = tb_customer_store.Createtb_customer_store(1, 0);
                var csm = new tb_customer_store
                {
                    news_latter_subscribe = 1
                };
                csm.busniess_website = cs.busniess_website;
                csm.card_verification_number = cs.card_verification_number;
                csm.create_datetime = DateTime.Now;
                csm.customer_address1 = cs.customer_address1;
                csm.customer_business_address = cs.customer_business_address;
                csm.customer_business_city = cs.customer_business_city;

                csm.customer_country = cs.customer_country;
                csm.shipping_country_code = cs.shipping_country_code;
                csm.customer_country_code = cs.customer_country_code;
                csm.customer_shipping_country = cs.customer_shipping_country;
                csm.customer_business_country_code = cs.customer_business_country_code;
                csm.customer_shipping_state = cs.customer_shipping_state;
                csm.shipping_state_code = cs.shipping_state_code;

                csm.customer_business_state_code = cs.customer_business_state_code;

                csm.customer_business_telephone = cs.customer_business_telephone;
                csm.customer_business_zip_code = cs.customer_business_zip_code;

                csm.customer_card_billing_shipping_address = cs.customer_card_billing_shipping_address;
                csm.customer_card_city = cs.customer_card_city;
                csm.customer_card_country = cs.customer_card_country;
                csm.customer_card_country_code = cs.customer_card_country_code;
                csm.customer_card_first_name = cs.customer_card_first_name;
                csm.customer_card_issuer = cs.customer_card_issuer;
                csm.customer_card_last_name = cs.customer_card_last_name;
                csm.customer_card_phone = cs.customer_card_phone;
                csm.customer_card_state = cs.customer_card_state;
                csm.customer_card_state_code = cs.customer_card_state_code;
                csm.customer_card_type = cs.customer_card_type;
                csm.customer_card_zip_code = cs.customer_card_zip_code;
                csm.customer_city = cs.customer_city;
                csm.customer_comment_note = cs.customer_comment_note;
                csm.customer_company = cs.customer_company;
                csm.customer_credit_card = "";
                csm.customer_email1 = cs.customer_email1;
                csm.customer_email2 = cs.customer_email2;
                csm.customer_expiry = cs.customer_expiry;
                csm.customer_fax = cs.customer_fax;
                csm.customer_first_name = cs.customer_first_name;
                csm.customer_last_name = cs.customer_last_name;
                csm.customer_login_name = cs.customer_login_name;
                csm.customer_note = cs.customer_note;
                csm.customer_password = cs.customer_password;
                csm.customer_rumor = cs.customer_rumor;
                csm.customer_serial_no = cs.customer_serial_no;
                csm.customer_shipping_address = cs.customer_shipping_address;
                csm.customer_shipping_city = cs.customer_shipping_city;
                csm.customer_shipping_zip_code = cs.customer_shipping_zip_code;
                csm.customer_shipping_first_name = cs.customer_shipping_first_name;
                csm.customer_shipping_last_name = cs.customer_shipping_last_name;
                csm.EBay_ID = cs.EBay_ID;
                csm.is_old = cs.is_old;
                csm.my_purchase_order = cs.my_purchase_order;
                csm.news_latter_subscribe = 1;
                csm.order_code = orderCode;
                if (paymeth > 0)
                    csm.pay_method = paymeth;

                csm.phone_c = cs.phone_c;
                csm.phone_d = cs.phone_d;
                csm.phone_n = cs.phone_n;
                csm.source = cs.source;
                csm.store_create_datetime = DateTime.Now;
                csm.system_category_serial_no = cs.system_category_serial_no;
                csm.tag = 1;
                csm.tax_execmtion = cs.tax_execmtion;
                csm.zip_code = cs.zip_code;

                //context.AddTotb_customer_store(csm);
                context.tb_customer_store.Add(csm);
                context.SaveChanges();

                foreach (XmlElement OrderPart in xe["TransactionArray"].ChildNodes)
                {
                    tb_order_ebay oem = new tb_order_ebay
                    {
                        sales_record_number = int.Parse(OrderPart["ShippingDetails"]["SellingManagerSalesRecordNumber"].InnerText)
                    };
                    oem.regdate = DateTime.Now;
                    oem.sales_record_number = int.Parse(OrderPart["ShippingDetails"]["SellingManagerSalesRecordNumber"].InnerText); ;
                    oem.order_code = orderCode;
                    oem.buyer_address1 = xe["ShippingAddress"]["Street1"].InnerText;
                    oem.buyer_address2 = xe["ShippingAddress"]["Street2"].InnerText;
                    oem.buyer_city = xe["ShippingAddress"]["CityName"].InnerText;
                    oem.buyer_country = countryNameE;
                    oem.buyer_email = xe["TransactionArray"]["Transaction"]["Buyer"]["Email"].InnerText;
                    oem.buyer_fullname = xe["ShippingAddress"]["Name"].InnerText;
                    oem.buyer_phone_number = xe["ShippingAddress"]["Phone"].InnerText;
                    oem.buyer_postal_code = xe["ShippingAddress"]["PostalCode"].InnerText;
                    oem.buyer_province = xe["ShippingAddress"]["StateOrProvince"].InnerText;
                    oem.cash_on_delivery_fee = 0M;
                    oem.cash_on_delivery_fee_unit = "";
                    oem.cash_on_delivery_option = "";
                    oem.checkout_date = DateTime.Parse(xe["CreatedTime"].InnerText.Replace("T", " ").Replace("Z", " "));
                    oem.custom_label = OrderPart["Item"]["SKU"] != null ? OrderPart["Item"]["SKU"].InnerText : "";
                    oem.feedback_left = "";
                    oem.feedback_received = "";
                    oem.insurance = 0M;
                    oem.insurance_unit = "";
                    oem.item_number = OrderPart["Item"]["ItemID"].InnerText;
                    oem.item_title = OrderPart["Item"]["Title"].InnerText;
                    oem.notes_to_yourself = "";

                    oem.order_id = xe["OrderID"].InnerText;
                    try
                    {
                        oem.paid_on_date = DateTime.Parse(xe["PaidTime"].InnerText.Replace("T", " ").Replace("Z", " "));
                    }
                    catch { oem.paid_on_date = DateTime.Now; }

                    oem.payment_method = xe["CheckoutStatus"]["PaymentMethod"].InnerText;
                    oem.paypal_transaction_id = OrderPart["TransactionID"].InnerText;
                    oem.quantity = short.Parse(OrderPart["QuantityPurchased"].InnerText);
                    //oem.regdate = DateTime.Parse(Util.DateTimeFormat.ToDateTimeString(m.Regdate));
                    oem.sale_date = DateTime.Parse(xe["CreatedTime"].InnerText.Replace("T", " ").Replace("Z", " "));
                    oem.sale_price = decimal.Parse(xe["AmountPaid"].InnerText);
                    oem.sale_price_unit = xe["AmountPaid"].GetAttribute("currencyID");
                    //oem.shipped_on_date = "";
                    if (xe["ShippingServiceSelected"]["ShippingServiceCost"] != null)
                    {
                        oem.shipping_and_handling = decimal.Parse(xe["ShippingServiceSelected"]["ShippingServiceCost"].InnerText);
                        oem.shipping_and_handling_unit = xe["ShippingServiceSelected"]["ShippingServiceCost"].GetAttribute("currencyID");
                    }
                    else
                    {
                        oem.shipping_and_handling = 0M; ;
                        oem.shipping_and_handling_unit = xe["Total"].GetAttribute("currencyID");
                    }
                    oem.shipping_service = xe["ShippingServiceSelected"]["ShippingService"].InnerText;
                    oem.total_price = decimal.Parse(xe["Total"].InnerText);
                    oem.total_price_unit = xe["Total"].GetAttribute("currencyID");
                    oem.transaction_id = OrderPart["TransactionID"].InnerText;
                    oem.user_id = xe["BuyerUserID"].InnerText;
                    //context.AddTotb_order_ebay(oem);
                    context.tb_order_ebay.Add(oem);
                    context.SaveChanges();

                    // tb_order_product op = tb_order_product.Createtb_order_product(0, 1);
                    var op = new tb_order_product
                    {
                        tag = 1
                    };
                    op.product_name = oem.item_title;
                    op.order_code = oh.order_code.ToString();

                    tb_ebay_code_and_luc_sku ecals = context.tb_ebay_code_and_luc_sku.FirstOrDefault(c => c.ebay_code.Equals(oem.item_number));

                    if (ecals == null)
                    {
                        string cusml_label = OrderPart["Item"]["SKU"] == null ? "" : OrderPart["Item"]["SKU"].InnerText;
                        var skuArray = cusml_label.Trim().Split(new char[] { ' ' });
                        var tempsku = skuArray[skuArray.Length - 1];
                        if (tempsku.Length == 6)
                        {

                            //  ecals = tb_ebay_code_and_luc_sku.Createtb_ebay_code_and_luc_sku(0);
                            ecals = new tb_ebay_code_and_luc_sku
                            {

                            };
                            ecals.BuyItNowPrice = decimal.Parse(OrderPart["TransactionPrice"].InnerText);
                            ecals.ebay_code = oem.item_number;
                            ecals.is_online = true;
                            ecals.is_sys = true;
                            int nSku;
                            int.TryParse(cusml_label.Split(new char[] { '|' })[0].Substring(4), out nSku);
                            ecals.SKU = nSku;
                            ecals.regdate = DateTime.Now;
                        }
                        else
                        {
                            decimal buyItNewPrice;
                            decimal.TryParse(OrderPart["TransactionPrice"].InnerText, out buyItNewPrice);

                            //tb_product pm = tb_product.Createtb_product(0);
                            var pm = new tb_product();
                            pm.menu_child_serial_no = 339; // other category id.
                            pm.product_short_name = oem.item_title;
                            pm.product_name = oem.item_title;
                            pm.product_img_sum = 1;
                            pm.product_name_long_en = oem.item_title;
                            pm.product_order = 1;
                            pm.is_non = 0;
                            pm.last_regdate = DateTime.Now;
                            pm.@new = 1;
                            pm.other_product_sku = 999999;
                            pm.product_current_cost = buyItNewPrice;
                            pm.product_current_price = buyItNewPrice;
                            pm.product_current_special_cash_price = buyItNewPrice;
                            pm.regdate = DateTime.Now;
                            pm.split_line = 0;
                            pm.tag = 1;
                            pm.issue = false;
                            pm.adjustment_enddate = DateTime.Now;
                            pm.real_cost_regdate = DateTime.Now;
                            pm.adjustment_regdate = DateTime.Now;
                            pm.product_size_id = 1;
                            pm.prodType = "NEW";
                            //context.AddTotb_product(pm);
                            context.tb_product.Add(pm);
                            context.SaveChanges();

                            //  ecals = tb_ebay_code_and_luc_sku.Createtb_ebay_code_and_luc_sku(0);
                            ecals = new tb_ebay_code_and_luc_sku();
                            ecals.BuyItNowPrice = buyItNewPrice;
                            ecals.ebay_code = oem.item_number;
                            ecals.is_online = true;
                            ecals.is_sys = false;
                            ecals.SKU = pm.product_serial_no;
                            ecals.regdate = DateTime.Now;
                        }
                        //context.AddTotb_ebay_code_and_luc_sku(ecals);
                        context.tb_ebay_code_and_luc_sku.Add(ecals);
                        context.SaveChanges();


                    }

                    if (ecals != null)
                    {
                        int sku;
                        int.TryParse(ecals.SKU.ToString(), out sku);
                        if (ecals.is_sys.HasValue && ecals.is_sys.Value)
                        {
                            tb_ebay_system_and_category esc = context.tb_ebay_system_and_category.FirstOrDefault(ec => ec.SystemSku == sku);
                            int newCode = DBProvider.NewSysCode();
                            op.ebayItemID = oem.item_number;
                            op.menu_child_serial_no = esc != null ? esc.eBaySysCategoryID : 0;// prod.menu_child_serial_no;
                            // op.menu_pre_serial_no =

                            op.order_product_cost = DBProvider.GeteBaySysCost(sku);
                            op.order_product_price = decimal.Parse(OrderPart["TransactionPrice"].InnerText);
                            op.order_product_sold = decimal.Parse(OrderPart["TransactionPrice"].InnerText);
                            op.order_product_sum = oem.quantity;
                            op.prodType = OrderPart["Item"]["ConditionDisplayName"].InnerText.ToLower().IndexOf("new") > -1 ? "NEW" : "Refurbished";
                            op.product_current_price_rate = op.order_product_sold;
                            op.product_name = oem.item_title;
                            op.product_serial_no = newCode;
                            op.product_type = 2; // 1 part, 2 system, 3 notebook
                            op.product_type_name = "System";// unit, notebook, system ;
                            op.save_price = 0;
                            op.sku = sku.ToString();
                            // context.AddTotb_order_product(op);
                            context.tb_order_product.Add(op);
                            context.SaveChanges();

                            IQueryable<tb_ebay_system_parts> sysList = context.tb_ebay_system_parts.Where(s => s.system_sku == sku && s.is_belong_price == true);

                            for (int i = 0; i < sysList.ToList().Count; i++)
                            {
                                var m = sysList.ToList()[i];
                                int commentID;
                                int.TryParse(m.comment_id.ToString(), out commentID);
                                tb_ebay_system_part_comment espc = context.tb_ebay_system_part_comment.FirstOrDefault(e => e.id.Equals(commentID));
                                tb_product prod = context.tb_product.FirstOrDefault(p => p.product_serial_no == m.luc_sku);


                                //tb_order_product_sys_detail sysDetail = tb_order_product_sys_detail.Createtb_order_product_sys_detail(0);
                                var sysDetail = new tb_order_product_sys_detail();
                                sysDetail.cate_name = espc != null ? espc.comment : "";
                                sysDetail.ebay_number = oem.item_number;
                                sysDetail.is_lock = false;
                                sysDetail.old_price = 0M;
                                sysDetail.part_group_id = m.part_group_id;
                                sysDetail.part_max_quantity = m.max_quantity;
                                sysDetail.part_quantity = m.part_quantity;
                                sysDetail.product_current_cost = prod != null ? prod.product_current_cost : 0M;
                                sysDetail.product_current_price = prod != null ? prod.product_current_price : 0M;
                                sysDetail.product_current_price_rate = 1;
                                sysDetail.product_current_sold = prod != null ? prod.product_current_price : 0M;
                                sysDetail.product_name = prod != null ? prod.product_ebay_name : "";
                                sysDetail.product_order = m.id;
                                sysDetail.product_serial_no = m.luc_sku;
                                sysDetail.save_price = 0M;
                                sysDetail.sys_tmp_code = newCode.ToString();
                                sysDetail.system_product_serial_no = sku;
                                sysDetail.system_templete_serial_no = newCode;

                                // context.AddTotb_order_product_sys_detail(sysDetail);
                                context.tb_order_product_sys_detail.Add(sysDetail);
                                context.SaveChanges();
                            }
                        }
                        else
                        {
                            tb_product prod = context.tb_product.FirstOrDefault(p => p.product_serial_no.Equals(sku));
                            if (prod != null)
                            {
                                tb_product_category pc = context.tb_product_category.FirstOrDefault(p => p.menu_child_serial_no == prod.menu_child_serial_no);

                                op.ebayItemID = oem.item_number;
                                op.menu_child_serial_no = prod.menu_child_serial_no;
                                // op.menu_pre_serial_no =
                                // op.order_code = oh.order_code.ToString();
                                op.order_product_cost = prod.product_current_cost;
                                op.order_product_price = decimal.Parse(OrderPart["TransactionPrice"].InnerText);
                                op.order_product_sold = decimal.Parse(OrderPart["TransactionPrice"].InnerText);
                                op.order_product_sum = oem.quantity;
                                op.prodType = OrderPart["Item"]["ConditionDisplayName"].InnerText.ToLower().IndexOf("new") > -1 ? "NEW" : "Refurbished";
                                op.product_current_price_rate = op.order_product_sold;
                                op.product_name = oem.item_title;
                                op.product_serial_no = sku;
                                op.product_type = pc.is_noebook == 1 ? 3 : 1; // 1 part, 2 system, 3 notebook
                                op.product_type_name = pc.is_noebook == 1 ? "Notebook" : "Unit";// unit, notebook, system ;
                                op.save_price = 0;
                                op.sku = sku.ToString();
                                // context.AddTotb_order_product(op);
                                context.tb_order_product.Add(op);
                                context.SaveChanges();

                                // 改变库存
                                ChangeEBayStockQuantity.Run(ecals.SKU.HasValue ? ecals.SKU.Value : 0);
                            }
                        }
                    }
                }
                BLL.SendMail.Send(orderCode);
            }
        }
        /// <summary>
        /// Get eBay active Items.
        /// </summary>
        /// <param name="page"></param>
        /// <param name="the"></param>
        static string LoadEbayOrder(DateTime beginDt, DateTime endDt, string orderId)
        {
            #region settings
            string devID = Config.devID;
            string appID = Config.appID;
            string certID = Config.certID;

            //Get the Server to use (Sandbox or Production)
            string serverUrl = Config.serverUrl;

            //Get the User Token to Use
            string userToken = Config.userToken;

            //SiteID = 0  (US) - UK = 3, Canada = 2, Australia = 15, ....
            //SiteID Indicates the eBay site to associate the call with
            int siteID = Config.siteID;
            #endregion

            #region Load The XML Document Template and Set the Neccessary Values
            //Load the XML Document to Use for this Request
            XmlDocument xmlDoc = new XmlDocument();

            ////Get XML Document from Embedded  Resources
            //xmlDoc.Load((Server.MapPath("/q_admin/ebayMaster/Online/Xml/GeteBayOfficialTimeRequest.xml")));

            ////Set the various node values   attr1858_26443
            //xmlDoc["GeteBayOfficialTimeRequest"]["RequesterCredentials"]["eBayAuthToken"].InnerText = userToken;

            string sendXml = string.Format(@"<?xml version=""1.0"" encoding=""utf-8""?>
<GetOrdersRequest xmlns=""urn:ebay:apis:eBLBaseComponents"">
  <RequesterCredentials>
    <eBayAuthToken>{0}</eBayAuthToken>
  </RequesterCredentials>
  {1}
  <OrderRole>Seller</OrderRole>
  <OrderStatus>All</OrderStatus>
</GetOrdersRequest>
", userToken
 , orderId == ""
    ? string.Format("<CreateTimeFrom>{0}T00:00:00.000Z</CreateTimeFrom><CreateTimeTo>{1}T00:00:00.000Z</CreateTimeTo>", beginDt.ToString("yyyy-MM-dd"), endDt.AddDays(1).ToString("yyyy-MM-dd"))
    : string.Format("<OrderIDArray><OrderID>{0}</OrderID></OrderIDArray>", orderId)
    );

            xmlDoc.LoadXml(sendXml);
            //Get XML into a string for use in encoding
            string xmlText = xmlDoc.InnerXml;
            //eBay.Service.Call.AddItemCall aic = new eBay.Service.Call.AddItemCall();


            //Put the data into a UTF8 encoded  byte array
            UTF8Encoding encoding = new UTF8Encoding();
            int dataLen = encoding.GetByteCount(xmlText);
            byte[] utf8Bytes = new byte[dataLen];
            Encoding.UTF8.GetBytes(xmlText, 0, xmlText.Length, utf8Bytes, 0);
            #endregion

            #region Setup The Request (inc. HTTP Headers
            //Create a new HttpWebRequest object for the ServerUrl
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(serverUrl);

            //Set Request Method (POST) and Content Type (text/xml)
            request.Method = "POST";
            request.ContentType = "text/xml";
            request.ContentLength = utf8Bytes.Length;

            //Add the Keys to the HTTP Headers
            request.Headers.Add("X-EBAY-API-DEV-NAME: " + devID);
            request.Headers.Add("X-EBAY-API-APP-NAME: " + appID);
            request.Headers.Add("X-EBAY-API-CERT-NAME: " + certID);

            //Add Compatability Level to HTTP Headers
            //Regulates versioning of the XML interface for the API
            request.Headers.Add("X-EBAY-API-COMPATIBILITY-LEVEL: 735");

            //Add function name, SiteID and Detail Level to HTTP Headers
            request.Headers.Add("X-EBAY-API-CALL-NAME:GetOrders");
            request.Headers.Add("X-EBAY-API-SITEID: " + siteID.ToString());

            //Time out = 15 seconds,  set to -1 for no timeout.
            //If times-out - throws a WebException with the
            //Status property set to WebExceptionStatus.Timeout.
            request.Timeout = 150000;

            #endregion

            #region Send The Request
            Stream str = null;
            try
            {
                //Set the request Stream
                str = request.GetRequestStream();
                //Write the equest to the Request Steam
                str.Write(utf8Bytes, 0, utf8Bytes.Length);
                str.Close();
                //Get response into stream
                WebResponse resp = request.GetResponse();
                str = resp.GetResponseStream();
            }
            catch (WebException wEx)
            {
                //Error has occured whilst requesting
                //Display error message and exit.
                if (wEx.Status == WebExceptionStatus.Timeout)
                    throw new Exception("Request Timed-Out.");
                else
                    throw new Exception(wEx.Message);

                //MessageBox.Show("Press Enter to Continue...\r\n" + wEx.Message);

            }
            #endregion

            #region Process Response
            // Get Response into String
            StreamReader sr = new StreamReader(str);
            string sssss = sr.ReadToEnd();
            xmlDoc.LoadXml(sssss);
            sr.Close();
            str.Close();

            if (!Directory.Exists(BLL.Variable.storePath))
            {
                Directory.CreateDirectory(BLL.Variable.storePath);
            }
            string filefullname = BLL.Variable.storePath + "\\_eBayOrders.xml";
            if (File.Exists(filefullname))
            {
                File.Delete(filefullname);
            }
            System.IO.StreamWriter sw = new System.IO.StreamWriter(filefullname);
            sw.Write(sssss);
            sw.Close();
            sw.Dispose();
            return sssss;
            #endregion
        }

        static string LoadEBayOrderPaypalTransaction()
        {
            #region settings
            string devID = Config.devID;
            string appID = Config.appID;
            string certID = Config.certID;

            //Get the Server to use (Sandbox or Production)
            string serverUrl = Config.serverUrl;

            //Get the User Token to Use
            string userToken = Config.userToken;

            //SiteID = 0  (US) - UK = 3, Canada = 2, Australia = 15, ....
            //SiteID Indicates the eBay site to associate the call with
            int siteID = Config.siteID;
            #endregion

            #region Load The XML Document Template and Set the Neccessary Values
            //Load the XML Document to Use for this Request
            XmlDocument xmlDoc = new XmlDocument();

            ////Get XML Document from Embedded  Resources
            //xmlDoc.Load((Server.MapPath("/q_admin/ebayMaster/Online/Xml/GeteBayOfficialTimeRequest.xml")));

            ////Set the various node values   attr1858_26443
            //xmlDoc["GeteBayOfficialTimeRequest"]["RequesterCredentials"]["eBayAuthToken"].InnerText = userToken;

            string sendXml = string.Format(@"<?xml version=""1.0"" encoding=""utf-8""?>
<GetSellerTransactionsRequest xmlns=""urn:ebay:apis:eBLBaseComponents"">
  <RequesterCredentials>
    <eBayAuthToken>{0}</eBayAuthToken>
  </RequesterCredentials>
  <IncludedContainingOrder>{1}</IncludedContainingOrder>
    <DetailLevel>ReturnAll</DetailLevel>
    {2}
</GetSellerTransactionsRequest>
"
                , userToken
                , "True"
                , Config.GetSellerTransactionsNumberOfDays == 0 ? "" : "<NumberOfDays>" + Config.GetSellerTransactionsNumberOfDays.ToString() + "</NumberOfDays>");

            xmlDoc.LoadXml(sendXml);
            //Get XML into a string for use in encoding
            string xmlText = xmlDoc.InnerXml;
            //eBay.Service.Call.AddItemCall aic = new eBay.Service.Call.AddItemCall();


            //Put the data into a UTF8 encoded  byte array
            UTF8Encoding encoding = new UTF8Encoding();
            int dataLen = encoding.GetByteCount(xmlText);
            byte[] utf8Bytes = new byte[dataLen];
            Encoding.UTF8.GetBytes(xmlText, 0, xmlText.Length, utf8Bytes, 0);
            #endregion

            #region Setup The Request (inc. HTTP Headers
            //Create a new HttpWebRequest object for the ServerUrl
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(serverUrl);

            //Set Request Method (POST) and Content Type (text/xml)
            request.Method = "POST";
            request.ContentType = "text/xml";
            request.ContentLength = utf8Bytes.Length;

            //Add the Keys to the HTTP Headers
            request.Headers.Add("X-EBAY-API-DEV-NAME: " + devID);
            request.Headers.Add("X-EBAY-API-APP-NAME: " + appID);
            request.Headers.Add("X-EBAY-API-CERT-NAME: " + certID);

            //Add Compatability Level to HTTP Headers
            //Regulates versioning of the XML interface for the API
            request.Headers.Add("X-EBAY-API-COMPATIBILITY-LEVEL: 735");

            //Add function name, SiteID and Detail Level to HTTP Headers
            request.Headers.Add("X-EBAY-API-CALL-NAME:GetSellerTransactions");
            request.Headers.Add("X-EBAY-API-SITEID: " + siteID.ToString());

            //Time out = 15 seconds,  set to -1 for no timeout.
            //If times-out - throws a WebException with the
            //Status property set to WebExceptionStatus.Timeout.
            request.Timeout = 150000;

            #endregion

            #region Send The Request
            Stream str = null;
            try
            {
                //Set the request Stream
                str = request.GetRequestStream();
                //Write the equest to the Request Steam
                str.Write(utf8Bytes, 0, utf8Bytes.Length);
                str.Close();
                //Get response into stream
                WebResponse resp = request.GetResponse();
                str = resp.GetResponseStream();
            }
            catch (WebException wEx)
            {
                //Error has occured whilst requesting
                //Display error message and exit.
                if (wEx.Status == WebExceptionStatus.Timeout)
                    throw new Exception("Request Timed-Out.");
                else
                    throw new Exception(wEx.Message);

                //MessageBox.Show("Press Enter to Continue...\r\n" + wEx.Message);

            }
            #endregion

            #region Process Response
            // Get Response into String
            StreamReader sr = new StreamReader(str);
            string sssss = sr.ReadToEnd();
            xmlDoc.LoadXml(sssss);
            sr.Close();
            str.Close();

            if (!Directory.Exists(BLL.Variable.storePath))
                Directory.CreateDirectory(BLL.Variable.storePath);
            string filefullname = BLL.Variable.storePath + "\\ebayPaypalTransaction.xml";
            if (File.Exists(filefullname))
                File.Delete(filefullname);
            System.IO.StreamWriter sw = new System.IO.StreamWriter(filefullname);
            sw.Write(sssss);
            sw.Close();
            sw.Dispose();
            return sssss;
            #endregion
        }

        static void SavePaypalTransaction(nicklu2Entities context, string strXml)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(strXml);

            if (xmlDoc["GetSellerTransactionsResponse"]["Ack"].InnerText != "Success"
                && xmlDoc["GetSellerTransactionsResponse"]["Ack"].InnerText != "Warning")
                return;

            foreach (XmlElement xe in xmlDoc["GetSellerTransactionsResponse"]["TransactionArray"].ChildNodes)
            {
                int orderCode;
                int.TryParse(xe["ShippingDetails"]["SellingManagerSalesRecordNumber"].InnerText, out orderCode);

                string paypalTransactionID = "";
                string paypalDatetime = "";
                string paypalPaymentOrRefundAmount = "";
                string paypalPriceUnit = "";
                if (xe["ExternalTransaction"] != null)
                {
                    paypalTransactionID = xe["ExternalTransaction"]["ExternalTransactionID"].InnerText;
                    paypalDatetime = xe["ExternalTransaction"]["ExternalTransactionTime"].InnerText;
                    paypalPaymentOrRefundAmount = xe["ExternalTransaction"]["PaymentOrRefundAmount"].InnerText;
                    paypalPriceUnit = xe["ExternalTransaction"]["PaymentOrRefundAmount"].Attributes["currencyID"].Value;

                    decimal pay_cash;
                    decimal.TryParse(paypalPaymentOrRefundAmount, out pay_cash);
                    tb_order_pay_record opr = context.tb_order_pay_record.FirstOrDefault(p => p.order_code == orderCode && p.pay_cash == pay_cash);
                    if (opr == null)
                    {
                        //opr = tb_order_pay_record.Createtb_order_pay_record(0);
                        opr = new tb_order_pay_record();
                        opr.regdate = DateTime.Now;
                        opr.balance = 0M;
                        opr.order_code = orderCode;
                        opr.pay_cash = pay_cash;
                        opr.pay_regdate = DateTime.Parse(paypalDatetime.Replace("T", " ").Replace(".000Z", ""));
                        opr.regdate = opr.pay_regdate;
                        //context.AddTotb_order_pay_record(opr);
                        context.tb_order_pay_record.Add(opr);
                    }

                    tb_order_paypal_record opr2 = context.tb_order_paypal_record.FirstOrDefault(p => p.transaction == paypalTransactionID);
                    if (opr2 == null)
                    {
                        //opr2 = tb_order_paypal_record.Createtb_order_paypal_record(0);
                        opr2 = new tb_order_paypal_record();
                        opr2.transaction = paypalTransactionID;
                        opr2.order_code = orderCode;
                        opr2.regdate = opr.regdate.Value;
                        opr2.Amt = opr.pay_cash;
                        //context.AddTotb_order_paypal_record(opr2);
                        context.tb_order_paypal_record.Add(opr2);
                    }
                    context.SaveChanges();
                }
            }
        }
    }
}
