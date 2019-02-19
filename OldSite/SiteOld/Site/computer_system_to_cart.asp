<%@LANGUAGE="VBSCRIPT" CODEPAGE="65001"%>
<%
'Response.Buffer = True 
Response.ExpiresAbsolute = Now() - 1 
Response.Expires = 0 
Response.CacheControl = "no-cache" 
Response.AddHeader "Pragma", "No-Cache"
%>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<title>LU Computers</title>
</head>

<body>

<!--#include virtual="site/inc/inc_helper.asp"-->

<%
	'call validHttpReferer()
	
	Dim SystemSku8		:	SystemSku8 = Session("SystemSku8")
	
	Dim system_sku		: 	system_sku	=	SQLescape(request("system_sku"))
	
	
	if system_sku = "" and len(SystemSku8) <> LAYOUT_SYSTEM_CODE_LENGTH  then 
		closeconn()
		response.Write("System sku is lost")
		response.End()
 	end if
	
	if len(LAYOUT_ORDER_CODE) <> LAYOUT_ORDER_LENGTH then
			cart_temp_code = GetNewOrderCode()
			SetCookiesOrderCode(cart_temp_code)
	end if		


	closeconn()

	response.Redirect( LAYOUT_HOST_URL & "computer_system_to_cart_next.asp?cmd="& request("cmd")&"&system_sku="& system_sku )

	'response.Redirect("Shopping_Cart.asp?category=sys")
%>
</body>
</html>