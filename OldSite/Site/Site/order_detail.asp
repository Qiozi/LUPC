<!--#include virtual="site/inc/inc_page_top.asp"-->

<table class="page_main" cellpadding="0" cellspacing="0">
	<tr>
    	<td  id="page_main_left" valign="top" style="padding-bottom:5px; padding-left: 2px" class='page_frame'>
        	<!-- left begin -->
            	<!--#include virtual="site/inc/inc_left_menu.asp"-->
            <!-- left end 	-->
    	</td>
    	
        <td id="page_main_center" valign="top"  class='page_frame'>
        	<!-- main begin -->
        	    <div id="page_main_banner"></div>
        	    <div class="page_main_nav">
                	<span class='nav1'><a href="/site/default.asp">Home</a></span>
                    <span class='nav1'>My Account</span>
                	
							<%
								if request("order_type") =1 then 
							%>
									<a href="member_center_Porder.asp" class="white-red-11"><span class='nav1'><strong> Orders </strong></span></a>
							<% 	else %>
									<a href="member_center_Aorder.asp" class="white-red-11"><span class='nav1'><strong> Orders </strong></span></a>
							<% 	end if %>
                    
                    <span class="nav1">Order#<%= SQLescape(request("order_code"))	 %></span>
                </div>
            	<div id="page_main_area">
                <%
	
						Call ValidateUserSessionInfo(LAYOUT_CCID ,"closeConn") 
						Dim current_tmp_order_code	:	current_tmp_order_code = SQLescape(request("order_code"))						
						dim view_order_code
						'view_order_code = encode.stringRequest("order_code")
						Dim amount
						Dim current_grand_price
						Dim rate_pay_methods
						dim user_country
						Dim subRS
						Dim customer_first_name
						Dim customer_last_name
						Dim purchase_order
						
						Dim view_shipping_name
						Dim view_shipping_city
						Dim view_shipping_phone
						Dim view_shipping_address
						Dim view_shipping_state
						Dim view_shipping_zip_code
						Dim pay_method
						Dim pay_method_2
						Dim view_billing_name
						Dim view_billing_city
						Dim view_billing_state
						Dim view_billing_address
						Dim view_billing_phone
						Dim view_billing_zip_code
						Dim cc_type
						Dim regdate
						Dim view_card_billing_info :	view_card_billing_info	=" style='display:none;' "	
						Dim view_shippint_country_code	
						Dim is_owen			:	is_owen = false
						
						
						set rs = conn.execute("select * from tb_customer_store where customer_serial_no="& SQLquote(LAYOUT_CCID) &" and order_code='"&current_tmp_order_code&"'")
						if not rs.eof then
							'order_date = rs("order_date")
							customer_first_name 		= rs("customer_first_name")
							customer_last_name 			= rs("customer_last_name")
							purchase_order 				= rs("my_purchase_order")
							view_shipping_name 			= ucase(rs("customer_shipping_first_name")) & " " & ucase(rs("customer_shipping_last_name"))
							view_shipping_city 			= rs("customer_shipping_city")
							view_shipping_phone 		= rs("phone_d")
							view_shipping_address 		= rs("customer_shipping_address")
							view_shipping_state 		= rs("shipping_state_code")
							view_shippint_country_code	= rs("shipping_country_code")
							view_shipping_zip_code 		= rs("customer_shipping_zip_code")
							pay_method 					= rs("pay_method")
							pay_method_2 				= pay_method
							view_billing_name  			= customer_first_name  & "&nbsp;" & customer_last_name
							view_billing_city 			= rs("customer_card_city") 
							view_billing_state  		= rs("customer_card_state_code")
							view_billing_address 		= rs("customer_card_billing_shipping_address")
							view_billing_phone 			= rs("phone_d")
							view_billing_zip_code 		= rs("customer_card_zip_code")
							cc_type 					= rs("customer_card_type")
							regdate 					= rs("create_datetime")
							is_owen						= true
						end if
						rs.close : set rs = nothing
						
							' 支付方式（如果是卡，则显示卡类型）
							if( cstr(pay_method) = cstr(LAYOUT_PAY_METHOD_CARD)) then
								pay_method = cc_type
							
							else
								pay_method = GetPayMethodNew(pay_method)
							end if
							
							if cstr(SQLescape(pay_method_2)) = cstr(LAYOUT_PAY_METHOD_CARD) or cstr(SQLescape(pay_method_2))=CSTR(LAYOUT_PAYPAL_METHOD_CREDIT_CARD) then 
								view_card_billing_info=" style='display:"""";' "						
							end if
							
							' 价格
							dim sub_total, shipping_and_handling, 	sales_tax, grand_total, tax_rate,input_order_discount
							input_order_discount = 0
							sub_total = 0
							shipping_and_handling =0
							sales_tax =0
							grand_total=0
							
							dim gst, pst, hst, gst_rate, pst_rate, hst_rate, sur_charge, order_price_unit
							gst = 0
							pst = 0
							hst = 0
							gst_rate = 0
							pst_rate = 0
							hst_rate = 0
							
							set rs = conn.execute("select gst, pst, hst, gst_rate, pst_rate, hst_rate,sub_total, total,shipping_charge,tax_charge,sur_charge, gst_rate+pst_rate+hst_rate tax, tax_rate,sub_total_rate, total_rate, grand_total,order_date,input_order_discount, price_unit from tb_order_helper where order_code='"&current_tmp_order_code&"'")
							if not rs.eof and is_owen then	
									
								shipping_and_handling 	= rs("shipping_charge")
								sales_tax 				= rs("tax_charge")	
								sur_charge 				= rs("sur_charge")
								input_order_discount 	= rs("input_order_discount")
								tax_rate 				= rs("tax")
								
								if cstr(tax_rate) = "0" then
									tax_rate = rs("tax_rate")
								end if					
	
								sub_total 				= rs("sub_total_rate")
								grand_total				= rs("grand_total")
								regdate 				= rs("order_date")								
								gst 					= rs("gst")
								pst 					= rs("pst")
								hst 					= rs("hst")
								gst_rate 				= rs("gst_rate")
								pst_rate 				= rs("pst_rate")
								hst_rate 				= rs("hst_rate")
								order_price_unit		= rs("price_unit")
							end if
							rs.close : set rs = nothing
					%>
                	<table width="600" height="670"  border="0" align="center" cellpadding="1" cellspacing="2" bgcolor="#FFFFFF" style="border:#8FC2E2 1px solid; ">
                            <tr>
                              <td valign="top" style="border:#E3E3E3 1px solid; "><table width="100%" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                  <td height="10"></td>
                                </tr>
                              </table>
                                <table width="80%"  border="0" align="center" cellpadding="0" cellspacing="0">
                                  <tr>
                                    <td>&nbsp;</td>
                                    </tr>
                                </table>
                                <div id="email_area">
                                <table width="98%" style="border-bottom:#327AB8 1px solid; "  border="0" align="center" cellpadding="0" cellspacing="0">
                                  <tr>
                                    <td>&nbsp;</td>
                                  </tr>
                                </table>
                                <table width="80%" height="30"  border="0" align="center" cellpadding="0" cellspacing="0">
                                  <tr>
                                    <td class="text_hui_11">&nbsp;</td>
                                  </tr>
                                  <tr>
                                    <td style="font-size:11pt"><strong> LU COMPUTERS ORDER FORM</strong></td>
                                  </tr>
                                </table>
                                <table width="80%"  border="0" align="center" cellpadding="2" cellspacing="0">
                                  <tr>
                                    <td width="50%" class="text_hui_11" >&nbsp;</td>
                                  <td width="50%" class="text_hui_11" >
                Tel: (866)999-7828 (416)446-7743</td>
                                  </tr>
                                  <tr>
                                    <td><p class="text_hui_11"><br>                      
                                      </p>                      </td>
                                  <td>&nbsp;</td>
                                  </tr>
                                  <tr>
                                    <td><table width="100%"  border="0" cellspacing="0" cellpadding="2">
                                      <tr>
                                        <td width="50%" class="text_hui_11"><strong>Order Number:</strong></td>
                                      <td width="50%" class="text_hui_11"><%= current_tmp_order_code%></td>
                                      </tr>
                                      <tr>
                                        <td class="text_hui_11"><strong>Date: </strong></td>
                                      <td class="text_hui_11"><%=ConvertDate(regdate) %></td>
                                      </tr>
                                      <tr>
                                        <td class="text_hui_11"><strong>Customer Name:</strong></td>
                                      <td class="text_hui_11"><%= customer_first_name %>&nbsp;<%= customer_last_name %></td>
                                      </tr>
                                    </table>
                                     </td>
                                    <td><table width="100%"  border="0" cellspacing="0" cellpadding="2">
                                      <tr>
                                        <td width="50%" class="text_hui_11"><strong> Customer Number:</strong></td>
                                        <td width="50%" class="text_hui_11"><%'=customerID%><iframe src="/FilterCustomerCode.aspx?customer_id=<%=LAYOUT_CCID%>" frameborder="0" width="80" style="height:12px;"  scrolling="no"></iframe></td>
                                      </tr>
                                      <tr>
                                        <td class="text_hui_11"><strong>Payment: </strong></td>
                                        <td class="text_hui_11"><%= pay_method%>　 </td>
                                      </tr>
                                      <tr>
                                        <td class="text_hui_11"><strong><!--Purchase Order# --></strong></td>
                                        <td class="text_hui_11"><%'= my_purchase_order%></td>
                                      </tr>
                                    </table></td>
                                  </tr>
                                </table>
                                <table width="98%" style="border-bottom:#327AB8 1px solid; "  border="0" align="center" cellpadding="0" cellspacing="0">
                                  <tr>
                                    <td>&nbsp;</td>
                                  </tr>
                                </table>                
                                <table width="80%"  border="0" align="center" cellpadding="2" cellspacing="0">
      
                                  <tr>
                                    <td class="text_hui_11" <%= view_card_billing_info %>><strong>Billing Address: </strong></td>
                                    <td class="text_hui_11"><strong>Shipping Address:</strong></td>
                                  </tr>
                                  <tr>
                                    <td <%= view_card_billing_info %>><p class="text_hui_11" >
                                    <%
                                    response.write view_billing_name &"<br/>"
                                    response.write view_billing_address &"<br/>"
                                    response.write view_billing_city &"&nbsp;&nbsp;" & view_billing_state &"<br/>"
                                    response.write view_billing_zip_code &"<br/>"
                                    'response.write view_billing_phone &"<br/>"
                                    %>
                                    <%'= customer_card_city &"&nbsp;&nbsp;"& customer_card_state &"&nbsp;&nbsp;"&customer_card_billing_shipping_address %>　 <br>
                                      Phone # <%= view_billing_phone %> </p>
                                      </td>
                                    <td class="text_hui_11"> 
                                    <%
                                    response.write view_shipping_name &"<br/>"
                                    response.write view_shipping_address &"<br/>"
                                    response.write view_shipping_city &"&nbsp;&nbsp;" & view_shipping_state &"<br/>"
                                    response.write view_shipping_zip_code &"<br/>"	
                                    %>
                                     <br>
                      Phone # <%= view_shipping_phone %> 
                                    <%
                                        dim Estimated_Shipping_Date,  ups_tracking_number
                                        Estimated_Shipping_Date = ""
                                        ups_tracking_number = ""
                                        set subRS = conn.execute("select date_format(regdate,'%m/%d/%Y') regdate,ups_tracking_number from tb_order_ups_tracking_number  where order_code='"& current_tmp_order_code &"' order by id desc limit 0,1")
                                        if not subRS.eof then 
                                            Estimated_Shipping_Date = subRS(0)
                                            ups_tracking_number = subRS(1)
                                        end if
                                        subRS.close : set subRS = nothing
                                        if Estimated_Shipping_Date <> "" then 
                                            response.Write("<br/><b>Estimated Shipping Date:</b>&nbsp;&nbsp;"& Estimated_Shipping_Date)		
                                            response.write ("<br/><b>UPS TRACKING NUMBER:</b>&nbsp;&nbsp; " & ups_tracking_number )		
                                        end if
                                    %>
                      </td>
                                  </tr>
                                </table>
                                <table width="98%" style="border-bottom:#327AB8 1px solid; "  border="0" align="center" cellpadding="0" cellspacing="0">
                                  <tr>
                                    <td>&nbsp;</td>
                                  </tr>
                                </table>
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
                                    <td><!--order begin-->
                                    <table width="100%"  border="0" cellpadding="2" cellspacing="1" bgcolor="#eeeeee">
                                    <%
                
                
                                            
                                            if isnumeric(current_tmp_order_code) and is_owen then
                                            n = 0
                                                ' product parts
                                                
                                                
                                                set rs = conn.execute("select p.product_serial_no, c.product_name,c.order_product_sum, c.order_product_sold from tb_order_product c inner join tb_product p on p.product_serial_no=c.product_serial_no where order_code='"&current_tmp_order_code&"' and  c.product_type=1")
                                                
                                                if not rs.eof then
                                                    
                                                do while not rs.eof 
                                                amount = cdbl(rs("order_product_sold")) * cint(rs("order_product_sum"))
                                                n = n +1
                                    %>
                                         <tr <%if n mod 2 = 0 then response.write " bgcolor=""#FFFFFF"" " else response.write " bgcolor=""#f2f2f2"""%>>
                                                <td >&nbsp;<%
                                                      'if len(rs("product_serial_no")) = 8 then 
                                                      '	response.write "<a href=""view_print_system.asp?cmd=print&id="&rs("product_serial_no")&""" onClick=""return js_callpage(this.href)"">"&rs("product_name")&"</a>"
                                                     ' else
                                                        response.write rs("product_name")
                                                     ' end if
                                                     %></td>
                                                <td width="9%" align="center" class="text_hui_11"><%=rs("order_product_sum")%></td>
                                                <td width="11%" align="right" class="text_hui_11" style="font-size:10pt;"><%= formatcurrency(rs("order_product_sold"),2)%></td>
                                                <td width="9%" align="right" class="text_hui_11" style="font-size:10pt;"><%= formatcurrency( amount,2)%>&nbsp; </td>
                                        </tr>
                                        <%
                                                rs.movenext
                                                    loop
                                                end if
                                                rs.close :set rs = nothing
                                                
                                                
                                                ' product noebooks
                                                set rs = conn.execute("select c.order_product_sold ,c.order_product_sum,c.product_name from tb_order_product c inner join tb_product p on p.product_serial_no=c.product_serial_no where order_code='"&current_tmp_order_code&"' and  c.product_type=3")
                                                if not rs.eof then
                                                do while not rs.eof 
                                                amount = cdbl(rs("order_product_sold")) * cint(rs("order_product_sum"))
                                                n = n +1
                                        %>
                                                 <tr <%if n mod 2 = 0 then response.write " bgcolor=""#FFFFFF"" " else response.write " bgcolor=""#f2f2f2"""%>>
                                                        <td >&nbsp;<%
                                                              'if len(rs("product_serial_no")) = 8 then 
                                                              '	response.write "<a href=""view_print_system.asp?cmd=print&id="&rs("product_serial_no")&""" onClick=""return js_callpage(this.href)"">"&rs("product_name")&"</a>"
                                                             ' else
                                                                response.write rs("product_name")
                                                             ' end if
                                                             %></td>
                                                        <td width="9%" align="center" class="text_hui_11"><%=rs("order_product_sum")%></td>
                                                        <td width="11%" align="right" class="text_hui_11" style="font-size:10pt;"><%= formatcurrency(rs("order_product_sold"),2)%></td>
                                                        <td width="9%" align="right" class="text_hui_11" style="font-size:10pt;"><%= formatcurrency( amount,2)%>&nbsp; </td>
                                                </tr>
                                        <%
                                                rs.movenext
                                                    loop
                                                end if
                                                rs.close :set rs = nothing
                                                
                                                ' product system 
                                                set rs = conn.execute("select order_product_sum,order_product_sold,product_serial_no,product_name from tb_order_product  where order_code='"&current_tmp_order_code&"' and length(product_serial_no)=8")
                                                
                                                if not rs.eof then
                                                    amount = 0
                                                    current_grand_price = 0
                                                    do while not rs.eof 
                                                    if rs("order_product_sold") <> "" then 
                                                        amount = cdbl(rs("order_product_sold")) * cint(rs("order_product_sum"))
                                                    end if
                                                    if rs("order_product_sold") <> "" then 
                                                        current_grand_price = rs("order_product_sold")
                                                    end if
                                                    n = n +1
                                        %>
                                                <tr <%if n mod 2 = 0 then response.write " bgcolor=""#FFFFFF"" " else response.write " bgcolor=""#f2f2f2"""%>>
                                                        <td >&nbsp;<%
                                                              'if len(rs("product_serial_no")) = 8 then 
                                                                'response.write "<a href=""view_print_system.asp?cmd=print&id="&rs("product_serial_no")&""" onClick=""return js_callpage(this.href)"">"&rs("product_name")&"</a>"
                                                            '  else
                                                                response.write rs("product_name")
                                                             ' end if
                                                             %></td>
                                                        <td width="9%" align="center" class="text_hui_11"><%=rs("order_product_sum")%></td>
                                                        <td width="11%" align="right" class="text_hui_11" style="font-size:10pt;"><%= formatcurrency(current_grand_price,2)%></td>
                                                        <td width="9%" align="right" class="text_hui_11" style="font-size:10pt;"><%= formatcurrency( amount,2)%>&nbsp; </td>
                                                </tr>
                                        <tr>
                                            <td style="background:white;" colspan="4">
                                                <%
                                                    set crs = conn.execute("select p.product_name,sp.part_quantity from tb_sp_tmp_detail sp inner join tb_product p on p.product_serial_no=sp.product_serial_no  where sys_tmp_code='"&rs("product_serial_no")&"' and (p.is_non=0 or p.product_name like '%onboard%') order by sp.product_order asc ")
                                                        if not crs.eof then 
                                                            response.write "<table style='margin-left:2em;list-style:decimal;width: 550px'>"
                                                            do while not crs.eof 
                                                                
                                                                response.write "<tr><td class='system_detail_list'>"&crs("product_name")& "</td><td style='width: 20px;'>x "& crs("part_quantity") & "</td></tr>"
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
                                        end if
                                        %>
                                        </table>
                                    <!--order end-->
                
                                    </td>
                                  </tr>
                                </table>
                                <table width="98%"   border="0" align="center" cellpadding="2" cellspacing="0" bgcolor="#eeeeee">
                                      <tr bgcolor="#FFFFFF">
                                        <td width="80%" class="order_price_comment"><span><strong>SUBTOTAL  </strong></span></td>
                                        <td style="border-bottom:#eeeeee 1px solid; border-left:#eeeeee 1px solid; border-right:#eeeeee 1px solid;" width="20%" align="right" bgcolor="#FFFFFF" ><span ><%
                                                          if sub_total <> "" then 
                                                            response.write formatcurrency(sub_total)
                                                          else
                                                            response.write sub_total
                                                          end if
                                                         
                                                          %></span><span class="price_unit"><%= order_price_unit %></span></td>
                                      </tr>
                                      <%
                                                if  instr( LAYOUT_RATE_PAY_METHODS , "["&pay_method_2&"]") < 1  then 
                                            %>
                                      <tr>
                                      		<td class="order_price_comment">SPECIAL CASH DISCOUNT</td>
                                        	<td style="border-bottom:#eeeeee 1px solid; border-left:#eeeeee 1px solid; border-right:#eeeeee 1px solid;" align="right" bgcolor="#FFFFFF" ><span  class="price1">
                                        -
                                        <%
                                                          if sur_charge<>"" then 
                                                            response.write formatcurrency(sur_charge)
                                                          else
                                                            response.write sur_charge
                                                          end if%>
                                        </span><span class="price_unit"><%= order_price_unit %></span></td>
                                      </tr>
                                      <% end if %>
                                      <tr>
                                       	 	<td class="order_price_comment">SHIPPING AND HANDLING</td>
                                            <td style="border-bottom:#eeeeee 1px solid; border-left:#eeeeee 1px solid; border-right:#eeeeee 1px solid;" align="right" bgcolor="#FFFFFF" ><span ><%
                                                          if shipping_and_handling <> "" then 
                                                            response.write formatcurrency(shipping_and_handling)
                                                          else
                                                            response.write shipping_and_handling
                                                          end if%></span><span class="price_unit"><%= order_price_unit %></span></td>
                                      </tr>
                                       <%
                                                          'if  sales_tax<>""  then 
                                                          '	response.write formatcurrency(sales_tax)
                                                          'else
                                                          '	response.write sales_tax
                                                          'end if
                                                         
                                                            if cint(gst_rate) > 0 then 
                                                                response.Write(" <tr>"&_
                                                                                     "<td class=""order_price_comment"">GST("&gst_rate&"%)</td><td style=""border-bottom:#eeeeee 1px solid; border-left:#eeeeee 1px solid; border-right:#eeeeee 1px solid;"" align=""right"" bgcolor=""#FFFFFF"" > <span>")
                                                                  if  cdbl(gst)> 0  then 
                                                                    response.write formatcurrency(gst)
                                                                  else
                                                                    response.write gst
                                                                  end if
                                                                 response.Write("</span><span class=""price_unit"">"& order_price_unit &"</span></td></tr>")
                                                            end if
                                                            if cint(pst_rate) > 0 then 
                                                                  response.Write(" <tr>"&_
                                                                                     "<td class=""order_price_comment"">PST("&pst_rate&"%)</td><td style=""border-bottom:#eeeeee 1px solid; border-left:#eeeeee 1px solid; border-right:#eeeeee 1px solid;"" align=""right"" bgcolor=""#FFFFFF"" > <span >")
                                                                  if  cdbl(pst)> 0  then 
                                                                    response.write formatcurrency(pst)
                                                                  else
                                                                    response.write pst
                                                                  end if
                                                                 response.Write("</span><span class=""price_unit"">"& order_price_unit &"</span></td></tr>")
                                                            end if
                                                            
                                                            if cint(hst_rate) >0 then 
                                                                   response.Write(" <tr>"&_
                                                                                     "<td class=""order_price_comment"">HST("&hst_rate&"%)</td><td style=""border-bottom:#eeeeee 1px solid; border-left:#eeeeee 1px solid; border-right:#eeeeee 1px solid;"" align=""right"" bgcolor=""#FFFFFF"" > <span >")
                                                                  if  cdbl(hst)> 0  then 
                                                                    response.write formatcurrency(hst)
                                                                  else
                                                                    response.write hst
                                                                  end if
                                                                 response.Write("</span><span class=""price_unit"">"& order_price_unit &"</span></td></tr>")
                                                            end if
                                              
														if cstr(view_shippint_country_code) = "US" then 
															response.Write(" <tr><td class=""order_price_comment"">Sales Tax(0%)</td>"&_
																				 "<td style=""border-bottom:#eeeeee 1px solid; border-left:#eeeeee 1px solid; border-right:#eeeeee 1px solid;"" align=""right"" bgcolor=""#FFFFFF"" class=""text_hui_11""> <span>$0.00</span><span class=""price_unit"">"& order_price_unit &"</span></td></tr>")
														end if
                                                          %>
                                      <tr>
                                      		<td class="order_price_comment">GRAND TOTAL</td>
                                        	<td style="border-bottom:#eeeeee 1px solid; border-left:#eeeeee 1px solid; border-right:#eeeeee 1px solid;" align="right" bgcolor="#FFFFFF" ><span><%
                                                          if "" <> (grand_total) then 
                                                            response.write formatcurrency(grand_total)
                                                          else
                                                            response.write grand_total
                                                          end if%></span><span class="price_unit"><%= order_price_unit %></span></td>
                                      </tr>
                                    </table>
                               
                                
                                <%
                                    dim charat, charat_cmd
                                    charat = ""
                                    set rs = conn.execute("select 	serial_no, order_code, product_serial_no, order_product_sum, order_product_price, 	tag, 	order_product_cost, 	sku, 	menu_child_serial_no, 	menu_pre_serial_no, 	product_type,	product_name, 	old_price, 	order_product_sold, 	save_price, 	product_type_name, 	is_old, 	product_current_price_rate, 	create_datetime, 	add_del	from 	tb_order_product_history where order_code='"& current_tmp_order_code &"'")
                                    if not rs.eof then 
                                        response.Write(" <hr size='1'><table style='width:100%'>")
                                            do while not rs.eof 
                                                if rs("add_del") = 1 then 
                                                    charat = "$"
                                                    charat_cmd = "(add)"
                                                else
                                                    charat = "-$"
                                                    charat_cmd = "(remove)"
                                                end if
                                                response.Write("<tr><td>"& rs("product_serial_no") &"</td>")
                                                response.Write("<td>"& rs("product_name")&"</td>")
                                                response.Write("<td>"& rs("order_product_sum") &"</td>")
                                                response.Write("<td style='text-align: right'>"& charat &rs("order_product_sold")&"</td>")
                                                response.Write("<td style='text-align: right'>"& charat &  cint(rs("order_product_sum")) * cdbl(rs("order_product_sold"))&"</td>")
                                                response.Write("<td>"& charat_cmd &"</td></tr>")
                                            rs.movenext
                                            loop
                                        response.Write("</table>")
                                    end if
                                    rs.close : set rs = nothing
                                %>
                                <table width="98%" style="border-bottom:#327AB8 1px solid; "  border="0" align="center" cellpadding="0" cellspacing="0">
                                  <tr>
                                    <td>&nbsp;</td>
                                  </tr>
                                </table></div>
                                <table width="100%"  border="0" align="left" cellpadding="3" cellspacing="0">
                                  <tr>
                                    <td height="60" align="center">
                                    <table border="0" cellpadding="3" cellspacing="0">
                                         
                                            <tr align="right">
                                              <td><table id="__" border="0" cellpadding="0" cellspacing="0" height="24" width="130">
                                                 
                                                    <tr>
                                                      <td width="28"><font style="font-size: 8.5pt;" face="Tahoma"> <img src="/soft_img/app/arrow_left.gif" alt="" height="24" width="28"></font></td>
                                                      <td class="btn_middle"><strong><a class="btn_img" onClick="window.history.go(-1);">Back</a></strong> </td>
                                                      <td width="6"><font style="font-size: 8.5pt;" face="Tahoma"> <img src="/soft_img/app/customer_bottom_04.gif" alt="" height="24" width="6"></font></td>
                                                    </tr>
                                                  
                                              </table></td>
                                             
                                            </tr>
                                          
                                      </table>
                                    </td>
                                    </tr>
                                </table></td>
                            </tr>
                          </table>
                          <table width="600"  border="0" align="center" cellpadding="0" cellspacing="0">
                                <tr>
                                  <td height="5"></td>
                                </tr>
                            </table>
                
                </div>
            <!-- main end 	-->
        </td>
        <td id="page_main_right" valign="top" class='page_frame'>
        	<!-- right begin -->                   	
            	<div id="page_main_right_html"><!--#include virtual="/Site/inc/inc_right.asp"--></div>
            <!-- right end 	-->
        </td>
        <td valign="bottom" id="page_main_right_backgroundImg" style="border-left: 1px solid #8E9AA8"><img src="/soft_img/app/left_bt.gif" width="14" height="214"></td>
    </tr>
</table>

<!--#include virtual="site/inc/inc_bottom.asp"-->

<% closeconn() %>
<script type="text/javascript">
$().ready(function(){
	//$('#page_main_area').load('/site/inc/inc_default.asp?'+rand(1000));
});
</script>