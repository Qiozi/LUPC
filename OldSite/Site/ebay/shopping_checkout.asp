<!--#include virtual="site/inc/inc_page_top.asp"-->
<table class="page_main" cellpadding="0" cellspacing="0">
	<tr>
    	<td  id="page_main_left" valign="top" style="padding-bottom:5px; padding-left: 2px" class='page_frame'>
        	<!-- left begin -->
            	<!--#include virtual="site/inc/inc_left_menu.asp"-->
            <!-- left end 	-->
    	</td>
    	
        <td id="page_main_center" valign="top">
        	<!-- main begin -->
        	    
        	    <div class="page_main_nav" id="page_main_nav">
                	<span class="nav1"><a href="/ebay/">Home</a></span>
                	<span class="nav1">Ebay</span>
                    <span class="nav1">Checkout</span>
                </div>
            	<div id="page_main_area" style='width: 600px'>
                	<%
						'
						' 
						'
						Response.Cookies("CurrentOrder") = "ebay"
						Dim shipping_company
						dim payMethodIDS 
						payMethodIDS = "15,25"
						Dim pay_method
						Dim shipping_state_code
						Dim state_code
						Dim shipping_country_code
						set rs = conn.execute("select "&_
							"	 ct.shipping_state_code, ct.shipping_state_code, ct.country_id, ct.shipping_country_code, ct.shipping_state_code"&_
							" ,pay_method"&_
							"	from tb_cart_temp_price ctp inner join tb_cart_temp ct on ct.cart_temp_code = ctp.order_code "&_
							"	where ctp.order_code='"&LAYOUT_EBAY_ORDER_CODE&"' and ct.current_system="& SQLquote(current_system) )
						if not rs.eof then			
							pay_method					=	rs("pay_method")
							state_code				=	rs("shipping_state_code")
							shipping_country_code		=	rs("shipping_country_code")			
						end if
						rs.close : set rs = nothing
		
					%>
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
                                    <td>
                                    <!--order begin-->
                                    <table width="100%"  border="0" cellpadding="2" cellspacing="1" bgcolor="#eeeeee">
                                    <%
                                        dim category, amount, unit_price
                                        category = SQLescape("category")
                                        unit_price = 0
                                        if len(LAYOUT_EBAY_ORDER_CODE) = LAYOUT_ORDER_LENGTH then						
                                            n = 0
                                                                                                
                                                ' product system 
                                                unit_price = 0
                                                set rs = conn.execute("select * from tb_cart_temp  where cart_temp_code='"&LAYOUT_EBAY_ORDER_CODE&"' and length(product_serial_no)=8")
												'Response.write ("select * from tb_cart_temp  where cart_temp_code='"&LAYOUT_EBAY_ORDER_CODE&"' and length(product_serial_no)=8")
                                                if not rs.eof then
                                                    do while not rs.eof 				
                                                    shipping_company = rs("shipping_company")	
													if 	SQLescape(rs("price")) <> "" then 
                                                    	unit_price = cdbl(SQLescape(rs("price"))) 
													else
														unit_price = 0
													end if
                                                    amount = unit_price * cint(rs("cart_temp_Quantity"))
                                                    n = n +1
                                        %>
                                                <tr bgcolor="#f2f2f2" <%'if n mod 2 = 0 then response.write " bgcolor=""#FFFFFF"" " else response.write " bgcolor=""#f2f2f2"""%>>
                                                        <td >&nbsp;
                                                            <%
                                                              'if len(rs("product_serial_no")) = 8 then 
                                                              '	response.write "<a href=""view_print_system.asp?cmd=print&id="&rs("product_serial_no")&""" onClick=""return js_callpage(this.href)"">"&rs("product_name")&"</a>"
                                                              'else
                                                                response.write rs("product_name")
                                                              'end if
                                                             %></td>
                                                        <td width="9%" align="center" class="text_hui_11"><%=rs("cart_temp_Quantity")%></td>
                                                        <td width="11%" align="right" class="text_hui_11"><%= formatcurrency(unit_price,2)%></td>
                                                        <td width="9%" align="right" class="text_hui_11"><%= formatcurrency( amount,2)%>&nbsp; </td>
                                                </tr>
                                       
                                        <tr>
                                            <td style="background:white;" colspan="4">
                                                <%

                                                    set crs = conn.execute("select part_quantity,sp.product_name, p.product_serial_no "&_                
				" from tb_sp_tmp_detail sp inner join tb_product p on sp.product_serial_no=p.product_serial_no"&_						
				" where sp.sys_tmp_code="& SQLquote(rs("product_serial_no"))&" and (p.is_non=0 or p.product_name like '%onboard%') and p.tag=1 order by   sp.product_order asc ")
													
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
                                        %>
                                      </table>
                                    <!--order end-->
                                    <%end if%>
                                     
                                    <%
										Dim	sub_total			
										Dim	shipping_handling	
										Dim	total				
										Dim	taxable_total		
										Dim	pst_charge			
										Dim	gst_charge			
										Dim	gst_rate			
										Dim	pst_rate			
										Dim	price_unit	
										'Dim state_code
										Dim	hst_charge			
										Dim	hst_rate			
										'response.Write(LAYOUT_ORDER_CODE)
										Set rs = COnn.execute("Select * from tb_cart_temp_price where order_code="& SQLquote(LAYOUT_ORDER_CODE))
										If not rs.eof then
											sub_total			=	rs("sub_total")
											shipping_handling	=	rs("shipping_and_handling")
											total				=	rs("grand_total_rate")
											taxable_total		=	rs("taxable_total")
											pst_charge			= 	rs("pst")
											gst_charge			=	rs("gst")
											gst_rate			=	rs("gst_rate")
											pst_rate			=	rs("pst_rate")
											price_unit			=	rs("price_unit")										
											hst_charge			=	rs("hst")
											hst_rate			=	rs("hst_rate")
										End if
										rs.close : set rs =nothing
									%>
                                    	<ul class='row'>
                                        	   <li>
                                                    <ul title='row'>
                                                        <li class='comment'>Sub Total:</li>
                                                        <li title='price'>&nbsp;<span class="price"><%= formatcurrency( sub_total, 2) %></span><span class="price_unit"><%= ( price_unit) %></span></li>
                                                    </ul>
                                                </li>
                                                <li>
                                                    <ul>
                                                        <li class='comment'>Shipping, Handling & Insurance(Not Adjusted):</li>
                                                        <li title='price'>&nbsp;<span class="price"><%= formatcurrency( shipping_handling, 2) %></span><span class="price_unit"><%= ( price_unit) %></span></li>
                                                    </ul>
                                                </li>
                                                <li>
                                                    <ul>
                                                        <li class='comment'>Taxable Total:</li>
                                                        <li title='price'>&nbsp;<span class="price"><%= formatcurrency( taxable_total, 2) %></span><span class="price_unit"><%= ( price_unit) %></span></li>
                                                    </ul>
                                                </li>
                                                <% 
                                                IF instr(LAYOUT_HST_STATE_IDS, state_code) >0 then 
                                                %>
                                                <li>
                                                    <ul>
                                                        <li class='comment'>HST(<%= hst_rate %>%):</li>
                                                        <li title='price'>&nbsp;<span class="price"><%= formatcurrency( hst_charge, 2) %></span><span class="price_unit"><%= ( price_unit) %></span></li>
                                                    </ul>
                                                </li>
                                                <%
                                                Else
                                                %>
                                                <li>
                                                    <ul>
                                                        <li class='comment'>GST(<%= gst_rate %>%):</li>
                                                        <li title='price'>&nbsp;<span class="price"><%= formatcurrency( gst_charge, 2) %></span><span class="price_unit"><%= ( price_unit) %></span></li>
                                                    </ul>
                                                </li>
                                                <li>
                                                    <ul>
                                                        <li class='comment'>PST(<%= pst_rate %>%):</li>
                                                        <li title='price'>&nbsp;<span class="price"><%= formatcurrency( pst_charge, 2) %></span><span class="price_unit"><%= ( price_unit) %></span></li>
                                                    </ul>
                                                </li>
                                                <% End if %>
                                                <li>
                                                    <ul>
                                                        <li class='comment'>Total:</li>
                                                        <li title='price'>&nbsp;<span class="price"><%= formatcurrency( total, 2) %></span><span class="price_unit"><%= ( price_unit) %></span></li>
                                                    </ul>
                                                </li>
                                        </ul>
                                    </td>
                                  </tr>
                          </table>          
                          <div style="border-bottom:#327AB8 1px solid; ">&nbsp;</div>
                                <table width="98%" style="border-bottom:#327AB8 1px solid; "  border="0" align="center" cellpadding="0" cellspacing="0">
                                      <tr>
                                        <td>
                                            <table width="90%"  border="0" align="center" cellpadding="0" cellspacing="0">
                                         
                                          <tr>
                                            <td height="20" align="center" ><strong>Please Select Your Payment Method<br>
                                            </strong></td>
                                          </tr>
                                         
                                          <tr>
                                            <td style="padding: 0px 40px">
                                            
                                            <form name="form_pay_method" id="form_pay_method" method="post" action="/site/shopping_check_method.asp">
                                                <input type="hidden" value="d" name="n">
                                             
                                             
                                              <fieldset style='margin-bottom:10px;margin-top:10px;'>
                                              <legend>Regular Pricing Payment Methods:</legend>
                                              <table width="100%"  border="0" align="center" cellpadding="2" cellspacing="1">
                                                <%							
                                                    set rs = conn.execute("select * from tb_pay_method_new where tag=1 and pay_method_serial_no in ("&payMethodIDS&") order by taxis asc")
                                                    if not rs.eof then 
                                                    do while not rs.eof 
                                                    
                                                %>
                                                <tr bgcolor="#FFFFFF">
                                                  <td width="20" bgcolor="#FFFFFF"><input type="radio" name="pay_method" value="<%=rs("pay_method_serial_no")%>" 
                                                  <% 
                                                    ' 如果是自取
                    
                                                    
                                                        ' 非自取
                                                        if cstr(SQLescape(pay_method)) = cstr(rs("pay_method_serial_no")) then 
                                                            response.Write(" checked")
                                                        end if
                                                        ' 如果不是Ontario洲，将不能自取
                                                        if cstr(state_code) <> cstr(LAYOUT_ONTARIO_Code) then 
                                                            if instr(LAYOUT_PAY_PICKUP_VALUE_s, "["& rs("pay_method_serial_no") &"]")>0 then 
                                                                response.Write(" disabled=""disabled"" ")
                                                            end if
                                                        end if
                                                        if cstr(ucase(SQLescape(shipping_country_code))) = "US" then 
                                                            set no_card_rs = conn.execute("select count(cart_temp_serial_no) from tb_cart_temp where is_noebook=1 and cart_temp_code='"& LAYOUT_ORDER_CODE &"'")
                                                            if not no_card_rs.eof then
                                                                if(no_card_rs(0) > 0)  and rs("pay_method_serial_no") = pay_method_card then 
                                                                    response.Write(" disabled=""disabled"" ")
                                                                end if
                                                            end if
                                                            no_card_rs.close : set no_card_rs = nothing
                                                        
                                                        End if
                                                    
                                                    %>></td>
                                                  <td bgcolor="#FFFFFF" class="text_hui_11">&nbsp; <%
							  	if LAYOUT_PAY_RECORD_METHOD_PAYPAL = rs("pay_method_serial_no") then 
									response.write "<img src=""https://www.paypal.com/en_US/i/btn/btn_xpressCheckout.gif"" />"
								else
									response.write rs("pay_method_name")
								end if
							  %></td>
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
                                    </table> <table width="100%"  border="0" cellpadding="3" cellspacing="0">
                                      <tr>
                                        <td height="60" align="center"><table width="45%"  border="0" cellpadding="3" cellspacing="0">
                                          <tr align="right">
                                            <td><table id="__01" width="130" height="24" border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                  <td width="28"><img src="/soft_img/app/arrow_left.gif" width="28" height="24" alt="" /></td>
                                                  <td align="center" class="btn_middle"><a class="btn_img" href="/ebay/cart.asp"><strong>Back</strong></a> </td>
                                                  <td width="6"><img src="/soft_img/app/customer_bottom_04.gif" width="6" height="24" alt="" /></td>
                                                </tr>
                                            </table></td>
                                            <td><table id="__01" width="130" height="24" border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                  <td width="28"><img src="/soft_img/app/arrow_right.gif" width="28" height="24" alt="" /></td>
                                                  <td align="center"  class="btn_middle"><a onclick="Check(document.getElementById('form_pay_method'));"  class="btn_img"  onfocus="this.style.color='red';" onblur="this.style.color='white'" ><strong>Next</strong></a></td>
                                                  <td width="6"><img src="/soft_img/app/customer_bottom_04.gif" width="6" height="24" alt="" /></td>
                                                </tr>
                                            </table></td>
                                          </tr>
                                        </table></td>
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
        $('#page_main_area').css("border","1px solid #8FC2E2").css("background", "#ffffff").css("padding", "1px");
        //$('#page_main_area').load("/ebay/inc/inc_system_view.asp?id=<%= Request("id") %>");
		$('ul.row ul').css("width", "100%").css('line-height', '20px');
		$("li.comment").css("border", "0px solid red").css("width", "430px").css("text-align", "right").css("float", "left");		
		$("li[title=price]").css("border", "0px solid red").css("float", "left").css("width", "150px").css("text-align", "right");
    });

function Check(the)
{
	
 	var v = $("input[name=pay_method][checked]").val();
	
	$("input[name=pay_method]").each(function(i){if ($(this).attr("checked")) { v = $(this).attr("value");}});
	
	
	if(typeof(v)== "undefined")
	{
		alert('please select Paymethod.');
		return false;
	}
	else
	{
		the.submit();
	}
}
</script>