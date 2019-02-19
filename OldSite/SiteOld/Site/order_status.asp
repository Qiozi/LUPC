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
	
	if LAYOUT_CCID = "" then 
		response.Redirect(LAYOUT_HOST_URL & "member_login.asp")
	else
		response.Redirect(LAYOUT_HOST_URL & "member_center_Porder.asp")
	end if
	response.End()
%>
</body>
</html>
