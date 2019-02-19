
<!--#include virtual="site/inc/inc_page_top.asp"-->
<!--#include virtual="site/no_express.asp"-->
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
        	    <div class='page_main_nav' id="page_main_nav">
                	<span class="nav1"><a href='<%= LAYOUT_HOST_URL %>'>home</a></span>
                    <span class="nav1">Shopping</span>
                    <span class="nav1">Order</span>
                </div>
            	<div id="page_main_area">
                		<%
							Dim email 	:	email = null
							Set rs = Conn.execute("Select customer_email2"&_
												  ",customer_login_name"&_
												  ", customer_email1 "&_
												  " from tb_customer_store "&_
												  " where Customer_serial_no="& SQLquote(LAYOUT_CCID) )
							if not rs.eof then
									if IsEmail(SQLescape(rs("customer_email1"))) and isNUll(email) then 
										email = rs("customer_email1")
									end if					  
									if IsEmail(SQLescape(rs("customer_email2"))) and  isNUll(email) then 
										email = rs("customer_email2")
									end if
									if IsEmail(SQLescape(rs("customer_login_name"))) and  isNUll(email) then 
										email = rs("customer_login_name")
										
									end if
									'response.write rs("customer_email1") &"|"&rs("customer_email2")&"|"&rs("customer_login_name")
							End if		
							'Response.write email
							'response.End()
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
                                  <td width="16"><img src="/soft_img/app/shop_arrow_gray.gif" width="17" height="23"></td>
                                  <td bgcolor="#E8E8E8">&nbsp;&nbsp;3. Personal Information</td>
                                  <td width="16"><img src="/soft_img/app/shop_arrow_gray_red.gif" width="16" height="23"></td>
                                  <td bgcolor="#FF6600">&nbsp;&nbsp;<span class="text_white"><strong>4. Submit</strong></span></td>
                                </tr>
                              </table>
                              <table width="100%" border="0" cellspacing="0" cellpadding="10">
                                <tr>
                                  <td height="10">
                                  </td>
                                </tr>
                                <tr>
                                    <td>
                                    <!-- main comment begin-->
                                    &nbsp;<p>CONGRATULATIONS!<br>
                
                                    <br>
                                    <br>
                                    <!--A copy of your order was sent to:-->
                                    <span  id="send_msg">You have successfully submitted your order!<br>
                A copy of your order was sent to: <%= email %><br>
                If you have paid by PayPal, you will receive your payment receipt via your PayPal associated email address.
                </span>
                                    </p>
                                    <p>Thank you for shopping with <a href="default.asp">lucomputers.com !</a></p>
                                    <p>&nbsp;</p>
                                    <p>WHAT'S NEXT?<br>
                    
                                    <br>
                                    If you selected Bank Trasfer, Email Transfer, Money Order, Check 
                                    as your pay method, please go ahead to make your payment.&nbsp; 
                                    We are waiting for your payment.&nbsp; As soon as your payment 
                                    arrives we will send you an email confirmation and process your 
                                    order.&nbsp; Please always contact us to inform about your 
                                    payment status.</p>
                                    <p>Once your computer has been shipped we will email you 
                                    tracking number.&nbsp; You can also log in to
                                    <a href="default.asp">www.lucomputers.com</a> and 
                                    find your tracking number in your account.</p>
                    
                                    <p>If you wish to inquire about status of your order, please log 
                                    in <a href="default.asp">www.lucomputers.com</a> 
                                    and select Your Account to see details. </p>
                                    <p><br>
                 
                                    <!-- main comment end-->
                                    </td>
                                </tr>
                              </table>
                                </td>
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
<%
'　防刷?
session("current_Page_load") = false

setOrderInoviceNo(LAYOUT_ORDER_CODE)
    
conn.execute("update tb_order_helper set is_ok=1,tag=1,Is_Modify=1 where order_code='"&LAYOUT_ORDER_CODE&"'")




dim paypal_transaction_id, paypal_avs, paypal_cvv2
paypal_transaction_id 	= SQLescape(request("TRANSACTIONID"))
paypal_avs 				= SQLescape(request("AVS"))
paypal_cvv2 			= SQLescape(request("CVV2"))

if paypal_transaction_id <> "" then 
    set rs = conn.execute("select grand_total from tb_order_helper where order_code='"& LAYOUT_ORDER_CODE &"'")
    if not rs.eof then

         call   AddOrderPayRecord(LAYOUT_ORDER_CODE, rs(0), LAYOUT_PAY_RECORD_METHOD_PAYPAL)
		
    end if
    rs.close : set rs = nothing
    
    conn.execute("Update tb_order_helper set order_pay_status_id='"& LAYOUT_PAYPAL_SUCCESS &"',Is_Modify=1 where order_code='"& LAYOUT_ORDER_CODE &"'")
    conn.execute("insert into tb_order_paypal_record ( transaction, avs, cvv2, order_code, regdate) values ( '"&paypal_transaction_id&"', '"&paypal_avs&"', '"&paypal_cvv2&"','"& LAYOUT_ORDER_CODE &"', now())")
end if

response.Cookies("state_shipping") = 0
SetCookiesOrderCode("")
response.Cookies("ebay_order_code") = ""
response.Cookies("pick_up_in_person") = 0
session("pay_method_return") = false
session("Is_over_paypal") = false
%>
<iframe src="/q_admin/email_simple_invoice.aspx?order_code=<%= LAYOUT_ORDER_CODE %>&email=<%= email %>&issend=true" name="iframe1" id="iframe1" style="width: 0px; height:0px;" frameborder="0" ></iframe>
<script type="text/javascript">
$().ready(function(){
	//$('#page_main_area').load('/site/inc/inc_default.asp');
});
</script>