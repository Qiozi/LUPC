using PayPal.PayPalAPIInterfaceService;
using PayPal.PayPalAPIInterfaceService.Model;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CheckOutPaypal : PageBase
{
    public static Dictionary<string, string> GetConfig()
    {
        return PayPal.Manager.ConfigManager.Instance.GetProperties();
    }

    //public readonly static string ReturnUrl = "https://lucomputers.com/CheckOutPaypalCallBack.aspx";// System.Configuration.ConfigurationManager.AppSettings[""];
    //public readonly static string CancelUrl = "http://lucomputers.com/CheckOutPaypalCancelUrl.aspx";
    //public readonly static string SellerEmail = "wu.th@qq.com";
    //public string LogoUrl = "/images/logo1.png";
    //public string RedirectUrl = string.Empty;
    public string ReturnUrl;
    public string CancelUrl;
    public string LogoUrl;
    public string SellerEmail;
    public string RedirectUrl;
    void SetExpressCheckout()
    {
        string schemeHost = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);

        //Load values from web.config (configuration file)
        var config = GetConfig();
        ReturnUrl = config["ReturnUrl"];
        CancelUrl = config["CancelUrl"];
        LogoUrl = config["LogoUrl"];
        SellerEmail = config["SellerEmail"];
        RedirectUrl = config["RedirectUrl"];
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        SetExpressCheckout();
        if (!IsPostBack)
        {
            int paypalOrderCode = int.Parse(HttpContext.Current.Session["paypalOrderCode"].ToString());

            // var test = new NVPAPICaller();
            if (HttpContext.Current.Session["payment_amt"] != null && HttpContext.Current.Session["payment_unit"] != null)
            {
                var rate = new LU.BLL.PRateProvider(db);
                string amt = HttpContext.Current.Session["payment_amt"].ToString();
                string priceUnit = HttpContext.Current.Session["payment_unit"].ToString();

                // 创建新订单
                //OrderHelper.CopyToOrder(paypalOrderCode
                //              , CurrCustomer.customer_serial_no.Value
                //              , false
                //              , this.cookiesHelper.CurrSiteCountry
                //              , rate.PRate(db)
                //              , db);

                //    Response.Redirect("/checkout_paypal_website_payments_pro_setExpressCheckout.asp?paypal=true&orderCode=" + paypalOrderCode + "&amt=" + amt + "&unit=" + priceUnit);

                HttpContext CurrContext = HttpContext.Current;
                SetExpressCheckoutResponseType responseSetExpressCheckoutResponseType = new SetExpressCheckoutResponseType();

                string item_name = "";
                string item_id = "";
                string item_desc = "";
                string item_quantity = "";
                string item_amount = "";
                string tax_amount = "";
                string shipping_amount = "";
                string handling_amount = "";
                string shipping_discount_amount = "";
                string insurance_amount = "";
                string total_amount = "";
                string currency_code = "";
                string payment_type = "";

                // var customerStore = db.tb_customer_store.SingleOrDefault(p => p.order_code.HasValue && p.order_code.Value.Equals(paypalOrderCode));
                var oc = paypalOrderCode.ToString();
                var order = db.tb_cart_temp_price.FirstOrDefault(p => p.order_code.Equals(oc));
                var orderProds = db.tb_cart_temp.Where(p => p.cart_temp_code.HasValue && p.cart_temp_code.Value.Equals(paypalOrderCode)).ToList();
                AddressType shipToAddress = new AddressType();
                var ecMethod = "ShorcutExpressCheckout";
                if (ecMethod != null && ecMethod == "ShorcutExpressCheckout")
                {
                    // Get parameters from index page (shorcut express checkout)
                    item_name = "LU Computers Order: " + paypalOrderCode;// Request.Form["item_name"];
                    item_id = paypalOrderCode.ToString(); //Request.Form["item_id"];
                    item_desc = "LU Computers Order: " + paypalOrderCode;// Request.Form["item_desc"];
                    item_quantity = "1";// Request.Form["item_quantity"];
                    item_amount = order.sub_total.ToString();// customerStore Request.Form["item_amount"];
                    tax_amount = order.sales_tax.ToString();// order.ta.ToString();// Request.Form["tax_amount"];
                    shipping_amount = order.shipping_and_handling.ToString();// Request.Form["shipping_amount"];
                    handling_amount = "0";// Request.Form["handling_amount"];
                    shipping_discount_amount = "0";// Request.Form["shipping_discount_amount"];
                    insurance_amount = "0";// Request.Form["insurance_amount"];
                    total_amount = order.grand_total.ToString();// order.grand_total.ToString();// Request.Form["total_amount"];
                    currency_code = priceUnit;// Request.Form["currency_code_type"];
                    payment_type = "Sale";// Request.Form["payment_type"];
                    Session["Total_Amount"] = total_amount;
                }

                Session["SellerEmail"] = SellerEmail;
                CurrencyCodeType currencyCode_Type = (CurrencyCodeType)Enum.Parse(typeof(CurrencyCodeType), currency_code, true);
                Session["currency_code_type"] = currencyCode_Type;
                PaymentActionCodeType payment_ActionCode_Type = (PaymentActionCodeType)Enum.Parse(typeof(PaymentActionCodeType), payment_type, true);
                Session["payment_action_type"] = payment_ActionCode_Type;
                // SetExpressCheckoutRequestDetailsType object
                SetExpressCheckoutRequestDetailsType setExpressCheckoutRequestDetails = new SetExpressCheckoutRequestDetailsType();
                // (Required) URL to which the buyer's browser is returned after choosing to pay with PayPal.
                setExpressCheckoutRequestDetails.ReturnURL = ReturnUrl;
                //(Required) URL to which the buyer is returned if the buyer does not approve the use of PayPal to pay you
                setExpressCheckoutRequestDetails.CancelURL = CancelUrl;
                // A URL to your logo image. Use a valid graphics format, such as .gif, .jpg, or .png
                setExpressCheckoutRequestDetails.cppLogoImage = LogoUrl;
                // To display the border in your principal identifying color, set the "cppCartBorderColor" parameter to the 6-digit hexadecimal value of that color
                // setExpressCheckoutRequestDetails.cppCartBorderColor = "0000CD";






                //Add more items if necessary by using the class 'PaymentDetailsItemType'

                // Payment Information
                List<PaymentDetailsType> paymentDetailsList = new List<PaymentDetailsType>();

                PaymentDetailsType paymentDetails = new PaymentDetailsType();
                paymentDetails.PaymentAction = payment_ActionCode_Type;
                paymentDetails.ItemTotal = new BasicAmountType(currencyCode_Type, item_amount);//item amount
                paymentDetails.TaxTotal = new BasicAmountType(currencyCode_Type, tax_amount); //tax amount;
                paymentDetails.ShippingTotal = new BasicAmountType(currencyCode_Type, shipping_amount); //shipping amount
                paymentDetails.HandlingTotal = new BasicAmountType(currencyCode_Type, handling_amount); //handling amount
                paymentDetails.ShippingDiscount = new BasicAmountType(currencyCode_Type, shipping_discount_amount); //shipping discount
                paymentDetails.InsuranceTotal = new BasicAmountType(currencyCode_Type, insurance_amount); //insurance amount
                paymentDetails.OrderTotal = new BasicAmountType(currencyCode_Type, total_amount); // order total amount
                paymentDetails.NoteText = item_id;
                //Item details
                foreach (var part in orderProds)
                {
                    PaymentDetailsItemType itemDetails = new PaymentDetailsItemType();
                    itemDetails.Name = string.Concat("SKU:", part.product_serial_no);
                    itemDetails.Amount = new BasicAmountType(currencyCode_Type, part.price.ToString());
                    itemDetails.Quantity = part.cart_temp_Quantity.Value;
                    itemDetails.Description = part.product_name;
                    itemDetails.Number = string.Concat(item_id, "-", part.product_serial_no);
                    paymentDetails.PaymentDetailsItem.Add(itemDetails);
                }

                // Unique identifier for the merchant. 
                SellerDetailsType sellerDetails = new SellerDetailsType();
                sellerDetails.PayPalAccountID = SellerEmail;
                paymentDetails.SellerDetails = sellerDetails;

                paymentDetailsList.Add(paymentDetails);
                setExpressCheckoutRequestDetails.PaymentDetails = paymentDetailsList;

                // Collect Shipping details if MARK express checkout

                SetExpressCheckoutReq setExpressCheckout = new SetExpressCheckoutReq();
                SetExpressCheckoutRequestType setExpressCheckoutRequest = new SetExpressCheckoutRequestType(setExpressCheckoutRequestDetails);
                setExpressCheckout.SetExpressCheckoutRequest = setExpressCheckoutRequest;

                // Create the service wrapper object to make the API call
                PayPalAPIInterfaceServiceService service = new PayPalAPIInterfaceServiceService();

                // API call
                // Invoke the SetExpressCheckout method in service wrapper object
                responseSetExpressCheckoutResponseType = service.SetExpressCheckout(setExpressCheckout);
                if (responseSetExpressCheckoutResponseType != null)
                {
                    // Response envelope acknowledgement
                    string acknowledgement = "SetExpressCheckout API Operation - ";
                    acknowledgement += responseSetExpressCheckoutResponseType.Ack.ToString();
                    //logger.Debug(acknowledgement + "\n");
                    System.Diagnostics.Debug.WriteLine(acknowledgement + "\n");
                    // # Success values
                    if (responseSetExpressCheckoutResponseType.Ack.ToString().Trim().ToUpper().Equals("SUCCESS"))
                    {
                        // # Redirecting to PayPal for authorization
                        // Once you get the "Success" response, needs to authorise the
                        // transaction by making buyer to login into PayPal. For that,
                        // need to construct redirect url using EC token from response.
                        // Express Checkout Token
                        string EcToken = responseSetExpressCheckoutResponseType.Token;
                        // logger.Info("Express Checkout Token : " + EcToken + "\n");
                        System.Diagnostics.Debug.WriteLine("Express Checkout Token : " + EcToken + "\n");
                        // Store the express checkout token in session to be used in GetExpressCheckoutDetails & DoExpressCheckout API operations
                        Session["EcToken"] = EcToken;
                        Response.Redirect(RedirectUrl + HttpUtility.UrlEncode(EcToken), false);
                        Context.ApplicationInstance.CompleteRequest();
                    }
                    // # Error Values
                    else
                    {
                        List<ErrorType> errorMessages = responseSetExpressCheckoutResponseType.Errors;
                        string errorMessage = "";
                        foreach (ErrorType error in errorMessages)
                        {
                            // logger.Debug("API Error Message : " + error.LongMessage);
                            System.Diagnostics.Debug.WriteLine("API Error Message : " + error.LongMessage + "\n");
                            errorMessage = errorMessage + error.LongMessage;
                            var err = new LU.Data.tb_order_paypal_error_info
                            {
                                code = error.ErrorCode,
                                order_code = paypalOrderCode,
                                errItem = error.LongMessage,
                                errKey = string.Join(",", error.ErrorParameters),
                                regdate = DateTime.Now
                            };
                            db.tb_order_paypal_error_info.Add(err);
                        }
                        db.SaveChanges();
                        //Redirect to error page in case of any API errors
                        CurrContext.Items.Add("APIErrorMessage", errorMessage);
                        Response.Redirect("~/APIError.aspx?ordercode=" + paypalOrderCode);
                    }
                }

            }
            else
            {
                Response.Redirect("/APIError.aspx?ordercode=" + paypalOrderCode);
            }
        }
    }


}