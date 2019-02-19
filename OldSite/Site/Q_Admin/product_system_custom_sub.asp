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
<select id="main">
<%
	dim part_group_id, parent_select_control_id
	part_group_id 				= SQLescape(Request("part_group_id"))
	parent_select_control_id 	= SQLescape(Request("parent_select_control_id"))
	

	set rs = conn.execute("select p.product_serial_no , concat(concat(lpad(p.product_serial_no, 5, ' '), ' --  '),'[', lpad(p.product_current_price, 7, ' '),']', product_name) product_name ,p.tag p_tag from tb_part_group_detail pgd inner join tb_product p on p.product_serial_no=pgd.product_serial_no where pgd.part_group_id='"& part_group_id &"' and split_line=0 and p.tag=1")
	if not rs.eof then
		do while not rs.eof 
			if(cint(rs("p_tag")) = 0) then
				response.Write("<option value='"& rs(0) &"' style='color:red;'> invalid:::"& rs(1) &"</option>")
			else
				
				response.Write("<option value='"& rs(0) &"'>"& rs(1) &"</option>")
			end if
		rs.movenext
		loop
    else
        response.write("<option value='-1'>Null</option>")
	end if
	rs.close : set rs = nothing
	
closeconn()
%>
</select>
<script type="text/javascript">
	parent.document.getElementById("<%= parent_select_control_id %>").innerHTML=document.getElementById("main").innerHTML;

</script>
</body>
</html>