<%@LANGUAGE="VBSCRIPT" CODEPAGE="65001"%>
<!--#include virtual="site/inc/inc_helper.asp"-->

<%
	
	
	dim pay_method, redirect_path, paymethod_paypal
	pay_method = SQLescape(request("pay_method"))
	'response.write pay_method : response.End()
	paymethod_paypal =  SQLescape(request("paymethod_paypal"))
	
	if paymethod_paypal = "15" or paymethod_paypal = "25" then
		LAYOUT_ORDER_CODE =  request("ordercode")
		response.Cookies("tmp_order_code") = LAYOUT_ORDER_CODE
		pay_method = paymethod_paypal
		Session("IsExistOrder") = true
	end if
	
	Call ValidateOrder_Code("site")
	
	
	'response.End()
	if( cstr(pay_method) <> cstr(LAYOUT_PAYPAL_METHOD_CARD))then 
		'Call ValidateUserSessionInfo(LAYOUT_CCID ,"closeConn") 		
		Call IsLoginWeb("/site/shopping_check_method.asp?pay_method="& pay_method)  
	end if
	'response.write pay_method : response.End()
	
	if pay_method <> "" and isnumeric(pay_method) then 
		set rs = conn.execute("select * from tb_pay_method_new where pay_method_serial_no="& SQLquote(pay_method))
		if not rs.eof then 
			if( cstr(pay_method) <> cstr(LAYOUT_PAYPAL_METHOD_CARD))then 
				conn.execute("update tb_customer set pay_method="&SQLquote(pay_method)&" where customer_serial_no="&SQLquote(LAYOUT_CCID))
			End if
			conn.execute("update tb_cart_temp set pay_method="&SQLquote(pay_method)&" where cart_temp_code='"&LAYOUT_ORDER_CODE&"'")
			session("order_ok_return") 	=	LAYOUT_HOST_URL & rs("pay_check_path_new")
			redirect_path = LAYOUT_HOST_URL & rs("pay_check_path_new")
		end if
		rs.close : set rs = nothing
	end if
	closeconn()	
	'response.write "d":response.End()
	DIM HTH
	
	if lcase(request.ServerVariables("SERVER_NAME")) = "localhost" then 
		HTH	=	""
	else
		HTH	=	"https://www.lucomputers.com/"
	end if

	'HTH = ""
	response.Redirect( HTH  & redirect_path)	
	Response.End()
%>