<%@LANGUAGE="VBSCRIPT" CODEPAGE="65001"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>LU Computers</title>
</head>
<script >
var alert_no_login = 'The e-mail address or password is incorrect.\r\n Please retype the e-mail address and password, or sign up if you haven\'t already done so.';
</script>

<body>
<!--#include virtual="site/inc/inc_helper.asp"-->
<%

	Dim  strSQL,rsIP, remember
	
	 remember = SQLescape(request.Form("remember"))
	'response.write cstr(Request("url")) 

		if instr(request.form("username"),"'")<>0 then response.redirect "default.asp"
		if instr(Lcase(request.form("username")),"insert")<>0 then response.redirect "default.asp"
		if instr(Lcase(request.form("username")),"delete")<>0 then response.redirect "default.asp"
		if instr(Lcase(request.form("username")),"select")<>0 then response.redirect "default.asp"
		if instr(Lcase(request.form("username"))," and ")<>0 then response.redirect "default.asp"
		if instr(Lcase(request.form("username"))," or ")<>0 then response.redirect "default.asp"

	Dim logon,pswd
	logon = SQLescape(request.form("username"))
	pswd  = SQLescape(trim(request.form("password")))

	strSQL="Select * from tb_customer where tag=1 and (customer_login_name='" & logon &"' or customer_email1='" & logon &"')"

	set rs = conn.execute(strSql)
	if rs.eof then
		response.write "<script>alert (alert_no_login);window.location.href ='/site/member_login.asp'</script>"
		response.end

	elseif rs("customer_password")<> pswd or len(SQLescape(pswd)) = 0 then 
		response.write "<script>alert (alert_no_login);window.location.href ='/site/member_login.asp'</script>"
		response.end
	else
	
		response.Cookies("customer_serial_no") = rs("customer_serial_no")
		Session("user_id") 			= rs("customer_serial_no")
		session("user") 			= rs("customer_login_name")
		session("Email") 			= rs("customer_email1")
		'if len(SQLescape(rs("tax_execmtion"))) > 0 then
		'	response.Cookies("tax_execmtion") 	= rs("tax_execmtion")
			
		'else
			response.Cookies("tax_execmtion") 	= ""
		'end if
		
		if len(SQLescape(rs("customer_first_name"))) > 0 then
			response.Cookies("customer_first_name") 	= ucase(rs("customer_first_name") )
		end if

		session("customer_last_name") = ucase(rs("customer_last_name"))
		
		if (remember = "1") then 
			response.Cookies("customer_serial_no") = rs("customer_serial_no")
			response.Cookies("customer_serial_no").Expires = DateAdd("m",60,now())
			response.Cookies("user") = rs("customer_login_name")	
			response.Cookies("user").Expires = DateAdd("m",60,now())
			response.Cookies("customer_first_name") = ucase(rs("customer_first_name") )
			response.Cookies("customer_first_name").Expires = DateAdd("m",60,now())
			response.Cookies("customer_last_name") = ucase(rs("customer_last_name"))
			response.Cookies("customer_last_name").Expires = DateAdd("m",60,now())
			Session("IsExistOrder") = false
		end if
		
		
		Set rsIP = Server.CreateObject("ADODB.Recordset")
		rsIP.open "tb_login_log",conn,1,3
		rsIP.addnew
		rsIP("login_name")= rs("customer_serial_no")
		rsIP("remote_address")=request("remote_addr")
		rsIP("login_datetime")=now
		rsip("login_log_category") = "c"
		rsip("http_user_agent") = ""
		rsIP.update
		rsIP.close
		set rsIP=nothing
		
		'
		'	use Arrange pick up 
		'
'		dim pay_method_string
'		pay_method_string = ""
'		if request("pay_method") <> "" then 
'			session("is_run_pick") = false
'			response.Cookies("pick_up_in_person") = 1
'			pay_method_string	= "&pay_method="& request("pay_method")
'		end if
		
		
		select case cstr(Request("url")) 
		    case "1":
		        Response.redirect "/ebay/to_cart.asp"
				response.End()	
			case "2":
				response.Redirect("/site/checkout_paypal_website_payments_pro_setExpressCheckout.asp?paypal=true")
				response.End()				
		    case else   
				if len(Request("url"))>2 then 
					response.Redirect(Request("url"))
				else
		        	response.Redirect("/site/Member_center_info.asp")
				end if
		end select
		'response.write cstr(Request("url")) 
		
		if IsExistOrderCode() then 
			response.redirect "Shopping_Cart.asp"
		else
			response.Redirect("Member_center_info.asp")
		end if
	end if
	rs.close
	set rs=nothing
	
	closeconn()
%>
</body>
</html>
