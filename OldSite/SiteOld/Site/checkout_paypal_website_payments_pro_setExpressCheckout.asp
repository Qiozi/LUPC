<%@LANGUAGE="VBSCRIPT" CODEPAGE="65001"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>LU Computers</title>
</head>

<body>
<!--#include virtual="site/inc/inc_helper.asp"-->
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


dim paymentAmount
Dim p_unit
if request("paypal") = "true" then 
		
		paymentAmount = ""
		
		set rs = conn.execute("select taxable_total, grand_total_rate, price_unit from tb_cart_temp_price where order_code='"&LAYOUT_ORDER_CODE&"'")
		if Session("IsExistOrder") = true then 
			rs.close  : set rs = nothing
			set rs = conn.execute("select taxable_total, grand_total, price_unit from tb_order_helper where order_code='"&LAYOUT_ORDER_CODE&"'")
			'response.write "select taxable_total, grand_total, price_unit from tb_order_helper where order_code='"&LAYOUT_ORDER_CODE&"'"
		end if
		
		if not rs.eof then 
			paymentAmount 	= 	rs(1)	
			p_unit			=	rs("price_unit")	
		end if
		rs.close:set rs = nothing
end if
closeconn()

%>
<form action="<%= LAYOUT_HOST_URL %>checkout_paypal_website_payments_pro_setExpressCheckout_exec.asp?d=<%=now()%>" method="post" name="form1" id="form1">
	<input type="hidden" value="Sale" name="paymentType" />
    <input type="hidden" value="<%= paymentAmount %>" name="paymentAmount" />
    <input type="hidden" value="<%= p_unit %>" name="currencyCodeType" />
</form>
<script language="javascript">
		document.getElementById("form1").submit();
</script>
</body>
</html>
