<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
</head>
<body>
<!--#include virtual="site/inc/validate_admin.asp"-->
<!--#include virtual="site/inc/inc_helper.asp"-->
<%
    dim Sys_sku
    dim cmd
    sys_sku     = SQLescape(Request("sys_sku"))
    cmd         = SQLescape(Request("cmd"))
    
    if cmd = "hideShow" then
        set rs = conn.execute("Select tag from tb_system_templete where system_templete_serial_no='"&sys_sku&"'")
        if not rs.eof then
            if rs("tag") = 1 then
                conn.execute("Update tb_system_templete set tag=0 where system_templete_serial_no='"& sys_sku &"'")
            else
                conn.execute("Update tb_system_templete set tag=1 where system_templete_serial_no='"& sys_sku &"'")

            end if
        end if
        rs.close : set rs = nothing
        
        closeconn()
        response.Clear()
        response.Write("OK")
        response.End()
    end if


    if cmd = "GetKeywords" then
        response.Clear()

        response.End
    end if


    closeconn()
 %>
</body>
</html>
