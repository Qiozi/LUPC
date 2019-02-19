<!--include virtual="public_helper/custom_helper.asp"-->
<!--#include virtual="site/inc/inc_helper.asp"-->
<%
	'' ' ' '' ' ' ' ' ' '' ' '' ' ' ' ' '' ' '  ' ' ' ' ' ' ' ' ' ' '  '
	'
	'	定义参数
	'
	' ' '' ' ' ' ' ' ' ' ' ' ' ' ' ' ' ' ' ' ' ' ' ' ' ' ' ' ' ' ' ' ' '
	dim templete_system_info(100, 6) 
	session("templete_system_info")	= null

	Dim case_sku		:		case_sku		=	null
 ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
 
	' 加载没有显示明细。
	session("view_system") = false
	session("is_change") = false

	dim system_templete_serial_no, category, factory, system_code
	system_code = SQLescape(request("system_code"))
	category = SQLescape(request("cid"))
	system_templete_serial_no = SQLescape(request("id"))	
	

	'------------------------------
	'
	' 如果是已配置后的產品， 先取得system_templete_serial_no
	'
	'------------------------------
	if len(system_code) = 8 then 
		set rs = conn.execute("select system_templete_serial_no from tb_system_code_store where system_code='"& system_code &"'")
		if not rs.eof then	
			system_templete_serial_no = rs(0)
		end if
		rs.close : set rs = nothing
	end if
	
	'-------------------------------
	' 存储 system_templete_serial_no
	'-------------------------------
	
	session("system_templete_serial_no") = system_templete_serial_no
	
	'-------------------------------
	' 存储 system code
	'-------------------------------
	if len(system_code) = 8 then 
		session("current_custom_system_code") = system_code
	else
		session("current_custom_system_code")  = ""
	end if
	

	
	'response.write system_templete_serial_no
	
	if len(system_templete_serial_no)=0 then closeconn():response.Write("no found info."):response.End()
	
	dim old_save_cost, old_current_price, old_current_price_rate, Special_cash_price



' 首次弹出时间
dim open_datetime 
open_datetime =now()
%>
<input type="hidden" value="<%= open_datetime %>" id="open_datetime">
<!-- warry 3 year execute method-->
<input type="hidden" id="warray3yearExecuteMethod" >
<input type="hidden" id="warray3yearExecuteMethodProductID" >
<input type="hidden" id="oldWarray3yearExecuteMethod" >
<input type="hidden" id="oldWarray3yearExecuteMethodProductID" >


              <table width="600" height="670" border="0" align="center" cellpadding="0" cellspacing="2" bgcolor="#FFFFFF" style="border:#8FC2E2 1px solid; ">
                <tr>
                  <td style="border:#E3E3E3 1px solid; " valign="top"><table width="100%"  border="0" cellspacing="0" 

cellpadding="0">
                    <tr>
                      <td width="300" valign="top"><table width="90%" height="90%"  border="0" align="center" cellpadding="0" 

cellspacing="0">
                        <tr>
                          <td width="227">
						 <div id="case_img_big">
						  <!-- <input type="hidden" id="product_big_image" name="product_big_image" value="<%=product_image_1_g%>">-->
                          <span id="case_img_big_2">
                          <%
						 
							'WriteSystemBigImg(crs(0))  
                        
						  %>
						  </span>                          
						  </div>                          
						  </td>
                        </tr>
                      </table></td>
                      <td valign="top"><table width="100%"  border="0" cellspacing="0" cellpadding="0">
                          <tr>
                            <td >
								<%
								
								%>
							<span id="custem_system_price_area">
							<table width="100%"  border="0" cellspacing="2" cellpadding="2">
								 <% if cint(old_save_cost) <> 0 then %>
							   <tr bgcolor="#f2f2f2" >
								  <td width="35%" bgcolor="#f2f2f2" class="text_hui_11"><strong>&nbsp;Regular&nbsp;Price: </strong></td>
								  <td align="left" bgcolor="#f2f2f2" class="text_hui_11"><strong><%=formatcurrency(cdbl(old_current_price_rate))%></strong></td>
								</tr>
								<tr bgcolor="#f2f2f2" >
								  <td width="35%" bgcolor="#f2f2f2" class="text_hui_11"><strong>&nbsp;Discount: </strong></td>
								  <td align="left" bgcolor="#f2f2f2" style="text-align:right">
                                  		<strong>
                                  			<span class="price_dis">-$<%=formatnumber(old_save_cost)%></span>
                                        </strong>
                                  </td>
								</tr>
							   <% end if %>
								<tr bgcolor="#f2f2f2" >
								  <td width="35%" bgcolor="#f2f2f2" class="text_hui_11"><strong>&nbsp;Now &nbsp;Low&nbsp;Price: </strong></td>
								  <td align="left" bgcolor="#f2f2f2" class="text_hui_11"><strong><%=formatcurrency(cdbl(old_current_price_rate) - old_save_cost, 2, -1, 0)%></strong></td>
								</tr>
							  <tr bgcolor="#f2f2f2" >
								<td class="text_hui_11"><strong>&nbsp;Special&nbsp;Cash&nbsp;Price: </strong></td>
								<td class="text_hui_11"><strong><%=formatcurrency(Special_cash_price)%></strong></td>
							  </tr>							
								
							  </table>
							</span></td>
                          </tr>
                          <tr>
                            <td height="20"><table width="100%" height="1"  border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                  <td background="images/line2.gif"><img src="images/line2.gif" width="3" height="1"></td>
                                </tr>
                            </table></td>
                          </tr>
                          <tr>
                            <td style="text-align:left;">The system you customize below will be fully assembled and tested before delivery. <br>
                            All components are brand new.</td>
                          </tr>
                          <tr>
                            <td>&nbsp;</td>
                          </tr>
                          <tr>
                            <td height="30">&nbsp;</td>
                          </tr>
                      </table></td>
                    </tr>
                    <tr>
                      <td colspan="2">
					  
					  <form action="" name="save" id="save" method="post">
						<input type="text" name="system_sku" value="<%= system_templete_serial_no %>" />
                    <table width="100%"  border="0" cellspacing="0" cellpadding="0">
                     <tr>
                        <td height="25">
							<div class="title1" style="cursor:hand; padding-top:3px;" id="product_c_1" onClick="getSet(1);">	
								<strong>Major&nbsp;&nbsp;Components</strong>
							</div>
							<div class="title2" style="cursor:hand; padding-top:3px;" id="product_c_2" onClick="getSet(2);" >	
								<strong>Accessories</strong>
							</div>
							<div class="title2" style="cursor:hand; padding-top:3px;" id="product_c_3" onClick="getSet(3);" >	
								<strong>Additional&nbsp;&nbsp;Parts</strong>
							</div>		
							</td>
                      </tr>
					   
					  <tr>
                        <td height="26" id="sys_parts_area">						
						<%
dim c_list, c_list_arr, all_product_count
all_product_count = 0
qn=0
dim area_group_count
area_group_count =0								
for i=1 to 3							
	qn=i
	if i = 1 then									
		c_list = application("Major_Components")
	end if
	
	if i = 2 then 
		c_list = application("Accessories")
	end if
	
	if i=3 then 
		c_list = application("Additional_Parts")
	end if
	
	response.Write("<div id=""cust_plane_"&qn&""" style=""display:")
	if qn=1 then response.Write "" else response.Write "none;"
	response.Write( """>")
	
	if trim(c_list) <> "" then 		
		dim s_detail_tmp_sql						
		if len(system_code) = 8 then 
			s_detail_tmp_sql	=	"(select part_quantity, part_max_quantity,system_product_serial_no,product_order,system_templete_serial_no, product_serial_no,1 showit,part_group_id  from tb_sp_tmp_detail where sys_tmp_code='"&system_code&"')"			
		else
			s_detail_tmp_sql = "select id system_product_serial_no,part_quantity,max_quantity part_max_quantity,id product_order,part_group_id,luc_sku product_serial_no,system_sku system_templete_serial_no,'1' showit from tb_ebay_system_parts where system_sku='"& system_templete_serial_no & "'"
		end if		
		
		set rs = conn.execute("select sp.system_product_serial_no ,pg.part_group_id,pg.part_group_name,mc.menu_child_serial_no,menu_child_name,menu_pre_serial_no, p.product_current_price,  sp.part_quantity, sp.part_max_quantity, p.product_name, p.product_serial_no, p.other_product_sku,p.is_non,sp.product_order, p.tag from tb_part_group pg inner join tb_product_category mc on pg.product_category=mc.menu_child_serial_no inner join  "&s_detail_tmp_sql&" sp on  sp.part_group_id=pg.part_group_id inner join tb_product p on p.product_serial_no=sp.product_serial_no where system_templete_serial_no='"&system_templete_serial_no&"' and product_category in ("&c_list&") and pg.showit=1  and sp.showit=1 and p.tag=1 and mc.tag=1 order by menu_child_order, sp.product_order asc")
		if not rs.eof then
		area_group_count = 0
		'response.Write((timer() - begin_timer)&"<br>")
		dim product_serial_no
		dim part_quantity : part_quantity = 1
		dim part_max_quantity : part_max_quantity = 1
		do while not rs.eof			
			part_quantity = cint(rs("part_quantity"))
			part_max_quantity = cint(rs("part_max_quantity"))
			'
			'	把产品数据存到session
			'
			'
			    templete_system_info(all_product_count, 0) = rs("system_product_serial_no")
			    templete_system_info(all_product_count, 1) = rs("product_serial_no")
			    templete_system_info(all_product_count, 2) = rs("part_group_id")
			    templete_system_info(all_product_count, 3) = rs("product_order")
			    templete_system_info(all_product_count, 4) = part_quantity
			    templete_system_info(all_product_count, 5) = part_max_quantity ' is export to page
			    all_product_count = all_product_count + 1
			    area_group_count = area_group_count + 1

		rs.movenext
		loop

		dim templete_system_infos 

		rs.movefirst
       
			
			dim current_price, plane_count, part_group_id, area_count, system_product_serial_no
			acce_n = 0 '输出CPU等产品
			plane_count = 0
			area_count = 0
			dim is_show
			is_show = false							
			do while not rs.eof 
			    part_group_id = rs("part_group_id")
                part_quantity = cint(rs("part_quantity"))
				system_product_serial_no = rs("system_product_serial_no")
				area_count = area_count + 1
				
				plane_count = plane_count + 1
				current_price = cdbl(rs("product_current_price")) * part_quantity
				'response.Write(system_product_serial_no)

%>
				
				<div id="product_plane_<%=qn%>" >				
					<table  width="100%" height="22" border="0" cellpadding="0" cellspacing="0" bgcolor="#efefef" style="cursor:hand; border-top: 1px solid #cccccc; <%if area_count = area_group_count then response.Write("border-bottom: 1px solid #cccccc;")%>" id="plane_<%=qn%>_<%=area_count%>">
						<tr onClick="displayProductGroup2('product_group_<%=rs("menu_child_serial_no")%>','product_check_<%=rs("menu_child_serial_no")%>','img_v_<%=rs("menu_child_serial_no")%>_<%=area_count%>','table_plane_group_<%=rs("menu_child_serial_no")%>_<%= plane_count%>', 'plane_<%=qn%>_<%=area_count%>', '<%if area_count = area_group_count then response.Write("1") else response.Write("0") %>','<%= system_product_serial_no %>',  'sub_group_detail_<%=system_product_serial_no%>', '<%= rs("part_max_quantity") %>', '<%= rs("part_quantity") %>', '<%=rs("product_serial_no")%>');">
							<td width="16">
                            	<%	
								
									'response.Write(rs("part_group_id"))
									'
									' reponse write warrary info
									'
									if cstr(rs("part_group_id")) = warrary_group_id then 
										 
										if area_count = area_group_count then warry_area_count = "1" else warry_area_count ="0"
										response.write "<input type=""hidden"" value=""sub_group_detail_"&system_product_serial_no&""" id=""warrary3yearHidden"" >"
										response.Write("<input type=""hidden"" value=""displayProductGroup3('product_group_"&rs("menu_child_serial_no")&"','product_check_"&rs("menu_child_serial_no")&"','img_v_"&rs("menu_child_serial_no")&"_"& area_count &"','table_plane_group_"& rs("menu_child_serial_no") &"_"& plane_count &"', 'plane_"& qn &"_"& area_count &"', '"& warry_area_count &"','"&  system_product_serial_no &"',  'sub_group_detail_"& system_product_serial_no &"', true);"" id=""warrary3yearHiddenValue"" >")
									end if
								%>
                            &nbsp;
                            	<input type="hidden" value="<%= rs("part_quantity") %>" id="current_part_quantity_<%= system_product_serial_no %>" />
								<input type="hidden" value="<%=rs("product_serial_no")%>" id="current_<%= system_product_serial_no %>">
								<input type="hidden" value="img_product_<%= system_product_serial_no %>_<%=rs("product_serial_no")%>" id="current_img_logo_<%= system_product_serial_no %>">
								<input type="hidden" value="product_child_img_product_<%= system_product_serial_no %>_<%=rs("product_serial_no")%>" id="a_product_name_<%= system_product_serial_no %>">
                                <script>
                                document.getElementById("a_product_name_<%= system_product_serial_no %>").value = "product_child_img_product_<%= system_product_serial_no %>_<%=rs("product_serial_no")%>";
                                </script>
							</td>
							<td style='text-align: left;'>
								<table cellpadding="0" cellspacing="0" width="100%">
									<tr>
										<td style="width: 120px;"><strong><%=  rs ("part_group_name") %>:</strong></td>
										<td>
                                        	<span id="current_part_quantity_view_<%= system_product_serial_no %>" style="
                                            <% if rs ("part_quantity") >1 then response.write "display:'';" else response.write "display:none;" %>
                                            color:blue;"><%=  rs ("part_quantity") %>X</span>

                                        	<span id="product_head_<%= rs("system_product_serial_no")%>">
                                        		
												<% if cstr(rs("tag")) = "0" then 
												        response.Write( NONE_SELECTED_TITLE) 
												      else 												        
												            response.Write rs("product_name")												        
												      end if %>
											</span>
                                            <span style="display:none;">
                                                (<%=rs("part_group_id")%>)	
                                            </span>		
																		
											</td>
									</tr>
								</table>							</td>
							<td width="24" style="padding-top:3px; " align="center"><a href="#position1"><img  style="cursor:hand" src="/soft_img/app/cust_arrow_2.gif"  border="0" id="img_v_<%=rs("menu_child_serial_no")%>_<%=area_count%>"></a> </td>
							<td width="6">&nbsp;</td>
						</tr>
					</table>
                    	<!--begin sub list-->
                        	<div id="sub_group_detail_<%=system_product_serial_no%>" style="display:none; text-align:center">Loading...</div>
                        <!--end   sub list-->
					</div>
					<%

        rs.movenext
        loop
    end if
    rs.close :set rs = nothing									
end if
'
'
' save configure info
'
session("templete_system_info") =	templete_system_info				
            
response.Write("</div>")

'parentrs.movenext
'								loop
'end if
'parentrs.close : set parentrs= nothing
next
						%>
						
						</td>
                      </tr>
                      <tr>
                        <td></td>
                      </tr><%if session("MN")>2 then%><%end if%>
                    </table>
					</form>
					
                      </td>
                    </tr>
                  </table>
                    <table width="100%"  border="0" cellspacing="0" cellpadding="0">
                      <tr>
                        <td height="40" align="right" style="color:#ff6600;font-weight:bold">
                        Updated Price : <span id="currentprice_3"><strong><%=formatcurrency(getSystemProductPrice)%></strong></span> 
                       
                        </td>
                      </tr>
                      <tr>
                        <td height="25" align="right" class="text_small">
						
						<table width="99%"  border="0" cellspacing="0" cellpadding="0" align="right">
                          <tr align="center">
						  	 <td width="155">
						  	 <table id="__01" width="155" height="24" border="0" cellpadding="0" cellspacing="0" style="display:none">
                                <tr>
                                  <td width="28"><img src="images/Review.gif" width="28" height="24" alt=""></td>
                                  <td align="center" style="background:url(/soft_img/app/customer_bottom_03.gif)"><div id="view_system_print"><a href="/computer_system_save_to_ebay.asp?1=1" onClick="return js_callpage_cus(this.href)"  class="white-hui-12"><strong>Ebay</strong></a></div></td>
                                  <td width="6"><img src="images/customer_bottom_04.gif" width="6" height="24" alt=""></td>
                                </tr>

                            </table>
                            </td>
							 <td ><table id="__01" width="155" height="24" border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                  <td width="28"><img src="/soft_img/app/Review.gif" width="28" height="24" alt=""></td>
                                  <td align="center" style="background:url(/soft_img/app/customer_bottom_03.gif)"><div id="view_system_print"><a href="view_configure_system.asp?1=1" onClick="return js_callpage_cus(this.href, 'system_view', 622, 620)"  class="btn_img"><strong>System Review</strong></a></div></td>
                                  <td width="6"><img src="/soft_img/app/customer_bottom_04.gif" width="6" height="24" alt=""></td>
                                </tr>
                            </table></td>
                            <td><table id="__01" width="115" height="24" border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                  <td width="28"><img src="/soft_img/app/reset.gif" width="28" height="24" alt=""></td>
                                  <td align="center" style="background:url(/soft_img/app/customer_bottom_03.gif)"><a onclick="window.location.reload();" class="btn_img"><strong>Reset</strong></a> </td>
                                  <td width="6"><img src="/soft_img/app/customer_bottom_04.gif" width="6" height="24" alt=""></td>
                                </tr>
                            </table></td>
                            <td><table id="__01" width="115" height="24" border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                  <td width="28"><img src="/soft_img/app/arrow_right.gif" width="28" height="24" alt=""></td>
                                  <td align="center" style="background:url(/soft_img/app/customer_bottom_03.gif)"><a href="javascript:customerSubmit();"  class="btn_img"><strong><span id="submit_button">Next</span></strong></a> </td>
                                  <td width="6"><img src="/soft_img/app/customer_bottom_04.gif" width="6" height="24" alt=""></td>
                                </tr>
                            </table></td>
                          </tr>
                        </table></td>
                      </tr>
                      <tr>
                        <td height="25" align="right" class="text_small"><span class="text_hui_11"><a href="view_configure_system.asp?change=true&cmd=print&id=<%=sys_tmp_sku%>" onClick="return js_callpage_cus(this.href, 'save_system_number', 602, 620)"  >To keep your configuration for future use, save and obtain System Number.</a></span></td>
                      </tr>
                    </table>
                    <table width="100%"  border="0" cellspacing="0" cellpadding="0">
                      <tr>
                        <td style="padding-bottom:5px; "><table width="100%"  border="0" cellpadding="3" cellspacing="0">
                          <tr>
                            <td width="70%">&nbsp;</td>
                            <td><table id="__01" width="100%" height="31" border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                  <td width="32"><img src="/soft_img/app/quest.gif" width="32" height="31" alt=""></td>
                                  <td style="background:url(/soft_img/app/save_title_02.gif); text-align:left"><a onClick="return js_callpage_cus(this.href,'question', 602,450)" href="<%= LAYOUT_HOST_URL %>ask_question.asp?cate=sys&id=<%=sys_tmp_sku%>&type=2&change=true" class="hui-red">Ask  a Question</a></td>
                                  <td width="9"><img src="/soft_img/app/save_title_03.gif" width="9" height="31" alt=""></td>
                                </tr>
                            </table></td>
                            
                            <td><table id="__01" width="100%" height="31" border="0" cellpadding="0" 

cellspacing="0" style="display:none">
                                <tr>
                                  <td width="32"><img src="images/save_title_01.gif" width="32" height="31" alt=""></td>
                
                                  <td background="images/save_title_02.gif">
							  	  </td>
                                  <td width="9"><img src="images/save_title_03.gif" width="9" height="31" alt=""></td>
                                </tr>
                            </table></td>
                          </tr>
                          <tr>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                          </tr>
                        </table></td>
                      </tr>
                    </table>
                    <table style="border-top:#e3e3e3 1px solid; " width="100%"  border="0" cellspacing="0" cellpadding="0">
                      <tr>
                        <td  style="padding:10px;text-align:left "><span class="text_hui_11">Prices, system package content and availability subject to change without notice. </span>
                          <p class="text_hui_11">Please read our FAQ for answers to most commonly asked questions. Any textual or pictorial information pertaining to products serves as a guide only. Lu Computers will not be held responsible for any information errata.</p></td>
                      </tr>
                    </table></td>
                </tr>
            </table>
            <DIV id="IconDiagram_Layer" style='width:172px; text-align:left;'>
              <table width="172" border="0" cellpadding="0" cellspacing="0"  style="background: url('/soft_img/app/fly_bg.gif') no-repeat;">
                  <tr>
                    <td height="260" valign="top"><table width="166" border="0" align="center" cellpadding="0" cellspacing="0">
                      <tr>                
                        <td height="30">&nbsp;</td>
                      </tr>
                      <tr>
                        <td style="border:#FDFBFA 1px solid; line-height:15px" height="30" bgcolor="#EFDACD">
                        	<table width="160" border="0" align="center" cellpadding="0" cellspacing="0">
                          <tr>
                            <td><span class="price" id="currentprice_1">$0</span> <span class="text_red2_11">Now Low Price</span></td>
                          </tr>                
                          <tr>
                            <td><span class="price" id="currentprice_2">$0</span> <span class="text_red2_11">Special Cash Price</span></td>
                          </tr>
                        </table></td>
                      </tr>
                      <tr>
                        <td height="5"></td>
                
                      </tr>
                      <tr>
                        <td><table width="160" border="0" align="center" cellpadding="1" cellspacing="1">
                          <tr>
                            <td class="text_white_11" style="border:#D69357 1px solid;" bgcolor="#CC792F">&nbsp;<a style="cursor:pointer; line-height:13px;" onClick="getSet(1);" class="btn_img">Select Major Components</a></td>
                          </tr>
                          <tr>
                            <td class="text_white_11" style="border:#D69357 1px solid;" bgcolor="#CC792F">&nbsp;<a style="cursor:pointer; line-height:13px;"  onClick="getSet(2);" class="btn_img">Select Accessories</a></td>
                          </tr>
                          <tr>
                            <td class="text_white_11" style="border:#D69357 1px solid;" bgcolor="#CC792F">&nbsp;<a style="cursor:pointer; line-height:13px;" onClick="getSet(3);" class="btn_img">Select Additional Parts</a></td>
                          </tr>
                         
                          <tr>
                            <td style="padding-left:5px;" class="text_white_11"><a onClick="js_callpage_cus('<%= LAYOUT_HOST_URL %>computer_system_quote.asp?1=1', 'system_quote', 450, 300);" class="btn_img" >Obtain Quote Number for<br />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;future reference.</a></td>
                          </tr>
                          
                        </table>
                          <table width="162" border="0" align="center" cellpadding="1" cellspacing="1">
                            <tr>
                              <td style="border:#D69357 1px solid;" bgcolor="#CC792F">&nbsp;<a href="<%= LAYOUT_HOST_URL %>ask_question.asp?cate=sys&type=2&change=true" style="cursor:pointer" class="white-hui-11"  onClick="return js_callpage_cus(this.href, 'question', 602, 450);">Ask seller a question</a></td>
                            </tr>
                            <tr>
                              <td style="border:#D69357 1px solid;" bgcolor="#CC792F">&nbsp;<a href="<%= LAYOUT_HOST_URL %>view_configure_system.asp?1=1" style="cursor:pointer" class="white-hui-11" onClick="return js_callpage_cus(this.href, 'view_system', 620, 620);">View my customized system</a></td>
                            </tr>
                
                            <tr>
                              <td style="border:#D69357 1px solid;" bgcolor="#CC792F">&nbsp;<a href="<%= LAYOUT_HOST_URL %>view_configure_system.asp?change=true&cmd=print" style="cursor:pointer" class="white-hui-11" onClick="return js_callpage_cus(this.href, 'view_system', 620, 620);">Print this customized system</a></td>
                            </tr>
                            <tr>
                              <td style="border:#D69357 1px solid;" bgcolor="#CC792F">&nbsp;<a href="<%= LAYOUT_HOST_URL %>view_configure_system.asp?change=true&cmd=email" style="cursor:pointer" class="white-hui-11" onClick="return js_callpage_cus(this.href, 'view_system', 620, 620);">Email this customized system</a></td>
                            </tr>
                          </table></td>
                      </tr>
                
                      <tr>
                        <td height="1"></td>
                      </tr>
                      <tr>
                        <td>
                        <table width="160" border="0" align="center" cellpadding="0" cellspacing="0">
                          <!--<tr>
                            <td style="padding-left:6px;" class="text_white_11"><a class="btn_img" onClick="window.location.replace('<%= LAYOUT_HOST_URL %>computer_system_to_cart.asp');">Arrange local pick up</a></td>
                          </tr>-->
                 <tr>
                        <td height="1"></td>
                      </tr>
                          <tr>
                            <td style="padding-left:6px;" class="text_white_11"><a class="btn_img" onClick="window.location.replace('<%= LAYOUT_HOST_URL %>computer_system_to_cart.asp');">Check shipping cost & total</a></td>
                          </tr>
                          
                        </table></td>
                      </tr>
                      <tr>
                        <td height="35" align="center"><a href="#"><img src="/soft_img/app/fly_add.gif" width="130" height="17" border="0" onClick="window.location.replace('<%= LAYOUT_HOST_URL %>computer_system_to_cart.asp');"/></a></td>
                      </tr>
                
                      
                    </table></td>
                  </tr>
                </table>
            </DIV>
<%
'
'	case sku
'
'

set rs = conn.execute("select max(product_serial_no) from (select p.menu_child_serial_no as product_category, p.product_serial_no from tb_product p  where product_serial_no in ("&GetConfigureProductIds()&")) a1 , (  select pc.* from tb_product_category pc , tb_computer_case cc where pc.menu_child_serial_no=cc.computer_case_category or pc.menu_pre_serial_no=cc.computer_case_category ) a2 where a1.product_category=a2.menu_child_serial_no ")
if not rs.eof then
	case_sku = rs(0)
end if
rs.close : set rs = nothing
'Response.write case_sku
CloseConn()
%>
<script type="text/javascript">
$().ready(function(){
	//$('#ifrmae1').attr("src", "/site/computer_system_save_configure_2.asp");
	//document.onload = __OnLoad_Diagram(); 
	$("#IconDiagram_Layer").floatdiv("customize");
	writeCaseImg('<%= case_sku %>');
});

</script>