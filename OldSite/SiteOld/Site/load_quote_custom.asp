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
closeconn()


dim id, cmd
id 		= SQLescape(request("id"))
cmd 	= SQLescape(Request("cmd"))

Session("SystemSku8") = ""
 
if len(id) = 8 then
	if cmd = "custom" then 
		if request("category") ="invalid" then 
			response.Redirect(LAYOUT_HOST_URL & "computer_system.asp?system_code="& id )
		else
			response.Redirect(LAYOUT_HOST_URL & "computer_system.asp?system_code="& id )
		end if
	end if
	
	if cmd = "buy" then
		response.Redirect(LAYOUT_HOST_URL & "computer_system_to_cart_next.asp?cmd=customize&system_sku="& id)
	end if


end if

%>

</body>
</html>
