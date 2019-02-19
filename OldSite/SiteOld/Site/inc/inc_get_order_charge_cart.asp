<!--#include virtual="site/inc/inc_helper.asp"-->

<%
	
	' 价格
		dim sub_total, shipping_and_handling, 	sales_tax, grand_total, sub_charge, taxable_total
		'Dim pickup_checked	:	pickup_checked	=	SQLescape(request("pickup_checked"))
		Dim pay_method 		:	pay_method 		=	null
		Dim state_shipping	:	state_shipping	=	null
		Dim country_id		:	country_id		=	null
		dim order_exist_notebook
			order_exist_notebook = false
			sub_total = 0
			shipping_and_handling =0
			sales_tax =0
			grand_total=0
			
		dim gst, pst, hst, gst_rate, pst_rate, hst_rate
		gst = 0
		pst = 0
		hst = 0
		gst_rate = 0
		pst_rate = 0
		hst_rate = 0
		
		Dim price_unit			:	price_unit		=	null
		Dim Order_code			:	Order_Code		= 	SQLescape(request("order_code"))
		Dim is_get_sur_charge	:	is_get_sur_charge=	SQLescape(request("is_get_sur_charge"))
	
		
		set rs = conn.execute("select ctp.gst, ctp.pst, ctp.hst, ctp.gst_rate, ctp.pst_rate, ctp.hst_rate"&_
							"	,ctp.tmp_price_serial_no, ctp.sub_total,ctp.sub_total_rate,ctp.grand_total_rate, ctp.shipping_and_handling"&_
							"	,ctp.sales_tax, ctp.grand_total, ctp.order_code, ctp.create_datetime,ctp.sur_charge ,ctp.taxable_total, ctp.price_unit"&_
							"	, ct.pay_method, ct.state_shipping, ct.country_id, ct.shipping_country_code, ct.shipping_state_code"&_
							"	from tb_cart_temp_price ctp inner join tb_cart_temp ct on ct.cart_temp_code = ctp.order_code "&_
							"	where ctp.order_code='"&Order_Code&"' and ct.current_system="& SQLquote(current_system) )
		
		if not rs.eof then
			sub_total 				= rs("sub_total")
			shipping_and_handling 	= rs("shipping_and_handling")
			sub_charge 				= rs("sur_charge")
			sales_tax 				= rs("sales_tax")
			taxable_total 			= rs("taxable_total")
			
	        gst = rs("gst")
	        pst = rs("pst")
	        hst = rs("hst")
	        gst_rate = rs("gst_rate")
	        pst_rate = rs("pst_rate")
	        hst_rate = rs("hst_rate")
	        
			price_unit 		= rs("price_unit")
			pay_method		=	rs("pay_method")
			'Response.write pay_method & "d"
			if  instr( LAYOUT_RATE_PAY_METHODS , "["&pay_method&"]") > 0 or pay_method="-1" or pay_method="" then 
				'sub_total = rs("sub_total_rate")
				grand_total	= rs("grand_total_rate")
			else
				'sub_total = rs("sub_total")
				grand_total	= rs("grand_total")
			end if
			sub_total 		= rs("sub_total_rate")
			
			
			state_shipping	=	rs("state_shipping")
			country_id		=	rs("country_id")
			Response.cookies("shipping_country_code") 	= 	rs("shipping_country_code")
			Response.cookies("shipping_state_code") 	=	rs("shipping_state_code")
		else
			response.write "Error."
			Response.end
		end if
		rs.close : set rs = nothing
%>

<table width="98%"   border="0" align="center" cellpadding="2" cellspacing="0" bgcolor="#eeeeee" id='cart_charge_table'>
                      <tr bgcolor="#FFFFFF">
                        <td width="85%" class="order_price_comment"><strong><span  style="line-height:24px">SUBTOTAL</span></strong></td>
                        <td style="border-bottom:#eeeeee 1px solid; border-left:#eeeeee 1px solid; border-right:#eeeeee 1px solid;" align="right" bgcolor="#FFFFFF" class="text_hui_11">
                          <span  class='price'><%
									  if sub_total<>"" then 
									  	response.write formatcurrency(sub_total)
									  else
									  	response.write sub_total
									  end if%></span><span class='price_unit'><%= price_unit %></span>
                        </td>
                      </tr>
                      <% 'response.Write( instr( LAYOUT_RATE_PAY_METHODS , "["&pay_method&"]"))
							if  instr( LAYOUT_RATE_PAY_METHODS , "["&pay_method&"]") =0 and pay_method <> "-1" and pay_method<>"" then 
						%>
                    <tr bgcolor="#FFFFFF">
                    	<td class="order_price_comment"> <span>SPECIAL CASH DISCOUNT </span></td>
                    	<td style="border-bottom:#eeeeee 1px solid; border-left:#eeeeee 1px solid; border-right:#eeeeee 1px solid;" align="right" bgcolor="#FFFFFF" class="text_hui_11" >&nbsp;<span class='price'><%
										response.Write("-$" & sub_charge)
									  'if sub_charge<>"" then 
'									  	response.write "-" & sub_charge)
'									  else
'									  	response.write sub_charge
'									  end if%><span class='price_unit'><%= price_unit %></span></td>
                  </tr>
                  	<% end if %>
                  <tr>
                  		<td class="order_price_comment">SHIPPING AND HANDLING </td>
                    	<td style="border-bottom:#eeeeee 1px solid; border-left:#eeeeee 1px solid; border-right:#eeeeee 1px solid;" align="right" bgcolor="#FFFFFF" class="text_hui_11"> <span id="lbl_shipping_charge_input" class='price'><%
									  if  shipping_and_handling <>""  then 
									  	response.write formatcurrency(shipping_and_handling)
									  else
									  	response.write shipping_and_handling
									  end if%></span><span class='price_unit'><%= price_unit %></span></td>
                  </tr>
                  <tr>
                  		<td class="order_price_comment">TAXABLE TOTAL (If applicable)</td>
                    	<td style="border-bottom:#eeeeee 1px solid; border-left:#eeeeee 1px solid; border-right:#eeeeee 1px solid;" align="right" bgcolor="#FFFFFF" class="text_hui_11"> <span id="lbl_tax_input" class='price'><%
									  if  taxable_total<>""  then 
									  	response.write formatcurrency(taxable_total)
									  else
									  	response.write taxable_total
									  end if%></span><span class='price_unit'><%= price_unit %></span> </td>
                  </tr>
                  <%
									  'if  sales_tax<>""  then 
									  '	response.write formatcurrency(sales_tax)
									  'else
									  '	response.write sales_tax
									  'end if
									 
                                        if cint(gst_rate) > 0 then 
                                            response.Write(" <tr> <td class=""order_price_comment"">GST(" &gst_rate&"%)</td>"&_
                                                                 "<td style=""border-bottom:#eeeeee 1px solid; border-left:#eeeeee 1px solid; border-right:#eeeeee 1px solid;"" align=""right"" bgcolor=""#FFFFFF"" class=""text_hui_11""> <span id=""lbl_tax_input"" class='price'>")
                                              if  cdbl(gst)> 0  then 
									          	response.write formatcurrency(gst)
									          else
									          	response.write gst
									          end if
                                             response.Write("</span><span class='price_unit'>"&price_unit&"</span></td></tr>")
                                        end if
                                        if cint(pst_rate) > 0 then 
                                              response.Write(" <tr>"&_
                                                                 "<td class=""order_price_comment"">PST(" &pst_rate&"%)</td><td style=""border-bottom:#eeeeee 1px solid; border-left:#eeeeee 1px solid; border-right:#eeeeee 1px solid;"" align=""right"" bgcolor=""#FFFFFF"" class=""text_hui_11""> <span id=""lbl_tax_input"" class='price'>")
                                              if  cdbl(pst)> 0  then 
									          	response.write formatcurrency(pst)
									          else
									          	response.write pst
									          end if
                                             response.Write("</span><span class='price_unit'>"&price_unit&"</span></td></tr>")
                                        end if
                                        
                                        if cint(hst_rate) >0 then 
                                               response.Write(" <tr><td class=""order_price_comment"">HST(" &hst_rate&"%)</td>"&_
                                                                 "<td style=""border-bottom:#eeeeee 1px solid; border-left:#eeeeee 1px solid; border-right:#eeeeee 1px solid;"" align=""right"" bgcolor=""#FFFFFF"" class=""text_hui_11""> <span id=""lbl_tax_input"" class='price'>")
                                              if  cdbl(hst)> 0  then 
									          	response.write formatcurrency(hst)
									          else
									          	response.write hst
									          end if
                                             response.Write("</span><span class='price_unit'>"&price_unit&"</span></td></tr>")
                                        end if
										
										if cstr(country_id) = CSUS then 
											response.Write(" <tr><td class=""order_price_comment"">Sales Tax(0%)</td>"&_
                                                                 "<td style=""border-bottom:#eeeeee 1px solid; border-left:#eeeeee 1px solid; border-right:#eeeeee 1px solid;"" align=""right"" bgcolor=""#FFFFFF"" class=""text_hui_11""> <span id=""lbl_tax_input"" class='price'>$0.00</span><span class='price_unit'>"&price_unit&"</span></td></tr>")
										end if
                          
									  %>
                  <tr>
                  		<td class="order_price_comment">GRAND TOTAL </td>
                    	<td style="border-bottom:#eeeeee 1px solid; border-left:#eeeeee 1px solid; border-right:#eeeeee 1px solid;" align="right" bgcolor="#FFFFFF" class="text_hui_11"><span id="lbl_total_input" class='price'><%
									  if  grand_total<>""  then 
									  	response.write formatcurrency(grand_total)
									  else
									  	response.write grand_total
									  end if%></span><span class='price_unit'><%= price_unit %></span> </td>
                  </tr>
                </table>

<script type="text/javascript">
	$().ready(function(){
		
		//if('<%= is_get_sur_charge %>' == 'true')
//			$('.sub_charge_span').html('<%= formatcurrency(sub_charge, 2) %>');
		$('#cart_charge_table td').css("background", "#EFEFEF");
	});
	
</script>