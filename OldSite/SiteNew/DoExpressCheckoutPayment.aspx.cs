using LU.BLL;
using PayPal.PayPalAPIInterfaceService;
using PayPal.PayPalAPIInterfaceService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DoExpressCheckoutPayment : System.Web.UI.Page
{
    LU.Data.nicklu2Entities DBContext = new LU.Data.nicklu2Entities();

    public static Dictionary<string, string> GetConfig()
    {
        return PayPal.Manager.ConfigManager.Instance.GetProperties();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            var config = GetConfig();
            var BNCode = config["SBN_CODE"];
            var cookiesHelper = new CookiesHelper(HttpContext.Current);
            var orderCode = cookiesHelper.CurrOrderCode;

            HttpContext CurrContext = HttpContext.Current;

            // Create the DoExpressCheckoutPaymentResponseType object
            DoExpressCheckoutPaymentResponseType responseDoExpressCheckoutPaymentResponseType = new DoExpressCheckoutPaymentResponseType();
            try
            {
                var rate = new LU.BLL.PRateProvider(DBContext);
                // create new order.
                var cid = LU.BLL.Users.UserToken.GetUserId(DBContext, cookiesHelper.UserToken);
                var user = DBContext.tb_customer.SingleOrDefault(p => p.ID.Equals(cid));
                OrderHelper.CopyToOrder(orderCode
                              , user.customer_serial_no.Value
                              , false
                              , cookiesHelper.CurrSiteCountry
                              , rate.PRate(DBContext)
                              , DBContext);
                var order = DBContext.tb_order_helper.Single(p => p.order_code.HasValue && p.order_code.Value.Equals(orderCode));
                order.tag = 0; // 完成支付发送后，再确认
                DBContext.SaveChanges();

                // Create the DoExpressCheckoutPaymentReq object
                DoExpressCheckoutPaymentReq doExpressCheckoutPayment = new DoExpressCheckoutPaymentReq();
                DoExpressCheckoutPaymentRequestDetailsType doExpressCheckoutPaymentRequestDetails = new DoExpressCheckoutPaymentRequestDetailsType();
                // The timestamped token value that was returned in the
                // `SetExpressCheckout` response and passed in the
                // `GetExpressCheckoutDetails` request.
                doExpressCheckoutPaymentRequestDetails.Token = (string)(Session["EcToken"]);
                // Unique paypal buyer account identification number as returned in
                // `GetExpressCheckoutDetails` Response
                doExpressCheckoutPaymentRequestDetails.PayerID = (string)(Session["PayerId"]);

                // # Payment Information
                // list of information about the payment
                List<PaymentDetailsType> paymentDetailsList = new List<PaymentDetailsType>();
                // information about the payment
                PaymentDetailsType paymentDetails = new PaymentDetailsType();
                CurrencyCodeType currency_code_type = (CurrencyCodeType)(Session["currency_code_type"]);
                PaymentActionCodeType payment_action_type = (PaymentActionCodeType)(Session["payment_action_type"]);
                //Pass the order total amount which was already set in session
                string total_amount = (string)(Session["Total_Amount"]);
                BasicAmountType orderTotal = new BasicAmountType(currency_code_type, total_amount);
                paymentDetails.OrderTotal = orderTotal;
                paymentDetails.PaymentAction = payment_action_type;

                //BN codes to track all transactions
                paymentDetails.ButtonSource = BNCode;

                // Unique identifier for the merchant. 
                SellerDetailsType sellerDetails = new SellerDetailsType();
                sellerDetails.PayPalAccountID = (string)(Session["SellerEmail"]);
                paymentDetails.SellerDetails = sellerDetails;

                paymentDetailsList.Add(paymentDetails);
                doExpressCheckoutPaymentRequestDetails.PaymentDetails = paymentDetailsList;

                DoExpressCheckoutPaymentRequestType doExpressCheckoutPaymentRequest = new DoExpressCheckoutPaymentRequestType(doExpressCheckoutPaymentRequestDetails);
                doExpressCheckoutPayment.DoExpressCheckoutPaymentRequest = doExpressCheckoutPaymentRequest;
                // Create the service wrapper object to make the API call
                PayPalAPIInterfaceServiceService service = new PayPalAPIInterfaceServiceService();
                // # API call
                // Invoke the DoExpressCheckoutPayment method in service wrapper object
                responseDoExpressCheckoutPaymentResponseType = service.DoExpressCheckoutPayment(doExpressCheckoutPayment);
                if (responseDoExpressCheckoutPaymentResponseType != null)
                {

                    // Response envelope acknowledgement
                    string acknowledgement = "DoExpressCheckoutPayment API Operation - ";
                    acknowledgement += responseDoExpressCheckoutPaymentResponseType.Ack.ToString();

                    System.Diagnostics.Debug.WriteLine(acknowledgement + "\n");
                    // # Success values
                    if (responseDoExpressCheckoutPaymentResponseType.Ack.ToString().Trim().ToUpper().Equals("SUCCESS"))
                    {
                        // Transaction identification number of the transaction that was
                        // created.
                        // This field is only returned after a successful transaction
                        // for DoExpressCheckout has occurred.
                        if (responseDoExpressCheckoutPaymentResponseType.DoExpressCheckoutPaymentResponseDetails.PaymentInfo != null)
                        {
                            IEnumerator<PaymentInfoType> paymentInfoIterator = responseDoExpressCheckoutPaymentResponseType.DoExpressCheckoutPaymentResponseDetails.PaymentInfo.GetEnumerator();
                            while (paymentInfoIterator.MoveNext())
                            {
                                PaymentInfoType paymentInfo = paymentInfoIterator.Current;

                                Session["Transaction_Id"] = paymentInfo.TransactionID;
                                Session["Transaction_Type"] = paymentInfo.TransactionType;
                                Session["Payment_Status"] = paymentInfo.PaymentStatus;
                                Session["Payment_Type"] = paymentInfo.PaymentType;
                                Session["Payment_Total_Amount"] = paymentInfo.GrossAmount.value;
                                System.Diagnostics.Debug.WriteLine("Transaction ID : " + paymentInfo.TransactionID + "\n");

                                // 保存Transaction_Id
                                var paymentInfoModel = new LU.Data.tb_order_paypal_paymentinfo
                                {
                                    OrderCode = orderCode,
                                    Payment_Status = paymentInfo.PaymentStatus.HasValue ? paymentInfo.PaymentStatus.Value.ToString() : string.Empty,
                                    Payment_Total_Amount = paymentInfo.GrossAmount.value,
                                    Payment_Type = paymentInfo.PaymentType.HasValue ? paymentInfo.PaymentType.Value.ToString() : string.Empty,
                                    Regdate = DateTime.Now,
                                    Transaction_Id = paymentInfo.TransactionID,
                                    Transaction_Type = paymentInfo.TransactionType.HasValue ? paymentInfo.TransactionType.Value.ToString() : string.Empty,
                                };
                                DBContext.tb_order_paypal_paymentinfo.Add(paymentInfoModel);
                                // 支付纪录
                                var record = new LU.Data.tb_order_pay_record
                                {
                                    order_code = orderCode,
                                    pay_record_id = 15, // paypal
                                    regdate = DateTime.Now,
                                    balance = 0,
                                    pay_cash = decimal.Parse(paymentInfo.GrossAmount.value),
                                    pay_regdate = DateTime.Now
                                };
                                DBContext.tb_order_pay_record.Add(record);
                                // 改订单状态，为完成
                                var orderInfo = DBContext.tb_order_helper.Single(p => p.order_code.HasValue && p.order_code.Value.Equals(orderCode));
                                orderInfo.tag = 1; // 完成支付发送后，再确认
                                orderInfo.order_pay_status_id = 2;

                                // 删除临时表数据
                                var tmpOrder = DBContext.tb_cart_temp.Where(p => p.cart_temp_code.HasValue && p.cart_temp_code.Value.Equals(orderCode)).ToList();
                                foreach (var item in tmpOrder)
                                {
                                    DBContext.tb_cart_temp.Remove(item);
                                }
                                var oc = orderCode.ToString();
                                var tmpOrderPrice = DBContext.tb_cart_temp_price.Where(p => p.order_code.Equals(oc));
                                foreach (var item in tmpOrderPrice)
                                {
                                    DBContext.tb_cart_temp_price.Remove(item);
                                }
                                DBContext.SaveChanges();
                                Response.Redirect("ShoppingCartGoSubmit.aspx?ordercode=" + orderCode + "&paypal_transaction_id=" + paymentInfo.TransactionID, true);
                            }

                        }
                        else
                        {
                            Response.Write("responseDoExpressCheckoutPaymentResponseType.DoExpressCheckoutPaymentResponseDetails.PaymentInfo is null...");
                        }
                    }
                    // # Error Values
                    else
                    {
                        List<ErrorType> errorMessages = responseDoExpressCheckoutPaymentResponseType.Errors;
                        string errorMessage = "";
                        foreach (ErrorType error in errorMessages)
                        {
                            var err = new LU.Data.tb_order_paypal_error_info
                            {
                                code = error.ErrorCode,
                                order_code = orderCode,
                                errItem = error.LongMessage,
                                errKey = string.Join(",", error.ErrorParameters),
                                regdate = DateTime.Now
                            };
                            DBContext.tb_order_paypal_error_info.Add(err);
                        }
                        DBContext.SaveChanges();
                        Server.Transfer("~/APIError.aspx");
                    }
                }
            }
            catch (System.Exception ex)
            {
                Logs.WriteErrorLog(ex);
                // Log the exception message               
                //  System.Diagnostics.Debug.WriteLine("Error Message : " + ex.Message);
            }
        }
    }
}