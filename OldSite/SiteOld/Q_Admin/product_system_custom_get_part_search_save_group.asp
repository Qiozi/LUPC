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
    dim group_id, group_ids, lu_sku, group_idss
    lu_sku 		= SQLescape(Request("lu_sku"))
    group_ids 	= SQLescape(Request("part_group_ids"))
    if(lu_sku <>"") then 
        conn.execute("delete from tb_part_group_detail where product_serial_no='"& lu_sku &"'")
        group_idss = split(group_ids, ",")
        for i = lbound(group_idss) to ubound(group_idss)
              group_id = trim(group_idss(i))
              if(group_id <> "0") then 
                        conn.execute("insert into tb_part_group_detail "&_
	                                        " ( part_group_id, product_serial_no ) "&_
	                                        " values "&_
	                                        " ( '"&group_id&"', '"&lu_sku&"')")
              end if  
        next
        response.Write("<script> alert('OK');</script>")
    end if
closeconn() %>
</body>
</html>
