<%@LANGUAGE="VBSCRIPT" CODEPAGE="65001"%>
<% session("t") = timer() %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<title>LU Computers</title>
<link href="lu.css" rel="stylesheet" type="text/css">
</head>

<body>
<!--#include virtual="site/inc/inc_helper.asp"-->
<%
	'call validHttpReferer()
	
	if request("product_serial_no") = "" and request("Pro_Id") = "" then
		response.write ("Param is lost.")
		response.End()
	end if
	
	Response.Cookies("CurrentOrder") = "site"
'	dim cart_temp_serial_no,cart_temp_code,product_serial_no,menu_child_serial_no,create_datetime,ip,customer,cart_temp_Quantity
'	dim product_list, product_list_arr, save_price
	'cart_temp_serial_no = qiozi_null
	dim tmp_order_code
	if not IsExistOrderCode() then
		
	
		tmp_order_code = GetNewOrderCode()
		SetCookiesOrderCode(tmp_order_code)
		'response.Write(getCode.order())
		'response.End()
	end if
'	response.Write(getCode.order())
'	response.End()

	response.Redirect("/site/shopping_cart_pre_next.asp?Pro_Id="& request("Pro_Id") &"&cid="& request("cid")&"&product_serial_no="& request("product_serial_no"))
	closeconn()
	response.End()
	


%>
</body>
</html>