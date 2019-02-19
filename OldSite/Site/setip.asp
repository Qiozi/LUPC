<%@LANGUAGE="VBSCRIPT" CODEPAGE="65001"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>LU Computers</title>
</head>
<!--#include file="public_helper/public_helper.asp"-->
<%
dim ip
ip = encode.stringrequest("ip")
if ip <> "" then 
	set rs = server.CreateObject("adodb.recordset")
	rs.open "select * from tb_luip where id=1", conn,1,3
	rs("ip") = ip
	rs("create_datetime") = now()
	rs.update()
	rs.close : set rs = nothing
else
	set rs = conn.execute("select * from tb_luip")
	if not rs.eof then
		do while not rs.eof 
			response.Write("<div >"& rs("ip") &"&nbsp;&nbsp;&nbsp;&nbsp;"& rs("create_datetime")&"</div>")
		rs.movenext
		loop
	end if
	rs.close : set rs = nothing
end if
closeconn()
%>

<body>
</body>
</html>
