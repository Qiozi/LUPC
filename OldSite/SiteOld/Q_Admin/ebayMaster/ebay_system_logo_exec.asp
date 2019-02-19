<%@LANGUAGE="VBSCRIPT" CODEPAGE="65001"%>
<!--#include virtual="site/inc/validate_admin.asp"-->
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>ebay system shipping</title>
</head>
<!--#include virtual="site/inc/inc_helper.asp"-->
<body>
<%
	Dim 	ebay_system_sku		:		ebay_system_sku 	=	SQLescape(request("ebay_system_sku"))
	Dim 	logo				:		logo				= 	SQLescape(request("logo"))
	
	
	if( len(ebay_system_sku)>0) then
		Conn.execute("Update tb_ebay_system Set logo_filenames="& SQLquote(logo) &" Where id="&SQLquote(ebay_system_sku) )
		
		Response.write("<script> alert('it is ok');</script>")
	else
		Response.write "Params is error."
	end IF
CloseConn()
%>
</body>
</html>