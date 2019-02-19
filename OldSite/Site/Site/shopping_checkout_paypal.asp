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
						Dim v_my_purchase_order				:	v_my_purchase_order			=	null
						Dim v_customer_shipping_country		:	v_customer_shipping_country	=	null
						Dim v_customer_shipping_zip_code	:	v_customer_shipping_zip_code=	null

							
						set rs = conn.execute("select * from tb_customer where customer_serial_no="& LAYOUT_CCID)
						if not rs.eof then
						
							v_zip_code 						= rs("zip_code")
							v_phone_d 						= rs("phone_d")
							v_phone_n 						= rs("phone_n")
							v_customer_email2 				= rs("customer_email2")
							v_customer_shipping_state 		= rs("customer_shipping_state")
							v_customer_first_name 			= rs("customer_shipping_first_name")
							v_customer_last_name 			= rs("customer_shipping_last_name")
							v_customer_shipping_city 		= rs("customer_shipping_city")
							v_customer_shipping_state 		= rs("shipping_state_code")
							v_customer_shipping_address 	= rs("customer_shipping_address")
							v_my_purchase_order 			= rs("my_purchase_order")
							v_customer_shipping_country 	= rs("shipping_country_code")
							v_customer_shipping_zip_code 	= rs("customer_shipping_zip_code")
							
						end if
						rs.close : set rs = nothing
					%>
                	<table width="600" height="670"  border="0" align="center" cellpadding="1" cellspacing="2" bgcolor="#FFFFFF" style="border:#8FC2E2 1px solid; ">
                            <tr>
                              <td valign="top" style="border:#E3E3E3 1px solid; "><table width="100%"  border="0" cellspacing="0" 
                
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
                              <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                  <td height="10"></td>
                                </tr>
                              </table>
                                <table width="98%"  border="0" align="center" cellpadding="0" cellspacing="0">
                                  <tr>
                                    <td><strong>&nbsp;&nbsp;&nbsp;PAYING BY PAYPAL</strong></td>
                                    <td align="right"><strong>Please call us for any questions:</strong><br>
                                      Toll Free: 1 (866) 999-7828<br>
                                      Toronto Local: (416) 446-7828</td>
                                  </tr>
                                </table>
                                <table width="80%"  border="0" align="center" cellpadding="0" cellspacing="0">
                                  <tr>
                                    <td class="text_hui_11" style="padding-left:30px; ">&nbsp;</td>
                                  </tr>
                                </table>
                                <table width="80%" style="border-bottom:#327AB8 1px solid; "  border="0" align="center" cellpadding="0" cellspacing="0">
                                  <tr>
                                    <td>&nbsp;</td>
                                  </tr>
                                </table>
                                <div id="text_area">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                  <tr>
                                    <td>&nbsp;</td>
                                  </tr>
                                </table>
                                
                                <table width="80%"  border="0" align="center" cellpadding="0" cellspacing="0">
                                  <tr>
                                    <td class="text_hui_11">&nbsp;</td>
                                  </tr>
                                  <tr>
                                    <td class="text_hui_11"><strong>CONTACT INFORMATION: </strong></td>
                                  </tr>
                                </table>
                                <table width="80%"  border="0" align="center" cellpadding="2" cellspacing="0">
                                  <tr>
                                    <td width="26%" class="text_hui_11"> Secondary Email Address: </td>
                                    <td valign="top"><%= v_customer_email2%></td>
                                  </tr>
                                  <tr>
                                    <td class="text_hui_11">Telephone 1:</td>
                                    <td><font face="Tahoma"> <span style="font-size: 12px;"><%= v_phone_d %></span></font></td>
                                  </tr>
                                  <tr>
                                    <td class="text_hui_11">Telephone 2: </td>
                                    <td><font face="Tahoma"> <span style="font-size: 12px;"><%= v_phone_n %></span></font></td>
                                  </tr>
                                  <tr>
                                    <td class="text_hui_11" colspan="2">&nbsp;</td>
                                  </tr>
                
                                </table>
                                <table width="80%"  border="0" align="center" cellpadding="2" cellspacing="0">
                                  
                                  <tr>
                                    <td width="26%" class="text_hui_11">&nbsp;</td>
                                    <td align="right" valign="top"><span class="text_hui_11" id="div_state3"><a style="color: blue; cursor:pointer" onclick="ChangeArea();">Change</a></span></td>
                                  </tr>
                                </table>
                                </div>                
                                
                                <form name="form2" id="form2" method="post" action="<%=LAYOUT_HOST_URL%>checkout_paypal_website_payments_pro_setExpressCheckout.asp">
                                <!--form name="form2" id="form2" method="post" action="checkout_email_exec.asp"-->
                
                                <input type="hidden" name="cmd" value="update">
                                <input type="hidden" name="paypal" value="true">
                                <div id="form_area" style="display:none">
                                <%
                            
                                
                                            'if isnumeric(v_customer_shipping_state) and isnumeric(request.Cookies("state_shipping")) then 
                '								if cstr(v_customer_shipping_state) <> cstr(request.Cookies("state_shipping")) then 
                '										v_customer_shipping_address = ""
                '										v_customer_shipping_city = ""
                '										v_customer_shipping_zip_code = ""
                '								end if
                '							end if
                                %>
                                  <!----><table width="80%"  border="0" align="center" cellpadding="0" cellspacing="0">
                                    <tr>
                                      <td class="text_hui_11">&nbsp;</td>
                                    </tr>
                                    <tr>
                                      <td class="text_hui_11"><strong>SHIPPING ADDRESS: </strong></td>
                                    </tr>
                                  </table>
                                  <table width="80%"  border="0" align="center" cellpadding="2" cellspacing="0">
                                  <tr>
                                    <td class="text_hui_11"> First name:<span class="STYLE1">*</span></td>
                                    <td valign="top"><span class="text_hui_11">
                                      <input name="shipping_first_name" type="text" class="input9pt" id="shipping_first_name" style="width:150px; " tabindex="1"   value="<%= v_customer_first_name%>" size="13">
                                    </span></td>
                                    </tr>
                                  <tr>
                                      <td width="26%" class="text_hui_11">Last Name:<span class="STYLE1">*</span></td>
                                      <td valign="top"><span class="text_hui_11">
                                        <input name="shipping_last_name" type="text" class="input9pt" id="shipping_last_name" style="width:150px; " tabindex="2"   value="<%= v_customer_last_name%>" size="13">
                                      </span></td>
                                    </tr>
                                    <tr>
                                      <td width="26%" class="text_hui_11">Address:<span class="STYLE1">*</span>　</td>
                                      <td valign="top"><span class="text_hui_11">
                                       
                                        <textarea  name="shipping_address" cols="40" rows="2" id="shipping_address" style="font-family:Tahoma; font-size:9pt;color:#333333"  tabindex="3"><%= v_customer_shipping_address%></textarea>
                                      </span></td>
                                    </tr>
                                    <tr>
                                      <td class="text_hui_11">Country:<span class="STYLE1">*</span> </td>
                                      <td valign="top"><span class="text_hui_11">
                                        <input name="shipping_country" type="hidden" value="<%=LAYOUT_SHIPPING_COUNTRY_CODE %>">
                                        <%= GetCountryName(LAYOUT_SHIPPING_COUNTRY_CODE) %>
                                      </span></td>
                                    </tr>
                                    <tr>
                                      <td class="text_hui_11">City:<span class="STYLE1">*</span> </td>
                                      <td valign="top"><span class="text_hui_11">
                                        <input name="shipping_city" type="text" class="input9pt" id="shipping_city" style="width:150px; " size="13"  value="<%= v_customer_shipping_city %>" tabindex="5">
                                      </span></td>
                                    </tr>
                                    <tr>
                                      <td class="text_hui_11">State (Province) :<span class="STYLE1">*</span> </td>
                                      <td valign="top"><span class="text_hui_11" id="div_state">
                                       <input name="shipping_state" type="hidden" value="<%=LAYOUT_SHIPPING_STATE_CODE %>"><%= (LAYOUT_SHIPPING_STATE_CODE) %>&nbsp;&nbsp;<a href="Shopping_Cart.asp" style="color: blue;">Change</a>
                                      </span> <span id="shipping_state_alter" style="display:none;"></span> </td>
                                    </tr>
                                    <tr>
                                      <td class="text_hui_11">Zip (Post) Code :<span class="STYLE1">*</span> </td>
                                      <td valign="top"><span class="text_hui_11">
                                        <input name="zip_code" type="text" class="input9pt" style="width:150px; "  size="13" maxlength="7" value="<%= v_customer_shipping_zip_code %>" tabindex="7">
                                      </span></td>
                                    </tr>
                                  </table>
                                  <table width="80%"  border="0" align="center" cellpadding="0" cellspacing="0">
                                    <tr>
                                      <td class="text_hui_11">&nbsp;</td>
                                    </tr>
                                    <tr>
                                      <td class="text_hui_11"><strong>CONTACT INFORMATION: </strong></td>
                                    </tr>
                                  </table>
                                  <table width="80%"  border="0" align="center" cellpadding="2" cellspacing="0">
                                    <tr>
                                      <td width="26%" class="text_hui_11"> Secondary Email Address: </td>
                                      <td valign="top"><span class="text_hui_11">
                                        <input name="secondary_email" type="text" class="input9pt" size="13" style="width:150px; "  value="<%= v_customer_email2%>" tabindex="8">
                                      </span></td>
                                    </tr>
                                    <tr>
                                      <td class="text_hui_11">Telephone 1:<span class="STYLE1">*</span> 　 </td>
                                      <td valign="top"><span class="text_hui_11">
                                        <input name="phone_d" type="text" class="input9pt" size="13" style="width:150px; " value="<%= v_phone_d %>" tabindex="9">
                                        <%= LAYOUT_PHONE_FORMAT %></span></td>
                                    </tr>
                                    <tr>
                                      <td class="text_hui_11">Telephone 2: </td>
                                      <td valign="top"><span class="text_hui_11">
                                        <input name="phone_n" type="text" class="input9pt" size="13" style="width:150px; "  value="<%= v_phone_n %>" tabindex="10">
                                        <%= LAYOUT_PHONE_FORMAT %></span></td>
                                    </tr>
                                    <tr>
                                      <td class="text_hui_11" colspan="2">&nbsp;</td>
                                    </tr>
                                    <!--<tr>
                                      <td class="text_hui_11">My Purchase Order #</td>
                                      <td><input name="my_purchase_order" type="text"  class="input" style="width:150px; " size="13" maxlength="50"   value="<%= v_my_purchase_order %>" tabindex="11"></td>
                                    </tr>-->
                                  </table>
                                  </div>
                                  <table width="80%"  border="0" align="center" cellpadding="0" cellspacing="0">
                                    <tr>
                                      <td class="text_hui_11"><strong>CUSTOMER COMMENT / NOTE:</strong></td>
                                    </tr>
                                  </table>
                                  <table width="80%"  border="0" align="center" cellpadding="2" cellspacing="0">
                                    <tr>
                                      <td align="center" class="text_hui_11"><textarea name="note" cols="40" rows="5"  style="font-family:Tahoma; font-size:9pt;color:#333333" tabindex="12"></textarea>
                                      </td>
                                    </tr>
                                  </table>
                                  </form>
                                  
                                <table width="98%" style="border-bottom:#327AB8 1px solid; "  border="0" align="center" cellpadding="0" cellspacing="0">
                                  <tr>
                                    <td>&nbsp;</td>
                                  </tr>
                                </table>
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                  <tr>
                                    <td height="40"></td>
                                  </tr>
                                </table>
                                <table width="100%"  border="0" align="left" cellpadding="3" cellspacing="0">
                                  <tr>
                                    <td height="70" align="center" valign="top"><table width="45%"  border="0" cellpadding="3" cellspacing="0">
                                      <tr align="right">
                                        <td><table id="__01" width="130" height="24" border="0" cellpadding="0" cellspacing="0" class="btn_table"  onclick="gotoBackCart('<%= LAYOUT_CURRENT_ORDER_TYPE %>');">
                                            <tr>
                                              <td width="28"><img src="/soft_img/app/arrow_left.gif" width="28" height="24" alt=""></td>
                                              <td align="center" background="/soft_img/app/customer_bottom_03.gif"  class="btn_style"><strong><a style="cursor: pointer;color: #FFFFFF;"  onclick="gotoBackCart('<%= LAYOUT_CURRENT_ORDER_TYPE %>');" tabindex="13">Back</a> </strong></td>
                                              <td width="6"><img src="/soft_img/app/customer_bottom_04.gif" width="6" height="24" alt=""></td>
                                            </tr>
                                        </table></td>
                                        <td><table id="__01" width="130" height="24" border="0" cellpadding="0" cellspacing="0" class="btn_table" onClick="Check(document.getElementById('form2'));" >
                                            <tr>
                                              <td width="28"><img src="/soft_img/app/arrow_right.gif" width="28" height="24" alt=""></td>
                                              <td align="center" background="/soft_img/app/customer_bottom_03.gif"><a style="cursor: pointer;color: #FFFFFF;" onClick="Check(document.getElementById('form2'));" onFocus="this.style.color='white';" onBlur="this.style.color='white'"  class="btn_style" tabindex="14"><strong>Next</strong></a></td>
                                              <td width="6"><img src="/soft_img/app/customer_bottom_04.gif" width="6" height="24" alt=""></td>
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
	
	if($('#shipping_country').val() == '')
	{
		alert(err);
		$('#shipping_country').focus();		
		return false;
	}
	
	if($('#shipping_city').val() == '')
	{
		alert(err);
		$('#shipping_city').focus();		
		return false;
	}
	
	if($('#shipping_state').val() == '')
	{
		alert(err);
		$('#shipping_state').focus();		
		return false;
	}
	
	if($('#zip_code').val() == '')
	{
		alert(err);
		$('#zip_code').focus();		
		return false;
	}
	if($('#phone_d').val() == '')
	{
		alert(err);
		$('#phone_d').focus();		
		return false;
	}
	$('#form2').submit();
}
</script>