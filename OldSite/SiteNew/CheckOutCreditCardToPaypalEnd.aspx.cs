using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CheckOutCreditCardToPaypalEnd : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && IsLocalHostFrom && IsLogin)
        {
            IsSignIn();
            //            paypal_transaction_id 	= SQLescape(request("TRANSACTIONID"))
            //paypal_avs 				= SQLescape(request("AVS"))
            //paypal_cvv2 			= SQLescape(request("CVV2"))

            //if paypal_transaction_id <> "" then 
            //    set rs = conn.execute("select grand_total from tb_order_helper where order_code='"& LAYOUT_ORDER_CODE &"'")
            //    if not rs.eof then

            //         call   AddOrderPayRecord(LAYOUT_ORDER_CODE, rs(0), LAYOUT_PAY_RECORD_METHOD_PAYPAL)

            //    end if
            //    rs.close : set rs = nothing

            //    conn.execute("Update tb_order_helper set order_pay_status_id='"& LAYOUT_PAYPAL_SUCCESS &"',Is_Modify=1 where order_code='"& LAYOUT_ORDER_CODE &"'")
            //    conn.execute("insert into tb_order_paypal_record ( transaction, avs, cvv2, order_code, regdate) values ( '"&paypal_transaction_id&"', '"&paypal_avs&"', '"&paypal_cvv2&"','"& LAYOUT_ORDER_CODE &"', now())")
            //end if

            var TRANSACTIONID = Util.GetStringSafeFromQueryString(Page, "TRANSACTIONID");
            var AVS = Util.GetStringSafeFromQueryString(Page, "AVS");
            var CVV2 = Util.GetStringSafeFromQueryString(Page, "CVV2");
            var OC = Util.GetInt32SafeFromQueryString(Page, "orderCode", 0);
            if (!string.IsNullOrEmpty(TRANSACTIONID) && OC > 0)
            {
                var order = db.tb_order_helper.Single(o => o.order_code.HasValue && o.order_code.Value.Equals(OC));

                var orderPayRecord = new LU.Data.tb_order_pay_record();
                orderPayRecord.order_code = OC;
                orderPayRecord.pay_cash = order.grand_total;
                orderPayRecord.balance = 0M;
                orderPayRecord.pay_regdate = DateTime.Now;
                int payMethodId;
                int.TryParse(order.pay_method, out payMethodId);
                orderPayRecord.pay_record_id = payMethodId;
                db.tb_order_pay_record.Add(orderPayRecord);
                // order paypal record
                var orderPaypal = new LU.Data.tb_order_paypal_record();
                orderPaypal.transaction = TRANSACTIONID;
                orderPaypal.avs = AVS;
                orderPaypal.cvv2 = CVV2;
                orderPaypal.order_code = OC;
                orderPaypal.regdate = DateTime.Now;
                db.tb_order_paypal_record.Add(orderPaypal);
                // order
                order.order_pay_status_id = 2; // paypal success;
                order.Is_Modify = true;

                db.SaveChanges();
                Response.Redirect("ShoppingCartGoSubmit.aspx", true);
            }
            else
            {
                Response.Write("params is error.");
            }
        }
    }
}