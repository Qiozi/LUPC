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
              <table width="98%"  border="0" align="center" cellpadding="0" cellspacing="0">
                  <tr>
                    <td><table width="100%" height="32"  border="0" cellpadding="2" cellspacing="1">
                      <tr align="center" bgcolor="#FFFFFF">
                        <td class="text_hui_11"><strong>Description</strong></td>
                        <td width="9%" class="text_hui_11"><strong>QTY </strong></td>
                        <td width="11%" class="text_hui_11"><strong>Unit Price </strong></td>
                        <td width="9%" class="text_hui_11"><strong>Total </strong></td>
                      </tr>
                    </table></td>
                  </tr>
                  <tr>
                    <td>
					<!--order begin-->
					<table width="100%"  border="0" cellpadding="2" cellspacing="1" bgcolor="#eeeeee">
					<%
						dim category,n, amount, unit_price
						category = SQLescape("category")
						unit_price = 0
						if len(LAYOUT_ORDER_CODE) = LAYOUT_ORDER_LENGTH then						
							n = 0
 								' product parts
								set rs = conn.execute("select p.product_serial_no, c.product_name,cart_temp_Quantity, price, price_rate,"&_
"case when product_store_sum >2 then 2 "&_
"when ltd_stock >2 then 2  "&_
"when product_store_sum + ltd_stock >2 then 2  "&_
"when product_store_sum  <=2 and product_store_sum >0 then 3 "&_
"when product_store_sum +ltd_stock <=2 and product_store_sum +ltd_stock >0 then 3 "&_
"when ltd_stock <=2 and ltd_stock >0 then 3 "&_
"when product_store_sum +ltd_stock =0 then 4 "&_
"when product_store_sum +ltd_stock <0 then 5 end as ltd_stock "&_
",shipping_company"&_
" from tb_cart_temp c inner join tb_product p on p.product_serial_no=c.product_serial_no where cart_temp_code='"&LAYOUT_ORDER_CODE&"' and is_noebook=0 and c.current_system="& SQLquote(current_system) )
								if not rs.eof then
									
								do while not rs.eof 					
									shipping_company = rs("shipping_company")			
									unit_price = cdbl(rs("price_rate"))	
									amount = unit_price * cint(rs("cart_temp_Quantity"))
									n = n +1
					%>
                                     <tr <%if n mod 2 = 0 then response.write " bgcolor=""#FFFFFF"" " else response.write " bgcolor=""#f2f2f2"""%>>
                                            <td>&nbsp;<%= rs("product_name")& FindPartStoreStatus_system_setting(rs("ltd_stock"))  %></td>
                                            <td width="9%" align="center" class="text_hui_11"><%=rs("cart_temp_Quantity")%></td>
                                            <td width="11%" align="right" class="text_hui_11"><%= formatcurrency(unit_price ,2)%></td>
                                            <td width="9%" align="right" class="text_hui_11"><%= formatcurrency(amount,2)%>&nbsp; </td>
                                    </tr>
                     
						<%
								rs.movenext
									loop
								end if
								rs.close :set rs = nothing
								
								
								' product noebooks
								unit_price = 0
								set rs = conn.execute("select p.product_serial_no, c.product_name,cart_temp_Quantity, price, price_rate,"&_
"case when product_store_sum >2 then 2 "&_
"when ltd_stock >2 then 2  "&_
"when product_store_sum + ltd_stock >2 then 2  "&_
"when product_store_sum  <=2 and product_store_sum >0 then 3 "&_
"when product_store_sum +ltd_stock <=2 and product_store_sum +ltd_stock >0 then 3 "&_
"when ltd_stock <=2 and ltd_stock >0 then 3 "&_
"when product_store_sum +ltd_stock =0 then 4 "&_
"when product_store_sum +ltd_stock <0 then 5 end as ltd_stock "&_
",shipping_company"&_
" from tb_cart_temp c inner join tb_product p on p.product_serial_no=c.product_serial_no where cart_temp_code='"&LAYOUT_ORDER_CODE&"' and is_noebook=1 and c.current_system="& SQLquote(current_system))
								if not rs.eof then
								do while not rs.eof 
									shipping_company = rs("shipping_company")
									order_exist_notebook = true								
									unit_price = cdbl(rs("price_rate")) 								
									amount = unit_price * cint(rs("cart_temp_Quantity"))
									n = n +1
						%>
								 <tr <%if n mod 2 = 0 then response.write " bgcolor=""#FFFFFF"" " else response.write " bgcolor=""#f2f2f2"""%>>
										<td >&nbsp;<%=rs("product_name")& FindPartStoreStatus_system_setting(rs("ltd_stock"))%></td>
										<td width="9%" align="center" class="text_hui_11"><%=rs("cart_temp_Quantity")%></td>
										<td width="11%" align="right" class="text_hui_11"><%= formatcurrency(unit_price,2)%></td>
										<td width="9%" align="right" class="text_hui_11"><%= formatcurrency( amount,2)%>&nbsp; </td>
								</tr>
								
						<%
								rs.movenext
									loop
								end if
								rs.close :set rs = nothing
								
								' product system 
								unit_price = 0
								set rs = conn.execute("select * from tb_cart_temp  where cart_temp_code='"&LAYOUT_ORDER_CODE&"' and length(product_serial_no)=8")
								if not rs.eof then
									do while not rs.eof 				
									shipping_company = rs("shipping_company")				
									unit_price = cdbl(rs("price_rate")) 
									amount = unit_price * cint(rs("cart_temp_Quantity"))
									n = n +1
						%>
								<tr <%if n mod 2 = 0 then response.write " bgcolor=""#FFFFFF"" " else response.write " bgcolor=""#f2f2f2"""%>>
										<td ><%											  
												response.write rs("product_name")											  
											 %></td>
										<td width="9%" align="center" class="text_hui_11"><%=rs("cart_temp_Quantity")%></td>
										<td width="11%" align="right" class="text_hui_11"><%= formatcurrency(unit_price,2)%></td>
										<td width="9%" align="right" class="text_hui_11"><%= formatcurrency( amount,2)%>&nbsp; </td>
								</tr>
                       
						<tr>
							<td style="background:white;" colspan="4">
								<%
									
									set crs = conn.execute("select part_quantity,p.product_name,pg.part_group_name, p.product_serial_no ,"&_
"case when product_store_sum >2 then 2 "&_
"when ltd_stock >2 then 2  "&_
"when product_store_sum + ltd_stock >2 then 2  "&_
"when product_store_sum  <=2 and product_store_sum >0 then 3 "&_
"when product_store_sum +ltd_stock <=2 and product_store_sum +ltd_stock >0 then 3 "&_
"when ltd_stock <=2 and ltd_stock >0 then 3 "&_
"when product_store_sum +ltd_stock =0 then 4 "&_
"when product_store_sum +ltd_stock <0 then 5 end as ltd_stock from tb_sp_tmp_detail sp inner join tb_product p on sp.product_serial_no=p.product_serial_no inner join tb_part_group pg on sp.part_group_id=pg.part_group_id inner join tb_product_category pc on pc.menu_child_serial_no=p.menu_child_serial_no where sp.sys_tmp_code="&rs("product_serial_no")&" and (p.is_non=0 or p.product_name like '%onboard%') and p.tag=1 order by   sp.product_order asc ")
										if not crs.eof then 
											response.write "<table style='margin-left:2em;list-style:decimal;line-height:15px; width: 550px'>"
											do while not crs.eof 
												
												response.write "<tr><td class='system_detail_list'>"&crs("product_name")& FindPartStoreStatus_system_setting(crs("ltd_stock"))& "</td><td style='width: 20px;'>x "& crs("part_quantity") & "</td></tr>"
											crs.movenext
											loop
											response.write "</table>"
										end if
										crs.close :set crs = nothing
								%>
								
							</td>
						</tr>
						<%
									rs.movenext
									loop
								end if
								rs.close :set rs = nothing
						%>
						</table>
					<!--order end-->
					<%end if%>
                     
					
					</td>
                  </tr>
                </table>
<table width="98%"   border="0" align="center" cellpadding="2" cellspacing="0" bgcolor="#eeeeee">
                      <tr bgcolor="#FFFFFF">
                        <td width="91%" class="order_price_comment"><strong><span  style="line-height:24px">SUBTOTAL</span></strong></td>
                        <td style="border-bottom:#eeeeee 1px solid; border-left:#eeeeee 1px solid; border-right:#eeeeee 1px solid;" align="right" bgcolor="#FFFFFF" class="text_hui_11">
                          <%
									  if sub_total<>"" then 
									  	response.write formatcurrency(sub_total)
									  else
									  	response.write sub_total
									  end if%><span class='price_unit'><%= price_unit %></span>
                        </td>
                      </tr>
                      <% 'response.Write( instr( LAYOUT_RATE_PAY_METHODS , "["&pay_method&"]"))
							if  instr( LAYOUT_RATE_PAY_METHODS , "["&pay_method&"]") =0 and pay_method <> "-1" and pay_method<>"" then 
						%>
                    <tr bgcolor="#FFFFFF">
                    	<td class="order_price_comment"> <span style="color:#FF4400">SPECIAL CASH DISCOUNT </span></td>
                    	<td style="border-bottom:#eeeeee 1px solid; border-left:#eeeeee 1px solid; border-right:#eeeeee 1px solid;color:#FF4400" align="right" bgcolor="#FFFFFF" class="text_hui_11">&nbsp;<%
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
                    	<td style="border-bottom:#eeeeee 1px solid; border-left:#eeeeee 1px solid; border-right:#eeeeee 1px solid;" align="right" bgcolor="#FFFFFF" class="text_hui_11"> <span id="lbl_shipping_charge_input"><%
									  if  shipping_and_handling <>""  then 
									  	response.write formatcurrency(shipping_and_handling)
									  else
									  	response.write shipping_and_handling
									  end if%></span><span class='price_unit'><%= price_unit %></span></td>
                  </tr>
                  <tr>
                  		<td class="order_price_comment">TAXABLE TOTAL (If applicable)</td>
                    	<td style="border-bottom:#eeeeee 1px solid; border-left:#eeeeee 1px solid; border-right:#eeeeee 1px solid;" align="right" bgcolor="#FFFFFF" class="text_hui_11"> <span id="lbl_tax_input"><%
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
                                                                 "<td style=""border-bottom:#eeeeee 1px solid; border-left:#eeeeee 1px solid; border-right:#eeeeee 1px solid;"" align=""right"" bgcolor=""#FFFFFF"" class=""text_hui_11""> <span id=""lbl_tax_input"">")
                                              if  cdbl(gst)> 0  then 
									          	response.write formatcurrency(gst)
									          else
									          	response.write gst
									          end if
                                             response.Write("</span><span class='price_unit'>"&price_unit&"</span></td></tr>")
                                        end if
                                        if cint(pst_rate) > 0 then 
                                              response.Write(" <tr>"&_
                                                                 "<td class=""order_price_comment"">PST(" &pst_rate&"%)</td><td style=""border-bottom:#eeeeee 1px solid; border-left:#eeeeee 1px solid; border-right:#eeeeee 1px solid;"" align=""right"" bgcolor=""#FFFFFF"" class=""text_hui_11""> <span id=""lbl_tax_input"">")
                                              if  cdbl(pst)> 0  then 
									          	response.write formatcurrency(pst)
									          else
									          	response.write pst
									          end if
                                             response.Write("</span><span class='price_unit'>"&price_unit&"</span></td></tr>")
                                        end if
                                        
                                        if cint(hst_rate) >0 then 
                                               response.Write(" <tr><td class=""order_price_comment"">HST(" &hst_rate&"%)</td>"&_
                                                                 "<td style=""border-bottom:#eeeeee 1px solid; border-left:#eeeeee 1px solid; border-right:#eeeeee 1px solid;"" align=""right"" bgcolor=""#FFFFFF"" class=""text_hui_11""> <span id=""lbl_tax_input"">")
                                              if  cdbl(hst)> 0  then 
									          	response.write formatcurrency(hst)
									          else
									          	response.write hst
									          end if
                                             response.Write("</span><span class='price_unit'>"&price_unit&"</span></td></tr>")
                                        end if
										
										if cstr(country_id) = CSUS then 
											response.Write(" <tr><td class=""order_price_comment"">Sales Tax(0%)</td>"&_
                                                                 "<td style=""border-bottom:#eeeeee 1px solid; border-left:#eeeeee 1px solid; border-right:#eeeeee 1px solid;"" align=""right"" bgcolor=""#FFFFFF"" class=""text_hui_11""> <span id=""lbl_tax_input"">$0.00</span><span class='price_unit'>"&price_unit&"</span></td></tr>")
										end if
                          
									  %>
                  <tr>
                  		<td class="order_price_comment">GRAND TOTAL </td>
                    	<td style="border-bottom:#eeeeee 1px solid; border-left:#eeeeee 1px solid; border-right:#eeeeee 1px solid;" align="right" bgcolor="#FFFFFF" class="text_hui_11"><span id="lbl_total_input"><%
									  if  grand_total<>""  then 
									  	response.write formatcurrency(grand_total)
									  else
									  	response.write grand_total
									  end if%></span><span class='price_unit'><%= price_unit %></span> </td>
                  </tr>
                </table>

<script type="text/javascript">
	$().ready(function(){
		
		if('<%= is_get_sur_charge %>' == 'true')
			$('#sub_charge_span').html('<%= sub_charge %>');
	});
	
</script>