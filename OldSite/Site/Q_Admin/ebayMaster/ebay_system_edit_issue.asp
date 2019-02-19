<%@LANGUAGE="VBSCRIPT" CODEPAGE="65001"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>Untitled Document</title>
</head>

<body>
<!--#include virtual="site/inc/validate_admin.asp"-->
<!--#include virtual="site/inc/inc_helper.asp"-->
<!--#include file="ebay_inc.asp"-->

<%
Dim sys_sku :	sys_sku	=	SQLescape(request("sys_sku"))

conn.execute("Update tb_ebay_system set is_issue=1 where id = '"&sys_sku&"'")

closeConn()
%>
<script type="text/javascript">
alert("it is ok");
</script>
</body>
</html>
