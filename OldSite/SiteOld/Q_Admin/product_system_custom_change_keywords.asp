<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>modify system keywords</title>
    <script type="text/javascript" src="JS/lib/jquery-1.3.2.min.js"></script>
</head>
<body>
<!--#include virtual="site/inc/validate_admin.asp"-->
<!--#include virtual="site/inc/inc_helper.asp"-->
<%
    dim sys_sku, curr_keys, curr_key, cmd
    sys_sku = SQLescape(request("sys_sku"))
    cmd = SQLescape(request("cmd"))

    if cmd = "modify" then

        response.write SQLescape(request("keywordGruop"))
        if SQLescape(request("keywordGruop")) <> "" then 
            set rs = conn.execute("select keyword from tb_product_category_keyword_sub where id in ("&SQLescape(request("keywordGruop"))&")")
            if not rs.eof then
                do while not rs.eof 
                    curr_keys = curr_keys & "["& rs(0) &"]"
                rs.movenext
                loop
            end if
            rs.close : set rs = nothing
        end if
        conn.execute("Update tb_ebay_system set keywords='"& curr_keys &"' where id='"& sys_sku &"'")
        closeconn()
        response.write("<script>$('#sysKeyword', window.parent.document).html("""& curr_keys &"""); window.location.href = '/q_admin/product_system_custom_change_keywords.asp?sys_sku="& sys_sku &"'; </script>")
        response.End()
    end if
    
    ' 前一个产品SKU
    set rs = conn.execute("Select SystemSku from tb_ebay_system_and_category ec inner join tb_ebay_system es on es.id=ec.SystemSku where SystemSku < "& sys_sku &" and es.showit=1 order by es.id desc limit 0,1")
    if not rs.eof then
        Response.write "<input type='button' value=""Pre Sys"" onclick='location.href = ""/q_admin/product_system_custom_change_keywords.asp?sys_sku="& rs(0) &"""' > " &vblf
    else
        Response.write "<input type='button' value=""Pre Sys"" onclick='alert(""no sys"");' > "&vblf
   
    end if
    rs.close

    ' 后一个系统
    set rs = conn.execute("Select SystemSku from tb_ebay_system_and_category ec inner join tb_ebay_system es on es.id=ec.SystemSku where SystemSku > "& sys_sku &" and es.showit=1 order by es.id asc limit 0,1")
    if not rs.eof then
        Response.write "<input type='button' value=""Next Sys"" onclick='location.href = ""/q_admin/product_system_custom_change_keywords.asp?sys_sku="& rs(0) &"""' > "&vblf
    else
        Response.write "<input type=""button"" value=""Next Sys"" onclick='alert(""no sys"");' > "&vblf
   
    end if
    rs.close

    Response.write "<hr size=1>"
    Response.write "<h3>"& sys_sku &"</h3>"
    set rs = conn.execute("select keywords, showit, ebay_system_name from tb_ebay_system where id='"& sys_sku &"'")
    if not rs.eof then
        curr_key = rs(0)
        response.Write rs("ebay_system_name") &"<hr size=1>"
        if curr_key <> "" then
            curr_key = replace(curr_key, "][", "|")
            curr_key = replace(curr_key, "]", "")
            curr_key = replace(curr_key, "[", "")
            curr_keys = split(curr_key, "|")
        end if
    end if
    rs.close

    ' 保存按钮
    response.write "<form action='/q_admin/product_system_custom_change_keywords.asp?cmd=modify&sys_sku="& sys_sku &"' method='post'>"
       
    response.write "<input type=""submit"" value=""Save"">"
    response.write "<hr size=1>"
    set rs = conn.execute("select pck.keyword keywordType,pcks.id, pcks.keyword subKeyword, pc.menu_child_name,pc.menu_pre_serial_no"&_
                        " from tb_product_category pc inner join ( "&_
                        " select menu_child_serial_no from tb_product_category "&_
                        " where menu_pre_serial_no=52 and page_category=0 and tag=1 "&_
                        " ) pp on pp.menu_child_serial_no = pc.menu_pre_serial_no and pc.tag=1 "&_
                        " inner join tb_product_category_keyword pck on pck.category_id=pc.menu_child_serial_no "&_
                        " inner join tb_product_category_keyword_sub pcks on pcks.parent_id=pck.id order by menu_pre_serial_no asc, menu_child_name asc")
    if not rs.eof then
        response.Write "<table>"
         do while not rs.eof 
            response.write "<tr>"
            response.Write "    <td> <input name='keywordGruop' value='"& rs("ID") &"' type='checkbox' "
                if curr_key <> "" then 
                    for i=lbound(curr_keys) to ubound(curr_keys)
                        if trim(curr_keys(i)) = trim(rs("subKeyword")) then
                            response.write " checked='checked' "
                        end if 
                    next
                end if
            response.write "    ></td>"
            response.write "    <td> "& rs("keywordType") &"</td>"
            response.write "    <td> "& rs("subKeyword") &"</td>"
            response.write "    <td> ("& getCateName (rs("menu_pre_serial_no")) & "==>"& rs("menu_child_name") &")</td>"
            response.write "</tr>"
        rs.movenext
        loop
      
        response.write "</table>"
    end if
    rs.close : set rs =nothing
    response.write "</form>"
closeconn()

function getCateName(cateid)
    dim rs 
    set rs = conn.execute("select menu_child_name from tb_product_category where menu_child_serial_no='"& cateid &"'")
    if not rs.eof then
        getCatename = rs(0)
    end if
    rs.close : set rs  = nothing

end function

 %>

</body>
</html>
