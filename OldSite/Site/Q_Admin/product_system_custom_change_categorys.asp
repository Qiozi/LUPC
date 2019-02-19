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
    dim sys_sku, curr_keys, curr_key, cmd, cateArray
    sys_sku = SQLescape(request("sys_sku"))
    cmd = SQLescape(request("cmd"))
    curr_keys = ""

    if cmd = "modify" then

        response.write SQLescape(request("keywordGruop"))
        curr_keys = SQLescape(request("keywordGruop"))
        if SQLescape(request("keywordGruop")) <> "" then 

            cateArray = split(SQLescape(request("keywordGruop")), ",")
            conn.execute("Delete from tb_ebay_system_and_category where SystemSku='"& trim(sys_sku) &"'")
            for i=lbound(cateArray) to ubound(cateArray)
                conn.execute("Insert into tb_ebay_system_and_category(eBaySysCategoryID, SystemSku) values ('"& trim(cateArray(i)) &"', '"& trim(sys_sku) &"')")

            next
            
            'set rs = conn.execute("select keyword from tb_product_category_keyword_sub where id in ("&SQLescape(request("keywordGruop"))&")")
            'if not rs.eof then
            '    do while not rs.eof 
            '        curr_keys = curr_keys & "["& rs(0) &"]"
            '    rs.movenext
            '    loop
            'end if
            'rs.close : set rs = nothing
        end if
        'conn.execute("Update tb_ebay_system set keywords='"& curr_keys &"' where id='"& sys_sku &"'")
        closeconn()
        response.write("<script>$('#sysCategory', window.parent.document).html("""& request("keywordGruop") &"""); window.location.href = '/q_admin/product_system_custom_change_categorys.asp?sys_sku="& sys_sku &"'; </script>")
        response.End()
    else
        set rs = conn.execute("Select * from tb_ebay_system_and_category where SystemSku='"& trim(sys_sku) &"'")
        if not rs.eof then
            do while not rs.eof 
            curr_keys = curr_keys & "["& rs("eBaySysCategoryID") &"]"
            rs.movenext
            loop
        end if
        rs.close : set rs = nothing
        
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
        response.Write rs("ebay_system_name") &"<hr size=1>"        
    end if
    rs.close

    ' 保存按钮
    response.write "<form action='/q_admin/product_system_custom_change_categorys.asp?cmd=modify&sys_sku="& sys_sku &"' method='post'>"
       
    response.write "<input type=""submit"" value=""Save"">"
    response.write "<hr size=1>"
    'response.Write curr_keys
    response.Write "<table>"
    set rs = conn.execute("select menu_child_serial_no , menu_child_name, menu_is_exist_sub from tb_product_category where tag=1 and page_category=0 and menu_pre_serial_no=0 order by menu_child_order asc")
   ' set rs = conn.execute("select pck.keyword keywordType,pcks.id, pcks.keyword subKeyword, pc.menu_child_name"&_
    '                    " from tb_product_category pc inner join ( "&_
    '                    " select menu_child_serial_no from tb_product_category "&_
    ''                    " where menu_pre_serial_no=0 and page_category=0 and tag=1 "&_
    '                    " ) pp on pp.menu_child_serial_no = pc.menu_pre_serial_no and pc.tag=1 "&_
    '                    " inner join tb_product_category_keyword pck on pck.category_id=pc.menu_child_serial_no "&_
    '                    " inner join tb_product_category_keyword_sub pcks on pcks.parent_id=pck.id order by subKeyword asc, menu_child_name asc")
    if not rs.eof then
       
         do while not rs.eof 

             if rs("menu_is_exist_sub") <> 1 then
                    response.write "<tr>"
                    response.Write "    <td> <input name='keywordGruop' value='"& rs("menu_child_serial_no") &"' type='checkbox' "
                        if curr_keys <> "" then 
                            if instr(curr_keys, "[" & trim(rs("menu_child_serial_no")) &"]") > 0 then
                               
                                    response.write " checked='checked' "
                             
                            end if
                        end if
                    response.write "    ></td>"
                    response.write "    <td> "& rs("menu_child_name") &"</td>"
                response.write "</tr>"
              end if

            set crs = conn.execute("select menu_child_serial_no , menu_child_name, menu_is_exist_sub from tb_product_category where tag=1 and page_category=0 and menu_pre_serial_no='"& rs("menu_child_serial_no") &"' order by menu_child_order asc")
            if not crs.eof then

                do while not crs.eof 
                    partCateName = crs("menu_child_name")
                    if crs("menu_is_exist_sub") <> 1 then
                        response.write "<tr>"
                        response.Write "    <td>&nbsp;&nbsp;&nbsp;&nbsp;<input name='keywordGruop' value='"& crs("menu_child_serial_no") &"' type='checkbox' "
                            if curr_keys <> "" then 
                               if instr(curr_keys, "[" & trim(crs("menu_child_serial_no")) &"]") > 0 then
                               
                                        response.write " checked='checked' "
                               end if                               
                            end if
                        response.write "    >"
                        response.write "    "& crs("menu_child_name") &"</td>"
                        response.write "</tr>"
                    end if
                        set srs = conn.execute("select menu_child_serial_no , menu_child_name, menu_is_exist_sub from tb_product_category where tag=1 and page_category=0 and menu_pre_serial_no='"& crs("menu_child_serial_no") &"' order by menu_child_order asc")
                        if not srs.eof then
                            do while not srs.eof 
                                if srs("menu_is_exist_sub") <> 1 then
                                    response.write "<tr>"
                                    response.Write "    <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input name='keywordGruop' value='"& srs("menu_child_serial_no") &"' type='checkbox' "
                                        if curr_keys <> "" then 
                                           if instr(curr_keys, "[" & trim(srs("menu_child_serial_no")) &"]") > 0 then
                               
                                                    response.write " checked='checked' "
                                           end if 
                                           
                                        end if
                                    response.write "    >"
                                    response.write "  "&partCateName & "==>"& srs("menu_child_name") &"</td>"
                                    response.write "</tr>"

                                    
                                end if
                            srs.movenext
                            loop
                        end if
                        srs.close : set srs = nothing
                    
                crs.movenext
                loop
            end if
            crs.close : set crs = nothing
        rs.movenext
        loop
      
        
    end if
    rs.close : set rs =nothing
    response.write "</table>"
    response.write "</form>"
closeconn() %>

</body>
</html>
