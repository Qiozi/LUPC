<!--#include virtual="site/inc/inc_page_top.asp"-->
<% 
'----------------------------------------------------------------------
'PayPal Express Checkout Example Step 2
'================================================
'Display the resulting authorization details from
'PayPal on a PayPal payment, and complete the
'payment authorization.  This script would be called
'when the buyer returns from PayPal and has authorized
'the payment
'----------------------------------------------------------------------
	'On Error Resume Next
	Dim resArray
	Dim reqArray
	Dim message
	Dim customerID
	Dim token
	Dim currCodeType
	Dim paymentAmount
	Dim paymentType
	Dim payer_id
	Dim final_url
	Dim rate_pay_methods
	Dim pay_pal_value
	Dim first_last
	Dim first_name
	Dim last_name
	Dim email
	Dim is_exist_user
	'Dim GetOrderRateByCart
	
	Set resArray	= SESSION("nvpResArray")
'----------------------------------------------------------------------
'Collect the necessary information to complete the
'authorization for the PayPal payment
'----------------------------------------------------------------------
	token			= SESSION("token")
	currCodeType	= SESSION("currencyCodeType")
	paymentAmount	= SESSION("paymentAmount")
	paymentType		= SESSION("PaymentType")
	payer_id		= SESSION("PayerID")
	email			= resArray("EMAIL")
	'response.write payer_id &"|"& email

'----------------------------------------------------------------------
 'Set the final URL to complete the authorization.  The
 'link will be displayed at the bottom the the browser for this
 'example.  The link would normally be displayed at the end of your checkout
 'and would finalize payment when clicked.
 '----------------------------------------------------------------------
	final_url		="checkout_paypal_website_payments_pro_doExpressCheckoutPayment.asp?token=" & token &_
					"&payerID="& payer_id &_ 
					"&paymentAmount="& paymentAmount &_ 
					"&currCodeType="& currCodeType &_ 
					"&paymentType=" &paymentType

	message			= "Get Express checkout Details!"
'----------------------------------------------------------------------
'Display the API request and API response back to the browser using Diaplay.asp.
'If the response from PayPal was a success, display the response parameters
'If the response was an error, display the errors received
'----------------------------------------------------------------------



'
'
'	save message to LUWeb
'
'
'customerID = LAYOUT_CCID
'if customerID = "" then SessionLost("closeConn")

dim state_id, state_code
set rs = conn.execute("select state_serial_no, state_code from tb_state_shipping where state_code='"&resArray("SHIPTOSTATE")&"' or state_name='"&resArray("SHIPTOSTATE")&"'")
if not rs.eof then
	state_id = rs(0)
	state_code = rs(1)
end if
rs.close : set rs = nothing


'
' if user is exist. then update info. else create
'
Set rs = conn.execute("Select count(customer_serial_no) from tb_customer  where customer_login_name='" & resArray("EMAIL") &"' ")
if not rs.eof then
	if rs(0) >0 then
		is_exist_user = true
	else
		is_exist_user = false
	End if
	
else
	is_exist_user = false
End if
rs.close : set rs = nothing


		set rs = server.createobject("adodb.recordset")
		
		if LAYOUT_CCID = "" then 
			if is_exist_user then 
				rs.open "select * from tb_customer where customer_login_name="& SQLquote(resArray("EMAIL")), conn, 1,3		
			else
				rs.open "select * from tb_customer ", conn, 1,3
				rs.addnew
				rs("Customer_serial_no") = GetNewCustomerCode()
				rs("create_datetime")=now()
			End if
		else
			rs.open "select * from tb_customer where customer_serial_no="& SQLquote(LAYOUT_CCID), conn, 1,3	
		end if
		
		rs("customer_shipping_address") = resArray("SHIPTOSTREET") & resArray("SHIPTOSTREET2")
		rs("customer_shipping_city") = resArray("SHIPTOCITY")
		rs("phone_d") = resArray("PHONENUM")
		rs("customer_shipping_state") = state_id
		rs("shipping_state_code") = state_code
		rs("customer_shipping_zip_code") = resArray("SHIPTOZIP")
		rs("customer_first_name") = resArray("FIRSTNAME")
		rs("customer_last_name")  = resArray("LASTNAME")
'		if(email <> "" ) then 
'			rs("customer_email1") = email
'		end if
		
		rs("customer_email1") = resArray("EMAIL")
		
		first_last = resArray("SHIPTONAME")
		if instr(first_last, " ")>0 then 
			rs("customer_shipping_first_name") = mid(first_last, 1, instr(first_last, " "))
			rs("customer_shipping_last_name") = replace(first_last, rs("customer_shipping_first_name"), "")
		else
			rs("customer_shipping_first_name") = resArray("SHIPTONAME")
		end if
		if resArray("SHIPTOCOUNTRYNAME") = "Canada" or resArray("SHIPTOCOUNTRYNAME") = "CA" then 
			rs("customer_shipping_country") = 1
			rs("shipping_country_code") = "CA"
		end if
		if resArray("SHIPTOCOUNTRYNAME") = "United States" or resArray("SHIPTOCOUNTRYNAME") = "US" then 
			rs("customer_shipping_country") = 2
			rs("shipping_country_code") = "US"
		end if
		first_name = rs("customer_shipping_first_name")
		last_name = rs("customer_shipping_last_name")
		
		if not is_exist_user then 
			rs("customer_country_code") = rs("shipping_country_code")
			rs("state_code")			= rs("shipping_state_code")	
			rs("customer_login_name")	= resArray("EMAIL")
			rs("EBay_ID")				= resArray("EMAIL")
			rs("customer_address1")		= rs("customer_shipping_address")
			rs("customer_city")			= rs("customer_shipping_city")
			rs("customer_country_code") = rs("shipping_country_code")
			rs("zip_code")				= rs("customer_shipping_zip_code")

		End if
		rs("pay_method")			=	LAYOUT_PAYPAL_METHOD_CARD

		rs.update()
		rs.close : set rs = Nothing
		

Set rs = conn.execute("Select * from tb_customer where customer_login_name="& SQLquote(resArray("EMAIL")))
if not rs.eof then
	'response.Cookies("customer_serial_no") = rs(0)
			LAYOUT_CCID	=	rs("customer_serial_no")	
			response.Cookies("customer_serial_no") = rs("customer_serial_no")
			Session("user_id") 			= rs("customer_serial_no")
			session("user") 			= rs("customer_login_name")
			session("Email") 			= rs("customer_email1")
			response.Cookies("customer_first_name") 	= ucase(rs("customer_first_name") )
			session("customer_last_name") = ucase(rs("customer_last_name"))

end if
'response.Write(resArray("SHIPTOCOUNTRYNAME"))
'response.End()

Call ValidateOrder_Code("")

 if not Session("IsExistOrder") = true then 
	Call CopyCartToOrder(LAYOUT_ORDER_CODE, LAYOUT_CCID, "")
end if


%>
<!-- #include virtual ="/paypal/CallerService.asp" -->


<table class="page_main" cellpadding="0" cellspacing="0">
	<tr>
    	<td  id="page_main_left" valign="top" style="padding-bottom:5px; padding-left: 2px"  class='page_frame'>
        	<!-- left begin -->
            	<!--#include virtual="site/inc/inc_left_menu.asp"-->
            <!-- left end 	-->
    	</td>
        <td id="page_main_center" valign="top" class='page_frame'>
        	<!-- main begin -->
        	    <div id="page_main_banner"></div>
        	    <div class="page_main_nav" id="page_main_nav"><span class="nav1">Home</span><span class="nav1">Paypal</span></div>
            	<div id="page_main_area">
                	<table width="600" height="670"  border="0" align="center" cellpadding="1" cellspacing="2" bgcolor="#FFFFFF" style="border:#8FC2E2 1px solid; ">
                          <tr>
                            <td valign="top" style="border:#E3E3E3 1px solid; ">
                                        <center>
                                    <form action='<%=final_url%>'>
                                                <table  width="100%" style="border-bottom:#327AB8 1px solid; ">
                                                    <tr>
                                                        <td width="204"><div align="right" class="text_hui_11">Order Total:</div></td>
                                                        <td  class="text_hui_11">&nbsp;&nbsp;<%= currCodeType %>&nbsp;$<%=paymentAmount%></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <div align="right"  class="text_hui_11">first name:</div></td>
                                                        <td>&nbsp;&nbsp; <%=first_name%></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <div align="right"  class="text_hui_11">last name:</div></td>
                                                        <td>&nbsp;&nbsp; <%=last_name%></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <div align="right"  class="text_hui_11">Street 1:</div></td>
                                                        <td>&nbsp;&nbsp; <%=resArray("SHIPTOSTREET")%></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <div align="right" class="text_hui_11">Street 2:</div></td>
                                                        <td>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <div align="right" class="text_hui_11">City:</div></td>
                                                        <td class="text_hui_11">&nbsp;&nbsp;<%=resArray("SHIPTOCITY")%></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <div align="right" class="text_hui_11">State:</div></td>
                                                        <td class="text_hui_11">&nbsp;&nbsp;<%=resArray("SHIPTOSTATE")%></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <div align="right" class="text_hui_11">Postal code:</div></td>
                                                        <td class="text_hui_11">&nbsp;&nbsp;<%=resArray("SHIPTOZIP")%></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="text_hui_11">
                                                            <div align="right" class="text_hui_11">Country:</div></td>
                                                        <td class="text_hui_11">&nbsp;&nbsp;<%=resArray("SHIPTOCOUNTRYNAME")%></td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" class="header">
                                                        <center>
                                                            <br>
                                                                <input type="submit" value="Pay">
                                                            <!--<input type="image" name="submit" src="https://www.paypal.com/en_US/i/btn/btn_xpressCheckout.gif" />-->
                                                        </center>
                                                        </td>
                                                    </tr>
                                                </table>
                                    </form>
                                </center>

                     		<table width="98%"  border="0" align="center" cellpadding="0" cellspacing="0">
                              <tr>
                                <td>&nbsp;</td>
                                <td align="right"><strong>Please call us for any questions:</strong><br>
                                  Toll Free: (866) 999-7828<br>
                                  Toronto Local: (416) 446-7828</td>
                              </tr>
                            </table>
                            <table width="98%" style="border-bottom:#327AB8 1px solid; " height="30"  border="0" align="center" cellpadding="0" cellspacing="0">
                              <tr>
                                <td align="center"><strong>ORDER SUMMARY</strong></td>
                              </tr>
                            </table>
                            
                            <div id="order_charge_area">    
                            	  <%
						Dim sub_total, shipping_and_handling, 	sales_tax, grand_total, tax_rate,sur_charge
						Dim order_current_system
						Dim order_price_unit
						Dim amount
						Dim pay_method_2
						Dim shipping_country_code
						
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
						
						set rs = conn.execute("select gst, pst, hst, gst_rate, pst_rate, hst_rate,sub_total, total,shipping_charge,tax_charge, gst_rate+pst_rate+hst_rate tax, sub_total_rate, total_rate,sur_charge,grand_total, date_format(create_datetime, '%b/%d/%y') order_date, current_system, price_unit, pay_method from tb_order_helper where order_code='"&LAYOUT_ORDER_CODE&"' and customer_serial_no="& SQLquote(LAYOUT_CCID))
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
							pay_method_2			= rs("pay_method")
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

                                                    set rs = conn.execute("select p.product_serial_no, c.product_name,c.order_product_sum cart_temp_Quantity, c.order_product_price price, c.product_current_price_rate, c.order_product_sold,"&_
                    "case when product_store_sum >2 then 2 "&_
                    "when ltd_stock >2 then 2  "&_
                    "when product_store_sum + ltd_stock >2 then 2  "&_
                    "when product_store_sum  <=2 and product_store_sum >0 then 3 "&_
                    "when product_store_sum +ltd_stock <=2 and product_store_sum +ltd_stock >0 then 3 "&_
                    "when ltd_stock <=2 and ltd_stock >0 then 3 "&_
                    "when product_store_sum +ltd_stock =0 then 4 "&_
                    "when product_store_sum +ltd_stock <0 then 5 end as ltd_stock from tb_order_product c inner join tb_product p on p.product_serial_no=c.product_serial_no where order_code='"&LAYOUT_ORDER_CODE&"' order by product_type asc")
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
                                                            response.write rs("product_name")& FindPartStoreStatus_system_setting(rs("ltd_stock"))
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
                                                        set crs = conn.execute("select part_quantity,sp.product_name,sp.cate_name, p.product_serial_no "&_
                  " from tb_order_product_sys_detail sp inner join tb_product p on sp.product_serial_no=p.product_serial_no  where sp.sys_tmp_code="&rs("product_serial_no")&" and (p.is_non=0 or p.product_name like '%onboard%') and p.tag=1 order by   sp.product_order asc ")
					
                                                            if not crs.eof then 
                                                                response.write "<table style='margin-left:2em;list-style:decimal;width: 550px'>"
                                                                do while not crs.eof 
                                                                    
                                                                    response.write "<tr><td  class='system_detail_list'>"&crs("product_name")& "</td><td style='width: 20px;'>x "& crs("part_quantity") & "</td></tr>"
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
                                                          end if%></span><span class="price_unit"><%= order_price_unit %></span>
                                            </td>
                                      </tr>             <%if cint(gst_rate) > 0 then 
															   response.Write(" <tr><td class=""order_price_comment"">GST("&gst_rate&"%)</td><td style=""border-bottom:#eeeeee 1px solid; border-left:#eeeeee 1px solid; border-right:#eeeeee 1px solid;"" align=""right"" bgcolor=""#FFFFFF"" > <span>")
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
                            </div>
		<!--//					<script type="text/javascript">
//								$().ready(function(){
//										$('#order_charge_area').load();
//								});
//							</script>-->
						
                        <table width="600"  border="0" align="center" cellpadding="0" cellspacing="0">
                          <tr>
                            <td height="5"></td>
                          </tr>
                        </table>
                        <table width="600"  border="0" align="center" cellpadding="0" cellspacing="0">
                          <tr>
                            <td height="5"></td>
                          </tr>
                        </table>
                        <table width="600"  border="0" align="center" cellpadding="0" cellspacing="0">
                          <tr>
                            <td height="5"></td>
                          </tr>
                      </table>
                      </td>
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
<iframe src="blank.html" style="width:0px; height:0px;" frameborder="0" id="iframe1" name="iframe1"></iframe>
<script type="text/javascript">
    $().ready(function(){

        $('#page_main_area').css("border","1px solid #8FC2E2").css("background", "#ffffff").css("padding", "1px");
        //$('#page_main_area').load("/ebay/inc/inc_system_view.asp?id=<%= Request("id") %>");
		$('ul.row ul').css("width", "100%").css('line-height', '20px');
		$("li.comment").css("border", "0px solid red").css("width", "430px").css("text-align", "right").css("float", "left");		
		$("li[title=price]").css("border", "0px solid red").css("float", "left").css("width", "150px").css("text-align", "right");

    });
</script>

<% 

    If Err.Number <> 0 Then 
	SESSION("ErrorMessage")	= ErrorFormatter(Err.Description,Err.Number,Err.Source,"checkout_paypal_website_payments_pro_getExpressCheckoutDetail.asp")
	Response.Redirect "APIError.asp"
	Else
	SESSION("ErrorMessage")	= Null
	End If
	
	closeconn()
    %>

</body>
</html>

