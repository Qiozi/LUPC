<!--#include virtual="site/inc/inc_helper.asp"-->

<%
	
	
	' 价格
		dim sub_total, shipping_and_handling, 	sales_tax, grand_total, sub_charge, taxable_total
		Dim pickup_checked	:	pickup_checked	=	SQLescape(request("pickup_checked"))
		Dim pay_method 		:	pay_method 		=	null
		Dim state_shipping	:	state_shipping	=	null
		Dim shipping_country_code		:	shipping_country_code		=	null
		Dim tax_execmtion	:	tax_execmtion	=	null
		Dim price_unit			:	price_unit		=	null
		Dim shipping_company	:	shipping_company=	null
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
		
		
		'
		'
		'
		if pickup_checked = "" then 
			'
			'
			' source cart pre.
			'
			pickup_checked	=	request.Cookies("pick_up_in_person")
		
		End if
		'
		'
		'	tax_execmtion
		'
		'Set rs = conn.execute("Select tax_execmtion from tb_customer where customer_serial_no="& SQLquote(LAYOUT_CCID))
		'If not rs.eof then
		tax_execmtion	=	request.Cookies("tax_execmtion")
		''response.write "&b"&tax_execmtion
		'end if
		'rs.close : set rs = nothing
		'
		'
		'	shipping_company
		'
		Set rs = conn.execute("Select max(shipping_company) from tb_cart_temp where cart_temp_code='"&LAYOUT_ORDER_CODE&"' and current_system="& SQLquote(current_system))		
		If not rs.eof then
			shipping_company	=	rs(0)
		end if
		rs.close : set rs = nothing
		

		
		set rs = conn.execute("select "&_
							"	 ct.pay_method, ct.shipping_state_code, ct.shipping_country_code, ct.shipping_country_code, ct.shipping_state_code"&_
							"	from tb_cart_temp_price ctp inner join tb_cart_temp ct on ct.cart_temp_code = ctp.order_code "&_
							"	where ctp.order_code='"&LAYOUT_ORDER_CODE&"' and ct.current_system="& SQLquote(current_system) )
		if not rs.eof then			
			pay_method		=	SQLescape(rs("pay_method"))
			state_shipping	=	SQLescape(rs("shipping_state_code"))
			shipping_country_code		=	SQLescape(rs("shipping_country_code"))		
		end if
		rs.close : set rs = nothing
%>
<table width="600" height="670"  border="0" align="center" cellpadding="1" cellspacing="2" bgcolor="#FFFFFF" style="border:#8FC2E2 1px solid; ">
            <tr>
              <td valign="top" style="border:#E3E3E3 1px solid; "><table width="100%"  border="0" cellspacing="0" 

cellpadding="0">
                <tr>
                  <td height="5" bgcolor="#E8E8E8">&nbsp;&nbsp;1. Delivery Options</td>
                  <td width="16"><img src="/soft_img/app/shop_arrow_gray_red.gif" width="16" height="23"></td>
                  <td bgcolor="#FF6600">&nbsp;&nbsp;<span class="text_white"><strong>2. Pay Methods</strong></span></td>
                  <td width="16"><img src="/soft_img/app/shop_arrow_red.gif" width="17" height="23"></td>
                  <td bgcolor="#E8E8E8">&nbsp;&nbsp;3. Personal Information</td>
                  <td width="17"><img src="/soft_img/app/shop_arrow_gray.gif" alt="" width="17" height="23"></td>
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
                	<div style="line-height:50px;text-align:center">Loading...</div>         
                </div>
                <div style="height:10px;width:98%; margin:auto;border-bottom:#327AB8 1px solid; " ></div>                
                <table width="98%" style="border-bottom:#327AB8 1px solid; "  border="0" align="center" cellpadding="0" cellspacing="0">
                  <tr>
                    <td>
                    	<table width="90%"  border="0" align="center" cellpadding="0" cellspacing="0">
                      <tr>
                        <td>&nbsp;</td>
                      </tr>
                      <tr>
                        <td height="20" align="center" ><strong>Please Select Your Payment Method<br>
                        </strong></td>
                      </tr>
                      <tr>
                        <td height="20" align="right" ><strong><a href="view_Payment.asp" onClick="return js_callpage(this.href)" class="orag-blue">Learn more about payment methods &gt;&gt;</a></strong></td>
                      </tr>
                      <tr>
                        <td height="14"></td>
                      </tr>
                      <tr>
                        <td style="padding: 0px 40px">
                        
                        <form name="form_pay_method" id="form_pay_method" method="post" action="/site/Shopping_Check_method.asp">
							<input type="hidden" value="d" name="n">
                          <fieldset >
                          <legend>Cash Discounted Payment Methods:</legend>
                          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#FF6600; font-weight:bold;">Discount:&nbsp;&nbsp;&nbsp;&nbsp;$<span id="sub_charge_span"><%= formatnumber(sub_charge,2) %> </span></span>
                          <table width="100%"  border="0" align="center" cellpadding="2" cellspacing="1">
                           	<%
								dim payMethodIDS 
								payMethodIDS = replace(replace(replace(LAYOUT_RATE_PAY_METHODS, "][", ","), "[", ""), "]","")
								set rs = conn.execute("select * from tb_pay_method_new where tag=1 and pay_method_serial_no not in ("&payMethodIDS&") order by taxis asc")
								if not rs.eof then 
								do while not rs.eof 
								
							%>
						    <tr bgcolor="#FFFFFF">
                              <td width="20" bgcolor="#FFFFFF"><input type="radio" onClick="changePayMethod(this, '<%= LAYOUT_ORDER_CODE %>', '<%= LAYOUT_SHIPPING_COUNTRY_CODE %>', '<%= shipping_company %>', '<%= LAYOUT_SHIPPING_STATE_CODE %>', '<%= tax_execmtion %>');" name="pay_method" value="<%=rs("pay_method_serial_no")%>" 
							  <% 
							  	' 如果是自取
								
								 if pickup_checked = "true" then 
							  		
									if  instr(LAYOUT_PAY_PICKUP_VALUE_s, "["& rs("pay_method_serial_no") &"]") < 1 then
											response.Write(" disabled=""disabled"" ")
									else
											if cstr(SQLescape(pay_method)) = cstr(SQLescape(rs("pay_method_serial_no")))  then 
												response.Write(" checked='true' ")
											end if
									end if
								else
									' 非自取
									if cstr(SQLescape(pay_method)) = cstr(SQLescape(rs("pay_method_serial_no"))) then 
										response.Write(" checked='true' ")
									end if
									' 如果不是Ontario洲，将不能自取
									if cstr(SQLescape(state_shipping)) <> cstr(SQLescape(LAYOUT_ONTARIO_Code)) and rs("pay_method_serial_no") = LAYOUT_PAY_PICKUP_VALUE then 
										'if instr(LAYOUT_PAY_PICKUP_VALUE_s, "["& rs("pay_method_serial_no") &"]")<0 then 
											response.Write(" disabled=""disabled"" ")
										'end if
									end if
									' 如果是美国，email,cash 
									if cstr(SQLescape(shipping_country_code)) = "US" then 
										if instr(LAYOUT_NO_SUPPERT_US, "[" & rs("pay_method_serial_no") & "]") > 0 then 
											response.write " disabled='true'"
										end if
									end if	
								end if
								
								%>
                                >
                                
                                </td>
                              <td bgcolor="#FFFFFF" class="text_hui_11">&nbsp;<%= rs("pay_method_name") %> 
                                <%if instr(LAYOUT_PAY_PICKUP_VALUE_s, "["& rs("pay_method_serial_no") &"]")>0 then %>
                                (depends on pay method upon pickup.)
                                <% end if %></td>
                            </tr>
							<%
								rs.movenext
								loop
								end if
								rs.close : set rs = nothing
							%>
                          </table>
                          </fieldset>
                          <!--<div style="text-align:right; color:#FF6600">
                          	Recalculate Discount &amp; Total:&nbsp;&nbsp; -$<%= formatnumber(sub_charge,2) %>
                          </div>--><br>
                          <fieldset >
                          <legend>Regular Pricing Payment Methods:</legend>
                          <table width="100%"  border="0" align="center" cellpadding="2" cellspacing="1">
                           	<%							
								set rs = conn.execute("select * from tb_pay_method_new where tag=1 and pay_method_serial_no in ("&payMethodIDS&") order by taxis asc")
								if not rs.eof then 
								do while not rs.eof 
								
							%>
						    <tr bgcolor="#FFFFFF">
                              <td width="20" bgcolor="#FFFFFF"><input type="radio" onClick="changePayMethod(this, '<%= LAYOUT_ORDER_CODE %>', '<%= LAYOUT_SHIPPING_COUNTRY_CODE %>', '<%= shipping_company %>', '<%= LAYOUT_SHIPPING_STATE_CODE %>', '<%= tax_execmtion %>');" name="pay_method" value="<%=rs("pay_method_serial_no")%>" 
							  <% 
							  	' 如果是自取

								if pickup_checked = "true" then 			
									if  instr(LAYOUT_PAY_PICKUP_VALUE_s, "["& SQLescape(rs("pay_method_serial_no")) &"]") < 1 then
											response.Write(" disabled=""disabled"" ")
									else
											if cstr(SQLescape(pay_method)) = cstr(SQLescape(rs("pay_method_serial_no")))  then 
												response.Write(" checked='true' ")
											end if
									end if
								else
									' 非自取
									if cstr(SQLescape(pay_method)) = cstr(SQLescape(rs("pay_method_serial_no"))) then 
										response.Write(" checked='true' ")
									end if
									' 如果不是Ontario洲，将不能自取
									if cstr(SQLescape(state_shipping)) <> cstr(LAYOUT_ONTARIO_Code) then 
										if instr(LAYOUT_PAY_PICKUP_VALUE_s, "["& SQLescape(rs("pay_method_serial_no")) &"]")>0 then 
											response.Write(" disabled=""disabled"" ")
										end if
									end if
									if cstr(SQLescape(shipping_country_code)) = "US" then 
										set no_card_rs = conn.execute("select count(is_noebook) from tb_cart_temp where is_noebook=1 and cart_temp_code='"& LAYOUT_ORDER_CODE &"'")
										if not no_card_rs.eof then
											if(no_card_rs(0) > 0)  and rs("pay_method_serial_no") = pay_method_card then 
												response.Write(" disabled=""disabled"" ")
											end if
										end if
										no_card_rs.close : set no_card_rs = nothing
									
									end if	
								end if
								
								%>></td>
                              <td bgcolor="#FFFFFF" class="text_hui_11">&nbsp;<%= rs("pay_method_name") %> </td>
                            </tr>
							<%
								rs.movenext
								loop
								end if
								rs.close : set rs = nothing
							%>
                          </table>
                          </fieldset>
                        </form></td>
                      </tr>
                    </table></td>
                  </tr>
                </table>
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                  <tr>
                    <td height="40"></td>
                  </tr>
                </table>
                <table width="100%"  border="0" align="left" cellpadding="3" cellspacing="0">
                  <tr>
                    <td height="60" align="center"><table width="45%"  border="0" cellpadding="3" cellspacing="0">
                      <tr align="right">
                        <td><table id="__01" width="130" height="24" border="0" cellpadding="0" cellspacing="0">
                            <tr>
                              <td width="28"><img src="/soft_img/app/arrow_left.gif" width="28" height="24" alt=""></td>
                              <td align="center" class="btn_middle"><a style="cursor: pointer;color: #FFFFFF; text-decoration:none"  class="btn_style" href="Shopping_Cart.asp"  onFocus="this.style.color='red';" onBlur="this.style.color='white'" ><strong>Back</strong></a> </td>
                              <td width="6"><img src="/soft_img/app/customer_bottom_04.gif" width="6" height="24" alt=""></td>
                            </tr>
                        </table></td>
                        <td><table id="__01" width="130" height="24" border="0" cellpadding="0" cellspacing="0">
                            <tr>
                              <td width="28"><img src="/soft_img/app/arrow_right.gif" width="28" height="24" alt=""></td>
                              <td align="center"  class="btn_middle"><a onClick="Check(document.getElementById('form_pay_method'));"  class="btn_img"  onFocus="this.style.color='red';" onBlur="this.style.color='white'" ><strong>Next</strong></a></td>
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
            
            <div style='display:none' id="display_none_area"></div>   
 <script type="text/javascript">
 $().ready(function(){ 	
	changePayMethod(null, '<%= LAYOUT_ORDER_CODE %>', '<%= LAYOUT_SHIPPING_COUNTRY_CODE%>', '<%= shipping_company%>', '<%= LAYOUT_SHIPPING_STATE_CODE%>', '<%= tax_execmtion %>', '<%= pay_method %>');
 });
 
 function changePayMethod(the,  order_code, shipping_country_code, shipping_company, shipping_state_code, tax_execmtion, pay_method)
 {

 	showLoading();
 	var paymethod ;
	if (the == null)
		paymethod = pay_method;
	else
		paymethod = the.value;
 	$('#display_none_area').load("/AccountCharge_new.aspx?tax_execmtion=" + tax_execmtion + "&Pay_method="+ paymethod +"&tmp_code=" + order_code + "&country_id=" + shipping_country_code + "&shipping_company=" + shipping_company + "&sate_shipping=" + shipping_state_code +"&current_system="+ <%= Current_system%> , 
		function(){	WriteOrderChargeArea('<%= LAYOUT_ORDER_CODE %>');}
	);
 }
 
 function WriteOrderChargeArea(order_code)
 {
 	$('#order_charge_area').load('/site/inc/inc_get_order_charge.asp?is_get_sur_charge=true&order_code='+order_code,	function(){ closeLoading();});
 }
 
function Check()
{
	
 	var v = $("input[name=pay_method][checked]").val();
	
	$("input[name=pay_method]").each(function(i){if ($(this).attr("checked")) { v = $(this).attr("value");}});
	
	if (typeof(v) == "undefined" )
	{
		alert('please select paymethod');
		return;
	}
	else
	{
		$('#form_pay_method').submit();
	}
	
}
 
 </script>