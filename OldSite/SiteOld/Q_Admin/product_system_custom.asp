<%@LANGUAGE="VBSCRIPT" CODEPAGE="65001"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>LU Computers</title>
<link href="App_Themes/default/admin.css" type="text/css" />
<script language="javascript" src="/js_css/jquery_lab/jquery-1.3.2.min.js"></script>
<script language="javascript" src="/q_admin/js/WinOpen.js"></script>
<script language="javascript" src="/js_css/jquery_lab/popup.js"></script>
<script language="javascript" src="/js_css/jquery_lab/popupclass.js"></script>
<style>
ul ,li { margin:0px; padding: 0px}
/*                    */
.ul_parent li {position:relative;display:inline;}
.ul_parent li ul{ display:none}
.ul_parent li:hover ul{ display:block; border:1px solid #ff3300;background:#f2f2f2;position:absolute;top:10px;margin-top:10px;left:0;}

.ul_parent td { background:#fff}

#part_list_search_result td { font-size: 8.5pt; }
input { font-size: 8pt;}
</style>

</head>
<script type="text/javascript">
function ChangePartComment(the)
{
	var part_comment = document.getElementsByTagName("SELECT");
	var count = 0;
	for(var i=0; i<part_comment.length; i++)
	{
		if(part_comment[i].id.substr(0,12) == "part_comment")
		{
			if(part_comment[i].id == the.id)
			{
				var selectedID = the.options[the.selectedIndex].value;

				document.getElementById("iframe1").src = "product_system_custom_sub.asp?part_group_id="+ selectedID +"&parent_select_control_id=part_detail_" + count;
			}
			count += 1;
		}
	}
	
}
function showAdSelectedArea(i, group_id, part_control_id) {
    document.getElementById("div_av_area").style.display = "";
    var part_title_control = document.getElementById(part_control_id);
    var part_title = part_title_control.options[part_title_control.selectedIndex].text;

    document.getElementById("iframe1").src = "product_system_custom_get_part_search.asp?part_title=" + part_title + "&part_group_id="+ group_id +"&parent_index="+ i;

}
function formSubmit() {
    var keyword = document.getElementById("part_keyword").value;
    if (keyword == "") {
        alert("Please input keyword.");
        document.getElementById("part_keyword").focus();
        return;
    }
    var part_group_id = document.getElementById("part_group_id").value;
    document.getElementById("iframe1").src = "product_system_custom_get_part_search_exec.asp?part_group_id=" + part_group_id + "&part_keyword=" + keyword;

}

function formSubmitSave() {
    var keyword = document.getElementById("part_keyword").value;
    if (keyword == "") {
        alert("Please input keyword.");
        document.getElementById("part_keyword").focus();
        return;
    }
    var part_group_id = document.getElementById("part_group_id").value;
    document.getElementById("iframe1").src = "product_system_custom_get_part_search_save_keyword.asp?part_group_id=" + part_group_id + "&part_keyword=" + keyword;

}

function ChangePartGroupValue(control_id, group_id, lu_sku) {

    var els = document.getElementsByTagName("INPUT");
    var values = "0";
    for (var i = 0; i < els.length; i++) {
        if (els[i].name.indexOf(control_id) != -1) {
            if (els[i].checked) {
               values += "," + els[i].value;
            }
        }            
    }
    document.getElementById("iframe1").src = "product_system_custom_get_part_search_save_group.asp?part_group_id=" + group_id + "&lu_sku=" + lu_sku +"&part_group_ids="+ values;

}
</script>
<body>

<div style="position: absolute ; display:none; left: 200px; top: 200px; background: #FFC7B9; width: 800px;padding: 10px; border: 1px solid #ff9900;padding-top:0px;" id="div_av_area">
</div>
<!--#include virtual="site/inc/validate_admin.asp"-->
<!--#include virtual="site/inc/inc_helper.asp"-->

<div>
    <input type="button" name="hide" value="Hide" onclick="hideSys($(this));"/>
    
    <input type="button" name="moveto" value= "Move To" onclick="moveTo();"/>
    <input type="button" name="copyto" value= "Copy To" onclick="copyTo();"/>
    <input type="button" name="modifySort" value="Modify Part Sort." onclick="modifyPartSort();" />
    <input type="button" name="modifyLogo" value="Modify Logo" onclick="modifyLogo();" />
</div>
<hr size="1" />
<%

	const ITEM_COUNT = 50
	
	dim parts_list(50)
	dim groups_list(50)
	dim part_quantity_list(50)
	dim part_max_quantity_list(50)
	
	for i = 0 to ITEM_COUNT
		parts_list(i) = -1
		groups_list(i) = -1
	next 
	
	dim system_name
	
	dim cmd, menu_child_serial_no, id

    dim keyIds

	
	id  = SQLescape(Request("id"))
	cmd = SQLescape(Request("cmd"))
	menu_child_serial_no = SQLescape(Request("menu_child_serial_no"))

    if menu_child_serial_no = "" then
        set rs = conn.execute("Select system_templete_category_serial_no from tb_system_templete where system_templete_serial_no = '"&id&"'")
        if not rs.eof then
            menu_child_serial_no = rs(0)
        end if
        rs.close : set rs = nothing
    end if
%>
<form action="/q_admin/product_system_custom_exec.asp" method="post">
<input type="hidden" name="menu_child_serial_no" value="<%= menu_child_serial_no%>">
<input type="hidden" name="id" value="<%= id%>">
<input type="hidden" name="cmd" value="<%= request("cmd") %>">
<div style="text-align:center; border-bottom:1px dotted #ccc">
	<%

		set rs = conn.execute(""&_
"select menu_child_serial_no, menu_child_name from tb_product_category where menu_child_serial_no in ("&_
"select menu_pre_serial_no from tb_product_category where menu_child_serial_no='"&menu_child_serial_no&"')"&_
" union all "&_
"select menu_child_serial_no, menu_child_name from tb_product_category where menu_child_serial_no='"&menu_child_serial_no&"'")
		if not rs.eof then 
			dim nn
			nn = 0
            response.write "<div style='text-align:left;'>"
			do while not rs.eof 
				nn = nn + 1
				if nn <> 1 then 
					response.Write(rs(1))
				else
					response.write rs(1) & " -- &gt; "
				end if
			rs.movenext
			loop
            Response.write "-- &gt; SKU: <b>"& id &"</b></div>"
		end if
		rs.close : set rs = nothing

        set rs = conn.execute("select menu_child_name from tb_product_virtual pv "&_
                                " inner join tb_product_category pc on pc.menu_child_serial_no = pv.menu_child_serial_no and pv.lu_sku = '"&id&"'")
        if not rs.eof then
            Response.write "<div style='text-align:left;'>Virtual Category: "
            do while not rs.eof 
                response.write "|<i><b>"& rs(0) &"</b></i><br>"
            rs.movenext
            loop
            Response.write "</div>"
        end if
        rs.close : set rs =nothing
        if cmd = "modify" then 
			response.Write("<input type=""submit"" value=""Modify""/>")		
		elseif cmd = "create" then
			response.Write("<input type=""submit"" value=""New""/>")
		end if

	%>
</div>
<p style='background: #f2f2f2;'>
     <b>Keywords:</b> <input type="button" onclick="return js_callpage_name_custom('/q_admin/product_system_custom_change_keywords.asp?sys_sku=<%= id %>', 'modify_ebay', 800, 700);" value="(press change)" >
    <%
        
        set rs = conn.execute("Select keywords from tb_system_templete where system_templete_serial_no='"& id &"'")
        if not rs.eof then
            keyIds = rs(0)
        end if
        rs.close : set rs = nothing

        response.write keyids
     %>
</p>
<ul class="ul_parent">
	<li style="color: #B9B9D5">
    	Duplicate System.
    	<UL style="border:1px solid #7474AC;margin-top: 2px">
        	<li style="clear:both; background:#fff; font-size:8.5pt; color: #333; ">
            	
            	<table cellpadding="0" cellspacing="1" style="width:100%; min-width:500px">
                	<tr>
                    	<td colspan="3">* 当点击"duplicate"按钮时，系统产品已创建。</td>
                	<%
					set rs = conn.execute("select system_templete_serial_no, system_templete_name from tb_system_templete where tag=1 and is_templete=1 order by system_templete_order")
					if not rs.eof then
						do while not rs.eof 
							
					%>
                    <tr>
                    	<td style="line-height: 25px; width: 70px; text-align:center"><%= rs(0) %></td><td nowrap="nowrap"><%= rs(1) %></td><td><a href="product_system_duplicate.asp?cmd=<%= cmd %>&id=<%= id %>&category=<%= menu_child_serial_no %>&duplicate_id=<%= rs(0) %>">duplicate</a></td>
                    <tr>
                    <%						
						rs.movenext
						loop
					else					
					%>
                    <tr>
                    	<td colspan="3"> 没有模板数据。请在system列表定义模板</td>
                    <tr>
                    <%
					end if
					rs.close : set rs = nothing
					%>
                	                    
                </table>
           </li>
        </UL>
    </li>
</ul>
<div style="border-bottom: 1px dotted #ccc">
	<%
		
	%>
   Title:  <input name="system_name" type="text" maxlength="100" size="100" value="<%= GetSystemName2(id) %>"/>

</div>

<%
set rs = conn.execute("select luc_sku product_serial_no, part_group_id , part_quantity,max_quantity part_max_quantity from tb_ebay_system_parts where system_sku='"& id &"' order by id asc ")
if not rs.eof then
	dim sum
	sum = 0
	do while not rs.eof 
		parts_list(sum) = rs(0)
		groups_list(sum) = rs(1)
		part_quantity_list(sum) = rs(2)
		part_max_quantity_list(sum) = rs(3)
		sum = sum + 1
	rs.movenext
	loop
end if
rs.close : set rs = nothing



dim part_comment
		set rs = conn.execute("select part_group_id, part_group_comment,  part_group_name,is_ebay from tb_part_group pg inner join tb_product_category pc on pc.menu_child_serial_no=pg.product_category where showit=1 and part_group_comment <> '' order by part_group_comment asc ")
		if not rs.eof then 
			part_comment = ("<select name=""part_comment"" id=""part_comment[qiozi]"" onchange=""ChangePartComment(this);"">"&vbcr)
			part_comment = part_comment & "<option value=""-1"">Null</option>"&vbcr
			do while not rs.eof 
				part_comment = part_comment & "<option value="""& rs(0) &""""
				if cint(rs("is_ebay"))=1 then 
				   part_comment = part_comment & " style='color:green' >eBay::"
				else
				   part_comment = part_comment & " >web:::"
				end if
				part_comment = part_comment & rs(1) &"</option>"	&vbcr			
			rs.movenext
			loop
			part_comment = part_comment & "</select>" &vbcr
		end if
		rs.close : set rs = nothing

function GeneratePartCommentSelect(part_comment_select, i, selectedValue)
	if cstr(selectedValue) = "-1" then 
		GeneratePartCommentSelect = replace(part_comment_select, "[qiozi]", "_" & i)
	else	
		GeneratePartCommentSelect = replace(replace(part_comment_select, """" & selectedValue & """",  """"& selectedValue & """ selected"), "[qiozi]", "_" & i)
	end if
end function

function GeneratePartDetail(i, group_id, selectedValue)
	dim rs, is_selected
	'response.Write(group_id)
	if cstr(group_id) <> "-1" and cstr(selectedValue) <> "-1" then 
	
		set rs = conn.execute("select p.product_serial_no ,  concat(concat(lpad(p.product_serial_no, 5, ' '), ' --  '),'[', lpad(p.product_current_price, 7, ' '),']', product_name, '(', p.ltd_stock, ')') product_name, p.tag tag "&_
		                      " from tb_part_group_detail pgd inner join tb_product p on p.product_serial_no=pgd.product_serial_no "&_
		                      " inner join tb_product_category pc on pc.menu_child_serial_no=p.menu_child_serial_no and pc.tag=1"&_
		                      " where pgd.part_group_id='"& group_id &"' and split_line=0 ")
		response.Write("<a href='' onclick=""showAdSelectedArea('"& i &"','"& group_id & "','part_detail_"& i &"');return false;"" >Search</a><select id='part_detail_"& i &"' name=""part_detail"">"&vbcr)
			
		if not rs.eof then 
			'response.Write(group_id)
			do while not rs.eof 
				if cstr(selectedValue) = cstr(rs(0)) then 
					is_selected = " selected='true' "
				else
					is_selected = ""
				end if
				if(cint(rs("tag")) = 1)then
					response.Write("<option value="""& rs(0) &""" "& is_selected &">"& rs(1) &"</option>"&vbcr)
				else
					if(len(is_selected)>5)then 
						response.Write("<option value="""& rs(0) &""" "& is_selected &" style='color:red;'> invalid:::"& rs(1) &"</option>"&vbcr)
					end if
				end if
				
			rs.movenext
			loop
			
		else		
			response.write("<option value='-1'>Null</option>")
		end if
		response.Write("</select> "&vbcr)
		rs.close : set rs = nothing
	else
		GeneratePartDetail = "<select id=""part_detail_"& i &""" name=""part_detail""><option></option></select>"
	end if
end function

function GeneratePartQuantity(i, group_id, value ) 
    if value = "" then value = 1
    response.Write("<input type='text' name='part_quantity' size='1' maxlength='1' id='part_quantity_"& i &"' value='"& value & "'>")
end function

function GeneratePartMaxQuantity(i, group_id, value)
    if value = "" then value = 1
    response.Write("<input type='text' name='part_max_quantity' size='1' maxlength='2' id='part_max_quantity_"& i &"' value='"& value & "'>")
end function


response.Write("<table>")
response.Write("<tr>")
response.Write("      <th>serial</th>")
response.Write("      <th>quantity</th>")
response.Write("      <th>max quantity</th>")
response.Write("      <th>group</th>")
response.Write("      <th>part</th>")
response.Write("</tr>")
for i=1 to ITEM_COUNT
	response.Write("<tr>")
	response.Write("<td nowrap='nowrap'>")
	response.Write(i)
	response.Write("</td>")
	response.Write("<td nowrap='nowrap'>")
	response.Write GeneratePartQuantity(i, groups_list(i-1), part_quantity_list(i-1))
	response.Write("</td>")
	response.Write("<td nowrap='nowrap'>")
	response.Write GeneratePartMaxQuantity(i, groups_list(i-1), part_max_quantity_list(i-1))
	response.Write("</td>")
	response.Write("<td nowrap='nowrap'>")
	response.Write GeneratePartCommentSelect( part_comment, i-1 , groups_list(i-1))
	response.Write("</td>")
	response.Write("<td nowrap='nowrap'>")
	'response.write groups_list(i-1) & "|"& parts_list(i-1)
	response.Write(GeneratePartDetail( i -1 , groups_list(i-1), parts_list(i-1)))
	response.Write("</td>")
	response.Write("</tr>"&vbcr)
next
response.Write("</table>")
closeconn()
%>

</form>

<script type="text/javascript">

	$('select[name=part_detail]').find('option').each(function(){
			if($(this).html().indexOf('valid') >0 )
			{
				$(this).parent().css("border", "1px solid red");
				
			}
	});
	
function hideSys(the)
{
    var sku = '<%= id %>';
    $.ajax({
        type: "POST",
        url: "product_system_custom_cmd.asp",
        data: "cmd=hideShow&sys_sku=" + sku,
        success: function (msg) {
            if (msg == "OK") {
                alert("OK");
                the.val("Show");
            }
            else
                alert("error.");

        }
            , error: function (msg) { alert(msg); }
    });

}

//
// the sys move to other category.
function moveTo()
{
    return js_callpage_name_custom("/q_admin/product_system_move_or_copy.aspx?cmd=Move&sku=<%= id %>", 'view_move', 560, 310); 
}
function copyTo() {
    return js_callpage_name_custom("/q_admin/product_system_move_or_copy.aspx?cmd=Copy&sku=<%= id %>", 'view_copy', 560, 310);
}

//
// modify part Sort on New Window.
function modifyPartSort()
{
    return js_callpage_name_custom("/q_admin/product_system_part_sort.aspx?id=<%= id %>", 'modify_ebay', 800, 700);
}

//
// modify system Logo.
function modifyLogo()
{
    return js_callpage_name_custom("/q_admin/product_helper_system_cpu_logo.aspx?templete_id=<%= id %>" , 'change_logo', 700, 600);
}
</script>
<iframe src="about:blank" id="iframe1" name="iframe1" style="width: 0px; height:0px; " frameborder="0"></iframe>
</body>
</html>
