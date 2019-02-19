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
<div id="div_search_area">
    <div style="text-align: right;"><input type="button" onclick="document.getElementById('div_av_area').style.display='none';" value="close"></div>
    <%
        dim part_title , part_group_id, parent_index
        part_group_id=request.QueryString("part_group_id")
        parent_index = request.QueryString("parent_index")
        part_title = request.QueryString("part_title")
        response.Write "<i>"& part_title &"</i><hr size='1'/>"
     %>
    <form action="product_system_custom_get_part_search_exec.asp" method="post" id="form1_search" name="form1_search">
        <input type="hidden" value="<%= part_group_id %>" name="part_group_id" id="part_group_id" />
        <input type="text" value="" name="part_keyword" id="part_keyword" /><input type="button" id="btn_search" value="Search" onclick="formSubmit();" />
        <input type="button" id="btn_save_keyword" value="Save" onclick="formSubmitSave();" />
    </form>
    <div>
    Quick Search:
            <%
                set rs = conn.execute("select keyword from tb_keyword k inner join tb_part_group pg on pg.product_category=k.category_id where part_group_id='"&part_group_id&"'")
                if not rs.eof then
                    do while not rs.eof 
                        response.Write "<span style='font-size:8.5pt;cursor: point' >" & rs(0) & "</span> <b>|</b> "
                    rs.movenext
                    loop
                end if
                rs.close : set rs = nothing
                
            %>
    </div>
    
    <hr size="1" />
        <div id="div_part_list_area">
        
        </div>
</div>
<%closeconn() %>
<script type="text/javascript">
    parent.document.getElementById("div_av_area").innerHTML = document.getElementById("div_search_area").innerHTML;
</script>
</body>
</html>
