<%@LANGUAGE="VBSCRIPT" CODEPAGE="65001"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>Untitled Document</title>
</head>

<body>
<!--#include virtual="site/inc/inc_helper.asp"-->
<%
 CloseConn()
 if not CurrentIsEbay then 
	response.Redirect("/site/shopping_cart.asp")
 else
 	Response.Redirect("/ebay/cart.asp")
 End if

%>
</body>
</html>
