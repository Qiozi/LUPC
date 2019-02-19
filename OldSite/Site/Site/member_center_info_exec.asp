<%@LANGUAGE="VBSCRIPT" CODEPAGE="65001"%>
<!--#include virtual="site/inc/inc_helper.asp"-->
<%

	Call ValidateUserSessionInfo(LAYOUT_CCID ,"closeConn") 
	
'	for each s in request.Form
'		response.write s &" = " & request.Form(s) & "<br>"
'	next
	if (SQLescape(request.form("user_pwd"))<> SQLescape(request.form("confirmpwd"))) and SQLescape(request.form("confirmpwd")) <> "" then 
		response.write "<script>alert('密码不相同')window.location.href='"&LAYOUT_HOST_URL&"member_center_info.asp';;</script>"
		response.end		
	end if


	dim is_news_latter
	if SQLescape(request.form("cb_news_letter")) = "" then 
		is_news_latter = 0
	else
		is_news_latter = 1
	end if

	
	set rs = server.createobject("adodb.recordset")
	rs.open "select * from tb_customer where customer_serial_no="& SQLquote(LAYOUT_CCID), conn,1,3
		rs("customer_password") 			= request.form("user_pwd")
		rs("customer_email1") 				= request.form("email1")
		rs("zip_code") 						= left(trim(replace(request.form("zip_code"), " ", "")),6)
		rs("customer_email2") 				= request.form("email2")
		rs("customer_first_name") 			= request.form("FN")
		rs("customer_address1") 			= request.form("address")
		rs("news_latter_subscribe") 		= is_news_latter
		rs("customer_last_name") 			= request.form("LN")
		rs("phone_n") 						= request.form("phone_n")
		rs("phone_d") 						= request.form("phone_d")
		rs("customer_city") 				= request.form("city")
		rs("phone_c") 						= request.form("phone_c")
		rs("customer_card_city") 			= request.form("city")
		rs("state_code") 					= request.form("shipping_state")
		rs("customer_country_code") 		= request.form("user_Country")
		rs("EBay_ID") 						= request.form("ebay_id")
		rs("customer_company") 				= request.form("busniess_company_name")
		rs("customer_business_telephone") 	= request.form("busniess_telephone")
		rs("customer_business_country_code")= request.form("busniess_country")
		rs("customer_business_address") 	= request.form("busniess_address")
		rs("customer_business_city") 		= request.form("busniess_city")
		rs("customer_business_zip_code") 	= request.form("busniess_zip_code")
		rs("tax_execmtion") 				= request.form("busniess_tax_exemption")
		Response.Cookies("tax_execmtion") 	=  rs("tax_execmtion")
		rs("busniess_website") 				= request.form("busniess_website")
		rs("customer_business_state_code") 	= request.form("busniess_state")
		rs("customer_shipping_first_name")	= request.form("customer_shipping_first_name")						 
		rs("customer_shipping_last_name")	= request.form("customer_shipping_last_name")						 
		rs("shipping_country_code")			= request.form("shipping_country_code")					 
		rs("customer_shipping_address")		= request.form("customer_shipping_address")					 
		rs("customer_shipping_city")		= request.form("customer_shipping_city")						 
		rs("shipping_state_code")			= request.form("shipping_state_code")						 
		rs("customer_shipping_zip_code")	= request.form("customer_shipping_zip_code")						 
								
	rs.update
	rs.close : set rs = nothing

	closeconn()
 	response.write ("<script> window.location.href='"&LAYOUT_HOST_URL&"member_center_info.asp';</script>")
%>