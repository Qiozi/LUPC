<%@LANGUAGE="VBSCRIPT" CODEPAGE="65001"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>LU Computers</title>
</head>

<body>

<div><h3>Order#:&nbsp;<%= request("order_code")%></h3></div>
<form action="customer_send_msg_exec.asp" method="post">
	<input type="hidden" name="order_code" value="<%= request("order_code")%>" />
	<textarea rows="10" name="content" cols="50" tabindex="1"></textarea>
  <div style="text-align:center">
  <input type="submit" value="Submit" tabindex="2" /> <input type="button" value="Cancel" tabindex="3" onclick="javascript: window.close();"/> 
   </div>
</form>
</body>
</html>
