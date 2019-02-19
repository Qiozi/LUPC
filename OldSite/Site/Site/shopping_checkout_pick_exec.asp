<%@LANGUAGE="VBSCRIPT" CODEPAGE="65001"%>
<!--#include virtual="site/inc/inc_helper.asp"-->

<%

	call ValidateUserSessionInfo(LAYOUT_CCID ,"closeConn") 
	Call ValidateOrder_Code("")
	
	set rs = server.createobject("adodb.recordset")
	rs.open "select * from tb_customer where customer_serial_no="& SQLquote(LAYOUT_CCID), conn, 1,3

	rs("customer_card_type") 			= request.form("card_type")
	rs("customer_credit_card") 			= request.form("card_number")
	rs("customer_expiry") 				= request.form("card_expiry_month")&request.form("card_expiry_year")
	rs("customer_card_issuer") 			= request.form("card_issuer")
	rs("customer_card_phone") 			= request.form("card_issuer_telephone")

	rs("customer_card_billing_shipping_address") = request.form("card_billing_shipping_address")
	rs("customer_card_city") 			= request.form("card_city")
	rs("customer_card_state") 			= request.form("card_state")
	rs("customer_card_zip_code") 		= request.form("card_zip_code")
	rs("customer_first_name") 			= request.form("customer_first_name")
	rs("customer_last_name") 			= request.form("customer_last_name")
	rs("customer_shipping_first_name") 	= rs("customer_first_name")
	rs("customer_shipping_last_name") 	= rs("customer_last_name")
	rs("customer_shipping_state") 		= Ontario_id
	rs("customer_email2") 				= request.form("secondary_email")
	rs("phone_d") 						= request.form("phone_d")
	rs("phone_n") 						= request.form("phone_n")
	rs("customer_card_country") 		= request.form("card_country")
	
	rs.update
	rs.close : set rs = nothing
		'
	' save Msg
	order_code = current_tmp_order_code
	content = request.form("note")
	call CustomerSendMsg( order_code, content)
	
	dim pick_up_datetime1, pick_up_datetime2

	pick_up_datetime1 = cstr(year(date())) & "-" & request.form("pick_up_month_1") &"-"& request.form("pick_up_dd_1") & " " & request.form("pick_up_hh_1") 
	pick_up_datetime2 = cstr(year(date())) & "-" & request.form("pick_up_month_2")&"-"& request.form("pick_up_dd_2") & " " & request.form("pick_up_hh_2")  

	'Response.write pick_up_datetime1
	conn.execute("update tb_cart_temp set pick_datetime_1='"&pick_up_datetime1&"', pick_datetime_2='"&pick_up_datetime2&"' where cart_temp_code='"&LAYOUT_ORDER_CODE&"'")
	
	closeconn()

	
%>
<script>
	window.location.replace("<%=LAYOUT_HOST_URL %>shopping_checkout_order.asp")
</script>