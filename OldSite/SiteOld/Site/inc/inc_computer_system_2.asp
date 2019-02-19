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
	
	Dim skus			:		skus			=	"0"

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
	'response.write system_code
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
	
	
	if len(system_templete_serial_no)=0 and len(system_code) <> LAYOUT_SYSTEM_CODE_LENGTH then closeconn():response.Write(NO_DATA_MATCH):response.End()
	
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


              <table width="100%" height="670" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#FFFFFF" style="border:#8FC2E2 0px solid; ">
                <tr>
                  <td style="border:#E3E3E3 1px solid; " valign="top"><table width="100%"  border="0" cellspacing="0" 

cellpadding="0">
                    <tr>
                      <td width="400" valign="top"><table width="90%" height="90%"  border="0" align="center" cellpadding="0" 

cellspacing="0">
                        <tr>
                          <td width="227">
						 <div id="case_img_big">
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
								 
							   <tr bgcolor="#f2f2f2" class='no_save'>
								  <td width="35%" bgcolor="#f2f2f2" class="text_hui_11" style="text-align:left" id="page_main_sys_regular_text1"><b>&nbsp;Regular&nbsp;Price: </b></td>
								  <td align="left" bgcolor="#f2f2f2" class="text_hui_11" style="text-align:right"><b><span class="price_dis">$</span><span name="regular_price" class="price_dis"></span></b><span class="price_unit"></span></td>
								</tr>
								<tr bgcolor="#f2f2f2"  class='no_save'>
								  <td width="35%" bgcolor="#f2f2f2" class="text_hui_11" style="text-align:left"><strong>&nbsp;Discount: </strong></td>
								  <td align="left" bgcolor="#f2f2f2" style="text-align:right" class="price">
                                  		<strong>
                                        	<span class="price">-$</span><span class="price" name="discount_price"><%=formatnumber(old_save_cost)%></span>
                                        </strong><span class="price_unit"></span>
                                  </td>
								</tr>
							  
								<tr bgcolor="#f2f2f2" >
								  <td width="35%" bgcolor="#f2f2f2" class="text_hui_11" style="text-align:left" id="page_main_sys_regular_text2"><b>&nbsp;Now &nbsp;Low&nbsp;Price: </b></td>
								  <td align="left" bgcolor="#f2f2f2" class="text_hui_11" style="text-align:right"><strong><span class="price">$</span><span name="now_low_price" class="price"></span></strong><span class="price_unit"></span></td>
								</tr>
							  <tr bgcolor="#f2f2f2" >
								<td class="text_hui_11"><strong>&nbsp;Special&nbsp;Cash&nbsp;Price: </strong></td>
								<td class="text_hui_11" style="text-align:right" class="price"><strong><span class="price">$</span><span name="special_cash_price" class="price"></span></strong><span class="price_unit"></span></td>
							  </tr>							
								<tr bgcolor="#f2f2f2">
                                  <td valign="top" bgcolor="#f2f2f2" style='text-align:left'><strong>&nbsp;Quote Number: </strong></td>
                                  <td align="left" bgcolor="#f2f2f2" style='text-align:right'>&nbsp;<a onClick="return subConfigure(4);" style="cursor:pointer;">Press here to obtain</a></td>
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
                            <td style="text-align:left;">
                                <div style="font-size:8pt;"><% response.Write(FindSpecialCashPriceComment()) %>
									<div style="height:8px; line-height:8px;font-size: 1pt;">&nbsp;</div>
                                    <p>Every unique computer takes 1-7 days to be preassembled and tested before installed into the computer chassis. System includes meticulous hand assembly and precision cable routing. We tune system performance to its best and complete driver updates. All manufacturer documentations and disks are included.</p>
                                </div>      </td>
                          </tr>
                          <tr>
                            <td>&nbsp;
                            </td>
                          </tr>
                          <tr>
                            <td height="30" style='padding-right:5px;'>    
                            	<table id="__01" width="115" class="btn_table" height="24" border="0" cellpadding="0" cellspacing="0"  onClick="return subConfigure(2);" align="right">
                                    <tr>
                                      <td width="6"><img src="/soft_img/app/3232.gif" width="6" height="24"></td>
                                      <td class="btn_middle"><a href="#" onClick="return subConfigure(2);" class="white-hui-12"><strong>Print</strong></a></td>
                                      <td width="6"><img src="/soft_img/app/customer_bottom_04.gif" width="6" height="24" alt=""></td>
                                    </tr>
                                  </table>
</td>
                          </tr>
                      </table></td>
                    </tr>
                    <tr>
                      <td colspan="2">
					  
					  <form action="<%= LAYOUT_HOST_URL %>computer_system_save_configure_2.asp" method="post" name="form1" id="form1" target="iframe1">
						<input type="hidden" name="system_sku" id="system_sku" value="<%= system_templete_serial_no %>" />
                        <input type="hidden" name="cmd" value="customize" />
                        <input type="hidden" name="system_sell" value="0"/>
                        <input type="hidden" name="system_discount" value="0" />
                        <input type="hidden" value="1" name="is_view_system" />
                        <input type="hidden" name="system_code" value="<%= system_code %>" />
                    <table width="100%"  border="0" cellspacing="0" cellpadding="0">
                     <tr>
                        <td height="25">
							<div class="title1" style="cursor:hand; padding-top:3px; line-height:16px;" id="product_c_1" onClick="getSet(1);">	
								<strong>Major&nbsp;&nbsp;Components</strong>
							</div>
							<div class="title2" style="cursor:hand; padding-top:3px; line-height:16px;" id="product_c_2" onClick="getSet(2);" >	
								<strong>Accessories</strong>
							</div>
							<div class="title2" style="cursor:hand; padding-top:3px; line-height:16px;" id="product_c_3" onClick="getSet(3);" >	
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
			s_detail_tmp_sql	=	"(select part_quantity, part_max_quantity,system_product_serial_no,system_templete_serial_no, product_serial_no,1 showit,part_group_id  from tb_sp_tmp_detail where sys_tmp_code='"&system_code&"')"			
		else
			s_detail_tmp_sql = " ( select id system_product_serial_no,part_quantity,max_quantity part_max_quantity,part_group_id,luc_sku product_serial_no from tb_ebay_system_parts where system_sku='"&system_templete_serial_no&"') "
		end if		
		
		set rs = conn.execute("select sp.system_product_serial_no"&_
							" ,pg.part_group_id"&_
							" ,pg.part_group_name"&_
							" ,mc.menu_child_serial_no"&_
							" ,menu_child_name"&_
							" ,menu_pre_serial_no"&_
							" ,p.product_current_price"&_
							" ,sp.part_quantity"&_
							" ,sp.part_max_quantity"&_
							" ,p.product_name"&_
							" ,p.product_serial_no"&_
							" ,p.other_product_sku"&_
							" ,p.is_non"&_
							" "&_
							" ,p.tag"&_
							" ,p.product_current_discount"&_
							" ,p.product_current_cost"&_
							" from tb_part_group pg inner join tb_product_category mc on pg.product_category=mc.menu_child_serial_no "&_
							" inner join  "&s_detail_tmp_sql&" sp on  sp.part_group_id=pg.part_group_id "&_
							" inner join tb_product p on p.product_serial_no=sp.product_serial_no "&_
							" where  product_category in ("&c_list&") and pg.showit=1 and p.tag=1 and mc.tag=1 order by menu_child_order, sp.system_product_serial_no asc")
							

		if not rs.eof then
		area_group_count = 0
		'response.Write((timer() - begin_timer)&"<br>")
		dim product_serial_no
		dim part_quantity : part_quantity = 1
		dim part_max_quantity : part_max_quantity = 1
		do while not rs.eof			

			part_quantity 		= cint(rs("part_quantity"))
			part_max_quantity 	= cint(rs("part_max_quantity"))
			'
			'	把产品数据存到session
			'
			'
			    all_product_count = all_product_count + 1
			    area_group_count = area_group_count + 1

		rs.movenext
		loop

		dim templete_system_infos 
		Dim single_price
		Dim single_cost
		Dim single_save

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
				
				'response.write rs("product_current_price")
				single_price	=	ConvertDecimal(rs("product_current_price")) 
				single_save		=	ConvertDecimal(rs("product_current_discount"))
				single_cost		=	ConvertDecimal(rs("product_current_cost"))
				'response.write single_price
				
				current_price = cdbl(single_price) * part_quantity
				'response.Write(system_product_serial_no)

%>
				
				<div id="product_plane_<%=qn%>" >				
					<table  width="100%" height="22" border="0" cellpadding="0" cellspacing="0" bgcolor="#efefef" style="cursor:hand; border-top: 1px solid #cccccc; <%if area_count = area_group_count then response.Write("border-bottom: 1px solid #cccccc;")%>" id="plane_<%=qn%>_<%=area_count%>">
						<tr style="cursor:pointer;" onClick="displayProductGroup2('product_group_<%=rs("menu_child_serial_no")%>','product_check_<%=rs("menu_child_serial_no")%>','img_v_<%=rs("menu_child_serial_no")%>_<%=area_count%>','table_plane_group_<%=rs("menu_child_serial_no")%>_<%= plane_count%>', 'plane_<%=qn%>_<%=area_count%>', '<%if area_count = area_group_count then response.Write("1") else response.Write("0") %>','<%= system_product_serial_no %>',  'sub_group_detail_<%=system_product_serial_no%>', '<%= rs("part_max_quantity") %>', '<%= rs("part_quantity") %>', '<%=rs("product_serial_no")%>');">
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
                            	<input type="hidden" value="<%= rs("part_quantity") %>" id="current_part_quantity_<%= system_product_serial_no %>" name='current_part_quantity' />
								<input type="hidden" value="<%=rs("product_serial_no")%>" id="current_part_<%= system_product_serial_no %>" name="current_part">
                                <input type="hidden" value="<%= rs("part_max_quantity") %>" id="current_max_quantity_<%= system_product_serial_no %>" name="current_max_quantity"/>
                                <input type="hidden" value="<%= CommaEncode(rs("product_name")) %>" id="current_part_name_<%= system_product_serial_no %>" name="current_part_name"/>
                                <input type="hidden" value="<%= rs("part_group_id") %>" id="current_part_group_id_<%= system_product_serial_no %>" name="current_part_group" />
                                <input type="hidden" value="<%= rs("system_product_serial_no") %>" id="current_part_priority_<%= system_product_serial_no %>" name="current_part_priority" />
                                <input type="hidden" value="<%= single_price %>" id="current_part_price_<%= system_product_serial_no %>" name="current_part_price" />
                                <input type="hidden" value="<%= single_save %>" id="current_part_discount_<%= system_product_serial_no %>" name="current_part_discount" />
                                <input type="hidden" value="<%= single_cost %>" id="current_part_cost_<%= system_product_serial_no %>" name="current_part_cost" />
                                <input type="hidden" value="<%= system_product_serial_no %>" name="system_product_serial_no" />
                              
								<input type="hidden" value="img_product_<%= system_product_serial_no %>_<%=rs("product_serial_no")%>" id="current_img_logo_<%= system_product_serial_no %>">
								<input type="hidden" value="product_child_img_product_<%= system_product_serial_no %>_<%=rs("product_serial_no")%>" id="a_product_name_<%= system_product_serial_no %>">
                                <input type="hidden" value="<%=  rs ("part_group_name") %>" id="current_cate_name_<%= system_product_serial_no %>" name="cate_name"  title="<%=rs("product_serial_no")%>"/>
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
            
response.Write("</div>")

next
						%>
						
						</td>
                      </tr>
                      <tr>
                        <td></td>
                      </tr>
                    </table>
                   
					</form>
					
                      </td>
                    </tr>
                  </table>
                    <table width="100%"  border="0" cellspacing="0" cellpadding="0">
                      <tr>
                        <td height="40" align="right" style="color:#ff6600;font-weight:bold">
                        Updated Price : <span id="currentprice_3"><strong><span name="now_low_price" class="price"></span><span class="price_unit"></span></strong></span> 
                       
                        </td>
                      </tr>
                      <tr>
                        <td height="25" align="right" class="text_small">
						
						<table width="99%"  border="0" cellspacing="0" cellpadding="0" align="right">
                          <tr align="center">
						  	 <td width="155">
						  	
                            </td>
							 <td >
                             <table id="__01" width="155" height="24" border="0" cellpadding="0" cellspacing="0" class="btn_table" onClick="return subConfigure(1);">
                                <tr>
                                  <td width="28"><img src="/soft_img/app/Review.gif" width="28" height="24" alt=""></td>
                                  <td align="center" style="background:url(/soft_img/app/customer_bottom_03.gif)"><div id="view_system_print"><a class="btn_img"><strong>System Review</strong></a></div></td>
                                  <td width="6"><img src="/soft_img/app/customer_bottom_04.gif" width="6" height="24" alt=""></td>
                                </tr>
                            </table></td>
                            <td><table id="__01" width="115" height="24" border="0" cellpadding="0" cellspacing="0" class="btn_table" onclick="window.location.reload();">
                                <tr>
                                  <td width="28"><img src="/soft_img/app/reset.gif" width="28" height="24" alt=""></td>
                                  <td align="center" style="background:url(/soft_img/app/customer_bottom_03.gif)"><a class="btn_img"><strong>Reset</strong></a> </td>
                                  <td width="6"><img src="/soft_img/app/customer_bottom_04.gif" width="6" height="24" alt=""></td>
                                </tr>
                            </table></td>
                            <td><table id="__01" width="115" height="24" border="0" cellpadding="0" cellspacing="0" class="btn_table" onclick="customerSubmit();">
                                <tr>
                                  <td width="28"><img src="/soft_img/app/arrow_right.gif" width="28" height="24" alt=""></td>
                                  <td align="center" style="background:url(/soft_img/app/customer_bottom_03.gif)"><a   class="btn_img"><strong><span id="submit_button">Next</span></strong></a> </td>
                                  <td width="6"><img src="/soft_img/app/customer_bottom_04.gif" width="6" height="24" alt=""></td>
                                </tr>
                            </table></td>
                          </tr>
                        </table></td>
                      </tr>
                      <tr>
                        <td height="25" align="right" class="text_small"><span class="text_hui_11"><a onClick="return subConfigure(4);"  >To keep your configuration for future use, save and obtain System Number.</a></span></td>
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
                                  <td style="background:url(/soft_img/app/save_title_02.gif); text-align:left"><a onClick="return js_callpage_cus(this.href,'question', 652,500)" href="<%= LAYOUT_HOST_URL %>ask_question.asp?cate=sys&id=<%=sys_tmp_sku%>&type=2&change=true" class="hui-red">Ask  a Question</a></td>
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
                    </table>
                    </td>
                </tr>
            </table>
            <span id="sys_customize_float2">
            <DIV id="IconDiagram_Layer" style="width:172px; text-align:left;position:absolute; left: 0px; top: 0px; ">
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
                            <td><span id="currentprice_1" style="color:#ff6600;">$<span name="now_low_price" class="price1"></span><span class="price_unit"></span></span> <span class="text_red2_11">Regular Price</span></td>
                          </tr>                
                          <tr>
                            <td><span id="currentprice_2" style="color:#ff6600;">$<span name="special_cash_price" class="price1"></span><span class="price_unit"></span></span> <span class="text_red2_11">Special Cash Price</span></td>
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
                            <td style="padding-left:5px;" class="text_white_11"><a onClick="return subConfigure(4);" class="btn_img" >Obtain Quote Number for<br />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;future reference.</a></td>
                          </tr>
                          
                        </table>
                          <table width="162" border="0" align="center" cellpadding="1" cellspacing="1">
                            <tr>
                              <td style="border:#D69357 1px solid;" bgcolor="#CC792F">&nbsp;<a style="cursor:pointer" class="btn_img"  onClick="return subConfigure(5);">Ask seller a question</a></td>
                            </tr>
                            <tr>
                              <td style="border:#D69357 1px solid;" bgcolor="#CC792F">&nbsp;<a style="cursor:pointer" class="btn_img" onClick="return subConfigure(1);">View my customized system</a></td>
                            </tr>
                
                            <tr>
                              <td style="border:#D69357 1px solid;" bgcolor="#CC792F">&nbsp;<a style="cursor:pointer" class="btn_img" onClick="return subConfigure(2);">Print this customized system</a></td>
                            </tr>
                            <tr>
                              <td style="border:#D69357 1px solid;" bgcolor="#CC792F">&nbsp;<a  style="cursor:pointer" class="btn_img" onClick="return subConfigure(3);">Email this customized system</a></td>
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
                            <td style="padding-left:6px;" class="text_white_11"><a class="btn_img" onClick="buy();">Check shipping cost & total</a></td>
                          </tr>
                          
                        </table></td>
                      </tr>
                      <tr>
                        <td align="center"><br /><a href="#"><img src="/soft_img/app/fly_add.gif" width="130" height="17" border="0" onClick="buy();"/></a><br /><br /><br /></td>
                      </tr>
                
                      
                    </table>
                    
                    </td>
                  </tr>
                </table>
            </DIV>
            </span>
<div id="view_quote_area" style="display:none">
<table width="400" cellspacing="1" cellpadding="2"  border="0" bgcolor="#417CB6" align="center">
	<tr>
    	<td align="left"><div style="margin-top : 2px; margin-bottom : 2px; margin-left : 3px;"><span style='background : transparent; color : #FFFFFF; font-size : 18px; font-weight: bold;'>Save Quote</span></div></td></tr>
	<tr>
		<td width="100%" style="background:#ffffff">
		&nbsp;<P align="center">This Configuration is already saved.<P align=center> It can be accessed by Quote Number: <b style="font-size:16pt;" id="view_quote"></b>&nbsp;&nbsp;<p align=center><a onclick="$('#view_quote_area').css({'display':'none'});return false;" style="color:blue">CLOSE THIS WINDOW</a><P>&nbsp;

		</td>
	</tr>
</table>
</div>

<%
CloseConn()
%>
<iframe src="/site/blank.html" id="iframe1" name="iframe1" width="0" height="0" frameborder="0"></iframe>
<script type="text/javascript">
$().ready(function(){

	$('#sys_customize_float').html($('#sys_customize_float2').html());
	$('#sys_customize_float2').html('');
	
	customizeSystemStatPrice();
	$('span.price_unit').html('<%= CCUN %>');
	 __OnLoad_Diagram (); 
	$("#view_quote_area").floatdiv("middle");
	bindHoverBTNTable();
	viewCaseImg();

});

function viewCaseImg()
{
	$('input[name=cate_name]').each(function(){
		if($(this).val().toLowerCase() == "case")
		{
			writeCaseImg($(this).attr('title'));
		}
	
	});
	
}

function buy()
{
	$('input[name=is_view_system]').val(100);
	$('#form1').attr('target', 'iframe1').submit();
	$('input[name=is_view_system]').val(1);
}

function subConfigure(t)
{
	$('input[name=is_view_system]').val(t);
	$('#form1').attr('target', 'iframe1').submit();
}

function ViewQuote(quote)
{
	$('#view_quote_area').css("display", '');
	$('#view_quote').html(quote);
}

function viewAskQuestion(quote)
{
	return js_callpage_cus('<%= LAYOUT_HOST_URL %>ask_question.asp?cate=sys_8&type=2&id='+quote, 'ask_question', 622, 455);
}

</script>