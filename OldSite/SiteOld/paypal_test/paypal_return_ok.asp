<%@LANGUAGE="VBSCRIPT" CODEPAGE="936"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
<title>LU Computers</title>
</head>

<body>
<%
response.Redirect("https://api.sandbox.paypal.com/nvp?token="& request("token") &"&METHOD=GetExpressCheckoutDetails")
%>
</body>
</html>
