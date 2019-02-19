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
                	<% Call ValidateUserSessionInfo(LAYOUT_CCID ,"closeConn")
					
						Dim subRS
					%>
                		<table width="100%" height="670"  border="0" align="center" cellpadding="0" cellspacing="2" bgcolor="#FFFFFF" style="border:#8FC2E2 1px solid; ">
                              <tr>
                                <td valign="top" style="border:#E3E3E3 1px solid; "><table style="border-bottom:#CDE3F2 1px solid; " width="100%"  border="0" cellspacing="0" cellpadding="3">
                                  <tr align="center">
                                    <td width="25%" class="info_nav2" onClick="window.location.href='<%= LAYOUT_HOST_URL %>member_center_info.asp'">My Profile </td>
                                  	<td width="25%" class="info_nav2" onClick="window.location.href='<%= LAYOUT_HOST_URL %>member_center_Porder.asp'">Pending Orders </td>
                                    <td width="25%" class="info_nav"  onClick="window.location.href='<%= LAYOUT_HOST_URL %>member_center_Aorder.asp'">All Orders </td>
                                    <td width="25%" class="info_nav2" onClick="window.location.href='<%= LAYOUT_HOST_URL %>member_logout.asp'">Logout</td>
                                  </tr>
                                </table>
                                  <table width="100%" height="200"  border="0" cellspacing="0">
                                    <tr>
                                      <td valign="top" bgcolor="#FFFFFF"><table width="100%" border="0" cellspacing="0" cellpadding="0">
                                        <tr>
                                          <td height="5"></td>
                                        </tr>
                                      </table>
                                        <table width="98%" border="0" align="center" cellpadding="0" cellspacing="0">
                                          <tr >
                                            <td width="10%" height="20" align="center" bgcolor="#ECF6FF" class="text_hui_11" style="border-right:#CDE3F2 1px solid; border-bottom:#CDE3F2 1px solid; " ><strong>ORDER #</strong></td>
                                            <td width="15%" align="center" bgcolor="#ECF6FF" class="text_hui_11" style="border-right:#CDE3F2 1px solid; border-bottom:#CDE3F2 1px solid; "><strong>STATUS </strong></td>
                                            <td width="20%" align="center" bgcolor="#ECF6FF" class="text_hui_11" style="border-right:#CDE3F2 1px solid; border-bottom:#CDE3F2 1px solid; "><strong>DATE</strong></td>
                                            <td width="20%" align="center" bgcolor="#ECF6FF" class="text_hui_11" style="border-bottom:#CDE3F2 1px solid; "><strong>DOWNLOAD </strong></td>
                                            <td width="20%" align="center" bgcolor="#ECF6FF" class="text_hui_11" style="border-bottom:#CDE3F2 1px solid; "><strong>AMOUNT </strong></td>
                                          </tr>
                                          <%
                                            
                                            set rs = conn.execute("select oh.order_source ,oh.msg_from_seller,oh.total,oh.order_code, ps.pre_status_name ,ps.view_invoice,order_date,customer_first_name,customer_last_name,oh.pre_status_serial_no,c.customer_shipping_state,oh.grand_total, oh.price_unit from tb_order_helper oh inner join tb_customer_store c on c.order_code=oh.order_code inner join tb_pre_status ps on ps.pre_status_serial_no=oh.pre_status_serial_no where c.customer_serial_no=" & SQLquote(LAYOUT_CCID) & "  and oh.is_ok=1 order by order_helper_serial_no desc")
                
                                            if not rs.eof then
                                            do while not rs.eof 
                                        %>
                                          <tr >
                                            <td height="20" align="left" class="text_hui_11" style="border-right:#CDE3F2 1px solid; border-bottom:#CDE3F2 1px solid; padding:4px; " >
                                            <% if cstr(rs("order_source")) <> "3" then %>
                                                <a href="<%= LAYOUT_HOST_URL %>order_detail.asp?order_code=<%= rs("order_code") %>">
                                            <% else %>
                                                <a href="<%= LAYOUT_HOST_URL %>order_detail_ebay.asp?order_code=<%= rs("order_code") %>">
                                            <% end if %>
                                            <%= rs("order_code") %></a></td>
                                            <td align="left" class="text_orange_11" style="border-right:#CDE3F2 1px solid; border-bottom:#CDE3F2 1px solid; padding:4px; "><%= rs("pre_status_name") %></td>
                                            <td align="left" class="text_hui_11" style="border-right:#CDE3F2 1px solid; border-bottom:#CDE3F2 1px solid; padding:4px; "><%= ConvertDate(rs("order_date")) %></td>
                                            <td align="right" class="text_hui_11" style="border-right:#CDE3F2 1px solid; border-bottom:#CDE3F2 1px solid; padding:4px; text-align:center">
                                    <% if  1 = rs("view_invoice") then %>
                                                    <span style="cursor: pointer" onClick="js_callpage_cus('q_admin/download_order_pdf.aspx?order_code=<%= rs("order_code") %>&customer_state=<%= rs("customer_shipping_state") %>', 'down_order_pdf', 1, 1);">Download</span>
                                                <% else %>
                                                &nbsp;
                                                <% end if %>							</td>
                                            <td align="right" class="text_hui_11" style="border-bottom:#CDE3F2 1px solid; padding:4px;"><% if rs("grand_total")<>"" then  response.write formatcurrency(rs("grand_total"), 2) else response.write formatcurrency(0, 2) %> <span class="price_unit"><%= rs("price_unit")%></span>                           </td>
                                          </tr>
                                          <%
                                            rs.movenext
                                            loop
                                            
                                            end if
                                            rs.close : set rs = nothing
                                        %>
                                          
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
