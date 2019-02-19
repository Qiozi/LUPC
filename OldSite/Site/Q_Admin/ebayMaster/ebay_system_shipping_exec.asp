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
	Dim 	shipping_id			:		shipping_id			= 	SQLescape(request("shipping_id"))
	Dim 	shipping_charge		:		shipping_charge		=	SQLescape(request("shipping_charge"))
	

	Dim 	shipping_id_g			:		shipping_id_g		= 	null
	Dim 	shipping_charge_g		:		shipping_charge_g	=	null
	
	if( len(shipping_id)>0) then
		shipping_id_g	=	split(shipping_id, ",")
		shipping_charge_g=	split(shipping_charge, ",")
		Response.write ubound(shipping_id_g) &"<br>"
		Response.write ubound(shipping_charge_g) &"<br>"
		Conn.execute("delete from tb_ebay_system_shipping where ebay_system_sku="& SQLquote(ebay_system_sku) )
		
		for i = lbound(shipping_id_g) to ubound(shipping_id_g)
			
			conn.execute("Insert into tb_ebay_system_shipping(ebay_system_sku, shipping_company_id, shipping_charge) "&_
								" values ( "& SQLquote(ebay_system_sku)&" , "& SQLquote(trim(shipping_id_g(i)))&", "&SQLquote(trim(shipping_charge_g(i)))&"); ")
		next
		Response.redirect("ebay_system_edit.asp?ebay_system_sku="& ebay_system_sku&"&cmd="&request("cmd"))
	else
		Response.write "Params is error."
	end IF
CloseConn()
%>
</body>
</html>
