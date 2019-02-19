<%@LANGUAGE="VBSCRIPT" CODEPAGE="65001"%>
<!--#include virtual="site/inc/inc_helper.asp"-->
<%

	call ValidateUserSessionInfo(LAYOUT_CCID ,"closeConn") 
	Call ValidateOrder_Code("")
	
	set rs = server.createobject("adodb.recordset")
	rs.open "select * from tb_customer where customer_serial_no="& LAYOUT_CCID, conn, 2,3

	rs("customer_card_type") 		= SQLescape(request.form("card_type"))
	rs("customer_credit_card") 		= SQLescape(request.form("card_number"))
	rs("customer_expiry") 			= SQLescape(request.form("card_expiry_month"))&SQLescape(request.form("card_expiry_year"))
	rs("customer_card_issuer") 		= SQLescape(request.form("card_issuer"))
	rs("customer_card_phone") 		= SQLescape(request.form("card_issuer_telephone"))
	rs("customer_card_first_name") 	= SQLescape(request.form("cart_first_name"))
	rs("customer_card_last_name") 	= SQLescape(request.form("cart_last_name"))
	rs("customer_card_billing_shipping_address") = SQLescape(request.form("card_billing_shipping_address"))
	rs("customer_card_city") 		= SQLescape(request.form("card_city"))
	rs("customer_card_state_code") 	= SQLescape(request.form("card_state"))
	rs("customer_card_zip_code") 	= SQLescape(request.form("card_zip_code"))
	rs("customer_shipping_address") = SQLescape(request.form("shipping_address"))
	rs("customer_shipping_city") 	= SQLescape(request.form("shipping_city"))
	rs("customer_email2") 			= SQLescape(request.form("secondary_email"))
	rs("phone_d") 					= SQLescape(request.form("phone_d"))
	rs("phone_n") 					= SQLescape(request.form("phone_n"))

	rs("customer_card_country_code") 	= SQLescape(request.form("card_country"))
	rs("shipping_country_code") = LAYOUT_SHIPPING_COUNTRY_CODE
	'rs("my_purchase_order") = SQLescape(request.form("my_purchase_order")
	rs("customer_shipping_first_name") = SQLescape(request.form("shipping_first_name"))
	rs("customer_shipping_last_name") = SQLescape(request.form("shipping_last_name"))
	rs("customer_shipping_zip_code") =  SQLescape(request.form("shipping_zip_code"))
	rs("shipping_state_code") 	= LAYOUT_SHIPPING_STATE_CODE
	rs("customer_shipping_city") 	= SQLescape(request.form("shipping_city"))
	'rs("customer_card_phone") = SQLescape(request.form("customer_card_phone")
	rs("card_verification_number") 	= SQLescape(request.form("card_verification_number"))
	'response.Write(rs("customer_card_state_code"))
	rs.update
	rs.close : set rs = nothing
	'Response.write SQLescape(request.form("card_state"))
	'
	' save Msg

	content = SQLescape(request.form("note"))
	call CustomerSendMsg( LAYOUT_ORDER_CODE, content)

	
	closeconn()
	'response.end
	
%>
<script>
	window.location.replace("<%= LAYOUT_HOST_URL %>shopping_checkout_order.asp")
</script>