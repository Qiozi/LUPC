<%@LANGUAGE="VBSCRIPT" CODEPAGE="65001"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

<title>Keyword Manage</title>
<link rel="stylesheet" type="text/css" href="../../js_css/b_lu.css" />
<script type="text/javascript" src="/JS_css/jquery_lab/jquery-1.3.2.min.js"></script>
</head>

<body>
<!--#include virtual="site/inc/validate_admin.asp"-->
<!--#include virtual="site/inc/inc_helper.asp"-->
<%
Dim ID			:	ID				=	SQLescape(request("id"))
	
	Set rs = conn.execute("Select menu_child_serial_no"&_
							" ,menu_child_name "&_
							" from tb_product_category where menu_child_serial_no="&SQLquote(ID))
	If not rs.eof then
		Response.write "<h3>"& rs("menu_child_name") &"</h3>"&vblf
	End if
	Rs.close : set rs = nothing
	Response.write "<hr size=1/>"&vblf
	
	Set rs = Conn.execute("Select  * from tb_product_category_keyword Where category_id="& SQLquote(id)&" order by priority asc ")
	If not rs.eof then
			Response.Write 				"<table class=""table_td_width"">"&vblf
			Do while not rs.eof 
				Response.write 			"	<tr>"&vblf
				Response.write  		"			<td>"&vblf
				Response.write  		"					<input type='text' name='cate_keyword' value='"& rs("keyword") &"' />"&vblf
				Response.write  		"			</td>"&vblf
				Response.write  		"			<td>"&vblf
				Set crs = conn.execute("Select * from tb_product_category_keyword_sub Where parent_id="& SQLquote(rs("id")))
				If not crs.eof then
					Do while not crs.eof 
						Response.write  "					<input type='text' name='child_keyword' value='"& crs("keyword") &"' />"&vblf
						Response.write  "					<input type='checkbox' name='child_showit' value='"& crs("keyword") &"' />showit<br/>"&vblf
					crs.movenext
					loop
				end if
				crs.close : set crs = nothing
				Response.write  		"			</td>"&vblf
				Response.Write 			"	</tr>"&vblf
			rs.movenext
			loop
			Response.write 				"</table>"&vblf
	End if
	rs.close : set rs = nothing
%>
	

<%
CloseConn()
%>
</body>
</html>
