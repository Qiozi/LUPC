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
                    <span class='nav1'>My Account</span>
                	
							<%
								if request("order_type") =1 then 
							%>
									<a href="member_center_Porder.asp" class="white-red-11"><span class='nav1'><strong> Orders </strong></span></a>
							<% 	else %>
									<a href="member_center_Aorder.asp" class="white-red-11"><span class='nav1'><strong> Orders </strong></span></a>
							<% 	end if %>
                    
                    <span class="nav1">Order#<%= SQLescape(request("order_code"))	 %></span>
                </div>
            	<div id="page_main_area">
                <%
	Dim current_tmp_order_code
	current_tmp_order_code = SQLescape(request("order_code"))	
	
	' 价格
    dim sales_record_number, user_id, buyer_fullname, buyer_phone_number 
	dim buyer_email
	dim buyer_address1
	dim buyer_address2
	dim buyer_city
	dim buyer_province
	dim buyer_postal_code
	dim buyer_country
	dim item_number
	dim item_title
	dim custom_label
	dim quantity
	dim sale_price
	dim sale_price_unit
	dim shipping_and_handling
	dim shipping_and_handling_unit
	dim insurance
	dim insurance_unit
	dim cash_on_delivery_fee
	dim cash_on_delivery_fee_unit
	dim total_price
	dim total_price_unit
	dim payment_method
	dim sale_date
	dim checkout_date
	dim paid_on_date
	dim shipped_on_date
	dim feedback_left
	dim feedback_received
	dim notes_to_yourself
	dim paypal_transaction_id
	dim shipping_service
	dim cash_on_delivery_option
	dim transaction_id
	dim order_id
	

		set rs = conn.execute("select sales_record_number, user_id, buyer_fullname, buyer_phone_number, "&_
	"buyer_email, "&_
	"buyer_address1, "&_
	"buyer_address2, "&_
	"buyer_city, "&_
	"buyer_province, "&_
	"buyer_postal_code, "&_
	"buyer_country, "&_
	"item_number, "&_
	"item_title, "&_
	"custom_label, "&_
	"quantity, "&_
	"sale_price, "&_
	"sale_price_unit, "&_
	"shipping_and_handling, "&_
	"shipping_and_handling_unit, "&_
	"insurance, "&_
	"insurance_unit, "&_
	"cash_on_delivery_fee, "&_
	"cash_on_delivery_fee_unit, "&_
	"total_price, "&_
	"total_price_unit, "&_
	"payment_method, "&_
	"date_format(sale_date, '%b/%d/%Y') sale_date, "&_
	"date_format(checkout_date, '%Y/%b/%d') checkout_date, "&_
	"date_format(paid_on_date, '%b/%d/%Y') paid_on_date,  "&_
	"date_format(shipped_on_date, '%b/%d/%Y') shipped_on_date,  "&_
	"feedback_left, "&_
	"feedback_received, "&_
	"notes_to_yourself, "&_
	"paypal_transaction_id, "&_
	"shipping_service, "&_
	"cash_on_delivery_option, "&_
	"transaction_id, "&_
	"order_id from tb_order_ebay where order_code='"&current_tmp_order_code&"' and user_id='"& session("user") & "'")
        if not rs.eof then
            sales_record_number = rs("sales_record_number")
            user_id = rs("user_id")
            buyer_fullname = rs("buyer_fullname")
            buyer_phone_number = rs("buyer_phone_number")            
	        buyer_email = rs("buyer_email")            
	        buyer_address1 = rs("buyer_address1")
	        buyer_address2 = rs("buyer_address2")
	        buyer_city = rs("buyer_city")
	        buyer_province = rs("buyer_province")
	        buyer_postal_code = rs("buyer_postal_code")
	        buyer_country = rs("buyer_country")
	        item_number = rs("item_number")
	        item_title = rs("item_title")
	        custom_label = rs("custom_label")
	        quantity = rs("quantity")
	        sale_price = rs("sale_price")
	        sale_price_unit = rs("sale_price_unit")
	        shipping_and_handling = rs("shipping_and_handling") 
	        shipping_and_handling_unit = rs("shipping_and_handling_unit")
	        insurance = rs("insurance")
	        insurance_unit = rs("insurance_unit")
	        cash_on_delivery_fee = rs("cash_on_delivery_fee") 
	        cash_on_delivery_fee_unit = rs("cash_on_delivery_fee_unit")
	        total_price = rs("total_price")
	        total_price_unit = rs("total_price_unit")
	        payment_method = rs("payment_method")
	        sale_date = rs("sale_date")
	        paid_on_date = rs("paid_on_date")

	        feedback_left = rs("feedback_left")
	        feedback_received = rs("feedback_received")
	        notes_to_yourself = rs("notes_to_yourself")
	        paypal_transaction_id = rs("paypal_transaction_id")
	        shipping_service = rs("shipping_service")
	        cash_on_delivery_option = rs("cash_on_delivery_option") 
	        transaction_id = rs("transaction_id")
	        order_id = rs("order_id")
		end if
		rs.close : set rs = nothing
%>
                	
                          <table width="600" height="670"  border="0" align="center" cellpadding="1" cellspacing="2" bgcolor="#FFFFFF" style="border:#8FC2E2 1px solid; ">
                            <tr>
                              <td valign="top" style="border:#E3E3E3 1px solid; "><table width="100%" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                  <td height="10"></td>
                                </tr>
                              </table>
                                <table width="80%"  border="0" align="center" cellpadding="0" cellspacing="0">
                                  <tr>
                                    <td>&nbsp;</td>
                                    </tr>
                                </table>
                                <div id="email_area">
                                <table width="98%" style="border-bottom:#327AB8 1px solid; "  border="0" align="center" cellpadding="0" cellspacing="0">
                                  <tr>
                                    <td>&nbsp;</td>
                                  </tr>
                                </table>
                                <table width="90%" height="30"  border="0" align="center" cellpadding="0" cellspacing="0">
                                  <tr>
                                    <td class="text_hui_11">&nbsp;</td>
                                  </tr>
                                  <tr>
                                    <td style="font-size:11pt"><strong> LU COMPUTERS ORDER FORM</strong></td>
                                  </tr>
                                </table>
                                <table width="90%"  border="0" align="center" cellpadding="2" cellspacing="0">
                                  <tr>
                                    <td width="50%" class="text_hui_11" >&nbsp;</td>
                                  <td width="50%" class="text_hui_11" >1875 Leslie Street, Unit 24 　 <br>
                 Toronto, Ontario, M3B 2M5 　 <br>
                Tel: (866)999-7828 (416)446-7743</td>
                                  </tr>
                                  <tr>
                                    <td><p class="text_hui_11"><br>                      
                                      </p>                      </td>
                                  <td>&nbsp;</td>
                                  </tr>
                                  <tr>
                                    <td colspan="2"><table width="100%"  border="0" cellspacing="0" cellpadding="2">
                                      <tr>
                                        <td width="20%" class="text_hui_11"><strong>Order Number:</strong></td>
                                      <td  class="text_hui_11"><%= current_tmp_order_code%></td>
                                      </tr>
                                      <tr>
                                        <td class="text_hui_11"><strong>Date: </strong></td>
                                      <td class="text_hui_11"><%= sale_date %></td>
                                      </tr>
                                      <tr>
                                        <td class="text_hui_11"><strong>Customer Name:</strong></td>
                                      <td class="text_hui_11"><%= buyer_fullname %></td>
                                      </tr>
                                      <tr>
                                        <td class="text_hui_11"><strong>Payment: </strong></td>
                                      <td class="text_hui_11"><%= payment_method %></td>
                                      </tr>
                                    </table>
                                     </td>
                                    
                                  </tr>
                                </table>
                                <table width="98%" style="border-bottom:#327AB8 1px solid; "  border="0" align="center" cellpadding="0" cellspacing="0">
                                  <tr>
                                    <td>&nbsp;</td>
                                  </tr>
                                </table>                <table width="90%"  border="0" align="center" cellpadding="2" cellspacing="0">
                                  
                                  <tr>
                                    <td class="text_hui_11"><strong>Shipping Address:</strong></td>
                                  </tr>
                                  <tr>
                                    <td class="text_hui_11"> 
                                    <%
                                    response.write buyer_fullname &"<br/>"
                                    response.write buyer_address1 &" &nbsp;"& buyer_address2 &"<br/>"
                                    response.write buyer_city &"&nbsp;&nbsp;" & buyer_province &"<br/>"
                                    response.write buyer_postal_code &"<br/>"	
                                    
                                    if buyer_phone_number <> "" then response.write "Phone #:&nbsp;" & buyer_phone_number  &"<br/>"
                                    
                                    dim Estimated_Shipping_Date, ups_tracking_number
                                    Estimated_Shipping_Date = ""
                                    ups_tracking_number = ""
                                    set srs = conn.execute("select date_format(regdate,'%m/%d/%Y') regdate, ups_tracking_number from tb_order_ups_tracking_number where order_code='"&current_tmp_order_code&"'")
                                    if not srs.eof then 
                                        Estimated_Shipping_Date = srs(0)
                                        ups_tracking_number = srs(1)
                                    end if
                                    srs.close : set srs = nothing
                                    if Estimated_Shipping_Date <> "" then 
                                        response.Write("<b>Shipping Date:</b>&nbsp;&nbsp;"& Estimated_Shipping_Date)		
                                        response.Write("<br/><b>UPS TRACKING NUMBER:</b> "& ups_tracking_number)		
                                    end if
                                    %>
                      </td>
                                  </tr>
                                </table>
                                <table width="98%" style="border-bottom:#327AB8 1px solid; "  border="0" align="center" cellpadding="0" cellspacing="0">
                                  <tr>
                                    <td>&nbsp;</td>
                                  </tr>
                                </table>
                
                                <table width="90%"   border="0" align="center" cellpadding="2" cellspacing="0" bgcolor="#eeeeee">
                                  <tr bgcolor="#FFFFFF">
                                    <td  style="padding-right:10px; line-height:18px; border: 0px solid #eeeeee" align="left" bgcolor="#FFFFFF" valign="top">
                                        <b>Item title</b></td>
                                    <td  align="left" bgcolor="#FFFFFF" class="text_hui_11" style=" font-size:10pt;" valign="top">
                                        <%= item_title %></td>
                                  </tr>
                                   <tr bgcolor="#FFFFFF">
                                    <td  style="padding-right:10px; line-height:18px; border: 0px solid #eeeeee; border-top: 0px;" align="left" bgcolor="#FFFFFF" valign="top">
                                       <b>Item number</b> </td>
                                    <td  align="left" bgcolor="#FFFFFF" class="text_hui_11" style=" font-size:10pt;" valign="top">
                                        <%= item_number %></td>
                                  </tr>
                                </table>
                                <table width="98%" style="border-bottom:#327AB8 1px solid; "  border="0" align="center" cellpadding="0" cellspacing="0">
                                  <tr>
                                    <td>&nbsp;</td>
                                  </tr>
                                </table></div>
                                <table width="90%"  border="0" align="center" cellpadding="3" cellspacing="0" >
                                  <tr>
                                    <td width="150"><b>Shipping And Handling:</b></td><td><%=shipping_and_handling_unit & " $"& shipping_and_handling %></td>
                                    </tr>
                                    <tr>
                                    <td  ><b>Total Price:</b></td><td><%=total_price_unit & " $"& total_price %></td>
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

<% closeconn() %>
<script type="text/javascript">
$().ready(function(){
	//$('#page_main_area').load('/site/inc/inc_default.asp?'+rand(1000));
});
</script>