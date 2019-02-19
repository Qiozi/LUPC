<!--#include virtual="site/no_express.asp"-->
<!--#include virtual="site/inc/inc_helper.asp"-->

<%	
	response.Cookies("pick_up_in_person") = ""

	dim m
	Dim order_code				:	order_code				=	SQLescape(request("order_code"))
	Dim shipping_company		:	shipping_company		=	SQLescape(request("shipping_company"))
	Dim country_code			:	country_code			=	SQLescape(request("country_code"))
	Dim state_code				:	state_code				=	SQLescape(request("state_code"))
	Dim state_id				:	state_id				=	null
	Dim country_id				:	country_id				= 	null
	Dim Other_Country 			:	Other_Country			=	SQLescape(request("Other_Country"))
	Dim Other_State				:	Other_State				=	SQLEscape(request("Other_State"))
	IF country_code = "CA" then 
		country_id	=	CSCA
	else
		country_id 	= 	CSUS
	end if
	
	if (country_code = "Other") then
		state_id 	= SaveNewCountryState(Other_Country, Other_State)
		state_code 	= Other_State
		country_code= Other_Country
	else
		state_id 	= GetStateIdByCode(state_code)
	end if
			
			if(country_code <> "CA" and country_code <> "US" and country_code <>"1" and country_code <>"2") then 
					set rs = conn.execute("select * from tb_shipping_company "&_
										" where shipping_company_id='"&LAYOUT_OTHER_COUNTRY_SHIPPING_COMPANY_ID&"' "&_
										" and showit=1 order by qty asc ")
					if not rs.eof then 
							response.write "<table style='border: 0px solid red;' cellpadding='0' cellspacing='0'><tr><td width='200'><input type='radio' id=""shipping_company_"& m &""" name=""shipping_company"" value='"& LAYOUT_OTHER_COUNTRY_SHIPPING_COMPANY_ID &"' " 
							if cstr(LAYOUT_OTHER_COUNTRY_SHIPPING_COMPANY_ID) = cstr(shipping_company) then response.write " checked='true' "
							if(pick = "true") then response.write "  disabled=""true"" "
							response.write "  onclick=""cartChangeCharge('"& order_code &"', '"& country_code &"', '"& LAYOUT_OTHER_COUNTRY_SHIPPING_COMPANY_ID &"', '"& state_id &"' );"">"& rs("shipping_company_name") 
							response.write "</td><td><span id='price_view_shipping_company_"&rs("shipping_company_id")&"' class='price1'></span>"&vblf
							'response.write state_id
							if isnumeric(state_id)  then
								if cint(state_id) > 0 then 
									Response.write " <script>$('#price_view_shipping_company_"&rs("shipping_company_id")&"').load('/AccountCharge2_new.aspx?state_id="& state_id &"&parent_radio=shipping_company_"& m &"&tmp_code="&order_code&"&shipping_company="&rs("shipping_company_id")&"&country_code="&country_code&"&current_system="& Current_System &"&'+rand(1000));</script>"
								end if
							end if
							response.write "</td></tr></table>"
					end if
					rs.close : set rs = nothing
			else
			
				Conn.Execute("Update tb_cart_temp Set state_shipping="&SQLquote(state_id)&" , country_id="& SQLquote(country_id) &_
							", shipping_state_code="& SQLquote(state_code) &", shipping_country_code="& SQLquote(country_code) &"  Where cart_temp_code="& SQLquote(order_code))
			'Response.write ("Update tb_cart_temp Set state_shipping="&SQLquote(state_id)&" , country_id="& SQLquote(country_id) &"  Where cart_temp_code="& SQLquote(order_code))
				set rs = conn.execute("select * from tb_shipping_company where system_category="&SQLquote(country_id)&" and showit=1 order by qty asc ")
				if not rs.eof then
					m = 0
					do while not rs.eof 
						
					
						if rs("is_sales_promotion") = 0 then 
							m = m + 1
							response.write "<table style='border: 0px solid red;' cellpadding='0' cellspacing='0'><tr><td width='200'><input type='radio' id=""shipping_company_"& m &""" name=""shipping_company"" value='"& rs("shipping_company_id")&"' " 
							if cstr(rs("shipping_company_id")) = cstr(shipping_company) then response.write " checked='true' "
							'if(pick = "true") then response.write "  disabled=""true"" "
							response.write "  onclick=""cartChangeCharge('"& order_code &"', '"& country_code &"', '"& rs("shipping_company_id") &"', '"& state_id &"' );"">"& rs("shipping_company_name") 
							response.write "</td><td><span id='price_view_shipping_company_"&rs("shipping_company_id")&"' class='price1'></span>"&vblf
							'response.write state_id
							if isnumeric(state_id)  then
								if cint(state_id) > 0 then 
									Response.write " <script>$('#price_view_shipping_company_"&rs("shipping_company_id")&"').load('/AccountCharge2_new.aspx?state_id="& state_id &"&parent_radio=shipping_company_"& m &"&tmp_code="&order_code&"&shipping_company="&rs("shipping_company_id")&"&country_code="&country_code&"&current_system="& Current_System &"&'+rand(1000));</script>"
								end if
							end if
							response.write "</td></tr></table>"
						elseif is_sale_promotion_shipping_charge then
							 m = m + 1
							response.write "<table style='border: 0px solid red;'><tr><td width='200'><input type='radio' id=""shipping_company_"& m &""" name=""shipping_company"" value='"& rs("shipping_company_id")&"' " 
							if cstr(rs("shipping_company_id")) = cstr(shipping_company) then response.write " checked='true' "
							if(pick = "true") then response.write "  disabled=""true"" "
							response.write "  onclick=""cartChangeCharge('"& order_code &"', '"& country_code &"', '"& rs("shipping_company_id") &"', '"& state_id &"' );"">"& rs("shipping_company_name") 
							response.write "</td><td><span id='price_view_shipping_company_"&rs("shipping_company_id")&"' class='price1'></span>"&vblf
							'response.write state_id
							if isnumeric(state_id)  then
								if cint(state_id) > 0 then 
									Response.write " <script>$('#price_view_shipping_company_"&rs("shipping_company_id")&"').load('/AccountCharge2_new.aspx?state_id="& state_id &"&parent_radio=shipping_company_"& m &"&tmp_code="&order_code&"&shipping_company="&rs("shipping_company_id")&"&country_code="&country_code&"&current_system="& Current_System &"&'+rand(1000));</script>"
								end if
							end if
							response.write "</td></tr></table>"
						end if
					rs.movenext
					loop										
				end if
				rs.close : set rs  = nothing
			end if
									%>
