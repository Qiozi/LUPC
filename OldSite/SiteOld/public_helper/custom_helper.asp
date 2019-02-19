<%
'
'
'   addToCustome(system_templete_serial_no, sys_tmp_sku)
'   CopyConfigureSystemToCart(system_templete_serial_no, sys_tmp_sku,tmp_order_code, is_copy_to_cart )
'   CopySystemToOtherQuote(new_system_code,old_system_id , change)
'
'
'
'
'
'
'
'
'
'
' ---------------------------------------------------------------------------------
function addToCustome(system_templete_serial_no, sys_tmp_sku)
' ---------------------------------------------------------------------------------
	dim crs, child, price, cost, save_cost, one_price, one_cost, one_save_cost, current_price, old_price_sum, single_sold
	dim ont_price_rate
	ont_price_rate = 0
	price = 0
	cost = 0
	
	save_cost =0
	
	
	
	if system_templete_serial_no <> "" and isnumeric(system_templete_serial_no) then 
	'如果是８位的ＩＤ，则是从已配好的系统进入
		if len(system_templete_serial_no) <> 8 or len(request("id"))= 8 then
			conn.execute("delete from tb_sp_tmp where sys_tmp_code='"&sys_tmp_sku&"'")
			conn.execute("delete from tb_sp_tmp_detail where sys_tmp_code='"&sys_tmp_sku&"'")
		end if
		
		if len(system_templete_serial_no) <> 8 then 		
			set rs = conn.execute("select st.*, st.system_templete_name as sys_tmp_product_name ,pc.is_noebook from tb_system_templete st inner join tb_product_category pc on st.system_templete_category_serial_no=pc.menu_child_serial_no where st.system_templete_serial_no="&system_templete_serial_no)
		else
			set rs = conn.execute("select *,0 as is_noebook from tb_sp_tmp  where sys_tmp_code="&system_templete_serial_no)
		end if
		
		if not rs.eof then 

					'ddddd= timer()
					'
					'
					' copy system_code to Store
					'
					if len(system_templete_serial_no) <> 8 then 
						conn.execute("insert into tb_system_code_store ( system_templete_serial_no, system_code, create_datetime, ip) values ( '"&system_templete_serial_no&"', '"&sys_tmp_sku&"', now(), '"&client.getip&"')")
					else
						conn.execute("insert into tb_system_code_store ( system_templete_serial_no, system_code, create_datetime, ip, old_system_code) values ( '"&system_templete_serial_no&"', '"&sys_tmp_sku&"', now(), '"&client.getip&"', '"&system_templete_serial_no&"')")
					end if					
					'
					'
					'
					'	copy system detial 
					'
					if len(system_templete_serial_no) <> 8 then 
						conn.execute("insert into tb_sp_tmp_detail(sys_tmp_code,product_serial_no,product_current_price,product_current_cost, "&_
" product_order,system_templete_serial_no, system_product_serial_no, part_group_id, save_price, re_sys_tmp_detail, "&_
" old_price, product_current_price_rate,product_current_sold, part_quantity, part_max_quantity) "&_
" select '"&sys_tmp_sku&"', p.product_serial_no,p.product_current_price,p.product_current_cost , "&_
" sp.product_order ,system_templete_serial_no, sp.system_product_serial_no, sp.part_group_id , p.product_current_discount,'-1', "&_
" p.product_current_price,p.product_current_price, p.product_current_price-p.product_current_discount, part_quantity, part_max_quantity "&_
" from tb_system_product sp "&_
" inner join tb_product p on sp.product_serial_no=p.product_serial_no where sp.showit=1 and p.tag=1 and system_templete_serial_no='"&system_templete_serial_no&"'")
					else
					
						conn.execute("insert into tb_sp_tmp_detail(sys_tmp_code,product_serial_no,product_current_price,product_current_cost,"&_
" product_order,system_templete_serial_no, system_product_serial_no, part_group_id, save_price, re_sys_tmp_detail, "&_
" old_price, product_current_price_rate,product_current_sold, part_quantity, part_max_quantity) "&_
" select '"&system_templete_serial_no&"',  p.product_serial_no,p.product_current_price,p.product_current_cost ,sp.product_order ,sp.sys_tmp_code , sp.system_product_serial_no, "&_
" sp.part_group_id,p.product_current_discount,sp.sys_tmp_detail,p.product_current_price,p.product_current_price,p.product_current_price-p.product_current_discount, part_quantity, part_max_quantity"&_
" from tb_sp_tmp_detail sp inner join tb_product p on sp.product_serial_no=p.product_serial_no "&_
" where p.tag=1 and sys_tmp_code='"&system_templete_serial_no&"'")
					end if
					
					dim price_save_cost
					price_save_cost = FindSystemPriceAndSaveAndCost8(sys_tmp_sku)
					save_cost = splitConfigurePrice(price_save_cost, 1 )
					price = splitConfigurePrice(price_save_cost, 0 )
					cost = splitConfigurePrice(price_save_cost, 2 )
						
					conn.execute("insert into tb_sp_tmp(sys_tmp_code,sys_tmp_price,create_datetime,tag,ip,system_templete_serial_no,email,"&_
					"is_noebook,sys_tmp_cost,sys_tmp_product_name,save_price,old_price,syst_tmp_price_rate) values "&_
					"( '"&sys_tmp_sku&"','"&cdbl(formatnumber(price-save_cost, 2))&"','"&now()&"','1','"&client.getIP&"','"&system_templete_serial_no&"','"&session("email")&"','"&rs("is_noebook")&"','"&cdbl(formatnumber(cost,2))&"','"&GetSystemName8(sys_tmp_sku)&"','"&cdbl(formatnumber(save_cost,2))&"','"&cdbl(formatnumber(price, 2))&"','"&cdbl(formatnumber(price, 2))&"')")


		end if
		rs.close:set rs = nothing
	end if
	end function
'	 ' ' ' ' ' ' ' ' ' ' ' ' '  ' ' ' ' ' ' ' '  ' ' ' ' ' ' ' ' '' 
'	
'	copy configure to cart
'	
'	
' ---------------------------------------------------------------------------------
	function CopyConfigureSystemToCart(system_templete_serial_no, sys_tmp_sku,tmp_order_code, is_copy_to_cart )
' ---------------------------------------------------------------------------------
		dim rs, price_sum, cost_sum, save_sum, cpu_category_id, system_name
		system_name = "System "
		cpu_category_id = 22
		price_sum = 0
		cost_sum = 0
		save_sum = 0
		dim templete_system_infos, system_product_serial_no, product_serial_no, product_order, part_group_id
		templete_system_infos =	session("templete_system_info")	
		
		if system_templete_serial_no = "" or isnull(templete_system_infos ) then 		
			response.Write("Sorry, your session has expired, please resubmit your information.")
			response.End()
		end if
'		
'		record system code history
'		
'
'		get CPU category id
'
		set rs = conn.execute("select computer_cpu_category from tb_computer_cpu")
		if not rs.eof then 
			cpu_category_id = rs(0)
		end if
		rs.close : set rs = nothing
				
		if sys_tmp_sku = "" then response.Write("Server is error. "):response.End()
		
		if session("current_custom_system_code")  = "" or isnull(session("current_custom_system_code")) then 
			conn.execute("insert into tb_system_code_store ( system_templete_serial_no, system_code, create_datetime, ip) values ( '"&system_templete_serial_no&"', '"&sys_tmp_sku&"', now(), '"&client.getip&"')")
		else
			conn.execute("insert into tb_system_code_store ( system_templete_serial_no, system_code, create_datetime, ip, old_system_code) values ( '"&system_templete_serial_no&"', '"&sys_tmp_sku&"', now(), '"&client.getip&"', '"&session("current_custom_system_code")&"')")
		end if

'		
'		copy detail
'			
		'dim begin, endtimer
		'begin = timer
		dim part_quantity, part_max_quantity
		part_max_quantity = 0
		part_quantity = 0
		for i=lbound(templete_system_infos,1) to ubound(templete_system_infos,1)
		
			system_product_serial_no =  templete_system_infos(i,0)
			product_serial_no = templete_system_infos(i,1)
			part_group_id = templete_system_infos(i,2)
			product_order = templete_system_infos(i,3)
			part_quantity = templete_system_infos(i,4)
			part_max_quantity = templete_system_infos(i,5)
			'response.Write(product_serial_no & "<br>")
			if system_product_serial_no <> "" and product_serial_no <> "" then 
				
				if (product_serial_no <> "") and isnumeric(product_serial_no) then 
					set rs = conn.execute("select  p.product_current_price, p.product_current_cost, p.product_current_discount, (product_current_price - product_current_discount) product_current_sold, pc.menu_pre_serial_no,p.menu_child_serial_no, p.product_short_name from tb_product p inner join tb_product_category pc on pc.menu_child_serial_no=p.menu_child_serial_no where product_serial_no='"&product_serial_no&"' and p.tag=1 and (pc.tag=1 or p.manufacturer_part_number like '%Overclocked%')")
					if not rs.eof then 
					conn.execute("insert into tb_sp_tmp_detail(sys_tmp_code,product_serial_no,product_current_price,product_current_cost,product_order,system_templete_serial_no, system_product_serial_no, part_group_id, save_price, old_price, product_current_price_rate,product_current_sold, part_quantity, part_max_quantity) values "&_
					" ( '"&sys_tmp_sku&"', '"&product_serial_no&"', '"& rs("product_current_price") &"','"& rs("product_current_cost") &"', '"& product_order &"','"& system_templete_serial_no &"','"& system_product_serial_no &"', '"& part_group_id &"', '"& rs("product_current_discount") &"','"& rs("product_current_price") &"','"& rs("product_current_price") &"', '"& rs("product_current_sold") &"', '"&part_quantity&"', '"&part_max_quantity&"' )")	
					
					price_sum = price_sum + cdbl(rs("product_current_price")) * cdbl(part_quantity)
					cost_sum = cost_sum + cdbl(rs("product_current_cost")) * cdbl(part_quantity)
					save_sum = save_sum + cdbl(rs("product_current_discount")) * cdbl(part_quantity)
					
						if cstr(cpu_category_id) = cstr(rs("menu_child_serial_no")) or cstr(cpu_category_id) = cstr(rs("menu_pre_serial_no")) then 
							system_name = rs("product_short_name") & " System"
						end if
					end if
	
					rs.close : set rs = nothing
				end if
			end if
			
		next
		'endtimer = timer
		'response.Write(endtimer-begin )
		'response.Write("<br>")
		'begin = timer

		'response.Write(system_name)
		dim  current_price, current_price_rate, now_low_price, price_and_save	, save_price, cost	
		
		current_price_rate = price_sum
		save_price = save_sum
		current_price = cdbl(current_price_rate) - cdbl(save_price)
		cost = cost_sum
		
		conn.execute("insert into tb_sp_tmp(sys_tmp_code, sys_tmp_price, create_datetime, tag, ip,  system_templete_serial_no, email, is_noebook, sys_tmp_cost, sys_tmp_product_name, save_price, old_price, syst_tmp_price_rate) values "&_
		"( '"& sys_tmp_sku &"', '"& current_price &"', now(), 1, '"& client.getip&"', '"& system_templete_serial_no &"', '"&session("email")&"', 0, '"& cost & "', '"& system_name &"', '"& save_price & "', '"& current_price_rate &"', '"& current_price_rate &"' )")
		'endtimer = timer
		'response.Write(endtimer-begin )
		'response.Write("<br>")
' 
' 		copy to cart
' 
' 
		dim customer_serial_no	
		if "" <> request.Cookies("customer_serial_no") then 
			customer_serial_no	=request.Cookies("customer_serial_no")
		else
			customer_serial_no = 0
		end if
		
		if is_copy_to_cart then 
			conn.execute("insert into tb_cart_temp(cart_temp_code, product_serial_no, menu_child_serial_no, create_datetime, ip, customer, cart_temp_Quantity, customer_serial_no, shipping_company, state_shipping, is_noebook, price, price_rate, product_name, cost) "&_
			" select '"& tmp_order_code &"', '"& sys_tmp_sku &"', '"& system_templete_serial_no &"', now(), '"&client.getip&"', '"& customer_serial_no &"', 1, '"&customer_serial_no&"', '"& GetShippingCompany(tmp_order_code) &"','"& GetStateShipping(tmp_order_code)&"', 0, sys_tmp_price, sys_tmp_price, sys_tmp_product_name, sys_tmp_cost from tb_sp_tmp where sys_tmp_code='"&sys_tmp_sku&"'")
		end if
	
	end function
'
'	because get a new quote, copy a system custom.
'	
' ---------------------------------------------------------------------------------
	function CopySystemToOtherQuote(new_system_code,old_system_id , change)
' ---------------------------------------------------------------------------------
		if change = "true" then 	
		' 插入新的system 配置
		
		conn.execute("insert into tb_sp_tmp 	( sys_tmp_code, sys_tmp_price, 	create_datetime, tag, ip, system_templete_serial_no,	email, system_category_serial_no, is_noebook, 	sys_tmp_cost, sys_tmp_product_name, save_price, 	old_price, is_old, old_part_id)"&_
		" select  '"&new_system_code&"', sys_tmp_price, now(), tag, ip, system_templete_serial_no, 	email, system_category_serial_no, is_noebook, 	sys_tmp_cost, sys_tmp_product_name, save_price, 	old_price, is_old, old_part_id from tb_sp_tmp where sys_tmp_code='"&old_system_id&"'")
			
		else
			new_system_code = session("new_system_code")
			' 删除已存在的所有配件
			conn.execute("delete from tb_sp_tmp_detail where sys_tmp_code='"&new_system_code&"'")
		end if
		
		conn.execute ("insert into tb_sp_tmp_detail 	( sys_tmp_code, product_serial_no, product_current_price, 	product_current_cost, product_order, system_templete_serial_no, 	system_product_serial_no, part_group_id, save_price, old_price, 	re_sys_tmp_detail, product_current_price_rate,product_current_sold)"&_
		" select 	'"&new_system_code&"', product_serial_no, product_current_price, product_current_cost, product_order, system_templete_serial_no,system_product_serial_no, part_group_id, save_price, old_price, sys_tmp_detail,product_current_price_rate,product_current_sold from tb_sp_tmp_detail where sys_tmp_code='"&old_system_id&"'")
		CopySystemToOtherQuote = new_system_code
	end function
	
	
	
	
' ---------------------------------------------------------------------------------
function ExportSelectControl(product_serial_no, max_quantity, part_quantity, lu_sku_selected, part_group_id, radio_id, sys_tmp_sku)
' ---------------------------------------------------------------------------------
    dim str, i
    str = ""

    if max_quantity  > 1 then 
        str = "<td><select id=""part_max_quantity_"& sys_tmp_sku &"_"&product_serial_no&""" name=""part_max_quantity"" onchange=""change_part_quantity('"& part_group_id &"', '"&product_serial_no&"', '"&radio_id&"',this);"">"
        for i=1 to max_quantity 
            str = str & "<option value="""& i &""" "
            if cint(part_quantity) = i and cstr(lu_sku_selected) = cstr(product_serial_no) then str = str & " selected=""true"""
            str = str & ">"& i &"</option>"
        next
        str = str & "</select></td>"
    end if

    ExportSelectControl = str
end function
	
	
	

	
%>
