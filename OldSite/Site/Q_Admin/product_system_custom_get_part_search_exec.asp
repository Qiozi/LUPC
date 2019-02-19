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
<div id="div_part_list" >
<%
    dim part_group_id, keyword
    part_group_id= request.QueryString("part_group_id")
    keyword = request.QueryString("part_keyword")
    
    if isnumeric(part_group_id) and keyword <> "" then
         set rs = conn.execute("select product_serial_no, product_current_price , product_name from tb_product where (product_name like '%"&keyword&"%' or keywords like '%"& keyword &"%') and tag=1 and split_line=0 and menu_child_serial_no in ("&_
                                       " select product_category from tb_part_group where part_group_id='"&part_group_id&"' "&_
                                       " union all "&_
                                       " select menu_child_serial_no from tb_product_category pc where menu_pre_serial_no in (select product_category from tb_part_group where part_group_id='"&part_group_id&"')) order by product_current_price asc") 
         if not rs.eof then 
                response.Write("<table id=""part_list_search_result"" cellspacing = ""0"">")
                dim background
                'background = " background: #ffffff; "
                        i = 0
                        do while not rs.eof 
                                i = i +1
                                if background = " background: #ffffff; " then 
                                        background = " background: #ccc; "
                                else
                                        background = " background: #ffffff; "
                                end if
                                response.Write("<form action=""product_system_custom_get_part_search_save_group.asp"" method=""get"" target=""_blank"" name=""form_search_"& i &""">")
                                response.Write("<tr style="""& background &""">")
                                response.Write(       "<td style='width: 40px;'>"&  rs(0) &"</td>")
                                response.Write(       "<td style='text-align:right; width: 60px; padding-right: 5px; color: blue'>$"&  rs(1) &"</td>")
                                response.Write(       "<td>"&  rs(2) &"</td>")
                                response.Write(       "<td><input type=""button"" value=""change group"" onclick=""ChangePartGroupValue('part_group_comment_"& rs(0) &"','"& part_group_id &"', '"& rs(0) &"');""><br /><input type=""button"" value=""Selected"" onclick=""""  disabled=""disabled""/></td>")
                                response.Write(       "<td style=""width: 301px"">"& GetPartGroups(part_group_id, rs(0)) & "</td>")
                                response.Write("</tr>")
                                response.Write("</form>")
                        rs.movenext
                        loop
               response.Write("</table>")
         end if
         rs.close : set rs = nothing
         
    else
            if(keyword = "") then 
                    response.Write "input keyword"
            else
                    response.Write "params is error"
            end if                   
    end if
closeconn() 

function GetPartGroups(part_group_id, lu_sku)
    dim rs ,i 
    set rs = conn.execute("select part_group_id, part_group_comment,"&_ 
    " (select count(pgd.part_group_detail_id) from tb_part_group_detail pgd where showit=1 and product_serial_no='"&lu_sku&"' and pgd.part_group_id = pg.part_group_id) from tb_part_group pg where product_category "&_
    " in (select product_category from tb_part_group where part_group_id='"& part_group_id &"' ) and showit=1 order by priority asc")
    if not rs.eof then
       GetPartGroups =  "<ul class=""ul_parent""><li><div>"
       i = 0
        do while not rs.eof 
                i = i + 1
                GetPartGroups = GetPartGroups &  "<div style=""float:left ; width: 150px;""> <input type=""checkbox"" value="""& rs(0) &""" name=""part_group_comment_"&lu_sku&"_"& i &""""
                if (rs(2) = 1 ) then GetPartGroups = GetPartGroups & " checked = ""checked"""
                GetPartGroups = GetPartGroups & " /> "& rs(1) &"</div>"
        rs.movenext
        loop
       GetPartGroups = GetPartGroups & "</div></li></ul>"
    end if
    rs.close :set rs = nothing
end function
%>
</div>

<script type="text/javascript">
    parent.document.getElementById("div_part_list_area").innerHTML = document.getElementById("div_part_list").innerHTML;
</script>
</body>
</html>
