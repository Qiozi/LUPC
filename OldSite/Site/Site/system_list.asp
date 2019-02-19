<!--#include virtual="site/inc/inc_page_top.asp"-->
<%
						Dim rsc
						Dim rscount
						Dim cpu_logo_path
						Dim cpurs2
						Dim countrs
						
						dim return_path, page_return_path
						page_return_path = SQLescape(request("page_return_path"))
						return_path =  SQLescape(request("return_path"))
						
						
						dim product_serial_nos, category, page_nav_title, page_nav, system_id
						product_serial_nos = SQLescape(request("system_ids"))
						page_nav_title	= SQLescape(request("page_title"))
						system_id = SQLescape(request("system_id"))
						
						if product_serial_nos <> "" then 
							product_serial_nos = replace(product_serial_nos, "|", ",")
						else
							product_serial_nos = "0"
						end if
						
						if page_nav_title <> "" then 
							page_nav_title = replace (page_nav_title, "%", " ")
						end if
						
						page_nav = "&nbsp;&nbsp;<img src=""/soft_img/app/arrow_8.gif"" width=""11"" height=""10"">&nbsp;&nbsp;<a href=""default.asp"" class=""white-red-11""><strong>Home</strong></a>&nbsp;&nbsp;<img src=""/soft_img/app/arrow_8.gif"" width=""11"" height=""10"">&nbsp;&nbsp;<a href='"& page_return_path &"' class='white-red-11'> <strong>"& return_path &"</strong></a>&nbsp;&nbsp;<img src=""/soft_img/app/arrow_8.gif"" width=""11"" height=""10"">&nbsp;&nbsp;<strong>"&page_nav_title&"</strong>"
						
						
					  %>
<table class="page_main" cellpadding="0" cellspacing="0">
	<tr>
    	<td  id="page_main_left" valign="top" style="padding-bottom:5px; padding-left: 2px" class='page_frame'>
        	<!-- left begin -->
            	<!--#include virtual="site/inc/inc_left_menu.asp"-->
            <!-- left end 	-->
    	</td>
    	
        <td id="page_main_center" valign="top" style="width:600px" class='page_frame'>
        	<!-- main begin -->
        	    <div id="page_main_banner"></div>
        	    <div class='page_main_nav' id="page_main_nav">
                	<span class="nav1"><a href='<%= LAYOUT_HOST_URL %>'>home</a></span>
                    <span class="nav1"><a href='<%= page_return_path %>'> <strong><%= return_path %></strong></a></span>
                    <span class="nav1"><%= page_nav_title%></span>
                </div>
            	<div id="page_main_area">
					
                             <table width="600" height="755" border="0" align="center" cellpadding="0" cellspacing="2" bgcolor="#FFFFFF" style="border:#8FC2E2 1px solid; ">
                                            <tr>
                                              <td style="border:#E3E3E3 1px solid;  padding-top:0px;" valign="top"><table height="20"  border="0" align="center" cellpadding="4" cellspacing="0" >
                                              <form action="system_list.asp">
                                                 <input type="hidden" value="<%= SQLescape(request("system_ids")) %>" name="system_ids">
                                                 <input type="hidden" value="<%= page_nav_title %>" name="page_title">
                                                 <input type="hidden" value="<%= return_path %>" name="return_path">
                                                 <input type="hidden" value="<%= page_return_path %>" name="page_return_path">
                              <tr>
                              
                                <td align="center" style="padding-bottom:12px; ">
                            
                            
                               <select class="b" name="system_id" >
                                    <option value="-1">--All--</option>
                                    <%
                                    ' system name
                                    
                                        set rs = conn.execute("select id, ebay_system_name,(select sum(p.product_current_price-p.product_current_discount) from tb_ebay_system_parts sp inner join tb_product p on p.product_serial_no = sp.luc_sku where p.tag=1 and system_sku=st1.id) price from tb_ebay_system st1 where id in ("& product_serial_nos &") and showit=1 order by tmp_sell asc  ") 
                                        if not rs.eof then
                                            do while not rs.eof 
                                    %>
                                                <option value="<%= rs("id") %>" 
                                                <% 	if system_id = cstr(rs("id")) then 
                                                        response.write " selected='true'"
                                                    end if
                                                %>
                                                >
                                                <% 
                                                    tmp_system_price = rs("tmp_sell")
                                                    response.write rs("ebay_system_name") &"["
                                                    'if cint(GetSystemSaveCost(rs("system_templete_serial_no"))) <> 0 then 
                            '					
                            '						
                            '						response.write  formatcurrency(cdbl(tmp_system_price) ) 
                            '						'response.write  formatcurrency(cdbl(tmp_system_price) -GetSystemSaveCost(rs("system_templete_serial_no")))
                            '						  else
                                                        response.write formatcurrency(cdbl(tmp_system_price) )
                                                     ' end if
                                                     response.write "]" %>  </option>
                                    <%
                                            rs.movenext
                                            loop			
                                        end if
                                    
                                    %>
                                </select> 
                            
                                  </td>
                            
                                <td align="center" style="padding-bottom:12px; "><input name="imageField2" type="image" src="/soft_img/app/go5.jpg" width="39" height="20" border="0"></td>
                            
                              </tr>
                              </form>
                            </table>
  
			  <%  	
							
						dim ps,rspagecount
							ps = 15
							
							set rsc = conn.execute("select count(id) from tb_ebay_system where showit=1 ")
							
							if SQLescape(request("keywords")) <> "" then ps = 10
							rspagecount=-int(-rsc(0)/ps)
							rscount = rsc(0)
							page=SQLescape(request("page"))
							if not isnumeric(page) or page = "" then page=1
							page=cint(page)
							if page>rspagecount then page=rspagecount
							if page<1 then page=1
						'response.write "select * from tb_system_templete where tag=1 and system_templete_serial_no="&cint(keyword) 
							dim system_cpu_logo_filename,system_cpu_logo_filename_vc, logo_image_filename_sys, logo_count
							system_cpu_logo_filename = ""
							system_cpu_logo_filename_vc = ""
							
							dim sql_where_params
							sql_where_params = ""
							if system_id <> "" and system_id <> "-1" then 
								sql_where_params = " and id in ("&system_id&") "
							end if
							set rs= conn.execute("select st1.*,(select sum(p.product_current_price) from tb_ebay_system_parts sp inner join tb_product p on p.product_serial_no = sp.luc_sku where p.tag=1 and system_sku=st1.id) price from tb_ebay_system st1 where id in ("& product_serial_nos &") and showit=1 "&sql_where_params&" order by tmp_sell asc  limit "& ps*(page-1) &","&ps)
				
							if not rs.eof then
							logo_count = 0
							do while not rs.eof 
							logo_count = 0
							'//system_cpu_logo_filename = rs("logo_image_filename")
							'/system_cpu_logo_filename_vc =  rs("logo_image_filename_vc")
							'logo_image_filename_sys = rs("logo_image_filename_sys")
							
							if len(system_cpu_logo_filename )  > 4 then 
								logo_count = logo_count + 1
							end if
							if len(system_cpu_logo_filename_vc )  > 4 then 
								logo_count = logo_count + 1
							end if
							if len(logo_image_filename_sys )  > 4 then 
								logo_count = logo_count + 1
							end if
					%>
                              <table width="100%"  border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                  <td width="22%" valign="top"><table width="100"  border="0" align="center" cellpadding="0" cellspacing="0">
                                      <tr>
                                        <td><a href="<%= LAYOUT_HOST_URL %>system_view.asp?cid=<%= request("id") %>&id=<%=rs("id")%>&class=<%=request("class")%>"><img src="<%=GetSystemPhotoByID2(rs("id"))%>"  border="0"></a></td>
                                      </tr>
                                      <tr>
                                        <td align="center" class="text_blue_13">[ <%= rs("id") %> ]</td>
                                      </tr>
                                  </table></td>
                                  <td valign="top"><table width="100%"  border="0" cellspacing="0" cellpadding="0">
                                      <tr>
                                        <td>
                                          <table width="100%"  border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                              <%
                                         response.Write( "<td style=""padding-bottom:3px;")
                                        
                                        response.write "width: "& logo_count * 40& "px;"
                                        
                                        response.write """>"
                                        
                                         if system_cpu_logo_filename <> "" then 
                                            response.write "&nbsp;<img  width=""31"" height=""35"" src="""&HTTP_PART_GALLERY_CPU_LOGO_PATH& system_cpu_logo_filename &""">"
                                            
                                         end if								
                                        
                                         if system_cpu_logo_filename_vc <> "" then 
                                            response.Write "&nbsp;<img  width=""31"" height=""35"" src="""&HTTP_PART_GALLERY_CPU_LOGO_PATH& system_cpu_logo_filename_vc &""">"
                                         end if
                                         
                                          if len(logo_image_filename_sys) > 4 then 
                                             response.Write "&nbsp;<img  width=""31"" height=""35"" src="""&HTTP_PART_GALLERY_CPU_LOGO_PATH& logo_image_filename_sys &""">"
                                         end if
                                        
                                        response.write ("</td>")
                                           

												response.Write("<td style='min-height: 40px;'><a  href=""/site/system_view.asp?cid="& request("id") &"&id="& rs("id")&"&class="&request("class")&"""><span id='logo_cpu_name'  class=""system_title100"" >")
													
												if trim(rs("ebay_system_name")) <> "" then 
														response.Write rs("ebay_system_name")
												else
													set cpurs2 = conn.execute("select p.product_serial_no, p.product_short_name,p.menu_child_serial_no from tb_ebay_system_parts sp inner join tb_product p on   sp.luc_sku=p.product_serial_no where p.tag=1 and p.is_non=0 and system_sku="& SQLquote(rs("id"))&" and menu_child_serial_no in (select menu_child_serial_no from tb_product_category where tag=1 and  menu_pre_serial_no in (select computer_cpu_category from tb_computer_cpu)) limit 0,1")
													if not cpurs2.eof then 
															response.Write cpurs2("product_short_name")	
													end if
													cpurs2.close :set cpurs2 = nothing									
													End if
												response.Write("&nbsp;System</span></a></td>")	
        
                                      %>
                                        
                                            </tr>
                                        </table></td>
                                      </tr>
                                      <tr>
                                        <td height="10"><table width="100%" height="1"  border="0" cellpadding="0" cellspacing="0">
                                            <tr>
                                              <td background="/soft_img/app/line2.gif"><img src="/soft_img/app/line2.gif" width="3" height="1"></td>
                                            </tr>
                                        </table></td>
                                      </tr>
                                      <tr>
                                        <td valign="top" class="text_hui_11">
                                          <%
        
                                    ' part count
                                    dim sys_product_order_sum
                                    set countrs  = conn.execute("select count(s.system_sku) from tb_ebay_system_parts s,tb_product p where s.system_sku="&rs("id")&" and p.tag=1 and (p.is_non=0 or p.product_name like '%onboard%') and p.menu_child_serial_no not in("&LAYOUT_none_display_product_category&") and p.product_serial_no=s.luc_sku order by s.id asc")
                                    sys_product_order_sum = countrs(0)
                                    set countrs = nothing
                                    
                                    ' read part info
                                    set crs = conn.execute("select p.product_short_name, p.product_serial_no, s.part_quantity from tb_ebay_system_parts s,tb_product p where s.system_sku="&rs("id")&" and p.tag=1 and (p.is_non=0 or p.product_name like '%onboard%') and p.menu_child_serial_no not in("&LAYOUT_none_display_product_category&") and p.product_serial_no=s.luc_sku order by s.id asc")
                                    
                                    if not crs.eof then
                                        response.write "<table width=""100%""><tr><td width=""50%"" height=""20px;"" valign='top'>"
                                        dim sys_product_order , is_split_table
                                        sys_product_order = 0 
                                         is_split_table = 0
                                        do while not crs.eof
                                        sys_product_order = sys_product_order + 1
                                        
                                        if is_split_table = 1 then 
                                            response.write "</td><td valign='top'>"
                                            is_split_table = 0
                                        end if
                                        if sys_product_order = (int(sys_product_order_sum /2) + (sys_product_order_sum mod 2) ) then 
                                            is_split_table = 1
                                        end if
                                        
                                    %>
                                          <div class='system_part'>
                                          <% if crs("part_quantity")>1 then response.Write crs("part_quantity") & "X " %>
                                          	 <a class="hui-orange-s" href="View_part.asp?id=<%= crs("product_serial_no")%>" onClick="javascript:js_callpage_cus(this.href, 'view_part', 602,600);return false;"><%= crs("product_short_name") %> </a></div>
                                          <%
                                        crs.movenext
                                        loop
                                        response.write ("</td></tr></table>")
                                    end if
                                    crs.close:set crs = nothing
                                  %></td>
                                      </tr>
                                      <tr>
                                        <td><table  border="0" align="right" cellpadding="2" cellspacing="0">
                                            <tr>
                                              <td class="text_red_12b" style="padding-right:8px;">
                                              <span class="price_big">
                                              <%
                                               dim tmp_system_price, tmp_system_save_price, tmp_system_price_first
        
                                               dim price_and_save
                                              price_and_save = GetSystemPriceAndSave(rs("id"))
                                              
                                              tmp_system_save_price = splitConfigurePrice(price_and_save,1)
                                              tmp_system_price_first = splitConfigurePrice(price_and_save,0)
                                              tmp_system_price = cdbl(tmp_system_price_first) - cdbl(tmp_system_save_price)
                                             
                                              if tmp_system_save_price <> 0 then 		
                                                response.write "<span   class=""price_dis"">" & formatcurrency(cdbl(tmp_system_price_first),2) &"</span>&nbsp;&nbsp;"
                                              end if
                                              response.write formatcurrency( tmp_system_price, 2)
                                    %><span class="price_unit"><%= CCUN %>"</span>
                                              </span>
                                              </td>
                                              <td width="65"><a href="<%= LAYOUT_HOST_URL %>system_view.asp?cid=<%= request("id") %>&class=<%=request("class")%>&id=<%=rs("id")%>"><img src="/soft_img/app/select_bt.gif" width="56" height="13" border="0"></a></td>
                                            </tr>
                                        </table></td>
                                      </tr>
                                  </table></td>
                                </tr>
                                <tr>
                                  <td height="20" colspan="2"><table width="100%"  border="0" cellpadding="0" cellspacing="1" bgcolor="#D0DAE1">
                                      <tr>
                                        <td height="3" bgcolor="#FFFFFF"></td>
                                      </tr>
                                  </table></td>
                                </tr>
                              </table>
                              <%
                            rs.movenext:loop
                            end if
                            rs.close : set rs = nothing
                         
                        %>
                              <br>
                           
                                   <table width="100%" height="50"  border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                  <td align="right">
                                  </td>
                                </tr>
                            </table>
                          </td>
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
	//$('#page_main_area').load('/site/inc/inc_default.asp');
});
</script>