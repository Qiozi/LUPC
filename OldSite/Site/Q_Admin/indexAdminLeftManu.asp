<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <script type="text/javascript" src="../js_css/jquery-1.9.1.js"></script>
    <style>
        body{font-size:9pt; padding:0px; margin:0px;}
        ul{ padding:1em; padding-top:0.5em; border:0px solid red; margin-top: 0px; padding-bottom:0px;}
    </style>
    <script type="text/javascript">
        function openHref(id) {
           
            $('#iframeCenter', parent.document.body).attr("src", "indexAdminPartEdit.aspx?cid=" + id);
           
        }
    </script>
</head>
<body>
<!--#include virtual="site/inc/validate_admin.asp"-->
<!--#include virtual="site/inc/inc_helper.asp"-->
<%
    dim Sys_sku
    dim cmd
    
    set rs = conn.execute("select menu_child_serial_no , menu_child_name from tb_product_category where menu_pre_serial_no=0 and tag=1 and page_category=1")
    if not rs.eof then
        do while not rs.eof
            response.Write "<ul>"
            Response.Write "    <li>"& rs("menu_child_name") 
            set srs = conn.execute("select menu_child_serial_no , menu_child_name, "&_
                                "(select count(id) from tb_pre_index_page_setting where cateid=pc.menu_child_serial_no) c"&_
                                " from tb_product_category pc where menu_pre_serial_no='"& rs("menu_child_serial_no") &"' and tag=1 and page_category=1")
            if not srs.eof then
                response.Write "<ul>"
                do while not srs.eof
                    Response.Write( "<li><a onclick=""openHref("&srs("menu_child_serial_no")&");return false;"">"& srs("menu_child_name") )
                    if srs("c")>0 then response.Write "<span style='color:blue;'>("& srs("c") & ")</span></a>"
                    response.Write ("</li>")
                srs.movenext
                loop
                response.Write "</ul>"
            end if
            srs.close : set srs = nothing
            response.Write "</li>"
            response.Write "</ul>"
        rs.movenext
        loop
    end if
    rs.close : set rs = nothing

    closeconn()
 %>
</body>
</html>
