<!--#include virtual="site/inc/inc_helper.asp"-->
<%

Dim product_notebook_sku(3)
	product_notebook_sku(0)	= 24507'23815'21121'21087'20215'18689'19113'17860'17533
	product_notebook_sku(1)	= 24715'23997'22899'20613'19930'19206'19203'17570'17577'16637
	product_notebook_sku(2)	= 24068'23938'20834'20316'19342'18645'18185'16958
	
Dim cctv_part_sku(3)
	cctv_part_sku(0)	=	20820'20549'20215'16007
	cctv_part_sku(1)	=	22480'22188'19930'16033
	cctv_part_sku(2)	=	20277'20821'20316'16043
	
dim product_list_sku(8)
	product_list_sku(0) = 217438'212820 '503046'2984
	product_list_sku(1) = 217374 '214622'2880
	product_list_sku(2) = 217439'213111    
	'product_list_sku(3) = 217440'214622
	'product_list_sku(4) = 217441'214622
	
	product_list_sku(5) = 217440'212995
	product_list_sku(6) = 217441'212979   '2883
	product_list_sku(7) = 213203
	
	 function sel_sql(sku)
        sel_sql = "select p.product_serial_no,p.product_short_name,p.product_name, sp.part_quantity  from tb_ebay_system_parts sp inner join tb_product p on p.product_serial_no=sp.luc_sku inner join tb_product_category pc on pc.menu_child_serial_no = p.menu_child_serial_no  INNER join tb_ebay_system st on st.id = sp.system_sku where"&_

		" ("&_
		" p.menu_child_serial_no in"&_
		" ("&_
		" select computers_memory_category 'category' from tb_computers_memory "&_
		" union all "&_
		" select computer_cpu_category from tb_computer_cpu "&_
		" union all"&_
		" select computer_case_category from tb_computer_case "&_
		" union all"&_
		" select computer_power_supply_category from tb_computer_power_supply "&_
		" union all"&_
		" select computer_motherboard_category from tb_computer_motherboard "&_
		" union all"&_
		" select computer_video_card_category from tb_computer_video_card"&_
		" union all"&_
		" select computers_hard_drive_category from tb_computers_hard_drive"&_
		" )"&_
		" or pc.menu_pre_serial_no in "&_
		" ("&_
		" select computers_memory_category 'category' from tb_computers_memory"&_
		" union all"&_
		" select computer_cpu_category from tb_computer_cpu"&_
		" union all"&_
		" select computer_video_card_category from tb_computer_video_card"&_
		" union all"&_
		" select computers_hard_drive_category from tb_computers_hard_drive"&_
		" )  "&_
		" )"&_
		" and "&_
		" sp.system_sku="&sku&" and p.is_non=0 order by sp.id asc "
      
    end function
	
    function short_name_str(id, name, quantity)
                Response.Write("<a class=""text_hui_11"" style=""display: block;width: 180px;line-height:12px;font-size:8pt;white-space:nowrap;text-overflow:ellipsis;overflow:hidden;"" href="""& LAYOUT_HOST_URL &"view_part.asp?id="& id &""" onClick=""javascript:js_callpage_cus(this.href,'view_part', 622, 580);return false;"">")
        if cstr(quantity) <> "1" then 
            name = quantity &"X "& name
        end if
        
        response.Write "• " & name &"</a>"
    end function
	
	function GerSelectedString(sku)
		dim rs, category
		set rs= conn.execute("select eBaySysCategoryID from tb_ebay_system_and_category where SystemSku="& sku)
		if not rs.eof then
			category = rs(0)
		end if
		rs.close : set rs = nothing
		GerSelectedString = "window.location.href='/site/system_view.asp?cid="& category &"&class=1&id="& sku &"'"
	end function
%>
<table width="600"  border="0" cellspacing="0" cellpadding="0" align="center">
            <tr>
              <td><img src="/soft_img/app/1_1.jpg" width="600" height="202"></td>
            </tr>
            <tr>
              <td valign="top" style="background:url(/soft_img/app/1_2.jpg) no-repeat; border: 0px solid red;" height="138"><table width="100%"  border="0" cellspacing="0" cellpadding="0">
                <tr>
                  <td width="33%" valign="top">
                  <table width="88%"  border="0" align="center" cellpadding="0" cellspacing="0">
                    <tr>
                      <td class="system_part_name_area">
					  	<%
							set rs = conn.execute(sel_sql(product_list_sku(0)))
                            if not rs.eof then 
                              '  response.Write("<ul>")
                                do while not rs.eof 
                                    short_name_str rs(0), rs(1) , rs("part_quantity")
                                rs.movenext
                                loop
                                'response.Write("</ul>")
                            end if
                            rs.close : set rs = nothing
						%>
					  </td>
                    </tr>
                    <tr>
                      <td><img src="/soft_img/app/line.jpg" width="178" height="6"></td>
                    </tr>
                    <tr>
                      <td>
                      <table width="100%"  border="0" cellspacing="0" cellpadding="4">
                        <tr>
                          <td class="text_red_12b">
						 <span class="price_big">
                          <%
							
							  dim price_and_save,tmp_system_save_price,tmp_system_price_first
							  price_and_save = GetSystemPriceAndSave(product_list_sku(0))
							  
							  tmp_system_save_price = splitConfigurePrice(price_and_save,1)
							  tmp_system_price_first = splitConfigurePrice(price_and_save,0)
							  tmp_system_price = cdbl(tmp_system_price_first) - cdbl(tmp_system_save_price)
									 
					 			if tmp_system_save_price <> 0 or rebate_save_price <> 0 then 		
									  	response.write "<span  class=""price_dis"">" & formatcurrency( cdbl(tmp_system_price_first), 2) &"</span>&nbsp;&nbsp;<br>"
								end if
								response.write  "<b>" & formatcurrency(tmp_system_price, 2) &"</b><span class='price_unit'>"& CCUN &"</span>"
								  
							%>	</span></td>
                        <td width="58"><img src="/soft_img/app/select.jpg" width="58" height="18" style="cursor:pointer" onClick="<%= GerSelectedString(product_list_sku(0)) %>"></td>
                        </tr>
                      </table></td>
                    </tr>
                  </table></td>
                <td width="33%" valign="top"><table width="88%"  border="0" align="center" cellpadding="0" cellspacing="0">
                  <tr>
                    <td  class="system_part_name_area">
					<%
							set rs = conn.execute(sel_sql(product_list_sku(1)))
                            if not rs.eof then 
                              '  response.Write("<ul>")
                                do while not rs.eof 
                                    short_name_str rs(0), rs(1) , rs("part_quantity")
                                rs.movenext
                                loop
                                'response.Write("</ul>")
                            end if
                            rs.close : set rs = nothing
						%></td>
                  </tr>
                  <tr>
                    <td><img src="/soft_img/app/line.jpg" width="178" height="6"></td>
                  </tr>
                  <tr>
                    <td><table width="100%"  border="0" cellspacing="0" cellpadding="4">
                        <tr>
                          <td class="text_red_12b">
						  <span class="price_big">
						  <%
						  	
							 price_and_save = GetSystemPriceAndSave(product_list_sku(1))
							  
							  tmp_system_save_price = splitConfigurePrice(price_and_save,1)
							  tmp_system_price_first = splitConfigurePrice(price_and_save,0)
							  tmp_system_price = cdbl(tmp_system_price_first) - cdbl(tmp_system_save_price)
								if tmp_system_save_price <> 0 or rebate_save_price <> 0 then 		
									  	response.write "<span  class=""price_dis"">" & formatcurrency( cdbl(tmp_system_price_first), 2) &"</span>&nbsp;&nbsp;<br>"
								end if
								response.write "<b>" & formatcurrency(tmp_system_price, 2) &"</b><span class='price_unit'>"& CCUN &"</span>"
								
%>						   </span></td>
                          <td width="58"><img src="/soft_img/app/select.jpg" width="58" height="18" style="cursor:pointer" onClick="<%= GerSelectedString(product_list_sku(1)) %>"></td>
                        </tr>
                    </table></td>
                  </tr>
                </table></td>
                  <td width="33%" valign="top"><table width="88%"  border="0" align="center" cellpadding="0" cellspacing="0">
                    <tr>
                      <td class="system_part_name_area"><%
							set rs = conn.execute(sel_sql(product_list_sku(2)))
                            if not rs.eof then 
                              '  response.Write("<ul>")
                                do while not rs.eof 
                                    short_name_str rs(0), rs(1) , rs("part_quantity")
                                rs.movenext
                                loop
                                'response.Write("</ul>")
                            end if
                            rs.close : set rs = nothing
						%></td>
                    </tr>
                    <tr>
                      <td><img src="/soft_img/app/line.jpg" width="178" height="6"></td>
                    </tr>
                    <tr>
                      <td><table width="100%"  border="0" cellspacing="0" cellpadding="4">
                          <tr>
                            <td class="text_red_12b"> 
							<span class="price_big">
							<%
							
							 price_and_save = GetSystemPriceAndSave(product_list_sku(2))
							  
							  tmp_system_save_price = splitConfigurePrice(price_and_save,1)
							  tmp_system_price_first = splitConfigurePrice(price_and_save,0)
							  tmp_system_price = cdbl(tmp_system_price_first) - cdbl(tmp_system_save_price)
								if tmp_system_save_price <> 0 or rebate_save_price <> 0 then 		
									  	response.write "<span  class=""price_dis"">" & formatcurrency( cdbl(tmp_system_price_first), 2) &"</span>&nbsp;&nbsp;<br>"
								end if
								response.write  "<b>" & formatcurrency(tmp_system_price, 2) &"</b><span class='price_unit'>"& CCUN &"</span>"
							
%>                              </span></td>
                            <td width="58"><img src="/soft_img/app/select.jpg" width="58" height="18" style="cursor:pointer" onClick="<%= GerSelectedString(product_list_sku(2)) %>"></td>
                          </tr>
                      </table></td>
                    </tr>
                  </table></td>
                </tr>
              </table>
                <!--<table width="100%"  border="0" cellspacing="0" cellpadding="0">
                  <tr>
                    <td><img src="/soft_img/app/1_21.jpg" width="600" height="13"></td>
                  </tr>
                </table>--></td>
            </tr>
          </table>
          <table width="600"  border="0" align="center" cellpadding="0" cellspacing="0">
            <tr>
              <td height="4"></td>
            </tr>
          </table>
          <table width="600"  border="0" cellspacing="0" cellpadding="0" align="center">
            <tr>
              <td><img src="/soft_img/app/1_4.jpg" width="600" height="202"></td>
            </tr>
            <tr>
              <td valign="top" style="background:url(/soft_img/app/1_2.jpg) no-repeat; border: 0px solid red;" height="138">
              <table width="100%"  border="0" cellspacing="0" cellpadding="0">
                  <tr>
                    <td width="33%"  valign="top"><table width="88%"  border="0" align="center" cellpadding="0" cellspacing="0">
                        <tr>
                          <td  class="system_part_name_area"><%
							set rs = conn.execute(sel_sql(product_list_sku(5)))
                            if not rs.eof then 
                              '  response.Write("<ul>")
                                do while not rs.eof 
                                    short_name_str rs(0), rs(1) , rs("part_quantity")
                                rs.movenext
                                loop
                                'response.Write("</ul>")
                            end if
                            rs.close : set rs = nothing
						%>
                          </td>
                        </tr>
                        <tr>
                          <td><img src="/soft_img/app/line.jpg" width="178" height="6"></td>
                        </tr>
                        <tr>
                          <td><table width="100%"  border="0" cellspacing="0" cellpadding="4">
                              <tr>
                                <td class="text_red_12b"><span class="price_big">
                                  <%
						
							
							 price_and_save = GetSystemPriceAndSave(product_list_sku(5))
							  
							  tmp_system_save_price = splitConfigurePrice(price_and_save,1)
							  tmp_system_price_first = splitConfigurePrice(price_and_save,0)
							  tmp_system_price = cdbl(tmp_system_price_first) - cdbl(tmp_system_save_price)
								if tmp_system_save_price <> 0 or rebate_save_price <> 0 then 		
									  	response.write "<span  class=""price_dis"">" & formatcurrency( cdbl(tmp_system_price_first), 2) &"</span>&nbsp;&nbsp;<br>"
								end if
								response.write  "<b>" & formatcurrency(tmp_system_price, 2) &"</b><span class='price_unit'>"& CCUN &"</span>"
									  
							%>
                                </span></td>
                                <td width="58"><img src="/soft_img/app/select.jpg" width="58" height="18" style="cursor:pointer" onClick="<%= GerSelectedString(product_list_sku(5)) %>"></td>
                              </tr>
                          </table></td>
                        </tr>
                    </table></td>
                    <td width="33%" valign="top"><table width="88%"  border="0" align="center" cellpadding="0" cellspacing="0">
                        <tr>
                          <td  class="system_part_name_area" valign="top">
                            <%
							set rs = conn.execute(sel_sql(product_list_sku(6)))
                            if not rs.eof then 
                              '  response.Write("<ul>")
                                do while not rs.eof 
                                    short_name_str rs(0), rs(1) , rs("part_quantity")
                                rs.movenext
                                loop
                                'response.Write("</ul>")
                            end if
                            rs.close : set rs = nothing
						%>
                         </td>
                        </tr>
                        <tr>
                          <td><img src="/soft_img/app/line.jpg" width="178" height="6"></td>
                        </tr>
                        <tr>
                          <td><table width="100%"  border="0" cellspacing="0" cellpadding="4">
                              <tr>
                                <td class="text_red_12b"><span class="price_big">
                                  <%
						
							 price_and_save = GetSystemPriceAndSave(product_list_sku(6))
							  
							  tmp_system_save_price = splitConfigurePrice(price_and_save,1)
							  tmp_system_price_first = splitConfigurePrice(price_and_save,0)
							  tmp_system_price = cdbl(tmp_system_price_first) - cdbl(tmp_system_save_price)
								if tmp_system_save_price <> 0 or rebate_save_price <> 0 then 		
									  	response.write "<span  class=""price_dis"">" & formatcurrency( cdbl(tmp_system_price_first), 2) &"</span>&nbsp;&nbsp;<br>"
								end if
								response.write  "<b>" & formatcurrency(tmp_system_price, 2) &"</b><span class='price_unit'>"& CCUN &"</span>"
									  
							%>
                                </span></td>
                                <td width="58"><img src="/soft_img/app/select.jpg" width="58" height="18" style="cursor:pointer" onClick="<%= GerSelectedString(product_list_sku(6)) %>"></td>
                              </tr>
                          </table></td>
                        </tr>
                    </table></td>
                    <td width="33%" valign="top"><table width="88%"  border="0" align="center" cellpadding="0" cellspacing="0">
                        <tr>
                          <td  class="system_part_name_area" valign="top">
                            <%
							set rs = conn.execute(sel_sql(product_list_sku(7)))
                            if not rs.eof then 
                              '  response.Write("<ul>")
                                do while not rs.eof 
                                    short_name_str rs(0), rs(1) , rs("part_quantity")
                                rs.movenext
                                loop
                                'response.Write("</ul>")
                            end if
                            rs.close : set rs = nothing
						%>
                        </td>
                        </tr>
                        <tr>
                          <td><img src="/soft_img/app/line.jpg" width="178" height="6"></td>
                        </tr>
                        <tr>
                          <td><table width="100%"  border="0" cellspacing="0" cellpadding="4">
                              <tr>
                                <td class="text_red_12b"><span class="price_big">
                                  <%
				
							 price_and_save = GetSystemPriceAndSave(product_list_sku(7))
							  
							  tmp_system_save_price = splitConfigurePrice(price_and_save,1)
							  tmp_system_price_first = splitConfigurePrice(price_and_save,0)
							  tmp_system_price = cdbl(tmp_system_price_first) - cdbl(tmp_system_save_price)
								if tmp_system_save_price <> 0 or rebate_save_price <> 0 then 		
									  	response.write "<span  class=""price_dis"">" & formatcurrency( cdbl(tmp_system_price_first), 2) &"</span>&nbsp;&nbsp;<br>"
								end if
								response.write  "<b>" & formatcurrency( tmp_system_price, 2) &"</b><span class='price_unit'>"& CCUN &"</span>"
							 		  
							%>
                                </span></td>
                                <td width="58"><img src="/soft_img/app/select.jpg" width="58" height="18" style="cursor:pointer" onClick="<%= GerSelectedString(product_list_sku(7)) %>"></td>
                              </tr>
                          </table></td>
                        </tr>
                    </table></td>
                  </tr>
                </table>
                  </td>
            </tr>
          </table>
          <table width="600"  border="0" align="center" cellpadding="0" cellspacing="0">
            <tr>
              <td height="4"></td>
            </tr>
          </table>
          <table width="600"  border="0" cellspacing="0" cellpadding="0"  align="center">
            <tr>
              <td><img src="/soft_img/app/3_1.jpg" width="600" height="202" /></td>
            </tr>
            <tr>
              <td style="background:url(/soft_img/app/1_2.jpg)  no-repeat"><table width="100%"  border="0" cellspacing="0" cellpadding="0">
                <tr>
                  <td width="33%" ><table width="88%"  border="0" align="center" cellpadding="0" cellspacing="0">
                      <tr>
                        <td align="left" valign="top" class="text_hui_11" style="padding:4px; padding-bottom:4px; line-height:12px;">Ultrabook/Tablet: Intel i5-3317U, 4GB RAM, 128G SSD Win8, Touch<br />
                         
  <br />        <br />
                    
                                      </td>
                    </tr>
                      <tr>
                        <td><img src="/soft_img/app/line.jpg" width="178" height="6"></td>
                      </tr>
                      <tr>
                        <td><table width="100%"  border="0" cellspacing="0" cellpadding="4">
                            <tr>
                              <td class="text_red_12b">
                               <%
							 dim single_save_price, part_current_price
							 single_save_price = GetSavePrice(product_notebook_sku(0))
							 part_current_price = cdbl(GetPartCurrentPrice(product_notebook_sku(0)))
							
							
							  if cdbl(single_save_price) <> 0 then
									response.write  "<span style='text-decoration:line-through;color: #cccccc;'>"&  ConvertDecimalUnit(current_system, part_current_price) & "</span><br/>"
									response.write  ConvertDecimalUnit(current_system, part_current_price - single_save_price)
							  else
									response.write  ConvertDecimalUnit(current_system, part_current_price) 
							  end if
												
												  %>                              </td>
                              <td width="58"><a href="product_parts_detail.asp?class=2&pro_class=90&id=<%= product_notebook_sku(0) %>&cid=350"><img src="/soft_img/app/select.jpg" width="58" height="18" border="0" ></a></td>
                            </tr>
                        </table></td>
                      </tr>
                  </table></td>
                  <td width="33%"><table width="88%"  border="0" align="center" cellpadding="0" cellspacing="0">
                      <tr>
                        <td align="left" class="text_hui_11" style="padding:4px; padding-bottom:4px; line-height:12px;" valign="top">
                            Ultrabook/Tablet: 
                            IDEA ENG YOGA13&nbsp; Intel Core i5-3337U, 
                            4GB RAM, 128G SSD, 13.3&quot; Touch, W8
                          <br /> 
                         <br /> 
                         </td>   	
                    </tr>
                      <tr>
                        <td><img src="/soft_img/app/line.jpg" width="178" height="6"></td>
                      </tr>
                      <tr>
                        <td><table width="100%"  border="0" cellspacing="0" cellpadding="4">
                            <tr>
                              <td class="text_red_12b"> <%
												 
												 single_save_price = GetSavePrice(product_notebook_sku(1))
												 part_current_price = cdbl(GetPartCurrentPrice(product_notebook_sku(1)))

												   if cint(single_save_price) <> 0 then
												  		response.write  "<span style='text-decoration:line-through;color: #cccccc;'>"&  ConvertDecimalUnit(current_system, part_current_price) & "</span><br/>"
									 			  		response.write  ConvertDecimalUnit(current_system, part_current_price - single_save_price)
												  else
														response.write  ConvertDecimalUnit(current_system, part_current_price) 
												  end if
												
												  %></td>
                              <td width="58"><a href="product_parts_detail.asp?class=2&pro_class=306&id=<%= product_notebook_sku(1) %>&cid=350"><img src="/soft_img/app/select.jpg" width="58" height="18" border="0" ></a></td>
                            </tr>
                        </table></td>
                      </tr>
                  </table></td>
                  <td width="33%"><table width="88%"  border="0" align="center" cellpadding="0" cellspacing="0">
                      <tr>
                        <td align="left" class="text_hui_11" style="padding:4px; padding-bottom:4px; line-height:12px;" valign="top"><p>Ultrabook: Intel Core i5-3317U, 
                            4GB 
                            RAM,</br>128G SSD, 13.3&quot; LED, W8P<br />
                            <br />
                          </p></td>
                    </tr>
                      <tr>
                        <td><img src="/soft_img/app/line.jpg" width="178" height="6"></td>
                      </tr>
                      <tr>
                        <td><table width="100%"  border="0" cellspacing="0" cellpadding="4">
                            <tr>
                              <td class="text_red_12b"><%
												 
												 single_save_price = GetSavePrice(product_notebook_sku(2))
												 part_current_price = cdbl(GetPartCurrentPrice(product_notebook_sku(2)))

												  if cint(single_save_price) <> 0 then
												  		response.write  "<span style='text-decoration:line-through;color: #cccccc;'>"&  ConvertDecimalUnit(current_system, part_current_price) & "</span><br/>"
												  		response.write  ConvertDecimalUnit(current_system, part_current_price - single_save_price)
												  else
														response.write  ConvertDecimalUnit(current_system, part_current_price) 
												  end if
												
												  %></td>
                              <td width="58"><a href="product_parts_detail.asp?class=2&pro_class=88&id=<%= product_notebook_sku(2) %>&cid=350"><img src="/soft_img/app/select.jpg" width="58" height="18" border="0" style="cursor:pointer" ></a></td>
                            </tr>
                        </table></td>
                      </tr>
                  </table></td>
                </tr>
              </table>
                <table width="100%"  border="0" cellspacing="0" cellpadding="0">
                  <tr>
                    <td><img src="/soft_img/app/1_21.jpg" width="600" height="13"></td>
                  </tr>
                </table></td>
            </tr>
          </table>
          <table width="600"  border="0" align="center" cellpadding="0" cellspacing="0">
            <tr>
              <td height="4"></td>
            </tr>
          </table>
          
<table width="600"  border="0" align="center" cellpadding="0" cellspacing="0">
            <tr>
              <td height="5"></td>
            </tr>
 </table>
 <script type="text/javascript">
     $().ready(function () {

         $('#menu_left_parent_1').css("display", "");
     });
</script>