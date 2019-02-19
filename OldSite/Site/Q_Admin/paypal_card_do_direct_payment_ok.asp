<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>paypal do direct payment ok</title>
</head>
<body>
<!--#include virtual="/public_helper/validate_admin.asp"-->
<!--#include virtual="/inc/conn.asp"-->
<%
dim paypal_transaction_id, paypal_avs, paypal_cvv2, order_code, code
paypal_transaction_id = Request("TRANSACTIONID")
paypal_avs = Request("AVS")
paypal_cvv2 = Request("CVV2")
code = request("code")
order_code = request("order_code")

dim LAYOUT_PAY_RECORD_METHOD_PAYPAL : LAYOUT_PAY_RECORD_METHOD_PAYPAL = 15


if paypal_transaction_id <> "" then 
	if len(order_code)>4 then 
        set rs = conn.execute("select amount from tb_order_paypal_record_info where order_code='"& order_code &"' order by id desc limit 0,1")
        if not rs.eof then
             call   AddOrderPayRecord(order_code, rs(0), LAYOUT_PAY_RECORD_METHOD_PAYPAL)
        end if
        rs.close : set rs = nothing
    end if
    if order_code <> "" then 
    conn.execute("Update tb_order_helper set order_pay_status_id='2',is_modify=1 where order_code='"& order_code &"'")
    conn.execute("insert into tb_order_paypal_record ( transaction, avs, cvv2, order_code, regdate, code) values ( '"&paypal_transaction_id&"', '"&paypal_avs&"', '"&paypal_cvv2&"','"& order_code &"', now(), '"& code &"')")
    end if
end if


' ---------------------------------------------------------------------------------
function AddOrderPayRecord(order_code, amt, pay_record_id)
' ---------------------------------------------------------------------------------

    conn.execute("insert into tb_order_pay_record "&_
	                   " ( order_code, pay_record_id, pay_regdate, pay_cash, regdate, balance) "&_
	                   " values "&_
	                   " ( '"& order_code &"', '"& pay_record_id &"', now(), '"& amt &"', now(), 0)")

end function


%>
<script type="text/javascript">
alert("It is OK.\r\ntransaction: <%= paypal_transaction_id%>");
window.location.href="paypal_card_do_direct_payment.asp";
</script>

</body>
</html>
