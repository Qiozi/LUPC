
<!--#include virtual="site/inc/inc_page_top.asp"-->
<!--#include virtual="site/no_express.asp"-->
<table class="page_main" cellpadding="0" cellspacing="0">
	<tr>
    	<td  id="page_main_left" valign="top" style="padding-bottom:5px; padding-left: 2px" class='page_frame'>
        	<!-- left begin -->
            	<!--#include virtual="site/inc/inc_left_menu.asp"-->
            <!-- left end 	-->
    	</td>
    	
        <td id="page_main_center" valign="top" class='page_frame'>
        	<!-- main begin -->
        	    <div id="page_main_banner"></div>
        	    <div class="page_main_nav">
                	<span class='nav1'><a href="/site/default.asp">Home</a></span>
                    <span class='nav1'>Shopping</span>
                	<span class='nav1'>Check Out</span>
                </div>
            	<div id="page_main_area">
                		<%
						
						Call ValidateUserSessionInfo(LAYOUT_CCID ,"closeConn") 
						Call ValidateOrder_Code("")
						Call CopyCartToOrder(LAYOUT_ORDER_CODE, LAYOUT_CCID, "")
						
						Dim order_date
						Dim shipping_country_code
						Dim order_current_system
						Dim order_price_unit
						Dim style_view_billing_address
						Dim style_view_shipping_address
						
						dim sub_total, shipping_and_handling, 	sales_tax, grand_total, tax_rate,sur_charge
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
						
						set rs = conn.execute("select gst, pst, hst, gst_rate, pst_rate, hst_rate,sub_total, total,shipping_charge,tax_charge, gst_rate+pst_rate+hst_rate tax, sub_total_rate, total_rate,sur_charge,grand_total, date_format(create_datetime, '%b/%d/%y') order_date, current_system, price_unit from tb_order_helper where order_code='"&LAYOUT_ORDER_CODE&"' and customer_serial_no="& SQLquote(LAYOUT_CCID))
						if not rs.eof then			
							shipping_and_handling 	= rs("shipping_charge")
							sales_tax 				= rs("tax_charge")	
							sur_charge 				= rs("sur_charge")	
							tax_rate 				= rs("tax")				
							grand_total				= rs("grand_total")				
							sub_total 				= rs("sub_total_rate")							
							gst 					= rs("gst")
							pst 					= rs("pst")
							hst 					= rs("hst")
							gst_rate 				= rs("gst_rate")
							pst_rate 				= rs("pst_rate")
							hst_rate 				= rs("hst_rate")
							order_current_system	= rs("current_system")
							order_price_unit		= rs("price_unit")
						end if
						rs.close : set rs = nothing
						
						Dim first_name, last_name, pay_method, pay_method_2, view_shipping_name, view_shipping_city, view_shipping_phone
						Dim view_shipping_address
						Dim view_shipping_state
						Dim view_shipping_zip_code
						Dim view_billing_name
						Dim view_billing_city
						Dim view_billing_state_code
						Dim view_billing_address
						Dim view_billing_phone
						Dim view_billing_zip_code
						Dim view_billing_country
						dIM customer_shipping_state
						'Dim my_purchase_order
						DIM view_shipping_country
						'DIM email
						Set rs = conn.execute("Select * from tb_customer_store where order_code="& SQLquote(LAYOUT_ORDER_CODE) &" and customer_serial_no="& SQLquote(LAYOUT_CCID))
						'Response.write ("Select * from tb_customer_store where order_code="& SQLquote(LAYOUT_ORDER_CODE) &" and customer_serial_no="& SQLquote(LAYOUT_CCID))
						If not rs.eof then 
							first_name 		= rs("customer_first_name")			
							last_name 		= rs("customer_last_name")	
							pay_method		= rs("pay_method")
							pay_method_2 	= rs("pay_method")
			
							if( cstr(pay_method) = cstr(LAYOUT_PAY_METHOD_CARD)) then
								pay_method 	= rs("customer_card_type")
							else
								pay_method 	= GetPayMethodNew(pay_method)
							end if
			
							view_shipping_name 		= ucase(rs("customer_shipping_first_name")) & "&nbsp;" & ucase(rs("customer_shipping_last_name"))
							view_shipping_city 		= rs("customer_shipping_city")
							view_shipping_phone 	= rs("phone_d")
			
							if rs("phone_n") <> "" and rs("phone_n") <> rs("phone_d") then 
								view_shipping_phone 	= view_shipping_phone & "," & rs("phone_n")
							end if
							
							view_shipping_address 	= rs("customer_shipping_address")
							view_shipping_state 	= rs("shipping_state_code")
							customer_shipping_state = rs("customer_shipping_state")
							view_shipping_zip_code 	= rs("customer_shipping_zip_code")
							shipping_country_code 	= rs("shipping_country_code")
							'if(isnumeric(rs("customer_shipping_country"))) then
							view_shipping_country = rs("shipping_country_code")
							'end if
			
							view_billing_name 		= ucase(rs("customer_card_first_name")) & "&nbsp;"& ucase(rs("customer_card_last_name"))
							view_billing_city 		= rs("customer_card_city")
							view_billing_state_code = rs("customer_card_state_code")
							view_billing_address 	= rs("customer_card_billing_shipping_address")
							view_billing_phone 		= rs("phone_d")
							view_billing_zip_code 	= rs("customer_card_zip_code")
							
							if(isnumeric(rs("customer_card_country"))) then 
								view_billing_country = GetCountryName(rs("customer_card_country"))
							end if
					
							
							
							'session("order_email") = email			
		
						end if
						rs.close : set rs = nothing
						
						if cstr(SQLescape(pay_method_2)) <> cstr(LAYOUT_PAY_METHOD_CARD) then 
							style_view_billing_address = " style='display:none;' "
						end if
						if instr(LAYOUT_PAY_PICKUP_VALUE_s, pay_method_2)>0 then
							style_view_shipping_address = " style='display:none;' "
						end if
						'Response.write SQLescape(pay_method)
					%>
                		<table width="100%" height="670"  border="0" align="center" cellpadding="1" cellspacing="2" bgcolor="#FFFFFF" style="border:#8FC2E2 1px solid; ">
                                <tr>
                                  <td valign="top" style="border:#E3E3E3 1px solid; "><table width="100%"  border="0" cellspacing="0" 
                    
                    cellpadding="0">
                                    <tr>
                                      <td height="5" bgcolor="#E8E8E8">&nbsp;&nbsp;1. Delivery Options</td>
                                      <td width="17"><img src="/soft_img/app/shop_arrow_gray.gif" width="17" height="23"></td>
                                      <td bgcolor="#e8e8e8">&nbsp;&nbsp;2. Pay Methods</td>
                                      <td width="16"><img src="/soft_img/app/shop_arrow_gray.gif" width="17" height="23"></td>
                                      <td bgcolor="#E8E8E8">&nbsp;&nbsp;3. Personal Information</td>
                                      <td width="16"><img src="/soft_img/app/shop_arrow_gray_red.gif" width="16" height="23"></td>
                                      <td bgcolor="#FF6600">&nbsp;&nbsp;<span class="text_white"><strong>4. Submit</strong></span></td>
                                    </tr>
                                  </table>
                                  <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                      <td height="10"></td>
                                    </tr>
                                  </table>
                                    <div id="email_area">
                                      <table width="80%" height="30"  border="0" align="center" cellpadding="0" cellspacing="0">
                                      <tr>
                                        <td class="text_hui_11">&nbsp;
                                            
                                        </td>
                                      </tr>
                                      <tr>
                                        <td><span style="font-family:tahoma; letter-spacing:0px; font-size:12px; color: #4C4C4C; text-decoration: none;	line-height: 18px; "><strong>LU COMPUTERS ORDER FORM</strong></span></td>
                                      </tr>
                                    </table>
                                    <table width="80%"  border="0" align="center" cellpadding="2" cellspacing="0">
                                      <tr>
                                        <td width="50%" class="text_hui_11" >&nbsp;</td>
                                      <td width="50%" class="text_hui_11" >1875 Leslie Street, Unit 24 　 <br>
                     Toronto, Ontario, M3B 2M5 　 <br>
                    Tel: (866)999-7828 (416)446-7743</td>
                                      </tr>
                                      <tr>
                                        <td></td>
                                      <td>&nbsp;</td>
                                      </tr>
                                      <tr>
                                        <td><table width="100%"  border="0" cellspacing="0" cellpadding="2">
                                          <tr>
                                            <td width="50%" class="text_hui_11"><strong>Order Number:</strong></td>
                                          <td width="50%" class="text_hui_11"><%= LAYOUT_ORDER_CODE %></td>
                                          </tr>
                                          <tr>
                                            <td class="text_hui_11"><strong>Date: </strong></td>
                                          <td class="text_hui_11"><%= ConvertDate(date())%></td>
                                          </tr>
                                          <tr>
                                            <td class="text_hui_11"><strong>Customer Name:</strong></td>
                                          <td class="text_hui_11"><%= first_name %>&nbsp;<%= last_name %></td>
                                          </tr>
                                        </table>
                                         </td>
                                        <td><table width="100%"  border="0" cellspacing="0" cellpadding="2">
                                          <tr>
                                            <td width="50%" class="text_hui_11"><strong> Customer Number:</strong></td>
                                            <td width="50%" class="text_hui_11"><iframe src="/FilterCustomerCode.aspx?customer_id=<%=LAYOUT_CCID%>" frameborder="0" width="80" style="height:12px;" scrolling="no"></iframe></td>
                                          </tr>
                                          <tr>
                                            <td class="text_hui_11"><strong>Payment: </strong></td>
                                            <td class="text_hui_11"><%= pay_method%></td>
                                          </tr>
                                          <tr>
                                            <td class="text_hui_11"><strong></strong></td>
                                            <td class="text_hui_11"></td>
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
                                        <td width="50%">&nbsp;</td>
                                        <td width="50%">&nbsp;</td>
                                      </tr>
                                      <tr>
                                        <td class="text_hui_11" <%= style_view_billing_address %>><strong>Billing Address: </strong></td>
                                        <td class="text_hui_11" <%= style_view_shipping_address %>><strong>Shipping Address:</strong></td>
                                      </tr>
                                      <tr>
                                        <td class="text_hui_11" <%= style_view_billing_address %>>
                                          <%
                                        response.write (view_billing_name) &"<br/>"
                                        response.write ucase(view_billing_address) &"<br/>"
                                        response.write ucase(view_billing_city) &",&nbsp;&nbsp;" &  ucase(view_billing_state_code) & ",&nbsp;&nbsp;" & ucase(view_billing_zip_code)
                            
                                        %>
                                          <br>
                    Phone # <%= view_billing_phone %> </span></td>
                                        <td class="text_hui_11" <%= style_view_shipping_address %>><%
                                        response.write  (view_shipping_name) &"<br/>"
                                        response.write  ucase(view_shipping_address) &"<br/>"
                                        response.write  ucase(view_shipping_city) &",&nbsp;&nbsp;" & ucase(view_shipping_state) & ",&nbsp;&nbsp;" &ucase(view_shipping_zip_code )
                                        %>
                                          <br>
                    Phone # <%= view_shipping_phone %> </td>
                                      </tr>
                                      
                                       <%
                                            dim  amount, current_order_pay_method
											Dim category
                                            category = SQLquote(request("category"))
                                            if isnumeric(LAYOUT_ORDER_CODE) then
                                            
                                            set rs = conn.execute("select pay_method , prick_up_datetime1, prick_up_datetime2 from tb_order_helper  where order_code='"&LAYOUT_ORDER_CODE&"'")
                                                
                                            if not rs.eof then
                                            current_order_pay_method =  cstr(rs("pay_method"))
                                            if instr(LAYOUT_PAY_PICKUP_VALUE_s, "["& current_order_pay_method&"]") > 0 then 
                                       %>
                                       
                                       <tr>
                                        <td colspan="2">
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td ><%
                                                    response.write "YOUR&nbsp;PICK&nbsp;UP&nbsp;DATE&nbsp;&&nbsp;TIME:&nbsp;&nbsp;&nbsp;" 
                                                    if not rs.eof then response.write "<span style=""FONT: 11px/16Px; font-family:tahoma; COLOR: green; letter-spacing:0px"">" & ConvertDateHour(rs("prick_up_datetime1"))&"</span>"
                                            %>
                                            
                                        </td>
                                        <td><% if not rs.eof then response.write ("&nbsp;or &nbsp;&nbsp;") %></td>
                                        <td  class="text_hui_11"><span style="COLOR: green">
                                            <%
                                                if not rs.eof then response.write ConvertDateHour(rs("prick_up_datetime2"))
                                            %></span>
                                        </td>
                                        </tr>
                                        
                                        </table>
                                      </tr><% end if
                                            rs.close : set rs = nothing%>
                                      <% end if%>
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
                    
                                            dim current_single_price
                                            current_single_price=0
                                                if isnumeric(LAYOUT_ORDER_CODE) then
                                                n = 0
                                                    '
                                                    ' product parts
                                                    '
                                                    'set rs = conn.execute("select p.product_serial_no, c.product_name,cart_temp_Quantity, price from tb_cart_temp c inner join tb_product p on p.product_serial_no=c.product_serial_no where cart_temp_code='"&LAYOUT_ORDER_CODE&"' and is_noebook=0")
                                                    set rs = conn.execute("select p.product_serial_no, c.product_name,c.order_product_sum cart_temp_Quantity, c.order_product_price price, c.product_current_price_rate, c.order_product_sold,"&_
                    "case when product_store_sum >2 then 2 "&_
                    "when ltd_stock >2 then 2  "&_
                    "when product_store_sum + ltd_stock >2 then 2  "&_
                    "when product_store_sum  <=2 and product_store_sum >0 then 3 "&_
                    "when product_store_sum +ltd_stock <=2 and product_store_sum +ltd_stock >0 then 3 "&_
                    "when ltd_stock <=2 and ltd_stock >0 then 3 "&_
                    "when product_store_sum +ltd_stock =0 then 4 "&_
                    "when product_store_sum +ltd_stock <0 then 5 end as ltd_stock, c.prodType from tb_order_product c inner join tb_product p on p.product_serial_no=c.product_serial_no where order_code='"&LAYOUT_ORDER_CODE&"' order by product_type asc")
                                                    if not rs.eof then
                                                        
                                                    do while not rs.eof 
                                                        'if  instr( rate_pay_methods , "["&current_order_pay_method&"]") > 0  then 
                                                            current_single_price = rs("order_product_sold")
                                                        'else
                    '										current_single_price = rs("price")
                    '									end if
                                                        amount = cdbl(current_single_price) * cint(rs("cart_temp_Quantity"))
                                                        n = n +1
                                        %>
                                             <tr <%if n mod 2 = 0 then response.write " bgcolor=""#FFFFFF"" " else response.write " bgcolor=""#f2f2f2"""%>>
                                                    <td >&nbsp;<%
                                                          'if len(rs("product_serial_no")) = 8 then 
                                                          '	response.write "<a href=""view_print_system.asp?cmd=print&id="&rs("product_serial_no")&""" onClick=""return js_callpage(this.href)"">"&rs("product_name")&"</a>"
                                                         ' else
                                                            response.write rs("product_name") 
                                                           ' if rs("prodType") <> "New" then response.Write "&nbsp;" & rs("prodType")
                                                            response.Write FindPartStoreStatus_system_setting(rs("ltd_stock"))
                                                         ' end if
                                                         %></td>
                                                    <td width="9%" align="center" class="text_hui_11"><%=rs("cart_temp_Quantity")%></td>
                                                    <td width="11%" align="right" ><span style="FONT: 11px/16Px; font-family:tahoma; COLOR: #4C4C4C; letter-spacing:0px"><%= formatcurrency(current_single_price,2)%></span> </td>
                                                    <td width="9%" align="right" ><span style="FONT: 11px/16Px; font-family:tahoma; COLOR: #4C4C4C; letter-spacing:0px"><%= formatcurrency( amount,2)%></span> </td>
                                            </tr>
                                            <%
                                                    rs.movenext
                                                        loop
                                                    end if
                                                    rs.close :set rs = nothing
                                                    
                                                    
                                                    ' product noebooks
                                                    'set rs = conn.execute("select * from tb_cart_temp c inner join tb_product p on p.product_serial_no=c.product_serial_no where cart_temp_code='"&LAYOUT_ORDER_CODE&"' and is_noebook=1")
                    '								if not rs.eof then
                    '								do while not rs.eof 
                    '								amount = cdbl(rs("price")) * cint(rs("cart_temp_Quantity"))
                    '								n = n +1
                    '						%>
                                             <!--<tr <%'if n mod 2 = 0 then response.write " bgcolor=""#FFFFFF"" " else response.write " bgcolor=""#f2f2f2"""%>>
                    '										<td >&nbsp;<%
                    '											  'if len(rs("product_serial_no")) = 8 then 
                    '											  '	response.write "<a href=""view_print_system.asp?cmd=print&id="&rs("product_serial_no")&""" onClick=""return js_callpage(this.href)"">"&rs("product_name")&"</a>"
                    '											 ' else
                    '												response.write rs("product_name")
                    '											 ' end if
                    '											 %></td>
                    '										<td width="9%" align="center" class="text_hui_11"><%'=rs("cart_temp_Quantity")%></td>
                    '										<td width="11%" align="right" class="text_hui_11"><%'= formatcurrency(rs("price"),2)%></td>
                    '										<td width="9%" align="right" class="text_hui_11"><%'= formatcurrency( amount,2)%>&nbsp; </td>
                    '								</tr>-->
                    <%
                    '								rs.movenext
                    '									loop
                    '								end if
                    '								rs.close :set rs = nothing
                                                    '
                                                    ' product system 
                                                    '
                                                    set rs = conn.execute("select  c.product_serial_no, c.product_name,c.order_product_sum cart_temp_Quantity, c.order_product_price  ,c.product_current_price_rate, c.order_product_sold from tb_order_product c where order_code='"&LAYOUT_ORDER_CODE&"' and length(product_serial_no)=8")
                                                    if not rs.eof then
                                                        do while not rs.eof 
                                                        
                                                        'if  instr( rate_pay_methods , "["&current_order_pay_method&"]") > 0  then 
                                                            current_single_price = rs("order_product_sold")
                                                        'else
                    '										current_single_price = rs("order_product_price")
                    '									end if
                                                        'response.write current_single_price
                                                        'if isnumeric(current_single_price) then
                                                            amount = cdbl(current_single_price) * cint(rs("cart_temp_Quantity"))
                                                        'end if
                                                        n = n +1
                                            %>
                                                    <tr <%if n mod 2 = 0 then response.write " bgcolor=""#FFFFFF"" " else response.write " bgcolor=""#f2f2f2"""%>>
                                                            <td >&nbsp;<%
                                                                  if len(rs("product_serial_no")) = 8 then 
                                                                    response.write "<a href=""view_print_system.asp?cmd=print&id="&rs("product_serial_no")&""" onClick=""return js_callpage(this.href)"">"&rs("product_name")&"</a>"
                                                                  else
                                                                    response.write rs("product_name")
                                                                  end if
                                                                 %></td>
                                                            <td width="9%" align="center" class="text_hui_11"><%=rs("cart_temp_Quantity")%></td>
                                                            <td width="11%" align="right" ><span style="FONT: 11px/16Px; font-family:tahoma; COLOR: #4C4C4C; letter-spacing:0px"><%= formatcurrency(current_single_price,2)%></span></td>
                                                            <td width="9%" align="right"><span style="FONT: 11px/16Px; font-family:tahoma; COLOR: #4C4C4C; letter-spacing:0px"><%= formatcurrency( amount,2)%></span></td>
                                                    </tr>
                                            <tr>
                                                <td style="background:white;" colspan="4">
                                                    <%
                                                        'set crs = conn.execute("select p.product_name from tb_sp_tmp_detail sp inner join tb_product p on p.product_serial_no=sp.product_serial_no inner join tb_menu_child mc on mc.menu_child_serial_no=p.menu_child_serial_no where sys_tmp_code='"&rs("product_serial_no")&"' and (p.is_non=0 or p.product_name like '%onboard%') and p.tag=1 order by sp.product_order asc ")
                                                        set crs = conn.execute("select part_quantity,sp.product_name,pg.part_group_name, p.product_serial_no ,"&_
                    "case when product_store_sum >2 then 2 "&_
                    "when ltd_stock >2 then 2  "&_
                    "when product_store_sum + ltd_stock >2 then 2  "&_
                    "when product_store_sum  <=2 and product_store_sum >0 then 3 "&_
                    "when product_store_sum +ltd_stock <=2 and product_store_sum +ltd_stock >0 then 3 "&_
                    "when ltd_stock <=2 and ltd_stock >0 then 3 "&_
                    "when product_store_sum +ltd_stock =0 then 4 "&_
                    "when product_store_sum +ltd_stock <0 then 5 end as ltd_stock from tb_order_product_sys_detail sp inner join tb_product p on sp.product_serial_no=p.product_serial_no inner join tb_part_group pg on sp.part_group_id=pg.part_group_id inner join tb_product_category pc on pc.menu_child_serial_no=p.menu_child_serial_no where sp.sys_tmp_code="&rs("product_serial_no")&" and (p.is_non=0 or p.product_name like '%onboard%') and p.tag=1 order by   sp.product_order asc ")
                                                            if not crs.eof then 
                                                                response.write "<table style='margin-left:2em;list-style:decimal;width: 550px'>"
                                                                do while not crs.eof 
                                                                    
                                                                    response.write "<tr><td  class='system_detail_list'>"&crs("product_name")& FindPartStoreStatus_system_setting(crs("ltd_stock"))& "</td><td style='width: 20px;'>x "& crs("part_quantity") & "</td></tr>"
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
                                            <%end if%>
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
                                              
														if cstr(SQLescape(shipping_country_code)) = "US" then 
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
                                    <table width="98%" style="border-top:#327AB8 1px solid; "  border="0" align="center" cellpadding="20" cellspacing="0">
                                      <tr>
                                        <td class="text_hui_11" ><p>Sales are subject to LU Computers' sales terms and policies. </p><p>No credit for any items that can be replaced. Any returned products must be complete and unused. All returns must be in their original packing material and re-saleable condition. Credit will not be issued unless the conditions are met. Returns must be reported within 14 days and are subject to a 15% and $30 whichever higher restocking charge is. Software and consumable items cannot be returned for credit or replacement.</p><p>Warranty claimed items must be shipped /carried in at customer's cost. Returned shipment without a LU issued RMA (Return Merchandise Authorization) number will be rejected. Warranty does not cover services completed by an unauthorized third party</p> </td>
                                      </tr>
                                    </table></div>
                                    <table width="100%"  border="0" align="left" cellpadding="3" cellspacing="0">
                                      <tr>
                                        <td height="60" align="center"><table width="100%" border="0" cellspacing="0" cellpadding="0">
                                          <tr>
                                            <td height="40"></td>
                                          </tr>
                                        </table>
                                        <table width="45%"  border="0" cellpadding="3" cellspacing="0">
                                          <tr >
                                            <td align="center"><table id="__01" width="130" height="24" border="0" cellpadding="0" cellspacing="0" class="btn_table" onClick="window.location.href='<%= session("order_ok_return") %>';">
                                              <tr>
                                                <td width="28"><img src="/soft_img/app/arrow_left.gif" width="28" height="24" alt=""></td>
                                                <td align="center" class="btn_middle"><!--<a style="text-decoration: none; cursor: pointer;color:white" onclick="printCont();"><strong>Print</strong></a>--><a class="btn_img" onClick="window.location.href='<%= session("order_ok_return") %>';"><strong>Back</strong></a>
                                                </td>
                                                <td width="6"><img src="/soft_img/app/customer_bottom_04.gif" width="6" height="24" alt=""></td>
                                              </tr>
                                            </table></td>
                                          <td align="center"><table id="__01" width="190" height="24" border="0" cellpadding="0" cellspacing="0" class="btn_table" onclick="window.location.href='<%= LAYOUT_HOST_URL %>shopping_cheout_order_ok.asp';">
                                            <tr>
                                              <td width="28"><img src="/soft_img/app/arrow_right.gif" width="28" height="24" alt=""></td>
                                              <td align="center"  class="btn_middle"><a  href="<%= LAYOUT_HOST_URL %>shopping_cheout_order_ok.asp" class="btn_img"><strong>Submit My Order</strong></a></td>
                                              <td width="6"><img src="/soft_img/app/customer_bottom_04.gif" width="6" height="24" alt=""></td>
                                            </tr>
                                          </table></td>
                                          </tr>
                                        </table>
                                          <p>&nbsp;</p>
                                          <p>&nbsp;</p></td>
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

        <td valign="bottom" id="page_main_right_backgroundImg" style="border-left: 1px solid #8E9AA8"><img src="/soft_img/app/left_bt.gif" width="14" height="214"></td>
    </tr>
</table>

<!--#include virtual="site/inc/inc_bottom.asp"-->
<script type="text/javascript">
$().ready(function(){
	//$('#page_main_area').load('/site/inc/inc_default.asp');
	bindHoverBTNTable();
});
</script>