<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=gb2312">
<title>LU Computers</title>
</head>

<body>

<!--#include virtual="site/inc/inc_helper.asp"-->

<%
response.Cookies("customer_serial_no") 	= ""
session("user") 						= ""
session("Email") 						= ""
response.Cookies("tax_execmtion") 		=  ""
response.Cookies("customer_first_name") 			= ""
session("customer_last_name") 			= ""
response.Cookies("customer_serial_no") 	= ""
response.Cookies("user") 				= ""
response.Cookies("customer_first_name") = ""

response.Cookies("customer_last_name") 	= ""

response.Redirect(LAYOUT_HOST_URL)
%>
</body>
</html>
