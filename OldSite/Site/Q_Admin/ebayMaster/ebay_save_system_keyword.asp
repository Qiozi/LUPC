<%@LANGUAGE="VBSCRIPT" CODEPAGE="65001"%>
<!--#include virtual="site/inc/validate_admin.asp"-->
<!--#include virtual="site/inc/inc_helper.asp"-->
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>Untitled Document</title>
</head>

<body>


<%
			
		Dim id 		:		id 			= SQLescape(request("id"))
		Dim keyword	:	keyword			= SQLescape(request("keyword"))
	
		if len(id)>0 then
			Conn.execute("Update tb_ebay_system Set keywords="& SQLquote(keyword) &" where id="& SQLquote(id))
			Response.write "<script>alert('it is ok');</script>"
		
		end if
		CloseConn()

%>
</body>
</html>
