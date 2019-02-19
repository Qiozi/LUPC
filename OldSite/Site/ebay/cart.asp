<!--#include virtual="site/inc/inc_page_top.asp"-->
<%
response.Cookies("CurrentOrder") = "ebay"


Dim current_state		:		current_state		=	null
Dim current_company_id	:		current_company_id	=	null
%>
<table class="page_main" cellpadding="0" cellspacing="0">
	<tr>
    	<td  id="page_main_left" valign="top" style="padding-bottom:5px; padding-left: 2px" class='page_frame'>
        	<!-- left begin -->
            	<!--#include virtual="site/inc/inc_left_menu.asp"-->
            <!-- left end 	-->
    	</td>
    	
        <td id="page_main_center" valign="top" style="width:600px" class='page_frame'>
        	<!-- main begin -->
        	    <div id="page_main_banner"></div>
        	   <div class="page_main_nav" id="page_main_nav">
                	<span class="nav1"><a href="/ebay/">Home</a></span>
                	<span class="nav1">Ebay</span>
                    <span class="nav1">Cart</span>
                </div>
            	<div id="page_main_area">
            	        <div style='background:#ffffff; text-align:left'>        
                              	            
            	            <%
								'call ValidateUserSessionInfo(LAYOUT_CCID ,"closeConn") 
								Call ValidateOrder_Code("ebay")
								
								Dim phone_d
								Dim customer_email1
								Dim customer_shipping_city
								Dim customer_shipping_address
								Dim customer_shipping_first_name
								Dim customer_shipping_last_name
								Dim customer_shipping_zip_code
								dim payMethodIDS 	:		payMethodIDS = "15,25"
								
								Set rs = conn.execute("Select "&_
													" phone_d"&_
													" ,customer_email1"&_
													" ,customer_shipping_city"&_
													" ,customer_shipping_address"&_
													" ,customer_shipping_first_name"&_
													" ,customer_shipping_last_name"&_
													" ,customer_shipping_zip_code"&_
													" From tb_customer "&_
													" Where customer_serial_no="& SQLquote(LAYOUT_CCID))
								if not rs.eof then
									phone_d							=	rs("phone_d")
									customer_email1					=	rs("customer_email1")
									customer_shipping_city			=	rs("customer_shipping_city")
									customer_shipping_address		=	rs("customer_shipping_address")
									customer_shipping_first_name	=	rs("customer_shipping_first_name")
									customer_shipping_last_name		=	rs("customer_shipping_last_name")
									customer_shipping_zip_code		=	rs("customer_shipping_zip_code")
								
								End if
								rs.close : set rs = nothing
							%>
            	            <table width="100%"  border="0" cellspacing="0" cellpadding="0" class="table_td_width">
					                <form name="FM" id="FM" action="<%= LAYOUT_HOST_URL %>modify_cart_quantity.asp" method="post">
                					  <input type="hidden" name="returnURL" value="/ebay/cart.asp" />
                                      
                                      <tr>
                                        <td><img src="/soft_img/app/iteam_title.gif" width="230" height="25"></td>
                                      </tr>
                                      <tr>
                                        <td><table width="100%"  border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                              <td height="5" style="background:#256AB1"></td>
                                            </tr>
                                            <tr>
                                              <td height="25" valign="top" style="background:#E8E8E8"><table width="100%"  border="0" cellspacing="0" 

                cellpadding="2">
                                                <tr>
                                                  <td width="13%" style="padding-left:5px;background:#E8E8E8">SKU</td>
                                                  <td width="49%" style="border-left:#256AB1 1px solid; padding-left:5px; background:#E8E8E8">Description</td>
                                                  <td width="7%" style="border-left:#256AB1 1px solid;  padding-left:5px;background:#E8E8E8">QTY</td>
                                                  <td width="11%" style="border-left:#256AB1 1px solid;  padding-left:5px;background:#E8E8E8">Unit Price</td>
                                                  <td width="11%" style="border-left:#256AB1 1px solid;  padding-left:5px;background:#E8E8E8">Amount</td>
                                                  <td style="border-left:#256AB1 1px solid;  padding-left:5px;background:#E8E8E8">Delete</td>
                                                </tr>
                                              </table></td>
                                            </tr>
                                        </table></td>
                                      </tr>
                                      <tr>
                                        <td>
                                        
                                        <table width="100%"  border="0" cellpadding="2" cellspacing="0">
                                              <tr>
                                                <td valign="top">
                                                
                                                <table width="100%"  border="0" cellspacing="0" cellpadding="2"  class="list_hover">
                                        <!-- Cart start-->
                                        <%
											'response.write LAYOUT_EBAY_ORDER_CODE
											'
											' delete other system Order.
											'
											'REsponse.write LAYOUT_EBAY_ORDER_CODE &"|"& LAYOUT_ORDER_CODE
											conn.execute("Delete from tb_cart_temp Where "&_
														" cart_temp_code="& SQLquote(LAYOUT_EBAY_ORDER_CODE) &" And current_system<>"&SQLquote(current_system))
                                            Set rs = conn.execute("select ct.product_serial_no,price sys_tmp_price, price_unit  "&_
                                                                    "   ,cart_temp_Quantity"&_
                                                                    "   ,cart_temp_serial_no"&_
																	"	,product_name "&_
																	"	,shipping_state_code"&_
																	"	,shipping_company"&_
                                                                    "	from tb_cart_temp ct "&_
                                                                    "	where cart_temp_code="& SQLquote(LAYOUT_EBAY_ORDER_CODE) &" and current_system="& SQLquote(Current_System)  )
											
                                            if not rs.eof then
												Dim cart_temp_Quantity, amount, amount_sub_total
												amount= 0
												amount_sub_total 	= 	0
												current_state		=	rs("shipping_state_code")
												current_company_id	=	rs("shipping_company")
                                                Do while not rs.eof
													cart_temp_Quantity = rs("cart_temp_Quantity")
													if not isnumeric(cart_temp_Quantity) then cart_temp_Quantity = 1
													cart_temp_Quantity = cint(cart_temp_Quantity)
													amount = cart_temp_Quantity * cdbl(rs("sys_tmp_price"))
													amount_sub_total = amount_sub_total + amount
						                %>
                                            
                                                  <tr>
                                                    <td width="80" style="padding-left:5px; min-height:40px">
                                                    	<input type="hidden" name="Order_Ids" value="<%= rs("cart_temp_serial_no") %>" />
                                                        <%= GetEbayNumberForSpTmp(rs("product_serial_no")) %>
                                                    </td>
                                                    <td >
                                                      <a onclick="return js_callpage_cus(this.href, 'print_sysw', 620, 700)" href="/ebay/system_view_mini.asp?cmd=print&system_code=<%=rs("product_serial_no")%>" class="hui_red"><%= rs("product_name")%></a>
                                                    </td>
                                                    <td width="7%">
                                                    	
                                                            <input name="quantity"  type="text" class="input" id="quantity"  style=" width:20px;" onKeyDown="return keydownevent();"  value="<%=rs("cart_temp_Quantity")%>" size="3" maxlength="3">
                                                    </td>
                                                    <td width="11%" style="text-align:right"><span class="price1"><%= formatcurrency(rs("sys_tmp_price"),2)%></span></td>
                                                    <td width="11%" style="text-align:right"><span class="price1"><%= formatcurrency( amount,2)%></span></td>
                                                    <td width="50"><a  onclick="delCart('<%= rs("cart_temp_serial_no") %>', $('input[name=returnURL]').val());" ><img 

                src="/soft_img/app/delect_bt.gif" width="48" height="18" border="0" style="cursor:pointer"></a></td>
                                                  </tr>
                                                
                                            <!---cart end-->
                                            <%
						                rs.movenext
						                loop
						                end if
						                rs.close : set rs = nothing
						              	
										Conn.Execute("Update Tb_cart_temp_price Set sub_total="& SQLquote(amount_sub_total)&" Where order_code="& SQLquote(LAYOUT_EBAY_ORDER_CODE))
					                %>
                                            </table></td>
                                              </tr>
                                              <tr>
                                                <td height="10" style="border-bottom:1px dotted #333333"></td>
                                              </tr>
                                            </table>
                                            <table width="100%" height="27"  border="0" cellpadding="0" cellspacing="0">
                                              <tr>
                                                <td align="right" style="background:#EFEFEF;border-top:#DDDDDD 1px solid; border-bottom:#DDDDDD 

                1px solid;">
                					<strong>Sub Total (<a href="<%= LAYOUT_HOST_URL %>General_faq.asp"><strong style="color: #095E81;"><%= CCUN %></strong></a>)：</strong>
                					<span class="price1"><%=formatcurrency(amount_sub_total, 2)%></span>&nbsp;&nbsp;
                                    <input type='hidden' id='cart_sub_total' value='<%= amount_sub_total %>' />
                                    </td>
                                              </tr>
                                          </table></td>
                                      </tr>
                                      <tr>
                                        <td style="padding-right:5px; " height="35" align="right" valign="bottom" ><table id="__01" width="120" height="24" border="0" cellpadding="0" cellspacing="0" align="right">
                                          <tr>
                                            <td width="28"> <img src="/soft_img/app/update.gif" width="28" height="24" alt=""></td>
                                            <td align="center" style="background: url(/soft_img/app/customer_bottom_03.gif)"><a href="javascript: document.getElementById('FM').submit();" class="btn_img"><strong>Update</strong></a> </td>
                                            <td width="6"> <img src="/soft_img/app/customer_bottom_04.gif" width="6" height="24" alt=""></td>
                                          </tr>
                                        </table></td>
                                      </tr>
				                      </form>
                                    </table>
            	            
                            <div class="cart_shiping_charge">
                            	<img src="/soft_img/app/charge_title.gif" height="25">
                                <div style='height:5px; background:#256AB1;'></div>
                    			<p><strong>You can find out the total amount BEFORE you check out.</strong> Please select your destination state / province and shipping method below.
                                </p>
                                <p>Orders are processed and shipped Monday through Friday. In-stock items and special orders (when available) are usually shipped immediately. You will be notified for any items if not shipped right away. Computer systems are usually shipped in 1-7 business days. But fast shipping is not guaranteed. LU Computers is a fast shipper; we take every effort to ship your item as soon as possible.
                                </p>
                                <p><strong>Shopping with LU Computers is safe and secure!  </strong></p>
                        
                        		<p>                         To protect your transaction, we use GeoTrust's service and 128-bit Secure Sockets Layer (SSL) technology, thereby offering the highest level of encryption or security possible. This means you can rest assured that communications between your browser and this site's web servers are private and secure, and your personal information is also stored securely in our server. 
                                </p>
                            </div>
                            
                            <!-- select state -->
                            <div >
                            	<ul class='row'>
                                	<li class='comment2'>Country/Province: <li>
                                    <li style='float:left;width: 390px; padding-left:5px;'><input type='hidden' name="current_company_id" value="<%= current_company_id %>" />
                       	  				<select name="country" id="country" onChange="cartChangeOrderState(this, null, $('input[name=current_company_id]').val(), null, false, '<%= LAYOUT_EBAY_ORDER_CODE %>');" >    
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
                                                        Response.write " >"& rs("state_code") &" - "& rs("state_name") &"</option>"
                                                    rs.movenext
                                                    loop
                                                end if
                                                rs.close : set rs = nothing
                                            %>
                                            </optgroup>
                                        </select>
                                    </li>
                                </ul>
                               <hr style="clear:both" class="border_white" />
                                <ul class='row'>
                                    <li class="comment2">Shipping Method:</li>
                                    <li id='cart_shipping_company'  style='float:left; width: 390px; padding-left:5px'></li>                                    
                                </ul>
                                <ul class="row" style='border-top:1px dotted #666666; margin-top:15px; padding-top:5px;'>
                                	<li class="comment2">Payment:</li>
                                    <li id="cart_pay_method_area" style='float:left; width: 390px; padding-left:5px'>
                                    	<%							
											set rs = conn.execute("select * from tb_pay_method_new where tag=1 and pay_method_serial_no in ("&payMethodIDS&") order by taxis asc")
											if not rs.eof then 
												do while not rs.eof 
													response.write "<input type='radio' name='pay_method' value='"& rs("pay_method_serial_no") &"'/>"
													if lcase(rs("pay_method_name")) = "paypal" then 
														response.write "<img src=""https://www.paypal.com/en_US/i/logo/PayPal_mark_37x23.gif""  style=""margin-right:7px;""/>"
													else
														Response.write rs("pay_method_name") 
													end if
													response.write "<br/>"
												rs.movenext
												loop
											end if
											rs.close : set rs = nothing
                                         %>
                                    
                                    </li>
                                </ul>
                                
                            </div>
                            <hr style="clear:both;color:#FFFFFF; border:0"/>
                            <div style="clear:both;margin-top:20px" id="cart_charge_area_p">
                 				<ul id="cart_charge_area"></ul>
                               
                                <p>LU Computers reserves the right to change above shipping fees if the actual shipping costs are significantly greater than above estimate.</p>
                                    
                            </div>
                            
                            <hr style="clear:both;color:#FFFFFF; border:0"/>
                            <div style="height: 100px;padding-right:5px;">
                                  <table id="__01" width="140" height="24" border="0" cellpadding="0" cellspacing="0"  align="right">
                                      <tr>
                                        <td width="28"><img src="/soft_img/app/pay.gif" width="28" height="24" alt=""></td>
                                        <td class="btn_middle">
                                       
                                        <a class="btn_img" id="ebay_cart_btn_next"><strong>Check out</strong></a>
                                        </td>
                                        <td width="6"><img src="/soft_img/app/customer_bottom_04.gif" width="6" height="24" alt=""></td>
                                      </tr>
                                  </table>
                                  
                                  <table id="__01" width="170" height="24" border="0" cellpadding="0" cellspacing="0" align="right" style="margin-left:10px;margin-right: 10px;">
                                          <tr>
                                            <td width="28"><img src="/soft_img/app/buy_car.gif" width="28" height="24" alt=""></td>
                                            <td class="btn_middle">
                                            <!--a href="Cartexec.asp?action=recalculate" onClick="if(confirm('are you sure!')){ return true; } else return false;" class="white-hui-12"-->
                                            <a href="/ebay/" class="btn_img"><strong>Continue Shopping</strong></a> </td>
                                            <td width="6"><img src="/soft_img/app/customer_bottom_04.gif" width="6" height="24" alt=""></td>
                                          </tr>
                                  </table>
                                 <table width="110" height="24" border="0" align="right" cellpadding="0" cellspacing="0" id="__01">
                                    <tr>
                                      <td width="28"><img src="/soft_img/app/clear.gif" width="28" height="24" alt=""></td>
                                      <td class="btn_middle"><!--a href="Cartexec.asp?action=recalculate" onClick="if(confirm('are you sure!')){ return true; } else return false;" class="white-hui-12"-->
                                        <a href="<%= LAYOUT_HOST_URL %>cart_del.asp?cmd=all&order_code=<%= LAYOUT_EBAY_ORDER_CODE %>&returnUrl=/ebay/cart.asp" class="btn_img"><strong>Clear Cart</strong></a> </td>
                                      <td width="6"><img src="/soft_img/app/customer_bottom_04.gif" width="6" height="24" alt=""></td>
                                    </tr>
                                  </table> 
                                                                
                           </div>     
                           
       	          </div>
            	
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
<% closeconn %>

<script type="text/javascript">
    $().ready(function(){
        $('#page_main_area').css("border","1px solid #8FC2E2").css("background", "#ffffff").css("padding", "1px");
		$('div.cart_shiping_charge').find("p").css("padding", "5px");
		$('ul.row').css("width", "100%").css("margin-top", "5px").css("clear","both");
		$("li.comment2").css("border", "0px solid red").css("width", "200px").css("text-align", "right").css("float", "left");
		
		cartChangeOrderState($('#country'), null, $('input[name=current_company_id]').val(), null, false, '<%= LAYOUT_EBAY_ORDER_CODE %>');
    });
	
//------------------------------------------------------------------------------------------
function cartChangeOrderState(theSelect, theRadio, shipping_company_id, shipping_charge, is_only_charge, order_code)
//------------------------------------------------------------------------------------------
{
		showLoading();

		if(theRadio != null){
			shipping_company_id = $(theRadio).attr("title"); 
			$('input[name=current_company_id]').val(shipping_company_id);
			
		}
		
		var country_code = $('#country').val();
		//if (!is_only_charge)
		//$('#cart_shipping_company').html('/site/inc/inc_cart_get_shipping_company.asp?order_code='+order_code+'&state_code='+ $('#country').val()+'&shipping_company_id='+ shipping_company_id +'&'+ rand(1000));
		$('#cart_shipping_company').load('/site/inc/inc_cart_get_shipping_company.asp?order_code='+order_code+'&state_code='+ $('#country').val()+'&shipping_company_id='+ shipping_company_id +'&'+ rand(1000)
			,function(){
			
				//cartLoadPayment(order_code, country_code,$('input[name=shipping_charge][checked=true]').attr('title'), $('input[name=shipping_charge]').val());
				cartChangeOrderCharge(order_code, country_code, $('input[name=shipping_charge][checked=true]').attr('title'), $('input[name=shipping_charge]').val() );
				$('#ebay_cart_btn_next').click(function(){gotoCheckout(1);});
			}
		);
		
		

}	



function cartLoadPayment(order_code, country_code,shipping_company, shipping_charge )
{
	//$('#cart_pay_method_area').html('/site/inc/inc_get_payment_area.asp?country_code='+ country_code +"&"+rand(1000));
//	$('#cart_pay_method_area').load('/site/inc/inc_get_payment_area.asp?country_code='+ country_code +"&"+rand(1000)
//		
//		,function(){
//			closeLoading();
//			$('#cart_pay_method_area').show('slow');
//			$('input[name=pay_method]').click(
//				function(){
					cartChangeOrderCharge(order_code, country_code, shipping_company, shipping_charge);
//					}
//				);
//		}
//	);
}



function cartChangeOrderCharge(order_code, state_code, shipping_company, shipping_charge)
{

	$('#cart_charge_area').load('/ebay/inc/inc_ebay_order_charge.asp?order_code='+order_code+'&state_code='+ state_code+"&shipping_company="+shipping_company +'&shipping_charge='+ shipping_charge+'&'+ rand(1000),
	 function(){ 
	 	closeLoading(); 
		$('#cart_charge_area').show('slow');
		
		//to_paypal_url_path
		//$('#cart_check_out_paypal').click(function(){gotoCheckout(2);});
	 	
	 });
}

function gotoCheckout(t)
{

	var checked = false;
	$('input[name=shipping_charge]').each(function(){
		if($(this).attr('checked'))
			checked = true;
	});
	
	var pay_method = -1;
	$('input[name=pay_method]').each(function(){
		if($(this).attr('checked'))
			pay_method = $(this).val();
	});

	if (checked&& pay_method != -1)
	{
			window.location.href='/ebay/shopping_check_method.asp?pay_method='+pay_method +'&'+ rand(1000);
	}
	else
		alert("Please select shipping method and payment.");
}

</script>
