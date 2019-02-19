<!--#include virtual="site/inc/inc_page_top.asp"-->
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
                    <span class='nav1'>My Acount</span>
                </div>
            	<div id="page_main_area">
                		<table width="100%" height="670" border="0" align="center" cellpadding="0" cellspacing="2" bgcolor="#FFFFFF" style="border:#8FC2E2 1px solid; ">
                                  <tr>
                                    <td valign="top" style="border:#E3E3E3 1px solid; ">
                                    <table style="border-bottom:#CDE3F2 1px solid; " width="100%"  border="0" cellspacing="0" cellpadding="3">
                                      <tr align="center">
                                        <td width="25%" class="info_nav"  onClick="window.location.href='<%= LAYOUT_HOST_URL %>member_center_info.asp'">My Profile </td>
                                      	<td width="25%" class="info_nav2" onClick="window.location.href='<%= LAYOUT_HOST_URL %>member_center_porder.asp'">Pending Orders </td>
                                        <td width="25%" class="info_nav2" onClick="window.location.href='<%= LAYOUT_HOST_URL %>member_center_aorder.asp'">All Orders </td>
                                        <td width="25%" class="info_nav2" onClick="window.location.href='<%= LAYOUT_HOST_URL %>member_logout.asp'">Logout</td>
                                      </tr>
                                    </table>
                                      <table width="100%" height="200"  border="0" cellspacing="0">
                                        <tr>
                                          <td valign="top" bgcolor="#FFFFFF"><table width="95%"  border="0" align="center" cellpadding="0" cellspacing="0">
                                            <tr>
                                              <td><!--include file="public_helper/md5.asp"-->
                                                  <%
                                                ' 验证用户ＩＤ是否存在
						'Response.write LAYOUT_CCID &"d"
                        Call ValidateUserSessionInfo(LAYOUT_CCID ,"closeConn") 
                        
						Dim customer_fullname 			:	customer_fullname		=	null
						Dim email1 					 	:	email1	=	null
						Dim email2 					 	:	email2	=	null
						Dim customer_password 		 	:	customer_password	=	null
					
						Dim customer_country 		 	:	customer_country		=	null
						Dim state_serial_no 		 	:	state_serial_no	=	null
						Dim zip_code 				 	:	zip_code	=	null
						Dim customer_first_name 	 	:	customer_first_name		=	null
						Dim customer_last_name 		 	:	customer_last_name		=	null
						Dim phone_d 				 	:	phone_d		=	null
						Dim phone_n 				 	:	phone_n		=	null
						Dim phone_c 				 	:	phone_c	=	null
						Dim v_customer_address1 		:	v_customer_address1		=	null
						Dim customer_email2 		 	:	customer_email2		=	null
						Dim customer_credit_card 	 	:	customer_credit_card		=	null
						Dim customer_expiry 		 	:	customer_expiry		=	null
						Dim customer_Company 		 	:	customer_Company		=	null
						Dim shipping_city 			 	:	shipping_city		=	null
						Dim EBay_ID 				 	:	EBay_ID		=	null
						Dim customer_note 			 	:	customer_note	=	null
						Dim email_tag 					:	email_tag		=	null			
						Dim v_customer_city 		 	:	v_customer_city		=	null
						Dim v_pay_method 				:	v_pay_method		=	null

						Dim v_my_purchase_order 		 	:	v_my_purchase_order		=	null
						Dim v_customer_business_country_code:	v_customer_business_country_code		=	null
						Dim v_customer_business_state_code 	:	v_customer_business_state_code		=	null
						Dim v_customer_business_zip_code 	:	v_customer_business_zip_code		=	null
						Dim v_customer_business_address 	:	v_customer_business_address=	null
						Dim v_customer_business_city 		:	v_customer_business_city		=	null
						Dim v_tax_execmtion     		 	:	v_tax_execmtion	=	null						
						Dim v_busniess_website 			 	:	v_busniess_website		=	null
						Dim v_busniess_city  			 	:	v_busniess_city	=	null
						Dim v_busniess_telephone 		 	:	v_busniess_telephone	=	null
						Dim v_customer_country_code 		:	v_customer_country_code	=	null
						Dim v_state_code 					:	v_state_code		=	null
						Dim customer_shipping_first_name	:	customer_shipping_first_name	=	null
						Dim customer_shipping_last_name		:	customer_shipping_last_name		=	null
						Dim shipping_country_code			:	shipping_country_code		=	null
						Dim customer_shipping_address		:	customer_shipping_address		=	null
						Dim customer_shipping_city			:	customer_shipping_city			=	null
						Dim shipping_state_code				:	shipping_state_code			=	null
						Dim customer_shipping_zip_code		:	customer_shipping_zip_code		=	null
						
                        	set rs = conn.execute( "select * from tb_customer where customer_serial_no="& SQLquote(LAYOUT_CCID))
                            if not rs.eof then 
                    
                                customer_fullname 		= rs("customer_login_name")
                                email1 					= rs("customer_email1")
                                email2 					= rs("customer_email2")
                                customer_password 		= rs("customer_password")
                            
                                customer_country 		= rs("customer_country")
                                state_serial_no 		= rs("state_serial_no")
                                zip_code 				= rs("zip_code")
                                customer_first_name 	= rs("customer_first_name")
                                customer_last_name 		= rs("customer_last_name")
                                phone_d 				= rs("phone_d")
                                phone_n 				= rs("phone_n")
                                phone_c 				= rs("phone_c")
                                v_customer_address1 	= rs("customer_address1")
                                customer_email2 		= rs("customer_email2")
                                customer_expiry 		= rs("customer_expiry")
                                customer_Company 		= rs("customer_Company")
                                shipping_city 			= rs("customer_shipping_city")
                                EBay_ID 				= rs("EBay_ID")
                                customer_note 			= rs("customer_note")
                                email_tag 				= rs("news_latter_subscribe")                                
                                v_customer_city 		= trim(rs("customer_city"))                               
                                v_pay_method 				= trim(rs("pay_method"))
								v_customer_business_country_code=	rs("customer_business_country_code")
								v_customer_business_state_code	=	rs("customer_business_state_code")
								v_customer_business_zip_code	=	rs("customer_business_zip_code")
								v_customer_business_address		=	rs("customer_business_address")
								v_customer_business_city		=	rs("customer_business_city")
                                v_tax_execmtion     		= trim(rs("tax_execmtion"))                                
                                v_busniess_website 			= trim(rs("busniess_website"))
                                v_busniess_city  			= trim(rs("customer_business_city"))
                                v_busniess_telephone 		= trim(rs("customer_business_telephone"))
								v_customer_country_code  	= rs("customer_country_code")
								v_state_code	= rs("state_code")
								customer_shipping_first_name= rs("customer_shipping_first_name")
								customer_shipping_last_name	= rs("customer_shipping_last_name")
								shipping_country_code		= rs("shipping_country_code")
								customer_shipping_address	= rs("customer_shipping_address")
								customer_shipping_city		= rs("customer_shipping_city")
								shipping_state_code			= rs("shipping_state_code")
								customer_shipping_zip_code	= rs("customer_shipping_zip_code")
								
                            end if
                            rs.close : set rs = nothing
                    
                            if( email_tag = "") then
                                email_tag = 0
                            end if
                            
                            ' 
                            'Response.cookies("tax_execmtion") =  v_tax_execmtion
                    %>
                        <div id="update_info" style="display:none">
                                                  <form method="post" action="<%= LAYOUT_HOST_URL %>member_center_info_exec.asp" name="fm" id="fm" onSubmit="return check(this);">
                                                    <table width="100%"  border="0" cellspacing="0" cellpadding="2">
                                                      <tr>
                                                        <td height="30" valign="bottom" class="text_blue_11"><strong >ACCOUNT-----------------------------------------------------------------------------------------------------</strong></td>
                                                      </tr>
                                                      <tr>
                                                        <td class="text_hui_11"><table width="95%" border="0" align="center" cellpadding="2" cellspacing="0">
                                                          <tr>
                                                            <td width="24%" class="text_hui_11">Login Name</td>
                                                            <td class="text_hui_11"><%=customer_fullname%></td>
                                                          </tr>
                                                          <tr>
                                                            <td class="text_hui_11">Password:<span class="text_orange_11">*</span></td>
                                                            <td class="text_hui_11"><input name="user_pwd" type="password" style="width:200px; "  class="input9pt" size="22" maxlength="80" value="<%=customer_password%>" tabindex="1">
                                                              &nbsp;&nbsp;</td>
                                                          </tr>
                                                          <tr>
                                                            <td class="text_hui_11"> Verify Password:<span class="text_orange_11">*</span></td>
                                                            <td class="text_hui_11"><input name="confirmpwd" type="password" style="width:200px; "  class="input9pt" size="22" maxlength="80" value="<%=customer_password%>" tabindex="2">
                                                              &nbsp;&nbsp;</td>
                                                          </tr>
                                                          <tr>
                                                            <td class="text_hui_11">First Name:</td>
                                                            <td class="text_hui_11"><input name="FN" type="text"  class="input9pt" style="width:200px; " size="22" maxlength="30" value="<%=customer_first_name%>" tabindex="3"></td>
                                                          </tr>
                                                          <tr>
                                                            <td class="text_hui_11">Last Name:</td>
                                                            <td class="text_hui_11"><input name="LN" type="text"  class="input9pt" style="width:200px; " size="22" maxlength="30" value="<%=customer_last_name%>" tabindex="4"></td>
                                                          </tr>
                                                          <tr>
                                                            <td class="text_hui_11">Home Phone:</td>
                                                            <td class="text_hui_11"><input name="phone_n" type="text"  class="input9pt" id="tel_home" style="width:200px; "   size="13" value="<%=phone_n%>" tabindex="5">
                                                              <%= LAYOUT_PHONE_FORMAT %></td>
                                                          </tr>
                                                          <tr>
                                                            <td class="text_hui_11">Business Phone:</td>
                                                            <td class="text_hui_11"><input name="phone_d" type="text"  class="input9pt" id="tel_work" style="width:200px; "  size="13" value="<%=phone_d%>" tabindex="6">
                                                              <%= LAYOUT_PHONE_FORMAT %></td>
                                                          </tr>
                                                          <tr>
                                                            <td class="text_hui_11">Mobile Phone : </td>
                                                            <td class="text_hui_11"><input name="phone_c" type="text"  class="input9pt" id="tel_cell" style="width:200px; " size="13" value="<%=phone_c%>" tabindex="7">
                                                              <%= LAYOUT_PHONE_FORMAT %></td>
                                                          </tr>
                                                          <tr>
                                                            <td class="text_hui_11">Country:</td>
                                                            <td class="text_hui_11"><select name="user_Country" id="user_Country" class="b"   style="width:200px; " onChange="infoChangeCountry(this, '<%= v_state_code %>', 'shipping_state', 'div_state');" tabindex="8">
                                                                <option value="0">Select Your Country</option>
                                                                <%
                                                                    set rs = conn.execute("select * from tb_country limit 0,2")
                                                                    if not rs.eof then 
                                                                        do while not rs.eof 
                                                                %>
                                                                 <option value="<%= rs("code") %>" 
                                                                 <%
                                                                   if cstr(SQLescape(rs("code"))) = cstr(SQLescape(v_customer_country_code)) then 
                                                                        response.write " selected=""true"" "
                                                                   end if                                                                  
                                                                 %>
                                                                 ><%= rs("name") %></option>
                                                                <%
                                                                        rs.movenext
                                                                        loop
                                                                    end if 
                                                                    rs.close : set rs = nothing
                                                                %>
                                                            </select></td>
                                                          </tr>
                                                          <tr>
                                                            <td class="text_hui_11">Address:</td>
                                                            <td class="text_hui_11">
                                                           
                                                            <textarea rows="2" cols="40"  name="address" style="font-family:Tahoma; font-size:9pt;color:#333333" id="address" tabindex="10"><%= v_customer_address1 %></textarea>
                                                            </td>
                                                          </tr>
                                                          <tr>
                                                            <td class="text_hui_11">City:</td>
                                                            <td class="text_hui_11"><input name="city" type="text"  class="input9pt" id="city" style="width:200px; " size="13" value="<%= v_customer_city %>" tabindex="11"></td>
                                                          </tr>
                                                          <tr>
                                                            <td class="text_hui_11">State (Province):</td>
                                                            <td class="text_hui_11">
                                                            <div id="div_state">
                                                            <select name="shipping_state" id="shipping_state" class="b" style="width:200px; "  tabindex="12">
                                                            </select>
                                                            </div>
                                                            </td>
                                                          </tr>
                                                          <tr>
                                                            <td class="text_hui_11">Zip (Post) Code:</td>
                                                            <td class="text_hui_11"><input name="zip_code" type="text"  class="input9pt" id="zip_code" style="width:200px; " value="<%=zip_code%>" size="13" maxlength="7" tabindex="13"></td>
                                                          </tr>
                                                          <tr>
                                                            <td class="text_hui_11">Email1:</td>
                                                            <td class="text_hui_11"><input name="email1" type="text"  class="input9pt" id="email1" style="width:200px; " size="13" value="<%=email1%>" tabindex="14"></td>
                                                          </tr>
                                                          <tr>
                                                            <td class="text_hui_11">Email2:</td>
                                                            <td class="text_hui_11"><input name="email2" type="text"  class="input9pt" id="email2" style="width:200px; " size="13" value="<%=email2%>" tabindex="15"></td>
                                                          </tr>
                                                          <tr>
                                                            <td class="text_hui_11">eBay ID:</td>
                                                            <td class="text_hui_11"><input name="ebay_id" type="text"  class="input9pt" id="ebay_id" style="width:200px; " size="13" value="<%=EBay_ID%>" tabindex="16"></td>
                                                          </tr>
                                                        </table></td>
                                                      </tr>
                                                      <tr>
                                                        <td style="cursor:pointer;" class="text_blue_11"><b>Shipping Address--------------------------------------------------------------------------------------------</b></td>
                                                      </tr>
                                                      <tr>
                                                      	
                                                        <td class="text_hui_11"><table width="95%" border="0" align="center" cellpadding="2" cellspacing="0">
                                                          <tr>
                                                            <td width="24%" class="text_hui_11">Shipping First Name:</td>
                                                            <td class="text_hui_11"><input name="customer_shipping_first_name" type="text"  class="input9pt" style="width:200px; " size="22" maxlength="30" value="<%=customer_shipping_first_name%>" tabindex="17"></td>
                                                          </tr>
                                                          <tr>
                                                            <td class="text_hui_11">Shipping Last Name:</td>
                                                            <td class="text_hui_11"><input name="customer_shipping_last_name" type="text"  class="input9pt" style="width:200px; " size="22" maxlength="30" value="<%=customer_shipping_last_name%>" tabindex="18"></td>
                                                          </tr>                                                          
                                                          <tr>
                                                            <td class="text_hui_11">Country:</td>
                                                            <td class="text_hui_11"><select name="shipping_country_code" id="shipping_country_code" class="b"   style="width:200px; " onChange="infoChangeCountry(this, '<%= shipping_state_code %>', 'shipping_state_code', 'div_shipping_state_code');" tabindex="19">
                                                                <option value="0">Select Your Country</option>
                                                                <%
                                                                    set rs = conn.execute("select * from tb_country limit 0,2")
                                                                    if not rs.eof then 
                                                                        do while not rs.eof 
                                                                %>
                                                                 <option value="<%= rs("code") %>" 
                                                                 <%
                                                                   if cstr(SQLescape(rs("code"))) = cstr(SQLescape(shipping_country_code)) then 
                                                                        response.write " selected=""true"" "
                                                                   end if                                                                  
                                                                 %>
                                                                 ><%= rs("name") %></option>
                                                                <%
                                                                        rs.movenext
                                                                        loop
                                                                    end if 
                                                                    rs.close : set rs = nothing
                                                                %>
                                                            </select></td>
                                                          </tr>
                                                          <tr>
                                                            <td class="text_hui_11">Address:</td>
                                                            <td class="text_hui_11">
                                                           
                                                            <textarea rows="2" cols="40"  name="customer_shipping_address" style="font-family:Tahoma; font-size:9pt;color:#333333" id="address" tabindex="20"><%= customer_shipping_address %></textarea>
                                                            </td>
                                                          </tr>
                                                          <tr>
                                                            <td class="text_hui_11">City:</td>
                                                            <td class="text_hui_11"><input name="customer_shipping_city" type="text"  class="input9pt" id="customer_shipping_city" style="width:200px; " size="13" value="<%= customer_shipping_city %>" tabindex="21"></td>
                                                          </tr>
                                                          <tr>
                                                            <td class="text_hui_11">State (Province):</td>
                                                            <td class="text_hui_11">
                                                            <div id="div_shipping_state_code">
                                                            <select name="shipping_state_code" id="shipping_state_code" class="b" style="width:200px; "  tabindex="22">
                                                            </select>
                                                            </div>
                                                            </td>
                                                          </tr>
                                                          <tr>
                                                            <td class="text_hui_11">Zip (Post) Code:</td>
                                                            <td class="text_hui_11"><input name="customer_shipping_zip_code" type="text"  class="input9pt" id="customer_shipping_zip_code" style="width:200px; " value="<%=customer_shipping_zip_code%>" size="13" maxlength="7" tabindex="23"></td>
                                                          </tr>                                                          
                                                        </table>
                                                        </td>
                                                      </tr>
                                                      <tr>
                                                        <td style="cursor:pointer;"><a onClick="showtr('reseller')" class="text_blue_11"><strong>BUSNIESS-------------------------------------(Click)--------------------------------------------------------</strong></a></td>
                                                      </tr>
                                                      <tr>
                                                        <td><table style="display:none" width="95%" align="center" border="0" cellpadding="2" cellspacing="0" bgcolor="#ECF6FF" id="reseller">
                                                          <tr>
                                                            <td width="24%" class="text_hui_11">&nbsp;Company Name:</td>
                                                            <td><span class="text_hui_11">
                                                              <input name="busniess_company_name" type="text"  class="input9pt" size="13" style="width:200px; " value="<%=customer_Company%>" tabindex="24">
                                                            </span></td>
                                                          </tr>
                                                          <tr>
                                                            <td class="text_hui_11">&nbsp;Telephone:</td>
                                                            <td valign="top"><span class="text_hui_11">
                                                              <input name="busniess_telephone" type="text"  class="input9pt" size="13" style="width:200px; " value="<%=v_busniess_telephone%>"  tabindex="25"><%= LAYOUT_PHONE_FORMAT %>
                                                            </span></td>
                                                          </tr>
                                                           <tr>
                                                            <td class="text_hui_11">&nbsp;Country:</td>
                                                            <td class="text_hui_11"><select name="busniess_country" id="busniess_country" class="b"  style="width:200px; " onChange="infoChangeCountry(this, '<%= v_customer_business_state_code %>', 'busniess_state', 'div_state2');" tabindex="26">
                                                                <option value="0">Select Your Country</option>
                                                                <%
                                                                    set rs = conn.execute("select * from tb_country limit 0,2")
                                                                    if not rs.eof then 
                                                                        do while not rs.eof 
                                                                %>
                                                                 <option value="<%= rs("code") %>" 
                                                                 
                                                                 <%
                                                                    if customer_country <> "" then 
                                                                        if cstr(rs("code")) = cstr(SQLescape(v_customer_business_country_code)) then 
                                                                            response.write " selected=""true"" "
                                                                        end if
                                                                    end if
                                                                 %>
                                                                 ><%= rs("name") %></option>
                                                                <%
                                                                        rs.movenext
                                                                        loop
                                                                    end if 
                                                                    rs.close : set rs = nothing
                                                                %>
                                                            </select></td>
                                                          </tr>
                                                          <tr>
                                                            <td class="text_hui_11">&nbsp;Address:</td>
                                                            <td><span class="text_hui_11">
                                                            
                                                              <textarea name="busniess_address" rows="2" cols="40" style="font-family:Tahoma; font-size:9pt;color:#333333" size="13" tabindex="27"><%=v_customer_business_address%></textarea>
                                                            </span></td>
                                                          </tr>
                                                          <tr>
                                                            <td class="text_hui_11">&nbsp;City:</td>
                                                            <td><span class="text_hui_11">
                                                              <input name="busniess_city" type="text"  class="input9pt" size="13" style="width:200px; " value="<%=v_busniess_city%>" tabindex="28">
                                                            </span></td>
                                                          </tr>
                                                          <tr>
                                                            <td class="text_hui_11">&nbsp;State (Province):</td>
                                                            <td><span class="text_hui_11" id="div_state2">
                                                              <select name="busniess_state" type="text"  class="input9pt"  tabindex="29"></select>
                                                            </span></td>
                                                          </tr>
                                                          <tr>
                                                            <td class="text_hui_11">&nbsp;Zip (Post) Code:</td>
                                                            <td><span class="text_hui_11">
                                                              <input name="busniess_zip_code" type="text"  class="input9pt" size="13" style="width:200px; "value="<%=v_customer_business_zip_code%>" tabindex="30">
                                                            </span></td>
                                                          </tr>
                                                          <tr>
                                                            <td class="text_hui_11">&nbsp;Tax Exemption Number:</td>
                                                            <td><span class="text_hui_11">
                                                              <input name="busniess_tax_exemption" type="text"  class="input9pt" maxlength="9" size="13" style="width:200px; " value="<%=v_tax_execmtion%>" tabindex="31">No space
                                                            </span></td>
                                                          </tr>
                                                          <tr>
                                                            <td class="text_hui_11">&nbsp;Website Address:</td>
                                                            <td><span class="text_hui_11">
                                                              <input name="busniess_website" type="text"  class="input9pt" size="13" style="width:200px; " value="<%=v_busniess_website%>" tabindex="32">
                                                            </span></td>
                                                          </tr>
                                                        </table></td>
                                                      </tr>
                                                      <tr>
                                                        <td><input name="cb_news_letter" type="checkbox" id="cb_news_letter"  <% if cstr(email_tag) = "1" then response.write " checked" %> tabindex="33">
                                                        <span class="text_hui_11">                                    News Letter Subscribe</span></td>
                                                      </tr>
                                                      <tr>
                                                        <td><table width="50%"  border="0" align="center" cellpadding="3" cellspacing="0">
                                                            <tr align="right">
                                                              <td><!--<table id="__01" width="115" height="24" border="0" cellpadding="0" cellspacing="0">
                                                                  <tr>
                                                                    <td width="6"><img src="/soft_img/app/3232.gif" width="6" height="24"></td>
                                                                    <td align="center" background="/soft_img/app/customer_bottom_03.gif"><a href="javascript: document.getElementById('fm').reset()" class="white-hui-12" ><strong>Reset</strong></a></td>
                                                                    <td width="6"><img src="/soft_img/app/customer_bottom_04.gif" width="6" height="24" alt=""></td>
                                                                  </tr>
                                                              </table>--></td>
                                                              <td><table id="__01" width="115" height="24" border="0" cellpadding="0" cellspacing="0">
                                                                  <tr>
                                                                    <td width="6"><img src="/soft_img/app/3232.gif" width="6" height="24"></td>
                                                                    <td align="center" background="/soft_img/app/customer_bottom_03.gif"><a href="#" class="white-hui-12" onClick="document.fm.submit()"><strong>Submit</strong></a></td>
                                                                    <td width="6"><img src="/soft_img/app/customer_bottom_04.gif" width="6" height="24" alt=""></td>
                                                                  </tr>
                                                              </table></td>
                                                            </tr>
                                                        </table></td>
                                                      </tr>
                                                    </table>
                                                </form>
                        </div>
                        <!-- update end -->
                        <!-- view info begin -->
                        <div id="view_info" >
                            
                        
                             <table width="100%"  border="0" cellspacing="0" cellpadding="2">
                                                      <tr>
                                                        <td height="30" valign="bottom" class="text_blue_11"><strong >ACCOUNT-----------------------------------------------------------------------------------------------------</strong></td>
                                                      </tr>
                                                      <tr>
                                                        <td class="text_hui_11"><table width="95%" border="0" align="center" cellpadding="2" cellspacing="0">
                                                          <tr>
                                                            <td width="24%" class="text_hui_11">Login Name</td>
                                                            <td class="text_hui_11"><%=customer_fullname%></td>
                                                          </tr>
                                                          
                                                          <tr>
                                                            <td class="text_hui_11">First Name:</td>
                                                            <td class="text_hui_11"><%=customer_first_name%></td>
                                                          </tr>
                                                          <tr>
                                                            <td class="text_hui_11">Last Name:</td>
                                                            <td class="text_hui_11"><%=customer_last_name%></td>
                                                          </tr>
                                                          <tr>
                                                            <td class="text_hui_11">Home Phone:</td>
                                                            <td class="text_hui_11"><%=phone_n%></td>
                                                          </tr>
                                                          <tr>
                                                            <td class="text_hui_11">Business Phone:</td>
                                                            <td class="text_hui_11"><%=phone_d%></td>
                                                          </tr>
                                                          <tr>
                                                            <td class="text_hui_11">Mobile Phone: </td>
                                                            <td class="text_hui_11"><%=phone_c%></td>
                                                          </tr>
                                                          <tr>
                                                            <td class="text_hui_11">Country:</td>
                                                            <td class="text_hui_11">
                                                                <%= v_customer_country_code %>
                                                           </td>
                                                          </tr>
                                                          <tr>
                                                            <td class="text_hui_11">Address:</td>
                                                            <td class="text_hui_11"><%=v_customer_address1%></td>
                                                          </tr>
                                                          <tr>
                                                            <td class="text_hui_11">City:</td>
                                                            <td class="text_hui_11"><%= v_customer_city %></td>
                                                          </tr>
                                                          <tr>
                                                            <td class="text_hui_11">State (Province):</td>
                                                            <td class="text_hui_11"><%= v_state_code %>			
                                                            </td>
                                                          </tr>
                                                          <tr>
                                                            <td class="text_hui_11">Zip (Post) Code:</td>
                                                            <td class="text_hui_11"><%'=v_customer_card_zip_code%></td>
                                                          </tr>
                                                          <tr>
                                                            <td class="text_hui_11">Email1:</td>
                                                            <td class="text_hui_11"><%=email1%></td>
                                                          </tr>
                                                          <tr>
                                                            <td class="text_hui_11">Email2:</td>
                                                            <td class="text_hui_11"><%=email2%></td>
                                                          </tr>
                                                          <tr>
                                                            <td class="text_hui_11">eBay ID:</td>
                                                            <td class="text_hui_11"><%=EBay_ID%></td>
                                                          </tr>
                                                        </table></td>
                                                      </tr>
                                                      <tr>
                                                        <td style="cursor:hand;" onClick="showtr('reseller')" class="text_blue_11"><strong >Shipping Address--------------------------------------------------------------------------------------------</strong></td>
                                                      </tr>
                                                      <tr>
                                                        <td class="text_hui_11"><table width="95%" border="0" align="center" cellpadding="2" cellspacing="0">
                                                          
                                                          <tr>
                                                            <td width="24%" class="text_hui_11">First Name:</td>
                                                            <td class="text_hui_11"><%=customer_shipping_first_name%></td>
                                                          </tr>
                                                          <tr>
                                                            <td class="text_hui_11">Last Name:</td>
                                                            <td class="text_hui_11"><%=customer_shipping_last_name%></td>
                                                          </tr>
                                                          <tr>
                                                            <td class="text_hui_11">Country:</td>
                                                            <td class="text_hui_11"><%=shipping_country_code%></td>
                                                          </tr>
                                                          <tr>
                                                            <td class="text_hui_11">Address:</td>
                                                            <td class="text_hui_11"><%=customer_shipping_address%></td>
                                                          </tr>
                                                          <tr>
                                                            <td class="text_hui_11">City: </td>
                                                            <td class="text_hui_11"><%=customer_shipping_city%></td>
                                                          </tr>
                                                          <tr>
                                                            <td class="text_hui_11">State:</td>
                                                            <td class="text_hui_11">
                                                                <%= shipping_state_code %>
                                                           </td>
                                                          </tr>
                                                          <tr>
                                                            <td class="text_hui_11">Zip (Post) Code:</td>
                                                            <td class="text_hui_11">
                                                                <%= customer_shipping_zip_code %>
                                                           </td>
                                                          </tr>
                                                          </table>
                                                          </td>
                                                      </tr>
                                                      <tr>
                                                        <td style="cursor:hand;" onClick="showtr('reseller')" class="text_blue_11"><strong >BUSINESS-----------------------------------------------------------------------------------------------------</strong></td>
                                                      </tr>
                                                      <tr>
                    
                                                        <td><table style="display:''" width="95%" align="center" border="0" cellpadding="2" cellspacing="0" bgcolor="#ECF6FF" id="reseller">
                                                          <tr>
                                                            <td width="24%" class="text_hui_11">&nbsp;Company Name:</td>
                                                            <td><span class="text_hui_11">
                                                              <%=customer_Company%>
                                                            </span></td>
                                                          </tr>
                                                          <tr>
                                                            <td class="text_hui_11">&nbsp;Telephone:</td>
                                                            <td valign="top"><span class="text_hui_11">
                                                              <%=v_busniess_telephone%>
                                                            </span></td>
                                                          </tr>
                                                           <tr>
                                                            <td class="text_hui_11">&nbsp;Country:</td>
                                                            <td class="text_hui_11">
                                                                <%=v_customer_business_country_code %>
                                                           </td>
                                                          </tr>
                                                          <tr>
                                                            <td class="text_hui_11">&nbsp;Address:</td>
                                                            <td><span class="text_hui_11">
                                                              <%=v_customer_business_address%>
                                                            </span></td>
                                                          </tr>
                                                          <tr>
                                                            <td class="text_hui_11">&nbsp;City:</td>
                                                            <td><span class="text_hui_11">
                                                              <%=v_busniess_city%>
                                                            </span></td>
                                                          </tr>
                                                          <tr>
                                                            <td class="text_hui_11">&nbsp;State (Province):</td>
                                                            <td><%= v_customer_business_state_code %>		</td>
                                                          </tr>
                                                          <tr>
                                                            <td class="text_hui_11">&nbsp;Zip (Post) Code:</td>
                                                            <td><span class="text_hui_11">
                                                              <%=v_customer_business_zip_code %>
                                                            </span></td>
                                                          </tr>
                                                          <tr>
                                                            <td class="text_hui_11">&nbsp;Tax Exemption Number:</td>
                                                            <td><span class="text_hui_11">
                                                              <%=v_tax_execmtion%>
                                                            </span></td>
                                                          </tr>
                                                          <tr>
                                                            <td class="text_hui_11">&nbsp;Website Address:</td>
                                                            <td><span class="text_hui_11">
                                                              <%=v_busniess_website%>
                                                            </span></td>
                                                          </tr>
                                                        </table></td>
                                                      </tr>
                                                      <tr>
                                                        <td>									
                                                        <span class="text_hui_11">    <% if cstr(email_tag) = "1" then response.write " News Letter Subscribe" %>                                </span></td>
                                                      </tr>
                                                      <tr>
                                                        <td></td>
                                                      </tr>
                                                    </table>
                                                    <!--<div onclick="document.getElementById('update_info').style.display='';document.getElementById('view_info').style.display='none';" style="cursor: pointer; color:#FF6600">Revise</div>-->
                                                     <table id="__01" width="115" height="24" border="0" cellpadding="0" cellspacing="0" align="center">
                                                            <tr>
                                                              <td width="6"><img src="/soft_img/app/3232.gif" width="6" height="24"></td>
                                                              <td align="center" background="/soft_img/app/customer_bottom_03.gif"><a onclick="document.getElementById('update_info').style.display='';document.getElementById('view_info').style.display='none';return false;" class="btn_img"><strong>Revise</strong></a></td>
                                                              <td width="6"><img src="/soft_img/app/customer_bottom_04.gif" width="6" height="24" alt=""></td>
                                                            </tr>
                                                        </table>
                        </div>						
                        <!-- view info end -->
                                                </td>
                                            </tr>
                                            <tr>
                                              <td>&nbsp;</td>
                                            </tr>
                                            
                                          </table></td>
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
	
	infoChangeCountry(document.getElementById('user_Country'), '<%= v_state_code %>', 'shipping_state', 'div_state');
	infoChangeCountry(document.getElementById('shipping_country_code'), '<%= shipping_state_code %>', 'shipping_state_code', 'div_shipping_state_code');
	infoChangeCountry(document.getElementById('busniess_country'), '<%= v_customer_business_state_code %>', 'busniess_state', 'div_state2');
});

function infoChangeCountry(the, selected_state_code, element_id, set_element_id)
{
		showLoading();
		var country_code = the.options[the.selectedIndex].value;
		$('#'+set_element_id).load('/site/inc/inc_get_state.asp?is_have_event=true&state_selected_code='+ selected_state_code+'&element_state_id='+ element_id +'&country_code='+ country_code, function(){closeLoading();});
}

function showtr(id)
{
	var the = document.getElementById(id);
	if(the.style.display == "")
		the.style.display = "none";
	else
		the.style.display = "";
}
</script>