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
						Call ValidateOrder_Code("ebay")
						
						Dim v_zip_code						:	v_zip_code					=	null
						Dim v_phone_d						:	v_phone_d					=	null
						Dim v_phone_n						:	v_phone_n					=	null
						Dim v_customer_email2				:	v_customer_email2			=	null
						Dim v_customer_shipping_first_name			:	v_customer_shipping_first_name		=	null
						Dim v_customer_shipping_last_name			:	v_customer_shipping_last_name		=	null
						Dim v_customer_shipping_city		:	v_customer_shipping_city	=	null
						Dim v_customer_shipping_state		:	v_customer_shipping_state	=	null
						Dim v_customer_shipping_address		:	v_customer_shipping_address	=	null
						Dim v_my_purchase_order				:	v_my_purchase_order			=	null
						Dim v_customer_shipping_country		:	v_customer_shipping_country	=	null
						Dim v_customer_shipping_zip_code	:	v_customer_shipping_zip_code=	null

						Dim v_customer_card_billing_shipping_address
						Dim v_customer_card_country
						DIm v_customer_card_city
						Dim v_customer_card_state
						Dim v_customer_card_zip_code
						Dim v_customer_card_first_name
						Dim v_customer_card_last_name
						Dim v_customer_card_country_code
						Dim v_customer_card_state_code
						set rs = conn.execute("select * from tb_customer where customer_serial_no="& LAYOUT_CCID)
						if not rs.eof then
						
							v_zip_code 						= rs("zip_code")
							v_phone_d 						= rs("phone_d")
							v_phone_n 						= rs("phone_n")
							v_customer_email2 				= rs("customer_email2")
							v_customer_shipping_state 		= rs("customer_shipping_state")
							v_customer_shipping_first_name 	= rs("customer_shipping_first_name")
							v_customer_shipping_last_name 	= rs("customer_shipping_last_name")
							v_customer_shipping_city 		= rs("customer_shipping_city")
							v_customer_shipping_state 		= rs("shipping_state_code")
							v_customer_shipping_address 	= rs("customer_shipping_address")
							v_my_purchase_order 			= rs("my_purchase_order")
							v_customer_shipping_country 	= rs("shipping_country_code")
							v_customer_shipping_zip_code 	= rs("customer_shipping_zip_code")

							v_customer_card_first_name		=	rs("customer_card_first_name")
							v_customer_card_last_name		=	rs("customer_card_last_name")
							v_customer_card_country_code	=	rs("customer_card_country_code")
							v_customer_card_billing_shipping_address=rs("customer_card_billing_shipping_address")
							v_customer_card_state_code		=	rs("customer_card_state_code")
							v_customer_card_city			=	rs("customer_card_city")
							v_customer_card_zip_code		= 	rs("customer_card_zip_code")
						end if
						rs.close : set rs = nothing
					%>
                	<table style="border: 1px solid rgb(143, 194, 226);" align="center" bgcolor="#ffffff" border="0" cellpadding="1" cellspacing="2" height="670" width="600">
            
                          <tr>
                            <td style="border: 1px solid rgb(227, 227, 227);" valign="top"><table width="100%"  border="0" cellspacing="0" 
            
            cellpadding="0">
                              <tr>
                                <td height="5" bgcolor="#E8E8E8">&nbsp;&nbsp;1. Delivery Options</td>
                                <td width="17"><img src="/soft_img/app/shop_arrow_gray.gif" width="17" height="23"></td>
                                <td bgcolor="#e8e8e8">&nbsp;&nbsp;2. Pay Methods</td>
                                <td width="16"><img src="/soft_img/app/shop_arrow_gray_red.gif" width="16" height="23"></td>
                                <td bgcolor="#FF6600">&nbsp;&nbsp;<span class="text_white"><strong>3. Personal Information</strong></span></td>
                                <td width="17"><img src="/soft_img/app/shop_arrow_red.gif" width="17" height="23"></td>
                                <td bgcolor="#e8e8e8">&nbsp;&nbsp;4. Submit</td>
                              </tr>
                            </table>
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                
                                  <tr>
                                    <td height="10"></td>
                                  </tr>
                                
                            </table>
                                <table align="center" border="0" cellpadding="0" cellspacing="0" width="98%">
                                  
                                    <tr>
                                      <td><font style="font-size: 8.5pt;" face="Tahoma">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <img src="/soft_img/app/payments.gif" height="34"></font></td>
                                      <td align="right"><font style="font-size: 8.5pt;" face="Tahoma"> <strong>Please call us for any questions:</strong><br>
                                        Toll Free: 1 (866) 999-7828<br>
                                        Toronto Local: (416) 446-7828</font></td>
                                    </tr>
                                  
                                </table>
                              <table align="center" border="0" cellpadding="0" cellspacing="0" height="30" width="86%">
                                  
                                    <tr>
                                      <td class="text_hui_11">&nbsp;</td>
                                    </tr>
                                    <tr>
                                      <td class="text_hui_11"><font face="Tahoma"><strong> <font size="2">PAYING BY </font></strong></font><strong> <font face="Tahoma" size="2">CREDIT CARD</font></strong></td>
                                    </tr>
                                    <tr>
                                      <td class="text_hui_11"><font style="font-size: 8.5pt;" face="Tahoma">&nbsp; <br>
                                        We accept VISA and Master Card only. We accept US and Canadian bank issuing credit cards only.</font></td>
                                    </tr>
                                    <tr>
                                      <td class="text_hui_11" height="25" valign="bottom"><font style="font-size: 8.5pt;" face="Tahoma"><strong>NOTE:</strong> All items with *&nbsp; below are required. </font></td>
                                    </tr>
                                  
                              </table>
                              
                              <div id="text_area">
                              <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                  <td height="25"><table width="97%" border="0" align="center" cellpadding="0" cellspacing="0">
                                    <tr>
                                      <td height="1"><hr color="#327ab8" size="1"></td>
                                    </tr>
                                  </table></td>
                                </tr>
                              </table>
                               
                              <table id="reseller2" align="center" border="0" cellpadding="2" cellspacing="0" width="80%">
                                
            
                                  <tr>
                                    <td class="text_hui_11"><font style="font-size: 8.5pt;" color="#ff3300" face="Tahoma"> <strong>SHIPPING ADDRESS</strong></font></td>
                                    <td class="text_hui_11"><font style="font-size: 8.5pt;" color="#ff3300" face="Tahoma">&nbsp; </font></td>
                                  </tr>
                                  <tr>
                                    <td colspan="2" style="padding-left: 40px;" class="text_hui_11" height="43"><font style="font-size: 8.5pt;" face="Tahoma"><br>
                                      If billing address and shipping address are 
                                      different, for your security, you must call your 
                                      credit card issuing bank to add this shipping 
                                      address on file as an alternative shipping address 
                                      for your credit card.</font></td>
                                  </tr>
                                  <tr>
                                    <td class="text_hui_11">&nbsp;</td>
                                    <td valign="top">&nbsp;</td>
                                  </tr>
                                  <tr>
                                    <td class="text_hui_11" height="26"><font style="font-size: 8.5pt;" face="Tahoma">First 
                                      name:</font></td>
                                    <td height="26"><%= v_customer_shipping_first_name%></td>
                                  </tr>
                                  <tr>
                                    <td class="text_hui_11"><font style="font-size: 8.5pt;" face="Tahoma">Last 
                                      Name:</font></td>
                                    <td><%= v_customer_shipping_last_name%></td>
                                  </tr>
                                  <tr>
                                    <td class="text_hui_11"><font style="font-size: 8.5pt;" face="Tahoma"> Address:</font></td>
                                    <td><span class="text_hui_11"> <font face="Tahoma"><span style="font-size: 8.5pt;"><!--input name="shipping_address" id="shipping_address" type="text" class="input" size="13" style="width:150px; " onchange="" value="25 DUNSDALE SQ" tabindex="14"-->
                                    </span></font></span><font face="Tahoma"><span style="font-size: 12px;"><%= v_customer_shipping_address%></span></font> </td>
                                  </tr>
                                  <tr>
                                    <td class="text_hui_11" height="21"><font style="font-size: 8.5pt;" face="Tahoma"> Country:</font></td>
                                    <td height="21"><%= GetCountryName(LAYOUT_SHIPPING_COUNTRY_CODE) %></td>
                                  </tr>
                                  <tr>
                                    <td class="text_hui_11"><font style="font-size: 8.5pt;" face="Tahoma">City:</font></td>
                                    <td valign="top"><%= v_customer_shipping_city %></td>
                                  </tr>
                                  <tr>
                                    <td class="text_hui_11" height="19"><font style="font-size: 8.5pt;" face="Tahoma">State 
                                      (Province):</font></td>
                                    <td height="19">
                                      <%= (LAYOUT_SHIPPING_STATE_CODE) %></td>
                                  </tr>
                                  <tr>
                                    <td class="text_hui_11"><font style="font-size: 8.5pt;" face="Tahoma">Zip 
                                      (Post) Code:</font></td>
                                    <td valign="top"><%= v_customer_shipping_zip_code%></td>
                                  </tr>
                                  <tr>
                                    <td class="text_hui_11"><font style="font-size: 8.5pt;" face="Tahoma">Email 
                                      Address:</font></td>
                                    <td valign="top"><%= v_customer_email2 %></td>
                                  </tr>
            
                                  <tr>
                                    <td class="text_hui_11"><font style="font-size: 8.5pt;" face="Tahoma"> Telephone 1: </font></td>
                                    <td><font face="Tahoma"> <span style="font-size: 12px;"><%= v_phone_n%></span></font></td>
                                  </tr>
                                  <tr>
                                    <td class="text_hui_11"><font style="font-size: 8.5pt;" face="Tahoma">&nbsp;</font></td>
                                    <td><font style="font-size: 8.5pt;" face="Tahoma">&nbsp;</font></td>
                                  </tr>
                                  
                                  <tr>
                                    <td class="text_hui_11"><font style="font-size: 8.5pt;" face="Tahoma">&nbsp;</font></td>
                                    <td align="right"><font style="font-size: 8.5pt;" face="Tahoma"> &nbsp;&nbsp;<a style="color: blue; cursor:pointer" onclick="ChangeArea();">Change</a></font></td>
                                  </tr>
                                
                              </table>
                              </div>
                              
                              <table align="center" border="0" cellpadding="0" cellspacing="0" width="97%">
                                  
                                    <tr>
                                      <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                      <td><form name="form2" id="form2" method="post" action="<%= LAYOUT_HOST_URL %>shopping_checkout_paypal_doDirectPayment.asp?d=<%= now() %>">
                                      
                                          <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                            
                                              <tr>
                                                <td height="1"><hr color="#327ab8" size="1"></td>
                                              </tr>
                                            
                                          </table>
                                        <div id="form_area" style="display: none">
                                        <table id="reseller" align="center" border="0" cellpadding="2" cellspacing="0" width="80%">
                                             <tr>
                                                <td class="text_hui_11" width="26%"><font style="font-size: 8.5pt;" face="Tahoma">&nbsp;</font></td>
                                                <td><font style="font-size: 8.5pt;" face="Tahoma">&nbsp;</font></td>
                                          </tr>
                                              
                                              <tr>
                                                <td class="text_hui_11" colspan="2"><font style="font-size: 8.5pt;" color="#ff3300" face="Tahoma"><strong>SHIPPING ADDRESS</strong></font></td>
                                               
                                              </tr>
                                             
                                              <tr>
                                                <td colspan="2" style="padding-left: 40px;" class="text_hui_11" height="43"><font style="font-size: 8.5pt;" face="Tahoma"><br>
                                                  If billing address and shipping address are 
                                                  different, for your security, you must call your 
                                                  credit card issuing bank to add this shipping 
                                                  address on file as an alternative shipping address 
                                                  for your credit card.</font></td>
                                              </tr>
                                              <tr>
                                                <td class="text_hui_11">&nbsp;</td>
                                                <td valign="top">&nbsp;</td>
                                              </tr>
                                              <tr>
                                                <td class="text_hui_11" height="26"><font style="font-size: 8.5pt;" face="Tahoma">First 
                                                  name:<span class="STYLE1">*</span></font></td>
                                                <td height="26" valign="top"><span class="text_hui_11"><font face="Tahoma"> <span style="font-size: 8.5pt;">
                                                  <input name="shipping_first_name" type="text" class="input9pt" id="shipping_first_name" style="width: 150px;" tabindex="1" onchange="StoreSession('s_shipping_file_name', this.value);" value="<%= v_customer_shipping_first_name%>" size="13">
                                                </span></font></span></td>
                                              </tr>
                                              <tr>
                                                <td class="text_hui_11"><font style="font-size: 8.5pt;" face="Tahoma">Last 
                                                  Name:<span class="STYLE1">*</span></font></td>
                                                <td valign="top"><span class="text_hui_11"><font face="Tahoma"><span style="font-size: 8.5pt;"><font face="Tahoma"><font face="Tahoma"><font face="Tahoma">
                                                  <input name="shipping_last_name" type="text" class="input9pt" id="shipping_last_name" style="width: 150px;" tabindex="2" onChange="StoreSession('s_shipping_last_name', this.value);" value="<%= v_customer_shipping_last_name%>" size="13">
                                                </font></font></font></span></font></span></td>
                                              </tr>
                                              <tr>
                                                <td class="text_hui_11"><font style="font-size: 8.5pt;" face="Tahoma"> Address: <span class="STYLE1">*</span>&#12288;</font></td>
                                                <td valign="top"><span class="text_hui_11"> <font face="Tahoma"><span style="font-size: 8.5pt;">
                                                  <textarea name="shipping_address" cols="40" rows="2" id="shipping_address" tabindex="3" onchange="StoreSession('s_shipping_address', this.value);"  style="font-family:Tahoma; font-size:9pt;color:#333333"><%= v_customer_shipping_address%></textarea>
                                                  <!--input name="shipping_address" id="shipping_address" type="text" class="input" size="13" style="width:150px; " onchange="" value="25 DUNSDALE SQ" tabindex="14"-->
                                                </span></font></span></td>
                                              </tr>
                                              <tr>
                                                <td class="text_hui_11" height="21"><font style="font-size: 8.5pt;" face="Tahoma"> Country:</font></td>
                                                <td height="21" valign="top"><%= GetCountryName(LAYOUT_SHIPPING_COUNTRY_CODE) %></td>
                                              </tr>
                                              <tr>
                                                <td class="text_hui_11"><font style="font-size: 8.5pt;" face="Tahoma">City:<span class="STYLE1">*</span> </font></td>
                                                <td valign="top"><span class="text_hui_11"> <font face="Tahoma"><span style="font-size: 8.5pt;">
                                                  <input name="shipping_city" type="text" class="input9pt" id="shipping_city" style="width: 150px;" tabindex="4" onchange="StoreSession('s_shipping_city', this.value);" value="<%= v_customer_shipping_city %>" size="13">
                                                </span></font></span></td>
                                              </tr>
                                              <tr>
                                                <td class="text_hui_11" height="19"><font style="font-size: 8.5pt;" face="Tahoma">State 
                                                  (Province)</font></td>
                                                <td height="19" valign="top"><%= (LAYOUT_SHIPPING_STATE_CODE) %><font style="font-size: 8.5pt;" face="Tahoma"> &nbsp;&nbsp;<a style="color: blue;" href="Shopping_Cart.asp">Change</a>
            
                                                </font></td>
                                              </tr>
                                              <tr>
                                                <td class="text_hui_11"><font style="font-size: 8.5pt;" face="Tahoma">Zip 
                                                  (Post) Code<span class="STYLE1">*</span> </font> </td>
                                                <td valign="top"><span class="text_hui_11"> <font face="Tahoma"><span style="font-size: 8.5pt;">
                                                  <input name="shipping_zip_code" type="text" class="input9pt" id="shipping_zip_code" style="width: 150px;" tabindex="5" onchange="StoreSession('s_shipping_zip_code', this.value);" value="<%= v_customer_shipping_zip_code%>" size="13" maxlength="7">
                                                </span></font></span></td>
                                              </tr>
                                              <tr>
                                                <td class="text_hui_11"><font style="font-size: 8.5pt;" face="Tahoma">Email 
                                                  Address:</font></td>
                                                <td valign="top"><span class="text_hui_11"> <font face="Tahoma"><span style="font-size: 8.5pt;">
                                                  <input name="secondary_email" type="text" class="input9pt" style="width: 150px;" tabindex="6" onchange="StoreSession('s_secondary_email', this.value);" value="<%= v_customer_email2 %>" size="13">
                                                </span></font></span></td>
                                              </tr>
            
                                              <tr>
                                                <td class="text_hui_11"><font style="font-size: 8.5pt;" face="Tahoma"> Telephone 1: </font></td>
                                                <td><span class="text_hui_11"><font face="Tahoma"> <span style="font-size: 8.5pt;">
                                                  <input name="phone_n" type="text" class="input9pt" id="phone_n" style="width: 150px;" tabindex="8" onchange="return StoreSession('s_telephone2', this.value);" value="<%= v_phone_n%>" size="13">
                                                  Format&#65306;555-777-8888</span></font></span></td>
                                              </tr>
                                              <tr>
                                                <td class="text_hui_11"><font style="font-size: 8.5pt;" face="Tahoma">&nbsp;</font></td>
                                                <td><font style="font-size: 8.5pt;" face="Tahoma">&nbsp;</font></td>
                                              </tr>
                                              
                                          </table>
                                        </div>
                                             <table id="reseller" align="center" border="0" cellpadding="2" cellspacing="0" width="80%">
                                             <tr>
                                                <td colspan="2" class="text_hui_11" height="23"><font style="font-size: 8.5pt;" color="#ff3300" face="Tahoma"> <strong>CREDIT CARD BILLING INFORMATION</strong></font></td>
                                              </tr>
                                              
                                    <tr>
                                        <td colspan="2" style="padding-left: 40px;" class="text_hui_11" height="43">
                                        <font style="font-size: 8.5pt;" face="Tahoma"><br>
                                        Address and telephone number must be exactly same as 
                                        appeared on your credit card monthly statement. 
                                        Please check for accuracy; incorrect information 
                                        causes delay in processing of your orders. </font>							</td>
                                    </tr>
                                              <tr>
                                                <td class="text_hui_11">&nbsp;</td>
                                                <td valign="top">&nbsp;</td>
                                              </tr>
                                              <tr>
                                                <td class="text_hui_11"><font style="font-size: 8.5pt;" face="Tahoma">First 
                                                  Name: <span class="STYLE1">*</span></font></td>
                                                <td valign="top"><span class="text_hui_11"><font face="Tahoma"><span style="font-size: 8.5pt;"><font face="Tahoma">
                                                  <input name="cart_first_name" type="text" class="input9pt" id="cart_first_name" style="width: 150px; font-size: 9pt; font-family: Tahoma; font-variant: small-caps;" tabindex="11" onChange="StoreSession('s_cart_first_name', this.value)" value="<%=v_customer_shipping_first_name%>" size="13">
                                                </font></span></font></span></td>
                                              </tr>
                                              <tr>
                                                <td class="text_hui_11"><font style="font-size: 8.5pt;" face="Tahoma">Last 
                                                  Name: <span class="STYLE1">*</span></font></td>
                                                <td valign="top"><span class="text_hui_11"> <font face="Tahoma"><span style="font-size: 8.5pt;">
                                                  <input name="cart_last_name" type="text" class="input9pt" id="cart_last_name" style="width: 150px; font-family: Tahoma; font-size: 9pt; font-variant: small-caps;" tabindex="12" onchange="StoreSession('s_cart_last_name', this.value)" value="<%=v_customer_shipping_last_name%>" size="13">
                                                </span></font></span></td>
                                              </tr>
                                              <tr>
                                                <td class="text_hui_11"><font style="font-size: 8.5pt;" face="Tahoma">Card 
                                                  Billing Address: <span class="STYLE1">*</span></font></td>
                                                <td valign="top"><span class="text_hui_11"> <font face="Tahoma"><span style="font-size: 8.5pt;">
                                                
                                                  <textarea name="card_billing_shipping_address"  cols="40" rows="2" id="card_billing_shipping_address" style="font-family: Tahoma; font-size: 9pt;color:#333333" tabindex="13" onchange="StoreSession('s_card_billing_shipping_address', this.value)"><%= v_customer_card_billing_shipping_address %></textarea>
                                                </span></font></span></td>
                                              </tr>
                                              <tr>
                                                <td class="text_hui_11"><font style="font-size: 8.5pt;" face="Tahoma"> Country:<span class="STYLE1">*</span></font></td>
                                                <td valign="top"><font face="Tahoma"><span style="font-size: 8.5pt;">
                                                  <select name="card_country" class="input9pt" id="card_country"  style="width:150px; " tabindex="14" onChange="cardChangeCountry(this, '<%= v_customer_card_state%>', 'card_state');StoreSession('s_card_country', this.options[this.selectedIndex].value);" >
                                                    <option value="0">Select Your Country</option>
                                                    <%
                                                            set rs = conn.execute("select * from tb_country limit 0,2")
                                                            if not rs.eof then 
                                                                do while not rs.eof 
                                                        %>
                                                    <option value="<%= rs("code") %>" 
                                                         <% 
                                                        
                                                            if cstr(v_customer_card_country) = cstr(rs("code")) then response.write " selected=""true""" 
                                                     
                                                            %>
                                                         ><%= rs("name") %></option>
                                                    <%
                                                                rs.movenext
                                                                loop
                                                            end if 
                                                            rs.close : set rs = nothing
                                                        %>
                                                  </select>
                                                </span></font></td>
                                              </tr>
                                              <tr>
                                                <td class="text_hui_11"><font style="font-size: 8.5pt;" face="Tahoma">City: <span class="STYLE1">*</span></font></td>
                                                <td valign="top"><span class="text_hui_11"> <font face="Tahoma"><span style="font-size: 8.5pt;">
                                                  <input name="card_city" type="text" class="input9pt" id="card_city" style="width: 150px;" tabindex="15" onchange="StoreSession('s_card_city', this.value);" value="<%= v_customer_card_city%>" size="13">
                                                </span></font></span></td>
                                              </tr>
                                              <tr>
                                                <td class="text_hui_11"><font style="font-size: 8.5pt;" face="Tahoma">State 
                                                  (Province)<span class="STYLE1">*</span> </font></td>
                                                <td valign="top"><span class="text_hui_11" id="card_state_area"> <font face="Tahoma"><span style="font-size: 8.5pt;" >
                                                  <select name="card_state" class="input9pt" id="card_state" style="width:150px; " tabindex="16" onChange="StoreSession('s_card_state', this.options[this.selectedIndex].value);">
                                                    <option value="-1">Please Select</option>                                                   
                                                  </select>
                                                </span></font></span></td>
                                              </tr>
                                              <tr>
                                                <td class="text_hui_11"><font style="font-size: 8.5pt;" face="Tahoma">Zip 
                                                  (Post) Code<span class="STYLE1">*</span> </font> </td>
                                                <td valign="top"><span class="text_hui_11"> <font face="Tahoma"><span style="font-size: 8.5pt;">
                                                  <input name="card_zip_code" type="text" class="input9pt" id="card_zip_code" style="width: 150px;" tabindex="17" onchange="StoreSession('s_card_zip_code', this.value);" value="<%= v_customer_card_zip_code %>" size="13" maxlength="7">
                                                </span></font></span></td>
                                              </tr>
                                              <tr>
                                                <td class="text_hui_11"><font style="font-size: 8.5pt;" face="Tahoma"> Telephone:<span class="STYLE1">*</span></font></td>
                                                <td><span class="text_hui_11"><font face="Tahoma"> <span style="font-size: 8.5pt;">
                                                  <input name="phone_d" type="text" class="input9pt" id="phone_d" style="width: 150px;" tabindex="18" onchange="return StoreSession('s_telephone1', this.value);" value="<%= v_phone_d%>" size="13">
                                                  Format&#65306;555-777-8888</span></font></span></td>
                                              </tr>
                                              <tr>
                                                <td class="text_hui_11"><font style="font-size: 8.5pt;" face="Tahoma">&nbsp;</font></td>
                                                <td><font style="font-size: 8.5pt;" face="Tahoma">&nbsp;</font></td>
                                              </tr>
                                              <tr>
                                                <td class="text_hui_11" width="26%" valign="top"><font style="font-size: 8.5pt;" face="Tahoma">Card 
                                                  Type:<span class="STYLE1">*</span></font></td>
                                                <td><font face="Tahoma"> <span style="font-size: 8.5pt;">
                                                  <select name="card_type" class="input9pt" id="card_type" style="width: 150px;" tabindex="19" onchange="StoreSession('s_card_type', this.options[this.selectedIndex].value);">
                                                        <option value="Visa" selected>Visa</option>
                                                        <option value="MasterCard">MasterCard</option>
                                                        <option value="Discover">Discover</option>
                                                        <option value="Amex">American Express</option>
                                                  </select>
                                                  <br />
                                                  For Canada, only MasterCard and Visa are allowable; Interac debit cards are not supported.
                                                </span></font></td>
                                              </tr>
                                            
                                              <tr>
                                                <td class="text_hui_11"><font style="font-size: 8.5pt;" face="Tahoma">Card 
                                                  Number:<span class="STYLE1">*</span></font></td>
                                                <td valign="top"><span class="text_hui_11"><font face="Tahoma"><span style="font-size: 8.5pt;">
                                                  <input name="card_number" type="text" class="input9pt" id="card_number" style="width: 150px;" tabindex="20"  value="<%'= v_customer_credit_card %>" size="13"><br>Format(no space):&nbsp;XXXXXXXXXXXXXXXX</span></font></span></td>
                                              </tr>
                                              <tr>
                                                <td class="text_hui_11"><font style="font-size: 8.5pt;" face="Tahoma">Card 
                                                  Expiry Date:<span class="STYLE1">*</span></font></td>
                                                <td><font style="font-size: 8.5pt;" face="Tahoma"> Month</font><font face="Tahoma"><span style="font-size: 8.5pt;">
                                                  <select name="card_expiry_month" id="card_expiry_month" class="input9pt" tabindex="21" onchange="StoreSession('s_card_expiry_month', this.options[this.selectedIndex].value);" style="width: 50px;">
                                                    <option value="-1"></option>
                                                    <option value="01">01</option>
                                                    <option value="02">02</option>
                                                    <option value="03">03</option>
                                                    <option value="04" >04</option>
                                                    <option value="05">05</option>
                                                    <option value="06">06</option>
                                                    <option value="07">07</option>
                                                    <option value="08">08</option>
                                                    <option value="09">09</option>
                                                    <option value="10">10</option>
                                                    <option value="11">11</option>
                                                    <option value="12">12</option>
                                                  </select>
                                                  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                                                  Year:
                                                  <select name="card_expiry_year" id="card_expiry_year" class="input9pt" tabindex="22" onchange="StoreSession('s_card_expiry_year', this.options[this.selectedIndex].value);" style="width: 80px;">
                                                                    <option value="-1"></option>
                                                                    <option value="2008">2008</option>
                                                                    <option value="2009" >2009</option>
                                                                    <option value="2010">2010</option>
                                                                    <option value="2011">2011</option>
                                                                    <option value="2012">2012</option>
                                                                    <option value="2013">2013</option>
                                                                    <option value="2014">2014</option>
                                                                    <option value="2015">2015</option>
                                                                    <option value="2016">2016</option>
                                                                    <option value="2017">2017</option>
                                                                    <option value="2018">2018</option>
                                                                    <option value="2019">2019</option>
                                                                    <option value="2020">2020</option>
                                                                  </select>
                                                </span></font></td>
                                              </tr>
                                              
                                               <tr>
                                                <td class="text_hui_11">Card Verification Number:<font style="font-size: 8.5pt;" face="Tahoma"><span class="STYLE1">*</span></font></td>
                                                <td valign="top"><input name="card_verification_number" id="card_verification_number" type="text" class="input9pt"   tabindex="23"  size="3"></td>
                                              </tr>
                                              <tr>
                                                <td class="text_hui_11"><font style="font-size: 8.5pt;" face="Tahoma">Card 
                                                  Issuing Bank:<span class="STYLE1">*</span><br>
                                                  &nbsp;</font></td>
                                                <td valign="top"><input name="card_issuer" id="card_issuer" type="text" class="input9pt"  style="width: 150px;" tabindex="23" onchange="StoreSession('s_card_issuer', this.value)" value="<%'= v_customer_card_issuer%>" size="13"></td>
                                              </tr>
                                              <tr>
                                                <td class="text_hui_11"><font style="font-size: 8.5pt;" face="Tahoma">Card 
                                                  Issuing Bank's Telephone Number: <span class="STYLE1">*</span><br>
                                                  &nbsp;</font></td>
                                                <td valign="top"><span class="text_hui_11"> <font face="Tahoma"><span style="font-size: 8.5pt;">
                                                  <input name="card_issuer_telephone" id="card_issuer_telephone" type="text" class="input9pt" style="width: 150px;" tabindex="24" onchange="StoreSession('s_card_issuer_telephone', this.value)" value="<%'= v_customer_card_phone%>" size="13">
                                                  Format&#65306;555-777-8888<br>
                                                  (It is on the back of your card)</span></font></span></td>
                                              </tr>
                                              <tr>
                                                <td colspan="2" class="text_hui_11"><font style="font-size: 8.5pt;" face="Tahoma"> <strong style="font-weight: 400;">Your special 
                                                  instructions and notes:</strong></font></td>
                                              </tr>
                                              <tr>
                                                <td colspan="2" class="text_hui_11" align="right"><font face="Tahoma"><span style="font-size: 8.5pt;">
                                                  <textarea name="note" cols="58" rows="5" tabindex="9" onchange="StoreSession('s_customer_comment', this.value);" style="font-family:Tahoma; font-size:9pt;color:#333333"></textarea>
                                               
                                                </span></font></td>
                                              </tr>
                                              <tr>
                                                <td colspan="2" class="text_hui_11" height="23">&nbsp;</td>
                                              </tr>
                                              
                                        </table>
                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                            
                                              <tr>
                                                <td height="1"><hr color="#327ab8" size="1"></td>
                                              </tr>
                                            
                                        </table>
                                      </form></td>
                                    </tr>
                                  
                              </table>
                               <!--<table style="border-bottom: 1px solid rgb(50, 122, 184);" align="center" border="0" cellpadding="0" cellspacing="0" width="98%">                     
                                    <tr>
                                      <td>
                                      
                                      
                                      </td>
                                    </tr>
                               </table>-->
                              <table style="border-bottom: 1px solid rgb(50, 122, 184);" align="center" border="0" cellpadding="0" cellspacing="0" width="98%">
                                  
                                    <tr>
                                      <td><table align="center" border="0" cellpadding="0" cellspacing="0" width="80%">
                                          
                                            
                                            <tr>
                                              <td class="text_hui_11"><font style="font-size: 8.5pt;" face="Tahoma">Sales are 
                                                subject to LU Computers' sales terms and policies.</font>
                                                  <p>LU Computers reserves the right to decline the acceptance of any order for any reason. All information provided here will be thoroughly researched to avoid credit card fraudulent activities before shipping. LU Computers may require additional information and/or verification before accepting and processing any order.</p>
                                                <p>LU Computers reserves the right to cancel an order or to issue a Return Merchandise Authorization (RMA) for merchandise that is advertised in error, that does not conform to advertised specifications, or was shipped in error. Charge backs from credit card issuing banks are subject to the above policies.<br>
                                                </p></td>
                                            </tr>
                                            <tr>
                                              <td>&nbsp;</td>
                                            </tr>
                                          
                                      </table></td>
                                    </tr>
                                  
                              </table>
                              <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                  <tr>
                                    <td height="60"></td>
                                  </tr>
                                </table>
                              <table border="0" cellpadding="3" cellspacing="0" width="100%">
                                  
                                    <tr>
                                      <td align="center"><table border="0" cellpadding="3" cellspacing="0" width="45%">
                                         
                                            <tr align="right">
                                              <td><table id="__" border="0" cellpadding="0" cellspacing="0" height="24" width="130">
                                                 
                                                    <tr>
                                                      <td width="28"><font style="font-size: 8.5pt;" face="Tahoma"> <img src="/soft_img/app/arrow_left.gif" alt="" height="24" width="28"></font></td>
                                                      <td class="btn_middle"><strong><a class="btn_img" onClick="window.location.href='<%= LAYOUT_HOST_URL %>shopping_cart.asp';">Back</a></strong> </td>
                                                      <td width="6"><font style="font-size: 8.5pt;" face="Tahoma"> <img src="/soft_img/app/customer_bottom_04.gif" alt="" height="24" width="6"></font></td>
                                                    </tr>
                                                  
                                              </table></td>
                                              <td><table id="__" border="0" cellpadding="0" cellspacing="0" height="24" width="130">
                                                  
                                                    <tr>
                                                      <td width="28"><font style="font-size: 8.5pt;" face="Tahoma"> <img src="/soft_img/app/arrow_right.gif" alt="" height="24" width="28"></font></td>
                                                      <td class="btn_middle" style="cursor:pointer" onClick="Check(document.getElementById('form2'));"><a class="btn_img"><b>Submit</b></a></td>
                                                      <td width="6"><font style="font-size: 8.5pt;" face="Tahoma"> <img src="/soft_img/app/customer_bottom_04.gif" alt="" height="24" width="6"></font></td>
                                                    </tr>
                                                  
                                              </table></td>
                                            </tr>
                                          
                                      </table></td>
                                    </tr>
                                  
                              </table>
                              <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                  <tr>
                                    <td height="60"></td>
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
	$('#card_state_area').load('/site/inc/inc_get_state.asp?is_have_event=true&state_selected_code=<%= v_customer_card_state_code %>&element_state_id=card_state&country_code=<%= v_customer_card_country_code %>');
});

function cardChangeCountry(the, selected_state_code, element_id)
{
	var country_code = the.options[the.selectedIndex].value;
	$('#card_state_area').load('/site/inc/inc_get_state.asp?is_have_event=true&state_selected_code='+ selected_state_code+'&element_state_id='+ element_id +'&country_code='+ country_code);
	
}
function StoreSession(str,s)
{

}
function Check(the)
{
	var err = "Please fill up all missing items.";
	
	if($('#shipping_first_name').val() == '')
	{
		alert(err);
		$('#shipping_first_name').focus();		
		return false;
	}
	
	if($('#shipping_last_name').val() == '')
	{
		alert(err);
		$('#shipping_last_name').focus();		
		return false;
	}
	
	if($('#shipping_address').val() == '')
	{
		alert(err);
		$('#shipping_address').focus();		
		return false;
	}
	
//	if($('#shipping_country').val() == '')
//	{
//		alert(err);
//		$('#shipping_country').focus();		
//		return false;
//	}
	
	if($('#shipping_city').val() == '')
	{
		alert(err);
		$('#shipping_city').focus();		
		return false;
	}
	
//	if($('#shipping_state').val() == '')
//	{
//		alert(err);
//		$('#shipping_state').focus();		
//		return false;
//	}
	
	if($('#shipping_zip_code').val() == '')
	{
		alert(err);
		$('#shipping_zip_code').focus();		
		return false;
	}
	if($('#phone_n').val() == '')
	{
		alert(err);
		$('#phone_n').focus();		
		return false;
	}
	
	if($('#cart_first_name').val() == '')
	{
		alert(err);
		$('#cart_first_name').focus();		
		return false;
	}
	if($('#cart_last_name').val() == '')
	{
		alert(err);
		$('#cart_last_name').focus();		
		return false;
	}
	if($('#card_billing_shipping_address').val() == '')
	{
		alert(err);
		$('#card_billing_shipping_address').focus();		
		return false;
	}
	if($('#card_country').val() == '0')
	{
		alert(err);
		$('#card_country').focus();		
		return false;
	}
	if($('#card_city').val() == '')
	{
		alert(err);
		$('#card_city').focus();		
		return false;
	}
	if($('#card_zip_code').val() == '')
	{
		alert(err);
		$('#card_zip_code').focus();		
		return false;
	}
	if($('#card_state').val() == '-1')
	{
		alert(err);
		$('#card_state').focus();		
		return false;
	}
	if($('#phone_d').val() == '')
	{
		alert(err);
		$('#phone_d').focus();		
		return false;
	}
	if($('#card_type').val() == '0')
	{
		alert(err);
		$('#card_type').focus();		
		return false;
	}
	if($('#card_number').val() == '')
	{
		alert(err);
		$('#card_number').focus();		
		return false;
	}
	if($('#card_expiry_month').val() == '-1')
	{
		alert(err);
		$('#card_expiry_month').focus();		
		return false;
	}
	if($('#card_expiry_year').val() == '-1')
	{
		alert(err);
		$('#card_expiry_year').focus();		
		return false;
	}
	if($('#card_verification_number').val() == '')
	{
		alert(err);
		$('#card_verification_number').focus();		
		return false;
	}
	if($('#card_issuer').val() == '')
	{
		alert(err);
		$('#card_issuer').focus();		
		return false;
	}
	$('#form2').submit();
}
</script>