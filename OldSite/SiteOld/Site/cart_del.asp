<%@LANGUAGE="VBSCRIPT" CODEPAGE="65001"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<title>LU Computers</title>
</head>

<body>
<!--#include virtual="site/inc/inc_helper.asp"-->
<!--include file="luadmin/class/cart_tempClass.asp"-->
<%
	'call ValidateUserSessionInfo(LAYOUT_CCID ,"closeConn") 
	
	dim cart_temp_serial_no
	Dim cmd					:	cmd 	=	SQLescape(request("cmd"))
	Dim order_code			:	order_code 	=	SQLescape(request("order_code"))
	cart_temp_serial_no =   SQLescape(request("Pro_id"))
	'
	'	delete order
	'
	if cmd = "all" and order_code <> "" then 
		Conn.execute("Delete from tb_cart_temp where cart_temp_code="&SQLquote(order_code)& " and customer_serial_no="& SQLquote(LAYOUT_CCID))
		Conn.execute("Delete from tb_cart_temp_price where order_code="& SQLquote(order_code))
	else
		Response.write "delete from tb_cart_temp where cart_temp_serial_no='"&cart_temp_serial_no&"'"
		conn.execute("delete from tb_cart_temp where cart_temp_serial_no='"&cart_temp_serial_no&"'")
	end if
	closeconn()


	if SQLescape(request("returnUrl")) <> "" then 
		response.Redirect(request("returnUrl"))
	else
		response.Redirect("/ebay/cart.asp")
		
	end if
%>
</body>
</html>
