<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>Untitled Document</title>
</head>

<body>
<!--#include virtual="site/inc/inc_helper.asp"-->

<%
	   ' On Error Resume Next
		'if session("email_to_me") = true then 
			dim id, email1,email2, email_type,email
			email		=	""
			id 			= request.form("id")
			email1 		= request.form("textfield")
			email2 		= request.form("textfield2")
			email_type 	= request.form("email_type")
			
			if IsEmail(email2) and IsEmail(email1) then 
				email 	= 	email1 & "," & email2
			elseif IsEmail(email2) then
				email	=	email2 
			elseif IsEmail(email1) then
				email 	= 	email1
			end if
			
			dim body_text
			
			if email_type = "8" then 
				'Response.write toUrl("view_configure_system.asp?no_href=yes&system_code="&id)
				body_text = getHTTPPage(toUrl("view_configure_system.asp?no_href=yes&system_code="&id))
			elseif email_type="12" then
				'response.write "<script>alert('"&toUrl("/ebay/system_view_mini.asp?no_href=yes&system_code="&id)&"');<script>"
				'response.end
				body_text = getHTTPPage(REPLACE(toUrl("/ebay/system_view_mini.asp?no_href=yes&system_code="&id), "/site/", ""))
			else
			
				body_text = getHTTPPage(toUrl("email_to_cutomer_system.asp?no_href=yes&email_to_me=yes&id="&id))
			end if
			body_text = replace(body_text, "src=""", "src="""& "http://"& request.ServerVariables("SERVER_NAME")&":"& request.ServerVariables("server_port"))
			body_text = replace(body_text, "(/", "(http://"& request.ServerVariables("SERVER_NAME")&":"& request.ServerVariables("server_port")&"/")
			response.write body_text
			'body_text = request.Form("body_html")
'			set jmail=server.CreateObject ("jmail.message")
'			jmail.Logging = true
'			jmail.Silent=true
'			jmail.charset="utf-8"
'			jmail.addrecipient "809840415@qq.com"'email1
'			jmail.subject = " Lu Computers - Product Information "&session("send_code")
'			jmail.appendtext "This is a mail of HTML type"
'			jmail.appendhtml "Hello World"'body_text
'	
'			jmail.from = "xiaowu021@126.com"
'			jmail.fromname="LU COMPUTERS"
'			jmail.priority = 3
'		
'			jmail.MailServerUserName ="xiaowu021@126.com"'"sales@lucomputers.com"
'			jmail.MailServerPassWord = "1234qwer"'"5calls2day"
'			eeerr=jmail.send("smtp.126.com")'"mail.lucomputers.com")
'			Response.write (jmail.ErrorCode&"<br>")
'			response.write (JMail.ErrorMessage &"<br/>")
'			response.write (jmail.ErrorSource &"<br/>")
'			response.write (jmail.log &"<br/>")
'			jmail.close
'			set jmail=nothing
'			response.write eeerr
		if email <> "" then 
			Dim result	:	result = SendEmail("Lu Computers - Product Information "&id, body_text, email)	
			
			'end if
			'If Err <> 0 Then ' error occurred
			'	   response.Clear()
			'	   response.write Err.Description
			'end if
			'REsponse.Write(Err)
			
			'On Error Goto 0
			if result then 
				response.write "<script> alert('System quote number and its specifications have been sent!  \r\nPlease reload your system on www.lucomputers.com with your Quote Number.\r\nPlease always refer to this Quote Number when contact a LU Computers representative.\r\nThank you!');parent.window.close();</script>"
			else   
				response.write "<script> alert('error!.');</script>"
			end if
		End if
	%>
</body>
</html>