
<% 
	dim cagetory_table_name , page_category_request, is_exist_sub, default_view
	
	cagetory_table_name = "tb_product_category"
	page_category_request = encode.IntRequest("page_category")
	default_view = 1
	dim menu_sub_img_1, menu_sub_img_2
	menu_sub_img_1 = "<img src=""/images/jiant4.gif"" border=""0"">"
	menu_sub_img_2 = "<img src=""/images/jiant3.gif"" border=""0"">"
%>

 <!--include virtual="/left_menu.htm"-->
 <qiozi>left_menu</qiozi>
 <!--top 10 begin-->
 <div id="left_top_10_area" style="display:none">
<table width="166" height="28" border="0" cellpadding="0" cellspacing="0" id="__01">
  <tr>
    <td> <img src="/images/title2_01.gif" width="19" height="28" alt=""></td>
    <td width="123" align="center" background="/images/title2_02.gif"><span class="text_orange_13">TOP 10 </span></td>
    <td> <img src="/images/title2_03.gif" width="23" height="28" alt=""></td>
  </tr>
</table>
<table width="165"  border="0" cellspacing="0" cellpadding="0">
  <tr>
    <td height="1"></td>
  </tr>
</table>
<table width="165"  border="0" cellpadding="1" cellspacing="2" bgcolor="#FFFFFF" style="border:#8FC2E2 1px solid; ">
  <tr>
    <td style="border:#E3E3E3 1px solid; "><table width="100%"  border="0" align="center" cellpadding="2" cellspacing="0">
      <%

	set rs = conn.execute("select product_serial_no,menu_child_serial_no, product_short_name, tt.top_comment from tb_top tt inner join tb_product p on p.product_serial_no=tt.top_sku")
	if not rs.eof then 
		i = 0
		do while not rs.eof 
			i = i + 1
	%>
      <tr>
        <td width="10" valign="top" style="padding-top:5px; border-bottom:solid 1px #f2f2f2;" ><img src="/images/products/<%=i%>.gif" width="20" height="11"></td>
        <td valign="top" style="border-bottom:solid 1px #f2f2f2;"><a href="/Product_parts_detail.asp?pro_class=<%= rs("menu_child_serial_no") %>&id=<%=rs("product_serial_no")%>&parent_id=<%=rs("menu_child_serial_no")%>" class="hui-orange-s">
		<%
			if rs("top_comment") = "" then 
				response.Write(rs("product_short_name"))
			else
				response.Write(rs("top_comment"))
			end if
		%>
        </a></td>
      </tr>
      <%
		rs.movenext
		loop
	end if
	rs.close :set rs = nothing
		%>
    </table>
   
    </td>
  </tr>
</table>
</div>
<!--top 10 end-->
<qiozi>view_left_menu</qiozi>

      <table width="165"  border="0" cellspacing="0" cellpadding="0">
        <tr>
          <td height="3"></td>
        </tr>
</table>
 <%
'	set rs=conn.execute ("select advertise_data from tb_advertise where advertise_serial_no=2")
'	if not rs.eof then
'		response.Write rs(0)
'	 if rs("advertise_data")<> "" then response.Write rs(0)
'	end if
'	rs.close :set rs = nothing

dim  left_content_all

left_content_all = ""

right_sql = "select left_content, exist_top from tb_right where right_page='"& current_page_name &"'"

if current_page_name <> "product_list.asp" and current_page_name <> "product_parts_detail.asp" and current_page_name<> "product_detail.asp" then 
	set rs = conn.execute(right_sql)
	if not rs.eof then 
		left_content_all = rs(0)
		if (rs(1)) = 1 then viewTop10()
	end if
	rs.close : set rs = nothing
end if
'response.write left_content_all & current_page_name

' 取得当前界面ID
if current_page_name = "product_list.asp" then 
	params_part_product_category =  request("id")
end if 
if current_page_name = "product_parts_detail.asp" or current_page_name="product_detail.asp" then 
	params_part_product_category =  request("parent_id")
end if


'response.end
	
' 如果当前ID没有，取父ID的内
if isnumeric(params_part_product_category) then 
dim find_content
	find_content = ""
	find_content  =  GetRightContent(current_page_name, params_part_product_category, true, "left_content") 
	if find_content <> "" then 
		left_content_all = find_content
	end if
end if
if left_content_all = "" then 
	set rs = conn.execute("select left_content, exist_top from tb_right where right_page='default'")
	if not rs.eof then 
		left_content_all = rs(0)
		if (rs(1)) = 1 then viewTop10()
	end if
	rs.close : set rs = nothing
end if

response.Write( left_content_all )
 %>
 <br />
 <% if session("user") = LAYOUT_MANAGER_NAME  and  LAYOUT_MANAGER_NAME <> "" then %> 
    <% 
   
    params_id = ""
    if current_page_name = "product_list.asp" then 
        params_id = "&id="&request("id")
    end if 
    if current_page_name = "product_parts_detail.asp" or current_page_name="product_detail.asp" then 
        params_id = "&id="&request("parent_id")
    end if%>
 	<a href="right_manage.aspx?page=<%=current_page_name & params_id%>" onclick="js_callpage_cus(this.href, 'right_manage', 800,600);return false;">Manage</a>
 	
<% end if 
function viewTop10()
	response.Write("<script >document.getElementById(""left_top_10_area"").style.display = '';</script>")
end function
%>
