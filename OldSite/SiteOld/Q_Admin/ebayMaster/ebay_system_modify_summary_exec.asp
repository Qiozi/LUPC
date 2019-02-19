<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<!--#include virtual="site/inc/validate_admin.asp"-->
<!--#include virtual="site/inc/inc_helper.asp"-->
<!--#include file="ebay_inc.asp"-->
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Untitled Page</title>
</head>
<body>
<%
        Dim system_sku      :   system_sku          =   SQLescape(request("system_sku")) 
        Dim summary_value   :   summary_value       =   SQLescape(request("summary_value")) 

        if len(system_sku) >0 and len(system_sku)<10 then
            conn.execute("Update tb_ebay_system set main_comment_ids='"& summary_value &"' where id="& system_sku)        
            response.write "OK"
        end if

closeconn() %>
</body>
</html>
