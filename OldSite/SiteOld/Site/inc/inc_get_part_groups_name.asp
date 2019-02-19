<%@LANGUAGE="VBSCRIPT" CODEPAGE="65001"%>
<!--#include virtual="site/inc/inc_helper.asp"-->
<%
	Dim id		:	id			=	SQLescape(request("id"))
	
	Set rs = conn.execute("select distinct pg.part_group_id, part_group_comment ,is_ebay"&_
							" from tb_part_group_detail  pgd "&_
							" inner join tb_part_group pg on pg.part_group_id=pgd.part_group_id"&_
							" where product_serial_no="& SQLquote(id) &" and pg.showit=1 and pgd.showit=1 ")
	if not rs.eof then
		do while not rs.eof 
			'Response.write "document.write('Hello');"
			if rs("is_ebay") = 1 then 
			    Response.write "<span style='color:green;'>["& rs("part_group_comment")&"]</span>&nbsp;"
			else
			    Response.write "["& rs("part_group_comment")&"]&nbsp;"
			end if
		rs.movenext
		loop
	End if
	rs.close : set rs = nothing

%>