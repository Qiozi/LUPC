<%@LANGUAGE="VBSCRIPT" CODEPAGE="65001"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>Untitled Document</title>
</head>

<body>
<table width="100%" height="25"  border="0" cellpadding="0" cellspacing="0">
        <tr>
          <td height="27" align="center" bgcolor="#327AB8" class="text_white_12"><strong><%=ucase(title)%></strong></td>
        </tr>
      </table>
<table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
          <td >
            <table width=100% cellspacing="0" cellpadding="0" border="0" bgcolor="#ffffff" align=center>
              <tr>
                <td>
				
				<table width="100%"  border="0" cellpadding="3" cellspacing="1" bgcolor="#327AB8">
                      <tr>
                        <td bgcolor="#FFFFFF"><table width="100%"  border="0" cellpadding="0" cellspacing="1" bgcolor="#E3E3E3">
                            <tr>
                              <td bgcolor="#FFFFFF"><table width="100%"  border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                  <td width="45%">
								  <div id="case_img_big_2">	
								  </div>
								  </td>
                                  <td valign="top"><table width="100%"  border="0" cellspacing="2" cellpadding="2">
                                     <% if cint(save_cost) <> 0 then %>
								   <tr bgcolor="#f2f2f2" >
                                      <td width="35%" bgcolor="#f2f2f2" class="text_hui_11"><strong>&nbsp;Regular&nbsp;Price: </strong></td>
                                      <td align="left" bgcolor="#f2f2f2" class="price"><strong><%=ConvertDecimalUnit(current_system,current_price_rate )%></strong></td>
                                    </tr>
									<tr bgcolor="#f2f2f2" >
                                      <td width="35%" bgcolor="#f2f2f2" class="text_hui_11"><strong>&nbsp;Discount: </strong></td>
                                      <td align="left" bgcolor="#f2f2f2" class="price" style="color:red;"><strong>-$<%=ConvertDecimalUnit(current_system,save_cost)%></strong></td>
                                    </tr>
								   <% end if %>
                                        <tr bgcolor="#f2f2f2" >
                                          <td width="35%" bgcolor="#f2f2f2" class="text_hui_11"><strong>&nbsp;Now &nbsp;Low&nbsp;Price: </strong></td>
                                          <td align="left" bgcolor="#f2f2f2" class="price"><strong><%=ConvertDecimalUnit(current_system,cdbl(current_price_rate) - cdbl(save_cost))%></strong></td>
                                        </tr>
                                      <tr bgcolor="#f2f2f2" >
                                        <td class="text_hui_11"><strong>&nbsp;*Special&nbsp;Cash&nbsp;Price: </strong></td>
                                        <td class="price"><strong><%=ConvertDecimalUnit(current_system,cdbl(specal_price) )%></strong></td>
                                      </tr>
    
                                        <tr bgcolor="#f2f2f2">
                                          <td valign="top" bgcolor="#f2f2f2" class="text_hui_11"><strong>&nbsp;Quote Number: </strong></td>
                                          <td align="left" bgcolor="#f2f2f2" >&nbsp;<%=id%></td>
                                        </tr>
                                         <tr bgcolor="#f2f2f2">
                                          <td valign="top" bgcolor="#f2f2f2" class="text_hui_11"><strong>&nbsp;Date: </strong></td>
                                          <td align="left" bgcolor="#f2f2f2">&nbsp;<%=ConvertDate(system_regdate)%></td>
                                        </tr>
                                        
                                      </table>
								  		<div style="font-size:8pt;"><% response.Write(FindSpecialCashPriceComment()) %>
											<div style="height:8px; line-height:8px;font-size: 1pt;">&nbsp;</div>
                                            <p>Every unique computer takes 1-7 days to be preassembled and tested before installed into the computer chassis. System includes meticulous hand assembly and precision cable routing. We tune system performance to its best and complete driver updates. All manufacturer documentations and disks are included.</p>
                                            <p>
                                     <% if request("cmd") = "print" then %>
                                     		<table >
                                                <tr>
                                                    <td>
                                                          <table id="__01" width="115" class="noPrint" height="24" border="0" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                              <td width="6"><img src="/soft_img/app/3232.gif" width="6" height="24"></td>
                                                              <td class="btn_middle"><a href="#" onClick="javascript:print()" class="white-hui-12"><strong>Print</strong></a></td>
                                                              <td width="6"><img src="/soft_img/app/customer_bottom_04.gif" width="6" height="24" alt=""></td>
                                                            </tr>
                                                          </table>
                                                   </td>                                               
                                                   <td align="center"  style="padding:10px;" >                                          				
                                                            <table id="__01" width="115" class="noPrint" height="24" border="0" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                              <td width="6"><img src="/soft_img/app/3232.gif" width="6" height="24"></td>
                                                              <td class="btn_middle"><a href="#" onClick="javascript:window.close()" class="white-hui-12"><strong>Close Window</strong></a></td>
                                                              <td width="6"><img src="/soft_img/app/customer_bottom_04.gif" width="6" height="24" alt=""></td>
                                                            </tr>
                                                          </table>	
                                                   </td>
                                                </tr>
                                            </table>        
                                                       
                                      <% elseif request("cmd") = "email" then %>
                                      		<div id="email_input">
                                              <table width="100%" border="0" align="center" cellpadding="2" cellspacing="0">
                                                <form action="email_to_me_system_ok.asp" method="post" name="form1" id='form1' onSubmit="return toSubmit(this);" target="ifr1">
                                                    <input type="hidden" name="email_type" value="8">
                                                    <input type="hidden" name="email_to_me" value="yes">
                                                    <input type="hidden" name="id" value="<%=id%>">
                                                  <tr>
                                                    <td colspan="2">Your email address 1: <input name="textfield" id="textfield" type="text" class="b" size="30" ></td>
                                                    </tr>
                                                  <tr>
                                                    <td colspan="2">Your email address 2: <input name="textfield2"  id="textfield2" type="text" class="b" size="30">
                                                  </td>
                                                    </tr>
                                                  </form> 
                                                  <tr>
                                                    <td width="50%" align="center">                                          <table id="__01" width="115" height="24" border="0" cellpadding="0" cellspacing="0">
                                                        <tr>
                                                          <td width="6"><img src="<%=tureurl%>/Images/3232.gif" width="6" height="24"></td>
                                                          <td align="center" background="<%=tureurl%>/Images/customer_bottom_03.gif"><a href="#" onClick="window.close()" class="white-hui-12"><strong>Cancel</strong></a> </td>
                                                          <td width="6"><img src="<%=tureurl%>/Images/customer_bottom_04.gif" width="6" height="24" alt=""></td>
                                                        </tr>
                                                    </table></td>
                                                    <td width="50%" align="center">                                          <table id="__01" width="115" height="24" border="0" cellpadding="0" cellspacing="0">
                                                        <tr>
                                                          <td width="6"><img src="<%=tureurl%>/Images/3232.gif" width="6" height="24"></td>
                                                          <td align="center" background="<%=tureurl%>/Images/customer_bottom_03.gif"><a href="#" class="white-hui-12" onClick="return toSubmit(document.getElementById('form1'));"><strong>Send</strong></a></td>
                                                          <td width="6"><img src="<%=tureurl%>/Images/customer_bottom_04.gif" width="6" height="24" alt=""></td>
                                                        </tr>
                                                    </table></td>
                                                  </tr>
                                                </table>
                                        </div>
								 <% else %>
                                    	<table >
                                        	<tr>
                                            	<td>
                                                <table id="__2" width="90" height="24" border="0" cellpadding="0" cellspacing="0" align="right">
                                                      <tr>
                                                        <td width="28"><img src="/soft_img/app/buy_car.gif" width="28" height="24" alt="" /></td>
                                                        <td align="center" style="background: url(/soft_img/app/customer_bottom_03.gif)"><a class="white-hui-12"  onClick="opener.document.getElementById('form1').submit(); window.close();return false;"><strong>Buy It</strong></a> </td>
                                                        <td width="6"><img src="/soft_img/app/customer_bottom_04.gif" width="6" height="24" alt="" /></td>
                                                      </tr>
                                                    </table>
                                                </td>
                                                <td>
                                                <table id="__3" width="185" height="24" border="0" cellpadding="0" cellspacing="0" align="right">
                                                      <tr>
                                                        <td width="28"><img src="/soft_img/app/buy_car.gif" width="28" height="24" alt="" /></td>
                                                        <td align="center" style="background:url(/soft_img/app/customer_bottom_03.gif)"><a href="computer_system_to_cart.asp?cmd=arrange" class="white-hui-12" onClick="opener.window.location.href = this.href ;window.close(); return false;"><strong>Arrange a local pick up</strong></a> </td>
                                                        <td width="6"><img src="/soft_img/app/customer_bottom_04.gif" width="6" height="24" alt="" /></td>
                                                      </tr>
                                                 </table>
                                                </td>
                                            </tr>
                                        </table>
                                 <% end if%>
                                    </p>
								  </div>

								  </td>
                                </tr>
                                
                              </table>
                                <table width="100%"  border="0" cellspacing="0" cellpadding="2">
                                  <tr>
                                    <td colspan="3" >
									<!-- begin -->
									
									<TABLE cellSpacing="0" cellPadding="3" width="100%">
                                      <TBODY>
									  
							<%
								set rs  = conn.execute("select p.product_name,pg.part_group_name, p.product_serial_no ,"&_
								"p.product_current_price-product_current_discount sell,"&_
"case when product_store_sum >2 then 2 "&_
"when ltd_stock >2 then 2  "&_
"when product_store_sum + ltd_stock >2 then 2  "&_
"when product_store_sum  <=2 and product_store_sum >0 then 3 "&_
"when product_store_sum +ltd_stock <=2 and product_store_sum +ltd_stock >0 then 3 "&_
"when ltd_stock <=2 and ltd_stock >0 then 3 "&_
"when product_store_sum +ltd_stock =0 then 4 "&_
"when product_store_sum +ltd_stock <0 then 5 end as ltd_stock , part_quantity from tb_sp_tmp_detail sp inner join tb_product p on sp.product_serial_no=p.product_serial_no inner join tb_part_group pg on sp.part_group_id=pg.part_group_id inner join tb_product_category pc on pc.menu_child_serial_no=p.menu_child_serial_no where sp.sys_tmp_code="&id&" and (p.is_non=0 or p.product_name like '%onboard%') order by   sp.product_order asc ")
						if not rs.eof then
							rscount = 0
							do while not rs.eof 
							rscount = rscount + 1
							
							%>
							<tr bgcolor="<%if rscount mod 2 = 1 then response.write "#efefef" else response.write "white"%>">
                            <td align="left" class="text_hui_11"><strong><%= rs("part_group_name")%> </strong></td>
                            
                            <td align="left" class="text_hui_11"><a class="hui-orange-s" onClick="return js_callpage_cus('<%= LAYOUT_HOST_URL %>view_part.asp?id=<%= rs("product_serial_no")%>', 'view', 602, 600);">
							<%=rs("product_name") & FindPartStoreStatus_system_setting(rs("ltd_stock"))%>
							</a></td>
							<td style="width: 15px;" class="text_hui_11">
                            
                            x <%
                                        response.Write rs("part_quantity")
                            %>
                            </td>
                          </tr>
							<%rs.movenext
							
							loop
							end if
							rs.close : set rs = nothing
							%>
                                      </TBODY>
                                    </TABLE>
										<!--end -->
									</td>
                                  </tr>
                                  <tr>
                                    <td colspan="3" align="left"  style="padding-left:10px; padding-right:10px; padding-bottom:10px; padding-top:10px;" ><p class="text_hui_11">                                      <strong>Congratulations!</strong> <br>
                                      You have successfully configured a new system. The quote number displays on top of this page. If you are not to submit your order now please keep this quote by emailing to yourself. </p>
                                      <p><span class="text_hui_11">You can continue submitting your order now or place your order later: <br><br>
                                      1) Visit www.lucomputers.com to use our fully automated and secure online ordering system, Enter this quote number to the box at the upper right corner of the web page. Submit your order when system configuration is loaded.<br>
                                      <br>
                                      2) Call us by phone (Toll free 1866.999.7828 or 416.446.7743) during business hours and please refer to this quote number. Do not hesitate to ask any questions and further changes to the configuration.<br>
                                      <br>
                                      3) Visit us in our store at 1875 Leslie Street, Unit 24, Toronto.<br><br>
                                      Shipping and applicable taxes are not included in quotation. USA customers are tax and duty free, Ontario residents 13%, HST provinces 13%, rest of Canada 5%. If pickup is your preferred method please load this quote and click button ARRANGE A LOCAL PICK UP.<br>
                                      <br>
                                      Business Hours: Mon-Fri 10AM - 7PM EST Sat 11AM - 4PM EST</span><br>
                                      </p></td>
                                  </tr>
                                  
                                </table>
                              </td>
                            </tr>
                        </table>                        </td>
                      </tr>
                  </table>                </td>
              </tr>
              <tr height=4>
                <td align="center" valign="middle"><TABLE border=0 cellpadding=0 cellspacing=0 style="width:100%px; background: #CCCCCC;">
                    <TR>
                      <TD></TD>
                    </TR>
                </TABLE></td>
              </tr>
          </table></td>
        </tr>
    </table>
</body>
</html>
