<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>LU Computers</title>
    <link href="App_Themes/default/admin.css" type="text/css" />
</head>
<body>
<!--#include virtual="site/inc/validate_admin.asp"-->
<!--#include virtual="site/inc/inc_helper.asp"-->
<%
    dim part_group_id, keyword
    part_group_id 	= SQLescape(Request("part_group_id"))
    keyword 		= SQLescape(Request("part_keyword"))
    
    if isnumeric(part_group_id) and keyword <>"" then
        conn.execute("insert into tb_keyword "&_
	" ( keyword, category_id) "&_
	" select '"&keyword&"', product_category from tb_part_group  where part_group_id = '"&part_group_id&"'")
	    response.Write("<script>alert('OK');</script>")
	else
	    response.Write("<script>alert('error');</script>")
    end if    
closeconn()
%>
</body>
</html>
