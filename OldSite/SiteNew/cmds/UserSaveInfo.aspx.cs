using LU.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class cmds_UserSaveInfo : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!IsLocalHostFrom || !IsLogin)
            {
                Response.Write("No...");
                Response.End();
            }
            string cmd = Util.GetStringSafeFromString(Page, "cmd");

            switch (cmd)
            {
                case "savePickup":
                    #region savePickup

                    var customer = CurrCustomer;
                    if (customer != null)
                    {
                        var statid = Util.GetInt32SafeFromString(Page, "stateProvince", 0);
                        var state = db.tb_state_shipping.FirstOrDefault(p => p.state_serial_no.Equals(statid));

                        if (Util.GetStringSafeFromString(Page, "Payment") != "22")
                        {
                            customer.customer_card_type = Util.GetStringSafeFromString(Page, "cardType");
                            customer.customer_credit_card = Util.GetStringSafeFromString(Page, "cardNumber");
                            customer.customer_expiry = Util.GetInt32SafeFromString(Page, "cartExpiryMonth", 0).ToString("00") +
                                Util.GetInt32SafeFromString(Page, "cartExpiryYear", 0).ToString("0000");
                            customer.customer_card_issuer = Util.GetStringSafeFromString(Page, "cardIssuer");
                            customer.customer_card_phone = Util.GetStringSafeFromString(Page, "cardTelephone");
                            customer.customer_card_first_name = Util.GetStringSafeFromString(Page, "firstName");
                            customer.customer_card_last_name = Util.GetStringSafeFromString(Page, "lastName");
                            customer.customer_card_billing_shipping_address = Util.GetStringSafeFromString(Page, "cardShippingAddress");
                            customer.customer_card_city = Util.GetStringSafeFromString(Page, "cardCity");
                            customer.customer_card_state = Util.GetInt32SafeFromString(Page, "stateProvince", 0);
                            customer.customer_card_state_code = state != null ? state.state_code : "";
                            customer.customer_card_zip_code = Util.GetStringSafeFromString(Page, "zipCode");
                            customer.card_verification_number = Util.GetStringSafeFromString(Page, "CardVerificationNumber");
                        }

                        customer.customer_first_name = Util.GetStringSafeFromString(Page, "firstName2");
                        customer.customer_last_name = Util.GetStringSafeFromString(Page, "lastName2");
                        customer.customer_shipping_first_name = customer.customer_first_name;
                        customer.customer_shipping_last_name = customer.customer_last_name;
                        customer.customer_email1 = Util.GetStringSafeFromString(Page, "email");
                        customer.phone_d = Util.GetStringSafeFromString(Page, "businessPhone");
                        customer.phone_n = Util.GetStringSafeFromString(Page, "homePhone");
                        customer.pay_method = Util.GetInt32SafeFromString(Page, "Payment", 21);
                        string pickupDate = Util.GetStringSafeFromString(Page, "pickupDate");
                        string pickupTime = string.Empty;
                        if (!string.IsNullOrEmpty(pickupDate))
                        {
                            if (pickupDate.IndexOf(" ") > -1)
                            {

                                pickupTime = pickupDate.Split(new char[] { ' ' })[1];
                                pickupDate = pickupDate.Split(new char[] { ' ' })[0];
                            }
                        }
                        //string pickupTime = string.IsNullOrEmpty(pickupDate) ? "" : (pickupDate.IndexOf(" ") > -1 ? pickupDate.Split(new char[] { ' ' })[1] : "");// Util.GetStringSafeFromString(Page, "pickupDpickupTimeate");
                        //pickupDate = string.IsNullOrEmpty(pickupDate) ? "" : (pickupDate.IndexOf(" ") > -1 ? pickupDate.Split(new char[] { ' ' })[1] : "");
                        if (pickupDate != "")
                        {
                            DateTime pickdt;
                            DateTime.TryParse(pickupDate, out pickdt);

                            DateTime.TryParse(pickdt.ToString("yyyy-MM-dd") + " " + (string.IsNullOrEmpty(pickupTime) ? "00" : pickupTime) + ":00", out pickdt);

                            var cartList = db.tb_cart_temp.Where(p => p.cart_temp_code.HasValue
                                && p.cart_temp_code.Value.Equals(this.cookiesHelper.CurrOrderCode)).ToList();
                            foreach (var c in cartList)
                            {
                                c.pick_datetime_1 = pickdt;
                                c.state_shipping = state.state_serial_no;
                            }
                            db.SaveChanges();

                        }
                        else
                        {
                            var cartList = db.tb_cart_temp.Where(p => p.cart_temp_code.HasValue
                               && p.cart_temp_code.Value.Equals(this.cookiesHelper.CurrOrderCode)).ToList();
                            foreach (var c in cartList)
                            {
                                c.state_shipping = state.state_serial_no;
                            }
                        }

                        OrderHelper.SaveOrderNote(Util.GetStringSafeFromString(Page, "comment"), this.cookiesHelper.CurrOrderCode, true, db);

                        db.SaveChanges();
                        Response.Write("OK");

                    }
                    else
                    {
                        Response.Write("no find user.");
                        Response.End();
                    }
                    #endregion
                    break;

                case "saveEmailTransfer":
                    #region save email transfer info

                    customer = CurrCustomer;

                    if (customer != null)
                    {
                        var statid = Util.GetInt32SafeFromString(Page, "stateProvince", 0);
                        var state = db.tb_state_shipping.FirstOrDefault(p => p.state_serial_no.Equals(statid));


                        customer.customer_shipping_first_name = Util.GetStringSafeFromString(Page, "firstName");
                        customer.customer_shipping_last_name = Util.GetStringSafeFromString(Page, "lastName");
                        customer.customer_shipping_address = Util.GetStringSafeFromString(Page, "address");
                        customer.customer_shipping_city = Util.GetStringSafeFromString(Page, "shippingCity");
                        customer.customer_shipping_state = Util.GetInt32SafeFromString(Page, "stateProvince", 0);

                        customer.shipping_state_code = state != null ? state.state_code : "";
                        customer.customer_shipping_zip_code = Util.GetStringSafeFromString(Page, "shippingZipCode");

                        customer.customer_email1 = Util.GetStringSafeFromString(Page, "email");
                        customer.phone_d = Util.GetStringSafeFromString(Page, "businessPhone");
                        customer.phone_n = Util.GetStringSafeFromString(Page, "homePhone");
                        customer.pay_method = 16;

                        OrderHelper.SaveOrderNote(Util.GetStringSafeFromString(Page, "comment"), this.cookiesHelper.CurrOrderCode, true, db);


                        var cartList = db.tb_cart_temp.Where(p => p.cart_temp_code.HasValue
                              && p.cart_temp_code.Value.Equals(this.cookiesHelper.CurrOrderCode)).ToList();
                        foreach (var c in cartList)
                        {
                            c.state_shipping = state.state_serial_no;
                        }

                        db.SaveChanges();

                        ChangeTaxRate(statid, this.cookiesHelper.CurrOrderCode.ToString());

                        Response.Write("OK");

                    }
                    else
                    {
                        Response.Write("no find user.");
                        Response.End();
                    }
                    #endregion
                    break;

                case "saveBankTransfer":
                    #region save Bank transfer info

                    customer = CurrCustomer;

                    if (customer != null)
                    {
                        var statid = Util.GetInt32SafeFromString(Page, "stateProvince", 0);
                        var state = db.tb_state_shipping.FirstOrDefault(p => p.state_serial_no.Equals(statid));


                        customer.customer_shipping_first_name = Util.GetStringSafeFromString(Page, "firstName");
                        customer.customer_shipping_last_name = Util.GetStringSafeFromString(Page, "lastName");
                        customer.customer_shipping_address = Util.GetStringSafeFromString(Page, "address");
                        customer.customer_shipping_city = Util.GetStringSafeFromString(Page, "shippingCity");
                        customer.customer_shipping_state = Util.GetInt32SafeFromString(Page, "stateProvince", 0);

                        customer.shipping_state_code = state != null ? state.state_code : "";
                        customer.customer_shipping_zip_code = Util.GetStringSafeFromString(Page, "shippingZipCode");

                        customer.customer_email1 = Util.GetStringSafeFromString(Page, "email");
                        customer.phone_d = Util.GetStringSafeFromString(Page, "businessPhone");
                        customer.phone_n = Util.GetStringSafeFromString(Page, "homePhone");
                        customer.pay_method = 17;
                        customer.tag = 1;

                        OrderHelper.SaveOrderNote(Util.GetStringSafeFromString(Page, "comment"), this.cookiesHelper.CurrOrderCode, true, db);

                        var cartList = db.tb_cart_temp.Where(p => p.cart_temp_code.HasValue
                             && p.cart_temp_code.Value.Equals(this.cookiesHelper.CurrOrderCode)).ToList();
                        foreach (var c in cartList)
                        {
                            c.state_shipping = state.state_serial_no;
                        }

                        db.SaveChanges();

                        ChangeTaxRate(statid, this.cookiesHelper.CurrOrderCode.ToString());

                        Response.Write("OK");

                    }
                    else
                    {
                        Response.Write("no find user.");
                        Response.End();
                    }
                    #endregion
                    break;

                case "saveMyAccount":
                    #region 保存 客户信息  myaccount 界面

                    customer = CurrCustomer;

                    if (customer != null)
                    {
                        customer.customer_first_name = Util.GetStringSafeFromString(Page, "FirstName");
                        customer.customer_last_name = Util.GetStringSafeFromString(Page, "lastName");
                        customer.phone_n = Util.GetStringSafeFromString(Page, "homePhone");
                        customer.phone_c = Util.GetStringSafeFromString(Page, "bussinessPhone");
                        customer.phone_d = Util.GetStringSafeFromString(Page, "mobilePhone");

                        int StatID;
                        int.TryParse(Util.GetStringSafeFromString(Page, "countryState"), out StatID);

                        if (StatID == 0)
                        {
                            var countryText = Util.GetStringSafeFromString(Page, "CountryText");
                            var stateText = Util.GetStringSafeFromString(Page, "StateText");

                            var state = StateHelper.GetState(countryText, stateText, db);

                            customer.state_serial_no = state.state_serial_no;
                            customer.customer_country = "3";
                            customer.customer_country_code = countryText;
                            customer.state_code = stateText;
                        }
                        else
                        {
                            var state = db.tb_state_shipping.FirstOrDefault(p => p.state_serial_no.Equals(StatID));
                            customer.state_serial_no = state.state_serial_no;
                            customer.customer_country = state.system_category_serial_no.Value.ToString();
                            customer.customer_country_code = state.Country.ToLower() == "canada" ? "CA" : "US";
                            customer.state_code = state.state_code;
                        }

                        customer.customer_address1 = Util.GetStringSafeFromString(Page, "Address");
                        customer.customer_city = Util.GetStringSafeFromString(Page, "city");
                        customer.zip_code = Util.GetStringSafeFromString(Page, "zip");
                        customer.customer_email1 = Util.GetStringSafeFromString(Page, "email1");
                        customer.customer_email2 = Util.GetStringSafeFromString(Page, "email2");

                        // shipping info
                        customer.customer_shipping_first_name = Util.GetStringSafeFromString(Page, "shippingFirstName");
                        customer.customer_shipping_last_name = Util.GetStringSafeFromString(Page, "shippingLastName");
                        customer.customer_shipping_address = Util.GetStringSafeFromString(Page, "shippingAddress");
                        customer.customer_shipping_city = Util.GetStringSafeFromString(Page, "shippingCity");
                        customer.customer_shipping_zip_code = Util.GetStringSafeFromString(Page, "shippingZip");


                        int.TryParse(Util.GetStringSafeFromString(Page, "ShippingCountryState"), out StatID);

                        if (StatID == 0)
                        {
                            var countryText = Util.GetStringSafeFromString(Page, "ShippingCountryText");
                            var stateText = Util.GetStringSafeFromString(Page, "ShippingStateText");

                            var state = StateHelper.GetState(countryText, stateText, db);
                            customer.customer_shipping_state = state.state_serial_no;
                            customer.customer_shipping_country = 3;
                            customer.shipping_country_code = countryText;
                            customer.shipping_state_code = stateText;
                        }
                        else
                        {
                            var state = db.tb_state_shipping.FirstOrDefault(p => p.state_serial_no.Equals(StatID));
                            customer.customer_shipping_state = state.state_serial_no;
                            customer.customer_shipping_country = state.system_category_serial_no.Value;
                            customer.shipping_country_code = state.Country.ToLower() == "canada" ? "CA" : "US";
                            customer.shipping_state_code = state.state_code;
                        }
                        ChangeTaxRate(StatID, this.cookiesHelper.CurrOrderCode.ToString());     // 变化订单 收税的洲值             

                        // business
                        customer.customer_company = Util.GetStringSafeFromString(Page, "CompanyName");
                        customer.customer_business_telephone = Util.GetStringSafeFromString(Page, "businessTelephone");
                        customer.customer_business_address = Util.GetStringSafeFromString(Page, "businessAddress");
                        customer.customer_business_zip_code = Util.GetStringSafeFromString(Page, "businessZip");
                        customer.customer_business_city = Util.GetStringSafeFromString(Page, "businessCity");
                        customer.tax_execmtion = Util.GetStringSafeFromString(Page, "businessTaxExectionNumber");
                        customer.busniess_website = Util.GetStringSafeFromString(Page, "website");

                        int.TryParse(Util.GetStringSafeFromString(Page, "BusinessCountryState"), out StatID);
                        if (StatID == 0)
                        {
                            var countryText = Util.GetStringSafeFromString(Page, "BusinessCountryText");
                            var stateText = Util.GetStringSafeFromString(Page, "BusinessStateText");

                            var state = StateHelper.GetState(countryText, stateText, db);
                            customer.customer_business_country_code = countryText;
                            customer.customer_business_state_code = stateText;
                        }
                        else
                        {
                            var state = db.tb_state_shipping.FirstOrDefault(p => p.state_serial_no.Equals(StatID));
                            customer.customer_business_country_code = state.Country.ToLower() == "canada" ? "CA" : "US";
                            customer.customer_business_state_code = state.state_code;
                        }

                        db.SaveChanges();

                        Response.Write("OK");

                    }
                    else
                    {
                        Response.Write("no find user.");
                        Response.End();
                    }
                    #endregion
                    break;
                case "saveCreditCard":
                    #region save credit card info

                    customer = CurrCustomer;

                    if (customer != null)
                    {
                        var statid = Util.GetInt32SafeFromString(Page, "stateProvince", 0);
                        var state = db.tb_state_shipping.FirstOrDefault(p => p.state_serial_no.Equals(statid));
                        var cardStateId = Util.GetInt32SafeFromString(Page, "cardState", 0);
                        var cardState = db.tb_state_shipping.FirstOrDefault(p => p.state_serial_no.Equals(cardStateId));

                        customer.customer_first_name = Util.GetStringSafeFromString(Page, "firstName");
                        customer.customer_last_name = Util.GetStringSafeFromString(Page, "lastName");
                        customer.customer_shipping_first_name = customer.customer_first_name;
                        customer.customer_shipping_last_name = customer.customer_last_name;
                        customer.customer_shipping_address = Util.GetStringSafeFromString(Page, "address");
                        customer.customer_shipping_country = 1;// Util.GetInt32SafeFromString(Page, "shippingcountry", 0);
                        customer.shipping_country_code = "Canada";
                        customer.customer_shipping_city = Util.GetStringSafeFromString(Page, "shippingCity");
                        customer.customer_shipping_state = statid;
                        customer.shipping_state_code = state.state_code;
                        customer.customer_shipping_zip_code = Util.GetStringSafeFromString(Page, "shippingZipCode");

                        customer.customer_card_first_name = Util.GetStringSafeFromString(Page, "cardFirstName");
                        customer.customer_card_last_name = Util.GetStringSafeFromString(Page, "cardLastName");
                        customer.customer_card_billing_shipping_address = Util.GetStringSafeFromString(Page, "cardBillingAddress");
                        customer.customer_card_country = 1;// Util.GetInt32SafeFromString(Page, "cardCountry", 0);
                        customer.customer_card_country_code = "Canada";
                        customer.customer_card_state = cardStateId;
                        customer.customer_card_state_code = cardState.state_code;
                        customer.customer_card_zip_code = Util.GetStringSafeFromString(Page, "cardZipcode");
                        customer.phone_d = Util.GetStringSafeFromString(Page, "cardTelephone");
                        customer.customer_card_type = Util.GetStringSafeFromString(Page, "CardType");
                        customer.customer_card_city = Util.GetStringSafeFromString(Page, "cardCity"); ;
                        customer.customer_credit_card = Util.GetStringSafeFromString(Page, "cardNumber");
                        customer.customer_expiry = Util.GetStringSafeFromString(Page, "card_expiry_month") + Util.GetStringSafeFromString(Page, "card_expiry_year");// Util.GetStringSafeFromString(Page, "cardExpiryDate");
                        customer.card_verification_number = Util.GetStringSafeFromString(Page, "cardVerificationNumber");
                        customer.customer_card_issuer = Util.GetStringSafeFromString(Page, "cardIssuingBank");
                        customer.customer_card_phone = Util.GetStringSafeFromString(Page, "cardIssuingTelephone");
                        customer.pay_method = 25;
                        OrderHelper.SaveOrderNote(Util.GetStringSafeFromString(Page, "notes"), this.cookiesHelper.CurrOrderCode, true, db);

                        var cartList = db.tb_cart_temp.Where(p => p.cart_temp_code.HasValue
                             && p.cart_temp_code.Value.Equals(this.cookiesHelper.CurrOrderCode)).ToList();
                        foreach (var c in cartList)
                        {
                            c.state_shipping = state.state_serial_no;
                        }
                        db.SaveChanges();
                        Response.Write("OK");

                    }
                    else
                    {
                        Response.Write("no find user.");
                        Response.End();
                    }
                    #endregion
                    break;

                case "saveCashDeposit":
                    #region save Bank transfer info

                    customer = CurrCustomer;

                    if (customer != null)
                    {
                        var statid = Util.GetInt32SafeFromString(Page, "stateProvince", 0);
                        var state = db.tb_state_shipping.FirstOrDefault(p => p.state_serial_no.Equals(statid));


                        customer.customer_shipping_first_name = Util.GetStringSafeFromString(Page, "firstName");
                        customer.customer_shipping_last_name = Util.GetStringSafeFromString(Page, "lastName");
                        customer.customer_shipping_address = Util.GetStringSafeFromString(Page, "address");
                        customer.customer_shipping_city = Util.GetStringSafeFromString(Page, "shippingCity");
                        customer.customer_shipping_state = Util.GetInt32SafeFromString(Page, "stateProvince", 0);

                        customer.shipping_state_code = state != null ? state.state_code : "";
                        customer.customer_shipping_zip_code = Util.GetStringSafeFromString(Page, "shippingZipCode");

                        customer.customer_email1 = Util.GetStringSafeFromString(Page, "email");
                        customer.phone_d = Util.GetStringSafeFromString(Page, "businessPhone");
                        customer.phone_n = Util.GetStringSafeFromString(Page, "homePhone");
                        customer.pay_method = 18;
                        customer.tag = 1;

                        OrderHelper.SaveOrderNote(Util.GetStringSafeFromString(Page, "comment"), this.cookiesHelper.CurrOrderCode, true, db);
                        var cartList = db.tb_cart_temp.Where(p => p.cart_temp_code.HasValue
                             && p.cart_temp_code.Value.Equals(this.cookiesHelper.CurrOrderCode)).ToList();
                        foreach (var c in cartList)
                        {
                            c.state_shipping = state.state_serial_no;
                        }
                        db.SaveChanges();

                        ChangeTaxRate(statid, this.cookiesHelper.CurrOrderCode.ToString());

                        Response.Write("OK");

                    }
                    else
                    {
                        Response.Write("no find user.");
                        Response.End();
                    }
                    #endregion
                    break;
                case "saveMoneyOrder":
                    #region save Bank transfer info

                    customer = CurrCustomer;

                    if (customer != null)
                    {
                        var statid = Util.GetInt32SafeFromString(Page, "stateProvince", 0);
                        var state = db.tb_state_shipping.FirstOrDefault(p => p.state_serial_no.Equals(statid));


                        customer.customer_shipping_first_name = Util.GetStringSafeFromString(Page, "firstName");
                        customer.customer_shipping_last_name = Util.GetStringSafeFromString(Page, "lastName");
                        customer.customer_shipping_address = Util.GetStringSafeFromString(Page, "address");
                        customer.customer_shipping_city = Util.GetStringSafeFromString(Page, "shippingCity");
                        customer.customer_shipping_state = Util.GetInt32SafeFromString(Page, "stateProvince", 0);

                        customer.shipping_state_code = state != null ? state.state_code : "";
                        customer.customer_shipping_zip_code = Util.GetStringSafeFromString(Page, "shippingZipCode");

                        customer.customer_email1 = Util.GetStringSafeFromString(Page, "email");
                        customer.phone_d = Util.GetStringSafeFromString(Page, "businessPhone");
                        customer.phone_n = Util.GetStringSafeFromString(Page, "homePhone");
                        customer.pay_method = 19;
                        customer.tag = 1;

                        OrderHelper.SaveOrderNote(Util.GetStringSafeFromString(Page, "comment"), this.cookiesHelper.CurrOrderCode, true, db);
                        var cartList = db.tb_cart_temp.Where(p => p.cart_temp_code.HasValue
                             && p.cart_temp_code.Value.Equals(this.cookiesHelper.CurrOrderCode)).ToList();
                        foreach (var c in cartList)
                        {
                            c.state_shipping = state.state_serial_no;
                        }
                        db.SaveChanges();

                        ChangeTaxRate(statid, this.cookiesHelper.CurrOrderCode.ToString());

                        Response.Write("OK");

                    }
                    else
                    {
                        Response.Write("no find user.");
                        Response.End();
                    }
                    #endregion
                    break;
                case "savePersonalCheckCompanyCheck":
                    #region save Bank transfer info

                    customer = CurrCustomer;

                    if (customer != null)
                    {
                        var statid = Util.GetInt32SafeFromString(Page, "stateProvince", 0);
                        var state = db.tb_state_shipping.FirstOrDefault(p => p.state_serial_no.Equals(statid));


                        customer.customer_shipping_first_name = Util.GetStringSafeFromString(Page, "firstName");
                        customer.customer_shipping_last_name = Util.GetStringSafeFromString(Page, "lastName");
                        customer.customer_shipping_address = Util.GetStringSafeFromString(Page, "address");
                        customer.customer_shipping_city = Util.GetStringSafeFromString(Page, "shippingCity");
                        customer.customer_shipping_state = Util.GetInt32SafeFromString(Page, "stateProvince", 0);

                        customer.shipping_state_code = state != null ? state.state_code : "";
                        customer.customer_shipping_zip_code = Util.GetStringSafeFromString(Page, "shippingZipCode");

                        customer.customer_email1 = Util.GetStringSafeFromString(Page, "email");
                        customer.phone_d = Util.GetStringSafeFromString(Page, "businessPhone");
                        customer.phone_n = Util.GetStringSafeFromString(Page, "homePhone");
                        customer.pay_method = 20;
                        customer.tag = 1;

                        OrderHelper.SaveOrderNote(Util.GetStringSafeFromString(Page, "comment"), this.cookiesHelper.CurrOrderCode, true, db);
                        var cartList = db.tb_cart_temp.Where(p => p.cart_temp_code.HasValue
                             && p.cart_temp_code.Value.Equals(this.cookiesHelper.CurrOrderCode)).ToList();
                        foreach (var c in cartList)
                        {
                            c.state_shipping = state.state_serial_no;
                        }
                        db.SaveChanges();

                        ChangeTaxRate(statid, this.cookiesHelper.CurrOrderCode.ToString());

                        Response.Write("OK");
                    }
                    else
                    {
                        Response.Write("no find user.");
                        Response.End();
                    }
                    #endregion
                    break;
            }
        }

        Response.End();
    }

    /// <summary>
    /// 当运输州改变的话，，，相应的税也改变
    /// 
    /// </summary>
    void ChangeTaxRate(int shippingStateID, string orderCode)
    {
        var stateModel = db.tb_state_shipping.FirstOrDefault(p => p.state_serial_no.Equals(shippingStateID));
        if (stateModel == null)
            return;

        var tempPrice = db.tb_cart_temp_price.FirstOrDefault(p => p.order_code.Equals(orderCode));
        if (tempPrice == null)
            return;

        tempPrice.gst = 0M;
        tempPrice.hst = 0M;
        tempPrice.pst = 0M;

        if (setting.HstStates.Contains(shippingStateID))
        {
            tempPrice.hst_rate = stateModel.gst + stateModel.pst;
            tempPrice.gst_rate = 0M;
            tempPrice.pst_rate = 0M;
        }
        else
        {
            tempPrice.gst_rate = stateModel.gst;
            tempPrice.pst_rate = stateModel.pst;
            tempPrice.hst = 0M;
        }

        db.SaveChanges();
    }
}