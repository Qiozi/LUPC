<!--#include virtual="site/inc/validate_admin.asp"-->
<!--#include virtual="site/inc/inc_helper.asp"-->
<!--#include virtual="q_admin/funs.asp"-->

<%
	Dim part_group_id 			:		part_group_id 		=	SQLescape(request("part_group_id"))
	Dim split_name				:		split_name 			=	SQLescape(request("split_name"))
	Dim luc_sku					:		luc_sku 			=	SQLescape(request("luc_sku"))
	Dim CategoryID				:		CategoryID 			=	SQLescape(request("CategoryID"))
	Dim cmd						:		cmd 				=	SQLescape(request("cmd"))
	Dim groupIDS				:		groupIDS			=	SQLescape(request("groupIDS"))
	Dim part_showit				:		part_showit			=	SQLescape(request("part_showit"))
	Dim luc_skus				:		luc_skus			=	SQLescape(request("luc_skus"))
	Dim system_sku              :       system_sku          =   SQLescape(request("system_sku"))
	Dim system_name             :       system_name         =   SQLescape(request("system_name"))
	
	dim is_group_exist : is_group_exist = true
%>


<%
	if cmd = "viewGroupDetail" then 
		set rs = conn.execute("select case when p.product_name_long_en<>'' then p.product_name_long_en else product_name end as product_name, p.product_serial_no "&_
							" ,p.product_current_price-p.product_current_discount price"&_
							" ,p.ltd_stock"&_
							" from tb_product p inner join tb_part_group_detail g on p.product_serial_no=g.product_serial_no "&_
							" and g.part_group_id='"&part_group_id&"' "&_
							" where p.split_name='"& split_name &"' and p.tag=1 and p.split_line=0 order by product_name asc ")
		if not rs.eof then
			response.write "<table cellpadding='3' cellspacing='0' width='95%'>"
			Do while not rs.eof 
				Response.write "<tr>"
				Response.write "	<td width='55' ><span class='part__name' id='"& rs("product_serial_no")&"'>["& rs("product_serial_no") &"]</span></td>"
				Response.write "	<td width='50' style='text-align:right'>$"& rs("price") &"</td>"
				Response.write "	<td width='40' style='text-align:center'>"& rs("ltd_stock") &"</td>"
				Response.write "	<td style='padding-left:10px;'><span>" & rs(0) & "</span></td>"
				Response.write "    <td style='color:red;text-align:right'> "& writePartOfSysSKU(rs("product_serial_no"))  &"</td>"

				Response.write "</tr>"
				'Response.write "<div class='part_group_detail_area' style='display:none' title='" & rs(0) & "' id=''>Waitting...</div>"
			rs.movenext
			loop
			Response.write "</table>"
		end if
		rs.close : set rs = nothing
	end if
	
	
	if cmd = "viewPartOfGroupDetail" then 
		set rs = conn.execute("Select * from tb_part_group where product_category='"& categoryID &"'")
		set crs = conn.execute("Select part_group_id from tb_part_group_detail where product_serial_no='"& luc_sku &"'")
		
		if not rs.eof then
			do while not rs.eof 
				is_group_exist = false
				if not crs.eof then
					
					do while not crs.eof 
						if(crs("part_group_id") = rs("part_group_id") ) then 
							Response.write "<div><input type='checkbox' name='chk_part_group' checked='true' value='"&rs("part_group_id")&"'>&nbsp;"& rs("part_group_comment") &"</div>"
							is_group_exist = true
						end if
						
					crs.movenext
					loop
					crs.movefirst
				end if
				if not is_group_exist then
					Response.write "<div><input type='checkbox' name='chk_part_group' value='"&rs("part_group_id")&"'>&nbsp;"& rs("part_group_comment") &"</div>"
				end if
			rs.movenext
			loop
		else
			Response.write "No Group."
		end if
		crs.close : set crs = nothing
		rs.close :set rs = nothing
		Response.write "<hr size='1'>"
		'
		' showit
		set rs = conn.execute("Select tag from tb_product where product_serial_no='"& luc_sku &"'")
		if not rs.eof then
			Response.write "<input type='checkbox' id='part_showit' "
			if rs(0) = 1 then response.write " checked='true' "
			response.write ">&nbsp;showit(退市)<br>"
		end if
		rs.close : set rs = nothing
	end if
	
	
	if cmd = "savePartInfo" then 
		conn.execute("Update tb_product set tag='"& part_showit &"',is_modify=1 where product_serial_no='"& luc_sku &"'")
		
		'if(trim(groupIDS)="") then 
			conn.execute("Delete from tb_part_group_detail where product_serial_no='"& luc_sku &"'")
		'else
			dim group_ids_arry :group_ids_arry = split(groupIDS, ",")
			for i=lbound(group_ids_arry) to ubound(group_ids_arry)
				conn.execute("insert into tb_part_group_detail "&_
							" ( part_group_id, product_serial_no) "&_
							" values "&_
							" ('"& trim(group_ids_arry(i)) &"', '"& luc_sku &"')")
			next
			Response.write "OK.."
		'end if
	end if
	
	if cmd = "savePartsToGroup" then 
	
		dim luc_skus_array 	:	luc_skus_array	=	split(luc_skus, ",")
		for i=lbound(luc_skus_array) to ubound(luc_skus_array)
			if(trim(part_group_id)<>"" and trim(luc_skus_array(i)) <> "") then
				conn.execute("delete from tb_part_group_detail where part_group_id='"& part_group_id &"' and product_serial_no='"&trim(luc_skus_array(i))&"'")
				conn.execute("insert into tb_part_group_detail "&_
							" ( part_group_id, product_serial_no) "&_
							" values "&_
							" ('"& part_group_id &"', '"& trim(luc_skus_array(i)) &"')")
			end if
		next
		Response.write "OK.."
	end if
	
	if cmd = "modifySystemName" then 
	    conn.execute("Update tb_system_templete set system_templete_name = '"& system_name &"' where system_templete_serial_no='"& system_sku &"'")
	    Response.Write "OK"
	end if
	
	if cmd = "hideSystemBySku" then
	    conn.execute("Update tb_system_templete set tag = 0 where system_templete_serial_no='"& system_sku &"'")
	    Response.Write "OK"
	
	end if
%>

<% closeconn() %>

