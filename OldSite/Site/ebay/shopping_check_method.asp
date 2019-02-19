<%@LANGUAGE="VBSCRIPT" CODEPAGE="65001"%>
<!--#include virtual="site/inc/inc_helper.asp"-->

<%
	
	
	dim pay_method, redirect_path
	pay_method = SQLescape(request("pay_method"))
	'response.write pay_method : response.End()
	if( cstr(pay_method) <> cstr(LAYOUT_PAYPAL_METHOD_CARD))then 

		Call IsLoginWeb("/ebay/shopping_check_method.asp?pay_method="& pay_method)
	end if
	'response.write pay_method : response.End()
	
	if pay_method <> "" and isnumeric(pay_method) then 
		set rs = conn.execute("select * from tb_pay_method_new where pay_method_serial_no="& SQLquote(pay_method))
		if not rs.eof then 
			if( cstr(pay_method) <> cstr(LAYOUT_PAYPAL_METHOD_CARD))then 
				conn.execute("update tb_customer set pay_method="&SQLquote(rs("pay_method_serial_no"))&" where customer_serial_no="&SQLquote(LAYOUT_CCID))
			End if
			conn.execute("update tb_cart_temp set pay_method="&SQLquote(pay_method)&" where cart_temp_code='"&LAYOUT_ORDER_CODE&"'")
			session("order_ok_return") 	=	LAYOUT_HOST_URL & rs("pay_check_path_new")
			redirect_path = LAYOUT_HOST_URL & rs("pay_check_path_new")
		end if
		rs.close : set rs = nothing
	end if
	closeconn()	
	
	response.Redirect(redirect_path)	
	Response.End()
%>