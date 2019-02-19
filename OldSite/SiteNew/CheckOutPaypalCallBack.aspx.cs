using PayPal.PayPalAPIInterfaceService;
using PayPal.PayPalAPIInterfaceService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CheckOutPaypalCallBack : System.Web.UI.Page
{
    CookiesHelper cookiesHelper;
    LU.Data.nicklu2Entities DBContext = new LU.Data.nicklu2Entities();

    protected void Page_Load(object sender, EventArgs e)
    {
        HttpContext CurrContext = HttpContext.Current;
        cookiesHelper = new CookiesHelper(CurrContext);
        // Create the GetExpressCheckoutDetailsResponseType object
        GetExpressCheckoutDetailsResponseType responseGetExpressCheckoutDetailsResponseType = new GetExpressCheckoutDetailsResponseType();
        try
        {
            // Create the GetExpressCheckoutDetailsReq object
            GetExpressCheckoutDetailsReq getExpressCheckoutDetails = new GetExpressCheckoutDetailsReq();
            // A timestamped token, the value of which was returned by `SetExpressCheckout` API response
            string EcToken = (string)(Session["EcToken"]);
            GetExpressCheckoutDetailsRequestType getExpressCheckoutDetailsRequest = new GetExpressCheckoutDetailsRequestType(EcToken);
            getExpressCheckoutDetails.GetExpressCheckoutDetailsRequest = getExpressCheckoutDetailsRequest;
            // Create the service wrapper object to make the API call
            PayPalAPIInterfaceServiceService service = new PayPalAPIInterfaceServiceService();
            // # API call
            // Invoke the GetExpressCheckoutDetails method in service wrapper object
            responseGetExpressCheckoutDetailsResponseType = service.GetExpressCheckoutDetails(getExpressCheckoutDetails);
            if (responseGetExpressCheckoutDetailsResponseType != null)
            {
                // Response envelope acknowledgement
                string acknowledgement = "GetExpressCheckoutDetails API Operation - ";
                acknowledgement += responseGetExpressCheckoutDetailsResponseType.Ack.ToString();

                System.Diagnostics.Debug.WriteLine(acknowledgement + "\n");
                // # Success values
                if (responseGetExpressCheckoutDetailsResponseType.Ack.ToString().Trim().ToUpper().Equals("SUCCESS"))
                {
                    // Unique PayPal Customer Account identification number. This
                    // value will be null unless you authorize the payment by
                    // redirecting to PayPal after `SetExpressCheckout` call.
                    string PayerId = responseGetExpressCheckoutDetailsResponseType.GetExpressCheckoutDetailsResponseDetails.PayerInfo.PayerID;
                    // Store PayerId in session to be used in DoExpressCheckout API operation
                    Session["PayerId"] = PayerId;

                    List<PaymentDetailsType> paymentDetails = responseGetExpressCheckoutDetailsResponseType.GetExpressCheckoutDetailsResponseDetails.PaymentDetails;
                    foreach (PaymentDetailsType paymentdetail in paymentDetails)
                    {
                        AddressType ShippingAddress = paymentdetail.ShipToAddress;
                        string orderCode = responseGetExpressCheckoutDetailsResponseType.GetExpressCheckoutDetailsResponseDetails.PaymentDetails[0].NoteText;

                        var cid = LU.BLL.Users.UserToken.GetUserId(DBContext, cookiesHelper.UserToken);
                        var user = DBContext.tb_customer.SingleOrDefault(p => p.ID.Equals(cid));
                        var customerSerialNo = user.customer_serial_no.Value;

                        if (ShippingAddress != null)
                        {
                            Session["Address_Name"] = ShippingAddress.Name;
                            Session["Address_Street"] = ShippingAddress.Street1 + " " + ShippingAddress.Street2;
                            Session["Address_CityName"] = ShippingAddress.CityName;
                            Session["Address_StateOrProvince"] = ShippingAddress.StateOrProvince;
                            Session["Address_CountryName"] = ShippingAddress.CountryName;
                            Session["Address_PostalCode"] = ShippingAddress.PostalCode;

                            var customer = DBContext.tb_customer.Single(p => p.customer_serial_no.HasValue &&
                                p.customer_serial_no.Value.Equals(customerSerialNo));
                            customer.customer_shipping_first_name = ShippingAddress.Name;
                            customer.customer_shipping_address = ShippingAddress.Street1 + " " + ShippingAddress.Street2;
                            customer.customer_shipping_city = ShippingAddress.CityName;
                            customer.customer_shipping_zip_code = ShippingAddress.PostalCode;
                            customer.EBay_ID = responseGetExpressCheckoutDetailsResponseType.GetExpressCheckoutDetailsResponseDetails.PayerInfo.Payer;
                            customer.phone_d = responseGetExpressCheckoutDetailsResponseType.GetExpressCheckoutDetailsResponseDetails.PayerInfo.ContactPhone;

                            if (ShippingAddress.CountryName.ToLower() == "ca" ||
                                ShippingAddress.CountryName.ToLower() == "canada")
                            {
                                customer.customer_shipping_country = 1;
                                customer.shipping_country_code = "CA";
                            }
                            else
                            {
                                customer.customer_shipping_country = 2;
                                customer.shipping_country_code = "US";
                            }
                            if (!string.IsNullOrEmpty(ShippingAddress.StateOrProvince))
                            {
                                var state = DBContext.tb_state_shipping.FirstOrDefault(p => p.state_code.Equals(ShippingAddress.StateOrProvince) ||
                                    p.state_name.Equals(ShippingAddress.StateOrProvince));
                                if (state != null)
                                {
                                    customer.shipping_state_code = state.state_code;
                                    customer.customer_shipping_state = state.state_serial_no;

                                }
                            }
                            DBContext.SaveChanges();
                        }
                        Session["Currency_Code"] = paymentdetail.OrderTotal.currencyID;
                        Session["Order_Total"] = paymentdetail.OrderTotal.value;
                        Session["Shipping_Total"] = paymentdetail.ShippingTotal.value;
                        List<PaymentDetailsItemType> itemList = paymentdetail.PaymentDetailsItem;
                        foreach (PaymentDetailsItemType item in itemList)
                        {
                            Session["Product_Quantity"] = item.Quantity;
                            Session["Product_Name"] = item.Name;

                        }
                        Session["Total_Amount"] = Session["Order_Total"];
                    }
                }
                // # Error Values
                else
                {
                    List<ErrorType> errorMessages = responseGetExpressCheckoutDetailsResponseType.Errors;
                    string errorMessage = "";
                    foreach (ErrorType error in errorMessages)
                    {
                        // logger.Debug("API Error Message : " + error.LongMessage);
                        System.Diagnostics.Debug.WriteLine("API Error Message : " + error.LongMessage + "\n");
                        errorMessage = errorMessage + error.LongMessage;
                    }
                    //Redirect to error page in case of any API errors
                    CurrContext.Items.Add("APIErrorMessage", errorMessage);
                    // Server.Transfer("~/Response.aspx");
                }
            }
            //Redirect to DoExpressCheckoutPayment.aspx page if the method chosen is MarkExpressCheckout
            //The buyer need not review the shipping address and shipping method as it's already provided
            string ecMethod = (string)(Session["ExpressCheckoutMethod"]);
            //if (ecMethod.Equals("MarkExpressCheckout"))
            //{
            //    Response.Redirect("DoExpressCheckoutPayment.aspx", false);
            //    Context.ApplicationInstance.CompleteRequest();
            //}

        }
        // # Exception log
        catch (System.Exception ex)
        {
            // Log the exception message
            // logger.Debug("Error Message : " + ex.Message);
            System.Diagnostics.Debug.WriteLine("Error Message : " + ex.Message);
        }

        // show product info
        BindProdList();
    }

    void BindProdList()
    {
        #region prod list
        var cartTemp = DBContext.tb_cart_temp.Where(p => p.cart_temp_code.HasValue
            && p.cart_temp_code.Value.Equals(this.cookiesHelper.CurrOrderCode)).ToList();

        var haveSysProd = cartTemp.FirstOrDefault(p => p.product_serial_no.HasValue
            && p.product_serial_no.Value.ToString().Length == 8) != null;

        string prodListString = "<table class=\"table table-striped\" width='100%' id='tableProdList'><thead><tr><th></th><th>Description</th><th>QTY</th></thead><tbody>";

        foreach (var ct in cartTemp)
        {
            string logostr = "";

            #region 取得 logo 字符串
            if (ct.product_serial_no.Value.ToString().Length != 8)
            {
                // 零件 logo
                int partID = ct.product_serial_no.Value;

                var img = DBContext.tb_product.FirstOrDefault(p => p.product_serial_no.Equals(partID));
                if (img != null)
                {
                    logostr = "<img src='https://lucomputers.com/pro_img/COMPONENTS/" + (img.other_product_sku.Value > 0 ? img.other_product_sku.Value : img.product_serial_no) + "_t.jpg' alt='...' width='40'>";
                }
            }
            else
            {
                // 机箱 logo 
                string sysID = ct.product_serial_no.Value.ToString();
                var caseModel = DBContext.tb_sp_tmp_detail.FirstOrDefault(p => p.sys_tmp_code.Equals(sysID)
                    && p.cate_name.ToLower() == "case");
                if (caseModel != null)
                {
                    int caseid = caseModel.product_serial_no.Value;
                    var img = DBContext.tb_product.FirstOrDefault(p => p.product_serial_no.Equals(caseid));
                    if (img != null)
                    {
                        logostr = "<img src='https://lucomputers.com/pro_img/COMPONENTS/" + (img.other_product_sku.Value > 0 ? img.other_product_sku.Value : img.product_serial_no) + "_t.jpg' alt='...' width='40'>";
                    }
                }
            }
            #endregion

            prodListString += "<tr " + (haveSysProd ? " class='success'" : "") + "><td>" + logostr + "</td><td>" + ct.product_name + "</td><td width='50'>" + ct.cart_temp_Quantity + "</td></tr>";
            if (ct.product_serial_no.Value.ToString().Length == 8)
            {
                #region 系统列表明细
                string sysCode = ct.product_serial_no.Value.ToString();
                var sysPartList = (from sp in DBContext.tb_sp_tmp_detail
                                   join p in DBContext.tb_product on sp.product_serial_no.Value equals p.product_serial_no
                                   where sp.sys_tmp_code.Equals(sysCode)
                                   select new
                                   {
                                       PartSKU = p.product_serial_no,
                                       CommName = sp.cate_name,
                                       Title = sp.product_name,
                                       ImgSku = p.other_product_sku.Value > 0 ? p.other_product_sku.Value : p.product_serial_no
                                   }).ToList();

                string partDetailString = "<table class='table'>";
                for (int i = 0; i < sysPartList.Count; i++)
                {
                    if (sysPartList[i].PartSKU != setting.NoneSelectedID)
                    {
                        partDetailString += string.Format(@"<tr><td>{0}</td><td>{1}</td><td>{2}</td></tr>"
                            , "<img src='https://lucomputers.com/pro_img/COMPONENTS/" + sysPartList[i].ImgSku + "_t.jpg' alt='...' width='20'>"
                            , sysPartList[i].CommName
                            , sysPartList[i].Title);
                    }
                }
                partDetailString += "</table>";

                prodListString += string.Format(@"<tr>
                                                <td></td>
                                                <td colspan='4'>{0}</td>
                                                </tr>"
                    , partDetailString
                    );
                #endregion
            }
        }
        prodListString += "</table>";
        ltOrderProdList.Text = prodListString;

        #endregion
    }

    protected void btnNext_Click(object sender, EventArgs e)
    {

        Response.Redirect("/DoExpressCheckoutPayment.aspx", false);
        Context.ApplicationInstance.CompleteRequest();
    }
}