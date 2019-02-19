<%@  language="VBSCRIPT" codepage="65001" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>LU Computers</title>
</head>
<body>
    <!--#include virtual="/inc/conn.asp"-->
    <!--#include virtual="/inc/inc_escape.asp"-->
    <%

	'call ValidateUserSessionInfo(LAYOUT_CCID ,"closeConn") 
	
	'if LAYOUT_CCID = "" THEN 
	'	Call IsLoginWeb("2")  
	'	RESPONSE.End()
	'eND IF
	
'	if (request("cmd") = "update") then
'		set rs = server.createobject("adodb.recordset")
'		rs.open "select * from tb_customer where customer_serial_no="& SQLquote(LAYOUT_CCID), conn, 1,3
'		rs("customer_shipping_address") 		= request.form("shipping_address")
'		rs("customer_shipping_city") 			= request.form("shipping_city")
'		rs("shipping_state_code") 				= request.form("shipping_state")
'		rs("customer_shipping_zip_code") 		= request.form("zip_code")
'		rs("customer_email2") 					= request.form("secondary_email")
'		rs("phone_d") 							= request.form("phone_d")
'		rs("phone_n") 							= request.form("phone_n")
'		rs("customer_shipping_first_name") 		= request.form("shipping_first_name")
'		rs("customer_shipping_last_name") 		= request.form("shipping_last_name")
'		rs("shipping_country_code") 			= request.form("shipping_country")		
'		rs.update
'		rs.close : set rs = nothing
'		
'		conn.execute("update tb_cart_temp set pay_method="&SQLquote(LAYOUT_PAYPAL_METHOD_CARD)&" where cart_temp_code='"&LAYOUT_ORDER_CODE&"'")
'		
'		call CustomerSendMsg( LAYOUT_ORDER_CODE, request.form("note"))
'	end if




dim orderCode
dim paymentAmount
Dim p_unit
if request("paypal") = "true" then 
		
		paymentAmount = ""
        orderCode = SQLescape(request("orderCode"))
        paymentAmount = SQLescape(request("amt"))
        p_unit=SQLescape(request("unit"))
		response.cookies("ordercode") = orderCode
		
else
     if request("pay") = "0" or request("pay") = 0 then
        orderCode = SQLescape(request("orderCode"))
        conn.execute("update tb_order_helper set tag=0 where order_code = '"&orderCode&"'")

        closeconn()

        Response.redirect("/")
        response.End
     end if
        response.Write("prams is error.")
        closeconn()
        response.End
end if

'set rs = conn.execute("select customer_login_name from tb_customer_store cs where cs.order_code = '"&orderCode&"'")
'if not rs.eof then
 '  if rs(0) = "qiozi@msn.com" then 
 '       paymentAmount = 1
'   end if    
'end if
'rs.close : set rs = nothing
closeconn()


    %>
    <form action="/checkout_paypal_website_payments_pro_setExpressCheckout_exec.asp?d=<%=now()%>"
    method="post" name="form1" id="form1">
    <input type="hidden" value="Sale" name="paymentType" />
    <input type="hidden" value="<%= paymentAmount %>" name="paymentAmount" />
    <input type="hidden" value="<%= p_unit %>" name="currencyCodeType" />
    <input type="hidden" value="<%= orderCode %>" name="orderCode" />
    </form>
    <script language="javascript">
        document.getElementById("form1").submit();
    </script>
</body>
</html>
