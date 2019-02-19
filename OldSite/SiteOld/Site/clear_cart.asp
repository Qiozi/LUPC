<%@LANGUAGE="VBSCRIPT" CODEPAGE="65001"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<title>LU Computers</title>
</head>

<body>
<!--#include virtual="site/inc/inc_helper.asp"-->
<%
	if len(LAYOUT_ORDER_CODE) = 6 then 
		conn.execute("delete from tb_cart_temp where cart_temp_code="& SQLquote(LAYOUT_ORDER_CODE))
	end if

	closeconn()
	if(request("returnURL")= "ebay") then
		response.Redirect("/ebay/")
	else
		response.Redirect("/site/")
	end if
%>
</body>
</html>