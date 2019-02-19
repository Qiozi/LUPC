<!--#include virtual="site/inc/inc_helper.asp"-->

<table width="100%" cellpadding="0" cellspacing="0">
<% 

dim on_sale_font_style : on_sale_font_style=""'" style=' color: blue; ' "	
    
dim ids, group_id, plane_count, sys_tmp_detail, incept_id, part_group_id
dim plane_count_sub
    incept_id 		= SQLescape(request("incept_id"))
    sys_tmp_detail 	= SQLescape(request("sys_tmp_detail"))
    plane_count 	= SQLescape(request("plane_count"))
    group_id 		= SQLescape(request("group_id"))
    ids 			= SQLescape(request("ids"))
    sys_tmp_sku 	= SQLescape(request("sys_tmp_sku"))
Dim split_name			: split_name		= SQLescape(request("split_name"))

    part_group_id = group_id
    
dim part_max_quantity 	: part_max_quantity = SQLescape(request("part_max_quantity"))
dim part_quantity 		: part_quantity 	= SQLescape(request("part_quantity"))
dim lu_sku_selected 	: lu_sku_selected 	= SQLescape(request("lu_sku_selected"))

    
    

'response.Write(ids)
if ids <> "" then 
	
	set rs = conn.execute("select p.product_serial_no, p.product_name,p.product_name_long_en,p.product_current_discount, p.menu_child_serial_no,p.product_current_price, p.product_short_name, p.other_product_sku, p.manufacturer_part_number"&_
" ,case when p.product_store_sum>p.ltd_stock then p.product_store_sum else p.ltd_stock end as store_sum "&_
" from tb_product p inner join tb_part_group_detail pgd on p.product_serial_no=pgd.product_serial_no"&_
" where pgd.part_group_id='"& group_id &"' and p.tag=1 and p.split_line=0 and p.product_serial_no not in ("& ids &") and p.split_name ='"& split_name &"' order by p.product_name asc ")

	if not rs.eof then 
		
			plane_count_sub = 0
		do while not rs.eof 
		    current_price_rate 	= ConvertDecimal(cdbl(rs("product_current_price")))
		    product_serial_no 	= rs("product_serial_no")
			plane_count_sub 	= plane_count_sub + 1
			save_cost 			= ConvertDecimal(cdbl(rs("product_current_discount")))
%>
			<tr onMouseOver="this.bgColor='#f2f2f2';" onMouseOut="this.bgColor='white';" >
				<td width="60" style="text-align:left">
					<span style="position:relative;">
						<span style="position:absolute; z-index:<%= clng(sys_tmp_detail) + 10 %>; top: 0px; left: 0px">
                        <% 
						
						'if PartChoosePhotoSKU(rs("product_serial_no"),rs("other_product_sku"))  then %>
							<img src="<%=HTTP_PART_GALLERY & PartChoosePhotoSKU(rs("product_serial_no"),rs("other_product_sku"))%>_t.jpg" 
							width="50" 
							height="50" 
							id="img_product_<%= sys_tmp_detail %>_<%=rs("product_serial_no")%>" 
							style="display:none;cursor: pointer" 
							onClick="javascript:popImage('<%=HTTP_PART_GALLERY & PartChoosePhotoSKU(rs("product_serial_no"),rs("other_product_sku"))%>_list_1.jpg','Lu Computers','middle_center',true,true);return false;" 
                            onerror="imgerror(this);"
							>
						<% 'end if %>
						</span>
					</span>	
				</td>
				<td width="25" nowrap="nowrap" name="product_group_<%=rs("menu_child_serial_no")%>" id="product_group_<%=rs("product_serial_no")%>"  valign="top" style="!important;padding-top:1px; ">				
				<table cellpadding="0" cellspacing="0">
					<tr>
					    <td><input 
					name="product_check_<%=sys_tmp_detail%>" 
					id="product_check_<%=rs("product_serial_no")&"_"&plane_count&"_"&plane_count_sub%>" 
					type="radio" 
					value="<%=rs("product_serial_no")%>" 
					onclick="getProductName('img_product_<%= sys_tmp_detail %>_<%=rs("product_serial_no")%>', 'product_child_img_product_<%= sys_tmp_detail %>_<%=rs("product_serial_no")%>','product_head_<%= sys_tmp_detail%>', '<%= sys_tmp_detail%>','<%=rs("product_serial_no")%>','<%= sys_tmp_sku %>');"
                     tag='<%= current_price_rate %>'
                    >
				 </td>	
				    <%response.Write ExportSelectControl( rs("product_serial_no") , part_max_quantity, part_quantity, lu_sku_selected, group_id, "product_check_"& rs("product_serial_no")&"_"&plane_count&"_"&plane_count_sub, sys_tmp_detail) %>
				 </tr>
				 </table>
				</td>
				<td valign="top" style="text-align:left">
					<table width="100%">
						<tr>
							<td style="text-align:left">
								<%=WritePartStoreSumAtSysList(rs("store_sum")) %>
                                <a class="hui-red" href="/site/view_part.asp?id=<%= rs("product_serial_no")%>" 
								onClick="js_callpage_name(this.href, 'custom_part_detail');return false;"  
								id="product_child_img_product_<%= sys_tmp_detail %>_<%=rs("product_serial_no")%>">
									<% 
									product_title = rs("product_name_long_en")
									if product_title = "" then
										product_title = rs("product_name")
									else
										if rs("manufacturer_part_number") <> "" and not isnull(rs("manufacturer_part_number")) then 
										product_title = product_title & " (" & rs("manufacturer_part_number") & ")" 
										end if
									end if
									if product_title = "" then 
										product_title=rs("product_short_name")
									end if
									product_title = product_title' & FindPartStoreStatus_system_setting(rs("ltd_stock"))
									response.Write product_title %>	
								</a>
							</td>
							<td style="text-align:right;" valign="top">
								<span style="display:none" id="product_child_single_price_<%= sys_tmp_detail %>_<%=product_serial_no%>"><%= current_price_rate%></span>
							    <span style="display:none" id="product_child_single_save_<%= sys_tmp_detail %>_<%=product_serial_no%>"><%=save_cost%></span>
                                <span style="display:none" id="product_child_single_cost_<%= sys_tmp_detail %>_<%=product_serial_no%>">
									<%= ConvertDecimal(current_price_rate) %>
                                </span>
                                <%									
								current_price_rate = current_price_rate '* part_quantity
								save_cost = save_cost '* part_quantity		
										
								if cint(save_cost) = 0 then 													    
								%><span class="price1">$</span><span id="product_child_price_price_<%= sys_tmp_detail %>_<%=product_serial_no%>" class="price1"><%=  formatnumber(current_price_rate, 2)%></span><span class="price_unit"><%= CCUN %></span>
								<% else%>                                
                                <span class="price_dis">$</span><span id="product_child_price_discount_<%= sys_tmp_detail %>_<%=product_serial_no%>" class="price_dis" style="color:blue"><%= formatnumber(current_price_rate, 2) %></span><span class="price1">$</span><span id="product_child_price_real_price_<%= sys_tmp_detail %>_<%=product_serial_no%>" <%=on_sale_font_style %> class="price1"><%= formatnumber(cdbl(current_price_rate) - cdbl(save_cost), 2)%></span><span class="price_unit"><%= CCUN %></span>
								<% end if %>					
							</td>
						</tr>
					</table>
				</td>
										
			</tr>
<%		
		rs.movenext
		loop
	else
		Response.write NO_DATA_MATCH
	end if
	rs.close : set rs = nothing
end if
%>
</table>

<%
closeconn() %>
