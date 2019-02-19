<%@LANGUAGE="VBSCRIPT" CODEPAGE="65001"%>
<%
Response.Buffer = True 
Response.ExpiresAbsolute = Now() - 2 
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
	
	Dim cmd 			:	cmd 			=	SQLescape(request("cmd"))
	Dim system_sku 		:	system_sku		=	SQLescape(request("system_sku"))	
	dim sys_prod_sku	:	sys_prod_sku 	= 	Session("SystemSku8")	
	
	Dim cart_temp_code
	
	cart_temp_code  = GetCookiesOrderCode()
	if cart_temp_code = "" then 
		cart_temp_code=	GetNewOrderCode()
		SetCookiesOrderCode(cart_temp_code)
	end if
	'response.write cart_temp_code
	if len(cart_temp_code)<> LAYOUT_ORDER_LENGTH then
		closeconn()
		response.Write("Order Code is lost.")
		response.End()
		cart_temp_code=	GetNewOrderCode()
		SetCookiesOrderCode(cart_temp_code)
	ENd if
	
	
		
	if cmd = "uncustomize" then 
		sys_prod_sku = GetNewSystemCode()
		cart_temp_code  = GetCookiesOrderCode()
		

		'Response.write system_sku &"|"& sys_prod_sku &"|"& cart_temp_code
		Call sysAddToSpTmp(system_sku, sys_prod_sku, cart_temp_code, true)
		response.write "<script>window.location.replace('/site/shopping_cart.asp?category=sys');</script>"
	elseif cmd = "arrange" then 
	
		session("is_run_pick") = false
		response.Cookies("pick_up_in_person") = "true"
		response.Cookies("shipping_state_code") = LAYOUT_ONTARIO_Code
		response.Cookies("shipping_country_code") = "CA"
		
		sys_prod_sku = GetNewSystemCode()
		cart_temp_code  = LAYOUT_ORDER_CODE
		
		Call sysAddToSpTmp(system_sku, sys_prod_sku, cart_temp_code, true)
		
		Conn.execute("Update tb_cart_temp Set shipping_state_code="& SQLquote(LAYOUT_ONTARIO_Code) &", shipping_country_code='CA' Where  cart_temp_code="& SQLquote(cart_temp_code))
		'response.Redirect( "/shopping_check_out.asp?country="&current_system_category&"&state_shipping="&Ontario_id&"&Pay_method=21")
		response.write "<script>window.location.replace('/site/shopping_check_out.asp?country="&current_system_category&"&state_shipping="&Ontario_id&"&Pay_method=21');</script>"
		
	elseif cmd = "go" then 
		if len(sys_prod_sku) = LAYOUT_SYSTEM_CODE_LENGTH then 
				cart_temp_code  = GetCookiesOrderCode()
				
				if LAYOUT_CCID = "" then LAYOUT_CCID = 0
		
				'call CopyConfigureSystemToCart( session("system_templete_serial_no"), sys_prod_sku, cart_temp_code, true, LAYOUT_HOST_IP)
				
				conn.execute("insert into tb_cart_temp(cart_temp_code, product_serial_no,  create_datetime, ip, customer, cart_temp_Quantity, customer_serial_no, shipping_company, state_shipping, is_noebook, price, price_rate, product_name, cost, price_unit, current_system) "&_
				" select '"& cart_temp_code &"', '"& sys_prod_sku &"',  now(), '"&LAYOUT_HOST_IP&"', '"& LAYOUT_CCID &"', 1, '"&LAYOUT_CCID&"', '"& GetShippingCompany(cart_temp_code) &"','"& GetStateShipping(cart_temp_code)&"', 0, sys_tmp_price, sys_tmp_price, sys_tmp_product_name, sys_tmp_cost, price_unit,"& SQLquote(Current_System) &" from tb_sp_tmp where sys_tmp_code='"&sys_prod_sku&"'")
				response.write "<script>window.location.replace('/site/Shopping_Cart.asp?category=sys');</script>"
		End if
	else
			
			if len(sys_prod_sku) = LAYOUT_SYSTEM_CODE_LENGTH then 
				
				cart_temp_code  = GetCookiesOrderCode()
				
				if len(system_sku) = LAYOUT_SYSTEM_CODE_LENGTH then 
					sys_prod_sku = system_sku
				end if
				
				if LAYOUT_CCID = "" then LAYOUT_CCID = -1
		
				'call CopyConfigureSystemToCart( session("system_templete_serial_no"), sys_prod_sku, cart_temp_code, true, LAYOUT_HOST_IP)
				
				conn.execute("insert into tb_cart_temp(cart_temp_code, product_serial_no,  create_datetime, ip, customer, cart_temp_Quantity, customer_serial_no, shipping_company, state_shipping, is_noebook, price, price_rate, product_name, cost, price_unit, current_system) "&_
				" select '"& cart_temp_code &"', '"& sys_prod_sku &"',  now(), '"&LAYOUT_HOST_IP&"', '"& LAYOUT_CCID &"', 1, '"&LAYOUT_CCID&"', '"& GetShippingCompany(cart_temp_code) &"','"& GetStateShipping(cart_temp_code)&"', 0, sys_tmp_price, sys_tmp_price, sys_tmp_product_name, sys_tmp_cost, price_unit,"& SQLquote(Current_System) &" from tb_sp_tmp where sys_tmp_code='"&sys_prod_sku&"'")
			
			else
				sys_prod_sku 	= GetNewSystemCode()				
				cart_temp_code  = GetCookiesOrderCode()
				
				call sysAddToSpTmp( system_sku, sys_prod_sku, cart_temp_code, true)
			end if
			
			response.write "<script>window.location.replace('/site/Shopping_Cart.asp?category=sys');</script>"
	end if
	closeconn()

	
	session("system_templete_serial_no") 	= ""
	session("templete_system_info") 		= ""
	Session("SystemSku8") 					= ""
	'response.Redirect("Shopping_Cart.asp?category=sys")
	

%>
</body>
</html>