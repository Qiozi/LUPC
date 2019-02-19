<%@LANGUAGE="VBSCRIPT" CODEPAGE="65001"%>
<!--<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<title>LU Computers</title>
<link href="lu.css" rel="stylesheet" type="text/css">
</head>

<body>-->

<!--#include virtual="site/inc/inc_helper.asp"-->
<%dim t_b_t
	t_b_t = timer()
	dim cart_temp_serial_no,cart_temp_code,product_serial_no,menu_child_serial_no,create_datetime,ip,customer,cart_temp_Quantity
	dim product_list, product_list_arr, save_price
    Dim prodType 
	cart_temp_serial_no = qiozi_null

	' get OrderCode
	cart_temp_code  = GetCookiesOrderCode()
	
	response.Write(cart_temp_code)
'	response.End()
	
	product_serial_no 		= SQLescape(request("Pro_Id"))
	menu_child_serial_no 	= SQLescape(request("cid"))
	create_datetime 		= now()
	ip 						= LAYOUT_HOST_IP
	customer 				= session("email")
	product_list 			= SQLescape(request("product_serial_no"))
	
	'
	'
	'	if cid is null, then query for DB
	'
	if len(menu_child_serial_no)<1 then 
		Set rs = conn.execute("Select menu_child_serial_no, prodType from tb_product Where product_serial_no="& SQLquote(product_serial_no))
		If not rs.eof then
			menu_child_serial_no	=	rs(0)
            prodType = rs("prodType")
		End if
		rs.close : set rs = nothing	
	End if
	

	
	dim is_noebook, product_current_price, product_name , old_price, product_current_price_rate, current_cost
	product_current_price = 0
	old_price = 0
	current_cost = 0
	is_noebook = 0
	set rs = conn.execute("select pc.is_noebook, p.product_current_price, p.product_name, p.product_current_cost, p.product_current_discount, p.prodType from tb_product p inner join tb_product_category pc on pc.menu_child_serial_no=p.menu_child_serial_no where p.product_serial_no="&SQLquote(product_serial_no))
	if not rs.eof then
		is_noebook = rs(0)
		'product_current_price = rs(1)
		'product_current_price_rate = product_current_price
		'old_price = rs(1)
		product_name = rs("product_name")
        prodType = rs("prodType")
		'current_cost = rs("product_current_cost")
		'save_price = cdbl(rs("product_current_discount"))
	end if
	rs.close : set rs = nothing

	'save_price = GetSavePrice(product_serial_no)
	'if isnumeric(save_price) then 
	'old_price = cdbl(product_current_price)
	
	'product_current_price = old_price - save_price
	'product_current_price_rate = product_current_price_rate - save_price
	'end if 

	dim customer_serial_no 
	if request.Cookies("customer_serial_no") <> "" then 
		customer_serial_no= request.Cookies("customer_serial_no")
	else
		customer_serial_no =0
	end if
	set rs = server.CreateObject("adodb.recordset")
	rs.open "select * from tb_cart_temp", conn,1,3
	rs.addnew
		rs("cart_temp_code") = cart_temp_code
		rs("product_serial_no") = product_serial_no
		rs("menu_child_serial_no") = menu_child_serial_no
		rs("create_datetime") = now()
		rs("ip") = LAYOUT_HOST_IP
		rs("customer") = request.Cookies("customer_serial_no")
		rs("cart_temp_Quantity") = 1
		
		rs("customer_serial_no") = customer_serial_no
		
		rs("shipping_company") = GetShippingCompany(cart_temp_code)
		rs("state_shipping") = GetStateShipping(cart_temp_code)
		rs("is_noebook") = is_noebook
		'rs("price") = ChangeSpecialCashPrice(product_current_price )
		rs("product_name") = product_name
		'rs("old_price") = FormatNumber(old_price, 2)
		'rs("save_price") = FormatNumber(save_price, 2)
		'rs("price_rate") = FormatNumber(product_current_price_rate,2)
		'rs("cost") = current_cost
        rs("prodType") = prodType
		rs("price_unit")	=	CCUN
		rs("current_system") = Current_System
	rs.update
	rs.close : set rs = nothing
	
	
	'conn.execute("insert into tb_cart_temp(cart_temp_code,product_serial_no,menu_child_serial_no,create_datetime,ip,customer,cart_temp_Quantity,customer_serial_no,"&_
'	"shipping_company,state_shipping,is_noebook,price,product_name,old_price,save_price,price_rate,cost) values "&_
'	"('"&cart_temp_code&"','"&product_serial_no&"','"&menu_child_serial_no&"',now(),'"&client.getIP&"','"&request.Cookies("customer_serial_no")&"',1,'"&customer_serial_no&"','"&GetShippingCompany(cart_temp_code)&"','"&GetStateShipping(cart_temp_code)&"','"&is_noebook&"','"&ChangeSpecialCashPriceByRate(product_current_price )&"','"&replace(product_name,"'","''")&"','"&FormatNumber(old_price, 2)&"','"&FormatNumber(save_price, 2)&"','"&FormatNumber(product_current_price_rate,2)&"','"&current_cost&"')")
	


	closeconn()
	
	response.write("<script>window.location.replace('/site/Shopping_Cart.asp');</script>")
	

%>
<!--</body>
</html>-->