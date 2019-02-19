<%@LANGUAGE="VBSCRIPT" CODEPAGE="65001"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>Untitled Document</title>
</head>

<body>
<!--#include virtual="/inc/conn.asp"-->
<!--#include virtual="/site/inc/inc_layout.asp"-->

<!--#include virtual="/site/inc/inc_escape.asp"-->
<!--#include virtual="/site/inc/inc_layout_params.asp"-->
<!--#include virtual="/site/inc/inc_func_sys.asp"-->
<!--#include virtual="/site/inc/system_transition_inc.asp"-->
	<%
		' use SYS list, change price.
		Conn.execute("Update tb_product_category Set update_price_date=date_sub(current_date, interval 1 day)")
	
	
		'response.write request.Cookies("CurrentIsEbay")
		'response.End()
		if not CurrentIsEbay then
			Response.Cookies("CurrentIsEbay") = true
			Response.Redirect("/ebay/")
		else
			Response.Cookies("CurrentIsEbay") = false
			Response.Redirect(LAYOUT_HOST_URL )
		end if
		
	%>
</body>
</html>
