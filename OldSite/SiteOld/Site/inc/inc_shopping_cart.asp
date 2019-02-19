<!--#include virtual="site/no_express.asp"-->
<!--#include virtual="site/inc/inc_helper.asp"-->
<div style='text-align:left; clear:both'>
           
            <table width="100%" height="670"  border="0" cellpadding="1" cellspacing="2" bgcolor="#FFFFFF" style="border:#8FC2E2 1px solid;">
              	<tr>
                		<td valign="top" style="border:#E3E3E3 1px solid; ">
                    			<table width="100%"  border="0" cellspacing="0" cellpadding="0">
                          				<tr>
                            					<td height="5" bgcolor="#ff6600" style='color:white'>&nbsp;&nbsp;<span class="text_white"><strong>1. Delivery Options</strong></span></td>
                                                <td width="17"><img src="/soft_img/app/shop_arrow_red.gif" width="17" height="23"></td>
                                                <td bgcolor="#e8e8e8">&nbsp;&nbsp;2. Pay Methods</td>
                                                <td width="16"><img src="/soft_img/app/shop_arrow_gray.gif" width="17" height="23"></td>
                                                <td bgcolor="#E8E8E8">&nbsp;&nbsp;3. Personal Information</td>
                                                <td width="17"><img src="/soft_img/app/shop_arrow_gray.gif" width="17" height="23"></td>
                                                <td bgcolor="#e8e8e8">&nbsp;&nbsp;4. Submit</td>
                          				</tr>
                  
                    			</table>
                                <form name="FM" id="FM" action="<%= LAYOUT_HOST_URL %>modify_cart_quantity.asp" method="post">
                                <input type='hidden' name='returnURL' value='<%= LAYOUT_HOST_URL %>shopping_cart.asp'/>
                 				<table width="100%"  border="0" cellspacing="0" cellpadding="0">					
                      					<tr>
                        						<td><img src="/soft_img/app/iteam_title.gif" width="230" height="25"></td>
                      					</tr>
                      					<tr>
                        						<td>
                                                		<table width="100%"  border="0" cellspacing="0" cellpadding="0">
                                                                <tr>
                                                                  <td height="5" bgcolor="#256AB1"></td>
                                                                </tr>
                                                                <tr>
                                                                  	<td height="25" valign="top" bgcolor="#E8E8E8">
                                                                    	<table width="100%"  border="0" cellspacing="0" cellpadding="2">
                                                                            <tr>
                                                                              <td width="11%" style="padding-left:5px;">SKU</td>
                                                                              <td width="45%" style="border-left:#256AB1 1px solid; padding-left:5px; ">Description</td>
                                                                              <td  width="15%" style="border-left:#256AB1 1px solid;  padding-left:5px;">QTY</td>
                                                                              <td width="11%" style="border-left:#256AB1 1px solid;  padding-left:5px;">Unit Price</td>
                                                                              <td width="11%" style="border-left:#256AB1 1px solid;  padding-left:5px;">Amount</td>
                                                                              <td style="border-left:#256AB1 1px solid;  padding-left:5px;">Delete</td>
                                                                            </tr>
                             											</table>
                                                                    </td>
                            									</tr>
                        								</table>
                                                </td>
                      					</tr>
                      					<tr>
                        						<td><!-- Cart start-->
                            <%

						dim category, cart_is_null
						cart_is_null = true
						category = SQLescape("category")
						
						'Call ValidateOrder_Code("site")
						
						
						if len(LAYOUT_ORDER_CODE)  = LAYOUT_ORDER_LENGTH then
						Dim part_save_price 	:	part_save_price	=	0

						ChangeTempOrderSystemPrice(LAYOUT_ORDER_CODE)
											
						set rs = conn.execute("select c.*, ifnull(ps.prod_sku, 0) lu_sku  from tb_cart_temp  c left join tb_product_shipping_fee ps "&_
" on ps.prod_sku=c.product_serial_no and ps.is_system=0 where c.cart_temp_code='"&LAYOUT_ORDER_CODE&"'")
						if not rs.eof then
						price_unit = rs("price_unit")
						n = 0
						dim subtotal, amount, ship_price, len_id
						DIm SKU
						
						subtotal = 0
						ship_price = 0
						quantity = 0
						len_id = 0
						
						do while not rs.eof 						
							SKU	= rs("product_serial_no")
                            dim single_price :		single_price	=	0

							if len(SKU) < LAYOUT_SYSTEM_CODE_LENGTH then 
							'
							'	if it is part, and update price.
							'							
								part_save_price	=	GetPartOnSaleSavePrice(sku)
								
								
								Set crs = conn.execute("Select product_current_cost,product_current_special_cash_price,product_current_price  from tb_product p where  product_serial_no='"& SKU &"'")
								if not crs.eof then
									single_price = ConvertDecimal(cdbl(crs("product_current_price"))- cdbl(part_save_price))
									
									CONN.execute("update tb_cart_temp ct, tb_product p "&_
												" Set ct.price="& ConvertDecimal( cdbl(crs("product_current_special_cash_price"))- part_save_price) &_
												", ct.save_price="&ConvertDecimal( cdbl(part_save_price))&_
												", ct.price_rate="& ConvertDecimal( cdbl(crs("product_current_price"))-cdbl(part_save_price))&_
												", ct.cost=p.product_current_cost where p.product_serial_no=ct.product_serial_no and p.product_serial_no="& SQLquote(SKU)&_
												" and cart_temp_code="& SQLquote(LAYOUT_ORDER_CODE))
									
								end if
								crs.close : set crs = nothing
							else
                                'single_price = cdbl(splitConfigurePrice(GetSystemPriceAndSave(rs("product_serial_no")), 0))
								single_price= cdbl(rs("price_rate"))
								'Response.write rs("price_rate")
							end if
							
						cart_is_null = false
						quantity = quantity + rs("cart_temp_Quantity")
						n = n+1
						
						amount = single_price * cint(rs("cart_temp_Quantity"))
						subtotal = subtotal + amount
						len_id = len(rs("product_serial_no"))
						
						if rs("lu_sku") >0 then
						    is_sale_promotion_shipping_charge = true
						end if
						%>
                            <table width="100%"  border="0" cellpadding="2" cellspacing="0" >
                              <tr>
                                <td valign="top"><table width="100%"  border="0" cellspacing="0" cellpadding="2" style="border-bottom:1px dotted #666666;">
                                  <tr>
                                    <td width="11%" style="padding-left:5px; height:40px">
                                        <%
									  if len(rs("product_serial_no")) = 8  then 
									  	response.write "<a href="""& LAYOUT_HOST_URL&"view_configure_system.asp?cmd=print&system_code="&rs("product_serial_no")&""" onClick=""return js_callpage_cus(this.href, 'view_system', 620, 600)"" class=""hui_orange_11"">"&GetSysOldSKUbyUncomtusize(rs("product_serial_no"))&"</a>"
									  else
									  	response.write rs("product_serial_no")
									  end if%>
                                        <%' if category = "sys" then %>
<%'else%>
                                        <!--img src="pro_img/components/<%=rs("product_serial_no")%>_t.jpg" width="50" border="0"  onerror="imgerror(this);"-->
                                        <%' end if%>
                                        <input type="hidden" name="Order_Ids" value="<%=rs("cart_temp_serial_no")%>">
                                    </td>
                                    <td width="45%" class="text_hui_11">
                                      <%
									  if len(rs("product_serial_no")) = 8 then 
									  	response.write "<a href="""& LAYOUT_HOST_URL&"view_configure_system.asp?cmd=print&system_code="&rs("product_serial_no")&""" onClick=""return js_callpage_cus(this.href, 'view_system', 620, 600)"" class=""hui_orange_11"">"&rs("product_name")&"</a>"
									  else
									  	response.write rs("product_name")
                                        if rs("prodType") <> "New" then response.Write "&nbsp;"& rs("prodType")
									  end if%>
                                    </td>
                                    <td width="13%">
                                            <input name="quantity"  type="text" class="input" id="quantity"  style=" width:20px;" onKeyDown="return keydownevent();"  value="<%=rs("cart_temp_Quantity")%>" size="3" maxlength="3"><input type='button' value="Update" onclick="document.getElementById('FM').submit();" style="width:45px;padding:1px"/>
                                     </td>
                                    <td width="11%" class="text_orange_13" style="text-align:right"><%= formatcurrency((single_price), 2)%></td>
                                    <td width="11%" class="text_orange_13" style="text-align:right"><%= formatcurrency(( amount), 2)%></td>
                                    <td><a  onclick="delCart('<%= rs("cart_temp_serial_no") %>', '<%= LAYOUT_HOST_URL %>shopping_cart.asp');" ><img 

src="/soft_img/app/delect_bt.gif" width="48" height="18" border="0" style="cursor:pointer"></a></td>
                                  </tr>
                                </table></td>
                              
                              </tr>
                            </table>
                            <!---cart end-->
                            <%
						rs.movenext
						loop
						end if
						rs.close : set rs = nothing
						end if		
					%>
                            
                            <table width="100%" height="27"  border="0" cellpadding="0" cellspacing="0">
                              <tr>
                                <td align="right" bgcolor="#EFEFEF" class="text_hui_11" style="border-top:#DDDDDD 1px solid; border-bottom:#DDDDDD 

1px solid;"><strong>Sub Total: </strong><span class="text_red_12b"><%=formatcurrency( subtotal, 2)%><span class="price_unit"><%= CCUN %></span></span>&nbsp;&nbsp;</td>
                              </tr>
                          </table></td>
                      </tr>
                    </table>
                     </form>
                     
                   <form action="<%= https_path_var %>Shopping_check_Out.asp" method="post" name=buy id="buy">
                   		<input type='hidden' name="payment" id='payment' value="ON" />

                  <table width="100%"  border="0" cellspacing="0" cellpadding="0">
                    <tr>
                      <td >
                            
                          <table width="100%"  border="0" 

align="center" cellpadding="2" cellspacing="0" style="margin-top:10px;">
							<tr>
                            	<td width="30%"></td>
                                <td style="line-height:15px; font-size:10pt;">&nbsp;</td>
                            </tr>
							<tr>
                            	<td width="30%"></td>
                                <td style="line-height:15px; font-size:10pt;"><b>How would you like to receive your order?</b></td>
                            </tr>
                            <tr>
                            	<td></td>
                                <td style="line-height:15px; font-size:10pt;">&nbsp;<input type='radio'  name="cart_radio_pick_or_shipping" value="0"  onclick="initialSelected(0);"/>I will pick up my order and pay at your store.</td>
                            </tr>
                            <tr>
                            	<td></td>
                                <td style="line-height:15px; font-size:10pt;">&nbsp;<input type='radio'  name="cart_radio_pick_or_shipping" value="1"  onclick="initialSelected(1)"/>Please ship my items to me.</td>
                            </tr>
                            <tr style="display:none" class="cart_shipping_info">
                            	<td></td>
                                <td style="color:#EF5412; cursor:pointer">&nbsp;&nbsp;&nbsp;<span style="color:#EF5412; ">The following items must be selected.</span></td>
                            </tr>
                            <tr>
                            	<td></td>
                                <td style="line-height:15px; font-size:10pt;">&nbsp;</td>
                            </tr>
                            <tr style="display:none" class="cart_shipping_info">
                              
                              <td align="right" class="text_hui_11"><b><span style="color:#EF5412;">*</span>Please select destination:</b></td>
                              <td style="padding-left:12px;">
							  	
                                <%
								Dim country_state_shipping
								country_state_shipping	=	GetOrderCountryCodeAndStateIDAndShipingCompanyID(LAYOUT_ORDER_CODE)
							  ' 取得 临时购物袋的值 state
							  dim current_state_code
							  current_state_code = splitConfigurePrice(country_state_shipping, 1)

							  
							  ' 取得 临时购物袋的值 shipping_company
							   dim shipping_company							
							   shipping_company = splitConfigurePrice(country_state_shipping, 2)
							  
							  	
							  Dim current_country_code		:	current_country_code	=	splitConfigurePrice(country_state_shipping, 0)
							  'Response.write current_state_code &"|"& current_country_code &"|"& shipping_company
							
							  %>
                                <select name="country" id="country" onChange="cartChangeCountry(null, '<%= LAYOUT_ORDER_CODE %>', null, null);" >    
                                            <option value='-1'>-- Select --</option>
                                            <optgroup label="Canada">                            	
                                            <%
                                                Set rs = conn.execute("Select state_code, state_name , state_serial_no "&_
                                                                        " from tb_state_shipping "&_
                                                                        " where system_category_serial_no=1"&_
                                                                        " order by  priority asc ")
                                                If not rs.eof then
                                                    Do while not rs.eof 
                                                        Response.write "<option value='"& rs("state_code") &"' "
                                                        if SQLescape(current_state) = cstr(rs("state_code")) then Response.write " selected = 'true' "
														response.Write " tag='CA' "
                                                        Response.write " >"& rs("state_code") &" - "& rs("state_name") &"</option>"
                                                    rs.movenext
                                                    loop
                                                end if
                                                rs.close : set rs = nothing
                                            %>
                                            </optgroup>
                                            <optgroup label="USA">                            	
                                            <%
                                                Set rs = conn.execute("Select state_code, state_name , state_serial_no "&_
                                                                        " from tb_state_shipping "&_
                                                                        " where system_category_serial_no=2"&_
                                                                        " order by  priority asc ")
                                                If not rs.eof then
                                                    Do while not rs.eof 
                                                        Response.write "<option value='"& rs("state_code") &"' "
                                                        if SQLescape(current_state) = cstr(rs("state_code")) then Response.write " selected = 'true' "
														response.Write " tag='US' "
                                                        Response.write " >"& rs("state_code") &" - "& rs("state_name") &"</option>"
                                                    rs.movenext
                                                    loop
                                                end if
                                                rs.close : set rs = nothing
                                            %>
                                            </optgroup>
                                </select>
                                </td>
                            </tr>

                            <tr style="display:none" class="cart_shipping_info">
                              <td align="right" class="text_hui_11" valign="top"><b><span style="color:#EF5412;">*</span>Please select ship method:</b></td>
                              <td class="text_hui_11" valign="top"  style="padding-left:5px; text-align:left">
								<span id="shipping_compay_area" ><!--<span style="color:#EF5412;">&nbsp;Please select Destination State/Province</span>--></span>
							  </td>
                            </tr>
                            <tr style="display:none" class="cart_shipping_info">
                            	<td colspan="2">
                                	<hr size="1" style='border:0; border-bottom:1px dotted #666666;'/>
                                </td>
                            </tr>
                            <tr style="display:none" class="cart_shipping_info">
                              <td align="right" class="text_hui_11" valign="top"><b><span style="color:#EF5412;">*</span>Please select pay method:</b></td>
                              <td class="text_hui_11" valign="top" style="padding-left:5px;">
								<span id="cart_pay_method_area"><!--<span style="color:#EF5412;">&nbsp;&nbsp;Please select ship method</span>--></span>
							  </td>
                            </tr>
                            
                          </table>
                          <input type=hidden name=sc2 value="<%=sc%>">
                          <input type=hidden name=price2 value="<%

=Total_price%>">                      </td>
                    </tr>
                    <tr>
                      <td>
                      <br />
                      	<div style="background:#EFEFEF; border-bottom:1px solid #dddddd; border-top: 1px solid #dddddd;">
                          <div id="charge_area"></div>
                          
                        </div>
					 </td>
                    </tr>					
                  </table>
                  <table width="100%" height="50"  border="0" cellpadding="0" cellspacing="0">
                    <tr>
                      <td colspan="3">&nbsp;</td>
                    </tr>                    
                    <tr>
                      <td width="4%" align="center"></td>
                      <td class="text_hui_11">
                              <table width="110" height="24" border="0" class="btn_table" align="right" cellpadding="0" cellspacing="0" id="__01" onclick="window.location.href='/site/clear_cart.asp';">
                                <tr>
                                  <td width="28"><img src="/soft_img/app/clear.gif" width="28" height="24" alt=""></td>
                                  <td align="center" background="/soft_img/app/customer_bottom_03.gif"><!--a href="Cartexec.asp?action=recalculate" onClick="if(confirm('are you sure!')){ return true; } else return false;" class="white-hui-12"-->
                                    <a href="/site/clear_cart.asp" class="btn_img"><strong>Clear Cart</strong></a> </td>
                                  <td width="6"><img src="/soft_img/app/customer_bottom_04.gif" width="6" height="24" alt=""></td>
                                </tr>
                              </table>
                      <br>
                      </td>
                      <td width="50%">					  
                              <table width="45%"  border="0" align="right" cellpadding="5" cellspacing="0">
                                <tr align="right">
                                   <td>
                                        <table id="__01" width="170" height="24" border="0" cellpadding="0" cellspacing="0" class="btn_table" onclick="window.location.href='/site/';">
                                              <tr>
                                                <td width="28"><img src="/soft_img/app/buy_car.gif" width="28" height="24" alt=""></td>
                                                <td align="center" background="/soft_img/app/customer_bottom_03.gif">
                                                <!--a href="Cartexec.asp?action=recalculate" onClick="if(confirm('are you sure!')){ return true; } else return false;" class="white-hui-12"-->
                                                <a href="/site/" class="btn_img"><strong>Continue Shopping</strong></a> </td>
                                                <td width="6"><img src="/soft_img/app/customer_bottom_04.gif" width="6" height="24" alt=""></td>
                                              </tr>
                                        </table>
                                  </td>
                                  <td>
                                        <table id="cart_next_table" width="140" height="24" border="0" cellpadding="0" cellspacing="0" class="btn_table" <% if cart_is_null then %>
                                         onClick="alert('this cart is empty!');"
                                         <% else %>
                                        	onClick="gotoCart();" 
                                            <% end if %>>
                                          <tr>
                                            <td width="28"><img src="/soft_img/app/pay.gif" width="28" height="24" alt=""></td>
                                            <td align="center" background="/soft_img/app/customer_bottom_03.gif">
                                           
                                            <a class="btn_img" id="cart_next" ><strong>Check out</strong></a> </td>
                                            
                                            <td width="6"><img src="/soft_img/app/customer_bottom_04.gif" width="6" height="24" alt=""></td>
                                          </tr>
                                        </table>
                                  </td>
                                </tr>
                              </table>
                              
                        
                      
                          </td>
                        </tr>
                      
                  </table>      
                  <table width="100%"  border="0" cellspacing="0" cellpadding="4">
                  	 <tr>
                            <td valign="top" class="text_hui_11" style="padding:5px; "><strong>You can find out the total amount BEFORE you check out.</strong> Please select your destination state / province and shipping method below.</td>
                          </tr>
                         <tr>
                          <td height="5"></td>
                        </tr>  
                        <tr>
                          <td class="text_hui_11">Orders are processed and shipped Monday through Friday. In-stock items and special orders (when available) are usually shipped immediately. You will be notified for any items if not shipped right away. Computer systems are usually shipped in 1-7 business days. But fast shipping is not guaranteed. LU Computers is a fast shipper; we take every effort to ship your item as soon as possible.</td>
                        </tr>
                        <tr>
                          <td height="5"></td>
                        </tr>
                        <tr>
                          <td class="text_hui_11"><strong>Shopping with LU Computers is safe and secure!  </strong></td>
                        </tr>
                        <tr>
                          <td class="text_hui_11">                            To protect your transaction, we use GeoTrust's service and 128-bit Secure Sockets Layer (SSL) technology, thereby offering the highest level of encryption or security possible. This means you can rest assured that communications between your browser and this site's web servers are private and secure, and your personal information is also stored securely in our server. </td>
                        </tr>
                        <tr>
                            <td class="text_hui_11">
                                LU Computers reserves the right to change above shipping fees if the actual shipping costs are significantly greater than above estimate.
                            </td>
                        </tr>
                        
                    </table>
                  <table width="100%"  border="0" cellspacing="0" cellpadding="0">
                    <tr>
                      <td>&nbsp;</td>
                    </tr>
                  </table></form></td>
              </tr>
            </table>
            
            <table width="600"  border="0" align="center" cellpadding="0" cellspacing="0">
              <tr>
                <td height="5"></td>
              </tr>
            </table>  
           
            <input type="hidden" id="RedirectCheckOut" value="0" />
</div>
<%
CloseConn()
%>
<script type="text/javascript">
$.ajaxSetup ({
    cache: false 
});

$().ready(function(){
	if ($('#country').val() != "0")	
	bindHoverBTNTable();
});

function cartChangeCountry(shipping_company, order_code, state_selected_code, is_no_load_payment)
{
	showLoading();
	
	var state_code = 	$('#country  option:selected').val();
	var country_code = 	$('#country  option:selected').attr('tag');
	cartLoadPayment(order_code, country_code,shipping_company, state_code )
	cartChangeState(null, shipping_company, order_code);
}



function cartChangeState(the, shipping_company, order_code){
	showLoading();
	var v = null;
	v = $('#country option:selected').val();
	
	var country_code = $('#country  option:selected').attr('tag');

	$('#shipping_compay_area').load('/site/inc/inc_get_shipping_company.asp?state_code='+v+'&country_code='+ country_code +'&shipping_company='+ shipping_company+'&order_code='+ order_code+'&'+ rand(1000)
		, function(){
			//alert(shipping_company);
			//alert(v);	
			cartChangeCharge(order_code, country_code,shipping_company, v );
			
		});
	$('#charge_area').hide();
}



function cartLoadPayment(order_code, country_code,shipping_company, state_id )
{
	//$('#cart_pay_method_area').html('/site/inc/inc_get_payment_area.asp?country_code='+ country_code +"&"+rand(1000));
	$('#cart_pay_method_area').load('/site/inc/inc_get_payment_area.asp?sub_total='+ <%= subtotal %> +'&order_code='+ order_code +'&country_code='+ country_code +"&"+rand(1000)
		
		,function(){
			closeLoading();
			$('#cart_pay_method_area').show('slow');
			$('input[name=pay_method]').unbind('click'	);		
			$('input[name=pay_method]').bind('click',
				function(){cartChangeCharge(order_code, country_code,shipping_company, state_id , false);}
				);
		});
}

function cartChangeCharge(order_code, country_code,shipping_company, sate_shipping , is_pick_up)
{
	showLoading();
	
	var RedirectCheckOut = $('#RedirectCheckOut').val();
	//
	// payment
	//
	var payment = $('input:radio[name=pay_method]:checked').val();
	
	if (is_pick_up)
		payment	=	'<%=LAYOUT_PAY_PICKUP_VALUE %>';

	// if is_pick_up is true, and first pickup
	if (parseInt(payment) == <%=LAYOUT_PAY_PICKUP_VALUE %> && !is_pick_up)
	{
		if($('#state_shipping').val() != "ON")
		{
			$('select[name=country] option[value=CA]').attr('checked', true);
			cartChangeCountry( -1, order_code, "ON", true);
		}

	}
	
	//
	//	shipping company
	//
	shipping_company  = $('input:radio[name=shipping_company]:checked').val();
	
	if((payment == '<%=LAYOUT_PAY_PICKUP_VALUE %>' )|| (parseInt(payment)>0 && parseInt(shipping_company) > 0 ))
	{
		$('#charge_area').hide();
		
		$('#charge_area').html('/AccountCharge_new.aspx?Pay_method='+ payment +'&tmp_code='+order_code+'&country_id='+country_code+'&shipping_company='+shipping_company+'&sate_shipping='+sate_shipping+'&category=<%=request("category")%>&tax_execmtion=<%= request.Cookies("tax_execmtion") %>&RedirectCheckOut='+ RedirectCheckOut +'&price_unit=<%= price_unit %>&current_system=<%= Current_System %>&'+ rand(1000) );
		
		$('#charge_area').load('/AccountCharge_new.aspx?Pay_method='+ payment +'&tmp_code='+order_code+'&country_id='+country_code+'&shipping_company='+shipping_company+'&sate_shipping='+sate_shipping+'&category=<%=request("category")%>&tax_execmtion=<%= request.Cookies("tax_execmtion") %>&RedirectCheckOut='+ RedirectCheckOut +'&price_unit=<%= price_unit %>&current_system=<%= Current_System %>&'+ rand(1000) 
			, function(){
				cartViewChargeArea(order_code);
		});
	}
	else
		closeLoading();
		
}

function cartViewChargeArea(order_code)
{
	//$('#charge_area').html('/site/inc/inc_get_order_charge_cart.asp?order_code='+order_code+'&is_get_sur_charge=true&'+ rand(1000) )
	$('#charge_area').load('/site/inc/inc_get_order_charge_cart.asp?order_code='+order_code+'&is_get_sur_charge=true&'+ rand(1000) 
			, function(){
				closeLoading();
				$('#charge_area').show('slow');
				
		});
}

function gotoCart(t)
{
	if (<%= lcase(cart_is_null) %>)
		return;
	
	var checked = parseInt($('input:radio[name=shipping_company]:checked').val()) >0;
  
	var pay_method = $('input:radio[name=pay_method]:checked').val();
	
	//return;
	
	var cart_radio = $('input[name=cart_radio_pick_or_shipping][checked]').val();
	$("input[name=cart_radio_pick_or_shipping]").each(function(i){ if ($(this).attr("checked")) { cart_radio = $(this).attr("value"); }});
	
	if (cart_radio != "0" && cart_radio != "1")
	{
		alert('Please select above options, thank you.');
		return;
	}	
	else if (cart_radio == '0')
	{
		window.location.href = "/site/shopping_check_method.asp?pay_method="+ '<%= LAYOUT_PAY_PICKUP_VALUE %>';
	}
	else if ((checked && pay_method != -1 ))
	{		
		window.location.href = "/site/shopping_check_method.asp?pay_method="+ pay_method;
	}
	else if(pay_method == '<%= LAYOUT_PAY_PICKUP_VALUE %>')
	{	
		window.location.href = "/site/shopping_check_method.asp?pay_method="+ pay_method;
	}
	else if(!checked) 
	{
		alert("Please select ship method.");
	}
	else
	{
		alert("Please select pay method.");
	}
	//
	
}

function initialSelected(v){
	//var v = $(this).val();
	
	if(v == "0")
	{
		// pick up
		$('input[name=cart_radio_pick_or_shipping][value=0]').attr("checked",true);
		$('input[name=cart_radio_pick_or_shipping][value=1]').attr("checked",false);
		$('.cart_shipping_info').css("display", "none");
		cartChangeCharge('<%= LAYOUT_ORDER_CODE %>', 'CA','-1', "ON" , true);
		//alert('<%= LAYOUT_ORDER_CODE %>');
	}
	else
	{
		$('input[name=cart_radio_pick_or_shipping][value=0]').attr("checked",false);
		$('input[name=cart_radio_pick_or_shipping][value=1]').attr("checked",true);
		$('.cart_shipping_info').css("display", "");
	}
	
	//$('#cart_next_table').unbind('click');
	//$('#cart_next_table').bind('click', function(){ gotoCart();});
	
}


</script>