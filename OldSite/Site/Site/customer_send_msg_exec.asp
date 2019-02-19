<%@LANGUAGE="VBSCRIPT" CODEPAGE="65001"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>LU Computers</title>
</head>

<body>
<!--#include virtual="site/inc/inc_helper.asp"-->

<%
Call ValidateUserSessionInfo(LAYOUT_CCID ,"closeConn")
'Dim tt
'if len(tt)>0 then
'	response.write "Y"
'else
'	response.write "N"
'end if
'
'response.End()
if(len(LAYOUT_CCID)>0)then 
	dim content, order_code, email
	order_code 	= SQLescape(request("order_code"))
	content 	= SQLescape(request("content"))

	set rs =conn.execute("select customer_email2,customer_login_name,customer_email1  from tb_customer_store where order_code='"&order_code&"'")
	if not rs.eof then 
		if IsEmail(rs("customer_email2")) then 
			email = rs("customer_email2")
		end if
		if IsEmail(rs("customer_login_name")) then 
			email = rs("customer_login_name")
		end if
		if IsEmail(rs("customer_email1")) then 
			email = rs("customer_email1")
		end if
	end if
	set rs = nothing


	call CustomerSendMsg( order_code, content)
	call SendEmail("FROM ORDER (" & order_code & ") MESSAGE ", "FROM：(" & order_code & "::"&email&") ORDER <br/>" & content, "sales@lucomputers.com,terryeah@gmail.com")
		
	'conn.execute("insert into tb_chat_msg(msg_order_code,msg_content_text, msg_type,msg_author, regdate)	values ('"&order_code&"', '"&content&"', 1, 'Me', '"& now()&"')")
	closeconn()
end if
%>
<script type="text/javascript">
	opener.location.reload();
	window.close();
</script>
</body>
</html>
