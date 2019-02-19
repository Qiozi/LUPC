<%@LANGUAGE="VBSCRIPT" CODEPAGE="65001"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<title>LU Computers</title>
</head>

<body>
<!--#include virtual="site/inc/inc_helper.asp"-->

<%
dim cmd , news_email
news_email = SQLescape(request("news_email"))

cmd = trim(request("cmd"))
if (cmd = "cancel" ) then 
	NewsLetter news_email, 0 
	%>
		<script>
			//alert("NewsLetter is cancel.")
			alert("NewsLetter is cancel");
			window.history.go(-1)
		</script>
	<%
else
	set rs=server.CreateObject("adodb.recordset")
	rs.open "select * from tb_news_letter where email='"&news_email&"'",conn,1,3
	if not rs.eof then
		if cint(rs("tag")) = 1 then 
	%>
			<script>alert ("You've registed before!(At <%=rs("regdate")%>)")</script>
	<%
		else
			conn.execute("update tb_news_letter set tag=1 where  email='"&news_email&"'")
	%>
		<script>alert ("Registed complete!")</script>
		<script>window.history.go(-1)</script>
	<%
		end if
	else

	rs.close : set rs = nothing
	
	NewsLetter news_email, 1 
	closeConn()
	set encode = nothing
	set code_helper = nothing
	
	'response.write news_email
	%>
	<script>alert ("Registed complete!")</script>
	<%end if%>
	<script>window.history.go(-1)</script>
<% end if %>
</body>
</html>
