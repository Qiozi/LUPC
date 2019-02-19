<%


	function GetShippingCompany(tmp_code)
		if tmp_code <> "" then 
			set grs = conn.execute("select max(shipping_company) from tb_cart_temp where cart_temp_code="&cart_temp_code)
			if not grs.eof then 
				GetShippingCompany = grs(0)
			else
				GetShippingCompany = -1
			end if
			grs.close : set grs = nothing
		else
			GetShippingCompany = -1
		end if
		if isnull(GetShippingCompany) then GetShippingCompany = -1
	end function
	
	function GetStateShipping(tmp_code)
		if tmp_code <> "" then
			set grs = conn.execute("select max(state_shipping) from tb_cart_temp where cart_temp_code="&cart_temp_code)
			if not grs.eof then 
				GetStateShipping = grs(0)
			else
				GetStateShipping = -1
			end if
			grs.close : set grs = nothing
		else
			GetStateShipping = -1
		end if
		if isnull(GetStateShipping) then GetStateShipping = -1
	end function
	

	
	function setSessionInfo()
		dim rs, items, costs
		items = 0
		costs = 0
		if IsExistOrderCode() then 		
			' components 产品	
			set rs = conn.execute ("select sum(cart_temp_Quantity) as c, sum(product_current_price*cart_temp_Quantity) as p from tb_cart_temp c inner join tb_product p on p.product_serial_no=c.product_serial_no where cart_temp_code='"&GetCookiesOrderCode()&"' and p.tag=1 and c.menu_child_serial_no=2 order by p.product_order asc")
			if not rs.eof then 
				if not isnull(rs("c")) then 
					items = items + cdbl(rs("c"))
				end if
				if not isnull(rs("p")) then 
					costs = costs + cdbl(rs("p"))
				end if
			end if
			rs.close :set rs = nothing
			' system 产品
			set rs = conn.execute ("select sum(cart_temp_Quantity) as c, sum(sys_tmp_price*cart_temp_Quantity) as p from tb_cart_temp c inner join tb_sp_tmp sp on c.product_serial_no=sp.sys_tmp_code where cart_temp_code='"&GetCookiesOrderCode()&"' and sp.tag=1 and c.menu_child_serial_no=3")
			if not rs.eof then 
				if not isnull(rs("c")) then 
					items = cdbl(items) + cdbl(rs("c"))
				end if
				if not isnull(rs("p")) then 
					costs = cdbl(costs) + cdbl(rs("p"))
				end if
				'response.write items
			end if
			rs.close :set rs = nothing
			'response.write items & costs & "d"
			session("items") = items
			session("costs") = costs
		end if
	end function
%>