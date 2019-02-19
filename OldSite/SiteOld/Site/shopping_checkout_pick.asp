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
                    <span class='nav1'>Shopping</span>
                	<span class='nav1'>Check Out</span>
                </div>
                
            	<div id="page_main_area">
                	<%
						
						
						
						call ValidateUserSessionInfo(LAYOUT_CCID ,"closeConn") 
						Call ValidateOrder_Code("")
						
						Dim v_zip_code						:	v_zip_code					=	null
						Dim v_phone_d						:	v_phone_d					=	null
						Dim v_phone_n						:	v_phone_n					=	null
						Dim v_customer_email2				:	v_customer_email2			=	null
						Dim v_customer_first_name			:	v_customer_first_name		=	null
						Dim v_customer_last_name			:	v_customer_last_name		=	null
						Dim v_customer_shipping_city		:	v_customer_shipping_city	=	null
						Dim v_customer_shipping_state		:	v_customer_shipping_state	=	null
						Dim v_customer_shipping_address		:	v_customer_shipping_address	=	null
						Dim v_customer_card_issuer			:	v_customer_card_issuer			=	null
						Dim v_customer_shipping_country		:	v_customer_shipping_country	=	null
						Dim v_customer_shipping_zip_code	:	v_customer_shipping_zip_code=	null
						Dim v_customer_card_type
						Dim v_customer_card_phone
						Dim v_customer_card_billing_shipping_address
						Dim v_customer_card_city
						Dim v_customer_card_state
						Dim v_customer_card_country
						Dim v_customer_address1
						Dim v_customer_credit_card
						Dim v_customer_expiry
						Dim v_customer_card_zip_code
						set rs = conn.execute("select * from tb_customer where customer_serial_no="& SQLquote(LAYOUT_CCID))
						if not rs.eof then
						
							v_zip_code 					= rs("zip_code")
							v_phone_d 					= rs("phone_d")
							v_phone_n 					= rs("phone_n")
							v_customer_address1 		= rs("customer_address1")
							v_customer_card_country 	= rs("customer_card_country")
							v_customer_email2 			= rs("customer_email2")
							v_customer_credit_card 		= rs("customer_credit_card")
							v_customer_expiry 			= rs("customer_expiry")
							v_customer_card_state 		= rs("customer_card_state")
							v_customer_first_name 		= rs("customer_first_name")
							v_customer_last_name 		= rs("customer_last_name")
							v_customer_card_type 		= rs("customer_card_type")
							v_customer_card_phone 		= rs("customer_card_phone")
							v_customer_card_issuer 		= rs("customer_card_issuer")
							v_customer_card_billing_shipping_address = rs("customer_card_billing_shipping_address")
							v_customer_card_city 		= rs("customer_card_city")
							v_customer_card_state 		= rs("customer_card_state")
							v_customer_card_zip_code 	= rs("customer_card_zip_code")
							v_customer_card_country 	= rs("customer_card_country")
							
						end if
						rs.close : set rs = nothing
					%>
                	<table width="100%" height="670"  border="0" align="center" cellpadding="1" cellspacing="2" bgcolor="#FFFFFF" style="border:#8FC2E2 1px solid; ">
                            <tr>
                              <td valign="top" style="border:#E3E3E3 1px solid; "><table width="100%"  border="0" cellspacing="0" 
                
                cellpadding="0">
                                <tr>
                                  <td height="5" bgcolor="#E8E8E8">&nbsp;&nbsp;1. Delivery Options</td>
                                  <td width="17"><img src="/soft_img/app/shop_arrow_gray.gif" width="17" height="23"></td>
                                  <td bgcolor="#e8e8e8">&nbsp;&nbsp;2. Pay Methods</td>
                                  <td width="16"><img src="/soft_img/app/shop_arrow_gray_red.gif" width="16" height="23"></td>
                                  <td bgcolor="#FF6600">&nbsp;&nbsp;<strong><span class="text_white">3. Personal Information</span></strong></td>
                                  <td width="17"><img src="/soft_img/app/shop_arrow_red.gif" width="17" height="23"></td>
                                  <td bgcolor="#e8e8e8">&nbsp;&nbsp;4. Submit</td>
                                </tr>
                              </table>
                              <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                  <td height="10"></td>
                                </tr>
                              </table>
                                <table width="98%"  border="0" align="center" cellpadding="0" cellspacing="0">
                                  <tr>
                                    <td><strong>&nbsp;&nbsp;&nbsp;LOCAL PICK UP </strong></td>
                                    <td align="right"><strong>Please call us for any questions:</strong><br>
                                      Toll Free: 1 (866) 999-7828<br>
                                      Toronto Local: (416) 446-7828</td>
                                  </tr>
                                </table>
                                <table width="80%" height="30"  border="0" align="center" cellpadding="0" cellspacing="0">
                                  <tr>
                                    <td class="text_hui_11">&nbsp;</td>
                                  </tr>
                                  <tr>
                                    <td class="text_hui_11">We   				welcome our local Toronto customers to pick up your order from our store   				location. No shipping or handling fee will be applied.<br>
                                      <br>
                Credit card is required to confirm your order for processing. At   				the time of pick up, you may pay with cash, debit or credit   				card.  If paying by credit card at the time of pickup, your card   				must be presented to be swiped, and the card holder must present   				to sign.<br>
                <br>
                Please note that credit card payments at the time of pick up do   				not qualify for our special cash discount.<br>
                <br>
                Please note that we do not accept checks, money orders,   				cashier's checks or PayPal for pick up.</td>
                                  </tr>
                                  <tr>
                                    <td valign="bottom" class="text_hui_11">&nbsp;</td>
                                  </tr>
                                </table>
                                <table width="98%" style="border-bottom:#327AB8 1px solid; "  border="0" align="center" cellpadding="0" cellspacing="0">
                                  <tr>
                                    <td>&nbsp;</td>
                                  </tr>
                                </table>
                                <div id="text_area" style="display:none">
                                <table width="80%"  border="0" align="center" cellpadding="0" cellspacing="0">
                                  <tr>
                                    <td class="text_hui_11">&nbsp;</td>
                                  </tr>
                                  <tr>
                                    <td class="text_hui_11"><strong> CREDIT CARD HOLDER:</strong></td>
                                  </tr>
                                </table>
                                <table width="80%"  border="0" align="center" cellpadding="2" cellspacing="0">
                                  <tr>
                                    <td class="text_hui_11" width="26%">First Name:</td>
                                    <td>STEVEN</td>
                                  </tr>
                                  <tr>
                                    <td class="text_hui_11" width="26%">Last Name:</td>
                                    <td>YAO</td>
                                  </tr>
                                  <tr>
                                    <td class="text_hui_11">Card Billing / Shipping Address:</td>
                                    <td><span class="text_hui_11"> <font face="Tahoma"><span style="font-size: 8.5pt;">
                                      <!--input name="shipping_address" id="shipping_address" type="text" class="input" size="13" style="width:150px; " onchange="" value="25 DUNSDALE SQ" tabindex="14"-->
                                    </span></font></span><font face="Tahoma"><span style="font-size: 12px;">34563456</span></font> </td>
                                  </tr>
                                  <tr>
                                    <td class="text_hui_11">Country:</td>
                                    <td>Canada</td>
                                  </tr>
                                  <tr>
                                    <td class="text_hui_11">City:</td>
                                    <td valign="top">sdfgsdf</td>
                                  </tr>
                                  <tr>
                                    <td class="text_hui_11">State (Province)</td>
                                    <td><!-- <span class="text_hui_11" id="div_state2">
                                            <select name="shipping_state" id="shipping_state" class="input"  style="width:150px; " onchange="changeShippingState(this);" tabindex="19"><option value="-1">Please Select</option></select>
                                          </span>
                                        <span style="display:none">  <span id="shipping_state_alter" style="display:none; color: red">&#27492;&#27954;&#19982;&#35746;&#21333;&#25968;()&#25454;&#19981;&#21516;</span></span>	-->
                                      Alberta</td>
                                  </tr>
                                  <tr>
                                    <td class="text_hui_11">Zip (Post) Code</td>
                                    <td valign="top">2343</td>
                                  </tr>
                                </table>
                                <table width="80%"  border="0" align="center" cellpadding="0" cellspacing="0">
                                  <tr>
                                    <td class="text_hui_11">&nbsp;</td>
                                  </tr>
                                  <tr>
                                    <td class="text_hui_11"><strong> CONTACT INFORMATION:</strong></td>
                                  </tr>
                                </table>
                                <table width="80%"  border="0" align="center" cellpadding="2" cellspacing="0">
                                  <tr>
                                    <td class="text_hui_11" width="26%">Secondary Email Address:</td>
                                    <td valign="top">sunnypro@163.com</td>
                                  </tr>
                                  <tr>
                                    <td class="text_hui_11">Telephone 1:</td>
                                    <td><font face="Tahoma"> <span style="font-size: 12px;"> 555-777-8888</span></font></td>
                                  </tr>
                                  <tr>
                                    <td class="text_hui_11">Telephone 2: </td>
                                    <td><font face="Tahoma"> <span style="font-size: 12px;"> 555-777-8888</span></font></td>
                                  </tr>
                                  <tr>
                                    <td class="text_hui_11">&nbsp;</td>
                                    <td>&nbsp;</td>
                                  </tr>
                                  
                                  <tr>
                                    <td colspan="2" class="text_hui_11"><strong>CUSTOMER COMMENT / NOTE:</strong></td>
                                  </tr>
                                  <tr>
                                    <td class="text_hui_11" align="center">&nbsp;</td>
                                    <td >My Note is this.................</td>
                                  </tr>
                                  <tr>
                                    <td class="text_hui_11" align="center">&nbsp;</td>
                                    <td align="right" valign="top"><span class="text_hui_11" id="div_state3"><a href="#" style="color: blue;">Change</a> </span> <span id="shipping_state_alter3" style="display:none;"></span> </td>
                                  </tr>
                                </table>
                                </div>
                                <form name="form2" id="form2" method="post" action="<%=LAYOUT_HOST_URL %>shopping_checkout_pick_exec.asp">
                                  
                                  <table width="80%"  border="0" align="center" cellpadding="2" cellspacing="0">
                                  <tr>
                                    <td colspan="2" align="right" class="text_hui_11"><br><span class="STYLE2">Today is: <%= WeekdayName(Weekday(Date)) %>, <%= day(date) %>&nbsp;<%= monthname(month(date),false)%>, <%= year(date)%></span></td>
                                  </tr>
                                  <tr>
                                    <td colspan="2" class="text_hui_11"><strong>YOUR PICK UP DATE &amp; TIME: </strong></td>
                                    </tr>
                                  <tr>
                                    <td colspan="2" class="text_hui_11">You may pick up your item the same day or next day for light systems, within 2-5 days for heavy systems. Please feel free to contact us to arrange your pick up. <br>

                                      <br>
                                      Please select the date and time you prefer to pick up your order: </td>
                                    </tr>
                                  <tr>
                                    <td class="text_hui_11" colspan="2" valign="top" >
                                        <table width="250" align="center" >
                                            <tr>
                                                <td>Date</td>
                                                <td><select name="pick_up_dd_1" id="pick_up_dd_1" class="input9pt" style="width: 40px; height: 15; background-color: rgb(255, 255, 160)" size="1" tabindex="1">
                                                  <%
                                                        for i=1 to 31
                                                            response.write "<option value='"& i &"' "
                                                            'if ( i = day(date())) then response.write " selected=""true"""
                                                            response.write " >"& i &"</option>"
                                                            
                                                        next
                                                    %>
                                              </select>								</td>
                                                <td>Month								</td>
                                                <td><select name="pick_up_Month_1" id="pick_up_Month_1" class="input9pt" style="width: 40px; height: 15; background-color: rgb(255, 255, 160)" size="1" tabindex="2">
                                                  <%
                                                        for i=1 to 12
                                                            response.write "<option value='"& i &"' "
                                                            'if ( i = month(date())) then response.write " selected=""true"""
                                                            response.write " >"& i &"</option>"
                                                        next
                                                    %>
                                              </select>								</td>
                                                <td>Time</td>
                                                <td><select name="pick_up_hh_1" id="pick_up_hh_1" class="input9pt" style="width: 40px; height: 15; background-color: rgb(255, 255, 160)" size="1" tabindex="3">
                                    <%
                                                        for i=11 to 19
                                                            response.write "<option value='"& i &"'>"& i &"</option>"
                                                            
                                                        next
                                                    %>
                                  </select></td>
                                            </tr>
                                          </table>                      </td>
                                  </tr>
                                 
                                  <tr>
                                    <td colspan="2" class="text_hui_11">If we are unable to make your item ready by that time, please allow another date and time:</td>
                                    </tr>
                                  <tr>
                                    <td class="text_hui_11" valign="top" colspan="2">
                                        <table width="250" align="center" >
                                            <tr>
                                                <td>Date</td>
                                                <td>  <select name="pick_up_dd_2" id="pick_up_dd_2" class="input9pt" style="width: 40px; height: 15; background-color: rgb(255, 255, 160)" size="1" tabindex="4">
                                                  <%
                                                        for i=1 to 31
                                                            response.write "<option value='"& i &"' "
                                                            'if ( i = day(date())) then response.write " selected=""true"""
                                                            response.write " >"& i &"</option>"
                                                        next
                                                    %>
                                              </select></td>
                                                <td>
                                                    Month</td>
                                                <td><select name="pick_up_Month_2" id="pick_up_Month_2" class="input9pt" style="width: 40px; height: 15; background-color: rgb(255, 255, 160)" size="1" tabindex="5">
                                                  <%
                                                        for i=1 to 12
                                                            response.write "<option value='"& i &"' "
                                                            'if ( i = month(date())) then response.write " selected=""true"""
                                                            response.write " >"& i &"</option>"
                                                        next
                                                    %>
                                              </select></td>
                                                <td>Time</td>
                                                <td> <select name="pick_up_hh_2" id="pick_up_hh_2" class="input9pt" style="width: 40px; height: 15; background-color: rgb(255, 255, 160)" size="1" tabindex="6">
                                                  <%
                                                        for i=11 to 19
                                                            response.write "<option value='"& i &"'>"& i &"</option>"
                                                            
                                                        next
                                                    %>
                                              </select></td>
                                            </tr>
                                          </table>						</td>
                
                                  </tr>
                                 
                                  <tr>
                                    <td colspan="2" class="text_hui_11"><blockquote>
                                      <p>We will contact you to confirm your pick up. Please refer to<a  href="/site/view_contact_us.asp" onClick="return js_callpage_cus(this.href, 'view_info', 620, 600)"> <span class="text_orange_11">information</span></a> of our business hours, address and direction for your pick up.</p>
                                    </blockquote></td>
                                    </tr>
                                  <tr>
                                    <td class="text_hui_11">&nbsp;</td>
                                    <td valign="top">&nbsp;</td>
                                  </tr>
                                  <tr>
                                    <td colspan="2" class="text_hui_11"><strong>CREDIT CARD (for deposit purpose only):</strong></td>
                                    </tr>
                                 <tr>
                                          <td width="26%" class="text_hui_11">Card Type:</td>
                                          <td><select name="card_type" id="card_type" class="input9pt" style="width:150px; " tabindex="7">
                                          <option value="0">Please select</option>
                                              <option value="MC" <% if v_customer_card_type ="MC" then response.write "selected" %>>Master Card</option>
                                              <option value="VS" <% if v_customer_card_type ="VS" then response.write "selected" %>>Visa</option>
                                        </select></td>
                                      </tr>
                                        <tr>
                                          <td class="text_hui_11">Card Number:</td>
                                          <td valign="top"><span class="text_hui_11">
                                            <input name="card_number" id="card_number" type="text" class="input9pt" size="13" style="width:150px; " onChange="changeStyle(this);return changeCardNumber(this);" value="<%'= v_customer_credit_card %>" tabindex="8">(Do not enter space.)
                
                                          </span></td>
                                        </tr>
                                        <tr>
                                          <td class="text_hui_11">Card Expiry Date:</td>
                                          <td>Month:
                                                <%
                                                
                                                %>
                                              <select name="card_expiry_month" class="input9pt" tabindex="9" style="width:60px">
                                                <option value="-1"></option>
                                                <%
                                                    dim x
                                                    for i=1 to 12
                                                        x = right("00" & i, 2)
                                                        response.write "<option value='"&x&"' "
                                                        if len(v_customer_expiry) > 3 then 
                                                            'if left(v_customer_expiry, 2) = x then response.write " selected='true'"
                                                        end if
                                                        response.write " >"&x&"</option>"
                                                    next
                                                %>
                                              </select>
                        Year:
                        
                                            <select name="card_expiry_year" class="input9pt"  tabindex="10" style="width:60px">
                                            <option value="-1"></option>
                                                <%
                                                    for i=2009 to 2020
                                                        response.write "<option value='"&i&"' "
                                                        if len(v_customer_expiry) > 4 then 
                                                            'if right(v_customer_expiry, 4) = cstr(i) then response.write " selected='true'"
                                                        end if
                                                        response.write ">"&i&"</option>"
                                                    next
                                                %>
                                          </select>		</td>
                                        </tr>
                                        <tr>
                                          <td class="text_hui_11">Card Issuer:<br></td>
                                          <td valign="top"><span class="text_hui_11">
                                            <input name="card_issuer" type="text" class="input9pt" size="13" style="width:150px; " value="<%'= v_customer_card_issuer%>" tabindex="11">
                                          </span></td>
                                        </tr>
                                        <tr>
                                          <td class="text_hui_11">Card Issuer's Telephone Number:<br>                            </td>
                                          <td valign="top"><span class="text_hui_11">
                                            <input name="card_issuer_telephone" type="text" class="input9pt" size="13" style="width:150px; "  value="<%'= v_customer_card_phone%>" tabindex="12">
                                            <%= LAYOUT_PHONE_FORMAT %><br>
                                            (It is on the back of your card)</span></td>
                                        </tr>
                                </table>                
                                <table width="80%"  border="0" align="center" cellpadding="0" cellspacing="0">
                                  <tr>
                                    <td class="text_hui_11">&nbsp;</td>
                                  </tr>
                                  <tr>
                                    <td class="text_hui_11"><strong> CREDIT CARD HOLDER:</strong></td>
                                  </tr>
                                </table>                <table width="80%"  border="0" align="center" cellpadding="2" cellspacing="0">
                                  <tr>
                                          <td class="text_hui_11" width="26%">First Name:</td>
                                          <td valign="top"><span class="text_hui_11">
                                             <input name="customer_first_name" type="text" class="input9pt" size="13" style="width:150px; " value="<%=v_customer_first_name%>" tabindex="13">
                                          </span></td>
                                      </tr>
                                         <tr>
                                          <td class="text_hui_11" width="26%">Last Name:</td>
                                          <td valign="top"><span class="text_hui_11">
                                             <input name="customer_last_name" type="text" class="input9pt" size="13" style="width:150px; " value="<%=v_customer_last_name%>" tabindex="13">
                                          </span></td>
                                      </tr>
                                        <tr>
                                          <td class="text_hui_11">Card Billing / Shipping Address:</td>
                                          <td valign="top"><span class="text_hui_11">
                                            <textarea name="card_billing_shipping_address" id="card_billing_shipping_address" rows="2" cols="40" tabindex="14"  style="font-family:Tahoma; font-size:9pt;color:#333333" ><%= v_customer_card_billing_shipping_address %></textarea>
                                            
                                          </span></td>
                                        </tr>
                                        <tr>
                                          <td class="text_hui_11">Country:</td>
                                          <td valign="top">
                                          <select name="card_country" id="card_country" class="input9pt"  tabindex="15"  onChange="changeStyle(this);changeState(this,'div_state',  'shipping_state_div_ca', 'shipping_state_div_us', 'card_state');">
                            <option value="0">Select Your Country</option>
                                                            <%
                                                                set rs = conn.execute("select * from tb_country where id=1 limit 0,2")
                                                                if not rs.eof then 
                                                                    do while not rs.eof 
                                                            %>
                                                             <option value="<%= rs("id") %>" 
                                                             <% 
                                                             if isnumeric(v_customer_card_country) then 
                                                                if cstr(v_customer_card_country) = cstr(rs("id")) then response.write " selected=""true""" 
                                                             end if
                                                                %>
                                                             ><%= rs("name") %></option>
                                                            <%
                                                                    rs.movenext
                                                                    loop
                                                                end if 
                                                                rs.close : set rs = nothing
                                                            %>
                                          </select>						  </td>
                                        </tr>
                                        <tr>
                                          <td class="text_hui_11">City:</td>
                                          <td valign="top"><span class="text_hui_11">
                                            <input name="card_city" id="card_city" type="text" class="input9pt" size="13" style="width:150px; " value="<%= v_customer_card_city%>" tabindex="16">
                                          </span></td>
                                        </tr>
                                        <tr>
                                          <td class="text_hui_11">State (Province)</td>
                                          <td valign="top"><span class="text_hui_11" id="div_state">
                                          
                                            <select name="card_state" id="card_state" class="input9pt"style="width:150px; " tabindex="17">
                                              <option value="-1">Please Select</option>
                                            <%
                                                if isnumeric(v_customer_card_country) then 
                                                    set rs = conn.execute("select state_serial_no,state_name from tb_state_shipping where system_category_serial_no=" & v_customer_card_country)
                                                    if not rs.eof then
                                                        
                                                        do while not rs.eof 
                                                            response.Write("<option value='"&rs("state_serial_no")&"' ")
                                                            if isnumeric(v_customer_card_state) then 
                                                                if cint(v_customer_card_state = rs("state_serial_no"))  then
                                                                     response.write " selected=""true"" "
                                                                end if
                                                            end if 
                                                            response.write (" >"&rs("state_name")&"</option>")
                                                        rs.movenext
                                                        loop
                                                    end if
                                                    rs.close :set rs  = nothing
                                                
                                                end if
                                            %>
                                            </select>
                                          </span> <span id="shipping_state_alter" style="display:none;"></span> </td>
                                        </tr>
                                        <tr>
                                          <td class="text_hui_11">Zip (Post) Code</td>
                                          <td valign="top"><span class="text_hui_11">
                                            <input name="card_zip_code" type="text" class="input9pt" id="card_zip_code" style="width:150px; " size="13" maxlength="7" value="<%= v_customer_card_zip_code %>" tabindex="18">
                                          </span></td>
                                        </tr>
                                </table>                
                                <table width="80%"  border="0" align="center" cellpadding="0" cellspacing="0">
                                  <tr>
                                    <td class="text_hui_11">&nbsp;</td>
                                  </tr>
                                  <tr>
                                    <td class="text_hui_11"><strong> CONTACT INFORMATION:</strong></td>
                                  </tr>
                                </table>
                                <table width="80%"  border="0" align="center" cellpadding="2" cellspacing="0">
                                   <tr>
                                          <td class="text_hui_11" width="26%">Secondary Email Address:</td>
                                          <td valign="top"><span class="text_hui_11">
                                            <input name="secondary_email" type="text" class="input9pt" size="13" style="width:150px; " value="<%= v_customer_email2 %>" tabindex="19">
                                          </span></td>
                                      </tr>
                                        <tr>
                                          <td class="text_hui_11">Business Phone:<span class="STYLE1">*</span></td>
                                          <td><span class="text_hui_11">
                                            <input name="phone_d" type="text" class="input9pt" size="13" style="width:150px; "  value="<%= v_phone_d%>" tabindex="20">
                                            <%= LAYOUT_PHONE_FORMAT %></span></td>
                                        </tr>
                                        <tr>
                                          <td class="text_hui_11">Home Phone: </td>
                                          <td><span class="text_hui_11">
                                            <input name="phone_n" type="text" class="input9pt" size="13" style="width:150px; "  value="<%= v_phone_n%>" tabindex="21">
                                            <%= LAYOUT_PHONE_FORMAT %></span></td>
                                        </tr>
                                        <tr>
                                          <td class="text_hui_11">&nbsp;</td>
                                          <td>&nbsp;</td>
                                        </tr>
                                        
                                        <tr>
                                          <td colspan="2" class="text_hui_11"><strong>CUSTOMER COMMENT / NOTE:</strong></td>
                                      </tr>
                                        <tr>
                                          <td colspan="2" align="center" class="text_hui_11">
                                            <textarea name="note" cols="40" rows="5"  style="font-family:Tahoma; font-size:9pt;color:#333333" tabindex="23"></textarea></td>
                                      </tr>
                                </table>                <table width="98%" style="border-bottom:#327AB8 1px solid; "  border="0" align="center" cellpadding="0" cellspacing="0">
                                  <tr>
                                    <td>&nbsp;</td>
                                  </tr>
                                </table>
                                </form>
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                  <tr>
                                    <td height="40"></td>
                                  </tr>
                                </table>
                                <table width="100%"  border="0" align="left" cellpadding="3" cellspacing="0">
                                  <tr>
                                    <td height="60" align="center"><table width="45%"  border="0" cellpadding="3" cellspacing="0">
                                      <tr align="right">
                                        <td><table id="__01" width="130" height="24" border="0" cellpadding="0" cellspacing="0" class="btn_table" onclick="gotoBackCart('<%= LAYOUT_CURRENT_ORDER_TYPE %>');" >
                                            <tr>
                                              <td width="28"><img src="/soft_img/app/arrow_left.gif" width="28" height="24" alt=""></td>
                                              <td align="center" class="btn_middle"><a class="btn_img" onclick="gotoBackCart('<%= LAYOUT_CURRENT_ORDER_TYPE %>');"  tabindex="24"><strong>Back</strong></a> </td>
                                              <td width="6"><img src="/soft_img/app/customer_bottom_04.gif" width="6" height="24" alt=""></td>
                                            </tr>
                                        </table></td>
                                        <td><table id="__01" width="130" height="24" border="0" cellpadding="0" cellspacing="0" class="btn_table" onClick="Check(document.getElementById('form2'));">
                                            <tr>
                                              <td width="28"><img src="/soft_img/app/arrow_right.gif" width="28" height="24" alt=""></td>
                                              <td align="center" class="btn_middle"><a class="btn_img" onClick="Check(document.getElementById('form2'));" tabindex="25" ><strong>Next</strong></a></td>
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
                          <table width="100%"  border="0" align="center" cellpadding="0" cellspacing="0">
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
	if("<%= LAYOUT_SHIPPING_STATE_CODE %>" == "<%= v_customer_shipping_state %>" && "<%= v_customer_shipping_country %>" == "<%= LAYOUT_SHIPPING_STATE_CODE %>" )
	{
		$('#text_area').css("display", "");
		$('#form_area').css("display", "none");
	}
	else
	{
		$('#text_area').css("display", "none");
		$('#form_area').css("display", "");
	}
	bindHoverBTNTable();
});

function Check(the)
{
	var err = "Please fill up all missing items.";
	
	if($('#phone_d').val() == '')
	{
		alert(err);
		$('#phone_d').focus();		
		return false;
	}
	$('#form2').submit();
}
</script>