<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<!--#include virtual="site/inc/validate_admin.asp"-->
<!--#include virtual="site/inc/inc_helper.asp"-->
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Part Price Change Settings</title>
    <script type="text/javascript" src="/JS_css/jquery_lab/jquery-1.3.2.min.js"></script>

    <link href="/js_css/b_lu.css" rel="stylesheet" type="text/css" />
</head>
<body>
<a name='top'></a>

<%
    Set rs = conn.execute("Select menu_child_serial_no , menu_child_name "&_
                        " from tb_product_category "&_
                        " where menu_pre_serial_no=0 and tag=1  order by menu_child_order asc")
    if not rs.eof then
        do while not rs.eof 
            Response.Write "<h4 style='clear:both;'>"& rs("menu_child_name") &"</h4>"          
            
            set crs = conn.execute("select menu_child_serial_no, menu_child_name "&_
                                " from tb_product_category "&_
                                " where menu_pre_serial_no='"& rs("menu_child_serial_no")&"' and tag=1 and page_category=1 "&_
                                " order by menu_child_order asc ")
            if not crs.eof then
                do while not crs.eof
                    response.Write "<a name='top_c_name' href='#"& crs("menu_child_serial_no") &"'>"& crs("menu_child_name") &"</a>"
                    
                crs.movenext
                loop
            end if
            crs.close : set crs = nothing
        rs.movenext
        loop
    end if
    rs.close : set rs = nothing
 %>
 <hr size='1' style="clear:both;">
<%
    Set rs = conn.execute("Select menu_child_serial_no , menu_child_name "&_
                        " from tb_product_category "&_
                        " where menu_pre_serial_no=0 and tag=1 order by menu_child_order asc")
    if not rs.eof then
        do while not rs.eof 
            Response.Write "<h3>"& rs("menu_child_name") &"</h3>"          
            
            set crs = conn.execute("select menu_child_serial_no, menu_child_name "&_
                                " from tb_product_category "&_
                                " where menu_pre_serial_no='"& rs("menu_child_serial_no")&"' and tag=1 and page_category=1 "&_
                                " order by menu_child_order asc ")
            if not crs.eof then
                do while not crs.eof
                    Response.Write "<a name='"& crs("menu_child_serial_no")&"'></a>"
                    Response.Write "<ul>"
                    Response.Write "<li>"
                    Response.Write "<div name='cmd_area'>"
                    Response.Write "<h4 style='padding-left:0px;'>"
                    
                    Response.Write  crs("menu_child_name") 
                    Response.Write "</h4>"
                   
                    Response.write "    <input type='button' value='Default' onclick=""defaultItem('"& crs("menu_child_serial_no") &"');"">"
                    Response.Write "    <input type='button' value='New' onclick="" newItem('"& crs("menu_child_serial_no") &"'); "">"
                    Response.Write "    <input type='button' value='Save' onclick=""saveItem($(this))"">"
                    response.Write "    <input type='hidden' value='"& crs("menu_child_serial_no")&"' name='cid'>"
                    Response.Write "    <a style='float:right;' href='#top' >Top</a>"
                    Response.Write "</div>"
                    set srs = conn.execute("select * "&_
                                        " from tb_part_price_change_setting "&_
                                        " where category_id='"& crs("menu_child_serial_no") &"' "&_
                                        " order by cost_min asc ")
                    
                    if not srs.eof then
                        Response.Write "    <table style='margin-left: 3em;'>"
                        do while not srs.eof                            
                            Response.Write "    <tr>"
                            Response.Write "        <td>"
                            Response.Write "            Special Cash = <input type='text' name='min_cost' value='"&srs("cost_min")&"'> &lt; "
                            Response.Write "        </td>"
                            Response.Write "        <td>"
                            Response.Write "            Cost + Adjustment"
                            Response.Write "        </td>" 
                            Response.Write "        <td>"
                            Response.Write "             &lt;= <input type='text' name='max_cost' value='"& srs("cost_max") &"'> ::::::"
                            Response.Write "        </td>"
                            Response.Write "        <td>"
                            Response.Write "             <input type='text' name='rate' size='3' value='"& srs("rate") &"'> "
                            Response.Write "        </td>"
                            Response.Write "        <td>"
                            if(srs("is_percent") = "1")then
                                Response.Write "             <input type='checkbox' name='percent' checked='checked' >% "
                            else
                                Response.Write "             <input type='checkbox' name='percent' >% "
                            end if
                            Response.Write "        </td>"
                            response.Write "    </tr>"     
                        srs.movenext
                        loop
                        response.Write "    </table>"
                        
                    end if
                    srs.close : set srs = nothing
                    response.Write "    </li>"
                    Response.Write "</ul>"
                
                crs.movenext
                loop            
            end if
            crs.close : set crs = nothing
        
        rs.movenext
        loop
    
    end if
    rs.close :set rs = nothing


closeconn() %>

<script type="text/javascript">

    $().ready(function() {
        $('a[name=top_c_name]').css({ 'display': 'block', 'float': 'left', 'padding': '3px', 'margin-left': '5px' });
        $('h3').css({ 'font-size': '12pt' });
        $('h4').css({ 'color': 'green', 'font-weight': 'bold', 'font-size': '10pt' });
        $('div[name=cmd_area]').css({ 'padding': '3px 3px 3px 2em', 'background': '#f2f2f2', 'border': '1px solid #ccc' });
        $('input[type=text]').css({ 'text-align': 'right' });
    });
    function newItem(cid) {
        $.ajax({
            type: "get",
            url: "/q_admin/part_price_change_setting_cmd.asp",
            data: "cmd=new&category_id=" + cid,
            success: function(msg) {
                if (msg.indexOf('OK') != -1)
                    window.location.reload();
            },
            error: function(msg) { alert('error:' + msg); }
        });
    }

    function defaultItem(cid) {
        if (confirm('Are You Sure.')) {
            $.ajax({
                type: "get",
                url: "/q_admin/part_price_change_setting_cmd.asp",
                data: "cmd=default&category_id=" + cid,
                success: function(msg) {
                    if (msg.indexOf('OK') != -1)
                        window.location.reload();
                },
                error: function(msg) { alert('error:' + msg); }
            });
        } 
    }
        

    function saveItem(e) {
        var vs = "";
        var mins = "";
        var maxs = "";
        var rates = "";
        var percents = "";
        var cid = '';

        e.parent().parent().find('input').each(function() {
            if ($(this).attr('name') == 'min_cost') {
                if (mins == "")
                    mins = $(this).val();
                else
                    mins += ',' + $(this).val();
            }

            if ($(this).attr('name') == 'max_cost') {
                if (maxs == "")
                    maxs = $(this).val();
                else
                    maxs += ',' + $(this).val();
            }

            if ($(this).attr('name') == 'rate') {
                if (rates == "")
                    rates = $(this).val();
                else
                    rates += ',' + $(this).val();
            }

            if ($(this).attr('name') == 'percent') {
                if (!$(this).attr('checked')) {
                    if (percents == "")
                        percents = "0";
                    else
                        percents += ',0';
                }
                else {
                    if (percents == "")
                        percents = "1";
                    else
                        percents += ',1';
                }
            }

            if ($(this).attr('name') == 'cid') {
                cid = $(this).val();
            }
        });

        $.ajax({
            type: "get",
            url: "/q_admin/part_price_change_setting_cmd.asp",
            data: "cmd=save&category_id=" + cid + "&mins=" + mins + "&maxs=" + maxs + "&rates=" + rates + "&percents=" + percents,
            success: function(msg) {
                if (msg.indexOf('OK') != -1)
                    window.location.reload();
            },
            error: function(msg) { alert('error:' + msg); }
        });
    }
</script>
</body>
</html>
