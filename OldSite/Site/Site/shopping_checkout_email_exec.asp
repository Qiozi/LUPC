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
	call ValidateUserSessionInfo(LAYOUT_CCID ,"closeConn") 
	Call ValidateOrder_Code("")

	dim customerID
	customerID = LAYOUT_CCID

	
	if (request("cmd") = "update") then
'		set rs = server.createobject("adodb.recordset")
'		rs.open "select * from tb_customer where customer_serial_no="& SQLquote(customerID), conn, 1,3
'		rs("customer_shipping_address") = encode.stringRequest("shipping_address")
'		rs("customer_shipping_city") = encode.stringRequest("shipping_city")
'		rs("customer_shipping_state") = encode.stringRequest("shipping_state")
'		rs("customer_shipping_zip_code") = encode.stringRequest("zip_code")
'		rs("customer_email2") = encode.stringRequest("secondary_email")
'		rs("phone_d") = encode.stringRequest("phone_d")
'		rs("phone_n") = encode.stringRequest("phone_n")
'		rs("customer_shipping_first_name") = encode.stringRequest("shipping_first_name")
'		rs("customer_shipping_last_name") = encode.stringRequest("shipping_last_name")
'		rs("customer_shipping_country") = encode.stringRequest("shipping_country")		
'		rs.update
'		rs.close : set rs = nothing
		
		Conn.execute("Update tb_customer Set "&_
						" customer_shipping_address		="& SQLquote(request.Form("shipping_address")) &_
						" ,customer_shipping_city 		="& SQLquote(request.Form("shipping_city")) &_
						" ,customer_shipping_zip_code 	="& SQLquote(request.Form("zip_code")) &_
						" ,customer_email2 				="& SQLquote(request.Form("secondary_email")) &_
						" ,phone_d 						="& SQLquote(request.Form("phone_d")) &_
						" ,phone_n 						="& SQLquote(request.Form("phone_n")) &_
						" ,customer_shipping_first_name ="& SQLquote(request.Form("shipping_first_name")) &_
						" ,customer_shipping_last_name 	="& SQLquote(request.Form("shipping_last_name")) &_
						" where customer_serial_no = "& SQLquote(LAYOUT_CCID))
			'
		' save Msg
		'order_code = current_tmp_order_code
		
		call CustomerSendMsg( LAYOUT_ORDER_CODE, SQLescape(request.Form("note")))
		
		Response.Redirect( LAYOUT_HOST_URL & "shopping_checkout_order.asp")
	end if
	
	CloseConn()
	
%>
</body>
</html>
