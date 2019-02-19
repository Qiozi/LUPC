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


    Dim sysTitle1, sysTitle2, sysTitle3, sysTitle4, sysTitle5, sysTitle6
    set rs = conn.execute("select id,sku,title from tb_pre_index_page_setting where id<=6 and isys=1")
    if not rs.eof then
        do while not rs.eof 
            if(rs("id") = 1) then 
                product_list_sku(0) = rs("sku")
                sysTitle1 = rs("title")
            end if

            if(rs("id") = 2) then 
                product_list_sku(1) = rs("sku")
                sysTitle2 = rs("title")
            end if

            if(rs("id") = 3) then 
                product_list_sku(2) = rs("sku")
                sysTitle3 = rs("title")
            end if

            if(rs("id") = 4) then 
                product_list_sku(5) = rs("sku")
                sysTitle4 = rs("title")
            end if

            if(rs("id") = 5) then 
                product_list_sku(6) = rs("sku")
                sysTitle5 = rs("title")
            end if

            if(rs("id") = 6) then 
                product_list_sku(7) = rs("sku")
                sysTitle6 = rs("title")
            end if


        rs.movenext
        loop

    end if
    rs.close : set rs =nothing
	
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

        Response.Write("<a class=""text_hui_11"" style=""display: block;width: 220px;line-height:12px;font-size:8.5pt;white-space:nowrap;text-overflow:ellipsis;overflow:hidden;"" href="""& LAYOUT_HOST_URL &"view_part.asp?id="& id &""" onClick=""javascript:js_callpage_cus(this.href,'view_part', 622, 580);return false;"">")
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

<table width="100%" cellspacing="0" cellpadding="0" align="center" style="background:url(/soft_img/app/1_1_1.jpg);clear:both;border: 1px solid white;">
            <tr>
              <td style="height: 193px;">&nbsp;</td>
            </tr>
            <tr>
              <td valign="top" style="padding-left:2px;height: 260px;"><table width="100%"  border="0" cellspacing="0" cellpadding="0">
                <tr>
                        <td class="pre-sys-title">
                              <label><%= sysTitle1 %></label>
                        </td>
                        <td class="pre-sys-title">
                            <label><%= sysTitle2 %></label>
                        </td>
                        <td class="pre-sys-title">
                            <label><%= sysTitle3 %></label>
                        </td>
                       
                    </tr>
                <tr>
                  <td width="33%" valign="top">
                  <table width="88%" align="center" cellpadding="0" cellspacing="0">
                    
                    <tr>
                      <td class="system_part_name_area" style="height: 185px; padding-top:0.9em;">
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
                      <td><img src="/soft_img/app/line.jpg" width="228" height="6"></td>
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
									  	response.write "<span  class=""price_dis"">" & formatcurrency( cdbl(tmp_system_price_first), 2) &"</span>&nbsp;&nbsp;"
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
                    <td  class="system_part_name_area" style="height: 185px; padding-top:0.9em;"">
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
                    <td><img src="/soft_img/app/line.jpg" width="228" height="6"></td>
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
									  	response.write "<span  class=""price_dis"">" & formatcurrency( cdbl(tmp_system_price_first), 2) &"</span>&nbsp;&nbsp;"
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
                      <td class="system_part_name_area" style="height: 185px; padding-top:0.9em;""><%
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
                      <td><img src="/soft_img/app/line.jpg" width="228" height="6"></td>
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
									  	response.write "<span  class=""price_dis"">" & formatcurrency( cdbl(tmp_system_price_first), 2) &"</span>&nbsp;&nbsp;"
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
          <table width="100%"  border="0" align="center" cellpadding="0" cellspacing="0">
            <tr>
              <td height="4"></td>
            </tr>
          </table>
          <table width="100%" cellspacing="0" cellpadding="0" align="center"  style="background:url(/soft_img/app/2_2_2.jpg); border:1px solid white;">
            <tr>
             <td style="height: 193px;">&nbsp;</td>
            </tr>
            <tr>
              <td valign="top" style="height: 260px;">
              <table width="100%"  border="0" cellspacing="0" cellpadding="0">
              <tr>
                        <td class="pre-sys-title">
                              <label><%= sysTitle4 %></label>
                        </td>
                        <td class="pre-sys-title">
                            <label><%= sysTitle5 %></label>
                        </td>
                        <td class="pre-sys-title">
                            <label><%= sysTitle6 %></label>
                        </td>
                       
                    </tr>
                  <tr>
                    <td width="33%"  valign="top"><table width="88%"  border="0" align="center" cellpadding="0" cellspacing="0">
                        <tr>
                          <td  class="system_part_name_area" style="height: 185px;padding-top:0.9em;"><%
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
                          <td><img src="/soft_img/app/line.jpg" width="228" height="6"></td>
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
									  	response.write "<span  class=""price_dis"">" & formatcurrency( cdbl(tmp_system_price_first), 2) &"</span>&nbsp;&nbsp;"
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
                          <td  class="system_part_name_area" valign="top"  style="height: 185px;padding-top:0.9em;">
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
                          <td><img src="/soft_img/app/line.jpg" width="228" height="6"></td>
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
									  	response.write "<span  class=""price_dis"">" & formatcurrency( cdbl(tmp_system_price_first), 2) &"</span>&nbsp;&nbsp;"
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
                          <td  class="system_part_name_area" valign="top"  style="height: 185px;padding-top:0.9em;">
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
                          <td><img src="/soft_img/app/line.jpg" width="228" height="6"></td>
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
									  	response.write "<span  class=""price_dis"">" & formatcurrency( cdbl(tmp_system_price_first), 2) &"</span>&nbsp;&nbsp;"
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

          <div style="background:white;width:100%; height:600px;border:1px solid #ccc; margin:5px auto 5px  auto;">
              <div name='cate_title' style="height:38px; background:#ccc">
                <label name='pre_index_title_part_cate' class="pre-index-sys--cate-title-selected width120" id="pre_index_title_part_cate_350" onmouseover="show(350);">Laptop</label>
                <label name='pre_index_title_part_cate' class="pre-index-sys--cate-title " style='width:98px;'  id="pre_index_title_part_cate_22" onmouseover="show(22);">CPU</label>
                <label name='pre_index_title_part_cate' class="pre-index-sys--cate-title width120" id="pre_index_title_part_cate_31" onmouseover="show(31);">Motherboard</label>
                <label name='pre_index_title_part_cate' class="pre-index-sys--cate-title width120" id="pre_index_title_part_cate_25" onmouseover="show(25);">Hard Drive</label>
                <label name='pre_index_title_part_cate' class="pre-index-sys--cate-title width120" id="pre_index_title_part_cate_41" onmouseover="show(41);">Video Card</label>
                <label name='pre_index_title_part_cate' class="pre-index-sys--cate-title width80" id="pre_index_title_part_cate_21" onmouseover="show(21);">Cases</label>
                <label name='pre_index_title_part_cate' class="pre-index-sys--cate-title " style='width:88px;' id="pre_index_title_part_cate_28" onmouseover="show(28);">LCD</label>
              </div>
     
                <div style="clear:both;">
                    <%
                    
                        set rs = conn.execute("select p.product_current_price,p.product_current_discount, p.menu_child_serial_no,product_serial_no,"&_
                                            " case when length(ps.title)>5 then ps.title else product_ebay_name end as currTitle, "&_
                                            " case when other_product_sku>0 then other_product_sku else product_serial_no end as img_sku, "&_
                                            " product_ebay_name"&_
                                            " from tb_product p inner join tb_pre_index_page_setting ps on ps.sku=p.product_serial_no where tag=1 and ps.isys=0 order by ps.cateid, ps.priority asc")
                        if not rs.eof then

                            
                            dim currCate
                            dim currTitle
                            dim currPrice
                            dim currDiscount

                            i=0
                            do while not rs.eof 
                                i=i+1

                                if i <> 1 and currCate <> rs("menu_child_serial_no") then 
                                 response.Write "</ul>" &vblf

                                end if

                                if currCate <> rs("menu_child_serial_no") or i=1 then 
                                    if ( rs("menu_child_serial_no") = 350 ) then 
                                        response.Write "<ul class=""ul-table"" name='part_list' id='part_list_"& rs("menu_child_serial_no")&"'>"&vblf
                                    else
                                        response.Write "<ul class=""ul-table nodisplay"" name='part_list' id='part_list_"& rs("menu_child_serial_no")&"'>"&vblf
                                    end if

                                    currCate = rs("menu_child_serial_no")
                                end if

                                currTitle = rs("currTitle")
                                if rs("menu_child_serial_no") = 350 then 
                                    currTitle=rs("product_ebay_name")
                                end if

                                currPrice = rs("product_current_price")
                                currDiscount = rs("product_current_discount")
                                if cint(currDiscount) > 0 then 
                                    currPrice = cint(currPrice) - cint(currDiscount)
                                end if

                                logoUrl = "http://www.lucomputers.com/pro_img/ebay_gallery/"& left(rs("product_serial_no"),1) & "/"& rs("img_sku")& "_ebay_list_t_1.jpg"
                        
                                Response.Write "<li class=""pre-index-part-logo-detail borderwhite1px"" id='partShowArea'>" &vblf
                                Response.Write "    <div class=""pre-index-border-white"">"&vblf
                                Response.Write "    <div style='background-image: url("&logoUrl&"); background-repeat: no-repeat;cursor:pointer; background-position: center center;height: 160px;' class=""pre-index-part-detail-logo"" onclick='window.location.href=""/site/Product_parts_detail.asp?pro_class="&rs("menu_child_serial_no")&"&id="&rs("product_serial_no")&"&cid="&rs("menu_child_serial_no")&""";'>"&vblf
                               ' if i=2 then
                                '    Response.write "        <span class='showRebateTag'>Rebate <br/>$12.42</span>"&vblf                        
                                '    Response.write "        <span class='showOnSaleTag'>On Sale <br/>$132.42</span>"&vblf   
                                'elseif i=5 then
                                '    Response.write "        <span class='showOnSaleTag'>On Sale <br/>$132.42</span>"&vblf   
                               ' end if
                                Response.Write "    </div>"&vblf
                                Response.Write "    <div class=""font9pt textCenter clearBoth overflowHidden"" style='height:45px;'> <a class=""overflowHidden"" href=""/site/Product_parts_detail.asp?pro_class="&rs("menu_child_serial_no")&"&id="&rs("product_serial_no")&"&cid="&rs("menu_child_serial_no")&""" target=""_blank""> "& currTitle & "</a></div>"&vblf
                                Response.write "    <div class=""pre-index-part-detail-price-area"" style='height: 30px;text-align:center;'>"&vblf
                                if (cint(currDiscount)>0) then 
                                    Response.Write "    <span class=""price_dis""><b>$"& rs("product_current_price") &"</b></span>&nbsp;"&vblf
                                end if
                                Response.Write "        <span class=""price"" style='margin-right:20px;'>$"& currPrice &"</span><a href='/site/Shopping_Cart_pre.asp?cid="&rs("menu_child_serial_no")&"&Pro_Id="&rs("product_serial_no")&"'><img src='/soft_img/app/buynow_bt.gif' style=''></a></div>"&vblf

                                Response.Write "    </div>"&vblf
                                Response.Write "</li>"&vblf

                            rs.movenext
                            loop
                            response.Write "</ul>" &vblf
                        end if
                        rs.close : set rs = nothing
                   
                     %>
                </div>

          </div>
       <div id="pre-view-index-area" title="Index"></div>
 <script type="text/javascript">
     $().ready(function () {

         $('#menu_left_parent_1').css("display", "");
         
     });
     function show(cid) {
         $('ul[name=part_list]').each(function () {
             var the = $(this);
             the.removeClass("show").addClass("nodisplay");
         });
         $('label[name=pre_index_title_part_cate]').each(function () {
             var the = $(this);
             the.removeClass("pre-index-sys--cate-title-selected").addClass("pre-index-sys--cate-title");
             //the.addClass("pre-index-sys--cate-title");
         });
         $('#part_list_' + cid).removeClass("nodisplay").addClass("show");

         $('#pre_index_title_part_cate_' + cid).removeClass("nodisplay").addClass("pre-index-sys--cate-title-selected");

     }
</script>