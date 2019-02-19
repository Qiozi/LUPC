
<!--#include virtual="/site/inc/inc_helper.asp"-->

<div id="set_parent_area">
<%
	dim get_parent_warrary_method_value 		:	get_parent_warrary_method_value = ""
	dim warray3yearExecuteMethodProductID		: 	warray3yearExecuteMethodProductID = ""
	dim Old_get_parent_warrary_method_value 	:	Old_get_parent_warrary_method_value = ""
	dim oldWarray3yearExecuteMethodProductID	: 	oldWarray3yearExecuteMethodProductID = ""
    
    dim part_max_quantity 	: part_max_quantity = SQLescape(request("part_max_quantity"))
    dim part_quantity 		: part_quantity = SQLescape(request("part_quantity"))
    dim lu_sku_selected 	: lu_sku_selected = SQLescape(request("lu_sku_selected"))
    dim on_sale_font_style 	: on_sale_font_style=""'" style=' color: blue; ' "	
	
	Dim split_post			:	split_post	=	0
	Dim split_name			:	split_name	=	""
	Dim is_exist_recommend 	: 	is_exist_recommend = true
	Dim skus				:	skus		=	"0"
	
	Dim store_sum			: 	store_sum 	=	"0"
'
'	
'	
'	function ValidateSystemHaveWindowProd()
'		ValidateSystemHaveWindowProd = false
'		dim wp_s, i,  systemid
'		systemid = split(GetConfigureProductIds(), ",")
'		wp_s = split(application("window_product_skus"), ",")
'		
'		for i = lbound(wp_s) to ubound(wp_s)
'			for j=lbound(systemid) to ubound(systemid)
'				if cstr(wp_s(i)) = cstr(systemid(j)) then
'					ValidateSystemHaveWindowProd = true			
'				end if
'			next
'		next
'	end function
	
''	
'	validate warrary3year
	dim warry_sql : warry_sql = ""
	
	'if ValidateSystemHaveWindowProd() and cstr(request("is_show_warrary3year")) = "1" then 
'	
'		warry_sql = " and p.product_serial_no="& LAYOUT_WARRARY_PRODUCT_ID&" "
'	end if
	
	'if not ValidateSystemHaveWindowProd()  then 
'	
'		warry_sql = " and p.product_serial_no<>"& LAYOUT_WARRARY_PRODUCT_ID&" "
'	end if
	
	'response.write("<script>alert('"& ValidateSystemHaveWindowProd() &"');<script>")
	'
	'
	'
	dim part_group_id, product_serial_no, menu_child_serial_no,other_product_sku, system_product_serial_no, is_non, product_name, product_short_name, current_price_rate, current_product_tmp_sql
	system_product_serial_no = SQLescape(request("system_product_serial_no"))
	plane_count = system_product_serial_no
	
    if session("current_custom_system_code")  = "" then 
		current_product_tmp_sql = "select  part_group_id, p.product_serial_no,p.menu_child_serial_no,p.other_product_sku, sp.id,p.is_non,p.product_name,p.product_short_name,p.product_current_price, p.tag,"&_
"case when product_store_sum >2 then 2 "&_
"when ltd_stock >2 then 2  "&_
"when product_store_sum + ltd_stock >2 then 2  "&_
"when product_store_sum  <=2 and product_store_sum >0 then 3 "&_
"when product_store_sum +ltd_stock <=2 and product_store_sum +ltd_stock >0 then 3 "&_
"when ltd_stock <=2 and ltd_stock >0 then 3 "&_
"when product_store_sum +ltd_stock =0 then 4 "&_
"when product_store_sum +ltd_stock <0 then 5 end as ltd_stock "&_
" , case when product_store_sum>ltd_stock then product_store_sum else ltd_stock end as store_sum from tb_ebay_system_parts sp inner join tb_product p on p.product_serial_no=sp.luc_sku  where sp.id='"&system_product_serial_no&"' "
	else
		current_product_tmp_sql = "select  part_group_id, p.product_serial_no,p.menu_child_serial_no,p.other_product_sku, sp.system_product_serial_no,p.is_non,p.product_name,p.product_short_name,p.product_current_price, p.tag,"&_
"case when product_store_sum >2 then 2 "&_
"when ltd_stock >2 then 2  "&_
"when product_store_sum + ltd_stock >2 then 2  "&_
"when product_store_sum  <=2 and product_store_sum >0 then 3 "&_
"when product_store_sum +ltd_stock <=2 and product_store_sum +ltd_stock >0 then 3 "&_
"when ltd_stock <=2 and ltd_stock >0 then 3 "&_
"when product_store_sum +ltd_stock =0 then 4 "&_
"when product_store_sum +ltd_stock <0 then 5 end as ltd_stock "&_
" , case when product_store_sum>ltd_stock then product_store_sum else ltd_stock end as store_sum from (select system_product_serial_no,product_order,system_templete_serial_no, product_serial_no,1 showit,part_group_id  from tb_sp_tmp_detail where sys_tmp_code='"&session("current_custom_system_code")&"') sp inner join tb_product p on p.product_serial_no=sp.product_serial_no  where system_product_serial_no='"&system_product_serial_no&"' and showit=1   "
	end if
	'response.Write(current_product_tmp_sql)
	set rs = conn.execute(current_product_tmp_sql)
	
	
	if not rs.eof then 
		
		if cstr(rs("tag")) = "1" then 
			is_non 					= rs("is_non")	
			other_product_sku 		= rs("other_product_sku")
			menu_child_serial_no 	= rs("menu_child_serial_no")
			product_serial_no 		= rs("product_serial_no")
			part_group_id  			= rs("part_group_id")
			current_price_rate 		= ConvertDecimal(rs("product_current_price"))			
			product_title 			= rs("product_name")
			skus					= skus & "," &rs("product_serial_no")
			store_sum 				= rs("store_sum")
			if product_title = "" then 
				product_title=rs("product_short_name")
			end if
			product_title = product_title & FindPartStoreStatus_system_setting(rs("ltd_stock"))
		else
			is_non 					= 0
			other_product_sku 		= rs("other_product_sku")
			menu_child_serial_no 	= rs("menu_child_serial_no")
			product_serial_no 		= rs("product_serial_no")
			part_group_id  			= rs("part_group_id")
			current_price_rate 		= 0			
			product_title 			= NONE_SELECTED_TITLE
			store_sum				= rs("store_sum")
			if product_title = "" then 
				product_title=rs("product_short_name")
			end if
		end if
		
		
	else
		closeconn()
		response.Write("Server Error!")
		response.End()
	end if
	rs.close : set rs = nothing

	''
	'	price
	'
	dim save_cost
	save_cost = ConvertDecimal(GetSavePrice(product_serial_no))
	'current_price_rate = changePrice(current_price_rate, card_rate)
'
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

	'Response.write product_serial_no
	dim cc_sum , p_page, p_page_size, plane_count_sub, tmp_sql
	plane_count_sub = 0
	p_page_size = 130
	cc_sum = 0
		
	dim tmp_sql_1, tmp_sql_2
	
		tmp_sql_1	=	"select p.product_serial_no,"&_
				" p.product_name"&_
				" ,p.product_short_name"&_
				" ,p.product_current_price"&_
				" ,p.menu_child_serial_no"&_
				" ,part.nominate"&_
				" ,'1' as is_nominate"&_
				" ,p.other_product_sku"&_
				" ,p.is_non"&_
				" ,p.product_order"&_
				" ,pc.menu_child_order "&_
				" ,split_line"&_
                " ,p.is_top"&_
				" ,case when p.product_store_sum>p.ltd_stock then p.product_store_sum else p.ltd_stock end as store_sum "&_
				" from tb_part_group_detail part inner join tb_product p on p.product_serial_no = part.product_serial_no"&_
				" inner join tb_product_category pc on p.menu_child_serial_no=pc.menu_child_serial_no "&_
				" where p.product_serial_no<>"& product_serial_no&  warry_sql & " and part.showit=1"&_
				" and (p.is_top=1 or p.is_non=1) and p.tag=1 and pc.tag=1 and part_group_id='"&part_group_id&"' order by p.is_non asc "

	set child = conn.execute(tmp_sql_1 )
   

							%>
    <!--begin part group -->
    
    <div id="product_part_group_id_<%=part_group_id%>_<%=plane_count%>" style="border:1px solid #ffffff;">				
    <table width="100%"  border="0" cellpadding="2" cellspacing="0" bgcolor="#FFFFFF" id="table_plane_group_<%=menu_child_serial_no%>_<%= plane_count%>"  style="display:">

<tr>							
			<td>
				<table cellpadding="0" cellspacing="0" border="0" width="100%">
            <tr>
                <td width="60" style="text-align:left">
                <span style="position:relative;">
                    <span style="position:absolute; z-index:<%= clng(system_product_serial_no) + 2000%>; top: 0px; left: 0px">
                        <% if cstr(is_non) <> "1" then %>
                        <img src="<%= HTTP_PART_GALLERY & PartChoosePhotoSKU(product_serial_no,other_product_sku)%>_t.jpg" width="50" height="50" id="img_product_<%= system_product_serial_no%>_<%=product_serial_no%>" onClick="javascript:popImage('<%= HTTP_PART_GALLERY & PartChoosePhotoSKU(product_serial_no,other_product_sku) %>_list_1.jpg','Lu Computers','middle_center',true,true);return false;" style=" cursor: pointer"> 
                        <% else %>
                        <img src="<%= HTTP_PART_GALLERY %>noExist.gif" width="50" height="50" id="img_product_<%= system_product_serial_no%>_<%=product_serial_no%>" onerror="imgerror(this);"/>
                        <% end if %>
                    </span>
                </span>
                </td>
                
                <td width="<% if part_max_quantity >1 then response.write "60" else response.write "10"  %>" nowrap="nowrap" name="product_group_<%=menu_child_serial_no%>" id="product_group_<%=product_serial_no%>"  valign="top" style="!important;padding-top:1px;left-align:left"><table cellpadding="0" cellspacing="0">
                        <tr>
                            <td><input 
                        name="product_check_<%=system_product_serial_no%>" 
                        id="product_check_<%=product_serial_no&"_"&plane_count&"_"&plane_count_sub%>" 
                        checked="true"
                        type="radio" 
                        value="<%=product_serial_no%>" 
                        onclick="getProductName('img_product_<%= system_product_serial_no %>_<%=product_serial_no%>', 'product_child_img_product_<%= system_product_serial_no %>_<%=product_serial_no%>','product_head_<%= system_product_serial_no%>', '<%=system_product_serial_no%>', '<%=product_serial_no%>','<%= sys_tmp_sku %>');"
                        tag='<%= current_price_rate %>'
                        ></td>
                        
                        <%
                            response.Write ExportSelectControl( product_serial_no , part_max_quantity, part_quantity, lu_sku_selected, part_group_id, "product_check_"&product_serial_no&"_"&plane_count&"_"&plane_count_sub, system_product_serial_no )
                                            if cstr(part_group_id) =	LAYOUT_WARRARY_GROUP_ID then 
                                                Old_get_parent_warrary_method_value = """getProductName('img_product_"&  system_product_serial_no &"_"& product_serial_no&"', 'product_child_img_product_"&  system_product_serial_no &"_"& product_serial_no&"','product_head_"&  system_product_serial_no&"', '"& system_product_serial_no&"', '"& product_serial_no&"','"&  sys_tmp_sku &"');"";"
												oldWarray3yearExecuteMethodProductID = "product_check_"& product_serial_no &"_"&plane_count&"_"&plane_count_sub 
                                            end if 
                                            
                                        %>
									    </tr></table>
                                        </td>
									<td valign="top">
										<table width="100%">
											<tr>
												<td style="text-align:left">
                                                	<%=WritePartStoreSumAtSysList(store_sum) %>
													<a class="hui-red" 
													<% if cstr(is_non) <> "1" then %>
                                                    href="view_part.asp?id=<%= product_serial_no%>" onClick="js_callpage_name(this.href, 'custom_part_detail');return false;"  
                                                    <% end if %>
                                                    id="product_child_img_product_<%= system_product_serial_no %>_<%=product_serial_no%>"                                                   
													style='color: #EF5412;'>                                                    
                                                    <%
												
													if is_non = 0 and instr(product_title, "onboard")<1 and instr(product_title, "warranty") <1  and product_title <> NONE_SELECTED_TITLE then %>
                                                    (Top Seller)&nbsp;
                                                    <%end if
													product_title = product_title '& FindPartStoreStatus_system_setting(child("ltd_stock"))
													response.write product_title%>
													</a>
												</td>
												<td style="text-align:right;" valign="top" nowrap="nowrap"><span style="display: none" id="product_child_single_price_<%= system_product_serial_no %>_<%=product_serial_no%>"><%= current_price_rate%></span>
												    <span style="display: none" id="product_child_single_save_<%= system_product_serial_no %>_<%=product_serial_no%>"><%=save_cost %></span>
                                                    <span style="display: none" id="product_child_single_cost_<%= system_product_serial_no %>_<%=product_serial_no%>">
														<%= current_price_rate %>
                                                    </span>
                                                    <%									
													current_price_rate = CDBL(current_price_rate) * part_quantity
													save_cost = save_cost * part_quantity		
															
													if cdbl(save_cost) = 0 then 													    
													%><span class="price1">$</span><span id="product_child_price_price_<%= system_product_serial_no %>_<%=product_serial_no%>"  class="price1"><%=  formatnumber(current_price_rate, 2) %></span><span class="price_unit"><%= CCUN %></span><% else%><span class="price_dis">$</span><span class='price_dis' id="product_child_price_discount_<%= system_product_serial_no %>_<%=product_serial_no%>"><%= formatnumber(current_price_rate, 2) %></span><span class="price1">$</span><span id="product_child_price_real_price_<%= system_product_serial_no %>_<%=product_serial_no%>" <%=on_sale_font_style %> class="price1"><%= formatnumber(current_price_rate - cdbl(save_cost), 2)%></span><span class="price_unit"><%= CCUN %></span><% end if %>
												</td>
											</tr>
										</table>
									</td>
									</tr>
						<%	
							      
						if not child.eof then		
							dim is_sub_group,pre_is_sub_group, is_sub_group_view, view_sub_group_id, is_view_sub_group, image_postion_rate, plane_count_2
							is_sub_group 			= 0
							is_sub_group_view 		= 0
							is_view_sub_group 		= 0
							image_postion_rate 		= 0
							do while not child.eof  
							
							skus 				=	skus 	& "," & child("product_serial_no")
							save_cost 			= ConvertDecimal(GetSavePrice(child("product_serial_no")))
							current_price_rate 	= ConvertDecimal(child("product_current_price"))
													
													
							plane_count_2 		= plane_count
							' 判断产品在列表中位置是第几个， 然后判断并处理图片的位置。
							image_postion_rate 	= image_postion_rate + 1
							
							' 取得此类的所有产品ID号，方便更改价格使用
							child_list_serial	= child_list_serial & "," & child("product_serial_no")

							plane_count_sub 	= plane_count_sub + 1
							' 是否是分割线
							
							%>
							<tr  onMouseOver="this.bgColor='#f2f2f2'" onMouseOut="this.bgColor='white'" id="sub_group_<%= is_sub_group%>_<%= plane_count %>_<%=image_postion_rate%>" >
							<td width="60"  style="text-align:left">
							<%
							' 如果是当前的被选中，则输入图片们位置
							'if (cstr(product_serial_no) = cstr(child("product_serial_no"))) then 
							%>
                            
                             <% if cstr(child("is_non")) <> "1" then %>
                              <span style="position:relative; z-index:10">
                            	 <span style="position:absolute; z-index:<%= 2000 + clng(system_product_serial_no)%>; top: 0px; left: 0px">
                            	<img src="<%= HTTP_PART_GALLERY & PartChoosePhotoSKU(child("product_serial_no"),child("other_product_sku"))%>_t.jpg" width="50" height="50" id="img_product_<%=system_product_serial_no%>_<%= child("product_serial_no") %>" onClick="javascript:popImage('<%= HTTP_PART_GALLERY %><%= PartChoosePhotoSKU(child("product_serial_no"),child("other_product_sku")) %>_list_1.jpg','Lu_Computers','middle_center',true,true);return false;" style=" display:none;cursor: pointer">
                             <% else %>   
                              <span style="position:relative; z-index:-1">
                            	 <span style="position:absolute; z-index:-<%= system_product_serial_no%>; top: 0px; left: 0px">
                              <img src="<%= HTTP_PART_GALLERY %>noExist.gif" width="50" height="50" id="img_product_<%=system_product_serial_no%>_<%= child("product_serial_no") %>" style="display:none;" onerror="imgerror(this);"/>
                             <% end if %>
                             </span></span>
							<%	
							'else
							'	response.write "&nbsp;"
								
							'end if
							%>&nbsp;
							</td>
							<td width="8" nowrap="nowrap" name="product_group_<%=menu_child_serial_no%>" id="product_group_<%=child("product_serial_no")%>"  valign="top" style="!important;padding-top:1px; text-align:left"><table cellpadding="0" cellspacing="0">
							<tr>
							    <td><input 
										name="product_check_<%=system_product_serial_no%>" 
										id="product_check_<%=child("product_serial_no")&"_"&plane_count&"_"&plane_count_sub%>" 
										<%
											if (product_serial_no = child("product_serial_no")) then 
												response.Write( " checked=""true"" " ) 
												is_view_sub_group = 1
											end if
										%>
										type="radio" 
										value="<%=child("product_serial_no")%>" 
										onclick="getProductName('img_product_<%= system_product_serial_no %>_<%=child("product_serial_no")%>', 'product_child_img_product_<%= system_product_serial_no %>_<%=child("product_serial_no")%>','product_head_<%= system_product_serial_no%>', '<%=system_product_serial_no%>', '<%=child("product_serial_no")%>','<%= sys_tmp_sku %>');"
                                        tag='<%= current_price_rate %>'
                                        ></td><%
                                            response.Write ExportSelectControl( child("product_serial_no") , part_max_quantity, part_quantity, lu_sku_selected, part_group_id, "product_check_"& child("product_serial_no")&"_"&plane_count&"_"&plane_count_sub, system_product_serial_no)
                                            if cstr(child("product_serial_no")) =	LAYOUT_WARRARY_PRODUCT_ID and  cstr(part_group_id) =	LAYOUT_WARRARY_GROUP_ID then 
                                                get_parent_warrary_method_value = """getProductName('img_product_"&  system_product_serial_no &"_"& child("product_serial_no")&"', 'product_child_img_product_"&  system_product_serial_no &"_"& child("product_serial_no")&"','product_head_"&  system_product_serial_no&"', '"& system_product_serial_no&"', '"& child("product_serial_no")&"','"&  sys_tmp_sku &"', true);"";"
												warray3yearExecuteMethodProductID = "product_check_"& child("product_serial_no")&"_"&plane_count&"_"&plane_count_sub 
                                            end if 
                                        %>
							            </tr>
							            </table>
							        </td>									
									<td valign="top" style="text-align:left">
										<table width="100%" cellpadding="0" cellspacing="0" border="0">
											<tr>
												<td>
                                                <%=WritePartStoreSumAtSysList(child("store_sum")) %>
                                                <a class="hui-red" 
                                                    <% if cstr(child("is_non")) <> "1" then %>
                                                    href="view_part.asp?id=<%= child("product_serial_no")%>" onClick="js_callpage_name(this.href, 'custom_part_detail');return false;"  
                                                    <% end if %>
                                                    id="product_child_img_product_<%= system_product_serial_no %>_<%=child("product_serial_no")%>" >
													
													<%
													product_title = child("product_name")
													if product_title = "" then 
														product_title=child("product_short_name")
													end if
													
													if child("is_top") = 1 then
															 if cstr(child("is_non")) = "0" and instr(lcase(product_title), "onboard")<1 and instr(lcase(product_title), "warranty") <1 then
															 response.write "<span style=''>&nbsp;(Top Seller)&nbsp;</span>"
												    		 end if
													end if
													product_title = product_title '& FindPartStoreStatus_system_setting(child("ltd_stock"))
													response.Write(product_title)
													%></a>
												</td>
												<td style="text-align:right;" valign="top">
                                                    
                                                    <span style="display:none" 
                                                    	id="product_child_single_price_<%= system_product_serial_no %>_<%=child("product_serial_no")%>">
														<%= current_price_rate%>
                                                    </span>
												    <span style="display:none" id="product_child_single_save_<%= system_product_serial_no %>_<%=child("product_serial_no")%>">
														<%= save_cost %>
                                                    </span>
                                                    <span style="display:none" id="product_child_single_cost_<%= system_product_serial_no %>_<%=child("product_serial_no")%>">
														<%= current_price_rate %>
                                                    </span>
                                                    <span class="price1">$</span><%if cdbl(save_cost) = 0 then%><span id="product_child_price_price_<%= system_product_serial_no %>_<%=child("product_serial_no")%>" class="price1"><%=  formatnumber(current_price_rate , 2)%></span><span class="price_unit"><%= CCUN %></span>
													<% else%><span style="text-decoration:line-through;color: blue;" class="price_dis" id="product_child_price_discount_<%= system_product_serial_no %>_<%=child("product_serial_no")%>">
															<%= formatnumber(current_price_rate, 2) %>
                                                        </span><span class="price_unit"><%= CCUN %></span>
                                                    	<span id="product_child_price_real_price_<%= system_product_serial_no %>_<%=child("product_serial_no")%>" <%=on_sale_font_style %> class="price1"><%=  formatnumber(cdbl(current_price_rate) - cdbl(save_cost), 2)%></span><span class="price_unit"><%= CCUN %></span>
                                                        
													<% end if %>
												</td>
											</tr>
										</table>
									</td>
															
								</tr>                                
                                
<% 

	if not end_page then
		'response.write "</div>"
	end if
	'is_sub_group_view = 0
	child.movenext:loop
							

else
	
	is_exist_recommend=false
end if
child.close:set child = nothing


%>					
	<!-- split name begin -->
                                
                                <% 
								
								Set srs = conn.execute("select distinct p.split_name from tb_part_group_detail part inner join tb_product p on p.product_serial_no = part.product_serial_no inner join tb_product_category pc on pc.menu_child_serial_no=p.menu_child_serial_no where  p.product_serial_no<>'"& product_serial_no&"' and p.product_serial_no <> '"&LAYOUT_WARRARY_PRODUCT_ID&"' and p.product_serial_no not in ("& skus &") and   part.showit=1 and p.is_non=0 and  p.tag=1 and p.split_name<>'' and part_group_id='"&part_group_id&"' order by  p.split_name asc")
								
								if not srs.eof then 
                               			Do while not srs.eof
												split_name = srs("split_name")
												split_post = split_post + 1
									%>
										<tr>
										<td width="60" style="max-width:80px">&nbsp;								
										</td>
										<td colspan="2" style="text-align:left" width="520px">	
										<div style="text-align:left;color: green;background: #f2f2f2; cursor:pointer" onClick="ViewSubGroup('sub_group_<%= plane_count %>_<%= part_group_id %>_<%=split_post%>', 'img_exp_<%= plane_count %>_<%=is_sub_group%>',this, '<%= skus %>', '<%= split_post %>', '<%= part_group_id %>', 'split_line_<%= plane_count %>_<%= part_group_id %>_<%=split_post%>', '<%= system_product_serial_no %>', '<%=part_max_quantity %>', '<%= part_quantity%>', '<%= lu_sku_selected%>', '<%= JSescape(split_name) %>');">
											<table style="width: 100%" align="left" border="0">
												<tr>
													<td style="width:10px">
														<img id="img_exp_<%= plane_count %>_<%=is_sub_group%>" src='/soft_img/app/col.gif'>
													</td>
													<td style="text-align:left;color: green;background: #f2f2f2;">
														
														<%= srs("split_name")%>
                                                        
													</td>
												</tr>
											</table>
										</div>					
										</td>
										</tr>
										<tr id="sub_group_<%= plane_count %>_<%= part_group_id %>_<%=split_post%>" style="display:none">
											<td colspan="3" id="split_line_<%= plane_count %>_<%= part_group_id %>_<%=split_post%>" style="text-align:center">Load......</td>
										</tr>	
                                     <%  srs.movenext
									 	 loop
								else
									if not is_exist_recommend then 
										Response.write NO_DATA_MATCH
									end if
								 end if
								 srs.close : set srs = nothing %>
									 							
							<!-- split name end -->	
                            
							<tr><td>&nbsp;</td>
								<td colspan="2" style="text-align:right; height:40px; padding-right:8px;">

									<!-- Close -->
                                   
										<span align="right" onClick='document.getElementById("<%= request("parent_current_configure")%>").style.display = "none";' style="cursor: pointer;"><img src="/soft_img/app/close_custome.gif"></span>
									    
									<!-- close end -->
								</td>
							</tr>	
		</table>
	<%
        ' 输出此类所有产品ID
        Response.write ("<input type=""hidden"" name=""child_list_ID_"&menu_child_serial_no&""" id=""child_list_ID_"&menu_child_serial_no&""" value="""&child_list_serial&""">")
        response.write ("</div>")

    'response.write p_page
    %>
            </td>
        </tr>
    

    <!--END part group -->
</table>
</div>
<% closeconn() %>
<script language="javascript" type="text/javascript">

	
	/*parent.document.getElementById("<%= request("parent_current_configure")%>").style.display = ""; 
	parent.document.getElementById("<%= request("parent_current_configure")%>").innerHTML = document.getElementById("set_parent_area").innerHTML;
	
		
	<%
	
	if  cstr(part_group_id) =	LAYOUT_WARRARY_GROUP_ID then 
		'response.Write(part_group_id & LAYOUT_WARRARY_GROUP_ID)
	%>
		
	<% if get_parent_warrary_method_value <> "" then %>
		parent.document.getElementById("warray3yearExecuteMethod").value= <%=get_parent_warrary_method_value %>	
		parent.document.getElementById("warray3yearExecuteMethodProductID").value = "<%= warray3yearExecuteMethodProductID %>";
	<% end if
	
		if Old_get_parent_warrary_method_value <> "" then 
	%>
		parent.document.getElementById("oldWarray3yearExecuteMethod").value= <%=Old_get_parent_warrary_method_value %>	
		parent.document.getElementById("oldWarray3yearExecuteMethodProductID").value = "<%= oldWarray3yearExecuteMethodProductID %>";
	<% end if 
	end if
	%>*/
	$().ready(function(){
		closeLoading();
	});
	
</script>

