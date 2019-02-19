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
	
	Dim returnURL 	: 	returnURL = SQLescape(request("returnURL"))
	
	dim cart_temp_serial_no, cart_temp_Quantity, cart_temp_serial_nos, cart_temp_Quantitys
	cart_temp_serial_no 	= SQLescape(request.Form("Order_Ids"))
	cart_temp_Quantity 		= SQLescape(request.Form("quantity"))
	
	
	cart_temp_serial_nos 	= split (cart_temp_serial_no, ",")
	cart_temp_Quantitys 	= split ( cart_temp_Quantity, ",")
	
	for i=lbound(cart_temp_serial_nos) to ubound(cart_temp_serial_nos)	
		conn.execute("update tb_cart_temp set cart_temp_Quantity='"&cart_temp_Quantitys(i)&"' where cart_temp_serial_no='"&cart_temp_serial_nos(i)&"'")	
	next
	
	conn.execute("delete from tb_cart_temp where cart_temp_Quantity=0 and cart_temp_code='"&LAYOUT_ORDER_CODE&"'")

	if returnURL <> "" then 
		path = returnURL
	else	
		path = "Shopping_cart.asp?category="&request("category")
	end if
	response.Redirect(path)
%>
</body>
</html>
