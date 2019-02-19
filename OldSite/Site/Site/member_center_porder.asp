<!--#include virtual="site/inc/inc_page_top.asp"-->
<%
	dim payOrderCode : payOrderCode = 0


%>
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
                	<% Call ValidateUserSessionInfo(LAYOUT_CCID ,"closeConn")
					
						Dim subRS
					%>
                		<table width="100%" height="670"  border="0" align="center" cellpadding="0" cellspacing="2" bgcolor="#FFFFFF" style="border:#8FC2E2 1px solid; ">
                              <tr>
                                <td valign="top" style="border:#E3E3E3 1px solid; "><table style="border-bottom:#CDE3F2 1px solid; " width="100%"  border="0" cellspacing="0" cellpadding="3">
                                  <tr align="center">
                                    <td width="25%" class="info_nav2" onClick="window.location.href='<%= LAYOUT_HOST_URL %>member_center_info.asp'">My Profile </td>
                                  	<td width="25%" class="info_nav"  onClick="window.location.href='<%= LAYOUT_HOST_URL %>member_center_porder.asp'">Pending Orders </td>
                                    <td width="25%" class="info_nav2" onClick="window.location.href='<%= LAYOUT_HOST_URL %>member_center_aorder.asp'">All Orders </td>
                                    <td width="25%" class="info_nav2" onClick="window.location.href='<%= LAYOUT_HOST_URL %>member_logout.asp'">Logout</td>
                                  </tr>
                                </table>
                                  <table width="98%" height="200"  border="0" align="center" cellspacing="0">
                                    <tr>
                                      <td valign="top" bgcolor="#FFFFFF"><table width="100%" border="0" cellspacing="0" cellpadding="0">
                                        <tr>
                                          <td height="5"></td>
                                        </tr>
                                      </table>
                <%
                                            
                                            set rs = conn.execute("select oh.order_source, oh.msg_from_seller,oh.total, order_code, ps.pre_status_name ,order_date,customer_first_name,customer_last_name ,oh.grand_total,oh.price_unit from tb_order_helper oh inner join tb_customer c on c.customer_serial_no=oh.customer_serial_no inner join tb_pre_status ps on ps.pre_status_serial_no=oh.pre_status_serial_no where c.customer_serial_no=" & SQLquote(LAYOUT_CCID) & " and ps.pre_status_serial_no<>"& LAYOUT_ORDER_STATUS_FINISHED &" and oh.is_ok=1 order by order_helper_serial_no desc")
                
                                            if not rs.eof then
                                            do while not rs.eof 
                                        %>
                                        <div style="background:#CCCCCC"><table width="100%" border="0" cellpadding="4" cellspacing="1" style="text-align: left; white-space: pre-wrap;white-space: -moz-pre-wrap;white-space: -pre-wrap;white-space: -o-pre-wrap;word-wrap: break-word;table-layout:fixed;">
                                          <tr>
                                            <td width="22%" class="text_hui_11" style="background:#FFFFFF"><span class="text_hui_11"><strong>ORDER #</strong></span></td>
                                            <td width="22%" class="text_hui_11" style="background:#FFFFFF"><strong>DATE</strong></td>
                                            <td class="text_hui_11" style="background:#FFFFFF"><strong>NAME</strong></td>
                                            <td width="22%" class="text_hui_11" style="background:#FFFFFF"><strong>AMOUNT</strong></td>
                                          </tr>
                                          <tr>
                                            <td class="text_hui_11" style="background:#FFFFFF"><% if cstr(rs("order_source")) <> "3" then %><a href="<%= LAYOUT_HOST_URL %>order_detail.asp?order_type=1&order_code=<%= rs("order_code") %>"><% else %><a href="<%= LAYOUT_HOST_URL %>order_detail_ebay.asp?order_type=1&order_code=<%= rs("order_code") %>"><% end if %><%= rs("order_code") %></a></td>
                                            <td class="text_hui_11" style="background:#FFFFFF"><%= ConvertDate(rs("order_date")) %></td>
                                            <td class="text_hui_11" style="background:#FFFFFF"><%= rs("customer_first_name") & " " & rs("customer_last_name") %></td>
                                            <td class="text_hui_11" style="background:#FFFFFF"><% if rs("grand_total")<>"" then  response.write formatcurrency(rs("grand_total"), 2) else response.write formatcurrency(0, 2) %><span class="price_unit"><%= rs("price_unit") %></span></td>
                                          </tr>
                                          <%
                                            dim Estimated_Shipping_Date
                                            dim ups_tracking_number
                                            Estimated_Shipping_Date = ""
                                            set subRS = conn.execute("select date_format(regdate,'%m/%d/%Y') regdate,ups_tracking_number from tb_order_ups_tracking_number where order_code='"&rs("order_code")&"'")
                                            if not subRS.eof then 
                                                Estimated_Shipping_Date = subRS(0)
                                                ups_tracking_number = subRS(1)
                                            end if
                                            subRS.close : set subRS = nothing
                                            if Estimated_Shipping_Date <> "" then 
                                            
                                          %>
                                          <tr>
                                            <td colspan="4" class="text_hui_11" style="background:#FFFFFF"><strong>SHIPPING DATE:</strong>&nbsp;&nbsp;<%= Estimated_Shipping_Date %> </td>
                                          </tr>
                                          <tr>
                                            <td colspan="4" class="text_hui_11" style="background:#FFFFFF"><strong>UPS TRACKING NUMBER:</strong>&nbsp;&nbsp;<%= ups_tracking_number %> </td>
                                          </tr>
                                          <% end if %>
                                          
                                          <% if payOrderCode = rs("order_code") then %>
                                          <tr><form action="shopping_check_method.asp" method="post">
                                          		<td colspan="4" class="text_hui_11" style="background:#FFFFFF">
    Pay Method:<br />
    &nbsp;&nbsp;&nbsp;&nbsp;<input type="radio" name="paymethod_paypal" id='paymethod_Paypal' value="15" /> 1. Paypal<br />
    &nbsp;&nbsp;&nbsp;&nbsp;<input type="radio" name="paymethod_paypal" value="25" checked="checked"/>2. Visa /Master Card<br />
    <input type="hidden" name="OrderCode" value="<%=rs("order_code") %>" />
    <input type="submit" name="paysubmit" value="Pay"  />
                                                </td>
                                               </form>
                                          </tr>
                                          <% end if %>
                                          
                                          <tr>
                                            <td colspan="4" class="text_hui_11" style="background:#FFFFFF"><strong>STATUS:</strong>&nbsp;&nbsp;<%= rs("pre_status_name") %></td>
                                          </tr>
                                          
                                          <tr>
                                            <td colspan="4" class="text_hui_11" style="background:#FFFFFF"><strong>MESSAGE :
                                            </strong>
                                              <div ><%
                                                    set crs= conn.execute("select msg_id, msg_order_code, msg_content_text, msg_type, msg_author, staff_id, regdate  from tb_chat_msg where msg_order_code='"&rs("order_code")&"'")
                                                    if not crs.eof then 
                                                        do while not crs.eof 
                                                            response.write trim(crs("msg_author"))
                                                            response.write "["& ConvertDateHour(crs("regdate")) &"] <pre style='margin-top:0px'>"& trim(crs("msg_content_text") )& "</pre>"						
                                                            response.write "<hr size=1>"
                                                        crs.movenext
                                                        loop
                                                    end if
                                                    crs.close : set crs = nothing
                                                %>                              
                                              </div></td>
                                          </tr>
                                        </table>
                                        </div>
                                        <div style="text-align:right; padding-top:5px;"><table id="__01" width="190" height="24" border="0" cellpadding="0" cellspacing="0">
                                          <tr>
                                            <td width="28"> <img src="/soft_img/app/arrow_send.gif" width="28" height="24" alt=""></td>
                                            <td class="btn_middle"><a class="btn_img" onClick="js_callpage_cus('customer_send_msg.asp?order_code=<%= rs("order_code") %>', 'Custoemr_Chat', 470, 320)" ><strong>Send Seller a Message</strong></a> </td>
                                            <td width="6"> <img src="/soft_img/app/customer_bottom_04.gif" width="6" height="24" alt=""></td>
                                          </tr>
                                        </table></div><br>
                                         
                                      
                                           <%
                                            rs.movenext
                                            loop
                                            
                                            end if
                                            rs.close : set rs = nothing
                                        %>
                                           
                                          
                                           
                                            
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
                
                </div>
            <!-- main end 	-->
        </td>

        <td valign="bottom" id="page_main_right_backgroundImg" style="border-left: 1px solid #8E9AA8"><img src="/soft_img/app/left_bt.gif" width="14" height="214"></td>
    </tr>
</table>

<!--#include virtual="site/inc/inc_bottom.asp"-->
